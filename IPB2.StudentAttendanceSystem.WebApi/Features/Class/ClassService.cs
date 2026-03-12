using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Class
{
    public class ClassService : IClassService
    {
        private readonly AppDbContext _dbContext;

        public ClassService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IQueryable<TblClass> ClassQuery()
        {
            IQueryable<TblClass> query = _dbContext.TblClasses
                   //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        private IQueryable<TblSchedule> ScheduleQuery()
        {
            IQueryable<TblSchedule> query = _dbContext.TblSchedules
                   //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        private IQueryable<TblTeacher> TeacherQuery()
        {
            IQueryable<TblTeacher> query = _dbContext.TblTeachers
                   //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        public async Task<List<ClassModel>> GetAllClassAsync(int pageNo, int pageSize)
        {
            var result = await ClassQuery()
                .OrderByDescending(x => x.ClassName)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ClassModel
                {
                    Id = x.Id,
                    ClassName = x.ClassName,
                    StartDate = x.StartDate,
                    Duration = x.Duration,
                    ScheduleId = x.ScheduleId,
                    TeacherId = x.TeacherId
                }).ToListAsync();
            return result;
        }
        public async Task<ServiceResponse> SaveClassAsync(CreateClassRequest req)
        {
            var isScheduleExist = await ScheduleQuery().AnyAsync(x => x.Id == req.ScheduleId);

            if (!isScheduleExist)            
                return new ServiceResponse { Status = ResponseTypes.NotFound,Message = "Schedule id not found."};            

            var isTeacherExist = await TeacherQuery().AnyAsync(x => x.Id == req.TeacherId);

            if (!isTeacherExist)            
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Teacher id not found."};
            

            await _dbContext.TblClasses.AddAsync(new TblClass
            {
                Id = Guid.NewGuid().ToString(),
                ClassName = req.ClassName,
                StartDate = req.StartDate,
                Duration = req.Duration,
                ScheduleId = req.ScheduleId,
                TeacherId = req.TeacherId,
                IsDelete = false
            });

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Class created successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
        public async Task<ServiceResponse> DeleteClassAsync(string id)
        {
            var item = await ClassQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (item is null) return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class not found." };
            // else if (item.IsDelete) return ResponseTypes.AlreadyDeleted;
            item.IsDelete = true;
            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Class deleted successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
        public async Task<ServiceResponse> UpdateClassAsync(CreateClassRequest request, string id)
        {
            var item = await ClassQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null) 
                return new ServiceResponse { Status = ResponseTypes.NotFound,Message = "Class not found."};

            var isScheduleExist = await ScheduleQuery().AnyAsync(x => x.Id == request.ScheduleId);

            if (!isScheduleExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Schedule id not found." };

            var isTeacherExist = await TeacherQuery().AnyAsync(x => x.Id == request.TeacherId);

            if (!isTeacherExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Teacher id not found." };


            item.ClassName = request.ClassName;
            item.StartDate = request.StartDate;
            item.Duration = request.Duration;
            item.ScheduleId = request.ScheduleId;
            item.TeacherId = request.TeacherId;

            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success,Message = "Class updated successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None,Message = "Failed. No rows were affected." };

        }
        public async Task<ServiceResponse> UpdatePatchClassAsync(UpdatePatchClassRequest request, string id)
        {
            var item = await ClassQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null) return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class not found." };

            if (!string.IsNullOrEmpty(request.ClassName)) item.ClassName = request.ClassName;

            if (request.StartDate.HasValue) item.StartDate = request.StartDate.Value;

            if (request.Duration != 0) item.Duration = request.Duration;

            if (!string.IsNullOrEmpty(request.ScheduleId))
            {
                var isScheduleExist = await ScheduleQuery().AnyAsync(x => x.Id == request.ScheduleId);

                if (!isScheduleExist)
                    return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Schedule id not found." };

                item.ScheduleId = request.ScheduleId;
            }
            if (!string.IsNullOrEmpty(request.TeacherId))
            {
                var isTeacherExist = await TeacherQuery().AnyAsync(x => x.Id == request.TeacherId);

                if (!isTeacherExist)
                    return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Teacher id not found." };

                item.TeacherId = request.TeacherId;
            }
            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Class updated successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
    }
}

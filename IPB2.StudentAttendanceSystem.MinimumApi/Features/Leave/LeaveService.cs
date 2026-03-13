using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Attendance;
using IPB2.StudentAttendanceSystem.WebApi.Features.Leave;
using IPB2.StudentLeaveSystem.WebApi.Features.Leave;
using Microsoft.EntityFrameworkCore;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Leave
{
    public class LeaveService : ILeaveService
    {
        private readonly AppDbContext _dbContext;

        public LeaveService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IQueryable<TblLeave> LeaveQuery()
        {
            IQueryable<TblLeave> query = _dbContext.TblLeaves
                   //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        private IQueryable<TblStudentEnroll> StudentQuery()
        {
            IQueryable<TblStudentEnroll> query = _dbContext.TblStudentEnrolls
                   //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        private IQueryable<TblClass> ClassQuery()
        {
            IQueryable<TblClass> query = _dbContext.TblClasses
                   //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        public async Task<List<LeaveModel>> GetAllLeaveAsync(int pageNo, int pageSize)
        {
            //var result = await LeaveQuery()
            //    .OrderByDescending(x => x.StudentEnrollId)
            //    .Skip((pageNo - 1) * pageSize)
            //    .Take(pageSize)
            //    .Select(x => new LeaveModel
            //    {
            //        Id = x.Id,
            //        LeaveDate = x.LeaveDate,
            //        ClassId = x.ClassId,
            //        StudentEnrollId = x.StudentEnrollId
            //    }).ToListAsync();

            var query = from lvs in _dbContext.TblLeaves
                        join cl in _dbContext.TblClasses on lvs.ClassId equals cl.Id
                        join stu in _dbContext.TblStudentEnrolls on lvs.StudentEnrollId equals stu.Id
                        where lvs.IsDelete == false
                        orderby lvs.LeaveDate ascending
                        select new LeaveModel
                        {
                            Id = lvs.Id,
                            LeaveDate = lvs.LeaveDate,
                            ClassName = cl.ClassName,
                            StudentName = stu.StudentName
                        };
            // ်for pagination logic
            var result = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return result;
        }
        public async Task<ServiceResponse> SaveLeaveAsync(CreateLeaveRequest req)
        {
            var isStudentExist = await StudentQuery().AnyAsync(x => x.Id == req.StudentEnrollId);

            if (!isStudentExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Student id not found." };

            var isClassExist = await ClassQuery().AnyAsync(x => x.Id == req.ClassId);

            if (!isClassExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };


            await _dbContext.TblLeaves.AddAsync(new TblLeave
            {
                Id = Guid.NewGuid().ToString(),
                LeaveDate = req.LeaveDate,
                StudentEnrollId = req.StudentEnrollId,
                ClassId = req.ClassId,
                IsDelete = false
            });

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Leave created successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
        public async Task<ServiceResponse> DeleteLeaveAsync(string id)
        {
            var item = await LeaveQuery().FirstOrDefaultAsync(x => x.Id == id);
            if (item is null) return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Leave not found." };
            // else if (item.IsDelete) return ResponseTypes.AlreadyDeleted;
            item.IsDelete = true;
            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Leave deleted successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
        public async Task<ServiceResponse> UpdateLeaveAsync(CreateLeaveRequest request, string id)
        {
            var item = await LeaveQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Leave not found." };

            var isStudentExist = await StudentQuery().AnyAsync(x => x.Id == request.StudentEnrollId);

            if (!isStudentExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Student id not found." };

            var isClassExist = await ClassQuery().AnyAsync(x => x.Id == request.ClassId);

            if (!isClassExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };


            item.LeaveDate = request.LeaveDate;
            item.ClassId = request.ClassId;
            item.StudentEnrollId = request.StudentEnrollId;

            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Leave updated successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };

        }
        public async Task<ServiceResponse> UpdatePatchLeaveAsync(UpdatePatchLeaveRequest request, string id)
        {
            var item = await LeaveQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null) return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Leave not found." };

            if (request.LeaveDate.HasValue) item.LeaveDate = request.LeaveDate.Value;            

            if (!string.IsNullOrEmpty(request.StudentEnrollId))
            {
                var isStudentExist = await StudentQuery().AnyAsync(x => x.Id == request.StudentEnrollId);

                if (!isStudentExist)
                    return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Student id not found." };

                item.StudentEnrollId = request.StudentEnrollId;
            }
            if (!string.IsNullOrEmpty(request.ClassId))
            {
                var isClassExist = await ClassQuery().AnyAsync(x => x.Id == request.ClassId);

                if (!isClassExist)
                    return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };

                item.ClassId = request.ClassId;
            }
            int rowAffected = await _dbContext.SaveChangesAsync();
            return rowAffected > 0
                    ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Leave updated successfully." }
                    : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }
    }
}

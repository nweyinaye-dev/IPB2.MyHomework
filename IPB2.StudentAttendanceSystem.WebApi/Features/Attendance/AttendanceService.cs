using Azure.Core;
using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Attendance
{
    public class AttendanceService : IAttendanceService
    {
        private readonly AppDbContext _dbContext;

        public AttendanceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<TblAttendanceLog> AttendanceQuery()
        {
            IQueryable<TblAttendanceLog> query = _dbContext.TblAttendanceLogs
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
        private IQueryable<TblStudentEnroll> StudentQuery()
        {
            IQueryable<TblStudentEnroll> query = _dbContext.TblStudentEnrolls
                   //.AsNoTracking()
                   .Where(x => x.IsDelete == false);
            return query;
        }
        public async Task<List<AttendanceLogModel>> GetAllAttendanceAsync(int pageNo, int pageSize)
        {
            var result = await AttendanceQuery()
                .OrderByDescending(x => x.StudentEnrollId)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new AttendanceLogModel
                {
                    Id = x.Id,
                    AttendanceDate = x.AttendanceDate,
                    ClassId = x.ClassId,
                    StudentEnrollId = x.StudentEnrollId,
                    TimeIn = x.TimeIn,
                    TimeOut = x.TimeOut,
                })
                .ToListAsync();

            return result;
        }

        public async Task<ServiceResponse> SaveAttendanceAsync(CreateAttendanceRequest req)
        {
            var isClassIdExist = await ClassQuery().AnyAsync(x => x.Id == req.ClassId);

            if (!isClassIdExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };

            var isStudentEnrollIdExist = await StudentQuery().AnyAsync(x => x.Id == req.StudentEnrollId);

            if (!isStudentEnrollIdExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Student id not found." };

            await _dbContext.TblAttendanceLogs.AddAsync(new TblAttendanceLog
            {
                Id = Guid.NewGuid().ToString(),
                AttendanceDate = req.AttendanceDate,
                ClassId = req.ClassId,
                StudentEnrollId = req.StudentEnrollId,
                TimeIn = req.TimeIn,
                TimeOut = req.TimeOut,
                IsDelete = false
            });

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Attendance created successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> DeleteAttendanceAsync(string id)
        {
            var item = await AttendanceQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = "Attendance not found."
                };

            item.IsDelete = true;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Attendance deleted successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> UpdateAttendanceAsync(CreateAttendanceRequest request, string id)
        {
            var item = await AttendanceQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = "Attendance not found."
                };

            var isClassIdExist = await ClassQuery().AnyAsync(x => x.Id == request.ClassId);

            if (!isClassIdExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };

            var isStudentEnrollIdExist = await StudentQuery().AnyAsync(x => x.Id == request.StudentEnrollId);

            if (!isStudentEnrollIdExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Student id not found." };

            item.AttendanceDate = request.AttendanceDate;
            item.ClassId = request.ClassId;
            item.StudentEnrollId = request.StudentEnrollId;
            item.TimeIn = request.TimeIn;
            item.TimeOut = request.TimeOut;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Attendance updated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> UpdatePatchAttendanceAsync(UpdatePatchAttendanceRequest request, string id)
        {
            var item = await AttendanceQuery().FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = "Attendance not found."
                };

            if (request.AttendanceDate.HasValue)
                item.AttendanceDate = request.AttendanceDate.Value;

            if (!string.IsNullOrEmpty(request.ClassId))
            {
                var isClassIdExist = await ClassQuery().AnyAsync(x => x.Id == request.ClassId);

                if (!isClassIdExist)
                    return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };

                item.ClassId = request.ClassId;
            }

            if (!string.IsNullOrEmpty(request.StudentEnrollId))
            {
                var isStudentEnrollIdExist = await StudentQuery().AnyAsync(x => x.Id == request.StudentEnrollId);

                if (!isStudentEnrollIdExist)
                    return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Student id not found." };

                item.StudentEnrollId = request.StudentEnrollId;
            }

            if (!string.IsNullOrEmpty(request.TimeIn))
                item.TimeIn = request.TimeIn;

            if (!string.IsNullOrEmpty(request.TimeOut))
                item.TimeOut = request.TimeOut;

            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Attendance updated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };
        }

        public async Task<ServiceResponse> CalculateAttendanceAsync(CalculateAttendanceRequest req)
        {
            var isStudentEnrollIdExist = await StudentQuery().AnyAsync(x => x.Id == req.StudentEnrollId);

            if (!isStudentEnrollIdExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Student id not found." };

            var isSClassEnrollIdExist = await ClassQuery().AnyAsync(x => x.Id == req.ClassId);

            if (!isSClassEnrollIdExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };

            var item = await AttendanceQuery().FirstOrDefaultAsync(x => x.StudentEnrollId == req.StudentEnrollId 
                                && x.AttendanceDate == req.FromDate);





            if (item is null)
                return new ServiceResponse
                {
                    Status = ResponseTypes.NotFound,
                    Message = $"Attendance log not for {req.FromDate}."
                };


            await _dbContext.TblAttendances.AddAsync(new TblAttendance
            {
                Id = Guid.NewGuid().ToString(),
                AttendanceDate = item.AttendanceDate,
                ClassId = item.ClassId,
                StudentEnrollId = item.StudentEnrollId,
                TimeIn = item.TimeIn,
                TimeOut = item.TimeOut,
                Status = string.IsNullOrEmpty(item.TimeIn) ? "Absent" : "Present",
                Late = "late",
                IsDelete = false,

            });
            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Attendance calculated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };

        }
    }
}

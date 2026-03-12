using Azure.Core;
using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        private IQueryable<TblLeave> LeaveQuery()
        {
            IQueryable<TblLeave> query = _dbContext.TblLeaves
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

            string status = "absent";
            string lateMinutes = "";
            List<TblAttendance> attendanceRecords = new List<TblAttendance>();

            TblAttendance attendance = new TblAttendance
            {
                Id = Guid.NewGuid().ToString(),
                AttendanceDate = req.FromDate,
                ClassId = req.ClassId,
                StudentEnrollId = req.StudentEnrollId,
                TimeIn = "",
                TimeOut = "",
                Status = status,
                Late = lateMinutes,
                IsDelete = false,

            };

            var isStudentEnrollIdExist = await StudentQuery().AnyAsync(x => x.Id == req.StudentEnrollId);

            if (!isStudentEnrollIdExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Student id not found." };

            var isSClassEnrollIdExist = await ClassQuery().AnyAsync(x => x.Id == req.ClassId);

            if (!isSClassEnrollIdExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };

            var isLeaveExist = await LeaveQuery().AnyAsync(x => x.Id == req.ClassId && x.StudentEnrollId == req.StudentEnrollId
                                && x.LeaveDate == req.FromDate);

            if (isLeaveExist)
                status = "leave";


            var item = await AttendanceQuery().FirstOrDefaultAsync(x => x.StudentEnrollId == req.StudentEnrollId && x.ClassId == req.ClassId
                                && x.AttendanceDate == req.FromDate); // to delete
            if(item is not null)
            {
                var list = await (from att in _dbContext.TblAttendanceLogs
                                  join cls in _dbContext.TblClasses on att.ClassId equals cls.Id
                                  join sch in _dbContext.TblSchedules on cls.ScheduleId equals sch.Id
                                  where att.StudentEnrollId == req.StudentEnrollId
                                     && att.AttendanceDate >= req.FromDate 
                                     && att.AttendanceDate <= req.ToDate
                                  select new
                                  {
                                      AttendaceDate = att.AttendanceDate,
                                      ActualTimeIn = att.TimeIn,
                                      ActualTimeOut = att.TimeOut,
                                      ScheduledStartTime = sch.StartTime
                                  }).ToListAsync();

                

                foreach (var data in list)
                {
                     status = "absent";
                     lateMinutes = "";

                    if (DateTime.TryParse(data.ActualTimeIn, out DateTime parsedTime) &&
                                DateTime.TryParse(data.ScheduledStartTime, out DateTime scheduleStartTime))
                    {
                        TimeSpan actualTime = parsedTime.TimeOfDay;
                        TimeSpan scheduledTime = scheduleStartTime.TimeOfDay;
                        TimeSpan difference = actualTime - scheduledTime;

                        if (difference.TotalMinutes > 15)
                        {
                            lateMinutes = ((int)difference.TotalMinutes).ToString();
                            status = "late";
                        } else status  = "present";
                    }
                    attendance = new TblAttendance
                    {
                        Id = Guid.NewGuid().ToString(),
                        AttendanceDate = data.AttendaceDate,
                        ClassId = req.ClassId,
                        StudentEnrollId = req.StudentEnrollId,
                        TimeIn = data.ActualTimeIn,
                        TimeOut = data.ActualTimeOut,
                        Status = status,
                        Late = lateMinutes,
                        IsDelete = false,

                    };
                    attendanceRecords.Add(attendance);
                }
                
            }


            await _dbContext.TblAttendances.AddRangeAsync(attendanceRecords);
            int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Attendance calculated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };

        }
    }
}

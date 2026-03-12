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

        private  async Task<List<TblAttendance>> CreateAttendanceList(CalculateAttendanceRequest req)
        {
            List<TblAttendance> attendanceRecords = new List<TblAttendance>();

            var list = await (from att in _dbContext.TblAttendanceLogs
                             join cls in _dbContext.TblClasses on att.ClassId equals cls.Id
                             join sch in _dbContext.TblSchedules on cls.ScheduleId equals sch.Id
                             where att.StudentEnrollId == req.StudentEnrollId
                                && att.ClassId == req.ClassId
                                && att.AttendanceDate >= req.FromDate
                                && att.AttendanceDate <= req.ToDate
                                && att.IsDelete == false
                             select new
                             {
                                 AttendaceDate = att.AttendanceDate,
                                 ActualTimeIn = att.TimeIn,
                                 ActualTimeOut = att.TimeOut,
                                 ScheduledStartTime = sch.StartTime
                             }).ToListAsync();

            for (DateOnly date = req.FromDate; date <= req.ToDate; date = date.AddDays(1))
            {
                string status = "absent";
                string lateMinutes = "";
                string timeIn = "";
                string timeOut = "";

                var attLog = list.FirstOrDefault(x => x.AttendaceDate == date);

                if (attLog != null)
                {
                    timeIn = attLog.ActualTimeIn;
                    timeOut = attLog.ActualTimeOut;

                    // Logic for Late/Present
                    if (DateTime.TryParse(attLog.ActualTimeIn, out DateTime parsedTime) &&
                        DateTime.TryParse(attLog.ScheduledStartTime, out DateTime scheduleStartTime))
                    {
                        TimeSpan difference = parsedTime.TimeOfDay - scheduleStartTime.TimeOfDay;

                        if (difference.TotalMinutes > 15)
                        {
                            lateMinutes = ((int)difference.TotalMinutes).ToString();
                            status = "late";
                        }
                        else
                        {
                            status = "present";
                        }
                    }
                }
                else
                {
                    var isLeaveExist = await LeaveQuery().AnyAsync(x => x.StudentEnrollId == req.StudentEnrollId
                                        && x.LeaveDate == date);

                    status = isLeaveExist ? "leave" : "absent";
                }

                attendanceRecords.Add(new TblAttendance
                {
                    Id = Guid.NewGuid().ToString(),
                    AttendanceDate = date, 
                    ClassId = req.ClassId,
                    StudentEnrollId = req.StudentEnrollId,
                    TimeIn = timeIn,
                    TimeOut = timeOut,
                    Status = status,
                    Late = lateMinutes,
                    IsDelete = false
                });
            }

            return attendanceRecords;
        }
        public async Task<ServiceResponse> CalculateAttendanceAsync(CalculateAttendanceRequest req)
        {
            List<TblAttendance> attendanceRecords = new List<TblAttendance>();

            // validation
            var isStudentEnrollIdExist = await StudentQuery().AnyAsync(x => x.Id == req.StudentEnrollId);

            if (!isStudentEnrollIdExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Student id not found." };

            var isSClassEnrollIdExist = await ClassQuery().AnyAsync(x => x.Id == req.ClassId);

            if (!isSClassEnrollIdExist)
                return new ServiceResponse { Status = ResponseTypes.NotFound, Message = "Class id not found." };

            // calculate attendance result - if exit log - present or late or absent, else no exit log - absent or leave
            attendanceRecords = await CreateAttendanceList(req);

           var existingAttendance = _dbContext.TblAttendances.Where(x => x.StudentEnrollId == req.StudentEnrollId
                                        && x.ClassId == req.ClassId 
                                        && x.AttendanceDate >= req.FromDate
                                        && x.AttendanceDate <= req.ToDate);

            _dbContext.RemoveRange(existingAttendance); // delete exiting attendance data

           await _dbContext.TblAttendances.AddRangeAsync(attendanceRecords);
           int rowAffected = await _dbContext.SaveChangesAsync();

            return rowAffected > 0
                ? new ServiceResponse { Status = ResponseTypes.Success, Message = "Attendance calculated successfully." }
                : new ServiceResponse { Status = ResponseTypes.None, Message = "Failed. No rows were affected." };

        }
    }
}

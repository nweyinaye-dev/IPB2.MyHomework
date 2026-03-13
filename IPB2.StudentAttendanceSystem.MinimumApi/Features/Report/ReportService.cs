using IPB2.EFCore.Database;
using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.EntityFrameworkCore;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Report
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _dbContext;

        public ReportService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
        public async Task<AttendanceReportResponse> GetAttendanceReportAsync(AttendanceReportRequest req)
        {           

            var isStudentExist = await StudentQuery().AnyAsync(x => x.Id == req.StudentEnrollId);

            if (!isStudentExist)
                return new AttendanceReportResponse { IsSuccess = false, Message = "Student id not found.", data = [] };

            var isClassExist = await ClassQuery().AnyAsync(x => x.Id == req.ClassId);

            if (!isClassExist)
                return new AttendanceReportResponse { IsSuccess = false, Message = "Class id not found.", data = [] };

            var query = from att in _dbContext.TblAttendances
                        join cls in _dbContext.TblClasses on att.ClassId equals cls.Id
                        join stu in _dbContext.TblStudentEnrolls on att.StudentEnrollId equals stu.Id
                        where att.StudentEnrollId == req.StudentEnrollId
                           && att.ClassId == req.ClassId
                           && att.AttendanceDate >= req.FromDate
                           && att.AttendanceDate <= req.ToDate
                           && att.IsDelete == false
                        orderby att.AttendanceDate ascending
                        select new AttendanceReportModel
                        {
                            AttendanceDate = att.AttendanceDate,
                            TimeIn = att.TimeIn,
                            TimeOut = att.TimeOut,
                            ClassName = cls.ClassName,
                            StudentName = stu.StudentName,
                            LateMinutes = att.Late.ToString(),
                            Status = att.Status 
                        };
            List<AttendanceReportModel> result = await query.ToListAsync();
            return new AttendanceReportResponse { IsSuccess = true, Message = "success", data = result };
        }
        
        public async Task<AttendanceSummaryReportResponse> GetAttendanceSummaryReportAsync(AttendanceReportRequest req)
        {
            var isStudentExist = await StudentQuery().AnyAsync(x => x.Id == req.StudentEnrollId);

            if (!isStudentExist)
                return new AttendanceSummaryReportResponse { IsSuccess = false, Message = "Student id not found.", data = [] };

            var isClassExist = await ClassQuery().AnyAsync(x => x.Id == req.ClassId);

            if (!isClassExist)
                return new AttendanceSummaryReportResponse { IsSuccess = false, Message = "Class id not found.", data = [] };

            var query = from att in _dbContext.TblAttendances
                            join cls in _dbContext.TblClasses on att.ClassId equals cls.Id
                            join stu in _dbContext.TblStudentEnrolls on att.StudentEnrollId equals stu.Id
                            where att.StudentEnrollId == req.StudentEnrollId
                               && att.ClassId == req.ClassId
                               && att.AttendanceDate >= req.FromDate
                               && att.AttendanceDate <= req.ToDate
                               && att.IsDelete == false
                            group att by new { stu.StudentName, cls.ClassName, req.FromDate, req.ToDate } into g
                            select new AttendanceSummaryReportModel
                            {
                                StudentName = g.Key.StudentName,
                                ClassName = g.Key.ClassName,
                                TotalPresentDays = g.Count(a => a.Status == "present"),
                                TotalAbsentDays = g.Count(a => a.Status == "absent"),
                                TotalLateDays = g.Count(a => a.Status == "late"),
                                TotalLeaveDays = g.Count(a => a.Status == "leave"),
                                TotalPossibleDays = g.Key.ToDate.DayNumber - g.Key.FromDate.DayNumber + 1
                            };

            var summaryList = await query.ToListAsync();

            var grades = await _dbContext.TblGrades.Where(g => g.IsDelete == false).ToListAsync();

            foreach (var item in summaryList)
            {
                if (item.TotalPossibleDays > 0)
                {
                    item.Percent = ((double)item.TotalPresentDays / item.TotalPossibleDays) * 100;

                    item.GradeName = grades.FirstOrDefault(g =>
                        item.Percent >= g.FromPercent &&
                        item.Percent <= g.ToPercent)?.GradeName ?? "N/A";
                }
            }
            return new AttendanceSummaryReportResponse { IsSuccess = true, Message = "success", data = summaryList };
            }
        }
}

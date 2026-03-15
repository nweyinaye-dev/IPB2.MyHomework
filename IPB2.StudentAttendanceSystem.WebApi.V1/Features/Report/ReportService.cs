using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Report
{
    public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.DbType = DbType.Date;
            parameter.Value = value.ToDateTime(TimeOnly.MinValue);
        }

        public override DateOnly Parse(object value)
        {
            return DateOnly.FromDateTime((DateTime)value);
        }
    }

    public class ReportService
    {
        private readonly AppDbContext _dbContext = new AppDbContext();

        static ReportService()
        {
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        }

        public ReportService() { }

        public ReportService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "IPB2",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true,
        };

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
            using (IDbConnection db = new SqlConnection(connectionString.ConnectionString))
            {
                db.Open();

                var sql = @"SELECT 
                                att.[AttendanceDate],
                                att.[TimeIn],
                                att.[TimeOut],
                                cls.[ClassName],
                                stu.[StudentName],
                                CAST(att.[Late] AS NVARCHAR(MAX)) AS [LateMinutes],
                                att.[Status]
                            FROM [dbo].[Tbl_Attendance] att
                            INNER JOIN [dbo].[Tbl_Class] cls ON att.[ClassId] = cls.[Id]
                            INNER JOIN [dbo].[Tbl_StudentEnroll] stu ON att.[StudentEnrollId] = stu.[Id]
                            WHERE att.[StudentEnrollId] = @StudentEnrollId
                                AND att.[ClassId] = @ClassId
                                AND att.[AttendanceDate] >= @FromDate
                                AND att.[AttendanceDate] <= @ToDate
                                AND att.[IsDelete] = 0
                            ORDER BY att.[AttendanceDate] ASC";

                var result = await db.QueryAsync<AttendanceReportModel>(sql, new
                {
                    req.StudentEnrollId,
                    req.ClassId,
                    req.FromDate,
                    req.ToDate
                });

                return new AttendanceReportResponse { IsSuccess = true, Message = "success", data = result.ToList() };
            }
        }
        public async Task<AttendanceSummaryReportResponse> GetAttendanceSummaryReportAsync(AttendanceReportRequest req)
        {
            using (IDbConnection db = new SqlConnection(connectionString.ConnectionString))
            {
                db.Open();

                var sql = @"SELECT 
                                stu.[StudentName],
                                cls.[ClassName],
                                SUM(CASE WHEN att.[Status] = 'present' THEN 1 ELSE 0 END) AS [TotalPresentDays],
                                SUM(CASE WHEN att.[Status] = 'absent' THEN 1 ELSE 0 END) AS [TotalAbsentDays],
                                SUM(CASE WHEN att.[Status] = 'late' THEN 1 ELSE 0 END) AS [TotalLateDays],
                                SUM(CASE WHEN att.[Status] = 'leave' THEN 1 ELSE 0 END) AS [TotalLeaveDays]
                            FROM [dbo].[Tbl_Attendance] att
                            INNER JOIN [dbo].[Tbl_Class] cls ON att.[ClassId] = cls.[Id]
                            INNER JOIN [dbo].[Tbl_StudentEnroll] stu ON att.[StudentEnrollId] = stu.[Id]
                            WHERE att.[StudentEnrollId] = @StudentEnrollId
                                AND att.[ClassId] = @ClassId
                                AND att.[AttendanceDate] >= @FromDate
                                AND att.[AttendanceDate] <= @ToDate
                                AND att.[IsDelete] = 0
                            GROUP BY stu.[StudentName], cls.[ClassName]";

                var summaryList = (await db.QueryAsync<AttendanceSummaryReportModel>(sql, new
                {
                    req.StudentEnrollId,
                    req.ClassId,
                    req.FromDate,
                    req.ToDate
                })).ToList();

                var grades = await _dbContext.TblGrades.Where(g => g.IsDelete == false).ToListAsync();
                int totalPossibleDays = req.ToDate.DayNumber - req.FromDate.DayNumber + 1;

                foreach (var item in summaryList)
                {
                    item.TotalPossibleDays = totalPossibleDays;
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
}

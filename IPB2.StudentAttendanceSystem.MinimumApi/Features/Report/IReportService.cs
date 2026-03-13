using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentLeaveSystem.WebApi.Features.Leave;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Report
{
    public interface IReportService
    {
        Task<AttendanceReportResponse> GetAttendanceReportAsync(AttendanceReportRequest req);
        Task<AttendanceSummaryReportResponse> GetAttendanceSummaryReportAsync(AttendanceReportRequest req);                                                                                                                                                                   
    }
}

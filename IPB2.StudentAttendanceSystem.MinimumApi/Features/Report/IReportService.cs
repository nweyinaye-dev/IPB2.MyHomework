

using IPB2.StudentAttendanceSystem.MinimumApi.Features.Report;

namespace IPB2.StudentAttendanceSystem.MinimumApi.Features.Report
{
    public interface IReportService
    {
        Task<AttendanceReportResponse> GetAttendanceReportAsync(AttendanceReportRequest req);
        Task<AttendanceSummaryReportResponse> GetAttendanceSummaryReportAsync(AttendanceReportRequest req);                                                                                                                                                                   
    }
}

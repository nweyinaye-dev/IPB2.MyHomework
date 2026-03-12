using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Report
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpPost("attendance")]
        public async Task<IActionResult> GetAttendanceReport(AttendanceReportModel request)
        {
            return Ok();
        }

        [HttpPost("summary")]
        public async Task<IActionResult> GetAttendanceSummaryReport(AttendanceReportModel request)
        {
            return Ok();
        }
    }
}

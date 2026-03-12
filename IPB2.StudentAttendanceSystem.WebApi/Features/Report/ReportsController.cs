using Azure;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentLeaveSystem.WebApi.Features.Leave;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Report
{
    [Route("api/reports")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService service)
        {
            _reportService = service;
        }

        [HttpPost("attendance")]
        public async Task<IActionResult> GetAttendanceReport(AttendanceReportRequest request)
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess)
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _reportService.GetAttendanceReportAsync(request);
            if (!response.IsSuccess)
            {
                return NotFound(response);
            }
            return Ok(response);
            
        }

        [HttpPost("summary")]
        public async Task<IActionResult> GetAttendanceSummaryReport(AttendanceReportRequest request)
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess)
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _reportService.GetAttendanceSummaryReportAsync(request);
            if (!response.IsSuccess)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        private ResponseBaseModel Validation(AttendanceReportRequest request)
        {
            // Require Validation
            if (string.IsNullOrWhiteSpace(request.ClassId))
                return new ResponseBaseModel { IsSuccess = false, Message = "Class id is required." };
            if (string.IsNullOrWhiteSpace(request.StudentEnrollId))
                return new ResponseBaseModel { IsSuccess = false, Message = "Student id is required." };

            return new ResponseBaseModel { IsSuccess = true, Message = "Validation successfully." };

        }
    }
}

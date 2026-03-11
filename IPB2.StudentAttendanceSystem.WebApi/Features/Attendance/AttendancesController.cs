using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Attendance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Attendance
{
    [Route("api/attendances")]
    [ApiController]
    public class AttendancesController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        public AttendancesController(IAttendanceService service)
        {
            _attendanceService = service;
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetAttendanceList(int pageNo, int pageSize)
        {
            if (pageNo < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
            if (pageSize < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

            List<AttendanceLogModel> result = await _attendanceService.GetAllAttendanceAsync(pageNo, pageSize);
            string message = result.Count > 0 ? "Get all Attendance successfully." : "No data.";
            return Ok(new GetAllAttendanceResponse
            {
                IsSuccess = true,
                Message = message,
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendance(CreateAttendanceRequest request)
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess)
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _attendanceService.SaveAttendanceAsync(request);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPut("{id}")] // entire object (if not exist, create new one)(if exit, update existing one)
        public async Task<IActionResult> UpsertAttendance(CreateAttendanceRequest request, string id)
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess)
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _attendanceService.UpdateAttendanceAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPatch("{id}")] // partially update
        public async Task<IActionResult> UpdatePatchAttendance(UpdatePatchAttendanceRequest request, string id)
        {
            var response = await _attendanceService.UpdatePatchAttendanceAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpDelete("{id}")] // partially update
        public async Task<IActionResult> DeleteAttendance(string id)
        {
            var response = await _attendanceService.DeleteAttendanceAsync(id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateAttendance(CalculateAttendanceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.StudentEnrollId))
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Student id is required." });

            var response = await _attendanceService.CalculateAttendanceAsync(request);
            return ResponseHelper.ConvertResponseType(response);
        }
        private ResponseBaseModel Validation(CreateAttendanceRequest request)
        {
            // Require Validation
            if (string.IsNullOrWhiteSpace(request.ClassId))
                return new ResponseBaseModel { IsSuccess = false, Message = "Class id is required." };
            if (string.IsNullOrWhiteSpace(request.StudentEnrollId))
                return new ResponseBaseModel { IsSuccess = false, Message = "Student id is required." };
            if (!TimeSpan.TryParseExact(request.TimeIn, "hh\\:mm\\:ss", CultureInfo.InvariantCulture, out TimeSpan startTime))
                return new ResponseBaseModel { IsSuccess = false, Message = "Timein must be in HH:mm:ss format" };

            if (!TimeSpan.TryParseExact(request.TimeOut, "hh\\:mm\\:ss", CultureInfo.InvariantCulture, out TimeSpan endTime))
                return new ResponseBaseModel { IsSuccess = false, Message = "Timeout must be in HH:mm:ss format" };

            if (endTime <= startTime)
                return new ResponseBaseModel { IsSuccess = false, Message = "Timeout must be after Timein" };


            return new ResponseBaseModel { IsSuccess = true, Message = "Validatin successfully." };

        }
    }
}

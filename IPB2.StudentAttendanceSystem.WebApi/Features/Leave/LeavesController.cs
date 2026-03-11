using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Leave;
using IPB2.StudentLeaveSystem.WebApi.Features.Leave;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Leave
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesController : ControllerBase
    {
        private readonly ILeaveService _leaveService;
        public LeavesController(ILeaveService service)
        {
            _leaveService = service;
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetLeavesList(int pageNo, int pageSize)
        {
            if (pageNo < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
            if (pageSize < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

            List<LeaveModel> result = await _leaveService.GetAllLeaveAsync(pageNo, pageSize);
            string message = result.Count > 0 ? "Get all Leave successfully." : "No data.";
            return Ok(new GetAllLeaveResponse
            {
                IsSuccess = true,
                Message = message,
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeave(CreateLeaveRequest request)
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess)
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _leaveService.SaveLeaveAsync(request);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPut("{id}")] // entire object (if not exist, create new one)(if exit, update existing one)
        public async Task<IActionResult> UpsertLeave(CreateLeaveRequest request, string id)
        {
            var response = await _leaveService.UpdateLeaveAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPatch("{id}")] // partially update
        public async Task<IActionResult> UpdateLeave(UpdatePatchLeaveRequest request, string id)
        {
            var response = await _leaveService.UpdatePatchLeaveAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeave(string id)
        {
            var response = await _leaveService.DeleteLeaveAsync(id);
            return ResponseHelper.ConvertResponseType(response);
        }
        private ResponseBaseModel Validation(CreateLeaveRequest request)
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

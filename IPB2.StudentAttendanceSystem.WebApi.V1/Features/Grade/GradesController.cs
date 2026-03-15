using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Grade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Grade
{
    [Route("api/grades")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly GradeService _gradeService;
        public GradesController(GradeService service)
        {
            _gradeService = service;
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetGradesList(int pageNo, int pageSize)
        {
            if (pageNo < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
            if (pageSize < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

            List<GradeModel> result = await _gradeService.GetAllGradeAsync(pageNo, pageSize);
            string message = result.Count > 0 ? "Get all Grade successfully." : "No data.";
            return Ok(new GetAllGradeResponse
            {
                IsSuccess = true,
                Message = message,
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateGrade(CreateGradeRequest request)
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess)
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _gradeService.SaveGradeAsync(request);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPut("{id}")] // entire object (if not exist, create new one)(if exit, update existing one)
        public async Task<IActionResult> UpsertGrade(CreateGradeRequest request, string id)
        {
            var response = await _gradeService.UpdateGradeAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPatch("{id}")] // partially update
        public async Task<IActionResult> UpdateGrade(UpdatePatchGradeRequest request, string id)
        {
            var response = await _gradeService.UpdatePatchGradeAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpDelete("{id}")] // partially update
        public async Task<IActionResult> DeleteGrade(string id)
        {
            var response = await _gradeService.DeleteGradeAsync(id);
            return ResponseHelper.ConvertResponseType(response);
        }
        private ResponseBaseModel Validation(CreateGradeRequest request)
        {
            // Require Validation
            if (string.IsNullOrWhiteSpace(request.GradeName))
                return new ResponseBaseModel { IsSuccess = false, Message = "Grade name is required." };
            if (request.FromPercent == 0)
                return new ResponseBaseModel { IsSuccess = false, Message = "Grade fromPercent is required." };
            if (request.ToPercent == 0)
                return new ResponseBaseModel { IsSuccess = false, Message = "Grade toPercent is required." };


            return new ResponseBaseModel { IsSuccess = true, Message = "Validatin successfully." };

        }
    }
}

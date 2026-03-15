using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.StudentEnroll
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsEnrollController : ControllerBase
    {
        private readonly StudentsEnrollService _studentsEnrollService = new StudentsEnrollService();
       

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetStudentsEnrollsList(int pageNo, int pageSize)
        {
            if (pageNo < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
            if (pageSize < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

            List<StudentsEnrollModel> result = await _studentsEnrollService.GetAllStudentsEnrollAsync(pageNo, pageSize);
            string message = result.Count > 0 ? "Get all StudentsEnroll successfully." : "No data.";
            return Ok(new GetAllStudentsEnrollResponse
            {
                IsSuccess = true,
                Message = message,
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudentsEnroll(CreateStudentsEnrollRequest request)
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess)
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _studentsEnrollService.SaveStudentsEnrollAsync(request);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPut("{id}")] // entire object (if not exist, create new one)(if exit, update existing one)
        public async Task<IActionResult> UpsertStudentsEnroll(CreateStudentsEnrollRequest request, string id)
        {
            var response = await _studentsEnrollService.UpdateStudentsEnrollAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPatch("{id}")] // partially update
        public async Task<IActionResult> UpdateStudentsEnroll(UpdatePatchStudentsEnrollRequest request, string id)
        {
            var response = await _studentsEnrollService.UpdatePatchStudentsEnrollAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpDelete("{id}")] // partially update
        public async Task<IActionResult> DeleteStudentsEnroll(string id)
        {
            var response = await _studentsEnrollService.DeleteStudentsEnrollAsync(id);
            return ResponseHelper.ConvertResponseType(response);
        }
        private ResponseBaseModel Validation(CreateStudentsEnrollRequest request)
        {
            // Require Validation
            if (string.IsNullOrWhiteSpace(request.StudentName))
                return new ResponseBaseModel { IsSuccess = false, Message = "Student name is required." };
            if (string.IsNullOrWhiteSpace(request.StudentPhoneno))
                return new ResponseBaseModel { IsSuccess = false, Message = "Student phoneno  is required." };
            if (request.StudentAge == 0)
                return new ResponseBaseModel { IsSuccess = false, Message = "Student age is required." };


            return new ResponseBaseModel { IsSuccess = true, Message = "Validatin successfully." };

        }
    }
}

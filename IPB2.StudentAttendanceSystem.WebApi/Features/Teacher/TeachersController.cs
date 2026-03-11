using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Teacher;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Teacher
{
    [Route("api/teachers")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeachersController(ITeacherService service)
        {
            _teacherService = service;
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetTeachersList(int pageNo, int pageSize)
        {
            if (pageNo < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
            if (pageSize < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

            List<TeacherModel> result = await _teacherService.GetAllTeacherAsync(pageNo, pageSize);
            string message = result.Count > 0 ? "Get all Teacher successfully." : "No data.";
            return Ok(new GetAllTeacherResponse
            {
                IsSuccess = true,
                Message = message,
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherRequest request)
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess)
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _teacherService.SaveTeacherAsync(request);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPut("{id}")] // entire object (if not exist, create new one)(if exit, update existing one)
        public async Task<IActionResult> UpsertTeacher(CreateTeacherRequest request, string id)
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess)
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _teacherService.UpdateTeacherAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPatch("{id}")] // partially update
        public async Task<IActionResult> UpdatePatchTeacher(UpdatePatchTeacherRequest request, string id)
        {
            var response = await _teacherService.UpdatePatchTeacherAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpDelete("{id}")] // partially update
        public async Task<IActionResult> DeleteTeacher(string id)
        {
            var response = await _teacherService.DeleteTeacherAsync(id);
            return ResponseHelper.ConvertResponseType(response);
        }
        private ResponseBaseModel Validation(CreateTeacherRequest request)
        {
            // Require Validation
            if (string.IsNullOrWhiteSpace(request.TeacherName))
                return new ResponseBaseModel { IsSuccess = false, Message = "Teacher name is required." };
            if (string.IsNullOrWhiteSpace(request.TeacherPhoneno))
                return new ResponseBaseModel { IsSuccess = false, Message = "Teacher phone no is required." };
            if (string.IsNullOrWhiteSpace(request.TeacherAddress))
                return new ResponseBaseModel { IsSuccess = false, Message = "Teacher address is required." };
           
            
            return new ResponseBaseModel { IsSuccess = true, Message = "Validatin successfully." };

        }
    }
}

using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Class;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Class
{
    [Route("api/class")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private  ClassService _classService =  new ClassService();
        
        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetClasssList(int pageNo, int pageSize)
        {
            if (pageNo < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
            if (pageSize < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

            List<ClassModel> result = await _classService.GetAllClassAsync(pageNo, pageSize);
            string message = result.Count > 0 ? "Get all Class successfully." : "No data.";
            return Ok(new GetAllClassResponse
            {
                IsSuccess = true,
                Message = message,
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass(CreateClassRequest request)
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess)
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _classService.SaveClassAsync(request);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPut("{id}")] // entire object (if not exist, create new one)(if exit, update existing one)
        public async Task<IActionResult> UpsertClass(CreateClassRequest request, string id)
        {
            var response = await _classService.UpdateClassAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPatch("{id}")] // partially update
        public async Task<IActionResult> UpdateClass(UpdatePatchClassRequest request, string id)
        {
            var response = await _classService.UpdatePatchClassAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteClass(string id)
        {
            var response = await _classService.DeleteClassAsync(id);
            return ResponseHelper.ConvertResponseType(response);
        }
        private ResponseBaseModel Validation(CreateClassRequest request)
        {
            // Require Validation
            if (string.IsNullOrWhiteSpace(request.ClassName))
                return new ResponseBaseModel { IsSuccess = false, Message = "Class name is required." };
            //if (string.IsNullOrWhiteSpace(request.StartDate))
            //    return new ResponseBaseModel { IsSuccess = false, Message = "Class start date is required." };
            if (request.Duration == 0)
                return new ResponseBaseModel { IsSuccess = false, Message = "Class duration is required." };
            if (string.IsNullOrWhiteSpace(request.ScheduleId))
                return new ResponseBaseModel { IsSuccess = false, Message = "Class schedule id is required." };
            if (string.IsNullOrWhiteSpace(request.TeacherId))
                return new ResponseBaseModel { IsSuccess = false, Message = "Class teacher id is required." };

            //var dateValidationRes = GeneralUtilities.ValidateDateFormat(request.StartDate);
            //if(!dateValidationRes.IsSuccess)
            //    return new ResponseBaseModel { IsSuccess = false, Message = dateValidationRes.Message };

            return new ResponseBaseModel { IsSuccess = true, Message = "Validatin successfully." };

        }
    }
}

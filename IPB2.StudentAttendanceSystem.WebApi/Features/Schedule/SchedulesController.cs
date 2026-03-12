using Azure.Core;
using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.StudentAttendanceSystem.WebApi.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Schedule
{
    [Route("api/schedules")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        public SchedulesController(IScheduleService service)
        {
            _scheduleService = service;
        }

        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<IActionResult> GetSchedulesList(int pageNo, int pageSize)
        {
            if (pageNo < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page number." });
            if (pageSize < 0) return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = "Invalid page size." });

            List<ScheduleModel> result = await _scheduleService.GetAllScheduleAsync(pageNo,pageSize);
            string message = result.Count > 0 ? "Get all schedule successfully." : "No data.";
            return Ok(new GetAllScheduleResponse
            {
                IsSuccess = true,
                Message = message,
                data = result
            }) ;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(CreateScheduleRequest request)
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess) 
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _scheduleService.SaveScheduleAsync(request);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPut("{id}")] // entire object (if not exist, create new one)(if exit, update existing one)
        public async Task<IActionResult> UpsertSchedule(CreateScheduleRequest request,string id)        
        {
            ResponseBaseModel validationRes = Validation(request);

            if (!validationRes.IsSuccess)
                return BadRequest(new ResponseBaseModel { IsSuccess = false, Message = validationRes.Message });

            var response = await _scheduleService.UpdateScheduleAsync(request,id);
            return ResponseHelper.ConvertResponseType(response);
        }

        [HttpPatch("{id}")] // partially update
        public async Task<IActionResult> UpdateSchedule(UpdatePatchScheduleRequest request,string id)
        {
            var response = await _scheduleService.UpdatePatchScheduleAsync(request, id);
            return ResponseHelper.ConvertResponseType(response);
        }  

        [HttpDelete("{id}")] // partially update
        public async Task<IActionResult> DeleteSchedule(string id)
        {
            var response  = await _scheduleService.DeleteScheduleAsync(id);
            return ResponseHelper.ConvertResponseType(response);
        }
        private ResponseBaseModel Validation(CreateScheduleRequest request)
        {
            // Require Validation
            if (string.IsNullOrWhiteSpace(request.ScheduleName)) 
                return new ResponseBaseModel { IsSuccess = false, Message = "Schedule name is required." };
            if (string.IsNullOrWhiteSpace(request.ScheduleDays))
                return new ResponseBaseModel { IsSuccess = false, Message = "Schedule days is required." };
            if (string.IsNullOrWhiteSpace(request.StartTime))
                return new ResponseBaseModel { IsSuccess = false, Message = "Schedule start time is required." };
            if (string.IsNullOrWhiteSpace(request.EndTime))
                return new ResponseBaseModel { IsSuccess = false, Message = "Schedule end time is required." };

            // Format Validation
            foreach (var day in request.ScheduleDays.Split(','))
            {
                if (!Enum.TryParse(day.Trim(), out Days result))
                    return new ResponseBaseModel { IsSuccess = false, Message = $"Invalid schedule day: {day}" };
            
            }
            if (!TimeSpan.TryParseExact(request.StartTime, "hh\\:mm\\:ss", CultureInfo.InvariantCulture, out TimeSpan startTime))
                return new ResponseBaseModel { IsSuccess = false, Message = "StartTime must be in HH:mm:ss format" };

            if (!TimeSpan.TryParseExact(request.EndTime, "hh\\:mm\\:ss", CultureInfo.InvariantCulture, out TimeSpan endTime))
                return new ResponseBaseModel { IsSuccess = false, Message = "EndTime must be in HH:mm:ss format" };

            if (endTime <= startTime)
                return new ResponseBaseModel { IsSuccess = false, Message = "EndTime must be after StartTime" };

            return new ResponseBaseModel { IsSuccess = true, Message = "Validatin successfully."};

        }
    }
}

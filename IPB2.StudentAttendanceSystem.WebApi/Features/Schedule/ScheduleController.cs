using IPB2.EFCore.Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Schedule
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        public ScheduleController(IScheduleService service)
        {
            _scheduleService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedules()
        {
            List<ScheduleModel> result = await _scheduleService.GetAllScheduleAsync();           

            return result.Count > 0 ? Ok(new GetAllScheduleResponse
            {
                IsSuccess = true,
                Message = "Get all schedule successfully.",
                data = result
            }) : Ok(new GetAllScheduleResponse
            {
                IsSuccess = true,
                Message = "No data.",
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(CreateScheduleRequest request)
        {
            return Ok();
        }

        [HttpPut("{id}")] // entire object (if not exist, create new one)(if exit, update existing one)
        public async Task<IActionResult> UpsertSchedule()
        {
            return Ok();
        }

        [HttpPatch("{id}")] // partially update
        public async Task<IActionResult> UpSchedule()
        {
            return Ok();
        }
    }
}

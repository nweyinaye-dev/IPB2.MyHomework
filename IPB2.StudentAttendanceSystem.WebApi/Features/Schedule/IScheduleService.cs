using IPB2.StudentAttendanceSystem.WebApi.Common;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Schedule
{
    public interface IScheduleService
    {
        Task<ResponseTypes> SaveScheduleAsync(CreateScheduleRequest req);
        Task<List<ScheduleModel>> GetAllScheduleAsync();
        Task<ScheduleModel> GetScheduleByIdAsync(string id);
        Task<ResponseTypes> DeleteScheduleAsync(string id);
        Task<ResponseTypes> UpdateScheduleEntityAsync(CreateScheduleRequest req, string id);
        Task<ResponseTypes> UpdateScheduleAsync(CreateScheduleRequest req, string id);
    }
}

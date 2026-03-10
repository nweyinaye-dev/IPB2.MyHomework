namespace IPB2.StudentAttendanceSystem.WebApi.Features.Schedule
{
    public interface IScheduleService
    {
        Task<int> SaveScheduleAsync(CreateScheduleRequest req);
        Task<List<ScheduleModel>> GetAllScheduleAsync();
    }
}

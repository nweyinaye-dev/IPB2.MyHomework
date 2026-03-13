using IPB2.StudentAttendanceSystem.MinimumApi.Common;

namespace IPB2.StudentAttendanceSystem.MinimumApi.Features.Schedule
{
    public interface IScheduleService
    {
        Task<ServiceResponse> SaveScheduleAsync(CreateScheduleRequest req);
        Task<List<ScheduleModel>> GetAllScheduleAsync(int pageNo, int pageSize);
        Task<ServiceResponse> DeleteScheduleAsync(string id);
        Task<ServiceResponse> UpdateScheduleAsync(CreateScheduleRequest req, string id);
        Task<ServiceResponse> UpdatePatchScheduleAsync(UpdatePatchScheduleRequest req, string id);
    }
}

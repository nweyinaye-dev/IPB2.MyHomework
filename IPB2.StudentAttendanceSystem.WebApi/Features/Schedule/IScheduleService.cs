using IPB2.StudentAttendanceSystem.WebApi.Common;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Schedule
{
    public interface IScheduleService
    {
        Task<ResponseTypes> SaveScheduleAsync(CreateScheduleRequest req);
        Task<List<ScheduleModel>> GetAllScheduleAsync(int pageNo, int pageSize);
        Task<ResponseTypes> DeleteScheduleAsync(string id);
        Task<ResponseTypes> UpdateScheduleAsync(CreateScheduleRequest req, string id);
        Task<ResponseTypes> UpdatePatchScheduleAsync(UpdatePatchScheduleRequest req, string id);
    }
}

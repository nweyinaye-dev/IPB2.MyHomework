using IPB2.StudentAttendanceSystem.MinimumApi.Common;
namespace IPB2.StudentLeaveSystem.MinimumApi.Features.Leave
{
    public interface ILeaveService
    {
        Task<ServiceResponse> SaveLeaveAsync(CreateLeaveRequest req);
        Task<List<LeaveModel>> GetAllLeaveAsync(int pageNo, int pageSize);
        Task<ServiceResponse> DeleteLeaveAsync(string id);
        Task<ServiceResponse> UpdateLeaveAsync(CreateLeaveRequest req, string id);
        Task<ServiceResponse> UpdatePatchLeaveAsync(UpdatePatchLeaveRequest req, string id);
    }
}

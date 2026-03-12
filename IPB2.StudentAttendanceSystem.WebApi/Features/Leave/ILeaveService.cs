using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentLeaveSystem.WebApi.Features.Leave;

namespace IPB2.StudentLeaveSystem.WebApi.Features.Leave
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

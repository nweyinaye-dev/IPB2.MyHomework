

using IPB2.StudentAttendanceSystem.MinimumApi.Common;

namespace IPB2.StudentAttendanceSystem.MinimumApi.Features.Attendance
{
    public interface IAttendanceService
    {
        Task<ServiceResponse> SaveAttendanceAsync(CreateAttendanceRequest req);
        Task<List<AttendanceLogModel>> GetAllAttendanceAsync(int pageNo, int pageSize);
        Task<ServiceResponse> DeleteAttendanceAsync(string id);
        Task<ServiceResponse> UpdateAttendanceAsync(CreateAttendanceRequest req, string id);
        Task<ServiceResponse> UpdatePatchAttendanceAsync(UpdatePatchAttendanceRequest req, string id);
        Task<ServiceResponse> CalculateAttendanceAsync(CalculateAttendanceRequest req);
    }
}

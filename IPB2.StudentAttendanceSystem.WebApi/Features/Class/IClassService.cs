using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Class;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Class
{
    public interface IClassService
    {
        Task<ServiceResponse> SaveClassAsync(CreateClassRequest req);
        Task<List<ClassModel>> GetAllClassAsync(int pageNo, int pageSize);
        Task<ServiceResponse> DeleteClassAsync(string id);
        Task<ServiceResponse> UpdateClassAsync(CreateClassRequest req, string id);
        Task<ServiceResponse> UpdatePatchClassAsync(UpdatePatchClassRequest req, string id);
    }
}

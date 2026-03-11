using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Class;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Class
{
    public interface IClassService
    {
        Task<ResponseTypes> SaveClassAsync(CreateClassRequest req);
        Task<List<ClassModel>> GetAllClassAsync(int pageNo, int pageSize);
        Task<ResponseTypes> DeleteClassAsync(string id);
        Task<ResponseTypes> UpdateClassAsync(CreateClassRequest req, string id);
        Task<ResponseTypes> UpdatePatchClassAsync(UpdatePatchClassRequest req, string id);
    }
}

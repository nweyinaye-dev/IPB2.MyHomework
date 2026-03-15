
using IPB2.StudentAttendanceSystem.MinimumApi.Common;

namespace IPB2.StudentAttendanceSystem.MinimumApi.Features.Teacher
{
    public interface ITeacherService
    {
        Task<ServiceResponse> SaveTeacherAsync(CreateTeacherRequest req);
        Task<List<TeacherModel>> GetAllTeacherAsync(int pageNo, int pageSize);
        Task<ServiceResponse> DeleteTeacherAsync(string id);
        Task<ServiceResponse> UpdateTeacherAsync(CreateTeacherRequest req, string id);
        Task<ServiceResponse> UpdatePatchTeacherAsync(UpdatePatchTeacherRequest req, string id);
    }
}

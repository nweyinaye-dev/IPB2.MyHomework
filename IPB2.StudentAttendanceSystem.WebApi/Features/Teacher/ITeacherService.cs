using IPB2.StudentAttendanceSystem.WebApi.Common;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Teacher
{
    public interface ITeacherService
    {
        Task<ResponseTypes> SaveTeacherAsync(CreateTeacherRequest req);
        Task<List<TeacherModel>> GetAllTeacherAsync(int pageNo, int pageSize);
        Task<ResponseTypes> DeleteTeacherAsync(string id);
        Task<ResponseTypes> UpdateTeacherAsync(CreateTeacherRequest req, string id);
        Task<ResponseTypes> UpdatePatchTeacherAsync(UpdatePatchTeacherRequest req, string id);
    }
}

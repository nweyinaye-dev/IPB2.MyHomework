using IPB2.StudentAttendanceSystem.WebApi.Common;


namespace IPB2.StudentAttendanceSystem.WebApi.Features.StudentEnroll
{
    public interface IStudentsEnrollService
    {
        Task<ServiceResponse> SaveStudentsEnrollAsync(CreateStudentsEnrollRequest req);
        Task<List<StudentsEnrollModel>> GetAllStudentsEnrollAsync(int pageNo, int pageSize);
        Task<ServiceResponse> DeleteStudentsEnrollAsync(string id);
        Task<ServiceResponse> UpdateStudentsEnrollAsync(CreateStudentsEnrollRequest req, string id);
        Task<ServiceResponse> UpdatePatchStudentsEnrollAsync(UpdatePatchStudentsEnrollRequest req, string id);
    }
}

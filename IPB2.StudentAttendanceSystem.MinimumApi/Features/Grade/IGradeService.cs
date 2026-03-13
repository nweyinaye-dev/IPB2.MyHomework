using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Grade;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Grade
{
    public interface IGradeService
    {
        Task<ServiceResponse> SaveGradeAsync(CreateGradeRequest req);
        Task<List<GradeModel>> GetAllGradeAsync(int pageNo, int pageSize);
        Task<ServiceResponse> DeleteGradeAsync(string id);
        Task<ServiceResponse> UpdateGradeAsync(CreateGradeRequest req, string id);
        Task<ServiceResponse> UpdatePatchGradeAsync(UpdatePatchGradeRequest req, string id);
    }
}

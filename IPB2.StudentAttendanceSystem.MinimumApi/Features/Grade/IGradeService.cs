

using IPB2.StudentAttendanceSystem.MinimumApi.Common;

namespace IPB2.StudentAttendanceSystem.MinimumApi.Features.Grade
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

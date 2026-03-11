using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Grade;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Grade
{
    public interface IGradeService
    {
        Task<ResponseTypes> SaveGradeAsync(CreateGradeRequest req);
        Task<List<GradeModel>> GetAllGradeAsync(int pageNo, int pageSize);
        Task<ResponseTypes> DeleteGradeAsync(string id);
        Task<ResponseTypes> UpdateGradeAsync(CreateGradeRequest req, string id);
        Task<ResponseTypes> UpdatePatchGradeAsync(UpdatePatchGradeRequest req, string id);
    }
}

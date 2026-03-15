

using IPB2.StudentAttendanceSystem.MinimumApi.Common;

namespace IPB2.StudentAttendanceSystem.MinimumApi.Features.Grade;

public class GradeModel
{
    public string Id { get; set; }
    public string GradeName { get; set; }
    public int FromPercent { get; set; }
    public int ToPercent { get; set; }
    public bool IsDelete { get; set; }
}

public class CreateGradeRequest
{
    public string GradeName { get; set; }
    public int FromPercent { get; set; }
    public int ToPercent { get; set; }
}
public class UpdatePatchGradeRequest
{
    public string? GradeName { get; set; }
    public int FromPercent { get; set; }
    public int ToPercent { get; set; }
}
public class GetAllGradeResponse : ResponseBaseModel
{
    public List<GradeModel> data { get; set; }
}
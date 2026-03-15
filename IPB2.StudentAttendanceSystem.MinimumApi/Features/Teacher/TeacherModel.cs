using IPB2.StudentAttendanceSystem.MinimumApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Common;

namespace IPB2.StudentAttendanceSystem.MinimumApi.Features.Teacher;

public class TeacherModel
{
    public string Id { get; set; }
    public string TeacherName { get; set; } = null!;
    public string TeacherPhoneno { get; set; } = null!;
    public string TeacherAddress { get; set; } = null!;
    public bool IsDelete { get; set; }
}

public class CreateTeacherRequest
{
    public string TeacherName { get; set; } = null!;
    public string TeacherPhoneno { get; set; } = null!;
    public string TeacherAddress { get; set; } = null!;
}

public class UpdatePatchTeacherRequest
{
    public string? TeacherName { get; set; } = null!;
    public string? TeacherPhoneno { get; set; } = null!;
    public string? TeacherAddress { get; set; } = null!;
}
public class GetAllTeacherResponse : ResponseBaseModel
{
    public List<TeacherModel> data { get; set; }
}

using IPB2.StudentAttendanceSystem.MinimumApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Common;

namespace IPB2.StudentAttendanceSystem.MinimumApi.Features.StudentEnroll;
public class StudentsEnrollModel
{
    public string Id { get; set; }
    public string StudentName { get; set; }
    public string StudentPhoneno { get; set; }
    public int StudentAge { get; set; }
    public string ClassName { get; set; }
    public bool IsDelete { get; set; }
}

public class CreateStudentsEnrollRequest
{
    public string StudentName { get; set; }
    public string StudentPhoneno { get; set; }
    public int StudentAge { get; set; }
    public string ClassId { get; set; }
}
public class UpdatePatchStudentsEnrollRequest
{
    public string? StudentName { get; set; }
    public string? StudentPhoneno { get; set; }
    public int StudentAge { get; set; }
    public string? ClassId { get; set; }
}
public class GetAllStudentsEnrollResponse : ResponseBaseModel
{
    public List<StudentsEnrollModel> data { get; set; }
}
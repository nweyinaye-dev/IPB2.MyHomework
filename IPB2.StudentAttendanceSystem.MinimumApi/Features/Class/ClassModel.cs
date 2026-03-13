
using IPB2.StudentAttendanceSystem.MinimumApi.Common;

namespace IPB2.StudentAttendanceSystem.MinimumApi.Features.Class;
public class ClassModel
{
    public string Id { get; set; }
    public string ClassName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int Duration { get; set; }
    public string ScheduleName { get; set; }
    public string TeacherName { get; set; }
    public bool IsDelete { get; set; }
}

public class CreateClassRequest
{
    public string ClassName { get; set; }
    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }
    public int Duration { get; set; }
    public string ScheduleId { get; set; }
    public string TeacherId { get; set; }
}
public class UpdatePatchClassRequest
{
    public string? ClassName { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int Duration { get; set; }
    public string? ScheduleId { get; set; }
    public string? TeacherId { get; set; }
}
public class GetAllClassResponse : ResponseBaseModel
{
    public List<ClassModel> data { get; set; }
}
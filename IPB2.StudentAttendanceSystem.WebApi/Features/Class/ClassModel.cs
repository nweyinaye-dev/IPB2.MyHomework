using IPB2.StudentAttendanceSystem.WebApi.Common;
namespace IPB2.StudentAttendanceSystem.WebApi.Features.Class;


public class ClassModel
{
    public string Id { get; set; }
    public string ClassName { get; set; }
    public DateOnly StartDate { get; set; }
    public int Duration { get; set; }
    public string ScheduleId { get; set; }
    public string TeacherId { get; set; }
    public bool IsDelete { get; set; }
}

public class CreateClassRequest
{
    public string ClassName { get; set; }
    public DateOnly StartDate { get; set; }
    public int Duration { get; set; }
    public string ScheduleId { get; set; }
    public string TeacherId { get; set; }
}
public class UpdatePatchClassRequest
{
    public string? ClassName { get; set; }
    public DateOnly? StartDate { get; set; }
    public int Duration { get; set; }
    public string? ScheduleId { get; set; }
    public string? TeacherId { get; set; }
}
public class GetAllClassResponse : ResponseBaseModel
{
    public List<ClassModel> data { get; set; }
}
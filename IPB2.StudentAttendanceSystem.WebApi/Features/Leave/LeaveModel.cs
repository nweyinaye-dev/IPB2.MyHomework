using IPB2.StudentAttendanceSystem.WebApi.Common;
namespace IPB2.StudentLeaveSystem.WebApi.Features.Leave;

public class LeaveModel
{
    public string Id { get; set; } = null!;

    public DateOnly LeaveDate { get; set; }

    public string ClassId { get; set; } = null!;

    public string StudentEnrollId { get; set; } = null!;

    public bool IsDelete { get; set; }
}
public class CreateLeaveRequest
{
    public DateOnly LeaveDate { get; set; }

    public string ClassId { get; set; } = null!;

    public string StudentEnrollId { get; set; } = null!;
}
public class UpdatePatchLeaveRequest
{
    public DateOnly? LeaveDate { get; set; }

    public string? ClassId { get; set; } = null!;

    public string? StudentEnrollId { get; set; } = null!;
}
public class GetAllLeaveResponse : ResponseBaseModel
{
    public List<LeaveLogModel> data { get; set; }
}
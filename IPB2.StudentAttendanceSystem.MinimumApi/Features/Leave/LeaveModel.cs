
using IPB2.StudentAttendanceSystem.MinimumApi.Common;

namespace IPB2.StudentLeaveSystem.MinimumApi.Features.Leave;

public class LeaveModel
{
    public string Id { get; set; } = null!;

    public DateOnly LeaveDate { get; set; }

    public string ClassName { get; set; } = null!;

    public string StudentName { get; set; } = null!;

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
    public List<LeaveModel> data { get; set; }
}
using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Schedule;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Attendance;

public class AttendanceLogModel
{
    public string Id { get; set; }
    public DateOnly AttendanceDate { get; set; }
    public string ClassName { get; set; }
    public string StudentName { get; set; }
    public string TimeIn { get; set; }
    public string TimeOut { get; set; }
    public bool IsDelete { get; set; }
}
public class CreateAttendanceRequest
{
    public DateOnly AttendanceDate { get; set; }
    public string ClassId { get; set; }
    public string StudentEnrollId { get; set; }
    public string TimeIn { get; set; }
    public string TimeOut { get; set; }
}
public class UpdatePatchAttendanceRequest
{
    public DateOnly? AttendanceDate { get; set; }
    public string? ClassId { get; set; }
    public string? StudentEnrollId { get; set; }
    public string? TimeIn { get; set; }
    public string? TimeOut { get; set; }
}

public class CalculateAttendanceRequest
{
    public string StudentEnrollId { get; set; }
    public string ClassId { get; set; }
    public DateOnly FromDate { get; set; }
    public DateOnly ToDate { get; set; }
}

public class AttendanceModel
{
    public string Id { get; set; }

    public DateOnly AttendanceDate { get; set; }

    public string ClassId { get; set; }

    public string StudentEnrollId { get; set; }

    public string TimeIn { get; set; }

    public string TimeOut { get; set; }

    public string Status { get; set; }

    public string Late { get; set; }
    public bool IsDelete { get; set; }

}
public class GetAllAttendanceResponse : ResponseBaseModel
{
    public List<AttendanceLogModel> data { get; set; }
}
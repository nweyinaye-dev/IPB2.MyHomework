using IPB2.StudentAttendanceSystem.WebApi.Common;
using IPB2.StudentAttendanceSystem.WebApi.Features.Attendance;
using System.Text.Json.Serialization;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Report;

public class AttendanceReportModel
{
    public string StudentName { get; set; }
    public DateOnly AttendanceDate { get; set; }
    public string ClassName { get; set; }
    public string TimeIn { get; set; }
    public string TimeOut { get; set; }
    public string Status { get; set; }
    public string LateMinutes { get; set; }

}

public class AttendanceSummaryReportModel
{
    public string StudentName { get; set; }
    public string ClassName { get; set; }
    public int TotalPresentDays { get; set; }
    public int TotalAbsentDays { get; set; }
    public int TotalLateDays { get; set; }
    public int TotalLeaveDays { get; set; }

    [JsonIgnore]
    public double Percent { get; set; }

    [JsonIgnore] 
    public int TotalPossibleDays { get; set; }
    public string Percentage => $"{Percent:F2}%";
    public string GradeName { get; set; }
   

}


public class AttendanceReportRequest
{
    public string StudentEnrollId { get; set; }
    public string ClassId { get; set; }
    public DateOnly FromDate { get; set; }
    public DateOnly ToDate { get; set; }
}
public class AttendanceReportResponse : ResponseBaseModel
{
    public List<AttendanceReportModel> data { get; set; }
}
public class AttendanceSummaryReportResponse : ResponseBaseModel
{
    public List<AttendanceSummaryReportModel> data { get; set; }
}
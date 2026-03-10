using IPB2.StudentAttendanceSystem.WebApi.Common;

namespace IPB2.StudentAttendanceSystem.WebApi.Features.Schedule;

public class ScheduleModel
{
    public string Id { get; set; }
    public string ScheduleName { get; set; } 
    public string ScheduleDays { get; set; } 
    public string StartTime { get; set; } 
    public string EndTime { get; set; }
    public int IsDelete { get; set; }
}

public class CreateScheduleRequest
{
    public string ScheduleName { get; set; } = null!;
    public string ScheduleDays { get; set; } = null!;
    public string StartTime { get; set; } 
    public string EndTime { get; set; } 
}

public class GetAllScheduleResponse : ResponseBaseModel
{
    public List<ScheduleModel> data { get; set; }
}
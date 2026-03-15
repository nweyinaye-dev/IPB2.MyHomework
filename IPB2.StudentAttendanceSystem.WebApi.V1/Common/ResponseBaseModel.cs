namespace IPB2.StudentAttendanceSystem.WebApi.Common;

public class ResponseBaseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}
public class ServiceResponse
{
    public ResponseTypes Status { get; set; }
    public string? Message { get; set; }

}
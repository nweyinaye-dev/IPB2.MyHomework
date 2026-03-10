namespace IPB2.StudentAttendanceSystem.WebApi.Common;

public enum Days
{
    None,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}
public enum ResponseTypes
{
    None = 0,
    NotFound = 404,
    AlreadyExists = 409,
    AlreadyDeleted = -1,
    Success = 200
}
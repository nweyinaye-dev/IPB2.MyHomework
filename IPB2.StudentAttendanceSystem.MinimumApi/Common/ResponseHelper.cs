namespace IPB2.StudentAttendanceSystem.MinimumApi.Common
{
    public class ResponseHelper
    {
        public static IResult ConvertResponseType(ServiceResponse status)
        {
            switch (status.Status)
            {
                case ResponseTypes.NotFound:
                    return Results.NotFound(
                    new ResponseBaseModel { IsSuccess = false, Message = status.Message ?? "Resource not found." });
                case ResponseTypes.AlreadyDeleted:
                    return Results.Ok(
                     new ResponseBaseModel { IsSuccess = false, Message = status.Message ??  "Resource already deleted." });
                case ResponseTypes.AlreadyExists:
                    return Results.Ok(
                    new ResponseBaseModel { IsSuccess = false, Message = status.Message ?? "Resource already exists." });
                case ResponseTypes.None:
                    return Results.Ok(
                    new ResponseBaseModel { IsSuccess = false, Message = status.Message ?? "Failed. No rows were affected." });
                case ResponseTypes.Success:
                    return  Results.Ok(new ResponseBaseModel { IsSuccess = true, Message = status.Message ?? "Process done successfully." });
                default:
                    return Results.Json(
                        data: new ResponseBaseModel { IsSuccess = false, Message = status.Message ?? "Unexpected error." },
                        statusCode: 500
                    );
            }
          
        }
    }
}

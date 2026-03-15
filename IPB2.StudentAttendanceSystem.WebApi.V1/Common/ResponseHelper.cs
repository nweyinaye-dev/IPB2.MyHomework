using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace IPB2.StudentAttendanceSystem.WebApi.Common
{
    public class ResponseHelper
    {
        public static IActionResult ConvertResponseType(ServiceResponse status)
        {
            switch (status.Status)
            {
                case ResponseTypes.NotFound:
                    return new Microsoft.AspNetCore.Mvc.NotFoundObjectResult(
                    new ResponseBaseModel { IsSuccess = false, Message = status.Message ?? "Resource not found." });
                case ResponseTypes.AlreadyDeleted:
                    return new Microsoft.AspNetCore.Mvc.OkObjectResult(
                     new ResponseBaseModel { IsSuccess = false, Message = status.Message ??  "Resource already deleted." });
                case ResponseTypes.AlreadyExists:
                    return new Microsoft.AspNetCore.Mvc.ConflictObjectResult(
                    new ResponseBaseModel { IsSuccess = false, Message = status.Message ?? "Resource already exists." });
                case ResponseTypes.None:
                    return new Microsoft.AspNetCore.Mvc.OkObjectResult(
                    new ResponseBaseModel { IsSuccess = false, Message = status.Message ?? "Failed. No rows were affected." });
                case ResponseTypes.Success:
                    return new Microsoft.AspNetCore.Mvc.OkObjectResult(
                    new ResponseBaseModel { IsSuccess = true, Message = status.Message ?? "Process done successfully." });
                default:
                    return new Microsoft.AspNetCore.Mvc.ObjectResult(
                     new ResponseBaseModel { IsSuccess = false, Message = status.Message ?? "Unexpected error." })
                    {
                        StatusCode = 500
                    };
            }
          
        }
    }
}

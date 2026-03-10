using Microsoft.AspNetCore.Mvc;

namespace IPB2.StudentAttendanceSystem.WebApi.Common
{
    public class ResponseHelper
    {
        public static IActionResult ConvertResponseType(ResponseTypes status, string successMessage = "Operation successful.")
        {
            switch (status)
            {
                case ResponseTypes.NotFound:
                    return new Microsoft.AspNetCore.Mvc.NotFoundObjectResult(
                    new ResponseBaseModel { IsSuccess = false, Message = "Resource not found." });
                case ResponseTypes.AlreadyDeleted:
                    return new Microsoft.AspNetCore.Mvc.OkObjectResult(
                     new ResponseBaseModel { IsSuccess = false, Message = "Resource already deleted." });
                case ResponseTypes.AlreadyExists:
                    return new Microsoft.AspNetCore.Mvc.ConflictObjectResult(
                    new ResponseBaseModel { IsSuccess = false, Message = "Resource already exists." });
                case ResponseTypes.None:
                    return new Microsoft.AspNetCore.Mvc.OkObjectResult(
                    new ResponseBaseModel { IsSuccess = false, Message = "Failed. No rows were affected." });
                case ResponseTypes.Success:
                    return new Microsoft.AspNetCore.Mvc.OkObjectResult(
                    new ResponseBaseModel { IsSuccess = true, Message = successMessage });
                default:
                    return new Microsoft.AspNetCore.Mvc.ObjectResult(
                     new ResponseBaseModel { IsSuccess = false, Message = "Unexpected error." })
                    {
                        StatusCode = 500
                    };
            }

          
        }
    }
}

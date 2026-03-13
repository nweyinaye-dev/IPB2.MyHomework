using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace IPB2.StudentAttendanceSystem.MinimumApi.Common;

public class GeneralUtilities
{
    public static ResponseBaseModel ValidateDateFormat(string dateStr, string expectedFormat = "yyyy-MM-dd")
    {
        if (!DateTime.TryParseExact(dateStr, expectedFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return new ResponseBaseModel { IsSuccess = true, Message = dateStr }; // Valid format
        }
        else
        {
            return new ResponseBaseModel { IsSuccess = false, Message = $"Invalid date format. Expected format: {expectedFormat}." }; ;
        }

    }
}

using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblAttendance
{
    public string Id { get; set; } = null!;

    public DateOnly AttendanceDate { get; set; }

    public string ClassId { get; set; } = null!;

    public string StudentEnrollId { get; set; } = null!;

    public string TimeIn { get; set; } = null!;

    public string TimeOut { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Late { get; set; } = null!;

    public bool IsDelete { get; set; }
}

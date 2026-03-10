using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblSchedule
{
    public string Id { get; set; } = null!;

    public string ScheduleName { get; set; } = null!;

    public string ScheduleDays { get; set; } = null!;

    public string StartTime { get; set; } = null!;

    public string EndTime { get; set; } = null!;

    public bool IsDelete { get; set; }
}

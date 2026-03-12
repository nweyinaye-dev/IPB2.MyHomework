using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblClass
{
    public string Id { get; set; } = null!;

    public string ClassName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int Duration { get; set; }

    public string ScheduleId { get; set; } = null!;

    public string TeacherId { get; set; } = null!;

    public bool IsDelete { get; set; }
}

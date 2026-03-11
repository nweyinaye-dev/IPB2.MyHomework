using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblLeave
{
    public string Id { get; set; } = null!;

    public DateOnly LeaveDate { get; set; }

    public string ClassId { get; set; } = null!;

    public string StudentEnrollId { get; set; } = null!;

    public bool IsDelete { get; set; }
}

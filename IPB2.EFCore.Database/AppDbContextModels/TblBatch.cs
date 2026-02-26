using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblBatch
{
    public string BatchId { get; set; } = null!;

    public string BatchName { get; set; } = null!;

    public int Duration { get; set; }

    public string FromDate { get; set; } = null!;

    public string ToDate { get; set; } = null!;

    public string InstructorName { get; set; } = null!;

    public bool IsDelete { get; set; }
}

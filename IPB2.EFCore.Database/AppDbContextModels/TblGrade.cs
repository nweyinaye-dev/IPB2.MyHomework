using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblGrade
{
    public string Id { get; set; } = null!;

    public string GradeName { get; set; } = null!;

    public int FromPercent { get; set; }

    public int ToPercent { get; set; }

    public bool IsDelete { get; set; }
}

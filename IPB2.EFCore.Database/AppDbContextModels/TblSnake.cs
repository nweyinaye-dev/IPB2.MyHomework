using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblSnake
{
    public int Id { get; set; }

    public string Mmname { get; set; } = null!;

    public string EngName { get; set; } = null!;

    public string? Detail { get; set; }

    public string IsPoison { get; set; } = null!;

    public string? IsDanger { get; set; }
}

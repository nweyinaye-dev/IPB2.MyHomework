using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblMyanmarMonth
{
    public int Id { get; set; }

    public string MonthMm { get; set; } = null!;

    public string MonthEn { get; set; } = null!;

    public string FestivalMm { get; set; } = null!;

    public string FestivalEn { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Detail { get; set; } = null!;
}

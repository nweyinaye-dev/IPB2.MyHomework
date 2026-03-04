using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblMyanmarMonthsImg
{
    public int Id { get; set; }

    public int MonthId { get; set; }

    public string ImgUrl { get; set; } = null!;

    public string ImgName { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblStudent
{
    public string Id { get; set; } = null!;

    public string StudentName { get; set; } = null!;

    public string StudentPhoneno { get; set; } = null!;

    public string ClassId { get; set; } = null!;

    public bool IsDelete { get; set; }
}

using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblTeacher
{
    public string Id { get; set; } = null!;

    public string TeacherName { get; set; } = null!;

    public string TeacherPhoneno { get; set; } = null!;

    public string? TeacherAddress { get; set; }

    public bool IsDelete { get; set; }
}

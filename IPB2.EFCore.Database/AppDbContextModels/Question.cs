using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class Question
{
    public int QuestionId { get; set; }

    public string QuestionName { get; set; } = null!;

    public string QuestionDesp { get; set; } = null!;
}

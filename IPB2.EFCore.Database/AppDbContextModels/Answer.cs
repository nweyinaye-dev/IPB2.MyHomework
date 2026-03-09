using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class Answer
{
    public int AnswerId { get; set; }

    public string AnswerImageUrl { get; set; } = null!;

    public string AnswerName { get; set; } = null!;

    public string AnswerDesp { get; set; } = null!;

    public int QuestionId { get; set; }
}

using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class BlogDetail
{
    public int BlogDetailId { get; set; }

    public int? BlogId { get; set; }

    public string? BlogContent { get; set; }
}

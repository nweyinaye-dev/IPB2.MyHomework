using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblBook
{
    public Guid BookId { get; set; }

    public string BookName { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Category { get; set; } = null!;

    public decimal Price { get; set; }

    public int Qty { get; set; }

    public bool IsDelete { get; set; }
}

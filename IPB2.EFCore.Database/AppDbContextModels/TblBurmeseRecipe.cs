using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class TblBurmeseRecipe
{
    public string Guid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Ingredients { get; set; } = null!;

    public string CookingInstructions { get; set; } = null!;

    public int UserTypeId { get; set; }

    public bool IsDelete { get; set; }
}

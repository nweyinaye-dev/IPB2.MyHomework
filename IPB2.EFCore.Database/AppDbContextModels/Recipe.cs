using System;
using System.Collections.Generic;

namespace IPB2.EFCore.Database.AppDbContextModels;

public partial class Recipe
{
    public string Guid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Ingredients { get; set; }

    public string? CookingInstructions { get; set; }

    public int UserTypeId { get; set; }

    public bool IsDelete { get; set; }
}

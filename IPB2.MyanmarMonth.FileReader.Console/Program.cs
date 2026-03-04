
using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.MyanmarMonth.FileReader.Console;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

var json = File.ReadAllText("MyanmarMonths.json");
var item = JsonConvert.DeserializeObject<MyanmarMonthModel>(json);

using (var dbcontext = new AppDbContext())
{
    var entityList = item.Tbl_Months.Select(x => new TblMyanmarMonth
    {
        Id = x.Id,
        MonthMm = x.MonthMm,
        MonthEn = x.MonthEn,
        FestivalMm = x.FestivalMm,
        FestivalEn = x.FestivalEn,
        Description = x.Description,
        Detail = x.Detail
    }).ToList();

    await dbcontext.TblMyanmarMonths.AddRangeAsync(entityList);
    await dbcontext.SaveChangesAsync();
}

Console.ReadLine();
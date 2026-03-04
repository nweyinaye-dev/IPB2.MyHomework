namespace IPB2.MyanmarMonths.WebApi.Models;

public class GetAllMyanmarMonthMmResponse
{
    public bool IsSuccess { get; set; }   
    public string Message { get; set; }
    public List<MyanmarMonthMm> List { get; set; }
}
public class MyanmarMonthMm 
{
    public int Id { get; set; }
    public string MonthMm { get; set; }
    public string? ImageUrl { get; set; } 
    public string? ImageName { get; set; } 
}
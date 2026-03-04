namespace IPB2.MyanmarMonths.WebApi.Models;

public class GetAllMyanmarMonthResponse
{
    public bool isIsSuccess { get; set; }   
    public string message { get; set; }
    public MyanmarMonthModel lst { get; set; }
}
public class MyanmarMonthModel
{
   // public [] Tbl_Months { get; set; }
}
using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.MyanmarMonths.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPB2.MyanmarMonths.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyanmarMonthController : ControllerBase
    {
        private readonly AppDbContext _db;

        public MyanmarMonthController()
        {
            _db = new AppDbContext();
        }

        [HttpGet]
        public IActionResult GetAllMyanmarMonthMn()
        {
            var lst = _db.TblMyanmarMonths
               .Select(x => new MyanmarMonthMm
               {
                   Id = x.Id,
                   MonthMm = x.MonthMm,
                   ImageUrl = _db.TblMyanmarMonthsImgs
                      .Where(img => img.MonthId == x.Id)
                      .Select(img => img.ImgUrl)
                      .FirstOrDefault(),
                   ImageName = _db.TblMyanmarMonthsImgs
                      .Where(img => img.MonthId == x.Id)
                      .Select(img => img.ImgName)
                      .FirstOrDefault(),
               })
               .ToList();

                return Ok(new GetAllMyanmarMonthMmResponse
                {
                    IsSuccess = true,
                    Message = "success",
                    List = lst
                });
        }

        [HttpGet("{id}")]
        public IActionResult GetAllMyanmarMonthById(int id)
        {
            var item = _db.TblMyanmarMonths.FirstOrDefault(x => x.Id == id);
            if (item is null)
            {
                return NotFound(new { Message = "No data found." });
            }

            return Ok(item);
        }
     
        }
}

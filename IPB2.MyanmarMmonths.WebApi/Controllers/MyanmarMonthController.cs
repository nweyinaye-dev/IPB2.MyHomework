using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPB2.MyanmarMonths.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyanmarMonthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMyanamrMonths()
        {
            return Ok();
        }
    }
}

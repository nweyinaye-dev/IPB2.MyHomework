using IPB2.EFCore.Database.AppDbContextModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPB2.DreamDictionary.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DreamDictionaryController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DreamDictionaryController()
        {
            _db = new AppDbContext();
        }

        // 1️⃣ Get Blog Titles
        [HttpGet]
        public async Task<IActionResult> GetBlogTitles()
        {

            var data = await _db.BlogHeaders
                .Select(x => new
                {
                    x.BlogId,
                    x.BlogTitle
                })
                .OrderBy(x => x.BlogId)
                .ToListAsync();

            return Ok(data);
        }

        // 2️⃣ Get Blog Content by BlogId
        [HttpGet("{blogId}")]
        public async Task<IActionResult> GetBlogContent(int blogId)
        {
            var data = await _db.BlogDetails
                .Where(x => x.BlogId == blogId)
                .Select(x => new
                {
                    x.BlogDetailId,
                    x.BlogId,
                    x.BlogContent
                })
                .ToListAsync();

            return Ok(data);
        }

        // 3️⃣ Search BlogTitle
        [HttpGet("Search")]
        public async Task<IActionResult> SearchBlogTitle(string keyword)
        {
            var data = await _db.BlogHeaders
                  .Where(x => EF.Functions.Like(x.BlogTitle, $"%{keyword}%", "\\"))
                  .Select(x => new
                  {
                      x.BlogId,
                      x.BlogTitle
                  })
                  .ToListAsync();

            return Ok(data);
        }

        // 3️⃣ Search BlogTitle
        [HttpGet("SearchContent")]
        public async Task<IActionResult> SearchBlogContent(string keyword)
        {
            var escapedKeyword = EscapeLikePattern(keyword);

            var data = await _db.BlogHeaders
                .Where(x => EF.Functions.Like(x.BlogTitle, $"%{escapedKeyword}%"))
                .Select(x => new
                {
                    x.BlogId,
                    x.BlogTitle
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("GetWithPagination")]
        public async Task<IActionResult> GetProducts(int pageNumber = 1, int pageSize = 10)
        {
            var products = await _db.BlogHeaders
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(products);
        }
        private static string EscapeLikePattern(string input)
        {
            return input
                .Replace("[", "[[]")
                .Replace("%", "[%]")
                .Replace("_", "[_]");
        }
    }
}
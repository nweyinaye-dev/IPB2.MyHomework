using IPB2.EFCore.Database.AppDbContextModels;
using Microsoft.EntityFrameworkCore;

namespace IPB2.MyanmarMonths.WebApi.Daos
{
    public class MyanmarMonthDao
    {
        private readonly AppDbContext _context = new AppDbContext();

        public async Task<List<TblMyanmarMonth>> GetAllAsync()
        {
            return await _context.TblMyanmarMonths.ToListAsync();
        }
    }
}

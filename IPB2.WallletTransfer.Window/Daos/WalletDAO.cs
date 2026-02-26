using IPB2.EFCore.Database.AppDbContextModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPB2.WallletTransfer.Window.Daos
{
    public class WalletDAO : IWalletDAO
    {
        private readonly AppDbContext _context = new AppDbContext();

        public async Task<List<TblAccount>> GetAllAsync()
        {
            return await _context.TblAccounts.ToListAsync();
        }

        public async Task<TblAccount?> GetByMobileAsync(string mobileNo)
        {
            return await _context.TblAccounts
                .FirstOrDefaultAsync(x => x.MobileNo == mobileNo);
        }

        public async Task CreateAsync(TblAccount wallet)
        {
            await _context.TblAccounts.AddAsync(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TblAccount wallet)
        {
            _context.TblAccounts.Update(wallet);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var wallet = await _context.TblAccounts.FindAsync(id);
            if (wallet != null)
            {
                _context.TblAccounts.Remove(wallet);
                await _context.SaveChangesAsync();
            }
        }

        Task<TblAccount?> IWalletDAO.GetByMobileAsync(string mobileNo)
        {
            throw new NotImplementedException();
        }       

        Task<List<TblAccount>> IWalletDAO.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CreateTransactioinAsync(TblTransactionRecord transaction)
        {
            await _context.TblTransactionRecords.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}

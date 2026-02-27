using IPB2.EFCore.Database.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPB2.WallletTransfer.Window.Daos
{
    public interface IWalletDAO
    {
        Task<TblAccount?> GetByMobileAsync(string mobileNo);
        Task CreateAsync(TblAccount wallet);
        Task UpdateAsync(TblAccount wallet);
        Task DeleteAsync(int id);
        Task<List<TblAccount>> GetAllAsync();

        Task<List<TblTransactionRecord>> GetAllTransactionAsync(string mobileNo);
        Task CreateTransactioinAsync(TblTransactionRecord wallet);
    }
}

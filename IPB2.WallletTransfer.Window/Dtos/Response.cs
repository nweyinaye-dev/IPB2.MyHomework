using IPB2.EFCore.Database.AppDbContextModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPB2.WallletTransfer.Window.Dtos
{
    public  class Response
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = "";
    }
    public class TransactionHistoryResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; } = "";

        public List<TblTransactionRecord> list { get; set; } = null;
    }
}

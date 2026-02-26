using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPB2.WallletTransfer.Window.Dtos
{
    public class TransferRequest
    {
        public TransferRequest(string senderMobileno, string receiverMobileno, decimal balance)
        {
            this.SenderMobileno = senderMobileno;
            this.ReceiverMobileno = receiverMobileno;
            this.Balance = balance;
        }

        public string SenderMobileno { get; set; }
        public string ReceiverMobileno { get; set; }
        public decimal Balance { get; set; }
    }
}

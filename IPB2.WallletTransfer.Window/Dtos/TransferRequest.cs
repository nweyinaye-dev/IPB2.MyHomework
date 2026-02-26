using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPB2.WallletTransfer.Window.Dtos
{
    public class TransferRequest
    {
        public TransferRequest(string senderMobileno, string receiverMobileno, decimal balance, string password)
        {
            this.SenderMobileno = senderMobileno;
            this.ReceiverMobileno = receiverMobileno;
            this.Balance = balance;
            Password = password;
        }

        public string SenderMobileno { get; set; }
        public string ReceiverMobileno { get; set; }
        public decimal Balance { get; set; }
        public string Password { get; set; }
    }

    public class WithdrawRequest
    {
        public WithdrawRequest(string mobileno, string password, decimal balance)
        {
            this.Mobileno = mobileno;
            this.Balance = balance;
            Password = password;
        }

        public string Mobileno { get; set; }
        public decimal Balance { get; set; }
        public string Password { get; set; }
    }
}

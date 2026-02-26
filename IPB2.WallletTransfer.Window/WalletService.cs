
using Azure.Core;
using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.WallletTransfer.Window.Daos;
using IPB2.WallletTransfer.Window.Dtos;
using Response = IPB2.WallletTransfer.Window.Dtos.Response;

namespace IPB2.WallletTransfer.Window
{
    public class WalletService : WalletDAO
    {
        public async Task<Response> CreateAccount(CreateAccountRequestDto req)
        {

            var exitAccount = await GetByMobileAsync(req.MobileNo);
            if (exitAccount != null)
                return new Response { isSuccess = false, Message = "Your mobileNo  already exits." };

            var newAccount = new TblAccount
            {
                Id = Guid.NewGuid().ToString(),
                Name = req.Name,
                MobileNo = req.MobileNo,
                Password = req.Password,
                Balance = 0,
                IsDelete = false,
            };
            await CreateAsync(newAccount);
            return new Response { isSuccess = true, Message = "Transfer success." };
        }

        public async Task<Response> Transfer(TransferRequest request)
        {
            var sender = await GetByMobileAsync(request.SenderMobileno);
            if (sender == null)
                return  new Response{ isSuccess = false, Message = "Sender not found." };

            if (sender.Password != request.Password)
                return new Response { isSuccess = false, Message = "Invalid password." };

            if (sender.Balance < request.Balance)
                return new Response { isSuccess = false, Message = "Insufficient balance." };

            var receiver = await GetByMobileAsync(request.ReceiverMobileno);

            if (receiver == null)
                return new Response { isSuccess = false, Message = "Receiver not found." };

            if (sender.MobileNo == receiver.MobileNo)
                return new Response { isSuccess = false, Message = "Cannot send to yourself." };

            sender.Balance -= request.Balance;
            receiver.Balance += request.Balance;

            await UpdateAsync(sender);
            await UpdateAsync(receiver);

            string sharedTxnId = Guid.NewGuid().ToString().ToUpper()[..8];
            DateTime now = DateTime.Now;
            var senderRecord = new TblTransactionRecord {
                TransactionId = Guid.NewGuid().ToString(),
                TxnId = sharedTxnId,
                FromMobileNo = sender.MobileNo,
                ToMobileNo = receiver.MobileNo,
                Amount = request.Balance,
                Message = "send",
                Timestamp = now
            };


            var receiveRecord = new TblTransactionRecord
            {
                TransactionId = Guid.NewGuid().ToString(),
                TxnId = sharedTxnId,
                FromMobileNo = sender.MobileNo,
                ToMobileNo = receiver.MobileNo,
                Amount = request.Balance,
                Message = "receive",
                Timestamp = now
            };

            await CreateTransactioinAsync(senderRecord);
            await CreateTransactioinAsync(receiveRecord);

            return new Response { isSuccess = true, Message = "Transfer success." };


        }
        
        public async Task<Response> Withdraw(WithdrawRequest req) {
            var account = await GetByMobileAsync(req.Mobileno);
            if (account == null)
                return new Response { isSuccess = false, Message = "Account not found." };

            if (account.Password != req.Password)
                return new Response { isSuccess = false, Message = "Invalid password." };

            if (account.Balance < req.Balance)
                return new Response { isSuccess = false, Message = "Insufficient balance." };

            account.Balance -= req.Balance;
            await UpdateAsync(account);

            return new Response { isSuccess = true, Message = $"Withdraw successfully.Your current balance is {account.Balance}." };

        }

        public async Task<Response> Deposit(WithdrawRequest req)
        {
            var account = await GetByMobileAsync(req.Mobileno);
            if (account == null)
                return new Response { isSuccess = false, Message = "Account not found." };

            if (account.Password != req.Password)
                return new Response { isSuccess = false, Message = "Invalid password." };

            account.Balance += req.Balance;
            await UpdateAsync(account);

            return new Response { isSuccess = true, Message = $"Deposit successfully.Your current balance is {account.Balance}." };

        }

    }
}

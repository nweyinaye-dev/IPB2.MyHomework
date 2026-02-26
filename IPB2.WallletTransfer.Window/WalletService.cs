using Azure;
using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.WallletTransfer.Window.Daos;
using IPB2.WallletTransfer.Window.Dtos;
using Response = IPB2.WallletTransfer.Window.Dtos.Response;

namespace IPB2.WallletTransfer.Window
{
    public class WalletService : WalletDAO
    {     
        public async Task<Response> Transfer(TransferRequest request)
        {

            var sender = await GetByMobileAsync(request.SenderMobileno);
            if (sender == null)
                return  new Response{ isSuccess = false, Message = "Sender not found." };


            //if (sender.Password != request.Password)
            //    return new Response { isSuccess = false, Message = "Sender not found." };
            //return Response.Fail("Invalid password.");

            if (sender.Balance < request.Balance)
                return new Response { isSuccess = false, Message = "Insufficient balance." };

            var receiver = await GetByMobileAsync(request.ReceiverMobileno);
            if (receiver == null)
                return new Response { isSuccess = false, Message = "Receiver not found." };

            sender.Balance -= request.Balance;
            receiver.Balance += request.Balance;

            await UpdateAsync(sender);
            await UpdateAsync(receiver);

            return new Response { isSuccess = true, Message = "Transfer success." };


        }
    }
}

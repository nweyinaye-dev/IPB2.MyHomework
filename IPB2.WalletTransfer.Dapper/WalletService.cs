using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace IPB2.WalletTransfer.Dapper
{    
    public class WalletService
    {
        SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "IPB2",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true,
        };
        
        public WalletDto GetWallet(string MobileNo)
        {
            using(IDbConnection db = new SqlConnection(connectionString.ConnectionString))
            {
                db.Open();

                var sql = $@"SELECT [WalletID]
                          ,[FullName]
                          ,[MobileNo]
                          ,[Balance]
                          ,[IsDelete]
                      FROM [dbo].[Tbl_Wallet] WHERE MobileNo = '{MobileNo}' and isDelete = 0";

                WalletDto result = db.Query<WalletDto>(sql).FirstOrDefault()!;
                return result;
            }
            
        }
        public Response CreateWallet(CreateWalletRequest request) {
            if (string.IsNullOrEmpty(request.FullName))
            {
                return new Response { isSuccess = false, Message = "Name is required." };
            }
            if (string.IsNullOrEmpty(request.MobileNo))
            {
                return new Response { isSuccess = false, Message = "Mobile no is required." };
            }
            WalletDto wallet = GetWallet(request.MobileNo);
            if (wallet != null) {
                return new Response { isSuccess = false, Message = "Already exits" };
            }

            using (IDbConnection db = new SqlConnection(connectionString.ConnectionString))
            {
                db.Open();

                var sql = $@"INSERT INTO [dbo].[Tbl_Wallet]
                           ([WalletID]
                           ,[FullName]
                           ,[MobileNo]
                           ,[Balance]
                           ,[IsDelete])
                     VALUES
                           ('{Guid.NewGuid().ToString()}'
                           ,'{request.FullName}'
                           ,'{request.MobileNo}'
                           ,{request.Balance}
                           ,0)";

                int rowAffected = db.Execute(sql);
                return rowAffected > 0 ? new Response
                {
                    isSuccess = true,
                    Message = "Wallet created successfully."
                } : new Response
                {
                    isSuccess = false,
                    Message = "Wallet created fail."
                };
            }

        }
    
        public int UpdateWalletBalance(string walletId,decimal amount) {
            using (IDbConnection db = new SqlConnection(connectionString.ConnectionString)) { 
                db.Open();
                var sql = $@"UPDATE [dbo].[Tbl_Wallet]
                           SET [Balance] = '{amount}'
                         WHERE WalletID = '{walletId}'";
                int rowAffected = db.Execute(sql);
                return rowAffected;
            }
        }
        public Response Transfer(TransferRequest request)
        {

            if (string.IsNullOrWhiteSpace(request.SenderMobileno)) {
                Console.WriteLine("Sender MobileNo is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ReceiverMobileno))
            {
                Console.WriteLine("Receiver MobileNo is required.");
            }            

            WalletDto sender = GetWallet(request.SenderMobileno);
            if (sender == null) {
                return new Response { isSuccess = false, Message = "Error: Sender MobileNo not found." };
            }

            var receiver = GetWallet(request.ReceiverMobileno);
            if (receiver == null) {
                return new Response { isSuccess = false, Message = "Error: Recipient not found." };
            }

            if (receiver.MobileNo == sender.MobileNo) {
                return new Response { isSuccess = false, Message = "Error: Cannot send to yourself." };
            }

            if(sender.Balance < request.Balance)
            {
                return new Response { isSuccess = false, Message = "Error: Insufficient funds." };
            }

            // Logic
           int rowAffected = UpdateWalletBalance(sender.WalletID, sender.Balance - request.Balance);// subtract from sender
           int rowAffected1 = UpdateWalletBalance(receiver.WalletID, receiver.Balance + request.Balance);// add to receiver



            if (rowAffected > 0 && rowAffected1 > 0) {
                // Logic: Create Records (One for Sender, One for Receiver)
                string sharedTxnId = Guid.NewGuid().ToString().ToUpper()[..8];
                DateTime now = DateTime.Now;
                var senderRecord = new TransactionRecord(
                    Guid.NewGuid().ToString(),
                    sharedTxnId,
                    sender.MobileNo,
                    receiver.MobileNo,
                    request.Balance,
                    "send",now);

                var receiveRecord = new TransactionRecord(
                    Guid.NewGuid().ToString(),
                    sharedTxnId,
                    sender.MobileNo, 
                    receiver.MobileNo, 
                    request.Balance, 
                    "receive", now);

                var affected = CreateTransactionRecord(senderRecord);
                var affected1 = CreateTransactionRecord(receiveRecord);

                if (affected > 0 && affected1 > 0) {
                    var mesage =$"Success! Sent ${request.Balance} to {request.ReceiverMobileno}.\nNew Balance: ${sender.Balance - request.Balance}";
                    
                    return new Response { isSuccess = true, Message = mesage };
                }
                else
                {
                    return new Response { isSuccess = false, Message = "Transfer fail." };
                    // need to rollback
                }
            }
            return new Response { isSuccess = false, Message = "Failed" };
            // need to rollback


        }
        
        public int CreateTransactionRecord(TransactionRecord record)
        {
            using(IDbConnection db = new SqlConnection(connectionString.ToString()))
            {
                db.Open();
                var sql = $@"INSERT INTO [dbo].[Tbl_TransactionRecord]                           
                           ([TransactionId]
                           ,[TxnId]
                           ,[FromMobileNo]
                           ,[ToMobileNo]
                           ,[Amount]
                           ,[Message]
                           ,[Timestamp])
                            VALUES
                           ('{record.TransactionId}'
                           ,'{record.TxnId}'
                           ,'{record.FromMobileNo}'
                           ,'{record.ToMobileNo}'
                           ,'{record.Amount}'
                           ,'{record.Message}'
                           ,'{record.Timestamp}')";
                return db.Execute(sql);
            }
        }
    }
}

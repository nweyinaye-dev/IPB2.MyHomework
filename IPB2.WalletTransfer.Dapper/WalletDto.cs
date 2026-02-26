namespace IPB2.WalletTransfer.Dapper;

/// <summary>
/// This class is Primary Constructor
/// </summary>
public class WalletDto(string WalletID,string FullName,string MobileNo,decimal Balance,bool IsDelete)
{
    public string WalletID = WalletID;
    public string FullName = FullName;
    public string MobileNo = MobileNo;
    public bool IsDelete = IsDelete;
    public decimal Balance = Balance;  
}

public class CreateWalletRequest {
    public CreateWalletRequest(string fullName, string mobileNo, decimal balance)
    {
        FullName = fullName;
        MobileNo = mobileNo;
        Balance = balance;
    }

    public string FullName {  get; set; }
    public string MobileNo { get; set; }
    public decimal Balance { get; set; }
}
public class TransferRequest
{
    public TransferRequest(string senderMobileno, string receiverMobileno, decimal balance)
    {
        this.SenderMobileno = senderMobileno;
        this.ReceiverMobileno = receiverMobileno;
        this.Balance = balance;
    }

    public string SenderMobileno {  get; set; }
    public string ReceiverMobileno { get; set; }
    public decimal Balance { get; set; }
}
public class Response()
{
    public bool isSuccess {  get; set; }
    public string Message { get; set; } = "";
}
public class TransactionRecord
{
    public TransactionRecord(string transactionId,string txnId,string fromMobileNo,string toMobileNo,decimal amount,string message,DateTime timestamp)
    {
        this.TransactionId = transactionId;
        this.TxnId = txnId;
        this.FromMobileNo = fromMobileNo;
        this.ToMobileNo = toMobileNo;
        this.Amount = amount;
        this.Message = message;
        this.Timestamp = timestamp;
    }
    public string TransactionId { get; set; }
    public string TxnId { get; set; }
    public string FromMobileNo { get; set; }
    public string ToMobileNo { get; set; }
    public decimal Amount { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
}

public enum WalletType {
    None,
    Create,
    Transfer,
    //Transaction,
    Exit
}

using IPB2.WalletTransfer.Dapper;

new WalletUI().Run();

// 1. Initialize Mock Data
//List<Wallet> wallets = [
//  new("W001", "Alice Smith", "09123456789", 1000.00m),
//  new("W002", "Bob Jones", "09987654321", 100.00m)
//];

//List<TransactionRecord> globalJournal = [];

//// 2. UI Logic
//Console.WriteLine("=== Mobile Wallet Transfer ===");
//var sender = wallets[0]; // Alice is logged in
//Console.WriteLine($"Logged in: {sender.FullName} ({sender.MobileNo})");
//Console.WriteLine($"Balance  : ${sender.Balance}");

//Console.Write("\nEnter Recipient Mobile No: ");
//string? targetMobile = Console.ReadLine();

//Console.Write("Enter Amount: ");
//if (decimal.TryParse(Console.ReadLine(), out decimal amount))
//{
//ExecuteTransfer(sender, targetMobile, amount);
//}

//// 3. The Transfer Logic
//void ExecuteTransfer(Wallet from, string? toMobile, decimal qty)
//{
//var to = wallets.FirstOrDefault(w => w.MobileNo == toMobile);

//// Validations
//if (to == null) { Console.WriteLine("Error: Recipient not found."); return; }
//if (to.MobileNo == from.MobileNo) { Console.WriteLine("Error: Cannot send to yourself."); return; }
//if (from.Balance < qty) { Console.WriteLine("Error: Insufficient funds."); return; }

//// Logic: Move the Money
//from.Balance -= qty;
//to.Balance += qty;

//// Logic: Create Records (One for Sender, One for Receiver)
//string sharedTxnId = Guid.NewGuid().ToString().ToUpper()[..8];
//DateTime now = DateTime.Now;

//// Record for Sender
//globalJournal.Add(new(sharedTxnId, from.MobileNo, to.MobileNo, qty, "Send", now));

//// Record for Receiver
//globalJournal.Add(new(sharedTxnId, from.MobileNo, to.MobileNo, qty, "Receive", now));

//Console.WriteLine($"\nSuccess! Sent ${qty} to {to.FullName}.");
//Console.WriteLine($"New Balance: ${from.Balance}");
//}

//// 4. Models (C# 12 Primary Constructors)
//public class Wallet(string walletId, string fullName, string mobileNo, decimal balance)
//{
//    public string WalletId { get; set; } = walletId;
//    public string FullName { get; set; } = fullName;
//    public string MobileNo { get; set; } = mobileNo;
//    public decimal Balance { get; set; } = balance;
//}

//public record TransactionRecord(
//  string TxnId,
//  string FromMobileNo,
//  string ToMobileNo,
//  decimal Amount,
//  string Message,
//  DateTime Timestamp
//);


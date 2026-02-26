using System.Collections;
using System.Reflection;

namespace IPB2.WalletTransfer.Dapper
{
    public class WalletUI
    {
        private readonly WalletService _walletServie = new WalletService();
       public void Run()
        {
            while (true)
            {
                MainMenu();
            }
        }
        private void MainMenu()
        {
            Console.WriteLine("\n *** Wallet Transfer ***");
            Console.WriteLine("1) Create Wallet");
            Console.WriteLine("2) Wallet Transfer");
            //Console.WriteLine("3) Get Transaction");
            Console.WriteLine("3) Exit");
            Console.Write("Please choose: ");
            var choose = Console.ReadLine();
            var isChoose = Enum.TryParse(choose, out WalletType type) ;
            switch (type)
            {
                case WalletType.Create: Create(); break;
                case WalletType.Transfer: Transfer(); break;
                //case WalletType.Transaction: Transaction(); break;
                case WalletType.Exit: Environment.Exit(0);break;
                case WalletType.None:
                default: Console.WriteLine("Invalid option.Try again.");break;
            }


        }
        private void Create() {
            Console.WriteLine("\n *** Create Wallet ***");
            Console.Write("Enter name: ");
            var name = Console.ReadLine() ?? "";

            Console.Write("Enter mobile no: ");
            var mobileno = Console.ReadLine() ?? "";

            start:
            Console.Write("Enter balance: ");
            var balance = Console.ReadLine() ?? "";

            if (!decimal.TryParse(balance, out decimal value)) {
                Console.WriteLine("Invalid input.Please try again.");
                goto start;
            }

            var response = _walletServie.CreateWallet(new CreateWalletRequest(name, mobileno, value));
            Console.WriteLine(response.Message);
        }
        private void Transfer() {

            Console.WriteLine("\n*** Mobile Wallet Transfer ***");
            Console.Write("Enter your mobile no: ");
            var mobileno = Console.ReadLine() ?? "";

            if (string.IsNullOrEmpty(mobileno)) {
                Console.WriteLine("Your MobileNo is required.");
                return;
            }

            var sender = _walletServie.GetWallet(mobileno);
            if(sender == null)
            {
                Console.WriteLine("Error: Sender MobileNo not found.");
                return;
            }

            Console.WriteLine($"Logged in: {sender.FullName} ({sender.MobileNo})");
            Console.WriteLine($"Balance  : ${sender.Balance}");

            Console.Write("\nEnter Recipient Mobile No: ");
            string? targetMobile = Console.ReadLine() ?? "";

            if (string.IsNullOrEmpty(targetMobile))
            {
                Console.WriteLine("Recipient MobileNo is required.");
                return;
            }

            Console.Write("Enter Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var response = _walletServie.Transfer(new TransferRequest(mobileno, targetMobile, amount));
            Console.WriteLine(response.Message);
        }
        private void Transaction() { }

    }
}

using IPB2.EFCore.Database.AppDbContextModels;
using IPB2.WallletTransfer.Window;
using IPB2.WallletTransfer.Window.Dtos;

namespace IPB2.WallletTransfer
{
    public partial class frmWallet : Form
    {
        private readonly WalletService walletService = new WalletService();
        public frmWallet()
        {
            InitializeComponent();
        }

        private void frmWallet_Load(object sender, EventArgs e)
        {

        }

        private bool validation(out string msg)
        {
            msg = string.Empty;
            if (string.IsNullOrWhiteSpace(this.txtMobileno.Text))
            {
                msg = "Sender Mobile number is required.";
                this.txtMobileno.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtPassword.Text))
            {
                msg = "Password is required.";
                this.txtPassword.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtRecMobileno.Text))
            {
                msg = "Recipient Mobile No is required.";
                this.txtRecMobileno.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtBalance.Text))
            {
                msg = "Balance is required.";
                this.txtBalance.Focus();
                return false;
            }
            return true;
        }

        private void txtBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private async void btnTransfer_Click(object sender, EventArgs e)
        {
            if (!validation(out string msg))
            {
                MessageBox.Show(msg);
                return;
            }

            var request = new TransferRequest(txtMobileno.Text, txtRecMobileno.Text, Convert.ToDecimal(txtBalance.Text));


            var response = await walletService.Transfer(request);
            MessageBox.Show(response.Message);
        }
    }
}

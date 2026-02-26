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

        #region private methods
        private bool Validation(out string msg)
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
        private bool Createvalidation(out string msg)
        {
            msg = string.Empty;
            if (string.IsNullOrWhiteSpace(this.txtName.Text.Trim()))
            {
                msg = "Name is required.";
                this.txtName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtCreateMobileno.Text.Trim()))
            {
                msg = "Mobile number is required.";
                this.txtCreateMobileno.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtCreatePassword.Text.Trim()))
            {
                msg = "Password is required.";
                this.txtPassword.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtConfirmPassword.Text.Trim()))
            {
                msg = "Confirm password is required.";
                this.txtConfirmPassword.Focus();
                return false;
            }
            if (this.txtCreatePassword.Text.Trim() != this.txtConfirmPassword.Text.Trim())
            {
                msg = "Password and confirm password doesn't match.";
                this.txtConfirmPassword.Text = string.Empty;
                this.txtConfirmPassword.Focus();

                return false;
            }
            return true;
        }

        private bool WithdrawValidation(out string msg)
        {
            msg = string.Empty;
            if (string.IsNullOrWhiteSpace(this.txtWithdrawMobileno.Text.Trim()))
            {
                msg = "Mobileno is required.";
                this.txtWithdrawMobileno.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtWithdrawPassword.Text.Trim()))
            {
                msg = "Password is required.";
                this.txtWithdrawPassword.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtWithdrawAmount.Text.Trim()))
            {
                msg = "Amount is required.";
                this.txtWithdrawAmount.Focus();
                return false;
            }
            return true;
        }

        private bool DepostiValidation(out string msg)
        {
            msg = string.Empty;
            if (string.IsNullOrWhiteSpace(this.txtDepositMobileno.Text.Trim()))
            {
                msg = "Mobileno is required.";
                this.txtDepositMobileno.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtDepositPassword.Text.Trim()))
            {
                msg = "Password is required.";
                this.txtDepositPassword.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.txtDepositAmount.Text.Trim()))
            {
                msg = "Amount is required.";
                this.txtDepositAmount.Focus();
                return false;
            }
            return true;
        }
        private void ClearData()
        {
            txtMobileno.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtRecMobileno.Text = string.Empty;
            txtBalance.Text = string.Empty;
            txtMobileno.Focus();
        }
        private void ClearCreateData()
        {
            txtName.Text = string.Empty;
            txtCreatePassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            txtCreateMobileno.Text = string.Empty;
            txtName.Focus();
        }
        private void ClearWithdrawData()
        {
            txtWithdrawMobileno.Text = string.Empty;
            txtWithdrawPassword.Text = string.Empty;
            txtWithdrawAmount.Text = string.Empty;
            txtWithdrawMobileno.Focus();
        }
        private void ClearDepositData()
        {
            txtDepositMobileno.Text = string.Empty;
            txtDepositPassword.Text = string.Empty;
            txtDepositAmount.Text = string.Empty;
            txtDepositMobileno.Focus();
        }
        #endregion
        private void txtBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtMobileno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtRecMobileno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private async void btnTransfer_Click(object sender, EventArgs e)
        {

            try
            {
                if (!Validation(out string msg))
                {
                    MessageBox.Show(msg);
                    return;
                }

                var request = new TransferRequest(txtMobileno.Text.Trim(), txtRecMobileno.Text.Trim(),
                    Convert.ToDecimal(txtBalance.Text.Trim()), txtPassword.Text.Trim());

                var response = await walletService.Transfer(request);

                if (response?.isSuccess == true)
                {
                    ClearData();
                }
                MessageBox.Show(response?.Message ?? "No response from wallet service.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during transfer: {ex.Message}");
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCreateMobileno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCreateCancel_Click(object sender, EventArgs e)
        {
            ClearCreateData();
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Createvalidation(out string msg))
                {
                    MessageBox.Show(msg);
                    return;
                }
                var req = new CreateAccountRequestDto(txtName.Text.Trim(), txtCreateMobileno.Text.Trim(),
                      txtCreateMobileno.Text.Trim(), txtConfirmPassword.Text.Trim());

                var response = await walletService.CreateAccount(req);

                if (response?.isSuccess == true)
                {
                    ClearCreateData();
                }
                MessageBox.Show(response?.Message ?? "No response from wallet service.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during transfer: {ex.Message}");
            }
        }

        #region withdraw panel
        private void txtWithdrawMobileno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtWithdrawAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                if (!WithdrawValidation(out string msg))
                {
                    MessageBox.Show(msg);
                    return;
                }
                //var req = new CreateAccountRequestDto(txtName.Text.Trim(), txtCreateMobileno.Text.Trim(),
                //      txtCreateMobileno.Text.Trim(), txtConfirmPassword.Text.Trim());

                //var response = await walletService.CreateAccount(req);

                //if (response?.isSuccess == true)
                //{
                //    ClearCreateData();
                //}
                //MessageBox.Show(response?.Message ?? "No response from wallet service.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during transfer: {ex.Message}");
            }
        }

        private void btnWithdrawCancel_Click(object sender, EventArgs e)
        {
            ClearWithdrawData();
        }

        #endregion

        #region deposit panel
        private void txtDepositMobileno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void txtDepositAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DepostiValidation(out string msg))
                {
                    MessageBox.Show(msg);
                    return;
                }
                //var req = new CreateAccountRequestDto(txtName.Text.Trim(), txtCreateMobileno.Text.Trim(),
                //      txtCreateMobileno.Text.Trim(), txtConfirmPassword.Text.Trim());

                //var response = await walletService.CreateAccount(req);

                //if (response?.isSuccess == true)
                //{
                //    ClearCreateData();
                //}
                //MessageBox.Show(response?.Message ?? "No response from wallet service.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during transfer: {ex.Message}");
            }
        }

        private void btnDepositCancel_Click(object sender, EventArgs e)
        {
            ClearDepositData();
        }

        #endregion



    }
}

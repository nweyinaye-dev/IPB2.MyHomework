namespace IPB2.WallletTransfer
{
    partial class frmWallet
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tbTransfer = new TabPage();
            btnCancel = new Button();
            btnTransfer = new Button();
            txtBalance = new TextBox();
            txtRecMobileno = new TextBox();
            txtPassword = new TextBox();
            txtMobileno = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            tbCreate = new TabPage();
            txtConfirmPassword = new TextBox();
            label8 = new Label();
            btnCreateCancel = new Button();
            btnCreate = new Button();
            txtCreatePassword = new TextBox();
            txtCreateMobileno = new TextBox();
            txtName = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btnWithdrawCancel = new Button();
            btnWithdraw = new Button();
            txtWithdrawAmount = new TextBox();
            txtWithdrawPassword = new TextBox();
            txtWithdrawMobileno = new TextBox();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            tabPage2 = new TabPage();
            btnDepositCancel = new Button();
            btnDeposit = new Button();
            txtDepositAmount = new TextBox();
            txtDepositPassword = new TextBox();
            txtDepositMobileno = new TextBox();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            tbTransfer.SuspendLayout();
            tbCreate.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // tbTransfer
            // 
            tbTransfer.Controls.Add(btnCancel);
            tbTransfer.Controls.Add(btnTransfer);
            tbTransfer.Controls.Add(txtBalance);
            tbTransfer.Controls.Add(txtRecMobileno);
            tbTransfer.Controls.Add(txtPassword);
            tbTransfer.Controls.Add(txtMobileno);
            tbTransfer.Controls.Add(label4);
            tbTransfer.Controls.Add(label3);
            tbTransfer.Controls.Add(label2);
            tbTransfer.Controls.Add(label1);
            tbTransfer.Location = new Point(4, 24);
            tbTransfer.Margin = new Padding(2);
            tbTransfer.Name = "tbTransfer";
            tbTransfer.Padding = new Padding(2);
            tbTransfer.Size = new Size(416, 250);
            tbTransfer.TabIndex = 1;
            tbTransfer.Text = "Transfer";
            tbTransfer.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(214, 183);
            btnCancel.Margin = new Padding(2);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(78, 20);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnTransfer
            // 
            btnTransfer.Location = new Point(93, 183);
            btnTransfer.Margin = new Padding(2);
            btnTransfer.Name = "btnTransfer";
            btnTransfer.Size = new Size(78, 20);
            btnTransfer.TabIndex = 8;
            btnTransfer.Text = "Transfer";
            btnTransfer.UseVisualStyleBackColor = true;
            btnTransfer.Click += btnTransfer_Click;
            // 
            // txtBalance
            // 
            txtBalance.Location = new Point(189, 133);
            txtBalance.Margin = new Padding(2);
            txtBalance.Name = "txtBalance";
            txtBalance.Size = new Size(138, 23);
            txtBalance.TabIndex = 7;
            txtBalance.TextAlign = HorizontalAlignment.Right;
            txtBalance.KeyPress += txtBalance_KeyPress;
            // 
            // txtRecMobileno
            // 
            txtRecMobileno.Location = new Point(189, 96);
            txtRecMobileno.Margin = new Padding(2);
            txtRecMobileno.Name = "txtRecMobileno";
            txtRecMobileno.Size = new Size(138, 23);
            txtRecMobileno.TabIndex = 6;
            txtRecMobileno.KeyPress += txtRecMobileno_KeyPress;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(189, 59);
            txtPassword.Margin = new Padding(2);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(138, 23);
            txtPassword.TabIndex = 3;
            // 
            // txtMobileno
            // 
            txtMobileno.Location = new Point(189, 20);
            txtMobileno.Margin = new Padding(2);
            txtMobileno.Name = "txtMobileno";
            txtMobileno.Size = new Size(138, 23);
            txtMobileno.TabIndex = 0;
            txtMobileno.KeyPress += txtMobileno_KeyPress;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(53, 136);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(48, 15);
            label4.TabIndex = 5;
            label4.Text = "Balance";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(53, 96);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(118, 15);
            label3.TabIndex = 4;
            label3.Text = "Recipient Mobile No.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(53, 59);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 2;
            label2.Text = "Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 23);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 1;
            label1.Text = "Sender Mobile No.";
            // 
            // tbCreate
            // 
            tbCreate.Controls.Add(txtConfirmPassword);
            tbCreate.Controls.Add(label8);
            tbCreate.Controls.Add(btnCreateCancel);
            tbCreate.Controls.Add(btnCreate);
            tbCreate.Controls.Add(txtCreatePassword);
            tbCreate.Controls.Add(txtCreateMobileno);
            tbCreate.Controls.Add(txtName);
            tbCreate.Controls.Add(label7);
            tbCreate.Controls.Add(label6);
            tbCreate.Controls.Add(label5);
            tbCreate.Location = new Point(4, 24);
            tbCreate.Margin = new Padding(2);
            tbCreate.Name = "tbCreate";
            tbCreate.Padding = new Padding(2);
            tbCreate.Size = new Size(416, 250);
            tbCreate.TabIndex = 0;
            tbCreate.Text = "Create";
            tbCreate.UseVisualStyleBackColor = true;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(187, 142);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(139, 23);
            txtConfirmPassword.TabIndex = 9;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(50, 150);
            label8.Name = "label8";
            label8.Size = new Size(104, 15);
            label8.TabIndex = 8;
            label8.Text = "Confirm Password";
            // 
            // btnCreateCancel
            // 
            btnCreateCancel.Location = new Point(207, 190);
            btnCreateCancel.Name = "btnCreateCancel";
            btnCreateCancel.Size = new Size(75, 23);
            btnCreateCancel.TabIndex = 7;
            btnCreateCancel.Text = "Cancel";
            btnCreateCancel.UseVisualStyleBackColor = true;
            btnCreateCancel.Click += btnCreateCancel_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(90, 190);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 6;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // txtCreatePassword
            // 
            txtCreatePassword.Location = new Point(187, 102);
            txtCreatePassword.Name = "txtCreatePassword";
            txtCreatePassword.Size = new Size(139, 23);
            txtCreatePassword.TabIndex = 5;
            // 
            // txtCreateMobileno
            // 
            txtCreateMobileno.Location = new Point(187, 65);
            txtCreateMobileno.Name = "txtCreateMobileno";
            txtCreateMobileno.Size = new Size(139, 23);
            txtCreateMobileno.TabIndex = 4;
            txtCreateMobileno.KeyPress += txtCreateMobileno_KeyPress;
            // 
            // txtName
            // 
            txtName.Location = new Point(187, 28);
            txtName.Name = "txtName";
            txtName.Size = new Size(139, 23);
            txtName.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(50, 110);
            label7.Name = "label7";
            label7.Size = new Size(57, 15);
            label7.TabIndex = 2;
            label7.Text = "Password";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(50, 73);
            label6.Name = "label6";
            label6.Size = new Size(66, 15);
            label6.TabIndex = 1;
            label6.Text = "Mobile No.";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(50, 36);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 0;
            label5.Text = "Name";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tbCreate);
            tabControl1.Controls.Add(tbTransfer);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(27, 20);
            tabControl1.Margin = new Padding(2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(424, 278);
            tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnWithdrawCancel);
            tabPage1.Controls.Add(btnWithdraw);
            tabPage1.Controls.Add(txtWithdrawAmount);
            tabPage1.Controls.Add(txtWithdrawPassword);
            tabPage1.Controls.Add(txtWithdrawMobileno);
            tabPage1.Controls.Add(label11);
            tabPage1.Controls.Add(label10);
            tabPage1.Controls.Add(label9);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(416, 250);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Withdraw";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnWithdrawCancel
            // 
            btnWithdrawCancel.Location = new Point(254, 190);
            btnWithdrawCancel.Name = "btnWithdrawCancel";
            btnWithdrawCancel.Size = new Size(75, 23);
            btnWithdrawCancel.TabIndex = 7;
            btnWithdrawCancel.Text = "Cancel";
            btnWithdrawCancel.UseVisualStyleBackColor = true;
            btnWithdrawCancel.Click += btnWithdrawCancel_Click;
            // 
            // btnWithdraw
            // 
            btnWithdraw.Location = new Point(134, 190);
            btnWithdraw.Name = "btnWithdraw";
            btnWithdraw.Size = new Size(75, 23);
            btnWithdraw.TabIndex = 6;
            btnWithdraw.Text = "Withdraw";
            btnWithdraw.UseVisualStyleBackColor = true;
            btnWithdraw.Click += btnWithdraw_Click;
            // 
            // txtWithdrawAmount
            // 
            txtWithdrawAmount.Location = new Point(190, 128);
            txtWithdrawAmount.Name = "txtWithdrawAmount";
            txtWithdrawAmount.Size = new Size(139, 23);
            txtWithdrawAmount.TabIndex = 5;
            txtWithdrawAmount.TextAlign = HorizontalAlignment.Right;
            txtWithdrawAmount.KeyPress += txtWithdrawAmount_KeyPress;
            // 
            // txtWithdrawPassword
            // 
            txtWithdrawPassword.Location = new Point(190, 80);
            txtWithdrawPassword.Name = "txtWithdrawPassword";
            txtWithdrawPassword.PasswordChar = '*';
            txtWithdrawPassword.Size = new Size(139, 23);
            txtWithdrawPassword.TabIndex = 4;
            // 
            // txtWithdrawMobileno
            // 
            txtWithdrawMobileno.Location = new Point(190, 35);
            txtWithdrawMobileno.Name = "txtWithdrawMobileno";
            txtWithdrawMobileno.Size = new Size(139, 23);
            txtWithdrawMobileno.TabIndex = 3;
            txtWithdrawMobileno.KeyPress += txtWithdrawMobileno_KeyPress;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(84, 136);
            label11.Name = "label11";
            label11.Size = new Size(51, 15);
            label11.TabIndex = 2;
            label11.Text = "Amount";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(84, 88);
            label10.Name = "label10";
            label10.Size = new Size(57, 15);
            label10.TabIndex = 1;
            label10.Text = "Password";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(84, 43);
            label9.Name = "label9";
            label9.Size = new Size(66, 15);
            label9.TabIndex = 0;
            label9.Text = "Mobile No.";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnDepositCancel);
            tabPage2.Controls.Add(btnDeposit);
            tabPage2.Controls.Add(txtDepositAmount);
            tabPage2.Controls.Add(txtDepositPassword);
            tabPage2.Controls.Add(txtDepositMobileno);
            tabPage2.Controls.Add(label14);
            tabPage2.Controls.Add(label13);
            tabPage2.Controls.Add(label12);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(416, 250);
            tabPage2.TabIndex = 3;
            tabPage2.Text = "Deposit";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDepositCancel
            // 
            btnDepositCancel.Location = new Point(261, 179);
            btnDepositCancel.Name = "btnDepositCancel";
            btnDepositCancel.Size = new Size(75, 23);
            btnDepositCancel.TabIndex = 7;
            btnDepositCancel.Text = "Cancel";
            btnDepositCancel.UseVisualStyleBackColor = true;
            btnDepositCancel.Click += btnDepositCancel_Click;
            // 
            // btnDeposit
            // 
            btnDeposit.Location = new Point(126, 179);
            btnDeposit.Name = "btnDeposit";
            btnDeposit.Size = new Size(75, 23);
            btnDeposit.TabIndex = 6;
            btnDeposit.Text = "Deposit";
            btnDeposit.UseVisualStyleBackColor = true;
            btnDeposit.Click += btnDeposit_Click_1;
            // 
            // txtDepositAmount
            // 
            txtDepositAmount.Location = new Point(188, 112);
            txtDepositAmount.Name = "txtDepositAmount";
            txtDepositAmount.Size = new Size(148, 23);
            txtDepositAmount.TabIndex = 5;
            txtDepositAmount.TextAlign = HorizontalAlignment.Right;
            // 
            // txtDepositPassword
            // 
            txtDepositPassword.Location = new Point(188, 66);
            txtDepositPassword.Name = "txtDepositPassword";
            txtDepositPassword.Size = new Size(148, 23);
            txtDepositPassword.TabIndex = 4;
            // 
            // txtDepositMobileno
            // 
            txtDepositMobileno.Location = new Point(188, 25);
            txtDepositMobileno.Name = "txtDepositMobileno";
            txtDepositMobileno.Size = new Size(148, 23);
            txtDepositMobileno.TabIndex = 3;
            txtDepositMobileno.KeyPress += txtDepositMobileno_KeyPress;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(106, 120);
            label14.Name = "label14";
            label14.Size = new Size(51, 15);
            label14.TabIndex = 2;
            label14.Text = "Amount";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(106, 74);
            label13.Name = "label13";
            label13.Size = new Size(57, 15);
            label13.TabIndex = 1;
            label13.Text = "Password";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(106, 33);
            label12.Name = "label12";
            label12.Size = new Size(66, 15);
            label12.TabIndex = 0;
            label12.Text = "Mobile No.";
            // 
            // frmWallet
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(480, 400);
            Controls.Add(tabControl1);
            Margin = new Padding(2);
            Name = "frmWallet";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Wallet";
            tbTransfer.ResumeLayout(false);
            tbTransfer.PerformLayout();
            tbCreate.ResumeLayout(false);
            tbCreate.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TabPage tbTransfer;
        private Button btnCancel;
        private Button btnTransfer;
        private TextBox txtBalance;
        private TextBox txtRecMobileno;
        private TextBox txtPassword;
        private TextBox txtMobileno;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TabPage tbCreate;
        private TabControl tabControl1;
        private Button btnExit;
        private Panel panel1;
        private TextBox txtCreatePassword;
        private TextBox txtCreateMobileno;
        private TextBox txtName;
        private Label label7;
        private Label label6;
        private Label label5;
        private Button btnCreateCancel;
        private Button btnCreate;
        private TextBox txtConfirmPassword;
        private Label label8;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label11;
        private Label label10;
        private Label label9;
        private Button btnWithdrawCancel;
        private Button btnWithdraw;
        private TextBox txtWithdrawAmount;
        private TextBox txtWithdrawPassword;
        private TextBox txtWithdrawMobileno;
        private TextBox txtDepositAmount;
        private TextBox txtDepositPassword;
        private TextBox txtDepositMobileno;
        private Label label14;
        private Label label13;
        private Label label12;
        private Button btnDepositCancel;
        private Button btnDeposit;
        //private Button btnExit;
    }
}

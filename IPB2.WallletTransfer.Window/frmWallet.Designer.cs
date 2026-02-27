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
            History = new TabPage();
            listView1 = new ListView();
            TransactionID = new ColumnHeader();
            From = new ColumnHeader();
            To = new ColumnHeader();
            Amount = new ColumnHeader();
            Message = new ColumnHeader();
            txtHistoryPassword = new TextBox();
            txtHistoryMobileno = new TextBox();
            btnSearch = new Button();
            label16 = new Label();
            label15 = new Label();
            tbTransfer.SuspendLayout();
            tbCreate.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            History.SuspendLayout();
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
            tbTransfer.Location = new Point(4, 34);
            tbTransfer.Name = "tbTransfer";
            tbTransfer.Padding = new Padding(3);
            tbTransfer.Size = new Size(721, 425);
            tbTransfer.TabIndex = 1;
            tbTransfer.Text = "Transfer";
            tbTransfer.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(396, 305);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(111, 33);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnTransferCancel_Click;
            // 
            // btnTransfer
            // 
            btnTransfer.Location = new Point(181, 305);
            btnTransfer.Name = "btnTransfer";
            btnTransfer.Size = new Size(111, 33);
            btnTransfer.TabIndex = 8;
            btnTransfer.Text = "Transfer";
            btnTransfer.UseVisualStyleBackColor = true;
            btnTransfer.Click += btnTransfer_Click;
            // 
            // txtBalance
            // 
            txtBalance.Location = new Point(396, 221);
            txtBalance.Name = "txtBalance";
            txtBalance.Size = new Size(195, 31);
            txtBalance.TabIndex = 7;
            txtBalance.TextAlign = HorizontalAlignment.Right;
            txtBalance.KeyPress += txtBalance_KeyPress;
            // 
            // txtRecMobileno
            // 
            txtRecMobileno.Location = new Point(396, 154);
            txtRecMobileno.Name = "txtRecMobileno";
            txtRecMobileno.Size = new Size(195, 31);
            txtRecMobileno.TabIndex = 6;
            txtRecMobileno.KeyPress += txtRecMobileno_KeyPress;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(396, 95);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(195, 31);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtMobileno
            // 
            txtMobileno.Location = new Point(396, 32);
            txtMobileno.Name = "txtMobileno";
            txtMobileno.Size = new Size(195, 31);
            txtMobileno.TabIndex = 0;
            txtMobileno.KeyPress += txtMobileno_KeyPress;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(116, 227);
            label4.Name = "label4";
            label4.Size = new Size(71, 25);
            label4.TabIndex = 5;
            label4.Text = "Balance";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(116, 160);
            label3.Name = "label3";
            label3.Size = new Size(176, 25);
            label3.TabIndex = 4;
            label3.Text = "Recipient Mobile No.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(116, 101);
            label2.Name = "label2";
            label2.Size = new Size(87, 25);
            label2.TabIndex = 2;
            label2.Text = "Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(116, 38);
            label1.Name = "label1";
            label1.Size = new Size(160, 25);
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
            tbCreate.Location = new Point(4, 34);
            tbCreate.Name = "tbCreate";
            tbCreate.Padding = new Padding(3);
            tbCreate.Size = new Size(721, 425);
            tbCreate.TabIndex = 0;
            tbCreate.Text = "Create";
            tbCreate.UseVisualStyleBackColor = true;
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Location = new Point(390, 221);
            txtConfirmPassword.Margin = new Padding(4, 5, 4, 5);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(197, 31);
            txtConfirmPassword.TabIndex = 9;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(129, 227);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(156, 25);
            label8.TabIndex = 8;
            label8.Text = "Confirm Password";
            // 
            // btnCreateCancel
            // 
            btnCreateCancel.Location = new Point(390, 302);
            btnCreateCancel.Margin = new Padding(4, 5, 4, 5);
            btnCreateCancel.Name = "btnCreateCancel";
            btnCreateCancel.Size = new Size(107, 38);
            btnCreateCancel.TabIndex = 7;
            btnCreateCancel.Text = "Cancel";
            btnCreateCancel.UseVisualStyleBackColor = true;
            btnCreateCancel.Click += btnCreateCancel_Click;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(178, 302);
            btnCreate.Margin = new Padding(4, 5, 4, 5);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(107, 38);
            btnCreate.TabIndex = 6;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            btnCreate.Click += btnCreate_Click;
            // 
            // txtCreatePassword
            // 
            txtCreatePassword.Location = new Point(390, 162);
            txtCreatePassword.Margin = new Padding(4, 5, 4, 5);
            txtCreatePassword.Name = "txtCreatePassword";
            txtCreatePassword.Size = new Size(197, 31);
            txtCreatePassword.TabIndex = 5;
            txtCreatePassword.UseSystemPasswordChar = true;
            // 
            // txtCreateMobileno
            // 
            txtCreateMobileno.Location = new Point(390, 107);
            txtCreateMobileno.Margin = new Padding(4, 5, 4, 5);
            txtCreateMobileno.Name = "txtCreateMobileno";
            txtCreateMobileno.Size = new Size(197, 31);
            txtCreateMobileno.TabIndex = 4;
            txtCreateMobileno.KeyPress += txtCreateMobileno_KeyPress;
            // 
            // txtName
            // 
            txtName.Location = new Point(390, 54);
            txtName.Margin = new Padding(4, 5, 4, 5);
            txtName.Name = "txtName";
            txtName.Size = new Size(197, 31);
            txtName.TabIndex = 3;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(129, 168);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(87, 25);
            label7.TabIndex = 2;
            label7.Text = "Password";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(129, 113);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(100, 25);
            label6.TabIndex = 1;
            label6.Text = "Mobile No.";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(129, 60);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(59, 25);
            label5.TabIndex = 0;
            label5.Text = "Name";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tbCreate);
            tabControl1.Controls.Add(tbTransfer);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(History);
            tabControl1.Location = new Point(1, 33);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(729, 463);
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
            tabPage1.Location = new Point(4, 34);
            tabPage1.Margin = new Padding(4, 5, 4, 5);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4, 5, 4, 5);
            tabPage1.Size = new Size(721, 425);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Withdraw";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnWithdrawCancel
            // 
            btnWithdrawCancel.Location = new Point(363, 271);
            btnWithdrawCancel.Margin = new Padding(4, 5, 4, 5);
            btnWithdrawCancel.Name = "btnWithdrawCancel";
            btnWithdrawCancel.Size = new Size(107, 38);
            btnWithdrawCancel.TabIndex = 7;
            btnWithdrawCancel.Text = "Cancel";
            btnWithdrawCancel.UseVisualStyleBackColor = true;
            btnWithdrawCancel.Click += btnWithdrawCancel_Click;
            // 
            // btnWithdraw
            // 
            btnWithdraw.Location = new Point(140, 271);
            btnWithdraw.Margin = new Padding(4, 5, 4, 5);
            btnWithdraw.Name = "btnWithdraw";
            btnWithdraw.Size = new Size(107, 38);
            btnWithdraw.TabIndex = 6;
            btnWithdraw.Text = "Withdraw";
            btnWithdraw.UseVisualStyleBackColor = true;
            btnWithdraw.Click += btnWithdraw_Click;
            // 
            // txtWithdrawAmount
            // 
            txtWithdrawAmount.Location = new Point(338, 190);
            txtWithdrawAmount.Margin = new Padding(4, 5, 4, 5);
            txtWithdrawAmount.Name = "txtWithdrawAmount";
            txtWithdrawAmount.Size = new Size(197, 31);
            txtWithdrawAmount.TabIndex = 5;
            txtWithdrawAmount.TextAlign = HorizontalAlignment.Right;
            txtWithdrawAmount.KeyPress += txtWithdrawAmount_KeyPress;
            // 
            // txtWithdrawPassword
            // 
            txtWithdrawPassword.Location = new Point(338, 121);
            txtWithdrawPassword.Margin = new Padding(4, 5, 4, 5);
            txtWithdrawPassword.Name = "txtWithdrawPassword";
            txtWithdrawPassword.Size = new Size(197, 31);
            txtWithdrawPassword.TabIndex = 4;
            txtWithdrawPassword.UseSystemPasswordChar = true;
            // 
            // txtWithdrawMobileno
            // 
            txtWithdrawMobileno.Location = new Point(338, 56);
            txtWithdrawMobileno.Margin = new Padding(4, 5, 4, 5);
            txtWithdrawMobileno.Name = "txtWithdrawMobileno";
            txtWithdrawMobileno.Size = new Size(197, 31);
            txtWithdrawMobileno.TabIndex = 3;
            txtWithdrawMobileno.KeyPress += txtWithdrawMobileno_KeyPress;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(140, 196);
            label11.Margin = new Padding(4, 0, 4, 0);
            label11.Name = "label11";
            label11.Size = new Size(77, 25);
            label11.TabIndex = 2;
            label11.Text = "Amount";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(140, 127);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(87, 25);
            label10.TabIndex = 1;
            label10.Text = "Password";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(140, 62);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(100, 25);
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
            tabPage2.Location = new Point(4, 34);
            tabPage2.Margin = new Padding(4, 5, 4, 5);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(4, 5, 4, 5);
            tabPage2.Size = new Size(721, 425);
            tabPage2.TabIndex = 3;
            tabPage2.Text = "Deposit";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDepositCancel
            // 
            btnDepositCancel.Location = new Point(373, 298);
            btnDepositCancel.Margin = new Padding(4, 5, 4, 5);
            btnDepositCancel.Name = "btnDepositCancel";
            btnDepositCancel.Size = new Size(107, 38);
            btnDepositCancel.TabIndex = 7;
            btnDepositCancel.Text = "Cancel";
            btnDepositCancel.UseVisualStyleBackColor = true;
            btnDepositCancel.Click += btnDepositCancel_Click;
            // 
            // btnDeposit
            // 
            btnDeposit.Location = new Point(180, 298);
            btnDeposit.Margin = new Padding(4, 5, 4, 5);
            btnDeposit.Name = "btnDeposit";
            btnDeposit.Size = new Size(107, 38);
            btnDeposit.TabIndex = 6;
            btnDeposit.Text = "Deposit";
            btnDeposit.UseVisualStyleBackColor = true;
            btnDeposit.Click += btnDeposit_Click;
            // 
            // txtDepositAmount
            // 
            txtDepositAmount.Location = new Point(269, 187);
            txtDepositAmount.Margin = new Padding(4, 5, 4, 5);
            txtDepositAmount.Name = "txtDepositAmount";
            txtDepositAmount.Size = new Size(210, 31);
            txtDepositAmount.TabIndex = 5;
            txtDepositAmount.TextAlign = HorizontalAlignment.Right;
            txtDepositAmount.KeyPress += txtDepositAmount_KeyPress;
            // 
            // txtDepositPassword
            // 
            txtDepositPassword.Location = new Point(269, 110);
            txtDepositPassword.Margin = new Padding(4, 5, 4, 5);
            txtDepositPassword.Name = "txtDepositPassword";
            txtDepositPassword.Size = new Size(210, 31);
            txtDepositPassword.TabIndex = 4;
            txtDepositPassword.UseSystemPasswordChar = true;
            // 
            // txtDepositMobileno
            // 
            txtDepositMobileno.Location = new Point(269, 42);
            txtDepositMobileno.Margin = new Padding(4, 5, 4, 5);
            txtDepositMobileno.Name = "txtDepositMobileno";
            txtDepositMobileno.Size = new Size(210, 31);
            txtDepositMobileno.TabIndex = 3;
            txtDepositMobileno.KeyPress += txtDepositMobileno_KeyPress;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(151, 200);
            label14.Margin = new Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new Size(77, 25);
            label14.TabIndex = 2;
            label14.Text = "Amount";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(151, 123);
            label13.Margin = new Padding(4, 0, 4, 0);
            label13.Name = "label13";
            label13.Size = new Size(87, 25);
            label13.TabIndex = 1;
            label13.Text = "Password";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(151, 55);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(100, 25);
            label12.TabIndex = 0;
            label12.Text = "Mobile No.";
            // 
            // History
            // 
            History.Controls.Add(listView1);
            History.Controls.Add(txtHistoryPassword);
            History.Controls.Add(txtHistoryMobileno);
            History.Controls.Add(btnSearch);
            History.Controls.Add(label16);
            History.Controls.Add(label15);
            History.Location = new Point(4, 34);
            History.Name = "History";
            History.Padding = new Padding(3);
            History.Size = new Size(721, 425);
            History.TabIndex = 4;
            History.Text = "Transaction History";
            History.UseVisualStyleBackColor = true;
            History.Leave += History_Leave;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { TransactionID, From, To, Amount, Message });
            listView1.Location = new Point(40, 96);
            listView1.Name = "listView1";
            listView1.Size = new Size(655, 267);
            listView1.TabIndex = 5;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // TransactionID
            // 
            TransactionID.Text = "Transaction ID";
            TransactionID.Width = 130;
            // 
            // From
            // 
            From.Text = "From";
            From.Width = 130;
            // 
            // To
            // 
            To.Text = "To";
            To.Width = 130;
            // 
            // Amount
            // 
            Amount.Text = "Amount";
            Amount.TextAlign = HorizontalAlignment.Right;
            Amount.Width = 130;
            // 
            // Message
            // 
            Message.Text = "Message";
            Message.Width = 130;
            // 
            // txtHistoryPassword
            // 
            txtHistoryPassword.Location = new Point(398, 20);
            txtHistoryPassword.Name = "txtHistoryPassword";
            txtHistoryPassword.Size = new Size(173, 31);
            txtHistoryPassword.TabIndex = 4;
            txtHistoryPassword.UseSystemPasswordChar = true;
            // 
            // txtHistoryMobileno
            // 
            txtHistoryMobileno.Location = new Point(113, 23);
            txtHistoryMobileno.Name = "txtHistoryMobileno";
            txtHistoryMobileno.Size = new Size(164, 31);
            txtHistoryMobileno.TabIndex = 3;
            txtHistoryMobileno.KeyPress += txtHistoryMobileno_KeyPress;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(603, 17);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(112, 34);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(305, 23);
            label16.Name = "label16";
            label16.Size = new Size(87, 25);
            label16.TabIndex = 1;
            label16.Text = "Password";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(7, 23);
            label15.Name = "label15";
            label15.Size = new Size(100, 25);
            label15.TabIndex = 0;
            label15.Text = "Mobile No.";
            // 
            // frmWallet
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(742, 667);
            Controls.Add(tabControl1);
            Name = "frmWallet";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ATM Window App";
            tbTransfer.ResumeLayout(false);
            tbTransfer.PerformLayout();
            tbCreate.ResumeLayout(false);
            tbCreate.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            History.ResumeLayout(false);
            History.PerformLayout();
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
        private TabPage History;
        private TextBox txtHistoryPassword;
        private TextBox txtHistoryMobileno;
        private Button btnSearch;
        private Label label16;
        private Label label15;
        private ListView listView1;
        private ColumnHeader TransactionID;
        private ColumnHeader From;
        private ColumnHeader To;
        private ColumnHeader Amount;
        private ColumnHeader Message;
        //private Button btnExit;
    }
}

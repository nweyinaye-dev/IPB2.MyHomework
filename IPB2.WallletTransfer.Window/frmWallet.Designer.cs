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
            tbExit = new TabPage();
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
            tabControl1 = new TabControl();
            tbTransfer.SuspendLayout();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tbExit
            // 
            tbExit.Location = new Point(4, 34);
            tbExit.Name = "tbExit";
            tbExit.Padding = new Padding(3);
            tbExit.Size = new Size(597, 375);
            tbExit.TabIndex = 2;
            tbExit.Text = "Exit";
            tbExit.UseVisualStyleBackColor = true;
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
            tbTransfer.Size = new Size(597, 375);
            tbTransfer.TabIndex = 1;
            tbTransfer.Text = "Transfer";
            tbTransfer.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(332, 305);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(112, 34);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnTransfer
            // 
            btnTransfer.Location = new Point(160, 305);
            btnTransfer.Name = "btnTransfer";
            btnTransfer.Size = new Size(112, 34);
            btnTransfer.TabIndex = 8;
            btnTransfer.Text = "Transfer";
            btnTransfer.UseVisualStyleBackColor = true;
            btnTransfer.Click += btnTransfer_Click;
            // 
            // txtBalance
            // 
            txtBalance.Location = new Point(270, 221);
            txtBalance.Name = "txtBalance";
            txtBalance.Size = new Size(195, 31);
            txtBalance.TabIndex = 7;
            txtBalance.TextAlign = HorizontalAlignment.Right;
            txtBalance.KeyPress += txtBalance_KeyPress;
            // 
            // txtRecMobileno
            // 
            txtRecMobileno.Location = new Point(270, 160);
            txtRecMobileno.Name = "txtRecMobileno";
            txtRecMobileno.Size = new Size(195, 31);
            txtRecMobileno.TabIndex = 6;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(270, 98);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(195, 31);
            txtPassword.TabIndex = 3;
            // 
            // txtMobileno
            // 
            txtMobileno.Location = new Point(270, 33);
            txtMobileno.Name = "txtMobileno";
            txtMobileno.Size = new Size(195, 31);
            txtMobileno.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(76, 227);
            label4.Name = "label4";
            label4.Size = new Size(71, 25);
            label4.TabIndex = 5;
            label4.Text = "Balance";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(76, 160);
            label3.Name = "label3";
            label3.Size = new Size(176, 25);
            label3.TabIndex = 4;
            label3.Text = "Recipient Mobile No.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(76, 98);
            label2.Name = "label2";
            label2.Size = new Size(87, 25);
            label2.TabIndex = 2;
            label2.Text = "Password";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(76, 39);
            label1.Name = "label1";
            label1.Size = new Size(160, 25);
            label1.TabIndex = 1;
            label1.Text = "Sender Mobile No.";
            // 
            // tbCreate
            // 
            tbCreate.Location = new Point(4, 34);
            tbCreate.Name = "tbCreate";
            tbCreate.Padding = new Padding(3);
            tbCreate.Size = new Size(597, 375);
            tbCreate.TabIndex = 0;
            tbCreate.Text = "Create";
            tbCreate.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tbCreate);
            tabControl1.Controls.Add(tbTransfer);
            tabControl1.Controls.Add(tbExit);
            tabControl1.Location = new Point(39, 34);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(605, 413);
            tabControl1.TabIndex = 4;
            // 
            // frmWallet
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 666);
            Controls.Add(tabControl1);
            Name = "frmWallet";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Wallet";
            Load += frmWallet_Load;
            tbTransfer.ResumeLayout(false);
            tbTransfer.PerformLayout();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage tbExit;
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
    }
}

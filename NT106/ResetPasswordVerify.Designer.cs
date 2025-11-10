namespace plan_fighting_super_start
{
    partial class ResetPasswordVerify
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblEmailCap;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblCodeCap;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblNewPassCap;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblConfirmCap;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblEmailCap = new Label();
            lblEmail = new Label();
            lblCodeCap = new Label();
            txtCode = new TextBox();
            lblNewPassCap = new Label();
            txtNewPassword = new TextBox();
            lblConfirmCap = new Label();
            txtConfirmPassword = new TextBox();
            chkShowPassword = new CheckBox();
            btnConfirm = new Button();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(14, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(526, 53);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Xác nhận đặt lại mật khẩu";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblEmailCap
            // 
            lblEmailCap.AutoSize = true;
            lblEmailCap.Font = new Font("Segoe UI", 9.5F);
            lblEmailCap.Location = new Point(30, 80);
            lblEmailCap.Name = "lblEmailCap";
            lblEmailCap.Size = new Size(130, 21);
            lblEmailCap.TabIndex = 1;
            lblEmailCap.Text = "Tài khoản (email):";
            // 
            // lblEmail
            // 
            lblEmail.AutoEllipsis = true;
            lblEmail.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblEmail.Location = new Point(163, 80);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(377, 27);
            lblEmail.TabIndex = 2;
            lblEmail.Text = "user@example.com";
            // 
            // lblCodeCap
            // 
            lblCodeCap.AutoSize = true;
            lblCodeCap.Font = new Font("Segoe UI", 10F);
            lblCodeCap.Location = new Point(30, 127);
            lblCodeCap.Name = "lblCodeCap";
            lblCodeCap.Size = new Size(207, 23);
            lblCodeCap.TabIndex = 3;
            lblCodeCap.Text = "Mã xác minh (OTP) đã gửi";
            // 
            // txtCode
            // 
            txtCode.Font = new Font("Segoe UI", 10F);
            txtCode.Location = new Point(34, 156);
            txtCode.Margin = new Padding(3, 4, 3, 4);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(479, 30);
            txtCode.TabIndex = 4;
            // 
            // lblNewPassCap
            // 
            lblNewPassCap.AutoSize = true;
            lblNewPassCap.Font = new Font("Segoe UI", 10F);
            lblNewPassCap.Location = new Point(30, 205);
            lblNewPassCap.Name = "lblNewPassCap";
            lblNewPassCap.Size = new Size(116, 23);
            lblNewPassCap.TabIndex = 5;
            lblNewPassCap.Text = "Mật khẩu mới";
            // 
            // txtNewPassword
            // 
            txtNewPassword.Font = new Font("Segoe UI", 10F);
            txtNewPassword.Location = new Point(34, 235);
            txtNewPassword.Margin = new Padding(3, 4, 3, 4);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(479, 30);
            txtNewPassword.TabIndex = 6;
            txtNewPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirmCap
            // 
            lblConfirmCap.AutoSize = true;
            lblConfirmCap.Font = new Font("Segoe UI", 10F);
            lblConfirmCap.Location = new Point(30, 283);
            lblConfirmCap.Name = "lblConfirmCap";
            lblConfirmCap.Size = new Size(185, 23);
            lblConfirmCap.TabIndex = 7;
            lblConfirmCap.Text = "Nhập lại mật khẩu mới";
            // 
            // txtConfirmPassword
            // 
            txtConfirmPassword.Font = new Font("Segoe UI", 10F);
            txtConfirmPassword.Location = new Point(34, 312);
            txtConfirmPassword.Margin = new Padding(3, 4, 3, 4);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(479, 30);
            txtConfirmPassword.TabIndex = 8;
            txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Font = new Font("Segoe UI", 9F);
            chkShowPassword.Location = new Point(34, 353);
            chkShowPassword.Margin = new Padding(3, 4, 3, 4);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(148, 24);
            chkShowPassword.TabIndex = 9;
            chkShowPassword.Text = "Hiển thị mật khẩu";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // btnConfirm
            // 
            btnConfirm.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConfirm.Location = new Point(34, 396);
            btnConfirm.Margin = new Padding(3, 4, 3, 4);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(183, 48);
            btnConfirm.TabIndex = 10;
            btnConfirm.Text = "Đặt lại mật khẩu";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 9F);
            lblStatus.ForeColor = Color.DimGray;
            lblStatus.Location = new Point(30, 460);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(485, 53);
            lblStatus.TabIndex = 11;
            lblStatus.Text = "Nhập mã xác minh đã gửi đến email và đặt mật khẩu mới.";
            // 
            // ResetPasswordVerify
            // 
            AcceptButton = btnConfirm;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(553, 535);
            Controls.Add(lblStatus);
            Controls.Add(btnConfirm);
            Controls.Add(chkShowPassword);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmCap);
            Controls.Add(txtNewPassword);
            Controls.Add(lblNewPassCap);
            Controls.Add(txtCode);
            Controls.Add(lblCodeCap);
            Controls.Add(lblEmail);
            Controls.Add(lblEmailCap);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ResetPasswordVerify";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Xác nhận đặt lại mật khẩu";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
    }
}

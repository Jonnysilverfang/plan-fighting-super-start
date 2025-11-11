using System.Windows.Forms;
using System.Drawing;

namespace plan_fighting_super_start
{
    partial class FogotPass
    {
        private System.ComponentModel.IContainer components = null;

        private Label labelTitle;
        private Label labelUser;
        private Label labelEmail;
        private Label labelCode;
        private Label labelNewPass;
        private Label labelConfirmPass;

        private TextBox textBoxUser;
        private TextBox textBoxEmail;
        private TextBox textBoxCode;
        private TextBox textBoxNewPass;
        private TextBox textBoxConfirmPass;

        private Button buttonSendCode;
        private Button buttonConfirm;
        private Button buttonCancel;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
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
            components = new System.ComponentModel.Container();
            labelTitle = new Label();
            labelUser = new Label();
            labelEmail = new Label();
            labelCode = new Label();
            labelNewPass = new Label();
            labelConfirmPass = new Label();

            textBoxUser = new TextBox();
            textBoxEmail = new TextBox();
            textBoxCode = new TextBox();
            textBoxNewPass = new TextBox();
            textBoxConfirmPass = new TextBox();

            buttonSendCode = new Button();
            buttonConfirm = new Button();
            buttonCancel = new Button();

            SuspendLayout();

            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 163);
            labelTitle.ForeColor = Color.FromArgb(0, 192, 192);
            labelTitle.Location = new Point(70, 15);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(290, 41);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "QUÊN MẬT KHẨU";

            // 
            // labelUser
            // 
            labelUser.AutoSize = true;
            labelUser.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            labelUser.Location = new Point(30, 70);
            labelUser.Name = "labelUser";
            labelUser.Size = new Size(119, 23);
            labelUser.TabIndex = 1;
            labelUser.Text = "Tên đăng nhập";

            // 
            // textBoxUser
            // 
            textBoxUser.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBoxUser.Location = new Point(30, 95);
            textBoxUser.Name = "textBoxUser";
            textBoxUser.PlaceholderText = "Nhập tên đăng nhập";
            textBoxUser.Size = new Size(340, 30);
            textBoxUser.TabIndex = 0;

            // 
            // labelEmail
            // 
            labelEmail.AutoSize = true;
            labelEmail.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            labelEmail.Location = new Point(30, 135);
            labelEmail.Name = "labelEmail";
            labelEmail.Size = new Size(57, 23);
            labelEmail.TabIndex = 3;
            labelEmail.Text = "Gmail";

            // 
            // textBoxEmail
            // 
            textBoxEmail.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBoxEmail.Location = new Point(30, 160);
            textBoxEmail.Name = "textBoxEmail";
            textBoxEmail.PlaceholderText = "Nhập Gmail đã đăng ký";
            textBoxEmail.Size = new Size(340, 30);
            textBoxEmail.TabIndex = 1;

            // 
            // buttonSendCode
            // 
            buttonSendCode.FlatStyle = FlatStyle.Flat;
            buttonSendCode.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            buttonSendCode.ForeColor = Color.FromArgb(0, 192, 192);
            buttonSendCode.Location = new Point(30, 205);
            buttonSendCode.Name = "buttonSendCode";
            buttonSendCode.Size = new Size(155, 35);
            buttonSendCode.TabIndex = 2;
            buttonSendCode.Text = "Gửi mã";
            buttonSendCode.UseVisualStyleBackColor = true;
            buttonSendCode.Click += buttonSendCode_Click;

            // 
            // labelCode
            // 
            labelCode.AutoSize = true;
            labelCode.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            labelCode.Location = new Point(30, 255);
            labelCode.Name = "labelCode";
            labelCode.Size = new Size(96, 23);
            labelCode.TabIndex = 6;
            labelCode.Text = "Mã xác minh";

            // 
            // textBoxCode
            // 
            textBoxCode.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBoxCode.Location = new Point(30, 280);
            textBoxCode.Name = "textBoxCode";
            textBoxCode.PlaceholderText = "Nhập mã đã gửi qua Gmail";
            textBoxCode.Size = new Size(340, 30);
            textBoxCode.TabIndex = 3;

            // 
            // labelNewPass
            // 
            labelNewPass.AutoSize = true;
            labelNewPass.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            labelNewPass.Location = new Point(30, 320);
            labelNewPass.Name = "labelNewPass";
            labelNewPass.Size = new Size(105, 23);
            labelNewPass.TabIndex = 8;
            labelNewPass.Text = "Mật khẩu mới";

            // 
            // textBoxNewPass
            // 
            textBoxNewPass.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBoxNewPass.Location = new Point(30, 345);
            textBoxNewPass.Name = "textBoxNewPass";
            textBoxNewPass.PlaceholderText = "Nhập mật khẩu mới";
            textBoxNewPass.Size = new Size(340, 30);
            textBoxNewPass.TabIndex = 4;
            textBoxNewPass.UseSystemPasswordChar = true;

            // 
            // labelConfirmPass
            // 
            labelConfirmPass.AutoSize = true;
            labelConfirmPass.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            labelConfirmPass.Location = new Point(30, 385);
            labelConfirmPass.Name = "labelConfirmPass";
            labelConfirmPass.Size = new Size(151, 23);
            labelConfirmPass.TabIndex = 10;
            labelConfirmPass.Text = "Nhập lại mật khẩu";

            // 
            // textBoxConfirmPass
            // 
            textBoxConfirmPass.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBoxConfirmPass.Location = new Point(30, 410);
            textBoxConfirmPass.Name = "textBoxConfirmPass";
            textBoxConfirmPass.PlaceholderText = "Nhập lại mật khẩu mới";
            textBoxConfirmPass.Size = new Size(340, 30);
            textBoxConfirmPass.TabIndex = 5;
            textBoxConfirmPass.UseSystemPasswordChar = true;

            // 
            // buttonConfirm
            // 
            buttonConfirm.FlatStyle = FlatStyle.Flat;
            buttonConfirm.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            buttonConfirm.ForeColor = Color.FromArgb(0, 192, 192);
            buttonConfirm.Location = new Point(30, 455);
            buttonConfirm.Name = "buttonConfirm";
            buttonConfirm.Size = new Size(155, 35);
            buttonConfirm.TabIndex = 6;
            buttonConfirm.Text = "Đổi mật khẩu";
            buttonConfirm.UseVisualStyleBackColor = true;
            buttonConfirm.Click += buttonConfirm_Click;

            // 
            // buttonCancel
            // 
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            buttonCancel.ForeColor = Color.Gray;
            buttonCancel.Location = new Point(215, 455);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(155, 35);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "Hủy";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;

            // 
            // FogotPass
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(400, 515);
            Controls.Add(buttonCancel);
            Controls.Add(buttonConfirm);
            Controls.Add(textBoxConfirmPass);
            Controls.Add(labelConfirmPass);
            Controls.Add(textBoxNewPass);
            Controls.Add(labelNewPass);
            Controls.Add(textBoxCode);
            Controls.Add(labelCode);
            Controls.Add(buttonSendCode);
            Controls.Add(textBoxEmail);
            Controls.Add(labelEmail);
            Controls.Add(textBoxUser);
            Controls.Add(labelUser);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FogotPass";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quên mật khẩu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}

using System.Windows.Forms;
using System.Drawing;

namespace plan_fighting_super_start
{
    partial class ChangePass
    {
        private System.ComponentModel.IContainer components = null;

        private Label labelTitle;
        private Label labelNewPass;
        private TextBox textBoxNewPass;
        private Button buttonChange;
        private Button buttonCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            labelTitle = new Label();
            labelNewPass = new Label();
            textBoxNewPass = new TextBox();
            buttonChange = new Button();
            buttonCancel = new Button();

            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 163);
            labelTitle.ForeColor = Color.FromArgb(0, 192, 192);
            labelTitle.Location = new Point(50, 20);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(178, 32);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "ĐỔI MẬT KHẨU";
            // 
            // labelNewPass
            // 
            labelNewPass.AutoSize = true;
            labelNewPass.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            labelNewPass.Location = new Point(25, 75);
            labelNewPass.Name = "labelNewPass";
            labelNewPass.Size = new Size(105, 23);
            labelNewPass.TabIndex = 1;
            labelNewPass.Text = "Mật khẩu mới";
            // 
            // textBoxNewPass
            // 
            textBoxNewPass.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBoxNewPass.Location = new Point(25, 100);
            textBoxNewPass.Name = "textBoxNewPass";
            textBoxNewPass.PlaceholderText = "Nhập mật khẩu mới";
            textBoxNewPass.Size = new Size(260, 30);
            textBoxNewPass.TabIndex = 0;
            textBoxNewPass.UseSystemPasswordChar = true;
            // 
            // buttonChange
            // 
            buttonChange.FlatStyle = FlatStyle.Flat;
            buttonChange.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            buttonChange.ForeColor = Color.FromArgb(0, 192, 192);
            buttonChange.Location = new Point(25, 150);
            buttonChange.Name = "buttonChange";
            buttonChange.Size = new Size(120, 35);
            buttonChange.TabIndex = 1;
            buttonChange.Text = "Đổi mật khẩu";
            buttonChange.UseVisualStyleBackColor = true;
            buttonChange.Click += buttonChange_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            buttonCancel.ForeColor = Color.Gray;
            buttonCancel.Location = new Point(165, 150);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(120, 35);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Hủy";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // ChangePass
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(310, 210);
            Controls.Add(buttonCancel);
            Controls.Add(buttonChange);
            Controls.Add(textBoxNewPass);
            Controls.Add(labelNewPass);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangePass";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đổi mật khẩu";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

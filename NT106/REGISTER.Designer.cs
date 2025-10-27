using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Font = System.Drawing.Font;
using Image = System.Drawing.Image;

namespace plan_fighting_super_start
{
    partial class Register
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.Button buttonRegister;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            textBoxUser = new TextBox();
            textBoxPass = new TextBox();
            buttonRegister = new Button();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            label3 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // textBoxUser
            // 
            textBoxUser.BackColor = Color.FromArgb(0, 192, 192);
            textBoxUser.Font = new Font("Segoe UI", 10.2F);
            textBoxUser.ForeColor = Color.White;
            textBoxUser.Location = new Point(127, 252);
            textBoxUser.Name = "textBoxUser";
            textBoxUser.PlaceholderText = "Tên đăng nhập";
            textBoxUser.Size = new Size(200, 30);
            textBoxUser.TabIndex = 0;
            textBoxUser.Text = "Tên Đăng Nhập";
            // 
            // textBoxPass
            // 
            textBoxPass.BackColor = Color.FromArgb(0, 192, 192);
            textBoxPass.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBoxPass.ForeColor = Color.White;
            textBoxPass.Location = new Point(127, 309);
            textBoxPass.Name = "textBoxPass";
            textBoxPass.PlaceholderText = "Mật khẩu";
            textBoxPass.Size = new Size(200, 30);
            textBoxPass.TabIndex = 1;
            textBoxPass.Text = "Mật khẩu";
            textBoxPass.UseSystemPasswordChar = true;
            textBoxPass.TextChanged += textBoxPass_TextChanged;
            // 
            // buttonRegister
            // 
            buttonRegister.BackColor = Color.Transparent;
            buttonRegister.FlatStyle = FlatStyle.Flat;
            buttonRegister.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            buttonRegister.ForeColor = Color.White;
            buttonRegister.Location = new Point(170, 364);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(90, 30);
            buttonRegister.TabIndex = 2;
            buttonRegister.Text = "Đăng ký";
            buttonRegister.UseVisualStyleBackColor = false;
            buttonRegister.Click += buttonRegister_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(155, 129);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(144, 87);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = Color.White;
            label1.Location = new Point(155, 65);
            label1.Name = "label1";
            label1.Size = new Size(203, 54);
            label1.TabIndex = 10;
            label1.Text = "REGISTER";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(89, 316);
            label3.Name = "label3";
            label3.Size = new Size(34, 23);
            label3.TabIndex = 12;
            label3.Text = "🔒";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(89, 255);
            label2.Name = "label2";
            label2.Size = new Size(34, 23);
            label2.TabIndex = 11;
            label2.Text = "👤";
            // 
            // Register
            // 
            BackColor = Color.White;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(439, 441);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(textBoxUser);
            Controls.Add(textBoxPass);
            Controls.Add(buttonRegister);
            Name = "Register";
            Text = "Đăng ký";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private PictureBox pictureBox2;
        private Label label1;
        private Label label3;
        private Label label2;
    }
}

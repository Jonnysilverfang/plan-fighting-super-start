<<<<<<< HEAD
﻿namespace plan_fighting_super_start
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
=======
﻿namespace Kien
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox textBoxUser;
        private TextBox textBoxPass;
        private Button buttonLogin;
        private Button buttonRegister;
        private CheckBox checkBoxShow;
        private PictureBox pictureBox2;
        private Label label1;
        private Label label2;
        private Label label3;

>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

<<<<<<< HEAD
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.checkBoxShow = new System.Windows.Forms.CheckBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.labelTitle.Location = new System.Drawing.Point(60, 30);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(300, 45);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "ĐĂNG NHẬP";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxUser
            // 
            this.textBoxUser.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxUser.Location = new System.Drawing.Point(65, 100);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(280, 29);
            this.textBoxUser.TabIndex = 1;
            // 
            // textBoxPass
            // 
            this.textBoxPass.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxPass.Location = new System.Drawing.Point(65, 150);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.Size = new System.Drawing.Size(280, 29);
            this.textBoxPass.TabIndex = 2;
            // 
            // checkBoxShow
            // 
            this.checkBoxShow.AutoSize = true;
            this.checkBoxShow.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.checkBoxShow.Location = new System.Drawing.Point(65, 190);
            this.checkBoxShow.Name = "checkBoxShow";
            this.checkBoxShow.Size = new System.Drawing.Size(128, 23);
            this.checkBoxShow.TabIndex = 3;
            this.checkBoxShow.Text = "Hiện mật khẩu";
            this.checkBoxShow.UseVisualStyleBackColor = true;
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonLogin.ForeColor = System.Drawing.Color.White;
            this.buttonLogin.Location = new System.Drawing.Point(65, 230);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(280, 40);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Đăng nhập";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.BackColor = System.Drawing.Color.LightGray;
            this.buttonRegister.FlatAppearance.BorderSize = 0;
            this.buttonRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRegister.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.buttonRegister.ForeColor = System.Drawing.Color.Black;
            this.buttonRegister.Location = new System.Drawing.Point(65, 280);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(280, 35);
            this.buttonRegister.TabIndex = 5;
            this.buttonRegister.Text = "Đăng ký tài khoản";
            this.buttonRegister.UseVisualStyleBackColor = false;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(410, 350);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.checkBoxShow);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.CheckBox checkBoxShow;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonRegister;
=======
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBoxUser = new TextBox();
            textBoxPass = new TextBox();
            buttonLogin = new Button();
            buttonRegister = new Button();
            checkBoxShow = new CheckBox();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // textBoxUser
            // 
            textBoxUser.BackColor = Color.FromArgb(0, 192, 192);
            textBoxUser.Font = new Font("Segoe UI", 10.2F);
            textBoxUser.ForeColor = Color.White;
            textBoxUser.Location = new Point(120, 216);
            textBoxUser.Name = "textBoxUser";
            textBoxUser.Size = new Size(200, 30);
            textBoxUser.TabIndex = 0;
            textBoxUser.Text = "Nhập tên đăng nhập";
            // 
            // textBoxPass
            // 
            textBoxPass.BackColor = Color.FromArgb(0, 192, 192);
            textBoxPass.Font = new Font("Segoe UI", 10.2F);
            textBoxPass.ForeColor = Color.White;
            textBoxPass.Location = new Point(120, 268);
            textBoxPass.Name = "textBoxPass";
            textBoxPass.PlaceholderText = "Nhập mật khẩu ";
            textBoxPass.Size = new Size(200, 30);
            textBoxPass.TabIndex = 1;
            textBoxPass.Text = "Nhập mật khẩu ";
            // 
            // buttonLogin
            // 
            buttonLogin.BackColor = Color.Transparent;
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            buttonLogin.ForeColor = Color.White;
            buttonLogin.Location = new Point(101, 342);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(112, 40);
            buttonLogin.TabIndex = 2;
            buttonLogin.Text = "Đăng nhập";
            buttonLogin.UseVisualStyleBackColor = false;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // buttonRegister
            // 
            buttonRegister.BackColor = Color.Transparent;
            buttonRegister.FlatStyle = FlatStyle.Flat;
            buttonRegister.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            buttonRegister.ForeColor = Color.White;
            buttonRegister.Location = new Point(247, 342);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(112, 40);
            buttonRegister.TabIndex = 3;
            buttonRegister.Text = "Đăng ký";
            buttonRegister.UseVisualStyleBackColor = false;
            buttonRegister.Click += buttonRegister_Click;
            // 
            // checkBoxShow
            // 
            checkBoxShow.AutoSize = true;
            checkBoxShow.BackColor = Color.Transparent;
            checkBoxShow.FlatStyle = FlatStyle.Flat;
            checkBoxShow.Font = new Font("Segoe UI", 10.2F);
            checkBoxShow.ForeColor = Color.White;
            checkBoxShow.Location = new Point(163, 304);
            checkBoxShow.Name = "checkBoxShow";
            checkBoxShow.Size = new Size(140, 27);
            checkBoxShow.TabIndex = 4;
            checkBoxShow.Text = "Hiện mật khẩu";
            checkBoxShow.UseVisualStyleBackColor = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(146, 113);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(161, 97);
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = Color.White;
            label1.Location = new Point(163, 46);
            label1.Name = "label1";
            label1.Size = new Size(145, 54);
            label1.TabIndex = 8;
            label1.Text = "LOGIN";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(87, 219);
            label2.Name = "label2";
            label2.Size = new Size(34, 23);
            label2.TabIndex = 9;
            label2.Text = "👤";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(87, 271);
            label3.Name = "label3";
            label3.Size = new Size(34, 23);
            label3.TabIndex = 10;
            label3.Text = "🔒";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(432, 436);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox2);
            Controls.Add(textBoxUser);
            Controls.Add(textBoxPass);
            Controls.Add(buttonLogin);
            Controls.Add(buttonRegister);
            Controls.Add(checkBoxShow);
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Show;
            Text = "Đăng nhập";
            TopMost = true;
            Load += Form1_Load_1;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
    }
}

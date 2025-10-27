namespace plan_fighting_super_start
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
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            labelTitle = new Label();
            textBoxUser = new TextBox();
            textBoxPass = new TextBox();
            checkBoxShow = new CheckBox();
            buttonLogin = new Button();
            buttonRegister = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 163);
            labelTitle.ForeColor = Color.FromArgb(0, 192, 192);
            labelTitle.Location = new Point(138, 18);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(194, 63);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "LOGIN";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBoxUser
            // 
            textBoxUser.Font = new Font("Segoe UI", 12F);
            textBoxUser.ForeColor = Color.FromArgb(0, 192, 192);
            textBoxUser.Location = new Point(72, 216);
            textBoxUser.Margin = new Padding(3, 4, 3, 4);
            textBoxUser.Name = "textBoxUser";
            textBoxUser.Size = new Size(319, 34);
            textBoxUser.TabIndex = 1;
            // 
            // textBoxPass
            // 
            textBoxPass.Font = new Font("Segoe UI", 12F);
            textBoxPass.ForeColor = Color.FromArgb(0, 192, 192);
            textBoxPass.Location = new Point(72, 267);
            textBoxPass.Margin = new Padding(3, 4, 3, 4);
            textBoxPass.Name = "textBoxPass";
            textBoxPass.Size = new Size(319, 34);
            textBoxPass.TabIndex = 2;
            // 
            // checkBoxShow
            // 
            checkBoxShow.AutoSize = true;
            checkBoxShow.BackColor = Color.Transparent;
            checkBoxShow.Font = new Font("Segoe UI", 10F);
            checkBoxShow.ForeColor = Color.FromArgb(0, 192, 192);
            checkBoxShow.Location = new Point(162, 309);
            checkBoxShow.Margin = new Padding(3, 4, 3, 4);
            checkBoxShow.Name = "checkBoxShow";
            checkBoxShow.Size = new Size(144, 27);
            checkBoxShow.TabIndex = 3;
            checkBoxShow.Text = "Hiện mật khẩu";
            checkBoxShow.UseVisualStyleBackColor = false;
            // 
            // buttonLogin
            // 
            buttonLogin.BackColor = Color.Transparent;
            buttonLogin.FlatAppearance.BorderSize = 0;
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonLogin.ForeColor = Color.FromArgb(0, 192, 192);
            buttonLogin.Location = new Point(72, 331);
            buttonLogin.Margin = new Padding(3, 4, 3, 4);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(154, 53);
            buttonLogin.TabIndex = 4;
            buttonLogin.Text = "Đăng nhập";
            buttonLogin.UseVisualStyleBackColor = false;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // buttonRegister
            // 
            buttonRegister.BackColor = Color.Transparent;
            buttonRegister.FlatAppearance.BorderSize = 0;
            buttonRegister.FlatStyle = FlatStyle.Flat;
            buttonRegister.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            buttonRegister.ForeColor = Color.FromArgb(0, 192, 192);
            buttonRegister.Location = new Point(232, 335);
            buttonRegister.Margin = new Padding(3, 4, 3, 4);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(204, 47);
            buttonRegister.TabIndex = 5;
            buttonRegister.Text = "Đăng ký tài khoản";
            buttonRegister.UseVisualStyleBackColor = false;
            buttonRegister.Click += buttonRegister_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(155, 104);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(158, 92);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(338, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(143, 71);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox3.Location = new Point(-9, 393);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(125, 78);
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(469, 467);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(buttonRegister);
            Controls.Add(buttonLogin);
            Controls.Add(checkBoxShow);
            Controls.Add(textBoxPass);
            Controls.Add(textBoxUser);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += Login_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();

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

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;


    }
}

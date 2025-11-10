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
            labelTitle = new Label();
            textBoxUser = new TextBox();
            textBoxPass = new TextBox();
            checkBoxShow = new CheckBox();
            buttonLogin = new Button();
            buttonRegister = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label3 = new Label();
            label2 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(0, 192, 192);
            labelTitle.Location = new Point(111, 107);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(256, 60);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "ĐĂNG NHẬP";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBoxUser
            // 
            textBoxUser.Font = new Font("Segoe UI", 12F);
            textBoxUser.Location = new Point(74, 211);
            textBoxUser.Margin = new Padding(3, 4, 3, 4);
            textBoxUser.Name = "textBoxUser";
            textBoxUser.Size = new Size(319, 34);
            textBoxUser.TabIndex = 1;
            // 
            // textBoxPass
            // 
            textBoxPass.Font = new Font("Segoe UI", 12F);
            textBoxPass.Location = new Point(74, 265);
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
            checkBoxShow.Location = new Point(163, 354);
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
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            buttonLogin.ForeColor = Color.FromArgb(0, 192, 192);
            buttonLogin.Location = new Point(74, 307);
            buttonLogin.Margin = new Padding(3, 4, 3, 4);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(126, 41);
            buttonLogin.TabIndex = 4;
            buttonLogin.Text = "Đăng nhập";
            buttonLogin.UseVisualStyleBackColor = false;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // buttonRegister
            // 
            buttonRegister.BackColor = Color.Transparent;
            buttonRegister.FlatStyle = FlatStyle.Flat;
            buttonRegister.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            buttonRegister.ForeColor = Color.FromArgb(0, 192, 192);
            buttonRegister.Location = new Point(274, 307);
            buttonRegister.Margin = new Padding(3, 4, 3, 4);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(119, 41);
            buttonRegister.TabIndex = 5;
            buttonRegister.Text = "Đăng ký";
            buttonRegister.UseVisualStyleBackColor = false;
            buttonRegister.Click += buttonRegister_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resource.Screenshot_2025_09_21_180231;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(-53, 388);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(183, 97);
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = Properties.Resource.Screenshot_2025_09_21_175525;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(348, -13);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(148, 90);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(0, 192, 192);
            label3.Location = new Point(25, 272);
            label3.Name = "label3";
            label3.Size = new Size(40, 28);
            label3.TabIndex = 14;
            label3.Text = "🔒";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(0, 192, 192);
            label2.Location = new Point(25, 211);
            label2.Name = "label2";
            label2.Size = new Size(40, 28);
            label2.TabIndex = 13;
            label2.Text = "👤";
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Popup;
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.FromArgb(0, 192, 192);
            button1.Location = new Point(172, 388);
            button1.Name = "button1";
            button1.Size = new Size(122, 67);
            button1.TabIndex = 15;
            button1.Text = "Bạn Quên mật khẩu?";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(469, 467);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
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
            ResumeLayout(false);
            PerformLayout();

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
        private Label label3;
        private Label label2;
        private Button button1;
    }
}

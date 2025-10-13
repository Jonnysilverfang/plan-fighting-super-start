namespace Kien
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox textBoxUser;
        private TextBox textBoxPass;
        private Button buttonLogin;
        private Button buttonRegister;
        private CheckBox checkBoxShow;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Label label1;
        private Label label2;
        private Label label3;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            textBoxUser = new TextBox();
            textBoxPass = new TextBox();
            buttonLogin = new Button();
            buttonRegister = new Button();
            checkBoxShow = new CheckBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // textBoxUser
            // 
            textBoxUser.Font = new Font("Segoe UI", 10.2F);
            textBoxUser.ForeColor = Color.FromArgb(0, 192, 192);
            textBoxUser.Location = new Point(120, 216);
            textBoxUser.Name = "textBoxUser";
            textBoxUser.Size = new Size(200, 30);
            textBoxUser.TabIndex = 0;
            textBoxUser.Text = "Nhập tên đăng nhập";
            // 
            // textBoxPass
            // 
            textBoxPass.Font = new Font("Segoe UI", 10.2F);
            textBoxPass.ForeColor = Color.FromArgb(0, 192, 192);
            textBoxPass.Location = new Point(120, 268);
            textBoxPass.Name = "textBoxPass";
            textBoxPass.PlaceholderText = "Nhập mật khẩu ";
            textBoxPass.Size = new Size(200, 30);
            textBoxPass.TabIndex = 1;
            textBoxPass.Text = "Nhập mật khẩu ";
            // 
            // buttonLogin
            // 
            buttonLogin.Font = new Font("Segoe UI", 10.2F);
            buttonLogin.ForeColor = Color.FromArgb(0, 192, 192);
            buttonLogin.Location = new Point(101, 342);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(112, 40);
            buttonLogin.TabIndex = 2;
            buttonLogin.Text = "Đăng nhập";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // buttonRegister
            // 
            buttonRegister.Font = new Font("Segoe UI", 10.2F);
            buttonRegister.ForeColor = Color.FromArgb(0, 192, 192);
            buttonRegister.Location = new Point(247, 342);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(112, 40);
            buttonRegister.TabIndex = 3;
            buttonRegister.Text = "Đăng ký";
            buttonRegister.UseVisualStyleBackColor = true;
            buttonRegister.Click += buttonRegister_Click;
            // 
            // checkBoxShow
            // 
            checkBoxShow.AutoSize = true;
            checkBoxShow.FlatStyle = FlatStyle.Flat;
            checkBoxShow.Font = new Font("Segoe UI", 10.2F);
            checkBoxShow.ForeColor = Color.FromArgb(0, 192, 192);
            checkBoxShow.Location = new Point(163, 304);
            checkBoxShow.Name = "checkBoxShow";
            checkBoxShow.Size = new Size(140, 27);
            checkBoxShow.TabIndex = 4;
            checkBoxShow.Text = "Hiện mật khẩu";
            checkBoxShow.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(318, -2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 62);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(146, 113);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(161, 97);
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.Location = new Point(-5, 383);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(125, 62);
            pictureBox3.TabIndex = 7;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = Color.FromArgb(0, 192, 192);
            label1.Location = new Point(163, 46);
            label1.Name = "label1";
            label1.Size = new Size(145, 54);
            label1.TabIndex = 8;
            label1.Text = "LOGIN";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(0, 192, 192);
            label2.Location = new Point(87, 219);
            label2.Name = "label2";
            label2.Size = new Size(34, 23);
            label2.TabIndex = 9;
            label2.Text = "👤";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(0, 192, 192);
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
            ClientSize = new Size(439, 441);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(textBoxUser);
            Controls.Add(textBoxPass);
            Controls.Add(buttonLogin);
            Controls.Add(buttonRegister);
            Controls.Add(checkBoxShow);
            Name = "Form1";
            Text = "Đăng nhập";
            Load += Form1_Load_1;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

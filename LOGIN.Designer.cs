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
        private PictureBox pictureBox2;
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
    }
}

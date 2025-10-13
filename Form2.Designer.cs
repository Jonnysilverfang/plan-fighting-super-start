namespace Kien
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            textBoxUser = new TextBox();
            textBoxPass = new TextBox();
            buttonRegister = new Button();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label3 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // textBoxUser
            // 
            textBoxUser.BackColor = Color.White;
            textBoxUser.Font = new Font("Segoe UI", 10.2F);
            textBoxUser.ForeColor = Color.FromArgb(0, 192, 192);
            textBoxUser.Location = new Point(127, 252);
            textBoxUser.Name = "textBoxUser";
            textBoxUser.PlaceholderText = "Tên đăng nhập";
            textBoxUser.Size = new Size(200, 30);
            textBoxUser.TabIndex = 0;
            textBoxUser.Text = "Tên Đăng Nhập";
            // 
            // textBoxPass
            // 
            textBoxPass.BackColor = Color.White;
            textBoxPass.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 163);
            textBoxPass.ForeColor = Color.FromArgb(0, 192, 192);
            textBoxPass.Location = new Point(127, 309);
            textBoxPass.Name = "textBoxPass";
            textBoxPass.PlaceholderText = "Mật khẩu";
            textBoxPass.Size = new Size(200, 30);
            textBoxPass.TabIndex = 1;
            textBoxPass.Text = "Mật khẩu";
            textBoxPass.UseSystemPasswordChar = true;
            // 
            // buttonRegister
            // 
            buttonRegister.BackColor = Color.White;
            buttonRegister.Font = new Font("Segoe UI", 10.2F);
            buttonRegister.ForeColor = Color.FromArgb(0, 192, 192);
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
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(155, 129);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(144, 87);
            pictureBox2.TabIndex = 7;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImage = (Image)resources.GetObject("pictureBox3.BackgroundImage");
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.Location = new Point(-2, 379);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(125, 62);
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(312, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 62);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.ForeColor = Color.FromArgb(0, 192, 192);
            label1.Location = new Point(155, 65);
            label1.Name = "label1";
            label1.Size = new Size(203, 54);
            label1.TabIndex = 10;
            label1.Text = "REGISTER";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(0, 192, 192);
            label3.Location = new Point(89, 316);
            label3.Name = "label3";
            label3.Size = new Size(34, 23);
            label3.TabIndex = 12;
            label3.Text = "🔒";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(0, 192, 192);
            label2.Location = new Point(89, 255);
            label2.Name = "label2";
            label2.Size = new Size(34, 23);
            label2.TabIndex = 11;
            label2.Text = "👤";
            // 
            // Form2
            // 
            BackColor = Color.White;
            ClientSize = new Size(439, 441);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(textBoxUser);
            Controls.Add(textBoxPass);
            Controls.Add(buttonRegister);
            Name = "Form2";
            Text = "Đăng ký";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox1;
        private Label label1;
        private Label label3;
        private Label label2;
    }
}

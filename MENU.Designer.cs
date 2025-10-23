using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Font = System.Drawing.Font;
using Image = System.Drawing.Image;

namespace plan_fighting_super_start
{
    partial class Menu
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox textBoxGold;
        private Button buttonPlay;
        private Button buttonUpgradeHP;
        private Button buttonUpgradeDamage;
        private Button buttonExit;
        private Label label1;
        private Label label2;
        private TextBox textBox1;  // TextBox for HP
        private Label label3;
        private TextBox textBox2;  // TextBox for Damage
        private Button button1;  // Nút "Chơi với người"
        private TextBox textBox3; // TextBox for Level
        private Label label4;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            textBoxGold = new TextBox();
            buttonPlay = new Button();
            buttonUpgradeHP = new Button();
            buttonUpgradeDamage = new Button();
            buttonExit = new Button();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            textBox2 = new TextBox();
            button1 = new Button();
            textBox3 = new TextBox();
            label4 = new Label();
            pictureBox2 = new PictureBox();
            labelWelcome = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // textBoxGold
            // 
            textBoxGold.BackColor = Color.FromArgb(192, 255, 255);
            textBoxGold.BorderStyle = BorderStyle.None;
            textBoxGold.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBoxGold.ForeColor = Color.FromArgb(0, 192, 192);
            textBoxGold.Location = new Point(267, 277);
            textBoxGold.Name = "textBoxGold";
            textBoxGold.ReadOnly = true;
            textBoxGold.Size = new Size(94, 23);
            textBoxGold.TabIndex = 1;
            // 
            // buttonPlay
            // 
            buttonPlay.BackColor = Color.Transparent;
            buttonPlay.FlatStyle = FlatStyle.Flat;
            buttonPlay.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonPlay.ForeColor = Color.FromArgb(0, 192, 192);
            buttonPlay.Location = new Point(129, 419);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(139, 40);
            buttonPlay.TabIndex = 2;
            buttonPlay.Text = "Chơi BOSS";
            buttonPlay.UseVisualStyleBackColor = false;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // buttonUpgradeHP
            // 
            buttonUpgradeHP.BackColor = Color.Transparent;
            buttonUpgradeHP.FlatStyle = FlatStyle.Flat;
            buttonUpgradeHP.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonUpgradeHP.ForeColor = Color.FromArgb(0, 192, 192);
            buttonUpgradeHP.Location = new Point(291, 520);
            buttonUpgradeHP.Name = "buttonUpgradeHP";
            buttonUpgradeHP.Size = new Size(144, 40);
            buttonUpgradeHP.TabIndex = 3;
            buttonUpgradeHP.Text = "Nâng HP";
            buttonUpgradeHP.UseVisualStyleBackColor = false;
            buttonUpgradeHP.Click += buttonUpgradeHP_Click;
            // 
            // buttonUpgradeDamage
            // 
            buttonUpgradeDamage.BackColor = Color.Transparent;
            buttonUpgradeDamage.FlatStyle = FlatStyle.Flat;
            buttonUpgradeDamage.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonUpgradeDamage.ForeColor = Color.FromArgb(0, 192, 192);
            buttonUpgradeDamage.Location = new Point(129, 520);
            buttonUpgradeDamage.Name = "buttonUpgradeDamage";
            buttonUpgradeDamage.Size = new Size(139, 40);
            buttonUpgradeDamage.TabIndex = 4;
            buttonUpgradeDamage.Text = "Nâng Damage";
            buttonUpgradeDamage.UseVisualStyleBackColor = false;
            buttonUpgradeDamage.Click += buttonUpgradeDamage_Click;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = Color.Transparent;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonExit.ForeColor = Color.FromArgb(0, 192, 192);
            buttonExit.Location = new Point(212, 584);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(120, 40);
            buttonExit.TabIndex = 5;
            buttonExit.Text = "Thoát";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(0, 192, 192);
            label1.Location = new Point(196, 277);
            label1.Name = "label1";
            label1.Size = new Size(50, 23);
            label1.TabIndex = 6;
            label1.Text = "Vàng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(0, 192, 192);
            label2.Location = new Point(196, 317);
            label2.Name = "label2";
            label2.Size = new Size(29, 20);
            label2.TabIndex = 7;
            label2.Text = "HP";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(192, 255, 255);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            textBox1.ForeColor = Color.FromArgb(0, 192, 192);
            textBox1.Location = new Point(267, 317);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(94, 20);
            textBox1.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(0, 192, 192);
            label3.Location = new Point(196, 356);
            label3.Name = "label3";
            label3.Size = new Size(53, 20);
            label3.TabIndex = 9;
            label3.Text = "DAME";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(192, 255, 255);
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            textBox2.ForeColor = Color.FromArgb(0, 192, 192);
            textBox2.Location = new Point(267, 356);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(94, 20);
            textBox2.TabIndex = 10;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button1.ForeColor = Color.FromArgb(0, 192, 192);
            button1.Location = new Point(291, 419);
            button1.Name = "button1";
            button1.Size = new Size(144, 40);
            button1.TabIndex = 11;
            button1.Text = "Chơi với người";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(192, 255, 255);
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBox3.ForeColor = Color.FromArgb(0, 192, 192);
            textBox3.Location = new Point(267, 235);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(94, 23);
            textBox3.TabIndex = 15;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(0, 192, 192);
            label4.Location = new Point(196, 235);
            label4.Name = "label4";
            label4.Size = new Size(27, 23);
            label4.TabIndex = 16;
            label4.Text = "Ải";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(187, 77);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(208, 135);
            pictureBox2.TabIndex = 17;
            pictureBox2.TabStop = false;
            // 
            // labelWelcome
            // 
            labelWelcome.BackColor = Color.Transparent;
            labelWelcome.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 163);
            labelWelcome.ForeColor = Color.FromArgb(0, 192, 192);
            labelWelcome.Location = new Point(234, 23);
            labelWelcome.Name = "labelWelcome";
            labelWelcome.Size = new Size(201, 40);
            labelWelcome.TabIndex = 0;
            labelWelcome.Text = "Xin chào!";
            // 
            // Form3
            // 
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(552, 666);
            Controls.Add(pictureBox2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(labelWelcome);
            Controls.Add(textBoxGold);
            Controls.Add(buttonPlay);
            Controls.Add(buttonUpgradeHP);
            Controls.Add(buttonUpgradeDamage);
            Controls.Add(buttonExit);
            Controls.Add(textBox3);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(textBox2);
            Name = "Form3";
            Text = "Menu Game";
            Load += Form3_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private PictureBox pictureBox2;
        private Label labelWelcome;
    }
}

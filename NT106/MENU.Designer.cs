using System.Windows.Forms;
using Font = System.Drawing.Font;
using Image = System.Drawing.Image;
using System.Drawing;

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
        private TextBox textBox1;  // HP
        private Label label3;
        private TextBox textBox2;  // Damage
        private Button button1;    // Chơi với người
        private TextBox textBox3;  // Level
        private Label label4;
        private Label labelWelcome;
        private SaveFileDialog saveFileDialog1;
        private Button button2;    // Rank

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
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
            labelWelcome = new Label();
            saveFileDialog1 = new SaveFileDialog();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // textBoxGold
            // 
            textBoxGold.BackColor = Color.FromArgb(15, 22, 45);
            textBoxGold.BorderStyle = BorderStyle.FixedSingle;
            textBoxGold.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBoxGold.ForeColor = Color.FromArgb(0, 192, 192);
            textBoxGold.Location = new Point(230, 183);
            textBoxGold.Name = "textBoxGold";
            textBoxGold.ReadOnly = true;
            textBoxGold.Size = new Size(140, 30);
            textBoxGold.TabIndex = 1;
            textBoxGold.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonPlay
            // 
            buttonPlay.BackColor = Color.FromArgb(15, 25, 45);
            buttonPlay.FlatStyle = FlatStyle.Flat;
            buttonPlay.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonPlay.ForeColor = Color.FromArgb(0, 192, 192);
            buttonPlay.Location = new Point(110, 350);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(150, 45);
            buttonPlay.TabIndex = 2;
            buttonPlay.Text = "Chơi BOSS";
            buttonPlay.UseVisualStyleBackColor = false;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // buttonUpgradeHP
            // 
            buttonUpgradeHP.BackColor = Color.FromArgb(15, 25, 45);
            buttonUpgradeHP.FlatStyle = FlatStyle.Flat;
            buttonUpgradeHP.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonUpgradeHP.ForeColor = Color.FromArgb(0, 192, 192);
            buttonUpgradeHP.Location = new Point(290, 410);
            buttonUpgradeHP.Name = "buttonUpgradeHP";
            buttonUpgradeHP.Size = new Size(150, 45);
            buttonUpgradeHP.TabIndex = 3;
            buttonUpgradeHP.Text = "Nâng HP";
            buttonUpgradeHP.UseVisualStyleBackColor = false;
            buttonUpgradeHP.Click += buttonUpgradeHP_Click;
            // 
            // buttonUpgradeDamage
            // 
            buttonUpgradeDamage.BackColor = Color.FromArgb(15, 25, 45);
            buttonUpgradeDamage.FlatStyle = FlatStyle.Flat;
            buttonUpgradeDamage.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonUpgradeDamage.ForeColor = Color.FromArgb(0, 192, 192);
            buttonUpgradeDamage.Location = new Point(110, 410);
            buttonUpgradeDamage.Name = "buttonUpgradeDamage";
            buttonUpgradeDamage.Size = new Size(150, 45);
            buttonUpgradeDamage.TabIndex = 4;
            buttonUpgradeDamage.Text = "Nâng Damage";
            buttonUpgradeDamage.UseVisualStyleBackColor = false;
            buttonUpgradeDamage.Click += buttonUpgradeDamage_Click;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = Color.FromArgb(15, 25, 45);
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonExit.ForeColor = Color.FromArgb(0, 192, 192);
            buttonExit.Location = new Point(290, 470);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(150, 45);
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
            label1.Location = new Point(140, 185);
            label1.Name = "label1";
            label1.Size = new Size(50, 23);
            label1.TabIndex = 6;
            label1.Text = "Vàng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(0, 192, 192);
            label2.Location = new Point(140, 230);
            label2.Name = "label2";
            label2.Size = new Size(33, 23);
            label2.TabIndex = 7;
            label2.Text = "HP";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(15, 22, 45);
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBox1.ForeColor = Color.FromArgb(0, 192, 192);
            textBox1.Location = new Point(230, 228);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(140, 30);
            textBox1.TabIndex = 8;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(0, 192, 192);
            label3.Location = new Point(140, 275);
            label3.Name = "label3";
            label3.Size = new Size(84, 23);
            label3.TabIndex = 9;
            label3.Text = "DAMAGE";
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.FromArgb(15, 22, 45);
            textBox2.BorderStyle = BorderStyle.FixedSingle;
            textBox2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBox2.ForeColor = Color.FromArgb(0, 192, 192);
            textBox2.Location = new Point(230, 273);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(140, 30);
            textBox2.TabIndex = 10;
            textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(15, 25, 45);
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button1.ForeColor = Color.FromArgb(0, 192, 192);
            button1.Location = new Point(290, 350);
            button1.Name = "button1";
            button1.Size = new Size(150, 45);
            button1.TabIndex = 11;
            button1.Text = "Chơi với người";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.FromArgb(15, 22, 45);
            textBox3.BorderStyle = BorderStyle.FixedSingle;
            textBox3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBox3.ForeColor = Color.FromArgb(0, 192, 192);
            textBox3.Location = new Point(230, 138);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(140, 30);
            textBox3.TabIndex = 15;
            textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(0, 192, 192);
            label4.Location = new Point(140, 140);
            label4.Name = "label4";
            label4.Size = new Size(51, 23);
            label4.TabIndex = 16;
            label4.Text = "Level";
            // 
            // labelWelcome
            // 
            labelWelcome.BackColor = Color.Transparent;
            labelWelcome.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 163);
            labelWelcome.ForeColor = Color.FromArgb(0, 192, 192);
            labelWelcome.Location = new Point(50, 30);
            labelWelcome.Name = "labelWelcome";
            labelWelcome.Size = new Size(460, 45);
            labelWelcome.TabIndex = 0;
            labelWelcome.Text = "Xin chào";
            labelWelcome.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(15, 25, 45);
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            button2.ForeColor = Color.FromArgb(0, 192, 192);
            button2.Location = new Point(110, 470);
            button2.Name = "button2";
            button2.Size = new Size(150, 45);
            button2.TabIndex = 17;
            button2.Text = "Rank";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(15, 25, 45);
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            button3.ForeColor = Color.FromArgb(0, 192, 192);
            button3.Location = new Point(230, 559);
            button3.Name = "button3";
            button3.Size = new Size(94, 41);
            button3.TabIndex = 18;
            button3.Text = "Đổi Pass";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = Color.Transparent;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            button4.ForeColor = Color.FromArgb(0, 192, 192);
            button4.Location = new Point(485, 12);
            button4.Name = "button4";
            button4.Size = new Size(63, 29);
            button4.TabIndex = 19;
            button4.Text = "🎁";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(10, 15, 30);
            BackgroundImage = Properties.Resource.Gemini_Generated_Image_dy9x6hdy9x6hdy9x;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(560, 680);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(buttonExit);
            Controls.Add(buttonUpgradeHP);
            Controls.Add(buttonUpgradeDamage);
            Controls.Add(button1);
            Controls.Add(buttonPlay);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(textBoxGold);
            Controls.Add(label1);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(labelWelcome);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu Game";
            Load += Form3_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        private Button button3;
        private Button button4;
    }
}

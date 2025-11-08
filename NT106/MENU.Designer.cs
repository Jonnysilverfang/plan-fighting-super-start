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
            this.textBoxGold = new System.Windows.Forms.TextBox();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonUpgradeHP = new System.Windows.Forms.Button();
            this.buttonUpgradeDamage = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Menu (Form)
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(10, 15, 30);
            this.BackgroundImage = Properties.Resource.Gemini_Generated_Image_dy9x6hdy9x6hdy9x;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(560, 680);
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Menu Game";
            this.Load += new System.EventHandler(this.Form3_Load);
            // 
            // labelWelcome
            // 
            this.labelWelcome.BackColor = Color.Transparent;
            this.labelWelcome.Font = new Font("Segoe UI", 20F, FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.labelWelcome.ForeColor = Color.FromArgb(0, 192, 192);
            this.labelWelcome.Location = new System.Drawing.Point(50, 30);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(460, 45);
            this.labelWelcome.TabIndex = 0;
            this.labelWelcome.Text = "Xin chào";
            this.labelWelcome.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4 - Level
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = Color.Transparent;
            this.label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.label4.ForeColor = Color.FromArgb(0, 192, 192);
            this.label4.Location = new System.Drawing.Point(140, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 23);
            this.label4.TabIndex = 16;
            this.label4.Text = "Level";
            // 
            // textBox3 - Level value
            // 
            this.textBox3.BackColor = Color.FromArgb(15, 22, 45);
            this.textBox3.BorderStyle = BorderStyle.FixedSingle;
            this.textBox3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.textBox3.ForeColor = Color.FromArgb(0, 192, 192);
            this.textBox3.Location = new System.Drawing.Point(230, 138);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(140, 30);
            this.textBox3.TabIndex = 15;
            this.textBox3.TextAlign = HorizontalAlignment.Center;
            // 
            // label1 - Gold
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = Color.Transparent;
            this.label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.label1.ForeColor = Color.FromArgb(0, 192, 192);
            this.label1.Location = new System.Drawing.Point(140, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Vàng";
            // 
            // textBoxGold - Gold value
            // 
            this.textBoxGold.BackColor = Color.FromArgb(15, 22, 45);
            this.textBoxGold.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxGold.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.textBoxGold.ForeColor = Color.FromArgb(0, 192, 192);
            this.textBoxGold.Location = new System.Drawing.Point(230, 183);
            this.textBoxGold.Name = "textBoxGold";
            this.textBoxGold.ReadOnly = true;
            this.textBoxGold.Size = new System.Drawing.Size(140, 30);
            this.textBoxGold.TabIndex = 1;
            this.textBoxGold.TextAlign = HorizontalAlignment.Center;
            // 
            // label2 - HP
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = Color.Transparent;
            this.label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.label2.ForeColor = Color.FromArgb(0, 192, 192);
            this.label2.Location = new System.Drawing.Point(140, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "HP";
            // 
            // textBox1 - HP value
            // 
            this.textBox1.BackColor = Color.FromArgb(15, 22, 45);
            this.textBox1.BorderStyle = BorderStyle.FixedSingle;
            this.textBox1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.textBox1.ForeColor = Color.FromArgb(0, 192, 192);
            this.textBox1.Location = new System.Drawing.Point(230, 228);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(140, 30);
            this.textBox1.TabIndex = 8;
            this.textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // label3 - Damage
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = Color.Transparent;
            this.label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.label3.ForeColor = Color.FromArgb(0, 192, 192);
            this.label3.Location = new System.Drawing.Point(140, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 23);
            this.label3.TabIndex = 9;
            this.label3.Text = "DAMAGE";
            // 
            // textBox2 - Damage value
            // 
            this.textBox2.BackColor = Color.FromArgb(15, 22, 45);
            this.textBox2.BorderStyle = BorderStyle.FixedSingle;
            this.textBox2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.textBox2.ForeColor = Color.FromArgb(0, 192, 192);
            this.textBox2.Location = new System.Drawing.Point(230, 273);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(140, 30);
            this.textBox2.TabIndex = 10;
            this.textBox2.TextAlign = HorizontalAlignment.Center;
            // 
            // buttonPlay - Chơi BOSS
            // 
            this.buttonPlay.BackColor = Color.FromArgb(15, 25, 45);
            this.buttonPlay.FlatAppearance.BorderSize = 1;
            this.buttonPlay.FlatStyle = FlatStyle.Flat;
            this.buttonPlay.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.buttonPlay.ForeColor = Color.FromArgb(0, 192, 192);
            this.buttonPlay.Location = new System.Drawing.Point(110, 350);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(150, 45);
            this.buttonPlay.TabIndex = 2;
            this.buttonPlay.Text = "Chơi BOSS";
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // button1 - Chơi với người
            // 
            this.button1.BackColor = Color.FromArgb(15, 25, 45);
            this.button1.FlatAppearance.BorderSize = 1;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.button1.ForeColor = Color.FromArgb(0, 192, 192);
            this.button1.Location = new System.Drawing.Point(290, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 45);
            this.button1.TabIndex = 11;
            this.button1.Text = "Chơi với người";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonUpgradeDamage
            // 
            this.buttonUpgradeDamage.BackColor = Color.FromArgb(15, 25, 45);
            this.buttonUpgradeDamage.FlatAppearance.BorderSize = 1;
            this.buttonUpgradeDamage.FlatStyle = FlatStyle.Flat;
            this.buttonUpgradeDamage.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.buttonUpgradeDamage.ForeColor = Color.FromArgb(0, 192, 192);
            this.buttonUpgradeDamage.Location = new System.Drawing.Point(110, 410);
            this.buttonUpgradeDamage.Name = "buttonUpgradeDamage";
            this.buttonUpgradeDamage.Size = new System.Drawing.Size(150, 45);
            this.buttonUpgradeDamage.TabIndex = 4;
            this.buttonUpgradeDamage.Text = "Nâng Damage";
            this.buttonUpgradeDamage.UseVisualStyleBackColor = false;
            this.buttonUpgradeDamage.Click += new System.EventHandler(this.buttonUpgradeDamage_Click);
            // 
            // buttonUpgradeHP
            // 
            this.buttonUpgradeHP.BackColor = Color.FromArgb(15, 25, 45);
            this.buttonUpgradeHP.FlatAppearance.BorderSize = 1;
            this.buttonUpgradeHP.FlatStyle = FlatStyle.Flat;
            this.buttonUpgradeHP.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.buttonUpgradeHP.ForeColor = Color.FromArgb(0, 192, 192);
            this.buttonUpgradeHP.Location = new System.Drawing.Point(290, 410);
            this.buttonUpgradeHP.Name = "buttonUpgradeHP";
            this.buttonUpgradeHP.Size = new System.Drawing.Size(150, 45);
            this.buttonUpgradeHP.TabIndex = 3;
            this.buttonUpgradeHP.Text = "Nâng HP";
            this.buttonUpgradeHP.UseVisualStyleBackColor = false;
            this.buttonUpgradeHP.Click += new System.EventHandler(this.buttonUpgradeHP_Click);
            // 
            // button2 - Rank
            // 
            this.button2.BackColor = Color.FromArgb(15, 25, 45);
            this.button2.FlatAppearance.BorderSize = 1;
            this.button2.FlatStyle = FlatStyle.Flat;
            this.button2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.button2.ForeColor = Color.FromArgb(0, 192, 192);
            this.button2.Location = new System.Drawing.Point(110, 470);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 45);
            this.button2.TabIndex = 17;
            this.button2.Text = "Rank";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonExit - Thoát
            // 
            this.buttonExit.BackColor = Color.FromArgb(15, 25, 45);
            this.buttonExit.FlatAppearance.BorderSize = 1;
            this.buttonExit.FlatStyle = FlatStyle.Flat;
            this.buttonExit.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.buttonExit.ForeColor = Color.FromArgb(0, 192, 192);
            this.buttonExit.Location = new System.Drawing.Point(290, 470);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(150, 45);
            this.buttonExit.TabIndex = 5;
            this.buttonExit.Text = "Thoát";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // Add controls
            // 
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonUpgradeHP);
            this.Controls.Add(this.buttonUpgradeDamage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxGold);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelWelcome);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}

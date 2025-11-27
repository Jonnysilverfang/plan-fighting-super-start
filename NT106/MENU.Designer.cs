using System.Windows.Forms;
using Font = System.Drawing.Font;
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
        private Button button3;    // Đổi Pass
        private Button button4;    // 🎁
        private Button button5;    // code

        private PictureBox pictureBoxAvatar;
        private PictureBox pictureBoxPlane;
        private Button buttonDoiMayBay;

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
            button5 = new Button();
            pictureBoxAvatar = new PictureBox();
            pictureBoxPlane = new PictureBox();
            buttonDoiMayBay = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAvatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPlane).BeginInit();
            SuspendLayout();
            // 
            // Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(10, 15, 30);
            BackgroundImage = Properties.Resource.Gemini_Generated_Image_dy9x6hdy9x6hdy9x;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(684, 680);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu Game";
            Load += Form3_Load;
            // 
            // button4 (🎁)
            // 
            button4.BackColor = Color.Transparent;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            button4.ForeColor = Color.FromArgb(0, 192, 192);
            button4.Location = new Point(610, 10);
            button4.Size = new Size(60, 28);
            button4.Text = "🎁";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5 (code)
            // 
            button5.BackColor = Color.Transparent;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            button5.ForeColor = Color.FromArgb(0, 192, 192);
            button5.Location = new Point(12, 10);
            button5.Size = new Size(60, 28);
            button5.Text = "code";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // labelWelcome
            // 
            labelWelcome.Location = new Point(80, 8);
            labelWelcome.Size = new Size(520, 46);
            labelWelcome.Text = "Xin chào";
            // 
            // pictureBoxAvatar
            // 
            pictureBoxAvatar.BackColor = Color.FromArgb(15, 22, 45);
            pictureBoxAvatar.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxAvatar.Location = new Point(80, 70);
            pictureBoxAvatar.Size = new Size(110, 100);
            pictureBoxAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            // 
            // pictureBoxPlane
            // 
            pictureBoxPlane.BackColor = Color.FromArgb(15, 22, 45);
            pictureBoxPlane.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxPlane.Location = new Point(360, 70);
            pictureBoxPlane.Size = new Size(170, 100);
            pictureBoxPlane.SizeMode = PictureBoxSizeMode.Zoom;
            // 
            // buttonDoiMayBay
            // 
            buttonDoiMayBay.Location = new Point(300, 175);
            buttonDoiMayBay.Size = new Size(240, 40);
            buttonDoiMayBay.Text = "ĐỔI ẢNH MÁY BAY";
            buttonDoiMayBay.UseVisualStyleBackColor = true;
            buttonDoiMayBay.Click += buttonDoiMayBay_Click;
            // 
            // label4 (Level)
            // 
            label4.Location = new Point(120, 235);
            label4.Size = new Size(100, 28);
            label4.Text = "Level";
            // 
            // textBox3 (Level)
            // 
            textBox3.Location = new Point(255, 235);
            textBox3.Size = new Size(160, 30);
            // 
            // label1 (Vàng)
            // 
            label1.Location = new Point(120, 280);
            label1.Size = new Size(100, 28);
            label1.Text = "Vàng";
            // 
            // textBoxGold
            // 
            textBoxGold.Location = new Point(255, 280);
            textBoxGold.Size = new Size(160, 30);
            // 
            // label2 (HP)
            // 
            label2.Location = new Point(120, 325);
            label2.Size = new Size(100, 28);
            label2.Text = "HP";
            // 
            // textBox1 (HP)
            // 
            textBox1.Location = new Point(255, 325);
            textBox1.Size = new Size(160, 30);
            // 
            // label3 (Damage)
            // 
            label3.Location = new Point(120, 370);
            label3.Size = new Size(100, 28);
            label3.Text = "DAMAGE";
            // 
            // textBox2 (Damage)
            // 
            textBox2.Location = new Point(255, 370);
            textBox2.Size = new Size(160, 30);
            // 
            // buttonPlay
            // 
            buttonPlay.Location = new Point(110, 420);
            buttonPlay.Size = new Size(160, 46);
            buttonPlay.Text = "Chơi BOSS";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // button1 (Chơi với người)
            // 
            button1.Location = new Point(320, 420);
            button1.Size = new Size(180, 46);
            button1.Text = "Chơi với người";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // buttonUpgradeDamage
            // 
            buttonUpgradeDamage.Location = new Point(110, 480);
            buttonUpgradeDamage.Size = new Size(160, 46);
            buttonUpgradeDamage.Text = "Nâng Damage";
            buttonUpgradeDamage.UseVisualStyleBackColor = true;
            buttonUpgradeDamage.Click += buttonUpgradeDamage_Click;
            // 
            // buttonUpgradeHP
            // 
            buttonUpgradeHP.Location = new Point(320, 480);
            buttonUpgradeHP.Size = new Size(180, 46);
            buttonUpgradeHP.Text = "Nâng HP";
            buttonUpgradeHP.UseVisualStyleBackColor = true;
            buttonUpgradeHP.Click += buttonUpgradeHP_Click;
            // 
            // button2 (Rank)
            // 
            button2.Location = new Point(110, 540);
            button2.Size = new Size(160, 46);
            button2.Text = "Rank";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // buttonExit
            // 
            buttonExit.Location = new Point(320, 540);
            buttonExit.Size = new Size(180, 46);
            buttonExit.Text = "Thoát";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // button3 (Đổi pass)
            // 
            button3.Location = new Point(260, 600);
            button3.Size = new Size(120, 38);
            button3.Text = "Đổi Pass";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Menu (add controls)
            // 
            Controls.Add(buttonDoiMayBay);
            Controls.Add(pictureBoxPlane);
            Controls.Add(pictureBoxAvatar);
            Controls.Add(button5);
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

            ((System.ComponentModel.ISupportInitialize)pictureBoxAvatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPlane).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

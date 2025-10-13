namespace Kien
{
    partial class Form3
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelWelcome;
        private TextBox textBoxGold;
        private Button buttonPlay;
        private Button buttonUpgradeHP;
        private Button buttonUpgradeDamage;
        private Button buttonExit;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private TextBox textBox2;
        private Button button1;
        private TextBox textBox3; // Thêm textBox3 để hiển thị Level

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelWelcome = new Label();
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
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            SuspendLayout();
            // 
            // labelWelcome
            // 
            labelWelcome.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 163);
            labelWelcome.ForeColor = Color.FromArgb(0, 192, 192);
            labelWelcome.Location = new Point(160, 9);
            labelWelcome.Name = "labelWelcome";
            labelWelcome.Size = new Size(201, 40);
            labelWelcome.TabIndex = 0;
            labelWelcome.Text = "Xin chào!";
            // 
            // textBoxGold
            // 
            textBoxGold.BackColor = Color.White;
            textBoxGold.BorderStyle = BorderStyle.None;
            textBoxGold.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBoxGold.ForeColor = Color.FromArgb(0, 192, 192);
            textBoxGold.Location = new Point(231, 161);
            textBoxGold.Name = "textBoxGold";
            textBoxGold.ReadOnly = true;
            textBoxGold.Size = new Size(65, 23);
            textBoxGold.TabIndex = 1;
            textBoxGold.TextChanged += textBoxGold_TextChanged;
            // 
            // buttonPlay
            // 
            buttonPlay.FlatStyle = FlatStyle.Flat;
            buttonPlay.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonPlay.ForeColor = Color.FromArgb(0, 192, 192);
            buttonPlay.Location = new Point(93, 303);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(139, 40);
            buttonPlay.TabIndex = 2;
            buttonPlay.Text = "Chơi BOSS";
            buttonPlay.Click += buttonPlay_Click;
            // 
            // buttonUpgradeHP
            // 
            buttonUpgradeHP.FlatStyle = FlatStyle.Flat;
            buttonUpgradeHP.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonUpgradeHP.ForeColor = Color.FromArgb(0, 192, 192);
            buttonUpgradeHP.Location = new Point(255, 404);
            buttonUpgradeHP.Name = "buttonUpgradeHP";
            buttonUpgradeHP.Size = new Size(144, 40);
            buttonUpgradeHP.TabIndex = 3;
            buttonUpgradeHP.Text = "Nâng HP";
            buttonUpgradeHP.Click += buttonUpgradeHP_Click;
            // 
            // buttonUpgradeDamage
            // 
            buttonUpgradeDamage.FlatStyle = FlatStyle.Flat;
            buttonUpgradeDamage.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonUpgradeDamage.ForeColor = Color.FromArgb(0, 192, 192);
            buttonUpgradeDamage.Location = new Point(93, 404);
            buttonUpgradeDamage.Name = "buttonUpgradeDamage";
            buttonUpgradeDamage.Size = new Size(139, 40);
            buttonUpgradeDamage.TabIndex = 4;
            buttonUpgradeDamage.Text = "Nâng Damage";
            buttonUpgradeDamage.Click += buttonUpgradeDamage_Click;
            // 
            // buttonExit
            // 
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            buttonExit.ForeColor = Color.FromArgb(0, 192, 192);
            buttonExit.Location = new Point(176, 468);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(120, 40);
            buttonExit.TabIndex = 5;
            buttonExit.Text = "Thoát";
            buttonExit.Click += buttonExit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(0, 192, 192);
            label1.Location = new Point(160, 161);
            label1.Name = "label1";
            label1.Size = new Size(50, 23);
            label1.TabIndex = 6;
            label1.Text = "Vàng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(0, 192, 192);
            label2.Location = new Point(160, 201);
            label2.Name = "label2";
            label2.Size = new Size(29, 20);
            label2.TabIndex = 7;
            label2.Text = "HP";
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            textBox1.ForeColor = Color.FromArgb(0, 192, 192);
            textBox1.Location = new Point(231, 201);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(65, 20);
            textBox1.TabIndex = 8;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(0, 192, 192);
            label3.Location = new Point(160, 240);
            label3.Name = "label3";
            label3.Size = new Size(53, 20);
            label3.TabIndex = 9;
            label3.Text = "DAME";
            // 
            // textBox2
            // 
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            textBox2.ForeColor = Color.FromArgb(0, 192, 192);
            textBox2.Location = new Point(231, 240);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(65, 20);
            textBox2.TabIndex = 10;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            button1.ForeColor = Color.FromArgb(0, 192, 192);
            button1.Location = new Point(255, 303);
            button1.Name = "button1";
            button1.Size = new Size(144, 40);
            button1.TabIndex = 11;
            button1.Text = "Chơi với người";
            button1.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.White;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            textBox3.ForeColor = Color.FromArgb(0, 192, 192);
            textBox3.Location = new Point(231, 119);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(65, 23);
            textBox3.TabIndex = 15;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label4.ForeColor = Color.FromArgb(0, 192, 192);
            label4.Location = new Point(160, 119);
            label4.Name = "label4";
            label4.Size = new Size(27, 23);
            label4.TabIndex = 16;
            label4.Text = "Ải";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label5.ForeColor = Color.FromArgb(0, 192, 192);
            label5.Location = new Point(292, 240);
            label5.Name = "label5";
            label5.Size = new Size(34, 23);
            label5.TabIndex = 17;
            label5.Text = "⚔";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(0, 192, 192);
            label6.Location = new Point(292, 201);
            label6.Name = "label6";
            label6.Size = new Size(34, 23);
            label6.TabIndex = 18;
            label6.Text = "\U0001fa78";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(0, 192, 192);
            label7.Location = new Point(292, 161);
            label7.Name = "label7";
            label7.Size = new Size(34, 23);
            label7.TabIndex = 19;
            label7.Text = "💰";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label8.ForeColor = Color.FromArgb(0, 192, 192);
            label8.Location = new Point(292, 119);
            label8.Name = "label8";
            label8.Size = new Size(34, 23);
            label8.TabIndex = 20;
            label8.Text = "🏆";
            // 
            // Form3
            // 
            BackColor = Color.White;
            ClientSize = new Size(490, 541);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(labelWelcome);
            Controls.Add(textBoxGold);
            Controls.Add(buttonPlay);
            Controls.Add(buttonUpgradeHP);
            Controls.Add(buttonUpgradeDamage);
            Controls.Add(buttonExit);
            Controls.Add(textBox3);
            Name = "Form3";
            Text = "Menu Game";
            Load += Form3_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
    }
}

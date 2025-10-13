namespace Kien
{
    partial class Form4
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.PictureBox boss;
        private System.Windows.Forms.PictureBox playerBullet;
        private System.Windows.Forms.Label txtScore;
        private System.Windows.Forms.ProgressBar playerHealthBar;
        private System.Windows.Forms.ProgressBar bossHealthBar;
        private System.Windows.Forms.Button buttonExit;

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer survivalTimer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            player = new PictureBox();
            boss = new PictureBox();
            playerBullet = new PictureBox();
            txtScore = new Label();
            playerHealthBar = new ProgressBar();
            bossHealthBar = new ProgressBar();
            buttonExit = new Button();
            gameTimer = new System.Windows.Forms.Timer(components);
            survivalTimer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            ((System.ComponentModel.ISupportInitialize)boss).BeginInit();
            ((System.ComponentModel.ISupportInitialize)playerBullet).BeginInit();
            SuspendLayout();
            // 
            // player
            // 
            player.BackColor = Color.Transparent;
            player.Image = (Image)resources.GetObject("player.Image");
            player.Location = new Point(350, 520);
            player.Name = "player";
            player.Size = new Size(62, 80);
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            player.TabIndex = 0;
            player.TabStop = false;
            // 
            // boss
            // 
            boss.BackColor = Color.Transparent;
            boss.BackgroundImage = (Image)resources.GetObject("boss.BackgroundImage");
            boss.BackgroundImageLayout = ImageLayout.Stretch;
            boss.Location = new Point(200, 50);
            boss.Name = "boss";
            boss.Size = new Size(120, 100);
            boss.SizeMode = PictureBoxSizeMode.StretchImage;
            boss.TabIndex = 1;
            boss.TabStop = false;
            // 
            // playerBullet
            // 
            playerBullet.BackColor = Color.Yellow;
            playerBullet.Location = new Point(-50, -50);
            playerBullet.Name = "playerBullet";
            playerBullet.Size = new Size(7, 20);
            playerBullet.TabIndex = 2;
            playerBullet.TabStop = false;
            playerBullet.Visible = false;
            // 
            // txtScore
            // 
            txtScore.Font = new Font("Microsoft Sans Serif", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            txtScore.ForeColor = Color.FromArgb(0, 192, 192);
            txtScore.Location = new Point(0, 9);
            txtScore.Name = "txtScore";
            txtScore.Size = new Size(473, 30);
            txtScore.TabIndex = 3;
            txtScore.Text = "Gold: 0  Time: 90";
            txtScore.TextAlign = ContentAlignment.MiddleCenter;
            txtScore.Click += txtScore_Click;
            // 
            // playerHealthBar
            // 
            playerHealthBar.ForeColor = Color.Lime;
            playerHealthBar.Location = new Point(0, 597);
            playerHealthBar.Name = "playerHealthBar";
            playerHealthBar.Size = new Size(300, 20);
            playerHealthBar.TabIndex = 4;
            // 
            // bossHealthBar
            // 
            bossHealthBar.ForeColor = Color.Red;
            bossHealthBar.Location = new Point(488, 12);
            bossHealthBar.Name = "bossHealthBar";
            bossHealthBar.Size = new Size(300, 20);
            bossHealthBar.TabIndex = 5;
            // 
            // buttonExit
            // 
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            buttonExit.ForeColor = Color.FromArgb(0, 192, 192);
            buttonExit.Location = new Point(350, 300);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(100, 40);
            buttonExit.TabIndex = 6;
            buttonExit.Text = "Thoát";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Visible = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // gameTimer
            // 
            gameTimer.Interval = 20;
            gameTimer.Tick += mainGameTimerEvent;
            // 
            // survivalTimer
            // 
            survivalTimer.Interval = 1000;
            survivalTimer.Tick += survivalTimer_Tick;
            // 
            // Form4
            // 
            BackColor = Color.White;
            ClientSize = new Size(800, 620);
            Controls.Add(buttonExit);
            Controls.Add(bossHealthBar);
            Controls.Add(playerHealthBar);
            Controls.Add(txtScore);
            Controls.Add(playerBullet);
            Controls.Add(boss);
            Controls.Add(player);
            KeyPreview = true;
            Name = "Form4";
            Text = "Boss Shooter Game";
            Load += Form4_Load;
            KeyDown += keyisdown;
            KeyUp += keyisup;
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            ((System.ComponentModel.ISupportInitialize)boss).EndInit();
            ((System.ComponentModel.ISupportInitialize)playerBullet).EndInit();
            ResumeLayout(false);
        }
    }
}

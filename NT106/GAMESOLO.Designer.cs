namespace plan_fighting_super_start
{
    partial class GAMEBOSS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Khai báo các Controls
        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.PictureBox boss;
        private System.Windows.Forms.ProgressBar playerHealthBar;
        private System.Windows.Forms.ProgressBar bossHealthBar;
        private System.Windows.Forms.Label txtScore;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer survivalTimer;
        private System.Windows.Forms.Button buttonExit;

        // Khai báo PictureBox ẩn chứa ảnh mẫu đạn
        private System.Windows.Forms.PictureBox playerBullet;
        private System.Windows.Forms.PictureBox bossBullet;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            // Khai báo resources manager
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GAMEBOSS));

            // Khởi tạo các Controls
            this.player = new System.Windows.Forms.PictureBox();
            this.boss = new System.Windows.Forms.PictureBox();
            this.playerHealthBar = new System.Windows.Forms.ProgressBar();
            this.bossHealthBar = new System.Windows.Forms.ProgressBar();
            this.txtScore = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.survivalTimer = new System.Windows.Forms.Timer(this.components);
            this.buttonExit = new System.Windows.Forms.Button();

            // Khởi tạo các PictureBox ẩn chứa ảnh mẫu đạn
            this.playerBullet = new System.Windows.Forms.PictureBox();
            this.bossBullet = new System.Windows.Forms.PictureBox();


            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boss)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBullet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bossBullet)).BeginInit();
            this.SuspendLayout();

            // 
            // player
            // 
            this.player.BackColor = System.Drawing.Color.Transparent;
            // Dòng này sử dụng tài nguyên (resource) từ tệp .resx
            this.player.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("player")));
            this.player.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.player.Location = new System.Drawing.Point(400, 500);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(60, 60);
            this.player.TabStop = false;
            this.player.Tag = "player";
            // 
            // boss
            // 
            this.boss.BackColor = System.Drawing.Color.Transparent;
            // Dòng này sử dụng tài nguyên (resource) từ tệp .resx
            this.boss.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("boss_ship"))); // <--- SỬ DỤNG TÊN ẢNH BOSS CỦA BẠN
            this.boss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.boss.Location = new System.Drawing.Point(350, 50);
            this.boss.Name = "boss";
            this.boss.Size = new System.Drawing.Size(120, 120);
            this.boss.TabStop = false;
            this.boss.Tag = "boss";
            // 
            // playerHealthBar
            // 
            this.playerHealthBar.Location = new System.Drawing.Point(12, 660);
            this.playerHealthBar.Name = "playerHealthBar";
            this.playerHealthBar.Size = new System.Drawing.Size(300, 23);
            this.playerHealthBar.TabIndex = 0;
            // 
            // bossHealthBar
            // 
            this.bossHealthBar.Location = new System.Drawing.Point(588, 12);
            this.bossHealthBar.Name = "bossHealthBar";
            this.bossHealthBar.Size = new System.Drawing.Size(300, 23);
            this.bossHealthBar.TabIndex = 1;
            // 
            // txtScore
            // 
            this.txtScore.AutoSize = true;
            this.txtScore.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScore.ForeColor = System.Drawing.Color.White;
            this.txtScore.Location = new System.Drawing.Point(12, 12);
            this.txtScore.Name = "txtScore";
            this.txtScore.Size = new System.Drawing.Size(56, 19);
            this.txtScore.TabIndex = 2;
            this.txtScore.Text = "Score";
            this.txtScore.Click += new System.EventHandler(this.txtScore_Click);

            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.mainGameTimerEvent);

            // 
            // survivalTimer
            // 
            this.survivalTimer.Interval = 1000;
            this.survivalTimer.Tick += new System.EventHandler(this.survivalTimer_Tick);

            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(400, 350);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(100, 40);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Exit to Menu";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Visible = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);

            // 
            // playerBullet (PictureBox ẩn chứa ảnh mẫu đạn)
            // 
            this.playerBullet.BackColor = System.Drawing.Color.Transparent;
            this.playerBullet.Location = new System.Drawing.Point(10, 10);
            this.playerBullet.Name = "playerBullet";
            this.playerBullet.Size = new System.Drawing.Size(12, 30);
            this.playerBullet.Visible = false;
            this.playerBullet.Tag = "playerBulletSample";

            // 
            // bossBullet (PictureBox ẩn chứa ảnh mẫu đạn)
            // 
            this.bossBullet.BackColor = System.Drawing.Color.Transparent;
            this.bossBullet.Location = new System.Drawing.Point(30, 10);
            this.bossBullet.Name = "bossBullet";
            this.bossBullet.Size = new System.Drawing.Size(24, 24);
            this.bossBullet.Visible = false;
            this.bossBullet.Tag = "bossBulletSample";


            // 
            // GAMEBOSS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.BackColor = System.Drawing.Color.Black;
            this.KeyPreview = true;
            this.Load += new System.EventHandler(this.Form4_Load); // Load Event
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyisdown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyisup);

            this.Controls.Add(this.player);
            this.Controls.Add(this.boss);
            this.Controls.Add(this.playerHealthBar);
            this.Controls.Add(this.bossHealthBar);
            this.Controls.Add(this.txtScore);
            this.Controls.Add(this.buttonExit);

            // Thêm các PictureBox ẩn
            this.Controls.Add(this.playerBullet);
            this.Controls.Add(this.bossBullet);

            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boss)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerBullet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bossBullet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}

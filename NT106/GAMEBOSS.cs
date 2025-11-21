using plan_fighting_super_start.Properties;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class GAMEBOSS : Form
    {
        // ====== THÔNG TIN ĐẠN BOSS (để không phải parse string mỗi frame) ======
        private sealed class BossBulletInfo
        {
            public int DirectionX { get; set; }
            public int Speed { get; set; }
        }

        // Logic variables
        private bool goLeft, goRight, shooting;
        private int playerSpeed = 8;
        private int bulletSpeed = 20;
        private int bossSpeed = 5;
        private int bossAttackTimer = 0;
        private int survivalTime = 90;

        private Random rnd = new Random();
        private int frameCounter = 0;

        private const int BASE_DAMAGE = 10;
        private int playerDamage;

        // bắn ít lại để giảm lag (có thể chỉnh lại tuỳ thích)
        private int bossAttackFrequency = 80;
        private int maxBossBullets = 40;

        // Trạng thái pause & end
        private bool isPaused = false;
        private bool gameEnded = false;

        // Load ảnh máy bay từ S3
        private readonly S3ImageService _imageService = new S3ImageService();

        public GAMEBOSS()
        {
            InitializeComponent();
            // cho form vẽ double-buffer để mượt hơn
            this.DoubleBuffered = true;
        }

        // ===== LOAD SKIN MÁY BAY =====
        private async Task LoadPlaneSkinAsync()
        {
            try
            {
                // Đường dẫn máy bay mặc định (file nằm cạnh .exe)
                string defaultPlanePath = Path.Combine(Application.StartupPath, "MayBay.png");

                // Nếu chưa chọn skin → dùng máy bay mặc định
                if (string.IsNullOrEmpty(AccountData.PlaneSkin))
                {
                    if (File.Exists(defaultPlanePath))
                    {
                        player.Image = Image.FromFile(defaultPlanePath);
                        player.SizeMode = PictureBoxSizeMode.StretchImage;
                        player.BackColor = Color.Transparent;
                    }
                    return;
                }

                // Đã chọn skin trên S3
                var img = await _imageService.GetImageAsync(AccountData.PlaneSkin);
                if (img != null)
                {
                    player.Image = img;
                    player.SizeMode = PictureBoxSizeMode.StretchImage;
                    player.BackColor = Color.Transparent;
                }
                else
                {
                    // fallback → quay lại máy bay mặc định
                    if (File.Exists(defaultPlanePath))
                    {
                        player.Image = Image.FromFile(defaultPlanePath);
                        player.SizeMode = PictureBoxSizeMode.StretchImage;
                        player.BackColor = Color.Transparent;
                    }
                }
            }
            catch
            {
                string defaultPlanePath = Path.Combine(Application.StartupPath, "MayBay.png");
                if (File.Exists(defaultPlanePath))
                {
                    player.Image = Image.FromFile(defaultPlanePath);
                    player.SizeMode = PictureBoxSizeMode.StretchImage;
                    player.BackColor = Color.Transparent;
                }
            }
        }

        // ===== FORM LOAD =====
        private async void Form4_Load(object sender, EventArgs e)
        {
            // nền em đang set trong Designer rồi, không đụng vào nữa

            // ⚠️ Giữ lại PlaneSkin đang có (do Menu vừa đổi)
            string currentPlaneSkin = AccountData.PlaneSkin;

            if (!string.IsNullOrEmpty(AccountData.Username))
            {
                // Load lại dữ liệu từ server (Gold, Level, HP, Damage,...)
                Database.LoadAccountData(AccountData.Username);
            }

            // Sau khi load xong, backend chưa có PlaneSkin
            // → nếu server không trả về thì gán lại giá trị cũ
            if (!string.IsNullOrEmpty(currentPlaneSkin))
            {
                AccountData.PlaneSkin = currentPlaneSkin;
            }

            playerDamage = BASE_DAMAGE + AccountData.UpgradeDamage;

            playerHealthBar.Maximum = AccountData.UpgradeHP;
            playerHealthBar.Value = playerHealthBar.Maximum;
            playerHealthBar.ForeColor = Color.Lime;

            int currentBossMaxHealth = GetBossMaxHealth();
            bossHealthBar.Maximum = currentBossMaxHealth;
            bossHealthBar.Value = currentBossMaxHealth;
            bossHealthBar.ForeColor = Color.Red;

            survivalTime = 90;
            txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level}";

            // 🔹 load skin máy bay trước khi start game
            await LoadPlaneSkinAsync();

            gameTimer.Start();
            survivalTimer.Start();
        }

        // Form đã hiển thị xong -> clear focus
        private void GAMEBOSS_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            this.Focus();
        }

        // Hàm tính máu boss theo level
        private int GetBossMaxHealth()
        {
            int level = Math.Max(1, AccountData.Level);

            double baseHp = 10000; // Máu level 1
            double growth = 1.3;   // Mỗi level +30%

            double hp = baseHp * Math.Pow(growth, level - 1);
            return (int)hp;
        }

        // ====== VÒNG LẶP GAME CHÍNH – ĐÃ BỎ TRAIL ĐỂ GIẢM LAG ======
        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            if (isPaused) return;

            frameCounter++;
            txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level}";

            // Player movement
            if (goLeft && player.Left > 0) player.Left -= playerSpeed;
            if (goRight && player.Right < this.ClientSize.Width) player.Left += playerSpeed;

            // Boss movement
            boss.Left += bossSpeed;
            if (boss.Left < 0 || boss.Right > this.ClientSize.Width)
                bossSpeed = -bossSpeed;

            // Boss shooting
            bossAttackTimer++;
            if (bossAttackTimer > bossAttackFrequency)
            {
                bossAttackTimer = 0;
                ShootBossBulletFan();
            }

            int currentBossBullets = 0;
            var toRemove = new System.Collections.Generic.List<Control>();

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is not PictureBox pb) continue;

                // ===== Player bullet =====
                if (pb.Tag is string tag && tag == "playerBullet")
                {
                    pb.Top -= bulletSpeed;

                    if (pb.Top < -pb.Height)
                    {
                        toRemove.Add(pb);
                        continue;
                    }

                    if (pb.Bounds.IntersectsWith(boss.Bounds))
                    {
                        bossHealthBar.Value = Math.Max(0, bossHealthBar.Value - playerDamage);
                        CreateExplosion(pb.Left, pb.Top, Color.Aqua);

                        toRemove.Add(pb);

                        if (bossHealthBar.Value == 0)
                        {
                            // xoá đạn còn lại rồi thoát
                            foreach (var c in toRemove)
                            {
                                this.Controls.Remove(c);
                                c.Dispose();
                            }
                            EndGame(true);
                            return;
                        }
                    }
                }
                // ===== Boss bullet =====
                else if (pb.Tag is BossBulletInfo info)
                {
                    currentBossBullets++;

                    pb.Top += info.Speed;
                    pb.Left += info.DirectionX * (info.Speed / 2);

                    if (pb.Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealthBar.Value = Math.Max(0, playerHealthBar.Value - 10);
                        if (playerHealthBar.Value < playerHealthBar.Maximum / 2) playerHealthBar.ForeColor = Color.Yellow;
                        if (playerHealthBar.Value < playerHealthBar.Maximum / 4) playerHealthBar.ForeColor = Color.Red;

                        CreateExplosion(pb.Left, pb.Top, Color.OrangeRed);

                        toRemove.Add(pb);

                        if (playerHealthBar.Value == 0)
                        {
                            foreach (var c in toRemove)
                            {
                                this.Controls.Remove(c);
                                c.Dispose();
                            }
                            EndGame(false);
                            return;
                        }
                    }
                    else if (pb.Top > this.ClientSize.Height + pb.Height ||
                             pb.Left < -pb.Width ||
                             pb.Right > this.ClientSize.Width + pb.Width)
                    {
                        toRemove.Add(pb);
                    }
                }
                // ===== Explosion =====
                else if (pb.Tag is string tag2 && tag2 == "explosion")
                {
                    pb.Width += 4;
                    pb.Height += 4;
                    pb.Left -= 2;
                    pb.Top -= 2;
                    pb.BackColor = Color.FromArgb(
                        Math.Max(0, pb.BackColor.A - 20),
                        pb.BackColor.R, pb.BackColor.G, pb.BackColor.B);

                    if (pb.BackColor.A <= 20)
                    {
                        toRemove.Add(pb);
                    }
                }
            }

            foreach (var c in toRemove)
            {
                this.Controls.Remove(c);
                c.Dispose();
            }

            // nếu đạn boss trên màn hình quá nhiều thì giảm tần suất bắn
            bossAttackFrequency = currentBossBullets > maxBossBullets ? 200 : 80;
        }

        // Hiệu ứng nổ
        private void CreateExplosion(int x, int y, Color color)
        {
            PictureBox boom = new PictureBox();
            boom.Size = new Size(16, 16);
            boom.Tag = "explosion";
            boom.BackColor = Color.FromArgb(220, color.R, color.G, color.B);
            boom.Left = x - boom.Width / 2;
            boom.Top = y - boom.Height / 2;
            boom.BringToFront();
            this.Controls.Add(boom);
        }

        // Đạn boss kiểu tia vàng dài, bắn tỏa quạt (đã giảm số đạn)
        private void ShootBossBulletFan()
        {
            int[] spreadDirections = { -2, -1, 0, 1, 2 };   // 5 viên 1 lần
            int baseSpeed = 10;

            foreach (int directionX in spreadDirections)
            {
                PictureBox bullet = new PictureBox();
                bullet.Size = new Size(10, 40);
                bullet.BackColor = Color.Transparent;

                Bitmap bmp = new Bitmap(bullet.Width, bullet.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.Clear(Color.Transparent);

                    float centerX = bullet.Width / 2f;

                    // Glow vàng
                    Rectangle glowRect = new Rectangle(0, 4, bullet.Width, bullet.Height - 4);
                    using (var glowBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        new Point(glowRect.X, glowRect.Y),
                        new Point(glowRect.X, glowRect.Bottom),
                        Color.FromArgb(0, 255, 255, 0),
                        Color.FromArgb(220, 255, 210, 60)))
                    {
                        g.FillEllipse(glowBrush, glowRect);
                    }

                    // Lõi đạn
                    Rectangle coreRect = new Rectangle(
                        (int)(centerX - 2),
                        6,
                        4,
                        bullet.Height - 16
                    );
                    using (var coreBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 220)))
                    {
                        g.FillRectangle(coreBrush, coreRect);
                    }

                    // Đầu nhọn phía dưới
                    PointF p1 = new PointF(centerX, bullet.Height);
                    PointF p2 = new PointF(coreRect.Left - 3, coreRect.Bottom - 2);
                    PointF p3 = new PointF(coreRect.Right + 3, coreRect.Bottom - 2);
                    PointF[] tip = { p1, p2, p3 };
                    using (var tipBrush = new SolidBrush(Color.FromArgb(255, 255, 230, 140)))
                    {
                        g.FillPolygon(tipBrush, tip);
                    }
                }

                bullet.Image = bmp;
                bullet.SizeMode = PictureBoxSizeMode.Normal;

                bullet.Left = boss.Left + boss.Width / 2 - bullet.Width / 2;
                bullet.Top = boss.Bottom - 5;

                int moveSpeed = baseSpeed + rnd.Next(-1, 2);
                bullet.Tag = new BossBulletInfo
                {
                    DirectionX = directionX,
                    Speed = moveSpeed
                };

                this.Controls.Add(bullet);
                bullet.BringToFront();
            }
        }

        // Đạn Player dạng tên lửa xanh (giữ nguyên)
        private void ShootPlayerBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.Size = new Size(20, 60);
            bullet.Tag = "playerBullet";
            bullet.BackColor = Color.Transparent;

            Bitmap bmp = new Bitmap(bullet.Width, bullet.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                float centerX = bullet.Width / 2f;

                // Thân tên lửa
                int bodyWidth = 8;
                int bodyHeight = 26;
                int bodyX = (int)(centerX - bodyWidth / 2f);
                int bodyY = 8;
                Rectangle bodyRect = new Rectangle(bodyX, bodyY, bodyWidth, bodyHeight);

                using (var bodyBrush = new SolidBrush(Color.White))
                {
                    g.FillRectangle(bodyBrush, bodyRect);
                }
                using (var bodyPen = new Pen(Color.FromArgb(200, 180, 180, 180), 1f))
                {
                    g.DrawRectangle(bodyPen, bodyRect);
                }

                // Đầu nhọn màu đỏ
                PointF tip = new PointF(centerX, 0);
                PointF leftBase = new PointF(bodyX, bodyY);
                PointF rightBase = new PointF(bodyX + bodyWidth, bodyY);
                PointF[] nose = { tip, leftBase, rightBase };
                using (var noseBrush = new SolidBrush(Color.OrangeRed))
                {
                    g.FillPolygon(noseBrush, nose);
                }

                // Cửa sổ xanh
                Rectangle windowRect = new Rectangle(bodyX + 1, bodyY + 6, bodyWidth - 2, bodyWidth - 4);
                using (var windowBrush = new SolidBrush(Color.FromArgb(220, 80, 160, 255)))
                {
                    g.FillEllipse(windowBrush, windowRect);
                }

                // Vây ngang 2 bên
                using (var finBrush = new SolidBrush(Color.FromArgb(200, 0, 180, 255)))
                {
                    // trái
                    PointF[] leftFin =
                    {
                        new PointF(bodyX, bodyY + bodyHeight - 4),
                        new PointF(bodyX - 5, bodyY + bodyHeight + 4),
                        new PointF(bodyX, bodyY + bodyHeight + 2),
                    };
                    g.FillPolygon(finBrush, leftFin);

                    // phải
                    PointF[] rightFin =
                    {
                        new PointF(bodyX + bodyWidth, bodyY + bodyHeight - 4),
                        new PointF(bodyX + bodyWidth + 5, bodyY + bodyHeight + 4),
                        new PointF(bodyX + bodyWidth, bodyY + bodyHeight + 2),
                    };
                    g.FillPolygon(finBrush, rightFin);
                }

                // Vệt lửa xanh
                int flameHeight = 22;
                Rectangle flameRect = new Rectangle(bodyX + 1, bodyY + bodyHeight, bodyWidth - 2, flameHeight);

                using (var flameBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Point(flameRect.X, flameRect.Y),
                    new Point(flameRect.X, flameRect.Bottom),
                    Color.FromArgb(230, 0, 255, 255),
                    Color.FromArgb(0, 0, 255, 255)))
                {
                    g.FillRectangle(flameBrush, flameRect);
                }
            }

            bullet.Image = bmp;
            bullet.SizeMode = PictureBoxSizeMode.Normal;

            bullet.Left = player.Left + player.Width / 2 - bullet.Width / 2;
            bullet.Top = player.Top - bullet.Height;

            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void survivalTimer_Tick(object sender, EventArgs e)
        {
            if (isPaused) return;

            survivalTime--;
            if (survivalTime <= 0)
            {
                EndGame(true);
            }
        }

        private void EndGame(bool win)
        {
            if (gameEnded) return;
            gameEnded = true;

            gameTimer.Stop();
            survivalTimer.Stop();
            isPaused = false;

            pausePanel.Visible = false;

            // Xoá bullet/explosion còn lại
            var toRemove = new System.Collections.Generic.List<Control>();
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox pb &&
                    (pb.Tag is string tag &&
                        (tag == "playerBullet" || tag == "explosion") ||
                     pb.Tag is BossBulletInfo))
                {
                    toRemove.Add(pb);
                }
            }
            foreach (var x in toRemove)
            {
                this.Controls.Remove(x);
                x.Dispose();
            }

            if (win)
            {
                AccountData.Gold += 200;
                AccountData.Level++;
                Database.UpdateAccountData();
                txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level} - WIN!";
            }
            else
            {
                AccountData.Gold += 50;
                Database.UpdateAccountData();
                txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level} - GAME OVER!";
            }

            buttonExit.Text = "Thoát về menu";
            buttonExit.Visible = true;
        }

        private void PauseGame()
        {
            if (isPaused || gameEnded) return;

            isPaused = true;
            gameTimer.Stop();
            survivalTimer.Stop();

            pausePanel.Visible = true;
            pauseTextLabel.Text = "⏸ TẠM DỪNG";
        }

        private void ResumeGame()
        {
            if (!isPaused || gameEnded) return;

            isPaused = false;
            gameTimer.Start();
            survivalTimer.Start();

            pausePanel.Visible = false;
        }

        private void btnResume_Click(object sender, EventArgs e) => ResumeGame();

        private void btnPauseExit_Click(object sender, EventArgs e)
        {
            if (gameEnded) return;

            var result = MessageBox.Show(
                "Bạn có chắc muốn thoát trận và quay về Menu?\nBạn sẽ không nhận thêm vàng cho trận này.",
                "Thoát trận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try { Database.UpdateAccountData(); } catch { }
            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            if (!gameEnded)
            {
                try { Database.UpdateAccountData(); } catch { }
            }
            this.Close();
        }

        private void PlayHitSound() { }
        private void PlayLoseSound() { }
        private void PlayWinSound() { }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = true;
            if (e.KeyCode == Keys.Right) goRight = true;

            if (e.KeyCode == Keys.Space && !shooting && !isPaused && !gameEnded)
            {
                shooting = true;
                ShootPlayerBullet();
            }

            // Nhấn P để pause / resume
            if (e.KeyCode == Keys.P)
            {
                if (!isPaused) PauseGame();
                else ResumeGame();
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = false;
            if (e.KeyCode == Keys.Right) goRight = false;
            if (e.KeyCode == Keys.Space) shooting = false;
        }

        private void txtScore_Click(object sender, EventArgs e) { }

        private void GAMEBOSS_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Không cần stop gì nữa
        }

        private void boss_Click(object sender, EventArgs e) { }
    }
}

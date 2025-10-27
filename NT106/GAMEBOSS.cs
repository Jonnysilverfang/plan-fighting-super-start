using plan_fighting_super_start.Properties; // Cần thiết để truy cập ảnh đạn từ Resources
using System;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    // Sử dụng partial class để kết nối với Form4.Designer.cs
    public partial class GAMEBOSS : Form
    {
        // --- Logic Variables / Trạng thái điều khiển ---
        private bool goLeft = false, goRight = false, shooting = false;
        private int playerSpeed = 8;
        private int bulletSpeed = 20;
        private int bossSpeed = 5;
        private int bossAttackTimer = 0;
        private int survivalTime = 90;
        private Random rnd = new Random();

        // --- Sát thương / điều chỉnh ---
        private const int BASE_DAMAGE = 10;
        private int playerDamage; // Tổng sát thương thực tế (BASE_DAMAGE + AccountData.UpgradeDamage)

        // --- Boss attack tuning ---
        private int bossAttackFrequency = 50;
        private int maxBossBullets = 50;

        // GIẢ LẬP DỮ LIỆU TÀI KHOẢN VÀ DATABASE
        // Bạn cần thay thế bằng logic tải/lưu dữ liệu thực tế của mình.
        public static class AccountData
        {
            public static int Level = 1;
            public static int Gold = 0;
            public static int UpgradeHP = 100;
            public static int UpgradeDamage = 0;
        }

        public static class Database
        {
            // CÁC HÀM NÀY CẦN LOGIC LƯU/TẢI DỮ LIỆU THỰC TẾ
            public static void UpdateAccountData() { /* Lưu dữ liệu */ }
            public static void LoadAccountData() { /* Tải dữ liệu */ }
        }

        public GAMEBOSS()
        {
            InitializeComponent();
        }

        // --- Khi Form load ---
        private void Form4_Load(object sender, EventArgs e)
        {
            // 1. TẢI DỮ LIỆU TỪ DATABASE
            Database.LoadAccountData();

            // 2. TÍNH TOÁN SÁT THƯƠNG THỰC TẾ
            playerDamage = BASE_DAMAGE + AccountData.UpgradeDamage;

            // 3. CÀI MÁU PLAYER
            playerHealthBar.Maximum = AccountData.UpgradeHP;
            playerHealthBar.Value = playerHealthBar.Maximum;
            playerHealthBar.ForeColor = Color.Lime;
            playerHealthBar.BackColor = Color.Black;

            // 4. CÀI MÁU BOSS DỰA TRÊN LEVEL HIỆN TẠI
            // Đã chỉnh sửa từ 10000 về 1000 để tránh lỗi MAX khi khởi tạo
            int currentBossMaxHealth = Math.Max(100, AccountData.Level * 1000);

            bossHealthBar.Maximum = currentBossMaxHealth;
            bossHealthBar.Value = currentBossMaxHealth;
            bossHealthBar.ForeColor = Color.Red;
            bossHealthBar.BackColor = Color.Black;

            // 5. HIỂN THỊ THÔNG TIN BAN ĐẦU
            survivalTime = 90;
            UpdateScoreText(); // Gọi hàm cập nhật hiển thị điểm

            // 6. BẮT ĐẦU GAME
            gameTimer.Start();
            survivalTimer.Start();
            // buttonExit.Visible = false; // Nếu bạn có nút Exit ẩn ban đầu
        }

        private void UpdateScoreText()
        {
            txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level}";
        }

        // --- Vòng lặp game ---
        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            UpdateScoreText();
            int currentBossBullets = 0; // Đếm số lượng đạn boss hiện tại

            // Player movement
            if (goLeft && player.Left > 0) player.Left -= playerSpeed;
            if (goRight && player.Right < this.ClientSize.Width) player.Left += playerSpeed;

            // Boss movement và Boss attack timer
            boss.Left += bossSpeed;
            if (boss.Left < 0 || boss.Right > this.ClientSize.Width)
            {
                bossSpeed = -bossSpeed;
            }

            bossAttackTimer++;
            if (bossAttackTimer > bossAttackFrequency)
            {
                bossAttackTimer = 0;
                ShootBossBulletRandom();
            }

            // Xử lý tất cả các object trên form (Đi ngược từ cuối để tránh lỗi khi Remove)
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                Control c = this.Controls[i];

                // PLAYER BULLET
                if (c is PictureBox && (string)c.Tag == "playerBullet")
                {
                    c.Top -= bulletSpeed;
                    if (c.Top < -c.Height)
                    {
                        this.Controls.Remove(c);
                        c.Dispose();
                        continue;
                    }

                    // Kiểm tra va chạm với boss
                    if (c.Bounds.IntersectsWith(boss.Bounds))
                    {
                        bossHealthBar.Value = Math.Max(0, bossHealthBar.Value - playerDamage);

                        // ShowExplosion(c.Left, c.Top); // Đã bị xóa ở một phiên bản

                        this.Controls.Remove(c);
                        c.Dispose();

                        if (bossHealthBar.Value == 0)
                        {
                            // ShowExplosion(boss.Left + boss.Width / 2, boss.Top + boss.Height / 2); // Đã bị xóa ở một phiên bản
                            EndGame(true);
                            return;
                        }
                        continue;
                    }
                }

                // BOSS BULLET
                if (c is PictureBox && (string)c.Tag == "bossBullet")
                {
                    currentBossBullets++;

                    string nm = (string)c.Name;
                    if (!string.IsNullOrEmpty(nm) && nm.Contains("angle:") && nm.Contains("speed:"))
                    {
                        try
                        {
                            // Logic di chuyển phức tạp hơn
                            string[] parts = nm.Split(',');
                            int dir = int.Parse(parts[0].Split(':')[1]);
                            int spd = int.Parse(parts[1].Split(':')[1]);

                            c.Top += spd;
                            c.Left += dir * (spd / 2);
                        }
                        catch
                        {
                            c.Top += 10; // Fallback
                        }
                    }
                    else
                    {
                        c.Top += 10; // Di chuyển cơ bản
                    }

                    if (c.Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealthBar.Value = Math.Max(0, playerHealthBar.Value - 10);

                        // ShowExplosion(c.Left, c.Top); // Đã bị xóa ở một phiên bản

                        // Đổi màu máu
                        if (playerHealthBar.Value < playerHealthBar.Maximum / 2) playerHealthBar.ForeColor = Color.Yellow;
                        if (playerHealthBar.Value < playerHealthBar.Maximum / 4) playerHealthBar.ForeColor = Color.Red;

                        this.Controls.Remove(c);
                        c.Dispose();

                        if (playerHealthBar.Value == 0)
                        {
                            // ShowExplosion(player.Left + player.Width / 2, player.Top + player.Height / 2); // Đã bị xóa ở một phiên bản
                            EndGame(false);
                            return;
                        }
                        continue;
                    }

                    if (c.Top > this.ClientSize.Height + c.Height || c.Left < -c.Width || c.Right > this.ClientSize.Width + c.Width)
                    {
                        this.Controls.Remove(c);
                        c.Dispose();
                        continue;
                    }
                }
            }

            // Điều chỉnh tần suất bắn boss
            bossAttackFrequency = (currentBossBullets > maxBossBullets) ? 200 : 50;
        }

        // --- Hiệu ứng nổ ---
        // HÀM NÀY CẦN HÌNH ẢNH NỔ hoặc sử dụng lại logic cũ, nhưng tôi đã hợp nhất logic nổ:
        private async void ShowExplosion(int x, int y)
        {
            PictureBox explosion = new PictureBox();
            int size = 50;
            explosion.Size = new Size(size, size);
            explosion.Left = x - size / 2;
            explosion.Top = y - size / 2;
            explosion.BackColor = Color.Transparent;

            Bitmap bmp = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (var brush = new System.Drawing.Drawing2D.PathGradientBrush(
                    new Point[]
                    {
                        new Point(0, size/2), new Point(size/2, 0),
                        new Point(size, size/2), new Point(size/2, size)
                    }))
                {
                    brush.CenterColor = Color.Yellow;
                    brush.SurroundColors = new Color[] { Color.Red };
                    g.FillEllipse(brush, 0, 0, size, size);
                }
            }

            explosion.Image = bmp;
            explosion.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(explosion);
            explosion.BringToFront();

            // Làm chớp sáng một chút
            for (int i = 0; i < 3; i++)
            {
                explosion.Visible = !explosion.Visible;
                await Task.Delay(80);
            }

            this.Controls.Remove(explosion);
            explosion.Dispose();
        }

        // --- Đạn boss ---
        private void ShootBossBulletRandom()
        {
            int[] horizontalDirections = { -1, 0, 1 };
            int baseSpeed = 10;

            for (int i = 0; i < 3; i++)
            {
                // Sử dụng hàm tạo đạn gradient (từ code bị hợp nhất)
                PictureBox bullet = CreateGradientBullet(24, 24, Color.OrangeRed, Color.Yellow);

                bullet.Tag = "bossBullet";
                bullet.Left = boss.Left + boss.Width / 2 - bullet.Width / 2;
                bullet.Top = boss.Bottom;

                int directionX = horizontalDirections[i];
                int moveSpeed = baseSpeed + rnd.Next(-2, 3);

                bullet.Name = $"angle:{directionX},speed:{moveSpeed}";

                this.Controls.Add(bullet);
                bullet.BringToFront();
            }
        }

        // --- Đạn player ---
        private void ShootPlayerBullet()
        {
            // Sử dụng hàm tạo đạn gradient (từ code bị hợp nhất)
            PictureBox bullet = CreateGradientBullet(10, 30, Color.Cyan, Color.White);

            bullet.Tag = "playerBullet";
            bullet.Left = player.Left + player.Width / 2 - bullet.Width / 2;
            bullet.Top = player.Top - bullet.Height;

            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        // --- Tạo đạn gradient ---
        private PictureBox CreateGradientBullet(int w, int h, Color inner, Color outer)
        {
            Bitmap bmp = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, w, h), inner, outer, 90f))
                {
                    g.FillEllipse(brush, 0, 0, w - 1, h - 1);
                }
            }

            PictureBox p = new PictureBox()
            {
                Image = bmp,
                Size = new Size(w, h),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            return p;
        }

        // --- Đếm ngược thời gian ---
        private void survivalTimer_Tick(object sender, EventArgs e)
        {
            survivalTime--;
            if (survivalTime <= 0)
            {
                EndGame(true);
            }
            else
            {
                UpdateScoreText();
            }
        }

        // --- Kết thúc game ---
        private void EndGame(bool win)
        {
            gameTimer.Stop();
            survivalTimer.Stop();

            // Xóa tất cả đạn
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                Control c = this.Controls[i];
                if (c is PictureBox && ((string)c.Tag == "playerBullet" || (string)c.Tag == "bossBullet"))
                {
                    this.Controls.Remove(c);
                    c.Dispose();
                }
            }

            // Xử lý dữ liệu và thông báo
            if (win)
            {
                AccountData.Gold += 20;
                AccountData.Level++;
                Database.UpdateAccountData();

                txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level} - WIN!";
            }
            else
            {
                AccountData.Gold += 5;
                Database.UpdateAccountData();
                txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level} - GAME OVER!";
            }

            // Hiển thị nút thoát
            // if (buttonExit != null) buttonExit.Visible = true; // Cần kiểm tra buttonExit có tồn tại không
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Database.UpdateAccountData();

            // Logic trở về Form Menu (Form3)
            try
            {
                Form menu = (Form)Activator.CreateInstance(Type.GetType("Kien.Form3"));
                menu.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi: Không tìm thấy Form3. Hãy đảm bảo Form3 đã được tạo trong namespace Kien.");
            }

            this.Close();
        }

        // --- Xử lý phím ---
        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = true;
            if (e.KeyCode == Keys.Right) goRight = true;
            if (e.KeyCode == Keys.Space && !shooting)
            {
                shooting = true;
                ShootPlayerBullet();
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = false;
            if (e.KeyCode == Keys.Right) goRight = false;
            if (e.KeyCode == Keys.Space) shooting = false;
        }

        private void txtScore_Click(object sender, EventArgs e) { }
    }
}

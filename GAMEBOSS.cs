using plan_fighting_super_start.Properties;
using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    // Sử dụng partial class để kết nối với Form4.Designer.cs
    public partial class GAMEBOSS : Form
    {
        // Logic Variables
        private bool goLeft, goRight, shooting;
        private int playerSpeed = 8;
        private int bulletSpeed = 20;
        private int bossSpeed = 5;
        private int bossAttackTimer = 0;
        private int survivalTime = 90;

        private Random rnd = new Random();

        // Biến Sát Thương Cố Định
        private const int BASE_DAMAGE = 10;
        private int playerDamage; // Tổng sát thương thực tế (BASE_DAMAGE + AccountData.UpgradeDamage)

        // Biến điều chỉnh tần suất và loại đạn boss
        private int bossAttackFrequency = 50;
        private int maxBossBullets = 50;

        // GIẢ LẬP DỮ LIỆU TÀI KHOẢN VÀ DATABASE
        public static class AccountData
        {
            public static int Level = 1;
            public static int Gold = 0;
            public static int UpgradeHP = 100;
            public static int UpgradeDamage = 0;
        }

        public static class Database
        {
            public static void UpdateAccountData() { /* Lưu dữ liệu */ }
            public static void LoadAccountData() { /* Tải dữ liệu */ }
        }

        public GAMEBOSS()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Đặt màu nền Form là Đen (nếu chưa được đặt trong Designer)
            this.BackColor = Color.Black;

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
            int currentBossMaxHealth = AccountData.Level * 10000;

            bossHealthBar.Maximum = currentBossMaxHealth;
            bossHealthBar.Value = currentBossMaxHealth;
            bossHealthBar.ForeColor = Color.Red;
            bossHealthBar.BackColor = Color.Black;

            // 5. HIỂN THỊ THÔNG TIN BAN ĐẦU
            survivalTime = 90;
            txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level}";

            // 6. BẮT ĐẦU GAME
            gameTimer.Start();
            survivalTimer.Start();
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level}";

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

            int currentBossBullets = 0;

            // Xử lý tất cả các object trên form
            foreach (Control x in this.Controls)
            {
                // Player bullet
                if (x is PictureBox && (string)x.Tag == "playerBullet")
                {
                    x.Top -= bulletSpeed;

                    if (x.Top < -x.Height)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                    }

                    // Kiểm tra va chạm với boss
                    if (x.Bounds.IntersectsWith(boss.Bounds))
                    {
                        bossHealthBar.Value = Math.Max(0, bossHealthBar.Value - playerDamage);
                        this.Controls.Remove(x);
                        x.Dispose();

                        if (bossHealthBar.Value == 0)
                        {
                            EndGame(true);
                        }
                    }
                }

                // Boss bullet
                if (x is PictureBox && (string)x.Tag == "bossBullet")
                {
                    currentBossBullets++;

                    string nameData = (string)x.Name;
                    if (!string.IsNullOrEmpty(nameData) && nameData.Contains("angle:") && nameData.Contains("speed:"))
                    {
                        string[] movementData = nameData.Split(',');
                        int directionX = int.Parse(movementData[0].Split(':')[1]);
                        int moveSpeed = int.Parse(movementData[1].Split(':')[1]);

                        x.Top += moveSpeed;
                        x.Left += directionX * (moveSpeed / 2);
                    }
                    else
                    {
                        x.Top += 10;
                    }

                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealthBar.Value = Math.Max(0, playerHealthBar.Value - 10);

                        if (playerHealthBar.Value < playerHealthBar.Maximum / 2) playerHealthBar.ForeColor = Color.Yellow;
                        if (playerHealthBar.Value < playerHealthBar.Maximum / 4) playerHealthBar.ForeColor = Color.Red;

                        this.Controls.Remove(x);
                        x.Dispose();

                        if (playerHealthBar.Value == 0)
                        {
                            EndGame(false);
                        }
                    }

                    if (x.Top > this.ClientSize.Height + x.Height || x.Left < -x.Width || x.Right > this.ClientSize.Width + x.Width)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                    }
                }
            }

            if (currentBossBullets > maxBossBullets)
            {
                bossAttackFrequency = 200;
            }
            else
            {
                bossAttackFrequency = 50;
            }
        }

        // 🚨 ĐÃ SỬA ĐỔI: Đạn Boss (Lớn, Đỏ Plasma)
        private void ShootBossBulletRandom()
        {
            int[] horizontalDirections = { -1, 0, 1 };
            int baseSpeed = 10;

            for (int i = 0; i < 3; i++)
            {
                PictureBox bullet = new PictureBox();
                // Kích thước lớn hơn, trông như quả cầu plasma
                bullet.Size = new Size(16, 32);
                bullet.Tag = "bossBullet";

                // Màu đỏ rực/Magenta cho hiệu ứng năng lượng tối
                bullet.BackColor = Color.Red;

                bullet.Left = boss.Left + boss.Width / 2 - bullet.Width / 2;
                bullet.Top = boss.Bottom;

                int directionX = horizontalDirections[i];
                int moveSpeed = baseSpeed + rnd.Next(-2, 3);

                bullet.Name = $"angle:{directionX},speed:{moveSpeed}";

                this.Controls.Add(bullet);
                bullet.BringToFront();
            }
        }

        // 🚨 ĐÃ SỬA ĐỔI: Đạn Player (Mảnh, Xanh Laser)
        private void ShootPlayerBullet()
        {
            PictureBox bullet = new PictureBox();
            // Kích thước mảnh, trông như tia laser
            bullet.Size = new Size(8, 28);
            bullet.Tag = "playerBullet";

            // Màu xanh Neon (Cyan/Aqua)
            bullet.BackColor = Color.Aqua;

            bullet.Left = player.Left + player.Width / 2 - bullet.Width / 2;
            bullet.Top = player.Top - bullet.Height;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void survivalTimer_Tick(object sender, EventArgs e)
        {
            survivalTime--;
            if (survivalTime <= 0)
            {
                EndGame(true);
            }
        }

        private void EndGame(bool win)
        {
            gameTimer.Stop();
            survivalTimer.Stop();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && ((string)x.Tag == "playerBullet" || (string)x.Tag == "bossBullet"))
                {
                    if (x != null)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                    }
                }
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

            buttonExit.Visible = true;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Database.UpdateAccountData();

            try
            {
                // Giả định Form menu là Form3
                Form MENU = (Form)Activator.CreateInstance(Type.GetType("plan-fighting-super-start.Menu"));
                MENU.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi: Không tìm thấy Form3. Hãy đảm bảo Form3 đã được tạo trong namespace Kien.");
            }

            this.Close();
        }

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
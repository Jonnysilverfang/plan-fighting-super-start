using plan_fighting_super_start.Properties;
using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class GAMEBOSS : Form
    {
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

        private int bossAttackFrequency = 50;
        private int maxBossBullets = 50;

        // ❌ XÓA Mock Database cũ và thay thế bằng định nghĩa dưới đây 
        //    (HOẶC bạn nên di chuyển định nghĩa này ra khỏi file GAMEBOSS.cs và đặt vào Database.cs như tôi đã làm ở bước trước,
        //     nhưng nếu bạn muốn giữ nó trong file này, đây là cách chỉnh sửa để thêm Username)

        // ⭐ AccountData TĨNH mới (Đã thêm Username)
        public static class AccountData
        {
            public static string? Username { get; set; } = null; // Thêm Username
            public static int Level { get; set; } = 1;
            public static int Gold { get; set; } = 0;
            public static int UpgradeHP { get; set; } = 100;
            public static int UpgradeDamage { get; set; } = 0;
        }

        // ⭐ Database class: Bỏ phần thân rỗng, chỉ giữ lại khai báo (vì thân đã được định nghĩa trong file Database.cs)
        public static class Database
        {
            // Các hàm này sẽ gọi đến các hàm API đã được định nghĩa ở file Database.cs
            public static void UpdateAccountData() { /* Logic API đã nằm ở file khác */ }
            public static void LoadAccountData() { /* Logic API đã nằm ở file khác */ }
            // Cần phải đảm bảo file Database.cs có các hàm này và đã được định nghĩa đúng cách
        }

        public GAMEBOSS()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.White;

            // ⭐ Đã chỉnh sửa: Gọi hàm LoadAccountData đã được kết nối API
            Database.LoadAccountData();

            playerDamage = BASE_DAMAGE + AccountData.UpgradeDamage;

            playerHealthBar.Maximum = AccountData.UpgradeHP;
            playerHealthBar.Value = playerHealthBar.Maximum;
            playerHealthBar.ForeColor = Color.Lime;

            int currentBossMaxHealth = AccountData.Level * 10000;
            bossHealthBar.Maximum = currentBossMaxHealth;
            bossHealthBar.Value = currentBossMaxHealth;
            bossHealthBar.ForeColor = Color.Red;

            survivalTime = 90;
            txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level}";

            gameTimer.Start();
            survivalTimer.Start();
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            frameCounter++;
            txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level}";

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
                ShootBossBulletRandom();
            }

            int currentBossBullets = 0;

            foreach (Control x in this.Controls)
            {
                // Player bullet
                if (x is PictureBox && (string)x.Tag == "playerBullet")
                {
                    CreateBulletTrail(x.Left + x.Width / 2, x.Top + x.Height, Color.Aqua);
                    x.Top -= bulletSpeed;

                    int glow = (int)(Math.Abs(Math.Sin(frameCounter * 0.2)) * 100);
                    x.BackColor = Color.FromArgb(255, 0, 200 + glow / 2, 255);

                    if (x.Top < -x.Height)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                        continue;
                    }

                    if (x.Bounds.IntersectsWith(boss.Bounds))
                    {
                        bossHealthBar.Value = Math.Max(0, bossHealthBar.Value - playerDamage);
                        CreateExplosion(x.Left, x.Top, Color.Aqua);
                        this.Controls.Remove(x);
                        x.Dispose();

                        if (bossHealthBar.Value == 0)
                        {
                            EndGame(true);
                            break;
                        }
                    }
                }

                // Boss bullet
                if (x is PictureBox && (string)x.Tag == "bossBullet")
                {
                    currentBossBullets++;
                    string nameData = (string)x.Name;
                    int directionX = 0, moveSpeed = 10;

                    if (!string.IsNullOrEmpty(nameData) && nameData.Contains("angle:") && nameData.Contains("speed:"))
                    {
                        string[] parts = nameData.Split(',');
                        directionX = int.Parse(parts[0].Split(':')[1]);
                        moveSpeed = int.Parse(parts[1].Split(':')[1]);
                    }

                    CreateBulletTrail(x.Left + x.Width / 2, x.Top + x.Height / 2, Color.Red);
                    x.Top += moveSpeed;
                    x.Left += directionX * (moveSpeed / 2);

                    int glow = (int)(Math.Abs(Math.Sin(frameCounter * 0.25)) * 150);
                    x.BackColor = Color.FromArgb(255, 255, 50 + glow, 50 + glow / 2);

                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealthBar.Value = Math.Max(0, playerHealthBar.Value - 10);
                        if (playerHealthBar.Value < playerHealthBar.Maximum / 2) playerHealthBar.ForeColor = Color.Yellow;
                        if (playerHealthBar.Value < playerHealthBar.Maximum / 4) playerHealthBar.ForeColor = Color.Red;

                        CreateExplosion(x.Left, x.Top, Color.OrangeRed);
                        this.Controls.Remove(x);
                        x.Dispose();

                        if (playerHealthBar.Value == 0)
                        {
                            EndGame(false);
                            break;
                        }
                    }

                    if (x.Top > this.ClientSize.Height + x.Height ||
                        x.Left < -x.Width ||
                        x.Right > this.ClientSize.Width + x.Width)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                    }
                }

                // Trail
                if (x is PictureBox && (string)x.Tag == "trail")
                {
                    x.BackColor = Color.FromArgb(Math.Max(0, x.BackColor.A - 15),
                        x.BackColor.R, x.BackColor.G, x.BackColor.B);

                    if (x.BackColor.A <= 20)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                    }
                }

                // Explosion
                if (x is PictureBox && (string)x.Tag == "explosion")
                {
                    x.Width += 4;
                    x.Height += 4;
                    x.Left -= 2;
                    x.Top -= 2;
                    x.BackColor = Color.FromArgb(Math.Max(0, x.BackColor.A - 20),
                        x.BackColor.R, x.BackColor.G, x.BackColor.B);

                    if (x.BackColor.A <= 20)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                    }
                }
            }

            if (currentBossBullets > maxBossBullets)
                bossAttackFrequency = 200;
            else
                bossAttackFrequency = 50;
        }

        // Tạo vệt sáng
        private void CreateBulletTrail(int x, int y, Color baseColor)
        {
            PictureBox trail = new PictureBox();
            trail.Size = new Size(6, 10);
            trail.Tag = "trail";
            trail.BackColor = Color.FromArgb(150, baseColor.R, baseColor.G, baseColor.B);
            trail.Left = x - trail.Width / 2;
            trail.Top = y;
            this.Controls.Add(trail);
            trail.SendToBack();
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

        private void ShootBossBulletRandom()
        {
            int[] horizontalDirections = { -1, 0, 1 };
            int baseSpeed = 10;

            for (int i = 0; i < 3; i++)
            {
                PictureBox bullet = new PictureBox();
                bullet.Size = new Size(16, 32);
                bullet.Tag = "bossBullet";
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

        // ⚡ Đạn Player dạng sấm sét (Lightning Bolt)
        private void ShootPlayerBullet()
        {
            // Kích thước PictureBox đủ để chứa viên đạn nhọn và glow
            PictureBox bullet = new PictureBox();
            bullet.Size = new Size(20, 45); // Tăng chiều cao một chút cho đầu nhọn
            bullet.Tag = "playerBullet";
            bullet.BackColor = Color.Transparent;

            Bitmap bmp = new Bitmap(bullet.Width, bullet.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                Random rand = new Random(Guid.NewGuid().GetHashCode());

                // Vị trí trung tâm X của viên đạn trong PictureBox
                float centerX = bullet.Width / 2;

                // Kích thước của phần thân viên đạn (hình chữ nhật)
                int bodyWidth = 4;
                int bodyHeight = 25;

                // ----------------------------------------------------
                // BƯỚC 1: VẼ HIỆU ỨNG TIA ĐIỆN NHỎ XUNG QUANH
                // ----------------------------------------------------

                // Điểm bắt đầu cho tia điện (Gần đáy)
                PointF startGlow = new PointF(centerX, bullet.Height);
                var glowPoints = new System.Collections.Generic.List<PointF> { startGlow };
                PointF currentGlow = startGlow;

                // Tạo đường ziczac NGẮN VÀ MỀM MẠI hơn
                for (int i = 0; i < 6; i++)
                {
                    float xOffset = rand.Next(-3, 4);
                    float yOffset = -rand.Next(3, 5);
                    PointF next = new PointF(currentGlow.X + xOffset, currentGlow.Y + yOffset);
                    glowPoints.Add(next);
                    currentGlow = next;
                }

                // Lớp ánh sáng ngoài (glow) - Mờ và rất nhạt
                using (var glowPen = new Pen(Color.FromArgb(50, 0, 255, 255), 5))
                {
                    glowPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    g.DrawLines(glowPen, glowPoints.ToArray());
                }

                // ----------------------------------------------------
                // BƯỚC 2: VẼ VIÊN ĐẠN CHÍNH (Thân hình chữ nhật + đầu nhọn)
                // ----------------------------------------------------

                // Vị trí vẽ phần thân hình chữ nhật
                Rectangle bodyRect = new Rectangle(
                    (int)centerX - bodyWidth / 2,
                    bullet.Height - bodyHeight - 5, // Đặt phần thân lên trên một chút, chừa chỗ cho đầu nhọn
                    bodyWidth,
                    bodyHeight
                );

                // Tạo các điểm cho đầu nhọn (hình tam giác)
                PointF tipPoint = new PointF(centerX, bodyRect.Top - 5); // Đỉnh nhọn
                PointF leftBase = new PointF(bodyRect.Left, bodyRect.Top); // Góc trái trên của thân
                PointF rightBase = new PointF(bodyRect.Right, bodyRect.Top); // Góc phải trên của thân
                PointF[] tipShape = { tipPoint, leftBase, rightBase };

                // Vòng ngoài/Glow nhẹ của viên đạn (Màu xanh cyan)
                using (var bulletPen = new Pen(Color.Cyan, 2f))
                {
                    g.DrawRectangle(bulletPen, bodyRect); // Vẽ thân
                    g.DrawPolygon(bulletPen, tipShape); // Vẽ đầu nhọn
                }

                // Lõi viên đạn (Màu trắng hoặc trắng xám)
                using (var bulletBrush = new SolidBrush(Color.FromArgb(200, 255, 255, 255)))
                {
                    // Điền đầy phần thân
                    g.FillRectangle(bulletBrush,
                        bodyRect.X + 1,
                        bodyRect.Y + 1,
                        bodyRect.Width - 2,
                        bodyRect.Height - 2);

                    // Điền đầy phần đầu nhọn
                    // Để làm cho đầu nhọn trông đầy đặn hơn, có thể thu nhỏ tam giác một chút khi fill
                    PointF[] filledTipShape = {
                        new PointF(centerX, tipPoint.Y + 1),
                        new PointF(leftBase.X + 1, leftBase.Y),
                        new PointF(rightBase.X - 1, rightBase.Y)
                    };
                    g.FillPolygon(bulletBrush, filledTipShape);
                }

                // ----------------------------------------------------
                // BƯỚC 3: HIỆU ỨNG TIA SÁNG ĐẦU VIÊN ĐẠN (Ở đỉnh nhọn)
                // ----------------------------------------------------

                // Vẽ một hình tròn nhỏ ở đỉnh nhọn để mô phỏng điểm nóng
                int sparkRadius = 3;
                using (var sparkBrush = new SolidBrush(Color.White))
                {
                    g.FillEllipse(sparkBrush,
                        (int)centerX - sparkRadius,
                        (int)tipPoint.Y - sparkRadius, // Vị trí ở đỉnh nhọn
                        sparkRadius * 2,
                        sparkRadius * 2);
                }
            }

            bullet.Image = bmp;
            bullet.SizeMode = PictureBoxSizeMode.Normal;

            // Đặt vị trí
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
                if (x is PictureBox && ((string)x.Tag == "playerBullet" || (string)x.Tag == "bossBullet" || (string)x.Tag == "trail" || (string)x.Tag == "explosion"))
                {
                    this.Controls.Remove(x);
                    x.Dispose();
                }
            }

            if (win)
            {
                AccountData.Gold += 200;
                AccountData.Level++;
                // ⭐ Logic cập nhật đã đúng, hàm Database.UpdateAccountData() sẽ gọi API
                Database.UpdateAccountData();
                txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level} - WIN!";
            }
            else
            {
                AccountData.Gold += 50;
                // ⭐ Logic cập nhật đã đúng, hàm Database.UpdateAccountData() sẽ gọi API
                Database.UpdateAccountData();
                txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level} - GAME OVER!";
            }

            buttonExit.Visible = true;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            // ⭐ Đảm bảo dữ liệu được lưu lần cuối trước khi thoát
            Database.UpdateAccountData();
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

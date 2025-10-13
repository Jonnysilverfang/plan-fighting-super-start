using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kien
{
    public partial class Form4 : Form
    {
        private bool goLeft, goRight, shooting;
        private int playerSpeed = 8;
        private int bulletSpeed = 20;
        private int bossSpeed = 5;
        private int bossAttackTimer = 0;
        private int survivalTime = 90;

        private Random rnd = new Random();

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // Cài máu cho player và boss
            playerHealthBar.Maximum = AccountData.UpgradeHP; // Lấy giá trị HP tối đa của player từ dữ liệu tài khoản
            playerHealthBar.Value = playerHealthBar.Maximum; // Đặt giá trị thanh máu player bằng HP tối đa
            playerHealthBar.ForeColor = Color.Lime; // Màu thanh tiến trình của người chơi (màu xanh lá)
            playerHealthBar.BackColor = Color.Black; // Màu nền của thanh máu người chơi (màu đen)

            bossHealthBar.Maximum = 1000; // Máu tối đa của boss
            bossHealthBar.Value = 1000; // Đặt giá trị thanh máu boss
            bossHealthBar.ForeColor = Color.Red; // Màu thanh tiến trình của boss (màu đỏ)
            bossHealthBar.BackColor = Color.Black; // Màu nền của thanh máu boss (màu đen)

            // Reset vàng và thời gian
            survivalTime = 90;
            txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}";

            // Bắt đầu game
            gameTimer.Start();
            survivalTimer.Start();
        }

        // Game loop
        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}";

            // Player movement
            if (goLeft && player.Left > 0) player.Left -= playerSpeed;
            if (goRight && player.Right < this.ClientSize.Width) player.Left += playerSpeed;

            // Boss di chuyển qua lại
            boss.Left += bossSpeed;
            if (boss.Left < 0 || boss.Right > this.ClientSize.Width)
            {
                bossSpeed = -bossSpeed;
            }

            // Boss bắn đạn định kỳ
            bossAttackTimer++;
            if (bossAttackTimer > 50) // mỗi 50 tick bắn 1 viên
            {
                bossAttackTimer = 0;
                ShootBossBullet();
            }

            // Xử lý tất cả các object trên form
            foreach (Control x in this.Controls)
            {
                // Player bullet
                if (x is PictureBox && (string)x.Tag == "playerBullet")
                {
                    x.Top -= bulletSpeed;

                    if (x.Top < 0)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                    }

                    if (x.Bounds.IntersectsWith(boss.Bounds))
                    {
                        bossHealthBar.Value = Math.Max(0, bossHealthBar.Value - (10 + AccountData.UpgradeDamage)); // Giảm máu boss
                        this.Controls.Remove(x);
                        x.Dispose();

                        if (bossHealthBar.Value == 0)
                        {
                            EndGame(true); // Kết thúc game, người chơi thắng
                        }
                    }
                }

                // Boss bullet
                if (x is PictureBox && (string)x.Tag == "bossBullet")
                {
                    x.Top += 10;

                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        playerHealthBar.Value = Math.Max(0, playerHealthBar.Value - 10); // Giảm máu người chơi

                        // Thay đổi màu sắc thanh máu khi máu người chơi thấp
                        if (playerHealthBar.Value < playerHealthBar.Maximum / 2)
                        {
                            playerHealthBar.ForeColor = Color.Yellow; // Chuyển sang màu vàng khi máu dưới 50%
                        }
                        if (playerHealthBar.Value < playerHealthBar.Maximum / 4)
                        {
                            playerHealthBar.ForeColor = Color.Red; // Chuyển sang màu đỏ khi máu dưới 25%
                        }

                        this.Controls.Remove(x);
                        x.Dispose();

                        if (playerHealthBar.Value == 0)
                        {
                            EndGame(false); // Kết thúc game, người chơi thua
                        }
                    }

                    if (x.Top > this.ClientSize.Height)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                    }
                }
            }
        }

        // Boss bắn đạn
        private void ShootBossBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.BackColor = Color.Red;
            bullet.Size = new Size(8, 20);
            bullet.Tag = "bossBullet";
            bullet.Left = boss.Left + boss.Width / 2 - bullet.Width / 2;
            bullet.Top = boss.Bottom;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        // Player bắn đạn
        private void ShootPlayerBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.BackColor = Color.Yellow;
            bullet.Size = new Size(5, 20);
            bullet.Tag = "playerBullet";
            bullet.Left = player.Left + player.Width / 2 - bullet.Width / 2;
            bullet.Top = player.Top - bullet.Height;
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        // Thời gian sống sót
        private void survivalTimer_Tick(object sender, EventArgs e)
        {
            survivalTime--;
            if (survivalTime <= 0)
            {
                EndGame(true); // Thắng do sống sót
            }
        }

        // End game
        private void EndGame(bool win)
        {
            gameTimer.Stop();
            survivalTimer.Stop();

            if (win)
            {
                AccountData.Gold += 20; // Thưởng nếu thắng
                AccountData.Level++; // Tăng số ải khi thắng

                // Tăng máu boss sau mỗi ải
                bossHealthBar.Maximum += 100;
                bossHealthBar.Value = bossHealthBar.Maximum; // Đặt lại máu của boss

                // Cập nhật UI với số ải hiện tại
                txtScore.Text = $"Gold: {AccountData.Gold}  Time: {survivalTime}  Level: {AccountData.Level}";

                // Cập nhật số ải trong cơ sở dữ liệu
                Database.UpdateAccountData();
            }
            else
            {
                AccountData.Gold += 5; // Thua nhưng vẫn có chút vàng
            }

            buttonExit.Visible = true;
        }

        // Bấm nút exit
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Database.UpdateAccountData();
            Form3 menu = new Form3();
            menu.Show();
            this.Close();
        }

        // Key down
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

        // Key up
        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = false;
            if (e.KeyCode == Keys.Right) goRight = false;
            if (e.KeyCode == Keys.Space) shooting = false;
        }

        private void txtScore_Click(object sender, EventArgs e) { }
    }
}

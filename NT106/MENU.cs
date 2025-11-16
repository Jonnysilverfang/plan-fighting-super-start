using System;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;   // ⭐ thêm

namespace plan_fighting_super_start
{
    public partial class Menu : Form
    {
        // ⭐ player nhạc nền
        private WindowsMediaPlayer bgmPlayer;

        public Menu()
        {
            InitializeComponent();

            // đăng ký sự kiện để dọn nhạc khi đóng form
            this.FormClosing += Menu_FormClosing;

            InitBackgroundMusic();
        }

        // ===== Nhạc nền bossgame.mp3 =====
        private void InitBackgroundMusic()
        {
            try
            {
                // Đường dẫn tới file bossgame.mp3 nằm trong thư mục exe
                string mp3Path = System.IO.Path.Combine(
                    Application.StartupPath,
                    "bossgame.mp3");

                bgmPlayer = new WindowsMediaPlayer();
                bgmPlayer.URL = mp3Path;
                bgmPlayer.settings.setMode("loop", true);  // lặp vô hạn
                bgmPlayer.settings.volume = 40;            // âm lượng 0–100
                bgmPlayer.controls.play();
            }
            catch (Exception ex)
            {
                // Nếu lỗi (thiếu file, thiếu COM, v.v.) thì chỉ báo nhẹ, không cho crash
                MessageBox.Show("Không phát được nhạc nền: " + ex.Message,
                    "Lỗi nhạc nền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Dừng nhạc, giải phóng khi đóng form
        private void Menu_FormClosing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                if (bgmPlayer != null)
                {
                    bgmPlayer.controls.stop();
                    bgmPlayer.close();
                    bgmPlayer = null;
                }
            }
            catch { }
        }

        // ===== Hàm dùng chung để load dữ liệu và cập nhật UI =====

        private void RefreshAccountDataAndUI()
        {
            try
            {
                if (!string.IsNullOrEmpty(AccountData.Username))
                {
                    Database.LoadAccountData(AccountData.Username);
                }
            }
            catch
            {
            }

            if (textBoxGold != null) textBoxGold.Text = AccountData.Gold.ToString();
            if (textBox1 != null) textBox1.Text = AccountData.UpgradeHP.ToString();
            if (textBox2 != null) textBox2.Text = AccountData.UpgradeDamage.ToString();
            if (textBox3 != null) textBox3.Text = AccountData.Level.ToString();
        }

        // Sự kiện load form (Designer đang gắn: Load += Form3_Load;)
        private void Form3_Load(object sender, EventArgs e)
        {
            if (labelWelcome != null)
            {
                labelWelcome.Text = "Xin chào";
            }

            RefreshAccountDataAndUI();

            if (buttonPlay != null) SetGameButton(buttonPlay);
            if (buttonUpgradeHP != null) SetGameButton(buttonUpgradeHP);
            if (buttonUpgradeDamage != null) SetGameButton(buttonUpgradeDamage);
            if (buttonExit != null) SetGameButton(buttonExit);
            if (button1 != null) SetGameButton(button1);
            if (button2 != null) SetGameButton(button2);

            if (textBoxGold != null) SetStatTextBox(textBoxGold);
            if (textBox1 != null) SetStatTextBox(textBox1);
            if (textBox2 != null) SetStatTextBox(textBox2);
            if (textBox3 != null) SetStatTextBox(textBox3);

            if (labelWelcome != null) SetHeaderLabel(labelWelcome);
            if (label1 != null) SetInfoLabel(label1);
            if (label2 != null) SetInfoLabel(label2);
            if (label3 != null) SetInfoLabel(label3);
            if (label4 != null) SetInfoLabel(label4);
        }

        // ===== Helpers: chỉ UI, không đụng logic =====

        private readonly Color Teal = Color.FromArgb(0, 192, 192);
        private readonly Color BgDark = Color.FromArgb(10, 15, 30);
        private readonly Color BgButton = Color.FromArgb(15, 25, 45);

        private void SetGameButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = Teal;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 120, 140);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 80, 100);
            button.BackColor = BgButton;
            button.ForeColor = Teal;
            button.UseVisualStyleBackColor = false;

            button.MouseEnter += (_, __) =>
            {
                button.BackColor = Teal;
                button.ForeColor = Color.Black;
            };

            button.MouseLeave += (_, __) =>
            {
                button.BackColor = BgButton;
                button.ForeColor = Teal;
            };
        }

        private void SetHeaderLabel(Label label)
        {
            label.BackColor = Color.Transparent;
            label.ForeColor = Teal;
            label.Font = new Font("Segoe UI", 20f, FontStyle.Bold);
            label.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void SetInfoLabel(Label label)
        {
            label.BackColor = Color.Transparent;
            label.ForeColor = Teal;
            label.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
        }

        private void SetStatTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.FromArgb(15, 22, 45);
            textBox.ForeColor = Teal;
            textBox.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            textBox.ReadOnly = true;
            textBox.TextAlign = HorizontalAlignment.Center;
        }

        // ====== Handlers nút bấm (logic giữ nguyên) ======

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new GAMEBOSS())
                {
                    form.ShowDialog(this);
                }

                RefreshAccountDataAndUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được chế độ chơi: " + ex.Message);
            }
        }

        private void buttonUpgradeHP_Click(object sender, EventArgs e)
        {
            if (AccountData.Gold >= 10)
            {
                AccountData.Gold -= 10;
                AccountData.UpgradeHP += 20;

                if (textBoxGold != null) textBoxGold.Text = AccountData.Gold.ToString();
                if (textBox1 != null) textBox1.Text = AccountData.UpgradeHP.ToString();

                try { Database.UpdateAccountData(); } catch { }
            }
            else
            {
                MessageBox.Show("Không đủ vàng để nâng HP!");
            }
        }

        private void buttonUpgradeDamage_Click(object sender, EventArgs e)
        {
            if (AccountData.Gold >= 15)
            {
                AccountData.Gold -= 15;
                AccountData.UpgradeDamage += 5;

                if (textBoxGold != null) textBoxGold.Text = AccountData.Gold.ToString();
                if (textBox2 != null) textBox2.Text = AccountData.UpgradeDamage.ToString();

                try { Database.UpdateAccountData(); } catch { }
            }
            else
            {
                MessageBox.Show("Không đủ vàng để nâng Damage!");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Room();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new Rank();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new ChangePass();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form = new Reward();
            form.Show();
        }
    }
}

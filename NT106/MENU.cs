using System;
using System.Drawing;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        // ===== Hàm dùng chung để load dữ liệu và cập nhật UI =====

        private void RefreshAccountDataAndUI()
        {
            // Tải dữ liệu tài khoản (nếu có API/DB và Username không null)
            try
            {
                if (!string.IsNullOrEmpty(AccountData.Username))
                {
                    Database.LoadAccountData(AccountData.Username);
                }
            }
            catch
            {
                // tránh sập app khi API lỗi; có thể log/MessageBox nếu muốn
            }

            // Cập nhật các TextBox hiển thị số liệu
            if (textBoxGold != null) textBoxGold.Text = AccountData.Gold.ToString();
            if (textBox1 != null) textBox1.Text = AccountData.UpgradeHP.ToString();
            if (textBox2 != null) textBox2.Text = AccountData.UpgradeDamage.ToString();
            if (textBox3 != null) textBox3.Text = AccountData.Level.ToString();
        }

        // Sự kiện load form (Designer đang gắn: Load += Form3_Load;)
        private void Form3_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin người chơi
            if (labelWelcome != null)
            {
                // Nếu muốn hiện tên sau này chỉ cần ghép AccountData.Username vào đây
                labelWelcome.Text = "Xin chào";
            }

            // Load dữ liệu + cập nhật UI
            RefreshAccountDataAndUI();

            // Style cho các control
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

        // Chơi solo (nút Play)
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            try
            {
                // Mở GAMEBOSS dạng modal, Menu sẽ chờ đến khi GAMEBOSS đóng
                using (var form = new GAMEBOSS())
                {
                    form.ShowDialog(this);
                }

                // Sau khi GAMEBOSS đóng (thắng/thua bấm Thoát),
                // dữ liệu đã được cộng Gold/Level và gọi UpdateAccountData trong GAMEBOSS
                // → Giờ reload lại từ API + cập nhật UI
                RefreshAccountDataAndUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được chế độ chơi: " + ex.Message);
            }
        }

        // Nâng HP
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

        // Nâng Damage
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

        // Thoát game
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Chơi với người (button1)
        private void button1_Click(object sender, EventArgs e)
        {
            var form = new Room();
            form.Show();
        }

        // Mở Rank (button2)
        private void button2_Click(object sender, EventArgs e)
        {
            var form = new Rank();
            form.Show();
        }
    }
}

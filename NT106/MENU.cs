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

        // Sự kiện load form (Designer đang gắn: Load += Form3_Load;)
        private void Form3_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin người chơi
            if (labelWelcome != null)
            {
                labelWelcome.Text = $"Xin chào";
            }

            // Tải dữ liệu tài khoản (nếu có API/DB)
            try
            {
                Database.LoadAccountData(AccountData.Username);
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

            // Làm trong suốt / style cho các control (gọi với kiểu đầy đủ để tránh mơ hồ)
            if (buttonPlay != null) SetTransparentButton(buttonPlay);
            if (buttonUpgradeHP != null) SetTransparentButton(buttonUpgradeHP);
            if (buttonUpgradeDamage != null) SetTransparentButton(buttonUpgradeDamage);
            if (buttonExit != null) SetTransparentButton(buttonExit);
            if (button1 != null) SetTransparentButton(button1);

            if (textBoxGold != null) SetTransparentTextBox(textBoxGold);
            if (textBox1 != null) SetTransparentTextBox(textBox1);
            if (textBox2 != null) SetTransparentTextBox(textBox2);
            if (textBox3 != null) SetTransparentTextBox(textBox3);

            if (labelWelcome != null) SetTransparentLabel(labelWelcome);
        }

        // ===== Helpers: dùng kiểu đầy đủ để diệt xung đột kiểu =====

        private void SetTransparentButton(System.Windows.Forms.Button button)
        {
            // Style tối giản, không đòi hỏi library ngoài
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.Transparent;
            button.ForeColor = Color.FromArgb(0, 192, 192);
            button.UseVisualStyleBackColor = false;

            button.MouseEnter += (_, __) =>
            {
                button.ForeColor = Color.White;
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            };

            button.MouseLeave += (_, __) =>
            {
                button.ForeColor = Color.FromArgb(0, 192, 192);
                button.FlatAppearance.BorderSize = 0;
            };
        }

        private void SetTransparentLabel(System.Windows.Forms.Label label)
        {
            label.BackColor = Color.Transparent;
            label.ForeColor = Color.FromArgb(0, 192, 192);
            label.Parent = this;
            label.BringToFront();
        }

        private void SetTransparentTextBox(System.Windows.Forms.TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.FromArgb(20, 20, 20);
            textBox.ForeColor = Color.FromArgb(0, 192, 192);
        }

        // ====== Handlers nút bấm (giữ nguyên tên theo Designer) ======

        // Chơi solo (nút Play)
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new GAMESOLO();
                form.Show();
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
            // TODO: mở form/phòng online nếu có
            var form = new GAMESOLO(); // tạm mở solo để tránh crash nếu chưa có form khác
            form.Show();
        }
    }
}

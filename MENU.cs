using System;
using System.Drawing;
using System.Windows.Forms;

<<<<<<< HEAD
namespace plan_fighting_super_start
{
    public partial class Menu : Form
    {
        public Menu()
=======
namespace Kien
{
    public partial class Form3 : Form
    {
        public Form3()
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
        {
            InitializeComponent();
        }

<<<<<<< HEAD
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
=======
        private void Form3_Load(object sender, EventArgs e)
        {
            // Hiển thị thông tin người chơi
            labelWelcome.Text = $"Xin chào";
            Database.LoadAccountData(AccountData.Username);

            // Cập nhật TextBox
            textBoxGold.Text = AccountData.Gold.ToString();
            textBox1.Text = AccountData.UpgradeHP.ToString();
            textBox2.Text = AccountData.UpgradeDamage.ToString();
            textBox3.Text = AccountData.Level.ToString();

            // Làm trong suốt
            SetTransparentButton(buttonPlay);
            SetTransparentButton(buttonUpgradeHP);
            SetTransparentButton(buttonUpgradeDamage);
            SetTransparentButton(buttonExit);
            SetTransparentButton(button1);

            SetTransparentTextBox(textBoxGold);
            SetTransparentTextBox(textBox1);
            SetTransparentTextBox(textBox2);
            SetTransparentTextBox(textBox3);

            SetTransparentLabel(labelWelcome);
        }

        // Button trong suốt
        private void SetTransparentButton(Button button)
        {
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.Transparent;
            button.ForeColor = Color.FromArgb(0, 192, 192);
            button.UseVisualStyleBackColor = false;
<<<<<<< HEAD

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
=======
            button.MouseEnter += (s, e) =>
            {
                button.ForeColor = Color.White;   // chữ sáng hơn
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192); // thêm viền sáng
            };

            button.MouseLeave += (s, e) =>
            {
                button.ForeColor = Color.FromArgb(0, 192, 192);    // về lại màu cũ
                button.FlatAppearance.BorderSize = 0; // bỏ viền
            };
        }

        // Label trong suốt
        private void SetTransparentLabel(Label label)
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
        {
            label.BackColor = Color.Transparent;
            label.ForeColor = Color.FromArgb(0, 192, 192);
            label.Parent = this;
            label.BringToFront();
        }

<<<<<<< HEAD
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
=======
        // TextBox style
        private void SetTransparentTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.None;
            textBox.ForeColor = Color.FromArgb(0, 192, 192);
            textBox.ReadOnly = true;
        }

        // Nút chơi BOSS
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            // 👉 Chỉnh logic ở đây
            Form4 gameForm = new Form4();
            gameForm.Owner = this;
            gameForm.Show();
            this.Hide();
        }

        // Update UI khi chơi xong
        public void UpdateGoldUI()
        {
            textBoxGold.Text = AccountData.Gold.ToString();
            textBox1.Text = AccountData.UpgradeHP.ToString();
            textBox2.Text = AccountData.UpgradeDamage.ToString();
            textBox3.Text = AccountData.Level.ToString();
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
        }

        // Nâng HP
        private void buttonUpgradeHP_Click(object sender, EventArgs e)
        {
            if (AccountData.Gold >= 10)
            {
                AccountData.Gold -= 10;
                AccountData.UpgradeHP += 20;

<<<<<<< HEAD
                if (textBoxGold != null) textBoxGold.Text = AccountData.Gold.ToString();
                if (textBox1 != null) textBox1.Text = AccountData.UpgradeHP.ToString();

                try { Database.UpdateAccountData(); } catch { }
            }
            else
            {
                MessageBox.Show("Không đủ vàng để nâng HP!");
=======
                textBoxGold.Text = AccountData.Gold.ToString();
                textBox1.Text = AccountData.UpgradeHP.ToString();

                Database.UpdateAccountData();
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
            }
        }

        // Nâng Damage
        private void buttonUpgradeDamage_Click(object sender, EventArgs e)
        {
            if (AccountData.Gold >= 15)
            {
                AccountData.Gold -= 15;
                AccountData.UpgradeDamage += 5;

<<<<<<< HEAD
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
=======
                textBoxGold.Text = AccountData.Gold.ToString();
                textBox2.Text = AccountData.UpgradeDamage.ToString();

                Database.UpdateAccountData();
            }
        }

        // Thoát
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

<<<<<<< HEAD
        // Chơi với người (button1)
        private void button1_Click(object sender, EventArgs e)
        {
            // TODO: mở form/phòng online nếu có
            var form = new GAMESOLO(); // tạm mở solo để tránh crash nếu chưa có form khác
            form.Show();
=======
        // Chơi với người
        private void button1_Click(object sender, EventArgs e)
        {
            // 👉 Chỉnh logic ở đây
            Form5 form5 = new Form5();
            form5.Show();
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
        }
    }
}

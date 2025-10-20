using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kien
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

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
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.Transparent;
            button.ForeColor = Color.FromArgb(0, 192, 192);
            button.UseVisualStyleBackColor = false;
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
        {
            label.BackColor = Color.Transparent;
            label.ForeColor = Color.FromArgb(0, 192, 192);
            label.Parent = this;
            label.BringToFront();
        }

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
        }

        // Nâng HP
        private void buttonUpgradeHP_Click(object sender, EventArgs e)
        {
            if (AccountData.Gold >= 10)
            {
                AccountData.Gold -= 10;
                AccountData.UpgradeHP += 20;

                textBoxGold.Text = AccountData.Gold.ToString();
                textBox1.Text = AccountData.UpgradeHP.ToString();

                Database.UpdateAccountData();
            }
        }

        // Nâng Damage
        private void buttonUpgradeDamage_Click(object sender, EventArgs e)
        {
            if (AccountData.Gold >= 15)
            {
                AccountData.Gold -= 15;
                AccountData.UpgradeDamage += 5;

                textBoxGold.Text = AccountData.Gold.ToString();
                textBox2.Text = AccountData.UpgradeDamage.ToString();

                Database.UpdateAccountData();
            }
        }

        // Thoát
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Chơi với người
        private void button1_Click(object sender, EventArgs e)
        {
            // 👉 Chỉnh logic ở đây
            Form5 form5 = new Form5();
            form5.Show();
        }
    }
}

using System;
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
            labelWelcome.Text = $"Xin chào, {AccountData.Username}!";
            Database.LoadAccountData(AccountData.Username);

            // Cập nhật UI
            textBoxGold.Text = AccountData.Gold.ToString();
            textBox1.Text = AccountData.UpgradeHP.ToString();     // HP
            textBox2.Text = AccountData.UpgradeDamage.ToString(); // Damage
            textBox3.Text = AccountData.Level.ToString();         // Hiển thị số ải
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Form4 gameForm = new Form4();
            gameForm.Owner = this;  // Gắn Owner để Form4 có thể gọi ngược về Form3
            gameForm.Show();
            this.Hide();
        }

        // Hàm cập nhật UI sau khi chơi xong Form4
        public void UpdateGoldUI()
        {
            textBoxGold.Text = AccountData.Gold.ToString();
            textBox1.Text = AccountData.UpgradeHP.ToString();
            textBox2.Text = AccountData.UpgradeDamage.ToString();
            textBox3.Text = AccountData.Level.ToString();  // Cập nhật số ải
        }

        private void buttonUpgradeHP_Click(object sender, EventArgs e)
        {
            if (AccountData.Gold >= 10)
            {
                AccountData.Gold -= 10;
                AccountData.UpgradeHP += 20;

                // Cập nhật UI
                textBoxGold.Text = AccountData.Gold.ToString();
                textBox1.Text = AccountData.UpgradeHP.ToString();

                Database.UpdateAccountData();
            }
        }

        private void buttonUpgradeDamage_Click(object sender, EventArgs e)
        {
            if (AccountData.Gold >= 15)
            {
                AccountData.Gold -= 15;
                AccountData.UpgradeDamage += 5;

                // Cập nhật UI
                textBoxGold.Text = AccountData.Gold.ToString();
                textBox2.Text = AccountData.UpgradeDamage.ToString();

                Database.UpdateAccountData();
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void textBoxGold_TextChanged(object sender, EventArgs e) { }
    }
}

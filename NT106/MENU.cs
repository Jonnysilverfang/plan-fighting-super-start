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
            if (buttonPlay != null) SetBoss
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new GAMEBOSS();
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
            if (AccouRoom
            var form = new Room(); 
            form.Show();
        }
    }
}

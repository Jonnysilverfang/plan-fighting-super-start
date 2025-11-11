using System;
using System.Drawing;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Nếu muốn xài placeholder mặc định thì có thể bỏ luôn đoạn này.
            // Ở đây mình set cho cả 3 textbox cho chắc.

            textBoxUser.Text = "Tên đăng nhập";
            textBoxUser.ForeColor = Color.Gray;

            textBoxEmail.Text = "Gmail";
            textBoxEmail.ForeColor = Color.Gray;

            textBoxPass.Text = "Mật khẩu";
            textBoxPass.ForeColor = Color.Gray;
            textBoxPass.UseSystemPasswordChar = false;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string user = textBoxUser.Text.Trim();
            string pass = textBoxPass.Text.Trim();
            string email = textBoxEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(user) || user == "Tên đăng nhập" ||
                string.IsNullOrWhiteSpace(pass) || pass == "Mật khẩu" ||
                string.IsNullOrWhiteSpace(email) || email == "Gmail")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập, Mật khẩu và Gmail!");
                return;
            }

            // Check format email đơn giản
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Gmail không hợp lệ, vui lòng kiểm tra lại!");
                return;
            }

            bool success = Database.RegisterAccount(user, pass, email);
            if (success)
            {
                MessageBox.Show("Đăng ký thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại hoặc Email bị trùng!");
            }
        }

        private void textBoxPass_TextChanged(object sender, EventArgs e)
        {
            // Nếu muốn: khi user bắt đầu gõ thì bật ẩn password, v.v…
            if (textBoxPass.ForeColor == Color.Gray && textBoxPass.Text == "Mật khẩu")
            {
                textBoxPass.Text = "";
                textBoxPass.ForeColor = Color.FromArgb(0, 192, 192);
                textBoxPass.UseSystemPasswordChar = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // nếu muốn: pictureBox1 làm nút đóng form hoặc quay lại
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kien
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // Placeholder Username
            textBoxUser.Text = "Tên đăng nhập";
            textBoxUser.ForeColor = Color.Gray;
            textBoxPass.Text = "Mật khẩu";
            textBoxPass.ForeColor = Color.Gray;
            textBoxPass.UseSystemPasswordChar = false;
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string user = textBoxUser.Text.Trim();
            string pass = textBoxPass.Text.Trim();

            if (user == "Tên đăng nhập" || pass == "Mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ!");
                return;
            }

            bool success = Database.RegisterAccount(user, pass);
            if (success)
            {
                MessageBox.Show("Đăng ký thành công!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại!");
            }
        }
    }
}
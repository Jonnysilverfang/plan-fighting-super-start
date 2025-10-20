using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Placeholder cho Username
            textBoxUser.Text = "Tên đăng nhập";
            textBoxUser.ForeColor = Color.Gray;

            // Placeholder cho Password
            textBoxPass.Text = "Mật khẩu";
            textBoxPass.ForeColor = Color.Gray;
            textBoxPass.UseSystemPasswordChar = false;

            // --- XỬ LÝ PLACEHOLDER USERNAME ---
            textBoxUser.Enter += (s, ev) =>
            {
                if (textBoxUser.Text == "Tên đăng nhập")
                {
                    textBoxUser.Text = "";
                    textBoxUser.ForeColor = Color.Black;
                }
            };
            textBoxUser.Leave += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(textBoxUser.Text))
                {
                    textBoxUser.Text = "Tên đăng nhập";
                    textBoxUser.ForeColor = Color.Gray;
                }
            };

            // --- XỬ LÝ PLACEHOLDER PASSWORD ---
            textBoxPass.Enter += (s, ev) =>
            {
                if (textBoxPass.Text == "Mật khẩu")
                {
                    textBoxPass.Text = "";
                    textBoxPass.ForeColor = Color.Black;
                }
                textBoxPass.UseSystemPasswordChar = !checkBoxShow.Checked;
            };

            textBoxPass.Leave += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(textBoxPass.Text))
                {
                    textBoxPass.Text = "Mật khẩu";
                    textBoxPass.ForeColor = Color.Gray;
                    textBoxPass.UseSystemPasswordChar = false;
                }
            };

            // --- HIỆN/ẨN MẬT KHẨU ---
            checkBoxShow.CheckedChanged += (s, ev) =>
            {
                if (textBoxPass.Text != "Mật khẩu")
                    textBoxPass.UseSystemPasswordChar = !checkBoxShow.Checked;
            };
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUser.Text.Trim();
            string password = textBoxPass.Text.Trim();

            if (username == "Tên đăng nhập" || password == "Mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool loginSuccess = Database.CheckLogin(username, password);

            if (loginSuccess)
            {
                Database.LoadAccountData(username);
                AccountData.Username = username;
                AccountData.Password = password;

                MessageBox.Show("Đăng nhập thành công!");

                Form3 form3 = new Form3();
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu sai!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}

using System;
<<<<<<< HEAD
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class Login : Form
    {
        public Login()
=======
using System.Drawing;
using System.Windows.Forms;

namespace Kien
{
    public partial class Form1 : Form
    {
        public Form1()
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
        {
            InitializeComponent();
        }

<<<<<<< HEAD
        private void Login_Load(object sender, EventArgs e)
        {
           
            // ⚠️ Bỏ qua khi đang ở chế độ Design để tránh lỗi Designer
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            // --- Placeholder cho Username ---
            textBoxUser.Text = "Tên đăng nhập";
            textBoxUser.ForeColor = Color.Gray;

            // --- Placeholder cho Password ---
=======
        private void Form1_Load(object sender, EventArgs e)
        {
            // Placeholder cho Username
            textBoxUser.Text = "Tên đăng nhập";
            textBoxUser.ForeColor = Color.Gray;

            // Placeholder cho Password
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
            textBoxPass.Text = "Mật khẩu";
            textBoxPass.ForeColor = Color.Gray;
            textBoxPass.UseSystemPasswordChar = false;

<<<<<<< HEAD
            // --- Khi nhập username ---
=======
            // --- XỬ LÝ PLACEHOLDER USERNAME ---
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
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

<<<<<<< HEAD
            // --- Khi nhập password ---
=======
            // --- XỬ LÝ PLACEHOLDER PASSWORD ---
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
            textBoxPass.Enter += (s, ev) =>
            {
                if (textBoxPass.Text == "Mật khẩu")
                {
                    textBoxPass.Text = "";
                    textBoxPass.ForeColor = Color.Black;
                }
                textBoxPass.UseSystemPasswordChar = !checkBoxShow.Checked;
            };
<<<<<<< HEAD
=======

>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
            textBoxPass.Leave += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(textBoxPass.Text))
                {
                    textBoxPass.Text = "Mật khẩu";
                    textBoxPass.ForeColor = Color.Gray;
                    textBoxPass.UseSystemPasswordChar = false;
                }
            };

<<<<<<< HEAD
            // --- Hiện/ẩn mật khẩu ---
=======
            // --- HIỆN/ẨN MẬT KHẨU ---
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
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

<<<<<<< HEAD
            if (username == "Tên đăng nhập" || password == "Mật khẩu" ||
                username == "" || password == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi",
=======
            if (username == "Tên đăng nhập" || password == "Mật khẩu")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ!", "Lỗi",
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool loginSuccess = Database.CheckLogin(username, password);

            if (loginSuccess)
            {
<<<<<<< HEAD
                MessageBox.Show("Đăng nhập thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Menu form3 = new Menu();
=======
                Database.LoadAccountData(username);
                AccountData.Username = username;
                AccountData.Password = password;

                MessageBox.Show("Đăng nhập thành công!");

                Form3 form3 = new Form3();
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
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
<<<<<<< HEAD
            Register form2 = new Register();
            form2.ShowDialog();
        }



=======
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
>>>>>>> 88a3da28403503078ef20e92d9801821b2664c55
    }
}

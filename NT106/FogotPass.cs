using System;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class FogotPass : Form
    {
        private bool codeSent = false;

        public FogotPass()
        {
            InitializeComponent();
            // ban đầu khóa phần nhập code + mật khẩu mới
            SetResetControlsEnabled(false);
        }

        private void SetResetControlsEnabled(bool enabled)
        {
            textBoxCode.Enabled = enabled;
            textBoxNewPass.Enabled = enabled;
            textBoxConfirmPass.Enabled = enabled;
            buttonConfirm.Enabled = enabled;
        }

        private void buttonSendCode_Click(object sender, EventArgs e)
        {
            string username = textBoxUser.Text.Trim();
            string email = textBoxEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Gmail!");
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Gmail không hợp lệ, vui lòng kiểm tra lại!");
                return;
            }

            bool success = Database.RequestResetCode(username, email);

            if (success)
            {
                MessageBox.Show("Đã gửi mã xác minh tới Gmail của bạn. Vui lòng kiểm tra hộp thư!");

                codeSent = true;
                SetResetControlsEnabled(true);
                textBoxCode.Focus();
            }
            else
            {
                // Lỗi đã được hiển thị bên trong Database.RequestResetCode
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (!codeSent)
            {
                MessageBox.Show("Vui lòng bấm 'Gửi mã' trước khi đổi mật khẩu.");
                return;
            }

            string username = textBoxUser.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            string code = textBoxCode.Text.Trim();
            string newPass = textBoxNewPass.Text;
            string confirm = textBoxConfirmPass.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Thiếu Tên đăng nhập hoặc Gmail. Vui lòng nhập lại!");
                return;
            }

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Vui lòng nhập mã xác minh đã gửi qua Gmail!");
                textBoxCode.Focus();
                return;
            }

            if (string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!");
                textBoxNewPass.Focus();
                return;
            }

            if (newPass != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!");
                textBoxConfirmPass.Focus();
                return;
            }

            bool success = Database.ConfirmResetPassword(username, email, code, newPass);

            if (success)
            {
                MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại với mật khẩu mới.");
                this.Close();
            }
            else
            {
                // Lỗi chi tiết đã show trong Database.ConfirmResetPassword
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using NT106;
using System;
using System.Windows.Forms;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace plan_fighting_super_start
{
    public partial class ResetPasswordVerify : Form
    {
        private readonly string _email;

        public ResetPasswordVerify(string email)
        {
            _email = email;
            InitializeComponent();
            lblEmail.Text = email; // hiển thị email để user biết đang reset cho tài khoản nào
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            string code = txtCode.Text.Trim();
            string newPass = txtNewPassword.Text;
            string confirm = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Vui lòng nhập mã xác minh đã gửi qua email.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCode.Focus();
                return;
            }

            if (string.IsNullOrEmpty(newPass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNewPassword.Focus();
                return;
            }

            if (newPass != confirm)
            {
                MessageBox.Show("Mật khẩu nhập lại không trùng khớp.",
                    "Kiểm tra mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return;
            }

            ToggleBusy(true);
            lblStatus.Text = "Đang xác nhận…";

            var result = await CognitoResetService.ConfirmResetAsync(_email, code, newPass);
            bool ok = result.Item1;
            string msg = result.Item2;

            ToggleBusy(false);
            lblStatus.Text = msg;

            if (ok)
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    // Quay về màn hình đăng nhập nếu có
                    var login = new Login();
                    login.StartPosition = FormStartPosition.CenterScreen;
                    login.Show();
                }
                catch { /* nếu chưa có form Login thì bỏ qua */ }

                this.Close();
            }
            else
            {
                MessageBox.Show(msg, "Không thể đặt lại mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            bool show = chkShowPassword.Checked;
            txtNewPassword.UseSystemPasswordChar = !show;
            txtConfirmPassword.UseSystemPasswordChar = !show;
        }

        private void ToggleBusy(bool busy)
        {
            btnConfirm.Enabled = !busy;
            txtCode.Enabled = !busy;
            txtNewPassword.Enabled = !busy;
            txtConfirmPassword.Enabled = !busy;
            chkShowPassword.Enabled = !busy;
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
        }
    }
}

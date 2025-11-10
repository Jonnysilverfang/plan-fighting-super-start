using NT106;
using System;
using System.Windows.Forms;
namespace plan_fighting_super_start
{
    public partial class Resetpassword : Form
    {
        // Đếm ngược (giây)
        private int _cooldownSeconds = 45;
        private int _remaining = 0;

        public Resetpassword()
        {
            InitializeComponent();
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập email đã đăng ký.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return;
            }

            ToggleSending(true);
            lblStatus.Text = "Đang gửi mã xác minh…";

            // KHÔNG dùng deconstruction để tránh CS8130
            var result = await CognitoResetService.SendResetCodeAsync(email);
            bool ok = result.Item1;
            string msg = result.Item2;

            lblStatus.Text = msg;
            ToggleSending(false);

            if (ok)
            {
                StartCooldown();
                // Nếu bạn đã có form xác nhận, mở nó ở đây
                try
                {
                    var verify = new ResetPasswordVerify(email);
                    verify.StartPosition = FormStartPosition.CenterParent;
                    verify.Show();
                    this.Close(); // hoặc Hide()
                }
                catch
                {
                    // Bỏ qua nếu form Verify chưa sẵn sàng
                }
            }
        }

        private async void btnResend_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập email đã đăng ký.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return;
            }

            ToggleSending(true);
            lblStatus.Text = "Đang gửi lại mã…";

            var result = await CognitoResetService.SendResetCodeAsync(email);
            bool ok = result.Item1;
            string msg = result.Item2;

            lblStatus.Text = msg;
            ToggleSending(false);

            if (ok)
            {
                StartCooldown();
            }
        }

        private void timerCooldown_Tick(object sender, EventArgs e)
        {
            if (_remaining > 0)
            {
                _remaining--;
                btnResend.Text = $"Gửi lại mã ({_remaining}s)";
                btnResend.Enabled = false;
            }
            else
            {
                timerCooldown.Stop();
                btnResend.Text = "Gửi lại mã";
                btnResend.Enabled = true;
            }
        }

        private void StartCooldown()
        {
            _remaining = _cooldownSeconds;
            btnResend.Enabled = false;
            btnResend.Text = $"Gửi lại mã ({_remaining}s)";
            timerCooldown.Start();
        }

        private void ToggleSending(bool sending)
        {
            btnSend.Enabled = !sending;
            btnResend.Enabled = !sending && _remaining <= 0;
            txtEmail.Enabled = !sending;
            Cursor = sending ? Cursors.WaitCursor : Cursors.Default;
        }
    }
}

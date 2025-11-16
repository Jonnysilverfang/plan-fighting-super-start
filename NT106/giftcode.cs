using System;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class giftcode : Form
    {
        public giftcode()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Nếu không cần xử lý gì khi text thay đổi,
            // có thể để trống như này.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string code = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show(
                    "Vui lòng nhập giftcode.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                textBox1.Focus();
                return;
            }

            // TODO: thay bằng logic kiểm tra giftcode thật (API, database, v.v.)
            // Demo: code "ABC123" là hợp lệ
            if (string.Equals(code, "ABC123", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(
                    "Giftcode hợp lệ! Bạn đã nhận được phần thưởng.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Ví dụ sau khi thành công:
                // textBox1.Clear();
                // this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Giftcode không hợp lệ hoặc đã được sử dụng.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                textBox1.SelectAll();
                textBox1.Focus();
            }
        }
    }
}

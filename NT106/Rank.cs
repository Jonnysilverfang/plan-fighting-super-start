using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Forms;
using System.Collections.Generic;

namespace plan_fighting_super_start
{
    public partial class Rank : Form
    {
        // ▼ Client gọi API
        private static readonly HttpClient http = new HttpClient();

        // ▼ API bảng xếp hạng
        private const string URL_BANGXEPHANG = "https://f1oj97uhee.execute-api.ap-southeast-1.amazonaws.com";

        // ▼ Hỗ trợ đọc JSON không phân biệt hoa thường
        private readonly JsonSerializerOptions tuyChonJson = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // ▼ Model item trong bảng xếp hạng
        private class DongXepHang
        {
            public string Username { get; set; } = "";
            public int Level { get; set; }
            public int Rank { get; set; }
        }

        // ▼ Dữ liệu GET trả về
        private class PhanHoiGET
        {
            public bool ok { get; set; }
            public List<DongXepHang> ranking { get; set; } = new();
        }

        // ▼ Dữ liệu POST trả về
        private class PhanHoiPOST
        {
            public bool ok { get; set; }
            public string message { get; set; } = "";
            public int currentLevel { get; set; }
            public List<DongXepHang> ranking { get; set; } = new();
        }

        // --------------------------------------------------

        public Rank()
        {
            InitializeComponent();

            // Điền username và level hiện có (từ Database)
            if (!string.IsNullOrWhiteSpace(AccountData.Username))
            {
                txtUser.Text = AccountData.Username;
                txtUser.ReadOnly = true;
                numLevel.Value = AccountData.Level;
            }

            this.Shown += async (_, __) => await TaiTopAsync(10);
        }

        // ---------------- HÀM HỖ TRỢ ----------------

        // Tải TOP N
        private async System.Threading.Tasks.Task TaiTopAsync(int soLuong)
        {
            try
            {
                statusLabel.Text = $"Đang tải TOP {soLuong} ...";

                var json = await http.GetStringAsync($"{URL_BANGXEPHANG}/get?limit={soLuong}");
                var duLieu = JsonSerializer.Deserialize<PhanHoiGET>(json, tuyChonJson);

                dgv.DataSource = duLieu!.ranking;
                statusLabel.Text = $"Đã tải TOP {soLuong}!";
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Lỗi tải bảng xếp hạng: " + ex.Message;
            }
        }

        // Gửi level lên server
        private async System.Threading.Tasks.Task GuiDiemLenMayChu(string ten, int level)
        {
            var resp = await http.PostAsJsonAsync($"{URL_BANGXEPHANG}/post", new { username = ten, level });
            resp.EnsureSuccessStatusCode();

            var duLieu = await resp.Content.ReadFromJsonAsync<PhanHoiPOST>(tuyChonJson);

            dgv.DataSource = duLieu!.ranking;
            statusLabel.Text = $"Cập nhật thành công! Level hiện tại: {duLieu.currentLevel}";
        }

        // ------------------ SỰ KIỆN ------------------

        // Nút Gửi Điểm
        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string ten = txtUser.Text.Trim();
                int level = (int)numLevel.Value;

                if (string.IsNullOrWhiteSpace(ten))
                {
                    MessageBox.Show("Vui lòng nhập Username!");
                    return;
                }

                statusLabel.Text = "Đang gửi ...";

                // Đồng bộ Level với tài khoản đang dùng
                if (ten == AccountData.Username)
                {
                    AccountData.Level = level;
                    Database.UpdateAccountData();
                }

                await GuiDiemLenMayChu(ten, level);
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Lỗi gửi điểm: " + ex.Message;
            }
        }

        // Nút Tải TOP
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await TaiTopAsync(10);
        }
    }
}

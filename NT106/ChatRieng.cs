using System;
using System.Drawing;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class ChatRieng : Form
    {
        private readonly string _ban;   // username người bên kia
        private readonly string _toi;   // username của mình
        private ChatSanhLAN? _kenhLan;

        public ChatRieng(string tenBan)
        {
            _ban = tenBan ?? "";
            _toi = AccountData.Username ?? "me";
            InitializeComponent();
        }

        private void ChatRieng_Load(object sender, EventArgs e)
        {
            lblTieuDe.Text = $"NHẮN VỚI  {_ban.ToUpper()}";

            _kenhLan = new ChatSanhLAN();
            _kenhLan.BatDauNghe();
            _kenhLan.NhanTinDM += XuLyTinNhanDM;
        }

        private void ChatRieng_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (_kenhLan != null)
                {
                    _kenhLan.NhanTinDM -= XuLyTinNhanDM;
                    _kenhLan.Dispose();
                }
            }
            catch { }
        }

        // Nhận tin DM từ ChatSanhLAN
        private void XuLyTinNhanDM(string tu, string den, string noiDung)
        {
            // Chỉ nhận tin thuộc cặp (_toi <-> _ban)
            bool dungCuoc =
                (string.Equals(tu, _ban, StringComparison.OrdinalIgnoreCase) &&
                 string.Equals(den, _toi, StringComparison.OrdinalIgnoreCase))
             || (string.Equals(tu, _toi, StringComparison.OrdinalIgnoreCase) &&
                 string.Equals(den, _ban, StringComparison.OrdinalIgnoreCase));

            if (!dungCuoc) return;

            ChenDong(tu, noiDung, tu.Equals(_toi, StringComparison.OrdinalIgnoreCase));
        }

        // Gửi tin
        private async void btnGui_Click(object sender, EventArgs e)
        {
            string msg = (txtNoiDung.Text ?? "").Trim();
            if (msg.Length == 0) return;

            try
            {
                if (_kenhLan == null)
                {
                    _kenhLan = new ChatSanhLAN();
                    _kenhLan.BatDauNghe();
                    _kenhLan.NhanTinDM += XuLyTinNhanDM;
                }

                await _kenhLan.GuiTinDMAsync(_toi, _ban, msg);
            }
            catch
            {
                // tránh crash nếu lỗi mạng
            }

            // Hiện luôn tin của mình
            ChenDong(_toi, msg, true);
            txtNoiDung.Clear();
        }

        // Thêm 1 dòng vào khung chat
        private void ChenDong(string ai, string text, bool laCuaToi)
        {
            if (rtbHopThoai.InvokeRequired)
            {
                rtbHopThoai.Invoke(new Action(() => ChenDong(ai, text, laCuaToi)));
                return;
            }

            if (rtbHopThoai.TextLength > 0)
                rtbHopThoai.AppendText(Environment.NewLine);

            string nhan = laCuaToi ? "[TÔI] " : "[BẠN] ";
            Color mauNhan = laCuaToi ? Color.Lime : Color.Cyan;
            Color mauTen = laCuaToi ? Color.White : Color.Gold;

            int start = rtbHopThoai.TextLength;

            // [TÔI] / [BẠN]
            rtbHopThoai.AppendText(nhan);
            rtbHopThoai.Select(start, nhan.Length);
            rtbHopThoai.SelectionColor = mauNhan;

            // Tên
            string phanTen = ai + ": ";
            rtbHopThoai.AppendText(phanTen);
            rtbHopThoai.Select(start + nhan.Length, phanTen.Length);
            rtbHopThoai.SelectionColor = mauTen;

            // Nội dung
            rtbHopThoai.AppendText(text);
            rtbHopThoai.SelectionColor = Color.White;

            // Cuộn xuống cuối
            rtbHopThoai.SelectionStart = rtbHopThoai.TextLength;
            rtbHopThoai.ScrollToCaret();
        }
    }
}

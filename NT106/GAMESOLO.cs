using System;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class GAMESOLO : Form
    {
        private NetworkManager _network;
        private bool _isHost;
        private string _roomId;

        // ===== Overloads để tránh lỗi CS7036 =====

        // Mặc định: tạo game solo, tự tạo NetworkManager
        public GAMESOLO()
            : this(new NetworkManager(), true, "SOLO-" + Guid.NewGuid().ToString("N").Substring(0, 6))
        { }

        // Cho phép truyền sẵn NetworkManager, còn lại mặc định solo
        public GAMESOLO(NetworkManager network)
            : this(network, true, "SOLO-" + Guid.NewGuid().ToString("N").Substring(0, 6))
        { }

        // Constructor “chuẩn” đang được Room.cs sử dụng
        public GAMESOLO(NetworkManager network, bool isHost, string roomId)
        {
            InitializeComponent();

            _network = network ?? new NetworkManager();
            _isHost = isHost;
            _roomId = string.IsNullOrWhiteSpace(roomId) ? "SOLO" : roomId;

            // Một vài thiết lập an toàn cho WinForms
            this.KeyPreview = true;   // nếu sau này bạn muốn bắt phím
            this.DoubleBuffered = true;

            // Thể hiện trạng thái trên tiêu đề form
            this.Text = (_isHost ? "[HOST] " : "[CLIENT] ") + "Room: " + _roomId;

            // TODO: nếu bạn có logic khởi tạo mạng, đặt vào try-catch này
            try
            {
                // Ví dụ:
                // if (_isHost) _network.StartHost(_roomId);
                // else         _network.Join(_roomId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không khởi tạo được mạng: " + ex.Message);
            }
        }

        // ====== Event handlers mà Designer đang gắn ======

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Thoát về menu/đóng game
            this.Close();
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Nếu NetworkManager có Dispose/Disconnect thì gọi ở đây.
                // Dùng try-catch để đảm bảo không văng lỗi khi đóng form.
                (_network as IDisposable)?.Dispose();
                _network = null;
            }
            catch { /* bỏ qua lỗi khi đóng */ }
        }

        // ===== (tuỳ chọn) bạn có thể bổ sung OnPaint/Timer/KeyDown sau này =====
    }
}

using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class Room : Form
    {
        private NetworkManager networkManager;
        private bool isHost;
        private string currentRoomId;
        private const int GAME_PORT = 8888; // giữ lại cho đủ tham số, không dùng nữa

        public Room()
        {
            InitializeComponent();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { networkManager?.Dispose(); } catch { }
        }

        private void SetStatus(string message)
        {
            if (InvokeRequired) Invoke(new Action(() => lblStatus.Text = message));
            else lblStatus.Text = message;
        }

        private static string MakeRoomId()
        {
            var rnd = new Random();
            return rnd.Next(100000, 999999).ToString();
        }

        private void WireNetworkEvents()
        {
            if (networkManager == null) return;

            networkManager.OnPeerConnected += () =>
            {
                UI(() =>
                {
                    btnStartGame.Enabled = true;
                    SetStatus($"Phòng {currentRoomId}: ĐÃ ĐỦ 2 NGƯỜI. Bạn có thể bấm Bắt đầu.");
                });
            };

            networkManager.OnMessageReceived += (msg) =>
            {
                if (msg == "START_GAME")
                {
                    UI(OpenGame);
                }
                else if (msg == "ROOM_NOT_FOUND")
                {
                    UI(() =>
                    {
                        SetStatus("Không tìm thấy phòng. Kiểm tra lại Room ID.");
                        btnJoinRoom.Enabled = true;
                    });
                }
            };

            networkManager.OnDisconnected += () =>
            {
                UI(() =>
                {
                    btnStartGame.Enabled = false;
                    SetStatus("Kết nối bị ngắt. Quay lại lobby hoặc tạo phòng khác.");
                });
            };
        }

        private void UI(Action a)
        {
            if (InvokeRequired) BeginInvoke(a);
            else a();
        }

        // ====== TẠO PHÒNG (HOST) ======
        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            try
            {
                if (!NetworkManager.IsNetworkAvailable())
                {
                    SetStatus("Không có mạng! Không thể tạo phòng.");
                    return;
                }

                currentRoomId = string.IsNullOrWhiteSpace(txtRoomID.Text)
                    ? MakeRoomId()
                    : txtRoomID.Text.Trim();
                txtRoomID.Text = currentRoomId;

                networkManager?.Dispose();
                networkManager = new NetworkManager();
                WireNetworkEvents();

                isHost = true;
                btnStartGame.Enabled = false;

                SetStatus($"Đang tạo phòng {currentRoomId} trên server...");
                networkManager.StartHost(currentRoomId, GAME_PORT);
                // Khi client join, Lambda sẽ gửi START_GAME → OnPeerConnected → nút BẮT ĐẦU sáng
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi tạo phòng: " + ex.Message);
            }
        }

        // ====== THAM GIA PHÒNG (CLIENT) ======
        private async void btnJoinRoom_Click(object sender, EventArgs e)
        {
            string roomId = txtRoomID.Text.Trim();
            if (string.IsNullOrEmpty(roomId))
            {
                SetStatus("Nhập Room ID trước!");
                return;
            }

            try
            {
                if (!NetworkManager.IsNetworkAvailable())
                {
                    SetStatus("Không có mạng! Không thể tham gia phòng.");
                    return;
                }

                currentRoomId = roomId;
                isHost = false;
                btnJoinRoom.Enabled = false;
                SetStatus($"Đang tham gia phòng {roomId} trên server...");

                networkManager?.Dispose();
                networkManager = new NetworkManager();
                WireNetworkEvents();

                await networkManager.JoinHost(currentRoomId, GAME_PORT);
                // Sau khi join, Lambda sẽ gửi JOIN_OK + START_GAME (nếu đủ 2 người)
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi tham gia phòng: " + ex.Message);
                btnJoinRoom.Enabled = true;
            }
        }

        // ====== NÚT BẮT ĐẦU (HOST BẤM) ======
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (networkManager == null || !networkManager.IsConnected)
            {
                SetStatus("Chưa kết nối với người chơi kia!");
                return;
            }

            // Host gửi tín hiệu START_GAME cho đối thủ
            networkManager.Send("START_GAME");
            OpenGame();
        }

        private void OpenGame()
        {
            var game = new GAMESOLO(networkManager, isHost, currentRoomId);
            game.Show();
            this.Hide();
            game.FormClosed += (_, __) =>
            {
                try
                {
                    this.Show();
                    SetStatus("Đã quay lại lobby.");
                }
                catch { }
            };
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AccountData.Username))
            {
                MessageBox.Show(
                    "Vui lòng đăng nhập trước khi xem lịch sử đấu.",
                    "Lỗi truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var form = new MatchHistoryForm();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở lịch sử đấu: " + ex.Message);
            }
        }
    }
}

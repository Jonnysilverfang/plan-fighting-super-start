using plan_fighting_super_start;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace plan_fighting_super_start

{
    public partial class Room : Form
    {
        private NetworkManager networkManager;
        private LANBroadcast lan;
        private bool isHost;
        private string currentRoomId;
        private const int GAME_PORT = 8888;

        public Room()
        {
            InitializeComponent();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { lan?.Dispose(); } catch { }
            try { networkManager?.Dispose(); } catch { }
        }

        private void SetStatus(string message)
        {
            if (InvokeRequired) { Invoke(new Action(() => lblStatus.Text = message)); }
            else lblStatus.Text = message;
        }

        private static string MakeRoomId()
        {
            var rnd = new Random();
            return rnd.Next(100000, 999999).ToString();
        }

        private void WireNetworkEvents()
        {
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

        private async void btnCreateRoom_Click(object sender, EventArgs e)
        {
            try
            {
                if (!NetworkManager.IsNetworkAvailable())
                {
                    SetStatus("Không có mạng LAN! Không thể tạo phòng.");
                    return;
                }

                // Tạo roomId ngẫu nhiên hoặc giữ giá trị đang nhập (nếu hợp lệ)
                currentRoomId = string.IsNullOrWhiteSpace(txtRoomID.Text)
                    ? MakeRoomId()
                    : txtRoomID.Text.Trim();

                txtRoomID.Text = currentRoomId;

                networkManager?.Dispose();
                networkManager = new NetworkManager();
                WireNetworkEvents();

                networkManager.StartHost(currentRoomId, GAME_PORT);
                isHost = true;

                lan?.Dispose();
                lan = new LANBroadcast();
                lan.StartBroadcast(currentRoomId, GAME_PORT);

                btnStartGame.Enabled = false;
                SetStatus($"Phòng đã tạo với ID: {currentRoomId}. Đang đợi người chơi...");
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi tạo phòng: " + ex.Message);
            }
        }

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
                    SetStatus("Không có mạng LAN! Không thể tham gia phòng.");
                    return;
                }

                currentRoomId = roomId;
                isHost = false;
                btnJoinRoom.Enabled = false;
                SetStatus($"Đang tìm phòng {roomId} trên LAN...");

                lan?.Dispose();
                lan = new LANBroadcast();
                lan.OnRoomFound += async (foundRoom, hostIP, port) =>
                {
                    if (foundRoom != roomId) return;

                    // Ngăn trùng lặp (broadcast có thể bắn nhiều lần)
                    lan?.Dispose();

                    // Chạy kết nối TCP ở thread nền
                    Task.Run(async () =>
                    {
                        try
                        {
                            networkManager?.Dispose();
                            networkManager = new NetworkManager();
                            WireNetworkEvents();

                            await networkManager.JoinHost(hostIP, port);

                            // => CẬP NHẬT UI PHẢI QUA UI(...)
                            UI(() =>
                            {
                                SetStatus($"Đã tham gia phòng {roomId} ({hostIP}). Chờ chủ phòng bấm Bắt đầu hoặc bạn có thể bấm luôn.");
                                btnJoinRoom.Enabled = true;
                                btnStartGame.Enabled = true;
                            });
                        }
                        catch (Exception ex)
                        {
                            UI(() =>
                            {
                                SetStatus("Kết nối tới host thất bại: " + ex.Message);
                                btnJoinRoom.Enabled = true;
                            });
                        }
                    });
                };
                lan.StartListen(roomId);
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi tham gia phòng: " + ex.Message);
                btnJoinRoom.Enabled = true;
            }
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (networkManager == null || !networkManager.IsConnected)
            {
                SetStatus("Chưa kết nối với người chơi kia!");
                return;
            }

            // Gửi tín hiệu mở game cho bên kia (để cả hai cùng vào Form6)
            networkManager.Send("START_GAME");
            OpenGame();
        }

        private void OpenGame()
        {
            var game = new GAMESOLO(networkManager, isHost, currentRoomId);
            // Để game tự quản lý đóng/mở; không dispose manager ở đây
            game.Show();
            this.Hide();
            game.FormClosed += (_, __) =>
            {
                // Khi đóng game, quay lại lobby (nếu còn sống), giữ kết nối để có thể vào lại 
                try
                {
                    this.Show();
                    SetStatus("Đã quay lại lobby.");
                }
                catch { }
            };
        }
    }
}

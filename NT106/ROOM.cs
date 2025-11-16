using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class Room : Form
    {
        private NetworkManager networkManager;
        private LANBroadcast lanBroadcast;

        private bool isHost;
        private string currentRoomId;

        // Cổng TCP cho game LAN
        private const int GAME_PORT = 9000;

        public Room()
        {
            InitializeComponent();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { networkManager?.Dispose(); } catch { }
            try { lanBroadcast?.Dispose(); } catch { }
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

        private void UI(Action a)
        {
            if (InvokeRequired) BeginInvoke(a);
            else a();
        }

        private void WireNetworkEvents()
        {
            if (networkManager == null) return;

            networkManager.OnPeerConnected += () =>
            {
                UI(() =>
                {
                    if (isHost)
                    {
                        btnStartGame.Enabled = true;
                        SetStatus($"Phòng {currentRoomId}: ĐÃ ĐỦ 2 NGƯỜI. Host có thể bấm BẮT ĐẦU.");
                    }
                    else
                    {
                        SetStatus($"Đã kết nối tới phòng {currentRoomId}. Chờ host bấm BẮT ĐẦU.");
                    }
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

        // ====== TẠO PHÒNG (HOST) ======
        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            try
            {
                if (!NetworkManager.IsNetworkAvailable())
                {
                    SetStatus("Không phát hiện mạng (LAN) khả dụng!");
                    return;
                }

                currentRoomId = string.IsNullOrWhiteSpace(txtRoomID.Text)
                    ? MakeRoomId()
                    : txtRoomID.Text.Trim();
                txtRoomID.Text = currentRoomId;

                // Dọn cũ
                try { networkManager?.Dispose(); } catch { }
                try { lanBroadcast?.Dispose(); } catch { }

                // Tạo server TCP
                networkManager = new NetworkManager();
                WireNetworkEvents();
                isHost = true;
                btnStartGame.Enabled = false;

                networkManager.StartHost(GAME_PORT);

                // Broadcast phòng cho client trong LAN
                lanBroadcast = new LANBroadcast();
                lanBroadcast.StartBroadcast(currentRoomId, GAME_PORT);

                SetStatus($"[HOST] Đã tạo phòng {currentRoomId}. Đang chờ người chơi khác trong LAN...");
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi tạo phòng (LAN): " + ex.Message);
            }
        }

        // ====== THAM GIA PHÒNG (CLIENT) ======
        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            string roomId = txtRoomID.Text.Trim();
            if (string.IsNullOrEmpty(roomId))
            {
                SetStatus("Nhập Room ID trước khi tham gia phòng!");
                return;
            }

            if (!NetworkManager.IsNetworkAvailable())
            {
                SetStatus("Không phát hiện mạng (LAN) khả dụng!");
                return;
            }

            currentRoomId = roomId;
            isHost = false;
            btnJoinRoom.Enabled = false;
            btnStartGame.Enabled = false;

            try { networkManager?.Dispose(); } catch { }
            try { lanBroadcast?.Dispose(); } catch { }

            lanBroadcast = new LANBroadcast();
            SetStatus($"[CLIENT] Đang tìm phòng {roomId} trong LAN...");

            // Khi tìm thấy host, kết nối TCP
            lanBroadcast.OnRoomFound += async (foundRoomId, hostIp, port) =>
            {
                // LANBroadcast đã lọc theo roomIdFilter, nhưng ta check lại cho chắc
                if (!string.Equals(foundRoomId, currentRoomId, StringComparison.Ordinal)) return;

                try
                {
                    // Dừng listen để tránh xử lý lặp
                    lanBroadcast.StopListen();

                    this.Invoke(new Action(() =>
                    {
                        SetStatus($"Tìm thấy host {hostIp}:{port}, đang kết nối...");
                    }));

                    networkManager = new NetworkManager();
                    WireNetworkEvents();

                    await networkManager.JoinHost(hostIp, port);

                    this.Invoke(new Action(() =>
                    {
                        SetStatus($"Đã kết nối tới host {hostIp}. Chờ host bấm BẮT ĐẦU.");
                    }));
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() =>
                    {
                        SetStatus("Lỗi kết nối host: " + ex.Message);
                        btnJoinRoom.Enabled = true;
                    }));
                }
            };

            // Bắt đầu nghe broadcast trong LAN
            lanBroadcast.StartListen(currentRoomId);
        }

        // ====== NÚT BẮT ĐẦU (HOST BẤM) ======
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (networkManager == null || !networkManager.IsConnected)
            {
                SetStatus("Chưa kết nối đủ 2 người!");
                return;
            }

            // Host gửi tín hiệu START_GAME cho client
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

        private void Room_Load(object sender, EventArgs e)
        {
            SetStatus("Chưa tạo/tham gia phòng.");
        }
    }
}

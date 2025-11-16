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

        private const int GAME_PORT = 9000; // TCP game port

        public Room()
        {
            InitializeComponent();
        }

        private void Room_Load(object sender, EventArgs e)
        {
            SetStatus("Chưa tạo/tham gia phòng.");
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { networkManager?.Dispose(); } catch { }
            try { lanBroadcast?.Dispose(); } catch { }
        }

        private void SetStatus(string msg)
        {
            if (InvokeRequired)
                Invoke(new Action(() => lblStatus.Text = msg));
            else
                lblStatus.Text = msg;
        }

        private static string MakeRoomId()
        {
            return new Random().Next(100000, 999999).ToString();
        }

        private void UI(Action a)
        {
            if (InvokeRequired) BeginInvoke(a); else a();
        }

        private void WireNetworkEvents()
        {
            if (networkManager == null) return;

            // Khi kết nối đủ 2 người
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

            // Nhận tín hiệu bắt đầu từ host
            networkManager.OnMessageReceived += (msg) =>
            {
                if (msg == "START_GAME")
                    UI(OpenGame);
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

        // ============================
        //         TẠO PHÒNG HOST
        // ============================
        private void btnCreateRoom_Click(object sender, EventArgs e)
        {
            try
            {
                if (!NetworkManager.IsNetworkAvailable())
                {
                    SetStatus("Không phát hiện mạng LAN khả dụng!");
                    return;
                }

                currentRoomId = string.IsNullOrWhiteSpace(txtRoomID.Text)
                    ? MakeRoomId()
                    : txtRoomID.Text.Trim();

                txtRoomID.Text = currentRoomId;

                try { networkManager?.Dispose(); } catch { }
                try { lanBroadcast?.Dispose(); } catch { }

                // Start TCP Host
                networkManager = new NetworkManager();
                WireNetworkEvents();
                isHost = true;
                btnStartGame.Enabled = false;

                networkManager.StartHost(GAME_PORT);

                // Broadcast LAN
                lanBroadcast = new LANBroadcast();
                lanBroadcast.StartBroadcast(currentRoomId, GAME_PORT);

                SetStatus($"[HOST] Đã tạo phòng {currentRoomId}. Đang chờ người chơi khác...");

                // Log lên DynamoDB
                var hostName = string.IsNullOrWhiteSpace(AccountData.Username) ? "Host" : AccountData.Username;
                _ = RoomLogger.LogHost(currentRoomId, hostName);
            }
            catch (Exception ex)
            {
                SetStatus("Lỗi tạo phòng (LAN): " + ex.Message);
            }
        }

        // ============================
        //         JOIN PHÒNG CLIENT
        // ============================
        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            string roomId = txtRoomID.Text.Trim();

            if (string.IsNullOrEmpty(roomId))
            {
                SetStatus("Nhập Room ID trước!");
                return;
            }

            if (!NetworkManager.IsNetworkAvailable())
            {
                SetStatus("Không phát hiện mạng LAN khả dụng!");
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

            // Khi tìm thấy Host trong LAN
            lanBroadcast.OnRoomFound += async (foundRoomId, hostIp, port) =>
            {
                if (foundRoomId != currentRoomId) return;

                try
                {
                    lanBroadcast.StopListen();

                    UI(() => SetStatus($"Tìm thấy host {hostIp}:{port}, đang kết nối..."));

                    networkManager = new NetworkManager();
                    WireNetworkEvents();

                    await networkManager.JoinHost(hostIp, port);

                    UI(() => SetStatus($"Đã kết nối tới host {hostIp}. Chờ host bấm BẮT ĐẦU."));

                    // Log lên DynamoDB
                    var guestName = string.IsNullOrWhiteSpace(AccountData.Username) ? "Client" : AccountData.Username;
                    _ = RoomLogger.LogGuest(currentRoomId, guestName);
                }
                catch (Exception ex)
                {
                    UI(() =>
                    {
                        SetStatus("Lỗi kết nối host: " + ex.Message);
                        btnJoinRoom.Enabled = true;
                    });
                }
            };

            lanBroadcast.StartListen(currentRoomId);
        }

        // ============================
        //         HOST BẤM START
        // ============================
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (networkManager == null || !networkManager.IsConnected)
            {
                SetStatus("Chưa đủ 2 người để bắt đầu!");
                return;
            }

            networkManager.Send("START_GAME");
            OpenGame();
        }

        // ============================
        //         MỞ GAME SOLO
        // ============================
        private void OpenGame()
        {
            var game = new GAMESOLO(networkManager, isHost, currentRoomId);
            game.Show();
            this.Hide();

            game.FormClosed += (_, __) =>
            {
                this.Show();
                SetStatus("Đã quay lại lobby.");
            };
        }

        // ============================
        //         LỊCH SỬ ĐẤU
        // ============================
        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AccountData.Username))
            {
                MessageBox.Show(
                    "Vui lòng đăng nhập để xem lịch sử.",
                    "Lỗi",
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

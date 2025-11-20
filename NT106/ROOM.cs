using System;
using System.Collections.Generic;
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

        private async void Room_Load(object sender, EventArgs e)
        {
            SetStatus("Chưa tạo/tham gia phòng.");

            // Load danh sách phòng từ API lên DataGridView
            await LoadRoomsAsync();
        }

        // ===== FORM CLOSING: đánh dấu END nếu đã có phòng =====
        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { networkManager?.Dispose(); } catch { }
            try { lanBroadcast?.Dispose(); } catch { }

            // Chỉ host mới báo END
            try
            {
                if (isHost && !string.IsNullOrEmpty(currentRoomId))
                {
                    _ = RoomApi.EndRoomAsync(currentRoomId);
                }
            }
            catch
            {
            }
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

            // Bị ngắt kết nối
            networkManager.OnDisconnected += () =>
            {
                UI(() =>
                {
                    btnStartGame.Enabled = false;
                    SetStatus("Kết nối bị ngắt. Quay lại lobby hoặc tạo phòng khác.");
                });
            };
        }

        // ======================================================
        //       LOAD DANH SÁCH PHÒNG TỪ API LÊN DATAGRIDVIEW
        // ======================================================
        private async Task LoadRoomsAsync()
        {
            try
            {
                var rooms = await RoomApi.GetRoomsAsync();

                UI(() =>
                {
                    IdRoom.Rows.Clear();

                    if (rooms == null || rooms.Count == 0)
                        return;

                    foreach (RoomApi.RoomInfo r in rooms)
                    {
                        string playersPart = $"{r.PlayerCount}/2";
                        string statusText = string.IsNullOrWhiteSpace(r.Status)
                            ? playersPart
                            : $"{playersPart} - {r.Status}";

                        // Cột 1: RoomID, cột 2: Host, cột 3: Players/Status
                        IdRoom.Rows.Add(r.RoomId, r.Host, statusText);
                    }
                });
            }
            catch (Exception ex)
            {
                SetStatus("Không tải được danh sách phòng: " + ex.Message);
            }
        }

        // ============================
        //         TẠO PHÒNG HOST
        // ============================
        private async void btnCreateRoom_Click(object sender, EventArgs e)
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

                // Tên host
                var hostName = string.IsNullOrWhiteSpace(AccountData.Username)
                    ? "Host"
                    : AccountData.Username;

                _ = RoomLogger.LogHost(currentRoomId, hostName);

                // POST lên API Gateway: action = create
                var ok = await RoomApi.CreateRoomAsync(currentRoomId, hostName);
                if (!ok)
                {
                    SetStatus($"[HOST] Đã tạo phòng {currentRoomId} (LAN OK) nhưng POST API thất bại.");
                }

                // Sau khi tạo xong, refresh lại danh sách phòng
                await LoadRoomsAsync();
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

                    // Tên guest
                    var guestName = string.IsNullOrWhiteSpace(AccountData.Username)
                        ? "Client"
                        : AccountData.Username;

                    _ = RoomLogger.LogGuest(currentRoomId, guestName);

                    // POST lên API Gateway: action = join
                    var ok = await RoomApi.JoinRoomAsync(currentRoomId, guestName);
                    if (!ok)
                    {
                        UI(() => SetStatus(
                            $"Đã join LAN nhưng POST API join thất bại (phòng {currentRoomId})."));
                    }

                    // Sau khi join thành công có thể reload lại list (cho đẹp)
                    await LoadRoomsAsync();
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
        private async void btnStartGame_Click(object sender, EventArgs e)
        {
            if (networkManager == null || !networkManager.IsConnected)
            {
                SetStatus("Chưa đủ 2 người để bắt đầu!");
                return;
            }

            // Chỉ host mới POST start
            if (isHost)
            {
                var hostName = string.IsNullOrWhiteSpace(AccountData.Username)
                    ? "Host"
                    : AccountData.Username;

                _ = RoomApi.StartRoomAsync(currentRoomId, hostName);
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

            game.FormClosed += async (_, __) =>
            {
                this.Show();
                SetStatus("Đã quay lại lobby.");

                // Sau khi game kết thúc, refresh danh sách phòng
                await LoadRoomsAsync();
            };
        }

        // ============================
        //         LỊCH SỬ ĐẤU
        // ============================
        private void button1_Click(object sender, EventArgs e)
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

        private void IdRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // hiện tại không cần làm gì
        }

        // Double–click dòng trong DataGridView để join phòng
        private void IdRoom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = IdRoom.Rows[e.RowIndex];
            var roomIdObj = row.Cells["Player1"].Value;
            var roomId = roomIdObj?.ToString();

            if (string.IsNullOrWhiteSpace(roomId))
                return;

            txtRoomID.Text = roomId;

            // Gọi lại logic join phòng
            btnJoinRoom_Click(btnJoinRoom, EventArgs.Empty);
        }
    }
}

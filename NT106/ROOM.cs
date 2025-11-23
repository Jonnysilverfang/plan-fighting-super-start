using System;
using System.Drawing;
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

        // Đánh dấu đã bắt đầu game hay chưa
        private bool gameStarted = false;

        // Đánh dấu form đang shutdown (host tự đóng), để phân biệt với peer disconnect
        private bool shuttingDown = false;

        public Room()
        {
            InitializeComponent();
        }

        // ========================= FORM LOAD =========================
        private async void Room_Load(object sender, EventArgs e)
        {
            SetStatus("Chưa tạo/tham gia phòng.");

            // Load danh sách phòng từ API lên DataGridView
            await LoadRoomsAsync();
        }

        // ===== FORM CLOSING: host thoát khi đã / chưa start =====
        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            shuttingDown = true; // đánh dấu là mình tự đóng form

            try { networkManager?.Dispose(); } catch { }
            try { lanBroadcast?.Dispose(); } catch { }

            // Chỉ host mới báo về API
            try
            {
                if (isHost && !string.IsNullOrEmpty(currentRoomId))
                {
                    if (gameStarted)
                    {
                        // Đang đấu → kết thúc phòng (END)
                        _ = RoomApi.EndRoomAsync(currentRoomId);
                    }
                    else
                    {
                        // CHƯA bắt đầu → hủy phòng (CANCELLED), KHÔNG đặt END
                        _ = RoomApi.CancelRoomAsync(currentRoomId);
                    }
                }
            }
            catch
            {
            }
        }

        // ========================= HELPER =========================
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

        // ========================= CHAT UI =========================
        // Thêm tin chat vào RichTextBox với màu HOST / CLIENT
        private void AppendChat(string from, string text, bool isHostSender)
        {
            if (chatBox == null) return;

            if (chatBox.InvokeRequired)
            {
                chatBox.Invoke(new Action(() => AppendChat(from, text, isHostSender)));
                return;
            }

            string prefix = isHostSender ? "[HOST] " : "[CLIENT] ";

            Color prefixColor = isHostSender ? Color.Red : Color.LightGray; // CLIENT màu xám
            Color nameColor = Color.Gold;
            Color textColor = Color.White;

            if (chatBox.TextLength > 0)
                chatBox.AppendText(Environment.NewLine);

            int start = chatBox.TextLength;

            // Prefix
            chatBox.AppendText(prefix);
            chatBox.Select(start, prefix.Length);
            chatBox.SelectionColor = prefixColor;

            // Tên người
            string namePart = from + ": ";
            chatBox.AppendText(namePart);
            chatBox.Select(start + prefix.Length, namePart.Length);
            chatBox.SelectionColor = nameColor;

            // Nội dung
            chatBox.AppendText(text);
            chatBox.SelectionColor = textColor;

            chatBox.SelectionStart = chatBox.TextLength;
            chatBox.ScrollToCaret();
        }

        // ====== HÀM DÙNG CHUNG: START HOST TCP (không đụng broadcast) ======
        private void StartHostServer()
        {
            // Dọn cái cũ (nếu có)
            try { networkManager?.Dispose(); } catch { }

            // Tạo mới NetworkManager và host TCP
            networkManager = new NetworkManager();
            WireNetworkEvents();
            networkManager.StartHost(GAME_PORT);
        }

        // Host xử lý khi người chơi còn lại rời phòng trước khi bắt đầu
        private async Task HandlePeerDisconnectedAsync()
        {
            // Chỉ quan tâm: mình là HOST, game chưa start, có roomId,
            // và KHÔNG phải do mình tự đóng form (shuttingDown = false)
            if (!isHost) return;
            if (gameStarted) return;
            if (string.IsNullOrEmpty(currentRoomId)) return;
            if (shuttingDown) return;

            try
            {
                // 1) Khởi động lại TCP host để có thể nhận client mới
                StartHostServer();

                // 2) Gọi API cập nhật lại trạng thái phòng về CREATED (1 người)
                var ok = await RoomApi.BackToCreatedAsync(currentRoomId);

                if (ok)
                {
                    UI(() =>
                    {
                        btnStartGame.Enabled = false;
                        SetStatus($"Người chơi rời phòng. Phòng {currentRoomId} trở lại trạng thái chờ (1/2).");
                    });

                    // Refresh lại list phòng trên lưới
                    await LoadRoomsAsync();
                }
                else
                {
                    UI(() =>
                    {
                        btnStartGame.Enabled = false;
                        SetStatus($"Người chơi rời phòng, nhưng API BackToCreatedAsync thất bại (room {currentRoomId}).");
                    });
                }
            }
            catch (Exception ex)
            {
                UI(() =>
                {
                    btnStartGame.Enabled = false;
                    SetStatus("Lỗi khi cập nhật phòng về CREATED: " + ex.Message);
                });
            }
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

            // Nhận message game / chat
            networkManager.OnMessageReceived += (msg) =>
            {
                // Host gửi START_GAME → client nhận được
                if (msg == "START_GAME")
                {
                    UI(() =>
                    {
                        gameStarted = true;
                        OpenGame();
                    });
                    return;
                }

                // Chat nội bộ: định dạng "CHAT|HOST|from|text" hoặc "CHAT|CLIENT|from|text"
                if (msg.StartsWith("CHAT|"))
                {
                    try
                    {
                        var parts = msg.Split(new[] { '|' }, 4);
                        if (parts.Length >= 4)
                        {
                            string role = parts[1];      // "HOST" hoặc "CLIENT"
                            string from = parts[2];
                            string text = parts[3];

                            bool isHostSender = role.Equals("HOST", StringComparison.OrdinalIgnoreCase);
                            AppendChat(from, text, isHostSender);
                        }
                    }
                    catch
                    {
                        // bỏ qua nếu message lỗi
                    }
                    return;
                }

                // Phòng không tồn tại (nếu Lambda có gửi)
                if (msg == "ROOM_NOT_FOUND")
                {
                    UI(() =>
                    {
                        SetStatus("Không tìm thấy phòng trên server.");
                        btnJoinRoom.Enabled = true;
                    });
                    return;
                }
            };

            // Bị ngắt kết nối (cả host lẫn client đều chạy vào đây)
            networkManager.OnDisconnected += () =>
            {
                UI(() =>
                {
                    btnStartGame.Enabled = false;
                    SetStatus("Kết nối bị ngắt. Quay lại lobby hoặc tạo phòng khác.");
                });

                // Nếu đang shutdown (host tự đóng form) thì không xử lý gì thêm
                if (shuttingDown)
                    return;

                // 1) Nếu mình là HOST và game chưa start → B out → quay về CREATED
                if (isHost && !gameStarted)
                {
                    _ = HandlePeerDisconnectedAsync();
                }
                // 2) Nếu mình là CLIENT (B) → host out
                else if (!isHost)
                {
                    // Client CHỈ quay về lobby, KHÔNG gọi Cancel/End phòng
                    UI(() =>
                    {
                        SetStatus("Host đã rời phòng. Bạn đã bị ngắt kết nối.");
                        btnJoinRoom.Enabled = true;
                    });
                }
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

        // ========================= TẠO PHÒNG HOST =========================
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

                // Reset cờ
                gameStarted = false;
                shuttingDown = false;

                // Host role
                isHost = true;
                btnStartGame.Enabled = false;

                // Broadcast LAN
                lanBroadcast = new LANBroadcast();
                lanBroadcast.StartBroadcast(currentRoomId, GAME_PORT);

                // Start TCP Host
                StartHostServer();

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

        // ========================= JOIN PHÒNG CLIENT =========================
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
            gameStarted = false;          // reset
            shuttingDown = false;
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

        // ========================= HOST BẤM START =========================
        private async void btnStartGame_Click(object sender, EventArgs e)
        {
            if (networkManager == null || !networkManager.IsConnected)
            {
                SetStatus("Chưa đủ 2 người để bắt đầu!");
                return;
            }

            gameStarted = true;   // đánh dấu đã bắt đầu

            // Chỉ host mới POST start
            if (isHost)
            {
                var hostName = string.IsNullOrWhiteSpace(AccountData.Username)
                    ? "Host"
                    : AccountData.Username;

                _ = RoomApi.StartRoomAsync(currentRoomId, hostName);
            }

            // Gửi lệnh START_GAME cho client trong phòng
            networkManager.Send("START_GAME");
            OpenGame();
        }

        // ========================= MỞ GAME SOLO =========================
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

        // ========================= LỊCH SỬ ĐẤU =========================
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

            // Nếu cột chứa RoomID tên khác, đổi "Player1" thành đúng tên cột RoomId của bạn
            var roomIdObj = row.Cells["Player1"].Value;
            var roomId = roomIdObj?.ToString();

            if (string.IsNullOrWhiteSpace(roomId))
                return;

            txtRoomID.Text = roomId;

            // Gọi lại logic join phòng
            btnJoinRoom_Click(btnJoinRoom, EventArgs.Empty);
        }

        // ========================= NÚT REFRESH DANH SÁCH PHÒNG =========================
        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                await LoadRoomsAsync();
            }
            catch (Exception ex)
            {
                SetStatus("Không tải được danh sách phòng: " + ex.Message);
            }
        }

        // ========================= GỬI CHAT =========================
        private void btnSendChat_Click(object sender, EventArgs e)
        {
            string text = txtChat.Text.Trim();
            if (string.IsNullOrEmpty(text))
                return;

            string from = string.IsNullOrWhiteSpace(AccountData.Username)
                ? (isHost ? "Host" : "Client")
                : AccountData.Username;

            // Hiện local trước
            AppendChat(from, text, isHost);
            txtChat.Clear();

            // Nếu chưa có kết nối / chưa join phòng thì không gửi qua mạng
            if (networkManager == null || !networkManager.IsConnected || string.IsNullOrEmpty(currentRoomId))
                return;

            string role = isHost ? "HOST" : "CLIENT";
            string payload = $"CHAT|{role}|{from}|{text}";

            try
            {
                networkManager.Send(payload);
            }
            catch
            {
                // lỗi gửi thì bỏ qua, không crash
            }
        }
    }
}

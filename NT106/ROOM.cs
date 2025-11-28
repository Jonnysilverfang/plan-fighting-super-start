using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class Room : Form
    {
        private NetworkManager networkManager;
        private LANBroadcast lanBroadcast;
        private ChatSanhLAN chatSanh;

        private bool isHost;
        private string currentRoomId;

        private const int GAME_PORT = 9000;

        private bool gameStarted = false;
        private bool shuttingDown = false;

        public Room()
        {
            InitializeComponent();
        }

        private static bool InDesigner()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return true;
            try { return Process.GetCurrentProcess().ProcessName.IndexOf("devenv", StringComparison.OrdinalIgnoreCase) >= 0; }
            catch { return false; }
        }

        private bool CanAccessUi()
        {
            return !IsDisposed && IsHandleCreated;
        }

        private void UpdateKenhItems(bool inRoom)
        {
            if (!CanAccessUi()) return;
            if (cmbKenh == null || cmbKenh.IsDisposed) return;

            cmbKenh.Items.Clear();
            cmbKenh.Items.Add("Kênh chung (Sảnh)");
            if (inRoom && !string.IsNullOrEmpty(currentRoomId))
                cmbKenh.Items.Add($"Kênh phòng ({currentRoomId})");
            cmbKenh.SelectedIndex = 0;
        }

        // ========================= FORM LOAD =========================
        private async void Room_Load(object sender, EventArgs e)
        {
            if (InDesigner())
            {
                return;
            }

            SetStatus("Chưa tạo/tham gia phòng.");
            UpdateKenhItems(false);

            btnCreateRoom.Enabled = true;
            btnJoinRoom.Enabled = true;
            btnStartGame.Enabled = false;
            btnLeaveRoom.Enabled = false;

            chatSanh = new ChatSanhLAN();
            chatSanh.BatDauNghe();
            chatSanh.NhanTinSanh += (ten, noiDung) =>
                UI(() => AppendChat($"[LOBBY]{ten}", noiDung, false));

            await LoadRoomsAsync();
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            shuttingDown = true;

            try { networkManager?.Dispose(); networkManager = null; } catch { }
            try { lanBroadcast?.Dispose(); lanBroadcast = null; } catch { }
            try { chatSanh?.Dispose(); chatSanh = null; } catch { }

            try
            {
                if (isHost && !string.IsNullOrEmpty(currentRoomId))
                {
                    if (gameStarted)
                        _ = RoomApi.EndRoomAsync(currentRoomId);
                    else
                        _ = RoomApi.CancelRoomAsync(currentRoomId);
                }
            }
            catch { }
        }

        // ===== HELPER =====

        private void SetStatus(string msg)
        {
            if (!CanAccessUi()) return;
            if (lblStatus == null || lblStatus.IsDisposed) return;

            try
            {
                if (InvokeRequired)
                    Invoke(new Action(() => lblStatus.Text = msg));
                else
                    lblStatus.Text = msg;
            }
            catch { }
        }

        private void UI(Action a)
        {
            if (!CanAccessUi()) return;

            try
            {
                if (InvokeRequired)
                    BeginInvoke(a);
                else
                    a();
            }
            catch { }
        }

        private string MakeRoomId() => new Random().Next(100000, 999999).ToString();

        // ===== CHAT =====

        private void AppendChat(string from, string text, bool isHostSender)
        {
            if (!CanAccessUi()) return;
            if (chatBox == null || chatBox.IsDisposed) return;

            if (chatBox.InvokeRequired)
            {
                try { chatBox.Invoke(new Action(() => AppendChat(from, text, isHostSender))); }
                catch { }
                return;
            }

            bool laLobby = from.StartsWith("[LOBBY]");
            if (laLobby) from = from.Substring("[LOBBY]".Length);

            Color prefixColor = laLobby ? Color.Cyan : (isHostSender ? Color.Red : Color.LightGray);
            string prefix = laLobby ? "[LOBBY] " : (isHostSender ? "[HOST] " : "[CLIENT] ");

            chatBox.SelectionStart = chatBox.TextLength;
            chatBox.SelectionColor = prefixColor;
            chatBox.AppendText(prefix);

            chatBox.SelectionColor = Color.Gold;
            chatBox.AppendText(from + ": ");

            chatBox.SelectionColor = Color.White;
            chatBox.AppendText(text + "\n");

            chatBox.ScrollToCaret();
        }

        // ===== HOST =====

        private void StartHostServer()
        {
            try { networkManager?.Dispose(); } catch { }

            networkManager = new NetworkManager();
            WireNetworkEvents();
            networkManager.StartHost(GAME_PORT);
        }

        private async Task HandlePeerDisconnectedAsync()
        {
            if (!isHost || gameStarted || string.IsNullOrEmpty(currentRoomId) || shuttingDown) return;

            try
            {
                StartHostServer();
                var ok = await RoomApi.BackToCreatedAsync(currentRoomId);

                UI(() =>
                {
                    btnStartGame.Enabled = false;
                    SetStatus("Người chơi rời phòng. Phòng trở về trạng thái chờ.");
                    btnLeaveRoom.Enabled = true;
                });

                await LoadRoomsAsync();
                UpdateKenhItems(true);
            }
            catch { }
        }

        // ===== WIRE EVENTS =====

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
                        btnLeaveRoom.Enabled = true;
                        SetStatus($"Phòng {currentRoomId}: Đã đủ 2 người.");
                    }
                    else
                    {
                        btnLeaveRoom.Enabled = true;
                        SetStatus("Đã kết nối. Chờ host bắt đầu.");
                    }
                });
            };

            networkManager.OnMessageReceived += (msg) =>
            {
                if (msg == "START_GAME")
                {
                    UI(() =>
                    {
                        gameStarted = true;
                        OpenGame();
                    });
                    return;
                }

                if (msg.StartsWith("LEFT_ROOM|"))
                {
                    UI(() => SetStatus("Người chơi đã rời phòng."));
                    return;
                }

                // CHAT
                if (msg.StartsWith("CHAT|"))
                {
                    var parts = msg.Split('|');
                    if (parts.Length >= 4)
                    {
                        string role = parts[1];
                        string from = parts[2];
                        string text = parts[3];
                        AppendChat(from, text, role == "HOST");
                    }
                }
            };

            networkManager.OnDisconnected += () =>
            {
                if (shuttingDown) return;

                if (isHost && !gameStarted)
                {
                    _ = HandlePeerDisconnectedAsync();
                    return;
                }

                if (!isHost)
                {
                    UI(() =>
                    {
                        SetStatus("Host rời phòng.");
                        btnJoinRoom.Enabled = true;
                        btnCreateRoom.Enabled = true;
                        btnLeaveRoom.Enabled = false;
                        UpdateKenhItems(false);
                        currentRoomId = null;
                        gameStarted = false;
                    });
                }
            };
        }

        // ===== LOAD ROOM LIST =====

        private async Task LoadRoomsAsync()
        {
            try
            {
                var rooms = await RoomApi.GetRoomsAsync();
                UI(() =>
                {
                    IdRoom.Rows.Clear();
                    if (rooms == null) return;

                    foreach (var r in rooms)
                    {
                        string statusText = $"{r.PlayerCount}/2 - {r.Status}";
                        IdRoom.Rows.Add(r.RoomId, r.Host, statusText);
                    }
                });
            }
            catch { }
        }

        // ===== CREATE ROOM =====

        private async void btnCreateRoom_Click(object sender, EventArgs e)
        {
            if (!NetworkManager.IsNetworkAvailable())
            {
                SetStatus("Không có mạng LAN!");
                return;
            }

            currentRoomId = string.IsNullOrWhiteSpace(txtRoomID.Text)
                ? MakeRoomId()
                : txtRoomID.Text.Trim();

            txtRoomID.Text = currentRoomId;

            try { networkManager?.Dispose(); networkManager = null; } catch { }
            try { lanBroadcast?.Dispose(); lanBroadcast = null; } catch { }

            gameStarted = false;
            shuttingDown = false;

            isHost = true;
            btnStartGame.Enabled = false;

            btnJoinRoom.Enabled = false;
            btnCreateRoom.Enabled = false;
            btnLeaveRoom.Enabled = true;

            lanBroadcast = new LANBroadcast();
            lanBroadcast.StartBroadcast(currentRoomId, GAME_PORT);

            StartHostServer();

            SetStatus($"HOST: Đã tạo phòng {currentRoomId}. Chờ người khác...");

            UpdateKenhItems(true);

            var hostName = string.IsNullOrWhiteSpace(AccountData.Username) ? "Host" : AccountData.Username;
            _ = RoomLogger.LogHost(currentRoomId, hostName);

            await RoomApi.CreateRoomAsync(currentRoomId, hostName);
            await LoadRoomsAsync();
        }

        // ===== JOIN ROOM =====

        private void btnJoinRoom_Click(object sender, EventArgs e)
        {
            string roomId = txtRoomID.Text.Trim();
            if (string.IsNullOrEmpty(roomId))
            {
                SetStatus("Hãy nhập Room ID");
                return;
            }

            if (!NetworkManager.IsNetworkAvailable())
            {
                SetStatus("Không có mạng LAN");
                return;
            }

            currentRoomId = roomId;
            isHost = false;
            gameStarted = false;
            shuttingDown = false;

            btnJoinRoom.Enabled = false;
            btnStartGame.Enabled = false;
            btnCreateRoom.Enabled = false;
            btnLeaveRoom.Enabled = true;

            try { networkManager?.Dispose(); networkManager = null; } catch { }
            try { lanBroadcast?.Dispose(); lanBroadcast = null; } catch { }

            lanBroadcast = new LANBroadcast();
            SetStatus($"Đang tìm phòng {roomId}...");

            lanBroadcast.OnRoomFound += async (foundRoomId, hostIp, port) =>
            {
                if (foundRoomId != currentRoomId) return;

                lanBroadcast.StopListen();

                UI(() => SetStatus($"Tìm thấy host, đang kết nối..."));

                networkManager = new NetworkManager();
                WireNetworkEvents();

                await networkManager.JoinHost(hostIp, port);

                UI(() =>
                {
                    SetStatus("Đã kết nối! Chờ host bắt đầu.");
                    UpdateKenhItems(true);
                    btnLeaveRoom.Enabled = true;
                });

                var guestName = string.IsNullOrWhiteSpace(AccountData.Username) ? "Client" : AccountData.Username;
                _ = RoomLogger.LogGuest(currentRoomId, guestName);
                await RoomApi.JoinRoomAsync(currentRoomId, guestName);

                await LoadRoomsAsync();
            };

            lanBroadcast.StartListen(currentRoomId);
        }

        // ===== START GAME =====

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (networkManager == null || !networkManager.IsConnected)
            {
                SetStatus("Chưa đủ 2 người!");
                return;
            }

            gameStarted = true;

            if (isHost)
            {
                var hostName = string.IsNullOrWhiteSpace(AccountData.Username) ? "Host" : AccountData.Username;
                _ = RoomApi.StartRoomAsync(currentRoomId, hostName);
            }

            networkManager.Send("START_GAME");
            OpenGame();
        }

        // ===== LEAVE ROOM =====

        private async void btnLeaveRoom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentRoomId))
            {
                SetStatus("Không ở trong phòng nào.");
                btnLeaveRoom.Enabled = false;
                return;
            }

            shuttingDown = true;

            try { networkManager?.Dispose(); networkManager = null; } catch { }
            try { lanBroadcast?.Dispose(); lanBroadcast = null; } catch { }

            if (isHost)
            {
                if (gameStarted)
                    _ = RoomApi.EndRoomAsync(currentRoomId);
                else
                    _ = RoomApi.CancelRoomAsync(currentRoomId);

                SetStatus($"Đã đóng phòng {currentRoomId}");
            }
            else
            {
                SetStatus($"Đã rời phòng {currentRoomId}");
            }

            isHost = false;
            gameStarted = false;
            currentRoomId = null;
            shuttingDown = false;

            UpdateKenhItems(false);

            btnCreateRoom.Enabled = true;
            btnJoinRoom.Enabled = true;
            btnStartGame.Enabled = false;
            btnLeaveRoom.Enabled = false;

            try { await LoadRoomsAsync(); } catch { }
        }

        // ===== OPEN GAME (FINAL FIX) =====

        private void OpenGame()
        {
            if (networkManager == null || !networkManager.IsConnected)
            {
                SetStatus("Không thể mở trận vì chưa kết nối.");
                return;
            }

            this.Enabled = false; // KHÔNG HIDE

            var game = new GAMESOLO(networkManager, isHost, currentRoomId);
            game.StartPosition = FormStartPosition.CenterScreen;

            game.FormClosed += async (_, __) =>
            {
                this.Enabled = true;
                this.Activate();

                SetStatus("Đã quay lại lobby.");

                btnCreateRoom.Enabled = true;
                btnJoinRoom.Enabled = true;
                btnStartGame.Enabled = false;
                btnLeaveRoom.Enabled = false;

                currentRoomId = null;
                isHost = false;
                gameStarted = false;

                await LoadRoomsAsync();
                UpdateKenhItems(false);
            };

            game.Show();
        }

        private async void btnSendChat_Click(object sender, EventArgs e)
        {
            string text = txtChat.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            string from = string.IsNullOrWhiteSpace(AccountData.Username)
                ? (isHost ? "Host" : "Client")
                : AccountData.Username;

            bool inRoom = networkManager != null && networkManager.IsConnected && !string.IsNullOrEmpty(currentRoomId);
            bool guiKenhPhong = cmbKenh.SelectedItem?.ToString()?.StartsWith("Kênh phòng") == true;

            if (!inRoom || !guiKenhPhong)
            {
                try { await chatSanh.GuiTinSanhAsync(from, text); } catch { }
                AppendChat(from, text, isHost);
                txtChat.Clear();
                return;
            }

            string role = isHost ? "HOST" : "CLIENT";
            string payload = $"CHAT|{role}|{from}|{text}";

            AppendChat(from, text, isHost);
            txtChat.Clear();

            try { networkManager.Send(payload); } catch { }
        }
        // ========================= SỰ KIỆN GRID / NÚT / TEXTBOX =========================

        private void IdRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Không làm gì cũng được, chỉ để Designer khỏi báo lỗi
        }

        private void IdRoom_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Double click vào dòng => tự fill RoomID và join
            if (e.RowIndex < 0) return;

            var row = IdRoom.Rows[e.RowIndex];

            var roomIdObj = row.Cells["Player1"]?.Value;
            var roomId = roomIdObj?.ToString();

            if (string.IsNullOrWhiteSpace(roomId)) return;

            txtRoomID.Text = roomId;
            // Tái sử dụng luôn logic join
            btnJoinRoom_Click(btnJoinRoom, EventArgs.Empty);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lịch sử đấu
            if (string.IsNullOrEmpty(AccountData.Username))
            {
                MessageBox.Show("Vui lòng đăng nhập để xem lịch sử.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private async void button2_Click(object sender, EventArgs e)
        {
            // Load lại danh sách phòng
            try
            {
                await LoadRoomsAsync();
            }
            catch (Exception ex)
            {
                SetStatus("Không tải được danh sách phòng: " + ex.Message);
            }
        }

        private void chatBox_TextChanged(object sender, EventArgs e)
        {
            // Để trống cũng được – chỉ để tránh lỗi Designer
        }

        private void txtChat_TextChanged(object sender, EventArgs e)
        {
            // Nếu sau này muốn gõ Enter để gửi thì xử lý ở đây
        }

    }
}

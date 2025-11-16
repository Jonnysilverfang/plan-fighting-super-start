using System;
using System.Drawing;
using System.Text.Json;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class GAMESOLO : Form
    {
        private NetworkManager _network;
        private bool _isHost;
        private string _roomId;

        private PictureBox _player, _opponent;
        private PictureBox _playerBullet, _opponentBullet;
        private System.Windows.Forms.Timer _gameTimer;

        private bool _goLeft, _goRight, _goUp, _goDown;
        private int _playerSpeed = 7;
        private int _bulletSpeed = 12;

        private int _playerBulletDir;
        private int _opponentBulletDir;

        private bool _gameEnded = false;
        private bool _paused = false;
        private int _stateTickCounter = 0;

        private string _localName;
        private string _opponentName = "Đối thủ";

        private int _playerHp = 3;
        private int _opponentHp = 3;
        private Label _hudYou, _hudEnemy;

        private Panel _pausePanel;
        private Button _btnResume, _btnQuit;

        public GAMESOLO()
            : this(new NetworkManager(), true, "SOLO-" + Guid.NewGuid().ToString("N")[..6]) { }

        public GAMESOLO(NetworkManager network)
            : this(network, true, "SOLO-" + Guid.NewGuid().ToString("N")[..6]) { }

        public GAMESOLO(NetworkManager network, bool isHost, string roomId)
        {
            InitializeComponent();

            this.ActiveControl = null;

            _network = network ?? new NetworkManager();
            _isHost = isHost;
            _roomId = string.IsNullOrWhiteSpace(roomId) ? "SOLO" : roomId;

            _localName = string.IsNullOrEmpty(AccountData.Username)
                ? (_isHost ? "Host" : "Client")
                : AccountData.Username;

            this.Text = (_isHost ? "[HOST] " : "[CLIENT] ") + "Room: " + _roomId;
            this.KeyPreview = true;
            this.DoubleBuffered = true;

            SetupGameObjects();
            SetupDirections();
            SetupHud();
            SetupPauseOverlay();
            SetupTimer();
            WireNetworkEvents();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!_gameEnded && keyData == Keys.Space)
            {
                if (!_paused) FirePlayerBullet();
                return true;
            }
            if (keyData == Keys.Escape)
            {
                TogglePause();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SetupGameObjects()
        {
            int w = Math.Max(800, this.ClientSize.Width);
            int h = Math.Max(600, this.ClientSize.Height);
            int ship = 40;

            _player = new PictureBox { Width = ship, Height = ship, BackColor = Color.DeepSkyBlue };
            _opponent = new PictureBox { Width = ship, Height = ship, BackColor = Color.OrangeRed };

            if (_isHost)
            {
                _player.Left = (w - ship) / 2; _player.Top = h - ship - 70;
                _opponent.Left = (w - ship) / 2; _opponent.Top = 70;
            }
            else
            {
                _player.Left = (w - ship) / 2; _player.Top = 70;
                _opponent.Left = (w - ship) / 2; _opponent.Top = h - ship - 70;
            }

            _playerBullet = new PictureBox { Width = 8, Height = 20, BackColor = Color.Yellow, Visible = false };
            _opponentBullet = new PictureBox { Width = 8, Height = 20, BackColor = Color.Lime, Visible = false };

            Controls.Add(_player);
            Controls.Add(_opponent);
            Controls.Add(_playerBullet);
            Controls.Add(_opponentBullet);

            _player.BringToFront();
            _opponent.BringToFront();
            _playerBullet.BringToFront();
            _opponentBullet.BringToFront();

            try { btnExit.BringToFront(); } catch { }

            this.PreviewKeyDown += AnyControl_PreviewKeyDown;
            try { btnExit.PreviewKeyDown += AnyControl_PreviewKeyDown; } catch { }
            _player.PreviewKeyDown += AnyControl_PreviewKeyDown;
            _opponent.PreviewKeyDown += AnyControl_PreviewKeyDown;

            this.KeyDown += GAMESOLO_KeyDown;
            this.KeyUp += GAMESOLO_KeyUp;
        }

        private void SetupDirections()
        {
            if (_isHost) { _playerBulletDir = -1; _opponentBulletDir = +1; }
            else { _playerBulletDir = +1; _opponentBulletDir = -1; }
        }

        private void SetupHud()
        {
            _hudYou = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                Left = 10,
                Top = (_isHost ? ClientSize.Height - 35 : 10)
            };
            _hudEnemy = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font(Font.FontFamily, 10, FontStyle.Bold),
                Left = 10,
                Top = (_isHost ? 10 : ClientSize.Height - 35)
            };
            Controls.Add(_hudYou);
            Controls.Add(_hudEnemy);
            UpdateHud();
        }

        private void UpdateHud()
        {
            string hearts(int hp) => new string('❤', Math.Max(0, hp));
            _hudYou.Text = $"Bạn: {_localName}  HP: {hearts(_playerHp)}";
            _hudEnemy.Text = $"Đối thủ: {_opponentName}  HP: {hearts(_opponentHp)}";
        }

        private void SetupPauseOverlay()
        {
            _pausePanel = new Panel
            {
                Visible = false,
                BackColor = Color.FromArgb(180, 0, 0, 0),
                Left = 0,
                Top = 0,
                Width = ClientSize.Width,
                Height = ClientSize.Height,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            _btnResume = new Button { Text = "Tiếp tục (ESC)", Width = 160, Height = 40 };
            _btnQuit = new Button { Text = "Thoát trận", Width = 160, Height = 40, Top = 60 };
            _btnResume.Left = _btnQuit.Left = (ClientSize.Width - _btnResume.Width) / 2;
            _btnResume.Top = (ClientSize.Height - 100) / 2;

            _btnResume.Click += (s, e) => TogglePause();
            _btnQuit.Click += (s, e) => ConfirmExit();

            _pausePanel.Controls.Add(_btnResume);
            _pausePanel.Controls.Add(_btnQuit);
            Controls.Add(_pausePanel);
            _pausePanel.BringToFront();
        }

        private void TogglePause()
        {
            if (_gameEnded) return;
            _paused = !_paused;
            _pausePanel.Visible = _paused;
            if (_paused) _gameTimer.Stop();
            else _gameTimer.Start();
            lblStatusGame.Text = _paused ? "Tạm dừng" : "Đang chơi…";
        }

        private void SetupTimer()
        {
            _gameTimer = new System.Windows.Forms.Timer { Interval = 16 };
            _gameTimer.Tick += GameTimer_Tick;
            _gameTimer.Start();
        }

        private void WireNetworkEvents()
        {
            if (_network == null) return;

            _network.OnMessageReceived += msg =>
            {
                if (IsDisposed) return;
                if (InvokeRequired) BeginInvoke(new Action(() => ProcessNetworkMessage(msg)));
                else ProcessNetworkMessage(msg);
            };

            _network.OnDisconnected += () =>
            {
                if (IsDisposed) return;
                if (InvokeRequired) BeginInvoke(new Action(OnDisconnectedUI));
                else OnDisconnectedUI();
            };

            SafeSend(new { type = "hello", name = _localName });
            lblStatusGame.Text = "Đang chờ đối thủ…";
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            if (_paused || _gameEnded || _player == null) return;

            int padding = 20;
            int midY = ClientSize.Height / 2;

            int minY = _isHost ? midY : padding;
            int maxY = _isHost ? (ClientSize.Height - _player.Height - padding)
                               : (midY - _player.Height);

            if (_goLeft && _player.Left > padding) _player.Left -= _playerSpeed;
            if (_goRight && _player.Right < ClientSize.Width - padding) _player.Left += _playerSpeed;
            if (_goUp && _player.Top > minY) _player.Top -= _playerSpeed;
            if (_goDown && _player.Top < maxY) _player.Top += _playerSpeed;

            if (_playerBullet.Visible)
            {
                _playerBullet.Top += _playerBulletDir * _bulletSpeed;
                if (_playerBullet.Top < 0 || _playerBullet.Top > ClientSize.Height)
                    _playerBullet.Visible = false;
            }

            if (_opponentBullet.Visible)
            {
                _opponentBullet.Top += _opponentBulletDir * _bulletSpeed;
                if (_opponentBullet.Top < 0 || _opponentBullet.Top > ClientSize.Height)
                    _opponentBullet.Visible = false;
            }

            if (_isHost)
            {
                if (_playerBullet.Visible && _opponent != null &&
                    _playerBullet.Bounds.IntersectsWith(_opponent.Bounds))
                {
                    _playerBullet.Visible = false;
                    ApplyHit(toOpponent: true);
                }

                if (_opponentBullet.Visible && _player != null &&
                    _opponentBullet.Bounds.IntersectsWith(_player.Bounds))
                {
                    _opponentBullet.Visible = false;
                    ApplyHit(toOpponent: false);
                }
            }

            _stateTickCounter++;
            if (_stateTickCounter % 2 == 0) SendPlayerState();
        }

        private void ApplyHit(bool toOpponent)
        {
            if (toOpponent) _opponentHp = Math.Max(0, _opponentHp - 1);
            else _playerHp = Math.Max(0, _playerHp - 1);

            UpdateHud();
            SafeSend(new { type = "hp", p = _playerHp, o = _opponentHp });

            if (_opponentHp <= 0 || _playerHp <= 0)
            {
                bool youWin = _opponentHp <= 0;
                EndGame(youWin, fromNetwork: false);
            }
        }

        private void SendPlayerState()
        {
            if (_network == null || !_network.IsConnected || _player == null) return;
            SafeSend(new { type = "state", x = _player.Left, y = _player.Top });
        }

        private void ProcessNetworkMessage(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return;
            if (msg == "START_GAME") return;

            try
            {
                using var doc = JsonDocument.Parse(msg);
                var root = doc.RootElement;
                if (!root.TryGetProperty("type", out var tp)) return;
                string type = tp.GetString();

                switch (type)
                {
                    case "hello":
                        if (root.TryGetProperty("name", out var n))
                        {
                            _opponentName = n.GetString() ?? _opponentName;
                            this.Text = (_isHost ? "[HOST] " : "[CLIENT] ") + "Room: " + _roomId + "  - vs " + _opponentName;
                            lblStatusGame.Text = "Đã kết nối với " + _opponentName;
                            UpdateHud();
                        }
                        break;

                    case "state":
                        if (_opponent == null) return;
                        if (root.TryGetProperty("x", out var x) && root.TryGetProperty("y", out var y))
                        {
                            _opponent.Left = x.GetInt32();
                            _opponent.Top = y.GetInt32();
                        }
                        break;

                    case "shoot":
                        SpawnOpponentBullet();
                        break;

                    case "hp":
                        if (root.TryGetProperty("p", out var p) && root.TryGetProperty("o", out var o))
                        {
                            _playerHp = p.GetInt32();
                            _opponentHp = o.GetInt32();
                            UpdateHud();
                        }
                        break;

                    case "result":
                        if (_gameEnded) return;
                        if (root.TryGetProperty("winner", out var wProp))
                        {
                            string winnerName = wProp.GetString();
                            bool youWin = string.Equals(winnerName, _localName, StringComparison.OrdinalIgnoreCase);
                            EndGame(youWin, fromNetwork: true);
                        }
                        break;
                }
            }
            catch
            {
                // bỏ qua message lỗi
            }
        }

        private void OnDisconnectedUI()
        {
            if (_gameEnded) return;
            _gameEnded = true;
            _gameTimer?.Stop();
            lblStatusGame.Text = "Mất kết nối.";
            MessageBox.Show("Kết nối bị ngắt. Quay lại lobby hoặc tạo phòng khác.", "Ngắt kết nối",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void SafeSend(object obj)
        {
            try
            {
                if (_network != null && _network.IsConnected)
                {
                    string json = JsonSerializer.Serialize(obj);
                    _network.Send(json);
                }
            }
            catch { }
        }

        private void FirePlayerBullet()
        {
            if (_gameEnded || _player == null || _playerBullet == null) return;
            if (_playerBullet.Visible) return;

            _playerBullet.Visible = true;
            _playerBullet.Left = _player.Left + _player.Width / 2 - _playerBullet.Width / 2;
            _playerBullet.Top = (_playerBulletDir < 0)
                ? (_player.Top - _playerBullet.Height)
                : _player.Bottom;

            SafeSend(new { type = "shoot" });
        }

        private void SpawnOpponentBullet()
        {
            if (_opponent == null || _opponentBullet == null) return;
            if (_opponentBullet.Visible) return;

            _opponentBullet.Visible = true;
            _opponentBullet.Left = _opponent.Left + _opponent.Width / 2 - _opponentBullet.Width / 2;
            _opponentBullet.Top = (_opponentBulletDir < 0)
                ? (_opponent.Top - _opponentBullet.Height)
                : _opponent.Bottom;
        }

        private void EndGame(bool youWin, bool fromNetwork)
        {
            if (_gameEnded) return;
            _gameEnded = true;
            _gameTimer?.Stop();
            _paused = false;
            _pausePanel.Visible = false;

            if (_isHost)
            {
                try
                {
                    string winner = youWin ? _localName : _opponentName;
                    string loser = youWin ? _opponentName : _localName;
                    Database.RecordMatchHistory(winner, loser);
                }
                catch { }
            }

            if (!fromNetwork)
                SafeSend(new { type = "result", winner = youWin ? _localName : _opponentName });

            MessageBox.Show(
                (youWin ? "🎉 Bạn THẮNG!\n" : "💥 Bạn THUA!\n") +
                $"Bạn: {_localName}\nĐối thủ: {_opponentName}",
                "Kết thúc trận đấu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            try
            {
                btnExit.Text = "Thoát trận";
                btnExit.Visible = true;
                btnExit.Enabled = true;
                btnExit.BringToFront();
            }
            catch { }
        }

        private void AnyControl_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right ||
                e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.IsInputKey = true;
            }
        }

        private void GAMESOLO_KeyDown(object? sender, KeyEventArgs e)
        {
            if (_gameEnded || _paused) return;

            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) _goLeft = true;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) _goRight = true;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W) _goUp = true;
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S) _goDown = true;
        }

        private void GAMESOLO_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) _goLeft = false;
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) _goRight = false;
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W) _goUp = false;
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S) _goDown = false;
        }

        private void btnExit_Click(object sender, EventArgs e) => ConfirmExit();

        private void ConfirmExit()
        {
            var ask = MessageBox.Show("Thoát trận và quay lại lobby?", "Thoát trận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ask == DialogResult.Yes)
            {
                try { _gameTimer?.Stop(); } catch { }
                try { (_network as IDisposable)?.Dispose(); _network = null; } catch { }
                Close();
            }
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _gameTimer?.Stop();
                (_network as IDisposable)?.Dispose();
                _network = null;
            }
            catch { }
        }

        private void GAMESOLO_Load(object sender, EventArgs e)
        {
        }
    }
}

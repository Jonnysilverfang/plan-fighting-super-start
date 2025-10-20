using Kien;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Kien
{
    public partial class Form6 : Form
    {
        private readonly NetworkManager _net;
        private readonly bool _isHost;
        private readonly string _roomId;

        private RectangleF my, peer;
        private float myHP = 100, peerHP = 100;
        private float speed = 3.5f;

        private List<Bullet> myBullets = new List<Bullet>();
        private List<Bullet> peerBullets = new List<Bullet>();

        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer sendTimer;

        private bool _gameStarted = false;
        private HashSet<Keys> _keys = new HashSet<Keys>();
        private Point mousePosition = Point.Empty;

        private class Bullet
        {
            public PointF P;
            public PointF V;
            public float R = 4;
        }

        public Form6(NetworkManager network, bool isHost, string roomId)
        {
            InitializeComponent();
            DoubleBuffered = true;

            _net = network;
            _isHost = isHost;
            _roomId = roomId;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(900, 600);

            my = _isHost
                ? new RectangleF(80, ClientSize.Height * 3 / 4f - 20, 24, 24)
                : new RectangleF(ClientSize.Width - 104, ClientSize.Height / 4f - 20, 24, 24);

            peer = new RectangleF(0, 0, 24, 24); // nhận từ POS

            gameTimer = new System.Windows.Forms.Timer { Interval = 16 };
            gameTimer.Tick += GameTick;

            sendTimer = new System.Windows.Forms.Timer { Interval = 40 };
            sendTimer.Tick += (s, e) => _net.Send($"POS:{my.X:0}:{my.Y:0}:{myHP:0}");

            this.MouseMove += (s, e) => mousePosition = e.Location;
            this.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left && myBullets.Count < 8)
                    ShootTowards(mousePosition);
            };

            KeyPreview = true;
            _net.OnMessageReceived += Net_OnMessageReceived;
            StartGame();
        }

        private void ShootTowards(Point target)
        {
            var dir = new PointF(target.X - (my.X + my.Width / 2), target.Y - (my.Y + my.Height / 2));
            var len = Math.Max(1f, (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y));
            var v = new PointF(dir.X / len * 8f, dir.Y / len * 8f);
            var b = new Bullet { P = new PointF(my.X + my.Width / 2, my.Y + my.Height / 2), V = v };
            myBullets.Add(b);
            _net.Send($"SHOT:{b.P.X:0}:{b.P.Y:0}:{b.V.X:0.0}:{b.V.Y:0.0}");
        }

        private void StartGame()
        {
            _gameStarted = true;
            gameTimer.Start();
            sendTimer.Start();
        }

        private void Net_OnMessageReceived(string msg)
        {
            if (msg.StartsWith("POS:"))
            {
                var sp = msg.Split(':');
                if (sp.Length == 4 &&
                    float.TryParse(sp[1], out float x) &&
                    float.TryParse(sp[2], out float y) &&
                    float.TryParse(sp[3], out float hp))
                {
                    peer.X = x;
                    peer.Y = y;
                    peerHP = hp;
                    Invalidate();
                }
                return;
            }

            if (msg.StartsWith("SHOT:"))
            {
                var sp = msg.Split(':');
                if (sp.Length == 5 &&
                    float.TryParse(sp[1], out float x) &&
                    float.TryParse(sp[2], out float y) &&
                    float.TryParse(sp[3], out float vx) &&
                    float.TryParse(sp[4], out float vy))
                {
                    peerBullets.Add(new Bullet { P = new PointF(x, y), V = new PointF(vx, vy) });
                }
                return;
            }

            if (msg.StartsWith("HIT:"))
            {
                var sp = msg.Split(':');
                if (sp.Length == 2 && float.TryParse(sp[1], out float dmg))
                {
                    myHP = Math.Max(0, myHP - dmg);
                    if (myHP <= 0) EndMatch(false);
                }
                return;
            }
        }

        private void EndMatch(bool win)
        {
            gameTimer.Stop();
            sendTimer.Stop();
            MessageBox.Show(win ? "Bạn THẮNG!" : "Bạn THUA!", "Kết quả");
            btnExit.Enabled = true;
        }

        private void GameTick(object sender, EventArgs e)
        {
            float dx = 0, dy = 0;

            if (_keys.Contains(Keys.Left)) dx -= speed;
            if (_keys.Contains(Keys.Right)) dx += speed;
            if (_keys.Contains(Keys.Up)) dy -= speed;
            if (_keys.Contains(Keys.Down)) dy += speed;

            my.X = Math.Max(0, Math.Min(ClientSize.Width - my.Width, my.X + dx));

            if (_isHost)
                my.Y = Math.Max(ClientSize.Height / 2, Math.Min(ClientSize.Height - my.Height, my.Y + dy));
            else
                my.Y = Math.Max(0, Math.Min(ClientSize.Height / 2 - my.Height, my.Y + dy));

            _keys.Clear();

            UpdateBullets(myBullets, ref peer, ref peerHP, true);
            UpdateBullets(peerBullets, ref my, ref myHP, false);

            Invalidate();

            if (peerHP <= 0) EndMatch(true);
            if (myHP <= 0) EndMatch(false);
        }

        private void UpdateBullets(List<Bullet> list, ref RectangleF target, ref float targetHP, bool notifyHit)
        {
            int i = 0;
            while (i < list.Count)
            {
                var b = list[i];
                b.P = new PointF(b.P.X + b.V.X, b.P.Y + b.V.Y);

                bool outScreen = b.P.X < -10 || b.P.X > ClientSize.Width + 10 ||
                                 b.P.Y < -10 || b.P.Y > ClientSize.Height + 10;
                bool hit = target.Contains(b.P);

                if (hit)
                {
                    const float dmg = 10;
                    if (notifyHit) _net.Send($"HIT:{dmg:0}");
                    targetHP = Math.Max(0, targetHP - dmg);
                }

                if (outScreen || hit) list.RemoveAt(i);
                else i++;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            g.FillRectangle(Brushes.DodgerBlue, my);
            g.FillRectangle(Brushes.OrangeRed, peer);

            foreach (var b in myBullets)
                g.FillEllipse(Brushes.Red, b.P.X - b.R, b.P.Y - b.R, b.R * 2, b.R * 2);
            foreach (var b in peerBullets)
                g.FillEllipse(Brushes.Red, b.P.X - b.R, b.P.Y - b.R, b.R * 2, b.R * 2);

            g.DrawRectangle(Pens.Black, 10, 10, 200, 20);
            g.FillRectangle(Brushes.Green, 10, 10, myHP * 2, 20);

            g.DrawRectangle(Pens.Black, ClientSize.Width - 210, 10, 200, 20);
            g.FillRectangle(Brushes.Red, ClientSize.Width - 210, 10, peerHP * 2, 20);
        }

        private void btnExit_Click(object sender, EventArgs e) => this.Close();

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            gameTimer?.Stop();
            sendTimer?.Stop();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!_gameStarted) return base.ProcessCmdKey(ref msg, keyData);
            switch (keyData)
            {
                case Keys.Left:
                case Keys.A:
                    _keys.Add(Keys.Left); return true;
                case Keys.Right:
                case Keys.D:
                    _keys.Add(Keys.Right); return true;
                case Keys.Up:
                case Keys.W:
                    _keys.Add(Keys.Up); return true;
                case Keys.Down:
                case Keys.S:
                    _keys.Add(Keys.Down); return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}

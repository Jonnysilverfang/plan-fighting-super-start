using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kien
{
    public class LANBroadcast : IDisposable
    {
        public const int BROADCAST_PORT = 9876;

        private UdpClient _sender;
        private UdpClient _listener;
        private CancellationTokenSource _ctsBroadcast;
        private CancellationTokenSource _ctsListen;

        public event Action<string, string, int> OnRoomFound;

        public void StartBroadcast(string roomId, int gamePort, int intervalMs = 1000)
        {
            StopBroadcast();

            _ctsBroadcast = new CancellationTokenSource();
            _sender = new UdpClient();
            _sender.EnableBroadcast = true;

            Task.Run(async () =>
            {
                var epBroadcast = new IPEndPoint(IPAddress.Broadcast, BROADCAST_PORT);
                var epLoopback = new IPEndPoint(IPAddress.Loopback, BROADCAST_PORT); // để “1 máy 2 tab”
                var token = _ctsBroadcast.Token;

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        string payload = $"ROOM:{roomId};PORT:{gamePort}";
                        byte[] data = Encoding.UTF8.GetBytes(payload);
                        // phát ra LAN
                        await _sender.SendAsync(data, data.Length, epBroadcast);
                        // phát về localhost để client cùng máy nhận được
                        await _sender.SendAsync(data, data.Length, epLoopback);
                    }
                    catch
                    {
                        // bỏ qua 1 tick lỗi
                    }
                    try { await Task.Delay(intervalMs, token); } catch { }
                }
            }, _ctsBroadcast.Token);
        }

        public void StopBroadcast()
        {
            try { _ctsBroadcast?.Cancel(); } catch { }
            try { _sender?.Close(); } catch { }
            _ctsBroadcast = null;
            _sender = null;
        }

        public void StartListen(string roomIdFilter)
        {
            StopListen();

            _ctsListen = new CancellationTokenSource();

            // Cho phép nhiều tiến trình cùng bind cổng UDP này (để chạy 2 instance trên cùng máy)
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try { sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true); } catch { }
            try { sock.ExclusiveAddressUse = false; } catch { }
            sock.Bind(new IPEndPoint(IPAddress.Any, BROADCAST_PORT));
            _listener = new UdpClient { Client = sock };

            Task.Run(async () =>
            {
                var token = _ctsListen.Token; // giữ local token để tránh _ctsListen == null
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        var result = await _listener.ReceiveAsync();
                        string msg = Encoding.UTF8.GetString(result.Buffer);

                        if (!msg.StartsWith("ROOM:")) continue;

                        string roomId = null;
                        int port = -1;
                        var parts = msg.Split(';');
                        foreach (var p in parts)
                        {
                            var kv = p.Split(':');
                            if (kv.Length != 2) continue;
                            string k = kv[0].Trim().ToUpperInvariant();
                            string v = kv[1].Trim();
                            if (k == "ROOM") roomId = v;
                            else if (k == "PORT" && int.TryParse(v, out int prt)) port = prt;
                        }

                        if (roomId == null || port <= 0) continue;
                        if (!string.Equals(roomId, roomIdFilter, StringComparison.Ordinal)) continue;

                        string hostIP = result.RemoteEndPoint.Address.ToString();
                        OnRoomFound?.Invoke(roomId, hostIP, port);
                    }
                    catch (ObjectDisposedException)
                    {
                        // listener đã đóng do StopListen(); thoát vòng lặp
                        break;
                    }
                    catch
                    {
                        // tiếp tục listen
                    }
                }
            }, _ctsListen.Token);
        }

        public void StopListen()
        {
            try { _ctsListen?.Cancel(); } catch { }
            try { _listener?.Close(); } catch { }
            _ctsListen = null;
            _listener = null;
        }

        public void Dispose()
        {
            StopBroadcast();
            StopListen();
        }
    }
}

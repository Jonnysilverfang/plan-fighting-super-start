using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Collections.Generic;

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
                var token = _ctsBroadcast.Token;

                // 🔹 Lấy broadcast address thật của mạng LAN hiện tại (vd: 192.168.1.255)
                var localBroadcast = GetLocalBroadcastAddress();

                // Danh sách endpoint cần gửi
                var broadcastTargets = new List<IPEndPoint>
                {
                    new IPEndPoint(localBroadcast, BROADCAST_PORT),
                    new IPEndPoint(IPAddress.Loopback, BROADCAST_PORT) // cho 2 tab cùng máy
                };

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        string payload = $"ROOM:{roomId};PORT:{gamePort}";
                        byte[] data = Encoding.UTF8.GetBytes(payload);

                        foreach (var ep in broadcastTargets)
                        {
                            try { await _sender.SendAsync(data, data.Length, ep); } catch { }
                        }
                    }
                    catch { }

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

            // Cho phép nhiều tiến trình cùng bind UDP port này (chạy nhiều instance)
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try { sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true); } catch { }
            try { sock.ExclusiveAddressUse = false; } catch { }
            sock.Bind(new IPEndPoint(IPAddress.Any, BROADCAST_PORT));
            _listener = new UdpClient { Client = sock };

            Task.Run(async () =>
            {
                var token = _ctsListen.Token;

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

        // 🔹 Lấy broadcast address của card mạng thật đang dùng
        private IPAddress GetLocalBroadcastAddress()
        {
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus != OperationalStatus.Up) continue;
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback ||
                    ni.NetworkInterfaceType == NetworkInterfaceType.Tunnel) continue;

                var ipProps = ni.GetIPProperties();
                foreach (var ua in ipProps.UnicastAddresses)
                {
                    if (ua.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        if (ua.IPv4Mask == null) continue;
                        return GetBroadcastAddress(ua.Address, ua.IPv4Mask);
                    }
                }
            }

            // fallback nếu không tìm được
            return IPAddress.Broadcast;
        }

        // 🔹 Tính broadcast address từ IP + subnet mask
        private IPAddress GetBroadcastAddress(IPAddress address, IPAddress subnetMask)
        {
            byte[] ipAdressBytes = address.GetAddressBytes();
            byte[] subnetMaskBytes = subnetMask.GetAddressBytes();

            if (ipAdressBytes.Length != subnetMaskBytes.Length)
                throw new ArgumentException("IP và Subnet Mask không khớp.");

            byte[] broadcastAddress = new byte[ipAdressBytes.Length];
            for (int i = 0; i < broadcastAddress.Length; i++)
            {
                broadcastAddress[i] = (byte)(ipAdressBytes[i] | (subnetMaskBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddress);
        }
    }
}

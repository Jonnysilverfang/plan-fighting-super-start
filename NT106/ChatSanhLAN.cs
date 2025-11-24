using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace plan_fighting_super_start
{
    /// <summary>
    /// Chat sảnh LAN (chưa vào phòng).
    /// - Dùng UDP broadcast để mọi người trong LAN đều nhận được chat.
    /// - Định dạng gói tin: LOBBY|tenNguoi|noiDung
    /// - CHỐNG ECHO: Bỏ qua mọi gói đến từ IP của chính máy (không hiển thị 2 lần).
    /// </summary>
    public class ChatSanhLAN : IDisposable
    {
        public const int CONG_CHAT_SANH = 9877;

        private UdpClient _sender;
        private UdpClient _listener;
        private CancellationTokenSource _ctsNghe;
        private Task _tacVuNghe;

        // Tập IP nội bộ để nhận ra gói do chính mình gửi (anti-echo)
        private HashSet<string> _localIPv4Set;

        /// <summary>
        /// Sự kiện khi nhận 1 tin chat sảnh.
        /// </summary>
        public event Action<string, string> NhanTinSanh; // (tenNguoi, noiDung)

        // ==================== NGHE CHAT SANH ====================
        /// <summary>
        /// Bắt đầu lắng nghe tin chat sảnh trong LAN.
        /// </summary>
        public void BatDauNghe()
        {
            DungNghe();

            _ctsNghe = new CancellationTokenSource();
            _listener = new UdpClient(CONG_CHAT_SANH);
            _listener.EnableBroadcast = true;

            // Build danh sách IP của chính máy để chống echo
            _localIPv4Set = LayTatCaIPv4CuaMay();

            _tacVuNghe = Task.Run(async () =>
            {
                var token = _ctsNghe.Token;

                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        UdpReceiveResult result = await _listener.ReceiveAsync().ConfigureAwait(false);
                        string remoteIp = result.RemoteEndPoint.Address.ToString();

                        // === CHỐNG ECHO: nếu gói đến từ IP của chính máy → bỏ qua
                        if (_localIPv4Set.Contains(remoteIp))
                            continue;

                        string text = Encoding.UTF8.GetString(result.Buffer);

                        // Format: LOBBY|ten|noiDung
                        var parts = text.Split('|');
                        if (parts.Length >= 3 && parts[0] == "LOBBY")
                        {
                            string tenNguoi = parts[1];
                            string prefix = $"LOBBY|{tenNguoi}|";
                            string noiDung = text.Length > prefix.Length
                                ? text.Substring(prefix.Length)
                                : string.Empty;

                            NhanTinSanh?.Invoke(tenNguoi, noiDung);
                        }
                    }
                }
                catch (ObjectDisposedException)
                {
                    // socket đóng
                }
                catch (TaskCanceledException)
                {
                    // ignore
                }
                catch
                {
                    // ignore
                }
            }, _ctsNghe.Token);
        }

        public void DungNghe()
        {
            try { _ctsNghe?.Cancel(); } catch { }
            try { _listener?.Close(); } catch { }

            _tacVuNghe = null;
            _listener = null;
            _ctsNghe = null;
        }

        // ==================== GỬI CHAT SANH ====================
        /// <summary>
        /// Gửi 1 tin chat sảnh tới mọi người trong LAN.
        /// </summary>
        public async Task GuiTinSanhAsync(string tenNguoi, string noiDung)
        {
            if (_sender == null)
            {
                _sender = new UdpClient();
                _sender.EnableBroadcast = true;
            }

            string msg = $"LOBBY|{tenNguoi}|{noiDung}";
            byte[] data = Encoding.UTF8.GetBytes(msg);

            try
            {
                // Gửi broadcast toàn mạng
                var ep = new IPEndPoint(IPAddress.Broadcast, CONG_CHAT_SANH);
                await _sender.SendAsync(data, data.Length, ep).ConfigureAwait(false);
            }
            catch
            {
                // ignore
            }
        }

        // ==================== UTIL: LẤY IP CỦA CHÍNH MÁY ====================
        private static HashSet<string> LayTatCaIPv4CuaMay()
        {
            var set = new HashSet<string>(StringComparer.Ordinal);
            foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus != OperationalStatus.Up) continue;
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;

                var ipProps = ni.GetIPProperties();
                foreach (var ua in ipProps.UnicastAddresses)
                {
                    if (ua.Address.AddressFamily != AddressFamily.InterNetwork) continue;
                    set.Add(ua.Address.ToString());
                }
            }
            // Thêm loopback để chắc ăn (một số adapter có thể trả về 127.0.0.1)
            set.Add(IPAddress.Loopback.ToString());
            return set;
        }

        public void Dispose()
        {
            DungNghe();
            try { _sender?.Close(); } catch { }
            _sender = null;
            _localIPv4Set = null;
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kien
{
    /// <summary>
    /// Quản lý TCP host/client, gửi/nhận message dạng dòng (line-based).
    /// </summary>
    public class NetworkManager : IDisposable
    {
        public event Action OnPeerConnected;
        public event Action<string> OnMessageReceived;
        public event Action OnDisconnected;

        private TcpListener _server;
        private TcpClient _client;
        private NetworkStream _stream;
        private StreamReader _reader;
        private StreamWriter _writer;
        private CancellationTokenSource _cts;
        private Task _listenTask;

        public bool IsHost { get; private set; }
        public bool IsConnected => _client != null && _client.Connected;

        public static bool IsNetworkAvailable()
        {
            if (!NetworkInterface.GetIsNetworkAvailable()) return false;
            // có ít nhất 1 NIC hoạt động thật sự (không phải loopback/tunnel)
            var nicUp = NetworkInterface.GetAllNetworkInterfaces()
                .Any(nic =>
                    nic.OperationalStatus == OperationalStatus.Up &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel);
            return nicUp;
        }

        public void StartHost(string roomId, int port = 8888)
        {
            if (!IsNetworkAvailable())
                throw new InvalidOperationException("Không có mạng LAN.");

            Stop();

            IsHost = true;
            _cts = new CancellationTokenSource();
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();

            // Chờ client kết nối
            Task.Run(async () =>
            {
                try
                {
                    _client = await _server.AcceptTcpClientAsync();
                    SetupStreams();
                    OnPeerConnected?.Invoke();
                    _listenTask = Task.Run(ListenLoop, _cts.Token);
                }
                catch
                {
                    OnDisconnected?.Invoke();
                }
            }, _cts.Token);
        }

        public async Task JoinHost(string hostIP, int port = 8888)
        {
            if (!IsNetworkAvailable())
                throw new InvalidOperationException("Không có mạng LAN.");

            Stop();

            IsHost = false;
            _cts = new CancellationTokenSource();
            _client = new TcpClient();
            try
            {
                await _client.ConnectAsync(IPAddress.Parse(hostIP), port);
                SetupStreams();
                _listenTask = Task.Run(ListenLoop, _cts.Token);
            }
            catch
            {
                Stop();
                throw;
            }
        }

        private void SetupStreams()
        {
            _stream = _client.GetStream();
            _reader = new StreamReader(_stream, Encoding.UTF8, false, 1024, true);
            _writer = new StreamWriter(_stream, Encoding.UTF8, 1024, true) { AutoFlush = true };
        }

        public void Send(string message)
        {
            if (_writer == null) return;
            try { _writer.WriteLine(message); } catch { }
        }

        private async Task ListenLoop()
        {
            try
            {
                while (!_cts.IsCancellationRequested && _client != null && _client.Connected)
                {
                    string line = await _reader.ReadLineAsync().ConfigureAwait(false);
                    if (line == null) break; // remote closed
                    OnMessageReceived?.Invoke(line);
                }
            }
            catch
            {
                // ignore, treat as disconnect
            }
            finally
            {
                OnDisconnected?.Invoke();
            }
        }

        public void Stop()
        {
            try { _cts?.Cancel(); } catch { }
            try { _reader?.Dispose(); } catch { }
            try { _writer?.Dispose(); } catch { }
            try { _stream?.Close(); } catch { }
            try { _client?.Close(); } catch { }
            try { _server?.Stop(); } catch { }

            _reader = null;
            _writer = null;
            _stream = null;
            _client = null;
            _server = null;
            _listenTask = null;
            _cts = null;
        }

        public void Dispose() => Stop();
    }
}

using System;
using System.Net.NetworkInformation;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace plan_fighting_super_start
{
    public class NetworkManager : IDisposable
    {
        // =============== CONFIG ===============
        // Đổi URL này bằng WebSocket URL của API Gateway
        private static readonly Uri Endpoint =
            new Uri("wss://78r8y26ose.execute-api.ap-southeast-1.amazonaws.com/production");

        private ClientWebSocket _ws;
        private CancellationTokenSource _cts;

        private string _roomId;
        private readonly string _username;

        // Event callback
        public event Action OnPeerConnected;
        public event Action<string> OnMessageReceived;
        public event Action OnDisconnected;

        public bool IsConnected => _ws != null && _ws.State == WebSocketState.Open;

        // ======================================
        public NetworkManager() : this(AccountData.Username ?? "Player") { }

        public NetworkManager(string username)
        {
            _username = string.IsNullOrWhiteSpace(username) ? "Player" : username;
        }

        public static bool IsNetworkAvailable()
        {
            try { return NetworkInterface.GetIsNetworkAvailable(); }
            catch { return true; }
        }

        // =============== HOST TẠO PHÒNG ===============
        public void StartHost(string roomId, int portIgnored)
        {
            _roomId = roomId;

            _ = EnsureConnectedAndSendAsync(new
            {
                action = "createRoom",
                roomId = _roomId,
                username = _username
            });
        }

        // =============== CLIENT JOIN PHÒNG ===============
        public async Task JoinHost(string roomId, int portIgnored)
        {
            _roomId = roomId;

            await EnsureConnectedAndSendAsync(new
            {
                action = "joinRoom",
                roomId = _roomId,
                username = _username
            });
        }

        // Kết nối WebSocket (nếu chưa) rồi gửi request đầu tiên
        private async Task EnsureConnectedAndSendAsync(object firstPayload)
        {
            if (!IsConnected)
            {
                _ws = new ClientWebSocket();
                _cts = new CancellationTokenSource();

                await _ws.ConnectAsync(Endpoint, _cts.Token);

                // Start background receiver
                _ = Task.Run(ReceiveLoopAsync);
            }

            await SendCoreAsync(firstPayload);
        }

        // =============== RECEIVE LOOP ===============
        private async Task ReceiveLoopAsync()
        {
            var buffer = new byte[8192];

            try
            {
                while (_ws.State == WebSocketState.Open)
                {
                    var result = await _ws.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);
                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    var jsonText = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    HandleIncoming(jsonText);
                }
            }
            catch
            {
                // ignore
            }
            finally
            {
                OnDisconnected?.Invoke();
            }
        }

        // =============== HANDLE MESSAGE ===============
        private void HandleIncoming(string text)
        {
            try
            {
                using var doc = JsonDocument.Parse(text);
                var root = doc.RootElement;

                // Lambda gửi dạng:
                // { "event": "xxx", ... }

                if (root.TryGetProperty("event", out var evProp))
                {
                    string ev = evProp.GetString();

                    switch (ev)
                    {
                        case "ROOM_CREATED":
                            // host đã tạo phòng xong
                            break;

                        case "JOIN_OK":
                            // client join thành công
                            break;

                        case "START_GAME":
                            // đủ 2 người → bật nút Start
                            OnPeerConnected?.Invoke();
                            break;

                        case "GAME_UPDATE":
                            if (root.TryGetProperty("data", out var dataProp))
                            {
                                OnMessageReceived?.Invoke(dataProp.GetString());
                            }
                            break;

                        case "ROOM_NOT_FOUND":
                            OnMessageReceived?.Invoke("ROOM_NOT_FOUND");
                            break;
                    }
                }
                else
                {
                    // Message thuần text
                    OnMessageReceived?.Invoke(text);
                }
            }
            catch
            {
                OnMessageReceived?.Invoke(text);
            }
        }

        // =============== SEND GAME MESSAGE ===============
        public void Send(string msg)
        {
            if (!IsConnected || string.IsNullOrEmpty(msg) || string.IsNullOrEmpty(_roomId))
                return;

            _ = SendCoreAsync(new
            {
                action = "gameMessage",
                roomId = _roomId,
                data = msg
            });
        }

        // Send JSON payload
        private async Task SendCoreAsync(object payload)
        {
            string json = JsonSerializer.Serialize(payload);
            byte[] data = Encoding.UTF8.GetBytes(json);

            await _ws.SendAsync(
                new ArraySegment<byte>(data),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }

        // Dispose
        public void Dispose()
        {
            try { _cts?.Cancel(); } catch { }
            try { _ws?.Abort(); } catch { }
            try { _ws?.Dispose(); } catch { }
        }
    }
}

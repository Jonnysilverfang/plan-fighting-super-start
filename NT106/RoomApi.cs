using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace plan_fighting_super_start
{
    public static class RoomApi
    {
        // Base URL của HTTP API gamesolo
        private const string ROOM_API_URL =
            "https://r42i9q5tl1.execute-api.ap-southeast-1.amazonaws.com/sololan";

        private static readonly HttpClient client = new HttpClient();

        private static async Task<bool> PostAsync(object payload)
        {
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var resp = await client.PostAsync(ROOM_API_URL, content);
                return resp.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Host tạo phòng
        public static Task<bool> CreateRoomAsync(string roomId, string hostName)
        {
            return PostAsync(new
            {
                action = "create",
                roomId = roomId,
                host = hostName
            });
        }

        // Client join phòng
        public static Task<bool> JoinRoomAsync(string roomId, string guestName)
        {
            return PostAsync(new
            {
                action = "join",
                roomId = roomId,
                guest = guestName
            });
        }

        // Host bấm BẮT ĐẦU
        public static Task<bool> StartRoomAsync(string roomId, string hostName)
        {
            return PostAsync(new
            {
                action = "start",
                roomId = roomId,
                host = hostName
            });
        }

        // ĐÁNH DẤU PHÒNG KẾT THÚC (END)
        public static Task<bool> EndRoomAsync(string roomId)
        {
            return PostAsync(new
            {
                action = "end",
                roomId = roomId
            });
        }
    }
}

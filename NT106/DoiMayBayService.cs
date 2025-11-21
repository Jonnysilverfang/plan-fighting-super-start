using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace plan_fighting_super_start
{
    public class DoiMayBayService
    {
        // ✅ Gọi đúng path /post/plane
        private const string API_PLANE =
            "https://ux7ir7zqt1.execute-api.ap-southeast-1.amazonaws.com/post/plane";

        private static readonly HttpClient http = new HttpClient();

        private class PlaneResponse
        {
            public int planeIndex { get; set; }
            public string key { get; set; }
            public string downloadUrl { get; set; }
        }

        // planeIndex: 1..5 (máy bay S3)
        public async Task<(Image? Image, string? Key)> DoiMayBayAsync(int planeIndex)
        {
            var bodyObj = new { plane = planeIndex };
            string json = JsonSerializer.Serialize(bodyObj);

            using var resp = await http.PostAsync(
                API_PLANE,
                new StringContent(json, Encoding.UTF8, "application/json"));

            // Nếu lỗi thì ném message rõ ràng hơn
            if (!resp.IsSuccessStatusCode)
            {
                string errBody = await resp.Content.ReadAsStringAsync();
                throw new Exception(
                    $"API đổi máy bay lỗi {(int)resp.StatusCode} {resp.StatusCode}: {errBody}");
            }

            string text = await resp.Content.ReadAsStringAsync();

            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<PlaneResponse>(text, opt);

            if (data == null || string.IsNullOrEmpty(data.downloadUrl))
                return (null, null);

            byte[] bytes = await http.GetByteArrayAsync(data.downloadUrl);
            using var ms = new MemoryStream(bytes);
            var img = Image.FromStream(ms);

            return (img, data.key);
        }
    }
}

using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace plan_fighting_super_start
{
    public class S3ImageService
    {
        // 🔹 API của bạn
        private const string API_IMAGE = "https://2cd28uutce.execute-api.ap-southeast-1.amazonaws.com/image";

        private static readonly HttpClient http = new HttpClient();

        // =====================================================================
        // 1) UPLOAD ẢNH THEO FILE GỐC (nếu bạn cần)
        // =====================================================================
        public async Task<string> UploadByOriginalNameAsync(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            string base64 = Convert.ToBase64String(bytes);

            string fileName = Path.GetFileName(filePath);
            string contentType = GetContentType(filePath);

            var body = new
            {
                action = "upload",
                fileName = fileName,
                imageBase64 = base64,
                contentType = contentType
            };

            string json = JsonSerializer.Serialize(body);

            using var resp = await http.PostAsync(
                API_IMAGE,
                new StringContent(json, Encoding.UTF8, "application/json"));

            resp.EnsureSuccessStatusCode();

            string text = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(text);

            return doc.RootElement.GetProperty("key").GetString();
        }

        // =====================================================================
        // 2) UPLOAD ẢNH THEO TÊN USER => avatars/{username}.png
        // =====================================================================
        public async Task<string> UploadImageAsync(string filePath, string username)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            string base64 = Convert.ToBase64String(bytes);

            string fileName = $"avatars/{username}.png";   // 🔥 key cố định theo username
            string contentType = GetContentType(filePath);

            var body = new
            {
                action = "upload",
                fileName = fileName,
                imageBase64 = base64,
                contentType = contentType
            };

            string json = JsonSerializer.Serialize(body);

            using var resp = await http.PostAsync(
                API_IMAGE,
                new StringContent(json, Encoding.UTF8, "application/json"));

            resp.EnsureSuccessStatusCode();

            string text = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(text);

            return doc.RootElement.GetProperty("key").GetString();   // => "avatars/user.png"
        }

        // =====================================================================
        // 3) DOWNLOAD ẢNH TỪ S3 THEO KEY
        // =====================================================================
        public async Task<Image> GetImageAsync(string key)
        {
            var body = new
            {
                action = "getUrl",
                key = key
            };

            string json = JsonSerializer.Serialize(body);

            using var resp = await http.PostAsync(
                API_IMAGE,
                new StringContent(json, Encoding.UTF8, "application/json"));

            resp.EnsureSuccessStatusCode();

            string text = await resp.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(text);

            string url = doc.RootElement.GetProperty("downloadUrl").GetString();

            // tải file từ presigned-url
            byte[] bytes = await http.GetByteArrayAsync(url);
            using var ms = new MemoryStream(bytes);

            return Image.FromStream(ms);
        }

        // =====================================================================
        // 🔧 HÀM PHỤ: xác định loại ảnh
        // =====================================================================
        private string GetContentType(string filePath)
        {
            string ext = Path.GetExtension(filePath)?.ToLower();

            return ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                _ => "image/png"
            };
        }
    }
}

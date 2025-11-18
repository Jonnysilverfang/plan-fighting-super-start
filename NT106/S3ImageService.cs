using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace plan_fighting_super_start
{
    public class S3ImageService
    {
        private const string API_IMAGE = "https://2cd28uutce.execute-api.ap-southeast-1.amazonaws.com/image";

        private static readonly HttpClient http = new HttpClient();

        // 🔹 Cache avatar theo key (avatars/username.png)
        private static readonly Dictionary<string, Image> _imageCache = new Dictionary<string, Image>();

        // =====================================================================
        // 1) UPLOAD ẢNH THEO TÊN USER => avatars/{username}.png
        //     - Tự động RESIZE + NÉN ảnh để nhanh hơn
        // =====================================================================
        public async Task<string> UploadImageAsync(string filePath, string username)
        {
            // Tối ưu: resize + nén ảnh trước khi upload
            byte[] optimizedBytes = OptimizeImage(filePath);

            string fileName = $"avatars/{username}.png";   // key trên S3
            string contentType = "image/png";              // mình xuất PNG/JPEG đều ok

            string base64 = Convert.ToBase64String(optimizedBytes);

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

            string key = doc.RootElement.GetProperty("key").GetString();

            // Xoá cache cũ (nếu user đổi avatar)
            if (!string.IsNullOrEmpty(key) && _imageCache.ContainsKey(key))
            {
                _imageCache[key].Dispose();
                _imageCache.Remove(key);
            }

            return key;   // "avatars/username.png"
        }

        // =====================================================================
        // 2) LẤY ẢNH TỪ S3 THEO KEY – CÓ CACHE
        // =====================================================================
        public async Task<Image> GetImageAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            // 🔹 Nếu đã có trong cache thì trả luôn, khỏi gọi S3
            if (_imageCache.TryGetValue(key, out var cachedImg) && cachedImg != null)
            {
                // Trả bản clone cho an toàn
                return (Image)cachedImg.Clone();
            }

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

            var img = Image.FromStream(ms);

            // Lưu vào cache để lần sau không phải tải lại
            if (_imageCache.ContainsKey(key))
            {
                _imageCache[key].Dispose();
                _imageCache[key] = (Image)img.Clone();
            }
            else
            {
                _imageCache.Add(key, (Image)img.Clone());
            }

            return img;
        }

        // =====================================================================
        // 3) HÀM TỐI ƯU ẢNH (resize + nén)
        // =====================================================================
        private byte[] OptimizeImage(string filePath)
        {
            using var original = Image.FromFile(filePath);

            // Giới hạn tối đa 256x256 cho avatar
            const int maxSize = 256;
            int w = original.Width;
            int h = original.Height;

            // Nếu ảnh đã nhỏ rồi thì vẫn convert sang PNG/JPEG cho gọn
            if (w <= maxSize && h <= maxSize)
            {
                using var ms = new MemoryStream();
                original.Save(ms, ImageFormat.Png);   // hoặc ImageFormat.Jpeg
                return ms.ToArray();
            }

            // Tính tỉ lệ scale
            float scale = (float)maxSize / Math.Max(w, h);
            int newW = (int)(w * scale);
            int newH = (int)(h * scale);

            // Resize với chất lượng tốt
            using var bmp = new Bitmap(newW, newH);
            using (var g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.DrawImage(original, 0, 0, newW, newH);
            }

            using var msOut = new MemoryStream();
            bmp.Save(msOut, ImageFormat.Png);   // PNG chất lượng cao, dung lượng vẫn nhỏ vì size 256x256
            return msOut.ToArray();
        }
    }
}

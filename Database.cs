using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

namespace plan_fighting_super_start
{
    // Model account từ Lambda
    public class AccountModel
    {
        public string? Username { get; set; } // Dùng string?
        public string? Password { get; set; }
        public int Gold { get; set; }
        public int UpgradeHP { get; set; }
        public int UpgradeDamage { get; set; }
        public int Level { get; set; }
    }

    // 🚨 KHẮC PHỤC LỖI TRỌNG YẾU: Model lịch sử đấu
    public class ClientMatchHistoryModel
    {
        public string? Id { get; set; }
        public string? WinnerUsername { get; set; }
        public string? LoserUsername { get; set; }
        public string? MatchDate { get; set; } // 🚨 Đã sửa thành string?
    }

    public static class Database
    {
        // 🚨 URL API GATEWAY CỦA BẠN
        private static readonly string ApiBaseUrl = "https://4xt8f352xe.execute-api.ap-southeast-1.amazonaws.com/";
        private static readonly HttpClient client = new HttpClient();

        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,
            PropertyNameCaseInsensitive = true // Giúp đọc JSON từ Python dễ dàng hơn
        };

        // --- ACCOUNT LOGIC ---

        public static bool CheckLogin(string username, string password)
        {
            try
            {
                var bodyData = new { Username = username, Password = password };
                string jsonBody = JsonSerializer.Serialize(bodyData, JsonOptions);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = client.PostAsync(ApiBaseUrl + "account/login", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var msg = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show($"Đăng nhập thất bại! {msg}", "Lỗi Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                var account = response.Content.ReadFromJsonAsync<AccountModel>(JsonOptions).Result;
                if (account != null)
                {
                    AccountData.Username = account.Username;
                    AccountData.Gold = account.Gold;
                    AccountData.UpgradeHP = account.UpgradeHP;
                    AccountData.UpgradeDamage = account.UpgradeDamage;
                    AccountData.Level = account.Level;
                    return true;
                }

                MessageBox.Show("Đăng nhập thất bại! Không nhận được dữ liệu account.");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối khi đăng nhập: " + ex.Message);
                return false;
            }
        }

        public static bool LoadAccountData(string username)
        {
            try
            {
                var response = client.GetAsync(ApiBaseUrl + "account/" + username).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var msg = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show($"Tải dữ liệu thất bại! API trả về {response.StatusCode}. Chi tiết: {msg}");
                    return false;
                }

                var account = response.Content.ReadFromJsonAsync<AccountModel>(JsonOptions).Result;
                if (account != null)
                {
                    AccountData.Username = account.Username;
                    AccountData.Gold = account.Gold;
                    AccountData.UpgradeHP = account.UpgradeHP;
                    AccountData.UpgradeDamage = account.UpgradeDamage;
                    AccountData.Level = account.Level;
                    return true;
                }

                MessageBox.Show("Tải dữ liệu thất bại! Không nhận được account.");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối khi tải dữ liệu: " + ex.Message);
                return false;
            }
        }

        public static bool RegisterAccount(string username, string password)
        {
            try
            {
                var bodyData = new { Username = username, Password = password };
                string jsonBody = JsonSerializer.Serialize(bodyData, JsonOptions);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = client.PostAsync(ApiBaseUrl + "account/register", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var msg = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show($"Đăng ký thất bại! {msg}", "Lỗi Đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message);
                return false;
            }
        }

        public static void UpdateAccountData()
        {
            try
            {
                var bodyData = new
                {
                    Username = AccountData.Username,
                    Gold = AccountData.Gold,
                    UpgradeHP = AccountData.UpgradeHP,
                    UpgradeDamage = AccountData.UpgradeDamage,
                    Level = AccountData.Level
                };

                string jsonBody = JsonSerializer.Serialize(bodyData, JsonOptions);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = client.PutAsync(ApiBaseUrl + "account/update", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var msg = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show("Cập nhật thất bại! " + msg, "Lỗi Cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        // --- MATCH HISTORY LOGIC ---

        public static void RecordMatchHistory(string winnerUsername, string loserUsername)
        {
            try
            {
                var matchData = new
                {
                    WinnerUsername = winnerUsername,
                    LoserUsername = loserUsername
                };

                string jsonBody = JsonSerializer.Serialize(matchData, JsonOptions);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = client.PostAsync(ApiBaseUrl + "matchhistory/add", content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var msg = response.Content.ReadAsStringAsync().Result;
                    MessageBox.Show($"Lỗi ghi lịch sử đấu! {msg}", "Lỗi Ghi log", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối khi ghi lịch sử đấu: " + ex.Message);
            }
        }

        public static List<ClientMatchHistoryModel> GetMatchHistory(string? username)
        {
            // 🚨 SỬA LỖI: Kiểm tra null trước khi gọi API
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Lỗi: Không tìm thấy Username để tải lịch sử đấu.", "Lỗi Dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<ClientMatchHistoryModel>();
            }

            try
            {
                var history = Task.Run(async () =>
                {
                    var response = await client.GetAsync(ApiBaseUrl + "matchhistory/" + username);

                    if (!response.IsSuccessStatusCode)
                    {
                        var msg = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Lỗi tải lịch sử đấu: " + msg, "Lỗi Lịch sử đấu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return new List<ClientMatchHistoryModel>();
                    }

                    var historyList = await response.Content.ReadFromJsonAsync<List<ClientMatchHistoryModel>>(JsonOptions);
                    return historyList ?? new List<ClientMatchHistoryModel>();
                }).GetAwaiter().GetResult();

                return history;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối khi tải lịch sử đấu: " + ex.Message);
                return new List<ClientMatchHistoryModel>();
            }
        }
    }
}
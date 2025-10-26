using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using static plan_fighting_super_start.GAMEBOSS;

namespace plan_fighting_super_start
{
    // Class phụ để deserialize JSON từ API
    public class AccountModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Gold { get; set; }
        public int UpgradeHP { get; set; }
        public int UpgradeDamage { get; set; }
        public int Level { get; set; }
    }

    public static class Database
    {
        private static readonly string ApiBaseUrl = "https://localhost:7250/api/Account/";
        private static readonly HttpClient client = new HttpClient();

        // Đăng nhập qua API
        public static bool CheckLogin(string username, string password)
        {
            try
            {
                var body = new { Username = username, Password = password };
                var response = client.PostAsJsonAsync(ApiBaseUrl + "login", body).Result;

                if (!response.IsSuccessStatusCode)
                    return false;

                var account = response.Content.ReadFromJsonAsync<AccountModel>().Result;
                if (account != null)
                {
                    AccountData.Username = account.Username;
                    AccountData.Gold = account.Gold;
                    AccountData.UpgradeHP = account.UpgradeHP;
                    AccountData.UpgradeDamage = account.UpgradeDamage;
                    AccountData.Level = account.Level;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
                return false;
            }
        }

        // Lấy dữ liệu từ API
        public static bool LoadAccountData(string username)
        {
            try
            {
                var response = client.GetAsync(ApiBaseUrl + username).Result;
                if (!response.IsSuccessStatusCode)
                    return false;

                var account = response.Content.ReadFromJsonAsync<AccountModel>().Result;
                if (account != null)
                {
                    AccountData.Username = account.Username;
                    AccountData.Gold = account.Gold;
                    AccountData.UpgradeHP = account.UpgradeHP;
                    AccountData.UpgradeDamage = account.UpgradeDamage;
                    AccountData.Level = account.Level;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
                return false;
            }
        }

        // Đăng ký
        public static bool RegisterAccount(string username, string password)
        {
            try
            {
                var body = new { Username = username, Password = password };
                var response = client.PostAsJsonAsync(ApiBaseUrl + "register", body).Result;
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký: " + ex.Message);
                return false;
            }
        }

        // Cập nhật
        public static void UpdateAccountData()
        {
            try
            {
                var body = new
                {
                    Username = AccountData.Username,
                    Gold = AccountData.Gold,
                    UpgradeHP = AccountData.UpgradeHP,
                    UpgradeDamage = AccountData.UpgradeDamage,
                    Level = AccountData.Level
                };

                var response = client.PutAsJsonAsync(ApiBaseUrl + "update", body).Result;
                if (!response.IsSuccessStatusCode)
                    MessageBox.Show("Cập nhật thất bại!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }
    }
}

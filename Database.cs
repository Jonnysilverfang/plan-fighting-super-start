using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Kien
{
    public static class Database
    {
        private static readonly string connectionString =
            "Server=tcp:database-2.c3gooss4uoib.ap-southeast-2.rds.amazonaws.com,1433;" +
            "Database=UserDB;User ID=admin;Password=Kiennguly;" +
            "Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        // Kiểm tra login
        public static bool CheckLogin(string username, string password)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            const string sql = "SELECT COUNT(*) FROM dbo.Accounts WHERE Username=@user AND Password=@pass";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@user", SqlDbType.NVarChar, 50).Value = username;
            cmd.Parameters.Add("@pass", SqlDbType.NVarChar, 200).Value = password;
            return (int)cmd.ExecuteScalar()! > 0;
        }

        // Lấy thông tin account
        public static bool LoadAccountData(string username)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            const string sql = "SELECT Gold, UpgradeHP, UpgradeDamage, Level FROM dbo.Accounts WHERE Username=@user";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@user", SqlDbType.NVarChar, 50).Value = username;
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                AccountData.Username = username;
                AccountData.Gold = reader.GetInt32(0);
                AccountData.UpgradeHP = reader.GetInt32(1);
                AccountData.UpgradeDamage = reader.GetInt32(2);
                AccountData.Level = reader.GetInt32(3); // Lấy level từ cơ sở dữ liệu
                return true;
            }
            return false;
        }

        // Đăng ký
        public static bool RegisterAccount(string username, string password)
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();

            // Check trùng
            using (var checkCmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Accounts WHERE Username=@user", conn))
            {
                checkCmd.Parameters.Add("@user", SqlDbType.NVarChar, 50).Value = username;
                if ((int)checkCmd.ExecuteScalar()! > 0) return false;
            }

            // Insert
            using (var cmd = new SqlCommand(
                @"INSERT INTO dbo.Accounts (Username, Password, Gold, UpgradeHP, UpgradeDamage, Level)
                  VALUES (@user, @pass, 0, 0, 0, 1)", conn)) // Set level default as 1
            {
                cmd.Parameters.Add("@user", SqlDbType.NVarChar, 50).Value = username;
                cmd.Parameters.Add("@pass", SqlDbType.NVarChar, 200).Value = password;
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        // Cập nhật vàng, nâng cấp và level
        public static void UpdateAccountData()
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            using var cmd = new SqlCommand(
                @"UPDATE dbo.Accounts
                  SET Gold=@gold, UpgradeHP=@hp, UpgradeDamage=@dmg, Level=@level
                  WHERE Username=@user", conn);

            cmd.Parameters.Add("@gold", SqlDbType.Int).Value = AccountData.Gold;
            cmd.Parameters.Add("@hp", SqlDbType.Int).Value = AccountData.UpgradeHP;
            cmd.Parameters.Add("@dmg", SqlDbType.Int).Value = AccountData.UpgradeDamage;
            cmd.Parameters.Add("@level", SqlDbType.Int).Value = AccountData.Level; // Lưu Level
            cmd.Parameters.Add("@user", SqlDbType.NVarChar, 50).Value = AccountData.Username;
            cmd.ExecuteNonQuery();
        }
    }
}

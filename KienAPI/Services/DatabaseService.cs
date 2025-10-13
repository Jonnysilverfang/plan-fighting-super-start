using System.Data;
using Microsoft.Data.SqlClient;
using KienAPI.Models;

namespace KienAPI.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        public bool CheckLogin(string username, string password)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = "SELECT COUNT(*) FROM Accounts WHERE Username=@u AND Password=@p";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);
            return (int)cmd.ExecuteScalar()! > 0;
        }

        public bool RegisterAccount(AccountData acc)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using (var check = new SqlCommand("SELECT COUNT(*) FROM Accounts WHERE Username=@u", conn))
            {
                check.Parameters.AddWithValue("@u", acc.Username);
                if ((int)check.ExecuteScalar()! > 0) return false;
            }

            string sql = "INSERT INTO Accounts (Username, Password, Gold, UpgradeHP, UpgradeDamage, Level) VALUES (@u,@p,0,0,0,1)";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", acc.Username);
            cmd.Parameters.AddWithValue("@p", acc.Password);
            cmd.ExecuteNonQuery();
            return true;
        }

        public bool UpdateAccount(AccountData acc)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = @"UPDATE Accounts 
                           SET Gold=@g, UpgradeHP=@hp, UpgradeDamage=@dmg, Level=@lv 
                           WHERE Username=@u";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@g", acc.Gold);
            cmd.Parameters.AddWithValue("@hp", acc.UpgradeHP);
            cmd.Parameters.AddWithValue("@dmg", acc.UpgradeDamage);
            cmd.Parameters.AddWithValue("@lv", acc.Level);
            cmd.Parameters.AddWithValue("@u", acc.Username);
            return cmd.ExecuteNonQuery() > 0;
        }

        public AccountData? GetAccount(string username)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            string sql = "SELECT Username, Gold, UpgradeHP, UpgradeDamage, Level FROM Accounts WHERE Username=@u";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", username);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;
            return new AccountData
            {
                Username = reader.GetString(0),
                Gold = reader.GetInt32(1),
                UpgradeHP = reader.GetInt32(2),
                UpgradeDamage = reader.GetInt32(3),
                Level = reader.GetInt32(4)
            };
        }
    }
}

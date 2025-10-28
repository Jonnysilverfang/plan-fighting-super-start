using Microsoft.Data.SqlClient; // [GÓI CẦN THIẾT]
using Leaderboard.Api.Models;

namespace Leaderboard.Api.Services
{
    public class AccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        // --- Truy vấn cơ bản cho AccountModel ---
        private AccountModel MapToAccount(SqlDataReader reader)
        {
            return new AccountModel
            {
                Username = reader["Username"].ToString()!,
                Password = reader["Password"].ToString()!,
                Gold = (int)reader["Gold"],
                UpgradeHP = (int)reader["UpgradeHP"],
                UpgradeDamage = (int)reader["UpgradeDamage"],
                Level = (int)reader["Level"]
            };
        }

        // --- 1. Lấy dữ liệu Account ---
        public AccountModel? GetAccount(string username)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT * FROM Accounts WHERE Username = @Username";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Username", username);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapToAccount(reader);
            }
            return null;
        }

        // --- 2. Đăng ký Account ---
        public bool RegisterAccount(AccountModel account)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                connection.Open();
                var sql = "INSERT INTO Accounts (Username, Password, Gold, UpgradeHP, UpgradeDamage, Level) VALUES (@U, @P, @G, @H, @D, @L)";
                using var command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@U", account.Username);
                command.Parameters.AddWithValue("@P", account.Password);
                command.Parameters.AddWithValue("@G", 100);
                command.Parameters.AddWithValue("@H", 100);
                command.Parameters.AddWithValue("@D", 10);
                command.Parameters.AddWithValue("@L", 1);

                return command.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex) when (ex.Number == 2627) // Lỗi khóa chính (Username trùng)
            {
                return false;
            }
            catch { return false; }
        }

        // --- 3. Cập nhật chỉ số Account ---
        public bool UpdateAccount(AccountModel account)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "UPDATE Accounts SET Gold = @G, UpgradeHP = @H, UpgradeDamage = @D, Level = @L WHERE Username = @U";
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@G", account.Gold);
            command.Parameters.AddWithValue("@H", account.UpgradeHP);
            command.Parameters.AddWithValue("@D", account.UpgradeDamage);
            command.Parameters.AddWithValue("@L", account.Level);
            command.Parameters.AddWithValue("@U", account.Username);

            return command.ExecuteNonQuery() > 0;
        }
    }
}
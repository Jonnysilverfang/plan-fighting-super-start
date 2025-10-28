using System.ComponentModel.DataAnnotations;

namespace Leaderboard.Api.Models
{
    // Lớp ánh xạ tới bảng Account trong SQL Server
    public class AccountModel
    {
        // Primary Key - Tên đăng nhập
        [Key]
        public string Username { get; set; } = string.Empty;

        // Cần mã hóa (hashing) trong thực tế.
        public string Password { get; set; } = string.Empty;

        // Dữ liệu chỉ số game
        public int Gold { get; set; } = 100; // Giá trị khởi tạo
        public int UpgradeHP { get; set; } = 100; // Giá trị khởi tạo
        public int UpgradeDamage { get; set; } = 10; // Giá trị khởi tạo
        public int Level { get; set; } = 1; // Giá trị khởi tạo
    }

    // Lớp dùng cho yêu cầu Đăng nhập/Đăng ký
    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
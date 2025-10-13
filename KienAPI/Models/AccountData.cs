namespace KienAPI.Models
{
    public class AccountData
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public int Gold { get; set; }
        public int UpgradeHP { get; set; }
        public int UpgradeDamage { get; set; }
        public int Level { get; set; }
    }
}

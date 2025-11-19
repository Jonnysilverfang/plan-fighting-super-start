using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class giftcode : Form
    {
        // Danh sách giftcode hợp lệ
        private static readonly Dictionary<string, GiftcodeReward> Giftcodes =
            new Dictionary<string, GiftcodeReward>(StringComparer.OrdinalIgnoreCase)
            {
                { "VIP666",      new GiftcodeReward("Giftcode tân thủ",   gold: 1000,  damageBonus: 5)  },
                { "NT106VIP",    new GiftcodeReward("Giftcode VIP",       gold: 500,  damageBonus: 15) },
                { "PLANFIGHTING",new GiftcodeReward("Giftcode sự kiện",   gold: 1000, damageBonus: 25) },
                { "10DIEMNT106",new GiftcodeReward("Giftcode sự kiện",   gold: 1000, damageBonus: 25) }
            };
        
        // File lưu các giftcode đã dùng (username|code)
        private static readonly string UsedCodeFilePath =
            Path.Combine(Application.StartupPath, "used_giftcodes.txt");

        // Lưu các key đã dùng trong RAM (key = username|code)
        private static readonly HashSet<string> UsedCodeKeys =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        // Static constructor: chạy 1 lần khi class giftcode được dùng lần đầu
        static giftcode()
        {
            try
            {
                if (File.Exists(UsedCodeFilePath))
                {
                    var lines = File.ReadAllLines(UsedCodeFilePath);
                    foreach (var line in lines)
                    {
                        var key = line.Trim();
                        if (!string.IsNullOrEmpty(key))
                        {
                            UsedCodeKeys.Add(key);
                        }
                    }
                }
            }
            catch
            {
                // Nếu lỗi đọc file thì bỏ qua, coi như chưa dùng code nào
            }
        }

        public giftcode()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Không cần xử lý gì cũng được
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string code = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(code))
            {
                MessageBox.Show(
                    "Vui lòng nhập giftcode.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                textBox1.Focus();
                return;
            }

            // Kiểm tra code hợp lệ
            if (!Giftcodes.TryGetValue(code, out var reward))
            {
                MessageBox.Show(
                    "Giftcode không hợp lệ.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            // Lấy username hiện tại (nếu chưa login thì cho là chuỗi rỗng)
            string username = AccountData.Username ?? string.Empty;

            // KEY = "username|code" → mỗi tài khoản chỉ dùng được 1 lần trên 1 máy
            string key = $"{username}|{code}";

            // Nếu muốn "mỗi máy chỉ dùng 1 lần, bất kể tài khoản"
            // thì đổi dòng trên thành: string key = code;

            // Kiểm tra xem đã dùng giftcode này chưa
            if (UsedCodeKeys.Contains(key))
            {
                MessageBox.Show(
                    "Giftcode này bạn đã sử dụng trước đó rồi.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                textBox1.SelectAll();
                textBox1.Focus();
                return;
            }

            // Áp dụng thưởng
            AccountData.Gold += reward.Gold;
            AccountData.UpgradeDamage += reward.DamageBonus;

            // Cập nhật lên server nếu có
            try
            {
                Database.UpdateAccountData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Đã cộng thưởng vào tài khoản trên máy, " +
                    "nhưng cập nhật lên server bị lỗi.\n\nChi tiết: " + ex.Message,
                    "Lỗi cập nhật server",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            // Ghi lại là đã dùng giftcode này
            try
            {
                UsedCodeKeys.Add(key);
                File.AppendAllLines(UsedCodeFilePath, new[] { key });
            }
            catch
            {
                // Nếu ghi file lỗi thì thôi, lần sau có thể xài lại,
                // nhưng cho đồ án thì khả năng này hiếm gặp.
            }

            MessageBox.Show(
                $"Đổi giftcode thành công!\n\n" +
                $"+{reward.Gold} vàng\n" +
                $"+{reward.DamageBonus} sát thương",
                "Thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            textBox1.Clear();
            textBox1.Focus();
        }

        // Class lưu thông tin thưởng
        private sealed class GiftcodeReward
        {
            public string Description { get; }
            public int Gold { get; }
            public int DamageBonus { get; }

            public GiftcodeReward(string description, int gold, int damageBonus)
            {
                Description = description;
                Gold = gold;
                DamageBonus = damageBonus;
            }
        }
    }
}

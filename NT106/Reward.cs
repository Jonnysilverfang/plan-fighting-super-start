using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    public partial class Reward : Form
    {
        // Thiết kế quà theo mốc level:
        //  - mỗi mốc nhận 1 lần duy nhất (dùng flag lưu trong DB)
        //  - bạn chỉnh lại số Damage/Gold theo ý muốn

        private class RewardInfo
        {
            public int Level { get; set; }
            public int DamageBonus { get; set; }
            public int GoldBonus { get; set; }
        }

        private readonly List<RewardInfo> _rewards = new List<RewardInfo>
        {
            new RewardInfo { Level = 10,  DamageBonus = 500,  GoldBonus = 1000 },
            new RewardInfo { Level = 50, DamageBonus = 1500, GoldBonus = 5000 },
            new RewardInfo { Level = 100,DamageBonus = 3000, GoldBonus = 20000 }
        };

        public Reward()
        {
            InitializeComponent();

            this.Load += Reward_Load;
            btnClaimReward.Click += BtnClaimReward_Click;
        }

        private void Reward_Load(object? sender, EventArgs e)
        {
            UpdateRewardUI();
        }

        // Kiểm tra 1 mốc đã được claim chưa dựa vào flag trong AccountData
        private bool IsLevelClaimed(int level)
        {
            switch (level)
            {
                case 10: return AccountData.RewardLv10Claimed;
                case 50: return AccountData.RewardLv50Claimed;
                case 100: return AccountData.RewardLv100Claimed;
                default: return true;
            }
        }

        // Đánh dấu 1 mốc đã claim
        private void SetLevelClaimed(int level)
        {
            switch (level)
            {
                case 10:
                    AccountData.RewardLv10Claimed = true;
                    break;
                case 50:
                    AccountData.RewardLv50Claimed = true;
                    break;
                case 100:
                    AccountData.RewardLv100Claimed = true;
                    break;
            }
        }

        private void UpdateRewardUI()
        {
            int level = AccountData.Level;

            List<string> available = new List<string>();

            foreach (var r in _rewards)
            {
                if (level >= r.Level && !IsLevelClaimed(r.Level))
                {
                    available.Add(
                        $"Lv {r.Level}: +{r.DamageBonus} Damage, +{r.GoldBonus} Gold (chưa nhận)"
                    );
                }
            }

            if (available.Count == 0)
            {
                labelInfo.Text =
                    $"Level hiện tại: {level}\n" +
                    "Bạn không còn phần thưởng level nào chưa nhận.\n" +
                    "Mỗi mốc chỉ được nhận 1 lần duy nhất.";
                btnClaimReward.Enabled = false;
            }
            else
            {
                labelInfo.Text =
                    $"Level hiện tại: {level}\n" +
                    "Các mốc có thể nhận:\n" +
                    string.Join("\n", available) +
                    "\n\nNhấn nút để nhận tất cả phần thưởng chưa nhận.";
                btnClaimReward.Enabled = true;
            }
        }

        private void BtnClaimReward_Click(object? sender, EventArgs e)
        {
            int level = AccountData.Level;
            int totalDamage = 0;
            int totalGold = 0;
            List<string> detail = new List<string>();

            foreach (var r in _rewards)
            {
                if (level >= r.Level && !IsLevelClaimed(r.Level))
                {
                    totalDamage += r.DamageBonus;
                    totalGold += r.GoldBonus;
                    SetLevelClaimed(r.Level);

                    detail.Add($"Lv {r.Level}: +{r.DamageBonus} Damage, +{r.GoldBonus} Gold");
                }
            }

            if (totalDamage == 0 && totalGold == 0)
            {
                MessageBox.Show(
                    "Bạn không còn phần thưởng nào để nhận.",
                    "Reward",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                UpdateRewardUI();
                return;
            }

            // Cộng vào tài khoản hiện tại
            AccountData.UpgradeDamage += totalDamage;
            AccountData.Gold += totalGold;

            // Lưu cả Damage, Gold và cờ RewardLvXXClaimed lên DB
            Database.UpdateAccountData();

            MessageBox.Show(
                $"Nhận phần thưởng thành công!\n" +
                $"Tổng cộng: +{totalDamage} Damage, +{totalGold} Gold\n\n" +
                string.Join("\n", detail),
                "Reward",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

            UpdateRewardUI();
        }
    }
}

namespace plan_fighting_super_start
{
    partial class Reward
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelInfo = new System.Windows.Forms.Label();
            this.btnClaimReward = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                                    | System.Windows.Forms.AnchorStyles.Bottom)
                                    | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInfo.AutoSize = false;
            this.labelInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelInfo.Location = new System.Drawing.Point(12, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(360, 110);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "Thông tin phần thưởng sẽ hiển thị ở đây.";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btnClaimReward
            // 
            this.btnClaimReward.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClaimReward.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClaimReward.Location = new System.Drawing.Point(120, 130);
            this.btnClaimReward.Name = "btnClaimReward";
            this.btnClaimReward.Size = new System.Drawing.Size(150, 35);
            this.btnClaimReward.TabIndex = 1;
            this.btnClaimReward.Text = "Nhận phần thưởng";
            this.btnClaimReward.UseVisualStyleBackColor = true;
            // (Sự kiện Click đã gán trong Reward.cs, nên Designer không gán ở đây)
            // 
            // Reward
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 181);
            this.Controls.Add(this.btnClaimReward);
            this.Controls.Add(this.labelInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Reward";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phần thưởng Level";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button btnClaimReward;
    }
}

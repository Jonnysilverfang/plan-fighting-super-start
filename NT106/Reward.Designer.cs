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
            labelInfo = new Label();
            btnClaimReward = new Button();
            SuspendLayout();
            // 
            // labelInfo
            // 
            labelInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelInfo.BackColor = Color.Transparent;
            labelInfo.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            labelInfo.ForeColor = Color.White;
            labelInfo.Location = new Point(14, 12);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(411, 147);
            labelInfo.TabIndex = 0;
            labelInfo.Text = "Thông tin phần thưởng sẽ hiển thị ở đây.";
            // 
            // btnClaimReward
            // 
            btnClaimReward.Anchor = AnchorStyles.Bottom;
            btnClaimReward.BackColor = Color.Transparent;
            btnClaimReward.FlatStyle = FlatStyle.Flat;
            btnClaimReward.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnClaimReward.ForeColor = Color.White;
            btnClaimReward.Location = new Point(137, 183);
            btnClaimReward.Margin = new Padding(3, 4, 3, 4);
            btnClaimReward.Name = "btnClaimReward";
            btnClaimReward.Size = new Size(171, 45);
            btnClaimReward.TabIndex = 1;
            btnClaimReward.Text = "Nhận phần thưởng";
            btnClaimReward.UseVisualStyleBackColor = false;
            // 
            // Reward
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resource.Gemini_Generated_Image_5ka7of5ka7of5ka7;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(439, 241);
            Controls.Add(btnClaimReward);
            Controls.Add(labelInfo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Reward";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Phần thưởng Level";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button btnClaimReward;
    }
}

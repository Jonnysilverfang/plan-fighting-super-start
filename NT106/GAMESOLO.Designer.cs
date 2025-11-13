using System.Drawing;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    partial class GAMESOLO
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnExit;
        private Label lblStatusGame;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnExit = new Button();
            lblStatusGame = new Label();
            SuspendLayout();
            // 
            // btnExit
            // 
            btnExit.Location = new Point(10, 10);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(80, 30);
            btnExit.TabIndex = 0;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;

            // ⭐ tránh ăn focus / Space click trước khi kết thúc trận
            btnExit.TabStop = false;
            btnExit.Visible = false;
            btnExit.PreviewKeyDown += AnyControl_PreviewKeyDown;
            // 
            // lblStatusGame
            // 
            lblStatusGame.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblStatusGame.AutoSize = true;
            lblStatusGame.BackColor = Color.Transparent;
            lblStatusGame.ForeColor = Color.White;
            lblStatusGame.Location = new Point(12, 570);
            lblStatusGame.Name = "lblStatusGame";
            lblStatusGame.Size = new Size(105, 15);
            lblStatusGame.TabIndex = 1;
            lblStatusGame.Text = "Đang chuẩn bị…";
            lblStatusGame.PreviewKeyDown += AnyControl_PreviewKeyDown;
            // 
            // GAMESOLO
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(900, 600);
            Controls.Add(lblStatusGame);
            Controls.Add(btnExit);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "GAMESOLO";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LAN Shooting Game";
            FormClosing += Form6_FormClosing;

            // ⭐ bắt phím ở cấp form
            KeyDown += GAMESOLO_KeyDown;
            KeyUp += GAMESOLO_KeyUp;
            PreviewKeyDown += AnyControl_PreviewKeyDown;

            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}

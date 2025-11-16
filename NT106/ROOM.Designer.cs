using System.Drawing;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    partial class Room
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelTitle;
        private Label labelRoom;
        private TextBox txtRoomID;
        private Button btnCreateRoom;
        private Button btnJoinRoom;
        private Button btnStartGame;
        private Label lblStatus;
        private Button btnHistory;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            labelTitle = new Label();
            labelRoom = new Label();
            txtRoomID = new TextBox();
            btnCreateRoom = new Button();
            btnJoinRoom = new Button();
            btnStartGame = new Button();
            lblStatus = new Label();
            btnHistory = new Button();
            SuspendLayout();
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.FromArgb(90, 0, 0, 0);
            labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(0, 255, 255);
            labelTitle.Location = new Point(30, 25);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(364, 37);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "SOLO LAN – 2 NGƯỜI CHƠI";
            // 
            // labelRoom
            // 
            labelRoom.AutoSize = true;
            labelRoom.BackColor = Color.Transparent;
            labelRoom.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            labelRoom.ForeColor = Color.FromArgb(0, 192, 192);
            labelRoom.Location = new Point(32, 100);
            labelRoom.Name = "labelRoom";
            labelRoom.Size = new Size(134, 23);
            labelRoom.TabIndex = 1;
            labelRoom.Text = "Room ID (6 số):";
            // 
            // txtRoomID
            // 
            txtRoomID.BackColor = Color.FromArgb(15, 22, 45);
            txtRoomID.BorderStyle = BorderStyle.FixedSingle;
            txtRoomID.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            txtRoomID.ForeColor = Color.FromArgb(0, 255, 255);
            txtRoomID.Location = new Point(180, 96);
            txtRoomID.Margin = new Padding(3, 4, 3, 4);
            txtRoomID.MaxLength = 6;
            txtRoomID.Name = "txtRoomID";
            txtRoomID.Size = new Size(146, 30);
            txtRoomID.TabIndex = 2;
            txtRoomID.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCreateRoom
            // 
            btnCreateRoom.BackColor = Color.FromArgb(10, 20, 40);
            btnCreateRoom.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            btnCreateRoom.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 140);
            btnCreateRoom.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 100);
            btnCreateRoom.FlatStyle = FlatStyle.Flat;
            btnCreateRoom.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnCreateRoom.ForeColor = Color.FromArgb(0, 192, 192);
            btnCreateRoom.Location = new Point(36, 155);
            btnCreateRoom.Margin = new Padding(3, 4, 3, 4);
            btnCreateRoom.Name = "btnCreateRoom";
            btnCreateRoom.Size = new Size(290, 45);
            btnCreateRoom.TabIndex = 3;
            btnCreateRoom.Text = "Tạo phòng (Host)";
            btnCreateRoom.UseVisualStyleBackColor = false;
            btnCreateRoom.Click += btnCreateRoom_Click;
            // 
            // btnJoinRoom
            // 
            btnJoinRoom.BackColor = Color.FromArgb(10, 20, 40);
            btnJoinRoom.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            btnJoinRoom.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 140);
            btnJoinRoom.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 100);
            btnJoinRoom.FlatStyle = FlatStyle.Flat;
            btnJoinRoom.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnJoinRoom.ForeColor = Color.FromArgb(0, 192, 192);
            btnJoinRoom.Location = new Point(36, 215);
            btnJoinRoom.Margin = new Padding(3, 4, 3, 4);
            btnJoinRoom.Name = "btnJoinRoom";
            btnJoinRoom.Size = new Size(290, 45);
            btnJoinRoom.TabIndex = 4;
            btnJoinRoom.Text = "Tham gia phòng (Client)";
            btnJoinRoom.UseVisualStyleBackColor = false;
            btnJoinRoom.Click += btnJoinRoom_Click;
            // 
            // btnStartGame
            // 
            btnStartGame.BackColor = Color.FromArgb(10, 20, 40);
            btnStartGame.Enabled = false;
            btnStartGame.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            btnStartGame.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 140);
            btnStartGame.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 100);
            btnStartGame.FlatStyle = FlatStyle.Flat;
            btnStartGame.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnStartGame.ForeColor = Color.FromArgb(0, 255, 255);
            btnStartGame.Location = new Point(36, 275);
            btnStartGame.Margin = new Padding(3, 4, 3, 4);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(290, 50);
            btnStartGame.TabIndex = 5;
            btnStartGame.Text = "BẮT ĐẦU";
            btnStartGame.UseVisualStyleBackColor = false;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoEllipsis = true;
            lblStatus.BackColor = Color.FromArgb(110, 0, 0, 0);
            lblStatus.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblStatus.ForeColor = Color.FromArgb(0, 192, 192);
            lblStatus.Location = new Point(36, 400);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(540, 70);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Trạng thái: Chưa tạo/tham gia phòng.";
            // 
            // btnHistory
            // 
            btnHistory.BackColor = Color.FromArgb(10, 20, 40);
            btnHistory.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            btnHistory.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 140);
            btnHistory.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 100);
            btnHistory.FlatStyle = FlatStyle.Flat;
            btnHistory.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnHistory.ForeColor = Color.FromArgb(0, 192, 192);
            btnHistory.Location = new Point(36, 335);
            btnHistory.Margin = new Padding(3, 4, 3, 4);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(290, 45);
            btnHistory.TabIndex = 7;
            btnHistory.Text = "📜 Lịch sử đấu";
            btnHistory.UseVisualStyleBackColor = false;
            btnHistory.Click += btnHistory_Click;
            // 
            // Room
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(10, 15, 30);
            BackgroundImage = Properties.Resource.Gemini_Generated_Image_5ka7of5ka7of5ka7;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(620, 500);
            Controls.Add(btnHistory);
            Controls.Add(lblStatus);
            Controls.Add(btnStartGame);
            Controls.Add(btnJoinRoom);
            Controls.Add(btnCreateRoom);
            Controls.Add(txtRoomID);
            Controls.Add(labelRoom);
            Controls.Add(labelTitle);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Room";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lobby – Solo LAN";
            FormClosing += Form5_FormClosing;
            Load += Room_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}

namespace plan_fighting_super_start
{
    partial class Room
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelRoom;
        private System.Windows.Forms.TextBox txtRoomID;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnHistory; // <-- nút mới

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
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(0, 192, 192);
            labelTitle.Location = new Point(27, 24);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(324, 37);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Solo LAN – 2 người chơi";
            // 
            // labelRoom
            // 
            labelRoom.AutoSize = true;
            labelRoom.BackColor = Color.Transparent;
            labelRoom.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            labelRoom.ForeColor = Color.FromArgb(0, 192, 192);
            labelRoom.Location = new Point(30, 99);
            labelRoom.Name = "labelRoom";
            labelRoom.Size = new Size(134, 23);
            labelRoom.TabIndex = 1;
            labelRoom.Text = "Room ID (6 số):";
            // 
            // txtRoomID
            // 
            txtRoomID.Location = new Point(170, 95);
            txtRoomID.Margin = new Padding(3, 4, 3, 4);
            txtRoomID.MaxLength = 6;
            txtRoomID.Name = "txtRoomID";
            txtRoomID.Size = new Size(146, 27);
            txtRoomID.TabIndex = 2;
            // 
            // btnCreateRoom
            // 
            btnCreateRoom.BackColor = Color.Transparent;
            btnCreateRoom.FlatStyle = FlatStyle.Flat;
            btnCreateRoom.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnCreateRoom.ForeColor = Color.FromArgb(0, 192, 192);
            btnCreateRoom.Location = new Point(30, 152);
            btnCreateRoom.Margin = new Padding(3, 4, 3, 4);
            btnCreateRoom.Name = "btnCreateRoom";
            btnCreateRoom.Size = new Size(255, 43);
            btnCreateRoom.TabIndex = 3;
            btnCreateRoom.Text = "Tạo phòng (Host)";
            btnCreateRoom.UseVisualStyleBackColor = false;
            btnCreateRoom.Click += btnCreateRoom_Click;
            // 
            // btnJoinRoom
            // 
            btnJoinRoom.BackColor = Color.Transparent;
            btnJoinRoom.FlatStyle = FlatStyle.Flat;
            btnJoinRoom.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnJoinRoom.ForeColor = Color.FromArgb(0, 192, 192);
            btnJoinRoom.Location = new Point(30, 216);
            btnJoinRoom.Margin = new Padding(3, 4, 3, 4);
            btnJoinRoom.Name = "btnJoinRoom";
            btnJoinRoom.Size = new Size(255, 43);
            btnJoinRoom.TabIndex = 4;
            btnJoinRoom.Text = "Tham gia phòng (Client)";
            btnJoinRoom.UseVisualStyleBackColor = false;
            btnJoinRoom.Click += btnJoinRoom_Click;
            // 
            // btnStartGame
            // 
            btnStartGame.BackColor = Color.Transparent;
            btnStartGame.Enabled = false;
            btnStartGame.FlatStyle = FlatStyle.Flat;
            btnStartGame.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnStartGame.ForeColor = Color.FromArgb(0, 192, 192);
            btnStartGame.Location = new Point(30, 280);
            btnStartGame.Margin = new Padding(3, 4, 3, 4);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(255, 51);
            btnStartGame.TabIndex = 5;
            btnStartGame.Text = "Bắt đầu";
            btnStartGame.UseVisualStyleBackColor = false;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoEllipsis = true;
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            lblStatus.ForeColor = Color.FromArgb(0, 192, 192);
            lblStatus.Location = new Point(30, 413);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(482, 61);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Trạng thái: Chưa tạo/tham gia phòng.";
            // 
            // btnHistory
            // 
            btnHistory.BackColor = Color.Transparent;
            btnHistory.FlatStyle = FlatStyle.Flat;
            btnHistory.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            btnHistory.ForeColor = Color.FromArgb(0, 192, 192);
            btnHistory.Location = new Point(30, 349);
            btnHistory.Margin = new Padding(3, 4, 3, 4);
            btnHistory.Name = "btnHistory";
            btnHistory.Size = new Size(255, 45);
            btnHistory.TabIndex = 7;
            btnHistory.Text = "📜 Lịch sử đấu";
            btnHistory.UseVisualStyleBackColor = false;
            btnHistory.Click += btnHistory_Click;
            // 
            // Room
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resource.Gemini_Generated_Image_5ka7of5ka7of5ka7;
            ClientSize = new Size(549, 507);
            Controls.Add(btnHistory);
            Controls.Add(lblStatus);
            Controls.Add(btnStartGame);
            Controls.Add(btnJoinRoom);
            Controls.Add(btnCreateRoom);
            Controls.Add(txtRoomID);
            Controls.Add(labelRoom);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Room";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lobby";
            FormClosing += Form5_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}

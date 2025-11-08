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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelRoom = new System.Windows.Forms.Label();
            this.txtRoomID = new System.Windows.Forms.TextBox();
            this.btnCreateRoom = new System.Windows.Forms.Button();
            this.btnJoinRoom = new System.Windows.Forms.Button();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnHistory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Room (Form)
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(10, 15, 30);
            this.BackgroundImage = Properties.Resource.Gemini_Generated_Image_5ka7of5ka7of5ka7;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(620, 500);
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Room";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Lobby – Solo LAN";
            this.FormClosing += new FormClosingEventHandler(this.Form5_FormClosing);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = Color.FromArgb(90, 0, 0, 0);
            this.labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.labelTitle.ForeColor = Color.FromArgb(0, 255, 255);
            this.labelTitle.Location = new Point(30, 25);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new Size(315, 37);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "SOLO LAN – 2 NGƯỜI CHƠI";
            // 
            // labelRoom
            // 
            this.labelRoom.AutoSize = true;
            this.labelRoom.BackColor = Color.Transparent;
            this.labelRoom.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            this.labelRoom.ForeColor = Color.FromArgb(0, 192, 192);
            this.labelRoom.Location = new Point(32, 100);
            this.labelRoom.Name = "labelRoom";
            this.labelRoom.Size = new Size(134, 23);
            this.labelRoom.TabIndex = 1;
            this.labelRoom.Text = "Room ID (6 số):";
            // 
            // txtRoomID
            // 
            this.txtRoomID.BackColor = Color.FromArgb(15, 22, 45);
            this.txtRoomID.BorderStyle = BorderStyle.FixedSingle;
            this.txtRoomID.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.txtRoomID.ForeColor = Color.FromArgb(0, 255, 255);
            this.txtRoomID.Location = new Point(180, 96);
            this.txtRoomID.Margin = new Padding(3, 4, 3, 4);
            this.txtRoomID.MaxLength = 6;
            this.txtRoomID.Name = "txtRoomID";
            this.txtRoomID.Size = new Size(146, 30);
            this.txtRoomID.TabIndex = 2;
            this.txtRoomID.TextAlign = HorizontalAlignment.Center;
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.BackColor = Color.FromArgb(10, 20, 40);
            this.btnCreateRoom.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            this.btnCreateRoom.FlatAppearance.BorderSize = 1;
            this.btnCreateRoom.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 140);
            this.btnCreateRoom.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 100);
            this.btnCreateRoom.FlatStyle = FlatStyle.Flat;
            this.btnCreateRoom.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.btnCreateRoom.ForeColor = Color.FromArgb(0, 192, 192);
            this.btnCreateRoom.Location = new Point(36, 155);
            this.btnCreateRoom.Margin = new Padding(3, 4, 3, 4);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new Size(290, 45);
            this.btnCreateRoom.TabIndex = 3;
            this.btnCreateRoom.Text = "Tạo phòng (Host)";
            this.btnCreateRoom.UseVisualStyleBackColor = false;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);
            // 
            // btnJoinRoom
            // 
            this.btnJoinRoom.BackColor = Color.FromArgb(10, 20, 40);
            this.btnJoinRoom.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            this.btnJoinRoom.FlatAppearance.BorderSize = 1;
            this.btnJoinRoom.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 140);
            this.btnJoinRoom.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 100);
            this.btnJoinRoom.FlatStyle = FlatStyle.Flat;
            this.btnJoinRoom.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.btnJoinRoom.ForeColor = Color.FromArgb(0, 192, 192);
            this.btnJoinRoom.Location = new Point(36, 215);
            this.btnJoinRoom.Margin = new Padding(3, 4, 3, 4);
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Size = new Size(290, 45);
            this.btnJoinRoom.TabIndex = 4;
            this.btnJoinRoom.Text = "Tham gia phòng (Client)";
            this.btnJoinRoom.UseVisualStyleBackColor = false;
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);
            // 
            // btnStartGame
            // 
            this.btnStartGame.BackColor = Color.FromArgb(10, 20, 40);
            this.btnStartGame.Enabled = false;
            this.btnStartGame.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            this.btnStartGame.FlatAppearance.BorderSize = 1;
            this.btnStartGame.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 140);
            this.btnStartGame.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 100);
            this.btnStartGame.FlatStyle = FlatStyle.Flat;
            this.btnStartGame.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.btnStartGame.ForeColor = Color.FromArgb(0, 255, 255);
            this.btnStartGame.Location = new Point(36, 275);
            this.btnStartGame.Margin = new Padding(3, 4, 3, 4);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new Size(290, 50);
            this.btnStartGame.TabIndex = 5;
            this.btnStartGame.Text = "BẮT ĐẦU";
            this.btnStartGame.UseVisualStyleBackColor = false;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.BackColor = Color.FromArgb(10, 20, 40);
            this.btnHistory.FlatAppearance.BorderColor = Color.FromArgb(0, 192, 192);
            this.btnHistory.FlatAppearance.BorderSize = 1;
            this.btnHistory.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 120, 140);
            this.btnHistory.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 100);
            this.btnHistory.FlatStyle = FlatStyle.Flat;
            this.btnHistory.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            this.btnHistory.ForeColor = Color.FromArgb(0, 192, 192);
            this.btnHistory.Location = new Point(36, 335);
            this.btnHistory.Margin = new Padding(3, 4, 3, 4);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new Size(290, 45);
            this.btnHistory.TabIndex = 7;
            this.btnHistory.Text = "📜 Lịch sử đấu";
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.BackColor = Color.FromArgb(110, 0, 0, 0);
            this.lblStatus.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 163);
            this.lblStatus.ForeColor = Color.FromArgb(0, 192, 192);
            this.lblStatus.Location = new Point(36, 400);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new Size(540, 70);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Trạng thái: Chưa tạo/tham gia phòng.";
            // 
            // Add controls to form
            // 
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.btnJoinRoom);
            this.Controls.Add(this.btnCreateRoom);
            this.Controls.Add(this.txtRoomID);
            this.Controls.Add(this.labelRoom);
            this.Controls.Add(this.labelTitle);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}

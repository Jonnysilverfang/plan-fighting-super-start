namespace Kien
{
    partial class Form5
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelRoom;
        private System.Windows.Forms.TextBox txtRoomID;
        private System.Windows.Forms.Button btnCreateRoom;
        private System.Windows.Forms.Button btnJoinRoom;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Label lblStatus;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
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
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Location = new System.Drawing.Point(24, 18);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(283, 30);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Solo LAN – 2 người chơi";
            // 
            // labelRoom
            // 
            this.labelRoom.AutoSize = true;
            this.labelRoom.Location = new System.Drawing.Point(26, 74);
            this.labelRoom.Name = "labelRoom";
            this.labelRoom.Size = new System.Drawing.Size(89, 15);
            this.labelRoom.TabIndex = 1;
            this.labelRoom.Text = "Room ID (6 số):";
            // 
            // txtRoomID
            // 
            this.txtRoomID.Location = new System.Drawing.Point(121, 71);
            this.txtRoomID.MaxLength = 6;
            this.txtRoomID.Name = "txtRoomID";
            this.txtRoomID.Size = new System.Drawing.Size(128, 23);
            this.txtRoomID.TabIndex = 2;
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.Location = new System.Drawing.Point(26, 114);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(223, 32);
            this.btnCreateRoom.TabIndex = 3;
            this.btnCreateRoom.Text = "Tạo phòng (Host)";
            this.btnCreateRoom.UseVisualStyleBackColor = true;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);
            // 
            // btnJoinRoom
            // 
            this.btnJoinRoom.Location = new System.Drawing.Point(26, 162);
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Size = new System.Drawing.Size(223, 32);
            this.btnJoinRoom.TabIndex = 4;
            this.btnJoinRoom.Text = "Tham gia phòng (Client)";
            this.btnJoinRoom.UseVisualStyleBackColor = true;
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);
            // 
            // btnStartGame
            // 
            this.btnStartGame.Enabled = false;
            this.btnStartGame.Location = new System.Drawing.Point(26, 210);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(223, 38);
            this.btnStartGame.TabIndex = 5;
            this.btnStartGame.Text = "Bắt đầu";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoEllipsis = true;
            this.lblStatus.Location = new System.Drawing.Point(26, 265);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(422, 46);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Trạng thái: Chưa tạo/tham gia phòng.";
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 330);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.btnJoinRoom);
            this.Controls.Add(this.btnCreateRoom);
            this.Controls.Add(this.txtRoomID);
            this.Controls.Add(this.labelRoom);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lobby";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form5_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}

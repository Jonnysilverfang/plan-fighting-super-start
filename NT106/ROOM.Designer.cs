namespace plan_fighting_super_start
{
    partial class Room
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

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Room));
            labelTitle = new Label();
            labelRoom = new Label();
            txtRoomID = new TextBox();
            btnCreateRoom = new Button();
            btnJoinRoom = new Button();
            btnStartGame = new Button();
            lblStatus = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            labelTitle.Size = new Size(101, 37);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "ROOM";
            // 
            // labelRoom
            // 
            labelRoom.AutoSize = true;
            labelRoom.BackColor = Color.Transparent;
            labelRoom.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            labelRoom.ForeColor = Color.FromArgb(0, 192, 192);
            labelRoom.Location = new Point(30, 99);
            labelRoom.Name = "labelRoom";
            labelRoom.Size = new Size(120, 20);
            labelRoom.TabIndex = 1;
            labelRoom.Text = "Room ID (6 số):";
            // 
            // txtRoomID
            // 
            txtRoomID.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtRoomID.ForeColor = Color.FromArgb(0, 192, 192);
            txtRoomID.Location = new Point(148, 95);
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
            btnCreateRoom.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCreateRoom.ForeColor = Color.FromArgb(0, 192, 192);
            btnCreateRoom.Location = new Point(30, 153);
            btnCreateRoom.Margin = new Padding(3, 4, 3, 4);
            btnCreateRoom.Name = "btnCreateRoom";
            btnCreateRoom.Size = new Size(264, 43);
            btnCreateRoom.TabIndex = 3;
            btnCreateRoom.Text = "Tạo phòng (Host)";
            btnCreateRoom.UseVisualStyleBackColor = false;
            btnCreateRoom.Click += btnCreateRoom_Click;
            // 
            // btnJoinRoom
            // 
            btnJoinRoom.BackColor = Color.Transparent;
            btnJoinRoom.FlatStyle = FlatStyle.Flat;
            btnJoinRoom.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnJoinRoom.ForeColor = Color.FromArgb(0, 192, 192);
            btnJoinRoom.Location = new Point(30, 216);
            btnJoinRoom.Margin = new Padding(3, 4, 3, 4);
            btnJoinRoom.Name = "btnJoinRoom";
            btnJoinRoom.Size = new Size(264, 43);
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
            btnStartGame.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnStartGame.ForeColor = Color.FromArgb(0, 192, 192);
            btnStartGame.Location = new Point(30, 280);
            btnStartGame.Margin = new Padding(3, 4, 3, 4);
            btnStartGame.Name = "btnStartGame";
            btnStartGame.Size = new Size(264, 51);
            btnStartGame.TabIndex = 5;
            btnStartGame.Text = "Bắt đầu";
            btnStartGame.UseVisualStyleBackColor = false;
            btnStartGame.Click += btnStartGame_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoEllipsis = true;
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatus.ForeColor = Color.FromArgb(0, 192, 192);
            lblStatus.Location = new Point(30, 353);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(489, 61);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Trạng thái: Chưa tạo/tham gia phòng.";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(353, 95);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(150, 147);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // Room
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(549, 440);
            Controls.Add(pictureBox1);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private PictureBox pictureBox1;

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

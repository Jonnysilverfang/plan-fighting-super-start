using System.Drawing;
using System.Windows.Forms;

namespace plan_fighting_super_start
{
    partial class Friend
    {
        private System.ComponentModel.IContainer components = null;

        private ListView lvFriends;
        private ColumnHeader colAvatar;
        private ColumnHeader colUsername;
        private ColumnHeader colStatus;
        private TextBox txtFriendUsername;
        private Label lblFriendUsername;
        private Button btnSendRequest;
        private Button btnAccept;
        private Button btnDecline;
        private Button btnRefresh;
        private Label lblTitle;
        private Label lblLoading;

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
            this.lvFriends = new System.Windows.Forms.ListView();
            this.colAvatar = new System.Windows.Forms.ColumnHeader();
            this.colUsername = new System.Windows.Forms.ColumnHeader();
            this.colStatus = new System.Windows.Forms.ColumnHeader();
            this.txtFriendUsername = new System.Windows.Forms.TextBox();
            this.lblFriendUsername = new System.Windows.Forms.Label();
            this.btnSendRequest = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblLoading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Friend (FORM)
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(10, 15, 30);
            this.ClientSize = new System.Drawing.Size(720, 460);
            this.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.ForeColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Friend";
            this.Text = "Cyber Friend List";
            this.Load += new System.EventHandler(this.Friend_Load);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Consolas", 16F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblTitle.ForeColor = Color.Cyan;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(180, 26);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "CYBER  FRIENDS";
            // 
            // lvFriends
            // 
            this.lvFriends.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAvatar,
            this.colUsername,
            this.colStatus});
            this.lvFriends.FullRowSelect = true;
            this.lvFriends.GridLines = false;                    // 🔥 Ẩn đường kẻ
            this.lvFriends.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.lvFriends.HideSelection = false;
            this.lvFriends.Location = new System.Drawing.Point(20, 50);
            this.lvFriends.MultiSelect = false;
            this.lvFriends.Name = "lvFriends";
            this.lvFriends.Size = new System.Drawing.Size(520, 350); // 🔥 Bảng cân đối hơn
            this.lvFriends.TabIndex = 0;
            this.lvFriends.UseCompatibleStateImageBehavior = false;
            this.lvFriends.View = System.Windows.Forms.View.Details;
            this.lvFriends.SelectedIndexChanged += new System.EventHandler(this.lvFriends_SelectedIndexChanged);
            this.lvFriends.ColumnWidthChanging += new ColumnWidthChangingEventHandler(this.lvFriends_ColumnWidthChanging);
            this.lvFriends.BackColor = Color.FromArgb(15, 20, 40);
            this.lvFriends.ForeColor = Color.White;
            this.lvFriends.BorderStyle = BorderStyle.FixedSingle; // khung mỏng gọn
            // 
            // colAvatar
            // 
            this.colAvatar.Text = "";
            this.colAvatar.Width = 60;   // chỗ hiển thị avatar 48x48
            // 
            // colUsername
            // 
            this.colUsername.Text = "User";
            this.colUsername.Width = 180;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 260;
            // 
            // txtFriendUsername
            // 
            this.txtFriendUsername.BackColor = Color.FromArgb(15, 25, 45);
            this.txtFriendUsername.BorderStyle = BorderStyle.FixedSingle;
            this.txtFriendUsername.ForeColor = Color.White;
            this.txtFriendUsername.Location = new System.Drawing.Point(150, 420);
            this.txtFriendUsername.Name = "txtFriendUsername";
            this.txtFriendUsername.Size = new System.Drawing.Size(210, 23);
            this.txtFriendUsername.TabIndex = 1;
            // 
            // lblFriendUsername
            // 
            this.lblFriendUsername.AutoSize = true;
            this.lblFriendUsername.Location = new System.Drawing.Point(20, 423);
            this.lblFriendUsername.Name = "lblFriendUsername";
            this.lblFriendUsername.Size = new System.Drawing.Size(112, 15);
            this.lblFriendUsername.TabIndex = 2;
            this.lblFriendUsername.Text = "Username bạn bè";
            // 
            // btnSendRequest
            // 
            this.btnSendRequest.Location = new System.Drawing.Point(370, 417);
            this.btnSendRequest.Name = "btnSendRequest";
            this.btnSendRequest.Size = new System.Drawing.Size(110, 27);
            this.btnSendRequest.TabIndex = 3;
            this.btnSendRequest.Text = "Gửi lời mời";
            this.btnSendRequest.UseVisualStyleBackColor = false;
            this.btnSendRequest.Click += new System.EventHandler(this.btnSendRequest_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(560, 95);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(140, 32);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "✔ Chấp nhận";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnDecline
            // 
            this.btnDecline.Location = new System.Drawing.Point(560, 137);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(140, 32);
            this.btnDecline.TabIndex = 5;
            this.btnDecline.Text = "✖ Từ chối";
            this.btnDecline.UseVisualStyleBackColor = false;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(560, 53);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 32);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "⟳ Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Font = new Font("Consolas", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            this.lblLoading.ForeColor = Color.Cyan;
            this.lblLoading.Location = new System.Drawing.Point(560, 30);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(84, 15);
            this.lblLoading.TabIndex = 11;
            this.lblLoading.Text = "LOADING...";
            this.lblLoading.Visible = false;
            // 
            // style buttons
            // 
            Color btnBg = Color.FromArgb(20, 40, 70);
            Color btnBorder = Color.Cyan;
            Button[] buttons = { btnSendRequest, btnAccept, btnDecline, btnRefresh };
            foreach (var ctrl in buttons)
            {
                ctrl.BackColor = btnBg;
                ctrl.ForeColor = Color.Cyan;
                ctrl.FlatStyle = FlatStyle.Flat;
                ctrl.FlatAppearance.BorderColor = btnBorder;
                ctrl.FlatAppearance.BorderSize = 1;
            }
            // 
            // Controls
            // 
            this.Controls.Add(this.lblLoading);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnSendRequest);
            this.Controls.Add(this.lblFriendUsername);
            this.Controls.Add(this.txtFriendUsername);
            this.Controls.Add(this.lvFriends);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

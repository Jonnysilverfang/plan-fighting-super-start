namespace plan_fighting_super_start
{
    partial class Rank
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
            pnlTop = new Panel();
            btnRefresh = new Button();
            btnSubmit = new Button();
            numLevel = new NumericUpDown();
            lblLevel = new Label();
            txtUser = new TextBox();
            lblUser = new Label();
            dgv = new DataGridView();
            statusStrip1 = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numLevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(btnRefresh);
            pnlTop.Controls.Add(btnSubmit);
            pnlTop.Controls.Add(numLevel);
            pnlTop.Controls.Add(lblLevel);
            pnlTop.Controls.Add(txtUser);
            pnlTop.Controls.Add(lblUser);
            pnlTop.Location = new Point(41, 64);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(12);
            pnlTop.Size = new Size(802, 112);
            pnlTop.TabIndex = 6;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(610, 68);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(94, 29);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Tải TOP 10";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(458, 68);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(94, 29);
            btnSubmit.TabIndex = 4;
            btnSubmit.Text = "Gửi điểm";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // numLevel
            // 
            numLevel.Location = new Point(458, 17);
            numLevel.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numLevel.Name = "numLevel";
            numLevel.Size = new Size(80, 27);
            numLevel.TabIndex = 3;
            // 
            // lblLevel
            // 
            lblLevel.AutoSize = true;
            lblLevel.Location = new Point(402, 20);
            lblLevel.Name = "lblLevel";
            lblLevel.Size = new Size(50, 20);
            lblLevel.TabIndex = 2;
            lblLevel.Text = "Level: ";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(120, 17);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(220, 27);
            txtUser.TabIndex = 1;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(36, 20);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(78, 20);
            lblUser.TabIndex = 0;
            lblUser.Text = "Username:";
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new Point(41, 182);
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersWidth = 51;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(802, 346);
            dgv.TabIndex = 7;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip1.Location = new Point(0, 527);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(882, 26);
            statusStrip1.TabIndex = 8;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(68, 20);
            statusLabel.Text = "Sẵn sàng";
            // 
            // Rank
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 553);
            Controls.Add(statusStrip1);
            Controls.Add(dgv);
            Controls.Add(pnlTop);
            MinimumSize = new Size(900, 600);
            Name = "Rank";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rank";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numLevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlTop;
        private NumericUpDown numLevel;
        private Label lblLevel;
        private TextBox txtUser;
        private Label lblUser;
        private Button btnRefresh;
        private Button btnSubmit;
        private DataGridView dgv;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
    }
}
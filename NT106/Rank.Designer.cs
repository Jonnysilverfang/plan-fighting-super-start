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
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            btnRefresh = new Button();
            dgvRank = new DataGridView();
            colHang = new DataGridViewTextBoxColumn();
            colTen = new DataGridViewTextBoxColumn();
            colLevel = new DataGridViewTextBoxColumn();
            statusStrip1 = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRank).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
            label1.Location = new Point(341, 9);
            label1.Name = "label1";
            label1.Size = new Size(200, 31);
            label1.TabIndex = 9;
            label1.Text = "BẢNG XẾP HẠNG";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(214, 65);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(230, 27);
            numericUpDown1.TabIndex = 10;
            numericUpDown1.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(524, 67);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(149, 29);
            btnRefresh.TabIndex = 11;
            btnRefresh.Text = "Tải bảng xếp hạng";
            btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // 
            // dgvRank
            // 
            dgvRank.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRank.Columns.AddRange(new DataGridViewColumn[] { colHang, colTen, colLevel });
            dgvRank.Location = new Point(191, 135);
            dgvRank.Name = "dgvRank";
            dgvRank.RowHeadersWidth = 51;
            dgvRank.Size = new Size(424, 182);
            dgvRank.TabIndex = 12;
            // 
            // colHang
            // 
            colHang.DataPropertyName = "TextBoxColumn";
            colHang.HeaderText = "Hạng";
            colHang.MinimumWidth = 6;
            colHang.Name = "colHang";
            colHang.Width = 80;
            // 
            // colTen
            // 
            colTen.HeaderText = "Tên";
            colTen.MinimumWidth = 6;
            colTen.Name = "colTen";
            colTen.Width = 200;
            // 
            // colLevel
            // 
            colLevel.HeaderText = "Level";
            colLevel.MinimumWidth = 6;
            colLevel.Name = "colLevel";
            colLevel.Width = 90;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { statusLabel });
            statusStrip1.Location = new Point(0, 527);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(882, 26);
            statusStrip1.TabIndex = 13;
            statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(68, 20);
            statusLabel.Text = "Sẵn sàng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(151, 67);
            label2.Name = "label2";
            label2.Size = new Size(38, 20);
            label2.TabIndex = 14;
            label2.Text = "TOP:";
            // 
            // Rank
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(882, 553);
            Controls.Add(label2);
            Controls.Add(statusStrip1);
            Controls.Add(dgvRank);
            Controls.Add(btnRefresh);
            Controls.Add(numericUpDown1);
            Controls.Add(label1);
            MinimumSize = new Size(900, 600);
            Name = "Rank";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Rank";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRank).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private NumericUpDown numericUpDown1;
        private Button btnRefresh;
        private DataGridView dgvRank;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusLabel;
        private DataGridViewTextBoxColumn Hạng;
        private DataGridViewTextBoxColumn Tên;
        private DataGridViewTextBoxColumn Level;
        private StatusStrip statusStrip2;
        private DataGridViewTextBoxColumn colHang;
        private DataGridViewTextBoxColumn colTen;
        private DataGridViewTextBoxColumn colLevel;
        private Label label2;
    }
}
namespace TIFBun {
    partial class Form1 {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.bwImport = new System.ComponentModel.BackgroundWorker();
            this.fbdSave = new System.Windows.Forms.FolderBrowserDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bSave = new System.Windows.Forms.ToolStripDropDownButton();
            this.bSaveTo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.bSaveNewDir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bSplits = new System.Windows.Forms.ToolStripDropDownButton();
            this.bResetSplit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.bSplit1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bSplit2 = new System.Windows.Forms.ToolStripMenuItem();
            this.bSplit3 = new System.Windows.Forms.ToolStripMenuItem();
            this.bSplitN = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.bClear = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lLoaded = new System.Windows.Forms.Label();
            this.lLoading = new System.Windows.Forms.Label();
            this.flp = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 524);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(176, 53);
            this.label1.TabIndex = 5;
            this.label1.Text = "※ 左クリック・・・削除マーク\r\n※ Alt + 左クリック・・・一覧から削除\r\n※ 右クリック・・・区切り\r\n※ Shift+左クリック・・・ちょっと拡大";
            // 
            // bwImport
            // 
            this.bwImport.WorkerReportsProgress = true;
            this.bwImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwImport_DoWork);
            this.bwImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwImport_RunWorkerCompleted);
            this.bwImport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwImport_ProgressChanged);
            // 
            // fbdSave
            // 
            this.fbdSave.Description = "保存先?";
            this.fbdSave.HelpRequest += new System.EventHandler(this.fbdSave_HelpRequest);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bSave,
            this.toolStripSeparator3,
            this.bSplits,
            this.toolStripSeparator4,
            this.bClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1049, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // bSave
            // 
            this.bSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bSaveTo,
            this.toolStripSeparator5,
            this.bSaveNewDir});
            this.bSave.Image = global::TIFBun.Properties.Resources.SaveAllHS;
            this.bSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(200, 22);
            this.bSave.Text = "分割してファイルを新規保存する...";
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bSaveTo
            // 
            this.bSaveTo.Name = "bSaveTo";
            this.bSaveTo.Size = new System.Drawing.Size(300, 22);
            this.bSaveTo.Text = "保存先のフォルダを選択して新規保存";
            this.bSaveTo.Click += new System.EventHandler(this.bSaveTo_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(297, 6);
            // 
            // bSaveNewDir
            // 
            this.bSaveNewDir.Name = "bSaveNewDir";
            this.bSaveNewDir.Size = new System.Drawing.Size(300, 22);
            this.bSaveNewDir.Text = "デスクトップに新規フォルダを作成して、新規保存";
            this.bSaveNewDir.Click += new System.EventHandler(this.bSaveNewDir_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // bSplits
            // 
            this.bSplits.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bResetSplit,
            this.toolStripSeparator6,
            this.bSplit1,
            this.bSplit2,
            this.bSplit3,
            this.bSplitN});
            this.bSplits.Image = global::TIFBun.Properties.Resources.CutHS;
            this.bSplits.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSplits.Name = "bSplits";
            this.bSplits.Size = new System.Drawing.Size(102, 22);
            this.bSplits.Text = "ページ分割...";
            this.bSplits.Click += new System.EventHandler(this.bSplits_Click);
            // 
            // bResetSplit
            // 
            this.bResetSplit.Name = "bResetSplit";
            this.bResetSplit.Size = new System.Drawing.Size(298, 22);
            this.bResetSplit.Text = "分割をすべて解除";
            this.bResetSplit.Click += new System.EventHandler(this.bResetSplit_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(295, 6);
            // 
            // bSplit1
            // 
            this.bSplit1.Name = "bSplit1";
            this.bSplit1.Size = new System.Drawing.Size(298, 22);
            this.bSplit1.Text = "1 ページごとに分割 [1][2][3]...";
            this.bSplit1.Click += new System.EventHandler(this.bSplit1_Click);
            // 
            // bSplit2
            // 
            this.bSplit2.Name = "bSplit2";
            this.bSplit2.Size = new System.Drawing.Size(298, 22);
            this.bSplit2.Text = "2 ページごとに分割 [1 2][3 4][5 6]...";
            this.bSplit2.Click += new System.EventHandler(this.bSplit2_Click);
            // 
            // bSplit3
            // 
            this.bSplit3.Name = "bSplit3";
            this.bSplit3.Size = new System.Drawing.Size(298, 22);
            this.bSplit3.Text = "3 ページごとに分割 [1 2 3][4 5 6][7 8 9]...";
            this.bSplit3.Click += new System.EventHandler(this.bSplit3_Click);
            // 
            // bSplitN
            // 
            this.bSplitN.Name = "bSplitN";
            this.bSplitN.Size = new System.Drawing.Size(298, 22);
            this.bSplitN.Text = "N ページごとに分割...";
            this.bSplitN.Click += new System.EventHandler(this.bSplitN_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // bClear
            // 
            this.bClear.Image = global::TIFBun.Properties.Resources.Clearallrequests_8816;
            this.bClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(103, 22);
            this.bClear.Text = "一覧を消去する";
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lLoaded, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lLoading, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1049, 23);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // lLoaded
            // 
            this.lLoaded.AutoSize = true;
            this.lLoaded.Dock = System.Windows.Forms.DockStyle.Top;
            this.lLoaded.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLoaded.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lLoaded.Location = new System.Drawing.Point(94, 0);
            this.lLoaded.Name = "lLoaded";
            this.lLoaded.Size = new System.Drawing.Size(952, 23);
            this.lLoaded.TabIndex = 12;
            // 
            // lLoading
            // 
            this.lLoading.AutoSize = true;
            this.lLoading.BackColor = System.Drawing.SystemColors.Info;
            this.lLoading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lLoading.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLoading.ForeColor = System.Drawing.SystemColors.InfoText;
            this.lLoading.Location = new System.Drawing.Point(3, 0);
            this.lLoading.Name = "lLoading";
            this.lLoading.Size = new System.Drawing.Size(85, 23);
            this.lLoading.TabIndex = 7;
            this.lLoading.Text = "Loading...";
            this.lLoading.Visible = false;
            // 
            // flp
            // 
            this.flp.AutoScroll = true;
            this.flp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp.Location = new System.Drawing.Point(0, 48);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(1049, 476);
            this.flp.TabIndex = 20;
            this.flp.Click += new System.EventHandler(this.flp_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 577);
            this.Controls.Add(this.flp);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "TIFBun";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker bwImport;
        private System.Windows.Forms.FolderBrowserDialog fbdSave;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lLoading;
        private System.Windows.Forms.ToolStripButton bClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton bSplits;
        private System.Windows.Forms.ToolStripMenuItem bResetSplit;
        private System.Windows.Forms.ToolStripMenuItem bSplit1;
        private System.Windows.Forms.ToolStripMenuItem bSplit2;
        private System.Windows.Forms.ToolStripMenuItem bSplit3;
        private System.Windows.Forms.ToolStripMenuItem bSplitN;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripDropDownButton bSave;
        private System.Windows.Forms.ToolStripMenuItem bSaveTo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem bSaveNewDir;
        private System.Windows.Forms.FlowLayoutPanel flp;
        private System.Windows.Forms.Label lLoaded;

    }
}


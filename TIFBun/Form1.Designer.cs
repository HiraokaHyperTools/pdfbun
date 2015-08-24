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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.bwImport = new System.ComponentModel.BackgroundWorker();
            this.fbdSave = new System.Windows.Forms.FolderBrowserDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.bSave = new System.Windows.Forms.ToolStripButton();
            this.bClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bMono = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbReso = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.bSelDst = new System.Windows.Forms.ToolStripButton();
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
            this.label1.Location = new System.Drawing.Point(0, 541);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 36);
            this.label1.TabIndex = 5;
            this.label1.Text = "※ 左クリック・・・削除\r\n※ 右クリック・・・区切り\r\n※ Shift+左クリック・・・ちょっと拡大";
            // 
            // bwImport
            // 
            this.bwImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwImport_DoWork);
            this.bwImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwImport_RunWorkerCompleted);
            // 
            // fbdSave
            // 
            this.fbdSave.Description = "保存先?";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bSave,
            this.bClear,
            this.toolStripSeparator1,
            this.bMono,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.cbReso,
            this.toolStripSeparator3,
            this.bSelDst});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(770, 25);
            this.toolStrip1.TabIndex = 14;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // bSave
            // 
            this.bSave.Image = ((System.Drawing.Image)(resources.GetObject("bSave.Image")));
            this.bSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(240, 22);
            this.bSave.Text = "保存する　（分割してファイルを新規保存する）";
            this.bSave.Click += new System.EventHandler(this.保存するToolStripMenuItem_Click);
            // 
            // bClear
            // 
            this.bClear.Image = ((System.Drawing.Image)(resources.GetObject("bClear.Image")));
            this.bClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(101, 22);
            this.bClear.Text = "一覧を消去する";
            this.bClear.Click += new System.EventHandler(this.一覧を消去するToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bMono
            // 
            this.bMono.CheckOnClick = true;
            this.bMono.Image = ((System.Drawing.Image)(resources.GetObject("bMono.Image")));
            this.bMono.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bMono.Name = "bMono";
            this.bMono.Size = new System.Drawing.Size(117, 22);
            this.bMono.Text = "モノクロ画像で保存";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(65, 22);
            this.toolStripLabel1.Text = "変換解像度";
            this.toolStripLabel1.ToolTipText = "PDF→画像変換でのDPI解像度";
            // 
            // cbReso
            // 
            this.cbReso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReso.Items.AddRange(new object[] {
            "96",
            "100",
            "150",
            "200",
            "300",
            "600"});
            this.cbReso.Name = "cbReso";
            this.cbReso.Size = new System.Drawing.Size(75, 25);
            this.cbReso.SelectedIndexChanged += new System.EventHandler(this.cbReso_SelectedIndexChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // bSelDst
            // 
            this.bSelDst.Image = ((System.Drawing.Image)(resources.GetObject("bSelDst.Image")));
            this.bSelDst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bSelDst.Name = "bSelDst";
            this.bSelDst.Size = new System.Drawing.Size(126, 22);
            this.bSelDst.Text = "保存先フォルダ選択...";
            this.bSelDst.Click += new System.EventHandler(this.bSelDst_Click);
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(770, 23);
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
            this.lLoaded.Size = new System.Drawing.Size(673, 23);
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
            this.flp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp.Location = new System.Drawing.Point(0, 48);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(770, 493);
            this.flp.TabIndex = 16;
            this.flp.Click += new System.EventHandler(this.flp_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 577);
            this.Controls.Add(this.flp);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "TIFBun";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
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
        private System.Windows.Forms.Label lLoaded;
        private System.Windows.Forms.Label lLoading;
        private System.Windows.Forms.FlowLayoutPanel flp;
        private System.Windows.Forms.ToolStripButton bSave;
        private System.Windows.Forms.ToolStripButton bClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bMono;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbReso;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton bSelDst;

    }
}


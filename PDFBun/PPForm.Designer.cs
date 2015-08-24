namespace PDFBun {
    partial class PPForm {
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
            this.numSplit = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.flpE = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.numSplit)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "いくつのファイルに分割しますか？";
            // 
            // numSplit
            // 
            this.numSplit.Location = new System.Drawing.Point(175, 18);
            this.numSplit.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numSplit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSplit.Name = "numSplit";
            this.numSplit.Size = new System.Drawing.Size(61, 19);
            this.numSplit.TabIndex = 1;
            this.numSplit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numSplit.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numSplit.ValueChanged += new System.EventHandler(this.numSplit_ValueChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(12, 378);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "分割設定する";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flpE
            // 
            this.flpE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flpE.AutoScroll = true;
            this.flpE.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpE.Location = new System.Drawing.Point(14, 43);
            this.flpE.Name = "flpE";
            this.flpE.Size = new System.Drawing.Size(580, 329);
            this.flpE.TabIndex = 3;
            // 
            // PPForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 413);
            this.Controls.Add(this.flpE);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numSplit);
            this.Controls.Add(this.label1);
            this.Name = "PPForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ページ数分割";
            this.Load += new System.EventHandler(this.PPForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSplit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.NumericUpDown numSplit;
        private System.Windows.Forms.FlowLayoutPanel flpE;
    }
}
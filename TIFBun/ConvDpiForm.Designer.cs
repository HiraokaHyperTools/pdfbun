namespace TIFBun {
    partial class ConvDpiForm {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.bSaveNow = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(462, 61);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "解像度を選択してください。大きいほど高画質です：";
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(350, 18);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Padding = new System.Windows.Forms.Padding(10);
            this.radioButton5.Size = new System.Drawing.Size(80, 36);
            this.radioButton5.TabIndex = 4;
            this.radioButton5.Text = "600 dpi";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(264, 18);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Padding = new System.Windows.Forms.Padding(10);
            this.radioButton4.Size = new System.Drawing.Size(80, 36);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.Text = "400 dpi";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Location = new System.Drawing.Point(178, 18);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Padding = new System.Windows.Forms.Padding(10);
            this.radioButton3.Size = new System.Drawing.Size(80, 36);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "300 dpi";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(92, 18);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Padding = new System.Windows.Forms.Padding(10);
            this.radioButton2.Size = new System.Drawing.Size(80, 36);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "200 dpi";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 18);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Padding = new System.Windows.Forms.Padding(10);
            this.radioButton1.Size = new System.Drawing.Size(80, 36);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "100 dpi";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // bSaveNow
            // 
            this.bSaveNow.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.bSaveNow.Location = new System.Drawing.Point(12, 134);
            this.bSaveNow.Name = "bSaveNow";
            this.bSaveNow.Size = new System.Drawing.Size(150, 46);
            this.bSaveNow.TabIndex = 2;
            this.bSaveNow.Text = "保存開始";
            this.bSaveNow.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = global::TIFBun.Properties.Settings.Default.Mono;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::TIFBun.Properties.Settings.Default, "Mono", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(12, 79);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Padding = new System.Windows.Forms.Padding(10);
            this.checkBox1.Size = new System.Drawing.Size(154, 36);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "白黒 TIF に変換します";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ConvDpiForm
            // 
            this.AcceptButton = this.bSaveNow;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 192);
            this.Controls.Add(this.bSaveNow);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConvDpiForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TIFBun";
            this.Load += new System.EventHandler(this.ConvDpiForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button bSaveNow;
    }
}
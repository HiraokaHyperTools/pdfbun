namespace TIFBun {
    partial class PicForm {
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
            this.lLeft = new System.Windows.Forms.Label();
            this.lRight = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lLeft
            // 
            this.lLeft.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lLeft.Image = global::TIFBun.Properties.Resources.arrow_back_color_32xMD;
            this.lLeft.Location = new System.Drawing.Point(0, 0);
            this.lLeft.MinimumSize = new System.Drawing.Size(48, 0);
            this.lLeft.Name = "lLeft";
            this.lLeft.Size = new System.Drawing.Size(48, 495);
            this.lLeft.TabIndex = 0;
            this.lLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lLeft_MouseDown);
            // 
            // lRight
            // 
            this.lRight.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lRight.Image = global::TIFBun.Properties.Resources.arrow_Forward_color_32xMD;
            this.lRight.Location = new System.Drawing.Point(681, 0);
            this.lRight.MinimumSize = new System.Drawing.Size(48, 0);
            this.lRight.Name = "lRight";
            this.lRight.Size = new System.Drawing.Size(48, 495);
            this.lRight.TabIndex = 1;
            this.lRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lRight_MouseDown);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Century", 14F);
            this.label1.Location = new System.Drawing.Point(48, 467);
            this.label1.MinimumSize = new System.Drawing.Size(0, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(633, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Century", 14F);
            this.label2.Location = new System.Drawing.Point(48, 0);
            this.label2.MinimumSize = new System.Drawing.Size(0, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(633, 28);
            this.label2.TabIndex = 4;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(48, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(633, 439);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // PicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(729, 495);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lRight);
            this.Controls.Add(this.lLeft);
            this.Name = "PicForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "拡大中・・・";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PicForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PicForm_Paint);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PicForm_FormClosed);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PicForm_MouseDown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PicForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lLeft;
        private System.Windows.Forms.Label lRight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
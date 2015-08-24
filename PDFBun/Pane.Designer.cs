namespace PDFBun {
    partial class Pane {
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

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // Pane
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Name = "Pane";
            this.Load += new System.EventHandler(this.Pane_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Pane_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Pane_MouseMove);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Pane_MouseClick);
            this.PaddingChanged += new System.EventHandler(this.Pane_PaddingChanged);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Pane_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pane_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Pane_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

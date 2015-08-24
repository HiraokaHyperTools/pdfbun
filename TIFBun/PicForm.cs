using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace TIFBun {
    public partial class PicForm : Form {
        IProv prov;

        public PicForm(IProv prov) {
            this.prov = prov;

            InitializeComponent();
        }
        public PicForm() {
            InitializeComponent();
        }

        private void PicForm_Load(object sender, EventArgs e) {
            prov.OriginalImage.SelectActiveFrame(FrameDimension.Page, prov.FrameIndex);
            BackgroundImage = prov.OriginalImage;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PDFBun {
    // http://d.hatena.ne.jp/alcy/20071103/1194087513
    public class ScrollLessFLP : FlowLayoutPanel {
        protected override Point ScrollToControl(Control activeControl) {
            return new Point(-this.HorizontalScroll.Value, -this.VerticalScroll.Value);
        }
    }
}

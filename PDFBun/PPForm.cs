using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PDFBun.Properties;

namespace PDFBun {
    public partial class PPForm : Form {

        public PPForm() {
            InitializeComponent();
        }

        private void numSplit_ValueChanged(object sender, EventArgs e) {
            int cy = Math.Min((int)numSplit.Value, panes.Count);
            double per = panes.Count / 1.0 / cy;
            double cur = per;
            gs.Clear();
            splits.Clear();
            List<Pane> curg = new List<Pane>();
            for (int x = 0, cx = panes.Count; x < cx; x++) {
                curg.Add(panes[x]);
                if (x + 1 == cx || cur <= x + 1) {
                    splits.Add(1 + x);
                    gs.Add(curg.ToArray());
                    curg.Clear();
                    cur += per;
                }
            }

            flpE.Controls.Clear();

            Bitmap p1 = Resources.Page1;
            int y = 1;
            foreach (Pane[] g in gs) {
                Label la = new Label();
                {
                    la.Text = "PDF#" + y + "";
                    la.AutoSize = true;
                    la.Anchor = AnchorStyles.Right;
                    flpE.Controls.Add(la);
                }
                Int64 cb = 0;
                for (int x = 0; x < g.Length; x++) {
                    PictureBox pb = new PictureBox();
                    pb.Image = p1;
                    pb.SizeMode = PictureBoxSizeMode.AutoSize;
                    pb.Anchor = AnchorStyles.Left;
                    cb += g[x].cbSave;
                    flpE.Controls.Add(pb);
                }
                if (flpE.Controls.Count != 0) flpE.SetFlowBreak(flpE.Controls[flpE.Controls.Count - 1], true);
                la.Text += "  " + GoodSize.Format(cb);
                y++;
            }
        }

        class GoodSize {
            public static string Format(Int64 cb) {
                if (cb < 0)
                    return "-" + Format(-cb);
                if (cb < 1024)
                    return cb + " B";
                cb /= 1024;
                if (cb < 1024)
                    return cb + " KB";
                cb /= 1024;
                if (cb < 1024)
                    return cb + " MB";
                cb /= 1024;

                return cb + " GB";
            }
        }

        public List<Pane[]> gs = new List<Pane[]>();

        public List<int> splits = new List<int>();

        private void PPForm_Load(object sender, EventArgs e) {
            numSplit_ValueChanged(sender, e);
        }

        public List<Pane> panes = new List<Pane>();

        public void AddPane(Pane p) {
            panes.Add(p);
        }

        private void button1_Click(object sender, EventArgs e) {

        }
    }
}
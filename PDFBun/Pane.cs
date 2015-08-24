using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PDFBun {
    public partial class Pane : UserControl {
        public Pane() {
            InitializeComponent();
        }

        private void Pane_Load(object sender, EventArgs e) {
            DoubleBuffered = true;
        }

        public String fpSrc = String.Empty;
        public int iPage = 0;
        public Int64 cbSave = 0;

        Bitmap ima = null;

        public Bitmap Image { get { return ima; } set { ima = value; Invalidate(); } }

        int cxThumb = 202;

        public int ThumbnailWidth { get { return cxThumb; } set { cxThumb = value; Invalidate(); } }

        public override Size GetPreferredSize(Size proposedSize) {
            return new Size(cxThumb + 6, cxThumb);
        }

        class Ut {
            internal static Point[] GetPts(int xc, int yc, Size ima, int rrot) {
                List<Point> pts = new List<Point>();
                switch (rrot) {
                    default:
                    case 0:
                        pts.Add(new Point(xc - (ima.Width + 1) / 2, yc - (ima.Height + 1) / 2));
                        pts.Add(new Point(xc + (ima.Width + 0) / 2, yc - (ima.Height + 1) / 2));
                        pts.Add(new Point(xc - (ima.Width + 1) / 2, yc + (ima.Height + 0) / 2));
                        break;
                    case 1: // 右90
                        pts.Add(new Point(xc + (ima.Height + 0) / 2, yc - (ima.Width + 1) / 2));
                        pts.Add(new Point(xc + (ima.Height + 0) / 2, yc + (ima.Width + 0) / 2));
                        pts.Add(new Point(xc - (ima.Height + 1) / 2, yc - (ima.Width + 1) / 2));
                        break;
                    case 2: // 180
                        pts.Add(new Point(xc + (ima.Width + 0) / 2, yc + (ima.Height + 0) / 2));
                        pts.Add(new Point(xc - (ima.Width + 1) / 2, yc + (ima.Height + 0) / 2));
                        pts.Add(new Point(xc + (ima.Width + 0) / 2, yc - (ima.Height + 1) / 2));
                        break;
                    case 3: // 左90
                        pts.Add(new Point(xc - (ima.Height + 1) / 2, yc + (ima.Width + 1) / 2));
                        pts.Add(new Point(xc - (ima.Height + 1) / 2, yc - (ima.Width + 1) / 2));
                        pts.Add(new Point(xc + (ima.Height + 0) / 2, yc + (ima.Width + 0) / 2));
                        break;
                }
                return pts.ToArray();
            }
        }

        private void Pane_Paint(object sender, PaintEventArgs e) {
            Rectangle rc0 = ClientRectangle;
            int w = Math.Min(rc0.Width, rc0.Height);
            Rectangle rc1 = Rectangle.FromLTRB(rc0.Right - w, rc0.Bottom - w, rc0.Right, rc0.Bottom);
            if (ima == null) return;
            Graphics cv = e.Graphics;
            cv.DrawRectangle(new Pen(Color.FromArgb(50, ForeColor)), Rectangle.FromLTRB(rc1.Left, rc1.Top, rc1.Right - 1, rc1.Bottom - 1));
            {
                int xc = (rc1.X + rc1.Right) / 2;
                int yc = (rc1.Y + rc1.Bottom) / 2;
                cv.DrawImage(ima, Ut.GetPts(xc, yc, ima.Size, rrot),
                    new Rectangle(Point.Empty, ima.Size),
                    GraphicsUnit.Pixel
                );
            }
            if (killMe) {
                Rectangle rc3 = Rectangle.FromLTRB(rc1.X + 3, rc1.Y + 3, rc1.Right - 3, rc1.Bottom - 3);
                cv.DrawLine(penX, rc3.X, rc3.Y, rc3.Right, rc3.Bottom);
                cv.DrawLine(penX, rc3.Right, rc3.Y, rc3.X, rc3.Bottom);
            }
            if (splitHere) {
                cv.DrawLine(penI, rc0.X + 1, rc1.Y, rc0.X + 1, rc1.Bottom);
            }

            int fno = Parent.Controls.IndexOf(this);
            String fnos = String.Format("{0}", 1 + fno);
            SizeF fnosize = cv.MeasureString(fnos, Font);
            PointF ptfno = new PointF((rc1.Right + rc1.Left) / 2 - fnosize.Width / 2, rc1.Bottom - 1 - fnosize.Height);
            cv.FillRectangle(Brushes.White, new RectangleF(ptfno, fnosize));
            cv.DrawString(fnos, Font, Brushes.Black, ptfno);
        }

        Pen penX = new Pen(Color.Red, 2);
        Pen penI = new Pen(Color.Blue, 3);

        private void Pane_PaddingChanged(object sender, EventArgs e) {
            Invalidate();
        }

        public bool DeleteMe { get { return killMe; } set { killMe = value; Invalidate(); } }
        public bool SplitFirst { get { return splitHere; } set { splitHere = value; Invalidate(); } }
        public int RRot { get { return rrot; } set { rrot = value; Invalidate(); } }

        bool killMe = false;
        bool splitHere = false;
        int step = -1;
        int rrot = 0;

        private void Pane_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                step = 0;
            }
            else if (e.Button == MouseButtons.Right) {
                if (0 != (ModifierKeys & Keys.Control)) {
                    rrot = (rrot - 1) & 3;
                    Invalidate();
                }
                else {
                    splitHere = !splitHere;
                    Invalidate();
                }
            }
        }

        private void Pane_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                if (0 != (ModifierKeys & Keys.Control)) {
                    rrot = (rrot + 1) & 3;
                    Invalidate();
                }
                else {
                    killMe = !killMe;
                    Invalidate();
                }
            }
        }

        private void Pane_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                step = -1;
            }
        }

        private void Pane_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                if (step >= 0) {
                    step++;
                    if (step == 5) {
                        DoDragDrop(Name, DragDropEffects.Copy);
                    }
                }
            }
        }

        public void ResetCustom() {
            SplitFirst = false;
            DeleteMe = false;
            RRot = 0;
        }
    }
}

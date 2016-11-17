using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;

namespace TIFBun {
    public partial class PicForm : Form {
        public List<PageItem> pageItems;
        public int pageIndex = 0;
        BackgroundWorker[] bwRealPics;
        Bitmap[] realPics;

        public PicForm() {
            InitializeComponent();
        }

        private void PicForm_Load(object sender, EventArgs e) {
            DoubleBuffered = true;
            pictureBox1.Region = new Region(Rectangle.Empty);
            bwRealPics = new BackgroundWorker[pageItems.Count];
            realPics = new Bitmap[pageItems.Count];

            Render();
        }

        TempFiles tempFiles = new TempFiles();

        Image imageRender = null;

        private void Render() {
            if (pageIndex >= 0) {
                imageRender = realPics[pageIndex] ?? pageItems[pageIndex].picThumb;
                Invalidate();

                if (bwRealPics[pageIndex] == null) {
                    //int cx = pictureBox1.Image.Width;
                    //int cy = pictureBox1.Image.Height;
                    //float fx = pictureBox1.Image.HorizontalResolution;
                    //float fy = pictureBox1.Image.VerticalResolution;

                    int last = pageIndex;
                    String fpTIF = tempFiles.NewFile(".tif");
                    bwRealPics[last] = new BackgroundWorker();
                    bwRealPics[last].DoWork += delegate(object sender, DoWorkEventArgs e) {
                        if (pageItems[pageIndex].provider.SaveTIF(fpTIF, 150)) {
                            Bitmap pic = new Bitmap(fpTIF);
                            e.Result = pic;
                        }
                    };
                    bwRealPics[last].RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e) {
                        if (e.Error == null && e.Result is Bitmap) {
                            realPics[last] = e.Result as Bitmap;
                            if (last == pageIndex) {
                                Render();
                            }
                        }
                    };
                    bwRealPics[last].RunWorkerAsync();
                }
            }
            label1.Text = String.Format("{0} / {1}", 1 + pageIndex, pageItems.Count);
        }

        void MovePage(int x) {
            x = Math.Max(0, Math.Min(pageItems.Count - 1, pageIndex + x));
            if (pageIndex != x) {
                pageIndex = x;
                for (int t = 0; t < pageItems.Count; t++) {
                    if (realPics[t] != null && Math.Abs(x - t) > 2) {
                        realPics[t] = null;
                        bwRealPics[t] = null;
                    }
                }
                Render();
            }
        }

        private void PicForm_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Left:
                    MovePage(-1);
                    break;
                case Keys.Right:
                    MovePage(1);
                    break;
            }
        }

        private void lLeft_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) MovePage(-1);

        }

        private void lRight_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) MovePage(1);
        }

        private void PicForm_FormClosed(object sender, FormClosedEventArgs e) {
            tempFiles.Dispose();
        }

        private void PicForm_Paint(object sender, PaintEventArgs e) {
            if (imageRender == null) return;

            Rectangle rcPage = pictureBox1.Bounds;
            rcPage.Inflate(-5, -5);
            int cw = Math.Max(imageRender.Width, imageRender.Height);
            Rectangle rcBox = jtifedit.Fitrect.FitScaled(rcPage, new Size(cw, cw));
            Rectangle rcTarget = jtifedit.Fitrect.FitScaled(rcBox, imageRender.Size);
            e.Graphics.DrawImage(imageRender, rcTarget);

            if (pageIndex < 0) return;

            PageItem pageItem = pageItems[pageIndex];
            Graphics cv = e.Graphics;

            if (pageItem.isDeleted) {
                //cv.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                Rectangle rcX = rcPage;
                int minWidth = Math.Min(rcX.Width, rcX.Height);
                rcX.X = (rcX.Width - minWidth) / 2;
                rcX.Y = (rcX.Height - minWidth) / 2;
                rcX.Width = rcX.Height = minWidth;
                cv.DrawLine(redPen, rcX.X, rcX.Y, rcX.Right, rcX.Bottom);
                cv.DrawLine(redPen, rcX.Right, rcX.Y, rcX.X, rcX.Bottom);
            }
            if (pageItem.isSep) {
                cv.FillRectangle(Brushes.Blue, Rectangle.FromLTRB(rcPage.X, rcPage.Y, rcPage.X + 3, rcPage.Bottom));
            }
        }

        Pen redPen = new Pen(Color.Red, 2);

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {

        }

        private void PicForm_MouseDown(object sender, MouseEventArgs e) {
            if (pageIndex < 0) return;

            if (e.Button == MouseButtons.Left) {
                pageItems[pageIndex].isDeleted = !pageItems[pageIndex].isDeleted;
            }
            else if (e.Button == MouseButtons.Right) {
                pageItems[pageIndex].isSep = !pageItems[pageIndex].isSep;
            }
            else {
                return;
            }

            Invalidate();
        }
    }
}
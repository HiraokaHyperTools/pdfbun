using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using jtifedit;
using System.Text.RegularExpressions;
using System.Threading;
using TIFBun.Properties;
using FreeImageAPI;
using Microsoft.VisualBasic;

namespace TIFBun {
    public partial class Form1 : Form {
        String[] args;

        public Form1(String[] args) {
            this.args = args;
            InitializeComponent();

            sfBC.Alignment = StringAlignment.Center;
            sfBC.LineAlignment = StringAlignment.Far;
        }

        StringFormat sfBC = new StringFormat();

        private void Form1_Load(object sender, EventArgs e) {
            cbReso.SelectedItem = "" + (convdpi = 200);

            foreach (String fp in args) {
                if (!File.Exists(fp)) continue;

                fptry = fp;
                saveDir = Path.GetDirectoryName(fp);

                IsLoading = true;

                bwImport.RunWorkerAsync();
                break;
            }
        }

        String saveDir = null;

        int convdpi = -1;

        void p_MouseDown(object sender, MouseEventArgs e) {
            Panel p = (Panel)sender;
            PageItem pageItem = (PageItem)p.Tag;
            if (0 != (e.Button & MouseButtons.Left)) {
                if (0 != (Form.ModifierKeys & Keys.Shift)) {
                    using (PicForm form = new PicForm(pageItem.prov)) {
                        form.ShowDialog(this);
                    }
                }
                else {
                    pageItem.isDeleted = !pageItem.isDeleted;
                    p.Invalidate();
                }
            }
            if (0 != (e.Button & MouseButtons.Right)) {
                pageItem.isSep = !pageItem.isSep;
                p.Invalidate();
            }
        }

        class PageItem {
            public Bitmap picThumb = null;
            public bool isDeleted = false;
            public bool isSep = false;
            public IProv prov = null;
            public int index = 0;
        }

        Size sizeThumb = new Size(200, 200);

        Pen redPen = new Pen(Color.Red, 2);

        void p_Paint(object sender, PaintEventArgs e) {
            Panel p = (Panel)sender;
            PageItem pageItem = (PageItem)p.Tag;

            {
                Graphics cv = e.Graphics;
                cv.DrawImageUnscaled(pageItem.picThumb, new Point(10, 10));
                if (pageItem.isDeleted) {
                    //cv.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    cv.DrawLine(redPen, 0, 0, p.Width, p.Height);
                    cv.DrawLine(redPen, 0, p.Height, p.Width, 0);
                }
                if (pageItem.isSep) {
                    cv.FillRectangle(Brushes.Blue, Rectangle.FromLTRB(0, 0, 3, p.Height));
                }
                cv.DrawString(String.Format("{0}", pageItem.index + 1), Font, SystemBrushes.WindowText, new Rectangle(Point.Empty, p.Size), sfBC);
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
        }

        String fptry = null;

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            String[] fpal = e.Data.GetData(DataFormats.FileDrop) as String[];
            if (fpal != null) {
                if (bwImport.IsBusy) {
                    MessageBox.Show(this, "変換が終了してから、もう一度追加してください。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (string fp in fpal) {
                    fptry = fp;
                    saveDir = Path.GetDirectoryName(fp);

                    IsLoading = true;

                    bwImport.RunWorkerAsync();
                    break;
                }
            }
        }

        List<PageItem> pageItems {
            get {
                List<PageItem> pageItems = new List<PageItem>();
                foreach (Control control in flp.Controls) {
                    pageItems.Add((PageItem)control.Tag);
                }
                return pageItems;
            }
        }

        private void 保存するToolStripMenuItem_Click(object sender, EventArgs e) {
            List<PageItem> pageItems = this.pageItems;

            List<int[]> alPGal = new List<int[]>();

            {
                List<int> al = new List<int>();
                for (int x = 0, cx = pageItems.Count; x < cx; x++) {
                    PageItem pageItem = pageItems[x];
                    if (al.Count != 0 && (pageItem.isSep)) {
                        alPGal.Add(al.ToArray());
                        al.Clear();
                    }
                    if (!pageItem.isDeleted) {
                        al.Add(x);
                    }
                    if (al.Count != 0 && (x == cx - 1)) {
                        alPGal.Add(al.ToArray());
                        al.Clear();
                    }
                }
            }

            if (alPGal.Count == 0) {
                MessageBox.Show(this, "出力するものはありません。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (saveDir == null) {
                if (fbdSave.ShowDialog(this) != DialogResult.OK)
                    return;
                saveDir = fbdSave.SelectedPath;
            }

            using (AH2 ah2 = new AH2())
            using (WaitNow wip = new WaitNow()) {
                wip.Cover(this);

                int vi = 1;
                List<IDisposable> aldispel = new List<IDisposable>();
                for (int t = 0; t < alPGal.Count; t++) {
                    int[] al = alPGal[t];
                    for (int c = 0; c < 1000; c++, vi++) {
                        String fpsave = Path.Combine(saveDir, String.Format("分割#{0:0000}.tif", vi));
                        if (File.Exists(fpsave)) continue;

                        ImageCodecInfo ci = null;
                        foreach (ImageCodecInfo ici in ImageCodecInfo.GetImageEncoders()) {
                            if (ici.MimeType.Equals("image/tiff")) {
                                ci = ici;
                            }
                        }

                        EncoderParameters eps = new EncoderParameters(3);
                        eps.Param[0] = new EncoderParameter(
                            System.Drawing.Imaging.Encoder.SaveFlag,
                            (Int64)EncoderValue.MultiFrame
                            );

                        Bitmap pic0 = null;
                        for (int x = 0; x < al.Length; x++) {
                            PageItem it = pageItems[al[x]];
                            Bitmap pic = it.prov.OriginalImage;
                            if (bMono.Checked) {
                                if (pic.PixelFormat != PixelFormat.Format1bppIndexed) {
                                    float rx = pic.HorizontalResolution, ry = pic.VerticalResolution;
                                    FIBITMAP fibSrc = FreeImage.CreateFromBitmap(pic);
                                    try {
                                        FreeImage.SetResolutionX(fibSrc, (uint)Math.Round(rx));
                                        FreeImage.SetResolutionY(fibSrc, (uint)Math.Round(ry));

                                        FIBITMAP fibDst = FreeImage.ConvertColorDepth(fibSrc, FreeImageAPI.FREE_IMAGE_COLOR_DEPTH.FICD_01_BPP);
                                        try {
                                            pic = FreeImage.GetBitmap(fibDst);
                                        }
                                        finally {
                                            FreeImage.Unload(fibDst);
                                        }
                                    }
                                    finally {
                                        FreeImage.Unload(fibSrc);
                                    }
                                }
                            }
                            pic0 = pic0 ?? pic;
                            pic.SelectActiveFrame(FrameDimension.Page, it.prov.FrameIndex);

                            int bpp;
                            eps.Param[1] = new EncoderParameter(
                                System.Drawing.Imaging.Encoder.ColorDepth,
                                (Int64)(bpp = Bitmap.GetPixelFormatSize(pic.PixelFormat))
                                );
                            eps.Param[2] = new EncoderParameter(
                                System.Drawing.Imaging.Encoder.Compression,
                                (Int64)(bpp == 1 ? EncoderValue.CompressionCCITT4 : EncoderValue.CompressionLZW)
                                );

                            if (x == 0) {
                                pic0.Save(fpsave, ci, eps);
                                aldispel.Add(pic);
                            }
                            else {
                                eps.Param[0] = new EncoderParameter(
                                    System.Drawing.Imaging.Encoder.SaveFlag,
                                    (Int64)EncoderValue.FrameDimensionPage
                                    );

                                pic0.SaveAdd(pic, eps);
                                aldispel.Add(pic);
                            }
                        }
                        break;
                    }
                }
            }

            MessageBox.Show(this, "保存しました。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        class FIUt {
            internal static bool IsPDF(Stream fs) {
                return (Encoding.ASCII.GetString(new BinaryReader(fs).ReadBytes(16)).Split('\n')[0].StartsWith("%PDF-1."));
            }

            internal static bool IsPDF(String fp) {
                using (FileStream fs = File.OpenRead(fp)) {
                    return IsPDF(fs);
                }
            }
        }

        class PDFUt {
            internal static string pdfinfo_exe { get { return Path.Combine(Application.StartupPath, "GNU\\pdfinfo.exe"); } }

            internal static int CntPages(String fp) {
                ProcessStartInfo psi = new ProcessStartInfo(pdfinfo_exe, " \"" + fp + "\"");
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Minimized;
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.StandardOutputEncoding = Encoding.ASCII;
                Process p = Process.Start(psi);
                String s = p.StandardOutput.ReadToEnd();
                p.WaitForExit();

                Match M = Regex.Match(s, "^Pages:\\s+(?<a>\\d+)", RegexOptions.Multiline);
                if (M.Success)
                    return int.Parse(M.Groups["a"].Value);
                return 0;
            }

            internal static string pdftoppm_exe { get { return Path.Combine(Application.StartupPath, "GNU\\pdftoppm.exe"); } }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="fppdf"></param>
            /// <param name="iPage">0based</param>
            /// <returns></returns>
            internal static String ExtractPage(String fppdf, int iPage, int dpi) {
                String fptmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                ProcessStartInfo psi = new ProcessStartInfo(pdftoppm_exe, " -r " + dpi + " -f " + (1 + iPage) + " -png -singlefile \"" + fppdf + "\" " + Path.GetFileName(fptmp));
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Minimized;
                psi.UseShellExecute = false;
                psi.WorkingDirectory = Path.GetDirectoryName(fptmp);
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                Process p = Process.Start(psi);
                p.WaitForExit();
                String sErr = p.StandardError.ReadToEnd();
                String sOut = p.StandardOutput.ReadToEnd();

                if (File.Exists(fptmp))
                    File.Delete(fptmp);

                if (p.ExitCode == 0) {
                    return fptmp + ".png";
                }

                return null;
            }

        }

        class TIFProv : IProv {
            public Bitmap picSrc, picSubTh;
            public int iFrame;

            public TIFProv(Bitmap picSrc, Bitmap picSubTh, int iFrame) {
                this.picSrc = picSrc;
                this.picSubTh = picSubTh;
                this.iFrame = iFrame;
            }

            #region IProv メンバ

            public Bitmap GetThumb(int cx, int cy) {
                picSubTh.SelectActiveFrame(FrameDimension.Page, iFrame);
                Bitmap th = new Bitmap(cx, cy);

                using (Graphics cv = Graphics.FromImage(th)) {
                    float fx = (picSubTh.HorizontalResolution == 0) ? 1 : picSubTh.VerticalResolution / picSubTh.HorizontalResolution;
                    cv.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    cv.DrawImage(picSubTh,
                        Fitrect.Fit(
                            new Rectangle(Point.Empty, new Size(cx, cy)),
                            new Size(
                                (int)(picSubTh.Width * fx),
                                (int)(picSubTh.Height)
                                )
                            )
                        );
                }

                return th;
            }

            public Bitmap OriginalImage { get { return picSrc; } }

            public int FrameIndex { get { return iFrame; } }

            #endregion
        }

        class PDFProv : IProv {
            public String fppdf;
            public Bitmap picSrc;
            public int iPage; // 0based

            public PDFProv(String fppdf, Bitmap picSrc, int iPage) {
                this.fppdf = fppdf;
                this.picSrc = picSrc;
                this.iPage = iPage;
            }

            #region IProv メンバ

            public Bitmap GetThumb(int cx, int cy) {
                Bitmap th = new Bitmap(cx, cy);

                using (Graphics cv = Graphics.FromImage(th)) {
                    cv.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    cv.DrawImage(picSrc, Fitrect.Fit(new Rectangle(Point.Empty, new Size(cx, cy)), new Size(picSrc.Width, picSrc.Height)));
                }

                return th;
            }

            public Bitmap OriginalImage {
                get { return picSrc; }
            }

            public int FrameIndex {
                get { return 0; }
            }

            #endregion
        }

        List<string> tempFiles = new List<string>();

        private void bwImport_DoWork(object sender, DoWorkEventArgs e) {
            List<IProv> alpic = new List<IProv>();

            if (FIUt.IsPDF(fptry)) {
                int cx = PDFUt.CntPages(fptry);
                for (int x = 0; x < cx; x++) {
                    String fppng = PDFUt.ExtractPage(fptry, x, convdpi);
                    if (fppng == null) continue;

                    lock (tempFiles) {
                        tempFiles.Add(fppng);
                    }

                    Bitmap picSrc = (Bitmap)Bitmap.FromStream(new MemoryStream(File.ReadAllBytes(fppng), false));
                    AddPage(new PDFProv(fptry, picSrc, x));

                    File.Delete(fppng);
                }
            }
            else {
                Bitmap picSrc = (Bitmap)Bitmap.FromStream(new MemoryStream(File.ReadAllBytes(fptry), false));
                Bitmap picSubTh = (Bitmap)picSrc.Clone();
                {
                    int cx = picSubTh.GetFrameCount(FrameDimension.Page);
                    for (int x = 0; x < cx; x++) {
                        AddPage(new TIFProv(picSrc, picSubTh, x));
                    }
                }
            }
        }

        private void AddPage(IProv prov) {
            PageItem it = new PageItem();
            it.picThumb = prov.GetThumb(sizeThumb.Width, sizeThumb.Height);
            it.prov = prov;
            it.index = flp.Controls.Count;
            AddPage2(it);
        }

        delegate void AddPage2Delegate(PageItem it);

        private void AddPage2(PageItem it) {
            if (InvokeRequired) { Invoke((AddPage2Delegate)this.AddPage2, it); return; }

            Panel p = new Panel();
            p.Size = sizeThumb;
            p.Parent = flp;
            p.MouseDown += new MouseEventHandler(p_MouseDown);
            p.Paint += new PaintEventHandler(p_Paint);
            p.Tag = it;
        }

        private void bwImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            IsLoading = false;
        }

        bool IsLoading {
            get {
                return lLoading.Visible;
            }
            set {
                lLoading.Visible = value;
                bSave.Enabled = !value;
            }
        }

        private void 一覧を消去するToolStripMenuItem_Click(object sender, EventArgs e) {
            if (MessageBox.Show(this, "すべてのページを消去しますか。", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                flp.Controls.Clear();
            }
        }

        private void cbReso_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                convdpi = int.Parse(cbReso.Text);
            }
            catch (FormatException) {
            }
        }

        private void flp_Click(object sender, EventArgs e) {
            flp.Focus();
        }

        private void bSelDst_Click(object sender, EventArgs e) {
            fbdSave.SelectedPath = saveDir ?? Application.StartupPath;
            if (fbdSave.ShowDialog(this) == DialogResult.OK) {
                saveDir = fbdSave.SelectedPath;
            }
        }

        private void bMono_Click(object sender, EventArgs e) {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            lock (tempFiles) {
                foreach (String fp in tempFiles) {
                    try {
                        File.Delete(fp);
                    }
                    catch (Exception) {

                    }
                }
            }
        }

        private void bResetSplit_Click(object sender, EventArgs e) {
            SplitPagesPer(0);
        }

        private void SplitPagesPer(int per) {
            int y = 0;
            foreach (PageItem pageItem in pageItems) {
                pageItem.isSep = (per == 0) ? false : ((y % per) == 0 && (y != 0));
                ++y;
            }
            flp.Invalidate(true);
        }

        private void bSplit1_Click(object sender, EventArgs e) {
            SplitPagesPer(1);
        }

        private void bSplit2_Click(object sender, EventArgs e) {
            SplitPagesPer(2);

        }

        private void bSplit3_Click(object sender, EventArgs e) {
            SplitPagesPer(3);

        }

        private void bSplitN_Click(object sender, EventArgs e) {
            int n;
            if (int.TryParse(Interaction.InputBox("ページ数?", Text, "4", -1, -1), out n) && n > 0) {
                SplitPagesPer(n);
            }
        }
    }

    public interface IProv {
        Bitmap GetThumb(int cx, int cy);

        Bitmap OriginalImage { get;}
        int FrameIndex { get;}
    }

}
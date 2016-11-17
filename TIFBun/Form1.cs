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
            foreach (String fp in args) {
                if (!File.Exists(fp)) continue;

                importFiles.Add(fp);
                saveDir = Path.GetDirectoryName(fp);

                IsLoading = true;

                bwImport.RunWorkerAsync();
                break;
            }
        }

        void p_MouseDown(object sender, MouseEventArgs e) {
            Panel p = (Panel)sender;
            PageItem pageItem = (PageItem)p.Tag;
            if (0 != (e.Button & MouseButtons.Left)) {
                if (0 != (Form.ModifierKeys & Keys.Shift)) {
                    using (PicForm form = new PicForm()) {
                        form.pageItems = pageItems;
                        form.pageIndex = pageItems.IndexOf(pageItem);
                        form.ShowDialog();
                        Invalidate(true);
                    }
                }
                else {
                    if (0 != (Form.ModifierKeys & Keys.Alt)) {
                        p.Dispose();
                        RePageNum();
                    }
                    else {
                        pageItem.isDeleted = !pageItem.isDeleted;
                    }
                    p.Invalidate();
                }
            }
            if (0 != (e.Button & MouseButtons.Right)) {
                pageItem.isSep = !pageItem.isSep;
                p.Invalidate();
            }
        }

        private void RePageNum() {
            int y = 0;
            foreach (PageItem pageItem in pageItems) {
                pageItem.index = y;
                y++;
            }
            Invalidate(true);
        }

        Size sizeThumb = new Size(200, 200);

        Pen redPen = new Pen(Color.Red, 2);

        void p_Paint(object sender, PaintEventArgs e) {
            Panel p = (Panel)sender;
            PageItem pageItem = (PageItem)p.Tag;

            {
                Graphics cv = e.Graphics;
                Rectangle rcPic = new Rectangle(Point.Empty, p.Size);
                rcPic.Inflate(-10, -10);
                Rectangle rcTarget = Fitrect.Fit(rcPic, pageItem.picThumb.Size);
                cv.DrawImage(pageItem.picThumb, rcTarget);
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

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            String[] fpal = e.Data.GetData(DataFormats.FileDrop) as String[];
            if (fpal != null) {
                lock (importFiles) {
                    foreach (string fp in fpal) {
                        importFiles.Add(fp);
                        saveDir = Path.GetDirectoryName(fp);

                        IsLoading = true;
                    }
                }
                if (!bwImport.IsBusy) {
                    bwImport.RunWorkerAsync();
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

        private void bSave_Click(object sender, EventArgs e) {

        }

        bool SaveThem() {
            List<PageItem> pageItems = this.pageItems;

            List<int[]> pageRefsArray = new List<int[]>();

            {
                List<int> pageRefs = new List<int>();
                for (int x = 0, cx = pageItems.Count; x < cx; x++) {
                    PageItem pageItem = pageItems[x];
                    if (pageRefs.Count != 0 && (pageItem.isSep)) {
                        pageRefsArray.Add(pageRefs.ToArray());
                        pageRefs.Clear();
                    }
                    if (!pageItem.isDeleted) {
                        pageRefs.Add(x);
                    }
                    if (pageRefs.Count != 0 && (x == cx - 1)) {
                        pageRefsArray.Add(pageRefs.ToArray());
                        pageRefs.Clear();
                    }
                }
            }

            if (pageRefsArray.Count == 0) {
                MessageBox.Show(this, "出力するものはありません。", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (saveDir == null) {
                if (fbdSave.ShowDialog(this) != DialogResult.OK)
                    return false;
                saveDir = fbdSave.SelectedPath;
            }

            bool anyPDF = false;
            foreach (int[] pageRefs in pageRefsArray) {
                foreach (int pageRef in pageRefs) {
                    anyPDF |= pageItems[pageRef].provider.IsPDF;
                }
            }

            if (anyPDF) {
                using (ConvDpiForm form = new ConvDpiForm()) {
                    if (form.ShowDialog() != DialogResult.OK) {
                        return false;
                    }
                }
            }

            int nTotal = 0, nSuccess = 0;
            String errs = "";

            using (AH2 ah2 = new AH2())
            using (WaitNow wip = new WaitNow()) {
                wip.Cover(this);

                int fileNum = 1;
                for (int t = 0; t < pageRefsArray.Count; t++) {
                    int[] pageRefs = pageRefsArray[t];
                    for (int c = 0; c < 10000; c++, fileNum++) {
                        String fpsave = Path.Combine(saveDir, String.Format("分割#{0:0000}.tif", fileNum));
                        if (File.Exists(fpsave)) continue;

                        for (int x = 0; x < pageRefs.Length; x++) {
                            PageItem pageItem = pageItems[pageRefs[x]];
                            String fpTempTIF = tempFiles.NewFile(".tif");
                            nTotal++;
                            if (pageItem.provider.SaveTIF(fpTempTIF, Settings.Default.ConvDpi)) {
                                if (Settings.Default.Mono && !pageItem.provider.IsMono) {
                                    FIBITMAP dib = FreeImage.LoadEx(fpTempTIF);
                                    try {
                                        FIBITMAP dib2 = FreeImage.Threshold(dib, 128);
                                        try {
                                            FreeImage.Save(FREE_IMAGE_FORMAT.FIF_TIFF, dib2, fpTempTIF, FREE_IMAGE_SAVE_FLAGS.TIFF_CCITTFAX4);
                                        }
                                        finally {
                                            FreeImage.UnloadEx(ref dib2);
                                        }
                                    }
                                    finally {
                                        FreeImage.UnloadEx(ref dib);
                                    }
                                }
                                if (Tiffcp.Run(fpsave, fpTempTIF, Settings.Default.Mono, x != 0)) {
                                    nSuccess++;
                                }
                                else {
                                    errs += String.Format("{0:#,##0} ページ目の保存に失敗\n", 1 + pageItem.index);
                                }
                            }
                            else {
                                errs += String.Format("{0:#,##0} ページ目の保存に失敗\n", 1 + pageItem.index);
                            }
                        }
                        break;
                    }
                }
            }

            MessageBox.Show(this, (nTotal == nSuccess) ? "保存しました。" : "部分的に保存が成功しました。\n\n---\n" + errs, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
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

        TempFiles tempFiles = new TempFiles();

        class ThumbUtil {
            internal static Bitmap Make(Bitmap pic, Size sizeThumb) {
                int cx = sizeThumb.Width;
                int cy = sizeThumb.Height;

                Bitmap thumb = new Bitmap(cx, cy);

                using (Graphics cv = Graphics.FromImage(thumb)) {
                    float fx = (pic.HorizontalResolution == 0) ? 1 : pic.VerticalResolution / pic.HorizontalResolution;
                    cv.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    cv.DrawImage(pic,
                        Fitrect.Fit(
                            new Rectangle(Point.Empty, new Size(cx, cy)),
                            new Size(
                                (int)(pic.Width * fx),
                                (int)(pic.Height)
                                )
                            )
                        );
                }

                return thumb;
            }
        }

        String saveDir = null;
        List<String> importFiles = new List<string>();

        private void bwImport_DoWork(object sender, DoWorkEventArgs e) {
            bwImport.ReportProgress(0, "");
            while (true) {
                String fptry = null;
                lock (importFiles) {
                    if (importFiles.Count == 0)
                        break;
                    fptry = importFiles[0];
                    importFiles.RemoveAt(0);
                }

                String fpTIF = null;
                bool isPDF = false;
                if (FIUt.IsPDF(fptry)) {
                    fpTIF = tempFiles.NewFile(".tif");
                    bwImport.ReportProgress(0, "PDF 画像変換");
                    if (!PDFUt.PDF2TIF(fptry, fpTIF, null, sizeThumb.Width, null)) {
                        continue;
                    }
                    isPDF = true;
                }
                else {
                    fpTIF = fptry;
                }

                if (fpTIF != null) {
#if true
                    FIMULTIBITMAP tif = FreeImage.OpenMultiBitmapEx(fpTIF, true, false);
                    List<PicProvider> providers = new List<PicProvider>();
                    try {
                        int nPages = FreeImage.GetPageCount(tif);
                        for (int x = 0; x < nPages; x++) {
                            FIBITMAP dib = FreeImage.LockPage(tif, x);
                            try {
                                FIBITMAP thumb = FreeImage.MakeThumbnail(dib, sizeThumb.Width, true);
                                try {
                                    bwImport.ReportProgress(0, String.Format("{0}/{1} from {2}", 1 + x, nPages, fptry));
                                    providers.Add(new PicProvider(FreeImage.GetBitmap(thumb), fpTIF, isPDF ? fptry : null, x, FreeImage.GetBPP(dib) == 1));
                                }
                                finally {
                                    FreeImage.UnloadEx(ref thumb);
                                }
                            }
                            finally {
                                FreeImage.UnlockPage(tif, dib, false);
                            }
                        }
                    }
                    finally {
                        FreeImage.CloseMultiBitmapEx(ref tif);
                    }

                    foreach (PicProvider pp in providers) {
                        AddPage(pp);
                    }
#else
                    using (Bitmap pic = new Bitmap(fpTIF)) {
                        int nPages = pic.GetFrameCount(FrameDimension.Page);
                        for (int x = 0; x < nPages; x++) {
                            pic.SelectActiveFrame(FrameDimension.Page, x);
                            AddPage(new PicProvider(ThumbUtil.Make(pic, sizeThumb), fpTIF, isPDF ? fptry : null, x, pic.PixelFormat == PixelFormat.Format1bppIndexed));
                        }
                    }
#endif
                }
            }
        }

        private void AddPage(PicProvider prov) {
            PageItem it = new PageItem();
            it.picThumb = prov.Thumb;
            it.provider = prov;
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
            if (e.Error == null) {
                if (importFiles.Count != 0) {
                    bwImport.RunWorkerAsync();
                    return;
                }
            }
            IsLoading = false;
            if (e.Error != null) {
                MessageBox.Show(this, "エラーが発生しました。\n\n" + e.Error, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        bool IsLoading {
            get {
                return lLoading.Visible;
            }
            set {
                lLoading.Visible = value;
                bSave.Enabled = bSplits.Enabled = bClear.Enabled = !value;
            }
        }

        private void bClear_Click(object sender, EventArgs e) {
            if (MessageBox.Show(this, "すべてのページを消去しますか。", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                flp.Controls.Clear();
            }
        }

        private void flp_Click(object sender, EventArgs e) {
            flp.Focus();
        }

        private void bMono_Click(object sender, EventArgs e) {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            tempFiles.Dispose();
        }

        private void bResetSplit_Click(object sender, EventArgs e) {
            SplitPagesPer(0);
        }

        private void SplitPagesPer(int per) {
            int y = 0;
            foreach (PageItem pageItem in pageItems) {
                if (!pageItem.isDeleted) {
                    pageItem.isSep = (per == 0) ? false : ((y % per) == 0 && (y != 0));
                    ++y;
                }
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

        private void bSplits_Click(object sender, EventArgs e) {

        }

        private void bSaveTo_Click(object sender, EventArgs e) {
            fbdSave.SelectedPath = saveDir;
            if (fbdSave.ShowDialog(this) == DialogResult.OK) {
                saveDir = fbdSave.SelectedPath;
                SaveThem();
            }
        }

        private void bSaveNewDir_Click(object sender, EventArgs e) {
            for (int t = 1; t < 1000; t++) {
                saveDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), String.Format("TIF 分割 {0:yyyy-MM-dd} #{1}", DateTime.Now, t));
                if (File.Exists(saveDir) || Directory.Exists(saveDir)) continue;
                Directory.CreateDirectory(saveDir);
                if (SaveThem()) {
                    Process.Start(saveDir);
                }
                break;
            }
        }

        private void Form1_Activated(object sender, EventArgs e) {
            flp.Focus();
        }

        private void fbdSave_HelpRequest(object sender, EventArgs e) {

        }

        private void bwImport_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            lLoading.Text = "サムネイル作成中。" + e.UserState;
        }
    }

    public class PicProvider {
        Bitmap thumb;
        String fpTIF;
        String fpPDF;
        int index; //0based
        bool isMono;

        public PicProvider(Bitmap thumb, String fpTIF, String fpPDF, int index, bool isMono) {
            this.thumb = thumb;
            this.fpTIF = fpTIF;
            this.fpPDF = fpPDF;
            this.index = index;
            this.isMono = isMono;
        }

        public Bitmap Thumb { get { return thumb; } }

        public bool IsMono { get { return isMono; } }

        public bool IsPDF { get { return fpPDF != null; } }

        public bool SaveTIF(string fpOut, int convdpi) {
            if (fpPDF != null) {
                return PDFUt.PDF2TIF(fpPDF, fpOut, convdpi, null, 1 + index);
            }
            else {
                return Tiffcp.Run(fpOut, fpTIF + "," + index, false, false);
            }
        }
    }

    public class Tiffcp {
        public static string tiffcp_exe { get { return Path.Combine(Application.StartupPath, "GNU\\tiffcp.exe"); } }

        public static bool Run(string fpDst, string fpSrc, bool g4, bool append) {
            ProcessStartInfo psi = new ProcessStartInfo(tiffcp_exe, String.Concat(""
                , (append ? " -a" : "")
                , (g4 ? " -c g4" : " -c zip")
                , " \"", fpSrc, "\""
                , " \"", fpDst, "\""
                ));
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Minimized;
            psi.UseShellExecute = false;
            psi.RedirectStandardError = true;
            Process p = Process.Start(psi);
            String err = p.StandardError.ReadToEnd();
            p.WaitForExit();

            return p.ExitCode == 0;
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

        public static bool PDF2TIF(string fpPDF, string fpTIF, int? dpi, int? scaleTo, int? singlePage) {
            Debug.Assert(fpTIF.ToLowerInvariant().EndsWith(".tif"));
            String fptmp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            ProcessStartInfo psi = new ProcessStartInfo(pdftoppm_exe, String.Concat(""
                , (scaleTo.HasValue ? " -scale-to " + scaleTo : "")
                , (dpi.HasValue ? " -r " + dpi : "")
                , (singlePage.HasValue ? " -singlefile -f " + singlePage : "")
                , " -tiff -tiffcompression deflate -singletiff"
                , " \"" + fpPDF + "\""
                , " \"" + fpTIF.Remove(fpTIF.Length - 4) + "\""
            ));
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

            return (p.ExitCode == 0);
        }
    }

    public class PageItem {
        public Bitmap picThumb = null;
        public bool isDeleted = false;
        public bool isSep = false;
        public PicProvider provider = null;
        public int index = 0;
    }
}
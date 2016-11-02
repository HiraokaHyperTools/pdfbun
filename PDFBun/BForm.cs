using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace PDFBun {
    public partial class BForm : Form {
        String[] alfs = null;
        public BForm(String[] args) {
            alfs = args;

            InitializeComponent();
        }

        private void BForm_Load(object sender, EventArgs e) {
            Text += " " + Application.ProductVersion;
            Show();
            Update();

            using (AH2 ah = new AH2())
            using (WaitNow wn = new WaitNow()) {
                wn.Cover(this);
                foreach (String fp in alfs) {
                    AppendIt(fp);
                }
                Application.DoEvents();
            }
        }

        private void flpPages_DragEnter(object sender, DragEventArgs e) {
            e.Effect = e.AllowedEffect & ((e.Data.GetDataPresent(DataFormats.FileDrop) || GetPane(e.Data.GetData(DataFormats.Text) as String) != null) ? DragDropEffects.Copy : DragDropEffects.None);
        }

        private Pane GetPane(String name) {
            return flpPages.Controls[name ?? ""] as Pane;
        }

        class EUt {
            internal static string pdftoppm { get { return Path.Combine(Application.StartupPath, @"GNU\pdftoppm.exe"); } }
            internal static string pdftk { get { return Path.Combine(Application.StartupPath, @"GNU\pdftk.exe"); } }
            internal static string tiff2pdf { get { return Path.Combine(Application.StartupPath, @"GNU\tiff2pdf.exe"); } }
        }

        class AH2 : IDisposable {
            Cursor cursor;

            public AH2() {
                cursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
            }

            #region IDisposable メンバ

            public void Dispose() {
                Cursor.Current = cursor;
            }

            #endregion
        }

        int W = 250;

        private void AppendIt(string fp) {
            Int64 cbPDF = new FileInfo(fp).Length;
            if (String.Compare(".pdf", Path.GetExtension(fp), true) == 0) {
                String prefixOut = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
                ProcessStartInfo psi = new ProcessStartInfo(EUt.pdftoppm, " -scale-to " + W + " -jpeg \"" + fp + "\" \"" + prefixOut + "\" ");
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                Process p = Process.Start(psi);
                p.WaitForExit();
                Debug.Assert(p.ExitCode == 0, String.Format("PDFからサムネイル画像を作成する事に失敗しました。({0})", p.ExitCode));
                List<Pane> perPDF = new List<Pane>();
                for (int x = 1; ; x++) {
                    String fpJPG = prefixOut + "-" + x + ".jpg";
                    if (!File.Exists(fpJPG)) {
                        fpJPG = prefixOut + "-" + x.ToString("00") + ".jpg";
                        if (!File.Exists(fpJPG)) {
                            fpJPG = prefixOut + "-" + x.ToString("000") + ".jpg";
                            if (!File.Exists(fpJPG)) {
                                fpJPG = prefixOut + "-" + x.ToString("0000") + ".jpg";
                                if (!File.Exists(fpJPG)) {
                                    break;
                                }
                            }
                        }
                    }
                    Bitmap picJPG = new Bitmap(new MemoryStream(File.ReadAllBytes(fpJPG)));
                    tempFiles.Add(fpJPG);
                    Pane pane = new Pane();
                    pane.AutoSize = true;
                    pane.Image = picJPG;
                    pane.Margin = new Padding(1);
                    pane.fpSrc = fp;
                    pane.iPage = x;
                    pane.Name = Guid.NewGuid().ToString("N");
                    pane.ThumbnailWidth = W + 2;
                    perPDF.Add(pane);
                    flpPages.Controls.Add(pane);
                }
                foreach (Pane pane in perPDF) {
                    pane.cbSave = cbPDF / perPDF.Count;
                }
                if (String.IsNullOrEmpty(fbdSave.SelectedPath)) {
                    fbdSave.SelectedPath = Path.GetDirectoryName(fp);
                }
            }
            else if ("|.tif|.tiff|".IndexOf("|" + Path.GetExtension(fp) + "|", StringComparison.InvariantCultureIgnoreCase) >= 0) {
                String fppdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".pdf");
                ProcessStartInfo psi = new ProcessStartInfo(EUt.tiff2pdf, " -o \"" + fppdf + "\" \"" + fp + "\"");
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                Process p = Process.Start(psi);
                p.WaitForExit();
                Debug.Assert(p.ExitCode == 0, String.Format("TIFFからPDFへの変換に失敗しました。({0})", p.ExitCode));
                if (String.IsNullOrEmpty(fbdSave.SelectedPath)) {
                    fbdSave.SelectedPath = Path.GetDirectoryName(fp);
                }
                tempFiles.Add(fppdf);
                AppendIt(fppdf);
            }
        }

        List<string> tempFiles = new List<string>();

        private void flpPages_DragDrop(object sender, DragEventArgs e) {
            String[] alfp = e.Data.GetData(DataFormats.FileDrop) as String[];
            if (alfp != null) {
                using (AH2 ah = new AH2())
                    foreach (String fp in alfp) {
                        AppendIt(fp);
                    }
                return;
            }

            Pane paneSrc = GetPane(e.Data.GetData(DataFormats.Text) as String);
            if (paneSrc != null) {
                Point pt = PointToClient(new Point(e.X, e.Y));
                foreach (Pane paneDst in flpPages.Controls) {
                    if (new Rectangle(paneDst.Location, paneDst.Size).Contains(pt)) {
                        int i = flpPages.Controls.IndexOf(paneDst);
                        flpPages.Controls.SetChildIndex(paneSrc, i);
                        flpPages.Refresh();
                        return;
                    }
                }
            }
        }

        class Parm {
            public SortedDictionary<string, string> pdf2key = new SortedDictionary<string, string>();
            public String cat = "", input = "";
            public List<int> frm = new List<int>();

            public String AddPDF(string pdf) {
                String key;
                pdf = pdf;//.Replace("\\", "\\\\");
                if (pdf2key.TryGetValue(pdf, out key))
                    return key;
                key = Ut.KeyGen(pdf2key.Count);
                pdf2key[key] = pdf;
                input += String.Format(" {0}=\"{1}\"", key, pdf);
                return key;
            }

            class Ut {
                internal static String KeyGen(int x) {
                    String k26 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    String r = "";
                    int digit = 0;
                    while (x != 0 || digit == 0) {
                        r = k26[x % 26] + r;
                        x /= 26;
                        digit++;
                    }
                    return r;
                }
            }
        }

        private void bSave_Click(object sender, EventArgs e) {
            if (fbdSave.ShowDialog(this) != DialogResult.OK)
                return;
            String saveDir = fbdSave.SelectedPath;

            List<Parm> parms = Decide();

            int total = 0, ok = 0;

            int c = 1;
            using (AH2 ah2 = new AH2())
            using (WaitNow wn = new WaitNow()) {
                wn.Cover(this);
                foreach (Parm parm in parms) {
                    if (parm.cat.Length == 0)
                        continue;
                    String fpOut;
                    while (true) {
                        fpOut = Path.Combine(saveDir, String.Format("分割#{0:0000}.pdf", c));
                        if (File.Exists(fpOut)) {
                            c++;
                            continue;
                        }
                        break;
                    }

                    ProcessStartInfo psi = new ProcessStartInfo(EUt.pdftk, " " + parm.input + " cat " + parm.cat + " output \"" + fpOut + "\"");
                    psi.UseShellExecute = false;
                    psi.CreateNoWindow = true;
                    Process p = Process.Start(psi);
                    p.Start();
                    p.WaitForExit();
                    total++;
                    ok += (p.ExitCode == 0) ? 1 : 0;
                }
            }

            if (total == 0) {
                MessageBox.Show(this, "何も保存していません。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ok == total) {
                MessageBox.Show(this, "分割＆保存は、すべて成功しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ok != 0) {
                MessageBox.Show(this, "分割＆保存は、部分的に成功しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else {
                MessageBox.Show(this, "分割＆保存は、すべて失敗しました。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bClear_Click(object sender, EventArgs e) {
            flpPages.Controls.Clear();
        }

        private void flpPages_DragLeave(object sender, EventArgs e) {

        }

        private void bPer1_Click(object sender, EventArgs e) {
            foreach (Pane paneDst in flpPages.Controls) {
                paneDst.SplitFirst = true;
            }
        }

        private void bConfirm_Click(object sender, EventArgs e) {
            List<Parm> parms = Decide();

            StringWriter wr = new StringWriter();

            wr.WriteLine("いま保存しますと、次のようになります：");
            wr.WriteLine();
            bool any = false;
            int fno = 1;
            foreach (Parm parm in parms) {
                if (parm.cat.Length == 0)
                    continue;
                String frm = "";
                foreach (int i in parm.frm) {
                    if (frm.Length != 0)
                        frm += " ";
                    frm += String.Format("{0}", i);
                }
                wr.WriteLine(fno + "つ目のPDFファイルを、" + frm + " から作成します。");
                wr.WriteLine();
                fno++;
                any = true;
            }
            if (!any) {
                wr.GetStringBuilder().Length = 0;
                wr.WriteLine("ページが無いので、何も保存しません。");
            }

            MessageBox.Show(this, wr.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private List<Parm> Decide() {
            Control.ControlCollection CC = flpPages.Controls;
            List<Parm> parms = new List<Parm>();
            parms.Add(new Parm());
            for (int i = 0; i < CC.Count; i++) {
                Pane pane = CC[i] as Pane;
                if (pane.SplitFirst)
                    parms.Add(new Parm());
                if (pane.DeleteMe)
                    continue;
                Parm parm = parms[parms.Count - 1];
                String k = parm.AddPDF(pane.fpSrc);
                parm.cat += String.Format(" {0}{1}{2}", k, pane.iPage, ",right,down,left".Split(',')[pane.RRot]);
                parm.frm.Add(pane.iPage);
            }

            return parms;
        }

        private void bClearSet_Click(object sender, EventArgs e) {
            foreach (Pane paneDst in flpPages.Controls) {
                paneDst.ResetCustom();
            }
        }

        private void bPerPages_Click(object sender, EventArgs e) {
            using (PPForm form = new PPForm()) {
                foreach (Pane p in flpPages.Controls)
                    if (!p.DeleteMe)
                        form.AddPane(p);
                if (form.ShowDialog(this) == DialogResult.OK) {
                    int x = 1;
                    foreach (Pane pane in form.panes) {
                        pane.SplitFirst = (form.splits.Contains(x - 1));
                        x++;
                    }
                }
            }
        }

        private void BForm_FormClosed(object sender, FormClosedEventArgs e) {
            foreach (String fp in tempFiles) {
                try {
                    File.Delete(fp);
                }
                catch (Exception) {

                }
            }
            tempFiles.Clear();
        }
    }
}
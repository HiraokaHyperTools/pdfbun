using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PDFBun {
    static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(String[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BForm(args));
        }
    }
}
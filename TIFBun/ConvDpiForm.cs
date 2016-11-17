using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TIFBun.Properties;

namespace TIFBun {
    public partial class ConvDpiForm : Form {
        public ConvDpiForm() {
            InitializeComponent();
        }

        private void ConvDpiForm_Load(object sender, EventArgs e) {
            if (Settings.Default.ConvDpi == 100) radioButton1.Checked = true;
            else if (Settings.Default.ConvDpi == 200) radioButton2.Checked = true;
            else if (Settings.Default.ConvDpi == 300) radioButton3.Checked = true;
            else if (Settings.Default.ConvDpi == 400) radioButton4.Checked = true;
            else if (Settings.Default.ConvDpi == 600) radioButton5.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            Settings.Default.ConvDpi = 100;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            Settings.Default.ConvDpi = 200;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {
            Settings.Default.ConvDpi = 300;

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) {
            Settings.Default.ConvDpi = 400;

        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e) {
            Settings.Default.ConvDpi = 600;

        }
    }
}
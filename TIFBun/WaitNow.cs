using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TIFBun {
    public partial class WaitNow : UserControl {
        public WaitNow() {
            InitializeComponent();
        }

        internal void Cover(Form form) {
            Parent = form;
            Location = Point.Empty;
            Size = form.ClientSize;
            BringToFront();
            Update();
            Show();
        }
    }
}

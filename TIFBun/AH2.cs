using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TIFBun {
    class AH2 : IDisposable {
        Cursor cursor;

        public AH2() {
            cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }

        #region IDisposable ÉÅÉìÉo

        public void Dispose() {
            Cursor.Current = cursor;
        }

        #endregion
    }
}

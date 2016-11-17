using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TIFBun {
    public class TempFiles : IDisposable {
        List<string> files = new List<string>();

        public String NewFile(String fext) {
            String fp = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + fext);
            return Add(fp);
        }

        public String Add(String fp) {
            lock (files) {
                files.Add(fp);
            }
            return fp;
        }

        #region IDisposable ÉÅÉìÉo

        public void Dispose() {
            lock (files) {
                foreach (String fp in files) {
                    try {
                        File.Delete(fp);
                    }
                    catch (Exception err) {
                        System.Diagnostics.Debug.WriteLine("& " + err);
                    }
                }
                files.Clear();
            }
        }

        #endregion
    }
}

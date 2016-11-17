using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace jtifedit {
    public class Fitrect {
        public static Rectangle Fit(Rectangle rcPage, Size sizePic) {
            if (sizePic.Width <= rcPage.Width && sizePic.Height <= rcPage.Width) {
                int cx = sizePic.Width;
                int cy = sizePic.Height;
                return new Rectangle((rcPage.Left + rcPage.Right - cx) / 2, (rcPage.Top + rcPage.Bottom - cy) / 2, +cx, +cy);
            }
            float fPage = (float)rcPage.Width / rcPage.Height;
            float fPic = (float)sizePic.Width / sizePic.Height;
            if (fPage < fPic) {
                // ‰¡’·
                int vy = rcPage.Height - (int)(sizePic.Height * (float)rcPage.Width / sizePic.Width);
                return Rectangle.FromLTRB(rcPage.X, rcPage.Y + vy / 2, rcPage.Right, rcPage.Bottom - (vy + 1) / 2);
            }
            else {
                // c’·
                int vx = rcPage.Width - (int)(sizePic.Width * (float)rcPage.Height / sizePic.Height);
                return Rectangle.FromLTRB(rcPage.X + (vx) / 2, rcPage.Y, rcPage.Right - (vx + 1) / 2, rcPage.Bottom);
            }
        }

        public static Rectangle FitScaled(Rectangle rcPage, Size sizePic) {
            float fPage = (float)rcPage.Width / rcPage.Height;
            float fPic = (float)sizePic.Width / sizePic.Height;
            if (fPage < fPic) {
                // ‰¡’·
                int vy = rcPage.Height - (int)(sizePic.Height * (float)rcPage.Width / sizePic.Width);
                return Rectangle.FromLTRB(rcPage.X, rcPage.Y + vy / 2, rcPage.Right, rcPage.Bottom - (vy + 1) / 2);
            }
            else {
                // c’·
                int vx = rcPage.Width - (int)(sizePic.Width * (float)rcPage.Height / sizePic.Height);
                return Rectangle.FromLTRB(rcPage.X + (vx) / 2, rcPage.Y, rcPage.Right - (vx + 1) / 2, rcPage.Bottom);
            }
        }
    }
}

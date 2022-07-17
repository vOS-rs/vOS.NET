using System;
using System.Collections.Generic;
using vOS.UI.Drawing;

namespace vOS.UI.Graphics
{
    public abstract class Window : UIElement
    {
        //public PixelMatrix icon;

        protected Window()
        {
            WindowManager.AddWindow(this);
        }

        ~Window()
        {
            WindowManager.RemoveWindow(this);
        }

        internal override IList<PixelMatrix> Paint()
        {

            return base.Paint();
        }

        /*public void Draw(PixelMatrix matrix) => OnDraw(typeof(Window), matrix);

        public void Draw(Rectangle rect) => OnDraw(typeof(Window), new PixelMatrix(rect));*/

        /*public void Draw(Point rect) => Draw(new Rectangle(rect, new Size(1,1)));

        public void Draw(Point rect, Size size) => Draw(new Rectangle(rect, size));*/
    }
}

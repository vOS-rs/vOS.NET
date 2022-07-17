using System;
using System.Collections.Generic;
using System.Text;
using vOS.UI.Drawing;

namespace vOS.UI.Graphics
{
    public class TextBox : UIElement
    {
        public Color Foreground;
        public Color Background;
        public string Text;

        internal override IList<PixelMatrix> Paint()
        {

            return base.Paint();
        }
    }
}

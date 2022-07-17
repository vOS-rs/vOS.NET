using System;
using System.Collections.Generic;
using System.Text;
using vOS.UI.Drawing;

namespace vOS.UI.Graphics
{
    public abstract class UIElement
    {
        public string Name;
        public string Tag;
        public List<UIElement> Childs;
        public Rectangle Rect;
        internal bool NeedRepaint = true;

        internal virtual IList<PixelMatrix> Paint()
        {
            var matrix = new List<PixelMatrix>();

            foreach (var Child in Childs)
                matrix.AddRange(Child.Paint());

            NeedRepaint = false;
            return matrix;
        }
    }
}

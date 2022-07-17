using System;
using System.Collections.Generic;
using System.Text;
using vOS.UI.Drawing;
using vOS.UI.Graphics;

namespace vOS.UI
{
    public class WindowManager
    {
        internal static List<Window> windows = new List<Window>();
        public static IReadOnlyList<Window> Windows => windows;
        public static event EventHandler<Window> NewWindow;

        internal static void AddWindow(Window window)
        {
            windows.Add(window);
            if (NewWindow != null)
                NewWindow(typeof(WindowManager), window);
        }

        internal static void RemoveWindow(Window window) => windows.Remove(window);

        public IList<PixelMatrix> Paint()
        {
            List<PixelMatrix> paints = new List<PixelMatrix>();

            foreach(var window in windows)
                paints.AddRange(CollectPaintings(window));

            return paints;
        }

        private IList<PixelMatrix> CollectPaintings(UIElement element)
        {
            var paints = new List<PixelMatrix>();

            foreach (var childElement in element.Childs)
            {
                if (childElement.NeedRepaint)
                    paints.AddRange(childElement.Paint());
                else
                    paints.AddRange(CollectPaintings(childElement));
            }

            return paints;
        }
    }
}

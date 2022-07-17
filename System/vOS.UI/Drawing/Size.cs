namespace vOS.UI.Drawing
{
    public struct Size
    {
        #region Constructors
        public Size(Point point)
        {
            Width = point.X;
            Height = point.Y;
        }

        public Size(int radius)
        {
            Width = radius;
            Height = radius;
        }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
        #endregion

        #region Properties
        public static readonly Size Empty;
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsZero => Width == 0 && Height == 0;
        #endregion
    }
}

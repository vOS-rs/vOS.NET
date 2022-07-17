namespace vOS.UI.Drawing
{
    public struct PixelMatrix
    {
        #region Constructors
        public PixelMatrix(Rectangle rectangle, Color[] colors = null)
        {
            Rectangle = rectangle;
            if (colors == null)
            {
                Colors = new Color[rectangle.Width * rectangle.Height];
                for (int i = 0; i < Colors.Length; ++i)
                    Colors[i] = new Color(System.Drawing.Color.Black);
            }
            else
                Colors = colors;
        }
        public PixelMatrix(Rectangle rectangle, Color color)
        {
            Rectangle = rectangle;
            Colors = new Color[rectangle.Width * rectangle.Height];

            for (int i = 0; i < Colors.Length; ++i)
                Colors[i] = color;
        }
        #endregion

        #region Properties
        public static readonly Color Empty;
        public Rectangle Rectangle { get; }
        public Color[] Colors { get; }
        #endregion
    }
}

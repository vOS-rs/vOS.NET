namespace vOS.UI.Drawing
{
    public struct Point
    {
        #region Constructors
        public Point(Size size)
        {
            X = size.Width;
            Y = size.Height;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region Properties
        public static readonly Point Empty;
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsZero => X == 0 && Y == 0;
        #endregion
    }
}

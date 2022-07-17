namespace vOS.UI.Drawing
{
    public struct Rectangle
    {
        #region Constructors
        public Rectangle(Point location, Size size)
        {
            Location = location;
            Size = size;
        }

        public Rectangle(int x, int y, int width, int height)
        {
            Location = new Point(x, y);
            Size = new Size(width, height);
        }
        #endregion

        #region Variables
        public Point Location;
        public Size Size;
        #endregion

        #region Properties
        public static readonly Rectangle Empty;
        public int X
        {
            get => Location.X;
            set => Location.X = value;
        }
        public int Y
        {
            get => Location.Y;
            set => Location.Y = value;
        }
        public int Width
        {
            get => Size.Width;
            set => Size.Width = value;
        }
        public int Height
        {
            get => Size.Height;
            set => Size.Height = value;
        }
        public int Top => Y;
        public int Bottom => Y - Height;
        public int Left => X;
        public int Right => X + Width;
        public bool IsZero => X == 0 && Y == 0 && Width == 0 && Height == 0;
        #endregion
    }
}

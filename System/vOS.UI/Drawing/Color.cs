namespace vOS.UI.Drawing
{
    public unsafe struct Color
    {
        #region Constructors
        public Color(byte r = 0, byte g = 0, byte b = 0, byte a = 0)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color(System.Drawing.Color sColor)
        {
            R = sColor.R;
            G = sColor.G;
            B = sColor.B;
            A = sColor.A;
        }
        #endregion

        #region Variables
        private fixed byte argb[4];
        #endregion

        #region Properties
        public static readonly Color Empty;
        public byte this[int index]
        {
            get => argb[index];
            set => argb[index] = value;
        }
        public byte R
        {
            get => argb[1];
            private set => argb[1] = value;
        }
        public byte G
        {
            get => argb[2];
            private set => argb[2] = value;
        }
        public byte B
        {
            get => argb[3];
            private set => argb[3] = value;
        }
        public byte A
        {
            get => argb[0];
            private set => argb[0] = value;
        }
        public bool IsEmpty => R == 0 && G == 0 && B == 0 && A == 0;
        #endregion
    }
}

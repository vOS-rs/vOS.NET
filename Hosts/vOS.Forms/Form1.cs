using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vOS.UI;
using vOS.UserSpace.Instance;

namespace vOS.Forms
{
    public partial class img_vOS : Form
    {
        Graphics graph;
        List<Tuple<Pen, Point>> pixels;

        public img_vOS()
        {
            InitializeComponent();

            OS.Init(Path.Combine(Environment.CurrentDirectory, "vOS_drive"), OS.DisplayModes.Desktop);

            WindowManager.NewWindow += WindowManager_NewWindow;
        }
        private void InitGraph()
        {
            graph = pictureBox1.CreateGraphics();
        }

        private void img_vOS_Load(object sender, EventArgs e)
        {
            InitGraph();
            Process.Start("HelloWorldUI");
        }

        private void WindowManager_NewWindow(object sender, UI.Graphics.Window window)
        {
            window.OnDraw += Window_OnDraw;
        }

        private void Window_OnDraw(object sender, UI.Drawing.PixelMatrix matrix)
        {
            var _pixels = new List<Tuple<Pen, Point>>();
            var colors = matrix.Colors;

            for (int i = 0; i < matrix.Colors.Length; i++)
            {
                //Buffer.BlockCopy(colors[i].Argb, 0, pixels, i * 4, 4); // 27fps

                //Array.Copy(colors[i].Argb, 0, pixels, i * 4, 4); // 23fps

                // 28fps
                /*pixels[i * 4] = colors[i].B;      // Blue
                pixels[i * 4 + 1] = colors[i].G;      // Green
                pixels[i * 4 + 2] = colors[i].R;      // Red
                pixels[i * 4 + 3] = colors[i].A;*/    // Alpha

                // 27fps
                /*pixels[i * 4] = colors[i].Argb[3];      // Blue
                pixels[i * 4 + 1] = colors[i].Argb[2];      // Green
                pixels[i * 4 + 2] = colors[i].Argb[1];      // Red
                pixels[i * 4 + 3] = colors[i].Argb[0];*/   // Alpha

                // 30fps
                pixels[i * 4] = colors[i][3];      // Blue
                pixels[i * 4 + 1] = colors[i][2];      // Green
                pixels[i * 4 + 2] = colors[i][1];      // Red
                pixels[i * 4 + 3] = colors[i][0];   // Alpha
            }
        }

        private void img_vOS_Paint(object sender, PaintEventArgs e)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write()
            }
                Image.
                var img = new Image();
            e.Graphics.DrawImage()
        }
    }
}

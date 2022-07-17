using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using vOS.UI;
using Window = System.Windows.Window;

namespace vOS.WPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("kernel32")]
        static extern bool AllocConsole();

        WriteableBitmap wb;
        UI.Drawing.Size SafeDrawArea;

        public MainWindow()
        {
            InitializeComponent();

            //AllocConsole();
            /*var outputter = new TextBlockOutputter(txtOut);
            Console.SetOut(outputter);*/

            //Console.WriteLine("Initializing Virtual OS...");
            OS.Init(Path.Combine(Environment.CurrentDirectory, "vOS_drive"), OS.DisplayModes.Desktop);
            WindowManager.NewWindow += WindowManager_NewWindow;
            //txtOut.Text = string.Empty;
        }

        

        private void InitCanvas()
        {
            // Create a new image
            Image img = new Image();
            RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(img, EdgeMode.Aliased);

            // Add this image to the canvas
            canvas.Children.Add(img);
            int width = (int)canvas.ActualWidth;
            int height = (int)canvas.ActualHeight;

            // Create the bitmap, and set
            wb = new WriteableBitmap(
                width,
                height,
                0, 0,
                PixelFormats.Bgra32,
                null
                );


            SafeDrawArea = new UI.Drawing.Size(width, height);

            img.Source = wb;
            img.Stretch = Stretch.None;
            img.HorizontalAlignment = HorizontalAlignment.Left;
            img.VerticalAlignment = VerticalAlignment.Top;
            Panel.SetZIndex(img, -100);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitCanvas();

            Process.Start("HelloWorldUI");
        }

        private void WindowManager_NewWindow(object sender, UI.Window window)
        {
            window.OnDraw += Window_OnDraw;
        }

        private void Window_OnDraw(object sender, UI.Drawing.PixelMatrix matrix)
        {
            var tick = Environment.TickCount;
            var colors = matrix.Colors;

            //Console.WriteLine(Environment.TickCount - tick); // 0
            Int32Rect rect = new Int32Rect(0, 0,
                matrix.Rectangle.Width > SafeDrawArea.Width ? SafeDrawArea.Width : matrix.Rectangle.Width,
                matrix.Rectangle.Height > SafeDrawArea.Height ? SafeDrawArea.Height : matrix.Rectangle.Height);
            /*int size = rect.Width * rect.Height * 4;
            byte[] pixels = new byte[size];

            // Setup the pixel array
            for (int i = 0; i < rect.Height * rect.Width; ++i)
            {
                pixels[i * 4 + 0] = 0;   // Blue
                pixels[i * 4 + 1] = 0;     // Green
                pixels[i * 4 + 2] = 0;     // Red
                pixels[i * 4 + 3] = 255;   // Alpha
            }*/

            //Console.WriteLine(Environment.TickCount - tick); // 0
            byte[] pixels = new byte[matrix.Colors.Length * 4];
            //Console.WriteLine(Environment.TickCount - tick); // 0

            for (int i = 0; i < colors.Length; i++)
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

            //Console.WriteLine(Environment.TickCount - tick); // 31
            Dispatcher.Invoke(() =>
                wb.WritePixels(rect, pixels, rect.Width * 4, 0, 0)); //matrix.Rectangle.X, matrix.Rectangle.Y
            //Console.WriteLine(Environment.TickCount - tick); // 31-47
        }

        /*private void txtIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                Console.WriteLine(txtIn.Text);
                var inputter = new TextBlockInputter(txtIn);
                Console.SetIn(inputter);
                Console.ReadLine();
                txtIn.Clear();
            }
        }*/
    }
}

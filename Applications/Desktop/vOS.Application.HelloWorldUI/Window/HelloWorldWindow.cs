using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vOS.UI;
using vOS.UI.Drawing;
using vOS.UI.Graphics;

namespace vOS.Application.HelloWorldUI
{
    class HelloWorldWindow : Window
    {
        //private long drawCount;

        public HelloWorldWindow()
        {
            /*var Tom = new Rectangle[]
            {
                new Rectangle(1,1,5,1), // ---
                new Rectangle(3,1,1,5), //  |

                new Rectangle(8,1,3,1), //  _
                new Rectangle(7,2,1,3), // |
                new Rectangle(11,2,1,3), //   |
                new Rectangle(8,5,3,1), //  -

                
                new Rectangle(13,1,1,5), // |
                new Rectangle(14,2,1,1), //  '
                new Rectangle(15,3,1,1), //  .
                new Rectangle(16,2,1,1), //   '
                new Rectangle(17,1,1,5), //     |
            };

            foreach (var rect in Tom)
                Draw(rect);

            PrintDrawCount();
            Test();*/


            var text = new TextBox
            {
                Text = "Hello World"
            };
        }

        /*private async void PrintDrawCount()
        {
            while (true)
            {
                Console.WriteLine("Draw count = {0}", drawCount);
                drawCount = 0;
                await Task.Delay(1000);
            }
        }

        private void Test()
        {
            PixelMatrix matrix;
            while (true)
            {
                Console.WriteLine(Environment.TickCount); // 0
                matrix = new PixelMatrix(new Rectangle(100, 100, 1920, 1080), new Color(System.Drawing.Color.Gray));
                Console.WriteLine(Environment.TickCount); // 31
                //Console.WriteLine(Environment.TickCount - tick); // 31
                Draw(matrix);
                Console.WriteLine(Environment.TickCount + "========="); // 47
                drawCount++;
                matrix = new PixelMatrix(new Rectangle(100, 100, 1920, 1080), new Color(System.Drawing.Color.Black));
                Draw(matrix);
                drawCount++;
            }
        }*/
    }
}

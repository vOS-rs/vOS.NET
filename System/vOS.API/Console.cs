using System;
using System.Text;

namespace vOS.API
{
    public class Console
    {
        public static void Write(object value)
        {

        }

        public static void WriteLine(object value) =>
            Write(value + Environment.NewLine);

        public static int Read()
        {
            //System.Diagnostics.Process.GetCurrentProcess()

            return '\0';
        }

        public static string ReadLine()
        {
            int key = '\0';
            StringBuilder line = new();

            while (key != '\n')
            {
                key = Read();
                line.Append(key);


            }

            return line.ToString();
        }
    }
}

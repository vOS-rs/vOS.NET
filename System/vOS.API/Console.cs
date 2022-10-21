using System;
using System.Text;

namespace vOS.API
{
    public class Console
    {
        public void Write(object value)
        {
            
        }

        public void WriteLine(object value) =>
            Write(value + Environment.NewLine);

        public int Read()
        {
            //System.Diagnostics.Process.GetCurrentProcess()

            return '\0';
        }

        public string ReadLine()
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

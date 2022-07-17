using System;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace vOS.WPF
{
    public class TextBlockInputter : TextReader
    {
        private readonly TextBox textBox = null;

        public TextBlockInputter(TextBox input)
        {
            textBox = input;
        }

        public override string ReadLine() => textBox.Text;
    }
}
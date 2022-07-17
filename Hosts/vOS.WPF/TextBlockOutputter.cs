// https://social.technet.microsoft.com/wiki/contents/articles/12347.wpf-howto-add-a-debugoutput-console-to-your-application.aspx
using System;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace vOS.WPF
{
    public class TextBlockOutputter : TextWriter
    {
        private readonly TextBlock textBlock = null;

        public TextBlockOutputter(TextBlock output)
        {
            textBlock = output;
        }

        public override void Write(char value)
        {
            base.Write(value);
            _ = textBlock.Dispatcher.BeginInvoke(new Action(() =>
              {
                  textBlock.Text += value.ToString();
              }));
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
}
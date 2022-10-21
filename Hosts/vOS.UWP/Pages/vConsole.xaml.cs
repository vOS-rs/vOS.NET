#nullable enable

using System;
using System.IO;
using System.Runtime.Loader;
using vOS.UserSpace.Instance;
using Windows.UI.Xaml.Controls;

namespace vOS.UWP.Pages
{
    public sealed partial class vConsole : Page
    {
        public vConsole()
        {
            InitializeComponent();

            test();

            WriteLine("Initializing Virtual OS...");
            OS.Init(Path.Combine(Environment.CurrentDirectory, "vOS_drive"), OS.DisplayModes.Console);
            Clear();
            var process = Process.Start("terminal", string.Join(" ", Environment.GetCommandLineArgs()));
            //process.WaitUntilExit();
            //Command.Send("terminal " + string.Join(" ", args));
            

        }

        private void Write(string value) =>
            Console_Text.Text += value;

        private void WriteLine(string? value = "") =>
            Write((value ?? string.Empty) + Environment.NewLine);

        private void Clear() =>
            Console_Text.Text = string.Empty;

        void test()
        {
            var dom = AppDomain.CreateDomain("test");
            

            AppDomain.Unload(dom);
        }
    }
}

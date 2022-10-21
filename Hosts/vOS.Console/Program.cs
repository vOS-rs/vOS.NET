using System;
using System.IO;
using vOS.UserSpace.Instance;

namespace vOS.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Initializing Virtual OS...");
            OS.Init(Path.Combine(Environment.CurrentDirectory, "vOS_drive"), OS.DisplayModes.Console);
            System.Console.Clear();
            var process = Process.Start("terminal", string.Join(" ", args));
            process.WaitUntilExit();
            //Command.Send("terminal " + string.Join(" ", args));

        }
    }
}

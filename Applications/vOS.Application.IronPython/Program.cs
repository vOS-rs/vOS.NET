using System;
using IronPython.Hosting;

namespace vOS.Application.IronPython
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var eng = Python.CreateEngine();
            var scope = eng.CreateScope();

            if (args.Length == 1)
                ShowHelp();
            else
                eng.ExecuteFile(args[1]);

            return 0;
        }

        private static void ShowHelp()
        {
            Console.WriteLine("IronPython [PythonScript FileName]");
        }
    }
}

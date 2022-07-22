using System;
using System.IO;

namespace vOS.Applications.Show
{
    public class Program
    {
        enum Options
        {
            None,
            Directory,
            Space,
        }

        static bool current = false;
        static Options option = Options.None;

        public static int Main(string[] args)
        {
            args[0] = string.Empty;

            foreach (var arg in args)
            {
                switch (arg)
                {
                    case "current":
                    case "cur":
                        current = true;
                        break;

                    case "directory":
                    case "dir":
                        option = Options.Directory;
                        break;

                    case "space":
                    case "sp":
                        option = Options.Space;
                        break;
                }
            }

            switch (option)
            {
                case Options.None:
                    break;

                case Options.Directory:
                    ShowDirectory();
                    break;

                case Options.Space:
                    break;
            }

            return 0;
        }

        public static void ShowDirectory()
        {
            var folders = Directory.GetDirectories(Environment.CurrentDirectory);
            var files = Directory.GetFiles(Environment.CurrentDirectory);

            foreach (var folder in folders)
            {
                Console.WriteLine(Path.GetDirectoryName(folder));
            }

            foreach (var file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }
        }

        public static void ShowSpace()
        {

        }
    }
}

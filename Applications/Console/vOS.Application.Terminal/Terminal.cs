using System;
using System.Collections.Generic;
using System.Text;

namespace vOS.Application.Terminal
{
    internal static class Terminal
    {
        internal static void WriteLine(string line = "")
        {
            switch (OS.DisplayMode)
            {
                case OS.DisplayModes.Console:
                    Console.WriteLine(line);
                    break;

                case OS.DisplayModes.Desktop:
                    break;

                default:
                    break;
            }
        }

        internal static void Write(string line)
        {
            switch (OS.DisplayMode)
            {
                case OS.DisplayModes.Console:
                    Console.Write(line);
                    break;

                case OS.DisplayModes.Desktop:
                    break;

                default:
                    break;
            }
        }

        internal static string ReadLine()
        {
            switch (OS.DisplayMode)
            {
                case OS.DisplayModes.Console:
                    return Console.ReadLine();

                case OS.DisplayModes.Desktop:
                    break;

                default:
                    break;
            }
            throw new NotImplementedException("Not available on desktop mode");
        }
    }
}

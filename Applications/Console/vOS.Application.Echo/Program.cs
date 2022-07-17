using System;

namespace vOS.Application.Echo
{
    public class Program
    {
        public static int Main(string[] args)
        {
            args[0] = string.Empty;

            Console.WriteLine(string.Join(" ", args));

            return 0;
        }
    }
}

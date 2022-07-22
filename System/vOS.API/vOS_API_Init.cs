using System;
using System.Collections.Generic;
using System.Text;

namespace vOS.API
{
    public static class vOS_API_Init
    {
        private static Guid ProcessId;

        public static string Tag;

        public static int main(string[] arguments, Guid instance, Guid previousInstance, int windowState)
        {
            ProcessId = instance;
            Application.Arguments = arguments;

            return 0;
        }
    }
}

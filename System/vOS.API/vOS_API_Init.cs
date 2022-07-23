using System;
using System.Collections.Generic;
using System.Text;

namespace vOS.API
{
    public class vOS_API_Init
    {
        public Application Application;

        private Guid ProcessId;

        public int Main(string[] arguments, Guid instance, Guid previousInstance, int windowState)
        {
            Application = new Application();

            ProcessId = instance;
            Application.Arguments = arguments;

            return 0;
        }
    }
}

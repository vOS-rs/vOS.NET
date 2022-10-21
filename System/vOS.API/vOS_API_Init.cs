using System;
using System.Collections.Generic;
using System.Text;
using vOS.UserSpace;
using vOS.UserSpace.Instance;

namespace vOS.API
{
    public class vOS_API_Init
    {
        public Console Console;
        public Application Application;

        public readonly Process CurrentInstance;

        private Guid InstanceHandle;
        private User user;

        public vOS_API_Init(string[] arguments, Guid instance, Guid previousInstance, int windowState)
        {
            Application = new Application();
            InitConsole();

            user = User.GetCurrentUser(InstanceHandle);
            CurrentInstance = Process.GetProcessByHandle(InstanceHandle);
            InstanceHandle = instance;

            Application.Arguments = arguments;
        }

        private void InitConsole()
        {


            Console = new Console();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace vOS.API
{
    internal static class vOS_API_Init
    {
        private static Type vOS_API_InitType;
        private static object vOS_API_InitInstance;

        internal static void Load()
        {
            // vOS_API_Init class instance
            vOS_API_InitType = Instance.Assembly.GetType("vOS.API.vOS_API_Init");
            vOS_API_InitInstance = Activator.CreateInstance(vOS_API_InitType);

            // vOS_API_Init api instance fields
            Application.Load(vOS_API_InitType
                .GetField("Application")
                .GetValue(vOS_API_InitInstance));

            // vOS_API_Init fields, properties and methods
            MainMethod = vOS_API_InitType.GetMethod("Main");
        }

        private static MethodInfo MainMethod;
        public static int Main(string[] arguments, Guid instance, Guid previousInstance, int windowState) =>
            (int)MainMethod.Invoke(vOS_API_InitInstance, new object[] { arguments, instance, previousInstance, windowState });
    }
}

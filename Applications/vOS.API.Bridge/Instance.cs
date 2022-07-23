using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace vOS.API
{
    internal class Instance
    {
        internal static Assembly Assembly;

        internal static void Load()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var apis = assemblies.Where(assembly =>
            {
                var name = assembly.GetName();

                return name.Name == "vOS.API";
            });

            if (apis.Count() == 0)
                throw new DllNotFoundException("vOS.API.dll");

            Assembly = apis.First();

            vOS_API_Init.Load();
            Console.Load();
        }
    }

    internal class Console
    {
        private static Type ConsoleType;

        internal static void Load() =>
            ConsoleType = Instance.Assembly.GetType("vOS.API.Console");

        internal static void Write(object value)
        {
            var method = ConsoleType.GetMethod("Write");

            method.Invoke(null, new object[] { value });
        }
    }
}

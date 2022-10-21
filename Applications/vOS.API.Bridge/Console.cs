using System;
using System.Reflection;

namespace vOS.API
{
    internal static class Console
    {
        private static Type ConsoleType;
        private static object ConsoleInstance;

        internal static void Load(Assembly assembly, object instance)
        {
            ConsoleType = assembly.GetType("vOS.API.Console");
            ConsoleInstance = instance;

            writeMethod = ConsoleType.GetMethod("Write");
            writeLineMethod = ConsoleType.GetMethod("WriteLine");
        }

        private static MethodInfo writeMethod;
        internal static void Write(object value) =>
            writeMethod.Invoke(ConsoleInstance, new object[] { value });

        private static MethodInfo writeLineMethod;
        internal static void WriteLine(object value) =>
            writeLineMethod.Invoke(ConsoleInstance, new object[] { value });
    }
}

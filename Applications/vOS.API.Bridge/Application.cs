using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace vOS.API
{
    internal static class Application
    {
        private static Type ApplicationType;
        private static object ApplicationInstance;

        internal static void Load(object instance)
        {
            ApplicationType = Instance.Assembly.GetType("vOS.API.Application");
            ApplicationInstance = instance;

            argumentsField = ApplicationType.GetProperty("Arguments");
        }

        private static PropertyInfo argumentsField;
        public static string[] Arguments
        {
            get => argumentsField.GetValue(ApplicationInstance) as string[];
        }
    }
}

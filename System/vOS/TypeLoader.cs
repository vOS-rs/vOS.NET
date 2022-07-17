// https://siderite.dev/blog/dynamically-loading-types-from-assembly.html/#at1839007142
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace vOS
{
    public class TypeLoader
    {
        private readonly object _resolutionLock = new object();

        private Assembly Context_Resolving(AssemblyLoadContext context, AssemblyName assemblyName)
        {
            var expectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName.Name + ".dll");
            return context.LoadFromAssemblyPath(expectedPath);
        }

        public Type LoadType(string typeName, string assemblyPath)
        {
            var context = AssemblyLoadContext.Default;
            lock (_resolutionLock)
            {
                context.Resolving += Context_Resolving;
                var type = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(ass => ass.GetTypes().Where(t => t.FullName == typeName))
                    .FirstOrDefault();
                if (type != null)
                {
                    return type;
                }
                var assembly = context.LoadFromAssemblyPath(assemblyPath);

                type = assembly.GetType(typeName, true);
                context.Resolving -= Context_Resolving;
                return type;
            }
        }
    }
}

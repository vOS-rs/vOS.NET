using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace vOS
{
    class vExeAssemblyLoadContext : AssemblyLoadContext
    {
        protected override Assembly Load(AssemblyName name)
        {
            return LoadFromAssemblyName(name);
            /*string assemblyPath = _resolver.ResolveAssemblyToPath(name);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;*/
        }
    }
}

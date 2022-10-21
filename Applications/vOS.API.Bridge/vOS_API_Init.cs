using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace vOS.API
{
    public static class vOS_API_Init
    {
        internal static Assembly Assembly;

        private static Type vOS_API_InitType;
        private static object vOS_API_InitInstance;

        // The main function inside the API should be called first
        public static int Main(string[] arguments, Guid instance, Guid previousInstance, int windowState)
        {
            GetApiAssembly(); // Get vOS.API
            CreateNewApiInstance(arguments, // Create a new vOS_API_Init(...);
                instance,
                previousInstance,
                windowState);
            InitApi();

            return StartMainProgramm(arguments); // Start the vMain of the current programm
        }
        
        private static void GetApiAssembly()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var apis = assemblies.Where(assembly =>
                assembly.GetName().Name == "vOS.API");

            if (apis.Count() == 0)
                throw new DllNotFoundException("vOS.API.dll");

            Assembly = apis.First();
        }

        private static void CreateNewApiInstance(string[] arguments, Guid instance, Guid previousInstance, int windowState)
        {
            // vOS_API_Init class instance
            vOS_API_InitType = Assembly.GetType("vOS.API.vOS_API_Init");
            vOS_API_InitInstance = Activator.CreateInstance(vOS_API_InitType, 
                arguments,
                instance,
                previousInstance,
                windowState );
        }

        private static void InitApi()
        {
            // vOS_API_Init api instance fields
            Application.Load(Assembly,
                             vOS_API_InitType
                                .GetField("Application")
                                .GetValue(vOS_API_InitInstance) );
            Console.Load(Assembly,
                         vOS_API_InitType
                            .GetField("Console")
                            .GetValue(vOS_API_InitInstance) );

            // vOS_API_Init fields, properties and methods
            // None
        }

        private static int StartMainProgramm(string[] arguments)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var vMains = GetMainsWithHelpAttribute(assembly);

            // No main
            if (vMains.Count() == 0)
                throw new EntryPointNotFoundException("Can't find the vMain entry point");

            // Get the main
            var vMain = vMains.First();

            // Start the app
            var parameters = vMain.GetParameters();
            object programReturn;

            if (parameters.Length == 1 && // vMain(string[] arguments)
                     parameters[0].ParameterType == typeof(string[]))
                programReturn = vMain.Invoke(null, new object[] { arguments }); // Invoke the main with the given arguments
            else if (parameters.Length == 0) // vMain()
                programReturn = vMain.Invoke(null, null); // Invoke the main without arguments
            else // Bad main, ex: main(int index, object value)
                throw new Exception("Bad Entry Point");

            // Get exit code
            if (programReturn is int exitCode)
                return exitCode;
            else if (programReturn is Task<int> exitCodeAsync)
                return exitCodeAsync.GetAwaiter().GetResult();
            else
                return 0;
        }

        private static IEnumerable<MethodInfo> GetMainsWithHelpAttribute(Assembly assembly)
        {
            // I learn that double loop and a break is bad for computer
            foreach (Type type in assembly.GetTypes())
                foreach (MethodInfo method in type.GetMethods())
                    if (CheckValidMainMethode(method))
                        yield return method;
        }

        private static bool CheckValidMainMethode(MethodInfo method)
        {
            // Must be static
            if (!method.IsStatic)
                return false;

            // Must have the name Main
            if (method.Name.ToLower() != "vmain")
                return false;

            // Must have 1 string parameter or not
            var parameters = method.GetParameters();

            if (// main(string[] arguments)
                parameters.Length == 1 &&
                parameters[0].ParameterType != typeof(string[]) ||
                // main()
                parameters.Length > 4)
                return false;

            // Must return a integer or not
            if (method.ReturnType != typeof(int) &&
                method.ReturnType != typeof(Task<int>) &&
                method.ReturnType != typeof(void))
                return false;

            return true;
        }
    }
}

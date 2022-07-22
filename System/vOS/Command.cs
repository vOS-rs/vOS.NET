using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace vOS
{
    public class Command
    {
        internal static int Send(string command)
        {
            // TODO: File perm ? (For user)
            var currentSession = Session.CurrentSession;

            var args = CommandLineToArgs(command);

            if (args.Length < 1)
                return 0;

            var fileName = args[0];

            // TODO: auto completion

            try
            {
                switch (Path.GetExtension(fileName).ToLower())
                {
                    case ".vapp":
                        // https://stackoverflow.com/questions/1395205/better-way-to-check-if-a-path-is-a-file-or-a-directory
                        FileAttributes attr = File.GetAttributes(fileName);

                        if (attr.HasFlag(FileAttributes.Directory)) // Folder (Executable folder)
                        {
                            //var vapp = 
                        }
                        else // File (Zip)
                        {
                            Stream unzippedEntryStream; // Unzipped data from a file in the archive

                            // https://docs.microsoft.com/fr-fr/dotnet/api/system.io.compression.ziparchive
                            using (FileStream zipToOpen = new FileStream(fileName, FileMode.Open))
                            {
                                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                                {
                                    // https://exceptionshub.com/unzip-a-memorystream-contains-the-zip-file-and-get-the-files.html
                                    foreach (ZipArchiveEntry entry in archive.Entries)
                                    {
                                        if (entry.FullName.EndsWith(".vexe", StringComparison.OrdinalIgnoreCase))
                                        {
                                            unzippedEntryStream = entry.Open(); // .Open will return a stream

                                            // Process entry data

                                            // https://www.delftstack.com/howto/csharp/stream-to-byte-array-in-csharp/
                                            MemoryStream ms = new MemoryStream();
                                            unzippedEntryStream.CopyTo(ms);

                                            var assembly_ = Assembly.Load(ms.ToArray());
                                            return ExecuteAssembly(assembly_, args);
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case "":
                    case ".vexe":
                        // TODO: remove
                        if (!Path.HasExtension(fileName))
                            fileName += ".vexe";

                        if (!File.Exists(fileName))
                        {
                            string relativePath = GetActualCaseForFileName(Path.Combine(Storage.GetFolder(Storage.KnowFolders.Applications), fileName));

                            if (File.Exists(relativePath))
                                fileName = relativePath;
                        }

                        /*
                         * In .Net Standart 2 we can't unload assembly so if we execut the same vexe with different name
                         * we got a "Assembly with same name is already loaded" exception
                         * Upgrading to 2.1 will make less compatibility
                         * TODO: Make assembly load 
                         */

                        //var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(fileName);

                        /*var wat = new vExeAssemblyLoadContext();
                        var assembly = wat.LoadFromAssemblyPath(fileName);*/

                        /*var dom = AppDomain.CreateDomain("test");
                        byte[] bytes = System.IO.File.ReadAllBytes(fileName);
                        var assembly = dom.Load(bytes);*/

                        var assembly = Assembly.LoadFrom(fileName);

                        //var test = new TypeLoader();


                        var exitCode = ExecuteAssembly(assembly, args);


                        //AppDomain.Unload(dom);


                        return exitCode;

                    default:
                        throw new Exception("Can't open this file");
                }
            }
            catch (TargetInvocationException tie)
            {
                Console.WriteLine(tie.InnerException);
            }
            catch (Exception e)
            {
                Console.WriteLine("Execution failed: " + e.Message);
                if (e.InnerException != null)
                    Console.WriteLine(e.InnerException.Message);
            }

            return 1;
        }

        private static int ExecuteAssembly(Assembly assembly, string[] args)
        {
            var mains = GetMainsWithHelpAttribute(assembly);
            var mos = assembly.GetLoadedModules();

            // No main
            if (mains.Count() < 1)
                throw new Exception("We can't execute this");

            // Multi mains
            if (mains.Count() > 1)
                Console.WriteLine("Warning: more than one entry point exists");

            // Get the main
            var main = mains.First();
            object programReturn;

            // Instanciate a class if needed
            object instance = null;

            if (!main.IsStatic)
                instance = Activator.CreateInstance(main.DeclaringType);

            // Start the app
            var parameters = main.GetParameters();

            if (parameters.Length == 4 && // main(string[] arguments, Guid instance, Guid previousInstance, int windowState)
                parameters[0].ParameterType == typeof(string[]) &&
                parameters[1].ParameterType == typeof(Guid) &&
                parameters[2].ParameterType == typeof(Guid) &&
                parameters[3].ParameterType == typeof(int))
                programReturn = main.Invoke(instance, new object[] { args, Guid.NewGuid(), Guid.Empty, 0 }); // Invoke the main with the given arguments and window info
            else if (parameters.Length == 1 && // main(string[] arguments)
                     parameters[0].ParameterType == typeof(string[]))
                programReturn = main.Invoke(instance, new object[] { args }); // Invoke the main with the given arguments
            else if (parameters.Length == 0) // main()
                programReturn = main.Invoke(instance, null); // Invoke the main without arguments
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

        // https://stackoverflow.com/questions/55479937/open-a-file-while-ignoring-case-in-the-path-and-filename
        private static string GetActualCaseForFileName(string pathAndFileName)
        {
            string directory = Path.GetDirectoryName(pathAndFileName);
            string pattern = Path.GetFileName(pathAndFileName);
            string resultFileName;

            // Enumerate all files in the directory, using the file name as a pattern
            // This will list all case variants of the filename even on file systems that
            // are case sensitive
            IEnumerable<string> foundFiles = Directory.EnumerateFiles(directory, pattern);

            if (foundFiles.Any())
            {
                if (foundFiles.Count() > 1)
                {
                    // More than two files with the same name but different case spelling found
                    throw new Exception("Ambiguous File reference for " + pathAndFileName);
                }
                else
                {
                    resultFileName = foundFiles.First();
                }
            }
            else
            {
                throw new FileNotFoundException("File not found: " + pathAndFileName, pathAndFileName);
            }

            return resultFileName;
        }

        // https://stackoverflow.com/questions/607178/how-enumerate-all-classes-with-custom-class-attribute
        private static IEnumerable<MethodInfo> GetMainsWithHelpAttribute(Assembly assembly)
        {
            // I learn that double loop and a break is bad for computer
            foreach (Type type in assembly.GetTypes())
                foreach (MethodInfo method in type.GetMethods())
                    if (CheckValidMainMethode(method))
                        yield return method;
        }

        // https://docs.microsoft.com/dotnet/csharp/fundamentals/program-structure/main-command-line#overview
        private static bool CheckValidMainMethode(MethodInfo method)
        {
            /*// Must be static
            if (!method.IsStatic)
                return false;*/

            // Must have the name Main
            if (method.Name.ToLower() != "main")
                return false;

            // Must have 1 string parameter or not
            var parameters = method.GetParameters();

            if (// main(string[] arguments, Guid instance, Guid previousInstance, int windowState)
                parameters.Length == 4 &&
                parameters[0].ParameterType != typeof(string[]) &&
                parameters[1].ParameterType != typeof(Guid) &&
                parameters[2].ParameterType != typeof(Guid) &&
                parameters[3].ParameterType != typeof(int) ||
                // main(string[] arguments)
                parameters.Length == 1 &&
                parameters[0].ParameterType != typeof(string[]) ||
                // main()
                parameters.Length > 1)
                return false;

            // Must return a integer or not
            if (method.ReturnType != typeof(int) &&
                         method.ReturnType != typeof(Task<int>) && method.ReturnType != typeof(void))
                return false;

            return true;
        }

        /*private static bool CheckValidApiMethode(MethodInfo method)
        {
            // Must be static
            if (!method.IsStatic)
                return false;

            // Must have the name vOS_Init
            if (method.Name != "vOS_Main")
                return false;

            // Must have 1 string parameter or not
            var parameters = method.GetParameters();

            // vOS_Main(Guid instance, Guid previousInstance, string commandLineArguments, int windowState)
            if (parameters.Length != 4 ||
                parameters[0].ParameterType != typeof(Guid) ||
                parameters[1].ParameterType != typeof(Guid) ||
                parameters[2].ParameterType != typeof(string) ||
                parameters[3].ParameterType != typeof(int))
                return false;

            // Must return a integer
            if (method.ReturnType != typeof(int))
                return false;

            return true;
        }*/

        // http://www.blackbeltcoder.com/Articles/strings/a-c-command-line-parser
        // https://stackoverflow.com/questions/24047674/how-to-reset-position-in-stringreader-to-begining-of-string
        private static string[] CommandLineToArgs(string commandLine)
        {
            if (commandLine == null)
                throw new NullReferenceException("commandLine");

            var Arguments = new List<string>();
            int start;
            int pos = 0;
            string result;

            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(commandLine)))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    // Loop until all characters processed
                    while (!sr.EndOfStream)
                    {
                        // Skip whitespace
                        if (char.IsWhiteSpace((char)sr.Peek()))
                        {
                            sr.Read();
                            pos++;
                        }

                        if (sr.Peek() == '"' || sr.Peek() == '\'')
                        {
                            // Parse quoted argument
                            char quote = (char)sr.Read();
                            pos++;
                            start = pos;
                            while (!sr.EndOfStream && sr.Peek() != quote)
                            {
                                sr.Read();
                                pos++;
                            }
                            result = commandLine.Substring(start, pos - start);
                            sr.Read(); // Eat closing quote
                            pos++;
                        }
                        else
                        {
                            // Parse vargument
                            start = pos;
                            while (!sr.EndOfStream && !char.IsWhiteSpace((char)sr.Peek()))
                            {
                                sr.Read();
                                pos++;
                            }
                            result = commandLine.Substring(start, pos - start);
                        }

                        // Adding the parsed argument
                        Arguments.Add(result);
                    }
                }
            }

            // Returning
            return Arguments.ToArray();
        }
    }
}

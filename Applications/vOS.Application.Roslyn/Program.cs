using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace vOS.Application.Roslyn
{
    // https://docs.microsoft.com/en-us/dotnet/api/microsoft.csharp.csharpcodeprovider?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0
    // https://stackoverflow.com/questions/4181668/execute-c-sharp-code-at-runtime-from-code-file
    public class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length > 1)
            {
                //  First parameter is the source file name.
                if (File.Exists(args[1]))
                {
                    return ExecuteScript(args[1]);
                }
                else
                {
                    Console.WriteLine("Input source file not found - {0}",
                        args[1]);
                }
            }
            else
            {
                Console.WriteLine("Input source file not specified on command line!");
                ShowHelp();
            }

            return 0;
        }

        private static void ShowHelp()
        {
            Console.WriteLine("CSharp [CS or CSX Script FileName]");
        }

        public static int ExecuteScript(string sourceName)
        {
            try
            {
                string code = File.ReadAllText(sourceName);

                object value = CSharpScript.EvaluateAsync(code).Result;

                if (value is int exitCode)
                    return exitCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace vOS
{
    public class Process
    {
        #region Constructors
        public Process(string fileName, string arguements)
        {
            FileName = fileName;
            Arguements = arguements;
            Name = Path.GetFileNameWithoutExtension(fileName);
            Id = Guid.NewGuid();

            _ = Task.Run(() => BeginRuntime());
        }
        #endregion

        #region Variables
        public static event EventHandler<int> OnExit;
        private static List<Process> processes = new List<Process>();
        #endregion

        #region Properties
        public static IReadOnlyList<Process> Processes => processes;
        public string Name { get; }
        public Guid Id { get; }
        public string FileName { get; }
        public string Arguements { get; }
        public int ExitCode { get; set; } = -1;
        #endregion

        #region Methods

        public void WaitUntilExit() => SpinWait.SpinUntil(() => ExitCode != -1);

        private void BeginRuntime()
        {
            ExitCode = Command.Send(FileName + " " + Arguements);

            if (OnExit != null)
                OnExit(typeof(Process), ExitCode);
        }

        public static Process GetProcessById(Guid id) => processes.Find(p => p.Id == id);

        public static Process GetProcessesByName(string name) => processes.Find(p => p.Name == name);

        public static IReadOnlyList<Process> GetProcesses() => Processes;

        public static Process Start(string fileName, string arguments = "")
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("FileName was empty");

            var process = new Process(fileName, arguments);

            processes.Add(process);

            return process;
        }

        public static Process Start(string command)
        {
            if (string.IsNullOrEmpty(command) || string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("Command was empty");

            /*var result = Regex.Match(command, @"^([\w\-]+)");
            command = command.Replace(result.Value, string.Empty);*/

            var fileName = command.Split(new char[] { ' ' }, 1);
            var process = new Process(fileName[0], command);

            processes.Add(process);

            return process;
        }

        #endregion
    }
}

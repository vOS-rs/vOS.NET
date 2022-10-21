#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace vOS.UserSpace.Instance
{
    public class Process
    {
        #region Constructors

        public Process(User user, ProcessInfo startInfo)
        {
            handle = Guid.NewGuid();
            this.user = user;
            standardOutput = new();
            standardInput = new();
            standardError = new();

            Name = Path.GetFileNameWithoutExtension(startInfo.FileName);
            Id = 0; //Generate id
            StartInfo = startInfo;
            StandardOutput = new(standardOutput);
            StandardInput = new(standardInput);
            StandardError = new(standardError);
        }
        #endregion

        #region Variables
        // This is the private key for the API
        private readonly Guid handle;
        private User user;

        /** Standard input from process **/
        private readonly MemoryStream standardOutput;
        private readonly MemoryStream standardInput;
        private readonly MemoryStream standardError;

        public readonly string Name;
        public readonly int Id;
        public readonly ProcessInfo StartInfo;
        public readonly DateTime StartTime;

        /** Standart output from external process **/
        public readonly StreamReader StandardOutput;
        public readonly StreamWriter StandardInput;
        public readonly StreamReader StandardError;

        public event EventHandler<int>? OnExit;
        #endregion

        #region Properties
        public int? ExitCode { get; private set; }
        #endregion

        #region Methods
        public static Process Start(User user, ProcessInfo processInfo)
        {
            if (processInfo == null)
                throw new ArgumentNullException("ProcessInfo");

            var process = new Process(user, processInfo);
            process.Start();

            return process;
        }

        public static Process Start(User user, string fileName, string arguments)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("FileName was empty");

           return Start(user, new ProcessInfo(fileName, arguments));
        }

        public static Process Start(User user, string command)
        {
            if (string.IsNullOrEmpty(command) || string.IsNullOrWhiteSpace(command))
                throw new ArgumentException("Command was empty");

            var fileNames = command.Split(new char[] { ' ' }, 1);
            var arguments = command.Substring(0, fileNames[0].Length + 1);

            return Start(user, new ProcessInfo(fileNames[0], arguments));
        }

        public static IReadOnlyList<Process> GetProcesses(User user)
        {
            var instances = user.instances;
            var processes = new Process[instances.Count()];

            for (int i = 0; instances.Count() < i; i++)
                processes[i] = new Process(user, instances.ElementAt(i));

            return processes;
        }

        public static Process? GetProcessByHandle(User user, Guid handle)
        {
            if (handle == null)
                throw new ArgumentNullException("handle");

            return GetProcesses(user).FirstOrDefault(p => p.handle == handle);
        }

        public bool Start()
        {
            _ = Task.Run(() => user.StartNewInstance(this));

            return true;
        }

        public void Close()
        {
            // TODO: Find a way to handle SigKill
            throw new NotImplementedException("Close");
        }

        public void Kill()
        {
            // TODO: Find a way to purge the app from memory by force (Assembly can't be unloaded, but we just use instance so find somethings to force GC on an instance)
            throw new NotImplementedException("Kill");
        }

        public override bool Equals(object? obj) =>
            obj is Process process &&
            handle.Equals(process.handle);

        public override int GetHashCode() =>
            1786700523 + handle.GetHashCode();
        #endregion
    }
}

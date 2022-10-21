#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using vOS.UserSpace.Instance;
using System.Threading;
using System.Diagnostics;

namespace vOS.API.Diagnostics
{
    public class Process
    {
        public Process(string fileName, string arguments = "")
        {
            instance = new( new ProcessInfo(
                            fileName,
                            arguments) );
        }

        private Process(UserSpace.Instance.Process process)
        {
            this.instance = process;

            OnExit += ProcessExited;

            process.OnExit += (sender, exitCode) =>
                OnExit.Invoke(this, exitCode);
        }

        private UserSpace.Instance.Process instance;

        public string Name => instance.Name;
        public int Id => instance.Id;
        public ProcessInfo StartInfo => instance.StartInfo;
        public DateTime StartTime => instance.StartTime;
        public DateTime ExitTime { get; private set; }
        public StreamReader StandardOutput => instance.StandardOutput;
        public StreamWriter StandardInput => instance.StandardInput;
        public StreamReader StandardError => instance.StandardError;
        public int? ExitCode => instance.ExitCode;

        public readonly ProcessModuleCollection Modules;
        public TimeSpan TotalProcessorTime => DateTime.(ExitTime., DateTime.Now).Subtract(StartTime);

        public string MainWindowTitle;
        public IntPtr MainWindowHandle;
        public bool HasExited => ExitCode.HasValue;
        public bool EnableRaisingEvents;
        public ProcessModule MainModule;

        public event EventHandler<int> OnExit;
        public event DataReceivedEventHandler ErrorDataReceived;
        public event DataReceivedEventHandler OutputDataReceived;

        public static Process GetCurrentProcess()
        {
            //var instance = vOS_API_Init.

            return new Process(instance);
        }

        public static Process GetProcessById(int id)
        {
            var instance = UserSpace.Instance.Process.Processes
                                .First(p => p.Id == id);

            return new Process(instance);
        }

        public static IReadOnlyList<Process> GetProcessesByName(string name)
        {
            var instances = UserSpace.Instance.Process.Processes
                                .Where(p => p.Name == name);
            var processes = new Process[instances.Count()];

            for (int i = 0; instances.Count() < i; i++)
                processes[i] = new Process(instances.ElementAt(i));

            return processes;
        }

        public static IReadOnlyList<Process> GetProcesses()
        {
            var instances = UserSpace.Instance.Process.GetProcesses();
            var processes = new Process[instances.Count()];

            for (int i = 0; instances.Count() < i; i++)
                processes[i] = new Process(instances.ElementAt(i));

            return processes;
        }

        public static Process Start(ProcessInfo processInfo)
        {
            var instance = UserSpace.Instance.Process.Start(processInfo);

            return new Process(instance);
        }

        public static Process Start(string fileName, string arguments = "")
        {
            var instance = UserSpace.Instance.Process.Start(fileName, arguments);

            return new Process(instance);
        }

        public void BeginErrorReadLine()
        {
            throw new NotImplementedException("BeginErrorReadLine");
        }

        public void BeginOutputReadLine()
        {
            throw new NotImplementedException("BeginOutputReadLine");
        }

        public void CancelErrorRead()
        {
            throw new NotImplementedException("CancelErrorRead");
        }

        public void CancelOutputRead()
        {
            throw new NotImplementedException("CancelOutputRead");
        }

        public bool CloseMainWindow()
        {
            throw new NotImplementedException("CloseMainWindow");
        }

        public bool Start() =>
            instance.Start();

        public void Close() =>
            instance.Close();

        public void Kill() =>
            instance.Kill();

        public bool WaitForExit(int milliseconds) =>
            SpinWait.SpinUntil(() => !ExitCode.HasValue, milliseconds);

        public void WaitUntilExit() =>
            SpinWait.SpinUntil(() => !ExitCode.HasValue);
       
        public bool WaitForInputIdle()
        {
            throw new NotImplementedException("WaitForInputIdle");
        }
       
        public bool WaitForInputIdle(int milliseconds)
        {
            throw new NotImplementedException("WaitForInputIdle");
        }

        private void ProcessExited(object sender, int e)
        {
            ExitTime = DateTime.Now;
        }
    }
}

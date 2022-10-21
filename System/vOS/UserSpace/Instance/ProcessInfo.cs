#nullable enable

using System;
using System.Text;

namespace vOS.UserSpace.Instance
{
    public class ProcessInfo
    {
        //
        // Résumé :
        //     Initializes a new instance of the System.Diagnostics.ProcessStartInfo class without
        //     specifying a file name with which to start the process.
        public ProcessInfo() { }
        //
        // Résumé :
        //     Initializes a new instance of the System.Diagnostics.ProcessStartInfo class and
        //     specifies a file name such as an application or document with which to start
        //     the process.
        //
        // Paramètres :
        //   fileName:
        //     An application or document with which to start a process.
        public ProcessInfo(string fileName)
        {
            FileName = fileName;
        }
        //
        // Résumé :
        //     Initializes a new instance of the System.Diagnostics.ProcessStartInfo class,
        //     specifies an application file name with which to start the process, and specifies
        //     a set of command-line arguments to pass to the application.
        //
        // Paramètres :
        //   fileName:
        //     An application with which to start a process.
        //
        //   arguments:
        //     Command-line arguments to pass to the application when the process starts.
        public ProcessInfo(string fileName, string arguments)
        {
            FileName = fileName;
            Arguments = arguments;
        }

        /// <summary>
        /// Gets or sets the preferred encoding for standard output.
        /// </summary>
        public Encoding? StandardOutputEncoding { get; set; } = null;
        /// <summary>
        /// Gets or sets the preferred encoding for error output.
        /// </summary>
        public Encoding? StandardErrorEncoding { get; set; } = null;
        /// <summary>
        /// Gets or sets a value that indicates whether the textual output of an application
        //     is written to the System.Diagnostics.Process.StandardOutput stream.
        /// </summary>
        public bool RedirectStandardOutput { get; set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether the input for an application is read
        //     from the System.Diagnostics.Process.StandardInput stream.
        /// </summary>
        public bool RedirectStandardInput { get; set; } = false;
        /// <summary>
        /// Gets or sets a value that indicates whether the error output of an application
        //     is written to the System.Diagnostics.Process.StandardError stream.
        /// </summary>
        public bool RedirectStandardError { get; set; } = false;
        /// <summary>
        /// Gets or sets the application or document to start.
        /// </summary>
        public string FileName { get; set; } = "";
        /// <summary>
        /// Gets or sets the window handle to use when an error dialog box is shown for a
        //     process that cannot be started.
        /// </summary>
        public IntPtr ErrorDialogParentHandle { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether an error dialog box is displayed to the
        //     user if the process cannot be started.
        /// </summary>
        public bool ErrorDialog { get; set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether to start the process in a new window.
        /// </summary>
        public bool CreateNoWindow { get; set; } = false;
        /// <summary>
        /// Gets or sets the set of command-line arguments to use when starting the application.
        /// </summary>
        public string Arguments { get; set; } = "";
        /// <summary>
        /// Gets or sets the window state to use when the process is started.
        /// </summary>
        public ProcessWindowState WindowStyle { get; set; } = ProcessWindowState.Normal;
        /// <summary>
        /// When the System.Diagnostics.ProcessStartInfo.UseShellExecute property is false,
        //     gets or sets the working directory for the process to be started. When System.Diagnostics.ProcessStartInfo.UseShellExecute
        //     is true, gets or sets the directory that contains the process to be started.
        /// </summary>
        public string WorkingDirectory { get; set; } = ""
    }
}

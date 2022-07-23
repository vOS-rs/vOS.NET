using System.IO;
using System.Reflection;

namespace vOS
{
    public class OS
    {
        internal static string WorkingDirectory { get; private set; }
        public static DisplayModes DisplayMode { get; private set; }
        public enum DisplayModes
        {
            Console,
            Desktop
        }

        public static void Init(string workingDirectory, DisplayModes displayMode)
        {
            //AppDomain.CurrentDomain.
            WorkingDirectory = workingDirectory;
            DisplayMode = displayMode;

            LoadAPI();
        }

        // Make the API accessible to all programm
        private static void LoadAPI()
        {
            Assembly.LoadFile(Path.Combine(Storage.GetFolder(Storage.KnowFolders.System), "vOS.API.dll"));
        }
    }
}

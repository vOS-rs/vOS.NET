using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace vOS
{
    internal static class StoragePaths
    {
        public static string System = Path.Combine(OS.WorkingDirectory, "System");
        public static string Users = Path.Combine(OS.WorkingDirectory, "Users");
        public static string Applications = Path.Combine(OS.WorkingDirectory, "Applications");
    }
}

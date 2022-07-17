using System;
using System.Collections.Generic;
using System.Globalization;

namespace vOS.Manifest
{
    [Serializable]
    public class vAppManifest
    {
        public Dictionary<string, string> localizedName;
        public Guid Id;
        public Dictionary<string, string> localizedIconPath;
    }
}

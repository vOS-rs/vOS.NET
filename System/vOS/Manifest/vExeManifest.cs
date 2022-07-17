using System;

namespace vOS.Manifest
{
    [Serializable]
    public class vExeManifest
    {
        public enum vExeType
        {
            Console,
            Window
        }

        public string Name;
        public Guid Id;
        public vExeType exeType;
    }
}

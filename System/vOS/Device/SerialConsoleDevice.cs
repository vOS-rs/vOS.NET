using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace vOS.Device
{
    internal class SerialConsoleDevice : IDevice
    {
        public SerialConsoleDevice(string name, TextWriter tx, TextReader rx)
        {
            Name = name;
            Type = DeviceType.Port;
            Tx = tx;
            Rx = rx;
        }

        public string Name { get; private set; }

        public DeviceType Type { get; private set; }

        public readonly TextWriter Tx;
        public readonly TextReader Rx;
    }
}

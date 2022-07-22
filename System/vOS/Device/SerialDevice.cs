using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace vOS.Device
{
    internal class SerialDevice : IDevice
    {
        public SerialDevice(string name, StreamWriter tx, StreamReader rx)
        {
            Name = name;
            Type = DeviceType.Port;
            Tx = tx;
            Rx = rx;
        }

        public string Name { get; private set; }

        public DeviceType Type { get; private set; }

        public readonly StreamWriter Tx;
        public readonly StreamReader Rx;
    }
}

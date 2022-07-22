namespace vOS.Device
{
    public interface IDevice
    {
        string Name { get; }
        DeviceType Type { get; }
    }
}

// File namespace
namespace VMCore;

/// <summary>
/// Represents a device connected to the virtual computer
/// </summary>
public interface IDevice
{
    /// <summary>
    /// When data has been stored in the memory region for this device,
    /// then the output data will be sent via this output bus 
    /// </summary>
    abstract void Write(int address, ushort value);

    /// <summary>
    /// When data has been read in the memory region for this device,
    /// then the read data will be copied to this output bus
    /// </summary>
    abstract void Read(int address, ushort value);
}

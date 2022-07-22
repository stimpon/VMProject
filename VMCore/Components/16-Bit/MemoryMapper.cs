// File namespace
namespace VMCore;

/// <summary>
/// Memory mapper component for a 16 Bit CPU
/// </summary>
public class MemoryMapper
{
    /// <summary>
    /// Total amount of memory
    /// </summary>
    public byte[] Memory { get; set; }

    /// <summary>
    /// Gets or sets all virtual regions in memory
    /// </summary>
    public List<MemoryRegion> Regions;

    /// <summary>
    /// Default constructor
    /// </summary>
    public MemoryMapper(int memSize)
    {
        // Create virtual memory
        this.Memory = new byte[memSize];
        // Create memory regions array
        this.Regions = new List<MemoryRegion>();
    }

    /// <summary>
    /// Checks if this memory address is allocated for a specific device
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    public MemoryRegion? FindRegion(int address)
    {
        // Get the region from the regions array
        return this.Regions.FirstOrDefault(r => address >= r.StartAddress && address <= r.EndAddress);
    }

    /// <summary>
    /// Loads byte code into memory
    /// </summary>
    public void LoadByteCode(byte[] code, int start)
    {
        // Loop through provided byte code
        for (int i = 0; i < code.Length; i++)
        {
            // Load byte into memory
            this.Memory[start] = code[i];
            // Increment mem pointer
            start++;
        }
    }

    /// <summary>
    /// Map a device
    /// </summary>
    /// <param name="device"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public void Map(IDevice device, int start, int end)
    {
        // Create region for this device
        this.Regions.Insert(0, new()
        {
            Device = device,
            StartAddress = start,
            EndAddress = end
        });
    }

    /// <summary>
    /// Reads a 16 bit value from memory
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public ushort GetUInt16(int index)
    {
        // Check if region exist
        var region = FindRegion(index);

        // Get value from index 1
        var val1 = Memory[index];
        // Get value from index 2
        var val2 = Memory[index + 1];

        // Convert to 16 bit value
        var final = (ushort)((val1 << 8) | val2);

        // Send read data to output bus if this region belongs to a I/O device
        if (region != null) region.Device.Read(index, final);

        // Convert to 16 bit value
        return final;
    }

    /// <summary>
    /// Reads a 16 bit value from memory
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public ushort GetUInt8(int index)
    {
        // Check if region exist
        var region = FindRegion(index);

        // Get value from index
        var val = Memory[index];

        // Send read data to output bus if this region belongs to a I/O device
        if (region != null) region.Device.Read(index, val);

        // Convert to 16 bit value
        return val;
    }

    /// <summary>
    /// Saves a 16 bit value to memory
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="index"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public ushort SetUInt16(int index, ushort value)
    {
        // Check if region exist
        var region = FindRegion(index);

        // Get 8 last bits of the value
        var val1 = (byte)(value >> 8);
        // Get the first 8 bits of the value
        var val2 = (byte)(0 | value);

        // Save the last 8 bits in slot 1
        Memory[index] = val1;
        // Save the first 8 bits in slot 2
        Memory[index + 1] = val2;

        // Send written data to output bus if this region belongs to a I/O device
        if (region != null) region.Device.Write(index, value);

        // Return the saved value
        return value;
    }

    /// <summary>
    /// Saves a 16 bit value to memory
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="index"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public ushort SetUInt8(int index, byte value)
    {
        // Check if region exist
        var region = FindRegion(index);

        // Save the value to memory
        Memory[index] = value;

        // Send written data to output bus if this region belongs to a I/O device
        if (region != null) region.Device.Write(index, value);

        // Return the saved value
        return value;
    }
}

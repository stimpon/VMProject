// File namespace
namespace VMCore;

/// <summary>
/// Contains array extensions
/// </summary>
public static class ArrayExtensions
{
    /// <summary>
    /// Reads a 16 bit value from a byte array
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public static ushort GetUInt16(this byte[] arr, int index)
    {
        // Get value from index 1
        var val1 = arr[index];
        // Get value from index 2
        var val2 = arr[index + 1];
        // Convert to 16 bit value
        return (ushort)((val1 << 8) | val2);
    }

    /// <summary>
    /// Saves a 16 bit value to a byte array
    /// </summary>
    /// <param name="arr"></param>
    /// <param name="index"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static ushort SetUInt16(this byte[] arr, int index, ushort value)
    {
        // Get 8 last bits of the value
        var val1 = (byte)(value >> 8);
        // Get the first 8 bits of the value
        var val2 = (byte)(0 | value);

        // Save the last 8 bits in slot 1
        arr[index] = val1;
        // Save the first 8 bits in slot 2
        arr[index + 1] = val2;

        // Return the saved value
        return value;
    }
}

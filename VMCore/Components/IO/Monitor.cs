// File namespace
namespace VMCore;

/// <summary>
/// Represents a 32x32 monitor
/// </summary>
public class Monitor : IDevice
{
    /// <summary>
    /// <see cref="IDevice.Read(int, ushort)"/>
    /// </summary>
    /// <param name="address"></param>
    /// <param name="value"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Read(int address, ushort value)
    {
        // No handling is needed
    }

    /// <summary>
    /// <see cref="IDevice.Write(int, ushort)"/>
    /// </summary>
    /// <param name="address"></param>
    /// <param name="value"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Write(int address, ushort value)
    {
        // Byte has been written, we need to print send it to stdout

        // Get character value from literal
        var character = value & 0x00FF;

        // Get x position for the character
        var x = (address % 32);
        // Get y position for the character
        var y = ((address / 32) % 32);

        // Set cursor position
        Console.SetCursorPosition(x, y);
        // Write the character
        Console.Write((char)character);
    }
}

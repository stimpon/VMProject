// File namespace
namespace VMCore;

/// <summary>
/// Represents a virtual region in memory
/// </summary>
public class MemoryRegion
{
    /// <summary>
    /// The virtual device this memory region is allocated for
    /// </summary>
    public IDevice Device { get; set; }

    /// <summary>
    /// Starting address in actual memory for this data block
    /// </summary>
    public int StartAddress { get; set; }

    /// <summary>
    /// Ending address in actual memory for this data block
    /// </summary>
    public int EndAddress { get; set; }

}

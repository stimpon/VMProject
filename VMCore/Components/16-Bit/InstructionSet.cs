// File namespace
namespace VMCore;

/// <summary>
/// Contains instructions for the 16 bit processor
/// </summary>
public enum Instruction
{
    /// <summary>
    /// Move a value to a register
    /// </summary>
    MOV_LIT_REG = 0x10,

    /// <summary>
    /// Move a value from one register to another register
    /// </summary>
    MOV_REG_REG = 0x11,

    /// <summary>
    /// Move a value from a register to memory
    /// </summary>
    MOV_REG_MEM = 0x12,

    /// <summary>
    /// Move a value from 1 location in memory to another location
    /// </summary>
    MOV_MEM_MEM = 0x13,

    /// <summary>
    /// Move value from memory to a register
    /// </summary>
    MOV_MEM_REG = 0x14,

    /// <summary>
    /// Add instruction
    /// </summary>
    ADD = 0x02,

    /// <summary>
    /// Halt instruction
    /// </summary>
    HLT = 0x01,

    /// <summary>
    /// No operationS
    /// </summary>
    NOP = 0x00

}

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
    /// Push a literal value to the stack
    /// </summary>
    PUSH_LIT = 0x15,

    /// <summary>
    /// Push a value from a register to the stack
    /// </summary>
    PUSH_REG = 0x16,

    /// <summary>
    /// Push a value from memory (heap) to the stack
    /// </summary>
    PUSH_MEM = 0x17,

    /// <summary>
    /// Delete top literal at the stack, copy it to the specified register
    /// </summary>
    POP_REG = 0x18,

    /// <summary>
    /// Delete top literl at the stack, copy it to the specifed memory address
    /// </summary>
    POP_MEM = 0x19,

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

// File scoped namespace
namespace VMCore;

using System;
using System.Linq;

/// <summary>
/// CPU emulator component
/// </summary>
public class CPU
{
    #region Public properties

    /// <summary>
    /// Computing memory
    /// </summary>
    public MemoryMapper Memory { get; set; }

    /// <summary>
    /// CPU registers
    /// </summary>
    public byte[] Registers { get; set; }

    #endregion

    #region Private members

    /// <summary>
    /// CPU register names
    /// </summary>
    public string[] RegisterNames { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Creates a new instance of the <see cref="CPU"/> class
    /// </summary>
    /// <param name="memory">Computing memory</param>
    public CPU(MemoryMapper memory)
    {
        // Save computing memory (RAM)
        this.Memory = memory;

        // Setup register names
        this.RegisterNames = new string[]
        {
            "ip",  // Instruction pointer 
            "acr", // Accumilator register
            "r1", "r2", "r3", "r4" // General purpose registers
        };

        // Setup registers for this 16 bit processor
        // We need to have 2 bytes per register
        this.Registers = new byte[RegisterNames.Length * 2];
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Reads a 16 bit from the specified register
    /// </summary>
    /// <param name="registerName">The name of the register</param>
    /// <returns>The fetched byte</returns>
    public ushort GetRegister(string registerName)
    {
        // Check if register exists
        if (!RegisterNames.Contains(registerName)) { throw new Exception("Invalid CPU register specified"); } // Reading to an invalid register

        // Get 16 bit register value
        return this.Registers.GetUInt16(Array.IndexOf(this.RegisterNames, registerName) * 2);
    }

    /// <summary>
    /// Reads a 16 bit from the specified register
    /// </summary>
    /// <param name="registerName">The name of the register</param>
    /// <returns>The fetched byte</returns>
    public ushort GetRegister(int registerAddress)
    {
        // Check if register exists
        if (this.Registers.Length < registerAddress) { throw new Exception("Invalid CPU register specified"); } // Reading to an invalid register

        // Get 16 bit register value
        return this.Registers.GetUInt16(registerAddress);
    }

    /// <summary>
    /// Writes a 16 bit value in the specified register
    /// </summary>
    /// <param name="registerName">The register name</param>
    /// <param name="value">The byte to write to the specified register</param>
    /// <returns>The byte that was written to the register</returns>
    public ushort SetRegister(string registerName, ushort value)
    {
        // Check if register exists
        if (!RegisterNames.Contains(registerName)) { throw new Exception("Invalid CPU register specified"); } // Writing to an invalid register
        // Save and return the 16 bit value in the provided register
        return this.Registers.SetUInt16(Array.IndexOf(this.RegisterNames, registerName) * 2, value);
    }

    /// <summary>
    /// Writes a 16 bit value in the specified register
    /// </summary>
    /// <param name="registerAddress">The register address</param>
    /// <param name="value">The byte to write to the specified register</param>
    /// <returns>The byte that was written to the register</returns>
    public ushort SetRegister(int registerAddress, ushort value)
    {
        // Check if register exists
        if (this.Registers.Length < registerAddress) { throw new Exception("Invalid CPU register specified"); } // Writing to an invalid register
        // Save and return the 16 bit value in the provided register
        return this.Registers.SetUInt16(registerAddress, value);
    }

    /// <summary>
    /// Fetches the next 8 bits from memory
    /// </summary>
    /// <returns></returns>
    public ushort Fetch8()
    {
        // Get address to the next instruction
        var nextInstructionAddress = GetRegister("ip");
        // CPU instruction will always be a maximum of 8 bits
        var instruction = this.Memory.GetUInt8(nextInstructionAddress);
        // Save address of the next instruction in ip register
        SetRegister("ip", (ushort)(nextInstructionAddress + 1));
        // Return the fetched instruction
        return instruction;
    }
    /// <summary>
    /// Fetches the next 16 bits from memory
    /// </summary>
    /// <returns></returns>
    public ushort Fetch16()
    {
        // Get address to the next instruction
        var nextInstructionAddress = GetRegister("ip");
        // CPU instruction will always be a maximum of 8 bits
        var instruction = this.Memory.GetUInt16(nextInstructionAddress);
        // Save address of the next instruction in ip register
        SetRegister("ip", (ushort)(nextInstructionAddress + 2));
        // Return the fetched instruction
        return instruction;
    }

    /// <summary>
    /// Executes the next CPU instruciton
    /// </summary>
    public void Execute(ushort instruction)
    {
        // Check the instruction
        switch ((Instruction)instruction)
        {
            // Add instruction
            case Instruction.ADD:
                {
                    // Fetch register where first value is stored 
                    var reg1 = this.Fetch16();
                    // Fetch register where second value is stored 
                    var reg2 = this.Fetch16();

                    // Get literal at first register
                    var lit1 = this.GetRegister(reg1);
                    // Get literal at second register
                    var lit2 = this.GetRegister(reg2);

                    // Add the 2 values and store them in the accumilator register
                    this.SetRegister("acr", (ushort)(lit1 + lit2));
                }
                break;

            // Move instruction (value to register)
            case Instruction.MOV_LIT_REG:
                {
                    // Fetch the value from memory
                    var literal = this.Fetch16();
                    // Fetch the registrer from memory
                    var reg = this.Fetch16();
                    // Save the literal value into the specified register
                    this.SetRegister(reg, literal);
                }
                break;

            // Move instruction (register to register)
            case Instruction.MOV_REG_REG:
                {
                    // Fetch register where first value is stored 
                    var reg1 = this.Fetch16();
                    // Fetch register where second value is stored 
                    var reg2 = this.Fetch16();
                    // Get literal at the first register
                    var lit = this.GetRegister(reg1);
                    // Save the literal into the second register
                    this.SetRegister(reg2, lit);
                }
                break;

            // Move instruction (memory to memory)
            case Instruction.MOV_MEM_MEM:
                {
                    // Fetch memory address where value 1 is stored
                    var mem1 = this.Fetch16();
                    // Fetch memory address where we will store value from address 1
                    var mem2 = this.Fetch16();
                    // Fetch value in memory address 1
                    var val = this.Memory.GetUInt16(mem1);
                    // Save value in memory address 2
                    this.Memory.SetUInt16(mem2, val);
                }
                break;

            // Move instruction (register to memory)
            case Instruction.MOV_REG_MEM:
                {
                    // Fetch the registry address
                    var reg = this.Fetch16();
                    // Fetch the memory address
                    var mem = this.Fetch16();
                    // Fetch value from register
                    var lit = this.GetRegister(reg);
                    // Store value in memory
                    this.Memory.SetUInt16(mem, lit);
                }
                break;

            // Move instruction (memory to register)
            case Instruction.MOV_MEM_REG:
                {
                    // Fetch the memory address
                    var mem = this.Fetch16();
                    // Fetch the registry address
                    var reg = this.Fetch16();
                    // Get value from memory
                    var lit = this.Memory.GetUInt16(mem);
                    // Save value in register
                    this.SetRegister(reg, lit);
                }
                break;

            // No operation instruction
            case Instruction.NOP:
                break;

            // Invalid cpu instrction was read
            default: throw new Exception("Invalid CPU instruction");
        }
    }

    /// <summary>
    /// Represents 1 CPU cycle
    /// </summary>
    public void Step()
    {
        // Fetch the next instruction (Always 8 bits)
        var instruction = this.Fetch8();
        // Execute instruciton
        Execute(instruction);
    }

    #endregion
}
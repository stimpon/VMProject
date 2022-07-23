// File namespace
namespace VMTester;

// Required namespaces
using System;
using System.Text;
using Spectre.Console;
using VMCore;

/// <summary>
/// Main program class
/// </summary>
public class Program
{
    /// <summary>
    /// Main entry point of the program
    /// </summary>
    /// <param name="Args"></param>
    public static void Main(string[] Args)
    {
        var memory = File.ReadAllText(Environment.CurrentDirectory + "/Byte code/stack_test")
            .Split(new String[] { "\r\n", " ", "\n", "\r" }, StringSplitOptions.None)
            .Where(b => !String.IsNullOrEmpty(b))
            .Select(b => Convert.ToByte(b, 16))
            .ToArray();

        // Setup memory mapper
        var mapper = new MemoryMapper(0xAA);
        // Connect virtual monitor
        //mapper.Map(new Monitor(), 0x1000, 0x1400);
        // Create a CPU and provide programmed memory
        var cpu = new CPU(mapper);
        // Load byte code
        cpu.Memory.LoadByteCode(memory, 0x00);

        cpu.Step();
        cpu.Step();
        cpu.Step();
        cpu.Step();
        cpu.Step();
        cpu.Step();

        PrintRegistersReadable(cpu);
        MemDump(cpu);
    }

    /// <summary>
    /// Prints all registers in the specified cpu
    /// </summary>
    /// <param name="cpu"></param>
    public static void PrintRegisters(CPU cpu)
    {
        // Create spectre table
        var table = new Table();

        // Add the 2 header columns
        table.AddColumn(new("[Green]Address[/]"));
        table.AddColumn(new("[Green]Value[/]"));

        // Loop through all cpu registers
        for(int i = 0; i < cpu.Registers.Length; i++)
        {
            // Add the current register to the table
            table.AddRow($"0x{i.ToString("X")}", $"0x{cpu.Registers[i].ToString("X2")}");
        }

        // Print the table
        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Prints a more radable table of the cpu's registers
    /// </summary>
    /// <param name="cpu"></param>
    public static void PrintRegistersReadable(CPU cpu)
    {
        // Create spectre table
        var table = new Table();

        // Add the 2 header columns
        table.AddColumn(new("[SandyBrown]Register[/]"));
        table.AddColumn(new("[Green]Address[/]"));
        table.AddColumn(new("[Green]Hex value[/]"));
        table.AddColumn(new("[Green]Decimal value[/]"));

        // Loop through all registers
        foreach(var address in cpu.RegisterNames)
        {
            table.AddRow(address, 
                $"0x{Array.IndexOf(cpu.RegisterNames, address) * 2}", 
                $"0x{cpu.GetRegister(address):X4}",
                cpu.GetRegister(address).ToString());
        }

        // Print the table
        AnsiConsole.Write(table);
    }

    public static void MemDump(CPU cpu)
    {
        // Create spectre table
        var table = new Table();

        // Add the 2 header columns
        table.AddColumn(new("[Green]Address[/]"));
        table.AddColumn(new("[Green]Value[/]"));

        // Loop through all cpu registers
        for (int i = 0; i < cpu.Memory.Memory.Length; i++)
        {
            // Add the current register to the table
            table.AddRow($"0x{i.ToString("X")}", $"0x{cpu.Memory.Memory[i].ToString("X2")}");
        }

        // Print the table
        AnsiConsole.Write(table);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HedgeLib.Exceptions;
using HedgeLib.IO;

namespace STSCTool
{
    public class STSCFile : FileBase
    {
        public string ScriptName { get; set; }
        public uint ScriptID = 0; // Used to store script information
        public List<STSCInstructions.Instruction> Instructions = new List<STSCInstructions.Instruction>();

        public int ManualCount = 0;

        public override void Load(Stream fileStream)
        {
            var reader = new ExtendedBinaryReader(fileStream, Encoding.UTF8);
            string sig = reader.ReadSignature();
            if (sig != "STSC")
                throw new InvalidSignatureException("STSC", sig);
            uint headerSize = reader.ReadUInt32();
            uint version = reader.ReadUInt32();
            if (version != 7)
            {
                Console.WriteLine("WARNING: Script version is not supported! Got {0}, expected {1}. Continuing with the wrong version will almost always fail.", version, 7);
                Console.ReadKey(true);
            }
            ScriptName = reader.ReadNullTerminatedString();
            reader.JumpAhead((uint)(0x20 - (ScriptName.Length + 1)));
            reader.JumpAhead(12);
            ScriptID = reader.ReadUInt32();
            // Stupid workaround to find the end of the code segment
            reader.Offset = (uint)reader.BaseStream.Length;
            while (true)
            {
                byte opcode = reader.ReadByte();

                if (opcode >= 0x90)
                {
                    Console.WriteLine("Error: Attempted to disassemble Instruction at {1:X} but got {0:X2}.", opcode, (int)reader.BaseStream.Position - 1);
                    Console.WriteLine("This usually means the Script is corrupt or one or more STSCFile's definitions are incorrect.");
                    Console.WriteLine("Disassembler must abort now!");
                    Console.ReadKey(true);
                    return;
                }

                // Check if its a known instruction
                if (STSCInstructions.Instructions[opcode] == null)
                {
                    Console.WriteLine("Error: Instruction {0:X2} at {1:X} is unknown!", opcode, (int)reader.BaseStream.Position - 1);
                    Console.WriteLine("This usually means STSCFile does not yet know the parameters on the instruction.");
                    Console.WriteLine("Disassembler must abort now!");
                    Console.ReadKey(true);
                    return;
                }

                try
                {
                    var instruction = STSCInstructions.Instructions[opcode].Read(reader);
                    Instructions.Add(instruction);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: Failed to Read instruction {0:X2} at {1:X}!", opcode, (int)reader.BaseStream.Position - 1);
                    Console.WriteLine("This usually means the Script is corrupt or one or more STSCFile's definitions are incorrect.");
                    Console.WriteLine("Please check the output file for finding out what instruction went wrong.");
                    Console.WriteLine("Disassembler must abort now! Exception: {0}", e);
                    Console.ReadKey(true);
                    return;
                }
                if (reader.BaseStream.Position >= reader.Offset)
                    break;
            }
        }

        public override void Save(Stream fileStream)
        {
            ExtendedBinaryWriter writer = new ExtendedBinaryWriter(fileStream, Encoding.UTF8);
            var strings = new List<string>();
            writer.WriteSignature("STSC");
            writer.AddOffset("EntryPosition");
            writer.Write(0x07); // Version
            writer.WriteSignature(ScriptName);
            writer.WriteNulls((uint) (0x20 - ScriptName.Length)); // Pad Script Name
            writer.Write(0x000507E3);
            writer.Write((short) 0x09);
            writer.Write((short) 0x0D);
            writer.Write((short) 0x19);
            writer.Write((short) 0x0D);
            writer.Write(ScriptID);
            writer.FillInOffset("EntryPosition");
            foreach (var instruction in Instructions)
            {
                writer.Write((byte)STSCInstructions.Instructions.FindIndex(t => t?.Name == instruction.Name));
                instruction.Write(writer, ref ManualCount, strings);
            }
            // Write String Table
            for (int i = 0; i < strings.Count; ++i)
            {
                writer.FillInOffset($"Strings_{i}");
                writer.WriteNullTerminatedString(strings[i]);
            }
            writer.FixPadding(0x10);
        }

        public override string ToString()
        {
            return ScriptName;
        }
    }
}

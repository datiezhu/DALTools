﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HedgeLib.IO;
using static STSCTool.STSCInstructions.ArgumentType;

namespace STSCTool
{
    public static class STSCInstructions
    {

        public static List<Instruction> Instructions = new List<Instruction>()
        {
            new Instruction("NOP", null),
            new Instruction("Exit", null),
            new Instruction("continue", null),
            new Instruction("Endv", null),
            null, // InfinitWait
            new Instruction("Wait", new []{ AT_Int32 }),
            new Instruction("Goto", new []{ AT_CodePointer }),
            new Instruction("return", null),
            null, // ReturnPush
            null, // ReturnPop
            null, // Call_iv
            new Instruction("SubStart", new []{ AT_Byte, AT_CodePointer }),
            new Instruction("SubEnd", new []{ AT_Byte }),
            new Instruction("RandJump", new []{ AT_PointerArray }),
            new Instruction("Printf", new []{ AT_String }),
            new Instruction("FileJump", new []{ AT_String }),

            null, // FlgOnJump
            null, // FlgOffJump
            new Instruction("FlagSet", new []{ AT_Int16, AT_Int16, AT_Byte }),
            null, // PrmTrueJump
            null, // PrmFalseJump
            new Instruction("PrmSet", new []{ AT_Int32, AT_Int32 }), 
            new Instruction("PrmCopy", new []{ AT_Int32, AT_Int32 }), 
            new Instruction("PrmAdd", new []{ AT_Int32, AT_Int32 }), 
            null, // PrmAddWk
            null, // PrmBranch
            new Instruction("Call", new []{ AT_CodePointer }),
            new Instruction("CallReturn", null),
            null, // SubEndWait
            new InstructionIf(),
            new InstructionSwitch("switch", null),
            null, // PrmRand

            new Instruction("DataBaseParam", new []{ AT_Byte, AT_ManualBinary }),
            new Instruction("NewGameOpen", null),
            new Instruction("EventStartMes", new []{ AT_String }),
            new Instruction("Dummy23", null),
            new Instruction("Dummy24", null),
            new Instruction("Dummy25", null),
            new Instruction("Dummy26", null),
            new Instruction("Dummy27", null),
            new Instruction("Dummy28", null),
            new Instruction("Dummy29", null),
            new Instruction("Dummy2A", null),
            new Instruction("Dummy2B", null),
            new Instruction("Dummy2C", null),
            new Instruction("Dummy2D", null),
            new Instruction("Dummy2E", null),
            null, // SetEventNumber

            new Instruction("PlayMovie", new []{ AT_String, AT_Int16, AT_Byte }),
            new Instruction("BgmWait", new []{ AT_Byte, AT_Int16 }),
            new Instruction("BgmVolume", new []{ AT_Int32, AT_Int16 }),
            new Instruction("SePlay", new []{ AT_Byte, AT_Byte, AT_Bool }),
            new Instruction("SeStop", new []{ AT_Byte, AT_Byte }),
            null, // SeWait 
            new Instruction("SeVolume", new []{ AT_Int16, AT_Int32, AT_Int16 }),
            new Instruction("SeAllStop", null),
            new Instruction("BgmDummy", new []{ AT_Int32, AT_Int16 }), // Data is discarded
            new Instruction("Dummy39", null),
            new Instruction("Dummy3A", null),
            new Instruction("Dummy3B", null),
            new Instruction("Dummy3C", null),
            new Instruction("Dummy3D", null),
            new Instruction("Dummy3E", null),
            new Instruction("Dummy3F", null),

            new Instruction("SetNowLoading", new []{ AT_Bool }),
            new Instruction("Fade", new []{ AT_Byte, AT_Int16, AT_Int16 }),
            new Instruction("PatternFade", new []{ AT_Int16, AT_Int16, AT_Int16 }),
            new Instruction("Quake", new []{ AT_Byte, AT_Int16}),
            new Instruction("CrossFade", new []{ AT_Int16}),
            new Instruction("PatternCrossFade", new []{ AT_Int16, AT_Int16 }),
            new Instruction("DispTone", new []{ AT_Byte }),
            new Instruction("Dummy47", null),
            new Instruction("Dummy48", null),
            new Instruction("Dummy49", null),
            new Instruction("Dummy4A", null),
            new Instruction("Dummy4B", null),
            new Instruction("Dummy4C", null),
            new Instruction("Dummy4D", null),
            new Instruction("Dummy4E", null),
            new Instruction("Wait2", new []{ AT_Int32 }),

            new Instruction("Mes", new []{ AT_Byte, AT_Bool, AT_Byte, AT_Bool, AT_String, AT_Int16 }),
            new Instruction("MesWait", null),
            new Instruction("MesTitle", new []{ AT_Byte }),
            new Instruction("SetChoice", new []{ AT_CodePointer, AT_String, AT_Int16 }),
            new Instruction("ShowChoices", new []{ AT_Bool }),
            new Instruction("SetFontSize", new []{ AT_Byte }),
            new Instruction("MapPlace", new []{ AT_Int16, AT_Int32, AT_CodePointer }),
            new Instruction("MapChara", new []{ AT_Int16, AT_Int16, AT_Byte }),
            new Instruction("MapBg", new []{ AT_Byte }), // mapBg{}.tex
            new Instruction("MapCoord", new []{ AT_Int16, AT_Byte, AT_Bool, AT_Int16, AT_Int16 }),
            new Instruction("MapStart", null),
            new Instruction("MapInit", null),
            new Instruction("MesWinClose", null),
            new Instruction("BgOpen", new []{ AT_Int32, AT_Int32}), // MesWinOpen?
            new Instruction("BgClose", new []{ AT_Byte}),
            new Instruction("MaAnime", new []{ AT_Byte}),

            new Instruction("BgMove", new []{ AT_Byte, AT_Int32, AT_Int16}),
            new Instruction("BgScale", new []{ AT_Float, AT_Int16, AT_Byte, AT_Bool}),
            new Instruction("BustOpen", new []{ AT_Byte, AT_Int32, AT_Int32}),
            new Instruction("BustClose", new []{ AT_Byte, AT_Int16}),
            new Instruction("BustMove", new []{ AT_Byte, AT_Int16, AT_Int16, AT_Int16, AT_Byte}),
            new Instruction("BustMoveAdd", new []{ AT_Byte, AT_Int16, AT_Int16, AT_Int16, AT_Byte}),
            new Instruction("BustScale", new []{ AT_Byte, AT_Float, AT_Int16, AT_Byte, AT_Byte}),
            new Instruction("BustPriority", new []{ AT_Byte, AT_Byte}),
            new Instruction("PlayVoice", new []{ AT_Byte, AT_Int32, AT_String }), 
            new Instruction("VoiceCharaDraw", new []{ AT_Int16 }),
            new Instruction("DateSet", new []{ AT_Byte, AT_Byte, AT_Byte }),
            new Instruction("TellOpen", new []{ AT_Byte, AT_Int32, AT_Int16 }),
            new Instruction("TellClose", new []{ AT_Byte, AT_Int16 }),
            new Instruction("Trophy", new []{ AT_Byte }),
            new Instruction("SetVibration", new []{ AT_Byte, AT_Float }), // NOTE: It's "Vibraiton" in-game, did they misspell vibration?
            new Instruction("BustQuake", new []{ AT_Byte, AT_Byte, AT_Int16}),
            
            new Instruction("BustFade", new []{ AT_Byte, AT_Float, AT_Float, AT_Int16}),
            new Instruction("BustCrossMove", null), // TODO
            new Instruction("BustTone", new []{ AT_Byte, AT_Byte}), // TODO
            new Instruction("BustAnime", new []{ AT_Byte, AT_Byte}), // TODO
            null, // CameraMoveXY
            null, // CameraMoveZ
            new Instruction("CameraMoveXYZ", new []{ AT_Int16, AT_Int16, AT_Float, AT_Int16, AT_Int16}), // TODO
            new Instruction("ScaleMode", new []{ AT_Byte}), // TODO
            new Instruction("GetBgNo", new []{ AT_Int32}), // TODO
            new Instruction("GetFadeState", new []{ AT_Int32}), // TODO
            new Instruction("SetAmbiguous", new []{ AT_Float, AT_Byte, AT_Bool }),
            new Instruction("AmbiguousPowerFade", new []{ AT_Float, AT_Float, AT_Int16 }),
            new Instruction("SetBlur", new []{ AT_Int32, AT_Bool }),
            new Instruction("BlurPowerFade", new []{ AT_Float, AT_Float, AT_Int16}),
            new Instruction("EnableMonologue", new [] { AT_Bool }), // TODO
            new Instruction("SetMirage", new [] { AT_Float, AT_Bool }), // TODO
            
            new Instruction("MiragePowerFade", new [] { AT_Int32, AT_Float, AT_Int16 }), // TODO
            new Instruction("MessageVoiceWait", new [] { AT_Byte }), // TODO
            new Instruction("SetRasterScroll", new [] { AT_Byte, AT_Float, AT_Int16, AT_Byte }),
            new Instruction("RasterScrollPowerFade", new [] { AT_Int32, AT_Float, AT_Int16 }),
            new Instruction("MesDel", null),
            new Instruction("MemoryOn", new [] { AT_Int16 }),
            new Instruction("SaveDateSet", new []{ AT_Byte, AT_String }),
            new Instruction("ExiPlay", new []{ AT_Int16, AT_Byte, AT_Byte, AT_Byte, AT_Byte, AT_Byte, AT_Int32, AT_Byte }), // TODO
            new Instruction("ExiStop", new []{ AT_Int16, AT_Byte }), // TODO
            new Instruction("GalleryFlg", new []{ AT_Int16, AT_Int16 }),
            new Instruction("DateChange", new []{ AT_Int32, AT_Int16 }),
            new Instruction("BustSpeed", new []{ AT_Byte, AT_Int32 }),
            new Instruction("DateRestNumber", new []{ AT_Byte }),
            new Instruction("MapTutorial", new []{ AT_Int16, AT_Int16 }),
            new Instruction("Ending", new []{ AT_Byte }),
            new Instruction("Set/Del+FixAuto", new []{ AT_Byte, AT_Byte, AT_Byte }),
            new Instruction("ExiLoopStop", new []{ AT_Int16, AT_Byte }),
            new Instruction("ExiEndWait", new []{ AT_Int16, AT_Byte }), // TODO
            new Instruction("Set/Del+EventKeyNg", new []{ AT_Byte }),
        };

        public enum ArgumentType
        {
            AT_Bool, AT_Byte, AT_Int16, AT_Int32, AT_Float, AT_String, AT_StringPtr, AT_CodePointer, AT_DataReference, AT_PointerArray, AT_ManualBinary
        }

        public class Instruction
        {
            public string Name;
            public ArgumentType[] ArgTypes;
            public List<object> Arguments = new List<object>();

            public Instruction(string name, ArgumentType[] arguments)
            {
                Name = name;
                ArgTypes = arguments;
            }

            public virtual T GetArgument<T>(int index)
            {
                return (T) Convert.ChangeType(Arguments[index], typeof(T));
            }

            public virtual Instruction Read(ExtendedBinaryReader reader)
            {
                var instruction = new Instruction(Name, ArgTypes);
                if (ArgTypes == null)
                    return instruction;
                for (int i = 0; i < ArgTypes.Length; ++i)
                {
                    if (ArgTypes[i] == AT_PointerArray)
                    {
                        int[] pointers = new int[reader.ReadByte()];
                        for (int i2 = 0; i2 < pointers.Length; ++i2)
                            pointers[i] = reader.ReadInt32();
                        Arguments.Add(pointers);
                    }
                    else
                        instruction.Arguments.Add(ReadByType(reader, ArgTypes[i]));
                }

                return instruction;
            }

            public virtual void Write(ExtendedBinaryWriter writer, ref int manualCount, List<string> strings = null)
            {
                if (ArgTypes == null)
                    return;
                for (int i = 0; i < ArgTypes.Length; ++i)
                {
                    if (ArgTypes[i] == AT_PointerArray)
                    {
                        var pointers = GetArgument<int[]>(i);
                        writer.Write((byte)pointers.Length);
                        foreach (var pointer in pointers)
                            writer.Write(pointer);
                    }
                    else
                        WriteByType(writer, ArgTypes[i], Arguments[i], ref manualCount, strings);
                }
            }

            public virtual int GetInstructionSize()
            {
                int size = 1;
                if (ArgTypes == null)
                    return size;
                foreach (var arg in ArgTypes)
                    switch (arg)
                    {
                        case AT_Bool:
                        case AT_Byte:
                            size += 1;
                            break;
                        case AT_Int16:
                            size += 2;
                            break;
                        case AT_Int32:
                        case AT_CodePointer:
                        case AT_DataReference:
                        case AT_Float:
                        case AT_String:
                        case AT_ManualBinary:
                            size += 4;
                            break;
                        case AT_PointerArray:
                            size += GetArgument<int[]>(0).Length * 4 + 1;
                            break;
                    }

                return size;
            }

            public static object ReadByType(ExtendedBinaryReader reader, ArgumentType type)
            {
                switch (type)
                {
                    case AT_Bool:
                        return reader.ReadBoolean();
                    case AT_Byte:
                        return reader.ReadByte();
                    case AT_Int16:
                        return reader.ReadInt16();
                    case AT_Int32:
                        return reader.ReadInt32();
                    case AT_Float:
                        return reader.ReadSingle();
                    case AT_String:
                        return reader.ReadStringElsewhere();
                    case AT_StringPtr:
                        return reader.ReadStringPointer();
                    case AT_CodePointer:
                        return reader.ReadInt32();
                    case AT_DataReference:
                        return reader.ReadUInt32();
                    case AT_ManualBinary:
                        return reader.ReadArrayRange(reader.ReadInt32(), reader.ReadInt32());
                    default:
                        return null;
                }
            }

            public static void WriteByType(ExtendedBinaryWriter writer, ArgumentType type, object value, ref int manualCount, List<string> strings = null)
            {
                switch (type)
                {
                    case AT_Bool:
                        writer.Write((bool)value);
                        break;
                    case AT_Byte:
                        writer.Write((byte)value);
                        break;
                    case AT_Int16:
                        writer.Write((short)value);
                        break;
                    case AT_Int32:
                        writer.Write((int)value);
                        break;
                    case AT_Float:
                        writer.Write((float)value);
                        break;
                    case AT_String:
                        if (strings == null)
                            break;
                        writer.AddOffset($"Strings_{strings.Count}");
                        strings.Add((string)value);
                        break;
                    case AT_StringPtr:
                        if (strings == null)
                            break;
                        writer.AddOffset($"StringsPtr_{strings.Count}");
                        strings.Add((string)value);
                        break;
                    case AT_CodePointer:
                        writer.Write((int)value);
                        break;
                    case AT_DataReference:
                        writer.Write((int)value);
                        break;
                    case AT_ManualBinary:
                        writer.AddOffset($"Manual_Ptr_{manualCount}l");
                        writer.AddOffset($"Manual_Ptr_{manualCount}h");
                        ++manualCount;
                        break;
                }
            }
        }

        public static string ReadStringElsewhere(this ExtendedBinaryReader reader, int position = 0, bool absolute = true)
        {
            long oldPos = reader.BaseStream.Position;
            try
            {
                if (position == 0)
                    reader.JumpTo(reader.ReadInt32(), absolute);
                else
                    reader.JumpTo(position, absolute);
                if (reader.Offset > reader.BaseStream.Position)
                    reader.Offset = (uint) reader.BaseStream.Position;
                string s = reader.ReadNullTerminatedString();
                if (position == 0)
                    reader.JumpTo(oldPos + 4);
                else
                    reader.JumpTo(oldPos);
                return s;
            }
            catch
            {
                reader.JumpTo(oldPos);
                return "";
            }
        }

        public static string ReadStringPointer(this ExtendedBinaryReader reader)
        {
            long oldPos = reader.BaseStream.Position;
            try
            {
                reader.JumpTo(reader.ReadInt32());
                reader.JumpTo(reader.ReadInt32());
                if (reader.Offset > reader.BaseStream.Position)
                    reader.Offset = (uint)reader.BaseStream.Position;
                string s = reader.ReadNullTerminatedString();
                reader.JumpTo(oldPos + 4);
                return s;
            }
            catch
            {
                reader.JumpTo(oldPos);
                return "";
            }
        }

        public static byte[] ReadArrayRange(this ExtendedBinaryReader reader, int start, int end)
        {
            long oldPos = reader.BaseStream.Position;
            try
            {
                reader.JumpTo(start);
                if (reader.Offset > reader.BaseStream.Position)
                    reader.Offset = (uint)reader.BaseStream.Position;
                int length = end - start;
                byte[] bytes = reader.ReadBytes(length);
                reader.JumpTo(oldPos);
                return bytes;
            }
            catch
            {
                reader.JumpTo(oldPos);
                return new byte[0] { };
            }
        }


        public class InstructionIf : Instruction
        {
            public class Comparison
            {
                public enum ComparisonOperators
                {
                    Equals, NotEquals, GreaterThenOrEqual, GreaterThan, LessThenOrEqual, LessThen, NotZero, Zero
                }

                public Comparison(uint left, ComparisonOperators oper, uint right)
                {
                    Left = left;
                    Operator = oper;
                    Right = right;
                }

                public ComparisonOperators Operator;
                public uint Left;
                public uint Right;
            }
            public List<Comparison> Comparisons = new List<Comparison>();

            public InstructionIf() : base("if", new [] {AT_CodePointer})
            {

            }

            public override Instruction Read(ExtendedBinaryReader reader)
            {
                var instruction = new InstructionIf();
                short amount = reader.ReadInt16();
                instruction.Arguments.Add(reader.ReadInt32());
                for (int i = 0; i < amount; ++i)
                {
                    uint left = reader.ReadUInt32();
                    uint right = reader.ReadUInt32();
                    var compareOp = (Comparison.ComparisonOperators)reader.ReadByte();
                    instruction.Comparisons.Add(new Comparison(left, compareOp, right));
                }
                return instruction;
            }

            public override void Write(ExtendedBinaryWriter writer, ref int manualCount, List<string> strings = null)
            {
                writer.Write((short)Comparisons.Count);
                writer.Write(GetArgument<int>(0));
                foreach (var comparison in Comparisons)
                {
                    writer.Write((uint)comparison.Left);
                    writer.Write((uint)comparison.Right);
                    writer.Write((byte)comparison.Operator);
                }
            }

            public override int GetInstructionSize()
            {
                int size = 1;
                size += 2;
                size += 4;
                size += Comparisons.Count * 9;

                return size;
            }
        }
        public class InstructionSwitch : Instruction
        {
            public InstructionSwitch(string name, ArgumentType[] arguments) : base(name, arguments)
            {
            }

            public override Instruction Read(ExtendedBinaryReader reader)
            {
                var instruction = new InstructionSwitch(Name, ArgTypes);
                uint unknown = reader.ReadUInt32();
                ushort amount = reader.ReadUInt16();
                bool endFlag = amount >> 15 == 1; 
                if (endFlag)
                    amount &= 0x7FF;

                instruction.ArgTypes = new ArgumentType[amount * 2 + 3];
                instruction.ArgTypes[0] = AT_DataReference;
                instruction.Arguments.Add(unknown);
                instruction.ArgTypes[1] = AT_Int16;
                instruction.Arguments.Add(amount);
                instruction.ArgTypes[2] = AT_Bool;
                instruction.Arguments.Add(endFlag);
                for (int i = 0; i < amount; ++i)
                {
                    // case
                    instruction.ArgTypes[i * 2 + 3 + 0] = AT_Int32;
                    instruction.Arguments.Add(reader.ReadInt32());
                    // location
                    instruction.ArgTypes[i * 2 + 3 + 1] = AT_CodePointer;
                    instruction.Arguments.Add(reader.ReadInt32());
                }
                return instruction;
            }

            public override void Write(ExtendedBinaryWriter writer, ref int manualCount, List<string> strings = null)
            {
                writer.Write(GetArgument<uint>(0));
                writer.Write((ushort)(GetArgument<ushort>(1) | GetArgument<ushort>(2) << 15));
                for (int i = 0; i < GetArgument<ushort>(1); ++i)
                {
                    // case
                    writer.Write(GetArgument<int>(i * 2 + 3 + 0));
                    // location
                    writer.Write(GetArgument<int>(i * 2 + 3 + 1));
                }
            }

            public override int GetInstructionSize()
            {
                // Cheap way to ignore the flag
                return base.GetInstructionSize() - 1;
            }
        }
    }
}

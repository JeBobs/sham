using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using static Sham.UserInterface;

namespace Sham.Burnout
{
    public class BinaryMAP
    {
        public FunctionData[] Data;

        public struct FunctionData
        {
            public byte[] Header;
            public byte[] Function;
        }

        public static BinaryMAP ParseMAP(string path)
        {
            if (!path.ToLower().Contains("bin"))
            {
                PrintLine("File does not appear to be a bin file! Other formats may be supported at a later date, sorry.");
                return null;
            }
            else
            {
                PrintTask("Parsing " + path);

                FileStream h = File.OpenRead(path);
                List<FunctionData> functionDatas = new List<FunctionData>();

                TryPrintDebug("File length is " + (int)h.Length, 3);

                for (int offset = 0; offset <= (int)h.Length; offset += 128)
                {
                    byte[] RawFunctionBuffer = new byte[128];
                    FunctionData FunctionBuffer = new FunctionData
                    {
                        Header = new byte[16],
                        Function = new byte[112]
                    };

                    int DebugBytesRead = h.Read(RawFunctionBuffer, 0, 128);
                    TryPrintDebug("Read " + DebugBytesRead + " into the raw function buffer.", 3);

                    Buffer.BlockCopy(RawFunctionBuffer, 0, FunctionBuffer.Function, 0, 16);
                    TryPrintDebug("Function header is " + Encoding.UTF8.GetString(FunctionBuffer.Function), 3);

                    Buffer.BlockCopy(RawFunctionBuffer, 16, FunctionBuffer.Function, 0, 112);
                    TryPrintDebug("Function is " + Encoding.UTF8.GetString(FunctionBuffer.Function), 2);

                    functionDatas.Add(FunctionBuffer);
                    TryPrintDebug("Added function data to buffer.", 1);

                    RawFunctionBuffer = null;
                }

                BinaryMAP map = new BinaryMAP
                {
                    Data = functionDatas.ToArray()
                };

                PrintTask("Parsed " + path, true);
                return map;
            }
        }
    }
}

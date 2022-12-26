using System;
using System.IO;

using static Sham.UserInterface;

namespace Sham.Halo
{
    internal class ShaderIncludeConverter
    {
        public static byte[] ParseHLSLTagToBytes(string path)
        {
            if (!path.ToLower().Contains("hlsl_include"))
                goto INVALID_FILE;


            // TODO: Redo the endianness conversions with BrnDataHandler's AssetEndianConverter
            // TODO: EVEN MORE IMPORTANT - WRITE A PROPER TAG DESERIALIZER!! WE'RE NOT IN KINDERGARTEN ANYMORE
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                byte[] bytes = new byte[4];

                // Seek and read tag type

                TryPrintDebug("Seeking to offset 48: " + stream.Seek(48, SeekOrigin.Begin), 4);
                
                if (stream.Read(bytes, 0, 4) != 4)
                    goto INVALID_FILE;

                var currentTagHex = BitConverter.ToString(bytes).Replace("-", string.Empty);

                TryPrintDebug("Current Tag Hex: " + currentTagHex, 4);

                // Ensure we have the correct lslh (LE) or hlsl (BE) HLSL tag header
                if (currentTagHex != "6C736C68" && currentTagHex != "686C736C")
                    goto INVALID_FILE;

                // 23C / 572 has the length of the include
                TryPrintDebug("Seeking to offset 572: " + stream.Seek(572, SeekOrigin.Begin), 4);

                if (stream.Read(bytes, 0, 4) != 4)
                    goto INVALID_FILE;

                int length = BitConverter.ToInt32(bytes, 0);

                TryPrintDebug("Tag block contents length: " + length , 2);

                //TryPrintDebug("Seeking to offset 572 + 8: " + stream.Seek(8, SeekOrigin.Current), 4);

                bytes = new byte[length];

                TryPrintDebug("Attempting to read " + length + " bytes, read " + stream.Read(bytes, 0, length) + " bytes", 2);

                return bytes;
            }

            INVALID_FILE:
                PrintLine("File does not appear to be a hlsl_include file! Skipping.");
                return new byte[0];
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using static Sham.UserInterface;

namespace Sham.Halo
{
    internal class ShaderIncludeConverter
    {
        public static byte[] ConvertTagIncludeToText(string path)
        {
            if (!path.ToLower().Contains("hlsl_include"))
            {
                PrintLine("File does not appear to be a hlsl_include file! skipping.");
                return new byte[0];
            }

            byte[] inputFile = File.ReadAllBytes(path);

            if (inputFile.Length - 576 - 853 < 0) return new byte[0];       // sANITY cHECK :)

            byte[] finalBytes = new byte[inputFile.Length - 576 - 853];
            Array.Copy(inputFile, 576, finalBytes, 0, inputFile.Length - 576 - 853);

            return finalBytes;
        }
    }
}

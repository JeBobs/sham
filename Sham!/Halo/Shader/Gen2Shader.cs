using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using static Sham.Halo.JointedMeshSkeleton;
using static Sham.UserInterface;

namespace Sham.Halo
{
    public static class Gen2Shader
    {
        // TODO: More Accurate Naming
        public enum ShaderTemplate
        {
            nil,
            tex_bump_shiny
        };

        public static readonly string[] ShaderModelString =
        {
            @"ÈÝÁl",
            @";Pº"
        };

        public static void CreateH2ShaderFile(Material material, string directory, ref FileConflictProperties props)
        {
            if (File.Exists(directory + PathSeparator + material.Name + ".shader"))
            {
                if (!props.setAlwaysContinue) props.shouldContinue = FileExistsConditional(directory, material.Name + ".shader", ref props);
                else if (!props.alwaysShouldContinue)
                {
                    NotifyFileSkip(material.Name + ".shader");
                    return;
                }
                if (props.shouldContinue)
                {
                    File.Delete(directory + PathSeparator + material.Name + ".shader");
                    TryPrintDebug("Deleting " + directory + PathSeparator + material.Name + ".shader.", 2);
                }
            }
            else props.shouldContinue = true;
            if (!props.shouldContinue)
            {
                TryPrintDebug("Shouldn't continue creating H2 shader file for one reason or another.", 4);
                return;
            }
            FileStream stream = new FileStream(directory + PathSeparator + material.Name + ".shader", FileMode.Append);
            List<byte> fileBuffer = new List<byte>();

            // Looks really bad right now, but will possibly be very useful for manupulating bits to support other versions.

            byte[] dash = Encoding.ASCII.GetBytes("dahs");
            byte[] shadertype = Encoding.GetEncoding("ISO-8859-1").GetBytes(ShaderModelString[(int)ShaderTemplate.nil]);
            byte at = Convert.ToByte(64);
            byte unk = Convert.ToByte(1);
            byte[] unk2 = Encoding.GetEncoding("ISO-8859-1").GetBytes("ÿ!MLBdfbt");
            byte unk3 = Convert.ToByte(128);
            byte[] unk4 = Encoding.GetEncoding("ISO-8859-1").GetBytes("mets");
            byte[] unkOptions = new byte[8]; // #0A0A
            byte[] unk5 = Encoding.GetEncoding("ISO-8859-1").GetBytes("ÿÿÿÿ");
            byte[] tils = Encoding.GetEncoding("ISO-8859-1").GetBytes("tils");

            // #0A0A - Clearly options, will implement later with more research.
            // 5th bit:
            // 00 -- null
            // 28 -- tex_bump
            // 2C -- illum_detail
            // 2E -- tex_bump_shiny

            // I know this is ew. I'll make it better later
            fileBuffer.AddRange(new byte[36]);
            fileBuffer.AddRange(dash);
            fileBuffer.AddRange(shadertype);
            fileBuffer.Add(at);
            fileBuffer.AddRange(new byte[11]);
            fileBuffer.Add(unk);
            fileBuffer.AddRange(new byte[2]);
            fileBuffer.AddRange(unk2);
            fileBuffer.AddRange(new byte[4]);
            fileBuffer.Add(unk);
            fileBuffer.AddRange(new byte[3]);
            fileBuffer.Add(unk3);
            fileBuffer.AddRange(new byte[3]);
            fileBuffer.AddRange(unk4);
            fileBuffer.AddRange(unkOptions);
            fileBuffer.AddRange(unk5);
            fileBuffer.AddRange(new byte[60]); // LOTS OF OPTIONS HERE, HAVE TO FILL OUT!
            fileBuffer.AddRange(tils);
            fileBuffer.AddRange(new byte[8]);
            fileBuffer.AddRange(unk5);
            fileBuffer.AddRange(new byte[36]);

            stream.Write(fileBuffer.ToArray(), 0, fileBuffer.Count);
            stream.Dispose();

            PrintLine("Wrote material " + material.Name + ".");
        }
    }
}

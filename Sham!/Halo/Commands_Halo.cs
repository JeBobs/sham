using System.IO;
using System.Text.RegularExpressions;

using static Sham.UserInterface;

using static Sham.Halo.Gen2Shader;
using static Sham.Halo.JointedMeshSkeleton;
using static Sham.Halo.JointedMeshCommon;
using static Sham.Halo.ShaderIncludeConverter;

namespace Sham.Halo
{
    public static class Commands_Halo
    {
        public static void Command_GenerateH2Shaders(string FilePath)
        {
            JMSHeader Header = ParseJMSHeader(FilePath);

            PrintTask("Creating Halo 2 shader files");
            PrintLine(""); // White Space

            string directory = Directory.CreateDirectory(new FileInfo(FilePath).Directory.FullName + PathSeparator + "shaders").FullName;
            FileConflictProperties props = new FileConflictProperties();
            foreach (Material m in Header.Materials)
            {
                if (!Directory.Exists(directory))
                {
                    TryPrintDebug("Attempting to create directory at " + directory + PathSeparator + "shaders" + ".", 2);
                }
                CreateH2ShaderFile(m, directory, ref props);
            }
            PrintTask("Wrote shaders to \"shaders\" directory in the JMS folder", true);
        }

        public static void Command_JMCompress(string FilePath, string outPath)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                PrintLine("No valid file was specified! Please check Help to view the valid syntax.");
                return;
            }

            string directory = Directory.CreateDirectory(new FileInfo(FilePath).Directory.FullName).FullName;

            TryPrintDebug("Directory file path is " + directory + ".", 1);

            if (string.IsNullOrEmpty(outPath) || outPath.Contains("_MAGIC_DEBUG"))
            {
                PrintLine("No output path specified, outputting to the source folder.");
                outPath = directory;
            }

            PrintTask("Starting jointed mesh file compression operation");

            string[] output = CompressJMFile(FilePath);

            string ogFileName = Path.GetFileNameWithoutExtension(FilePath);
            string extension = Path.GetFileName(FilePath).Replace(ogFileName, "");

            string finalName = ogFileName + "_compressed" + extension;
            string finalPath = outPath + PathSeparator + finalName;

            TryPrintDebug("Final file path is " + finalPath + ".", 1);

            FileConflictProperties props = new FileConflictProperties();

            if (File.Exists(finalPath))
            {
                props.shouldContinue = FileExistsConditional(directory, finalName, ref props);
            }
            else props.shouldContinue = true;
            if (props.shouldContinue)
            {
                PrintTask("Writing compressed jointed mesh file to disk");
                File.WriteAllLines(finalPath, output);
                PrintTask("Wrote compressed jointed mesh file", true);
            }

            PrintTask("Completed compression operation", true);
        }

        public static void Command_ConvertIncludeToShader(string FilePath, string outPath)
        {
            PrintTask("Starting include tag file conversion operation");

            if (string.IsNullOrEmpty(FilePath))
            {
                PrintLine("No valid file was specified! Please check Help to view the valid syntax.");
                return;
            }

            string directory = Directory.CreateDirectory(new FileInfo(FilePath).Directory.FullName).FullName;
            
            TryPrintDebug("Directory file path is " + directory + ".", 1);

            if (string.IsNullOrEmpty(outPath) || outPath.Contains("_MAGIC_DEBUG"))
            {
                PrintLine("No output path specified, outputting to \"_converted\" folder.");
                outPath = directory + PathSeparator + "_converted";
            }
            else outPath = directory;

            byte[] output = ConvertTagIncludeToText(FilePath);

            if (output.Length == 0)
            {
                PrintLine("Empty file or incorrect type!");
                NotifyFileSkip(FilePath);
                return;
            }

            string ogFileName = Path.GetFileNameWithoutExtension(FilePath);

            // Listen. There most likely is a better way to do this.
            // I'm in a rush. I'd like to just get this working so I don't
            // have to manually edit 350+ files to remove some garbage.
            // 
            // I'm going to stop monolouging so I can focus on finishing this.

            // Some possibly useful regex patterns:
            // _*\.hlsl_include
            //[a-zA-Z]+

            MatchCollection extMatches = Regex.Matches(ogFileName, @"_[a-zA-Z]+");
            MatchCollection matches = Regex.Matches(ogFileName, @"[a-zA-Z]+");

            string finalName = ogFileName.Replace(extMatches[extMatches.Count - 1].ToString(), "") + "." + matches[matches.Count - 1].ToString();
            string finalPath = outPath + PathSeparator + finalName;

            TryPrintDebug("Final file path is " + finalPath + ".", 1);

            FileConflictProperties props = new FileConflictProperties();

            if (File.Exists(finalPath))
            {
                props.shouldContinue = FileExistsConditional(outPath, finalName, ref props);
            }
            else props.shouldContinue = true;
            if (props.shouldContinue)
            {
                PrintTask("Writing converted shader file to disk");

                File.WriteAllBytes(finalPath, output);

                PrintTask("Wrote converted shader file", true);
            }

            PrintTask("Completed conversion operation", true);
        }
    }
}

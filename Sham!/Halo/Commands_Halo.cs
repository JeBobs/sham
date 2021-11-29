using System.IO;

using static Sham.UserInterface;

using static Sham.Halo.Gen2Shader;
using static Sham.Halo.JointedMeshSkeleton;
using static Sham.Halo.JointedMeshCommon;

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
            if (string.IsNullOrEmpty(outPath))
            {
                PrintLine("No output path specified, outputting to the source folder.");
                outPath = FilePath;
            }

            PrintTask("Starting jointed mesh file compression operation");

            string[] output = CompressJMFile(FilePath);
            string directory = Directory.CreateDirectory(new FileInfo(FilePath).Directory.FullName).FullName;

            string ogFileName = Path.GetFileNameWithoutExtension(FilePath);
            string extension = Path.GetFileName(FilePath).Replace(ogFileName, "");

            string finalName = ogFileName + "_compressed" + extension;
            string finalPath = directory + PathSeparator + finalName;

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
    }
}

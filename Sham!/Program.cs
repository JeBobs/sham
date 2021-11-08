using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

using static Sham.JointedMeshSkeleton;
using static Sham.CommandDirectory;
using static Sham.UserInterface;
using static Sham.Gen2Shader;

namespace Sham
{
    class ShamInstance
    {
        public static int DebugLevel;
        public static AssemblyName AppInfo;
        static void Main(string[] args)
        {
            // Title Bar
            AppInfo = Assembly.GetEntryAssembly().GetName();
            Console.WriteLine(AppInfo.Name + " Version " + AppInfo.Version + " - Made with <3 by JeBobs");

            // Handle Arguments
            string FilePath = "";
            string Command = "";
            string argument = "";
            if (args.Length > 0)
            {
                Command = args[0].ToLower();
                if (args.Length > 1) FilePath = args[1];
                if (!int.TryParse(args[args.Length - 1], out DebugLevel)) DebugLevel = -1;
                if (args.Length > 2)  argument = args[2];
            }

            // Initialize Debug
            if (DebugLevel >= 2)
            {
                Console.Write("Passed with arguments ");
                foreach (string s in args) Console.Write(s + " ");
                PrintHeader("Debug Level is " + DebugLevel);
            }

            // Basic Linux Path Support
            if (Directory.GetCurrentDirectory().Substring(0,1) == "/")
            {
                PathSeparator = @"/";
            }

            // Parse Command
            switch (Command)
            {
                case "jmcompress":
                    Command_JMCompress(FilePath, argument);
                    break;
                case "generateh2shaders":
                    Command_GenerateH2Shaders(FilePath);
                    break;
                case "help":
                    string arg = "";
                    if (args.Length >= 2) arg = args[1];
                    Command_Help(arg);
                    break;
                case "":
                default:
                    Command_Help();
                    break;
            }
        }

        static void Command_GenerateH2Shaders(string FilePath)
        {
            JMSHeader Header = ParseJMSHeader(FilePath);

            PrintTask("Creating Halo 2 shader files");
            Console.WriteLine(); // White Space

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

        static void Command_Help(string command = "")
        {
            string h = !string.IsNullOrEmpty(command) ? command : AppInfo.Name;
            Console.WriteLine("Help for " + h + ":");

            h = "\n\t[ ] indicates a required argument.\n\t< > indicates an optional argument.\n\n";
            TryPrintDebug("Input help command is " + command, 4);
            switch (command)
            {
                case "generateh2shaders":
                    h += AddHelpEntry(CommandHelp[(int)Command.GenerateH2Shaders]);
                    break;

                case "generateh3shaders":
                    h += AddHelpEntry(CommandHelp[(int)Command.GenerateH3Shaders]);
                    break;
                case "jmcompress":
                    h += AddHelpEntry(CommandHelp[(int)Command.JMCompress]);
                    break;
                case "help":
                default:
                    foreach (string s in CommandHelp) h += AddHelpEntry(s);
                    break;
            }
            Console.WriteLine(h);
        }

        static void Command_JMCompress(string FilePath, string outPath)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                Console.WriteLine("No valid file was specified! Please check Help to view the valid syntax.");
                return;
            }
            if (string.IsNullOrEmpty(outPath))
            {
                Console.WriteLine("No output path specified, outputting to the source folder.");
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

        public static JMSHeader ParseJMSHeader(string path)
        {
            if (!path.ToLower().Contains("jms"))
            {
                Console.WriteLine("File does not appear to be a jms file! Other formats may be supported at a later date, sorry.");
                return null;
            }
            else
            {
                PrintTask("Parsing " + path);

                string[] h = File.ReadAllLines(path);
                JMSHeader header = new JMSHeader();

                Node NodeBuffer = new Node();
                Material MaterialBuffer = new Material();
                int parsingNodeLine = 0, parsingShaderLine = 0;
                bool ParsedNodes = false, ParsedMaterials = false;

                foreach (string line in h)
                {
                    TryPrintDebug("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ " + line, 4);
                    if (line.StartsWith(";", StringComparison.OrdinalIgnoreCase))
                    {
                        TryPrintDebug("Hit comment line, ignoring.", 2);
                    }
                    else if (line.StartsWith(Environment.NewLine, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(line))
                    {
                        TryPrintDebug("Hit blank line, ignoring.", 2);
                    }
                    else
                    {
                        // Parse Version
                        if (header.Version == -1)
                        {
                            if (!int.TryParse(line, out header.Version))
                            {
                                Console.WriteLine("Input file is not a compatible JMS version! Aborting.");
                                break;
                            }
                            else PrintHeader("JMS Version is " + header.Version);
                            continue;
                        }

                        // Parse Node Count
                        if (header.NodeCount == -1)
                        {
                            PrintTask("Starting node parsing");
                            if (!int.TryParse(line, out header.NodeCount))
                            {
                                Console.WriteLine("Input file does not have valid data! Aborting.");
                                break;
                            }
                            else PrintHeader("This model has " + header.NodeCount + " nodes.");
                            continue;
                        }

                        // Parse Nodes - none of this is actually needed for shaders
                        if (header.NodeCount > 0 && !ParsedNodes)
                        {
                            // I know we don't have to do this butttttttt it may be used for something later
                            if (header.Nodes.Count < header.NodeCount || parsingNodeLine != 0)
                            {
                                TryPrintDebug("Parsing node line " + parsingNodeLine + ".", 3);
                                // TODO: Fill this with real data
                                switch (parsingNodeLine)
                                {
                                    case 0:
                                        NodeBuffer.NodeName = line;
                                        TryPrintDebug("Setting node name to " + NodeBuffer.NodeName + ".", 2);
                                        break;

                                    case 1:
                                        _ = int.TryParse(line, out NodeBuffer.ParentNodeIndex);
                                        TryPrintDebug("Setting parent node index to " + NodeBuffer.ParentNodeIndex + ".", 2);
                                        break;

                                    case 2:
                                        TryPrintDebug("Skipping Rotation.", 2);
                                        //NodeBuffer.Rotation.x = System.Text.RegularExpressions.Regex.Match(line, @"([+-]?(?=\.\d|\d)(?:\d+)?(?:\.?\d*))(?:[eE]([+-]?\d+))?").Value;
                                        //Console.WriteLine(System.Text.RegularExpressions.Regex.Match(line, @"([+-]?(?=\.\d|\d)(?:\d+)?(?:\.?\d*))(?:[eE]([+-]?\d+))?").Value);
                                        break;
                                    case 3:
                                        TryPrintDebug("Skipping Location.", 2);
                                        break;
                                }
                                parsingNodeLine++;
                                if (parsingNodeLine >= JMSHeader.NodeLineLength)
                                {
                                    header.Nodes.Add(NodeBuffer);
                                    parsingNodeLine = 0;
                                    NodeBuffer = new Node();
                                }
                                continue;
                            }
                            else
                            {
                                ParsedNodes = true;
                                foreach (Node n in header.Nodes) Console.WriteLine("Acknowledged node " + n.NodeName + ".");
                                PrintTask("Finished parsing nodes", true);
                            }
                        }

                        // Parse Material Count
                        if (header.MaterialCount == -1)
                        {
                            PrintTask("Starting material parsing");
                            if (!int.TryParse(line, out header.MaterialCount))
                            {
                                Console.WriteLine("Input file does not have valid data! Aborting.");
                                break;
                            }
                            else PrintHeader("This model has " + header.MaterialCount + " materials.");
                            continue;
                        }

                        // Parse Materials
                        if (header.MaterialCount > 0 && !ParsedMaterials)
                        {
                            // I know we don't have to do this butttttttt it may be used for something later
                            if (header.Materials.Count < header.MaterialCount || parsingShaderLine != 0)
                            {
                                TryPrintDebug("Parsing material line " + parsingShaderLine + ".", 3);
                                // TODO: Fill this with real data
                                switch (parsingShaderLine)
                                {
                                    case 0:
                                        MaterialBuffer.Name = line.Replace(")", string.Empty);
                                        TryPrintDebug("Setting material name to " + MaterialBuffer.Name + ".", 2);
                                        break;

                                    case 1:
                                        _ = int.TryParse(System.Text.RegularExpressions.Regex.Match(line, @"[0-9]+").Value, out MaterialBuffer.MaterialSlotIndex);
                                        TryPrintDebug("Setting material slot index to " + MaterialBuffer.MaterialSlotIndex + ".", 2);

                                        break;
                                }
                                parsingShaderLine++;
                                if (parsingShaderLine >= JMSHeader.ShaderLineLength)
                                {
                                    header.Materials.Add(MaterialBuffer);
                                    parsingShaderLine = 0;
                                    MaterialBuffer = new Material();
                                }
                                continue;
                            }
                            else
                            {
                                ParsedMaterials = true;
                                foreach (Material m in header.Materials) Console.WriteLine("Acknowledged material " + m.Name + ".");
                                PrintTask("Finished parsing materials", true);
                            }
                        }

                        // h
                        if (header.NodeCount + header.MaterialCount > 20) PrintHeader("This model is a bitch.");
                        PrintTask("Finished analysing JMS header", true);
                        break;
                    }
                }
                return header;
            }
        }

        public static string[] CompressJMFile(string path)
        {
            if (!path.ToLower().Contains("jm"))
            {
                Console.WriteLine("File does not appear to be a jms/jma file! Other formats may be supported at a later date, sorry.");
                return new string[0];
            }
            else
            {
                PrintTask("Creating compressed buffer for " + path);
                List<string> compressed = new List<string>();
                string[] h = File.ReadAllLines(path);
                foreach (string line in h)
                {
                    TryPrintDebug("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ " + line, 4);
                    if (!line.StartsWith(";", StringComparison.OrdinalIgnoreCase)&& !line.StartsWith(Environment.NewLine, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(line))
                    {
                        compressed.Add(line);
                        TryPrintDebug("Adding line " + line, 3);
                    }
                    else
                    {
                        TryPrintDebug("Ignoring line " + line, 2);
                    }
                }
                PrintTask("Finished creating compressed buffer", true);
                return compressed.ToArray();
            }
        }

        static void CreateH2ShaderFile(Material material, string directory, ref FileConflictProperties props)
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

            Console.WriteLine("Wrote material " + material.Name + ".");
        }
    }
}
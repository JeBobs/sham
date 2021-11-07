using System;
using System.IO;
using System.Text;
using System.Reflection;

using static Sham.JointedMeshSkeleton;
using static Sham.CommandDirectory;
using static Sham.UserInterface;

namespace Sham
{
    class Program
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
            if (args.Length > 0)
            {
                Command = args[0].ToLower();
                if (args.Length > 1) FilePath = args[1];
                if (args.Length > 2)
                {
                    if (!int.TryParse(args[2], out DebugLevel))
                    {
                        Console.WriteLine("Error specifying debug level! Using debug level 0.");
                    }
                }
            }

            // Initialize Debug
            if (DebugLevel >= 2)
            {
                Console.Write("Passed with arguments ");
                foreach (string s in args) Console.Write(s + " ");
                Console.WriteLine();
            }

            // Parse Command
            switch (Command)
            {
                case "generateh2shaders":
                    Command_GenerateH2Shaders(FilePath);
                    break;
                case "help":
                    string arg = args.Length > 2 ? args[1] : "";
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

            foreach (Material m in Header.Materials)
            {
                string directory = Directory.CreateDirectory(new FileInfo(FilePath).Directory.FullName + @"\shaders").FullName;
                if (!Directory.Exists(directory))
                {
                    TryPrintDebug("Attempting to create directory at " + directory + @"\shaders" + ".", 2);
                }
                CreateH2ShaderFile(m, directory);
            }
            PrintTask("Wrote shaders to \"shaders\" directory in the JMS folder", true);
        }

        static void Command_Help(string command = "")
        {
            string h = !string.IsNullOrEmpty(command) ? command : AppInfo.Name;
            Console.WriteLine("Help for " + h + ":");

            h = "\n\t[ ] indicates a required argument.\n\t< > indicates an optional argument.\n\n";
            switch (command)
            {
                case "generateh2shaders":
                    h += CommandHelp[(int)Command.GenerateH2Shaders];
                    break;

                case "generateh3shaders":
                    h += CommandHelp[(int)Command.GenerateH3Shaders];
                    break;

                case "help":
                default:
                    foreach (string s in CommandHelp)
                    {
                        h += "\t" + s + "\n";
                    }
                    break;
            }
            Console.WriteLine(h);
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

        static void CreateH2ShaderFile(Material material, string directory)
        {
            byte[] header = new byte[40]; // Not sure if it should be anything but 00's
            byte[] dash = Encoding.Unicode.GetBytes("dahs");
            TryPrintDebug("Header is " + Encoding.ASCII.GetString(header) + ", dash is " + Encoding.ASCII.GetString(dash) + ".", 1);
            for (int i = 37; i >= 40; i++)
            {
                header[i] = dash[i - 37];
            }
            TryPrintDebug("Writing " + Encoding.ASCII.GetString(header) + " to file.", 1);
            File.WriteAllBytes(directory + @"\" + material.Name + ".shader", header);
            //File.WriteAllText(directory + @"\" + material.Name + ".shader", "please help me");
            Console.WriteLine("Wrote material " + material.Name + ".");
        }
    }
}

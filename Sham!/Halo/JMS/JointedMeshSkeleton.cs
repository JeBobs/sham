using System;
using System.Collections.Generic;
using System.IO;

using static Sham.UserInterface;

namespace Sham.Halo
{
    public class JointedMeshSkeleton
    {
        public class JMSHeader
        {
            public int Version = -1;
            public int NodeCount = -1;
            public List<Node> Nodes = new List<Node>();
            public int MaterialCount = -1;
            public List<Material> Materials = new List<Material>();

            public const int NodeLineLength = 4;
            public const int ShaderLineLength = 2;
        }

        public struct Material
        {
            public string Name;
            public int MaterialSlotIndex;
            public string Permutation;
            public string Region;
        }

        public struct Node
        {
            public string NodeName;
            public int ParentNodeIndex;
            public Quaternion Rotation;
            public Vector3 Location;
        }

        public struct Vector3
        {
            public float x;
            public float y;
            public float z;
        }

        public struct Quaternion
        {
            public float x;
            public float y;
            public float z;
            public float w;
        }

        public static JMSHeader ParseJMSHeader(string path)
        {
            if (!path.ToLower().Contains("jms"))
            {
                PrintLine("File does not appear to be a jms file! Other formats may be supported at a later date, sorry.");
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
                                PrintLine("Input file is not a compatible JMS version! Aborting.");
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
                                PrintLine("Input file does not have valid data! Aborting.");
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
                                        //PrintLine(System.Text.RegularExpressions.Regex.Match(line, @"([+-]?(?=\.\d|\d)(?:\d+)?(?:\.?\d*))(?:[eE]([+-]?\d+))?").Value);
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
                                foreach (Node n in header.Nodes) PrintLine("Acknowledged node " + n.NodeName + ".");
                                PrintTask("Finished parsing nodes", true);
                            }
                        }

                        // Parse Material Count
                        if (header.MaterialCount == -1)
                        {
                            PrintTask("Starting material parsing");
                            if (!int.TryParse(line, out header.MaterialCount))
                            {
                                PrintLine("Input file does not have valid data! Aborting.");
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
                                foreach (Material m in header.Materials) PrintLine("Acknowledged material " + m.Name + ".");
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
    }
}

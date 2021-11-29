using System;
using System.Collections.Generic;
using System.IO;

using static Sham.UserInterface;

namespace Sham.Halo
{
    internal class JointedMeshCommon
    {
        public static string[] CompressJMFile(string path)
        {
            if (!path.ToLower().Contains("jm"))
            {
                PrintLine("File does not appear to be a jms/jma file! Other formats may be supported at a later date, sorry.");
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
                    if (!line.StartsWith(";", StringComparison.OrdinalIgnoreCase) && !line.StartsWith(Environment.NewLine, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(line))
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
    }
}

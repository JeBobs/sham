using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sham_
{
    class CommandDirectory
    {
        public enum Command
        {
            Help,
            GenerateH2Shaders,
            GenerateH3Shaders,
            CompressJMS
        }

        public static string[] CommandHelp =
        {
            "help <command> - Either shows this screen or provides help for a command.",
            "GenerateH2Shaders [JMS File] - Generates empty Halo 2 compatible shader files for each material slot.",
            "GenerateH3Shaders [JMS File] - Generates empty Halo 3 compatible shader files for each material slot.",
            "CompressJMS [JMS File] - Takes an input JMS file and removes white-space & comments."
        };
    }
}

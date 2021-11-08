using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sham
{
    class CommandDirectory
    {
        public enum Command
        {
            Help,
            GenerateH2Shaders,
            GenerateH3Shaders,
            JMCompress
        }

        public static string[] CommandHelp =
        {
            "help <command> - Either shows this screen or provides help for a command.",
            "GenerateH2Shaders [JMS File] - Generates empty Halo 2 compatible shader files for each material slot.",
            "GenerateH3Shaders [JMS File] - Generates empty Halo 3 compatible shader files for each material slot.",
            "JMCompress [JMS/JMA File] - Takes an input JMS/JMA/JMM file and removes white-space & comments."
        };
    }
}

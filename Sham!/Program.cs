using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

using static Sham.CommandDirectory;
using static Sham.UserInterface;

using static Sham.Halo.Commands_Halo;

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
            PrintLine(AppInfo.Name + " Version " + AppInfo.Version + " - Made with <3 by JeBobs");

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
                Print("Passed with arguments ");
                foreach (string s in args) Print(s + " ");
                PrintHeader("Debug Level is " + DebugLevel);
            }

            // Basic Linux Path Support
            if (Directory.GetCurrentDirectory().Substring(0,1) == "/")
            {
                PathSeparator = @"/";
            }

            // Magic Debug Option 
            // (Allows us to pass null file paths while retaining a debug level)
            if (argument == "_MAGIC_DEBUG") argument = null;

            // Parse Command
            switch (Command)
            {
                case "jmcompress":
                    Command_JMCompress(FilePath, argument);
                    break;
                case "generateh2shaders":
                    Command_GenerateH2Shaders(FilePath);
                    break;
                case "includetoshader": // TODO CLEANUP PLEASE!!!
                    if (File.GetAttributes(FilePath).HasFlag(FileAttributes.Directory))
                    {
                        DirectoryInfo d = new DirectoryInfo(FilePath);
                        TryPrintDebug("Directory info: \n" + d, 4);
                        foreach (FileInfo f in d.GetFiles(@"*.hlsl_include"))
                        {
                            TryPrintDebug("Foreach path is " + f.FullName + ".", 4);
                            Command_IncludeToShader(f.FullName, argument);
                        }
                    }
                    else Command_IncludeToShader(FilePath, argument);
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

        static void Command_Help(string command = "")
        {
            string h = !string.IsNullOrEmpty(command) ? command : AppInfo.Name;
            PrintLine("Help for " + h + ":");

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
                case "includetoshader":
                    h += AddHelpEntry(CommandHelp[(int)Command.IncludeToShader]);
                    break;
                case "help":
                default:
                    foreach (string s in CommandHelp) h += AddHelpEntry(s);
                    break;
            }
            PrintLine(h);
        }
    }
}

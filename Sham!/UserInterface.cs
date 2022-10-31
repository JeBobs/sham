using Sham.Properties;
using System;
using System.IO;

namespace Sham
{
    public class UserInterface
    {
        // Still have yet to try this new method on *nix!
        public static string PathSeparator = Path.DirectorySeparatorChar.ToString();

        // Useful if we want to output text another way in the future,
        // Or if we want to eventually support other languages.

        public static void PrintHeader(string text)
        {
            Console.WriteLine(StringResources.HeaderLarge + text + StringResources.HeaderLarge);
        }

        public static void PrintTask(string text, bool complete = false)
        {
            Console.WriteLine("\n ---" + (complete ? " ✓ " : " ■ ") + text + "...");
        }

        public static void TryPrintDebug(string text, int level)
        {
            if (ShamInstance.DebugLevel >= level) 
                Console.WriteLine(StringResources.Text_DebugLevel + level + ": " + text);
        }

        public static void PrintLine(string text)
        {
            Console.WriteLine(text);
        }

        public static void Print(string text)
        {
            Console.Write(text);
        }

        public static void NotifyFileSkip(string filePath)
        {
            Console.WriteLine("Skipping " + filePath + ".");
        }

        public static bool GetBoolFromString(string h)
        {
            return h == "y" ? true : false;
        }

        public static string AddHelpEntry(string command)
        {
            return "\t" + command + "\n";
        }

        // Should probably be moved elsewhere at some point

        public static bool FileExistsConditional(string directory, string fileName, ref FileConflictProperties properties)
        {
            if (properties.shouldContinue) 
                return properties.shouldContinue;

            PrintLine(string.Format(StringResources.Text_FileExistsOverwritePrompt, directory + PathSeparator + fileName));
            PrintLine("(preface with * to apply to all occurrences)");

            string input = Console.ReadLine();

            if (input.ToLower() == "*" || string.IsNullOrEmpty(input)) 
                FileExistsConditional(directory, fileName, ref properties);

            switch (input.ToLower().Substring(0, 1))
            {
                case "*":
                    properties.setAlwaysContinue = true;
                    properties.alwaysShouldContinue = GetBoolFromString(input.ToLower().Substring(1, 1));
                    properties.shouldContinue = GetBoolFromString(input.ToLower().Substring(1, 1));
                    if (!properties.shouldContinue) NotifyFileSkip(fileName);
                    break;

                case "y":
                    properties.shouldContinue = true;
                    break;

                case "n":
                case "hell naw":
                default:
                    properties.shouldContinue = false;
                    NotifyFileSkip(fileName);
                    break;
            }
            return properties.shouldContinue;
        }

        public struct FileConflictProperties
        {
            public bool shouldContinue;
            public bool setAlwaysContinue;
            public bool alwaysShouldContinue;
        }

    }
}

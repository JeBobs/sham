using System;

namespace Sham
{
    public class UserInterface
    {
        public static string PathSeparator = @"\";
        public static void PrintHeader(string text)
        {
            Console.WriteLine("\n-------------------------\n" + text + "\n-------------------------\n");
        }

        public static void PrintTask(string text, bool complete = false)
        {
            if (complete) Console.WriteLine("\n --- ✓ " + text + ".");
            else Console.WriteLine("\n --- ■ " + text + "...");
        }

        public static void TryPrintDebug(string text, int level)
        {
            if (ShamInstance.DebugLevel >= level) Console.WriteLine("DEBUG L" + level + ": " + text);
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

        public static bool FileExistsConditional(string directory, string fileName, ref FileConflictProperties properties)
        {
            if (properties.shouldContinue) return properties.shouldContinue;

            PrintLine("File " + directory + PathSeparator + fileName + " exists, overwrite?");
            PrintLine("(preface with * to apply to all occurrences)");

            string input = Console.ReadLine();

            if (input.ToLower() == "*" || string.IsNullOrEmpty(input)) FileExistsConditional(directory, fileName, ref properties);

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

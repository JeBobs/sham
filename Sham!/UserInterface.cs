﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sham_
{
    class UserInterface
    {
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
            if (Program.DebugLevel >= level) Console.WriteLine("DEBUG L" + level + ": " + text);
        }
    }
}

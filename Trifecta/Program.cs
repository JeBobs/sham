using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Sham.UserInterface;

namespace Trifecta
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static AssemblyName AppInfo;

        [STAThread]
        static void Main()
        {
            // Set this to 4 for now
            Sham.ShamInstance.DebugLevel = 4;

            // Title Bar
            AppInfo = Assembly.GetEntryAssembly().GetName();
            PrintLine(AppInfo.Name + " Version " + AppInfo.Version + " - Made with <3 by JeBobs");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

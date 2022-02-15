using System;
using System.Windows.Forms;

using static Sham.UserInterface;

namespace Trifecta
{
    public partial class DebugForm : Form
    {
        public DebugForm()
        {
            InitializeComponent();
        }

        private void LaunchToolButton_Click(object sender, EventArgs e)
        {
            char no = ' ';
            TryPrintDebug(NativeMethods.main(0, no, no).ToString(), 0);
        }
    }
}

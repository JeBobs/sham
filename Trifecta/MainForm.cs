using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using static Sham.UserInterface;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Trifecta.TrifectaHandler;
using ListView = System.Windows.Forms.ListView;

namespace Trifecta
{
    public partial class MainForm : Form
    {
        public string tagsPWD = "tags";
        public string dataPWD = "data";
        public MainForm()
        {
            InitializeComponent();

            TryPrintDebug("Initializing main form in directory " + Directory.GetCurrentDirectory() + ".", 1);

            // TODO: Eventaully abstract this and separate from mainform constructor

            tagsPWD = Directory.GetCurrentDirectory() + PathSeparator + "tags";
            dataPWD = Directory.GetCurrentDirectory() + PathSeparator + "data";

            TagsListView.SmallImageList = ListViewImages;
            DataListView.SmallImageList = ListViewImages;

            PopulateDirectoryListView(TagsListView, tagsPWD, false);
            PopulateDirectoryListView(DataListView, dataPWD, false);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            TryPrintDebug("Registered Keydown.", 4);
            if (e.Control)
            {
                if (e.KeyCode == Keys.A)
                {
                    // We'll do this later.
                    // ListView senderList = (ListView)sender;
                    // foreach (ListViewItem item in senderList.Items)
                    // {
                    //     item.Selected = true;
                    // }
                }
                if (e.KeyCode == Keys.Space)
                {
                    TryPrintDebug("Toggling file view visibility.", 2);
                    DirectoryContainer.Visible = !DirectoryContainer.Visible;
                    e.Handled = true;
                }

                if (e.Shift)
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        DebugForm d = new DebugForm();
                        d.ShowDialog();
                    }
                }
            }
        }

        private void ListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TryPrintDebug("Detected right mouse click.", 4);
                var senderList = (ListView)sender;
                var focusedItem = senderList.FocusedItem;

                ContextMenuStrip strip = null;
                switch (senderList.Name)
                {
                    case "TagsListView":
                        strip = TagsContextMenu;
                        break;
                    case "DataListView":
                        strip = DataContextMenu;
                        break;
                }
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
                {
                    strip.Show(Cursor.Position);
                }
            }
            QuickViewVisibilityTest();
        }

        void QuickViewVisibilityTest()
        {
            TryPrintDebug("There are currently " + TagsListView.SelectedItems.Count + " tag(s) selected in the view.", 4);
            TagsQuickToolStrip.Visible = TagsListView.SelectedItems.Count > 0;
            TryPrintDebug("There are currently " + DataListView.SelectedItems.Count + " data file(s) selected in the view.", 4);
            DataQuickToolStrip.Visible = DataListView.SelectedItems.Count > 0;
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TryPrintDebug("Registered mouse click event on view.", 3);

            TryPrintDebug("Sender type is " + sender.GetType().ToString(), 4);

            var senderList = (ListView)sender;
            var clickedItem = senderList.HitTest(e.Location).Item;

            TryPrintDebug("Sender is " + senderList.Name, 4);

            // TODO: Redo this to be more performant

            if (clickedItem != null)
            {
                string dir = "";
                switch (senderList.Name)
                {
                    case "TagsListView":
                        dir = tagsPWD;
                        break;
                    case "DataListView":
                        dir = dataPWD;
                        break;
                }

                FileViewSelectHandler(senderList, clickedItem.Text, dir, out dir);

                switch (senderList.Name)
                {
                    case "TagsListView":
                        tagsPWD = dir;
                        break;
                    case "DataListView":
                        dataPWD = dir;
                        break;
                }
            }
        }

        public void RunTool(string command, string argument, bool fromPWD = false, bool interfaceDataDirectory = false, bool toolFast = true)
        {
            string ifDirectory = interfaceDataDirectory ? "data" : "tags";
            string PWD = "";
            string dataPath = "";

            if (fromPWD)
            {
                PWD = interfaceDataDirectory ? dataPWD : tagsPWD;
                dataPath = PWD.Replace(Directory.GetCurrentDirectory() + PathSeparator + ifDirectory, "") + PathSeparator;
                if (dataPath.StartsWith("\\"))
                {
                    dataPath = dataPath.Remove(0, 1);
                }
            }

            TryPrintDebug("Current program directory is " + Directory.GetCurrentDirectory(), 4);
            TryPrintDebug("Running " + "tool with arguments " + command + " " + dataPath + argument, 3);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo()
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                FileName = toolFast ? "tool_fast.exe" : "tool.exe",
                Arguments = command + " " + dataPath + argument
            };
            process.StartInfo = startInfo;
            process.Start();
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // There's probably a better way to do this, might redo. Just wanted to get something working.
            TryPrintDebug("Selected import context menu option.", 4);
            List<string> bitmaps = new List<string>();
            List<string> jmsMeshes = new List<string>();
            List<string> fbxMeshes = new List<string>();
            foreach (ListViewItem item in DataListView.SelectedItems)
            {
                if (item.Text.Contains(".tif") || item.Text.Contains(".tiff") || item.Text.Contains(".dds"))
                {
                    bitmaps.Add(item.Text);
                    TryPrintDebug("Adding " + item.Text + " to bitmap import list.", 4);
                }
                // Will do later
                if (item.Text.Contains(".jms"))
                {
                    jmsMeshes.Add(item.Text);
                    TryPrintDebug("Adding " + item.Text + " to JMS import list.", 4);
                }
                if (item.Text.Contains(".fbx"))
                {
                    fbxMeshes.Add(item.Text);
                    TryPrintDebug("Adding " + item.Text + " to FBX import list.", 4);
                }
            }
            foreach (string bitmap in bitmaps)
            {
                RunTool("bitmap_single", bitmap, true, true);
            }
            foreach (string jmsMesh in jmsMeshes)
            {
                RunTool("render", jmsMesh, true, true);
            }
        }

        private void exploreInTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TryPrintDebug("DataPWD is " + dataPWD + ".", 4);
            string dataPath = dataPWD.Replace
                (
                    Directory.GetCurrentDirectory()
                    + PathSeparator + "data",

                    Directory.GetCurrentDirectory()
                    + PathSeparator + "tags"
                );
            if (DataListView.SelectedItems.Count == 1)
            {
                FileAttributes info = File.GetAttributes(dataPWD + PathSeparator + DataListView.SelectedItems[0].Text);
                TryPrintDebug("Explore In Tags File selected is " + dataPWD + PathSeparator + DataListView.SelectedItems[0].Text, 4);
                switch (info)
                {
                    case FileAttributes.Directory:
                        dataPath += PathSeparator + DataListView.SelectedItems[0].Text;
                        break;
                    default:
                        break;
                }
            }
            TryPrintDebug("Exploring directory " + dataPath + ".", 4);
            PopulateDirectoryListView(TagsListView, dataPath);
            tagsPWD = dataPath;
            DirectoryContainer.SelectedIndex = 0;
        }

        private void ListView_ItemChangedHandler(object sender, EventArgs e)
        {
            QuickViewVisibilityTest();
        }
    }
}

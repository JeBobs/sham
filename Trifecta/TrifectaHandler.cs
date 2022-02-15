using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using static Sham.UserInterface;

namespace Trifecta
{
    class TrifectaHandler
    {
        public static void PopulateDirectoryListView(ListView list, string FilePath, bool listUpDirectory = true)
        {
            list.Clear();

            Directory.CreateDirectory(FilePath);

            // Obtain a handle to the system image list.
            NativeMethods.SHFILEINFO shfi = new NativeMethods.SHFILEINFO();
            IntPtr hSysImgList = NativeMethods.SHGetFileInfo("",
                                                             0,
                                                             ref shfi,
                                                             (uint)Marshal.SizeOf(shfi),
                                                             NativeMethods.SHGFI_SYSICONINDEX
                                                              | NativeMethods.SHGFI_LARGEICON);

            // Set the ListView control to use that image list.
            IntPtr hOldImgList = NativeMethods.SendMessage(list.Handle,
                                                           NativeMethods.LVM_SETIMAGELIST,
                                                           NativeMethods.SHGFI_LARGEICON,
                                                           hSysImgList);

            // If the ListView control already had an image list, delete the old one.
            if (hOldImgList != IntPtr.Zero)
            {
                NativeMethods.ImageList_Destroy(hOldImgList);
            }

            // Set up the ListView control's basic properties.
            // Put it in "Details" mode, create a column so that "Details" mode will work,
            // and set its theme so it will look like the one used by Explorer.
            list.View = View.LargeIcon;
            list.Columns.Add("Name", 500);
            NativeMethods.SetWindowTheme(list.Handle, "Explorer", null);

            // Get the items from the file system, and add each of them to the ListView,
            // complete with their corresponding name and icon indices.
            string[] directory = Directory.GetFileSystemEntries(FilePath + PathSeparator);

            // God, I wish this worked. Apparently the above version doesn't
            // list extensions of common file types if it's disabled in the OS.
            //string[] directory = Directory.GetFiles(FilePath + PathSeparator);

            TryPrintDebug("Listing directory " + FilePath + PathSeparator + ", which has " + directory.Length + " files.", 4);

            if (listUpDirectory)
            {
                list.Items.Add("..");
            }

            foreach (string file in directory)
            {
                TryPrintDebug("Listing file " + file + ".", 2);
                IntPtr himl = NativeMethods.SHGetFileInfo(file, 0, ref shfi, (uint)Marshal.SizeOf(shfi),
                                                            NativeMethods.SHGFI_DISPLAYNAME
                                                            | NativeMethods.SHGFI_SYSICONINDEX
                                                            | NativeMethods.SHGFI_SMALLICON);
                list.Items.Add(shfi.szDisplayName, shfi.iIcon);
            }
        }

        public static void FileViewSelectHandler(ListView view, string itemClicked, string PWD, out string newPWD)
        {
            // Needed just in case
            newPWD = PWD;

            FileAttributes info = File.GetAttributes(PWD + PathSeparator + itemClicked);
            TryPrintDebug("Clicked item " + itemClicked, 4);

            switch (info)
            {
                case FileAttributes.Normal:
                    TryPrintDebug("Event registered, listing is a file. Not implemented yet, sorry!", 1);
                    break;
                case FileAttributes.Directory:
                    TryPrintDebug("File " + itemClicked + " is a directory.", 4);

                    if (itemClicked == "..")
                    {
                        newPWD = Directory.GetParent(PWD).FullName;
                    }
                    else
                    {
                        newPWD = PWD + PathSeparator + itemClicked;
                    }

                    TryPrintDebug("Event registered, attempting to open " + newPWD + ".", 3);

                    string homeDir = Directory.GetCurrentDirectory() + PathSeparator;

                    // Please don't crucify me for this. I promise I'll fix it later. Thanks.

                    bool inHomeDirectory =
                        newPWD == homeDir + "tags\\" || newPWD == homeDir + "tags" ||
                        newPWD == homeDir + "data\\" || newPWD == homeDir + "data"
                        ? true : false;

                    TryPrintDebug("Being in home directory is " + inHomeDirectory, 4);

                    PopulateDirectoryListView(view, newPWD, !inHomeDirectory);

                    break;
            }
        }
    }
}


namespace Trifecta
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.DirectoryContainer = new System.Windows.Forms.TabControl();
            this.TagsTab = new System.Windows.Forms.TabPage();
            this.TagsQuickToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.TagsListView = new System.Windows.Forms.ListView();
            this.DataTab = new System.Windows.Forms.TabPage();
            this.DataQuickToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.DataListView = new System.Windows.Forms.ListView();
            this.DataContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exploreInTagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TagsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tagsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ListViewImages = new System.Windows.Forms.ImageList(this.components);
            this.DirectoryContainer.SuspendLayout();
            this.TagsTab.SuspendLayout();
            this.TagsQuickToolStrip.SuspendLayout();
            this.DataTab.SuspendLayout();
            this.DataQuickToolStrip.SuspendLayout();
            this.DataContextMenu.SuspendLayout();
            this.TagsContextMenu.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DirectoryContainer
            // 
            this.DirectoryContainer.Controls.Add(this.TagsTab);
            this.DirectoryContainer.Controls.Add(this.DataTab);
            this.DirectoryContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DirectoryContainer.Location = new System.Drawing.Point(0, 374);
            this.DirectoryContainer.Margin = new System.Windows.Forms.Padding(0);
            this.DirectoryContainer.Multiline = true;
            this.DirectoryContainer.Name = "DirectoryContainer";
            this.DirectoryContainer.SelectedIndex = 0;
            this.DirectoryContainer.Size = new System.Drawing.Size(915, 247);
            this.DirectoryContainer.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.DirectoryContainer.TabIndex = 0;
            this.DirectoryContainer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            // 
            // TagsTab
            // 
            this.TagsTab.Controls.Add(this.TagsQuickToolStrip);
            this.TagsTab.Controls.Add(this.TagsListView);
            this.TagsTab.Location = new System.Drawing.Point(4, 22);
            this.TagsTab.Name = "TagsTab";
            this.TagsTab.Padding = new System.Windows.Forms.Padding(3);
            this.TagsTab.Size = new System.Drawing.Size(907, 221);
            this.TagsTab.TabIndex = 0;
            this.TagsTab.Text = "Tags";
            this.TagsTab.UseVisualStyleBackColor = true;
            this.TagsTab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            // 
            // TagsQuickToolStrip
            // 
            this.TagsQuickToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.TagsQuickToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.TagsQuickToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.TagsQuickToolStrip.Location = new System.Drawing.Point(3, 3);
            this.TagsQuickToolStrip.Name = "TagsQuickToolStrip";
            this.TagsQuickToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.TagsQuickToolStrip.Size = new System.Drawing.Size(901, 25);
            this.TagsQuickToolStrip.TabIndex = 1;
            this.TagsQuickToolStrip.Text = "toolStrip2";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(31, 22);
            this.toolStripButton1.Text = "Edit";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton2.Text = "Preview";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(47, 22);
            this.toolStripButton3.Text = "Extract";
            // 
            // TagsListView
            // 
            this.TagsListView.AllowDrop = true;
            this.TagsListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TagsListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TagsListView.HideSelection = false;
            this.TagsListView.LabelEdit = true;
            this.TagsListView.Location = new System.Drawing.Point(3, 31);
            this.TagsListView.Margin = new System.Windows.Forms.Padding(0);
            this.TagsListView.Name = "TagsListView";
            this.TagsListView.Size = new System.Drawing.Size(901, 187);
            this.TagsListView.TabIndex = 0;
            this.TagsListView.UseCompatibleStateImageBehavior = false;
            this.TagsListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListView_ItemChangedHandler);
            this.TagsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.TagsListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseClick);
            this.TagsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDoubleClick);
            // 
            // DataTab
            // 
            this.DataTab.Controls.Add(this.DataQuickToolStrip);
            this.DataTab.Controls.Add(this.DataListView);
            this.DataTab.Location = new System.Drawing.Point(4, 22);
            this.DataTab.Name = "DataTab";
            this.DataTab.Padding = new System.Windows.Forms.Padding(3);
            this.DataTab.Size = new System.Drawing.Size(907, 221);
            this.DataTab.TabIndex = 1;
            this.DataTab.Text = "Data";
            this.DataTab.UseVisualStyleBackColor = true;
            this.DataTab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            // 
            // DataQuickToolStrip
            // 
            this.DataQuickToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.DataQuickToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.DataQuickToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
            this.DataQuickToolStrip.Location = new System.Drawing.Point(3, 3);
            this.DataQuickToolStrip.Name = "DataQuickToolStrip";
            this.DataQuickToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.DataQuickToolStrip.Size = new System.Drawing.Size(901, 25);
            this.DataQuickToolStrip.TabIndex = 2;
            this.DataQuickToolStrip.Text = "toolStrip2";
            this.DataQuickToolStrip.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDoubleClick);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(31, 22);
            this.toolStripButton4.Text = "Edit";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton5.Text = "Preview";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(47, 22);
            this.toolStripButton6.Text = "Extract";
            // 
            // DataListView
            // 
            this.DataListView.AllowDrop = true;
            this.DataListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DataListView.HideSelection = false;
            this.DataListView.Location = new System.Drawing.Point(3, 31);
            this.DataListView.Name = "DataListView";
            this.DataListView.Size = new System.Drawing.Size(901, 187);
            this.DataListView.TabIndex = 0;
            this.DataListView.UseCompatibleStateImageBehavior = false;
            this.DataListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListView_ItemChangedHandler);
            this.DataListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.DataListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseClick);
            this.DataListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDoubleClick);
            // 
            // DataContextMenu
            // 
            this.DataContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataToolStripMenuItem,
            this.exploreInTagsToolStripMenuItem});
            this.DataContextMenu.Name = "DataContextMenu";
            this.DataContextMenu.Size = new System.Drawing.Size(153, 48);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dataToolStripMenuItem.Text = "Import...";
            this.dataToolStripMenuItem.Click += new System.EventHandler(this.dataToolStripMenuItem_Click);
            // 
            // exploreInTagsToolStripMenuItem
            // 
            this.exploreInTagsToolStripMenuItem.Name = "exploreInTagsToolStripMenuItem";
            this.exploreInTagsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exploreInTagsToolStripMenuItem.Text = "Explore in Tags";
            this.exploreInTagsToolStripMenuItem.Click += new System.EventHandler(this.exploreInTagsToolStripMenuItem_Click);
            // 
            // TagsContextMenu
            // 
            this.TagsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tagsToolStripMenuItem});
            this.TagsContextMenu.Name = "DataContextMenu";
            this.TagsContextMenu.Size = new System.Drawing.Size(97, 26);
            // 
            // tagsToolStripMenuItem
            // 
            this.tagsToolStripMenuItem.Name = "tagsToolStripMenuItem";
            this.tagsToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.tagsToolStripMenuItem.Text = "tags";
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(915, 349);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(915, 374);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(41, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Margin = new System.Windows.Forms.Padding(0);
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 25);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // ListViewImages
            // 
            this.ListViewImages.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ListViewImages.ImageSize = new System.Drawing.Size(16, 16);
            this.ListViewImages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 621);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.DirectoryContainer);
            this.Name = "MainForm";
            this.Text = "Trifecta";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.DirectoryContainer.ResumeLayout(false);
            this.TagsTab.ResumeLayout(false);
            this.TagsTab.PerformLayout();
            this.TagsQuickToolStrip.ResumeLayout(false);
            this.TagsQuickToolStrip.PerformLayout();
            this.DataTab.ResumeLayout(false);
            this.DataTab.PerformLayout();
            this.DataQuickToolStrip.ResumeLayout(false);
            this.DataQuickToolStrip.PerformLayout();
            this.DataContextMenu.ResumeLayout(false);
            this.TagsContextMenu.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl DirectoryContainer;
        private System.Windows.Forms.TabPage TagsTab;
        private System.Windows.Forms.TabPage DataTab;
        private System.Windows.Forms.ListView TagsListView;
        private System.Windows.Forms.ListView DataListView;
        private System.Windows.Forms.ContextMenuStrip DataContextMenu;
        private System.Windows.Forms.ContextMenuStrip TagsContextMenu;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tagsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exploreInTagsToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStrip TagsQuickToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStrip DataQuickToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ImageList ListViewImages;
    }
}


namespace GraphicsHowTo
{
    partial class HowToForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HowToForm));
            this.axAgUiAxVOCntrl1 = new AGI.STKX.Controls.AxAgUiAxVOCntrl();
            this.tvCode = new System.Windows.Forms.TreeView();
            this.ilTree = new System.Windows.Forms.ImageList(this.components);
            this.rtbCode = new System.Windows.Forms.RichTextBox();
            this.splitTreeAnd3D = new System.Windows.Forms.SplitContainer();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.linkSourceFile = new System.Windows.Forms.LinkLabel();
            this.cbShowUsing = new System.Windows.Forms.CheckBox();
            this.cmEdit = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmZoomTo = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.zoomToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSourceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmParent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.expandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parentExpandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parentCollapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilContextMenu = new System.Windows.Forms.ImageList(this.components);
            this.splitTreeAnd3D.Panel1.SuspendLayout();
            this.splitTreeAnd3D.Panel2.SuspendLayout();
            this.splitTreeAnd3D.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.cmEdit.SuspendLayout();
            this.cmZoomTo.SuspendLayout();
            this.cmParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // axAgUiAxVOCntrl1
            // 
            this.axAgUiAxVOCntrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axAgUiAxVOCntrl1.Enabled = true;
            this.axAgUiAxVOCntrl1.Location = new System.Drawing.Point(0, 0);
            this.axAgUiAxVOCntrl1.Name = "axAgUiAxVOCntrl1";
            this.axAgUiAxVOCntrl1.Size = new System.Drawing.Size(660, 525);
            this.axAgUiAxVOCntrl1.TabIndex = 0;
            this.axAgUiAxVOCntrl1.MouseDownEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseDownEventHandler(this.StkMouseDown);
            this.axAgUiAxVOCntrl1.MouseUpEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseUpEventHandler(this.StkMouseUp);
            this.axAgUiAxVOCntrl1.MouseMoveEvent += new AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEventHandler(this.StkMouseMove);
            this.axAgUiAxVOCntrl1.DblClick += new System.EventHandler(this.StkMouseDoubleClick);
            // 
            // tvCode
            // 
            this.tvCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvCode.HideSelection = false;
            this.tvCode.ImageIndex = 0;
            this.tvCode.ImageList = this.ilTree;
            this.tvCode.Location = new System.Drawing.Point(0, 0);
            this.tvCode.Name = "tvCode";
            this.tvCode.SelectedImageIndex = 0;
            this.tvCode.Size = new System.Drawing.Size(350, 525);
            this.tvCode.TabIndex = 1;
            this.tvCode.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCode_AfterSelect);
            this.tvCode.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tvCode_MouseMove);
            this.tvCode.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvCode_BeforeSelect);
            // 
            // ilTree
            // 
            this.ilTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ilTree.ImageSize = new System.Drawing.Size(16, 16);
            this.ilTree.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // rtbCode
            // 
            this.rtbCode.AcceptsTab = true;
            this.rtbCode.BackColor = System.Drawing.SystemColors.Control;
            this.rtbCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbCode.Location = new System.Drawing.Point(0, 0);
            this.rtbCode.Name = "rtbCode";
            this.rtbCode.ReadOnly = true;
            this.rtbCode.Size = new System.Drawing.Size(1014, 203);
            this.rtbCode.TabIndex = 2;
            this.rtbCode.Text = "";
            this.rtbCode.WordWrap = false;
            this.rtbCode.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rtbCode_MouseUp);
            // 
            // splitTreeAnd3D
            // 
            this.splitTreeAnd3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitTreeAnd3D.Location = new System.Drawing.Point(0, 0);
            this.splitTreeAnd3D.Name = "splitTreeAnd3D";
            // 
            // splitTreeAnd3D.Panel1
            // 
            this.splitTreeAnd3D.Panel1.Controls.Add(this.tvCode);
            // 
            // splitTreeAnd3D.Panel2
            // 
            this.splitTreeAnd3D.Panel2.Controls.Add(this.axAgUiAxVOCntrl1);
            this.splitTreeAnd3D.Size = new System.Drawing.Size(1014, 525);
            this.splitTreeAnd3D.SplitterDistance = 350;
            this.splitTreeAnd3D.TabIndex = 3;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.splitTreeAnd3D);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.linkSourceFile);
            this.splitContainer.Panel2.Controls.Add(this.cbShowUsing);
            this.splitContainer.Panel2.Controls.Add(this.rtbCode);
            this.splitContainer.Size = new System.Drawing.Size(1014, 732);
            this.splitContainer.SplitterDistance = 525;
            this.splitContainer.TabIndex = 4;
            // 
            // linkSourceFile
            // 
            this.linkSourceFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkSourceFile.Location = new System.Drawing.Point(606, 186);
            this.linkSourceFile.Name = "linkSourceFile";
            this.linkSourceFile.Size = new System.Drawing.Size(400, 13);
            this.linkSourceFile.TabIndex = 4;
            this.linkSourceFile.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.linkSourceFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkSourceFile_LinkClicked);
            // 
            // cbShowUsing
            // 
            this.cbShowUsing.AutoSize = true;
            this.cbShowUsing.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbShowUsing.Location = new System.Drawing.Point(0, 186);
            this.cbShowUsing.Name = "cbShowUsing";
            this.cbShowUsing.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.cbShowUsing.Size = new System.Drawing.Size(1014, 17);
            this.cbShowUsing.TabIndex = 3;
            this.cbShowUsing.Text = "Show using directives";
            this.cbShowUsing.UseVisualStyleBackColor = true;
            this.cbShowUsing.CheckedChanged += new System.EventHandler(this.cbShowUsing_CheckedChanged);
            // 
            // cmEdit
            // 
            this.cmEdit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.cmEdit.Name = "cmEdit";
            this.cmEdit.Size = new System.Drawing.Size(123, 48);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // cmZoomTo
            // 
            this.cmZoomTo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToToolStripMenuItem,
            this.openSourceFileToolStripMenuItem,
            this.collapseAllToolStripMenuItem,
            this.expandAllToolStripMenuItem});
            this.cmZoomTo.Name = "cmZoomTo";
            this.cmZoomTo.Size = new System.Drawing.Size(164, 92);
            // 
            // zoomToToolStripMenuItem
            // 
            this.zoomToToolStripMenuItem.Name = "zoomToToolStripMenuItem";
            this.zoomToToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.zoomToToolStripMenuItem.Text = "Zoom To";
            this.zoomToToolStripMenuItem.Click += new System.EventHandler(this.zoomToToolStripMenuItem_Click);
            // 
            // openSourceFileToolStripMenuItem
            // 
            this.openSourceFileToolStripMenuItem.Name = "openSourceFileToolStripMenuItem";
            this.openSourceFileToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.openSourceFileToolStripMenuItem.Text = "Open Source File";
            this.openSourceFileToolStripMenuItem.Click += new System.EventHandler(this.openSourceFileToolStripMenuItem_Click);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // cmParent
            // 
            this.cmParent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandToolStripMenuItem,
            this.collapseToolStripMenuItem,
            this.parentExpandAllToolStripMenuItem,
            this.parentCollapseAllToolStripMenuItem});
            this.cmParent.Name = "cmParent";
            this.cmParent.Size = new System.Drawing.Size(137, 92);
            // 
            // expandToolStripMenuItem
            // 
            this.expandToolStripMenuItem.Name = "expandToolStripMenuItem";
            this.expandToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.expandToolStripMenuItem.Text = "Expand";
            this.expandToolStripMenuItem.Click += new System.EventHandler(this.expandToolStripMenuItem_Click);
            // 
            // collapseToolStripMenuItem
            // 
            this.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem";
            this.collapseToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.collapseToolStripMenuItem.Text = "Collapse";
            this.collapseToolStripMenuItem.Click += new System.EventHandler(this.collapseToolStripMenuItem_Click);
            // 
            // parentExpandAllToolStripMenuItem
            // 
            this.parentExpandAllToolStripMenuItem.Name = "parentExpandAllToolStripMenuItem";
            this.parentExpandAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.parentExpandAllToolStripMenuItem.Text = "Expand All";
            this.parentExpandAllToolStripMenuItem.Click += new System.EventHandler(this.parentExpandAllToolStripMenuItem_Click);
            // 
            // parentCollapseAllToolStripMenuItem
            // 
            this.parentCollapseAllToolStripMenuItem.Name = "parentCollapseAllToolStripMenuItem";
            this.parentCollapseAllToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.parentCollapseAllToolStripMenuItem.Text = "Collapse All";
            this.parentCollapseAllToolStripMenuItem.Click += new System.EventHandler(this.parentCollapseAllToolStripMenuItem_Click);
            // 
            // ilContextMenu
            // 
            this.ilContextMenu.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilContextMenu.ImageStream")));
            this.ilContextMenu.TransparentColor = System.Drawing.Color.Transparent;
            this.ilContextMenu.Images.SetKeyName(0, "ExpandAll");
            this.ilContextMenu.Images.SetKeyName(1, "CollapseAll");
            this.ilContextMenu.Images.SetKeyName(2, "Copy");
            this.ilContextMenu.Images.SetKeyName(3, "SelectAll");
            // 
            // HowToForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 732);
            this.Controls.Add(this.splitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HowToForm";
            this.Text = "GraphicsHowTo";
            this.Load += new System.EventHandler(this.HowToForm_Load);
            this.Shown += new System.EventHandler(this.HowToForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HowToForm_FormClosing);
            this.Resize += new System.EventHandler(this.HowToForm_Resize);
            this.splitTreeAnd3D.Panel1.ResumeLayout(false);
            this.splitTreeAnd3D.Panel2.ResumeLayout(false);
            this.splitTreeAnd3D.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            this.splitContainer.ResumeLayout(false);
            this.cmEdit.ResumeLayout(false);
            this.cmZoomTo.ResumeLayout(false);
            this.cmParent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AGI.STKX.Controls.AxAgUiAxVOCntrl axAgUiAxVOCntrl1;
        private System.Windows.Forms.TreeView tvCode;
        private System.Windows.Forms.RichTextBox rtbCode;
        private System.Windows.Forms.SplitContainer splitTreeAnd3D;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ContextMenuStrip cmEdit;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmZoomTo;
        private System.Windows.Forms.ToolStripMenuItem zoomToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSourceFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmParent;
        private System.Windows.Forms.ToolStripMenuItem expandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parentExpandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parentCollapseAllToolStripMenuItem;
        private System.Windows.Forms.ImageList ilTree;
        private System.Windows.Forms.ImageList ilContextMenu;
        private System.Windows.Forms.CheckBox cbShowUsing;
        private System.Windows.Forms.LinkLabel linkSourceFile;
    }
}


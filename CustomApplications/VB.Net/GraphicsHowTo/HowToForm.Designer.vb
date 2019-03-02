Imports AGI.STKX.Controls

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class HowToForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HowToForm))
        Me.AxAgUiAxVOCntrl1 = New AxAgUiAxVOCntrl()
        Me.tvCode = New System.Windows.Forms.TreeView()
        Me.ilTree = New System.Windows.Forms.ImageList(Me.components)
        Me.rtbCode = New System.Windows.Forms.RichTextBox()
        Me.splitTreeAnd3D = New System.Windows.Forms.SplitContainer()
        Me.splitContainer = New System.Windows.Forms.SplitContainer()
        Me.linkSourceFile = New System.Windows.Forms.LinkLabel()
        Me.cbShowUsing = New System.Windows.Forms.CheckBox()
        Me.cmEdit = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.copyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.selectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmZoomTo = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.zoomToToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.openSourceFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.collapseAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.expandAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmParent = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.expandToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.collapseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.parentExpandAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.parentCollapseAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ilContextMenu = New System.Windows.Forms.ImageList(Me.components)
        Me.splitTreeAnd3D.Panel1.SuspendLayout()
        Me.splitTreeAnd3D.Panel2.SuspendLayout()
        Me.splitTreeAnd3D.SuspendLayout()
        Me.splitContainer.Panel1.SuspendLayout()
        Me.splitContainer.Panel2.SuspendLayout()
        Me.splitContainer.SuspendLayout()
        Me.cmEdit.SuspendLayout()
        Me.cmZoomTo.SuspendLayout()
        Me.cmParent.SuspendLayout()
        Me.SuspendLayout()
        '
        'AxAgUiAxVOCntrl1
        '
        Me.AxAgUiAxVOCntrl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxAgUiAxVOCntrl1.Enabled = True
        Me.AxAgUiAxVOCntrl1.Location = New System.Drawing.Point(0, 0)
        Me.AxAgUiAxVOCntrl1.Name = "AxAgUiAxVOCntrl1"
        Me.AxAgUiAxVOCntrl1.Size = New System.Drawing.Size(660, 525)
        Me.AxAgUiAxVOCntrl1.TabIndex = 0
        '
        'tvCode
        '
        Me.tvCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tvCode.HideSelection = False
        Me.tvCode.ImageIndex = 0
        Me.tvCode.ImageList = Me.ilTree
        Me.tvCode.Location = New System.Drawing.Point(0, 0)
        Me.tvCode.Name = "tvCode"
        Me.tvCode.SelectedImageIndex = 0
        Me.tvCode.Size = New System.Drawing.Size(350, 525)
        Me.tvCode.TabIndex = 1
        '
        'ilTree
        '
        Me.ilTree.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ilTree.ImageSize = New System.Drawing.Size(16, 16)
        Me.ilTree.TransparentColor = System.Drawing.Color.Transparent
        '
        'rtbCode
        '
        Me.rtbCode.AcceptsTab = True
        Me.rtbCode.BackColor = System.Drawing.SystemColors.Control
        Me.rtbCode.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbCode.Location = New System.Drawing.Point(0, 0)
        Me.rtbCode.Name = "rtbCode"
        Me.rtbCode.ReadOnly = True
        Me.rtbCode.Size = New System.Drawing.Size(1014, 203)
        Me.rtbCode.TabIndex = 2
        Me.rtbCode.Text = ""
        Me.rtbCode.WordWrap = False
        '
        'splitTreeAnd3D
        '
        Me.splitTreeAnd3D.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitTreeAnd3D.Location = New System.Drawing.Point(0, 0)
        Me.splitTreeAnd3D.Name = "splitTreeAnd3D"
        '
        'splitTreeAnd3D.Panel1
        '
        Me.splitTreeAnd3D.Panel1.Controls.Add(Me.tvCode)
        '
        'splitTreeAnd3D.Panel2
        '
        Me.splitTreeAnd3D.Panel2.Controls.Add(Me.AxAgUiAxVOCntrl1)
        Me.splitTreeAnd3D.Size = New System.Drawing.Size(1014, 525)
        Me.splitTreeAnd3D.SplitterDistance = 350
        Me.splitTreeAnd3D.TabIndex = 3
        '
        'splitContainer
        '
        Me.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitContainer.Location = New System.Drawing.Point(0, 0)
        Me.splitContainer.Name = "splitContainer"
        Me.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainer.Panel1
        '
        Me.splitContainer.Panel1.Controls.Add(Me.splitTreeAnd3D)
        '
        'splitContainer.Panel2
        '
        Me.splitContainer.Panel2.Controls.Add(Me.linkSourceFile)
        Me.splitContainer.Panel2.Controls.Add(Me.cbShowUsing)
        Me.splitContainer.Panel2.Controls.Add(Me.rtbCode)
        Me.splitContainer.Size = New System.Drawing.Size(1014, 732)
        Me.splitContainer.SplitterDistance = 525
        Me.splitContainer.TabIndex = 4
        '
        'linkSourceFile
        '
        Me.linkSourceFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.linkSourceFile.Location = New System.Drawing.Point(606, 186)
        Me.linkSourceFile.Name = "linkSourceFile"
        Me.linkSourceFile.Size = New System.Drawing.Size(400, 13)
        Me.linkSourceFile.TabIndex = 4
        Me.linkSourceFile.TextAlign = System.Drawing.ContentAlignment.BottomRight
        '
        'cbShowUsing
        '
        Me.cbShowUsing.AutoSize = True
        Me.cbShowUsing.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.cbShowUsing.Location = New System.Drawing.Point(0, 186)
        Me.cbShowUsing.Name = "cbShowUsing"
        Me.cbShowUsing.Padding = New System.Windows.Forms.Padding(4, 0, 0, 0)
        Me.cbShowUsing.Size = New System.Drawing.Size(1014, 17)
        Me.cbShowUsing.TabIndex = 3
        Me.cbShowUsing.Text = "Show using directives"
        Me.cbShowUsing.UseVisualStyleBackColor = True
        '
        'cmEdit
        '
        Me.cmEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.copyToolStripMenuItem, Me.selectAllToolStripMenuItem})
        Me.cmEdit.Name = "cmEdit"
        Me.cmEdit.Size = New System.Drawing.Size(123, 48)
        '
        'copyToolStripMenuItem
        '
        Me.copyToolStripMenuItem.Name = "copyToolStripMenuItem"
        Me.copyToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.copyToolStripMenuItem.Text = "Copy"
        '
        'selectAllToolStripMenuItem
        '
        Me.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem"
        Me.selectAllToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.selectAllToolStripMenuItem.Text = "Select All"
        '
        'cmZoomTo
        '
        Me.cmZoomTo.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.zoomToToolStripMenuItem, Me.openSourceFileToolStripMenuItem, Me.collapseAllToolStripMenuItem, Me.expandAllToolStripMenuItem})
        Me.cmZoomTo.Name = "cmZoomTo"
        Me.cmZoomTo.Size = New System.Drawing.Size(164, 92)
        '
        'zoomToToolStripMenuItem
        '
        Me.zoomToToolStripMenuItem.Name = "zoomToToolStripMenuItem"
        Me.zoomToToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.zoomToToolStripMenuItem.Text = "Zoom To"
        '
        'openSourceFileToolStripMenuItem
        '
        Me.openSourceFileToolStripMenuItem.Name = "openSourceFileToolStripMenuItem"
        Me.openSourceFileToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.openSourceFileToolStripMenuItem.Text = "Open Source File"
        '
        'collapseAllToolStripMenuItem
        '
        Me.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem"
        Me.collapseAllToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.collapseAllToolStripMenuItem.Text = "Collapse All"
        '
        'expandAllToolStripMenuItem
        '
        Me.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem"
        Me.expandAllToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.expandAllToolStripMenuItem.Text = "Expand All"
        '
        'cmParent
        '
        Me.cmParent.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.expandToolStripMenuItem, Me.collapseToolStripMenuItem, Me.parentExpandAllToolStripMenuItem, Me.parentCollapseAllToolStripMenuItem})
        Me.cmParent.Name = "cmParent"
        Me.cmParent.Size = New System.Drawing.Size(137, 92)
        '
        'expandToolStripMenuItem
        '
        Me.expandToolStripMenuItem.Name = "expandToolStripMenuItem"
        Me.expandToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.expandToolStripMenuItem.Text = "Expand"
        '
        'collapseToolStripMenuItem
        '
        Me.collapseToolStripMenuItem.Name = "collapseToolStripMenuItem"
        Me.collapseToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.collapseToolStripMenuItem.Text = "Collapse"
        '
        'parentExpandAllToolStripMenuItem
        '
        Me.parentExpandAllToolStripMenuItem.Name = "parentExpandAllToolStripMenuItem"
        Me.parentExpandAllToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.parentExpandAllToolStripMenuItem.Text = "Expand All"
        '
        'parentCollapseAllToolStripMenuItem
        '
        Me.parentCollapseAllToolStripMenuItem.Name = "parentCollapseAllToolStripMenuItem"
        Me.parentCollapseAllToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.parentCollapseAllToolStripMenuItem.Text = "Collapse All"
        '
        'ilContextMenu
        '
        Me.ilContextMenu.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ilContextMenu.ImageSize = New System.Drawing.Size(16, 16)
        Me.ilContextMenu.TransparentColor = System.Drawing.Color.Transparent
        '
        'HowToForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1014, 732)
        Me.Controls.Add(Me.splitContainer)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "HowToForm"
        Me.Text = "GraphicsHowTo"
        Me.splitTreeAnd3D.Panel1.ResumeLayout(False)
        Me.splitTreeAnd3D.Panel2.ResumeLayout(False)
        Me.splitTreeAnd3D.ResumeLayout(False)
        Me.splitContainer.Panel1.ResumeLayout(False)
        Me.splitContainer.Panel2.ResumeLayout(False)
        Me.splitContainer.Panel2.PerformLayout()
        Me.splitContainer.ResumeLayout(False)
        Me.cmEdit.ResumeLayout(False)
        Me.cmZoomTo.ResumeLayout(False)
        Me.cmParent.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents AxAgUiAxVOCntrl1 As AxAgUiAxVOCntrl
    Friend WithEvents tvCode As System.Windows.Forms.TreeView
    Friend WithEvents rtbCode As System.Windows.Forms.RichTextBox
    Friend WithEvents splitTreeAnd3D As System.Windows.Forms.SplitContainer
    Friend WithEvents splitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents cmEdit As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents copyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents selectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmZoomTo As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents zoomToToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents openSourceFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents collapseAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents expandAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmParent As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents expandToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents collapseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents parentExpandAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents parentCollapseAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ilTree As System.Windows.Forms.ImageList
    Friend WithEvents ilContextMenu As System.Windows.Forms.ImageList
    Friend WithEvents cbShowUsing As System.Windows.Forms.CheckBox
    Friend WithEvents linkSourceFile As System.Windows.Forms.LinkLabel

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OnAnimUpdate
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents AxAgUiAxVOCntrl1 As AGI.STKX.Controls.AxAgUiAxVOCntrl
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents PopupMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents OpenScenario As System.Windows.Forms.OpenFileDialog
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OnAnimUpdate))
        Me.StatusBar1 = New System.Windows.Forms.StatusBar
        Me.PopupMenu = New System.Windows.Forms.ContextMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.MenuItem7 = New System.Windows.Forms.MenuItem
        Me.MenuItem8 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.OpenScenario = New System.Windows.Forms.OpenFileDialog
        Me.AxAgUiAxVOCntrl1 = New AGI.STKX.Controls.AxAgUiAxVOCntrl
        Me.SuspendLayout()
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 483)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(691, 22)
        Me.StatusBar1.TabIndex = 1
        '
        'PopupMenu
        '
        Me.PopupMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Load Scenario"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem4, Me.MenuItem5, Me.MenuItem6, Me.MenuItem7, Me.MenuItem8})
        Me.MenuItem2.Text = "Animation..."
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 0
        Me.MenuItem4.Text = "Start"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 1
        Me.MenuItem5.Text = "Pause"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 2
        Me.MenuItem6.Text = "Reset"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 3
        Me.MenuItem7.Text = "Faster"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 4
        Me.MenuItem8.Text = "Slower"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem9, Me.MenuItem10})
        Me.MenuItem3.Text = "View..."
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 0
        Me.MenuItem9.Text = "Zoom In"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 1
        Me.MenuItem10.Text = "Home View"
        '
        'OpenScenario
        '
        Me.OpenScenario.Filter = "Scenario Files | *.sc"
        Me.OpenScenario.InitialDirectory = "C:\My Documents"
        Me.OpenScenario.Title = "Open STK Scenario..."
        '
        'AxAgUiAxVOCntrl1
        '
        Me.AxAgUiAxVOCntrl1.Enabled = True
        Me.AxAgUiAxVOCntrl1.Location = New System.Drawing.Point(0, 0)
        Me.AxAgUiAxVOCntrl1.Name = "AxAgUiAxVOCntrl1"
        Me.AxAgUiAxVOCntrl1.Size = New System.Drawing.Size(691, 477)
        Me.AxAgUiAxVOCntrl1.TabIndex = 0
        '
        'OnAnimUpdate
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(691, 505)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.AxAgUiAxVOCntrl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OnAnimUpdate"
        Me.Text = "OnAnimUpdate"
        Me.ResumeLayout(False)

    End Sub

End Class

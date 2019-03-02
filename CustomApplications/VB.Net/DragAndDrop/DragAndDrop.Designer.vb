<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    Public Sub New()
        MyBase.New()
        If m_vb6FormDefInstance Is Nothing Then
            If m_InitializingDefInstance Then
                m_vb6FormDefInstance = Me
            Else
                Try
                    'For the start-up form, the first instance created is the default instance.
                    If System.Reflection.Assembly.GetExecutingAssembly.EntryPoint.DeclaringType Is Me.GetType Then
                        m_vb6FormDefInstance = Me
                    End If
                Catch
                End Try
            End If
        End If
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Private stkRootObject As AGI.STKObjects.IAgStkObjectRoot = Nothing
    Public WithEvents AgUiAx2DCntrl1 As AGI.STKX.Controls.AxAgUiAx2DCntrl
    Public WithEvents AgUiAxVOCntrl1 As AGI.STKX.Controls.AxAgUiAxVOCntrl
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents PopupMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.AgUiAx2DCntrl1 = New AGI.STKX.Controls.AxAgUiAx2DCntrl()
        Me.AgUiAxVOCntrl1 = New AGI.STKX.Controls.AxAgUiAxVOCntrl()
        Me.PopupMenu = New System.Windows.Forms.ContextMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.MenuItem9 = New System.Windows.Forms.MenuItem()
        Me.SuspendLayout()
        '
        'AgUiAx2DCntrl1
        '
        Me.AgUiAx2DCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AgUiAx2DCntrl1.Location = New System.Drawing.Point(0, 0)
        Me.AgUiAx2DCntrl1.Name = "AgUiAx2DCntrl1"
        Me.AgUiAx2DCntrl1.NoLogo = True
        Me.AgUiAx2DCntrl1.PanModeEnabled = True
        Me.AgUiAx2DCntrl1.Picture = CType(resources.GetObject("AgUiAx2DCntrl1.Picture"), System.Drawing.Image)
        Me.AgUiAx2DCntrl1.Size = New System.Drawing.Size(833, 546)
        Me.AgUiAx2DCntrl1.TabIndex = 1
        Me.AgUiAx2DCntrl1.Visible = False
        '
        'AgUiAxVOCntrl1
        '
        Me.AgUiAxVOCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AgUiAxVOCntrl1.Location = New System.Drawing.Point(0, 0)
        Me.AgUiAxVOCntrl1.Name = "AgUiAxVOCntrl1"
        Me.AgUiAxVOCntrl1.Picture = CType(resources.GetObject("AgUiAxVOCntrl1.Picture"), System.Drawing.Image)
        Me.AgUiAxVOCntrl1.Size = New System.Drawing.Size(513, 313)
        Me.AgUiAxVOCntrl1.TabIndex = 0
        '
        'PopupMenu
        '
        Me.PopupMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItem7, Me.MenuItem8, Me.MenuItem9})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "2D View"
        '
        'MenuItem2
        '
        Me.MenuItem2.Checked = True
        Me.MenuItem2.DefaultItem = True
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "3D View"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem4, Me.MenuItem5, Me.MenuItem6})
        Me.MenuItem3.Text = "Drop Mode"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 0
        Me.MenuItem4.Text = "Automatic"
        '
        'MenuItem5
        '
        Me.MenuItem5.Checked = True
        Me.MenuItem5.DefaultItem = True
        Me.MenuItem5.Index = 1
        Me.MenuItem5.Text = "Manual"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 2
        Me.MenuItem6.Text = "None"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 3
        Me.MenuItem7.Text = "Zoom In"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 4
        Me.MenuItem8.Text = "Home View"
        '
        'MenuItem9
        '
        Me.MenuItem9.Enabled = False
        Me.MenuItem9.Index = 5
        Me.MenuItem9.Text = "Zoom Out"
        Me.MenuItem9.Visible = False
        '
        'Form1
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(838, 549)
        Me.Controls.Add(Me.AgUiAx2DCntrl1)
        Me.Controls.Add(Me.AgUiAxVOCntrl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Drag & Drop Example"
        Me.ResumeLayout(False)

    End Sub

End Class

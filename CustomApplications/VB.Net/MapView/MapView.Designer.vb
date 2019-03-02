<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MapView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MapView))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Check1 = New System.Windows.Forms.CheckBox()
        Me.Command5 = New System.Windows.Forms.Button()
        Me.Command4 = New System.Windows.Forms.Button()
        Me.Command1 = New System.Windows.Forms.Button()
        Me.AgUiAx2DCntrl1 = New AGI.STKX.Controls.AxAgUiAx2DCntrl()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'Check1
        '
        Me.Check1.BackColor = System.Drawing.SystemColors.Control
        Me.Check1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Check1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Check1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Check1.Location = New System.Drawing.Point(819, 75)
        Me.Check1.Name = "Check1"
        Me.Check1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Check1.Size = New System.Drawing.Size(105, 25)
        Me.Check1.TabIndex = 4
        Me.Check1.Text = "Allow Pan"
        Me.Check1.UseVisualStyleBackColor = False
        '
        'Command5
        '
        Me.Command5.BackColor = System.Drawing.SystemColors.Control
        Me.Command5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Command5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command5.Location = New System.Drawing.Point(819, 163)
        Me.Command5.Name = "Command5"
        Me.Command5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command5.Size = New System.Drawing.Size(97, 49)
        Me.Command5.TabIndex = 3
        Me.Command5.Text = "Zoom Out"
        Me.Command5.UseVisualStyleBackColor = False
        '
        'Command4
        '
        Me.Command4.BackColor = System.Drawing.SystemColors.Control
        Me.Command4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Command4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command4.Location = New System.Drawing.Point(819, 107)
        Me.Command4.Name = "Command4"
        Me.Command4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command4.Size = New System.Drawing.Size(97, 49)
        Me.Command4.TabIndex = 2
        Me.Command4.Text = "Zoom In"
        Me.Command4.UseVisualStyleBackColor = False
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(819, 11)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(97, 49)
        Me.Command1.TabIndex = 1
        Me.Command1.Text = "Open Scenario"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'AgUiAx2DCntrl1
        '
        Me.AgUiAx2DCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AgUiAx2DCntrl1.Location = New System.Drawing.Point(12, 12)
        Me.AgUiAx2DCntrl1.MinimumSize = New System.Drawing.Size(400, 200)
        Me.AgUiAx2DCntrl1.Name = "AgUiAx2DCntrl1"
        Me.AgUiAx2DCntrl1.PanModeEnabled = True
        Me.AgUiAx2DCntrl1.Picture = CType(resources.GetObject("AgUiAx2DCntrl1.Picture"), System.Drawing.Image)
        Me.AgUiAx2DCntrl1.Size = New System.Drawing.Size(800, 400)
        Me.AgUiAx2DCntrl1.TabIndex = 5
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'MapView
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(930, 447)
        Me.Controls.Add(Me.AgUiAx2DCntrl1)
        Me.Controls.Add(Me.Check1)
        Me.Controls.Add(Me.Command5)
        Me.Controls.Add(Me.Command4)
        Me.Controls.Add(Me.Command1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MapView"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "MapView"
        Me.ResumeLayout(False)

    End Sub

    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents AgUiAx2DCntrl1 As AGI.STKX.Controls.AxAgUiAx2DCntrl
    Public WithEvents Check1 As System.Windows.Forms.CheckBox
    Public WithEvents Command5 As System.Windows.Forms.Button
    Public WithEvents Command4 As System.Windows.Forms.Button
    Public WithEvents Command1 As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog

End Class

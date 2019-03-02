<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VbEventsForm
    Inherits System.Windows.Forms.Form

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
    Public WithEvents AgUiAx2DCntrl1 As AGI.STKX.Controls.AxAgUiAx2DCntrl
    Public WithEvents Command4 As System.Windows.Forms.Button
    Public WithEvents AgUiAxVOCntrl1 As AGI.STKX.Controls.AxAgUiAxVOCntrl
    Public WithEvents Command3 As System.Windows.Forms.Button
    Public WithEvents Command2 As System.Windows.Forms.Button
    Public WithEvents Command1 As System.Windows.Forms.Button
    Public WithEvents Trace As System.Windows.Forms.TextBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VbEventsForm))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Command4 = New System.Windows.Forms.Button()
        Me.Command3 = New System.Windows.Forms.Button()
        Me.Command2 = New System.Windows.Forms.Button()
        Me.Command1 = New System.Windows.Forms.Button()
        Me.Trace = New System.Windows.Forms.TextBox()
        Me.AgUiAx2DCntrl1 = New AGI.STKX.Controls.AxAgUiAx2DCntrl()
        Me.AgUiAxVOCntrl1 = New AGI.STKX.Controls.AxAgUiAxVOCntrl()
        Me.Command6 = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'Command4
        '
        Me.Command4.BackColor = System.Drawing.SystemColors.Control
        Me.Command4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Command4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command4.Location = New System.Drawing.Point(250, 8)
        Me.Command4.Name = "Command4"
        Me.Command4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command4.Size = New System.Drawing.Size(113, 25)
        Me.Command4.TabIndex = 6
        Me.Command4.Text = "3D - Zoom In"
        Me.Command4.UseVisualStyleBackColor = False
        '
        'Command3
        '
        Me.Command3.BackColor = System.Drawing.SystemColors.Control
        Me.Command3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Command3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command3.Location = New System.Drawing.Point(642, 8)
        Me.Command3.Name = "Command3"
        Me.Command3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command3.Size = New System.Drawing.Size(113, 25)
        Me.Command3.TabIndex = 3
        Me.Command3.Text = "2D - Zoom Out"
        Me.Command3.UseVisualStyleBackColor = False
        '
        'Command2
        '
        Me.Command2.BackColor = System.Drawing.SystemColors.Control
        Me.Command2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Command2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command2.Location = New System.Drawing.Point(538, 8)
        Me.Command2.Name = "Command2"
        Me.Command2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command2.Size = New System.Drawing.Size(97, 25)
        Me.Command2.TabIndex = 2
        Me.Command2.Text = "2D - Zoom In"
        Me.Command2.UseVisualStyleBackColor = False
        '
        'Command1
        '
        Me.Command1.BackColor = System.Drawing.SystemColors.Control
        Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command1.Location = New System.Drawing.Point(12, 8)
        Me.Command1.Name = "Command1"
        Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command1.Size = New System.Drawing.Size(113, 25)
        Me.Command1.TabIndex = 1
        Me.Command1.Text = "Open Scenario"
        Me.Command1.UseVisualStyleBackColor = False
        '
        'Trace
        '
        Me.Trace.AcceptsReturn = True
        Me.Trace.BackColor = System.Drawing.SystemColors.Window
        Me.Trace.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Trace.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Trace.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Trace.Location = New System.Drawing.Point(8, 494)
        Me.Trace.MaxLength = 0
        Me.Trace.Multiline = True
        Me.Trace.Name = "Trace"
        Me.Trace.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Trace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Trace.Size = New System.Drawing.Size(951, 264)
        Me.Trace.TabIndex = 0
        '
        'AgUiAx2DCntrl1
        '
        Me.AgUiAx2DCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AgUiAx2DCntrl1.Location = New System.Drawing.Point(459, 48)
        Me.AgUiAx2DCntrl1.Name = "AgUiAx2DCntrl1"
        Me.AgUiAx2DCntrl1.PanModeEnabled = True
        Me.AgUiAx2DCntrl1.Picture = CType(resources.GetObject("AgUiAx2DCntrl1.Picture"), System.Drawing.Image)
        Me.AgUiAx2DCntrl1.Size = New System.Drawing.Size(500, 440)
        Me.AgUiAx2DCntrl1.TabIndex = 5
        '
        'AgUiAxVOCntrl1
        '
        Me.AgUiAxVOCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AgUiAxVOCntrl1.Location = New System.Drawing.Point(12, 48)
        Me.AgUiAxVOCntrl1.Name = "AgUiAxVOCntrl1"
        Me.AgUiAxVOCntrl1.Picture = CType(resources.GetObject("AgUiAxVOCntrl1.Picture"), System.Drawing.Image)
        Me.AgUiAxVOCntrl1.Size = New System.Drawing.Size(451, 440)
        Me.AgUiAxVOCntrl1.TabIndex = 4
        '
        'Command6
        '
        Me.Command6.BackColor = System.Drawing.SystemColors.Control
        Me.Command6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Command6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Command6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Command6.Location = New System.Drawing.Point(131, 8)
        Me.Command6.Name = "Command6"
        Me.Command6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Command6.Size = New System.Drawing.Size(113, 25)
        Me.Command6.TabIndex = 8
        Me.Command6.Text = "Save Scenario"
        Me.Command6.UseVisualStyleBackColor = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Enabled = False
        Me.CheckBox1.Location = New System.Drawing.Point(765, 10)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(73, 18)
        Me.CheckBox1.TabIndex = 9
        Me.CheckBox1.Text = "Pan Mode"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'VbEventsForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(962, 764)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Command6)
        Me.Controls.Add(Me.AgUiAx2DCntrl1)
        Me.Controls.Add(Me.Command4)
        Me.Controls.Add(Me.AgUiAxVOCntrl1)
        Me.Controls.Add(Me.Command3)
        Me.Controls.Add(Me.Command2)
        Me.Controls.Add(Me.Command1)
        Me.Controls.Add(Me.Trace)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "VbEventsForm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "VB.NET – STK/X Globe & Map controls – Mouse & Keyboard events"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Command6 As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox

End Class

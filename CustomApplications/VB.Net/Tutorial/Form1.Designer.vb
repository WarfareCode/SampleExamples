<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

        stkxApp = New AGI.STKX.AgSTKXApplication

    End Sub

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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents AxAgUiAx2DCntrl1 As AGI.STKX.Controls.AxAgUiAx2DCntrl
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.AxAgUiAxVOCntrl1 = New AGI.STKX.Controls.AxAgUiAxVOCntrl()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.AxAgUiAx2DCntrl1 = New AGI.STKX.Controls.AxAgUiAx2DCntrl()
        Me.SuspendLayout()
        '
        'AxAgUiAxVOCntrl1
        '
        Me.AxAgUiAxVOCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AxAgUiAxVOCntrl1.Location = New System.Drawing.Point(493, 7)
        Me.AxAgUiAxVOCntrl1.Name = "AxAgUiAxVOCntrl1"
        Me.AxAgUiAxVOCntrl1.Picture = CType(resources.GetObject("AxAgUiAxVOCntrl1.Picture"), System.Drawing.Image)
        Me.AxAgUiAxVOCntrl1.Size = New System.Drawing.Size(454, 610)
        Me.AxAgUiAxVOCntrl1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(20, 430)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(120, 62)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "New Scenario"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(20, 499)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(120, 63)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Zoom In"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(20, 569)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(120, 62)
        Me.Button3.TabIndex = 4
        Me.Button3.Text = "Zoom Out"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(227, 430)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(246, 201)
        Me.Label1.TabIndex = 5
        '
        'AxAgUiAx2DCntrl1
        '
        Me.AxAgUiAx2DCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AxAgUiAx2DCntrl1.Location = New System.Drawing.Point(7, 7)
        Me.AxAgUiAx2DCntrl1.Name = "AxAgUiAx2DCntrl1"
        Me.AxAgUiAx2DCntrl1.PanModeEnabled = True
        Me.AxAgUiAx2DCntrl1.Picture = CType(resources.GetObject("AxAgUiAx2DCntrl1.Picture"), System.Drawing.Image)
        Me.AxAgUiAx2DCntrl1.Size = New System.Drawing.Size(466, 409)
        Me.AxAgUiAx2DCntrl1.TabIndex = 6
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(953, 651)
        Me.Controls.Add(Me.AxAgUiAx2DCntrl1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.AxAgUiAxVOCntrl1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub

End Class

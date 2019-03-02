<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.button9 = New System.Windows.Forms.Button
        Me.button8 = New System.Windows.Forms.Button
        Me.button7 = New System.Windows.Forms.Button
        Me.button6 = New System.Windows.Forms.Button
        Me.button5 = New System.Windows.Forms.Button
        Me.button4 = New System.Windows.Forms.Button
        Me.button3 = New System.Windows.Forms.Button
        Me.button2 = New System.Windows.Forms.Button
        Me.button1 = New System.Windows.Forms.Button
        Me.AxAgUiAxVOCntrl1 = New AGI.STKX.Controls.AxAgUiAxVOCntrl
        Me.AxAgUiAxVOCntrl2 = New AGI.STKX.Controls.AxAgUiAxVOCntrl
        Me.SuspendLayout()
        '
        'button9
        '
        Me.button9.Enabled = False
        Me.button9.Location = New System.Drawing.Point(819, 321)
        Me.button9.Name = "button9"
        Me.button9.Size = New System.Drawing.Size(109, 23)
        Me.button9.TabIndex = 19
        Me.button9.Text = "Propagate the Orbit"
        Me.button9.UseVisualStyleBackColor = True
        '
        'button8
        '
        Me.button8.Enabled = False
        Me.button8.Location = New System.Drawing.Point(818, 277)
        Me.button8.Name = "button8"
        Me.button8.Size = New System.Drawing.Size(110, 37)
        Me.button8.TabIndex = 18
        Me.button8.Text = "Target a Circular Orbit"
        Me.button8.UseVisualStyleBackColor = True
        '
        'button7
        '
        Me.button7.Enabled = False
        Me.button7.Location = New System.Drawing.Point(819, 231)
        Me.button7.Name = "button7"
        Me.button7.Size = New System.Drawing.Size(109, 40)
        Me.button7.TabIndex = 17
        Me.button7.Text = "Create a Mars Point Mass Propagator"
        Me.button7.UseVisualStyleBackColor = True
        '
        'button6
        '
        Me.button6.Enabled = False
        Me.button6.Location = New System.Drawing.Point(819, 201)
        Me.button6.Name = "button6"
        Me.button6.Size = New System.Drawing.Size(109, 23)
        Me.button6.TabIndex = 16
        Me.button6.Text = "Stop Near Mars"
        Me.button6.UseVisualStyleBackColor = True
        '
        'button5
        '
        Me.button5.Enabled = False
        Me.button5.Location = New System.Drawing.Point(819, 144)
        Me.button5.Name = "button5"
        Me.button5.Size = New System.Drawing.Size(109, 50)
        Me.button5.TabIndex = 15
        Me.button5.Text = "Propagating the Interplanetary Trajectory"
        Me.button5.UseVisualStyleBackColor = True
        '
        'button4
        '
        Me.button4.Enabled = False
        Me.button4.Location = New System.Drawing.Point(819, 102)
        Me.button4.Name = "button4"
        Me.button4.Size = New System.Drawing.Size(109, 35)
        Me.button4.TabIndex = 14
        Me.button4.Text = "Define the Initial State"
        Me.button4.UseVisualStyleBackColor = True
        '
        'button3
        '
        Me.button3.Enabled = False
        Me.button3.Location = New System.Drawing.Point(819, 72)
        Me.button3.Name = "button3"
        Me.button3.Size = New System.Drawing.Size(109, 23)
        Me.button3.TabIndex = 13
        Me.button3.Text = "Set up graphics"
        Me.button3.UseVisualStyleBackColor = True
        '
        'button2
        '
        Me.button2.Enabled = False
        Me.button2.Location = New System.Drawing.Point(819, 42)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(109, 23)
        Me.button2.TabIndex = 12
        Me.button2.Text = "Define Planets"
        Me.button2.UseVisualStyleBackColor = True
        '
        'button1
        '
        Me.button1.Location = New System.Drawing.Point(818, 12)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(110, 23)
        Me.button1.TabIndex = 11
        Me.button1.Text = "New Scenario"
        Me.button1.UseVisualStyleBackColor = True
        '
        'AxAgUiAxVOCntrl1
        '
        Me.AxAgUiAxVOCntrl1.Enabled = True
        Me.AxAgUiAxVOCntrl1.Location = New System.Drawing.Point(-3, 0)
        Me.AxAgUiAxVOCntrl1.Name = "AxAgUiAxVOCntrl1"
        Me.AxAgUiAxVOCntrl1.Size = New System.Drawing.Size(816, 404)
        Me.AxAgUiAxVOCntrl1.TabIndex = 20
        '
        'AxAgUiAxVOCntrl2
        '
        Me.AxAgUiAxVOCntrl2.Enabled = True
        Me.AxAgUiAxVOCntrl2.Location = New System.Drawing.Point(-3, 410)
        Me.AxAgUiAxVOCntrl2.Name = "AxAgUiAxVOCntrl2"
        Me.AxAgUiAxVOCntrl2.Size = New System.Drawing.Size(816, 332)
        Me.AxAgUiAxVOCntrl2.TabIndex = 21
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(940, 754)
        Me.Controls.Add(Me.AxAgUiAxVOCntrl2)
        Me.Controls.Add(Me.AxAgUiAxVOCntrl1)
        Me.Controls.Add(Me.button9)
        Me.Controls.Add(Me.button8)
        Me.Controls.Add(Me.button7)
        Me.Controls.Add(Me.button6)
        Me.Controls.Add(Me.button5)
        Me.Controls.Add(Me.button4)
        Me.Controls.Add(Me.button3)
        Me.Controls.Add(Me.button2)
        Me.Controls.Add(Me.button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents button9 As System.Windows.Forms.Button
    Private WithEvents button8 As System.Windows.Forms.Button
    Private WithEvents button7 As System.Windows.Forms.Button
    Private WithEvents button6 As System.Windows.Forms.Button
    Private WithEvents button5 As System.Windows.Forms.Button
    Private WithEvents button4 As System.Windows.Forms.Button
    Private WithEvents button3 As System.Windows.Forms.Button
    Private WithEvents button2 As System.Windows.Forms.Button
    Private WithEvents button1 As System.Windows.Forms.Button
    Friend WithEvents AxAgUiAxVOCntrl1 As AGI.STKX.Controls.AxAgUiAxVOCntrl
    Friend WithEvents AxAgUiAxVOCntrl2 As AGI.STKX.Controls.AxAgUiAxVOCntrl

End Class

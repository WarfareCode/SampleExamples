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
        Me.button8 = New System.Windows.Forms.Button
        Me.button7 = New System.Windows.Forms.Button
        Me.button6 = New System.Windows.Forms.Button
        Me.button5 = New System.Windows.Forms.Button
        Me.button4 = New System.Windows.Forms.Button
        Me.button3 = New System.Windows.Forms.Button
        Me.button2 = New System.Windows.Forms.Button
        Me.button1 = New System.Windows.Forms.Button
        Me.AxAgUiAxVOCntrl1 = New AGI.STKX.Controls.AxAgUiAxVOCntrl
        Me.button10 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'button8
        '
        Me.button8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button8.Enabled = False
        Me.button8.Location = New System.Drawing.Point(775, 329)
        Me.button8.Name = "button8"
        Me.button8.Size = New System.Drawing.Size(105, 23)
        Me.button8.TabIndex = 16
        Me.button8.Text = "Run MCS"
        Me.button8.UseVisualStyleBackColor = True
        '
        'button7
        '
        Me.button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button7.Enabled = False
        Me.button7.Location = New System.Drawing.Point(775, 288)
        Me.button7.Name = "button7"
        Me.button7.Size = New System.Drawing.Size(105, 35)
        Me.button7.TabIndex = 15
        Me.button7.Text = "Propagate the Outer Orbit"
        Me.button7.UseVisualStyleBackColor = True
        '
        'button6
        '
        Me.button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button6.Enabled = False
        Me.button6.Location = New System.Drawing.Point(775, 247)
        Me.button6.Name = "button6"
        Me.button6.Size = New System.Drawing.Size(105, 35)
        Me.button6.TabIndex = 14
        Me.button6.Text = "Maneuver into the Outer Orbit"
        Me.button6.UseVisualStyleBackColor = True
        '
        'button5
        '
        Me.button5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button5.Enabled = False
        Me.button5.Location = New System.Drawing.Point(775, 193)
        Me.button5.Name = "button5"
        Me.button5.Size = New System.Drawing.Size(105, 48)
        Me.button5.TabIndex = 13
        Me.button5.Text = "Propagate the Transfer Orbit to Apogee"
        Me.button5.UseVisualStyleBackColor = True
        '
        'button4
        '
        Me.button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button4.Enabled = False
        Me.button4.Location = New System.Drawing.Point(775, 148)
        Me.button4.Name = "button4"
        Me.button4.Size = New System.Drawing.Size(105, 39)
        Me.button4.TabIndex = 12
        Me.button4.Text = "Maneuver into the Transfer Ellipse"
        Me.button4.UseVisualStyleBackColor = True
        '
        'button3
        '
        Me.button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button3.Enabled = False
        Me.button3.Location = New System.Drawing.Point(775, 104)
        Me.button3.Name = "button3"
        Me.button3.Size = New System.Drawing.Size(105, 38)
        Me.button3.TabIndex = 11
        Me.button3.Text = "Propagate the Parking Orbit"
        Me.button3.UseVisualStyleBackColor = True
        '
        'button2
        '
        Me.button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button2.Enabled = False
        Me.button2.Location = New System.Drawing.Point(775, 57)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(105, 41)
        Me.button2.TabIndex = 10
        Me.button2.Text = "Define the Initial State"
        Me.button2.UseVisualStyleBackColor = True
        '
        'button1
        '
        Me.button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button1.Location = New System.Drawing.Point(775, 27)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(105, 23)
        Me.button1.TabIndex = 9
        Me.button1.Text = "New Scenario"
        Me.button1.UseVisualStyleBackColor = True
        '
        'AxAgUiAxVOCntrl1
        '
        Me.AxAgUiAxVOCntrl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxAgUiAxVOCntrl1.Enabled = True
        Me.AxAgUiAxVOCntrl1.Location = New System.Drawing.Point(0, 0)
        Me.AxAgUiAxVOCntrl1.Name = "AxAgUiAxVOCntrl1"
        Me.AxAgUiAxVOCntrl1.Size = New System.Drawing.Size(750, 624)
        Me.AxAgUiAxVOCntrl1.TabIndex = 17
        '
        'button10
        '
        Me.button10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button10.Enabled = False
        Me.button10.Location = New System.Drawing.Point(775, 358)
        Me.button10.Name = "button10"
        Me.button10.Size = New System.Drawing.Size(104, 23)
        Me.button10.TabIndex = 21
        Me.button10.Text = "Display Results"
        Me.button10.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 624)
        Me.Controls.Add(Me.button10)
        Me.Controls.Add(Me.AxAgUiAxVOCntrl1)
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
    Private WithEvents button8 As System.Windows.Forms.Button
    Private WithEvents button7 As System.Windows.Forms.Button
    Private WithEvents button6 As System.Windows.Forms.Button
    Private WithEvents button5 As System.Windows.Forms.Button
    Private WithEvents button4 As System.Windows.Forms.Button
    Private WithEvents button3 As System.Windows.Forms.Button
    Private WithEvents button2 As System.Windows.Forms.Button
    Private WithEvents button1 As System.Windows.Forms.Button
    Friend WithEvents AxAgUiAxVOCntrl1 As AGI.STKX.Controls.AxAgUiAxVOCntrl
    Private WithEvents button10 As System.Windows.Forms.Button

End Class

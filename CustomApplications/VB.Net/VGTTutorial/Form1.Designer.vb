<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VGTTutorial
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VGTTutorial))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.Button_ResetAnim = New System.Windows.Forms.ToolStripButton
        Me.Button_StepBack = New System.Windows.Forms.ToolStripButton
        Me.Button_ReverseAnim = New System.Windows.Forms.ToolStripButton
        Me.Button_Pause = New System.Windows.Forms.ToolStripButton
        Me.Button_Play = New System.Windows.Forms.ToolStripButton
        Me.Button_StepAhead = New System.Windows.Forms.ToolStripButton
        Me.Button_SlowDown = New System.Windows.Forms.ToolStripButton
        Me.Button_SpeedUp = New System.Windows.Forms.ToolStripButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.CloseScenario1 = New System.Windows.Forms.Button
        Me.CreateAngle = New System.Windows.Forms.Button
        Me.CreatePlane = New System.Windows.Forms.Button
        Me.CreateAxes = New System.Windows.Forms.Button
        Me.ShowVelocity = New System.Windows.Forms.Button
        Me.CreateVector = New System.Windows.Forms.Button
        Me.CreateSatellite = New System.Windows.Forms.Button
        Me.CreateScenario = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.MainLabel = New System.Windows.Forms.Label
        Me.AxAgUiAxVOCntrl1 = New AGI.STKX.Controls.AxAgUiAxVOCntrl
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Button_ResetAnim, Me.Button_StepBack, Me.Button_ReverseAnim, Me.Button_Pause, Me.Button_Play, Me.Button_StepAhead, Me.Button_SlowDown, Me.Button_SpeedUp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(873, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'Button_ResetAnim
        '
        Me.Button_ResetAnim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Button_ResetAnim.Image = Global.VGTTutorial.My.Resources.Resources.reset
        Me.Button_ResetAnim.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Button_ResetAnim.Name = "Button_ResetAnim"
        Me.Button_ResetAnim.Size = New System.Drawing.Size(23, 22)
        Me.Button_ResetAnim.Text = "ToolStripButton1"
        Me.Button_ResetAnim.ToolTipText = "Reset"
        '
        'Button_StepBack
        '
        Me.Button_StepBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Button_StepBack.Image = Global.VGTTutorial.My.Resources.Resources.stepbak
        Me.Button_StepBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Button_StepBack.Name = "Button_StepBack"
        Me.Button_StepBack.Size = New System.Drawing.Size(23, 22)
        Me.Button_StepBack.Text = "ToolStripButton2"
        Me.Button_StepBack.ToolTipText = "Step Backword"
        '
        'Button_ReverseAnim
        '
        Me.Button_ReverseAnim.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Button_ReverseAnim.Image = Global.VGTTutorial.My.Resources.Resources.playbak
        Me.Button_ReverseAnim.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Button_ReverseAnim.Name = "Button_ReverseAnim"
        Me.Button_ReverseAnim.Size = New System.Drawing.Size(23, 22)
        Me.Button_ReverseAnim.Text = "ToolStripButton3"
        Me.Button_ReverseAnim.ToolTipText = "Play Backward"
        '
        'Button_Pause
        '
        Me.Button_Pause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Button_Pause.Image = Global.VGTTutorial.My.Resources.Resources.pause
        Me.Button_Pause.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Button_Pause.Name = "Button_Pause"
        Me.Button_Pause.Size = New System.Drawing.Size(23, 22)
        Me.Button_Pause.Text = "ToolStripButton4"
        Me.Button_Pause.ToolTipText = "Pause"
        '
        'Button_Play
        '
        Me.Button_Play.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Button_Play.Image = Global.VGTTutorial.My.Resources.Resources.play
        Me.Button_Play.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Button_Play.Name = "Button_Play"
        Me.Button_Play.Size = New System.Drawing.Size(23, 22)
        Me.Button_Play.Text = "ToolStripButton5"
        Me.Button_Play.ToolTipText = "Play"
        '
        'Button_StepAhead
        '
        Me.Button_StepAhead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Button_StepAhead.Image = Global.VGTTutorial.My.Resources.Resources._step
        Me.Button_StepAhead.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Button_StepAhead.Name = "Button_StepAhead"
        Me.Button_StepAhead.Size = New System.Drawing.Size(23, 22)
        Me.Button_StepAhead.Text = "ToolStripButton6"
        Me.Button_StepAhead.ToolTipText = "Step Forward"
        '
        'Button_SlowDown
        '
        Me.Button_SlowDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Button_SlowDown.Image = Global.VGTTutorial.My.Resources.Resources.slowdown
        Me.Button_SlowDown.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Button_SlowDown.Name = "Button_SlowDown"
        Me.Button_SlowDown.Size = New System.Drawing.Size(23, 22)
        Me.Button_SlowDown.Text = "ToolStripButton7"
        Me.Button_SlowDown.ToolTipText = "Decrease Speed"
        '
        'Button_SpeedUp
        '
        Me.Button_SpeedUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.Button_SpeedUp.Image = Global.VGTTutorial.My.Resources.Resources.speedup
        Me.Button_SpeedUp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Button_SpeedUp.Name = "Button_SpeedUp"
        Me.Button_SpeedUp.Size = New System.Drawing.Size(23, 22)
        Me.Button_SpeedUp.Text = "ToolStripButton8"
        Me.Button_SpeedUp.ToolTipText = "Increase Speed"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.AutoScrollMinSize = New System.Drawing.Size(873, 155)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 456)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(873, 155)
        Me.Panel1.TabIndex = 2
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.AutoSize = True
        Me.GroupBox3.Controls.Add(Me.CloseScenario1)
        Me.GroupBox3.Controls.Add(Me.CreateAngle)
        Me.GroupBox3.Controls.Add(Me.CreatePlane)
        Me.GroupBox3.Controls.Add(Me.CreateAxes)
        Me.GroupBox3.Controls.Add(Me.ShowVelocity)
        Me.GroupBox3.Controls.Add(Me.CreateVector)
        Me.GroupBox3.Controls.Add(Me.CreateSatellite)
        Me.GroupBox3.Controls.Add(Me.CreateScenario)
        Me.GroupBox3.Location = New System.Drawing.Point(533, 3)
        Me.GroupBox3.MinimumSize = New System.Drawing.Size(337, 145)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(337, 145)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Actions"
        '
        'CloseScenario1
        '
        Me.CloseScenario1.Enabled = False
        Me.CloseScenario1.Location = New System.Drawing.Point(183, 101)
        Me.CloseScenario1.Name = "CloseScenario1"
        Me.CloseScenario1.Size = New System.Drawing.Size(129, 23)
        Me.CloseScenario1.TabIndex = 7
        Me.CloseScenario1.Text = "Exit"
        Me.CloseScenario1.UseVisualStyleBackColor = True
        '
        'CreateAngle
        '
        Me.CreateAngle.Enabled = False
        Me.CreateAngle.Location = New System.Drawing.Point(183, 73)
        Me.CreateAngle.Name = "CreateAngle"
        Me.CreateAngle.Size = New System.Drawing.Size(129, 23)
        Me.CreateAngle.TabIndex = 6
        Me.CreateAngle.Text = "Create Angle"
        Me.CreateAngle.UseVisualStyleBackColor = True
        '
        'CreatePlane
        '
        Me.CreatePlane.Enabled = False
        Me.CreatePlane.Location = New System.Drawing.Point(183, 45)
        Me.CreatePlane.Name = "CreatePlane"
        Me.CreatePlane.Size = New System.Drawing.Size(129, 23)
        Me.CreatePlane.TabIndex = 5
        Me.CreatePlane.Text = "Create Plane"
        Me.CreatePlane.UseVisualStyleBackColor = True
        '
        'CreateAxes
        '
        Me.CreateAxes.Enabled = False
        Me.CreateAxes.Location = New System.Drawing.Point(183, 17)
        Me.CreateAxes.Name = "CreateAxes"
        Me.CreateAxes.Size = New System.Drawing.Size(129, 23)
        Me.CreateAxes.TabIndex = 4
        Me.CreateAxes.Text = "Create Axes"
        Me.CreateAxes.UseVisualStyleBackColor = True
        '
        'ShowVelocity
        '
        Me.ShowVelocity.Enabled = False
        Me.ShowVelocity.Location = New System.Drawing.Point(32, 101)
        Me.ShowVelocity.Name = "ShowVelocity"
        Me.ShowVelocity.Size = New System.Drawing.Size(129, 23)
        Me.ShowVelocity.TabIndex = 3
        Me.ShowVelocity.Text = "Show Velocity Vector"
        Me.ShowVelocity.UseVisualStyleBackColor = True
        '
        'CreateVector
        '
        Me.CreateVector.Enabled = False
        Me.CreateVector.Location = New System.Drawing.Point(32, 73)
        Me.CreateVector.Name = "CreateVector"
        Me.CreateVector.Size = New System.Drawing.Size(129, 23)
        Me.CreateVector.TabIndex = 2
        Me.CreateVector.Text = "Create Vector"
        Me.CreateVector.UseVisualStyleBackColor = True
        '
        'CreateSatellite
        '
        Me.CreateSatellite.Enabled = False
        Me.CreateSatellite.Location = New System.Drawing.Point(32, 45)
        Me.CreateSatellite.Name = "CreateSatellite"
        Me.CreateSatellite.Size = New System.Drawing.Size(129, 23)
        Me.CreateSatellite.TabIndex = 1
        Me.CreateSatellite.Text = "Create Satellite"
        Me.CreateSatellite.UseVisualStyleBackColor = True
        '
        'CreateScenario
        '
        Me.CreateScenario.Location = New System.Drawing.Point(32, 17)
        Me.CreateScenario.Name = "CreateScenario"
        Me.CreateScenario.Size = New System.Drawing.Size(129, 23)
        Me.CreateScenario.TabIndex = 0
        Me.CreateScenario.Text = "New Scenario"
        Me.CreateScenario.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.AutoSize = True
        Me.GroupBox2.Controls.Add(Me.TextBox1)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(294, 3)
        Me.GroupBox2.MinimumSize = New System.Drawing.Size(233, 145)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(233, 145)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Information"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.MenuBar
        Me.TextBox1.Location = New System.Drawing.Point(6, 34)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(188, 20)
        Me.TextBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(159, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Distance from Satellite to Facility"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.AutoSize = True
        Me.GroupBox1.Controls.Add(Me.MainLabel)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.MinimumSize = New System.Drawing.Size(284, 145)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(284, 145)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Description"
        '
        'MainLabel
        '
        Me.MainLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainLabel.Location = New System.Drawing.Point(3, 16)
        Me.MainLabel.Name = "MainLabel"
        Me.MainLabel.Size = New System.Drawing.Size(278, 126)
        Me.MainLabel.TabIndex = 0
        '
        'AxAgUiAxVOCntrl1
        '
        Me.AxAgUiAxVOCntrl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AxAgUiAxVOCntrl1.Enabled = True
        Me.AxAgUiAxVOCntrl1.Location = New System.Drawing.Point(0, 25)
        Me.AxAgUiAxVOCntrl1.Name = "AxAgUiAxVOCntrl1"
        Me.AxAgUiAxVOCntrl1.Size = New System.Drawing.Size(873, 431)
        Me.AxAgUiAxVOCntrl1.TabIndex = 3
        '
        'VGTTutorial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(873, 611)
        Me.Controls.Add(Me.AxAgUiAxVOCntrl1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "VGTTutorial"
        Me.Text = "VGTTutorial"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents Button_ResetAnim As System.Windows.Forms.ToolStripButton
    Friend WithEvents Button_StepBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents Button_ReverseAnim As System.Windows.Forms.ToolStripButton
    Friend WithEvents Button_Pause As System.Windows.Forms.ToolStripButton
    Friend WithEvents Button_Play As System.Windows.Forms.ToolStripButton
    Friend WithEvents Button_StepAhead As System.Windows.Forms.ToolStripButton
    Friend WithEvents Button_SlowDown As System.Windows.Forms.ToolStripButton
    Friend WithEvents Button_SpeedUp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents AxAgUiAxVOCntrl1 As AGI.STKX.Controls.AxAgUiAxVOCntrl
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents MainLabel As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CloseScenario1 As System.Windows.Forms.Button
    Friend WithEvents CreateAngle As System.Windows.Forms.Button
    Friend WithEvents CreatePlane As System.Windows.Forms.Button
    Friend WithEvents CreateAxes As System.Windows.Forms.Button
    Friend WithEvents ShowVelocity As System.Windows.Forms.Button
    Friend WithEvents CreateVector As System.Windows.Forms.Button
    Friend WithEvents CreateSatellite As System.Windows.Forms.Button
    Friend WithEvents CreateScenario As System.Windows.Forms.Button

End Class

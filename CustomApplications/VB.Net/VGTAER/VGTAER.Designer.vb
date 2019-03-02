Partial Class VGTAER
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VGTAER))
        Me.STKControl3D = New AGI.STKX.Controls.AxAgUiAxVOCntrl
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.Rewind = New System.Windows.Forms.Button
        Me.Pause = New System.Windows.Forms.Button
        Me.Play = New System.Windows.Forms.Button
        Me.label1 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.rangeValueLabel = New System.Windows.Forms.Label
        Me.elevationValueLabel = New System.Windows.Forms.Label
        Me.azimuthValueLabel = New System.Windows.Forms.Label
        Me.groupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'STKControl3D
        '
        Me.STKControl3D.Enabled = True
        Me.STKControl3D.Location = New System.Drawing.Point(0, 0)
        Me.STKControl3D.Name = "STKControl3D"
        Me.STKControl3D.Size = New System.Drawing.Size(540, 479)
        Me.STKControl3D.TabIndex = 0
        '
        'groupBox1
        '
        Me.groupBox1.Controls.Add(Me.Rewind)
        Me.groupBox1.Controls.Add(Me.Pause)
        Me.groupBox1.Controls.Add(Me.Play)
        Me.groupBox1.Location = New System.Drawing.Point(546, 12)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(162, 45)
        Me.groupBox1.TabIndex = 18
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Animation Control"
        '
        'Rewind
        '
        Me.Rewind.Location = New System.Drawing.Point(102, 16)
        Me.Rewind.Name = "Rewind"
        Me.Rewind.Size = New System.Drawing.Size(53, 23)
        Me.Rewind.TabIndex = 16
        Me.Rewind.Text = "Rewind"
        Me.Rewind.UseVisualStyleBackColor = True
        '
        'Pause
        '
        Me.Pause.Location = New System.Drawing.Point(49, 16)
        Me.Pause.Name = "Pause"
        Me.Pause.Size = New System.Drawing.Size(47, 23)
        Me.Pause.TabIndex = 15
        Me.Pause.Text = "Pause"
        Me.Pause.UseVisualStyleBackColor = True
        '
        'Play
        '
        Me.Play.Location = New System.Drawing.Point(8, 16)
        Me.Play.Name = "Play"
        Me.Play.Size = New System.Drawing.Size(35, 23)
        Me.Play.TabIndex = 14
        Me.Play.Text = "Play"
        Me.Play.UseVisualStyleBackColor = True
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(554, 87)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(44, 13)
        Me.label1.TabIndex = 19
        Me.label1.Text = "Azimuth"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(554, 111)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(51, 13)
        Me.label2.TabIndex = 20
        Me.label2.Text = "Elevation"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(554, 134)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(39, 13)
        Me.label3.TabIndex = 21
        Me.label3.Text = "Range"
        '
        'rangeValueLabel
        '
        Me.rangeValueLabel.AutoSize = True
        Me.rangeValueLabel.Location = New System.Drawing.Point(614, 134)
        Me.rangeValueLabel.Name = "rangeValueLabel"
        Me.rangeValueLabel.Size = New System.Drawing.Size(0, 13)
        Me.rangeValueLabel.TabIndex = 24
        '
        'elevationValueLabel
        '
        Me.elevationValueLabel.AutoSize = True
        Me.elevationValueLabel.Location = New System.Drawing.Point(614, 111)
        Me.elevationValueLabel.Name = "elevationValueLabel"
        Me.elevationValueLabel.Size = New System.Drawing.Size(0, 13)
        Me.elevationValueLabel.TabIndex = 23
        '
        'azimuthValueLabel
        '
        Me.azimuthValueLabel.AutoSize = True
        Me.azimuthValueLabel.Location = New System.Drawing.Point(614, 87)
        Me.azimuthValueLabel.Name = "azimuthValueLabel"
        Me.azimuthValueLabel.Size = New System.Drawing.Size(0, 13)
        Me.azimuthValueLabel.TabIndex = 22
        '
        'VGTAER
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(715, 478)
        Me.Controls.Add(Me.rangeValueLabel)
        Me.Controls.Add(Me.elevationValueLabel)
        Me.Controls.Add(Me.azimuthValueLabel)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.groupBox1)
        Me.Controls.Add(Me.STKControl3D)
        Me.Name = "VGTAER"
        Me.Text = "VGT AER Example"
        Me.groupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

	#End Region

    Private STKControl3D As AGI.STKX.Controls.AxAgUiAxVOCntrl
    Private groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents Rewind As System.Windows.Forms.Button
    Private WithEvents Pause As System.Windows.Forms.Button
    Private WithEvents Play As System.Windows.Forms.Button
	Private label1 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private rangeValueLabel As System.Windows.Forms.Label
	Private elevationValueLabel As System.Windows.Forms.Label
	Private azimuthValueLabel As System.Windows.Forms.Label
End Class


<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmObscurationTool
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
    Friend WithEvents btnCloseScenario As System.Windows.Forms.Button
    Friend WithEvents btnNewSat As System.Windows.Forms.Button
    Friend WithEvents btnCompute As System.Windows.Forms.Button
    Friend WithEvents btnNewScenario As System.Windows.Forms.Button
    Friend WithEvents AxAgUiAxGfxAnalysisCntrl1 As AGI.STKX.Controls.AxAgUiAxGfxAnalysisCntrl
    Friend WithEvents btnNewSensor As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmObscurationTool))
        Me.AxAgUiAxVOCntrl1 = New AGI.STKX.Controls.AxAgUiAxVOCntrl()
        Me.btnCloseScenario = New System.Windows.Forms.Button()
        Me.btnNewSat = New System.Windows.Forms.Button()
        Me.btnCompute = New System.Windows.Forms.Button()
        Me.btnNewScenario = New System.Windows.Forms.Button()
        Me.AxAgUiAxGfxAnalysisCntrl1 = New AGI.STKX.Controls.AxAgUiAxGfxAnalysisCntrl()
        Me.btnNewSensor = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'AxAgUiAxVOCntrl1
        '
        Me.AxAgUiAxVOCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AxAgUiAxVOCntrl1.Location = New System.Drawing.Point(0, 0)
        Me.AxAgUiAxVOCntrl1.Name = "AxAgUiAxVOCntrl1"
        Me.AxAgUiAxVOCntrl1.Picture = CType(resources.GetObject("AxAgUiAxVOCntrl1.Picture"), System.Drawing.Image)
        Me.AxAgUiAxVOCntrl1.Size = New System.Drawing.Size(576, 304)
        Me.AxAgUiAxVOCntrl1.TabIndex = 0
        '
        'btnCloseScenario
        '
        Me.btnCloseScenario.Location = New System.Drawing.Point(584, 344)
        Me.btnCloseScenario.Name = "btnCloseScenario"
        Me.btnCloseScenario.Size = New System.Drawing.Size(104, 56)
        Me.btnCloseScenario.TabIndex = 15
        Me.btnCloseScenario.Text = "Close Scenario"
        '
        'btnNewSat
        '
        Me.btnNewSat.Location = New System.Drawing.Point(584, 88)
        Me.btnNewSat.Name = "btnNewSat"
        Me.btnNewSat.Size = New System.Drawing.Size(104, 48)
        Me.btnNewSat.TabIndex = 14
        Me.btnNewSat.Text = "New Satellite"
        '
        'btnCompute
        '
        Me.btnCompute.Location = New System.Drawing.Point(584, 216)
        Me.btnCompute.Name = "btnCompute"
        Me.btnCompute.Size = New System.Drawing.Size(104, 48)
        Me.btnCompute.TabIndex = 13
        Me.btnCompute.Text = "Compute"
        '
        'btnNewScenario
        '
        Me.btnNewScenario.Location = New System.Drawing.Point(584, 24)
        Me.btnNewScenario.Name = "btnNewScenario"
        Me.btnNewScenario.Size = New System.Drawing.Size(104, 48)
        Me.btnNewScenario.TabIndex = 12
        Me.btnNewScenario.Text = "New Scenario"
        '
        'AxAgUiAxGfxAnalysisCntrl1
        '
        Me.AxAgUiAxGfxAnalysisCntrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AxAgUiAxGfxAnalysisCntrl1.ControlMode = AGI.STKX.AgEGfxAnalysisMode.eObscurationTool
        Me.AxAgUiAxGfxAnalysisCntrl1.Location = New System.Drawing.Point(0, 304)
        Me.AxAgUiAxGfxAnalysisCntrl1.Name = "AxAgUiAxGfxAnalysisCntrl1"
        Me.AxAgUiAxGfxAnalysisCntrl1.Picture = CType(resources.GetObject("AxAgUiAxGfxAnalysisCntrl1.Picture"), System.Drawing.Image)
        Me.AxAgUiAxGfxAnalysisCntrl1.Size = New System.Drawing.Size(576, 312)
        Me.AxAgUiAxGfxAnalysisCntrl1.TabIndex = 11
        '
        'btnNewSensor
        '
        Me.btnNewSensor.Location = New System.Drawing.Point(584, 152)
        Me.btnNewSensor.Name = "btnNewSensor"
        Me.btnNewSensor.Size = New System.Drawing.Size(104, 48)
        Me.btnNewSensor.TabIndex = 16
        Me.btnNewSensor.Text = "New Sensor"
        '
        'btnReport
        '
        Me.btnReport.Location = New System.Drawing.Point(584, 280)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(104, 48)
        Me.btnReport.TabIndex = 17
        Me.btnReport.Text = "Report"
        '
        'frmObscurationTool
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(693, 623)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.btnNewSensor)
        Me.Controls.Add(Me.btnCloseScenario)
        Me.Controls.Add(Me.btnNewSat)
        Me.Controls.Add(Me.btnCompute)
        Me.Controls.Add(Me.btnNewScenario)
        Me.Controls.Add(Me.AxAgUiAxGfxAnalysisCntrl1)
        Me.Controls.Add(Me.AxAgUiAxVOCntrl1)
        Me.Name = "frmObscurationTool"
        Me.Text = "Obscuration Tool"
        Me.ResumeLayout(False)

    End Sub

End Class

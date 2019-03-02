<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RubberBandSelectForm
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
    Public WithEvents BtnSelectObjs As System.Windows.Forms.Button
    Public WithEvents BtnLoadISRScen As System.Windows.Forms.Button
    Public WithEvents AgUiAxVOCntrl1 As AGI.STKX.Controls.AxAgUiAxVOCntrl
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RubberBandSelectForm))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtnSelectObjs = New System.Windows.Forms.Button
        Me.BtnLoadISRScen = New System.Windows.Forms.Button
        Me.AgUiAxVOCntrl1 = New AGI.STKX.Controls.AxAgUiAxVOCntrl
        Me.SuspendLayout()
        '
        'BtnSelectObjs
        '
        Me.BtnSelectObjs.BackColor = System.Drawing.SystemColors.Control
        Me.BtnSelectObjs.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnSelectObjs.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectObjs.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnSelectObjs.Location = New System.Drawing.Point(664, 64)
        Me.BtnSelectObjs.Name = "BtnSelectObjs"
        Me.BtnSelectObjs.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnSelectObjs.Size = New System.Drawing.Size(105, 41)
        Me.BtnSelectObjs.TabIndex = 2
        Me.BtnSelectObjs.Text = "Select objects"
        Me.BtnSelectObjs.UseVisualStyleBackColor = False
        '
        'BtnLoadISRScen
        '
        Me.BtnLoadISRScen.BackColor = System.Drawing.SystemColors.Control
        Me.BtnLoadISRScen.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnLoadISRScen.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnLoadISRScen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnLoadISRScen.Location = New System.Drawing.Point(664, 16)
        Me.BtnLoadISRScen.Name = "BtnLoadISRScen"
        Me.BtnLoadISRScen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnLoadISRScen.Size = New System.Drawing.Size(105, 41)
        Me.BtnLoadISRScen.TabIndex = 1
        Me.BtnLoadISRScen.Text = "Load ISR Scenario"
        Me.BtnLoadISRScen.UseVisualStyleBackColor = False
        '
        'AgUiAxVOCntrl1
        '
        Me.AgUiAxVOCntrl1.Enabled = True
        Me.AgUiAxVOCntrl1.Location = New System.Drawing.Point(0, 0)
        Me.AgUiAxVOCntrl1.Name = "AgUiAxVOCntrl1"
        Me.AgUiAxVOCntrl1.Size = New System.Drawing.Size(653, 507)
        Me.AgUiAxVOCntrl1.TabIndex = 0
        '
        'RubberBandSelectForm
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(775, 511)
        Me.Controls.Add(Me.BtnSelectObjs)
        Me.Controls.Add(Me.BtnLoadISRScen)
        Me.Controls.Add(Me.AgUiAxVOCntrl1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RubberBandSelectForm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rubber-band Select Example"
        Me.ResumeLayout(False)

    End Sub

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DrawRects
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
    Public WithEvents BtnListAllForEach As System.Windows.Forms.Button
    Public WithEvents BtnListAll As System.Windows.Forms.Button
    Public WithEvents BtnClearAll As System.Windows.Forms.Button
    Public WithEvents Combo1 As System.Windows.Forms.ComboBox
    Public WithEvents BtnAddRect As System.Windows.Forms.Button
    Public WithEvents BtnNewScen As System.Windows.Forms.Button
    Public WithEvents AgUiAxVOCntrl1 As AGI.STKX.Controls.AxAgUiAxVOCntrl
    Public WithEvents BtnSelectColor As System.Windows.Forms.Button
    Public WithEvents Combo2 As System.Windows.Forms.ComboBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    Friend WithEvents ColorPicker As System.Windows.Forms.ColorDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DrawRects))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtnListAllForEach = New System.Windows.Forms.Button
        Me.BtnListAll = New System.Windows.Forms.Button
        Me.BtnClearAll = New System.Windows.Forms.Button
        Me.Combo1 = New System.Windows.Forms.ComboBox
        Me.BtnAddRect = New System.Windows.Forms.Button
        Me.BtnNewScen = New System.Windows.Forms.Button
        Me.AgUiAxVOCntrl1 = New AGI.STKX.Controls.AxAgUiAxVOCntrl
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.BtnSelectColor = New System.Windows.Forms.Button
        Me.Combo2 = New System.Windows.Forms.ComboBox
        Me.ColorPicker = New System.Windows.Forms.ColorDialog
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnListAllForEach
        '
        Me.BtnListAllForEach.BackColor = System.Drawing.SystemColors.Control
        Me.BtnListAllForEach.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnListAllForEach.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnListAllForEach.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnListAllForEach.Location = New System.Drawing.Point(656, 208)
        Me.BtnListAllForEach.Name = "BtnListAllForEach"
        Me.BtnListAllForEach.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnListAllForEach.Size = New System.Drawing.Size(113, 41)
        Me.BtnListAllForEach.TabIndex = 9
        Me.BtnListAllForEach.Text = "List All (For Each)"
        Me.BtnListAllForEach.UseVisualStyleBackColor = False
        '
        'BtnListAll
        '
        Me.BtnListAll.BackColor = System.Drawing.SystemColors.Control
        Me.BtnListAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnListAll.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnListAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnListAll.Location = New System.Drawing.Point(656, 160)
        Me.BtnListAll.Name = "BtnListAll"
        Me.BtnListAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnListAll.Size = New System.Drawing.Size(113, 41)
        Me.BtnListAll.TabIndex = 8
        Me.BtnListAll.Text = "List All"
        Me.BtnListAll.UseVisualStyleBackColor = False
        '
        'BtnClearAll
        '
        Me.BtnClearAll.BackColor = System.Drawing.SystemColors.Control
        Me.BtnClearAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnClearAll.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClearAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnClearAll.Location = New System.Drawing.Point(656, 112)
        Me.BtnClearAll.Name = "BtnClearAll"
        Me.BtnClearAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnClearAll.Size = New System.Drawing.Size(113, 41)
        Me.BtnClearAll.TabIndex = 7
        Me.BtnClearAll.Text = "Clear All"
        Me.BtnClearAll.UseVisualStyleBackColor = False
        '
        'Combo1
        '
        Me.Combo1.BackColor = System.Drawing.SystemColors.Window
        Me.Combo1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Combo1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Combo1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Combo1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Combo1.Location = New System.Drawing.Point(664, 280)
        Me.Combo1.Name = "Combo1"
        Me.Combo1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Combo1.Size = New System.Drawing.Size(97, 22)
        Me.Combo1.TabIndex = 3
        '
        'BtnAddRect
        '
        Me.BtnAddRect.BackColor = System.Drawing.SystemColors.Control
        Me.BtnAddRect.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnAddRect.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAddRect.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnAddRect.Location = New System.Drawing.Point(656, 64)
        Me.BtnAddRect.Name = "BtnAddRect"
        Me.BtnAddRect.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnAddRect.Size = New System.Drawing.Size(113, 41)
        Me.BtnAddRect.TabIndex = 2
        Me.BtnAddRect.Text = "Add Rect"
        Me.BtnAddRect.UseVisualStyleBackColor = False
        '
        'BtnNewScen
        '
        Me.BtnNewScen.BackColor = System.Drawing.SystemColors.Control
        Me.BtnNewScen.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnNewScen.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNewScen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnNewScen.Location = New System.Drawing.Point(656, 16)
        Me.BtnNewScen.Name = "BtnNewScen"
        Me.BtnNewScen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnNewScen.Size = New System.Drawing.Size(113, 41)
        Me.BtnNewScen.TabIndex = 1
        Me.BtnNewScen.Text = "New Scenario"
        Me.BtnNewScen.UseVisualStyleBackColor = False
        '
        'AgUiAxVOCntrl1
        '
        Me.AgUiAxVOCntrl1.Enabled = True
        Me.AgUiAxVOCntrl1.Location = New System.Drawing.Point(0, 0)
        Me.AgUiAxVOCntrl1.Name = "AgUiAxVOCntrl1"
        Me.AgUiAxVOCntrl1.Size = New System.Drawing.Size(647, 507)
        Me.AgUiAxVOCntrl1.TabIndex = 0
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.BtnSelectColor)
        Me.Frame1.Controls.Add(Me.Combo2)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(656, 264)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(113, 97)
        Me.Frame1.TabIndex = 4
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Line Style"
        '
        'BtnSelectColor
        '
        Me.BtnSelectColor.BackColor = System.Drawing.SystemColors.Control
        Me.BtnSelectColor.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnSelectColor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectColor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.BtnSelectColor.Location = New System.Drawing.Point(8, 64)
        Me.BtnSelectColor.Name = "BtnSelectColor"
        Me.BtnSelectColor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.BtnSelectColor.Size = New System.Drawing.Size(97, 25)
        Me.BtnSelectColor.TabIndex = 6
        Me.BtnSelectColor.Text = "Color"
        Me.BtnSelectColor.UseVisualStyleBackColor = False
        '
        'Combo2
        '
        Me.Combo2.BackColor = System.Drawing.SystemColors.Window
        Me.Combo2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Combo2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Combo2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Combo2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Combo2.Location = New System.Drawing.Point(8, 40)
        Me.Combo2.Name = "Combo2"
        Me.Combo2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Combo2.Size = New System.Drawing.Size(97, 22)
        Me.Combo2.TabIndex = 5
        '
        'DrawRects
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(775, 514)
        Me.Controls.Add(Me.BtnListAllForEach)
        Me.Controls.Add(Me.BtnListAll)
        Me.Controls.Add(Me.BtnClearAll)
        Me.Controls.Add(Me.Combo1)
        Me.Controls.Add(Me.BtnAddRect)
        Me.Controls.Add(Me.BtnNewScen)
        Me.Controls.Add(Me.AgUiAxVOCntrl1)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DrawRects"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DrawRects"
        Me.Frame1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

End Class

Imports System.Drawing

Public Class ColorForm

    Dim color1 As Color
    Dim color2 As Color
    Dim method As Integer = 0

    Private Sub ColorFrom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = method
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If (ComboBox2.SelectedIndex = 0) Then
            PictureBox1.BackColor = Color.Green
            color1 = Color.Green
        ElseIf (ComboBox2.SelectedIndex = 1) Then
            PictureBox1.BackColor = Color.Red
            color1 = Color.Red
        ElseIf (ComboBox2.SelectedIndex = 2) Then
            PictureBox1.BackColor = Color.Yellow
            color1 = Color.Yellow
        ElseIf (ComboBox2.SelectedIndex = 3) Then
            PictureBox1.BackColor = Color.LightBlue
            color1 = Color.LightBlue
        ElseIf (ComboBox2.SelectedIndex = 4) Then
            If (ColorDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
                PictureBox1.BackColor = ColorDialog1.Color
                color1 = ColorDialog1.Color
            End If
        End If
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If (ComboBox3.SelectedIndex = 0) Then
            PictureBox2.BackColor = Color.Green
            Color2 = Color.Green
        ElseIf (ComboBox3.SelectedIndex = 1) Then
            PictureBox2.BackColor = Color.Red
            Color2 = Color.Red
        ElseIf (ComboBox3.SelectedIndex = 2) Then
            PictureBox2.BackColor = Color.Yellow
            Color2 = Color.Yellow
        ElseIf (ComboBox3.SelectedIndex = 3) Then
            PictureBox2.BackColor = Color.LightBlue
            Color2 = Color.LightBlue
        ElseIf (ComboBox3.SelectedIndex = 4) Then
            If (ColorDialog2.ShowDialog() = System.Windows.Forms.DialogResult.OK) Then
                PictureBox2.BackColor = ColorDialog2.Color
                color2 = ColorDialog2.Color
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If (ComboBox1.SelectedIndex = 0) Then
            Label2.Text = "Start Color:"
            ComboBox3.Visible = True
            Label3.Visible = True
            PictureBox2.Visible = True
        Else
            Label2.Text = "Color:"
            ComboBox3.Visible = False
            Label3.Visible = False
            PictureBox2.Visible = False
        End If
        method = ComboBox1.SelectedIndex
    End Sub

    Public ReadOnly Property firstColor() As Color
        Get
            Return color1
        End Get
    End Property

    Public ReadOnly Property secondColor() As Color
        Get
            Return color2
        End Get
    End Property

    Public ReadOnly Property colorMethod() As Integer
        Get
            Return ComboBox1.SelectedIndex
        End Get
    End Property

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub CanelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CanelButton.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class
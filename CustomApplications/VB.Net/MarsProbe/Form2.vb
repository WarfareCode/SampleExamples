Public Class Form2

    Public Property ReportData() As String
        Get
            Return Me.richTextBox1.Text
        End Get
        Set(ByVal value As String)
            Me.richTextBox1.Text = value
        End Set
    End Property
    Public Property FormTitle() As String

        Get
            Return Me.Text
        End Get
        Set(ByVal value As String)
            Me.Text = value
        End Set
    End Property

End Class
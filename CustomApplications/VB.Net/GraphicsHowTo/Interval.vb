Public Structure Interval
    'Implements IEquatable(Of Interval)
    Public Property Minimum() As Double
        Get
            Return m_Minimum
        End Get
        Set(ByVal value As Double)
            m_Minimum = Value
        End Set
    End Property

    Public Property Maximum() As Double
        Get
            Return m_Maximum
        End Get
        Set(ByVal value As Double)
            m_Maximum = Value
        End Set
    End Property

    Public Sub New(ByVal minimum As Double, ByVal maximum As Double)
        m_Minimum = minimum
        m_Maximum = maximum
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is Interval Then
            Dim i As Interval = CType(obj, Interval)
            Return Me.Equals(i)
        Else
            Return False
        End If
    End Function

    Public Overloads Function Equals(other As Interval) As Boolean
        Return (other.Maximum = Maximum AndAlso other.Minimum = Minimum)
    End Function

    Public Overrides Function GetHashCode() As Integer
        'based on GetHashCode inside the .Net libararies
        Dim finalHashCode As Integer = &H61E04917 'sufficiently large random number
        finalHashCode = ((finalHashCode << 5) + finalHashCode) Xor Minimum.GetHashCode()
        finalHashCode = ((finalHashCode << 5) + finalHashCode) Xor Maximum.GetHashCode()
        Return finalHashCode
    End Function

    Public Shared Operator =(ByVal interval1 As Interval, ByVal interval2 As Interval) As Boolean
        Return interval1.Equals(interval2)
    End Operator

    Public Shared Operator <>(ByVal interval1 As Interval, ByVal interval2 As Interval) As Boolean
        Return Not (interval1.Equals(interval2))
    End Operator

    Private m_Minimum As Double
    Private m_Maximum As Double
End Structure

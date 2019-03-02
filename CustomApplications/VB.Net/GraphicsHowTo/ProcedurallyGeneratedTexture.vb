Imports System.Collections.Generic
Imports System.Text

Public Class ProcedurallyGeneratedTexture
	Public Sub New()
		Initialize(256, &Hff)
	End Sub

	Public Sub New(size As Integer)
		Initialize(size, &Hff)
	End Sub

	Public Sub New(size As Integer, rgb As Integer)
		Initialize(size, rgb)
	End Sub

	Private Sub Initialize(size As Integer, rgb As Integer)
		m_x = 0
		m_size = size

		m_r = rgb And &Hff0000
		m_g = rgb And &Hff00
		m_b = rgb And &Hff
		m_a = 255

		GenTexture()
	End Sub

	Private Shared Function Noise(x As Integer, y As Integer) As Double
		Dim n As Integer = x + y * 57
		n = (n << 13) Xor n
		Return (1.0 - ((n * (n * n * 19973 + 97003) + 104729) And &H7fffffff) / 1073741824.0)
	End Function

	Private Shared Function Interpolate(a As Double, b As Double, x As Double) As Double
		Dim f As Double = (1 - Math.Cos(x * Math.PI)) * 0.5
		Return a * (1 - f) + b * f
	End Function

	Private Shared Function SmoothNoise(x As Integer, y As Integer) As Double
		Dim corners As Double = (Noise(x - 1, y - 1) + Noise(x + 1, y - 1) + Noise(x - 1, y + 1) + Noise(x + 1, y + 1)) / 16
		Dim sides As Double = (Noise(x - 1, y) + Noise(x + 1, y) + Noise(x, y - 1) + Noise(x, y + 1)) / 8
		Dim center As Double = Noise(x, y) / 4
		Return corners + sides + center
	End Function


	Private Shared Function InterpolateNoise(x As Double, y As Double) As Double
		Dim iX As Integer = CInt(Math.Truncate(x))
		Dim dX As Double = x - iX

		Dim iY As Integer = CInt(Math.Truncate(y))
		Dim dY As Double = y - iY

		Dim v1 As Double = SmoothNoise(iX, iY)
		Dim v2 As Double = SmoothNoise(iX + 1, iY)
		Dim v3 As Double = SmoothNoise(iX, iY + 1)
		Dim v4 As Double = SmoothNoise(iX + 1, iY + 1)

		Dim i1 As Double = Interpolate(v1, v2, dX)
		Dim i2 As Double = Interpolate(v3, v4, dX)

		Return Interpolate(i1, i2, dY)
	End Function

	Private Shared Function GenNoise(x As Double, y As Double, size As Double) As Double
		Dim total As Double = 0.0
		Dim initialSize As Double = size

        Dim i As Integer = 0
        While (i < size)
            total += InterpolateNoise(x / size, y / size) * size
            size /= 2.0
            i += 1
        End While

		Return total / initialSize
	End Function

	Private Sub GenTexture()
		m_texture = New Byte(m_size * m_size * 4 - 1) {}

		For i As Integer = 0 To m_size - 1
			For j As Integer = 0 To (m_size * 4) - 5 Step 4
				Dim color As Double = GenNoise(i, j \ 4, 64) * 255
				Dim index As Integer = i * m_size * 4 + j
				m_texture(index) = CByte(Math.Truncate((m_r / 255.0) * color))
				m_texture(index + 1) = CByte(Math.Truncate((m_g / 255.0) * color))
				m_texture(index + 2) = CByte(Math.Truncate((m_b / 255.0) * color))
				m_texture(index + 3) = CByte(m_a)
			Next
		Next
		m_x = m_size
	End Sub

    Public Function [Next]() As Byte()
        Dim texture As Byte() = New Byte(m_size * m_size * 4 - 1) {}
        For i As Integer = m_size * 4 To m_size * m_size * 4 - 1
            texture(i - m_size * 4) = m_texture(i)
        Next

        Dim index As Integer = (m_size * m_size * 4) - (m_size * 4)
        For j As Integer = 0 To m_size * 4 - 1 Step 4
            Dim color As Double = GenNoise(m_x, j \ 4, 64) * 255
            texture(index + j) = CByte(Math.Truncate((m_r / 255.0) * color))
            texture(index + j + 1) = CByte(Math.Truncate((m_g / 255.0) * color))
            texture(index + j + 2) = CByte(Math.Truncate((m_b / 255.0) * color))
            texture(index + j + 3) = CByte(m_a)
        Next
        m_x += 1

        m_texture = texture
        Return m_texture
    End Function

	Private m_x As Integer
	Private m_size As Integer
	Private m_texture As Byte()

	Private m_r As Integer
	Private m_g As Integer
	Private m_b As Integer
	Private m_a As Integer
End Class

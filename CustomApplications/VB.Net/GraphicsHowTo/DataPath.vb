Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Configuration
Imports System.Windows.Forms
Imports AGI.STKObjects
Imports AGI.STKUtil

Namespace AGI
	Public Enum DataPathRoot
		TestData
		Relative
	End Enum

	<Serializable> _
	Public Class DataPath
		Public Sub New(root As DataPathRoot, relative As String)
			m_root = root
			m_relative = relative
		End Sub

		Public ReadOnly Property FullPath() As String
			Get
                Dim root As String
                If m_root = DataPathRoot.TestData Then
                    ' all users
                    Dim allUsers As String = System.Environment.GetEnvironmentVariable("ALLUSERSPROFILE")
                    ' rest of path added a few lines below
                    Dim dataDir As String = allUsers
                    Dim notVista As Boolean = (System.Environment.GetEnvironmentVariable("ProgramData") Is Nothing)
                    If notVista Then
                        dataDir = allUsers & "\Application Data"
                    End If
                    root = dataDir & "\AGI\STK 11\"
                Else
                    ' Set a path to a directory containing the test resources.
                    root = Path.Combine(Application.StartupPath, "Data")
                End If

                Dim relative As String = m_relative.Replace("\"c, Path.DirectorySeparatorChar)
                relative = relative.Replace("/"c, Path.DirectorySeparatorChar)

                Return Path.Combine(root, relative)
			End Get
		End Property

		Public ReadOnly Property Uri() As Uri
			Get
				Return New Uri(FullAbsolutePath)
			End Get
		End Property

		Public ReadOnly Property FullAbsolutePath() As String
			Get
				Dim fullPath__1 As String = FullPath
				If String.IsNullOrEmpty(fullPath__1) Then
					Return Directory.GetCurrentDirectory() & Path.DirectorySeparatorChar
				End If

				If Not Path.IsPathRooted(fullPath__1) Then
					Return Path.Combine(Directory.GetCurrentDirectory(), fullPath__1)
				End If

				Return fullPath__1
			End Get
		End Property

		Public Overrides Function Equals(obj As Object) As Boolean
			Dim dp As DataPath = TryCast(obj, DataPath)
			If dp IsNot Nothing Then
				Return FullPath = dp.FullPath
			Else
				Return False
			End If
		End Function

		Public Overrides Function GetHashCode() As Integer
			Return m_root.GetHashCode() Xor m_relative.GetHashCode()
		End Function

		Private m_root As DataPathRoot
		Private m_relative As String
	End Class
End Namespace

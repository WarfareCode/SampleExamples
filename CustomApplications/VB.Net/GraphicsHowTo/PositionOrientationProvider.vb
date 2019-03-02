Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports AGI.STKUtil
Imports AGI.STKObjects

Public Class PositionOrientationProvider
	Private Const Separator As String = "    "

	Public Sub New(filename As String, root As AgStkObjectRoot)
		m_Dates = New List(Of Double)()
		m_Positions = New List(Of Array)()
		m_Orientations = New List(Of Array)()
		m_root = root

		Using sr As New StreamReader(filename)
			While sr.Peek() >= 0
				Dim sEntries As String() = sr.ReadLine().Replace(Separator, ",").Split(","C)
				m_Dates.Add(Double.Parse(root.ConversionUtility.NewDate("UTCG", sEntries(0)).Format("epSec")))

                Dim x As Double = Double.Parse(sEntries(1), CultureInfo.InvariantCulture)
                Dim y As Double = Double.Parse(sEntries(2), CultureInfo.InvariantCulture)
                Dim z As Double = Double.Parse(sEntries(3), CultureInfo.InvariantCulture)
				Dim pos As Array = New Object() {x, y, z}
				m_Positions.Add(pos)

                x = Double.Parse(sEntries(4), CultureInfo.InvariantCulture)
                y = Double.Parse(sEntries(5), CultureInfo.InvariantCulture)
                z = Double.Parse(sEntries(6), CultureInfo.InvariantCulture)
                Dim w As Double = Double.Parse(sEntries(7), CultureInfo.InvariantCulture)
				Dim orientation As Array = New Object() {x, y, z, w}
				m_Orientations.Add(orientation)
			End While
		End Using
	End Sub

	Public ReadOnly Property Dates() As IList(Of Double)
		Get
			Return m_Dates
		End Get
	End Property
	Public ReadOnly Property Positions() As IList(Of Array)
		Get
			Return m_Positions
		End Get
	End Property
	Public ReadOnly Property Orientations() As IList(Of Array)
		Get
			Return m_Orientations
		End Get
	End Property

	Public Function FindIndexOfClosestTime(searchTime As Double, startIndex As Integer, searchLength As Integer) As Integer
		' Find the midpoint of the length
		Dim midpoint As Integer = startIndex + (searchLength \ 2)

		' Base cases
		If m_Dates(startIndex) = searchTime OrElse searchLength = 1 Then
			Return startIndex
		End If
		If searchLength = 2 Then
			Dim diff1 As Double = m_Dates(startIndex) - searchTime
			Dim diff2 As Double = m_Dates(startIndex + 1) - searchTime

			If Math.Abs(diff1) < Math.Abs(diff2) Then
				Return startIndex
			Else
				' Note: error on the larger time if equal
				Return startIndex + 1
			End If
		End If
		If m_Dates(midpoint) = searchTime Then
			Return midpoint
		End If

		' Normal case: binary search
		If searchTime < m_Dates(midpoint) Then
			Return FindIndexOfClosestTime(searchTime, startIndex, midpoint - startIndex)
		Else
			Return FindIndexOfClosestTime(searchTime, midpoint + 1, startIndex + searchLength - (midpoint + 1))
		End If
	End Function


	Private ReadOnly m_Dates As IList(Of Double)
	Private ReadOnly m_Positions As IList(Of Array)
	Private ReadOnly m_Orientations As IList(Of Array)
	Private ReadOnly m_root As AgStkObjectRoot
End Class

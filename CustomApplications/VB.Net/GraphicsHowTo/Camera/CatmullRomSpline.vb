Imports System.Collections.Generic
Imports AGI.STKUtil
Imports AGI.STKObjects

Namespace Camera
	Public Class CatmullRomSpline
		'
		' Creates a spline given a shape and a list of cartographics
		'
		Public Sub New(centralBody As String, positions As ICollection(Of Array), root As AgStkObjectRoot)
			CalculateControlPoints(root, centralBody, positions)
			CalculateInterpolationPoints(root)
		End Sub

		'
		' Creates a spline given a central body, two start points and an altitude
		'
		Public Sub New(root As AgStkObjectRoot, centralBody As String, start As Array, [end] As Array, altitude As Double)
			altitude = Math.Abs(altitude)
			Dim height As Double = 0.8

			Dim aboveStart As Array = New Object() {CDbl(start.GetValue(0)), CDbl(start.GetValue(1)), altitude * height}
			Dim middle As Array = New Object() {(CDbl(start.GetValue(0)) + CDbl([end].GetValue(0))) / 2, (CDbl(start.GetValue(1)) + CDbl([end].GetValue(1))) / 2, altitude}
			Dim aboveEnd As Array = New Object() {CDbl([end].GetValue(0)), CDbl([end].GetValue(1)), altitude * height}

			Dim cartographics As New List(Of Array)()
			cartographics.Add(start)
			cartographics.Add(aboveStart)
			cartographics.Add(middle)
			cartographics.Add(aboveEnd)
			cartographics.Add([end])

			CalculateControlPoints(root, centralBody, cartographics)
			CalculateInterpolationPoints(root)
		End Sub

		'
		' Returns a list of interpolator points
		'
		Public ReadOnly Property InterpolatorPoints() As ICollection(Of Array)
			Get
				Return m_InterpolatorPoints
			End Get
		End Property

		'
		' Sets the number of interpolation points that should be part of the spline
		'
		Public Sub SetNumberOfInterpolationPoints(numberOfPoints As Integer, root As AgStkObjectRoot)
			m_NumberOfInterpolatorPoints = numberOfPoints

			m_NumberOfInterpolatorPoints = Math.Max(m_NumberOfInterpolatorPoints, (m_ControlPoints.Count - 3) * 2)
			m_NumberOfInterpolatorPoints = Math.Min(m_NumberOfInterpolatorPoints, 1000000)

			CalculateInterpolationPoints(root)
		End Sub

		'
		' Calculates the control points for the spline
		'
		Private Sub CalculateControlPoints(root As AgStkObjectRoot, centralBody As String, positions As ICollection(Of Array))
			Dim positionsList As New List(Of Array)(positions)
			m_ControlPoints = New List(Of Array)()
			Dim numPoints As Integer = positionsList.Count
			If numPoints >= 2 Then
                Dim virtualStart As Array = New Object() _
                { _
                    CDbl(positionsList(0).GetValue(0)) + (CDbl(positionsList(0).GetValue(0)) - CDbl(positionsList(1).GetValue(0))), _
                    CDbl(positionsList(0).GetValue(1)) + (CDbl(positionsList(0).GetValue(1)) - CDbl(positionsList(1).GetValue(1))), _
                    CDbl(positionsList(0).GetValue(2)) + (CDbl(positionsList(0).GetValue(2)) - CDbl(positionsList(1).GetValue(2))) _
                }

                Dim virtualEnd As Array = New Object() _
                { _
                    CDbl(positionsList(numPoints - 1).GetValue(0)) + (CDbl(positionsList(numPoints - 1).GetValue(0)) - CDbl(positionsList(numPoints - 2).GetValue(0))), _
                    CDbl(positionsList(numPoints - 1).GetValue(1)) + (CDbl(positionsList(numPoints - 1).GetValue(1)) - CDbl(positionsList(numPoints - 2).GetValue(1))), _
                    CDbl(positionsList(numPoints - 1).GetValue(2)) + (CDbl(positionsList(numPoints - 1).GetValue(2)) - CDbl(positionsList(numPoints - 2).GetValue(2))) _
                }

				m_ControlPoints.Add(CartographicToCartesian(virtualStart, centralBody, root))
				For i As Integer = 0 To numPoints - 1
					m_ControlPoints.Add(CartographicToCartesian(positionsList(i), centralBody, root))
				Next
				m_ControlPoints.Add(CartographicToCartesian(virtualEnd, centralBody, root))
			End If
		End Sub

		'
		' Calculates the interpolator points for the spline
		'
		Private Sub CalculateInterpolationPoints(root As AgStkObjectRoot)
			m_InterpolatorPoints = New List(Of Array)()

			If m_ControlPoints.Count >= 4 Then
				For i As Integer = 1 To m_ControlPoints.Count - 3
					Dim points As Array() = New Array(3) {}
					points(0) = m_ControlPoints(i - 1)
					points(1) = m_ControlPoints(i)
					points(2) = m_ControlPoints(i + 1)
					points(3) = m_ControlPoints(i + 2)

					Dim [end] As Integer = m_NumberOfInterpolatorPoints \ (m_ControlPoints.Count - 3)
					For t As Integer = 0 To [end] - 1
						Dim time As Double = CDbl(t) / CDbl([end] - 1)
						Dim t1 As Double = time
						Dim t2 As Double = time * time
						Dim t3 As Double = time * time * time



                        Dim position As Array = New Double() _
                        { _
                            0.5 * ((2 * CDbl(points(1).GetValue(0))) + (-CDbl(points(0).GetValue(0)) + CDbl(points(2).GetValue(0))) * t1 + _
                            (2 * CDbl(points(0).GetValue(0)) - 5 * CDbl(points(1).GetValue(0)) + 4 * CDbl(points(2).GetValue(0)) - CDbl(points(3).GetValue(0))) * t2 + _
                            (-CDbl(points(0).GetValue(0)) + 3 * CDbl(points(1).GetValue(0)) - 3 * CDbl(points(2).GetValue(0)) + CDbl(points(3).GetValue(0))) * t3), _
 _
                            0.5 * ((2 * CDbl(points(1).GetValue(1))) + (-CDbl(points(0).GetValue(1)) + CDbl(points(2).GetValue(1))) * t1 + _
                            (2 * CDbl(points(0).GetValue(1)) - 5 * CDbl(points(1).GetValue(1)) + 4 * CDbl(points(2).GetValue(1)) - CDbl(points(3).GetValue(1))) * t2 + _
                            (-CDbl(points(0).GetValue(1)) + 3 * CDbl(points(1).GetValue(1)) - 3 * CDbl(points(2).GetValue(1)) + CDbl(points(3).GetValue(1))) * t3), _
 _
                            0.5 * ((2 * CDbl(points(1).GetValue(2))) + (-CDbl(points(0).GetValue(2)) + CDbl(points(2).GetValue(2))) * t1 + _
                            (2 * CDbl(points(0).GetValue(2)) - 5 * CDbl(points(1).GetValue(2)) + 4 * CDbl(points(2).GetValue(2)) - CDbl(points(3).GetValue(2))) * t2 + _
                            (-CDbl(points(0).GetValue(2)) + 3 * CDbl(points(1).GetValue(2)) - 3 * CDbl(points(2).GetValue(2)) + CDbl(points(3).GetValue(2))) * t3) _
                        }

						m_InterpolatorPoints.Add(position)
					Next
				Next
			End If
		End Sub

		Public Shared Function CartographicToCartesian(cartographic As Array, centralBody As String, root As AgStkObjectRoot) As Array
			Dim position As IAgPosition = root.ConversionUtility.NewPositionOnCB(centralBody)
			position.AssignPlanetodetic(CDbl(cartographic.GetValue(0)), CDbl(cartographic.GetValue(1)), Math.Max(CDbl(cartographic.GetValue(2)), 0))

			Dim x As Double = 0, y As Double = 0, z As Double = 0
			position.QueryCartesian(x, y, z)

			Return New Double() {x, y, z}
		End Function

		Public Shared Function CartesianToCartographic(cartesian As Array, centralBody As String, root As AgStkObjectRoot) As Array
			Dim position As IAgPosition = root.ConversionUtility.NewPositionOnCB(centralBody)
			position.AssignCartesian(CDbl(cartesian.GetValue(0)), CDbl(cartesian.GetValue(1)), CDbl(cartesian.GetValue(2)))

			Dim lat As Object = 0, lon As Object = 0
			Dim alt As Double = 0
			position.QueryPlanetodetic(lat, lon, alt)
            Return New Object() {lat, lon, alt}
		End Function

		Private m_ControlPoints As List(Of Array)
		Private m_InterpolatorPoints As List(Of Array)
		Private m_NumberOfInterpolatorPoints As Integer = 10000
	End Class
End Namespace

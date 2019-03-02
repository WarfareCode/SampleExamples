Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil

Public Class AltitudeOverlay
	Inherits StatusOverlay(Of Double)
	Public Sub New(scene As IAgStkGraphicsScene, manager As IAgStkGraphicsSceneManager)
		MyBase.New(False, 0, 10000000, "0", "10000", manager)
		m_Scene = scene
        m_SceneManager = manager
        AddHandler DirectCast(scene, AgStkGraphicsScene).Rendering, AddressOf SceneRendering
	End Sub

	Private Sub SceneRendering(Sender As Object, Args As IAgStkGraphicsRenderingEventArgs)
		Update(Value, m_SceneManager)
	End Sub

	Public Overrides Function ValueTransform(value As Double) As Double
		Return If((value >= 1), Math.Log10(value / 10000.0), value)
	End Function

	Public Overrides ReadOnly Property Value() As Double
		Get
            Return m_Scene.Camera.Distance
		End Get
	End Property

	Public Overrides ReadOnly Property Text() As String
		Get
            Return "Current Altitude:" & vbLf & _
                    String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0,10:0.000} km" & vbLf & vbLf & vbLf, Value / 1000)
		End Get
	End Property

	''' <summary>
	''' Add a list of intervals of doubles using a different color for each.
	''' </summary>
	''' <param name="intervals">Collection of raw (non-ValueTransformed) Intervals.</param>
	Friend Sub AddIntervals(intervals As ICollection(Of Interval))
        Dim colors As Color() = New Color() _
        { _
            System.Drawing.Color.SkyBlue, System.Drawing.Color.LightGreen, _
            System.Drawing.Color.Yellow, System.Drawing.Color.LightSalmon, _
            System.Drawing.Color.DarkRed, System.Drawing.Color.MediumPurple _
        }

		Dim i As Integer = 0
		For Each interval As Interval In intervals
            Indicator.AddInterval(ValueTransform(interval.Minimum), ValueTransform(interval.Maximum), IndicatorStyle.Bar, _
                                  colors(i), m_SceneManager)
			i = (i + 1) Mod colors.Length
		Next
	End Sub

	Friend Sub RemoveIntervals(intervals As ICollection(Of Interval))
		For Each interval As Interval In intervals
			Indicator.RemoveInterval(ValueTransform(interval.Minimum), ValueTransform(interval.Maximum))
		Next
	End Sub

	Private m_Scene As IAgStkGraphicsScene
	Private m_SceneManager As IAgStkGraphicsSceneManager

End Class

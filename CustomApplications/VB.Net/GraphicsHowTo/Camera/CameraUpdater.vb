Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Timers
Imports AGI.STKGraphics
Imports AGI.STKUtil
Imports AGI.STKObjects
Imports AGI.STKVgt

Namespace Camera
	Public Enum CameraUpdaterStyle
		Rotating
		Fixed
	End Enum

	Public Class CameraUpdater
		Implements IDisposable
		'
		' Creates a CameraUpdater
		'
		Public Sub New(scene As IAgStkGraphicsScene, root As AgStkObjectRoot, positions As ICollection(Of Array), numberOfSeconds As Double)
			Initialize(scene, root, positions, numberOfSeconds, 60, CameraUpdaterStyle.Fixed)
		End Sub

		'
		' Creates a CameraUpdater with all of the options as parameters
		'
		Public Sub New(scene As IAgStkGraphicsScene, root As AgStkObjectRoot, positions As ICollection(Of Array), numberOfSeconds As Double, framerate As Double, style As CameraUpdaterStyle)
			Initialize(scene, root, positions, numberOfSeconds, framerate, style)
		End Sub

		Public Sub Dispose() Implements IDisposable.Dispose
			Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub

		Protected Overrides Sub Finalize()
			Try
				Dispose(False)
			Finally
				MyBase.Finalize()
			End Try
		End Sub

		Protected Overridable Sub Dispose(disposing As Boolean)
			If disposing Then
				If m_Timer IsNot Nothing Then
					m_Timer.Dispose()
					m_Timer = Nothing
					m_Scene.Camera.LockViewDirection = False
				End If
			End If
		End Sub

		'
		' Returns whether or not the animation is still running
		'
		Public Function IsRunning() As Boolean
			Return Not m_Stop
		End Function

		'
		' Initialize method that is called by the constructors
		'
		Private Sub Initialize(scene As IAgStkGraphicsScene, root As AgStkObjectRoot, positions As ICollection(Of Array), numberOfSeconds As Double, framerate As Double, style As CameraUpdaterStyle)
			m_Style = style

			'
			' Initialize the scene and positions
			'
			m_Root = root
			m_Scene = scene
			m_Scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
			m_Scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed

			m_Positions = New List(Of Array)(positions)
			m_CurrentPosition = 0
			m_PreviousPosition = 0

			'
			' Initialize the stopwatch and timer
			'
			m_NumberOfSeconds = numberOfSeconds
			m_Stopwatch = New Stopwatch()
			m_Stopwatch.Start()

			m_Timer = New System.Timers.Timer(1000 / framerate)
            m_Timer.SynchronizingObject = GraphicsHowTo.HowToForm.ActiveForm
			AddHandler m_Timer.Elapsed, New ElapsedEventHandler(AddressOf Timer_Elapsed)
			m_Timer.Start()

			m_Stop = False
		End Sub

		'
		' Update the position of the camera for every timer elapsed event
		'
		Private Sub Timer_Elapsed(sender As Object, e As System.Timers.ElapsedEventArgs)
			'
			' If simulation has not finished
			'
			If Not m_Stop Then
				m_CurrentPosition = (m_Stopwatch.ElapsedMilliseconds / (m_NumberOfSeconds * 1000)) * m_Positions.Count

				'
				' Determine when to stop the simulation
				'
				If m_CurrentPosition > 0 AndAlso m_CurrentPosition < m_Positions.Count Then
					'
					' Calculate the camera position and direction
					'
					m_PreviousPosition = m_CurrentPosition

					Dim fromPosition As Array = m_Positions(CInt(Math.Truncate(m_CurrentPosition)))

					Dim fromPoint As IAgCrdnPoint = VgtHelper.CreatePoint(m_Root.CentralBodies.Earth.Vgt, AgECrdnPointType.eCrdnPointTypeFixedInSystem)
					DirectCast(fromPoint, IAgCrdnPointFixedInSystem).Reference.SetSystem(m_Root.VgtRoot.WellKnownSystems.Earth.Fixed)
					DirectCast(fromPoint, IAgCrdnPointFixedInSystem).FixedPoint.AssignCartesian(CDbl(fromPosition.GetValue(0)), CDbl(fromPosition.GetValue(1)), CDbl(fromPosition.GetValue(2)))

					Dim toPosition As Array = CatmullRomSpline.CartesianToCartographic(fromPosition, "Earth", m_Root)
					toPosition.SetValue(0.0, 2)
					' We want to look at the Earth's surface.
					toPosition = CatmullRomSpline.CartographicToCartesian(toPosition, "Earth", m_Root)

					Dim toPoint As IAgCrdnPoint = VgtHelper.CreatePoint(m_Root.CentralBodies.Earth.Vgt, AgECrdnPointType.eCrdnPointTypeFixedInSystem)
					DirectCast(toPoint, IAgCrdnPointFixedInSystem).Reference.SetSystem(m_Root.VgtRoot.WellKnownSystems.Earth.Fixed)
					DirectCast(toPoint, IAgCrdnPointFixedInSystem).FixedPoint.AssignCartesian(CDbl(toPosition.GetValue(0)), CDbl(toPosition.GetValue(1)), CDbl(toPosition.GetValue(2)))

					m_Scene.Camera.View(m_Root.VgtRoot.WellKnownAxes.Earth.Fixed, fromPoint, toPoint)
					m_Scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ
					m_Scene.Camera.Axes = m_Root.VgtRoot.WellKnownAxes.Earth.Fixed
					m_Scene.Camera.LockViewDirection = True

					If m_Style = CameraUpdaterStyle.Rotating Then
						m_Scene.Camera.Axes = VgtHelper.CreateAxes(m_Root.CentralBodies.Earth.Vgt, AgECrdnAxesType.eCrdnAxesTypeFixed)
					End If
				Else
					'
					' Stop the simulation and calculates the final camera position and direction
					'
					m_Stop = True
                    m_Timer.Stop()
					m_Timer.Close()
					m_Timer.Dispose()

					Dim fromPosition As Array = m_Positions(CInt(Math.Truncate(m_PreviousPosition)))

					Dim fromPoint As IAgCrdnPoint = VgtHelper.CreatePoint(m_Root.CentralBodies.Earth.Vgt, AgECrdnPointType.eCrdnPointTypeFixedInSystem)
					DirectCast(fromPoint, IAgCrdnPointFixedInSystem).Reference.SetSystem(m_Root.VgtRoot.WellKnownSystems.Earth.Fixed)
					DirectCast(fromPoint, IAgCrdnPointFixedInSystem).FixedPoint.AssignCartesian(CDbl(fromPosition.GetValue(0)), CDbl(fromPosition.GetValue(1)), CDbl(fromPosition.GetValue(2)))

					Dim toPosition As Array = m_Positions(m_Positions.Count - 1)
					Dim toPoint As IAgCrdnPoint = VgtHelper.CreatePoint(m_Root.CentralBodies.Earth.Vgt, AgECrdnPointType.eCrdnPointTypeFixedInSystem)
					DirectCast(toPoint, IAgCrdnPointFixedInSystem).Reference.SetSystem(m_Root.VgtRoot.WellKnownSystems.Earth.Fixed)
					DirectCast(toPoint, IAgCrdnPointFixedInSystem).FixedPoint.AssignCartesian(CDbl(toPosition.GetValue(0)), CDbl(toPosition.GetValue(1)), CDbl(toPosition.GetValue(2)))

					m_Scene.Camera.View(m_Root.VgtRoot.WellKnownAxes.Earth.Fixed, fromPoint, toPoint)
					m_Scene.Camera.Axes = VgtHelper.CreateAxes(m_Root.CentralBodies.Earth.Vgt, AgECrdnAxesType.eCrdnAxesTypeFixed)
					m_Scene.Camera.LockViewDirection = False
				End If

				m_Scene.Render()
			End If
		End Sub

		'
		' Member variables
		'
		Private m_Style As CameraUpdaterStyle

		Private m_Root As AgStkObjectRoot
		Private m_Scene As IAgStkGraphicsScene
		Private m_Positions As List(Of Array)
		Private m_CurrentPosition As Double
		Private m_PreviousPosition As Double

		Private m_NumberOfSeconds As Double
		Private m_Timer As System.Timers.Timer
		Private m_Stopwatch As Stopwatch
		Private m_Stop As Boolean
	End Class
End Namespace

Imports System.Collections.Generic
Imports System.Windows.Forms
#Region "UsingDirectives"
Imports System.Drawing
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace Camera
	Class CameraFollowingSplineCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("Camera\CameraFollowingSplineCodeSnippet.vb")
		End Sub

        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            '
            ' Create a camera transition from Washington D.C. to New Orleans
            '
            Dim startPosition As Array = New Object() {attachID("$startLat$Start latitude$", 38.85), attachID("$startLong$Start longitude$", -77.04), attachID("$startAlt$Start altitude$", 0.0)}
            Dim endPosition As Array = New Object() {attachID("$endLat$End latitude$", 29.98), attachID("$endLong$End longitude$", -90.25), attachID("$endAlt$End altitude$", 0.0)}
            Dim spline As New CatmullRomSpline(root, attachID("$planetName$Name of the planet to place the start and end points$", "Earth"), startPosition, endPosition, attachID("$transitionAlt$Max altitude of the transition$", 2000000))
            '#End Region

            m_Spline = spline

            m_PointBatch = TryCast(CreatePointBatch(startPosition, endPosition, manager), IAgStkGraphicsPrimitive)
            m_TextBatch = TryCast(CreateTextBatch("Washington D.C.", "New Orleans", startPosition, endPosition, manager), IAgStkGraphicsPrimitive)

            manager.Primitives.Add(m_PointBatch)
            manager.Primitives.Add(m_TextBatch)

            OverlayHelper.AddTextBox("A Catmull-Rom spline is used to smoothly zoom from one " & vbCrLf & _
                                     "location to another, over a given number of seconds, and " & vbCrLf & _
                                     "reaching a specified maximum altitude." & vbCrLf & vbCrLf & _
                                     "You can use this technique in your applications by including " & vbCrLf & _
                                     "the CatmullRomSpline and CameraUpdater classes from the HowTo.", manager)
            'm_DebugPointBatch = CreateDebugPoints(root) as IAgStkGraphicsPrimitive;
            'manager.Primitives.Add(m_DebugPointBatch);
        End Sub

		Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			If m_CameraUpdater Is Nothing OrElse Not m_CameraUpdater.IsRunning() Then
				Dim spline As CatmullRomSpline = m_Spline

				'#Region "CodeSnippet"
				Dim cameraUpdater As New CameraUpdater(scene, root, spline.InterpolatorPoints, 6)
				'#End Region

				m_CameraUpdater = cameraUpdater
			End If
		End Sub

		Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
			m_CameraUpdater.Dispose()

			m_Spline = Nothing
			m_CameraUpdater = Nothing

			manager.Primitives.Remove(m_PointBatch)
			manager.Primitives.Remove(m_TextBatch)
			OverlayHelper.RemoveTextBox(manager)

			If m_DebugPointBatch IsNot Nothing Then
				manager.Primitives.Remove(m_DebugPointBatch)
				m_DebugPointBatch = Nothing
			End If

			m_PointBatch = Nothing
			m_TextBatch = Nothing

			scene.Render()
		End Sub

		'
		' Creates points for the interpolator curve (for debugging purposes)
		'
		Private Function CreateDebugPoints(root As AgStkObjectRoot) As IAgStkGraphicsPointBatchPrimitive
			Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

			Dim positions As IList(Of Array) = New List(Of Array)()
			For Each c As Array In m_Spline.InterpolatorPoints
				positions.Add(CatmullRomSpline.CartesianToCartographic(c, "Earth", root))
			Next
			Dim positionsArray As Array = ConvertIListToArray(positions)

			Dim colorsArray As Array = Array.CreateInstance(GetType(Object), positionsArray.Length \ 3)
			For i As Integer = 0 To (positionsArray.Length \ 3) - 1
                colorsArray.SetValue(Color.Blue.ToArgb(), i)
			Next

			Dim debugPointBatch As IAgStkGraphicsPointBatchPrimitive = manager.Initializers.PointBatchPrimitive.Initialize()
            debugPointBatch.SetCartographicWithColorsAndRenderPass("Earth", positionsArray, colorsArray, AgEStkGraphicsRenderPassHint.eStkGraphicsRenderPassHintOpaque)
			debugPointBatch.PixelSize = 8

			Return debugPointBatch
		End Function

		'
		' Creates the points for the two cities
		'
		Private Shared Function CreatePointBatch(start As Array, [end] As Array, manager As IAgStkGraphicsSceneManager) As IAgStkGraphicsPointBatchPrimitive
			Dim positionsArray As Array = ConvertIListToArray(start, [end])

            Dim colors As Array = New Object() {Color.Red.ToArgb(), Color.Red.ToArgb()}

			Dim pointBatch As IAgStkGraphicsPointBatchPrimitive = manager.Initializers.PointBatchPrimitive.Initialize()
            pointBatch.SetCartographicWithColorsAndRenderPass("Earth", positionsArray, colors, AgEStkGraphicsRenderPassHint.eStkGraphicsRenderPassHintOpaque)
			pointBatch.PixelSize = 8

			Return pointBatch
		End Function

		'
		' Creates the text for the two cities
		'
		Private Shared Function CreateTextBatch(startName As String, endName As String, start As Array, [end] As Array, manager As IAgStkGraphicsSceneManager) As IAgStkGraphicsTextBatchPrimitive
			Dim text As Array = New Object() {startName, endName}

			Dim positionsArray As Array = ConvertIListToArray(start, [end])

			Dim parameters As IAgStkGraphicsTextBatchPrimitiveOptionalParameters = manager.Initializers.TextBatchPrimitiveOptionalParameters.Initialize()

			Dim pixelOffset As Array = New Object() {3, 3}

			parameters.PixelOffset = pixelOffset

            Dim font As IAgStkGraphicsGraphicsFont = manager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline("Arial", 12, AgEStkGraphicsFontStyle.eStkGraphicsFontStyleRegular, True)
			Dim textBatch As IAgStkGraphicsTextBatchPrimitive = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font)
            DirectCast(textBatch, IAgStkGraphicsPrimitive).Color = Color.White
            textBatch.OutlineColor = Color.Red
			textBatch.SetCartographicWithOptionalParameters("Earth", positionsArray, text, parameters)

			Return textBatch
		End Function

		'
		' Member variables
		'
		Private m_Spline As CatmullRomSpline
		Private m_CameraUpdater As CameraUpdater

		Private m_DebugPointBatch As IAgStkGraphicsPrimitive
		Private m_PointBatch As IAgStkGraphicsPrimitive
		Private m_TextBatch As IAgStkGraphicsPrimitive
	End Class
End Namespace

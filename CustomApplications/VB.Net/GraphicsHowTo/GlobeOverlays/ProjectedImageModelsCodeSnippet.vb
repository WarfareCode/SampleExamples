Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.IO

#Region "UsingDirectives"
Imports System.Globalization
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports System.Drawing
Imports AGI.STKUtil
Imports AGI.STKVgt
#End Region

Class ProjectedImageModelsCodeSnippet
	Inherits CodeSnippet
	Public Sub New()
        MyBase.New("GlobeOverlays\ProjectedImageModelsCodeSnippet.vb")
    End Sub

    Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
        Dim videoFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "ProjectedImagery/buildings.avi").FullPath
        Dim providerFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "ProjectedImagery/buildings.txt").FullPath
        ExecuteSnippet(scene, root, videoFile, providerFile)
    End Sub

    Public Sub ExecuteSnippet(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot, <AGI.CodeSnippets.CodeSnippet.Parameter("videoFile", "Video file to project")> ByVal videoFile As String, <AGI.CodeSnippets.CodeSnippet.Parameter("providerFile", "Text file containing position and orientation data for the projection")> ByVal providerFile As String)
        Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
        Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)
        Dim animationSettings As IAgScAnimation = DirectCast(root.CurrentScenario, IAgScenario).Animation
        Dim scenario As IAgScenario = DirectCast(root.CurrentScenario, IAgScenario)
        Try
            '
            ' Set-up the animation for this specific example
            '
            animation.Pause()
            animationSettings.AnimStepValue = 1.0 / 7.5
            animationSettings.RefreshDelta = 1.0 / 15.0
            animationSettings.RefreshDeltaType = AgEScRefreshDeltaType.eRefreshDelta
            scenario.StopTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "05 Oct 2010 16:00:52.000").Format("epSec"))
            scenario.StartTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "05 Oct 2010 16:00:00.000").Format("epSec"))
            animationSettings.StartTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "05 Oct 2010 16:00:00.000").Format("epSec"))
            animationSettings.EnableAnimCycleTime = True
            animationSettings.AnimCycleTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "05 Oct 2010 16:00:52.000").Format("epSec"))
            animationSettings.AnimCycleType = AgEScEndLoopType.eLoopAtTime
            animation.Rewind()

            '#Region "CodeSnippet"
            '
            ' Enable Raster Model Projection
            '
            scene.GlobeOverlaySettings.ProjectedRasterModelProjection = True

            '
            ' Add projected raster globe overlay with a raster and projection stream
            '
            Dim videoStream As IAgStkGraphicsVideoStream = manager.Initializers.VideoStream.InitializeWithStringUri( _
                videoFile)
            videoStream.Playback = AgEStkGraphicsVideoPlayback.eStkGraphicsVideoPlaybackTimeInterval
            videoStream.IntervalStartTime = root.ConversionUtility.NewDate(attachID("$dateFormat$Format of the date$", "UTCG"), attachID("$startDate$Date to begin the projection$", "05 Oct 2010 16:00:00.000"))
            videoStream.IntervalEndTime = root.ConversionUtility.NewDate(attachID("$dateFormat$Format of the date$", "UTCG"), attachID("$endDate$Date to end the projection$", "05 Oct 2010 16:00:52.000"))

            Dim projectionProvider As New PositionOrientationProvider( _
                providerFile, root)

            Dim activator As IAgStkGraphicsProjectionRasterStreamPluginActivator = manager.Initializers.ProjectionRasterStreamPluginActivator.Initialize()
            Dim proxy As IAgStkGraphicsProjectionRasterStreamPluginProxy = activator.CreateFromDisplayName(attachID("$pluginDisplayName$DisplayName of the ProjectionRasterStreamPlugin$", "ProjectionRasterStreamPlugin.VBNET"))

            '
            ' Use reflection to set the plugin's properties
            '
            Dim plugin As Type = proxy.RealPluginObject.[GetType]()
            plugin.GetProperty("NearPlane").SetValue(proxy.RealPluginObject, attachID("$nearPlane$The near plane of the projection$", 20.0), Nothing)
            plugin.GetProperty("FarPlane").SetValue(proxy.RealPluginObject, attachID("$farPlane$The far plane of the projection$", 300.0), Nothing)
            plugin.GetProperty("FieldOfViewHorizontal").SetValue(proxy.RealPluginObject, attachID("$fieldOfViewHorizontal$The horizontal field of view$", 0.23271), Nothing)
            plugin.GetProperty("FieldOfViewVertical").SetValue(proxy.RealPluginObject, attachID("$fieldOfViewVertical$The vertical field of view$", 0.17593), Nothing)
            plugin.GetProperty("Dates").SetValue(proxy.RealPluginObject, projectionProvider.Dates, Nothing)
            plugin.GetProperty("Positions").SetValue(proxy.RealPluginObject, projectionProvider.Positions, Nothing)
            plugin.GetProperty("Orientations").SetValue(proxy.RealPluginObject, projectionProvider.Orientations, Nothing)


            Dim projectionStream As IAgStkGraphicsProjectionStream = proxy.ProjectionStream

            Dim rasterProjection As IAgStkGraphicsProjectedRasterOverlay = manager.Initializers.ProjectedRasterOverlay.Initialize(DirectCast(videoStream, IAgStkGraphicsRaster), DirectCast(projectionStream, IAgStkGraphicsProjection))
            rasterProjection.ShowFrustum = attachID("$showFrustum$Determines if the frustum is shown$", True)
            rasterProjection.FrustumColor = attachID("$frustumColor$The System.Drawing.Color of the frustum$", Color.Black)
            rasterProjection.FrustumTranslucency = attachID("$frustumTranslucency$The translucency of the frustum$", 0.5F)
            rasterProjection.ShowShadows = attachID("$showShadows$Determines if shadows are shown$", True)
            rasterProjection.ShadowColor = attachID("$shadowColor$The System.Drawing.Color of the shadow$", System.Drawing.Color.Orange)
            rasterProjection.ShadowTranslucency = attachID("$shadowTranslucency$The translucency of the shadow$", 0.5F)
            rasterProjection.ShowFarPlane = attachID("$showFarPlane$Determines if the far plane is shown$", True)
            rasterProjection.FarPlaneColor = attachID("$farPlaneColor$The System.Drawing.Color of the far plane$", System.Drawing.Color.LightBlue)
            rasterProjection.Color = attachID("$tintColor$The System.Drawing.Color of the tint to add to the raster$", System.Drawing.Color.LightBlue)
            DirectCast(rasterProjection, IAgStkGraphicsGlobeImageOverlay).Translucency = attachID("$rasterTranslucency$The translucency of the raster$", 0.2F)

            scene.CentralBodies.Earth.Imagery.Add(DirectCast(rasterProjection, IAgStkGraphicsGlobeImageOverlay))

            '#End Region

            m_Overlay = DirectCast(rasterProjection, IAgStkGraphicsGlobeImageOverlay)
        Catch e As Exception
            If e.Message.Contains("ProjectionRasterStreamPlugin") Then
                MessageBox.Show("A COM exception has occurred." & vbLf & vbLf & _
                                    "It is possible that one of the following may be the issue:" & vbLf & vbLf & _
                                    "1. ProjectionRasterStreamPlugin.dll is not registered for COM interop." & vbLf & vbLf & _
                                    "2. That the plugin has not been added to the GfxPlugin category within a <install dir>\Plugins\*.xml file." & vbLf & vbLf & _
                                    "To resolve either of these issues:" & vbLf & vbLf & _
                                    "1. To register the plugin, open a Visual Studio " & _
                                    If(IntPtr.Size = 8, "x64 ", "") & "Command Prompt and execute the command:" & vbLf & vbLf & _
                                    vbTab & "regasm /codebase ""<install dir>\<CodeSamples>\Extend\Graphics\VB.Net\ProjectionRasterStreamPlugin\bin\<Config>\ProjectionRasterStreamPlugin.dll""" & vbLf & vbLf & _
                                    vbTab & "Note: if you do not have access to a Visual Studio Command Prompt regasm can be found here:" & vbLf & _
                                    vbTab & "C:\Windows\Microsoft.NET\Framework" & If(IntPtr.Size = 8, "64", "") & "\<.NET Version>\" & vbLf & vbLf & _
                                    "2. To add it to the GfxPlugins plugins registry category:" & vbLf & vbLf & _
                                    vbTab & "a. Copy the Graphics.xml from the <install dir>\CodeSamples\Extend\Graphics\Graphics.xml file to the <install dir>\Plugins directory." & vbLf & vbLf & _
                                    vbTab & "b. Then uncomment the plugin entry that contains a display name of ProjectionRasterStreamPlugin.VBNET." & vbLf & vbLf, "Plugin Not Registered", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                MessageBox.Show("Could not create globe overlay.  Your video card may not support this feature.", _
                                "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
            Return
        End Try

        '
        ' Add model
        '
        Dim model As IAgStkGraphicsModelPrimitive = manager.Initializers.ModelPrimitive.InitializeWithStringUri( _
            New AGI.DataPath(AGI.DataPathRoot.Relative, "Models/phoenix_gray/phoenix.dae").FullPath)

        Dim position As Array = New Object() {33.4918312268, -112.0751720286, 0.0}
        Dim origin As IAgPosition = root.ConversionUtility.NewPositionOnEarth()
        origin.AssignPlanetodetic(CDbl(position.GetValue(0)), CDbl(position.GetValue(1)), CDbl(position.GetValue(2)))
        Dim axes As IAgCrdnAxesFixed = CreateAxes(root, "Earth", origin)
        Dim system__1 As IAgCrdnSystem = CreateSystem(root, "Earth", origin, axes)
        Dim result As IAgCrdnAxesFindInAxesResult = root.VgtRoot.WellKnownAxes.Earth.Fixed.FindInAxes(DirectCast(root.CurrentScenario, IAgScenario).Epoch, DirectCast(axes, IAgCrdnAxes))

        model.SetPositionCartographic("Earth", position)
        model.Orientation = result.Orientation

        manager.Primitives.Add(DirectCast(model, IAgStkGraphicsPrimitive))

        m_Primitive = DirectCast(model, IAgStkGraphicsPrimitive)

        OverlayHelper.AddTextBox("Video is projected onto a model by first initializing a VideoStream " & vbCrLf & _
                                 "object with a video.  A ProjectedRasterOverlay is then created using " & vbCrLf & _
                                 "the video stream and a projection stream defining how to project the " & vbCrLf & _
                                 "video onto the model. Shadows are visualized in orange.", manager)
    End Sub

	Public Overrides Sub View(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
		Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
		Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)
		animation.PlayForward()

		Dim center As Array = m_Primitive.BoundingSphere.Center
		Dim boundingSphere As IAgStkGraphicsBoundingSphere = manager.Initializers.BoundingSphere.Initialize(center, 100)

        ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere, 20, 25)

		scene.Render()
	End Sub

	Public Overrides Sub Remove(scene As IAgStkGraphicsScene, root As AgStkObjectRoot)
		Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
		Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)

		manager.Primitives.Remove(m_Primitive)

		If m_Overlay IsNot Nothing Then
			scene.CentralBodies("Earth").Imagery.Remove(m_Overlay)

			animation.Rewind()
			SetAnimationDefaults(root)
			m_Overlay = Nothing
			m_Primitive = Nothing

			'
			' Disable Raster Model Projection
			'
			scene.GlobeOverlaySettings.ProjectedRasterModelProjection = False

			OverlayHelper.RemoveTextBox(manager)
			scene.Render()
		End If
	End Sub

	Private m_Primitive As IAgStkGraphicsPrimitive
	Private m_Overlay As IAgStkGraphicsGlobeImageOverlay
End Class

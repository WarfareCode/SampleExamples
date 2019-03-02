#Region "UsingDirectives"
Imports System.Globalization
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.IO
Imports AGI.STKGraphics
Imports AGI.STKObjects
Imports AGI.STKUtil
#End Region

Namespace GlobeOverlays
	Class ProjectedImageCodeSnippet
		Inherits CodeSnippet
		Public Sub New()
            MyBase.New("GlobeOverlays\ProjectedImageCodeSnippet.vb")
			m_Terrain = New TerrainOverlayCodeSnippet()
			m_Imagery = New GlobeImageOverlayCodeSnippet()
        End Sub

        Public Overrides Sub Execute(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim videoFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "ProjectedImagery/fig8.avi").FullPath
            Dim providerFile As String = New AGI.DataPath(AGI.DataPathRoot.Relative, "ProjectedImagery/fig8.txt").FullPath
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
                animationSettings.AnimStepValue = 1.0
                animationSettings.RefreshDelta = 1.0 / 30.0
                animationSettings.RefreshDeltaType = AgEScRefreshDeltaType.eRefreshDelta
                scenario.StartTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:00:00.000").Format("epSec"))
                scenario.StopTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:11:58.162").Format("epSec"))
                animationSettings.StartTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:00:00.000").Format("epSec"))
                animationSettings.EnableAnimCycleTime = True
                animationSettings.AnimCycleTime = Double.Parse(root.ConversionUtility.NewDate("UTCG", "30 May 2008 14:11:58.162").Format("epSec"))
                animationSettings.AnimCycleType = AgEScEndLoopType.eLoopAtTime
                animation.Rewind()


                '#Region "CodeSnippet"
                '
                ' Add projected raster globe overlay with a raster and projection stream
                '
                Dim videoStream As IAgStkGraphicsVideoStream = manager.Initializers.VideoStream.InitializeWithStringUri( _
                    videoFile)
                videoStream.Playback = AgEStkGraphicsVideoPlayback.eStkGraphicsVideoPlaybackTimeInterval
                videoStream.IntervalStartTime = root.ConversionUtility.NewDate(attachID("$dateFormat$Format of the date$", "UTCG"), attachID("$startDate$Date to begin the projection$", "30 May 2008 14:00:00.000"))
                videoStream.IntervalEndTime = root.ConversionUtility.NewDate(attachID("$dateFormat$Format of the date$", "UTCG"), attachID("$endDate$Date to end the projection$", "30 May 2008 14:11:58.162"))

                Dim projectionProvider As New PositionOrientationProvider( _
                    providerFile, root)

                Dim activator As IAgStkGraphicsProjectionRasterStreamPluginActivator = manager.Initializers.ProjectionRasterStreamPluginActivator.Initialize()
                Dim proxy As IAgStkGraphicsProjectionRasterStreamPluginProxy = activator.CreateFromDisplayName(attachID("$pluginDisplayName$Display Name of the ProjectionRasterStreamPlugin$", "ProjectionRasterStreamPlugin.VBNET"))

                '
                ' Use reflection to set the plugin's properties
                '
                Dim plugin As Type = proxy.RealPluginObject.[GetType]()
                plugin.GetProperty("NearPlane").SetValue(proxy.RealPluginObject, attachID("$nearPlane$The near plane of the projection$", 20.0), Nothing)
                plugin.GetProperty("FarPlane").SetValue(proxy.RealPluginObject, attachID("$farPlane$The far plane of the projection$", 10000.0), Nothing)
                plugin.GetProperty("FieldOfViewHorizontal").SetValue(proxy.RealPluginObject, attachID("$fieldOfViewHorizontal$The horizontal field of view$", 0.230908805), Nothing)
                plugin.GetProperty("FieldOfViewVertical").SetValue(proxy.RealPluginObject, attachID("$fieldOfViewVertical$The vertical field of view$", 0.174532925), Nothing)
                plugin.GetProperty("Dates").SetValue(proxy.RealPluginObject, projectionProvider.Dates, Nothing)
                plugin.GetProperty("Positions").SetValue(proxy.RealPluginObject, projectionProvider.Positions, Nothing)
                plugin.GetProperty("Orientations").SetValue(proxy.RealPluginObject, projectionProvider.Orientations, Nothing)


                Dim projectionStream As IAgStkGraphicsProjectionStream = proxy.ProjectionStream

                Dim rasterProjection As IAgStkGraphicsProjectedRasterOverlay = manager.Initializers.ProjectedRasterOverlay.Initialize(DirectCast(videoStream, IAgStkGraphicsRaster), DirectCast(projectionStream, IAgStkGraphicsProjection))
                rasterProjection.ShowFrustum = attachID("$showFrustum$Determines if the frustum is shown$", True)
                rasterProjection.ShowShadows = attachID("$showShadows$Determines if shadows are shown$", True)

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
            ' Add terrain and imagery
            '
            m_Terrain.Execute(scene, root)
            m_Imagery.Execute(scene, root)

            OverlayHelper.AddTextBox("Video is projected onto terrain by first initializing a VideoStream " & vbCrLf & _
                                     "object with a video.  A ProjectedRasterOverlay is then created using " & vbCrLf & _
                                     "the video stream and a projection stream defining how to project the " & vbCrLf & _
                                     "video onto terrain.", manager)
        End Sub

        Public Overrides Sub View(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)
            animation.PlayForward()

            m_Terrain.View(scene, root)
        End Sub

        Public Overrides Sub Remove(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            m_Imagery.Remove(scene, root)
            m_Terrain.Remove(scene, root)

            If m_Overlay IsNot Nothing Then
                Dim animation As IAgAnimation = DirectCast(root, IAgAnimation)

                scene.CentralBodies("Earth").Imagery.Remove(m_Overlay)

                animation.Rewind()
                SetAnimationDefaults(root)

                m_Overlay = Nothing
                OverlayHelper.RemoveTextBox(DirectCast(root.CurrentScenario, IAgScenario).SceneManager)
                scene.Render()
            End If
        End Sub

        Private m_Overlay As IAgStkGraphicsGlobeImageOverlay
        Private m_Imagery As GlobeImageOverlayCodeSnippet
        Private m_Terrain As TerrainOverlayCodeSnippet
    End Class
End Namespace

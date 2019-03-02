#Region "UsingDirectives"

Imports System.Drawing
Imports GraphicsHowTo.Primitives
Imports AGI.STKGraphics
Imports AGI.STKObjects

#End Region

Namespace Camera
    Public Class CameraRecordingSnapshotCodeSnippet
        Inherits CodeSnippet

        Public Sub New()
            MyBase.New("Camera\CameraRecordingSnapshotCodeSnippet.vb")
        End Sub

        ' Name
        ' Description 
        ' Category    
        ' References  
        ' Namespaces  
        ' EID  
        <AGI.CodeSnippets.CodeSnippet( _
            "TakeASnapshotOfTheCamera'sView", _
            "Take a snapshot of the camera's view", _
            "Graphics | Camera", _
            "System", _
            "System", _
            "AgSTKGraphicsLib~IAgStkGraphicsScene" _
            )> _
        Public Overrides Sub Execute(<AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")> ByVal scene As IAgStkGraphicsScene, <AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")> ByVal root As AgStkObjectRoot)
            '#Region "CodeSnippet"
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager

            '
            ' The snapshot can be saved to a file, texture, image, or the clipboard
            '
            Dim texture As IAgStkGraphicsRendererTexture2D = scene.Camera.Snapshot.SaveToTexture()

            Dim overlay As IAgStkGraphicsTextureScreenOverlay = CreateOverlayFromTexture(texture, root)
            Dim screenOverlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays, IAgStkGraphicsScreenOverlayCollectionBase)
            screenOverlayManager.Add(DirectCast(overlay, IAgStkGraphicsScreenOverlay))
            '#End Region

            OverlayHelper.AddTextBox("A snapshot of the current view is saved to a texture," & vbCrLf & _
                                     "which is then used to create a screen overlay.  Snapshots " & vbCrLf & _
                                     "can also be saved to a file, image, or the clipboard.", manager)

            scene.Render()
            m_Overlay = overlay
        End Sub

        Private Function CreateOverlayFromTexture(ByVal texture As IAgStkGraphicsRendererTexture2D, ByVal root As AgStkObjectRoot) As IAgStkGraphicsTextureScreenOverlay
            Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
            Dim textureScreenOverlay As IAgStkGraphicsTextureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYTexture(0, 0, texture)
            Dim overlay As IAgStkGraphicsOverlay = DirectCast(textureScreenOverlay, IAgStkGraphicsOverlay)
            overlay.BorderSize = 2
            overlay.BorderColor = Color.White
            overlay.Scale = 0.2
            overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenter

            Return textureScreenOverlay
        End Function

        Public Overrides Sub View(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)

        End Sub

        Public Overrides Sub Remove(ByVal scene As IAgStkGraphicsScene, ByVal root As AgStkObjectRoot)
            If m_Overlay IsNot Nothing Then
                Dim manager As IAgStkGraphicsSceneManager = DirectCast(root.CurrentScenario, IAgScenario).SceneManager
                Dim screenOverlayManager As IAgStkGraphicsScreenOverlayCollectionBase = DirectCast(manager.ScreenOverlays, IAgStkGraphicsScreenOverlayCollectionBase)
                screenOverlayManager.Remove(DirectCast(m_Overlay, IAgStkGraphicsScreenOverlay))
                scene.Render()

                m_Overlay = Nothing
                OverlayHelper.RemoveTextBox(manager)
            End If
        End Sub

        Private m_Overlay As IAgStkGraphicsTextureScreenOverlay
    End Class
End Namespace

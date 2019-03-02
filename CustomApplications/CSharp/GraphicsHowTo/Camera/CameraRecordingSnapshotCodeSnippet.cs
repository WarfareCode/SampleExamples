#region UsingDirectives
using System.Drawing;
using GraphicsHowTo.Primitives;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.Camera
{
    public class CameraRecordingSnapshotCodeSnippet : CodeSnippet
    {
        public CameraRecordingSnapshotCodeSnippet()
            : base(@"Camera\CameraRecordingSnapshotCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "TakeASnapshotOfTheCamera'sView",
            /* Description */ "Take a snapshot of the camera's view",
            /* Category    */ "Graphics | Camera",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsScene"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            //
            // The snapshot can be saved to a file, texture, image, or the clipboard
            //
            IAgStkGraphicsRendererTexture2D texture = scene.Camera.Snapshot.SaveToTexture();

            IAgStkGraphicsTextureScreenOverlay textureScreenOverlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYTexture(0, 0, texture);
            IAgStkGraphicsOverlay overlay = (IAgStkGraphicsOverlay)textureScreenOverlay;
            overlay.BorderSize = 2;
            overlay.BorderColor = Color.White;
            overlay.Scale = 0.2;
            overlay.Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenter;
            IAgStkGraphicsScreenOverlayCollectionBase screenOverlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays;
            screenOverlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);
            #endregion

            OverlayHelper.AddTextBox(
@"A snapshot of the current view is saved to a texture,
which is then used to create a screen overlay.  Snapshots 
can also be saved to a file, image, or the clipboard.", manager);
            
            scene.Render();
            m_Overlay = (IAgStkGraphicsTextureScreenOverlay)overlay;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {

        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                IAgStkGraphicsScreenOverlayCollectionBase screenOverlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays;
                screenOverlayManager.Remove((IAgStkGraphicsScreenOverlay)m_Overlay);
                scene.Render();

                m_Overlay = null;
                OverlayHelper.RemoveTextBox(manager);
            }
        }

        private IAgStkGraphicsTextureScreenOverlay m_Overlay;
    }
}

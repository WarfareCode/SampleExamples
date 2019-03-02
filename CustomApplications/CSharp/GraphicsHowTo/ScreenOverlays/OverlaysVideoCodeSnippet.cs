#region UsingDirectives
using System;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.ScreenOverlays
{
    class OverlaysVideoCodeSnippet : CodeSnippet
    {
        public OverlaysVideoCodeSnippet()
            : base(@"ScreenOverlays\OverlaysVideoCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string videoFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Videos/ShenzhouVII_BX1.wmv").FullPath;
            Execute(scene, root, videoFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddAVideo",
            /* Description */ "Add a video with a texture overlay",
            /* Category    */ "Graphics | ScreenOverlays",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsScreenOverlay"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("videoFile", "The video file")] string videoFile)
        {
            try
            {
#region CodeSnippet
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;

                IAgStkGraphicsVideoStream videoStream = manager.Initializers.VideoStream.InitializeWithStringUri(videoFile);
                videoStream.Playback = /*$playbackMode$The playback mode of the video$*/AgEStkGraphicsVideoPlayback.eStkGraphicsVideoPlaybackRealTime;
                videoStream.Loop = /*$shouldLoop$Whether or not the video will return to the beginning when it reaches the end$*/true;

                IAgStkGraphicsTextureScreenOverlay overlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(
                    /*$xLocation$The X location of the screen overlay$*/0, /*$yLocation$The Y location of the screen overlay$*/0,
                    ((IAgStkGraphicsRaster)videoStream).Width / 4,
                    ((IAgStkGraphicsRaster)videoStream).Height / 4);
                ((IAgStkGraphicsOverlay)overlay).Translucency = /*$translucency$The translucency of the screen overlay$*/0.3f;
                ((IAgStkGraphicsOverlay)overlay).Origin = /*$origin$The origin of the screen overlay$*/AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight;
                ((IAgStkGraphicsOverlay)overlay).BorderSize = /*$borderSize$The size of the border around the screen overlay$*/1;
                ((IAgStkGraphicsOverlay)overlay).BorderTranslucency = /*$borderTranslucency$The translucency of the border$*/0.3f;
                overlay.Texture = manager.Textures.FromRaster((IAgStkGraphicsRaster)videoStream);

                overlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);
#endregion

                m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;
                ((IAgAnimation)root).PlayForward();
            }
            catch
            {
                System.Text.StringBuilder message = new System.Text.StringBuilder("There was a problem accessing the video file located at: ");
                message.Append(videoFile);

                System.Windows.Forms.MessageBox.Show(message.ToString(), "Error finding the video");
            }
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            //
            // Overlays are always fixed to the screen regardless of view
            //
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
                
                ((IAgAnimation)root).Rewind();
                overlayManager.Remove(m_Overlay);
                scene.Render();

                m_Overlay = null;
            }
        }

        private IAgStkGraphicsScreenOverlay m_Overlay;
    };
}

#region UsingDirectives
using System;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.ScreenOverlays
{
    class OverlaysTextureCodeSnippet : CodeSnippet
    {
        public OverlaysTextureCodeSnippet()
            : base(@"ScreenOverlays\OverlaysTextureCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string imageFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/agi_logo_transparent.png").FullPath;
            Execute(scene, root, imageFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddALogo",
            /* Description */ "Add a company logo with a texture overlay",
            /* Category    */ "Graphics | ScreenOverlays",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsScreenOverlay"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("imageFile", "The image file")] string imageFile)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;

            IAgStkGraphicsRendererTexture2D texture2D = manager.Textures.LoadFromStringUri(
                imageFile);

            IAgStkGraphicsTextureScreenOverlay overlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(
                /*$xLocation$The X location of the screen overlay$*/10, /*$yLocation$The yLocation of the screen overlay$*/0,
                texture2D.Template.Width / 2,
                texture2D.Template.Height / 2);
            ((IAgStkGraphicsOverlay)overlay).Translucency = /*$translucency$The translucency of the screen overlay$*/0.1f;
            ((IAgStkGraphicsOverlay)overlay).Origin = /*$origin$The origin of the screen overlay$*/AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterRight;
            overlay.Texture = texture2D;

            overlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);
#endregion

            m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;
            OverlayHelper.AddTextBox(
@"TextureScreenOverlay can be used to overlay a company logo by loading 
the logo image into a Texture2D and providing it to a TextureScreenOverlay.", manager);
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
                
                overlayManager.Remove(m_Overlay);
                m_Overlay = null;

                OverlayHelper.RemoveTextBox(manager);
                scene.Render();
            }
        }

        private IAgStkGraphicsScreenOverlay m_Overlay;
    };
}

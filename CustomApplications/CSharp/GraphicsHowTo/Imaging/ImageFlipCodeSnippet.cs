#region UsingDirectives
using System;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.Imaging
{
    class ImageFlipCodeSnippet : CodeSnippet
    {
        public ImageFlipCodeSnippet()
            : base(@"Imaging\ImageFlipCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string imageFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/f-22a_raptor.png").FullPath;
            Execute(scene, root, imageFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "FlipAnImage",
            /* Description */ "Flip an image",
            /* Category    */ "Graphics | Imaging",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsFlipFilter"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("imageFile", "The image file")] string imageFile)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
            //
            // The URI can be a file path, http, https, or ftp location
            //
            IAgStkGraphicsRaster image = manager.Initializers.Raster.InitializeWithStringUri(
                imageFile);
            image.Flip(/*$flipAxes$The axes the image is flipped about$*/AgEStkGraphicsFlipAxis.eStkGraphicsFlipAxisVertical);

            IAgStkGraphicsRendererTexture2D texture = manager.Textures.FromRaster(image);

            IAgStkGraphicsTextureScreenOverlay overlay = manager.Initializers.TextureScreenOverlay.Initialize();
            ((IAgStkGraphicsOverlay)overlay).Size = new object[]
            {
                /*$overlayWidth$The width of the screen overlay$*/0.2, /*$overlayHeight$The height of the screen overlay$*/0.2,
                /*$overlayWidthUnit$The width unit of the screen overlay$*/AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction,
                /*$overlayHeightUnit$The height unit of the screen overlay$*/AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction
            };
            ((IAgStkGraphicsOverlay)overlay).Origin = /*$overlayOrigin$The origin of the screen overlay$*/AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopLeft;
            overlay.Texture = texture;

            overlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);
#endregion
            OverlayHelper.AddOriginalImageOverlay(manager);
            OverlayHelper.LabelOverlay((IAgStkGraphicsScreenOverlay)overlay, "Flipped", manager);
            m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;
            
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;

            overlayManager.Remove(m_Overlay);
            OverlayHelper.RemoveOriginalImageOverlay(manager);
            scene.Render();

            m_Overlay = null;
        }

        private IAgStkGraphicsScreenOverlay m_Overlay;
        
    };
}

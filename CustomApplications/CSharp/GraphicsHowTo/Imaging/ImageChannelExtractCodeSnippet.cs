#region UsingDirectives
using System;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.Imaging
{
    class ImageChannelExtractCodeSnippet : CodeSnippet
    {
        public ImageChannelExtractCodeSnippet()
            : base(@"Imaging\ImageChannelExtractCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string imageFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/f-22a_raptor.png").FullPath;
            Execute(scene, root, imageFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "ExtractAnImageChannel",
            /* Description */ "Extract the alpha component from an image",
            /* Category    */ "Graphics | Imaging",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsBandExtractFilter"
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

            //
            // Extract the alpha channel from the image
            //
            IAgStkGraphicsBandExtractFilter channelExtract = manager.Initializers.BandExtractFilter.InitializeWithBand(AgEStkGraphicsRasterBand.eStkGraphicsRasterBandAlpha);
            image.ApplyInPlace((IAgStkGraphicsRasterFilter)channelExtract);

            IAgStkGraphicsRendererTexture2D texture = manager.Textures.FromRaster(image);

            //
            // Display the image using a screen overlay
            //
            IAgStkGraphicsTextureScreenOverlay overlay = manager.Initializers.TextureScreenOverlay.Initialize();
            ((IAgStkGraphicsOverlay)overlay).Width = /*$overlayWidth$The width of the screen overlay$*/0.2;
            ((IAgStkGraphicsOverlay)overlay).WidthUnit = /*$overlayWidthUnit$The width unit of the screen overlay$*/AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;
            ((IAgStkGraphicsOverlay)overlay).Height = /*$overlayHeight$The height of the screen overlay$*/0.2;
            ((IAgStkGraphicsOverlay)overlay).HeightUnit = /*$overlayHeightUnit$The height of the screen overlay$*/AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;
            ((IAgStkGraphicsOverlay)overlay).Origin = /*$overlayOrigin$The origin of the screen overlay$*/AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight;
            overlay.Texture = texture;

            overlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);
#endregion
            OverlayHelper.AddOriginalImageOverlay(manager);
            OverlayHelper.LabelOverlay((IAgStkGraphicsScreenOverlay)overlay, "Alpha Channel", manager);
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

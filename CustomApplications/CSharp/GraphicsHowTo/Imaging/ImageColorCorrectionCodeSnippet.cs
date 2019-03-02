#region UsingDirectives
using System;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.Imaging
{
    class ImageColorCorrectionCodeSnippet : CodeSnippet
    {
        public ImageColorCorrectionCodeSnippet()
            : base(@"Imaging\ImageColorCorrectionCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string imageFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/f-22a_raptor.png").FullPath;
            Execute(scene, root, imageFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AdjustAnImagesColor",
            /* Description */ "Adjust brightness, contrast, and gamma",
            /* Category    */ "Graphics | Imaging",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsSequenceFilter"
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
            // Add brightness, contrast, and gamma correction filters to sequence
            //
            IAgStkGraphicsSequenceFilter sequenceFilter = manager.Initializers.SequenceFilter.Initialize();
            sequenceFilter.Add((IAgStkGraphicsRasterFilter)manager.Initializers.BrightnessFilter.InitializeWithAdjustment(/*$brightnessAdjustment$The amount to adjust the brightness of the image$*/.1));
            sequenceFilter.Add((IAgStkGraphicsRasterFilter)manager.Initializers.ContrastFilter.InitializeWithAdjustment(/*$contrastAdjustment$The amount to adjust the contrast of the image$*/.2));
            sequenceFilter.Add((IAgStkGraphicsRasterFilter)manager.Initializers.GammaCorrectionFilter.InitializeWithGamma(/*$gammaAdjustment$The amount to adjust the gamma of the image$*/.9));
            image.ApplyInPlace((IAgStkGraphicsRasterFilter)sequenceFilter);

            IAgStkGraphicsRendererTexture2D texture = manager.Textures.FromRaster(image);

            //
            // Display the image using a screen overlay
            //
            IAgStkGraphicsTextureScreenOverlay overlay = manager.Initializers.TextureScreenOverlay.Initialize();
            ((IAgStkGraphicsOverlay)overlay).Width = /*$overlayWidth$The width of the screen overlay$*/0.2;
            ((IAgStkGraphicsOverlay)overlay).WidthUnit = /*$overlayWidthUnit$The width unit of the screen overlay$*/AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;
            ((IAgStkGraphicsOverlay)overlay).Height = /*$overlayHeight$The height of the screen overlay$*/0.2;
            ((IAgStkGraphicsOverlay)overlay).HeightUnit = /*$overlayHeightUnit$The height of the screen overlay$*/AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;
            ((IAgStkGraphicsOverlay)overlay).Origin = /*$overlayOrigin$The origin of the screen overlay$*/AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenterRight;
            overlay.Texture = texture;

            overlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);
#endregion
            OverlayHelper.AddOriginalImageOverlay(manager);
            OverlayHelper.LabelOverlay((IAgStkGraphicsScreenOverlay)overlay, "Correction Filters", manager);
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

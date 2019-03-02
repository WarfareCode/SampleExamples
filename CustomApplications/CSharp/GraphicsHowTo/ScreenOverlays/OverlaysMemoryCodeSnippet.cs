#region UsingDirectives
using System.Drawing;
using System.Collections.Generic;
using AGI.STKGraphics;
using AGI.STKObjects;
using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
#endregion

namespace GraphicsHowTo.ScreenOverlays
{
    class OverlaysMemoryCodeSnippet : CodeSnippet
    {
        public OverlaysMemoryCodeSnippet()
            : base(@"ScreenOverlays\OverlaysMemoryCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string imageFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "OverlayFromMemory.bmp").FullPath;
            Execute(scene, root, imageFile);
        }

        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("imageFile", "The image file")] string imageFile)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;

            int size = /*$size$The size of the screen overlay$*/256;
            ProcedurallyGeneratedTexture tex = new ProcedurallyGeneratedTexture(size);

            // Create a Bitmap and BitmapData and Lock all pixels to be written
            Bitmap bitmap = new Bitmap(size, size);
            BitmapData bitmapData = bitmap.LockBits(
                                       new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                       ImageLockMode.WriteOnly, bitmap.PixelFormat);

            // Copy the data from the byte array into BitmapData.Scan0
            byte[] data = tex.Next();
            Marshal.Copy(data, 0, bitmapData.Scan0, data.Length);

            // Unlock the pixels and save temporarily
            bitmap.UnlockBits(bitmapData);
            bitmap.Save(imageFile);

            IAgStkGraphicsRaster img = manager.Initializers.Raster.InitializeWithStringUriXYWidthAndHeight(
                imageFile, 0, 0, size, size);

            IAgStkGraphicsTextureScreenOverlay overlay = manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(0, 0, size, size);
            overlay.Texture = manager.Textures.FromRaster((IAgStkGraphicsRaster)img);
            ((IAgStkGraphicsOverlay)overlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenter;

            overlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);
            scene.Render();
#endregion
            m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;
            System.IO.File.Delete(imageFile);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            // Use current view.
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;

            overlayManager.Remove(m_Overlay);
            scene.Render();

            m_Overlay = null;
        }

        private IAgStkGraphicsScreenOverlay m_Overlay;
    };
}

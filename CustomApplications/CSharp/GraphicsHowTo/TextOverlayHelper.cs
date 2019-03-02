using System;
using System.Text;
using System.Drawing;
using AGI.STKGraphics;

namespace GraphicsHowTo
{
    public static class TextOverlayHelper
    {
        public static IAgStkGraphicsTextureScreenOverlay CreateTextOverlay(string text, Font font, IAgStkGraphicsSceneManager manager)
        {
            string textBitmapPath = CreateTextBitmap(text, font);
            IAgStkGraphicsRendererTexture2D textTexture = manager.Textures.LoadFromStringUri(textBitmapPath);

            IAgStkGraphicsTextureScreenOverlay textOverlay =
                manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(
                0, 0, textTexture.Template.Width, textTexture.Template.Height);
            textOverlay.Texture = textTexture;
            textOverlay.TextureFilter = manager.Initializers.TextureFilter2D.NearestClampToEdge;

            System.IO.File.Delete(textBitmapPath);

            return textOverlay;
        }

        public static string CreateTextBitmap(string text, Font font)
        {
            Size textSize = CodeSnippet.MeasureString(text, font);
            Bitmap textBitmap = new Bitmap(textSize.Width, textSize.Height);
            Graphics gfx = Graphics.FromImage(textBitmap);
            gfx.DrawString(text, font, Brushes.White, new PointF(0, 0));

            string filePath = GenerateUniqueFilename();
            textBitmap.Save(filePath);

            return filePath;
        }

        public static void UpdateTextOverlay(IAgStkGraphicsTextureScreenOverlay overlay, string text, Font font, IAgStkGraphicsSceneManager manager)
        {
            string textBitmapPath = CreateTextBitmap(text, font);
            IAgStkGraphicsRendererTexture2D updatedTextTexture = manager.Textures.LoadFromStringUri(textBitmapPath);
            System.IO.File.Delete(textBitmapPath);

            ((IAgStkGraphicsOverlay)overlay).Size = 
                new object[] { updatedTextTexture.Template.Width, updatedTextTexture.Template.Height, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels };
            IAgStkGraphicsRendererTexture2D temp = overlay.Texture;
            overlay.Texture = updatedTextTexture;

            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(temp); //calling this here improves memory performance
        }

        private static string GenerateUniqueFilename()
        {
            string filename = new AGI.DataPath(AGI.DataPathRoot.Relative, "TextOverlay").FullPath + fileNumber.ToString() + ".bmp";
            fileNumber++;

            if (!System.IO.File.Exists(filename))
                return filename;
            else
                return GenerateUniqueFilename();
        }

        private static uint fileNumber = 0;
    }
}

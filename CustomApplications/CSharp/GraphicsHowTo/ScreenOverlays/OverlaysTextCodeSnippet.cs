#region UsingDirectives
using System;
using System.Drawing;
using System.Windows.Forms;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.ScreenOverlays
{
    class OverlaysTextCodeSnippet : 
        CodeSnippet
    {
        public OverlaysTextCodeSnippet()
            : base(@"ScreenOverlays\OverlaysTextCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string temporaryFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "TemoraryTextOverlay.bmp").FullPath;
            Execute(scene, root, temporaryFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddATextOverlay",
            /* Description */ "Write text to a texture overlay",
            /* Category    */ "Graphics | ScreenOverlays",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsScreenOverlay"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("temporaryFile", "The file to which the bitmap will be temporarily saved (should not exist yet)")] string temporaryFile)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
            
            Font font = new Font(/*$fontName$The name of the font to use$*/"Arial", /*$fontSize$The size of the font$*/12, /*$fontStyle$The style of the font$*/FontStyle.Bold);
            string text = /*$text$The text to add to the screen overlay$*/"STK Engine\nAnalytical Graphics\nOverlays";
            Size textSize = MeasureString(text, font);
            Bitmap textBitmap = new Bitmap(textSize.Width, textSize.Height);
            Graphics gfx = Graphics.FromImage(textBitmap);
            gfx.DrawString(text, font, Brushes.White, new PointF(0, 0));

            IAgStkGraphicsTextureScreenOverlay overlay =
                manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(/*$xLocation$The X location of the screen overlay$*/10, /*$yLocation$The Y location of the screen overlay$*/10, textSize.Width, textSize.Height);
            ((IAgStkGraphicsOverlay)overlay).Origin = /*$origin$The origin of the screen overlay$*/AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomLeft;

            //
            // Any bitmap can be written to a texture by temporarily saving the texture to disk.
            //
            string filePath = temporaryFile;
            textBitmap.Save(filePath);
            overlay.Texture = manager.Textures.LoadFromStringUri(filePath);
            System.IO.File.Delete(filePath); // The temporary file is not longer required and can be deleted
            
            overlay.TextureFilter = /*$textureFilter$The texture filter for the overlay$*/manager.Initializers.TextureFilter2D.NearestClampToEdge;

            overlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);
#endregion

            m_Overlay = (IAgStkGraphicsScreenOverlay)overlay;
            OverlayHelper.AddTextBox(
@".NET's Graphics.DrawString method can be used to write 
text into Bitmap for use for the TextureScreenOverlay.", manager);
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

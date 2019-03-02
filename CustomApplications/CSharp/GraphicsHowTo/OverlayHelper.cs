using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text;
using AGI.STKGraphics;
using AGI.STKObjects;

namespace GraphicsHowTo
{
    public static class OverlayHelper
    {
        internal static void AddOriginalImageOverlay(IAgStkGraphicsSceneManager manager)
        {
            if (m_OriginalImageOverlay == null)
            {
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;

                IAgStkGraphicsRendererTexture2D texture = manager.Textures.FromRaster(
                    manager.Initializers.Raster.InitializeWithStringUri(
                    new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/f-22a_raptor.png").FullPath));
                m_OriginalImageOverlay = manager.Initializers.TextureScreenOverlay.Initialize();
                ((IAgStkGraphicsOverlay)m_OriginalImageOverlay).Width = 0.2;
                ((IAgStkGraphicsOverlay)m_OriginalImageOverlay).WidthUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;
                ((IAgStkGraphicsOverlay)m_OriginalImageOverlay).Height = 0.2;
                ((IAgStkGraphicsOverlay)m_OriginalImageOverlay).HeightUnit = AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitFraction;
                m_OriginalImageOverlay.Texture = texture;
                ((IAgStkGraphicsOverlay)m_OriginalImageOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginCenter;
                LabelOverlay((IAgStkGraphicsScreenOverlay)m_OriginalImageOverlay, "Original", manager);

                overlayManager.Add((IAgStkGraphicsScreenOverlay)m_OriginalImageOverlay);
            }
        }

        internal static void RemoveOriginalImageOverlay(IAgStkGraphicsSceneManager manager)
        {
            if (m_OriginalImageOverlay != null)
            {
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
                overlayManager.Remove((IAgStkGraphicsScreenOverlay)m_OriginalImageOverlay);
                m_OriginalImageOverlay = null;
            }
        }

        internal static void AddTimeOverlay(AgStkObjectRoot root)
        {
            if (m_TimeOverlay == null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                m_TimeOverlay = new TimeOverlay(root);
                m_TimeOverlay.SetDefaultStyle();
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
                overlayManager.Add(m_TimeOverlay.RealScreenOverlay);
                PositionStatusOverlays(manager);
            }
        }

        internal static void AddAltitudeOverlay(IAgStkGraphicsScene scene, IAgStkGraphicsSceneManager manager)
        {
            if (m_AltitudeOverlay == null)
            {
                m_AltitudeOverlay = new AltitudeOverlay(scene, manager);
                m_AltitudeOverlay.SetDefaultStyle();
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
                overlayManager.Add(m_AltitudeOverlay.RealScreenOverlay);
                PositionStatusOverlays(manager);
            }
        }

        internal static void AddDistanceOverlay(IAgStkGraphicsScene scene, IAgStkGraphicsSceneManager manager)
        {
            if (m_DistanceOverlay == null)
            {
                m_DistanceOverlay = new DistanceOverlay(scene, manager);
                m_DistanceOverlay.SetDefaultStyle();
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
                overlayManager.Add(m_DistanceOverlay.RealScreenOverlay);
                PositionStatusOverlays(manager);
            }
            PositionStatusOverlays(manager);
        }

        internal static void RemoveTimeOverlay(IAgStkGraphicsSceneManager manager)
        {
            if (m_TimeOverlay != null)
            {
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
                overlayManager.Remove(m_TimeOverlay.RealScreenOverlay);
                PositionStatusOverlays(manager);
                m_TimeOverlay = null;
            }
        }
        internal static void RemoveAltitudeOverlay(IAgStkGraphicsSceneManager manager)
        {
            if (m_AltitudeOverlay != null)
            {
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
                overlayManager.Remove(m_AltitudeOverlay.RealScreenOverlay);
                PositionStatusOverlays(manager);
                m_AltitudeOverlay = null;
            }
        }
        internal static void RemoveDistanceOverlay(IAgStkGraphicsSceneManager manager)
        {
            if (m_DistanceOverlay != null)
            {
                IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
                overlayManager.Remove(m_DistanceOverlay.RealScreenOverlay);
                PositionStatusOverlays(manager);
                m_DistanceOverlay = null;
            }

            PositionStatusOverlays(manager);
        }

        internal static TimeOverlay TimeDisplay
        {
            get { return m_TimeOverlay; }
        }
        internal static AltitudeOverlay AltitudeDisplay
        {
            get { return m_AltitudeOverlay; }
        }
        internal static DistanceOverlay DistanceDisplay
        {
            get { return m_DistanceOverlay; }
        }

        public static void LabelOverlay(IAgStkGraphicsScreenOverlay overlay, string text, IAgStkGraphicsSceneManager manager)
        {
            Font font = new Font("Arial", 12, FontStyle.Bold);

            IAgStkGraphicsTextureScreenOverlay textOverlay = TextOverlayHelper.CreateTextOverlay(text, font, manager);
            ((IAgStkGraphicsOverlay)textOverlay).ClipToParent = false;

            IAgStkGraphicsScreenOverlayCollectionBase parentOverlayManager = ((IAgStkGraphicsOverlay)overlay).Overlays as IAgStkGraphicsScreenOverlayCollectionBase;
            parentOverlayManager.Add((IAgStkGraphicsScreenOverlay)textOverlay);
        }

        /// <summary>
        /// Displays a text box.
        /// </summary>
        /// <param name="text">Text to display.</param>
        internal static void AddTextBox(string text, IAgStkGraphicsSceneManager manager)
        {
            AddTextBox(text, 0, 0, manager);
        }

        /// <summary>
        /// Displays a text box.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// /// <param name="xTranslation">The x translation of the text box.</param>
        /// /// <param name="yTranslation">The y translation of the text box.</param>
        internal static void AddTextBox(string text, double xTranslation, double yTranslation, IAgStkGraphicsSceneManager manager)
        {
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;

            Font font = new Font("Arial", 12, FontStyle.Bold);

            IAgStkGraphicsTextureScreenOverlay overlay = TextOverlayHelper.CreateTextOverlay(text, font, manager);
            ((IAgStkGraphicsOverlay)overlay).BorderSize = 2;
            ((IAgStkGraphicsOverlay)overlay).BorderColor = Color.White;

            Array overlayPosition = ((IAgStkGraphicsOverlay)overlay).Position;
            Array overlaySize = ((IAgStkGraphicsOverlay)overlay).Size;

            IAgStkGraphicsScreenOverlay baseOverlay = manager.Initializers.ScreenOverlay.InitializeWithPosAndSize(ref overlayPosition, ref overlaySize);
            ((IAgStkGraphicsOverlay)baseOverlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopRight;
            ((IAgStkGraphicsOverlay)baseOverlay).TranslationX = xTranslation;
            ((IAgStkGraphicsOverlay)baseOverlay).TranslationY = yTranslation;
            ((IAgStkGraphicsOverlay)baseOverlay).Color = Color.Black;
            ((IAgStkGraphicsOverlay)baseOverlay).Translucency = 0.5f;

            IAgStkGraphicsScreenOverlayCollectionBase baseOverlayManager = ((IAgStkGraphicsOverlay)baseOverlay).Overlays as IAgStkGraphicsScreenOverlayCollectionBase;
            baseOverlayManager.Add((IAgStkGraphicsScreenOverlay)overlay);

            m_TextBox = baseOverlay;

            ((IAgStkGraphicsOverlay)m_TextBox).Position = new object[] { 5, 5, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels };
            overlayManager.Add(m_TextBox);
        }

        /// <summary>
        /// Remove the text box associated with the given snippet.
        /// </summary>
        internal static void RemoveTextBox(IAgStkGraphicsSceneManager manager)
        {
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
            overlayManager.Remove(m_TextBox);
            m_TextBox = null;
        }

        private static void PositionStatusOverlays(IAgStkGraphicsSceneManager manager)
        {
            if (m_TimeOverlay != null)
            {
                m_TimeOverlay.Update(m_TimeOverlay.Value, manager);
                m_TimeOverlay.Position = new object[] { 5, 5, 
                    AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, 
                    AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels };
            }
            if (m_AltitudeOverlay != null)
            {
                m_AltitudeOverlay.Update(m_AltitudeOverlay.Value, manager);
                m_AltitudeOverlay.Position = new object[] { 5, 5 
                    + ((m_TimeOverlay == null) ? 0 : m_TimeOverlay.Height + 5), 
                    AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels,
                    AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels};
            }
            if (m_DistanceOverlay != null)
            {
                m_DistanceOverlay.Update(m_DistanceOverlay.Value, manager);
                m_DistanceOverlay.Position = new object[] { 5, 5
                    + ((m_TimeOverlay == null) ? 0 : m_TimeOverlay.Height + 5)
                    + ((m_AltitudeOverlay == null) ? 0 : m_AltitudeOverlay.Height + 5),
                    AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels,
                    AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels};
            }
        }

        private static IAgStkGraphicsTextureScreenOverlay m_OriginalImageOverlay;
        private static TimeOverlay m_TimeOverlay;
        private static AltitudeOverlay m_AltitudeOverlay;
        private static DistanceOverlay m_DistanceOverlay;

        private static IAgStkGraphicsScreenOverlay m_TextBox;
    }
}
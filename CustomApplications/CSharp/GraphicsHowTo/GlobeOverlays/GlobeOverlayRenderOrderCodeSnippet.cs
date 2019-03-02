#region UsingDirectives
using System;
using System.IO;
using System.Windows.Forms;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.GlobeOverlays
{
    class GlobeOverlayRenderOrderCodeSnippet : CodeSnippet
    {
        public GlobeOverlayRenderOrderCodeSnippet()
            : base(@"GlobeOverlays\GlobeOverlayRenderOrderCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string topOverlayFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "TerrainAndImagery/top.jp2").FullPath;
            string bottomOverlayFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "TerrainAndImagery/bottom.jp2").FullPath;
            Execute(scene, root, topOverlayFile, bottomOverlayFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddOrderedGlobeImagery",
            /* Description */ "Draw an image on top of another",
            /* Category    */ "Graphics | GlobeOverlays",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsGlobeImageOverlay"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("topOverlayFile", "The top globe overlay file")] string topOverlayFile, [AGI.CodeSnippets.CodeSnippet.Parameter("bottomOverlayFile", "The bottom globe overlay file")] string bottomOverlayFile)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            try
            {
#region CodeSnippet
                IAgStkGraphicsGlobeImageOverlay topOverlay = scene.CentralBodies.Earth.Imagery.AddUriString(topOverlayFile);
                IAgStkGraphicsGlobeImageOverlay bottomOverlay = scene.CentralBodies.Earth.Imagery.AddUriString(bottomOverlayFile);

                //
                // Since bottom.jp2 was added after top.jp2, bottom.jp2 will be 
                // drawn on top.  In order to draw top.jp2 on top, we swap the Overlays. 
                //
                scene.CentralBodies.Earth.Imagery.Swap(topOverlay, bottomOverlay);
#endregion

                m_TopOverlay = topOverlay;
                m_BottomOverlay = bottomOverlay;
            }
            catch
            {
                MessageBox.Show("Could not create globe Overlay.  Your video card may not support this feature.",
                                "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            OverlayHelper.AddTextBox(
                @"Swap, BringToFront, and SendToBack methods are used 
to change the ordering of imagery on the globe.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_TopOverlay != null)
            {
                Array top = ((IAgStkGraphicsGlobeOverlay)m_TopOverlay).Extent;
                Array bottom = ((IAgStkGraphicsGlobeOverlay)m_BottomOverlay).Extent;

                scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
                scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;

                ViewHelper.ViewExtent(scene, root, "Earth",
                                           Math.Min((double)top.GetValue(0), (double)bottom.GetValue(0)),
                                           Math.Min((double)top.GetValue(1), (double)bottom.GetValue(1)),
                                           Math.Max((double)top.GetValue(2), (double)bottom.GetValue(2)),
                                           Math.Max((double)top.GetValue(3), (double)bottom.GetValue(3)),
                                           -90,
                                           25);

                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_TopOverlay != null)
            {
                scene.CentralBodies["Earth"].Imagery.Remove(m_TopOverlay);
                scene.CentralBodies["Earth"].Imagery.Remove(m_BottomOverlay);

                m_TopOverlay = null;
                m_BottomOverlay = null;
                OverlayHelper.RemoveTextBox(((IAgScenario)root.CurrentScenario).SceneManager);

                scene.Render();
            }
        }

        private IAgStkGraphicsGlobeImageOverlay m_TopOverlay;
        private IAgStkGraphicsGlobeImageOverlay m_BottomOverlay;
    } 
}

#region UsingDirectives
using System;
using System.IO;
using System.Windows.Forms;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.GlobeOverlays
{
    public class GlobeImageOverlayCodeSnippet : CodeSnippet
    {
        public GlobeImageOverlayCodeSnippet()
            : base(@"GlobeOverlays\GlobeImageOverlayCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string globeOverlayFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/St Helens.jp2").FullPath;
            Execute(scene, root, globeOverlayFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddGlobeImagery",
            /* Description */ "Add jp2 imagery to the globe",
            /* Category    */ "Graphics | GlobeOverlays",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsGlobeImageOverlay"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("globeOverlayFile", "The globe overlay file")] string globeOverlayFile)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsGlobeImageOverlay globeOverlay = null;
            IAgStkGraphicsImageCollection overlays = scene.CentralBodies.Earth.Imagery;
            foreach (IAgStkGraphicsGlobeOverlay overlay in overlays)
            {
                if (overlay.UriAsString != null && overlay.UriAsString.EndsWith("St Helens.jp2", StringComparison.Ordinal))
                {
                    globeOverlay = (IAgStkGraphicsGlobeImageOverlay)overlay;
                    break;
                }
            }

            //
            // Don't load imagery if another code snippet already loaded it.
            //
            if (globeOverlay == null)
            {
                try
                {
#region CodeSnippet
                    //
                    // Either jp2 or pdttx can be used here
                    //
                    IAgStkGraphicsGlobeImageOverlay overlay = scene.CentralBodies.Earth.Imagery.AddUriString(globeOverlayFile);
#endregion

                    m_overlays = overlay;
                }
                catch
                {
                    MessageBox.Show("Could not create globe overlays.  Your video card may not support this feature.",
                        "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                m_overlays = globeOverlay;
            }
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_overlays != null)
            {
                scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
                scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;
                Array extent = ((IAgStkGraphicsGlobeOverlay)m_overlays).Extent;
                scene.Camera.ViewExtent("Earth", ref extent);
                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_overlays != null)
            {
                scene.CentralBodies["Earth"].Imagery.Remove(m_overlays);
                scene.Render();

                m_overlays = null;
            }
        }

        private IAgStkGraphicsGlobeImageOverlay m_overlays;
    } 
}

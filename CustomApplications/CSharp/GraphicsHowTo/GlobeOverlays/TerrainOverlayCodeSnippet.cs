#region UsingDirectives
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.GlobeOverlays
{
    class TerrainOverlayCodeSnippet : CodeSnippet
    {
        public TerrainOverlayCodeSnippet()
            : base(@"GlobeOverlays\TerrainOverlayCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string terrainOverlayFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Textures/St Helens.pdtt").FullPath;
            Execute(scene, root, terrainOverlayFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddGlobeTerrain",
            /* Description */ "Add terrain to the globe",
            /* Category    */ "Graphics | GlobeOverlays",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsGlobeImageOverlay"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("terrainOverlayFile", "The terrain overlay file")] string terrainOverlayFile)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsTerrainOverlay globeOverlay = null;
            IAgStkGraphicsTerrainCollection overlays = scene.CentralBodies.Earth.Terrain;
            foreach (IAgStkGraphicsTerrainOverlay overlay in overlays)
            {
                if (((IAgStkGraphicsGlobeOverlay)overlay).UriAsString.EndsWith("St Helens.pdtt", StringComparison.Ordinal))
                {
                    globeOverlay = overlay;
                    break;
                }
            }

            //
            // Don't load terrain if another code snippet already loaded it.
            //
            if (globeOverlay == null)
            {
                try
                {
#region CodeSnippet
                    IAgStkGraphicsTerrainOverlay overlay = scene.CentralBodies.Earth.Terrain.AddUriString(
                        terrainOverlayFile);
#endregion

                    m_Overlay = overlay;
                }
                catch
                {
                    MessageBox.Show("Could not create globe overlay.  Your video card may not support this feature or you may not have the necessary license.",
                        "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                m_Overlay = globeOverlay;
            }
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
                scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;
                Array extent = ((IAgStkGraphicsGlobeOverlay)m_Overlay).Extent;
                ViewHelper.ViewExtent(scene, root, "Earth", extent,
                    45,
                    15);

                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                scene.CentralBodies["Earth"].Terrain.Remove(m_Overlay);
                scene.Render();

                m_Overlay = null;
            }
        }

        private IAgStkGraphicsTerrainOverlay m_Overlay;
    };
}

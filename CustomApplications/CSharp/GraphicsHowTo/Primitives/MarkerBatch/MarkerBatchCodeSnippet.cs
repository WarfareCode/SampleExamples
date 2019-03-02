#region UsingDirectives
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.Primitives.MarkerBatch
{
    class MarkerBatchCodeSnippet : CodeSnippet
    {
        public MarkerBatchCodeSnippet()
            : base(@"Primitives\MarkerBatch\MarkerBatchCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string markerImageFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/facility.png").FullPath;
            Execute(scene, root, markerImageFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawASetOfMarkers",
            /* Description */ "Draw a set of markers",
            /* Category    */ "Graphics | Primitives | Marker Batch Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsMarkerBatchPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("markerImageFile", "The image file to use for the markers")] string markerImageFile)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            Array positions = new object[]
            {
                /*$lat1$The latitude of the first marker$*/39.88, /*$lon1$The longitude of the first marker$*/-75.25, /*$alt1$The altitude of the first marker$*/0,    // Philadelphia
                /*$lat2$The latitude of the second marker$*/38.85, /*$lon2$The longitude of the second marker$*/-77.04, /*$alt2$The altitude of the second marker$*/0, // Washington, D.C.   
                /*$lat3$The latitude of the third marker$*/29.98, /*$lon3$The longitude of the third marker$*/-90.25, /*$alt3$The altitude of the third marker$*/0, // New Orleans
                /*$lat4$The latitude of the fourth marker$*/37.37, /*$lon4$The longitude of the fourth marker$*/-121.92, /*$alt4$The altitude of the fourth marker$*/0    // San Jose
            };

            IAgStkGraphicsMarkerBatchPrimitive markerBatch = manager.Initializers.MarkerBatchPrimitive.Initialize();
            markerBatch.Texture = manager.Textures.LoadFromStringUri(
                markerImageFile);
            markerBatch.SetCartographic(/*$planetName$The planet on which the markers are placed$*/"Earth", ref positions);

            manager.Primitives.Add((IAgStkGraphicsPrimitive)markerBatch);
#endregion

            m_MarkerBatch = (IAgStkGraphicsPrimitive)markerBatch;
            OverlayHelper.AddTextBox(
@"A collection of positions is passed to the MarkerBatchPrimitive to 
visualize markers for each position.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_MarkerBatch != null)
            {
                ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_MarkerBatch.BoundingSphere);
                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_MarkerBatch != null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                manager.Primitives.Remove(m_MarkerBatch);
                m_MarkerBatch = null;

                OverlayHelper.RemoveTextBox(manager);
                scene.Render();
            }
        }

        private IAgStkGraphicsPrimitive m_MarkerBatch;
    };
}

using System.Windows.Forms;
#region UsingDirectives
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.Picking
{
    class PickPerItemCodeSnippet : CodeSnippet
    {
        public PickPerItemCodeSnippet()
            : base(@"Picking\PickPerItemCodeSnippet.cs")
        {
        }

        public void DoubleClick(IAgStkGraphicsScene scene, AgStkObjectRoot root, int mouseX, int mouseY)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IList<Array> markerPositions = m_markerPositions;

            if (m_MarkerBatch != null)
            {
#region CodeSnippet
                Array selectedMarkerCartesianPosition = null;
                //
                // Get a collection of picked objects under the mouse location.
                // The collection is sorted with the closest object at index zero.
                //
                IAgStkGraphicsPickResultCollection collection = scene.Pick(/*$PickX$The X position to pick at$*/mouseX, /*$PickY$The Y position to pick at$*/mouseY);
                if (collection.Count != 0)
                {
                    IAgStkGraphicsObjectCollection objects = collection[0].Objects;
                    IAgStkGraphicsMarkerBatchPrimitive batchPrimitive = objects[0] as IAgStkGraphicsMarkerBatchPrimitive;

                    //
                    // Was a marker in our marker batch picked?
                    //
                    if (batchPrimitive == /*$desiredMarkerBatch$The marker batch to apply the pick action to$*/m_MarkerBatch)
                    {
                        //
                        // Get the index of the particular marker we picked
                        //
                        IAgStkGraphicsBatchPrimitiveIndex markerIndex = objects[1] as IAgStkGraphicsBatchPrimitiveIndex;
                        
                        //
                        // Get the position of the particular marker we picked
                        //
                        Array markerCartographic = markerPositions[markerIndex.Index];

                        IAgPosition markerPosition = root.ConversionUtility.NewPositionOnEarth();
                        markerPosition.AssignPlanetodetic(
                            (double)markerCartographic.GetValue(0),
                            (double)markerCartographic.GetValue(1), 
                            (double)markerCartographic.GetValue(2));

                        double x, y, z;
                        markerPosition.QueryCartesian(out x, out y, out z);

                        selectedMarkerCartesianPosition = new object[] { x, y, z };
                    }
                }
#endregion
                if (selectedMarkerCartesianPosition != null)
                {
                    ViewHelper.ViewBoundingSphere(scene, root, /*$planetName$The name of the planet the marker is located on$*/"Earth",
                                manager.Initializers.BoundingSphere.Initialize(ref selectedMarkerCartesianPosition, /*$radius$The radius of the bounding sphere to view$*/100));
                    scene.Render();
                }
            }
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string markerFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/facility.png").FullPath;
            Execute(scene, root, markerFile, null);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "ZoomToAMarker",
            /* Description */ "Zoom to a particular marker in a batch",
            /* Category    */ "Graphics | Picking",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsPickResult"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("markerFile", "The file to use for the markers")] string markerFile, [AGI.CodeSnippets.CodeSnippet.Parameter("markerPositions", "A list of the marker positions")] IList<Array> markerPositions)
        {
#region CodeSnippet
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

                IList<Array> positions = new List<Array>();
                positions.Add(new object[] {/*$lat1$The first maker's latitude$*/39.88, /*$lon1$The first maker's longitude$*/-75.25, /*$alt1$The first maker's altitude$*/3000.0 });
                positions.Add(new object[] {/*$lat2$The second maker's latitude$*/38.85, /*$lon2$The second maker's longitude$*/-77.04, /*$alt2$The second maker's altitude$*/3000.0 });
                positions.Add(new object[] {/*$lat3$The third maker's latitude$*/38.85, /*$lon3$The third maker's longitude$*/-77.04, /*$alt3$The third maker's altitude$*/0.0 });
                positions.Add(new object[] {/*$lat4$The fourth maker's latitude$*/29.98, /*$lon4$The fourth maker's longitude$*/-90.25, /*$alt4$The fourth maker's altitude$*/0.0 });
                positions.Add(new object[] {/*$lat5$The fifth maker's latitude$*/37.37, /*$lon5$The fifth maker's longitude$*/-121.92, /*$alt5$The fifth maker's altitude$*/0.0 });

                Array positionsArray = Array.CreateInstance(typeof(object), positions.Count * 3);
                for (int i = 0; i < positions.Count; ++i)
                {
                    Array position = positions[i];
                    position.CopyTo(positionsArray, i * 3);
                }

                IAgStkGraphicsMarkerBatchPrimitive markerBatch = manager.Initializers.MarkerBatchPrimitive.Initialize();
                markerBatch.Texture = manager.Textures.LoadFromStringUri(
                    markerFile);
                markerBatch.SetCartographic(/*$planetName$The planet on which the markers will be placed$*/"Earth", ref positionsArray);

                // Save the positions of the markers for use in the pick event
                markerPositions = positions;

                // Enable per item picking
                markerBatch.PerItemPickingEnabled = true;

                manager.Primitives.Add((IAgStkGraphicsPrimitive)markerBatch);
#endregion

            m_MarkerBatch = (IAgStkGraphicsPrimitive)markerBatch;
            OverlayHelper.AddTextBox(
                @"Double click on a marker to zoom to it.

The PerItemPicking property is set to true for the 
batch primitive.  Scene.Pick is called in response 
to the 3D window's MouseDoubleClick event to determine
the primitive and item index under the mouse. Camera.ViewSphere
is then used to zoom to the marker at the picked index.", manager);
            m_markerPositions = markerPositions;
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
        private IList<Array> m_markerPositions;
    };
}

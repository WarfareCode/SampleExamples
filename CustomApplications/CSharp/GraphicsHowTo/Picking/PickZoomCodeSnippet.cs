using System;
using System.IO;
#region UsingDirectives
using System.Collections.ObjectModel;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
#endregion

namespace GraphicsHowTo.Picking
{
    class PickZoomCodeSnippet : CodeSnippet
    {
        public PickZoomCodeSnippet()
            : base(@"Picking\PickZoomCodeSnippet.cs")
        {
        }

        public void DoubleClick(IAgStkGraphicsScene scene, AgStkObjectRoot root, int mouseX, int mouseY)
        {
            if (m_Models != null)
            {
#region CodeSnippet
                IAgStkGraphicsPrimitive selectedModel = null;
                //
                // Get a collection of picked objects under the mouse location.
                // The collection is sorted with the closest object at index zero.
                //
                IAgStkGraphicsPickResultCollection collection = scene.Pick(/*$PickX$The X position to pick at$*/mouseX, /*$PickY$The Y position to pick at$*/mouseY);
                if (collection.Count != 0)
                {
                    IAgStkGraphicsObjectCollection objects = collection[0].Objects;
                    IAgStkGraphicsCompositePrimitive composite = objects[0] as IAgStkGraphicsCompositePrimitive;

                    //
                    // Was a model in our composite picked?
                    //
                    if (composite == /*$desiredPrimitive$The primitive to apply the pick action to$*/m_Models)
                    {
                        selectedModel = objects[1] as IAgStkGraphicsPrimitive;
                    }
                }
#endregion
                if (selectedModel != null)
                {
                    ViewHelper.ViewBoundingSphere(scene, root, /*$planetName$The planet on which the primitive lays$*/"Earth", selectedModel.BoundingSphere);
                    scene.Render();
                }
            }
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "ZoomToAModelOnDoubleClick",
            /* Description */ "Zoom to a model on double click",
            /* Category    */ "Graphics | Picking",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsPickResult"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsCompositePrimitive models = manager.Initializers.CompositePrimitive.Initialize();

            //
            // Create the positions
            //
            Array p0 = new object[3]{39.88, -75.25, 3000.0};
            Array p1 = new object[3]{38.85, -77.04, 0.0};
            Array p2 = new object[3]{29.98, -90.25, 0.0};
            Array p3 = new object[3]{37.37, -121.92, 0.0};

            models.Add(CreateModel(p0, root));
            models.Add(CreateModel(p1, root));
            models.Add(CreateModel(p2, root));
            models.Add(CreateModel(p3, root));

            manager.Primitives.Add((IAgStkGraphicsPrimitive)models);

            OverlayHelper.AddTextBox(
@"Double click on a model to zoom to it.

Scene.Pick is called in response to the 3D window's 
MouseDoubleClick event to determine the primitive under the 
mouse. Camera.ViewSphere is then used to zoom to the primitive.", manager);
            
            m_Models = (IAgStkGraphicsPrimitive)models;
        }

        private static IAgStkGraphicsPrimitive CreateModel(Array position, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            IAgPosition origin = root.ConversionUtility.NewPositionOnEarth();
            origin.AssignPlanetodetic((double)position.GetValue(0), (double)position.GetValue(1), (double)position.GetValue(2));
            IAgCrdnAxesFixed axes = CreateAxes(root, "Earth", origin);
            IAgCrdnSystem system = CreateSystem(root, "Earth", origin, axes);

            IAgCrdnAxesFindInAxesResult result = root.VgtRoot.WellKnownAxes.Earth.Fixed.FindInAxes(((IAgScenario)root.CurrentScenario).Epoch, ((IAgCrdnAxes)axes));

            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(
                new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility.mdl").FullPath);
            model.SetPositionCartographic("Earth", ref position);
            model.Orientation = result.Orientation;
            model.Scale = Math.Pow(10, 3.5);

            return (IAgStkGraphicsPrimitive)model;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Models.BoundingSphere);
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove(m_Models);
            OverlayHelper.RemoveTextBox(manager);
            scene.Render();

            m_Models = null;
            
        }

        private IAgStkGraphicsPrimitive m_Models;
        
    };
}

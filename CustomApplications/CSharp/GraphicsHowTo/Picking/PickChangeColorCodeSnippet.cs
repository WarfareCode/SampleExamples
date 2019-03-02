using System;
#region UsingDirectives
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
#endregion

namespace GraphicsHowTo.Picking
{
    class PickChangeColorCodeSnippet : CodeSnippet
    {
        public PickChangeColorCodeSnippet()
            : base(@"Picking\PickChangeColorCodeSnippet.cs")
        {
        }

        public void MouseMove(IAgStkGraphicsScene scene, AgStkObjectRoot root, int mouseX, int mouseY)
        {
            if (m_Models != null)
            {
#region CodeSnippet
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
                        IAgStkGraphicsPrimitive model = objects[1] as IAgStkGraphicsPrimitive;

                        //
                        // Selected Model
                        //
                        model.Color = /*$pickedColor$The System.Drawing.Color to change the primitive to when it's picked$*/Color.Cyan;

                        if (model != m_SelectedModel)
                        {
                            //
                            // Unselect previous model
                            //
                            if (m_SelectedModel != null)
                            {
                                m_SelectedModel.Color = /*$notPickedColor$The System.Drawing.Color to change the primitive to when it's not picked$*/Color.Red;
                            }
                            m_SelectedModel = model;
                            scene.Render();
                        }
                        return;
                   }
                }

                //
                // Unselect previous model
                //
                if (m_SelectedModel != null)
                {
                    m_SelectedModel.Color = /*$notPickedColor$The System.Drawing.Color to change the primitive to when it's not picked$*/Color.Red;
                    m_SelectedModel = null;
                    scene.Render();
                }
#endregion
            }
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            Execute(scene, root, null);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "ChangeModelColorOnMouseOver",
            /* Description */ "Change a model's color on mouse over",
            /* Category    */ "Graphics | Picking",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsPickResult"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("SelectedModel", "The previously selected model")] IAgStkGraphicsPrimitive m_SelectedModel)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Random r = new Random();

            IAgStkGraphicsCompositePrimitive models = manager.Initializers.CompositePrimitive.Initialize();

            for (int i = 0; i < 25; ++i)
            {
                Array position = new object[3] {
                    35 + r.NextDouble(),
                    -(82 + r.NextDouble()), 0.0};

                models.Add(CreateModel(position, root));
            }

            manager.Primitives.Add((IAgStkGraphicsPrimitive)models);

            OverlayHelper.AddTextBox(
@"Move the mouse over a model to change its color from red to cyan.

This technique, “roll over” picking, is implemented by calling 
Scene.Pick in the 3D window's MouseMove event to determine 
which primitive is under the mouse.", manager);

            m_Models = (IAgStkGraphicsPrimitive)models;
            m_SelectedModel = null;
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
                new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility-colorless.mdl").FullPath);
            model.SetPositionCartographic("Earth", ref position);
            model.Orientation = result.Orientation;
            model.Scale = Math.Pow(10, 2);
            ((IAgStkGraphicsPrimitive)model).Color = Color.Red;

            return (IAgStkGraphicsPrimitive)model;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Models.BoundingSphere, -90, 15);
            scene.Camera.Distance *= 0.7; //zoom in a bit
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove(m_Models);
            OverlayHelper.RemoveTextBox(manager);
            scene.Render();

            m_Models = null;
            m_SelectedModel = null;
            
        }

        private IAgStkGraphicsPrimitive m_Models;
        private IAgStkGraphicsPrimitive m_SelectedModel;
        
    };
}

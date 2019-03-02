using System;
using System.IO;
using System.Collections.Generic;

#region UsingDirectives

using System.Collections.ObjectModel;
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;

#endregion

namespace GraphicsHowTo.Picking
{
    public class PickRectangularCodeSnippet : CodeSnippet
    {
        public PickRectangularCodeSnippet()
            : base(@"Picking\PickRectangularCodeSnippet.cs")
        {
        }

        public void MouseMove(IAgStkGraphicsScene scene, AgStkObjectRoot root, int mouseX, int mouseY)
        {
            IAgStkGraphicsSceneManager manager2 = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager2.ScreenOverlays.Overlays;
            List<IAgStkGraphicsModelPrimitive> SelectedModels = m_SelectedModels;
            if (m_Models != null)
            {
                if (!overlayManager.Contains(m_Overlay))
                {
                    overlayManager.Add(m_Overlay);
                }
                ((IAgStkGraphicsOverlay)m_Overlay).Position = new object[] { mouseX, mouseY, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels, AgEStkGraphicsScreenOverlayUnit.eStkGraphicsScreenOverlayUnitPixels };

#region CodeSnippet
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                //
                // Get a collection of picked objects in a 100 by 100 rectangular region.
                // The collection is sorted with the closest object at index zero.
                //
                List<IAgStkGraphicsModelPrimitive> newModels = new List<IAgStkGraphicsModelPrimitive>();
                IAgStkGraphicsPickResultCollection collection = scene.PickRectangular(/*$PickX$The X position to pick at$*/mouseX - /*$halfRegionSize$Half of the region size to pick within$*/50, /*$PickY$The Y position to pick at$*/mouseY + /*$halfRegionSize$Half of the region size to pick within$*/50, /*$PickX$The X position to pick at$*/mouseX + /*$halfRegionSize$Half of the region size to pick within$*/50, /*$PickY$The Y position to pick at$*/mouseY - /*$halfRegionSize$Half of the region size to pick within$*/50);
                foreach (IAgStkGraphicsPickResult pickResult in collection)
                {
                    IAgStkGraphicsObjectCollection objects = pickResult.Objects;
                    IAgStkGraphicsCompositePrimitive composite = objects[0] as IAgStkGraphicsCompositePrimitive;

                    //
                    // Was a model in our composite picked?
                    //
                    if (composite == /*$desiredPrimitive$The primitive to apply the pick action to$*/m_Models)
                    {
                        IAgStkGraphicsModelPrimitive model = objects[1] as IAgStkGraphicsModelPrimitive;

                        //
                        // Selected Model
                        //
                        ((IAgStkGraphicsPrimitive)model).Color = /*$pickedColor$The System.Drawing.Color to change the primitive to when it's picked$*/Color.Cyan;
                        newModels.Add(model);
                    }
                }
                // 
                // Reset color of models that were previous selected but were not in this pick. 
                //
                foreach (IAgStkGraphicsModelPrimitive selectedModel in SelectedModels)
                {
                    if (!newModels.Contains(selectedModel))
                    {
                        ((IAgStkGraphicsPrimitive)selectedModel).Color = /*$notPickedColor$The System.Drawing.Color to change the primitive to when it's not picked$*/Color.Red;
                    }
                }
                SelectedModels = newModels;

                manager.Render();

#endregion
            }
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            Execute(scene, root, null);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "ChangeModelColorInARegion",
            /* Description */ "Change model colors within a rectangular region",
            /* Category    */ "Graphics | Picking",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsPickResult"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("SelectedModels", "The previously selected models")] List<IAgStkGraphicsModelPrimitive> SelectedModels)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            // Create a screen overlay to visualize the 100 by 100 picking region.
            m_Overlay = manager.Initializers.ScreenOverlay.Initialize(0, 0, 100, 100);
            ((IAgStkGraphicsOverlay)m_Overlay).PinningOrigin = AgEStkGraphicsScreenOverlayPinningOrigin.eStkGraphicsScreenOverlayPinningOriginCenter;
            ((IAgStkGraphicsOverlay)m_Overlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginTopLeft;
            ((IAgStkGraphicsOverlay)m_Overlay).Translucency = .9f;
            ((IAgStkGraphicsOverlay)m_Overlay).BorderSize = 2;

            Random r = new Random();

            IAgStkGraphicsCompositePrimitive models = manager.Initializers.CompositePrimitive.Initialize();
            m_SelectedModels = new List<IAgStkGraphicsModelPrimitive>();

            for (int i = 0; i < 25; ++i)
            {
                Array position = new object[3] {
                    35 + r.NextDouble(),
                    -(82 + r.NextDouble()), 0.0};

                models.Add(CreateModel(position, root));
            }

            manager.Primitives.Add((IAgStkGraphicsPrimitive)models);

            OverlayHelper.AddTextBox(
@"Move the rectangular box over models to change their color.

This technique, “roll over” picking with a rectangular region,
is implemented by calling Scene.PickRectangular in the 3D 
window's MouseMove event to determine which primitives are 
under the rectangular region associated with the mouse.", manager);

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
                new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/facility-colorless.mdl").FullPath);
            model.SetPositionCartographic("Earth", ref position);
            model.Orientation = result.Orientation;
            model.Scale = Math.Pow(10, 2);
            ((IAgStkGraphicsPrimitive)model).Color = Color.Red;

            return (IAgStkGraphicsPrimitive)model;
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            ViewHelper.ViewBoundingSphere(scene, root, "Earth",
                                               m_Models.BoundingSphere,
                                               -90,
                                               15);
            scene.Camera.Distance *= 0.7; //zoom in a bit
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
            
            overlayManager.Remove(m_Overlay);
            manager.Primitives.Remove(m_Models);
            OverlayHelper.RemoveTextBox(manager);
            scene.Render();

            m_Models = null;
            m_SelectedModels = null;
        }

        private IAgStkGraphicsPrimitive m_Models;
        private List<IAgStkGraphicsModelPrimitive> m_SelectedModels;
        private IAgStkGraphicsScreenOverlay m_Overlay;
    }
}

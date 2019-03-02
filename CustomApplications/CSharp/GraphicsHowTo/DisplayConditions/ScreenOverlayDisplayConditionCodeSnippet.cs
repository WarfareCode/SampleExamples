#region UsingDirectives
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.DisplayConditions
{
    class ScreenOverlayDisplayConditionCodeSnippet : CodeSnippet
    {
        public ScreenOverlayDisplayConditionCodeSnippet()
            : base(@"DisplayConditions\ScreenOverlayDisplayConditionCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsModelPrimitive model = CreateTankModel(root, manager);
            IAgPosition position = root.ConversionUtility.NewPositionOnEarth();
            position.AssignCartesian((double)model.Position.GetValue(0), (double)model.Position.GetValue(1), (double)model.Position.GetValue(2));
            Array planetocentricPosition = position.QueryPlanetocentricArray();
            IAgStkGraphicsScreenOverlay overlay = CreateTextOverlay("Mobile SA-10 Launcher\n" +
                "Latitude: " + String.Format("{0:0}", (double)planetocentricPosition.GetValue(0)) + "\n" +
                "Longitude: " + String.Format("{0:0}", (double)planetocentricPosition.GetValue(1)), manager);
            Execute(scene, root, model, overlay);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAScreenOverlayWithACondition",
            /* Description */ "Draw a screen overlay based on viewer distance",
            /* Category    */ "Graphics | DisplayConditions",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsDistanceDisplayCondition"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("Model", "A model")] IAgStkGraphicsModelPrimitive model, [AGI.CodeSnippets.CodeSnippet.Parameter("Overlay", "A overlay")] IAgStkGraphicsScreenOverlay overlay)
        {
            #region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;

            IAgStkGraphicsDistanceToPrimitiveDisplayCondition condition = 
                manager.Initializers.DistanceToPrimitiveDisplayCondition.InitializeWithDistances(
                (IAgStkGraphicsPrimitive)model, /*$minDistance$Minimum distance at which the overlay is visible$*/0, /*$maxDistance$Maximum distance at which the overlay is visible$*/40000);
            ((IAgStkGraphicsOverlay)overlay).DisplayCondition = (IAgStkGraphicsDisplayCondition)condition;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)model);
            overlayManager.Add(overlay);
            #endregion

            m_Model = (IAgStkGraphicsPrimitive)model;
            m_Overlay = overlay;
            OverlayHelper.AddTextBox(
@"Zoom in to within 40 km to see the overlay appear.
 
This is implemented by assigning a 
DistanceToPrimitiveDisplayCondition to the overlay's 
DisplayCondition property.", manager);
            
            OverlayHelper.AddDistanceOverlay(scene, manager);
            OverlayHelper.DistanceDisplay.AddIntervals(new Interval[] { new Interval(0, 40000) });
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Model.BoundingSphere, 225, 25);
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            IAgStkGraphicsScreenOverlayCollectionBase overlayManager = (IAgStkGraphicsScreenOverlayCollectionBase)manager.ScreenOverlays.Overlays;
            manager.Primitives.Remove(m_Model);
            overlayManager.Remove(m_Overlay);
            OverlayHelper.RemoveTextBox(manager);
            OverlayHelper.DistanceDisplay.RemoveIntervals(new Interval[] { new Interval(0, 40000) });
            OverlayHelper.RemoveDistanceOverlay(manager);

            scene.Render();

            m_Model = null;
            m_Overlay = null;
            
        }

        private static IAgStkGraphicsModelPrimitive CreateTankModel(AgStkObjectRoot root, IAgStkGraphicsSceneManager manager)
        {
            IAgStkGraphicsModelPrimitive model = manager.Initializers.ModelPrimitive.InitializeWithStringUri(
                new AGI.DataPath(AGI.DataPathRoot.Relative, "Models/sa10-mobile-a.mdl").FullPath);

            Array position = new object[3] { 56, 37, 0.0 };
            model.SetPositionCartographic("Earth", ref position);
            model.Scale = 1000;


            IAgOrientation orientation = root.ConversionUtility.NewOrientation();
            orientation.AssignEulerAngles(AgEEulerOrientationSequence.e321, -37, -26, 22);
            model.Orientation = orientation;

            return model;
        }

        private IAgStkGraphicsScreenOverlay CreateTextOverlay(string text, IAgStkGraphicsSceneManager manager)
        {
            Font font = new Font("Consolas", 12, FontStyle.Bold);
            Size textSize = CodeSnippet.MeasureString(text, font);
            Bitmap textBitmap = new Bitmap(textSize.Width, textSize.Height);
            Graphics gfx = Graphics.FromImage(textBitmap);
            gfx.DrawString(text, font, Brushes.White, new PointF(0, 0));

            string textBitmapFilepath = new AGI.DataPath(AGI.DataPathRoot.Relative, "SA-10TextOverlay.bmp").FullPath;
            textBitmap.Save(textBitmapFilepath);

            IAgStkGraphicsRendererTexture2D texture = manager.Textures.LoadFromStringUri(textBitmapFilepath);
            IAgStkGraphicsTextureScreenOverlay overlay =
                manager.Initializers.TextureScreenOverlay.InitializeWithXYWidthHeight(0, 60, texture.Template.Width, texture.Template.Height);
            ((IAgStkGraphicsOverlay)overlay).Origin = AgEStkGraphicsScreenOverlayOrigin.eStkGraphicsScreenOverlayOriginBottomCenter;
            overlay.Texture = texture;
            ((IAgStkGraphicsOverlay)overlay).BorderSize = 2;
            ((IAgStkGraphicsOverlay)overlay).BorderColor = Color.White;

            System.IO.File.Delete(textBitmapFilepath);

            return overlay as IAgStkGraphicsScreenOverlay;
        }

        private IAgStkGraphicsPrimitive m_Model;
        private IAgStkGraphicsScreenOverlay m_Overlay;
        
    };
}

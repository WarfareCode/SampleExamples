#region UsingDirectives
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using System;
using AGI.STKVgt;
#endregion

namespace GraphicsHowTo.Primitives.Polyline
{
    class PolylineEllipseCodeSnippet : CodeSnippet
    {
        public PolylineEllipseCodeSnippet()
            : base(@"Primitives\Polyline\PolylineEllipseCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAnEllipse",
            /* Description */ "Draw the outline of an ellipse on the globe",
            /* Category    */ "Graphics | Primitives | Polyline Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array center = new object[] { /*$lat$The latitude of the center point$*/38.85, /*$lon$The longitude of the center point$*/-77.04, /*$alt$The altitude of the center point$*/3000.0 }; // Washington, DC

            IAgStkGraphicsSurfaceShapesResult shape = manager.Initializers.SurfaceShapes.ComputeEllipseCartographic(
                /*$planetName$The planet on which the circle will be placed$*/"Earth", ref center, /*$majorAxisRadius$The radius of the major axis$*/45000, /*$minorAxisRadius$The radius of the minor axis$*/30000, /*$bearing$The bearing of the ellipse$*/45);
            Array positions = shape.Positions;

            IAgStkGraphicsPolylinePrimitive line = manager.Initializers.PolylinePrimitive.InitializeWithType(shape.PolylineType);
            line.Set(ref positions);
            ((IAgStkGraphicsPrimitive)line).Color = /*$color$The color of the ellipse$*/Color.Cyan;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)line);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)line;
            OverlayHelper.AddTextBox(
@"SurfaceShapes.ComputeEllipseCartographic is used to compute the 
positions of an ellipse on the surface, which is visualized with 
the polyline primitive.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", m_Primitive.BoundingSphere);
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove(m_Primitive);
            m_Primitive = null;

            OverlayHelper.RemoveTextBox(manager);
            scene.Render();
        }

        private IAgStkGraphicsPrimitive m_Primitive;
    };
}

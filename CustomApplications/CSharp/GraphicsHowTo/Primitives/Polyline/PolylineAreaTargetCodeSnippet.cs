#region UsingDirectives
using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using AGI.STKUtil;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.Primitives.Polyline
{
    class PolylineAreaTargetCodeSnippet : CodeSnippet
    {
        public PolylineAreaTargetCodeSnippet()
            : base(@"Primitives\Polyline\PolylineAreaTargetCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            Array positions = STKUtil.ReadAreaTargetPoints(
                new AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/_pennsylvania_1.at").FullPath, root);
            Execute(scene, root, positions);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAnAreaTargetOutline",
            /* Description */ "Draw a STK area target outline on the globe",
            /* Category    */ "Graphics | Primitives | Polyline Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsPolylinePrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("positions", "The area target positions")] Array positions)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            IAgStkGraphicsPolylinePrimitive line = manager.Initializers.PolylinePrimitive.Initialize();
            line.Set(ref positions);
            line.Width = /*$width$The width of the polyline$*/2;
            ((IAgStkGraphicsPrimitive)line).Color = /*$color$The color of the polyline$*/Color.Yellow;
            line.DisplayOutline = /*$showOutline$Whether or not an outline is shown around the polyline$*/true;
            line.OutlineWidth = /*$outlineWidth$The width of the outline$*/2;
            line.OutlineColor = /*$outlineColor$The color of the outline$*/Color.Black;

            manager.Primitives.Add((IAgStkGraphicsPrimitive)line);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)line;
            OverlayHelper.AddTextBox(
@"Positions defining the boundary of an STK area target are read from 
disk and visualized with the polyline primitive.", manager);
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

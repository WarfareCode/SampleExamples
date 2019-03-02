#region UsingDirectives
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using System;
#endregion

namespace GraphicsHowTo.Primitives.OrderedComposite
{
    class OrderedCompositeZOrderCodeSnippet : CodeSnippet
    {
        public OrderedCompositeZOrderCodeSnippet()
            : base(@"Primitives\OrderedComposite\OrderedCompositeZOrderCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            Array pennsylvaniaPositions = STKUtil.ReadAreaTargetPoints(
                new AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/_pennsylvania_1.at").FullPath, root);
            Array areaCode610Positions = STKUtil.ReadAreaTargetPoints(
                new AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/610.at").FullPath, root);
            Array areaCode215Positions = STKUtil.ReadAreaTargetPoints(
                new AGI.DataPath(AGI.DataPathRoot.Relative, "AreaTargets/215.at").FullPath, root);
            Array schuylkillPositions = STKUtil.ReadLineTargetPoints(
                new AGI.DataPath(AGI.DataPathRoot.Relative, "LineTargets/Schuylkill.lt").FullPath, root);
            Execute(scene, root, pennsylvaniaPositions, areaCode610Positions, areaCode215Positions, schuylkillPositions);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawASetOfOrderedPrimitives",
            /* Description */ "Z-order primitives on the surface",
            /* Category    */ "Graphics | Primitives | Composite Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsSolidPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("pennsylvaniaPositions", "Pennsylvania positions")] Array pennsylvaniaPositions, [AGI.CodeSnippets.CodeSnippet.Parameter("areaCode610Positions", "Area Code 610 positions")] Array areaCode610Positions, [AGI.CodeSnippets.CodeSnippet.Parameter("areaCode215Positions", "Area Code 215 positions")] Array areaCode215Positions, [AGI.CodeSnippets.CodeSnippet.Parameter("schuylkillPositions", "Schuylkill positions")] Array schuylkillPositions)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            IAgStkGraphicsTriangleMeshPrimitive pennsylvania = manager.Initializers.TriangleMeshPrimitive.Initialize();
            pennsylvania.SetTriangulator((IAgStkGraphicsTriangulatorResult)manager.Initializers.SurfacePolygonTriangulator.Compute(
                "Earth", ref pennsylvaniaPositions));
            ((IAgStkGraphicsPrimitive)pennsylvania).Color = /*$color$System.Drawing.Color of the primitive$*/Color.Yellow;

            IAgStkGraphicsTriangleMeshPrimitive areaCode610 = manager.Initializers.TriangleMeshPrimitive.Initialize();
            areaCode610.SetTriangulator((IAgStkGraphicsTriangulatorResult)manager.Initializers.SurfacePolygonTriangulator.Compute(
                "Earth", ref areaCode610Positions));
            ((IAgStkGraphicsPrimitive)areaCode610).Color = /*$color$System.Drawing.Color of the primitive$*/Color.DarkRed;

            IAgStkGraphicsTriangleMeshPrimitive areaCode215 = manager.Initializers.TriangleMeshPrimitive.Initialize();
            areaCode215.SetTriangulator((IAgStkGraphicsTriangulatorResult)manager.Initializers.SurfacePolygonTriangulator.Compute(
                "Earth", ref areaCode215Positions));
            ((IAgStkGraphicsPrimitive)areaCode215).Color = /*$color$System.Drawing.Color of the primitive$*/Color.Green;

            IAgStkGraphicsPolylinePrimitive schuylkillRiver = manager.Initializers.PolylinePrimitive.Initialize();
            schuylkillRiver.Set(ref schuylkillPositions);
            ((IAgStkGraphicsPrimitive)schuylkillRiver).Color =/*$color$System.Drawing.Color of the primitive$*/ Color.Blue;
            schuylkillRiver.Width = 2;

            IAgStkGraphicsCompositePrimitive composite = manager.Initializers.CompositePrimitive.Initialize();
            composite.Add((IAgStkGraphicsPrimitive)pennsylvania);
            composite.Add((IAgStkGraphicsPrimitive)areaCode610);
            composite.Add((IAgStkGraphicsPrimitive)areaCode215);
            composite.Add((IAgStkGraphicsPrimitive)schuylkillRiver);

            manager.Primitives.Add((IAgStkGraphicsPrimitive)composite);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)composite;

            OverlayHelper.AddTextBox(
@"Using an OrderedCompositePrimitive, the Schuylkill River polyline 
is drawn on top of the 215 and 610 area code triangle meshes, which 
are drawn on top of the Pennsylvania triangle mesh.

Primitives added to the composite last are drawn on top.  The order
of primitives in the composite can be changed with methods such as
BringToFront() and SendToBack().", manager);

            Array text = new object[]
            {
                "Pennsylvania",
                "610",
                "215",
                "Schuylkill River"
            };

            Array positions = new object[12];
            ((IAgStkGraphicsPrimitive)pennsylvania).BoundingSphere.Center.CopyTo(positions, 0);
            ((IAgStkGraphicsPrimitive)areaCode610).BoundingSphere.Center.CopyTo(positions, 3);
            ((IAgStkGraphicsPrimitive)areaCode215).BoundingSphere.Center.CopyTo(positions, 6);
            ((IAgStkGraphicsPrimitive)schuylkillRiver).BoundingSphere.Center.CopyTo(positions, 9);

            IAgStkGraphicsGraphicsFont font = manager.Initializers.GraphicsFont.InitializeWithNameSizeFontStyleOutline("MS Sans Serif", 16, AgEStkGraphicsFontStyle.eStkGraphicsFontStyleBold, true);
            IAgStkGraphicsTextBatchPrimitive textBatch = manager.Initializers.TextBatchPrimitive.InitializeWithGraphicsFont(font);
            ((IAgStkGraphicsPrimitive)textBatch).Color = Color.White;
            textBatch.OutlineColor = Color.Black;
            textBatch.Set(ref positions, ref text);

            composite.Add((IAgStkGraphicsPrimitive)textBatch);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            Array center = m_Primitive.BoundingSphere.Center;
            IAgStkGraphicsBoundingSphere boundingSphere = manager.Initializers.BoundingSphere.Initialize(
                ref center, m_Primitive.BoundingSphere.Radius * 0.35);
            ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere,
                -27, 3);

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
    }
}

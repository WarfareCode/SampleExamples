#region UsingDirectives
using System.Drawing;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
using System;
#endregion

namespace GraphicsHowTo.Primitives.Solid
{
    class SolidCylinderCodeSnippet : CodeSnippet
    {
        public SolidCylinderCodeSnippet()
            : base(@"Primitives\Solid\SolidCylinderCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgPosition origin = root.ConversionUtility.NewPositionOnEarth();
            origin.AssignPlanetodetic(28.488889, -80.577778, 1000);
            IAgCrdnAxesFixed axes = CreateAxes(root, "Earth", origin);
            m_Axes = (IAgCrdnAxes)axes;
            IAgCrdnSystem system = CreateSystem(root, "Earth", origin, axes);
            Execute(scene, root, system);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawACylinder",
            /* Description */ "Draw a cylinder",
            /* Category    */ "Graphics | Primitives | Solid Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsSolidPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("system", "A system for the solid")] IAgCrdnSystem system)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            IAgStkGraphicsSolidTriangulatorResult result = manager.Initializers.CylinderTriangulator.CreateSimple(/*$length$The length of the cylinder along the z axis$*/1000, /*$radius$The radius of the cylinder$*/2000);
            IAgStkGraphicsSolidPrimitive solid = manager.Initializers.SolidPrimitive.Initialize();
            ((IAgStkGraphicsPrimitive)solid).ReferenceFrame = system;
            ((IAgStkGraphicsPrimitive)solid).Color = /*$color$The color of the cylinder$*/Color.Yellow;
            solid.OutlineColor = /*$outlineColor$The System.Drawing.Color of the outline of the cylinder$*/Color.Black;
            solid.OutlineWidth = /*$outlineWdith$the width of the outline$*/2;
            solid.OutlineAppearance = /*$outlineAppearance$The appearance of the outline$*/AgEStkGraphicsOutlineAppearance.eStkGraphicsStylizeBackLines;
            solid.BackLineColor = /*$backLineColor$The System.Drawing.Color of the backLine of the cylinder$*/Color.Black;
            solid.BackLineWidth = /*$backLineWidth$The width of the back line of the cylinder$*/1;
            solid.SetWithResult(result);

            manager.Primitives.Add((IAgStkGraphicsPrimitive)solid);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)solid;
            OverlayHelper.AddTextBox(
@"CylinderTriangulator.Compute is used to compute triangles for a cylinder, 
which are visualized using a SolidPrimitive.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgCrdnAxes referenceAxes = ((IAgCrdnAxesFixed)m_Axes).ReferenceAxes.GetAxes();
            IAgCrdnAxesOnSurface onSurface = (IAgCrdnAxesOnSurface)referenceAxes;
            Array offset = new object[] {m_Primitive.BoundingSphere.Radius * 2.5, m_Primitive.BoundingSphere.Radius * 2.5, m_Primitive.BoundingSphere.Radius * 0.5};
            scene.Camera.ViewOffset(m_Axes, onSurface.ReferencePoint.GetPoint(), ref offset);
            scene.Render();
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            manager.Primitives.Remove(m_Primitive);

            m_Primitive = null;
            m_Axes = null;
            OverlayHelper.RemoveTextBox(manager);

            scene.Render();
        }

        private IAgStkGraphicsPrimitive m_Primitive = null;
        private IAgCrdnAxes m_Axes = null;
    };
}

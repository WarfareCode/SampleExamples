#region UsingDirectives
using System.Drawing;
using AGI.STKObjects;
using AGI.STKGraphics;
using AGI.STKUtil;
using AGI.STKVgt;
using System;
#endregion

namespace GraphicsHowTo.Primitives.Solid
{
    class SolidEllipsoidCodeSnippet : CodeSnippet
    {
        public SolidEllipsoidCodeSnippet()
            : base(@"Primitives\Solid\SolidEllipsoidCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgPosition origin = root.ConversionUtility.NewPositionOnEarth();
            origin.AssignPlanetodetic(28.488889, -80.577778, 4000);
            IAgCrdnAxesFixed axes = CreateAxes(root, "Earth", origin);
            m_Axes = (IAgCrdnAxes)axes;
            IAgCrdnSystem system = CreateSystem(root, "Earth", origin, axes);
            Execute(scene, root, system);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAnEllipsoid",
            /* Description */ "Draw an ellipsoid",
            /* Category    */ "Graphics | Primitives | Solid Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsSolidPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("system", "A system for the solid")] IAgCrdnSystem system)
        {
#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

            Array radii = new object[] {/*$xRadius$X radius$*/2000, /*$yRadius$Y radius$*/1000, /*$zRadius$Z radius$*/1000 };
            IAgStkGraphicsSolidTriangulatorResult result = manager.Initializers.EllipsoidTriangulator.ComputeSimple(ref radii);
            IAgStkGraphicsSolidPrimitive solid = manager.Initializers.SolidPrimitive.Initialize();
            ((IAgStkGraphicsPrimitive)solid).ReferenceFrame = system;
            ((IAgStkGraphicsPrimitive)solid).Color =  /*$color$The System.Drawing.Color of the ellipsoid$*/Color.Orange;
            ((IAgStkGraphicsPrimitive)solid).Translucency = /*$translucency$The translucency of the ellipsoid$*/0.3f;
            solid.OutlineAppearance = /*$outlineAppearance$The appearance of the outline$*/AgEStkGraphicsOutlineAppearance.eStkGraphicsFrontLinesOnly;
            solid.OutlineColor = /*$outlineColor$The System.Drawing.Color of the outline of the ellipsoid$*/Color.Blue;
            solid.OutlineWidth = /*$outlineWdith$the width of the outline$*/2;
            solid.DisplaySilhouette = /*$silhouetteVisible$Whether or not to display the silhouette of the ellipsoid$*/true;
            solid.SetWithResult(result);

            manager.Primitives.Add((IAgStkGraphicsPrimitive)solid);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)solid;
            OverlayHelper.AddTextBox(
@"EllipsoidTriangulator.Compute is used to compute triangles for an 
ellipsoid, which are visualized using a SolidPrimitive.  Its outline 
and silhouette appearance are customized.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgCrdnAxes referenceAxes = ((IAgCrdnAxesFixed)m_Axes).ReferenceAxes.GetAxes();
            IAgCrdnAxesOnSurface onSurface = (IAgCrdnAxesOnSurface)referenceAxes;
            Array offset = new object[] {m_Primitive.BoundingSphere.Radius * 2, m_Primitive.BoundingSphere.Radius * 2, m_Primitive.BoundingSphere.Radius * 0.2};
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

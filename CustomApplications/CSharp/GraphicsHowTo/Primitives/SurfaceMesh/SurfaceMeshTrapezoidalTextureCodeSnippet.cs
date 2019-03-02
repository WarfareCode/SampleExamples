#region UsingDirectives
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
#endregion

namespace GraphicsHowTo.Primitives.SurfaceMesh
{
    class SurfaceMeshTrapezoidalTextureCodeSnippet : CodeSnippet
    {
        public SurfaceMeshTrapezoidalTextureCodeSnippet()
            : base(@"Primitives\SurfaceMesh\SurfaceMeshTrapezoidalTextureCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string textureFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "TerrainAndImagery/surfaceMeshTrapezoidalTexture.jpg").FullPath;
            Execute(scene, root, textureFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAFilledAreaTargetOnGlobe",
            /* Description */ "Draw a filled STK area target on the globe",
            /* Category    */ "Graphics | Primitives | Surface Mesh Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsSurfaceMeshPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("textureFile", "The texture file")] string textureFile)
        {
            IAgStkGraphicsSceneManager videoCheck = ((IAgScenario)root.CurrentScenario).SceneManager;

            if (!videoCheck.Initializers.SurfaceMeshPrimitive.SupportedWithDefaultRenderingMethod())
            {
                MessageBox.Show("Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.",
                    "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

#region CodeSnippet
            IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
            //
            // Load the UAV image where each corner maps to a longitude and latitude defined 
            // in degrees below.
            //
            //    lower left  = (-0.386182, 42.938583)
            //    lower right = (-0.375100, 42.929871)
            //    upper right = (-0.333891, 42.944780)
            //    upper left  = (-0.359980, 42.973438)
            //
            
            IAgStkGraphicsRendererTexture2D texture = manager.Textures.LoadFromStringUri(
                textureFile);

            //
            // Define the bounding extent of the image.  Create a surface mesh that uses this extent.
            //
            IAgStkGraphicsSurfaceMeshPrimitive mesh = manager.Initializers.SurfaceMeshPrimitive.Initialize();
            mesh.Texture = texture;

            Array cartographicExtent = new object[] 
            {
                /*$westLon$Westernmost longitude$*/-0.386182,
                /*$southLat$Southernmost latitude$*/42.929871,
                /*$eastLon$Easternmost longitude$*/-0.333891,
                /*$northLat$Northernmost latitude$*/42.973438 
            };

            IAgStkGraphicsSurfaceTriangulatorResult triangles = manager.Initializers.SurfaceExtentTriangulator.ComputeSimple(/*$planetName$The name of the planet on which the surface mesh will be placed$*/"Earth", ref cartographicExtent);
            mesh.Set(triangles);
            ((IAgStkGraphicsPrimitive)mesh).Translucency = /*$translucency$The translucency of the surface mesh$*/0.0f;

            //
            // Create the texture matrix that maps the image corner points to their actual
            // cartographic coordinates.  A few notes:
            //
            // 1. The TextureMatrix does not do any special processing on these values
            //    as if they were cartographic coordinates.
            //
            // 2. Because of 1., the values only have to be correct relative to each
            //    other, which is why they do not have to be converted to radians.
            //
            // 3. Because of 2., if your image straddles the +/- 180 degs longitude line, 
            //    ensure that longitudes east of the line are greater than those west of
            //    the line.  For example, if one point were 179.0 degs longitude and the
            //    other were to the east at -179.0 degs, the one to the east should be
            //    specified as 181.0 degs.
            //

            Array c0 = new object[] { /*$c0Lon$Longitude of the lower left corner$*/-0.386182, /*$c0Lat$Latitude of the lower left corner$*/42.938583 };
            Array c1 = new object[] { /*$c1Lon$Longitude of the lower right corner$*/-0.375100, /*$c0Lat$Latitude of the lower right corner$*/42.929871 };
            Array c2 = new object[] { /*$c2Lon$Longitude of the upper right corner$*/-0.333891, /*$c0Lat$Latitude of the upper right corner$*/42.944780 };
            Array c3 = new object[] { /*$c3Lon$Longitude of the upper left corner$*/-0.359980, /*$c0Lat$Latitude of the upper left corner$*/42.973438 };

            mesh.TextureMatrix = manager.Initializers.TextureMatrix.InitializeWithRectangles(
                ref c0, ref c1, ref c2, ref c3);
            
            //
            // Enable the transparent texture border option on the mesh so that the texture will not
            // bleed outside of the trapezoid.
            //
            mesh.TransparentTextureBorder = true;

            //
            // Add the surface mesh to the Scene manager
            //
            manager.Primitives.Add((IAgStkGraphicsPrimitive)mesh);
#endregion

            m_Primitive = (IAgStkGraphicsPrimitive)mesh;
            OverlayHelper.AddTextBox(
@"The surface mesh's TextureMatrix is used 
to map a rectangular texture to a trapezoid.", manager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
            scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;

            Array extent = new double[] {
                -0.386182,
                42.929871,
                -0.333891,
                42.973438 };

            ViewHelper.ViewExtent(scene, root, "Earth", extent, 
                -135, 30);
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

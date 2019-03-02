#region UsingDirectives
using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.Primitives.SurfaceMesh
{
    class SurfaceMeshTransformationsCodeSnippet : CodeSnippet
    {
        public SurfaceMeshTransformationsCodeSnippet()
            : base(@"Primitives\SurfaceMesh\SurfaceMeshTransformationsCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string textureFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "Markers/water.png").FullPath;
            Execute(scene, root, textureFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "DrawAMovingTexture",
            /* Description */ "Draw a moving water texture using affine transformations",
            /* Category    */ "Graphics | Primitives | Surface Mesh Primitive",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsSurfaceMeshPrimitive"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("textureFile", "The file to use as the texture of the surface mesh")] string textureFile)
        {
            IAgStkGraphicsSceneManager manager2 = ((IAgScenario)root.CurrentScenario).SceneManager;
            if (!manager2.Initializers.SurfaceMeshPrimitive.SupportedWithDefaultRenderingMethod())
            {
                MessageBox.Show("Your video card does not support the surface mesh primitive.  OpenGL 2.0 is required.",
                    "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
#region CodeSnippet
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                Array cartographicExtent = new object[] 
                {
                    /*$westLon$Westernmost longitude$*/-96,
                    /*$southLat$Southernmost latitude$*/22,
                    /*$eastLon$Easternmost longitude$*/-85,
                    /*$northLat$Northernmost latitude$*/28 
                };

                IAgStkGraphicsSurfaceTriangulatorResult triangles =
                    manager.Initializers.SurfaceExtentTriangulator.ComputeSimple(/*$planetName$The planet on which the surface mesh will be placed$*/"Earth", ref cartographicExtent);

                IAgStkGraphicsRendererTexture2D texture = manager.Textures.LoadFromStringUri(
                    textureFile);
                IAgStkGraphicsSurfaceMeshPrimitive mesh = manager.Initializers.SurfaceMeshPrimitive.Initialize();
                ((IAgStkGraphicsPrimitive)mesh).Translucency = /*$translucency$The translucency of the surface mesh$*/0.3f;
                mesh.Texture = texture;
                mesh.TextureFilter = /*$textureFilter$The type of filter to use for the texture$*/manager.Initializers.TextureFilter2D.LinearRepeat;
                mesh.Set(triangles);
                manager.Primitives.Add((IAgStkGraphicsPrimitive)mesh);
#endregion

                m_Primitive = (IAgStkGraphicsPrimitive)mesh;
                m_Translation = 0;
            }

            OverlayHelper.AddTextBox(
@"Animation effects such as water can be created by modifying a surface 
mesh's TextureMatrix property over time.", manager2);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            IAgAnimation animation = (IAgAnimation)root;
            //
            // Set-up the animation for this specific example
            //
            animation.Pause();
            SetAnimationDefaults(root);
            ((IAgScenario)root.CurrentScenario).Animation.AnimStepValue = 1.0;
            animation.PlayForward();

            if (m_Primitive != null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                Array center = m_Primitive.BoundingSphere.Center;
                IAgStkGraphicsBoundingSphere boundingSphere =
                    manager.Initializers.BoundingSphere.Initialize(ref center, 500000);

                ViewHelper.ViewBoundingSphere(scene, root, "Earth", boundingSphere, -90, 35);
                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Primitive != null)
            {
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;
                IAgAnimation animation = (IAgAnimation)root;
                animation.Rewind();
                SetAnimationDefaults(root);

                manager.Primitives.Remove(m_Primitive);
                m_Primitive = null;

                OverlayHelper.RemoveTextBox(manager);
                scene.Render();
            }
        }

#region CodeSnippet
                internal void TimeChanged(IAgStkGraphicsScene scene, AgStkObjectRoot root, double TimeEpSec)
                {
                    //
                    //  Translate the surface mesh every animation update
                    //
                    if (m_Primitive != null)
                    {
                        IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

                        m_Translation = (float)TimeEpSec;
                        m_Translation /= 1000;

                        Matrix transformation = new Matrix();
                        transformation.Translate(-m_Translation, 0); // Sign determines the direction of apparent flow

                        // Convert the matrix to an object array
                        Array transformationArray = Array.CreateInstance(typeof(object), transformation.Elements.Length);
                        for (int i = 0; i < transformationArray.Length; ++i)
                        {
                            transformationArray.SetValue((object)transformation.Elements.GetValue(i), i);
                        }

                        ((IAgStkGraphicsSurfaceMeshPrimitive)m_Primitive).TextureMatrix =
                            manager.Initializers.TextureMatrix.InitializeWithAffineTransform(ref transformationArray);
                    }
                }
#endregion

        private IAgStkGraphicsPrimitive m_Primitive;
        private float m_Translation;
    };
}

#region UsingDirectives
using AGI.STKGraphics;
using AGI.STKObjects;
#endregion

namespace GraphicsHowTo.Camera
{
    class CameraFixedFrameCodeSnippet : CodeSnippet
    {
        public CameraFixedFrameCodeSnippet()
            : base(@"Camera\CameraFixedFrameCodeSnippet.cs")
        {
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "ViewACenralBody",
            /* Description */ "Change view mode to use Earth's fixed frame",
            /* Category    */ "Graphics | Camera",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsScene"
            )]
        public override void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root)
        {
            OverlayHelper.AddTextBox(
@"By default, the camera is in the Earth's inertial frame.  
During animation, the globe will rotate.  In this example, 
the camera is changed to the Earth's fixed frame, so the 
camera does not move relative to the Earth during animation.", ((IAgScenario)root.CurrentScenario).SceneManager);
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            //
            // Set-up the animation for this specific example
            //
            IAgAnimation animationControl = (IAgAnimation)root;
            IAgScAnimation animationSettings = ((IAgScenario)root.CurrentScenario).Animation;

            animationControl.Pause();
            SetAnimationDefaults(root);
            animationSettings.AnimStepValue = 60.0;
            animationControl.PlayForward();

            scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
            scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;

#region CodeSnippet
            scene.Camera.ViewCentralBody(/*$centralBodyName$Name of the Central Body to view$*/"Earth", /*$axes$The axes for the camera to use$*/root.VgtRoot.WellKnownAxes.Earth.Fixed);
#endregion
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            scene.Camera.ViewCentralBody("Earth", root.VgtRoot.WellKnownAxes.Earth.Inertial);

            OverlayHelper.RemoveTextBox(((IAgScenario)root.CurrentScenario).SceneManager);
            scene.Render();
            ((IAgAnimation)root).Rewind();
        }
    }
}

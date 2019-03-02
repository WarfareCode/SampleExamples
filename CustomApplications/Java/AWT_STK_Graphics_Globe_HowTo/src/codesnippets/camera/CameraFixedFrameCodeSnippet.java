package codesnippets.camera;

//#region Imports

//Java API
import java.awt.*;

//AGI Java API
import agi.stkgraphics.*;
import agi.stkobjects.*;

//Sample API
import utils.helpers.*;
import codesnippets.*;

//#endregion

public class CameraFixedFrameCodeSnippet
extends STKGraphicsCodeSnippet
{
	public CameraFixedFrameCodeSnippet(Component c)
	{
		super(c, "Change view mode to use Earth's fixed frame", "camera", "CameraFixedFrameCodeSnippet.java");
	}

	public void execute(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
		IAgScenario scenario = (IAgScenario)root.getCurrentScenario();
		IAgStkGraphicsSceneManager sceneManager = scenario.getSceneManager();
		
		OverlayHelper.addTextBox(this, sceneManager, "By default, the camera is in the Earth's inertial frame.  \r\nDuring animation, the globe will rotate.  In this example, \r\nthe camera is changed to the Earth's fixed frame, so the \r\ncamera does not move relative to the Earth during animation.");
	}

	public void view(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        // Set-up the animation for this specific example
        IAgAnimation animationControl = (IAgAnimation)root;
        IAgScAnimation animationSettings = ((IAgScenario)root.getCurrentScenario()).getAnimation();

        animationControl.pause();
        STKObjectsHelper.setAnimationDefaults(root);
        animationSettings.setAnimStepValue(60.0);
        animationControl.playForward();

        scene.getCamera().setConstrainedUpAxis(AgEStkGraphicsConstrainedUpAxis.E_STK_GRAPHICS_CONSTRAINED_UP_AXIS_Z);
        scene.getCamera().setAxes(root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());

        //#region CodeSnippet
        scene.getCamera().viewCentralBody("Earth", root.getVgtRoot().getWellKnownAxes().getEarth().getFixed());
        //#endregion
	}

	public void remove(AgStkObjectRootClass root, AgStkGraphicsSceneClass scene)
	throws Throwable
	{
        scene.getCamera().viewCentralBody("Earth", root.getVgtRoot().getWellKnownAxes().getEarth().getInertial());

        OverlayHelper.removeTextBox(((IAgScenario)root.getCurrentScenario()).getSceneManager());
        scene.render();
        ((IAgAnimation)root).rewind();
	}
}
#region UsingDirectives
using System.Windows.Forms;
using System.IO;
using AGI.STKGraphics;
using AGI.STKObjects;
using AGI.STKUtil;
using System;
#endregion

namespace GraphicsHowTo.DisplayConditions
{
    public class TimeDisplayConditionCodeSnippet : CodeSnippet
    {
        public TimeDisplayConditionCodeSnippet()
            : base(@"DisplayConditions\TimeDisplayConditionCodeSnippet.cs")
        {
        }

        public override void Execute(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            string globeOverlayFile = new AGI.DataPath(AGI.DataPathRoot.Relative, "TerrainAndImagery/DisplayConditionExample.jp2").FullPath;
            Execute(scene, root, globeOverlayFile);
        }

        [AGI.CodeSnippets.CodeSnippet(
            /* Name        */ "AddTimeIntervalDisplayCondition",
            /* Description */ "Draw a primitive based on a time interval",
            /* Category    */ "Graphics | DisplayConditions",
            /* References  */ "System",
            /* Namespaces  */ "System",
            /* EID         */ "AgSTKGraphicsLib~IAgStkGraphicsTimeIntervalDisplayCondition"
            )]
        public void Execute([AGI.CodeSnippets.CodeSnippet.Parameter("Scene", "Current Scene")] IAgStkGraphicsScene scene, [AGI.CodeSnippets.CodeSnippet.Parameter("Root", "STK Object Model root")] AgStkObjectRoot root, [AGI.CodeSnippets.CodeSnippet.Parameter("globeOverlayFile", "Location of the globe overlay file")] string globeOverlayFile)
        {
            try
            {
#region CodeSnippet
                IAgStkGraphicsSceneManager manager = ((IAgScenario)root.CurrentScenario).SceneManager;

                IAgStkGraphicsGeospatialImageGlobeOverlay overlay = manager.Initializers.GeospatialImageGlobeOverlay.InitializeWithString(globeOverlayFile);

                IAgDate start = root.ConversionUtility.NewDate(/*$dateFormat$Format of the date$*/"UTCG", /*$startDate$The start date$*/"30 May 2008 14:30:00.000");
                IAgDate end = root.ConversionUtility.NewDate(/*$dateFormat$Format of the date$*/"UTCG", /*$endDate$The end date$*/"30 May 2008 15:00:00.000");

                ((IAgScenario)root.CurrentScenario).Animation.StartTime = double.Parse(start.Subtract("sec", 3600).Format("epSec"));

                IAgStkGraphicsTimeIntervalDisplayCondition condition = 
                    manager.Initializers.TimeIntervalDisplayCondition.InitializeWithTimes(start, end);
                ((IAgStkGraphicsGlobeOverlay)overlay).DisplayCondition = condition as IAgStkGraphicsDisplayCondition;

                scene.CentralBodies.Earth.Imagery.Add((IAgStkGraphicsGlobeImageOverlay)overlay);
#endregion

                m_Overlay = (IAgStkGraphicsGlobeImageOverlay)overlay;

                OverlayHelper.AddTextBox(
                    @"The overlay will be drawn on 5/30/2008 between 
2:30:00 PM and 3:00:00 PM. 

This is implemented by assigning a 
TimeIntervalDisplayCondition to the overlay's 
DisplayCondition property.", manager);

                OverlayHelper.AddTimeOverlay(root);

                m_Start = start;
                m_End = end;
                OverlayHelper.TimeDisplay.AddInterval(double.Parse(m_Start.Format("epSec").ToString()), double.Parse(m_End.Format("epSec").ToString()));
            }
            catch
            {
                MessageBox.Show("Could not create globe overlays.  Your video card may not support this feature.",
                                "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public override void View(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                //
                // Set-up the animation for this specific example
                //
                IAgAnimation animation = (IAgAnimation)root;

                animation.Pause();
                SetAnimationDefaults(root);

                animation.PlayForward();

                scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
                scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;
                Array extent = ((IAgStkGraphicsGlobeOverlay)m_Overlay).Extent;
                scene.Camera.ViewExtent("Earth", ref extent);
                scene.Render();
            }
        }

        public override void Remove(IAgStkGraphicsScene scene, AgStkObjectRoot root)
        {
            if (m_Overlay != null)
            {
                ((IAgAnimation)root).Rewind();
                OverlayHelper.TimeDisplay.RemoveInterval(double.Parse(m_Start.Format("epSec").ToString()), double.Parse(m_End.Format("epSec").ToString()));
                OverlayHelper.RemoveTimeOverlay(((IAgScenario)root.CurrentScenario).SceneManager);
                OverlayHelper.RemoveTextBox(((IAgScenario)root.CurrentScenario).SceneManager);

                scene.CentralBodies.Earth.Imagery.Remove(m_Overlay);
                scene.Render();

                m_Overlay = null;
            }
        }

        private IAgStkGraphicsGlobeImageOverlay m_Overlay;

        private IAgDate m_Start;
        private IAgDate m_End;
    }
}

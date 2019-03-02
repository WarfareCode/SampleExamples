using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using System;
using AGI.STKGraphics;
using AGI.STKUtil;
using AGI.STKObjects;
using AGI.STKVgt;

namespace GraphicsHowTo.Camera
{
    public enum CameraUpdaterStyle { Rotating, Fixed };

    public class CameraUpdater : IDisposable
    {
        //
        // Creates a CameraUpdater
        //
        public CameraUpdater(IAgStkGraphicsScene scene, AgStkObjectRoot root, ICollection<Array> positions, double numberOfSeconds)
        {
            Initialize(scene, root, positions, numberOfSeconds, 60, CameraUpdaterStyle.Fixed);
        }

        //
        // Creates a CameraUpdater with all of the options as parameters
        //
        public CameraUpdater(IAgStkGraphicsScene scene, AgStkObjectRoot root, ICollection<Array> positions, double numberOfSeconds, double framerate, CameraUpdaterStyle style)
        {
            Initialize(scene, root, positions, numberOfSeconds, framerate, style);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CameraUpdater()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_Timer != null)
                {
                    m_Timer.Dispose();
                    m_Timer = null;
                    m_Scene.Camera.LockViewDirection = false;
                }
            }
        }

        //
        // Returns whether or not the animation is still running
        //
        public bool IsRunning()
        {
            return !m_Stop;
        }

        //
        // Initialize method that is called by the constructors
        //
        private void Initialize(IAgStkGraphicsScene scene, AgStkObjectRoot root, ICollection<Array> positions, double numberOfSeconds, double framerate, CameraUpdaterStyle style)
        {
            m_Style = style;

            //
            // Initialize the scene and positions
            //
            m_Root = root;
            m_Scene = scene;
            m_Scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
            m_Scene.Camera.Axes = root.VgtRoot.WellKnownAxes.Earth.Fixed;

            m_Positions = new List<Array>(positions);
            m_CurrentPosition = 0;
            m_PreviousPosition = 0;

            //
            // Initialize the stopwatch and timer
            //
            m_NumberOfSeconds = numberOfSeconds;
            m_Stopwatch = new Stopwatch();
            m_Stopwatch.Start();

            m_Timer = new System.Timers.Timer(1000 / framerate);
            m_Timer.SynchronizingObject = GraphicsHowTo.HowToForm.ActiveForm;
            m_Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            m_Timer.Start();

            m_Stop = false;
        }

        //
        // Update the position of the camera for every timer elapsed event
        //
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //
            // If simulation has not finished
            //
            if (!m_Stop)
            {
                m_CurrentPosition = (m_Stopwatch.ElapsedMilliseconds / (m_NumberOfSeconds * 1000)) * m_Positions.Count;

                //
                // Determine when to stop the simulation
                //
                if (m_CurrentPosition > 0 && m_CurrentPosition < m_Positions.Count)
                {
                    //
                    // Calculate the camera position and direction
                    //
                    m_PreviousPosition = m_CurrentPosition;

                    Array fromPosition = m_Positions[(int)m_CurrentPosition];

                    IAgCrdnPoint fromPoint = VgtHelper.CreatePoint(m_Root.CentralBodies.Earth.Vgt, AgECrdnPointType.eCrdnPointTypeFixedInSystem);
                    ((IAgCrdnPointFixedInSystem)fromPoint).Reference.SetSystem(m_Root.VgtRoot.WellKnownSystems.Earth.Fixed);
                    ((IAgCrdnPointFixedInSystem)fromPoint).FixedPoint.AssignCartesian((double)fromPosition.GetValue(0), (double)fromPosition.GetValue(1), (double)fromPosition.GetValue(2));

                    Array toPosition = CatmullRomSpline.CartesianToCartographic(fromPosition, "Earth", m_Root);
                    toPosition.SetValue(0.0, 2); // We want to look at the Earth's surface.
                    toPosition = CatmullRomSpline.CartographicToCartesian(toPosition, "Earth", m_Root);

                    IAgCrdnPoint toPoint = VgtHelper.CreatePoint(m_Root.CentralBodies.Earth.Vgt, AgECrdnPointType.eCrdnPointTypeFixedInSystem);
                    ((IAgCrdnPointFixedInSystem)toPoint).Reference.SetSystem(m_Root.VgtRoot.WellKnownSystems.Earth.Fixed);
                    ((IAgCrdnPointFixedInSystem)toPoint).FixedPoint.AssignCartesian((double)toPosition.GetValue(0), (double)toPosition.GetValue(1), (double)toPosition.GetValue(2));

                    m_Scene.Camera.View(m_Root.VgtRoot.WellKnownAxes.Earth.Fixed, fromPoint, toPoint);
                    m_Scene.Camera.ConstrainedUpAxis = AgEStkGraphicsConstrainedUpAxis.eStkGraphicsConstrainedUpAxisZ;
                    m_Scene.Camera.Axes = m_Root.VgtRoot.WellKnownAxes.Earth.Fixed;
                    m_Scene.Camera.LockViewDirection = true;

                    if (m_Style == CameraUpdaterStyle.Rotating)
                    {
                        m_Scene.Camera.Axes = VgtHelper.CreateAxes(m_Root.CentralBodies.Earth.Vgt, AgECrdnAxesType.eCrdnAxesTypeFixed);
                    }
                }
                //
                // Stop the simulation and calculates the final camera position and direction
                //
                else
                {
                    m_Stop = true;
                    m_Timer.Stop();
                    m_Timer.Close();
                    m_Timer.Dispose();

                    Array fromPosition = m_Positions[(int)m_PreviousPosition];

                    IAgCrdnPoint fromPoint = VgtHelper.CreatePoint(m_Root.CentralBodies.Earth.Vgt, AgECrdnPointType.eCrdnPointTypeFixedInSystem);
                    ((IAgCrdnPointFixedInSystem)fromPoint).Reference.SetSystem(m_Root.VgtRoot.WellKnownSystems.Earth.Fixed);
                    ((IAgCrdnPointFixedInSystem)fromPoint).FixedPoint.AssignCartesian((double)fromPosition.GetValue(0), (double)fromPosition.GetValue(1), (double)fromPosition.GetValue(2));

                    Array toPosition = m_Positions[m_Positions.Count - 1];
                    IAgCrdnPoint toPoint = VgtHelper.CreatePoint(m_Root.CentralBodies.Earth.Vgt, AgECrdnPointType.eCrdnPointTypeFixedInSystem);
                    ((IAgCrdnPointFixedInSystem)toPoint).Reference.SetSystem(m_Root.VgtRoot.WellKnownSystems.Earth.Fixed);
                    ((IAgCrdnPointFixedInSystem)toPoint).FixedPoint.AssignCartesian((double)toPosition.GetValue(0), (double)toPosition.GetValue(1), (double)toPosition.GetValue(2));

                    m_Scene.Camera.View(m_Root.VgtRoot.WellKnownAxes.Earth.Fixed, fromPoint, toPoint);
                    m_Scene.Camera.Axes = VgtHelper.CreateAxes(m_Root.CentralBodies.Earth.Vgt, AgECrdnAxesType.eCrdnAxesTypeFixed);
                    m_Scene.Camera.LockViewDirection = false;
                }

                m_Scene.Render();
            }
        }

        //
        // Member variables
        //
        private CameraUpdaterStyle m_Style;

        private AgStkObjectRoot m_Root;
        private IAgStkGraphicsScene m_Scene;
        private List<Array> m_Positions;
        private double m_CurrentPosition;
        private double m_PreviousPosition;

        private double m_NumberOfSeconds;
        private System.Timers.Timer m_Timer;
        private Stopwatch m_Stopwatch;
        private bool m_Stop;
    }
}

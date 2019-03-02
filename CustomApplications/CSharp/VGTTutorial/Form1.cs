using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Collections;
using System.Collections.Specialized;
using System.Resources;
using System.Reflection;

using AGI.STKUtil;
using AGI.STKX;
using AGI.STKObjects;
using AGI.STKVgt;


namespace VGTTutorial
{
    public partial class VGT_Tutorial : Form
    {
        #region Private Data Members
        AgStkObjectRoot stkRootObject = null;
        AgSatellite _satellite = null;
        IAgFacility _facility = null;
        const string sViewPointName = "ViewPoint";
        const string sDistanceVectorName = "accessFac";
        ResourceManager resmgr = new ResourceManager("VGTTutorial.Strings", Assembly.GetExecutingAssembly());
        #endregion

        private AGI.STKObjects.AgStkObjectRoot stkRoot
        {
            get
            {
                if (stkRootObject == null)
                {
                    stkRootObject = new AGI.STKObjects.AgStkObjectRoot();
                    stkRootObject.OnAnimUpdate += new IAgStkObjectRootEvents_OnAnimUpdateEventHandler(stkRootObject_OnAnimUpdate);
                }
                return stkRootObject;
            }
        }

        void stkRootObject_OnAnimUpdate(double TimeEpSec)
        {
            if (_satellite != null)
            {
                IAgCrdnProvider provider = _satellite.Vgt;
                if (provider.Vectors.Contains(sDistanceVectorName))
                {
                    IAgCrdnVector vector = provider.Vectors[sDistanceVectorName];
                    IAgCrdnVectorFindInAxesResult result = vector.FindInAxes(TimeEpSec, provider.WellKnownAxes.Earth.Fixed);
                    if (result.IsValid)
                    {
                        // Calculate the vector's L2-norm.
                        IAgCartesian3Vector cartVec = result.Vector;
                        double mag = Math.Sqrt(
                            cartVec.X*cartVec.X + 
                            cartVec.Y*cartVec.Y +
                            cartVec.Z*cartVec.Z);

                        txtDistanceFromSatToFacility.Text = string.Format("{0} km", (mag/1000).ToString("F"));
                    }
                }
            }
        }

        void CloseScenario()
        {
            if (stkRootObject != null)
            {
                stkRootObject.CloseScenario();
            }
        }

        public VGT_Tutorial()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainLabel.Text = resmgr.GetString("Welcome");
        }

        private void NewScenario(object sender, EventArgs e)
        {
            //--Set Instructions--//
            MainLabel.Text = resmgr.GetString("CreateSatellite");
           
            //--Creates new scenario--//
            stkRoot.NewScenario("VGTTutorial");
            stkRoot.UnitPreferences.SetCurrentUnit("TimeUnit", "sec");
            stkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec");

            IAgScenario scenario = stkRoot.CurrentScenario as IAgScenario;
            scenario.Animation.AnimStepValue = 5;

            //--Enable Next--//
            Create_Satellite.Enabled = true;
            //--Disable Current--//
            Create_Scenario.Enabled = false;
        }

        private void CreateSatellite(object sender, EventArgs e)
        {
            //--Set Instructions--//
            MainLabel.Text = resmgr.GetString("CreateVector");

            //--Create and Propogate _satellite--//
            _satellite = (AGI.STKObjects.AgSatellite)stkRoot.CurrentScenario.Children.New(
                         AGI.STKObjects.AgESTKObjectType.eSatellite, "AGILE_31135");
            _satellite.SetPropagatorType(AGI.STKObjects.AgEVePropagatorType.ePropagatorSGP4);
            AGI.STKObjects.IAgVePropagatorSGP4 prop = (AGI.STKObjects.IAgVePropagatorSGP4)_satellite.Propagator;
            prop.Step = 10;
            prop.Propagate();
     
            //--Create Facility--//
            _facility = (IAgFacility)stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eFacility, "accessFac");
            _facility.Graphics.LabelVisible = true;
            _facility.VO.Model.Visible = true;
            
            //--Initialize _satellite--//
            _satellite = (AgSatellite)stkRoot.CurrentScenario.Children["AGILE_31135"];
            
            //--Set vector/angle Scale for appearance--//
            _satellite.VO.Vector.VectorSizeScale = 1.3;
            _satellite.VO.Vector.AngleSizeScale = 1;

            //--Initialize _provider as IAgStkObject to access VGT--//
            IAgCrdnProvider provider = _satellite.Vgt;
            
            //--Set up view point to use with "ZoomTo" function--//
            //--Point coordinates relative to provider (_satellite)--//
            IAgCrdnPointFixedInSystem viewPoint = (IAgCrdnPointFixedInSystem)provider.Points.Factory.Create(
                "ViewPoint", "View sat from this point", AgECrdnPointType.eCrdnPointTypeFixedInSystem);
            viewPoint.FixedPoint.AssignCartesian(.09, -.1, -.025);

            //--Enable Next--//
            Create_Vector.Enabled = true;
            //--Disable current--//
            Create_Satellite.Enabled = false;

            //--Zoom to Satellite--//
            ZoomTo();
        }

        private void CreateVector(object sender, EventArgs e)
        {
            //--Set Instructions--//
            MainLabel.Text = resmgr.GetString("ShowVelocityVector");

            IAgCrdnProvider provider = _satellite.Vgt;
            stkRoot.BeginUpdate();
            try
            {
                //--Create Vector to accessFac--//
                IAgCrdnVectorDisplacement vDisplacement = (IAgCrdnVectorDisplacement)provider.Vectors.Factory.Create(
                    sDistanceVectorName, "Vector to facility", AGI.STKVgt.AgECrdnVectorType.eCrdnVectorTypeDisplacement);
                vDisplacement.Origin.SetPoint(provider.Points["Center"]);
                vDisplacement.Destination.SetPoint((_facility as IAgStkObject).Vgt.Points["Center"]);
                vDisplacement.Apparent = true;

                //--Show the Vectors--//
                _satellite.VO.Vector.RefCrdns.Add(AgEGeometricElemType.eVectorElem, (vDisplacement as IAgCrdn).QualifiedPath);
                IAgVORefCrdnVector voVectorToFac = (IAgVORefCrdnVector)_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.eVectorElem, (vDisplacement as IAgCrdn).QualifiedPath);
                voVectorToFac.MagnitudeVisible = true;
                voVectorToFac.LabelVisible = true;
                voVectorToFac.Color = Color.AliceBlue;
            }
            finally
            {
                stkRoot.EndUpdate();
            }
            //Enable Next//
            Show_Velocity_Vector.Enabled = true;
            //Disable Current//
            Create_Vector.Enabled = false;
        }

        private void ShowVelocityVector(object sender, EventArgs e)
        {
            MainLabel.Text = resmgr.GetString("CreateAxes");

            IAgCrdnProvider provider = _satellite.Vgt;

            string path = (provider.Vectors["Velocity"] as IAgCrdn).QualifiedPath;

            stkRoot.BeginUpdate();
            try
            {
                //--Show Predefined Velocity Vector--//
                IAgVORefCrdnVector voVelocity = (IAgVORefCrdnVector)_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.eVectorElem, path);
                voVelocity.Visible = true;
                voVelocity.MagnitudeVisible = true;
                voVelocity.LabelVisible = true;
            }
            finally
            {
                stkRoot.EndUpdate();
            }
            //--Disable current--//
            Show_Velocity_Vector.Enabled = false;
            //--Enable next--//
            Create_Axes.Enabled = true;
        }      
    
        private void CreateAxes(object sender, EventArgs e)
        {
            MainLabel.Text = resmgr.GetString("CreatePlane");

            IAgCrdnProvider provider = _satellite.Vgt;
            string path = (provider.Axes["ICR"] as IAgCrdn).QualifiedPath;
            stkRoot.BeginUpdate();
            try
            {
                //--Add Predefined Axis--//
                _satellite.VO.Vector.RefCrdns.Add(AgEGeometricElemType.eAxesElem, path);
                IAgVORefCrdnAxes voAxes = (IAgVORefCrdnAxes)_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.eAxesElem, path);
                voAxes.Visible = true;
                voAxes.Color = Color.Tomato;

                //--Hide velocity label--//
                _satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.eVectorElem,
                    (provider.Vectors["Velocity"] as IAgCrdn).QualifiedPath).Visible = false;
            }
            finally
            {
                stkRoot.EndUpdate();
            }
            //--Show next--//
            Create_Plane.Enabled = true;
            //--Disable Current--//
            Create_Axes.Enabled = false;
        }

        private void CreatePlane(object sender, EventArgs e)
        {
            MainLabel.Text = resmgr.GetString("CreateAngles");

            IAgCrdnProvider provider = _satellite.Vgt;

            stkRoot.BeginUpdate();
            try
            {
                //--Create Plane Normal to Velocity Vector--//
                IAgCrdnPlaneNormal plane = (IAgCrdnPlaneNormal)provider.Planes.Factory.Create("NormalPlane", "Normal to Velocity vector.",
                    AgECrdnPlaneType.eCrdnPlaneTypeNormal);
                plane.NormalVector.SetVector(provider.Vectors["Velocity"]);
                plane.ReferenceVector.SetVector(provider.Vectors["Earth"]);
                plane.ReferencePoint.SetPoint(provider.Points["Center"]);

                string path = (provider.Planes["NormalPlane"] as IAgCrdn).QualifiedPath;
                //--Add plane to View--//
                _satellite.VO.Vector.RefCrdns.Add(AgEGeometricElemType.ePlaneElem, path);
                IAgVORefCrdnPlane voPlane = (IAgVORefCrdnPlane)_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.ePlaneElem, path);
                voPlane.TransparentPlaneVisible = true;
                voPlane.Color = Color.Yellow;
                voPlane.AxisLabelsVisible = true;

                //--Add BodyXY Plane--//
                string pathXY = (provider.Planes["BodyXY"] as IAgCrdn).QualifiedPath;
                IAgVORefCrdnPlane voPlaneBodyXY = (IAgVORefCrdnPlane)_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.ePlaneElem, pathXY);
                voPlaneBodyXY.Visible = true;
                voPlaneBodyXY.TransparentPlaneVisible = true;
                voPlaneBodyXY.Color = Color.Yellow;
                voPlaneBodyXY.AxisLabelsVisible = true;

                //--Hide Axes Labels--//
                path = (provider.Axes["ICR"] as IAgCrdn).QualifiedPath;
                IAgVORefCrdnAxes axes = (IAgVORefCrdnAxes)_satellite.VO.Vector.RefCrdns.GetCrdnByName(AgEGeometricElemType.eAxesElem, path);
                axes.LabelVisible = false;
            }
            finally
            {
                stkRoot.EndUpdate();
            }
            //--Show next--//
            Create_Angle.Enabled = true;
            //--Disable Current--//
            Create_Plane.Enabled = false;
        }

        private void CreateAngle(object sender, EventArgs e)
        {
            //--Set Instructions--//
            MainLabel.Text = resmgr.GetString("Finish");

            IAgVOVector vector3d = _satellite.VO.Vector;
            IAgCrdnProvider provider = _satellite.Vgt;
            stkRoot.BeginUpdate();
            try
            {
                //--Angle from Normal Plane to XY Plane (trajectory)--//    
                IAgCrdnAngleBetweenPlanes anglePlane = (IAgCrdnAngleBetweenPlanes)provider.Angles.Factory.Create("AngleBetweenPlanes",
                    "Angle from PlaneXY to NormalPlane", AgECrdnAngleType.eCrdnAngleTypeBetweenPlanes);
                anglePlane.FromPlane.SetPlane(provider.Planes["BodyXY"]);
                anglePlane.ToPlane.SetPlane(provider.Planes["NormalPlane"]);

                //--Add to View--//
                vector3d.RefCrdns.Add(AgEGeometricElemType.eAngleElem, ((IAgCrdn)anglePlane).QualifiedPath);
                IAgVORefCrdnAngle voAnglePlane = (IAgVORefCrdnAngle)vector3d.RefCrdns.GetCrdnByName(
                    AgEGeometricElemType.eAngleElem, ((IAgCrdn)anglePlane).QualifiedPath);
                voAnglePlane.AngleValueVisible = true;

                //--Angle from Velocity/Trajector vector to AccessFacility vector--//
                IAgCrdnAngleBetweenVectors angleVector = (IAgCrdnAngleBetweenVectors)provider.Angles.Factory.Create("AngleToVector",
                    "Angle from Vector to Plane", AgECrdnAngleType.eCrdnAngleTypeBetweenVectors);
                angleVector.FromVector.SetVector(provider.Vectors["Velocity"]);
                angleVector.ToVector.SetVector(provider.Vectors[sDistanceVectorName]);

                //--Add to View--//
                vector3d.RefCrdns.Add(AgEGeometricElemType.eAngleElem, ((IAgCrdn)angleVector).QualifiedPath);
                IAgVORefCrdnAngle voAngleVector = (IAgVORefCrdnAngle)vector3d.RefCrdns.GetCrdnByName(
                    AgEGeometricElemType.eAngleElem, ((IAgCrdn)angleVector).QualifiedPath);
                voAngleVector.AngleValueVisible = true;
                voAngleVector.Color = Color.SpringGreen;

                //--Angle from AccessFacility vector to Plane--//
                IAgCrdnAngleToPlane anglePlaneFac = (IAgCrdnAngleToPlane)provider.Angles.Factory.Create("AngleFromFac",
                    "Angle from Plane to Vector", AgECrdnAngleType.eCrdnAngleTypeToPlane);
                anglePlaneFac.ReferenceVector.SetVector(provider.Vectors[sDistanceVectorName]);
                anglePlaneFac.ReferencePlane.SetPlane(provider.Planes["NormalPlane"]);
                //--Add to View--//
                _satellite.VO.Vector.RefCrdns.Add(AgEGeometricElemType.eAngleElem, (anglePlaneFac as IAgCrdn).QualifiedPath);
                IAgVORefCrdnAngle voAngleFac = (IAgVORefCrdnAngle)vector3d.RefCrdns.GetCrdnByName(
                    AgEGeometricElemType.eAngleElem, (anglePlaneFac as IAgCrdn).QualifiedPath);
                voAngleFac.AngleValueVisible = true;
            }
            finally
            {
                stkRoot.EndUpdate();
            }
            //--Show next--//
            Close_Scenario.Enabled = true;
            //--Disable current--//
            Create_Angle.Enabled = false;
        }

        private void Unload(object sender, EventArgs e)
        {
            CloseScenario();
            Application.Exit();
        }

        private void resetAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.Rewind();
        }

        private void startFwdAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.PlayForward();
        }

        private void pauseAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.Pause();
        }

        private void startRevAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.PlayBackward();
        }

        private void incTimeStepAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.Faster();
        }

        private void decTimeStepAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.Slower();
        }

        private void stepFwdAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.StepForward();
        }

        private void stepRevAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.StepBackward();
        }

        private void ZoomToSatClick(object sender, EventArgs e)
        {
            ZoomTo();
        }

        private void ZoomTo()
        {
            IAgCrdnPointFixedInSystem viewPoint = (IAgCrdnPointFixedInSystem)_satellite.Vgt.Points[sViewPointName];
            //--Zoom to _satellite--//
            string zoomCmd = string.Format("VO * View FromTo FromRegName \"STK Object\" FromName \"{1}\" ToRegName \"STK Object\" ToName \"{0}\"",
                string.Format("{0}/{1}",_satellite.ClassName, _satellite.InstanceName), 
                ((IAgCrdn)viewPoint).QualifiedPath);
            stkRootObject.ExecuteCommand(zoomCmd);
        }

        private void VGT_Tutorial_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseScenario();
        }
    }
}

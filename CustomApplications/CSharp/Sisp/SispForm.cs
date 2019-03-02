using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

using AGI.STKObjects;
using AGI.STKUtil;
using AGI.STKVgt;
using AGI.STKX;



namespace Sisp
{
    /// <summary>
    /// Summary description for Sisp
    /// </summary>
    public partial class SispForm : Form
    {
        #region Private Data Members
        private const string TYPE_SAT = "Satellite";
        private const string TYPE_AIR = "Aircraft";
        private const string TYPE_GROUND = "Ground";
        private const string TYPE_SHIP = "Ship";

        private const string ACCESS_FAC_NAME = "accessFac";

        private const short GLOBETAP_INDEX_2D = 0;
        private const short GLOBETAP_INDEX_3D = 1;

        private bool survAccessOn;
        AgStkObjectRoot stkRootObject = null;
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AGI.STKX.AgSTKXApplication STKXApp = null;
            try
            {
                STKXApp = new AGI.STKX.AgSTKXApplication();

                if (!STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeGlobeControl))
                {
                    MessageBox.Show("You do not have the required license.", "License Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (System.Runtime.InteropServices.COMException exception)
            {
                if (exception.ErrorCode == unchecked((int)0x80040154))
                {
                    string errorMessage = "Could not instantiate AgSTKXApplication.";
                    errorMessage += Environment.NewLine;
                    errorMessage += Environment.NewLine;
                    errorMessage += "Check that STK or STK Engine 64-bit is installed on this machine.";

                    MessageBox.Show(errorMessage, "STK Engine Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    throw;
                }
            }
            if (STKXApp != null)
            {
                Application.Run(new SispForm());
            }
        }

        private AGI.STKObjects.AgStkObjectRoot stkRoot
        {
            get
            {
                if (stkRootObject == null)
                {
                    stkRootObject = new AGI.STKObjects.AgStkObjectRoot();
                }
                return stkRootObject;
            }
        }

        public SispForm()
        {
            survAccessOn = false;            

            InitializeComponent();
        }

        /// <summary>
        /// Creates a new scenario, adds a Facility, binds dataviews, and populates the datagrid
        /// </summary>
        private void SispForm_Load(object sender, EventArgs e)
        {           
            stkRoot.NewScenario("SISP");
            stkRoot.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec");
            IAgFacility accessfacility = stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eFacility, ACCESS_FAC_NAME) as IAgFacility;
            accessfacility.Graphics.LabelVisible = false;
            accessfacility.VO.Model.Visible = false;

            availableVehicleTable.DataSource = VehicleDatabase.Instance.UnshownVehicle;
            shownVehicleTable.DataSource = VehicleDatabase.Instance.ShownVehicle;
            availableSatelliteTable.DataSource = VehicleDatabase.Instance.UnshownSatellite;
            shownSatelliteTable.DataSource = VehicleDatabase.Instance.ShownSatellite;

            PopulateVehicleTable();
            PopulateSatelliteTable();

            ReloadVehicles();
            ReloadSatellite();

            ReloadComboList();
        }

        #region Object Model Code
        /// <summary>
        /// STK Connect Commands
        /// </summary>
        private void SendCommand(string command)
        {
            AGI.STKX.IAgExecCmdResult rVal = null;
            try
            {
                rVal = this.axAgUiAxVOCntrl1.Application.ExecuteCommand(command);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void resetAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.Rewind();
        }

        private void stepRevAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.StepBackward();
        }

        private void startRevAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.PlayBackward();
        }

        private void pauseAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.Pause();
        }

        private void startFwdAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.PlayForward();
        }

        private void stepFwdAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.StepForward();
        }

        private void decTimeStepAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.Slower();
        }

        private void incTimeStepAnimButton_Click(object sender, EventArgs e)
        {
            stkRoot.Faster();
        }

        /// <summary>
        /// Using supplied data from Form, creates satellite through the object model and connect
        /// </summary>
        private void AddSatellite(string satelliteName, string state, string satForceCode, string opCapacity)
        {
            string[] stateValue = state.Split();

            if (stateValue.Length != 6)
                return;

            stkRoot.UnitPreferences.SetCurrentUnit("DistanceUnit", "m");
            stkRoot.UnitPreferences.SetCurrentUnit("TimeUnit", "sec");

            IAgSatellite satellite = stkRoot.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, satelliteName) as IAgSatellite;

            satellite.SetPropagatorType(AgEVePropagatorType.ePropagatorJ2Perturbation);
            IAgVePropagatorJ2Perturbation j2prop = satellite.Propagator as IAgVePropagatorJ2Perturbation;
            IAgOrbitStateClassical classical = j2prop.InitialState.Representation.ConvertTo(AgEOrbitStateType.eOrbitStateClassical) as IAgOrbitStateClassical;
            classical.CoordinateSystemType = AgECoordinateSystem.eCoordinateSystemJ2000;
            IAgOrbitStateCoordinateSystem coordsys = classical.CoordinateSystem as IAgOrbitStateCoordinateSystem;
            classical.Epoch = 0;

            classical.SizeShapeType = AgEClassicalSizeShape.eSizeShapeSemimajorAxis;
            IAgClassicalSizeShapeSemimajorAxis sizeshape = classical.SizeShape as IAgClassicalSizeShapeSemimajorAxis;
            sizeshape.SemiMajorAxis = Convert.ToDouble(stateValue[0]);

            classical.SizeShapeType = AgEClassicalSizeShape.eSizeShapeMeanMotion;
            IAgClassicalSizeShapeMeanMotion sizeshape1 = classical.SizeShape as IAgClassicalSizeShapeMeanMotion;
            sizeshape1.Eccentricity = Convert.ToDouble(stateValue[1]);

            classical.Orientation.Inclination = Convert.ToDouble(stateValue[2]);

            classical.Orientation.ArgOfPerigee = Convert.ToDouble(stateValue[3]);

            classical.Orientation.AscNodeType = AgEOrientationAscNode.eAscNodeRAAN;
            IAgOrientationAscNodeRAAN raan = classical.Orientation.AscNode as IAgOrientationAscNodeRAAN;
            raan.Value = Convert.ToDouble(stateValue[4]);

            classical.LocationType = AgEClassicalLocation.eLocationMeanAnomaly;
            IAgClassicalLocationMeanAnomaly loc = classical.Location as IAgClassicalLocationMeanAnomaly;
            loc.Value = Convert.ToDouble(stateValue[5]);

            j2prop.InitialState.Representation.Assign(classical);
            IAgCrdnEventIntervalSmartInterval interval = j2prop.EphemerisInterval;
            interval.SetStartAndStopTimes(0, 86400);
            j2prop.Step = 60.0; // in seconds
            j2prop.Propagate();

            Color color;

            if (satForceCode.Equals("Red"))
                color = Color.Red;
            else if (satForceCode.Equals("Blue"))
                color = Color.Blue;
            else
                color = Color.Gray;

            satellite.Graphics.SetAttributesType(AgEVeGfxAttributes.eAttributesBasic);
            IAgVeGfxAttributesBasic satAtt = satellite.Graphics.Attributes as IAgVeGfxAttributesBasic;
            satAtt.Color = color;
            satAtt.Inherit = false;
            satAtt.LabelVisible = true;

            if (Int32.Parse(opCapacity) <= 30)
                color = Color.Red;
            else if (Int32.Parse(opCapacity) >= 70)
                color = Color.Green;
            else
                color = Color.Yellow;

            SendCommand("VO */Satellite/" + satelliteName + " AttitudeView Sphere Show On SphereColor Yellow LabelType None");

            SendCommand("VO */Satellite/" + satelliteName + " AttitudeView Projection Name Earth Show Off ");
            SendCommand("VO */Satellite/" + satelliteName + " AttitudeView Projection Name Moon Show Off ");
            SendCommand("VO */Satellite/" + satelliteName + " AttitudeView Projection Name Sun Show Off ");
        }

        /// <summary>
        /// Using supplied data from Form, creates vehicle through the object model and connect
        /// </summary>
        private void AddVehicle(string vehicleName, string vehicleType, string state, string vehicleForceCode, string opCapacity)
        {
            AgESTKObjectType vehType;
            String[] points;
            int i;
            Color color1;
            string color2;

            if (vehicleType == TYPE_AIR)
                vehType = AgESTKObjectType.eAircraft;
            else if (vehicleType == TYPE_GROUND)
                vehType = AgESTKObjectType.eGroundVehicle;
            else if (vehicleType == TYPE_SHIP)
                vehType = AgESTKObjectType.eShip;
            else
            {
                MessageBox.Show("Database Value for vehicle " + vehicleName + " is invalid");
                return;
            }

            IAgStkObject vehicle = stkRoot.CurrentScenario.Children.New(vehType, vehicleName);
            IAgGreatArcVehicle vehGreatArcVehicle = vehicle as IAgGreatArcVehicle;
            IAgGreatArcGraphics vehGreatArcGraphics = null;
            IAgVePropagatorGreatArc prop = null;

            switch (vehicle.ClassType)
            {
                case AgESTKObjectType.eAircraft:
                    IAgAircraft aircraft = vehicle as IAgAircraft;
                    vehGreatArcGraphics = aircraft.Graphics;
                    break;
                case AgESTKObjectType.eShip:
                    IAgShip ship = vehicle as IAgShip;
                    vehGreatArcGraphics = ship.Graphics;
                    break;
                case AgESTKObjectType.eGroundVehicle:
                    IAgGroundVehicle groundVehicle = vehicle as IAgGroundVehicle;
                    vehGreatArcGraphics = groundVehicle.Graphics;
                    break;
            }

            points = state.Split(new Char[] { ' ' });

            vehGreatArcVehicle.SetRouteType(AgEVePropagatorType.ePropagatorGreatArc);
            prop = vehGreatArcVehicle.Route as IAgVePropagatorGreatArc;
            prop.Method = AgEVeWayPtCompMethod.eDetermineTimeAccFromVel;

            for (i = 0; i < (((points.Length) + 1) / 3); i++)
            {
                IAgVeWaypointsElement e = prop.Waypoints.Add();
                e.Latitude = Convert.ToDouble(points[(i * 3) + 0]);
                e.Longitude = Convert.ToDouble(points[(i * 3) + 1]);
                e.Altitude = Convert.ToDouble(points[(i * 3) + 2]);
                e.Speed = 1;
            }

            prop.Propagate();

            if (vehicleForceCode.Equals("Red"))
                color1 = Color.Red;
            else if (vehicleForceCode.Equals("Blue"))
                color1 = Color.Blue;
            else
                color1 = Color.Gray;

            if (vehGreatArcGraphics != null)
            {
                IAgVeGfxAttributesBasic vehBasicAttributes = vehGreatArcGraphics.Attributes as IAgVeGfxAttributesBasic;
                vehBasicAttributes.Inherit = false;
                vehBasicAttributes.LabelVisible = true;
                vehBasicAttributes.Color = color1;
            }

            if (Convert.ToInt32(opCapacity) <= 30)
                color2 = "Red";
            else if (Convert.ToInt32(opCapacity) >= 70)
                color2 = "Green";
            else
                color2 = "Yellow";

            if (vehicleType.Equals("Ground"))
                vehicleType = "GroundVehicle";

            SendCommand("VO */" + vehicleType + "/" + vehicleName + " Attitudeview Sphere Show On SphereColor " + color2 + " LabelType None");

            SendCommand("VO */" + vehicleType + "/" + vehicleName + " AttitudeView Projection Name Earth Show Off ");
            SendCommand("VO */" + vehicleType + "/" + vehicleName + " AttitudeView Projection Name Moon Show Off ");
            SendCommand("VO */" + vehicleType + "/" + vehicleName + " AttitudeView Projection Name Sun Show Off ");
        }

        /// <summary>
        /// Removes satellite
        /// </summary>
        private void RemoveSatellite(string satelliteName)
        {
            stkRoot.CurrentScenario.Children.Unload(AgESTKObjectType.eSatellite, satelliteName);
        }

        /// <summary>
        /// Removes vehicle
        /// </summary>
        private void RemoveVehicle(string vehicleName, string vehicleType)
        {
            // unload the vehicle from the scenario.

            AgESTKObjectType vehType;

            if (vehicleType.Equals(TYPE_AIR))
                vehType = AgESTKObjectType.eAircraft;
            else if (vehicleType.Equals(TYPE_SHIP))
                vehType = AgESTKObjectType.eShip;
            else if (vehicleType.Equals(TYPE_GROUND))
                vehType = AgESTKObjectType.eGroundVehicle;
            else
            {
                MessageBox.Show("Database Value for vehicle " + vehicleName + " is invalid");
                return;
            }

            stkRoot.CurrentScenario.Children.Unload(vehType, vehicleName);
        }

        /// <summary>
        /// Finds Access of Loaded Satellites
        /// </summary>
        private void FindAccesses()
        {
            string satName;
            string forceCode;
            Color color1;
            Color color2;

            IAgSatellite satellite = null;
            IAgStkObject accessFacObj = stkRoot.CurrentScenario.Children[ACCESS_FAC_NAME];

            stkRoot.BeginUpdate();

            for (int i = 0; i < VehicleDatabase.Instance.GetCount(VehicleDatabase.LoadedType.LoadedSatellite); i++)
            {
                forceCode = Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedSatellite, i, SatelliteInfo.ForceCode));
                satName = Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedSatellite, i, SatelliteInfo.Name));
                
                satellite = stkRoot.CurrentScenario.Children[satName] as IAgSatellite;

                if (forceCode.Equals("Blue"))
                {
                    color1 = Color.Blue;
                    color2 = Color.Green;
                }
                else
                {
                    color1 = Color.Red;
                    color2 = Color.Yellow;
                }

                if (satellite.Graphics.AttributesType != AgEVeGfxAttributes.eAttributesAccess)
                    satellite.Graphics.SetAttributesType(AgEVeGfxAttributes.eAttributesAccess);

                IAgVeGfxAttributesAccess satAccGfxAttribute = satellite.Graphics.Attributes as IAgVeGfxAttributesAccess;

                satAccGfxAttribute.AccessObjects.RemoveAll();
                satAccGfxAttribute.AccessObjects.AddObject(accessFacObj);
                satAccGfxAttribute.NoAccess.IsVisible = true;
                satAccGfxAttribute.NoAccess.Color = color1;
                satAccGfxAttribute.DuringAccess.IsVisible = true;
                satAccGfxAttribute.DuringAccess.Line.Width = AgELineWidth.e3;
                satAccGfxAttribute.DuringAccess.Color = color2;

                IAgStkObject satObj = satellite as IAgStkObject;
                IAgStkAccess satObjAccess = satObj.GetAccess(accessFacObj.Path);
                satObjAccess.ComputeAccess();
            }

            stkRoot.EndUpdate();
        }

        /// <summary>
        /// Removes all Acesses
        /// </summary>
        private void RemoveAccesses()
        {
            string satName;
            string satType;
            string forceCode;
            Color color1;
            Color color2;

            IAgSatellite satellite = null;
            IAgStkObject accessFacObj = stkRoot.CurrentScenario.Children[ACCESS_FAC_NAME];

            stkRoot.BeginUpdate();
            
            for (int i = 0; i < VehicleDatabase.Instance.GetCount(VehicleDatabase.LoadedType.LoadedSatellite); i++)
            {
                satName = Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedSatellite, i, SatelliteInfo.Name));
                forceCode = Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedSatellite, i, SatelliteInfo.ForceCode));
                satType = Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedSatellite, i, SatelliteInfo.Type));

                satellite = stkRoot.CurrentScenario.Children[satName] as IAgSatellite;

                if (satellite.Graphics.AttributesType == AgEVeGfxAttributes.eAttributesAccess)
                {
                    if (forceCode.Equals("Blue"))
                    {
                        color1 = Color.Blue;
                        color2 = Color.Blue;
                    }
                    else
                    {
                        color1 = Color.Red;
                        color2 = Color.Red;
                    }

                    IAgStkObject satObj = satellite as IAgStkObject;
                    IAgStkAccess satObjAccess = satObj.GetAccess(accessFacObj.Path);
                    satObjAccess.RemoveAccess();

                    IAgVeGfxAttributesAccess satAccGfxAttribute = satellite.Graphics.Attributes as IAgVeGfxAttributesAccess;

                    satAccGfxAttribute.NoAccess.Color = color1;
                    satAccGfxAttribute.DuringAccess.Line.Width = AgELineWidth.e1;
                    satAccGfxAttribute.DuringAccess.Color = color2;
                }
            }

            stkRoot.EndUpdate();
        }

        #endregion

        #region UI

        /// <summary>
        /// Repopulates Combo List
        /// </summary>
        private void ReloadComboList()
        {
            viewFromButton.DropDownItems.Clear();

            for (int i = 0; i < VehicleDatabase.Instance.GetCount(VehicleDatabase.LoadedType.LoadedSatellite); i++)
            {
                string type = Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedSatellite, i, SatelliteInfo.Type));
                string name = Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedSatellite, i, SatelliteInfo.Name));

                AddStripDownButtonItem(name, type);
            }

            for (int i = 0; i < VehicleDatabase.Instance.GetCount(VehicleDatabase.LoadedType.LoadedVehicle); i++)
            {
                string type = Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedVehicle, i, VehicleInfo.Type));
                string name = Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedVehicle, i, VehicleInfo.Name));
                
                AddStripDownButtonItem(name, type);
            }
        }

        private void AddStripDownButtonItem(string name, string type)
        {
            if (type.Contains("Ground"))
                type += "Vehicle";

            viewFromButton.DropDownItems.Add(type + '/' + name);
        }

        private void RemoveStripDownButtonItem(string name, string type)
        {
            if (type.Contains("Ground"))
                type += "Vehicle";

            foreach (ToolStripItem t in viewFromButton.DropDownItems)
            {
                if (t.Text.Equals(type + '/' + name))
                {
                    viewFromButton.DropDownItems.Remove(t);
                    break;
                }
            }
        }

        /// <summary>
        /// Initialize Pre-loaded Vehicles from database
        /// </summary>
        private void ReloadVehicles()
        {
            SendCommand("BatchGraphics * On");
            stkRoot.BeginUpdate();

            for (int i = 0; i < VehicleDatabase.Instance.GetCount(VehicleDatabase.LoadedType.LoadedVehicle); i++)
            {
                AddVehicle((string)VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedVehicle, i, VehicleInfo.Name),
                    (string)VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedVehicle, i, VehicleInfo.Type),
                    (string)VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedVehicle, i, VehicleInfo.State),
                    (string)VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedVehicle, i, VehicleInfo.ForceCode),
                    (string)VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedVehicle, i, VehicleInfo.OpCapacity));
            }

            stkRoot.EndUpdate();
            SendCommand("BatchGraphics * Off");
        }

        /// <summary>
        /// Initialize Pre-loaded Satellites from database
        /// </summary>
        private void ReloadSatellite()
        {
            SendCommand("BatchGraphics * On");
            stkRoot.BeginUpdate();

            for (int i = 0; i < VehicleDatabase.Instance.GetCount(VehicleDatabase.LoadedType.LoadedSatellite); i++)
            {
                AddSatellite((string)VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedSatellite, i, SatelliteInfo.Name),
                       Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedSatellite, i, SatelliteInfo.State)),
                       Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedSatellite, i, SatelliteInfo.ForceCode)),
                       Convert.ToString(VehicleDatabase.Instance.GetValue(VehicleDatabase.LoadedType.LoadedSatellite, i, SatelliteInfo.OpCapacity)));
            }

            stkRoot.EndUpdate();
            SendCommand("BatchGraphics * Off");
        }

        /// <summary>
        /// Fill Loaded Satellite from Database, passes Form data to datasource
        /// </summary>
        private void PopulateSatelliteTable()
        {
            int minTotalMass, maxTotalMass, minFuelMass, maxFuelMass;

            if (!minTotalMassBox.Checked || !Int32.TryParse(minTotalMassText.Text, out minTotalMass))
                minTotalMass = Int32.MinValue;
            if (!maxTotalMassBox.Checked || !Int32.TryParse(maxTotalMassText.Text, out maxTotalMass))
                maxTotalMass = Int32.MaxValue;
            if (!minFuelMassBox.Checked || !Int32.TryParse(minFuelMassText.Text, out minFuelMass))
                minFuelMass = Int32.MinValue;
            if (!maxFuelMassBox.Checked || !Int32.TryParse(maxFuelMassText.Text, out maxFuelMass))
                maxFuelMass = Int32.MaxValue;

            int forcecode, missionsatellite;

            if (forcecodeSatellite1.Checked)
                forcecode = 0;
            else if (forcecodeSatellite2.Checked)
                forcecode = 1;
            else if (forcecodeSatellite3.Checked)
                forcecode = 2;
            else
                forcecode = 3;

            if (missionSatellite1.Checked)
                missionsatellite = 0;
            else if (missionSatellite2.Checked)
                missionsatellite = 1;
            else if (missionSatellite3.Checked)
                missionsatellite = 2;
            else
                missionsatellite = 3;

            VehicleDatabase.Instance.FilterSatelliteData(
                forcecode,
                missionsatellite,
                minTotalMassBox.Checked,
                minTotalMass,
                maxTotalMassBox.Checked,
                maxTotalMass,
                minFuelMassBox.Checked,
                minFuelMass,
                maxFuelMassBox.Checked,
                maxFuelMass
            );
        }

        /// <summary>
        /// Fill Loaded Vehicle from Database, passes Form data to datasource
        /// </summary>
        private void PopulateVehicleTable()
        {
            int forcecode, theater, platform;

            if (forcecodeVehicle1.Checked)
                forcecode=0;
            else if (forcecodeVehicle2.Checked)
                forcecode=1;
            else if (forcecodeVehicle3.Checked)
                forcecode=2;
            else
                forcecode=3;

            if (theaterVehicleAllRadio.Checked)
                theater = 0;
            else if (theaterVehicleNorthRadio.Checked)
                theater = 1;
            else if (theaterVehicleSouthRadio.Checked)
                theater = 2;
            else if (theaterVehiclePaRadio.Checked)
                theater = 3;
            else if (theaterVehicleEuRadio.Checked)
                theater = 4;
            else
                theater = 5;

            if (platformVehicleAllRadio.Checked)
                platform = 0;
            else if (platformVehicleGroundRadio.Checked)
                platform = 1;
            else if (platformVehicleAircraftRadio.Checked)
                platform = 2;
            else
                platform = 3;

            VehicleDatabase.Instance.FilterVehicleData(
                forcecode,
                theater,
                platform
            );
        }


        #region Windows Form Event Handlers
        /// <summary>
        /// Adds all satellites that are selected in the unloaded view
        /// </summary>
        private void addSatelliteShown_Click(object sender, EventArgs e)
        {
            SendCommand("BatchGraphics * On");
            stkRoot.BeginUpdate();

            ArrayList selectedSatelliteArrayList;

            VehicleDatabase.Instance.LoadSatellite(availableSatelliteTable.SelectedRows, out selectedSatelliteArrayList);

            foreach (String[] str in selectedSatelliteArrayList)
            {
                AddSatellite(str[0],
                    str[1],
                    str[2],
                    str[3]);

                AddStripDownButtonItem(str[0], "Satellite");
                UpdateHSTabSatInfo(str[0]);
            }

            stkRoot.EndUpdate();
            SendCommand("BatchGraphics * Off");

            PopulateSatelliteTable();
        }

        /// <summary>
        /// Adds all vehicles that are selected in the unloaded view
        /// </summary>
        private void addVehicleShown_Click(object sender, EventArgs e)
        {
            SendCommand("BatchGraphics * On");
            stkRoot.BeginUpdate();

            ArrayList selectedVehicleArrayList;

            VehicleDatabase.Instance.LoadVehicle(availableVehicleTable.SelectedRows, out selectedVehicleArrayList);

            foreach (String[] str in selectedVehicleArrayList)
            {
                AddVehicle(str[0],
                    str[1],
                    str[2],
                    str[3],
                    str[4]);

                AddStripDownButtonItem(str[0], str[1]);
                UpdateHSTabVehicleInfo(str[0]);
            }

            stkRoot.EndUpdate();
            SendCommand("BatchGraphics * Off");

            PopulateVehicleTable();
        }

        /// <summary>
        /// Removes all satellites that are selected in the loaded view
        /// </summary>
        private void removeSatelliteShown_Click(object sender, EventArgs e)
        {
            ArrayList selectedSatelliteArrayList;
            VehicleDatabase.Instance.UnloadSatellie(shownSatelliteTable.SelectedRows, out selectedSatelliteArrayList);

            stkRoot.BeginUpdate();

            foreach (String[] str in selectedSatelliteArrayList)
            {
                RemoveSatellite(str[0]);

                RemoveStripDownButtonItem(str[0], "Satellite");
            }

            stkRoot.EndUpdate();

            PopulateSatelliteTable();
        }

        /// <summary>
        /// Removes all vehicles that are selected in the loaded view
        /// </summary>
        private void removeVehicleShown_Click(object sender, EventArgs e)
        {
            ArrayList selectedVehicleArrayList;
            VehicleDatabase.Instance.UnloadVehicle(shownVehicleTable.SelectedRows, out selectedVehicleArrayList);

            stkRoot.BeginUpdate();

            foreach (String[] str in selectedVehicleArrayList)
            {
                RemoveVehicle(str[0], str[1]);

                RemoveStripDownButtonItem(str[0], str[1]);
            }

            stkRoot.EndUpdate();

            PopulateVehicleTable();
        }

        private void forcecodeVehicle_CheckedChanged(object sender, EventArgs e)
        {
            PopulateVehicleTable();
        }

        private void theaterVehicle_CheckedChanged(object sender, EventArgs e)
        {
            PopulateVehicleTable();
        }

        private void platformVehicle_CheckedChanged(object sender, EventArgs e)
        {
            PopulateVehicleTable();
        }

        private void forcecodeSatellite_CheckedChanged(object sender, EventArgs e)
        {
            PopulateSatelliteTable();
        }

        private void missionSatellite_Click(object sender, EventArgs e)
        {
            PopulateSatelliteTable();
        }

        private void maxTotalMassBox_CheckedChanged(object sender, EventArgs e)
        {
            PopulateSatelliteTable();
        }

        private void maxFuelMassBox_CheckedChanged(object sender, EventArgs e)
        {
            PopulateSatelliteTable();
        }

        private void map_DblClick(object sender, EventArgs e)
        {
            UpdateFacility();
        }

        private void globe_DblClick(object sender, EventArgs e)
        {
            UpdateFacility();
        }

        /// <summary>
        /// If a vehicle or satellite is clicked on, update the info on that object
        /// Then, if find access is currently active, move the facility, and call findAccess
        /// </summary>
        private void UpdateFacility()
        {
            string tableName;
            string objName;
            string path;
            int x, y;
            string[] pathInfo;

            x = Int32.Parse(suvellanceXTextBox.Text);
            y = Int32.Parse(suvellanceYTextBox.Text);

            path = PickObjectInfo(x, y);
            pathInfo = path.Split(new Char[] { '/' });
            if (pathInfo.Length > 1)
            {
                objName = pathInfo[6];
                tableName = pathInfo[5];

                if (tableName.Equals("Satellite"))
                {
                    UpdateHSTabSatInfo(objName);
                }
                else
                {
                    UpdateHSTabVehicleInfo(objName);
                }
            }

            if (survAccessOn)
            {
                suvellanceLatitudeTextBox.Text = Convert.ToString(PickInfoLat(x, y));
                suvellanceLongitudeTextBox.Text = Convert.ToString(PickInfoLon(x, y));

                IAgFacility accessFacilityObject = stkRoot.CurrentScenario.Children[ACCESS_FAC_NAME] as IAgFacility;
                IAgPosition accessFacilityPosition = accessFacilityObject.Position;
                accessFacilityPosition.AssignPlanetodetic(PickInfoLat(x, y), PickInfoLon(x, y), 0.0);
                FindAccesses();
            }
        }

        /// <summary>
        /// Using the vehicle name, gets vehicle info from datasource, updates Information panel
        /// </summary>
        private void UpdateHSTabVehicleInfo(string name)
        {
            ArrayList vehicleInfoArrayList = null;

            VehicleDatabase.Instance.GetVehicleInfo(name, out vehicleInfoArrayList);

            vehicleInfoNameText.Text = (string)vehicleInfoArrayList[0];
            vehicleInfoTypeText.Text = (string)vehicleInfoArrayList[1];
            vehicleInfoForceText.Text = (string)vehicleInfoArrayList[2];
            vehicleInfoMissionText.Text = (string)vehicleInfoArrayList[3];
            vehicleInfoStateText.Text = (string)vehicleInfoArrayList[4];
            vehicleInfoOriginText.Text = (string)vehicleInfoArrayList[5];
            vehicleInfoWeaponsText.Text = (string)vehicleInfoArrayList[6];
            vehicleInfoTheaterText.Text = (string)vehicleInfoArrayList[7];
            vehicleInfoOpsText.Text = (string)vehicleInfoArrayList[8];
            vehicleInfoLoadedText.Text = (string)vehicleInfoArrayList[9];
            vehNotes.Text = (string)vehicleInfoArrayList[10];

            infoTabControl.SelectedTab = vehicleTabPage;
        }

        /// <summary>
        /// Using the satellite name, gets satellite info from datasource, updates Information panel
        /// </summary>
        private void UpdateHSTabSatInfo(string name)
        {
            ArrayList satelliteInfoArrayList = null;

            VehicleDatabase.Instance.GetSatelliteInfo(name, out satelliteInfoArrayList);

            satelliteInfoNameText.Text = (string)satelliteInfoArrayList[0];
            satelliteInfoForceText.Text = (string)satelliteInfoArrayList[1];
            satelliteInfoOriginText.Text = (string)satelliteInfoArrayList[2];
            satelliteInfoSizeText.Text = (string)satelliteInfoArrayList[3];
            satelliteInfoRcsText.Text = (string)satelliteInfoArrayList[4];
            satelliteInfoTotalText.Text = (string)satelliteInfoArrayList[5];
            satelliteInfoFuelText.Text = (string)satelliteInfoArrayList[6];
            satelliteInfoAvailDvText.Text = (string)satelliteInfoArrayList[7];
            satelliteInfoAttitudeText.Text = (string)satelliteInfoArrayList[8];
            satelliteInfoOpsText.Text = (string)satelliteInfoArrayList[9];
            satNotes.Text = (string)satelliteInfoArrayList[10];

            infoTabControl.SelectedTab = satelliteTabPage;
        }

        /// <summary>
        /// Returns the path of the object at the (x, y) pixel
        /// </summary>
        private string PickObjectInfo(int x, int y)
        {
            AgPickInfoData pickInfoData;

            pickInfoData = SetPickInfoData(x, y);
            return pickInfoData.ObjPath;
        }

        /// <summary>
        /// Returns the lat of the (x, y) pixel
        /// </summary>
        private double PickInfoLat(int x, int y)
        {
            AgPickInfoData pickInfoData;
            pickInfoData = SetPickInfoData(x, y);
            
            return pickInfoData.Lat;
        }

        /// <summary>
        /// Returns the lat of the (x, y) pixel
        /// </summary>
        private double PickInfoLon(int x, int y)
        {
            AgPickInfoData pickInfoData;
            pickInfoData = SetPickInfoData(x, y);
            
            return pickInfoData.Lon;
        }

        /// <summary>
        /// Return the pick info data object at pixel x, y
        /// </summary>
        private AgPickInfoData SetPickInfoData(int x, int y)
        {
            if (globeViewTabControl.SelectedIndex == GLOBETAP_INDEX_2D)
                return axAgUiAx2DCntrl1.PickInfo(x, y);
            else if (globeViewTabControl.SelectedIndex == GLOBETAP_INDEX_3D)
            {
                return axAgUiAxVOCntrl1.PickInfo(x, y);
            }
            return null;
        }

        private void orientNorthButton_Click(object sender, EventArgs e)
        {
            SendCommand("VO * View North");
        }

        private void homeViewButton_Click(object sender, EventArgs e)
        {
            SendCommand("VO * View Home");
        }

        private void viewFromButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            SendCommand(String.Format("VO * View FromTo FromRegName \"STK Object\" FromName \"{0}\" ToRegName \"STK Object\" ToName \"{0}\"", e.ClickedItem.Text));
        }

        private void axAgUiAx2DCntrl1_MouseMoveEvent(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseMoveEvent e)
        {
            suvellanceXTextBox.Text = Convert.ToString(e.x);
            suvellanceYTextBox.Text = Convert.ToString(e.y);
        }

        private void axAgUiAxVOCntrl1_MouseMoveEvent(object sender, AxAGI.STKX.IAgUiAxVOCntrlEvents_MouseMoveEvent e)
        {
            suvellanceXTextBox.Text = Convert.ToString(e.x);
            suvellanceYTextBox.Text = Convert.ToString(e.y);
        }

        private void minTotalMassText_TextChanged(object sender, EventArgs e)
        {
            PopulateSatelliteTable();
        }

        private void maxTotalMassText_TextChanged(object sender, EventArgs e)
        {
            PopulateSatelliteTable();
        }

        private void minFuelMassText_TextChanged(object sender, EventArgs e)
        {
            PopulateSatelliteTable();
        }

        private void maxFuelMassText_TextChanged(object sender, EventArgs e)
        {
            PopulateSatelliteTable();
        }

        private void minFuelMassBox_CheckedChanged(object sender, EventArgs e)
        {
            PopulateSatelliteTable();
        }

        private void minTotalMassBox_CheckedChanged(object sender, EventArgs e)
        {
            PopulateSatelliteTable();
        }

        #endregion

        private void zoomInVO_Click(object sender, EventArgs e)
        {
            if (globeViewTabControl.SelectedIndex == GLOBETAP_INDEX_3D)
                axAgUiAxVOCntrl1.ZoomIn();
            else
                axAgUiAx2DCntrl1.ZoomIn();
        }

        private void zoomOutVO_Click(object sender, EventArgs e)
        {
            if (globeViewTabControl.SelectedIndex == GLOBETAP_INDEX_3D)
                SendCommand("VO * View Home");
            else
                axAgUiAx2DCntrl1.ZoomOut();
        }

        private void orientTopButton_Click(object sender, EventArgs e)
        {
            SendCommand("VO * View Top");
        }

        private void findAccessButton_Click(object sender, EventArgs e)
        {
            if (findAccessButton.Text.Equals("Find Accesses"))
            {
                survAccessOn = true;
                findAccessButton.Text = "Stop Finding Accesses";

                FindAccesses();
            }
            else
            {
                survAccessOn = false;
                findAccessButton.Text = "Find Accesses";
            }
        }

        private void removeAccessButton_Click(object sender, EventArgs e)
        {
            RemoveAccesses();
        }

        #endregion

        private void SispForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stkRootObject != null)
            {
                stkRootObject.CloseScenario();
            }
        }
    }
}
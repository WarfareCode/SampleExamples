//-------------------------------------------------------------------------
//
//  This is part of the STK 8 Object Model Examples
//  Copyright (C) 2006 Analytical Graphics, Inc.
//
//  This source code is intended as a reference to users of the
//	STK 8 Object Model.
//
//  File: Form1.cs
//  DataProviders
//
//
//  The features used in this example show how to hook event notification
//  up to STK through C# so that you can listen to events such as animation
//  and object creation.
//
//--------------------------------------------------------------------------
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;



namespace DataProviders
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class Form1 : Form
	{
		private AGI.STKObjects.AgStkObjectRoot objModelRootObject = null;

		private AGI.STKObjects.AgSatellite Satellite = null;
		private AGI.STKObjects.AgFacility Facility = null;
		private AGI.STKObjects.IAgStkAccess Access = null;

		private bool IsPlacingFacility = false;

        private AGI.STKObjects.AgStkObjectRoot objModelRoot
        {
            get
            {
                if (objModelRootObject == null)
                {
                    objModelRootObject = new AGI.STKObjects.AgStkObjectRoot();
                }
                return objModelRootObject;
            }
        }

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

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

                if (!STKXApp.IsFeatureAvailable(AGI.STKX.AgEFeatureCodes.eFeatureCodeEngineRuntime))
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
                Application.Run(new Form1());
            }
		}

		/// <summary>
		/// Creates a new scenario, adds a Facility and a Satellite and changes some basic properties
		/// </summary>
		private void Form1_Load(object sender, System.EventArgs e)
		{			
			objModelRoot.NewScenario("AccessTest");
			Satellite = (AGI.STKObjects.AgSatellite)objModelRoot.CurrentScenario.Children.New(
				AGI.STKObjects.AgESTKObjectType.eSatellite, "AccSat");
			SetSatelliteColor(MarkerColorButton.BackColor);
			Facility = (AGI.STKObjects.AgFacility)objModelRoot.CurrentScenario.Children.New(
				AGI.STKObjects.AgESTKObjectType.eFacility, "AccFac");
			SetFacilityColor(MarkerColorButton2.BackColor);
			UpdateFacPositionText();

			comboBox1.Items.AddRange(new object[] {"TwoBody","J2Perturbation","J4Perturbation","SGP4","HPOP"});
			comboBox1.Text = "TwoBody";

            // Add few levels to the collection of satellite's elevation contours.
            // The contours' visibility is controlled with Graphics.ElevContours.IsVisible.
            string prevUnit = objModelRoot.UnitPreferences.GetCurrentUnitAbbrv("Angle");
            objModelRoot.UnitPreferences.SetCurrentUnit("Angle", "deg");
            Satellite.Graphics.ElevContours.Elevations.AddLevelRange(0, 90, 15);
            objModelRoot.UnitPreferences.SetCurrentUnit("Angle", prevUnit);
        }
		/// <summary>
		/// Setting the satellite's color
		/// </summary>
		private void SetSatelliteColor(Color _color)
		{
			Satellite.Graphics.SetAttributesType(AGI.STKObjects.AgEVeGfxAttributes.eAttributesBasic);
            Satellite.Graphics.PassData.GroundTrack.SetLeadDataType(AGI.STKObjects.AgELeadTrailData.eDataAll);
			AGI.STKObjects.IAgVeGfxAttributesBasic gfx = (AGI.STKObjects.IAgVeGfxAttributesBasic)Satellite.Graphics.Attributes;
			gfx.Inherit = false;
			gfx.IsVisible = true;
			gfx.Color = _color;
		}

		/// <summary>
		/// Setting the facility's color
		/// </summary>
		private void SetFacilityColor(Color _color)
		{
			Facility.Graphics.InheritFromScenario = false;
			Facility.Graphics.LabelVisible = true;
			Facility.Graphics.LabelColor = _color;
		}

		/// <summary>
		/// Updates the facilities text to indicate where the facility is located
		/// </summary>
		private void UpdateFacPositionText()
		{
            AGI.STKUtil.IAgPlanetodetic pos = (AGI.STKUtil.IAgPlanetodetic)Facility.Position.ConvertTo(
				AGI.STKUtil.AgEPositionType.ePlanetodetic);
			placetext.Text = "Lat: " + pos.Lat + "\n Lon: " + pos.Lon;
		}

		/// <summary>
		/// Shows a graph of the data
		/// </summary>
		private void GraphButton_Click(object sender, System.EventArgs e)
		{
			ArrayList xaxis = new ArrayList();
			ArrayList yaxis = new ArrayList();

			AGI.STKObjects.IAgScenario sc = (AGI.STKObjects.IAgScenario)objModelRoot.CurrentScenario;

			System.Array cols = new object[] { "Start Time", "Duration" };
			AGI.STKObjects.IAgDataProvider dpInfo = (AGI.STKObjects.IAgDataProvider)Access.DataProviders["Access Data"];
			AGI.STKObjects.IAgDrResult	  resInfo = ((AGI.STKObjects.IAgDataPrvInterval)dpInfo).ExecElements(
				sc.StartTime,
				sc.StopTime,
				ref cols);

			if (resInfo.Intervals.Count > 0)
			{
				foreach(AGI.STKObjects.IAgDrInterval interval in resInfo.Intervals)
				{
					foreach(double value in interval.DataSets[0].GetValues())
					{
						xaxis.Add(value / 180 );
					}
					foreach(double value in interval.DataSets[1].GetValues())
					{
						yaxis.Add(value / 5 );
					}
				}
			}

			GraphForm graphform = new GraphForm(Satellite.InstanceName + " Access to " + Facility.InstanceName,
								"Date", "Duration", xaxis,yaxis) ;
			graphform.Show();
			graphform.Focus();
		}

		/// <summary>
		/// Sets the satellite's propagator type and propagates it.
		/// </summary>
		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(comboBox1.SelectedIndex == 0)
			{
				Satellite.SetPropagatorType(AGI.STKObjects.AgEVePropagatorType.ePropagatorTwoBody);
				AGI.STKObjects.IAgVePropagatorTwoBody prop = (AGI.STKObjects.IAgVePropagatorTwoBody)Satellite.Propagator;
				prop.Propagate();
			}
			else if(comboBox1.SelectedIndex == 1)
			{
				Satellite.SetPropagatorType(AGI.STKObjects.AgEVePropagatorType.ePropagatorJ2Perturbation);
				AGI.STKObjects.IAgVePropagatorJ2Perturbation prop = (AGI.STKObjects.IAgVePropagatorJ2Perturbation)Satellite.Propagator;
				prop.Propagate();
			}
			else if(comboBox1.SelectedIndex == 2)
			{
				Satellite.SetPropagatorType(AGI.STKObjects.AgEVePropagatorType.ePropagatorJ4Perturbation);
				AGI.STKObjects.IAgVePropagatorJ4Perturbation prop = (AGI.STKObjects.IAgVePropagatorJ4Perturbation)Satellite.Propagator;
				prop.Propagate();
			}
			else if(comboBox1.SelectedIndex == 3)
			{
				Satellite.SetPropagatorType(AGI.STKObjects.AgEVePropagatorType.ePropagatorSGP4);
				AGI.STKObjects.IAgVePropagatorSGP4 prop = (AGI.STKObjects.IAgVePropagatorSGP4)Satellite.Propagator;
				prop.Propagate();
			}
			else if(comboBox1.SelectedIndex == 4)
			{
				Satellite.SetPropagatorType(AGI.STKObjects.AgEVePropagatorType.ePropagatorHPOP);
				AGI.STKObjects.IAgVePropagatorHPOP prop = (AGI.STKObjects.IAgVePropagatorHPOP)Satellite.Propagator;
				prop.Propagate();
			}
		}

		/// <summary>
		/// Sets the satellite's color with the chosen type.
		/// </summary>
		private void MarkerColorButton_Click(object sender, System.EventArgs e)
		{
			ColorDialog colordial = new ColorDialog();
			colordial.AllowFullOpen = false ;
			colordial.ShowHelp = true ;
			colordial.Color = MarkerColorButton.BackColor;
			if (colordial.ShowDialog() == DialogResult.OK)
			{
				MarkerColorButton.BackColor =  colordial.Color;
				SetSatelliteColor(MarkerColorButton.BackColor);
			}
		}

		/// <summary>
		/// Changes the satellite's elevation contours visible and fillvisible properties.
		/// </summary>
		private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
		{
			if (checkBox1.Checked)
			{
				Satellite.Graphics.ElevContours.IsVisible = true;
				Satellite.Graphics.ElevContours.IsFillVisible = true;
			}
			else
			{
				Satellite.Graphics.ElevContours.IsFillVisible = false;
				Satellite.Graphics.ElevContours.IsVisible = false;
			}
		}

		/// <summary>
		/// Uses the mouse down event in STKX to position the facility
		/// </summary>
		private void PlaceFacilityButton_Click(object sender, System.EventArgs e)
		{
			Cursor.Current = PlaceFacilityButton.Cursor;
			IsPlacingFacility = true;
			placetext.Text = "Click on the 2D Map to set position.";
			PlaceFacilityButton.Enabled = false;
		}

		/// <summary>
		/// Moves the facility to the mouse's coordinates.
		/// </summary>
		private void axAgUiAx2DCntrl1_MouseDownEvent(object sender, AxAGI.STKX.IAgUiAx2DCntrlEvents_MouseDownEvent e)
		{
			if (IsPlacingFacility)
			{
				AGI.STKX.IAgPickInfoData pickInfoData = axAgUiAx2DCntrl1.PickInfo(e.x, e.y);
                AGI.STKUtil.IAgPlanetodetic pos = (AGI.STKUtil.IAgPlanetodetic)Facility.Position.ConvertTo(
					AGI.STKUtil.AgEPositionType.ePlanetodetic);
				
				pos.Lat = pickInfoData.Lat;
				pos.Lon = pickInfoData.Lon;
                if (!Facility.UseTerrain)
                {
                    pos.Alt = pickInfoData.Alt;
                }
				Facility.Position.Assign(pos);
				
				UpdateFacPositionText();
				IsPlacingFacility = false;
				PlaceFacilityButton.Enabled = true;
			}
		}

		/// <summary>
		/// Changes the facilities color with the given value.
		/// </summary>
		private void MarkerColorButton2_Click(object sender, System.EventArgs e)
		{
			ColorDialog colordial = new ColorDialog();
			colordial.AllowFullOpen = false ;
			colordial.ShowHelp = true ;
			colordial.Color = MarkerColorButton.BackColor;
			if (colordial.ShowDialog() == DialogResult.OK)
			{
				MarkerColorButton2.BackColor =  colordial.Color;
				SetFacilityColor(MarkerColorButton2.BackColor);
			}
		}

		/// <summary>
		/// Computes access between a satellite and a facility. 
		/// </summary>
		private void ComputeButton_Click(object sender, System.EventArgs e)
		{
			objModelRoot.UnitPreferences.SetCurrentUnit("DateFormat", "EpSec");

            Access = (AGI.STKObjects.IAgStkAccess)Satellite.GetAccessToObject((AGI.STKObjects.IAgStkObject)Facility);
			Access.AccessTimePeriod = AGI.STKObjects.AgEAccessTimeType.eScenarioAccessTime;
			Access.ComputeAccess();
			SetFacilityColor(MarkerColorButton2.BackColor);
			ComputeButton.Enabled = false;
			GraphButton.Enabled = true;
		}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (objModelRootObject != null)
            {
                objModelRootObject.CloseScenario();
            }
        }
	}
}


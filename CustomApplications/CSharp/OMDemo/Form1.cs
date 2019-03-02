//-------------------------------------------------------------------------
//
//  This is part of the STK 8 Object Model Examples
//  Copyright (C) 2006 Analytical Graphics, Inc.
//
//  This source code is intended as a reference to users of the
//	STK 8 Object Model.
//
//  File: Form1.cs
//  GPSDatabaseAccessDemo
//
//  This program demonstrates use of the STK Object Model in conjunction
//	with two other integrable technologies, an odbc data source in the form
//	of a microsoft access database, and XML.  The example loads a GPS constellation of
//	satellites from a preexisting database, and does the same of a facility from a
//	previously defined xml file containing positional data. It then computes and
//	retreives access data, and writes the output to an XML file. 
//
//  The features used are: Basic object manipulation, SGP4 Propagation,
//	Facility positioning, and Access computation/data providers.
//
//--------------------------------------------------------------------------

using System;
using System.Data.Odbc;
using System.IO;
using System.Windows.Forms;
using System.Xml;

using AGI.STKObjects;
using AGI.STKVgt;


using System.Data.SQLite;
using System.Data;

namespace OMDemo
{
	/// <summary>
	/// GPSDatabaseAccessDemo
	/// </summary>
	public partial class Form1 : System.Windows.Forms.Form
	{
        private AGI.STKObjects.AgStkObjectRoot stkRootObject = null;
		private AGI.STKObjects.IAgStkObjectElementCollection Satellites = null;
		private AGI.STKObjects.IAgFacility Facility = null;
		private AGI.STKObjects.IAgStkAccess Access = null;

        private AGI.STKObjects.AgStkObjectRoot objModelRoot
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

		#region FormSetup

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
                Application.Run(new Form1());
            }
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{			
			objModelRoot.NewScenario("ObjectModelGPSDemo");
			IAgScenario scene = (IAgScenario)objModelRoot.CurrentScenario;
			scene.StartTime = "1 Jul 2005 12:00:00.000";
			scene.StopTime = "5 Jul 2005 12:00:00.000";
		}

		private void loadSatButton_Click(object sender, System.EventArgs e)
		{
			loadSatButton.Enabled = false;
			if (!loadFacilityButton.Enabled) ComputeButton.Enabled = true;

			ConnectAndLoadGPSDatabase();
		}

		private void ConnectAndLoadGPSDatabase()
		{
            try
            {
                DataTable dt = new DataTable();
                SQLiteConnection connection = new SQLiteConnection("Data Source=GPS.db3;New=False;FailIfMissing=true");
                connection.Open();
                SQLiteCommand mycommand = new SQLiteCommand(connection);
                mycommand.CommandText = "Select * FROM TLETable";
                SQLiteDataReader reader = mycommand.ExecuteReader();
                dt.Load(reader);
                AddGPSSatellite(dt);
                reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

			Satellites = objModelRoot.CurrentScenario.Children.GetElements(
				AGI.STKObjects.AgESTKObjectType.eSatellite);

			objModelRoot.Rewind();
		}

		private void AddGPSSatellite(DataTable dataSource)
		{
            for (int i = 0; i < dataSource.Rows.Count; i++)
            {
                // Define GPS satellite from the datasource using the SGP4 propagator
                // to configure and propagate the satellite.

                AGI.STKObjects.IAgSatellite Satellite = (AGI.STKObjects.IAgSatellite)objModelRoot.
                    CurrentScenario.Children.New(AGI.STKObjects.AgESTKObjectType.eSatellite, "GPS" + dataSource.Rows[i].ItemArray[0].ToString());

                Satellite.SetPropagatorType(AGI.STKObjects.AgEVePropagatorType.ePropagatorSGP4);
                AGI.STKObjects.IAgVePropagatorSGP4 SGP4 = (AGI.STKObjects.IAgVePropagatorSGP4)Satellite.Propagator;

                IAgCrdnEventIntervalSmartInterval interval = SGP4.EphemerisInterval;
                interval.SetExplicitInterval("1 Jul 2005 12:00:00.000", "5 Jul 2005 12:00:00.000");

                SGP4.Segments.AddSeg();
                AGI.STKObjects.IAgVeSGP4Segment Segment = (AGI.STKObjects.IAgVeSGP4Segment)SGP4.Segments[0];

                Segment.SSCNum = dataSource.Rows[i].ItemArray[0].ToString();
                Segment.Classification = dataSource.Rows[i].ItemArray[1].ToString();
                Segment.IntlDesignator = dataSource.Rows[i].ItemArray[2].ToString();
                Segment.Epoch = double.Parse(dataSource.Rows[i].ItemArray[3].ToString());
                Segment.BStar = double.Parse(dataSource.Rows[i].ItemArray[4].ToString());
                Segment.RevNumber = int.Parse(dataSource.Rows[i].ItemArray[5].ToString());
                Segment.Inclination = dataSource.Rows[i].ItemArray[6];
                Segment.RAAN = dataSource.Rows[i].ItemArray[7].ToString();
                Segment.Eccentricity = double.Parse(dataSource.Rows[i].ItemArray[8].ToString());
                Segment.ArgOfPerigee = dataSource.Rows[i].ItemArray[9].ToString();
                Segment.MeanAnomaly = dataSource.Rows[i].ItemArray[10].ToString();
                Segment.MeanMotion = dataSource.Rows[i].ItemArray[11].ToString();

                SGP4.Propagate();
            }
		}

		private void ComputeButton_Click(object sender, System.EventArgs e)
		{
			ComputeAndWriteAccessXML();
		}

		private void ComputeAndWriteAccessXML()
		{
			// Compute Access from each GPS satellite to the loaded facility,
			// and write access data to XML document

            XmlTextWriter AccessXML = new XmlTextWriter(Path.GetDirectoryName(Application.ExecutablePath) + @"\..\..\..\Access.xml", null);
			AccessXML.Formatting = Formatting.Indented;
			AccessXML.WriteStartDocument();
			AccessXML.WriteStartElement("GPS_Satellite_Access");
			foreach (AGI.STKObjects.IAgStkObject Satellite in Satellites)
			{
				Access = (AGI.STKObjects.IAgStkAccess)Satellite.GetAccessToObject((AGI.STKObjects.IAgStkObject)Facility);
				Access.AccessTimePeriod = AGI.STKObjects.AgEAccessTimeType.eScenarioAccessTime;
				Access.ComputeAccess();

				// Only report specific data using the data provider's ExecElements functionality
				// to specify certain elements of the access data set

				AGI.STKObjects.IAgScenario Scenario = (AGI.STKObjects.IAgScenario)objModelRoot.CurrentScenario;
				System.Array Elements = new object[] { "Start Time", "Stop Time", "Duration" };
				AGI.STKObjects.IAgDataProvider DataProvider = (AGI.STKObjects.IAgDataProvider)Access.DataProviders["Access Data"];
				AGI.STKObjects.IAgDrResult Result = ((AGI.STKObjects.IAgDataPrvInterval)DataProvider).ExecElements(
					Scenario.StartTime,
					Scenario.StopTime,
					ref Elements);

				WriteAccessElement(Satellite, Result, AccessXML);
			}
			AccessXML.WriteEndElement();
			AccessXML.Flush();
			AccessXML.WriteEndDocument();
			AccessXML.Close();
		}

		private void WriteAccessElement(IAgStkObject Satellite, IAgDrResult Result, XmlTextWriter AccessXML)
		{
			// Write access data to XML

			AccessXML.WriteStartElement(Satellite.InstanceName + "_To_" + ((AGI.STKObjects.IAgStkObject)Facility).InstanceName);
			foreach (AGI.STKObjects.IAgDrInterval AccessInterval in Result.Intervals)
			{
				for(int Access = 0; Access < AccessInterval.DataSets[0].Count; Access++)
				{
					AccessXML.WriteStartElement("Access");

					AccessXML.WriteStartElement("Start_Time");
					AccessXML.WriteString(AccessInterval.DataSets[0].GetValues().GetValue(Access).ToString());
					AccessXML.WriteEndElement();
					AccessXML.WriteStartElement("Stop_Time");
					AccessXML.WriteString(AccessInterval.DataSets[1].GetValues().GetValue(Access).ToString());
					AccessXML.WriteEndElement();
					AccessXML.WriteStartElement("Duration");
					AccessXML.WriteString(AccessInterval.DataSets[2].GetValues().GetValue(Access).ToString());
					AccessXML.WriteEndElement();

					AccessXML.WriteEndElement();
				}
			}
			AccessXML.WriteEndElement();
		}

		private void loadFacilityButton_Click(object sender, System.EventArgs e)
		{
			loadFacilityButton.Enabled = false;
			if (!loadSatButton.Enabled) ComputeButton.Enabled = true;

			LoadFacilityXML();
		}

		private void LoadFacilityXML()
		{
			// Define a facility from an existing XML file containing structured positional data

            AGI.STKUtil.IAgPlanetodetic FacPosition = null;

            XmlTextReader FacilityXML = new XmlTextReader(Path.GetDirectoryName(Application.ExecutablePath) + @"\..\..\..\Facility.xml");
			while (FacilityXML.Read())
			{
				if (FacilityXML.NodeType == XmlNodeType.Element)
				{
					if (FacilityXML.LocalName.Equals("InstanceName"))
					{
                        Facility = (AGI.STKObjects.IAgFacility)objModelRoot.CurrentScenario.Children.New(
                            AGI.STKObjects.AgESTKObjectType.eFacility, FacilityXML.ReadString());
                        FacPosition = (AGI.STKUtil.IAgPlanetodetic)Facility.Position.ConvertTo(
							AGI.STKUtil.AgEPositionType.ePlanetodetic);
					}
					if (FacilityXML.LocalName.Equals("Latitude"))  FacPosition.Lat = Convert.ToDouble(FacilityXML.ReadString());
					if (FacilityXML.LocalName.Equals("Longitude")) FacPosition.Lon = Convert.ToDouble(FacilityXML.ReadString());
					if (FacilityXML.LocalName.Equals("Altitude"))
                    {
                        Facility.UseTerrain = false;
                        FacPosition.Alt = Convert.ToDouble(FacilityXML.ReadString());
                    }
				}
			}
			Facility.Position.Assign(FacPosition);
		}

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stkRootObject != null)
            {
                stkRootObject.CloseScenario();
            }
        }
	}
}

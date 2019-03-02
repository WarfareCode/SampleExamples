//-------------------------------------------------------------------------
//
//  This is part of the STK 8 Object Model Examples
//  Copyright (C) 2006 Analytical Graphics, Inc.
//
//  This source code is intended as a reference to users of the
//	STK 8 Object Model.
//
//  File: ScenarioExamples.cs
//  ScenarioExamples
//
//
//  The features used in this example are: Basic scenario manipulations 
//  including Global Attributes BasicAnimation, EarthData, Database, 
//  Terrain, Graphics and VO 
//
//--------------------------------------------------------------------------
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.Ui.Application;

namespace Scenario
{
	class ScenarioExamples
	{
		[STAThread]
		static void Main(string[] args)
		{
			ScenarioExamples oExamples = new ScenarioExamples();
			oExamples.Run();
		}

		~ScenarioExamples()
		{
		}

		#region Class Members
        private AgUiApplication     m_oSTK;
		private IAgStkObjectRoot	m_oApplication;
		#endregion

		#region Constructor
		public ScenarioExamples()
		{
			try
			{
                m_oSTK = Marshal.GetActiveObject("STK11.Application") as AGI.Ui.Application.AgUiApplication;
                Console.Write("Looking for an instance of STK... ");
			}
			catch
			{
                Console.Write("Creating a new STK 11 instance... ");
                Guid clsID = typeof(AgUiApplicationClass).GUID;
                Type oType = Type.GetTypeFromCLSID(clsID);
                m_oSTK = Activator.CreateInstance(oType) as AGI.Ui.Application.AgUiApplication;
                try
                {
                    m_oSTK.LoadPersonality("STK");
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    Console.WriteLine("Error");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to continue . . .");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
			}

            try
            {
                m_oApplication = (IAgStkObjectRoot)m_oSTK.Personality2;
                Console.WriteLine("done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey();
                Environment.Exit(0);
            }
		}
		#endregion

		#region WaitingForEnter
		private void WaitingForEnter(bool bExit)
		{
			if( bExit )
				Console.Write("Press Enter to exit:");
			else
				Console.Write("Press Enter to continue:");
			Console.ReadLine();
		}
		#endregion

		#region Run
		public void Run()
		{
			// Close current scenario
			if( m_oApplication.CurrentScenario != null )
			{
				Console.WriteLine("The current Scenario is: {0}", m_oApplication.CurrentScenario.InstanceName);
				Console.Write("Closing Scenario... ");
				m_oApplication.CloseScenario();
				Console.WriteLine("done.");
			}
			// Create a new scenario
			Console.Write("Creating a new Scenario... ");
			m_oApplication.NewScenario("ScenarioExamples");
			Console.WriteLine("done.");
			Console.WriteLine("The current Scenario is: {0}", m_oApplication.CurrentScenario.InstanceName);

			// Get current Scenario
			IAgScenario oScenario = (AGI.STKObjects.IAgScenario)m_oApplication.CurrentScenario;
			if( oScenario == null )
			{
				Console.WriteLine("The pointer is invalid!");
				return;
			}
			
			// BasicTimePeriod
			BasicTimePeriod( oScenario );
			WaitingForEnter(false);

			// BasicGlobalAttributes
			BasicGlobalAttributes( oScenario );
			WaitingForEnter(false);

			// BasicAnimation
			BasicAnimation( oScenario.Animation );
			WaitingForEnter(false);

			// BasicEarthData
			BasicEarthData( oScenario.EarthData );
			WaitingForEnter(false);

			// BasicDatabase
			BasicDatabase( oScenario.GenDbs );
			WaitingForEnter(false);

			// BasicTerrain
			BasicTerrain( oScenario.Terrain["Earth"].TerrainCollection);
			WaitingForEnter(false);

			// BasicDescription
			BasicDescription(m_oApplication.CurrentScenario);
			WaitingForEnter(false);

			// Graphics
			Graphics( oScenario.Graphics );
			WaitingForEnter(false);

			// VO
			VO( oScenario.VO );
                        WaitingForEnter(true);
		}
		#endregion

		#region BasicTimePeriod
		private void BasicTimePeriod( IAgScenario oScenario )
		{
			Console.WriteLine("----- BASIC TIME PERIOD ----- BEGIN -----");
			if(oScenario == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// set DateFormat
			string strDateFormat = m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("DateFormat");
			Console.WriteLine("\tThe current DateFormat is: {0}", strDateFormat);
			m_oApplication.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG");
			Console.WriteLine("\tThe new DateFormat is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("DateFormat"));
			// Epoch
			Console.WriteLine("\tThe current Epoch is: {0}", oScenario.Epoch);
			oScenario.Epoch = "1 Jun 1999 12:00:00.00";
			Console.WriteLine("\tThe new Epoch is: {0}", oScenario.Epoch);
			// StartTime
			Console.WriteLine("\tThe current StartTime is: {0}", oScenario.StartTime);
			oScenario.StartTime = "1 Jun 1999 12:00:00.00";
			Console.WriteLine("\tThe new StartTime is: {0}", oScenario.StartTime);
			// StopTime
			Console.WriteLine("\tThe current StopTime is: {0}", oScenario.StopTime);
			oScenario.StopTime = "10 Jun 1999 12:00:00.01";
			Console.WriteLine("\tThe new StopTime is: {0}", oScenario.StopTime);
			// SetTimePeriod
			Console.WriteLine("\tThe current StartTime = {0}, StopTime = {1}", oScenario.StartTime, oScenario.StopTime);
			oScenario.SetTimePeriod("15 Sep 2003 12:00:00.00", "15 Oct 2003 12:00:00.00");
			Console.WriteLine("\tThe new StartTime = {0}, StopTime = {1}", oScenario.StartTime, oScenario.StopTime);
			// restore DateFormat
			m_oApplication.UnitPreferences.SetCurrentUnit("DateFormat", strDateFormat);
			Console.WriteLine("\tThe new DateFormat (restored) is: {0}", strDateFormat);
			Console.WriteLine("----- BASIC TIME PERIOD ----- END -----");
		}
		#endregion

		#region BasicGlobalAttributes
		private void BasicGlobalAttributes( IAgScenario oScenario )
		{
			Console.WriteLine("----- BASIC GLOBAL ATTRIBUTES ----- BEGIN -----");
			if(oScenario == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// SatNoOrbitWarning
			Console.WriteLine("\tThe current SatNoOrbitWarning flag is: {0}", oScenario.SatNoOrbitWarning);
			oScenario.SatNoOrbitWarning = true;
			Console.WriteLine("\tThe new SatNoOrbitWarning flag is: {0}", oScenario.SatNoOrbitWarning);
			// MslNoOrbitWarning
			Console.WriteLine("\tThe current MslNoOrbitWarning flag is: {0}", oScenario.MslNoOrbitWarning);
			oScenario.MslNoOrbitWarning = true;
			Console.WriteLine("\tThe new MslNoOrbitWarning flag is: {0}", oScenario.MslNoOrbitWarning);
			// AcWGS84Warning (eAlways)
			Console.WriteLine("\tThe current AcWGS84Warning flag is: {0}", oScenario.AcWGS84Warning);
			oScenario.AcWGS84Warning = AgEAcWGS84WarningType.eAlways;
			Console.WriteLine("\tThe new AcWGS84Warning flag is: {0}", oScenario.AcWGS84Warning);
			// AcWGS84Warning (eNever)
			oScenario.AcWGS84Warning = AgEAcWGS84WarningType.eNever;
			Console.WriteLine("\tThe new AcWGS84Warning flag is: {0}", oScenario.AcWGS84Warning);
			// AcWGS84Warning (eOnlyOnce)
			oScenario.AcWGS84Warning = AgEAcWGS84WarningType.eOnlyOnce;
			Console.WriteLine("\tThe new AcWGS84Warning flag is: {0}", oScenario.AcWGS84Warning);
			Console.WriteLine("----- BASIC GLOBAL ATTRIBUTES ----- END -----");
		}
		#endregion

		#region BasicDescription
		public void BasicDescription( IAgStkObject oObject )
		{
			Console.WriteLine("----- BASIC DESCRIPTION ----- BEGIN -----");
			if(oObject == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// Short Description test
			Console.WriteLine("\tThe current ShortDescription is: {0}", oObject.ShortDescription);
			oObject.ShortDescription = "This is a new short description.";
			Console.WriteLine("\tThe new ShortDescription is: {0}", oObject.ShortDescription);
			oObject.ShortDescription = "";
			Console.WriteLine("\tThe new ShortDescription is: {0}", oObject.ShortDescription);
			// Long Description test
			Console.WriteLine("\tThe current LongDescription is: {0}", oObject.LongDescription);
			oObject.LongDescription = "This is a new long description.";
			Console.WriteLine("\tThe new LongDescription is: {0}", oObject.LongDescription);
			oObject.LongDescription = "";
			Console.WriteLine("\tThe new LongDescription is: {0}", oObject.LongDescription);
			Console.WriteLine("----- BASIC DESCRIPTION ----- END -----");
		}
		#endregion

		#region BasicAnimation
		public void BasicAnimation( IAgScAnimation oAnimation )
		{
			Console.WriteLine("----- BASIC ANIMATION ----- BEGIN -----");
			if(oAnimation == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// set DateFormat
			string strDateFormat = m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("DateFormat");
			Console.WriteLine("\tThe current DateFormat is: {0}", strDateFormat);
			m_oApplication.UnitPreferences.SetCurrentUnit("DateFormat", "UTCG");
			Console.WriteLine("\tThe new DateFormat is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("DateFormat"));
			// StartTime
			Console.WriteLine("\tThe current StartTime is: {0}", oAnimation.StartTime);
			oAnimation.StartTime = "1 Jun 2004 12:00:00.00";
			Console.WriteLine("\tThe new StartTime is: {0}", oAnimation.StartTime);
			// EnableAnimCycleTime
			Console.WriteLine("\tThe current EnableAnimCycleTime flag is: {0}", oAnimation.EnableAnimCycleTime);
			oAnimation.EnableAnimCycleTime = true;
			Console.WriteLine("\tThe new EnableAnimCycleTime flag is: {0}", oAnimation.EnableAnimCycleTime);
			// AnimCycleTime
			Console.WriteLine("\tThe current AnimCycleTime is: {0}", oAnimation.AnimCycleTime);
			oAnimation.AnimCycleTime = "1 Jun 2004 12:00:00.01";
			Console.WriteLine("\tThe new AnimCycleTime is: {0}", oAnimation.AnimCycleTime);
			// AnimCycleType
			Console.WriteLine("\tThe current AnimCycleType is: {0}", oAnimation.AnimCycleType);
			oAnimation.AnimCycleType = AgEScEndLoopType.eLoopAtTime;
			Console.WriteLine("\tThe new AnimCycleType is: {0}", oAnimation.AnimCycleType);
			oAnimation.AnimCycleType = AgEScEndLoopType.eEndTime;
			Console.WriteLine("\tThe new AnimCycleType is: {0}", oAnimation.AnimCycleType);
			// RefreshDeltaType (eRefreshDelta)
			Console.WriteLine("\tThe current RefreshDeltaType is: {0}", oAnimation.RefreshDeltaType);
			oAnimation.RefreshDeltaType = AgEScRefreshDeltaType.eRefreshDelta;
			Console.WriteLine("\tThe new RefreshDeltaType is: {0}", oAnimation.RefreshDeltaType);
			// RefreshDelta
			Console.WriteLine("\tThe current RefreshDelta is: {0}", oAnimation.RefreshDelta);
			oAnimation.RefreshDelta = 123;
			Console.WriteLine("\tThe new RefreshDelta is: {0}", oAnimation.RefreshDelta);
			// RefreshDeltaType (eHighSpeed)
			oAnimation.RefreshDeltaType = AgEScRefreshDeltaType.eHighSpeed;
			Console.WriteLine("\tThe new RefreshDeltaType is: {0}", oAnimation.RefreshDeltaType);
			try { oAnimation.RefreshDelta = 321; }
			catch(Exception e) { Console.Write("\t\tExpected exception: {0}", e.Message); }
			// AnimStepType (eScRealTime)
			Console.WriteLine("\tThe current AnimStepType is: {0}", oAnimation.AnimStepType);
			oAnimation.AnimStepType = AgEScTimeStepType.eScRealTime;
			Console.WriteLine("\tThe new AnimStepType is: {0}", oAnimation.AnimStepType);
			// AnimStepValue
			Console.WriteLine("\tThe current AnimStepValue is: {0}", oAnimation.AnimStepValue);
			oAnimation.AnimStepValue = 12;
			Console.WriteLine("\tThe new AnimStepValue is: {0}", oAnimation.AnimStepValue);
			// AnimStepType (eScXRealTime)
			oAnimation.AnimStepType = AgEScTimeStepType.eScXRealTime;
			Console.WriteLine("\tThe new AnimStepType is: {0}", oAnimation.AnimStepType);
			// AnimStepValue
			Console.WriteLine("\tThe current AnimStepValue is: {0}", oAnimation.AnimStepValue);
			oAnimation.AnimStepValue = 21;
			Console.WriteLine("\tThe new AnimStepValue is: {0}", oAnimation.AnimStepValue);
			// AnimStepType (eScTimeStep)
			oAnimation.AnimStepType = AgEScTimeStepType.eScTimeStep;
			Console.WriteLine("\tThe new AnimStepType is: {0}", oAnimation.AnimStepType);
			// AnimStepValue
			Console.WriteLine("\tThe current AnimStepValue is: {0}", oAnimation.AnimStepValue);
			oAnimation.AnimStepValue = 1234;
			Console.WriteLine("\tThe new AnimStepValue is: {0}", oAnimation.AnimStepValue);
			// restore DateFormat
			m_oApplication.UnitPreferences.SetCurrentUnit("DateFormat", strDateFormat);
			Console.WriteLine("\tThe new DateFormat (restored) is: {0}", strDateFormat);
			Console.WriteLine("----- BASIC ANIMATION ----- END -----");
		}
		#endregion
	
		#region BasicEarthData
		private void BasicEarthData( IAgScEarthData oEarthData )
		{
			try
			{
				Console.WriteLine("----- BASIC EARTH DATA ----- BEGIN -----");
				if(oEarthData == null)
				{
					Console.WriteLine("The input parameter is invalid!");
					return;
				}
				// EOPFilename
				Console.WriteLine("\tThe current EOPFilename is: {0}", oEarthData.EOPFilename);
				// EOPStartTime
				Console.WriteLine("\tThe current EOPStartTime is: {0}", oEarthData.EOPStartTime);
				// EOPStopTime
				Console.WriteLine("\tThe current EOPStopTime is: {0}", oEarthData.EOPStopTime);
				// get STK Home Dir
                IAgExecCmdResult oResult = m_oApplication.ExecuteCommand("GetDirectory / STKHome");
				string strHomeDir = oResult[0];
				// EOPFilename
				oEarthData.EOPFilename = strHomeDir + @"\DynamicEarthData\EOP_v61.dat";
				Console.WriteLine("\tThe new EOPFilename is: {0}", oEarthData.EOPFilename);
				// EOPStartTime
				Console.WriteLine("\tThe new EOPStartTime is: {0}", oEarthData.EOPStartTime);
				// EOPStopTime
				Console.WriteLine("\tThe new EOPStopTime is: {0}", oEarthData.EOPStopTime);
				// ReloadEOP
				oEarthData.ReloadEOP();
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
			Console.WriteLine("----- BASIC EARTH DATA ----- END -----");
		}
		#endregion

		#region BasicDatabase
		private void BasicDatabase( IAgScGenDbCollection oGenDBCollection )
		{
			Console.WriteLine("----- BASIC DATABASE ----- BEGIN -----");
			if(oGenDBCollection == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// Count
			Console.WriteLine("\tThe GenDb collection contains: {0} elements", oGenDBCollection.Count);
			for( int iIndex = 0; iIndex < oGenDBCollection.Count; iIndex++ )
			{
				Console.WriteLine("\t\tElement {0}: Type = {5}, DefaultDb = {2}, DefaultDir = {3}, EnableAuxDb = {4}, AuxDb = {1}",
					iIndex, oGenDBCollection[iIndex].AuxDb, oGenDBCollection[iIndex].DefaultDb,
					oGenDBCollection[iIndex].DefaultDir, oGenDBCollection[iIndex].EnableAuxDb,
					oGenDBCollection[iIndex].Type);
			}
			Console.WriteLine("----- BASIC DATABASE ----- END -----");
		}
		#endregion

		#region BasicTerrain
		private void BasicTerrain( IAgTerrainCollection oTC )
		{
			try
			{
				Console.WriteLine("----- BASIC TERRAIN ----- BEGIN -----");
				if(oTC == null)
				{
					Console.WriteLine("The input parameter is invalid!");
					return;
				}
                // Remove
                oTC.Remove(0);
				// Count
				Console.WriteLine("\tThe current Terrain collection contains: {0} elements.", oTC.Count);
				// Add
				IAgTerrain oTerrain = oTC.Add(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Remove(0, 8)) + @"\..\..\ny512.dte",
					AgETerrainFileType.eMUSERasterFile);
				Console.WriteLine("\tThe new Terrain collection contains: {0} elements.", oTC.Count);
				for( int iIndex = 0; iIndex < oTC.Count; iIndex++ )
				{
					Console.WriteLine("\t\tElement {0}: FileTyp = {7}, Location = {1}, NELatitude = {2}, NELongitude = {3}, Resolution = {4}, SWLatitude = {5}, SWLongitude = {6}",
						iIndex, oTC[iIndex].Location, oTC[iIndex].NELatitude, oTC[iIndex].NELongitude,
						oTC[iIndex].Resolution, oTC[iIndex].SWLatitude, oTC[iIndex].SWLongitude, oTC[iIndex].FileType);
				}
				// Remove
				oTC.Remove(0);
				Console.WriteLine("\tAfter Remove(0) the Terrain collection contains: {0} elements.", oTC.Count);
				// Add
				oTerrain = oTC.Add(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Remove(0, 8)) + @"\..\..\ny512.dte",
					AgETerrainFileType.eMUSERasterFile);
				// Location
				oTerrain.Location = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Remove(0, 8)) + @"\..\..\korea.dte";
				Console.WriteLine("\tThe new Terrain collection contains: {0} elements.", oTC.Count);
				for( int iIndex = 0; iIndex < oTC.Count; iIndex++ )
				{
					Console.WriteLine("\t\tElement {0}: FileTyp = {7}, Location = {1}, NELatitude = {2}, NELongitude = {3}, Resolution = {4}, SWLatitude = {5}, SWLongitude = {6}",
						iIndex, oTC[iIndex].Location, oTC[iIndex].NELatitude, oTC[iIndex].NELongitude,
						oTC[iIndex].Resolution, oTC[iIndex].SWLatitude, oTC[iIndex].SWLongitude, oTC[iIndex].FileType);
				}
				// RemoveAll
				oTC.RemoveAll();
				Console.WriteLine("\tAfter RemoveAll() the Terrain collection contains: {0} elements.", oTC.Count);
			}
			catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
			Console.WriteLine("----- BASIC TERRAIN ----- END -----");
		}
		#endregion

		#region Graphics
		private void Graphics( IAgScGraphics oGfx )
		{
			Console.WriteLine("----- GRAPHICS ----- BEGIN -----");
			if(oGfx == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// AccessAnimHigh
			Console.WriteLine("\tThe current AccessAnimHigh is: {0}", oGfx.AccessAnimHigh);
			oGfx.AccessAnimHigh = true;
			Console.WriteLine("\tThe new AccessAnimHigh is: {0}", oGfx.AccessAnimHigh);
			// AccessLinesVisible
			Console.WriteLine("\tThe current AccessLinesVisible is: {0}", oGfx.AccessLinesVisible);
			oGfx.AccessLinesVisible = true;
			Console.WriteLine("\tThe new AccessLinesVisible is: {0}", oGfx.AccessLinesVisible);
			// AccessStatHigh
			Console.WriteLine("\tThe current AccessStatHigh is: {0}", oGfx.AccessStatHigh);
			oGfx.AccessStatHigh = true;
			Console.WriteLine("\tThe new AccessStatHigh is: {0}", oGfx.AccessStatHigh);
			// AllowAnimUpdate
			Console.WriteLine("\tThe current AllowAnimUpdate is: {0}", oGfx.AllowAnimUpdate);
			oGfx.AllowAnimUpdate = true;
			Console.WriteLine("\tThe new AllowAnimUpdate is: {0}", oGfx.AllowAnimUpdate);
			// CentroidsVisible
			Console.WriteLine("\tThe current CentroidsVisible is: {0}", oGfx.CentroidsVisible);
			oGfx.CentroidsVisible = true;
			Console.WriteLine("\tThe new CentroidsVisible is: {0}", oGfx.CentroidsVisible);
			// ElsetNumVisible
			Console.WriteLine("\tThe current ElsetNumVisible is: {0}", oGfx.ElsetNumVisible);
			oGfx.ElsetNumVisible = true;
			Console.WriteLine("\tThe new ElsetNumVisible is: {0}", oGfx.ElsetNumVisible);
			// GndMarkersVisible
			Console.WriteLine("\tThe current GndMarkersVisible is: {0}", oGfx.GndMarkersVisible);
			oGfx.GndMarkersVisible = true;
			Console.WriteLine("\tThe new GndMarkersVisible is: {0}", oGfx.GndMarkersVisible);
			// GndTracksVisible
			Console.WriteLine("\tThe current GndTracksVisible is: {0}", oGfx.GndTracksVisible);
			oGfx.GndTracksVisible = true;
			Console.WriteLine("\tThe new GndTracksVisible is: {0}", oGfx.GndTracksVisible);
			// InertialPosLabelsVisible
			Console.WriteLine("\tThe current InertialPosLabelsVisible is: {0}", oGfx.InertialPosLabelsVisible);
			oGfx.InertialPosLabelsVisible = true;
			Console.WriteLine("\tThe new InertialPosLabelsVisible is: {0}", oGfx.InertialPosLabelsVisible);
			// InertialPosVisible
			Console.WriteLine("\tThe current InertialPosVisible is: {0}", oGfx.InertialPosVisible);
			oGfx.InertialPosVisible = true;
			Console.WriteLine("\tThe new InertialPosVisible is: {0}", oGfx.InertialPosVisible);
			// LabelsVisible
			Console.WriteLine("\tThe current LabelsVisible is: {0}", oGfx.LabelsVisible);
			oGfx.LabelsVisible = true;
			Console.WriteLine("\tThe new LabelsVisible is: {0}", oGfx.LabelsVisible);
			// OrbitMarkersVisible
			Console.WriteLine("\tThe current OrbitMarkersVisible is: {0}", oGfx.OrbitMarkersVisible);
			oGfx.OrbitMarkersVisible = true;
			Console.WriteLine("\tThe new OrbitMarkersVisible is: {0}", oGfx.OrbitMarkersVisible);
			// OrbitsVisible
			Console.WriteLine("\tThe current OrbitsVisible is: {0}", oGfx.OrbitsVisible);
			oGfx.OrbitsVisible = true;
			Console.WriteLine("\tThe new OrbitsVisible is: {0}", oGfx.OrbitsVisible);
			// PlanetOrbitsVisible
			Console.WriteLine("\tThe current PlanetOrbitsVisible is: {0}", oGfx.PlanetOrbitsVisible);
			oGfx.PlanetOrbitsVisible = true;
			Console.WriteLine("\tThe new PlanetOrbitsVisible is: {0}", oGfx.PlanetOrbitsVisible);
			// SensorsVisible
			Console.WriteLine("\tThe current SensorsVisible is: {0}", oGfx.SensorsVisible);
			oGfx.SensorsVisible = true;
			Console.WriteLine("\tThe new SensorsVisible is: {0}", oGfx.SensorsVisible);
			// SubPlanetLabelsVisible
			Console.WriteLine("\tThe current SubPlanetLabelsVisible is: {0}", oGfx.SubPlanetLabelsVisible);
			oGfx.SubPlanetLabelsVisible = true;
			Console.WriteLine("\tThe new SubPlanetLabelsVisible is: {0}", oGfx.SubPlanetLabelsVisible);
			// SubPlanetPointsVisible
			Console.WriteLine("\tThe current SubPlanetPointsVisible is: {0}", oGfx.SubPlanetPointsVisible);
			oGfx.SubPlanetPointsVisible = true;
			Console.WriteLine("\tThe new SubPlanetPointsVisible is: {0}", oGfx.SubPlanetPointsVisible);
			Console.WriteLine("----- GRAPHICS ----- END -----");
		}
		#endregion

		#region VO
		private void VO( IAgScVO oVO )
		{
			Console.WriteLine("----- VO ----- BEGIN -----");
			if(oVO == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// ChunkImageCacheSize
			Console.WriteLine("\tThe current ChunkImageCacheSize is: {0}", oVO.ChunkImageCacheSize);
			oVO.ChunkImageCacheSize = 25;
			Console.WriteLine("\tThe new ChunkImageCacheSize is: {0}", oVO.ChunkImageCacheSize);
			// IsNegativeAltitudeAllowed
			Console.WriteLine("\tThe current IsNegativeAltitudeAllowed is: {0}", oVO.IsNegativeAltitudeAllowed);
			oVO.IsNegativeAltitudeAllowed = false;
			Console.WriteLine("\tThe new IsNegativeAltitudeAllowed is: {0}", oVO.IsNegativeAltitudeAllowed);
			oVO.IsNegativeAltitudeAllowed = true;
			Console.WriteLine("\tThe new IsNegativeAltitudeAllowed is: {0}", oVO.IsNegativeAltitudeAllowed);
			// RenderNewVOWindow
			Console.WriteLine("\tThe current RenderNewVOWindow is: {0}", oVO.RenderNewVOWindow);
			oVO.RenderNewVOWindow = false;
			Console.WriteLine("\tThe new RenderNewVOWindow is: {0}", oVO.RenderNewVOWindow);
			oVO.RenderNewVOWindow = true;
			Console.WriteLine("\tThe new RenderNewVOWindow is: {0}", oVO.RenderNewVOWindow);
			// SurfaceReference (eMeanSeaLevel)
			Console.WriteLine("\tThe current SurfaceReference is: {0}", oVO.SurfaceReference);
			oVO.SurfaceReference = AgESurfaceReference.eMeanSeaLevel;
			Console.WriteLine("\tThe new SurfaceReference is: {0}", oVO.SurfaceReference);
			// SurfaceReference (eWGS84Ellipsoid)
			oVO.SurfaceReference = AgESurfaceReference.eWGS84Ellipsoid;
			Console.WriteLine("\tThe new SurfaceReference is: {0}", oVO.SurfaceReference);

			// GlobeServer
			Console.WriteLine("\tThe current EnableGlobeServerAccess flag is: {0}", oVO.EnableGlobeServerAccess);
			oVO.EnableGlobeServerAccess = false;
			Console.WriteLine("\tThe new EnableGlobeServerAccess flag is: {0}", oVO.EnableGlobeServerAccess);
			oVO.EnableGlobeServerAccess = true;
			Console.WriteLine("\tThe new EnableGlobeServerAccess flag is: {0}", oVO.EnableGlobeServerAccess);
			// ReloadGlobeServerConfigData
			oVO.ReloadGlobeServerConfigData();
			// GlobeServers
			IAgScGlobeServersCollection oGSCollection = oVO.GlobeServers;
			Console.WriteLine("\tThe GlobeServers collection contains: {0} elements", oGSCollection.Count);
			foreach(IAgScGlobeServerConfigData oElement in oGSCollection)
			{
				Console.WriteLine("\t\tElement: Name = {0}, Port = {1}", oElement.Name, oElement.Port);
			}
			// Add
			IAgScGlobeServerConfigData oGSConfigData = oGSCollection.Add("Ooga.booga.server", 314);
			Console.WriteLine("\tAfter Add() GlobeServers collection contains: {0} elements", oGSCollection.Count);
			for( int iIndex = 0; iIndex < oGSCollection.Count; iIndex++ )
			{
				Console.WriteLine("\t\tElement {0}:  Name = {1}, Port = {2}, Username = {3}",
					iIndex, oGSCollection[iIndex].Name, oGSCollection[iIndex].Port, oGSCollection[iIndex].Username);
			}
			// Name
			oGSConfigData.Name = "www.google.com";
			// Port
			oGSConfigData.Port = 1234;
			// Username
			oGSConfigData.Username = "someusername";
			// SetPassword
			oGSConfigData.SetPassword("somepassword");
			Console.WriteLine("\tAfter ConfigData modification the GlobeServers collection contains: {0} elements",
				oGSCollection.Count);
			for( int iIndex = 0; iIndex < oGSCollection.Count; iIndex++ )
			{
				Console.WriteLine("\t\tElement {0}:  Name = {1}, Port = {2}, Username = {3}",
					iIndex, oGSCollection[iIndex].Name, oGSCollection[iIndex].Port, oGSCollection[iIndex].Username);
			}
			// RemoveAt
			oGSCollection.RemoveAt(0);
			Console.WriteLine("\tAfter RemoveAt() GlobeServers collection contains: {0} elements", oGSCollection.Count);
			// RemoveAll
			oGSCollection.RemoveAll();
			Console.WriteLine("\tAfter RemoveAll() GlobeServers collection contains: {0} elements", oGSCollection.Count);
			// Add
			oGSConfigData = oGSCollection.Add("globeserver.agi.com", 80);
			// Username
			oGSConfigData.Username = "myuser";
			// SetPassword
			oGSConfigData.SetPassword("mypassword");

			// SmallFont
			IAgSc3dFont oSmall = oVO.SmallFont;
			// AvailableFonts
			System.Array arFonts = oSmall.AvailableFonts;
			Console.WriteLine("\tAvailable {0} small fonts", arFonts.Length);
			for(int iIndex = 0; iIndex < arFonts.Length; iIndex++ )
			{
				// IsFontAvailable
				if( oSmall.IsFontAvailable( arFonts.GetValue(iIndex).ToString() ) )
				{
					Console.WriteLine("\t\tFont {0} is: {1}", iIndex, arFonts.GetValue(iIndex));
				}
			}
			// Name
			oSmall.Name = "Impact";
			oSmall.Bold = true;
			oSmall.Italic = true;
			oSmall.PtSize = AgESc3dPtSize.eSc3dFontSize22;

			oVO.MediumFont.Name = arFonts.GetValue(4).ToString();
			oVO.MediumFont.PtSize = (AGI.STKObjects.AgESc3dPtSize) 18;
			oVO.LargeFont.Name = arFonts.GetValue(5).ToString();
			Console.WriteLine("----- VO ----- END -----");
		}
		#endregion


	}
}

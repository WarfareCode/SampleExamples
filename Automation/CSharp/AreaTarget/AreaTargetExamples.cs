using System;
using System.Reflection;
using System.Runtime.InteropServices;
using AGI.STKObjects;
using AGI.STKUtil;
using AGI.Ui.Application;
using System.Drawing;

namespace AreaTarget
{
	class AreaTargetExamples
	{
		[STAThread]
		static void Main(string[] args)
		{
			AreaTargetExamples oExamples = new AreaTargetExamples();
			oExamples.Run();
		}

		#region Class Members
        private AgUiApplication     m_oSTK;
		private IAgStkObjectRoot	m_oApplication;
		#endregion

		#region Positions enum
		[FlagsAttribute] 
		private enum Positions
		{
			Cartesian = 1,
			Cylindrical = 2,
			Geocentric = 4,
			Geodetic = 8,
			Spherical = 16,
            Planetocentric = 32,
            Planetodetic = 64,
            All = Cartesian | Cylindrical | Geocentric | Geodetic | Spherical | Planetocentric | Planetodetic
		};
		#endregion

		#region Constructor
		public AreaTargetExamples()
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
            try
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
			    m_oApplication.NewScenario("AreaTargetExamples");
			    Console.WriteLine("done.");
			    Console.WriteLine("The current Scenario is: {0}", m_oApplication.CurrentScenario.InstanceName);

			    // Create a new AreaTarget
			    IAgAreaTarget oAT = (AGI.STKObjects.IAgAreaTarget)m_oApplication.CurrentScenario.Children.New(
				    AGI.STKObjects.AgESTKObjectType.eAreaTarget, "AT");
			    if( oAT == null )
			    {
				    Console.WriteLine("Can't add a new Areatarget to scenario!");
				    return;
			    }

			    // BasicDescription
			    BasicDescription((IAgStkObject)oAT);
			    WaitingForEnter(false);

			    // BasicDescription
			    BasicBoundary(oAT);
			    WaitingForEnter(false);

			    // BasicCentroid
			    BasicCentroid(oAT);
			    WaitingForEnter(false);

			    // Graphics
			    Graphics( oAT.Graphics );
			    WaitingForEnter(false);

			    // DisplayTimes
			    DisplayTimes( (IAgDisplayTm)oAT );
			    WaitingForEnter(false);

			    // VO
			    VO( oAT.VO );
			    WaitingForEnter(false);

			    // VOVectors
			    VOVectors( oAT.VO.Vector );
			    WaitingForEnter(false);

			    // AccessConstraints
			    AccessConstraints( oAT.AccessConstraints );
			    WaitingForEnter(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:");
                Console.WriteLine("\t"+ex.Message);
                Environment.Exit(0);
            }
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

		#region BasicBoundary
		public void BasicBoundary( IAgAreaTarget oAT )
		{
			Console.WriteLine("----- BASIC BOUNDARY ----- BEGIN -----");
			if(oAT == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// ResetUnits
			m_oApplication.UnitPreferences.ResetUnits();
			// AreaType (eEllipse)
			Console.WriteLine("\tThe current AreaType is: {0}", oAT.AreaType);
			oAT.AreaType = AgEAreaType.eEllipse;
			Console.WriteLine("\tThe new AreaType is: {0}", oAT.AreaType);
			// AreaTypeData
			IAgAreaTypeEllipse oEllipse = (IAgAreaTypeEllipse)oAT.AreaTypeData;
			// SemiMajorAxis
			Console.WriteLine("\tThe current SemiMajorAxis is: {0}", oEllipse.SemiMajorAxis);
			oEllipse.SemiMajorAxis = 456.123;
			Console.WriteLine("\tThe new SemiMajorAxis is: {0}", oEllipse.SemiMajorAxis);
			// SemiMinorAxis
			Console.WriteLine("\tThe current SemiMinorAxis is: {0}", oEllipse.SemiMinorAxis);
			oEllipse.SemiMinorAxis = 123.456;
			Console.WriteLine("\tThe new SemiMinorAxis is: {0}", oEllipse.SemiMinorAxis);
			// Bearing
			Console.WriteLine("\tThe current Bearing is: {0}", oEllipse.Bearing);
			oEllipse.Bearing = 12.34;
			Console.WriteLine("\tThe new Bearing is: {0}", oEllipse.Bearing);

			// AreaType (ePattern)
			oAT.AreaType = AgEAreaType.ePattern;
			Console.WriteLine("\tThe new AreaType is: {0}", oAT.AreaType);
			// AreaTypeData
			IAgAreaTypePatternCollection oCollection = (IAgAreaTypePatternCollection)oAT.AreaTypeData;
			// Count
			Console.WriteLine("\tThe current AreaTypePattern collection contains: {0} elements", oCollection.Count);
			foreach(IAgAreaTypePattern oElement in oCollection)
			{
				Console.WriteLine("\t\tElement: Latitude = {0}, Longitude = {1}", oElement.Lat, oElement.Lon);
			}
			// RemoveAll
			oCollection.RemoveAll();
			Console.WriteLine("\tThe new AreaTypePattern collection contains: {0} elements", oCollection.Count);
			// Add
			oCollection.Add( 78.9, 123.456 );
			Console.WriteLine("\tThe new AreaTypePattern collection contains: {0} elements", oCollection.Count);
			for( int iIndex = 0; iIndex < oCollection.Count; iIndex++ )
			{
				// Index
				Console.WriteLine("\t\tElement {0}: Latitude = {1}, Longitude = {2}",
					iIndex, oCollection[iIndex].Lat, oCollection[iIndex].Lon);
			}
			// Remove
			oCollection.Remove(0);
			Console.WriteLine("\tThe new AreaTypePattern collection contains: {0} elements", oCollection.Count);
			// Add
			oCollection.Add( -12.34, -123.456 );
			Console.WriteLine("\tThe current AreaTypePattern collection contains: {0} elements", oCollection.Count);
			for( int iIndex = 0; iIndex < oCollection.Count; iIndex++ )
			{
				// Index
				Console.WriteLine("\t\tElement {0}: Latitude = {1}, Longitude = {2}",
					iIndex, oCollection[iIndex].Lat, oCollection[iIndex].Lon);
			}

			IAgAreaTypePattern oPattern = oCollection[0];
			// Lat
			oPattern.Lat = 34.12;
			// Lon
			oPattern.Lon = 321.654;
			Console.WriteLine("\tThe new AreaTypePattern collection contains: {0} elements", oCollection.Count);
			for( int iIndex = 0; iIndex < oCollection.Count; iIndex++ )
			{
				// Index
				Console.WriteLine("\t\tElement {0}: Latitude = {1}, Longitude = {2}",
					iIndex, oCollection[iIndex].Lat, oCollection[iIndex].Lon);
			}
			// RemoveAll
			oCollection.RemoveAll();
			Console.WriteLine("\tThe new AreaTypePattern collection contains: {0} elements", oCollection.Count);
			Console.WriteLine("----- BASIC BOUNDARY ----- END -----");
		}
		#endregion

		#region BasicCentroid
		public void BasicCentroid( IAgAreaTarget oAT )
		{
			Console.WriteLine("----- BASIC CENTROID ----- BEGIN -----");
			if(oAT == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// ResetUnits
			m_oApplication.UnitPreferences.ResetUnits();
			// AutoCentroid
			Console.WriteLine("\tThe current AutoCentroid is: {0}", oAT.AutoCentroid);
			oAT.AutoCentroid = false;
			Console.WriteLine("\tThe new AutoCentroid is: {0}", oAT.AutoCentroid);
			oAT.AutoCentroid = true;
			Console.WriteLine("\tThe new AutoCentroid is: {0}", oAT.AutoCentroid);
			// UseTerrainData
			Console.WriteLine("\tThe current UseTerrainData is: {0}", oAT.UseTerrainData);
			oAT.UseTerrainData = false;
			Console.WriteLine("\tThe new UseTerrainData is: {0}", oAT.UseTerrainData);
			oAT.UseTerrainData = true;
			Console.WriteLine("\tThe new UseTerrainData is: {0}", oAT.UseTerrainData);
			// UseLocalTimeOffset (false)
			Console.WriteLine("\tThe current UseLocalTimeOffset is: {0}", oAT.UseLocalTimeOffset);
			oAT.UseLocalTimeOffset = false;
			Console.WriteLine("\tThe new UseLocalTimeOffset is: {0}", oAT.UseLocalTimeOffset);
			// LocalTimeOffset
			try { oAT.LocalTimeOffset = 321; }
			catch(Exception e) { Console.Write("\t\tExpected exception: {0}", e.Message); }
			// UseLocalTimeOffset (true)
			oAT.UseLocalTimeOffset = true;
			Console.WriteLine("\tThe new UseLocalTimeOffset is: {0}", oAT.UseLocalTimeOffset);
			// LocalTimeOffset
			Console.WriteLine("\tThe current LocalTimeOffset is: {0}", oAT.LocalTimeOffset);
			oAT.LocalTimeOffset = 12.34;
			Console.WriteLine("\tThe new LocalTimeOffset is: {0}", oAT.LocalTimeOffset);
			// Position
			Position( oAT.Position, Positions.All );

			Console.WriteLine("----- BASIC CENTROID ----- END -----");
		}
		#endregion

		#region Position
		private void Position( IAgPosition oPosition, Positions eTypes )
		{
			Console.WriteLine("----- BASIC POSITION ----- BEGIN -----");
			if(oPosition == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// set DistanceUnit
			String strDistanceUnit = m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("DistanceUnit");
			Console.WriteLine("\tThe current DistanceUnit format is: {0}", strDistanceUnit);
			m_oApplication.UnitPreferences.SetCurrentUnit("DistanceUnit", "m");
			Console.WriteLine("\tThe new DistanceUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("DistanceUnit"));
			// set LatitudeUnit
			string strLatitudeUnit = m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("LatitudeUnit");
			Console.WriteLine("\tThe current LatitudeUnit format is: {0}", strLatitudeUnit);
			m_oApplication.UnitPreferences.SetCurrentUnit("LatitudeUnit","rad");
			Console.WriteLine("\tThe new LatitudeUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("LatitudeUnit"));
			// set LongitudeUnit
			string strLongitudeUnit = m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("LongitudeUnit");
			Console.WriteLine("\tThe current LongitudeUnit format is: {0}", strLongitudeUnit);
			m_oApplication.UnitPreferences.SetCurrentUnit("LongitudeUnit", "rad");
			Console.WriteLine("\tThe new LongitudeUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("LongitudeUnit"));
			// set AngleUnit
			string strAngleUnit = m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("AngleUnit");
			Console.WriteLine("\tThe current AngleUnit format is: {0}", strAngleUnit);
			m_oApplication.UnitPreferences.SetCurrentUnit("AngleUnit", "rad");
			Console.WriteLine("\tThe new AngleUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("AngleUnit"));

			Console.WriteLine("\tCurrent position type is: {0}", oPosition.PosType);
			DisplayPosition( oPosition );
			// Cartesian position test
			if( (eTypes & Positions.Cartesian) == Positions.Cartesian )
			{
				IAgCartesian oCartesian =
					(IAgCartesian)oPosition.ConvertTo(AgEPositionType.eCartesian);
				Console.WriteLine("\tNew position type is: {0}", oCartesian.PosType);
				Console.WriteLine("\t\tCurrent values:");
				DisplayPosition( oCartesian );

				oCartesian.ConvertTo(AgEPositionType.eCartesian);
				if( (eTypes & Positions.Cylindrical) == Positions.Cylindrical )
					oCartesian.ConvertTo(AgEPositionType.eCylindrical);
				if( (eTypes & Positions.Geocentric) == Positions.Geocentric )
                    oCartesian.ConvertTo(AgEPositionType.eGeocentric);
				if( (eTypes & Positions.Geodetic) == Positions.Geodetic )
                    oCartesian.ConvertTo(AgEPositionType.eGeodetic);
				if( (eTypes & Positions.Spherical) == Positions.Spherical )
					oCartesian.ConvertTo(AgEPositionType.eSpherical);
                if ((eTypes & Positions.Planetocentric) == Positions.Planetocentric)
                    oCartesian.ConvertTo(AgEPositionType.ePlanetocentric);
                if ((eTypes & Positions.Planetodetic) == Positions.Planetodetic)
                    oCartesian.ConvertTo(AgEPositionType.ePlanetodetic);    
			}

			// Cylindrical position test
			if( (eTypes & Positions.Cylindrical) == Positions.Cylindrical )
			{
				IAgCylindrical oCylindrical = 
					(IAgCylindrical)oPosition.ConvertTo(AgEPositionType.eCylindrical);
				Console.WriteLine("\tNew position type is: {0}", oCylindrical.PosType);
				Console.WriteLine("\t\tCurrent values:");
				DisplayPosition( oCylindrical );

				oCylindrical.ConvertTo(AgEPositionType.eCylindrical);
				if( (eTypes & Positions.Cartesian) == Positions.Cartesian )
					oCylindrical.ConvertTo(AgEPositionType.eCartesian);
                if ((eTypes & Positions.Geocentric) == Positions.Geocentric)
                    oCylindrical.ConvertTo(AgEPositionType.eGeocentric);
                if ((eTypes & Positions.Geodetic) == Positions.Geodetic)
                    oCylindrical.ConvertTo(AgEPositionType.eGeodetic);
				if( (eTypes & Positions.Spherical) == Positions.Spherical )
					oCylindrical.ConvertTo(AgEPositionType.eSpherical);
                if ((eTypes & Positions.Planetocentric) == Positions.Planetocentric)
                    oCylindrical.ConvertTo(AgEPositionType.ePlanetocentric);
                if ((eTypes & Positions.Planetodetic) == Positions.Planetodetic)
                    oCylindrical.ConvertTo(AgEPositionType.ePlanetodetic);
			}

			// Geocentric position test
            if ((eTypes & Positions.Geocentric) == Positions.Geocentric)
			{
                IAgGeocentric oGeocentric =
                    (IAgGeocentric)oPosition.ConvertTo(AgEPositionType.eGeocentric);
                Console.WriteLine("\tNew position type is: {0}", oGeocentric.PosType);
				Console.WriteLine("\t\tCurrent values:");
                DisplayPosition(oGeocentric);

                oGeocentric.ConvertTo(AgEPositionType.eGeocentric);
				if( (eTypes & Positions.Cartesian) == Positions.Cartesian )
                    oGeocentric.ConvertTo(AgEPositionType.eCartesian);
				if( (eTypes & Positions.Cylindrical) == Positions.Cylindrical )
                    oGeocentric.ConvertTo(AgEPositionType.eCylindrical);
				if( (eTypes & Positions.Geodetic) == Positions.Geodetic )
                    oGeocentric.ConvertTo(AgEPositionType.eGeodetic);
				if( (eTypes & Positions.Spherical) == Positions.Spherical )
                    oGeocentric.ConvertTo(AgEPositionType.eSpherical);
                if ((eTypes & Positions.Planetocentric) == Positions.Planetocentric)
                    oGeocentric.ConvertTo(AgEPositionType.ePlanetocentric);
                if ((eTypes & Positions.Planetodetic) == Positions.Planetodetic)
                    oGeocentric.ConvertTo(AgEPositionType.ePlanetodetic);
			}

			// Geodetic position test
            if ((eTypes & Positions.Geodetic) == Positions.Geodetic)
			{
                IAgGeodetic oGeodetic =
                    (IAgGeodetic)oPosition.ConvertTo(AgEPositionType.eGeodetic);
                Console.WriteLine("\tNew position type is: {0}", oGeodetic.PosType);
				Console.WriteLine("\t\tCurrent values:");
                DisplayPosition(oGeodetic);

                oGeodetic.ConvertTo(AgEPositionType.eGeodetic);
				if( (eTypes & Positions.Cartesian) == Positions.Cartesian )
                    oGeodetic.ConvertTo(AgEPositionType.eCartesian);
				if( (eTypes & Positions.Cylindrical) == Positions.Cylindrical )
                    oGeodetic.ConvertTo(AgEPositionType.eCylindrical);
				if( (eTypes & Positions.Geocentric) == Positions.Geocentric )
                    oGeodetic.ConvertTo(AgEPositionType.eGeocentric);
				if( (eTypes & Positions.Spherical) == Positions.Spherical )
                    oGeodetic.ConvertTo(AgEPositionType.eSpherical);
                if ((eTypes & Positions.Planetocentric) == Positions.Planetocentric)
                    oGeodetic.ConvertTo(AgEPositionType.ePlanetocentric);
                if ((eTypes & Positions.Planetodetic) == Positions.Planetodetic)
                    oGeodetic.ConvertTo(AgEPositionType.ePlanetodetic);
			}

			// Spherical position test
			if( (eTypes & Positions.Spherical) == Positions.Spherical )
			{
				IAgSpherical oSpherical =
					(IAgSpherical)oPosition.ConvertTo(AgEPositionType.eSpherical);
				Console.WriteLine("\tNew position type is: {0}", oSpherical.PosType);
				Console.WriteLine("\t\tCurrent values:");
				DisplayPosition( oSpherical );

				oSpherical.ConvertTo(AgEPositionType.eSpherical);
				if( (eTypes & Positions.Cartesian) == Positions.Cartesian )
					oSpherical.ConvertTo(AgEPositionType.eCartesian);
				if( (eTypes & Positions.Cylindrical) == Positions.Cylindrical )
					oSpherical.ConvertTo(AgEPositionType.eCylindrical);
                if ((eTypes & Positions.Geocentric) == Positions.Geocentric)
                    oSpherical.ConvertTo(AgEPositionType.eGeocentric);
                if ((eTypes & Positions.Geodetic) == Positions.Geodetic)
                    oSpherical.ConvertTo(AgEPositionType.eGeodetic);
                if ((eTypes & Positions.Planetocentric) == Positions.Planetocentric)
                    oSpherical.ConvertTo(AgEPositionType.ePlanetocentric);
                if ((eTypes & Positions.Planetodetic) == Positions.Planetodetic)
                    oSpherical.ConvertTo(AgEPositionType.ePlanetodetic);
			}
            // Planetocentric position test
            if ((eTypes & Positions.Planetocentric) == Positions.Planetocentric)
            {
                IAgPlanetocentric oPlanetocentric =
                    (IAgPlanetocentric)oPosition.ConvertTo(AgEPositionType.ePlanetocentric);
                Console.WriteLine("\tNew position type is: {0}", oPlanetocentric.PosType);
                Console.WriteLine("\t\tCurrent values:");
                DisplayPosition(oPlanetocentric);

                oPlanetocentric.ConvertTo(AgEPositionType.ePlanetocentric);
                if ((eTypes & Positions.Cartesian) == Positions.Cartesian)
                    oPlanetocentric.ConvertTo(AgEPositionType.eCartesian);
                if ((eTypes & Positions.Cylindrical) == Positions.Cylindrical)
                    oPlanetocentric.ConvertTo(AgEPositionType.eCylindrical);
                if ((eTypes & Positions.Geocentric) == Positions.Geocentric)
                    oPlanetocentric.ConvertTo(AgEPositionType.eGeocentric);
                if ((eTypes & Positions.Geodetic) == Positions.Geodetic)
                    oPlanetocentric.ConvertTo(AgEPositionType.eGeodetic);
                if ((eTypes & Positions.Spherical) == Positions.Spherical)
                    oPlanetocentric.ConvertTo(AgEPositionType.eSpherical);
                if ((eTypes & Positions.Planetodetic) == Positions.Planetodetic)
                    oPlanetocentric.ConvertTo(AgEPositionType.ePlanetodetic);
            }

            // Planetodetic position test
            if ((eTypes & Positions.Planetodetic) == Positions.Planetodetic)
            {
                IAgPlanetodetic oPlanetodetic =
                    (IAgPlanetodetic)oPosition.ConvertTo(AgEPositionType.ePlanetodetic);
                Console.WriteLine("\tNew position type is: {0}", oPlanetodetic.PosType);
                Console.WriteLine("\t\tCurrent values:");
                DisplayPosition(oPlanetodetic);

                oPlanetodetic.ConvertTo(AgEPositionType.ePlanetodetic);
                if ((eTypes & Positions.Cartesian) == Positions.Cartesian)
                    oPlanetodetic.ConvertTo(AgEPositionType.eCartesian);
                if ((eTypes & Positions.Cylindrical) == Positions.Cylindrical)
                    oPlanetodetic.ConvertTo(AgEPositionType.eCylindrical);
                if ((eTypes & Positions.Geocentric) == Positions.Geocentric)
                    oPlanetodetic.ConvertTo(AgEPositionType.eGeocentric);
                if ((eTypes & Positions.Geodetic) == Positions.Geodetic)
                    oPlanetodetic.ConvertTo(AgEPositionType.eGeodetic);
                if ((eTypes & Positions.Spherical) == Positions.Spherical)
                    oPlanetodetic.ConvertTo(AgEPositionType.eSpherical);
                if ((eTypes & Positions.Planetocentric) == Positions.Planetocentric)
                    oPlanetodetic.ConvertTo(AgEPositionType.ePlanetocentric);
            }
			// restore AngleUnit
			m_oApplication.UnitPreferences.SetCurrentUnit("AngleUnit", strAngleUnit);
			Console.WriteLine("\tThe restored AngleUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("AngleUnit"));
			// restore DistanceUnit
			m_oApplication.UnitPreferences.SetCurrentUnit("DistanceUnit", strDistanceUnit);
			Console.WriteLine("\tThe restored DistanceUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("DistanceUnit"));
			// restore LatitudeUnit
			m_oApplication.UnitPreferences.SetCurrentUnit("LatitudeUnit", strLatitudeUnit);
			Console.WriteLine("\tThe restored LatitudeUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("LatitudeUnit"));
			// restore LongitudeUnit
			m_oApplication.UnitPreferences.SetCurrentUnit("LongitudeUnit", strLongitudeUnit);
			Console.WriteLine("\tThe restored LongitudeUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("LongitudeUnit"));
			Console.WriteLine("----- BASIC POSITION ----- END -----");
		}
		#endregion

		#region DisplayPosition
		private void DisplayPosition( AGI.STKUtil.IAgPosition oPosition )
		{
			if(oPosition == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			switch( oPosition.PosType )
			{
				case AgEPositionType.eCartesian:
				{
					IAgCartesian oCartesian =
						(IAgCartesian)oPosition.ConvertTo(AgEPositionType.eCartesian);
					Console.WriteLine("\t\tCartesian X is: {0}", oCartesian.X);
					Console.WriteLine("\t\tCartesian Y is: {0}", oCartesian.Y);
					Console.WriteLine("\t\tCartesian Z is: {0}", oCartesian.Z);
					break;
				}
				case AgEPositionType.eCylindrical:
				{
					IAgCylindrical oCylindrical =
						(IAgCylindrical)oPosition.ConvertTo(AgEPositionType.eCylindrical);
					Console.WriteLine("\t\tCylindrical Radius is: {0}", oCylindrical.Radius);
					Console.WriteLine("\t\tCylindrical Z is: {0}", oCylindrical.Z);
					Console.WriteLine("\t\tCylindrical Lon is: {0}", oCylindrical.Lon);
					break;
				}
				case AgEPositionType.eSpherical:
				{
					IAgSpherical oSpherical =
						(IAgSpherical)oPosition.ConvertTo(AgEPositionType.eSpherical);
					Console.WriteLine("\t\tSpherical Lat is: {0}", oSpherical.Lat);
					Console.WriteLine("\t\tSpherical Lon is: {0}", oSpherical.Lon);
					Console.WriteLine("\t\tSpherical Radius is: {0}", oSpherical.Radius);
					break;
				}
                case AgEPositionType.eGeodetic:
                {
                    IAgGeodetic oGeodetic =
                        (IAgGeodetic)oPosition.ConvertTo(AgEPositionType.eGeodetic);
                    Console.WriteLine("\t\tGeodetic Lat is: {0}", oGeodetic.Lat);
                    Console.WriteLine("\t\tGeodetic Lon is: {0}", oGeodetic.Lon);
                    Console.WriteLine("\t\tGeodetic Alt is: {0}", oGeodetic.Alt);
                    break;
                }
                case AgEPositionType.eGeocentric:
                {
                    IAgGeocentric oGeocentric =
                        (IAgGeocentric)oPosition.ConvertTo(AgEPositionType.eGeocentric);
                    Console.WriteLine("\t\tGeocentric Lat is: {0}", oGeocentric.Lat);
                    Console.WriteLine("\t\tGeocentric Lon is: {0}", oGeocentric.Lon);
                    Console.WriteLine("\t\tGeocentric Alt is: {0}", oGeocentric.Alt);
                    break;
                }
                case AgEPositionType.ePlanetodetic:
                {
                    IAgPlanetodetic oPlanetodetic =
                        (IAgPlanetodetic)oPosition.ConvertTo(AgEPositionType.ePlanetodetic);
                    Console.WriteLine("\t\tPlanetodetic Lat is: {0}", oPlanetodetic.Lat);
                    Console.WriteLine("\t\tPlanetodetic Lon is: {0}", oPlanetodetic.Lon);
                    Console.WriteLine("\t\tPlanetodetic Alt is: {0}", oPlanetodetic.Alt);
                    break;
                }
                case AgEPositionType.ePlanetocentric:
                {
                    IAgPlanetocentric oPlanetocentric =
                        (IAgPlanetocentric)oPosition.ConvertTo(AgEPositionType.ePlanetocentric);
                    Console.WriteLine("\t\tPlanetocentric Lat is: {0}", oPlanetocentric.Lat);
                    Console.WriteLine("\t\tPlanetocentric Lon is: {0}", oPlanetocentric.Lon);
                    Console.WriteLine("\t\tPlanetocentric Alt is: {0}", oPlanetocentric.Alt);
                    break;
                }
				default:
					Console.WriteLine("\tInvalid Position type!");
					break;
			}
		}
		#endregion

		#region Graphics
		public void Graphics( IAgATGraphics oGraphics )
		{
			Console.WriteLine("----- GRAPHICS ----- BEGIN -----");
			if(oGraphics == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// Inherit
			Console.WriteLine("\tThe current Inherit is: {0}", oGraphics.Inherit);
			oGraphics.Inherit = true;
			Console.WriteLine("\tThe new Inherit is: {0}", oGraphics.Inherit);
			oGraphics.Inherit = false;
			Console.WriteLine("\tThe new Inherit is: {0}", oGraphics.Inherit);
			// LabelVisible
			Console.WriteLine("\tThe current LabelVisible is: {0}", oGraphics.LabelVisible);
			oGraphics.LabelVisible = false;
			Console.WriteLine("\tThe new LabelVisible is: {0}", oGraphics.LabelVisible);
			oGraphics.LabelVisible = true;
			Console.WriteLine("\tThe new LabelVisible is: {0}", oGraphics.LabelVisible);
			// CentroidVisible
			Console.WriteLine("\tThe current CentroidVisible is: {0}", oGraphics.CentroidVisible);
			oGraphics.CentroidVisible = false;
			Console.WriteLine("\tThe new CentroidVisible is: {0}", oGraphics.CentroidVisible);
			oGraphics.CentroidVisible = true;
			Console.WriteLine("\tThe new CentroidVisible is: {0}", oGraphics.CentroidVisible);
			// BoundaryVisible
			Console.WriteLine("\tThe current BoundaryVisible is: {0}", oGraphics.BoundaryVisible);
			oGraphics.BoundaryVisible = false;
			Console.WriteLine("\tThe new BoundaryVisible is: {0}", oGraphics.BoundaryVisible);
			oGraphics.BoundaryVisible = true;
			Console.WriteLine("\tThe new BoundaryVisible is: {0}", oGraphics.BoundaryVisible);
			// BoundaryFill
			Console.WriteLine("\tThe current BoundaryFill is: {0}", oGraphics.BoundaryFill);
			oGraphics.BoundaryFill = false;
			Console.WriteLine("\tThe new BoundaryFill is: {0}", oGraphics.BoundaryFill);
			oGraphics.BoundaryFill = true;
			Console.WriteLine("\tThe new BoundaryFill is: {0}", oGraphics.BoundaryFill);
			// BoundingRectVisible
			Console.WriteLine("\tThe current BoundingRectVisible is: {0}", oGraphics.BoundingRectVisible);
			oGraphics.BoundingRectVisible = false;
			Console.WriteLine("\tThe new BoundingRectVisible is: {0}", oGraphics.BoundingRectVisible);
			oGraphics.BoundingRectVisible = true;
			Console.WriteLine("\tThe new BoundingRectVisible is: {0}", oGraphics.BoundingRectVisible);
			// BoundaryPtsVisible
			Console.WriteLine("\tThe current BoundaryPtsVisible is: {0}", oGraphics.BoundaryPtsVisible);
			oGraphics.BoundaryPtsVisible = false;
			Console.WriteLine("\tThe new BoundaryPtsVisible is: {0}", oGraphics.BoundaryPtsVisible);
			oGraphics.BoundaryPtsVisible = true;
			Console.WriteLine("\tThe new BoundaryPtsVisible is: {0}", oGraphics.BoundaryPtsVisible);
			// BoundaryColor
			Console.WriteLine("\tThe current BoundaryColor is: {0}", oGraphics.BoundaryColor);
			oGraphics.BoundaryColor = Color.FromArgb(0x123456);
			Console.WriteLine("\tThe new BoundaryColor is: {0}", oGraphics.BoundaryColor);
			// BoundaryWidth
			Console.WriteLine("\tThe current BoundaryWidth is: {0}", oGraphics.BoundaryWidth);
			oGraphics.BoundaryWidth = 3;
			Console.WriteLine("\tThe new BoundaryWidth is: {0}", oGraphics.BoundaryWidth);
			// BoundaryStyle
			Console.WriteLine("\tThe current BoundaryStyle is: {0}", oGraphics.BoundaryStyle);
			oGraphics.BoundaryStyle = AgELineStyle.eDashed;
			Console.WriteLine("\tThe new BoundaryStyle is: {0}", oGraphics.BoundaryStyle);
			// CentroidColor
			Console.WriteLine("\tThe current CentroidColor is: {0}", oGraphics.CentroidColor);
			oGraphics.CentroidColor = Color.FromArgb(0x234567);
			Console.WriteLine("\tThe new CentroidColor is: {0}", oGraphics.CentroidColor);
			// Color
			Console.WriteLine("\tThe current Color is: {0}", oGraphics.Color);
			oGraphics.Color = Color.FromArgb(0x345678);
			Console.WriteLine("\tThe new Color is: {0}", oGraphics.Color);
			// LabelColor
			Console.WriteLine("\tThe current LabelColor is: {0}", oGraphics.LabelColor);
			oGraphics.LabelColor = Color.FromArgb(0x456789);
			Console.WriteLine("\tThe new LabelColor is: {0}", oGraphics.LabelColor);
			// LabelName
			Console.WriteLine("\tThe current LabelName is: {0}", oGraphics.LabelName);
			oGraphics.LabelName = "My target";
			Console.WriteLine("\tThe new LabelName is: {0}", oGraphics.LabelName);
			// MarkerStyle
			Console.WriteLine("\tThe current MarkerStyle is: {0}", oGraphics.MarkerStyle);
			oGraphics.MarkerStyle = "Star";
			Console.WriteLine("\tThe new MarkerStyle is: {0}", oGraphics.MarkerStyle);
			// UseInstNameLabel
			Console.WriteLine("\tThe current UseInstNameLabel is: {0}", oGraphics.UseInstNameLabel);
			oGraphics.UseInstNameLabel = false;
			Console.WriteLine("\tThe new UseInstNameLabel is: {0}", oGraphics.UseInstNameLabel);
			oGraphics.UseInstNameLabel = true;
			Console.WriteLine("\tThe new UseInstNameLabel is: {0}", oGraphics.UseInstNameLabel);
			Console.WriteLine("\tThe new LabelName is: {0}", oGraphics.LabelName);
			// LabelNotes
			LabelNotes( oGraphics.LabelNotes );

			Console.WriteLine("----- GRAPHICS ----- END -----");
		}
		#endregion

		#region LabelNotes
		private void LabelNotes( IAgLabelNoteCollection oCollection )
		{
			Console.WriteLine("----- GRAPHICS LABELNOTES ----- BEGIN -----");
			if(oCollection == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// Count
			Console.WriteLine("\tThe current LabelNotes collection contains: {0} elements.", oCollection.Count);
			for( int iIndex = 0; iIndex < oCollection.Count; iIndex++ )
			{
				Console.WriteLine("\t\tElement {0}: Note = {1}, NoteVisible = {2}, LabelVisible = {3}",
					iIndex, oCollection[iIndex].Note, oCollection[iIndex].NoteVisible,
					oCollection[iIndex].LabelVisible );
			}
			// Count
			int iCount = oCollection.Count;
			// Add
			IAgLabelNote oNote = oCollection.Add("Label Note 1");
			// Add
			oNote = oCollection.Add("Label Note 2");
			Console.WriteLine("\tThe new LabelNotes collection contains: {0} elements.", oCollection.Count);
			foreach( IAgLabelNote oElement in oCollection )
			{
				// Note, NoteVisible, LabelVisible
				Console.WriteLine("\t\tElement: Note = {0}, NoteVisible = {1}, LabelVisible = {2}",
					oElement.Note, oElement.NoteVisible, oElement.LabelVisible );
			}
			// Remove
			oCollection.Remove(oCollection.Count - 1);
			Console.WriteLine("\tThe new LabelNotes collection contains: {0} elements", oCollection.Count);
			foreach( IAgLabelNote oElement in oCollection )
			{
				Console.WriteLine("\t\tBefore: Note = {0}, NoteVisible = {1}, LabelVisible = {2}",
					oElement.Note, oElement.NoteVisible, oElement.LabelVisible );
				// Note
				oElement.Note = "Modified Label Note";
				// LabelVisible
				oElement.LabelVisible = true;
				// NoteVisible
				oElement.NoteVisible = AGI.STKObjects.AgENoteShowType.eNoteOn;
				oElement.NoteVisible = AGI.STKObjects.AgENoteShowType.eNoteIntervals;
				oElement.NoteVisible = AGI.STKObjects.AgENoteShowType.eNoteOff;
				Console.WriteLine("\t\tAfter: Note = {0}, NoteVisible = {1}, LabelVisible = {2}",
					oElement.Note, oElement.NoteVisible, oElement.LabelVisible );
			}
			Console.WriteLine("----- GRAPHICS LABELNOTES ----- END -----");
		}
		#endregion

		#region DisplayTimes
		private void DisplayTimes( IAgDisplayTm oDisplay )
		{
			Console.WriteLine("----- GRAPHICS DISPLAY TIMES ----- BEGIN -----");
			if(oDisplay == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// DisplayStatusSupportedTypes
			System.Array arTypes = oDisplay.DisplayStatusSupportedTypes;
			Console.WriteLine("\tDisplayTimes supports {0} types:", arTypes.GetLength(0));
			for( int iIndex = 0; iIndex < arTypes.GetLength(0); iIndex++ )
			{
				Console.WriteLine("\t\tType {0}: {1} ({2})", iIndex, arTypes.GetValue(iIndex,1),
					(AGI.STKObjects.AgEDisplayTimesType)arTypes.GetValue(iIndex,0) );
			}
			// eDuringChainAccess
			if( !oDisplay.IsDisplayStatusTypeSupported(AGI.STKObjects.AgEDisplayTimesType.eDuringChainAccess) )
			{
				try
				{
					oDisplay.SetDisplayStatusType(AGI.STKObjects.AgEDisplayTimesType.eDuringChainAccess);
				}
				catch(Exception e)
				{
					Console.WriteLine("\tExpected exception: {0}", e.Message);
				}
			}
			// eDisplayTypeUnknown
			try
			{
				oDisplay.SetDisplayStatusType(AGI.STKObjects.AgEDisplayTimesType.eDisplayTypeUnknown);
			}
			catch(Exception e)
			{
				Console.WriteLine("\tExpected exception: {0}", e.Message);
			}
			// DisplayStatusType
			Console.WriteLine("\tThe current DisplayStatusType is: {0}", oDisplay.DisplayStatusType);
			for( int iIndex = 0; iIndex < arTypes.GetLength(0); iIndex++ )
			{
				AgEDisplayTimesType eType =	(AgEDisplayTimesType)arTypes.GetValue(iIndex,0);
				// IsDisplayStatusTypeSupported
				if( !oDisplay.IsDisplayStatusTypeSupported( eType ) )
				{
					Console.WriteLine("The {0} type should be supported!", eType);
					continue;
				}
				// SetDisplayStatusType
				oDisplay.SetDisplayStatusType( eType );
				Console.WriteLine("\tThe new DisplayStatusType is: {0}", oDisplay.DisplayStatusType);
				// DisplayTimesData
				switch( eType )
				{
					case AgEDisplayTimesType.eAlwaysOff:
					case AgEDisplayTimesType.eAlwaysOn:
					case AgEDisplayTimesType.eDuringChainAccess:
					{
						Console.WriteLine("\t\tNo DisplayTimesData available.");
						break;
					}
					case AgEDisplayTimesType.eDuringAccess:
					{
						Console.WriteLine("\t\tThe DuringAccess data is available.");
						break;
					}
					case AgEDisplayTimesType.eUseIntervals:
					{
						Console.WriteLine("\t\tThe IntervalCollection data is available.");
						break;
					}
					default:
					{
						Console.WriteLine("Type {0} should not be supported!", eType);
						break;
					}
				}
			}
			Console.WriteLine("----- GRAPHICS DISPLAY TIMES ----- END -----");
		}
		#endregion

		#region VO
		private void VO( IAgATVO oVO )
		{
			Console.WriteLine("----- VO ----- BEGIN -----");
			if(oVO == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// set DistanceUnit
			Console.WriteLine("\tThe current DistanceUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("DistanceUnit"));
			m_oApplication.UnitPreferences.SetCurrentUnit("DistanceUnit", "km");
			Console.WriteLine("\tThe new DistanceUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("DistanceUnit"));
			// set AngleUnit
			Console.WriteLine("\tThe current AngleUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("AngleUnit"));
			m_oApplication.UnitPreferences.SetCurrentUnit("AngleUnit", "deg");
			Console.WriteLine("\tThe new AngleUnit format is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("AngleUnit"));
			// EnableLabelMaxViewingDist (false)
			Console.WriteLine("\tThe current EnableLabelMaxViewingDist is: {0}", oVO.EnableLabelMaxViewingDist);
			oVO.EnableLabelMaxViewingDist = false;
			Console.WriteLine("\tThe new EnableLabelMaxViewingDist is: {0}", oVO.EnableLabelMaxViewingDist);
			// LabelMaxViewingDist
			try { oVO.LabelMaxViewingDist = 1e+012; }
			catch(Exception e) { Console.Write("\t\tExpected exception: {0}", e.Message); }
			// EnableLabelMaxViewingDist (true)
			oVO.EnableLabelMaxViewingDist = true;
			Console.WriteLine("\tThe new EnableLabelMaxViewingDist is: {0}", oVO.EnableLabelMaxViewingDist);
			// LabelMaxViewingDist
			Console.WriteLine("\tThe current LabelMaxViewingDist is: {0}", oVO.LabelMaxViewingDist);
			oVO.LabelMaxViewingDist = 1e+012;
			Console.WriteLine("\tThe new LabelMaxViewingDist is: {0}", oVO.LabelMaxViewingDist);
			// FillInterior (false)
			Console.WriteLine("\tThe current FillInterior is: {0}", oVO.FillInterior);
			oVO.FillInterior = false;
			Console.WriteLine("\tThe new FillInterior is: {0}", oVO.FillInterior);
			// LabelMaxViewingDist
			try { oVO.PercentTranslucencyInterior = 34; }
			catch(Exception e) { Console.WriteLine("\t\tExpected exception: {0}", e.Message); }
			// FillGranularity
			try { oVO.FillGranularity = 44; }
			catch(Exception e) { Console.Write("\t\tExpected exception: {0}", e.Message); }
			// FillInterior (true)
			oVO.FillInterior = true;
			Console.WriteLine("\tThe new FillInterior is: {0}", oVO.FillInterior);
			// PercentTranslucencyInterior
			Console.WriteLine("\tThe current PercentTranslucencyInterior is: {0}", oVO.PercentTranslucencyInterior);
			oVO.PercentTranslucencyInterior = 12;
			Console.WriteLine("\tThe new PercentTranslucencyInterior is: {0}", oVO.PercentTranslucencyInterior);
			// FillGranularity
			Console.WriteLine("\tThe current FillGranularity is: {0}", oVO.FillGranularity);
			oVO.FillGranularity = 0.345;
			Console.WriteLine("\tThe new FillGranularity is: {0}", oVO.FillGranularity);
			try { oVO.FillGranularity = 0.001; }
			catch(Exception e) { Console.Write("\t\tExpected exception: {0}", e.Message); }
			try { oVO.FillGranularity = 6.1; }
			catch(Exception e) { Console.Write("\t\tExpected exception: {0}", e.Message); }
			// restore Units
			m_oApplication.UnitPreferences.ResetUnits();
			// BorderWall
			VOBorderWall( oVO.BorderWall );
			Console.WriteLine("----- VO ----- END -----");
		}
		#endregion

		#region VOBorderWall
		private void VOBorderWall( IAgVOBorderWall oWall )
		{
			Console.WriteLine("----- VO BORDER WALL ----- BEGIN -----");
			if(oWall == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// UseBorderWall (false)
			Console.WriteLine("\tThe current UseBorderWall flag is: {0}", oWall.UseBorderWall);
			oWall.UseBorderWall = false;
			Console.WriteLine("\tThe new UseBorderWall flag is: {0}", oWall.UseBorderWall);
			// UpperEdgeAltRef
			try { oWall.UpperEdgeAltRef = AgEBorderWallUpperLowerEdgeAltRef.eAltRefMSL; }
			catch(Exception e) { Console.WriteLine("\t\tExpected exception: {0}", e.Message); }
			// LowerEdgeAltRef
			try { oWall.LowerEdgeAltRef = AgEBorderWallUpperLowerEdgeAltRef.eAltRefTerrain; }
			catch(Exception e) { Console.WriteLine("\t\tExpected exception: {0}", e.Message); }
			// UpperEdgeHeight
			try { oWall.UpperEdgeHeight = 12.34; }
			catch(Exception e) { Console.Write("\t\tExpected exception: {0}", e.Message); }
			// LowerEdgeHeight
			try { oWall.LowerEdgeHeight = 34.12; }
			catch(Exception e) { Console.Write("\t\tExpected exception: {0}", e.Message); }
			// UseWallTranslucency
			try { oWall.UseWallTranslucency = false; }
			catch(Exception e) { Console.WriteLine("\t\tExpected exception: {0}", e.Message); }
			// UseLineTranslucency
			try { oWall.UseLineTranslucency = false; }
			catch(Exception e) { Console.WriteLine("\t\tExpected exception: {0}", e.Message); }
			// WallTranslucency
			try { oWall.WallTranslucency = 34.56; }
			catch(Exception e) { Console.WriteLine("\t\tExpected exception: {0}", e.Message); }
			// LineTranslucency
			try { oWall.LineTranslucency = 56.34; }
			catch(Exception e) { Console.WriteLine("\t\tExpected exception: {0}", e.Message); }
			// UseBorderWall (true)
			oWall.UseBorderWall = true;
			Console.WriteLine("\tThe new UseBorderWall flag is: {0}", oWall.UseBorderWall);
			// set DistanceUnit
			string strDistanceUnit = m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("DistanceUnit");
			Console.WriteLine("\t\tThe current DistanceUnit is: {0}", strDistanceUnit);
			m_oApplication.UnitPreferences.SetCurrentUnit("DistanceUnit", "mi");
			Console.WriteLine("\t\tThe new DistanceUnit is: {0}",
				m_oApplication.UnitPreferences.GetCurrentUnitAbbrv("DistanceUnit"));
			// UpperEdgeAltRef
			Console.WriteLine("\t\tThe current UpperEdge is: {0}", oWall.UpperEdgeAltRef);
			oWall.UpperEdgeAltRef = AgEBorderWallUpperLowerEdgeAltRef.eAltRefMSL;
			Console.WriteLine("\t\tThe new UpperEdge is: {0}", oWall.UpperEdgeAltRef);
			oWall.UpperEdgeAltRef = AgEBorderWallUpperLowerEdgeAltRef.eAltRefObject;
			Console.WriteLine("\t\tThe new UpperEdge is: {0}", oWall.UpperEdgeAltRef);
			oWall.UpperEdgeAltRef = AgEBorderWallUpperLowerEdgeAltRef.eAltRefTerrain;
			Console.WriteLine("\t\tThe new UpperEdge is: {0}", oWall.UpperEdgeAltRef);
			oWall.UpperEdgeAltRef = AgEBorderWallUpperLowerEdgeAltRef.eAltRefWGS84;
			Console.WriteLine("\t\tThe new UpperEdge is: {0}", oWall.UpperEdgeAltRef);
			// UpperEdgeHeight
			Console.WriteLine("\t\tThe current UpperEdgeHeight is: {0}", oWall.UpperEdgeHeight);
			oWall.UpperEdgeHeight = 123.4567;
			Console.WriteLine("\t\tThe new UpperEdgeHeight is: {0}", oWall.UpperEdgeHeight);
			try { oWall.UpperEdgeHeight = -9876543210.1; }
			catch(Exception e) { Console.Write("\t\t\tExpected exception: {0}", e.Message); }
			// LowerEdgeAltRef
			Console.WriteLine("\t\tThe current LowerEdge is: {0}", oWall.LowerEdgeAltRef);
			oWall.LowerEdgeAltRef = AgEBorderWallUpperLowerEdgeAltRef.eAltRefMSL;
			Console.WriteLine("\t\tThe new LowerEdge is: {0}", oWall.LowerEdgeAltRef);
			oWall.LowerEdgeAltRef = AgEBorderWallUpperLowerEdgeAltRef.eAltRefObject;
			Console.WriteLine("\t\tThe new LowerEdge is: {0}", oWall.LowerEdgeAltRef);
			oWall.LowerEdgeAltRef = AgEBorderWallUpperLowerEdgeAltRef.eAltRefTerrain;
			Console.WriteLine("\t\tThe new LowerEdge is: {0}", oWall.LowerEdgeAltRef);
			oWall.LowerEdgeAltRef = AgEBorderWallUpperLowerEdgeAltRef.eAltRefWGS84;
			Console.WriteLine("\t\tThe new LowerEdge is: {0}", oWall.LowerEdgeAltRef);
			// LowerEdgeHeight
			Console.WriteLine("\t\tThe current LowerEdgeHeight is: {0}", oWall.LowerEdgeHeight);
			oWall.LowerEdgeHeight = 123.4567;
			Console.WriteLine("\t\tThe new LowerEdgeHeight is: {0}", oWall.LowerEdgeHeight);
			try { oWall.LowerEdgeHeight = -9876543210.1; }
			catch(Exception e) { Console.Write("\t\t\tExpected exception: {0}", e.Message); }
			// restore DistanceUnit
			m_oApplication.UnitPreferences.SetCurrentUnit("DistanceUnit", strDistanceUnit);
			Console.WriteLine("\t\tThe new DistanceUnit (restored) is: {0}", strDistanceUnit);
			// UseWallTranslucency (false)
			Console.WriteLine("\t\tThe current UseWallTranslucency flag is: {0}", oWall.UseWallTranslucency);
			oWall.UseWallTranslucency = false;
			Console.WriteLine("\t\tThe new UseWallTranslucency flag is: {0}", oWall.UseWallTranslucency);
			try { oWall.WallTranslucency = 34.56; }
			catch(Exception e) { Console.WriteLine("\t\t\tExpected exception: {0}", e.Message); }
			// UseWallTranslucency (true)
			oWall.UseWallTranslucency = true;
			Console.WriteLine("\t\tThe new UseWallTranslucency flag is: {0}", oWall.UseWallTranslucency);
			// WallTranslucency
			Console.WriteLine("\t\t\tThe current WallTranslucency is: {0}", oWall.WallTranslucency);
			oWall.WallTranslucency = 34.56;
			Console.WriteLine("\t\t\tThe new WallTranslucency is: {0}", oWall.WallTranslucency);
			try { oWall.WallTranslucency = 1234.56; }
			catch(Exception e) { Console.Write("\t\t\t\tExpected exception: {0}", e.Message); }
			// UseLineTranslucency (false)
			Console.WriteLine("\t\tThe current UseLineTranslucency flag is: {0}", oWall.UseLineTranslucency);
			oWall.UseLineTranslucency = false;
			Console.WriteLine("\t\tThe new UseLineTranslucency flag is: {0}", oWall.UseLineTranslucency);
			try { oWall.LineTranslucency = 34.56; }
			catch(Exception e) { Console.WriteLine("\t\t\tExpected exception: {0}", e.Message); }
			// UseLineTranslucency (true)
			oWall.UseLineTranslucency = true;
			Console.WriteLine("\t\tThe new UseLineTranslucency flag is: {0}", oWall.UseLineTranslucency);
			// LineTranslucency
			Console.WriteLine("\t\t\tThe current LineTranslucency is: {0}", oWall.LineTranslucency);
			oWall.LineTranslucency = 34.56;
			Console.WriteLine("\t\t\tThe new LineTranslucency is: {0}", oWall.LineTranslucency);
			try { oWall.LineTranslucency = 1234.56; }
			catch(Exception e) { Console.Write("\t\t\t\tExpected exception: {0}", e.Message); }
			Console.WriteLine("----- VO BORDER WALL ----- END -----");
		}
		#endregion

		#region VOVectors
		private void VOVectors( IAgVOVector oVector )
		{
			Console.WriteLine("----- VO VECTORS ----- BEGIN -----");
			if(oVector == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// ScaleRelativeToModel
			Console.WriteLine("\tThe current ScaleRelativeToModel flag is: {0}", oVector.ScaleRelativeToModel);
			try { oVector.ScaleRelativeToModel = true; }
			catch(Exception e) { Console.WriteLine("\t\tExpected exception: {0}", e.Message); }
			// VectorSizeScale
			Console.WriteLine("\tThe current Vector Size Scale is: {0}", oVector.VectorSizeScale);
			oVector.VectorSizeScale = 9.87654321;
			Console.WriteLine("\tThe new Vector Size Scale is: {0}", oVector.VectorSizeScale);
			try { oVector.VectorSizeScale = 1234.56789; }
			catch(Exception e) { Console.WriteLine("\t\tExpected exception: {0}", e.Message); }
			// AngleSizeScale
			Console.WriteLine("\tThe current Angle Size Scale is: {0}", oVector.AngleSizeScale);
			oVector.AngleSizeScale = 3.21987654;
			Console.WriteLine("\tThe new Angle Size Scale is: {0}", oVector.AngleSizeScale);
			try { oVector.AngleSizeScale = 1234.56789; }
			catch(Exception e) { Console.WriteLine("\t\tExpected exception: {0}", e.Message); }
			// RefCrdns
			IAgVORefCrdnCollection oCollection = oVector.RefCrdns;
			// AvailableCrdns
			System.Array arAvailable = oCollection.AvailableCrdns;
			Console.WriteLine("\tThe AvailableCrdns array contains {0} elements", arAvailable.GetLength(0));
			bool bNeedsToAddVector = true;
			bool bNeedsToAddAxes = true;
			bool bNeedsToAddAngle = true;
			bool bNeedsToAddPoint = true;
			bool bNeedsToAddPlane = true;
			// Count
			Console.WriteLine("\tThe current VectorCollection contains: {0} elements", oCollection.Count);
			for(int iIndex = 0; iIndex < oCollection.Count; iIndex++)
			{
				IAgVORefCrdn oElement = oCollection[iIndex];
				Console.WriteLine("\t\tElement {0}: Name = {1}, Type = {2}",
					iIndex, oElement.Name, oElement.TypeID);
				switch( oElement.TypeID )
				{
					case AGI.STKObjects.AgEGeometricElemType.eAngleElem:
					{
						bNeedsToAddAngle = false;
						break;
					}
					case AGI.STKObjects.AgEGeometricElemType.eAxesElem:
					{
						bNeedsToAddAxes = false;
						break;
					}
					case AGI.STKObjects.AgEGeometricElemType.ePlaneElem:
					{
						bNeedsToAddPlane = false;
						break;
					}
					case AGI.STKObjects.AgEGeometricElemType.ePointElem:
					{
						bNeedsToAddPoint = false;
						break;
					}
					case AGI.STKObjects.AgEGeometricElemType.eVectorElem:
					{
						bNeedsToAddVector = false;
						break;
					}
					default:
						break;
				}
			}
			// Add Angle element
			if( bNeedsToAddAngle )
			{
				for( int iIndex = 0; iIndex < arAvailable.GetLength(0); iIndex++ )
				{
					AgEGeometricElemType eType =
						(AgEGeometricElemType)arAvailable.GetValue(iIndex,1);
					if( eType == AgEGeometricElemType.eAngleElem )
					{
						IAgVORefCrdn oElement =
							oCollection.Add( eType, arAvailable.GetValue(iIndex,0).ToString() );
						Console.WriteLine("\t\tAdded element: Name = {0}, Type = {1}",
							oElement.Name, oElement.TypeID);
						break;
					}
				}
			}
			// Add Axes element
			if( bNeedsToAddAxes )
			{
				for( int iIndex = 0; iIndex < arAvailable.GetLength(0); iIndex++ )
				{
					AgEGeometricElemType eType =
						(AgEGeometricElemType)arAvailable.GetValue(iIndex,1);
					if( eType == AgEGeometricElemType.eAxesElem )
					{
						IAgVORefCrdn oElement =
							oCollection.Add( eType, arAvailable.GetValue(iIndex,0).ToString() );
						Console.WriteLine("\t\tAdded element: Name = {0}, Type = {1}",
							oElement.Name, oElement.TypeID);
						break;
					}
				}
			}
			// Add Plane element
			if( bNeedsToAddPlane )
			{
				for( int iIndex = 0; iIndex < arAvailable.GetLength(0); iIndex++ )
				{
					AgEGeometricElemType eType =
						(AgEGeometricElemType)arAvailable.GetValue(iIndex,1);
					if( eType == AgEGeometricElemType.ePlaneElem )
					{
						IAgVORefCrdn oElement =
							oCollection.Add( eType, arAvailable.GetValue(iIndex,0).ToString() );
						Console.WriteLine("\t\tAdded element: Name = {0}, Type = {1}",
							oElement.Name, oElement.TypeID);
						break;
					}
				}
			}
			// Add Point element
			if( bNeedsToAddPoint )
			{
				for( int iIndex = 0; iIndex < arAvailable.GetLength(0); iIndex++ )
				{
					AgEGeometricElemType eType =
						(AgEGeometricElemType)arAvailable.GetValue(iIndex,1);
					if( eType == AgEGeometricElemType.ePointElem )
					{
						IAgVORefCrdn oElement =
							oCollection.Add( eType, arAvailable.GetValue(iIndex,0).ToString() );
						Console.WriteLine("\t\tAdded element: Name = {0}, Type = {1}",
							oElement.Name, oElement.TypeID);
						break;
					}
				}
			}
			// Add Vector element
			if( bNeedsToAddVector )
			{
				for( int iIndex = 0; iIndex < arAvailable.GetLength(0); iIndex++ )
				{
					AgEGeometricElemType eType =
						(AgEGeometricElemType)arAvailable.GetValue(iIndex,1);
					if( eType == AgEGeometricElemType.eVectorElem )
					{
						IAgVORefCrdn oElement =
							oCollection.Add( eType, arAvailable.GetValue(iIndex,0).ToString() );
						Console.WriteLine("\t\tAdded element: Name = {0}, Type = {1}",
							oElement.Name, oElement.TypeID);
						break;
					}
				}
			}
			// Count
			Console.WriteLine("\tThe new VectorCollection contains: {0} elements", oCollection.Count);
			foreach(IAgVORefCrdn oElement in oCollection)
			{
				Console.WriteLine("\t\tElement: Name = {0}, Type = {1}",
					oElement.Name, oElement.TypeID);
			}
			for(int iIndex = 0; iIndex < oCollection.Count; iIndex++)
			{
				// Item
				IAgVORefCrdn oElement = oCollection[iIndex];
				Console.WriteLine("\t-> {0}", oElement.Name);
				// Visible (false)
				Console.WriteLine("\t\tThe current Visible flag is: {0}", oElement.Visible);
				oElement.Visible = false;
				Console.WriteLine("\t\tThe new Visible flag is: {0}", oElement.Visible);
				// Color
				try { oElement.Color = Color.FromArgb(0x34); }
				catch(Exception e) { Console.WriteLine("\t\t\tExpected exception: {0}", e.Message); }
				// LabelVisible
				try { oElement.LabelVisible = true; }
				catch(Exception e) { Console.WriteLine("\t\t\tExpected exception: {0}", e.Message); }
				// Visible (true)
				oElement.Visible = true;
				Console.WriteLine("\t\tThe new Visible flag is: {0}", oElement.Visible);
				// Color
				Console.WriteLine("\t\t\tThe current Color is: {0}", oElement.Color);
				oElement.Color = Color.FromArgb(0x00987321);
				Console.WriteLine("\t\t\tThe new Color is: {0}", oElement.Color);
				// LabelVisible
				Console.WriteLine("\t\t\tThe current LabelVisible flag is: {0}", oElement.LabelVisible);
				oElement.LabelVisible = true;
				Console.WriteLine("\t\t\tThe new LabelVisible flag is: {0}", oElement.LabelVisible);
			}
			// Remove
			Console.WriteLine("\tBefore Remove(0) the Vector Collection contains: {0} elements", oCollection.Count);
			oCollection.Remove(0);
			Console.WriteLine("\tAfter Remove(0) the Vector Collection contains: {0} elements", oCollection.Count);
			// RemoveByName
			Console.WriteLine("\tBefore RemoveByName() the Vector Collection contains: {0} elements", oCollection.Count);
			oCollection.RemoveByName( oCollection[0].TypeID, oCollection[0].Name );
			Console.WriteLine("\tAfter RemoveByName() the Vector Collection contains: {0} elements", oCollection.Count);
			// RemoveAll
			Console.WriteLine("\tBefore RemoveAll() the Vector Collection contains: {0} elements", oCollection.Count);
			oCollection.RemoveAll();
			Console.WriteLine("\tAfter RemoveAll() the Vector Collection contains: {0} elements", oCollection.Count);
			Console.WriteLine("----- VO VECTORS ----- END -----");
		}
		#endregion

		#region AccessConstraints
		private void AccessConstraints( IAgAccessConstraintCollection oCollection )
		{
			Console.WriteLine("----- ACCESS CONSTRAINTS ----- BEGIN -----");
			if(oCollection == null)
			{
				Console.WriteLine("The input parameter is invalid!");
				return;
			}
			// AvailableConstraints
			System.Array arAvailable = oCollection.AvailableConstraints();
			Console.WriteLine("\tThis object has got {0} available constraints.", arAvailable.GetLength(0));
			for( int iIndex = 0; iIndex < arAvailable.GetLength(0); iIndex++ )
			{
				Console.WriteLine("\t\tConstraint {0}: {1} ({2})", iIndex, arAvailable.GetValue(iIndex, 0),
					(AgEAccessConstraints)arAvailable.GetValue(iIndex, 1));
			}
			// Count
			Console.WriteLine("\tThe current AccessConstraints collection contains: {0} constraints", oCollection.Count);
			for( int iIndex = 0; iIndex < oCollection.Count; iIndex++ )
			{
				Console.WriteLine("\t\tConstraint {0}:  ExclIntvl = {1}",
					oCollection[iIndex].ConstraintName, oCollection[iIndex].ExclIntvl);
			}
			for( int iIndex = 0; iIndex < arAvailable.GetLength(0); iIndex++ )
			{
				AgEAccessConstraints eType = (AgEAccessConstraints)arAvailable.GetValue(iIndex, 1);
				// IsConstraintSupported
				if( !oCollection.IsConstraintSupported(eType) )
				{
					if( eType == AgEAccessConstraints.eCstrNone )
					{
						continue;
					}

					Console.WriteLine("\t\tThe {0} constraint should be supported.",
						(AgEAccessConstraints)arAvailable.GetValue(iIndex, 1));
				}

				// test constraint
				ConstraintTest( oCollection, eType );
				if( eType == AgEAccessConstraints.eCstrExclusionZone )
				{
					// IsConstraintActive
					if( !oCollection.IsConstraintActive( eType ) )
					{
						Console.WriteLine("\t\tThe {0} constraint is not active", eType);
					}
				}
			}
			Console.WriteLine("\tThe new Collection contains: {0} constraints", oCollection.Count);
			for( int iIndex = 0; iIndex < arAvailable.GetLength(0); iIndex++ )
			{
				AgEAccessConstraints eType = (AgEAccessConstraints)arAvailable.GetValue(iIndex, 1);
				// IsConstraintActive
				if( !oCollection.IsConstraintActive(eType) )
				{
					Console.WriteLine("\t\tThe {0} constraint is not active.", eType);
				}
				else
				{
					// RemoveConstraint
					oCollection.RemoveConstraint(eType);
					Console.WriteLine("\t\tThe {0} constraint was removed from collection.", eType);
				}
			}
			Console.WriteLine("\tThe new Collection contains: {0} constraints", oCollection.Count);

		Console.WriteLine("----- ACCESS CONSTRAINTS ----- END -----");
		}
		#endregion

		#region ConstraintTest
		private void ConstraintTest( IAgAccessConstraintCollection oCollection, AgEAccessConstraints eType )
		{
			Console.WriteLine("The {0} constraint test:", eType);
			IAgAccessConstraint oConstraint;
			// IsConstraintActive
			if( !oCollection.IsConstraintActive(eType) )
			{
				// AddConstraint
				oConstraint = oCollection.AddConstraint(eType);
				Console.WriteLine("\tThe {0} constraint was added into collection",
					oConstraint.ConstraintName);
			}
			else
			{
				Console.WriteLine("\tThe {0} constraint is already active", eType);
			}
			// GetActiveConstraint
			oConstraint = oCollection.GetActiveConstraint( eType );

			// Base interface properties test
			Console.WriteLine("\t\tExclIntvl = {0} (current)", oConstraint.ExclIntvl);
			oConstraint.ExclIntvl = !oConstraint.ExclIntvl;
			Console.WriteLine("\t\tExclIntvl = {0} (new)", oConstraint.ExclIntvl);
			oConstraint.ExclIntvl = !oConstraint.ExclIntvl;
			Console.WriteLine("\t\tExclIntvl = {0} (restored)", oConstraint.ExclIntvl);
		}
		#endregion

	}
}

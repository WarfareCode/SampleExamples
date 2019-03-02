//-------------------------------------------------------------------------
//
//  This is part of the STK 8 Object Model Examples
//  Copyright (C) 2006 Analytical Graphics, Inc.
//
//  This source code is intended as a reference to users of the
//	STK 8 Object Model.
//
//  File: Class1.cs
//  Events
//
//
//  The features used in this example show how to add various access 
//  constraints to facilities, sensors and satellites. 
//
//--------------------------------------------------------------------------

using System;
using System.Reflection;
using System.Runtime.InteropServices;
using AGI.STKObjects;
using AGI.Ui.Application;
namespace AccessConstraints
{
	/// <summary>
	/// This example shows how access constraints are used and tries to explain the 
	/// intricacies involved.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Class1 class1 = new Class1();
			class1.Run();
		}

		#region Class Members
        private AgUiApplication     m_oSTK;
		private IAgStkObjectRoot	m_oApplication;
		#endregion

		#region Constructor
		public Class1()
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

		public void Run()
		{
			this.m_oApplication.CloseScenario();
			this.m_oApplication.NewScenario("AccessConstraints");

			AGI.STKUtil.IAgUnitPrefsDimCollection dimensions = this.m_oApplication.UnitPreferences;
			dimensions.ResetUnits();
			dimensions.SetCurrentUnit("DateFormat", "UTCG");
			IAgSatellite sat1 = (IAgSatellite)this.m_oApplication.CurrentScenario.Children.New(AgESTKObjectType.eSatellite, "Satellite1");
			sat1.SetPropagatorType(AgEVePropagatorType.ePropagatorTwoBody);
			IAgVePropagatorTwoBody satProp = sat1.Propagator as IAgVePropagatorTwoBody;
			satProp.Propagate();

			IAgFacility fac1 = (IAgFacility)this.m_oApplication.CurrentScenario.Children.New(AgESTKObjectType.eFacility, "Facility1");
			IAgSensor sensor1 = (IAgSensor)this.m_oApplication.CurrentScenario.Children["Facility1"].Children.New(AgESTKObjectType.eSensor, "Sensor1");
			//First thing is to get the Access Constraint collection for the satellite.
			IAgAccessConstraintCollection constraintCollection = sat1.AccessConstraints;
			
			//A good number of access constraints use the IAgAccessCnstrMinMax interface. To set the values properly
			//you first need to Enable the respective property.
			IAgAccessCnstrMinMax minmax = (IAgAccessCnstrMinMax)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrAzimuthAngle);
			minmax.EnableMin = true;
			minmax.EnableMax = true;
			minmax.Min = 20;
			minmax.Max = 180;


			//Some access constraints like the Line Of Site use the base interface IAgAccessConstraint.
			IAgAccessConstraint generic = null;
			if(constraintCollection.IsConstraintActive(AgEAccessConstraints.eCstrLineOfSight))
			{
				generic = (IAgAccessConstraint)constraintCollection.GetActiveConstraint(AgEAccessConstraints.eCstrLineOfSight);
			}
			else
			{
				generic = (IAgAccessConstraint)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrLineOfSight);
			}

			//Getting an Angle constraint
			IAgAccessCnstrAngle angle = (IAgAccessCnstrAngle)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrSunSpecularExclusion);
			angle.Angle = 12;

			//Getting a background constraint
			IAgAccessCnstrBackground background = (IAgAccessCnstrBackground)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrBackground);
			background.Background = AgECnstrBackground.eBackgroundGround;
			

			//Getting a groundtrack constraint
			IAgAccessCnstrGroundTrack groundtrack = (IAgAccessCnstrGroundTrack)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrGroundTrack);
			groundtrack.Direction = AgECnstrGroundTrack.eDirectionAscending;

			//Adding an inclusion zone.
			IAgAccessCnstrZone inclusionzone = (IAgAccessCnstrZone)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrInclusionZone);
			inclusionzone.MinLon = 1;
			inclusionzone.MinLat = 1;
			inclusionzone.MaxLat = 15;
			inclusionzone.MaxLon = 15;


			//Adding a Condition constraint.
			IAgAccessCnstrCondition condition = (IAgAccessCnstrCondition)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrLighting);
			condition.Condition = AgECnstrLighting.ePenumbra;

			//A CrdnCn Constraint.
			IAgAccessCnstrCrdnCn crdn = (IAgAccessCnstrCrdnCn)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrCrdnAngle);
			crdn.EnableMin = true;
			crdn.EnableMax = true;
			crdn.Min = 1;
			crdn.Max = 75;
			crdn.Reference = "Satellite/Satellite1 VelocityAzimuth Angle";


			//If you call the AddConstraint method with the eCstrExclusionZone enum, an IAgAccessCnstrZone interface will be returned.
			IAgAccessCnstrZone exclusion = (IAgAccessCnstrZone)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrExclusionZone);
			exclusion.MinLat = 2;
			exclusion.MinLon = 2;
			exclusion.MaxLon = 14;
			exclusion.MaxLat = 14;

			
			IAgAccessCnstrZone exclusion2 = (IAgAccessCnstrZone)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrExclusionZone);
			exclusion2.MinLat = 2;
			exclusion2.MinLon = 2;
			exclusion2.MaxLon = 14;
			exclusion2.MaxLat = 14;

            //If the GetActiveConstraint method is called using the eCstrExclusionZone enum an IAgAccessCnstrExclusionZonesCollection
			//interface is returned. This contains all the exclusion zones that were added to the access constrain collection
			//Once you obtain the IAgAccessCnstrExclusionZonesCollection you can iterate through the collection and view or modify
			//each exclusion zone.
			IAgAccessCnstrExclZonesCollection exclusions = (IAgAccessCnstrExclZonesCollection)constraintCollection.GetActiveConstraint(AgEAccessConstraints.eCstrExclusionZone);
			Console.WriteLine(exclusions.Count);
			for(int i = 0; i < exclusions.Count; i++)
			{
				object minlat, minlon, maxlat, maxlon;
				exclusions.GetExclZone(i, out minlat, out minlon, out maxlat, out maxlon);
				Console.WriteLine("minimum lat = {0}\nminimum lon = {1}\nmaximum lat = {2}\nmaximum lon = {3}", minlat, minlon, maxlat, maxlon);
				exclusions.ChangeExclZone(i, 3, 3, 12, 12);
			}


			//Adds an interval access constraint with a default interval.
			IAgAccessCnstrIntervals intervals = (IAgAccessCnstrIntervals)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrIntervals);
			intervals.ActionType = AgEActionType.eActionInclude;
			Console.WriteLine(intervals.Intervals.Count);
			intervals.Intervals.RemoveAll();
			intervals.Intervals.Add("1 Jul 2005 12:00:00.000", "1 Jul 2005 12:10:00.000");

			//
			Console.WriteLine("The size of collection is {0}", constraintCollection.Count);
			IAgAccessCnstrThirdBody thirdbody = (IAgAccessCnstrThirdBody)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrThirdBodyObstruction);
			Console.WriteLine("After count {0}", constraintCollection.Count);
			System.Array available = thirdbody.AvailableObstructions;
			for(int i = 0; i < available.Length; i++)
			{
				Console.WriteLine(available.GetValue(i));
			}

			thirdbody.AddObstruction("Sun");
			Console.WriteLine("After add obstruction {0}", constraintCollection.Count);


			//To add an object exclusion angle access constraint to the collection, you first need to call
			//the AddConstraint method with the eCstrObjExclusionAngle enum. This doesn't add it to the accessconstraint collection 
			//just yet. For it to be added to the collection, you need to add an exclusion object first. Once an exclusion object is
			//added the access constraint collection size is increased by one. 
			Console.WriteLine(constraintCollection.Count);
			IAgAccessCnstrObjExAngle exangle = (IAgAccessCnstrObjExAngle)constraintCollection.AddConstraint(AgEAccessConstraints.eCstrObjectExclusionAngle);
			Console.WriteLine(constraintCollection.Count);

			exangle.AddExclusionObject((string)exangle.AvailableObjects.GetValue(0));

			//The exclusion angle is shared for all objects.
			exangle.ExclusionAngle = 11;
			Console.WriteLine(constraintCollection.Count);
			
			//By adding another exclusion object the access constraint collection remains at the same size.
			exangle.AddExclusionObject((string)exangle.AvailableObjects.GetValue(1));
			Console.WriteLine(constraintCollection.Count);

			for(int i = 0; i < exangle.AssignedObjects.Length; i++)
			{
				Console.WriteLine(exangle.AssignedObjects.GetValue(i));
			}

			Console.Write("Press Enter to exit:");
			Console.ReadLine();
		}
	}
}

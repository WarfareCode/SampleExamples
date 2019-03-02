//=====================================================
//  Copyright 2007, Analytical Graphics, Inc.          
//=====================================================

//================================
// MATLAB specific variables
//===============================

var m_mFilename = "rangeExample";
var m_MatlabApp = null;

// NOTE: There are special things which must be done to 
// efficiently use MATLAB in an Access Constraint plugin,
// because the Constraint may go through the Init()/Free()
// lifecycle many times. Currently, our experience is that
// MATLAB will be closed once the plugin is freed, UNLESS
// the user Connects to MATLAB from STK. To do so, go to
// Edit->Preferences, select MATLAB and hit the Open MATLAB button. By
// doing this, STK will open MATLAB and keep a reference
// to it, preventing it from being closed by the plugin being
// freed.

//==========================================
// Reference Frames Enumeration
//==========================================
var eInertial 		= 0;
var eFixed 			= 1;
var eLVLH 			= 2;
var eNTC 			= 3;

//==========================================
// Time Scale Enumeration
//==========================================
var eUTC 			= 0;
var eTAI 			= 1;
var eTDT 			= 2;
var eUT1 			= 3;
var eSTKEpochSec 	= 4;
var eTDB 			= 5;
var eGPS 			= 6;

//==========================================
// Log Msg Type Enumeration
//==========================================
var eLogMsgDebug	 	= 0;
var eLogMsgInfo 		= 1;
var eLogMsgForceInfo 	= 2;
var eLogMsgWarning 		= 3;
var eLogMsgAlarm 		= 4;

//AgEAccessConstraintDependencyFlags: Enumeration of STK Object Types
var eAircraft		= 1;
var eFacility		= 8;
var eGroundVehicle	= 9;
var eLaunchVehicle	= 10;
var eMissile		= 13;
var ePlanet			= 15;
var eRadar			= 16;
var eReceiver		= 17;
var eSatellite		= 18;
var eSensor			= 20;
var eShip			= 21;
var eStar			= 22;
var eSubmarine		= 30;
var eTarget			= 23;
var eTransmitter	= 24;

//AgEAccessConstraintDependencyFlags: Enumeration of Access Constraint Dependency Flags
var eDependencyRelativePosVel	= 1;
var eDependencyRelativeAcc		= 2;
var eDependencyPosVel			= 4;
var eDependencyAcc				= 8;
var eDependencyAttitude			= 16;
var eDependencyRelSun			= 32;
var eDependencyNone				= 4096;

//AgEAccessLightTimeDelayFrame: Enumeration of frames used in Access to compute light time delay.
var eLightTimeDelayFrameCBI		= 1;
var eLightTimeDelayFrameSSBary	= 2;

// AgEApparentPositionSignalSense: Enumeration of the signal sense of the apparent position computation.
var eTransmitSignal	= 1;
var eReceiveSignal	= 2;

//AgEApparentPositionAberrationType: Enumeration of methods of incorporating aberration into the apparent position computation.
var eAberrationTotal	= 1;
var eAberrationAnnual	= 2;
var eAberrationNone		= 3;

// AgEAccessApparentPositionType: Enumeration of types of apparent positions computed by Access.
var eLightPathApparentPosition	= 1;
var eRefractedApparentPosition	= 2;
var eProperApparentPosition		= 3;

// AgEAltitudeReference: Enumeration of references used for reporting altitude.
var eEllispoidReference	= 1;
var eMSLReference		= 2;
var eTerrainReference	= 3;

//==========================================
// Declare Global Variables
//==========================================
var m_Site			= null;
var m_DisplayName	= "Matlab.RangeExample";
var m_StkRootObject = null;

function Message(logMsgType, msg)
{
	if(m_Site != null)
	{
		m_Site.Message( logMsgType, msg );
	}
}

function GetDisplayName()
{
	return m_DisplayName;
}

function Register( Result )
{
	Result.BaseObjectType = eAircraft;
	Result.BaseDependency = eDependencyRelativePosVel;
	Result.Dimension = "Distance";	
	Result.MinValue = 0.0;

	Result.TargetDependency = eDependencyRelativePosVel;
	Result.AddTarget(eFacility);
	Result.AddTarget(eGroundVehicle);
	Result.AddTarget(eSatellite);		
	Result.Register();

	Result.Message(eLogMsgInfo, m_DisplayName+": Register(Aircraft to Facility/GroundVehicle/Satellite)");

	Result.BaseObjectType = eFacility;
	Result.ClearTargets();
	Result.AddTarget(eAircraft);
	Result.AddTarget(eSatellite);	
	Result.Register();

	Result.Message(eLogMsgInfo, m_DisplayName+": Register(Facility to Aircraft/Satellite)");

	Result.BaseObjectType = eGroundVehicle;
	Result.Register();

	Result.Message(eLogMsgInfo, m_DisplayName+": Register(GroundVehicle to Aircraft/Satellite)");

	Result.BaseObjectType = eSatellite;
	Result.ClearTargets();
	Result.AddTarget(eAircraft);
	Result.AddTarget(eFacility);
	Result.AddTarget(eGroundVehicle);	
	Result.Register();

	Result.Message(eLogMsgInfo, m_DisplayName+": Register(Satellite to Aircraft/Facility/GroundVehicle)");
}

function Init( AgUtPluginSite )
{
	m_Site = AgUtPluginSite;
	
	Message( eLogMsgInfo, m_DisplayName+": Init()" );
	
	if( m_Site != null )
	{
		// Demonstrate getting ObjectModel handle
	
		//----------------------------------------------------
		// Get a pointer to the STK Object Model root object
		//----------------------------------------------------
					
		m_StkRootObject = m_Site.StkRootObject;
	}

	// Get handle to Matlab
	try
	{
		m_MatlabApp = GetObject("","Matlab.Application");
	}
	catch (e)
	{
		m_MatlabApp = new ActiveXObject("Matlab.Application");
	}
	
	if(m_MatlabApp == null)
	{
		Message( eLogMsgAlarm, m_DisplayName+": Init(): Cannot get handle to Matlab. Aborting..." );
		return false;
	}

    
    return true;
} 

function PreCompute( Result )
{
	Message( eLogMsgInfo, m_DisplayName+": PreCompute()" );
	
	// Demonstrate using ObjectModel handle

	if(m_StkRootObject != null)
	{
		var scenObj = m_StkRootObject.CurrentScenario;

		if(scenObj != null)
		{
			var currentScenario = scenObj.InstanceName;

			Message(eLogMsgInfo, "Current Scenario is "+ currentScenario);
		}
	}
	
	return true;
}

function Evaluate( Result, fromObject, toObject )
{
	if(Result != null)
	{
		var vbPosVelArray = fromObject.RelativePosition_Array( eLightPathApparentPosition, eFixed );
		var jsPosVelArray = vbPosVelArray.toArray();
				
		//Set variables into the base workspace of Matlab
		m_MatlabApp.PutWorkspaceData("relPosX", "base", jsPosVelArray[0]);
		m_MatlabApp.PutWorkspaceData("relPosY", "base", jsPosVelArray[1]);
		m_MatlabApp.PutWorkspaceData("relPosZ", "base", jsPosVelArray[2]);

		// Execute the mfile on those variables
		var argArray = new Array(3);
		argArray[0] = "relPosX";
		argArray[1] = "relPosY";
		argArray[2] = "relPosZ";
				
		var vbArray = ExecuteMatlabFunction(m_MatlabApp, m_mFilename, 1, argArray);

		//var  outArg = new Object[1];
		//m_MatlabApp.Feval(m_mFilename, 1, outArg, "relPosX=", "relPosY=", "relPosZ=");

		// Get the computed variable
		
		var range = -1.0;
		
		if(vbArray != null)
		{
			var jsOutArray = vbArray.toArray();
			range = jsOutArray[0];
		}
		else
		{
			Message(eLogMsgAlarm,  m_DisplayName+": Evaluate(): Matlab did not return data correctly!");
			Result.Value = 0.0;
			return false;
		}
				
		Result.Value = range;
	}

	return true;
}

function PostCompute( Result )
{
	Message( eLogMsgInfo, m_DisplayName+": PostCompute()" );
	
	return true;
}

function Free()
{
	Message( eLogMsgInfo, m_DisplayName+": Free()" );
	
	m_StkRootObject = null;
	m_Site = null;
	m_MatlabApp = null;
}

//=====================================================
//  Copyright 2007, Analytical Graphics, Inc.          
//=====================================================

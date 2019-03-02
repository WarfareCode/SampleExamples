//=====================================================
//  Copyright 2018-2018, Analytical Graphics, Inc.          
//=====================================================

//=====================================================
// This example models an exponential atmospheric
// density consistent with STK's exponential models.
//=====================================================

//==========================================
// Log Msg Type Enumeration
//==========================================
var eLogMsgDebug	 	= 0;
var eLogMsgInfo 		= 1;
var eLogMsgForceInfo 	= 2;
var eLogMsgWarning 		= 3;
var eLogMsgAlarm 		= 4;

//==========================================
// AgEAttrAddFlags Enumeration
//==========================================
var eFlagNone			= 0;
var eFlagTransparent	= 2;
var eFlagHidden			= 4;
var eFlagTransient		= 8;  
var eFlagReadOnly		= 16;
var eFlagFixed = 32;

//==========================================
// Declare Global Variables
//==========================================
var m_AgUtPluginSite		= null;
var m_AgAttrScope			= null;

var m_MsgCntr				= -1;
var m_Enabled				= true;
var m_DebugMode				= false;
var m_MsgInterval			= 500;
var m_refDen				= 1.217;
var m_refAlt				= 0.0;
var m_scaleAlt				= 8.5 * 1000.0;
var m_Density				= -1;
var m_computesTemp			= false;
var m_computesPressure		= false;
var m_userIndex				= 0;

//==========================================
// Message handling
//==========================================
function Message ( severity, msg )
{
	if ( m_AgUtPluginSite )
	{
		m_AgUtPluginSite.Message( severity, msg );
	}
}

function DebugMsg ( msg )
{
	if ( m_DebugMode )
	{
		if ( m_MsgCntr % m_MsgInterval == 0 )
		{
			Message( eLogMsgDebug, msg );
		}
	}
}

//========================
// GetPluginConfig method
//========================
function GetPluginConfig( AgAttrBuilder )
{
	if( m_AgAttrScope == null )
	{
		m_AgAttrScope = AgAttrBuilder.NewScope();

		// This plugin specific attrs
		AgAttrBuilder.AddDoubleDispatchProperty( m_AgAttrScope, "RefDensity", "Reference Density", "RefDensity", 0);
		AgAttrBuilder.AddDoubleDispatchProperty( m_AgAttrScope, "RefAltitude", "Reference Altitude", "RefAltitude", 0);
		AgAttrBuilder.AddDoubleDispatchProperty( m_AgAttrScope, "ScaleAltitude", "Scale Altitude", "ScaleAltitude", 0);

		// General plugin attrs
		AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "PluginEnabled", "If the plugin is enabled or has experienced an error", "Enabled", eFlagNone);
		AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "DebugMode", "Turn debug messages on or off", "DebugMode", eFlagNone);

		// Messaging attr
		AgAttrBuilder.AddIntDispatchProperty( m_AgAttrScope, "MessageInterval", "The interval at which to send messages during propagation in Debug mode", "MsgInterval", eFlagNone);
	}

	return m_AgAttrScope;
}  

//===========================
// VerifyPluginConfig method
//===========================
function VerifyPluginConfig( AgUtPluginConfigVerifyResult )
{
    var Result = true;
    var Message = "Ok";

	AgUtPluginConfigVerifyResult.Result  = Result;
	AgUtPluginConfigVerifyResult.Message = Message;
}

//======================
// Register Method
//======================
function Register( AgAsDensityModelResultRegister ) // find out what 'input' is
{
	if( AgAsDensityModelResultRegister )
	{
		if( m_DebugMode == true )
		{
			AgAsDensityModelResultRegister.Message( eLogMsgInfo, "Register() called" );
		}
	}
} 

//======================
// Init Method
//======================
function Init( AgUtPluginSite )
{
	m_AgUtPluginSite = AgUtPluginSite;
	
	if( m_AgUtPluginSite )
	{
		if ( m_DebugMode == true )
		{
			if ( m_Enabled == true )
			{
				Message( eLogMsgInfo, "Init(): Enabled" );
			}
			else
			{
				Message( eLogMsgInfo, "Init(): Disabled because Enabled flag is false" );
			}
		}
		else if(m_Enabled == false)
		{
			Message( eLogMsgAlarm, "Init(): Disabled because Enabled flag is false" );
		}
	}
   
    return m_Enabled;
}

//======================
// Evaluate Method
//======================
function Evaluate( AgAsDensityModelResultEval )
{
	m_MsgCntr++;

	if(m_Enabled == true && AgAsDensityModelResultEval )
	{
		m_Enabled = setDensity( AgAsDensityModelResultEval );
	}
	return m_Enabled;
}

//======================
// Local setDensity Method
//======================
function setDensity( AgAsDensityModelResultEval )
{
	var enabled = false;
	var altitude = AgAsDensityModelResultEval.Altitude;
	if( altitude )
	{
		m_Density = m_refDen * Math.exp((m_refAlt - altitude) / m_scaleAlt);
		if( m_Density )
		{
			AgAsDensityModelResultEval.SetDensity( m_Density );
			enabled = true;
		}
	}
	
	return enabled;
}

//===========================================================
// Free Method
//===========================================================
function Free()
{
	if( m_AgUtPluginSite != null )
	{	
		m_AgUtPluginSite = null;
	}
	
	return true;
}

// ============================================================
//  Computes Temperature property
// ============================================================
function ComputesTemperature()
{
	return m_computesTemp;
}

// ============================================================
//  Computes Pressure property
// ============================================================
function ComputesPressure()
{
	return m_computesPressure;
}

// ============================================================
//  New methods
// ============================================================
function CentralBody()
{
	return "Earth";
}
function UsesAugmentedSpaceWeather()
{
	return false;
}
function AtmFluxLags()
{
	return false;
}
function AugmentedAtmFluxLags()
{
	return false;
}


// ============================================================
//  Enabled property
// ============================================================
function GetEnabled()
{
	return m_Enabled;
}

function SetEnabled( input )
{
	m_Enabled = input;
}

// ======================================================
//  MsgStatus property
// ======================================================
function GetDebugMode()
{
	return m_DebugMode;
}

function SetDebugMode( input )
{
    m_DebugMode = input;
}

// =======================================================
//  EvalMsgInterval property
// =======================================================
function GetMsgInterval()
{
	return m_MsgInterval;
}

function SetMsgInterval( input )
{
	m_MsgInterval = input;
}

// =======================================================
//  RefDen property
// =======================================================
function GetRefDensity()
{
	return m_refDen;
}

function SetRefDensity( input )
{
	m_refDen = input;
}

// =======================================================
//  RefAlt property
// =======================================================
function GetRefAltitude()
{
	return m_refAlt;
}

function SetRefAltitude( input )
{
	m_refAlt = input;
}

// =======================================================
//  ScaleAlt property
// =======================================================
function GetScaleAltitude()
{
	return m_scaleAlt;
}

function SetScaleAltitude( input )
{
	m_scaleAlt = input;
}

//=====================================================
//  Copyright 2018-2018, Analytical Graphics, Inc.
//=====================================================

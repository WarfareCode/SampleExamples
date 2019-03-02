//=====================================================
//  Copyright 2009, Analytical Graphics, Inc.          
//=====================================================

//==========================================
// Reference Frames Enumeration
//==========================================
var eInertial 		= 0;
var eFixed 			= 1;
var eLVLH 			= 2;
var eNTC 			= 3;

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
var eFlagFixed			= 32;

//==========================================
// EventType Enumeration
//==========================================
var eEventTypesPrePropagate = 0;
var eEventTypesPreNextStep = 1;
var eEventTypesEvaluate = 2;
var eEventTypesPostPropagate = 3;

//==========================================
// State Values Enumeration
//==========================================
var AgEAsEOMFuncPluginInputStateValuesPosX = 0;
var AgEAsEOMFuncPluginInputStateValuesPosY = 1;
var AgEAsEOMFuncPluginInputStateValuesPosZ = 2;
var AgEAsEOMFuncPluginInputStateValuesVelX = 3;
var AgEAsEOMFuncPluginInputStateValuesVelY = 4;
var AgEAsEOMFuncPluginInputStateValuesVelZ = 5;
var AgEAsEOMFuncPluginInputStateValuesPosCBFX = 6;
var AgEAsEOMFuncPluginInputStateValuesPosCBFY = 7;
var AgEAsEOMFuncPluginInputStateValuesPosCBFZ = 8;
var AgEAsEOMFuncPluginInputStateValuesVelCBFX = 9;
var AgEAsEOMFuncPluginInputStateValuesVelCBFY = 10;
var AgEAsEOMFuncPluginInputStateValuesVelCBFZ = 11;
var AgEAsEOMFuncPluginInputStateValuesCBIVelInCBFX = 12;
var AgEAsEOMFuncPluginInputStateValuesCBIVelInCBFY = 13;
var AgEAsEOMFuncPluginInputStateValuesCBIVelInCBFZ = 14;
var AgEAsEOMFuncPluginInputStateValuesQuat1 = 15;
var AgEAsEOMFuncPluginInputStateValuesQuat2 = 16;
var AgEAsEOMFuncPluginInputStateValuesQuat3 = 17;
var AgEAsEOMFuncPluginInputStateValuesQuat4 = 18;
var AgEAsEOMFuncPluginInputStateValuesCBIToCBF00 = 19;
var AgEAsEOMFuncPluginInputStateValuesCBIToCBF01 = 20;
var AgEAsEOMFuncPluginInputStateValuesCBIToCBF02 = 21;
var AgEAsEOMFuncPluginInputStateValuesCBIToCBF10 = 22;
var AgEAsEOMFuncPluginInputStateValuesCBIToCBF11 = 23;
var AgEAsEOMFuncPluginInputStateValuesCBIToCBF12 = 24;
var AgEAsEOMFuncPluginInputStateValuesCBIToCBF20 = 25;
var AgEAsEOMFuncPluginInputStateValuesCBIToCBF21 = 26;
var AgEAsEOMFuncPluginInputStateValuesCBIToCBF22 = 27;
var AgEAsEOMFuncPluginInputStateValuesAngVelCBFX = 28;
var AgEAsEOMFuncPluginInputStateValuesAngVelCBFY = 29;
var AgEAsEOMFuncPluginInputStateValuesAngVelCBFZ = 30;
var AgEAsEOMFuncPluginInputStateValuesAltitude = 31;
var AgEAsEOMFuncPluginInputStateValuesLatitude = 32;
var AgEAsEOMFuncPluginInputStateValuesLongitude = 33;
var AgEAsEOMFuncPluginInputStateValuesTotalMass = 34;
var AgEAsEOMFuncPluginInputStateValuesDryMass = 35;
var AgEAsEOMFuncPluginInputStateValuesFuelMass = 36;
var AgEAsEOMFuncPluginInputStateValuesCd = 37;
var AgEAsEOMFuncPluginInputStateValuesDragArea = 38;
var AgEAsEOMFuncPluginInputStateValuesAtmosphericDensity = 39;
var AgEAsEOMFuncPluginInputStateValuesAtmosphericAltitude = 40;
var AgEAsEOMFuncPluginInputStateValuesCr = 41;
var AgEAsEOMFuncPluginInputStateValuesSRPArea = 42;
var AgEAsEOMFuncPluginInputStateValuesKr1 = 43;
var AgEAsEOMFuncPluginInputStateValuesKr2 = 44;
var AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFX = 45;
var AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFY = 46;
var AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFZ = 47;
var AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFX = 48;
var AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFY = 49;
var AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFZ = 50;
var AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosX = 51;
var AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosY = 52;
var AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosZ = 53;
var AgEAsEOMFuncPluginInputStateValuesApparentSunPosCBFX = 54;
var AgEAsEOMFuncPluginInputStateValuesApparentSunPosCBFY = 55;
var AgEAsEOMFuncPluginInputStateValuesApparentSunPosCBFZ = 56;
var AgEAsEOMFuncPluginInputStateValuesApparentSatPosCBFX = 57;
var AgEAsEOMFuncPluginInputStateValuesApparentSatPosCBFY = 58;
var AgEAsEOMFuncPluginInputStateValuesApparentSatPosCBFZ = 59;
var AgEAsEOMFuncPluginInputStateValuesApparentSatToSunCBIPosX = 60;
var AgEAsEOMFuncPluginInputStateValuesApparentSatToSunCBIPosY = 61;
var AgEAsEOMFuncPluginInputStateValuesApparentSatToSunCBIPosZ = 62;
var AgEAsEOMFuncPluginInputStateValuesTrueSunPosCBFX = 63;
var AgEAsEOMFuncPluginInputStateValuesTrueSunPosCBFY = 64;
var AgEAsEOMFuncPluginInputStateValuesTrueSunPosCBFZ = 65;
var AgEAsEOMFuncPluginInputStateValuesTrueSatPosCBFX = 66;
var AgEAsEOMFuncPluginInputStateValuesTrueSatPosCBFY = 67;
var AgEAsEOMFuncPluginInputStateValuesTrueSatPosCBFZ = 68;
var AgEAsEOMFuncPluginInputStateValuesTrueSatToSunCBIPosX = 69;
var AgEAsEOMFuncPluginInputStateValuesTrueSatToSunCBIPosY = 70;
var AgEAsEOMFuncPluginInputStateValuesTrueSatToSunCBIPosZ = 71;
var AgEAsEOMFuncPluginInputStateValuesSolarIntensity = 72;
var AgEAsEOMFuncPluginInputStateValuesRadPressureCoefficient = 73;
var AgEAsEOMFuncPluginInputStateValuesRadPressureArea = 74;
var AgEAsEOMFuncPluginInputStateValuesMassFlowRate = 75;
var AgEAsEOMFuncPluginInputStateValuesTankPressure = 76;
var AgEAsEOMFuncPluginInputStateValuesTankTemperature = 77;
var AgEAsEOMFuncPluginInputStateValuesFuelDensity = 78;
var AgEAsEOMFuncPluginInputStateValuesThrustX = 79;
var AgEAsEOMFuncPluginInputStateValuesThrustY = 80;
var AgEAsEOMFuncPluginInputStateValuesThrustZ = 81;
var AgEAsEOMFuncPluginInputStateValuesDeltaV = 82;
var AgEAsEOMFuncPluginInputStateValuesGravityAccelX = 83;
var AgEAsEOMFuncPluginInputStateValuesGravityAccelY = 84;
var AgEAsEOMFuncPluginInputStateValuesGravityAccelZ = 85;
var AgEAsEOMFuncPluginInputStateValuesTwoBodyAccelX = 86;
var AgEAsEOMFuncPluginInputStateValuesTwoBodyAccelY = 87;
var AgEAsEOMFuncPluginInputStateValuesTwoBodyAccelZ = 88;
var AgEAsEOMFuncPluginInputStateValuesGravityPertAccelX = 89;
var AgEAsEOMFuncPluginInputStateValuesGravityPertAccelY = 90;
var AgEAsEOMFuncPluginInputStateValuesGravityPertAccelZ = 91;
var AgEAsEOMFuncPluginInputStateValuesSolidTidesAccelX = 92;
var AgEAsEOMFuncPluginInputStateValuesSolidTidesAccelY = 93;
var AgEAsEOMFuncPluginInputStateValuesSolidTidesAccelZ = 94;
var AgEAsEOMFuncPluginInputStateValuesOceanTidesAccelX = 95;
var AgEAsEOMFuncPluginInputStateValuesOceanTidesAccelY = 96;
var AgEAsEOMFuncPluginInputStateValuesOceanTidesAccelZ = 97;
var AgEAsEOMFuncPluginInputStateValuesDragAccelX = 98;
var AgEAsEOMFuncPluginInputStateValuesDragAccelY = 99;
var AgEAsEOMFuncPluginInputStateValuesDragAccelZ = 100;
var AgEAsEOMFuncPluginInputStateValuesThirdBodyAccelX = 101;
var AgEAsEOMFuncPluginInputStateValuesThirdBodyAccelY = 102;
var AgEAsEOMFuncPluginInputStateValuesThirdBodyAccelZ = 103;
var AgEAsEOMFuncPluginInputStateValuesSRPAccelX = 104;
var AgEAsEOMFuncPluginInputStateValuesSRPAccelY = 105;
var AgEAsEOMFuncPluginInputStateValuesSRPAccelZ = 106;
var AgEAsEOMFuncPluginInputStateValuesNoShadowSRPAccelX = 107;
var AgEAsEOMFuncPluginInputStateValuesNoShadowSRPAccelY = 108;
var AgEAsEOMFuncPluginInputStateValuesNoShadowSRPAccelZ = 109;
var AgEAsEOMFuncPluginInputStateValuesGenRelAccelX = 110;
var AgEAsEOMFuncPluginInputStateValuesGenRelAccelY = 111;
var AgEAsEOMFuncPluginInputStateValuesGenRelAccelZ = 112;
var AgEAsEOMFuncPluginInputStateValuesAlbedoAccelX = 113;
var AgEAsEOMFuncPluginInputStateValuesAlbedoAccelY = 114;
var AgEAsEOMFuncPluginInputStateValuesAlbedoAccelZ = 115;
var AgEAsEOMFuncPluginInputStateValuesThermalPressureAccelX = 116;
var AgEAsEOMFuncPluginInputStateValuesThermalPressureAccelY = 117;
var AgEAsEOMFuncPluginInputStateValuesThermalPressureAccelZ = 118;
var AgEAsEOMFuncPluginInputStateValuesAddedAccelX = 119;
var AgEAsEOMFuncPluginInputStateValuesAddedAccelY = 120;
var AgEAsEOMFuncPluginInputStateValuesAddedAccelZ = 121;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosXPosX = 122;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosXPosY = 123;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosXPosZ = 124;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosXVelX = 125;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosXVelY = 126;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosXVelZ = 127;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosYPosX = 128;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosYPosY = 129;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosYPosZ = 130;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosYVelX = 131;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosYVelY = 132;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosYVelZ = 133;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosZPosX = 134;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosZPosY = 135;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosZPosZ = 136;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosZVelX = 137;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosZVelY = 138;
var AgEAsEOMFuncPluginInputStateValuesStateTransPosZVelZ = 139;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelXPosX = 140;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelXPosY = 141;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelXPosZ = 142;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelXVelX = 143;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelXVelY = 144;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelXVelZ = 145;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelYPosX = 146;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelYPosY = 147;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelYPosZ = 148;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelYVelX = 149;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelYVelY = 150;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelYVelZ = 151;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelZPosX = 152;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelZPosY = 153;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelZPosZ = 154;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelZVelX = 155;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelZVelY = 156;
var AgEAsEOMFuncPluginInputStateValuesStateTransVelZVelZ = 157;

//==========================================
// Declare Global Variables
//==========================================

// Axes in which the delta-v is integrated.  This value can be changed on the 
// propagator panel.
var m_DeltaVAxes = "VNC(Earth)";


var m_AgUtPluginSite		= null;
var m_CrdnPluginProvider	= null;
var m_CrdnConfiguredAxes    = null;
var m_AgAttrScope			= null;

var m_thrustXIndex = 0;
var m_thrustYIndex = 0;
var m_thrustZIndex = 0;
var m_massIndex = 0;

var m_effectiveImpulseIndex = 0;
var m_integratedDeltaVxIndex = 0;
var m_integratedDeltaVyIndex = 0;
var m_integratedDeltaVzIndex = 0;

//========================
// GetPluginConfig method
//========================
function GetPluginConfig( AgAttrBuilder )
{
	if( m_AgAttrScope == null )
	{
		m_AgAttrScope = AgAttrBuilder.NewScope();
		
		// Create an attribute for the delta-V axes, so it appears on the panel.
		AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "DeltaVAxes", "Axes in which to integrate delta-V", "DeltaVAxes", 0);
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
// Init Method
//======================
function Init( AgUtPluginSite )
{
	m_AgUtPluginSite = AgUtPluginSite;
	
	if( m_AgUtPluginSite == null )
	{
	    return false;
	}
	
	m_CrdnPluginProvider 	= m_AgUtPluginSite.VectorToolProvider;
	
	if(m_CrdnPluginProvider != null)
	{
	    // we'll use this to rotate from inertial to the specified axes
		m_CrdnConfiguredAxes = m_CrdnPluginProvider.ConfigureAxes( "Inertial", "CentralBody/Earth", m_DeltaVAxes, "");
		
		if (m_CrdnConfiguredAxes != null)
		{
		    return true;
		}
	}
   
    return false;
} 

//======================
// Register Method
//======================
function Register( AgAsEOMFuncPluginRegisterHandler )
{
    // plugin needs the thrust vector and the mass
    AgAsEOMFuncPluginRegisterHandler.RegisterInput(AgEAsEOMFuncPluginInputStateValuesThrustX);
    AgAsEOMFuncPluginRegisterHandler.RegisterInput(AgEAsEOMFuncPluginInputStateValuesThrustY);
    AgAsEOMFuncPluginRegisterHandler.RegisterInput(AgEAsEOMFuncPluginInputStateValuesThrustZ);
    
    AgAsEOMFuncPluginRegisterHandler.RegisterInput(AgEAsEOMFuncPluginInputStateValuesTotalMass);

    
    // plugin gives the derivative of effective impulse and integrated delta-V
    AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("EffectiveImpulse");
    AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVx");
    AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVy");
    AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVz");

    // plugin only needs to be called on evaluate
    AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPrePropagate);
    AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPreNextStep);
    AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPostPropagate);
    
    return true;
}


//======================
// SetIndices Function
//======================
function SetIndices( AgAsEOMFuncPluginSetIndicesHandler )
{
    // get the indices for the input variables
    m_thrustXIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(AgEAsEOMFuncPluginInputStateValuesThrustX);
    m_thrustYIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(AgEAsEOMFuncPluginInputStateValuesThrustY);
    m_thrustZIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(AgEAsEOMFuncPluginInputStateValuesThrustZ);
    m_massIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(AgEAsEOMFuncPluginInputStateValuesTotalMass);
    
    // get the indices for the derivatives we will output
    m_effectiveImpulseIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("EffectiveImpulse");
    m_integratedDeltaVxIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVx");
    m_integratedDeltaVyIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVy");
    m_integratedDeltaVzIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVz");

    return true;
}

//=================
// Calc Method
//=================
function Calc( eventType, AgAsEOMFuncPluginStateVector )
{
    // get the current thrust values, and give back the derivatives of
    // effective impulse and the integrated delta V components

    // get thrust
    var thrustX = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustXIndex);
    var thrustY = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustYIndex);
    var thrustZ = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustZIndex);

    // get mass
    var mass = AgAsEOMFuncPluginStateVector.GetInputValue(m_massIndex);
    

    // derivative of effective impulse is the total thrust magnitude
    var thrustMag = Math.sqrt(thrustX*thrustX + thrustY*thrustY + thrustZ*thrustZ);
    AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_effectiveImpulseIndex, thrustMag);
    
    // rotate thrust vector to desired integration frame for integrated delta-V
    var thrustVBArray = m_CrdnConfiguredAxes.TransformComponents_Array(AgAsEOMFuncPluginStateVector, thrustX, thrustY, thrustZ);
    var thrustArray = thrustVBArray.toArray();
    
    // the derivative of each integrated delta-V component is that component of thrust acceleration 
    AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVxIndex, thrustArray[0] / mass);
    AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVyIndex, thrustArray[1] / mass);
    AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVzIndex, thrustArray[2] / mass);

    return true;
}


//===========================================================
// Free Method
//===========================================================
function Free()
{
	if( m_AgUtPluginSite != null )
	{	
		m_AgUtPluginSite 		= null
	}
	
	return true;
}

//============================================================
// DeltaVAxes property
//============================================================
function GetDeltaVAxes()
{
	return m_DeltaVAxes;
}

function SetDeltaVAxes( axes )
{
	m_DeltaVAxes = axes;
}



//=====================================================
//  Copyright 2009, Analytical Graphics, Inc.          
//=====================================================

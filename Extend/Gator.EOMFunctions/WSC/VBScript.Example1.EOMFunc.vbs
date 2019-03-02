'======================================================
'  Copyright 2009, Analytical Graphics, Inc.          
' =====================================================

'=================================
' Reference Frames Enumeration
'=================================
Dim eInertial, eFixed, eLVLH, eNTC
   
eInertial 		= 0
eFixed 			= 1
eLVLH 			= 2
eNTC 			= 3
 

'==================================
' Time Scale Enumeration
'==================================
Dim eUTC, eTAI, eTDT, eUT1, eSTKEpochSec, eTDB, eGPS
  
eUTC 			= 0
eTAI 			= 1
eTDT 			= 2
eUT1 			= 3
eSTKEpochSec 	= 4
eTDB 			= 5
eGPS 			= 6


'==================================
' Log Msg Type Enumeration
'==================================
Dim eLogMsgDebug, eLogMsgInfo, eLogMsgForceInfo, eLogMsgWarning, eLogMsgAlarm

eLogMsgDebug	 	= 0
eLogMsgInfo 		= 1
eLogMsgForceInfo 	= 2
eLogMsgWarning 		= 3
eLogMsgAlarm 		= 4

'===========================================
' AgEAttrAddFlags Enumeration
'===========================================
Dim eFlagNone, eFlagTransparent, eFlagHidden, eFlagTransient, eFlagReadOnly, eFlagFixed

eFlagNone			= 0
eFlagTransparent	= 2
eFlagHidden			= 4
eFlagTransient		= 8  
eFlagReadOnly		= 16
eFlagFixed			= 32

'==========================================
' EventType Enumeration
'==========================================
Dim eEventTypesPrePropagate, eEventTypesPreNextStep, eEventTypesEvaluate, eEventTypesPostPropagate

eEventTypesPrePropagate = 0
eEventTypesPreNextStep = 1
eEventTypesEvaluate = 2
eEventTypesPostPropagate = 3

'==========================================
' State Values Enumeration
'==========================================
dim AgEAsEOMFuncPluginInputStateValuesPosY
AgEAsEOMFuncPluginInputStateValuesPosY = 1
dim AgEAsEOMFuncPluginInputStateValuesPosZ
AgEAsEOMFuncPluginInputStateValuesPosZ = 2
dim AgEAsEOMFuncPluginInputStateValuesVelX
AgEAsEOMFuncPluginInputStateValuesVelX = 3
dim AgEAsEOMFuncPluginInputStateValuesVelY
AgEAsEOMFuncPluginInputStateValuesVelY = 4
dim AgEAsEOMFuncPluginInputStateValuesVelZ
AgEAsEOMFuncPluginInputStateValuesVelZ = 5
dim AgEAsEOMFuncPluginInputStateValuesPosCBFX
AgEAsEOMFuncPluginInputStateValuesPosCBFX = 6
dim AgEAsEOMFuncPluginInputStateValuesPosCBFY
AgEAsEOMFuncPluginInputStateValuesPosCBFY = 7
dim AgEAsEOMFuncPluginInputStateValuesPosCBFZ
AgEAsEOMFuncPluginInputStateValuesPosCBFZ = 8
dim AgEAsEOMFuncPluginInputStateValuesVelCBFX
AgEAsEOMFuncPluginInputStateValuesVelCBFX = 9
dim AgEAsEOMFuncPluginInputStateValuesVelCBFY
AgEAsEOMFuncPluginInputStateValuesVelCBFY = 10
dim AgEAsEOMFuncPluginInputStateValuesVelCBFZ
AgEAsEOMFuncPluginInputStateValuesVelCBFZ = 11
dim AgEAsEOMFuncPluginInputStateValuesCBIVelInCBFX
AgEAsEOMFuncPluginInputStateValuesCBIVelInCBFX = 12
dim AgEAsEOMFuncPluginInputStateValuesCBIVelInCBFY
AgEAsEOMFuncPluginInputStateValuesCBIVelInCBFY = 13
dim AgEAsEOMFuncPluginInputStateValuesCBIVelInCBFZ
AgEAsEOMFuncPluginInputStateValuesCBIVelInCBFZ = 14
dim AgEAsEOMFuncPluginInputStateValuesQuat1
AgEAsEOMFuncPluginInputStateValuesQuat1 = 15
dim AgEAsEOMFuncPluginInputStateValuesQuat2
AgEAsEOMFuncPluginInputStateValuesQuat2 = 16
dim AgEAsEOMFuncPluginInputStateValuesQuat3
AgEAsEOMFuncPluginInputStateValuesQuat3 = 17
dim AgEAsEOMFuncPluginInputStateValuesQuat4
AgEAsEOMFuncPluginInputStateValuesQuat4 = 18
dim AgEAsEOMFuncPluginInputStateValuesCBIToCBF00
AgEAsEOMFuncPluginInputStateValuesCBIToCBF00 = 19
dim AgEAsEOMFuncPluginInputStateValuesCBIToCBF01
AgEAsEOMFuncPluginInputStateValuesCBIToCBF01 = 20
dim AgEAsEOMFuncPluginInputStateValuesCBIToCBF02
AgEAsEOMFuncPluginInputStateValuesCBIToCBF02 = 21
dim AgEAsEOMFuncPluginInputStateValuesCBIToCBF10
AgEAsEOMFuncPluginInputStateValuesCBIToCBF10 = 22
dim AgEAsEOMFuncPluginInputStateValuesCBIToCBF11
AgEAsEOMFuncPluginInputStateValuesCBIToCBF11 = 23
dim AgEAsEOMFuncPluginInputStateValuesCBIToCBF12
AgEAsEOMFuncPluginInputStateValuesCBIToCBF12 = 24
dim AgEAsEOMFuncPluginInputStateValuesCBIToCBF20
AgEAsEOMFuncPluginInputStateValuesCBIToCBF20 = 25
dim AgEAsEOMFuncPluginInputStateValuesCBIToCBF21
AgEAsEOMFuncPluginInputStateValuesCBIToCBF21 = 26
dim AgEAsEOMFuncPluginInputStateValuesCBIToCBF22
AgEAsEOMFuncPluginInputStateValuesCBIToCBF22 = 27
dim AgEAsEOMFuncPluginInputStateValuesAngVelCBFX
AgEAsEOMFuncPluginInputStateValuesAngVelCBFX = 28
dim AgEAsEOMFuncPluginInputStateValuesAngVelCBFY
AgEAsEOMFuncPluginInputStateValuesAngVelCBFY = 29
dim AgEAsEOMFuncPluginInputStateValuesAngVelCBFZ
AgEAsEOMFuncPluginInputStateValuesAngVelCBFZ = 30
dim AgEAsEOMFuncPluginInputStateValuesAltitude
AgEAsEOMFuncPluginInputStateValuesAltitude = 31
dim AgEAsEOMFuncPluginInputStateValuesLatitude
AgEAsEOMFuncPluginInputStateValuesLatitude = 32
dim AgEAsEOMFuncPluginInputStateValuesLongitude
AgEAsEOMFuncPluginInputStateValuesLongitude = 33
dim AgEAsEOMFuncPluginInputStateValuesTotalMass
AgEAsEOMFuncPluginInputStateValuesTotalMass = 34
dim AgEAsEOMFuncPluginInputStateValuesDryMass
AgEAsEOMFuncPluginInputStateValuesDryMass = 35
dim AgEAsEOMFuncPluginInputStateValuesFuelMass
AgEAsEOMFuncPluginInputStateValuesFuelMass = 36
dim AgEAsEOMFuncPluginInputStateValuesCd
AgEAsEOMFuncPluginInputStateValuesCd = 37
dim AgEAsEOMFuncPluginInputStateValuesDragArea
AgEAsEOMFuncPluginInputStateValuesDragArea = 38
dim AgEAsEOMFuncPluginInputStateValuesAtmosphericDensity
AgEAsEOMFuncPluginInputStateValuesAtmosphericDensity = 39
dim AgEAsEOMFuncPluginInputStateValuesAtmosphericAltitude
AgEAsEOMFuncPluginInputStateValuesAtmosphericAltitude = 40
dim AgEAsEOMFuncPluginInputStateValuesCr
AgEAsEOMFuncPluginInputStateValuesCr = 41
dim AgEAsEOMFuncPluginInputStateValuesSRPArea
AgEAsEOMFuncPluginInputStateValuesSRPArea = 42
dim AgEAsEOMFuncPluginInputStateValuesKr1
AgEAsEOMFuncPluginInputStateValuesKr1 = 43
dim AgEAsEOMFuncPluginInputStateValuesKr2
AgEAsEOMFuncPluginInputStateValuesKr2 = 44
dim AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFX
AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFX = 45
dim AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFY
AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFY = 46
dim AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFZ
AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSunPosCBFZ = 47
dim AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFX
AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFX = 48
dim AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFY
AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFY = 49
dim AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFZ
AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatPosCBFZ = 50
dim AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosX
AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosX = 51
dim AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosY
AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosY = 52
dim AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosZ
AgEAsEOMFuncPluginInputStateValuesApparentToTrueCbSatToSunCBIPosZ = 53
dim AgEAsEOMFuncPluginInputStateValuesApparentSunPosCBFX
AgEAsEOMFuncPluginInputStateValuesApparentSunPosCBFX = 54
dim AgEAsEOMFuncPluginInputStateValuesApparentSunPosCBFY
AgEAsEOMFuncPluginInputStateValuesApparentSunPosCBFY = 55
dim AgEAsEOMFuncPluginInputStateValuesApparentSunPosCBFZ
AgEAsEOMFuncPluginInputStateValuesApparentSunPosCBFZ = 56
dim AgEAsEOMFuncPluginInputStateValuesApparentSatPosCBFX
AgEAsEOMFuncPluginInputStateValuesApparentSatPosCBFX = 57
dim AgEAsEOMFuncPluginInputStateValuesApparentSatPosCBFY
AgEAsEOMFuncPluginInputStateValuesApparentSatPosCBFY = 58
dim AgEAsEOMFuncPluginInputStateValuesApparentSatPosCBFZ
AgEAsEOMFuncPluginInputStateValuesApparentSatPosCBFZ = 59
dim AgEAsEOMFuncPluginInputStateValuesApparentSatToSunCBIPosX
AgEAsEOMFuncPluginInputStateValuesApparentSatToSunCBIPosX = 60
dim AgEAsEOMFuncPluginInputStateValuesApparentSatToSunCBIPosY
AgEAsEOMFuncPluginInputStateValuesApparentSatToSunCBIPosY = 61
dim AgEAsEOMFuncPluginInputStateValuesApparentSatToSunCBIPosZ
AgEAsEOMFuncPluginInputStateValuesApparentSatToSunCBIPosZ = 62
dim AgEAsEOMFuncPluginInputStateValuesTrueSunPosCBFX
AgEAsEOMFuncPluginInputStateValuesTrueSunPosCBFX = 63
dim AgEAsEOMFuncPluginInputStateValuesTrueSunPosCBFY
AgEAsEOMFuncPluginInputStateValuesTrueSunPosCBFY = 64
dim AgEAsEOMFuncPluginInputStateValuesTrueSunPosCBFZ
AgEAsEOMFuncPluginInputStateValuesTrueSunPosCBFZ = 65
dim AgEAsEOMFuncPluginInputStateValuesTrueSatPosCBFX
AgEAsEOMFuncPluginInputStateValuesTrueSatPosCBFX = 66
dim AgEAsEOMFuncPluginInputStateValuesTrueSatPosCBFY
AgEAsEOMFuncPluginInputStateValuesTrueSatPosCBFY = 67
dim AgEAsEOMFuncPluginInputStateValuesTrueSatPosCBFZ
AgEAsEOMFuncPluginInputStateValuesTrueSatPosCBFZ = 68
dim AgEAsEOMFuncPluginInputStateValuesTrueSatToSunCBIPosX
AgEAsEOMFuncPluginInputStateValuesTrueSatToSunCBIPosX = 69
dim AgEAsEOMFuncPluginInputStateValuesTrueSatToSunCBIPosY
AgEAsEOMFuncPluginInputStateValuesTrueSatToSunCBIPosY = 70
dim AgEAsEOMFuncPluginInputStateValuesTrueSatToSunCBIPosZ
AgEAsEOMFuncPluginInputStateValuesTrueSatToSunCBIPosZ = 71
dim AgEAsEOMFuncPluginInputStateValuesSolarIntensity
AgEAsEOMFuncPluginInputStateValuesSolarIntensity = 72
dim AgEAsEOMFuncPluginInputStateValuesRadPressureCoefficient
AgEAsEOMFuncPluginInputStateValuesRadPressureCoefficient = 73
dim AgEAsEOMFuncPluginInputStateValuesRadPressureArea
AgEAsEOMFuncPluginInputStateValuesRadPressureArea = 74
dim AgEAsEOMFuncPluginInputStateValuesMassFlowRate
AgEAsEOMFuncPluginInputStateValuesMassFlowRate = 75
dim AgEAsEOMFuncPluginInputStateValuesTankPressure
AgEAsEOMFuncPluginInputStateValuesTankPressure = 76
dim AgEAsEOMFuncPluginInputStateValuesTankTemperature
AgEAsEOMFuncPluginInputStateValuesTankTemperature = 77
dim AgEAsEOMFuncPluginInputStateValuesFuelDensity
AgEAsEOMFuncPluginInputStateValuesFuelDensity = 78
dim AgEAsEOMFuncPluginInputStateValuesThrustX
AgEAsEOMFuncPluginInputStateValuesThrustX = 79
dim AgEAsEOMFuncPluginInputStateValuesThrustY
AgEAsEOMFuncPluginInputStateValuesThrustY = 80
dim AgEAsEOMFuncPluginInputStateValuesThrustZ
AgEAsEOMFuncPluginInputStateValuesThrustZ = 81
dim AgEAsEOMFuncPluginInputStateValuesDeltaV
AgEAsEOMFuncPluginInputStateValuesDeltaV = 82
dim AgEAsEOMFuncPluginInputStateValuesGravityAccelX
AgEAsEOMFuncPluginInputStateValuesGravityAccelX = 83
dim AgEAsEOMFuncPluginInputStateValuesGravityAccelY
AgEAsEOMFuncPluginInputStateValuesGravityAccelY = 84
dim AgEAsEOMFuncPluginInputStateValuesGravityAccelZ
AgEAsEOMFuncPluginInputStateValuesGravityAccelZ = 85
dim AgEAsEOMFuncPluginInputStateValuesTwoBodyAccelX
AgEAsEOMFuncPluginInputStateValuesTwoBodyAccelX = 86
dim AgEAsEOMFuncPluginInputStateValuesTwoBodyAccelY
AgEAsEOMFuncPluginInputStateValuesTwoBodyAccelY = 87
dim AgEAsEOMFuncPluginInputStateValuesTwoBodyAccelZ
AgEAsEOMFuncPluginInputStateValuesTwoBodyAccelZ = 88
dim AgEAsEOMFuncPluginInputStateValuesGravityPertAccelX
AgEAsEOMFuncPluginInputStateValuesGravityPertAccelX = 89
dim AgEAsEOMFuncPluginInputStateValuesGravityPertAccelY
AgEAsEOMFuncPluginInputStateValuesGravityPertAccelY = 90
dim AgEAsEOMFuncPluginInputStateValuesGravityPertAccelZ
AgEAsEOMFuncPluginInputStateValuesGravityPertAccelZ = 91
dim AgEAsEOMFuncPluginInputStateValuesSolidTidesAccelX
AgEAsEOMFuncPluginInputStateValuesSolidTidesAccelX = 92
dim AgEAsEOMFuncPluginInputStateValuesSolidTidesAccelY
AgEAsEOMFuncPluginInputStateValuesSolidTidesAccelY = 93
dim AgEAsEOMFuncPluginInputStateValuesSolidTidesAccelZ
AgEAsEOMFuncPluginInputStateValuesSolidTidesAccelZ = 94
dim AgEAsEOMFuncPluginInputStateValuesOceanTidesAccelX
AgEAsEOMFuncPluginInputStateValuesOceanTidesAccelX = 95
dim AgEAsEOMFuncPluginInputStateValuesOceanTidesAccelY
AgEAsEOMFuncPluginInputStateValuesOceanTidesAccelY = 96
dim AgEAsEOMFuncPluginInputStateValuesOceanTidesAccelZ
AgEAsEOMFuncPluginInputStateValuesOceanTidesAccelZ = 97
dim AgEAsEOMFuncPluginInputStateValuesDragAccelX
AgEAsEOMFuncPluginInputStateValuesDragAccelX = 98
dim AgEAsEOMFuncPluginInputStateValuesDragAccelY
AgEAsEOMFuncPluginInputStateValuesDragAccelY = 99
dim AgEAsEOMFuncPluginInputStateValuesDragAccelZ
AgEAsEOMFuncPluginInputStateValuesDragAccelZ = 100
dim AgEAsEOMFuncPluginInputStateValuesThirdBodyAccelX
AgEAsEOMFuncPluginInputStateValuesThirdBodyAccelX = 101
dim AgEAsEOMFuncPluginInputStateValuesThirdBodyAccelY
AgEAsEOMFuncPluginInputStateValuesThirdBodyAccelY = 102
dim AgEAsEOMFuncPluginInputStateValuesThirdBodyAccelZ
AgEAsEOMFuncPluginInputStateValuesThirdBodyAccelZ = 103
dim AgEAsEOMFuncPluginInputStateValuesSRPAccelX
AgEAsEOMFuncPluginInputStateValuesSRPAccelX = 104
dim AgEAsEOMFuncPluginInputStateValuesSRPAccelY
AgEAsEOMFuncPluginInputStateValuesSRPAccelY = 105
dim AgEAsEOMFuncPluginInputStateValuesSRPAccelZ
AgEAsEOMFuncPluginInputStateValuesSRPAccelZ = 106
dim AgEAsEOMFuncPluginInputStateValuesNoShadowSRPAccelX
AgEAsEOMFuncPluginInputStateValuesNoShadowSRPAccelX = 107
dim AgEAsEOMFuncPluginInputStateValuesNoShadowSRPAccelY
AgEAsEOMFuncPluginInputStateValuesNoShadowSRPAccelY = 108
dim AgEAsEOMFuncPluginInputStateValuesNoShadowSRPAccelZ
AgEAsEOMFuncPluginInputStateValuesNoShadowSRPAccelZ = 109
dim AgEAsEOMFuncPluginInputStateValuesGenRelAccelX
AgEAsEOMFuncPluginInputStateValuesGenRelAccelX = 110
dim AgEAsEOMFuncPluginInputStateValuesGenRelAccelY
AgEAsEOMFuncPluginInputStateValuesGenRelAccelY = 111
dim AgEAsEOMFuncPluginInputStateValuesGenRelAccelZ
AgEAsEOMFuncPluginInputStateValuesGenRelAccelZ = 112
dim AgEAsEOMFuncPluginInputStateValuesAlbedoAccelX
AgEAsEOMFuncPluginInputStateValuesAlbedoAccelX = 113
dim AgEAsEOMFuncPluginInputStateValuesAlbedoAccelY
AgEAsEOMFuncPluginInputStateValuesAlbedoAccelY = 114
dim AgEAsEOMFuncPluginInputStateValuesAlbedoAccelZ
AgEAsEOMFuncPluginInputStateValuesAlbedoAccelZ = 115
dim AgEAsEOMFuncPluginInputStateValuesThermalPressureAccelX
AgEAsEOMFuncPluginInputStateValuesThermalPressureAccelX = 116
dim AgEAsEOMFuncPluginInputStateValuesThermalPressureAccelY
AgEAsEOMFuncPluginInputStateValuesThermalPressureAccelY = 117
dim AgEAsEOMFuncPluginInputStateValuesThermalPressureAccelZ
AgEAsEOMFuncPluginInputStateValuesThermalPressureAccelZ = 118
dim AgEAsEOMFuncPluginInputStateValuesAddedAccelX
AgEAsEOMFuncPluginInputStateValuesAddedAccelX = 119
dim AgEAsEOMFuncPluginInputStateValuesAddedAccelY
AgEAsEOMFuncPluginInputStateValuesAddedAccelY = 120
dim AgEAsEOMFuncPluginInputStateValuesAddedAccelZ
AgEAsEOMFuncPluginInputStateValuesAddedAccelZ = 121
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosXPosX
AgEAsEOMFuncPluginInputStateValuesStateTransPosXPosX = 122
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosXPosY
AgEAsEOMFuncPluginInputStateValuesStateTransPosXPosY = 123
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosXPosZ
AgEAsEOMFuncPluginInputStateValuesStateTransPosXPosZ = 124
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosXVelX
AgEAsEOMFuncPluginInputStateValuesStateTransPosXVelX = 125
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosXVelY
AgEAsEOMFuncPluginInputStateValuesStateTransPosXVelY = 126
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosXVelZ
AgEAsEOMFuncPluginInputStateValuesStateTransPosXVelZ = 127
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosYPosX
AgEAsEOMFuncPluginInputStateValuesStateTransPosYPosX = 128
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosYPosY
AgEAsEOMFuncPluginInputStateValuesStateTransPosYPosY = 129
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosYPosZ
AgEAsEOMFuncPluginInputStateValuesStateTransPosYPosZ = 130
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosYVelX
AgEAsEOMFuncPluginInputStateValuesStateTransPosYVelX = 131
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosYVelY
AgEAsEOMFuncPluginInputStateValuesStateTransPosYVelY = 132
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosYVelZ
AgEAsEOMFuncPluginInputStateValuesStateTransPosYVelZ = 133
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosZPosX
AgEAsEOMFuncPluginInputStateValuesStateTransPosZPosX = 134
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosZPosY
AgEAsEOMFuncPluginInputStateValuesStateTransPosZPosY = 135
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosZPosZ
AgEAsEOMFuncPluginInputStateValuesStateTransPosZPosZ = 136
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosZVelX
AgEAsEOMFuncPluginInputStateValuesStateTransPosZVelX = 137
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosZVelY
AgEAsEOMFuncPluginInputStateValuesStateTransPosZVelY = 138
dim AgEAsEOMFuncPluginInputStateValuesStateTransPosZVelZ
AgEAsEOMFuncPluginInputStateValuesStateTransPosZVelZ = 139
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelXPosX
AgEAsEOMFuncPluginInputStateValuesStateTransVelXPosX = 140
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelXPosY
AgEAsEOMFuncPluginInputStateValuesStateTransVelXPosY = 141
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelXPosZ
AgEAsEOMFuncPluginInputStateValuesStateTransVelXPosZ = 142
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelXVelX
AgEAsEOMFuncPluginInputStateValuesStateTransVelXVelX = 143
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelXVelY
AgEAsEOMFuncPluginInputStateValuesStateTransVelXVelY = 144
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelXVelZ
AgEAsEOMFuncPluginInputStateValuesStateTransVelXVelZ = 145
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelYPosX
AgEAsEOMFuncPluginInputStateValuesStateTransVelYPosX = 146
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelYPosY
AgEAsEOMFuncPluginInputStateValuesStateTransVelYPosY = 147
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelYPosZ
AgEAsEOMFuncPluginInputStateValuesStateTransVelYPosZ = 148
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelYVelX
AgEAsEOMFuncPluginInputStateValuesStateTransVelYVelX = 149
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelYVelY
AgEAsEOMFuncPluginInputStateValuesStateTransVelYVelY = 150
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelYVelZ
AgEAsEOMFuncPluginInputStateValuesStateTransVelYVelZ = 151
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelZPosX
AgEAsEOMFuncPluginInputStateValuesStateTransVelZPosX = 152
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelZPosY
AgEAsEOMFuncPluginInputStateValuesStateTransVelZPosY = 153
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelZPosZ
AgEAsEOMFuncPluginInputStateValuesStateTransVelZPosZ = 154
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelZVelX
AgEAsEOMFuncPluginInputStateValuesStateTransVelZVelX = 155
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelZVelY
AgEAsEOMFuncPluginInputStateValuesStateTransVelZVelY = 156
dim AgEAsEOMFuncPluginInputStateValuesStateTransVelZVelZ
AgEAsEOMFuncPluginInputStateValuesStateTransVelZVelZ = 157

'================================
' Global Variables
'================================
Dim m_AgUtPluginSite
Dim m_AgAttrScope
Dim m_CrdnPluginProvider
Dim m_CrdnConfiguredAxes

Set m_AgUtPluginSite 	   = Nothing
Set m_AgAttrScope 		   = Nothing
Set m_CrdnPluginProvider   = Nothing
Set m_CrdnConfiguredAxes   = Nothing

Dim m_DeltaVAxes

m_DeltaVAxes = "VNC(Earth)"

dim m_thrustXIndex
dim m_thrustYIndex
dim m_thrustZIndex
dim m_massIndex
dim m_effectiveImpulseIndex
dim m_integratedDeltaVxIndex
dim m_integratedDeltaVyIndex
dim m_integratedDeltaVzIndex

m_thrustXIndex = 0
m_thrustYIndex = 0
m_thrustZIndex = 0
m_massIndex = 0
m_effectiveImpulseIndex = 0
m_integratedDeltaVxIndex = 0
m_integratedDeltaVyIndex = 0
m_integratedDeltaVzIndex = 0

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( AgAttrBuilder )

	If( m_AgAttrScope is Nothing ) Then
   
		Set m_AgAttrScope = AgAttrBuilder.NewScope()
		
		' Create an attribute for the delta-V axes, so it appears on the panel.
		Call AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "DeltaVAxes", "Axes in which to integrate delta-V", "DeltaVAxes", 0)
		

	End If

	Set GetPluginConfig = m_AgAttrScope

End Function  

'===========================
' VerifyPluginConfig method
'===========================
Function VerifyPluginConfig(AgUtPluginConfigVerifyResult)
   
    Dim Result
    Dim Message

	Result = true
	Message = "Ok"

	AgUtPluginConfigVerifyResult.Result  = Result
	AgUtPluginConfigVerifyResult.Message = Message

End Function  

'======================
' Init Method
'======================
Function Init( AgUtPluginSite )

    Dim ret
    ret = false

	Set m_AgUtPluginSite = AgUtPluginSite
	
	If( Not m_AgUtPluginSite is Nothing ) Then
	
		Set m_CrdnPluginProvider 	= m_AgUtPluginSite.VectorToolProvider
		
		If(Not m_CrdnPluginProvider is Nothing) Then

            ' we'll use this to rotate from inertial to the specified axes
			Set m_CrdnConfiguredAxes  = m_CrdnPluginProvider.ConfigureAxes( "Inertial", "CentralBody/Earth", m_DeltaVAxes, "")
			
			If (Not m_CrdnConfiguredAxes is Nothing) Then
			
			    ret = true
			    
			End If
			
		End If			
		
	End If
	
    Init = ret

End Function
 

'======================
' Register Method
'======================
Function Register( AgAsEOMFuncPluginRegisterHandler )

    ' plugin needs the thrust vector and the mass
    Call AgAsEOMFuncPluginRegisterHandler.RegisterInput(AgEAsEOMFuncPluginInputStateValuesThrustX)
    Call AgAsEOMFuncPluginRegisterHandler.RegisterInput(AgEAsEOMFuncPluginInputStateValuesThrustY)
    Call AgAsEOMFuncPluginRegisterHandler.RegisterInput(AgEAsEOMFuncPluginInputStateValuesThrustZ)
    
    Call AgAsEOMFuncPluginRegisterHandler.RegisterInput(AgEAsEOMFuncPluginInputStateValuesTotalMass)

    
    ' plugin gives the derivative of effective impulse and integrated delta-V
    Call AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("EffectiveImpulse")
    Call AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVx")
    Call AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVy")
    Call AgAsEOMFuncPluginRegisterHandler.RegisterUserDerivativeOutput("IntegratedDeltaVz")

    ' plugin only needs to be called on evaluate
    Call AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPrePropagate)
    Call AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPreNextStep)
    Call AgAsEOMFuncPluginRegisterHandler.ExcludeEvent(eEventTypesPostPropagate)
    
    Register = true

End Function


'======================
' SetIndices Function
'======================
Function SetIndices( AgAsEOMFuncPluginSetIndicesHandler )

    ' get the indices for the input variables
    m_thrustXIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(AgEAsEOMFuncPluginInputStateValuesThrustX)
    m_thrustYIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(AgEAsEOMFuncPluginInputStateValuesThrustY)
    m_thrustZIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(AgEAsEOMFuncPluginInputStateValuesThrustZ)
    m_massIndex = AgAsEOMFuncPluginSetIndicesHandler.GetInputIndex(AgEAsEOMFuncPluginInputStateValuesTotalMass)
    
    ' get the indices for the derivatives we will output
    m_effectiveImpulseIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("EffectiveImpulse")
    m_integratedDeltaVxIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVx")
    m_integratedDeltaVyIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVy")
    m_integratedDeltaVzIndex = AgAsEOMFuncPluginSetIndicesHandler.GetUserDerivativeOutputIndex("IntegratedDeltaVz")

    SetIndices = true

End Function

'=================
' Calc Method
'=================
Function Calc( eventType, AgAsEOMFuncPluginStateVector )

    ' get the current thrust values, and give back the derivatives of
    ' effective impulse and the integrated delta V components

    ' get thrust
    Dim thrustX, thrustY, thrustZ
    thrustX = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustXIndex)
    thrustY = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustYIndex)
    thrustZ = AgAsEOMFuncPluginStateVector.GetInputValue(m_thrustZIndex)

    ' get mass
    Dim mass
    mass = AgAsEOMFuncPluginStateVector.GetInputValue(m_massIndex)
    

    ' derivative of effective impulse is the total thrust magnitude
    Dim thrustMag
    thrustMag = Sqr(thrustX*thrustX + thrustY*thrustY + thrustZ*thrustZ)
    Call AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_effectiveImpulseIndex, thrustMag)
    
    ' rotate thrust vector to desired integration frame for integrated delta-V
    Dim thrustArray
    thrustArray = m_CrdnConfiguredAxes.TransformComponents_Array(AgAsEOMFuncPluginStateVector, thrustX, thrustY, thrustZ)    
    ' the derivative of each integrated delta-V component is that component of thrust acceleration 
    Call AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVxIndex, thrustArray(0) / mass)
    Call AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVyIndex, thrustArray(1) / mass)
    Call AgAsEOMFuncPluginStateVector.AddDerivativeOutputValue(m_integratedDeltaVzIndex, thrustArray(2) / mass)

    Calc = true
    
End Function

'===========================================================
' Free Method
'===========================================================
Sub Free()

	If( Not m_AgUtPluginSite is Nothing ) Then
	
		Set m_AgUtPluginSite 		= Nothing
		Set m_CrdnPluginProvider   	= Nothing
		Set m_CrdnConfiguredAxes 	= Nothing

	End If

End Sub

'============================================================
' DeltaVAxes property
'============================================================
Function GetDeltaVAxes()

       GetDeltaVAxes = m_DeltaVAxes

End Function

Function SetDeltaVAxes(axes)

       m_DeltaVAxes = axes

End Function


'======================================================
'  Copyright 2009, Analytical Graphics, Inc.          
' =====================================================

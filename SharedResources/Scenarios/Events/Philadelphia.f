stk.v.10.0
WrittenBy    STK_v10.0.0

BEGIN Facility

Name        Philadelphia

	BEGIN CentroidPosition

		CentralBody            Earth
		DisplayCoords          Geodetic
		EcfLatitude            4.00068206787109e+001
		EcfLongitude           -7.51346817016602e+001
		EcfAltitude            0.00000000000000e+000
		HeightAboveGround      0.00000000000000e+000
		ComputeTrnMaskAsNeeded Off
		DisplayAltRef          Ellipsoid
		UseTerrainInfo         Off
		NumAzRaysInMask        360
		TerrainNormalMode      UseCbShape

	END CentroidPosition

BEGIN Extensions
    
    BEGIN Graphics

            BEGIN Attributes

                MarkerColor             #f0deb2
                LabelColor              #f0deb2
                LineStyle               0
                MarkerStyle             9
                FontStyle               0

            END Attributes

            BEGIN Graphics

                Show                    On
                Inherit                 On
                IsDynamic               Off
                ShowLabel               On
                ShowAzElMask            Off
                ShowAzElFill            Off
                AzElFillStyle           7
                AzElFillAltTranslucency 0.500000
                UseAzElColor            Off
                AzElColor               #ffffff
                MinDisplayAlt           0.000
                MaxDisplayAlt           10000000.000
                NumAzElMaskSteps        1
                ShowAzElAtRangeMask       Off
                ShowAzElAtRangeFill       Off
                AzElFillRangeTranslucency 0.500000
                AzElAtRangeFillStyle      7
                UseAzElAtRangeColor          Off
                AzElAtRangeColor          #ffffff
                MinDisplayRange           0.000
                MaxDisplayRange           10000000.000
                NumAzElAtRangeMaskSteps   1

            BEGIN RangeContourData
                    Show                  Off
                    ShowRangeFill         Off
                    RangeFillTranslucency 0.500000
                    LabelUnits            4
                    NumDecimalDigits      3
            BEGIN ContourLevel
                Value      1.000000000000e+005
                Color      #4169e1
                LineStyle  0
                LineWidth  1
                Numbered   On
                ShowText   Off
                LabelAngle 0
            END ContourLevel
            BEGIN ContourLevel
                Value      2.000000000000e+005
                Color      #87cefa
                LineStyle  0
                LineWidth  1
                Numbered   On
                ShowText   Off
                LabelAngle 0
            END ContourLevel
            BEGIN ContourLevel
                Value      3.000000000000e+005
                Color      #00ced1
                LineStyle  0
                LineWidth  1
                Numbered   On
                ShowText   Off
                LabelAngle 0
            END ContourLevel
            BEGIN ContourLevel
                Value      4.000000000000e+005
                Color      #6b8e23
                LineStyle  0
                LineWidth  1
                Numbered   On
                ShowText   Off
                LabelAngle 0
            END ContourLevel
            BEGIN ContourLevel
                Value      5.000000000000e+005
                Color      #8fbc8f
                LineStyle  0
                LineWidth  1
                Numbered   On
                ShowText   Off
                LabelAngle 0
            END ContourLevel

            END RangeContourData

            END Graphics

            Begin DisplayTimes
                DisplayType	AlwaysOn
            End DisplayTimes
    END Graphics
    
    BEGIN LaserCAT
		Mode                     TargetObject
		StartTime                1 Jun 2003 12:00:00.000000000
		StopTime                 2 Jun 2003 12:00:00.000000000
		RangeConstraint         500000000.00000
		MinElevationAng         0.34907
		Duration                0.00000
		ExclHalfAng             0.08727
		MaxPVtoScenario         10
		CenterFrequency         14000000000.00000
		BandWidth               20000000.00000
		Linear_PowerFlux/EIRP   1.0000000000000e+014
		Linear_PowerThreshold   6.3095734448019e-004
		TransmitOn              1
		ReceiveOn               0
		PVDataBase              stkAllComm.tce
		RFIDataBase             stkAllComm.rfi
		UseGeomFilters          Yes
		UseOutOfDate            Yes
		NearEarthOutOfDate       10.00000
		DeepSpaceOutOfDate       40.00000
		LoadPotVictims          No
		UsePotVictimList        No
    END LaserCAT
    
    BEGIN ExternData
    END ExternData
    
    BEGIN RFI
		Mode                     TargetObject
		StartTime                1 Jun 2003 12:00:00.000000000
		StopTime                 2 Jun 2003 12:00:00.000000000
		RangeConstraint         500000000.00000
		MinElevationAng         0.34907
		Duration                0.00000
		ExclHalfAng             0.08727
		MaxPVtoScenario         10
		CenterFrequency         14000000000.00000
		BandWidth               20000000.00000
		Linear_PowerFlux/EIRP   1.0000000000000e+014
		Linear_PowerThreshold   6.3095734448019e-004
		TransmitOn              1
		ReceiveOn               0
		PVDataBase              stkAllComm.tce
		RFIDataBase             stkAllComm.rfi
		UseGeomFilters          Yes
		UseOutOfDate            Yes
		NearEarthOutOfDate       10.00000
		DeepSpaceOutOfDate       40.00000
		LoadPotVictims          No
		UsePotVictimList        No
    END RFI
    
    BEGIN ADFFileData
    END ADFFileData
    
    BEGIN AccessConstraints
		LineOfSight   IncludeIntervals 
		ElevationAngle		Min  0.0000000000e+000   IncludeIntervals 
    END AccessConstraints
    
    BEGIN ObjectCoverage
    END ObjectCoverage
    
    BEGIN Desc
        LongText    183
City Name		Philadelphia
Type			1
Province		Pennsylvania
Country			United States
Province Rank		0
Population		0
Latitude		40.0068206787109 deg
Longitude		-75.1346817016602 deg

    END Desc
    
    BEGIN Atmosphere
<!-- STKv4.0 Format="XML" -->
<STKOBJECT>
<OBJECT Class = "AtmosphereExtension" Name = "STK_Atmosphere_Extension">
    <OBJECT Class = "link" Name = "AtmosAbsorptionModel">
        <OBJECT Class = "AtmosphericAbsorptionModel" Name = "Simple_Satcom">
            <OBJECT Class = "string" Name = "Category"> &quot;@Top&quot; </OBJECT>
            <OBJECT Class = "bool" Name = "Clonable"> True </OBJECT>
            <OBJECT Class = "string" Name = "Description"> &quot;Simple Satcom gaseous absorption model&quot; </OBJECT>
            <OBJECT Class = "bool" Name = "ReadOnly"> False </OBJECT>
            <OBJECT Class = "double" Name = "SurfaceTemperature"> 293.15 K </OBJECT>
            <OBJECT Class = "string" Name = "Type"> &quot;Simple Satcom&quot; </OBJECT>
            <OBJECT Class = "string" Name = "UserComment"> &quot;Simple Satcom gaseous absorption model&quot; </OBJECT>
            <OBJECT Class = "string" Name = "Version"> &quot;1.0.0 a&quot; </OBJECT>
            <OBJECT Class = "double" Name = "WaterVaporConcentration"> 7.5 g*m^-3 </OBJECT>
        </OBJECT>
    </OBJECT>
    <OBJECT Class = "string" Name = "Category"> &quot;&quot; </OBJECT>
    <OBJECT Class = "bool" Name = "Clonable"> True </OBJECT>
    <OBJECT Class = "string" Name = "Description"> &quot;STK Atmosphere Extension&quot; </OBJECT>
    <OBJECT Class = "bool" Name = "EnableLocalRainData"> False </OBJECT>
    <OBJECT Class = "bool" Name = "InheritAtmosAbsorptionModel"> True </OBJECT>
    <OBJECT Class = "double" Name = "LocalRainIsoHeight"> 2000 m </OBJECT>
    <OBJECT Class = "double" Name = "LocalRainRate"> 1 mm*hr^-1 </OBJECT>
    <OBJECT Class = "double" Name = "LocalSurfaceTemp"> 293.15 K </OBJECT>
    <OBJECT Class = "bool" Name = "ReadOnly"> False </OBJECT>
    <OBJECT Class = "string" Name = "Type"> &quot;STK Atmosphere Extension&quot; </OBJECT>
    <OBJECT Class = "string" Name = "UserComment"> &quot;STK Atmosphere Extension&quot; </OBJECT>
    <OBJECT Class = "string" Name = "Version"> &quot;1.0.0 a&quot; </OBJECT>
</OBJECT>
</STKOBJECT>
    END Atmosphere
    
    BEGIN RCS
	Inherited          True
	LinearClutterCoef        1.000000e+000
	BEGIN RCSBAND
		LinearConstantValue      1.000000e+000
		Swerling      0
		BandData      3.000000e+006 3.000000e+011
		BandData      2.997920e+006 3.000000e+006
	END RCSBAND
    END RCS
    
    BEGIN Identification
    END Identification
    
    BEGIN Crdn
    END Crdn
    
    BEGIN VO
    END VO

END Extensions

BEGIN SubObjects

END SubObjects

END Facility


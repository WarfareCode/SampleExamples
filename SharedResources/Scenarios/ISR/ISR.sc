stk.v.10.0
WrittenBy    STK_v10.0.0
BEGIN Scenario
    Name            ISR

BEGIN Epoch

    Epoch        4 Jul 2000 00:00:00.000000000
    SmartEpoch
	BEGIN	EVENT
			Epoch	4 Jul 2000 00:00:00.000000000
			EventEpoch
				BEGIN	EVENT
					Type	EVENT_LINKTO
					Name	AnalysisStartTime
				END	EVENT
			EpochState	Implicit
	END	EVENT


END Epoch

BEGIN Interval

Start                   4 Jul 2000 00:00:00.000000000
Stop                    6 Jul 2000 00:00:01.000000000
    SmartInterval
	BEGIN	EVENTINTERVAL
			BEGIN Interval
				Start	4 Jul 2000 00:00:00.000000000
				Stop	6 Jul 2000 00:00:01.000000000
			END Interval
			IntervalState	Explicit
	END	EVENTINTERVAL

EpochUsesAnalStart      No
AnimStartUsesAnalStart  No
AnimStopUsesAnalStop    No

END Interval

BEGIN EOPFile

    EOPFilename     EOP-v1.1.txt

END EOPFile

BEGIN GlobalPrefs

    SatelliteNoOrbWarning    No
    MissilePerigeeWarning    No
    MissileStopTimeWarning   No
    AircraftWGS84Warning     Always
END GlobalPrefs

BEGIN CentralBody

    PrimaryBody     Earth

END CentralBody

BEGIN CentralBodyTerrain

END CentralBodyTerrain

BEGIN StarCollection

    Name     Hipparcos 2 Mag 6

END StarCollection

BEGIN ScenarioLicenses
    Module    AMMv10.0
    Module    ASTGv10.0
    Module    AnalysisWBv10.0
    Module    CATv10.0
    Module    COVv10.0
    Module    Commv10.0
    Module    DISv10.0
    Module    RT3Clientv10.0
    Module    Radarv10.0
    Module    SEETv10.0
    Module    STKIntegrationv10.0
    Module    STKParallelComputingv10.0
    Module    STKProfessionalv10.0
    Module    STKTIMv10.0
    Module    STKv10.0
    Module    SatProv10.0
    Module    TIREMv10.0
    Module    UPropv10.0
END ScenarioLicenses

BEGIN WebData
        EnableWebTerrainData    No
        SaveWebTerrainDataPasswords    No
        BEGIN ConfigServerDataList
            BEGIN ConfigServerData
                Name "globeserver.agi.com"
                Port 80
                DataURL "bin/getGlobeSvrConfig.pl"
            END ConfigServerData
        END ConfigServerDataList
END WebData

BEGIN Extensions
    
    BEGIN Graphics

BEGIN Animation

    StartTime          4 Jul 2000 22:00:00.010000000
    EndTime            4 Jul 2000 23:00:00.000000000
    CurrentTime        4 Jul 2000 22:00:00.010000000
    Mode               Stop
    Direction          Forward
    UpdateDelta        5.000000
    RefreshDelta       0.100000
    XRealTimeMult      1.000000
    RealTimeOffset     0.000000
    XRtStartFromPause  Yes

END Animation


        BEGIN DisplayFlags
            ShowLabels           On
            ShowPassLabel        Off
            ShowElsetNum         Off
            ShowGndTracks        On
            ShowGndMarkers       On
            ShowOrbitMarkers     On
            ShowPlanetOrbits     Off
            ShowPlanetCBIPos     On
            ShowPlanetCBILabel   On
            ShowPlanetGndPos     On
            ShowPlanetGndLabel   On
            ShowSensors          On
            ShowWayptMarkers     Off
            ShowWayptTurnMarkers Off
            ShowOrbits           On
            ShowDtedRegions      On
            ShowAreaTgtCentroids On
            ShowToolBar          On
            ShowStatusBar        On
            ShowScrollBars       On
            AllowAnimUpdate      Off
            AccShowLine          On
            AccAnimHigh          On
            AccStatHigh          On
            ShowPrintButton      Off
            ShowAnimButtons      On
            ShowAnimModeButtons  On
            ShowZoomMsrButtons   On
            ShowMapCbButton      On
        END DisplayFlags

BEGIN WinFonts

    System
    MS Sans Serif,22,0,0
    MS Sans Serif,28,0,0

END WinFonts

BEGIN MapData

    Begin TerrainConverterData
           NorthLat        0.00000000000000e+000
           EastLon         0.00000000000000e+000
           SouthLat        0.00000000000000e+000
           WestLon         0.00000000000000e+000
           ColorByRGB      No
           AltsFromMSL     No
           UseColorRamp    Yes
           UseRegionMinMax Yes
           SizeSameAsSrc   Yes
           MinAltHSV       0.00000000000000e+000 7.00000000000000e-001 8.00000000000000e-001 4.00000000000000e-001
           MaxAltHSV       1.00000000000000e+006 0.00000000000000e+000 2.00000000000000e-001 1.00000000000000e+000
           SmoothColors    Yes
           CreateChunkTrn  Yes
           OutputFormat    TXM
    End TerrainConverterData

    DisableDefKbdActions     Off
    TextShadowStyle          None
    TextShadowColor          #000000
    BingLevelOfDetailScale   2.000000
    BEGIN Map
        MapNum         1
        TrackingMode   LatLon
        PickEnabled    On
        PanEnabled     On

        BEGIN MapAttributes
            PrimaryBody          Earth
            SecondaryBody        Sun
            CenterLatitude       38.495168
            CenterLongitude      127.063992
            ProjectionAltitude   63621860.000000
            FieldOfView          35.000000
            OrthoDisplayDistance 20000000.000000
            TransformTrajectory  On
            EquatorialRadius     6378137.000000
            BackgroundColor      #000000
            LatLonLines          On
            LatSpacing           2.000000
            LonSpacing           2.000000
            LatLonLineColor      #999999
            LatLonLineStyle      2
            ShowOrthoDistGrid    Off
            OrthoGridXSpacing    5
            OrthoGridYSpacing    5
            OrthoGridColor       #ffffff
            ShowImageExtents     Off
            ImageExtentLineColor #ffffff
            ImageExtentLineStyle 0
            ImageExtentLineWidth 1.000000
            ShowImageNames       Off
            ImageNameFont        0
            Projection           EquidistantCylindrical
            Resolution           High
            CoordinateSys        ECF
            UseBackgroundImage   Off
            UseBingForBackground Off
            BingType             Aerial
            BingLogoHorizAlign   Right
            BingLogoVertAlign    Bottom
            BackgroundImageFile  Basic.bmp
            UseNightLights       Off
            NightLightsFactor    3.500000
            UseCloudsFile        Off
            BEGIN ZoomLocations
                BEGIN ZoomLocation
                    CenterLat    0.000000
                    CenterLon    0.000000
                    ZoomWidth    359.999998
                    ZoomHeight   180.000000
                End ZoomLocation
                BEGIN ZoomLocation
                    CenterLat    35.773480
                    CenterLon    129.682997
                    ZoomWidth    37.348703
                    ZoomHeight   18.674351
                End ZoomLocation
                BEGIN ZoomLocation
                    CenterLat    36.468802
                    CenterLon    125.539687
                    ZoomWidth    24.077993
                    ZoomHeight   12.038996
                End ZoomLocation
                BEGIN ZoomLocation
                    CenterLat    38.495168
                    CenterLon    127.063992
                    ZoomWidth    9.297442
                    ZoomHeight   4.648721
                End ZoomLocation
            END ZoomLocations
            UseVarAspectRatio    No
            SwapMapResolution    Yes
            NoneToVLowSwapDist   2000000.000000
            VLowToLowSwapDist    20000.000000
            LowToMediumSwapDist  10000.000000
            MediumToHighSwapDist 5000.000000
            HighToVHighSwapDist  1000.000000
            VHighToSHighSwapDist 398.107171
            BEGIN Axes
                DisplayAxes no
                CoordSys    CBI
                2aryCB      Sun
                Display+x   yes
                Label+x     yes
                Color+x     #ffffff
                Scale+x     3.000000
                Display-x   yes
                Label-x     yes
                Color-x     #ffffff
                Scale-x     3.000000
                Display+y   yes
                Label+y     yes
                Color+y     #ffffff
                Scale+y     3.000000
                Display-y   yes
                Label-y     yes
                Color-y     #ffffff
                Scale-y     3.000000
                Display+z   yes
                Label+z     yes
                Color+z     #ffffff
                Scale+z     3.000000
                Display-z   yes
                Label-z     yes
                Color-z     #ffffff
                Scale-z     3.000000
            END Axes

        END MapAttributes

        BEGIN MapList
            BEGIN Detail
                Alias RWDB2_Coastlines
                Show Yes
                Color #8fbc8f
            END Detail
            BEGIN Detail
                Alias RWDB2_International_Borders
                Show No
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_Islands
                Show Yes
                Color #8fbc8f
            END Detail
            BEGIN Detail
                Alias RWDB2_Lakes
                Show No
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_Provincial_Borders
                Show No
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_Rivers
                Show No
                Color #00ff00
            END Detail
        END MapList


        BEGIN MapAnnotations
        END MapAnnotations

        BEGIN DisplayFlags
            ShowLabels           On
            ShowPassLabel        Off
            ShowElsetNum         Off
            ShowGndTracks        On
            ShowGndMarkers       On
            ShowOrbitMarkers     On
            ShowPlanetOrbits     Off
            ShowPlanetCBIPos     On
            ShowPlanetCBILabel   On
            ShowPlanetGndPos     On
            ShowPlanetGndLabel   On
            ShowSensors          On
            ShowWayptMarkers     Off
            ShowWayptTurnMarkers Off
            ShowOrbits           On
            ShowDtedRegions      On
            ShowAreaTgtCentroids On
            ShowToolBar          On
            ShowStatusBar        On
            ShowScrollBars       On
            AllowAnimUpdate      Off
            AccShowLine          On
            AccAnimHigh          On
            AccStatHigh          On
            ShowPrintButton      Off
            ShowAnimButtons      On
            ShowAnimModeButtons  On
            ShowZoomMsrButtons   On
            ShowMapCbButton      On
        END DisplayFlags

        BEGIN SoftVTR
            OutputFormat     BMP
            Directory        C:\TEMP
            BaseName         Frame
            Digits           4
            Frame            0
            LastAnimTime     0.000000
            OutputMode       Normal
            HiResAssembly    Assemble
            HRWidth          6000
            HRHeight         4500
            HRDPI            600.000000
            UseSnapInterval  No
            SnapInterval     0.000000
            WmvCodec         "Windows Media Video 9"
            Framerate        30
            Bitrate          3000000
        END SoftVTR


        BEGIN TimeDisplay
            Show             0
            TextColor        #00ffff
            TextTranslucency 0.000000
            ShowBackground   0
            BackColor        #000000
            BackTranslucency 0.400000
            XPosition        20
            YPosition        -20
        END TimeDisplay

        BEGIN LightingData
            DisplayAltitude              0.000000
            SubsolarPoint                Off
            SubsolarPointColor           #ffff00
            SubsolarPointMarkerStyle     2

            ShowUmbraLine                Off
            UmbraLineColor               #ffff00
            UmbraLineStyle               0
            UmbraLineWidth               1
            FillUmbra                    Off
            UmbraFillColor               #000000
            ShowSunlightLine             Off
            SunlightLineColor            #ffff00
            SunlightLineStyle            0
            SunlightLineWidth            1
            FillSunlight                 Off
            SunlightFillColor            #ffff00
            SunlightMinOpacity           0.000000
            SunlightMaxOpacity           0.200000
            UmbraMaxOpacity              0.700000
            UmbraMinOpacity              0.400000
        END LightingData
    END Map

END MapData

        BEGIN GfxClassPref

        END GfxClassPref


        BEGIN ConnectGraphicsOptions

            AsyncPickReturnUnique          OFF

        END ConnectGraphicsOptions

    END Graphics
    
    BEGIN Overlays
    END Overlays
    
    BEGIN Broker
    END Broker
    
    BEGIN ClsApp
		RangeConstraint         5000.000
		ApoPeriPad              30000.000
		OrbitPathPad            30000.000
		TimeDistPad             30000.000
		OutOfDate               2592000.000
		MaxApoPeriStep          900.000
		ApoPeriAngle            0.785
		UseApogeePerigeeFilter  Yes
		UsePathFilter           Yes
		UseTimeFilter           Yes
		UseOutOfDate            Yes
		CreateSats              No
		MaxSatsToCreate         500
		UseModelScale           No
		ModelScale              4.000
		UseCrossRefDb           Yes
		CollisionDB                     stkAllTLE.tce
		CollisionCrossRefDB             stkAllTLE.sd
		ShowLine                Yes
		AnimHighlight           Yes
		StaticHighlight         Yes
		UseLaunchWindow                         No
		LaunchWindowUseEntireTraj               Yes
		LaunchWindowTrajMETStart                0.000
		LaunchWindowTrajMETStop                 900.000
		LaunchWindowStart                       0.000
		LaunchWindowStop                        0.000
		LaunchMETOffset                         0.000
		LaunchWindowUseSecEphem                 No 
		LaunchWindowUseScenFolderForSecEphem    Yes
		LaunchWindowUsePrimEphem                No 
		LaunchWindowUseScenFolderForPrimEphem   Yes
    LaunchWindowIntervalPtr
	BEGIN	EVENTINTERVAL
			BEGIN Interval
				Start	4 Jul 2000 00:00:00.000000000
				Stop	6 Jul 2000 00:00:01.000000000
			END Interval
			IntervalState	Explicit
	END	EVENTINTERVAL

		LaunchWindowUsePrimMTO                  No 
		GroupLaunches                           No 
		LWTimeConvergence                       1.000e-003
		LWRelValueConvergence                   1.000e-008
		LWTSRTimeConvergence                    1.000e-004
		LWTSRRelValueConvergence                1.000e-010
		LaunchWindowStep                        300.000
		MaxTSRStep                              180.000
		MaxTSRRelMotion                         20.000
		UseLaunchArea                           No 
		LaunchAreaOrientation                   North
		LaunchAreaAzimuth                       0.000
		LaunchAreaXLimits                       -10000.000   10000.000
		LaunchAreaYLimits                       -10000.000   10000.000
		LaunchAreaNumXIntrPnts                  1
		LaunchAreaNumYIntrPnts                  1
		LaunchAreaAltReference                  Ellipsoid
		TargetSameStop                          No 
		SkipSurfaceMetric                       No 
		LWAreaTSRRelValueConvergence            1.000e-010
		AreaLaunchWindowStep                    300.000
		AreaMaxTSRStep                          30.000
		AreaMaxTSRRelMotion                     1.000
		ShowLaunchArea                          No 
		ShowBlackoutTracks                      No 
		BlackoutColor                           #ff0000
		ShowClearedTracks                       No 
		UseObjectForClearedColor                No 
		ClearedColor                             #ffffff
		ShowTracksSegments                      No 
		ShowMinRangeTracks                      No 
		MinRangeTrackTimeStep                   0.500000
		UsePrimStepForTracks                    Yes
		GfxTracksTimeStep                       30.000
		GfxAreaNumXIntrPnts                     1
		GfxAreaNumYIntrPnts                     1
		CreateLaunchMTO                         No 
		CovarianceSigmaScale                    3.000
		CovarianceMode                          None 
    END ClsApp
    
    BEGIN Units
		DistanceUnit		NauticalMiles
		TimeUnit		Seconds
		DateFormat		GregorianUTC
		AngleUnit		Degrees
		MassUnit		Kilograms
		PowerUnit		dBW
		FrequencyUnit		Gigahertz
		SmallDistanceUnit		Meters
		LatitudeUnit		Degrees
		LongitudeUnit		Degrees
		DurationUnit		Hr:Min:Sec
		Temperature		Kelvin
		SmallTimeUnit		Seconds
		RatioUnit		Decibel
		RcsUnit		Decibel
		DopplerVelocityUnit		MetersperSecond
		SARTimeResProdUnit		Meter-Second
		ForceUnit		Newtons
		PressureUnit		Pascals
		SpecificImpulseUnit		Seconds
		PRFUnit		Kilohertz
		BandwidthUnit		Megahertz
		SmallVelocityUnit		CentimetersperSecond
		Percent		Percentage
		MissionModelerDistanceUnit		NauticalMiles
		MissionModelerTimeUnit		Hours
		MissionModelerAltitudeUnit		Feet
		MissionModelerFuelQuantityUnit		Pounds
		MissionModelerRunwayLengthUnit		Kilofeet
		MissionModelerBearingAngleUnit		Degrees
		MissionModelerAngleOfAttackUnit		Degrees
		MissionModelerAttitudeAngleUnit		Degrees
		MissionModelerGUnit		StandardSeaLevelG
		SolidAngle		Steradians
		MissionModelerTSFCUnit		TSFCLbmHrLbf
		MissionModelerPSFCUnit		PSFCLbmHrHp
		MissionModelerForceUnit		Pounds
		MissionModelerPowerUnit		Horsepower
		SpectralBandwidthUnit		Hertz
		BitsUnit		MegaBits
		MagneticFieldUnit		nanoTesla
    END Units
    
    BEGIN ReportUnits
		DistanceUnit		Kilometers
		TimeUnit		Seconds
		DateFormat		GregorianUTC
		AngleUnit		Degrees
		MassUnit		Kilograms
		PowerUnit		dBW
		FrequencyUnit		Gigahertz
		SmallDistanceUnit		Meters
		LatitudeUnit		Degrees
		LongitudeUnit		Degrees
		DurationUnit		Hr:Min:Sec
		Temperature		Kelvin
		SmallTimeUnit		Seconds
		RatioUnit		Decibel
		RcsUnit		Decibel
		DopplerVelocityUnit		MetersperSecond
		SARTimeResProdUnit		Meter-Second
		ForceUnit		Newtons
		PressureUnit		Pascals
		SpecificImpulseUnit		Seconds
		PRFUnit		Kilohertz
		BandwidthUnit		Megahertz
		SmallVelocityUnit		CentimetersperSecond
		Percent		Percentage
		MissionModelerDistanceUnit		NauticalMiles
		MissionModelerTimeUnit		Hours
		MissionModelerAltitudeUnit		Feet
		MissionModelerFuelQuantityUnit		Pounds
		MissionModelerRunwayLengthUnit		Kilofeet
		MissionModelerBearingAngleUnit		Degrees
		MissionModelerAngleOfAttackUnit		Degrees
		MissionModelerAttitudeAngleUnit		Degrees
		MissionModelerGUnit		StandardSeaLevelG
		SolidAngle		Steradians
		MissionModelerTSFCUnit		TSFCLbmHrLbf
		MissionModelerPSFCUnit		PSFCLbmHrHp
		MissionModelerForceUnit		Pounds
		MissionModelerPowerUnit		Horsepower
		SpectralBandwidthUnit		Hertz
		BitsUnit		MegaBits
		MagneticFieldUnit		nanoTesla
    END ReportUnits
    
    BEGIN ConnectReportUnits
		DistanceUnit		NauticalMiles
		TimeUnit		Seconds
		DateFormat		GregorianUTC
		AngleUnit		Degrees
		MassUnit		Kilograms
		PowerUnit		dBW
		FrequencyUnit		Gigahertz
		SmallDistanceUnit		Meters
		LatitudeUnit		Degrees
		LongitudeUnit		Degrees
		DurationUnit		Hr:Min:Sec
		Temperature		Kelvin
		SmallTimeUnit		Seconds
		RatioUnit		Decibel
		RcsUnit		Decibel
		DopplerVelocityUnit		MetersperSecond
		SARTimeResProdUnit		Meter-Second
		ForceUnit		Newtons
		PressureUnit		Pascals
		SpecificImpulseUnit		Seconds
		PRFUnit		Kilohertz
		BandwidthUnit		Megahertz
		SmallVelocityUnit		CentimetersperSecond
		Percent		Percentage
		MissionModelerDistanceUnit		NauticalMiles
		MissionModelerTimeUnit		Hours
		MissionModelerAltitudeUnit		Feet
		MissionModelerFuelQuantityUnit		Pounds
		MissionModelerRunwayLengthUnit		Kilofeet
		MissionModelerBearingAngleUnit		Degrees
		MissionModelerAngleOfAttackUnit		Degrees
		MissionModelerAttitudeAngleUnit		Degrees
		MissionModelerGUnit		StandardSeaLevelG
		SolidAngle		Steradians
		MissionModelerTSFCUnit		TSFCLbmHrLbf
		MissionModelerPSFCUnit		PSFCLbmHrHp
		MissionModelerForceUnit		Pounds
		MissionModelerPowerUnit		Horsepower
		SpectralBandwidthUnit		Hertz
		BitsUnit		MegaBits
		MagneticFieldUnit		nanoTesla
    END ConnectReportUnits
    
    BEGIN ADFFileData
    END ADFFileData
    
    BEGIN GenDb

		BEGIN Database
		    DbType       Satellite
		    DefDb        stkSatDb.sd
		    UseMyDb      Off
		    MyDb         .\stkSatDb.sd
		    MaxMatches   2000

		END Database

		BEGIN Database
		    DbType       City
		    DefDb        stkAllCitiesDb.cd
		    UseMyDb      Off
		    MyDb         \
		    MaxMatches   2000

		END Database

		BEGIN Database
		    DbType       Facility
		    DefDb        stkFacility.fd
		    UseMyDb      Off
		    MyDb         .\stkFacility.fd
		    MaxMatches   2000

		END Database
    END GenDb
    
    BEGIN Msgp4Ext
    END Msgp4Ext
    
    BEGIN FileLocations
    END FileLocations
    
    BEGIN Author
	Optimize	Yes
	UseBasicGlobe	Yes
	ReadOnly	No
	ViewerPassword	No
	STKPassword	No
	ExcludeInstallFiles	No
	BEGIN ExternalFileList
	END ExternalFileList
    END Author
    
    BEGIN ExportDataFile
    FileType         Ephemeris
    Directory        C:\Source\STKXExamples\Public\Samples\Scenario\ISR\
    IntervalType    Ephemeris
    TimePeriodStart  0.000000e+000
    TimePeriodStop   0.000000e+000
    StepType         Ephemeris
    StepSize         60.000000
    EphemType        STK
    UseVehicleCentralBody   Yes
    CentralBody      Earth
    SatelliteID      -200000
    CoordSys         J2000
    NonSatCoordSys   Fixed
    InterpolateBoundaries  Yes
    EphemFormat      Current
    InterpType       9
    InterpOrder      5
    AttCoordSys      Fixed
    Quaternions      0
    ExportCovar      Position
    AttitudeFormat   Current
    TimePrecision      6
    CCSDSDateFormat    YMD
    CCSDSEphFormat     SciNotation
    CCSDSTimeSystem    UTC
    CCSDSRefFrame      ICRF
    UseSatCenterAndFrame   No
    END ExportDataFile
    
    BEGIN Desc
    END Desc
    
    BEGIN RfEnv
<!-- STKv4.0 Format="XML" -->
<STKOBJECT>
<OBJECT Class = "RFEnvironment" Name = "STK_RF_Environment">
    <OBJECT Class = "link" Name = "ActiveCommSystem">
        <OBJECT Class = "string" Name = ""> &quot;None&quot; </OBJECT>
    </OBJECT>
    <OBJECT Class = "string" Name = "Category"> &quot;&quot; </OBJECT>
    <OBJECT Class = "bool" Name = "Clonable"> True </OBJECT>
    <OBJECT Class = "string" Name = "Description"> &quot;STK RF Environment&quot; </OBJECT>
    <OBJECT Class = "double" Name = "EarthTemperature"> 290 K </OBJECT>
    <OBJECT Class = "link" Name = "PropagationChannel">
        <OBJECT Class = "PropagationChannel" Name = "RF_Propagation_Channel">
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
            <OBJECT Class = "string" Name = "Category"> &quot;@Top&quot; </OBJECT>
            <OBJECT Class = "bool" Name = "Clonable"> True </OBJECT>
            <OBJECT Class = "link" Name = "CloudFogModel">
                <OBJECT Class = "CloudFogLossModel" Name = "ITU_840-3">
                    <OBJECT Class = "string" Name = "Category"> &quot;@Top&quot; </OBJECT>
                    <OBJECT Class = "bool" Name = "Clonable"> True </OBJECT>
                    <OBJECT Class = "double" Name = "CloudCeiling"> 3000 m </OBJECT>
                    <OBJECT Class = "double" Name = "CloudLayerThickness"> 500 m </OBJECT>
                    <OBJECT Class = "double" Name = "CloudLiqWaterDensity"> 7.5 g*m^-3 </OBJECT>
                    <OBJECT Class = "double" Name = "CloudTemp"> 273.15 K </OBJECT>
                    <OBJECT Class = "string" Name = "Description"> &quot;ITU 840-3&quot; </OBJECT>
                    <OBJECT Class = "bool" Name = "ReadOnly"> False </OBJECT>
                    <OBJECT Class = "string" Name = "Type"> &quot;ITU 840-3&quot; </OBJECT>
                    <OBJECT Class = "string" Name = "UserComment"> &quot;ITU 840-3&quot; </OBJECT>
                    <OBJECT Class = "string" Name = "Version"> &quot;1.0.0 a&quot; </OBJECT>
                </OBJECT>
            </OBJECT>
            <OBJECT Class = "string" Name = "Description"> &quot;RF Propagation Channel&quot; </OBJECT>
            <OBJECT Class = "link" Name = "RainModel">
                <OBJECT Class = "RainLossModel" Name = "ITU-R_P618-9">
                    <OBJECT Class = "string" Name = "Category"> &quot;Previous Versions&quot; </OBJECT>
                    <OBJECT Class = "bool" Name = "Clonable"> True </OBJECT>
                    <OBJECT Class = "string" Name = "Description"> &quot;ITU-R P618-9 rain model&quot; </OBJECT>
                    <OBJECT Class = "bool" Name = "ReadOnly"> False </OBJECT>
                    <OBJECT Class = "double" Name = "SurfaceTemperature"> 273.15 K </OBJECT>
                    <OBJECT Class = "string" Name = "Type"> &quot;ITU-R P618-9&quot; </OBJECT>
                    <OBJECT Class = "string" Name = "UserComment"> &quot;ITU-R P618-9 rain model&quot; </OBJECT>
                    <OBJECT Class = "string" Name = "Version"> &quot;1.0.0 a&quot; </OBJECT>
                </OBJECT>
            </OBJECT>
            <OBJECT Class = "bool" Name = "ReadOnly"> False </OBJECT>
            <OBJECT Class = "link" Name = "TropoScintModel">
                <OBJECT Class = "TropoScintLossModel" Name = "ITU_618-8_Scintillation">
                    <OBJECT Class = "string" Name = "Category"> &quot;@Top&quot; </OBJECT>
                    <OBJECT Class = "bool" Name = "Clonable"> True </OBJECT>
                    <OBJECT Class = "bool" Name = "ComputeDeepFade"> False </OBJECT>
                    <OBJECT Class = "string" Name = "Description"> &quot;ITU 618-8 Scintillation&quot; </OBJECT>
                    <OBJECT Class = "double" Name = "FadeOutage"> 0.001 unitValue </OBJECT>
                    <OBJECT Class = "double" Name = "PercentTimeRefracGrad"> 0.1 unitValue </OBJECT>
                    <OBJECT Class = "bool" Name = "ReadOnly"> False </OBJECT>
                    <OBJECT Class = "double" Name = "SurfaceTemperature"> 273.15 K </OBJECT>
                    <OBJECT Class = "string" Name = "Type"> &quot;ITU 618-8 Scintillation&quot; </OBJECT>
                    <OBJECT Class = "string" Name = "UserComment"> &quot;ITU 618-8 Scintillation&quot; </OBJECT>
                    <OBJECT Class = "string" Name = "Version"> &quot;1.0.0 a&quot; </OBJECT>
                </OBJECT>
            </OBJECT>
            <OBJECT Class = "string" Name = "Type"> &quot;RF Propagation Channel&quot; </OBJECT>
            <OBJECT Class = "link" Name = "UrbanTerresPropLossModel">
                <OBJECT Class = "UrbanTerrestrialPropagationLossModel" Name = "Two_Ray">
                    <OBJECT Class = "string" Name = "Category"> &quot;&quot; </OBJECT>
                    <OBJECT Class = "bool" Name = "Clonable"> True </OBJECT>
                    <OBJECT Class = "string" Name = "Description"> &quot;Two Ray (Fourth Power Law) atmospheric absorption model&quot; </OBJECT>
                    <OBJECT Class = "double" Name = "LossFactor"> 1 &quot;&quot; </OBJECT>
                    <OBJECT Class = "bool" Name = "ReadOnly"> False </OBJECT>
                    <OBJECT Class = "double" Name = "SurfaceTemperature"> 273.15 K </OBJECT>
                    <OBJECT Class = "string" Name = "Type"> &quot;Two Ray&quot; </OBJECT>
                    <OBJECT Class = "string" Name = "UserComment"> &quot;Two Ray (Fourth Power Law) atmospheric absorption model&quot; </OBJECT>
                    <OBJECT Class = "string" Name = "Version"> &quot;1.0.0 a&quot; </OBJECT>
                </OBJECT>
            </OBJECT>
            <OBJECT Class = "bool" Name = "UseAtmosAbsorptionModel"> False </OBJECT>
            <OBJECT Class = "bool" Name = "UseCloudFogModel"> False </OBJECT>
            <OBJECT Class = "bool" Name = "UseCustomA"> False </OBJECT>
            <OBJECT Class = "bool" Name = "UseCustomB"> False </OBJECT>
            <OBJECT Class = "bool" Name = "UseCustomC"> False </OBJECT>
            <OBJECT Class = "bool" Name = "UseRainModel"> False </OBJECT>
            <OBJECT Class = "string" Name = "UserComment"> &quot;RF Propagation Channel&quot; </OBJECT>
            <OBJECT Class = "bool" Name = "UseTropoScintModel"> False </OBJECT>
            <OBJECT Class = "bool" Name = "UseUrbanTerresPropLossModel"> False </OBJECT>
            <OBJECT Class = "string" Name = "Version"> &quot;1.0.0 a&quot; </OBJECT>
        </OBJECT>
    </OBJECT>
    <OBJECT Class = "double" Name = "RainOutagePercent"> 0.1 &quot;&quot; </OBJECT>
    <OBJECT Class = "bool" Name = "ReadOnly"> False </OBJECT>
    <OBJECT Class = "string" Name = "Type"> &quot;STK RF Environment&quot; </OBJECT>
    <OBJECT Class = "string" Name = "UserComment"> &quot;STK RF Environment&quot; </OBJECT>
    <OBJECT Class = "string" Name = "Version"> &quot;1.0.0 a&quot; </OBJECT>
</OBJECT>
</STKOBJECT>
    END RfEnv
    
    BEGIN CommRad
    END CommRad
    
    BEGIN RCS
	Inherited          False
	LinearClutterCoef        1.000000e+000
	BEGIN RCSBAND
		LinearConstantValue      1.000000e+000
		Swerling      0
		BandData      3.000000e+006 3.000000e+011
		BandData      2.997920e+006 3.000000e+006
	END RCSBAND
    END RCS
    
    BEGIN Gator
    END Gator
    
    BEGIN Crdn
    END Crdn
    
    BEGIN SpiceExt
    END SpiceExt
    
    BEGIN FlightScenExt
    END FlightScenExt
    
    BEGIN DIS

		Begin General

			Verbose                    Off
			Processing                 Off
			Statistics                 Off
			ExerciseID                 -1
			ForceID                    -1

		End General


		Begin Output

			Version                    5
			ExerciseID                 1
			forceID                    1
			HeartbeatTimer             5.000000
			DistanceThresh             1.000000
			OrientThresh               3.000000

		End Output


		Begin Time

			Mode                       rtPDUTimestamp

		End Time


		Begin PDUInfo


		End PDUInfo


		Begin Parameters

			ParmData  COLORFRIENDLY        blue
			ParmData  COLORNEUTRAL         white
			ParmData  COLOROPFORCE         red
			ParmData  MAXDRELSETS          10000

		End Parameters


		Begin Network

			NetIF                      Default
			Mode                       Broadcast
			McastIP                    224.0.0.1
			Port                       3000
			rChannelBufferSize         65000
			ReadBufferSize             500
			QueuePollPeriod            100
			MaxRcvQueueEntries         4000
			MaxRcvIOThreads            4
			sChannelBufferSize         65000

		End Network


		Begin EntityTypeDef


#			order: kind:domain:country:catagory:subCatagory:specific:xtra ( -1 = * )

			1:1:-1:-1:-1:-1:-1		GroundVehicle
			1:2:-1:-1:-1:-1:-1		Aircraft
			1:2:222:1:2:-1:-1		Aircraft	"MIG29.mdl"
			1:2:222:1:5:-1:-1		Aircraft	"MIG21.mdl"
			1:2:225:1:-1:-1:-1		Aircraft	"F16.mdl"
			1:2:225:1:15:-1:-1		Aircraft	"F18.mdl"
			1:2:225:7:2:-1:-1		Aircraft	"UH60.mdl"
			1:2:225:20:-1:-1:-1		Aircraft	"UH60.mdl"
			1:3:-1:-1:-1:-1:-1		Ship
			1:3:225:1:-1:-1:-1		Ship	"USSCARLVINSON.mdl"
			1:3:225:2:-1:-1:-1		Ship	"CRUISER.mdl"
			1:3:225:3:-1:-1:-1		Ship	"AEGIS.mdl"
			1:5:-1:-1:-1:-1:-1		Satellite
			1:5:225:1:1:-1:-1		Satellite	"SHUTTLE.mdl"
			1:5:225:2:1:2:-1		Satellite	"DSCSIII.mdl"
			1:5:225:2:1:4:-1		Satellite	"MILSTAR.mdl"
			1:5:225:2:1:6:-1		Satellite	"TDRS.mdl"
			1:5:225:2:2:2:-1		Satellite	"GPS.mdl"
			2:-1:-1:-1:-1:-1:-1		Missile
			2:9:-1:-1:-1:-1:-1		Missile	"PEGASUS.mdl"
			5:-1:-1:-1:-1:-1:-1		GroundVehicle	"Facility.mdl"

		End EntityTypeDef


		Begin EntityFilter
			Include                    *:*:*
		End EntityFilter

    END DIS
    
    BEGIN VO
    END VO

END Extensions

BEGIN SubObjects

Class Aircraft

	AWACS
	Globalhawk
	Guardrail
	Ikonos_Target_1
	Ikonos_Target_1A
	Ikonos_Target_2
	JSTARS
	RC-135
	U-2

END Class

Class AreaTarget

	North_Korea
	South_Korea_1
	South_Korea_2

END Class

Class Chain

	AWACS_to_all_AC
	Globalhawk_to_GPS
	Globalhawk_to_Threats
	Guardrail_to_Threats
	JSTARS_to_Threats
	Osan_to_AWACS
	RC-135_to_AWACS
	RC-135_to_Threats
	U-2_to_Threats

END Class

Class Constellation

	Globalhawk
	GPS
	Guardrail
	JSTARS
	RC-135
	Threats

END Class

Class Facility

	Camera_Target_1
	Camera_Target_2
	Camera_Target_3
	DMZ
	KCOIC
	Munson_Corridor
	Okinawa
	Osan
	threat_1
	threat_2
	threat_3
	Wonsan

END Class

Class Satellite

	GPS_2-02
	GPS_2-03
	GPS_2-04
	GPS_2-05
	GPS_2-06
	GPS_2-07
	GPS_2-08
	GPS_2-09
	GPS_2-10
	GPS_2-11
	GPS_2-12
	GPS_2-13
	GPS_2-14
	GPS_2-15
	GPS_2-16
	GPS_2-17
	GPS_2-18
	GPS_2-19
	GPS_2-20
	GPS_2-21
	GPS_2-22
	GPS_2-23
	GPS_2-24
	GPS_2-25
	GPS_2-26
	GPS_2-27
	GPS_2-28
	IKONOS
	INMARSAT_2-F1
	INMARSAT_2-F2
	INMARSAT_2-F4

END Class

END SubObjects

BEGIN References
    Instance *
        *
        Chain/AWACS_to_all_AC
        Chain/Globalhawk_to_GPS
        Chain/Globalhawk_to_Threats
        Chain/Guardrail_to_Threats
        Chain/JSTARS_to_Threats
        Chain/Osan_to_AWACS
        Chain/RC-135_to_AWACS
        Chain/RC-135_to_Threats
        Chain/U-2_to_Threats
        Constellation/GPS
        Constellation/Globalhawk
        Constellation/Guardrail
        Constellation/JSTARS
        Constellation/RC-135
        Constellation/Threats
    END Instance
    Instance Aircraft/AWACS
        *
        Aircraft/AWACS
        Aircraft/AWACS/Sensor/Radar_Down
        Aircraft/AWACS/Sensor/Radar_Up
        Aircraft/AWACS/Sensor/Scanning_Beam
        Aircraft/AWACS/Sensor/to_Guardrail
        Aircraft/AWACS/Sensor/to_JSTARS
        Aircraft/AWACS/Sensor/to_RC-135
        Aircraft/AWACS/Sensor/to_U-2
        Aircraft/RC-135/Sensor/to_AWACS
        Chain/AWACS_to_all_AC
        Chain/Osan_to_AWACS
        Chain/RC-135_to_AWACS
        Facility/Osan/Sensor/to_AWACS
    END Instance
    Instance Aircraft/AWACS/Sensor/Radar_Down
        Aircraft/AWACS/Sensor/Radar_Down
    END Instance
    Instance Aircraft/AWACS/Sensor/Radar_Up
        Aircraft/AWACS/Sensor/Radar_Up
    END Instance
    Instance Aircraft/AWACS/Sensor/Scanning_Beam
        Aircraft/AWACS/Sensor/Scanning_Beam
    END Instance
    Instance Aircraft/AWACS/Sensor/to_Guardrail
        Aircraft/AWACS/Sensor/to_Guardrail
    END Instance
    Instance Aircraft/AWACS/Sensor/to_JSTARS
        Aircraft/AWACS/Sensor/to_JSTARS
    END Instance
    Instance Aircraft/AWACS/Sensor/to_RC-135
        Aircraft/AWACS/Sensor/to_RC-135
    END Instance
    Instance Aircraft/AWACS/Sensor/to_U-2
        Aircraft/AWACS/Sensor/to_U-2
    END Instance
    Instance Aircraft/Globalhawk
        *
        Aircraft/Globalhawk
        Aircraft/Globalhawk/Sensor/GHawk_Main
        Aircraft/Globalhawk/Sensor/GHawk_Scanner_2
        Aircraft/Globalhawk/Sensor/GPS_FOV
        Aircraft/Globalhawk/Sensor/Ghawk_Scanner_1
        Aircraft/Globalhawk/Sensor/Ghawk_Scanner_3
        Aircraft/Globalhawk/Sensor/Ghawk_Scanner_4
        Aircraft/Globalhawk/Sensor/to_INMARSAT_2F4
        Chain/Globalhawk_to_GPS
        Constellation/Globalhawk
    END Instance
    Instance Aircraft/Globalhawk/Sensor/GHawk_Main
        Aircraft/Globalhawk/Sensor/GHawk_Main
        Constellation/Globalhawk
    END Instance
    Instance Aircraft/Globalhawk/Sensor/GHawk_Scanner_2
        Aircraft/Globalhawk/Sensor/GHawk_Scanner_2
    END Instance
    Instance Aircraft/Globalhawk/Sensor/GPS_FOV
        Aircraft/Globalhawk/Sensor/GPS_FOV
        Chain/Globalhawk_to_GPS
    END Instance
    Instance Aircraft/Globalhawk/Sensor/Ghawk_Scanner_1
        Aircraft/Globalhawk/Sensor/Ghawk_Scanner_1
    END Instance
    Instance Aircraft/Globalhawk/Sensor/Ghawk_Scanner_3
        Aircraft/Globalhawk/Sensor/Ghawk_Scanner_3
    END Instance
    Instance Aircraft/Globalhawk/Sensor/Ghawk_Scanner_4
        Aircraft/Globalhawk/Sensor/Ghawk_Scanner_4
    END Instance
    Instance Aircraft/Globalhawk/Sensor/to_INMARSAT_2F4
        Aircraft/Globalhawk/Sensor/to_INMARSAT_2F4
    END Instance
    Instance Aircraft/Guardrail
        *
        Aircraft/AWACS/Sensor/to_Guardrail
        Aircraft/Guardrail
        Aircraft/Guardrail/Sensor/Left_Side_Guardrail_Sensor
        Aircraft/Guardrail/Sensor/Right_Side_Guardrail_Sensor
        Chain/AWACS_to_all_AC
        Constellation/Guardrail
    END Instance
    Instance Aircraft/Guardrail/Sensor/Left_Side_Guardrail_Sensor
        Aircraft/Guardrail/Sensor/Left_Side_Guardrail_Sensor
        Constellation/Guardrail
    END Instance
    Instance Aircraft/Guardrail/Sensor/Right_Side_Guardrail_Sensor
        Aircraft/Guardrail/Sensor/Right_Side_Guardrail_Sensor
        Constellation/Guardrail
    END Instance
    Instance Aircraft/Ikonos_Target_1
        Aircraft/Ikonos_Target_1
        Satellite/IKONOS/Sensor/Ikonos_Camera
    END Instance
    Instance Aircraft/Ikonos_Target_1A
        Aircraft/Ikonos_Target_1A
        Satellite/IKONOS
    END Instance
    Instance Aircraft/Ikonos_Target_2
        Aircraft/Ikonos_Target_2
        Satellite/IKONOS
        Satellite/IKONOS/Sensor/Ikonos_Camera
    END Instance
    Instance Aircraft/JSTARS
        *
        Aircraft/AWACS/Sensor/to_JSTARS
        Aircraft/JSTARS
        Aircraft/JSTARS/Sensor/JStars_Left_Side
        Aircraft/JSTARS/Sensor/JStars_Right_Side
        Chain/AWACS_to_all_AC
        Constellation/JSTARS
    END Instance
    Instance Aircraft/JSTARS/Sensor/JStars_Left_Side
        Aircraft/JSTARS/Sensor/JStars_Left_Side
        Constellation/JSTARS
    END Instance
    Instance Aircraft/JSTARS/Sensor/JStars_Right_Side
        Aircraft/JSTARS/Sensor/JStars_Right_Side
        Constellation/JSTARS
    END Instance
    Instance Aircraft/RC-135
        *
        Aircraft/AWACS/Sensor/to_RC-135
        Aircraft/RC-135
        Aircraft/RC-135/Sensor/RC-135_Left_Side
        Aircraft/RC-135/Sensor/RC-135_Right_Side
        Aircraft/RC-135/Sensor/to_AWACS
        Chain/AWACS_to_all_AC
        Chain/RC-135_to_AWACS
        Constellation/RC-135
    END Instance
    Instance Aircraft/RC-135/Sensor/RC-135_Left_Side
        Aircraft/RC-135/Sensor/RC-135_Left_Side
        Constellation/RC-135
    END Instance
    Instance Aircraft/RC-135/Sensor/RC-135_Right_Side
        Aircraft/RC-135/Sensor/RC-135_Right_Side
        Constellation/RC-135
    END Instance
    Instance Aircraft/RC-135/Sensor/to_AWACS
        Aircraft/RC-135/Sensor/to_AWACS
        Chain/RC-135_to_AWACS
    END Instance
    Instance Aircraft/U-2
        *
        Aircraft/AWACS/Sensor/to_U-2
        Aircraft/U-2
        Aircraft/U-2/Sensor/Camera_1
        Chain/U-2_to_Threats
    END Instance
    Instance Aircraft/U-2/Sensor/Camera_1
        Aircraft/U-2/Sensor/Camera_1
        Chain/U-2_to_Threats
    END Instance
    Instance AreaTarget/North_Korea
        AreaTarget/North_Korea
    END Instance
    Instance AreaTarget/South_Korea_1
        AreaTarget/South_Korea_1
    END Instance
    Instance AreaTarget/South_Korea_2
        AreaTarget/South_Korea_2
    END Instance
    Instance Chain/AWACS_to_all_AC
        Chain/AWACS_to_all_AC
    END Instance
    Instance Chain/Globalhawk_to_GPS
        Chain/Globalhawk_to_GPS
    END Instance
    Instance Chain/Globalhawk_to_Threats
        Chain/Globalhawk_to_Threats
    END Instance
    Instance Chain/Guardrail_to_Threats
        Chain/Guardrail_to_Threats
    END Instance
    Instance Chain/JSTARS_to_Threats
        Chain/JSTARS_to_Threats
    END Instance
    Instance Chain/Osan_to_AWACS
        Chain/Osan_to_AWACS
    END Instance
    Instance Chain/RC-135_to_AWACS
        Chain/RC-135_to_AWACS
    END Instance
    Instance Chain/RC-135_to_Threats
        Chain/RC-135_to_Threats
    END Instance
    Instance Chain/U-2_to_Threats
        Chain/U-2_to_Threats
    END Instance
    Instance Constellation/GPS
        Chain/Globalhawk_to_GPS
    END Instance
    Instance Constellation/Globalhawk
        Chain/Globalhawk_to_Threats
    END Instance
    Instance Constellation/Guardrail
        Chain/Guardrail_to_Threats
    END Instance
    Instance Constellation/JSTARS
        Chain/JSTARS_to_Threats
    END Instance
    Instance Constellation/RC-135
        Chain/RC-135_to_Threats
    END Instance
    Instance Constellation/Threats
        Chain/Globalhawk_to_Threats
        Chain/Guardrail_to_Threats
        Chain/JSTARS_to_Threats
        Chain/RC-135_to_Threats
        Chain/U-2_to_Threats
    END Instance
    Instance Facility/Camera_Target_1
        Facility/Camera_Target_1
        Satellite/IKONOS
        Satellite/IKONOS/Sensor/Ikonos_Camera
    END Instance
    Instance Facility/Camera_Target_2
        Facility/Camera_Target_2
        Satellite/IKONOS
        Satellite/IKONOS/Sensor/Ikonos_Camera
    END Instance
    Instance Facility/Camera_Target_3
        Facility/Camera_Target_3
        Satellite/IKONOS
        Satellite/IKONOS/Sensor/Ikonos_Camera
    END Instance
    Instance Facility/DMZ
        Constellation/Threats
        Facility/DMZ
    END Instance
    Instance Facility/KCOIC
        Facility/KCOIC
        Satellite/IKONOS/Sensor/Comm_Beam
    END Instance
    Instance Facility/Munson_Corridor
        Constellation/Threats
        Facility/Munson_Corridor
    END Instance
    Instance Facility/Okinawa
        Facility/Okinawa
        Satellite/IKONOS/Sensor/Comm_Beam
    END Instance
    Instance Facility/Osan
        Chain/Osan_to_AWACS
        Facility/Osan
        Facility/Osan/Sensor/to_AWACS
        Satellite/INMARSAT_2-F4/Sensor/to_Osan
    END Instance
    Instance Facility/Osan/Sensor/to_AWACS
        Chain/Osan_to_AWACS
        Facility/Osan/Sensor/to_AWACS
    END Instance
    Instance Facility/Wonsan
        *
        Constellation/Threats
        Facility/Wonsan
        Facility/Wonsan/Sensor/SA-5
    END Instance
    Instance Facility/Wonsan/Sensor/SA-5
        Facility/Wonsan/Sensor/SA-5
    END Instance
    Instance Facility/threat_1
        Constellation/Threats
        Facility/threat_1
        Facility/threat_1/Sensor/threat_1_SA-2
    END Instance
    Instance Facility/threat_1/Sensor/threat_1_SA-2
        Facility/threat_1/Sensor/threat_1_SA-2
    END Instance
    Instance Facility/threat_2
        Constellation/Threats
        Facility/threat_2
        Facility/threat_2/Sensor/threat_2_SA-2
    END Instance
    Instance Facility/threat_2/Sensor/threat_2_SA-2
        Facility/threat_2/Sensor/threat_2_SA-2
    END Instance
    Instance Facility/threat_3
        *
        Constellation/Threats
        Facility/threat_3
        Facility/threat_3/Sensor/threat_3_SA-3
    END Instance
    Instance Facility/threat_3/Sensor/threat_3_SA-3
        Facility/threat_3/Sensor/threat_3_SA-3
    END Instance
    Instance Satellite/GPS_2-02
        Constellation/GPS
        Satellite/GPS_2-02
    END Instance
    Instance Satellite/GPS_2-03
        Constellation/GPS
        Satellite/GPS_2-03
    END Instance
    Instance Satellite/GPS_2-04
        Constellation/GPS
        Satellite/GPS_2-04
    END Instance
    Instance Satellite/GPS_2-05
        Constellation/GPS
        Satellite/GPS_2-05
    END Instance
    Instance Satellite/GPS_2-06
        Constellation/GPS
        Satellite/GPS_2-06
    END Instance
    Instance Satellite/GPS_2-07
        Constellation/GPS
        Satellite/GPS_2-07
    END Instance
    Instance Satellite/GPS_2-08
        Constellation/GPS
        Satellite/GPS_2-08
    END Instance
    Instance Satellite/GPS_2-09
        Constellation/GPS
        Satellite/GPS_2-09
    END Instance
    Instance Satellite/GPS_2-10
        Constellation/GPS
        Satellite/GPS_2-10
    END Instance
    Instance Satellite/GPS_2-11
        Constellation/GPS
        Satellite/GPS_2-11
    END Instance
    Instance Satellite/GPS_2-12
        Constellation/GPS
        Satellite/GPS_2-12
    END Instance
    Instance Satellite/GPS_2-13
        Constellation/GPS
        Satellite/GPS_2-13
    END Instance
    Instance Satellite/GPS_2-14
        Constellation/GPS
        Satellite/GPS_2-14
    END Instance
    Instance Satellite/GPS_2-15
        Constellation/GPS
        Satellite/GPS_2-15
    END Instance
    Instance Satellite/GPS_2-16
        Constellation/GPS
        Satellite/GPS_2-16
    END Instance
    Instance Satellite/GPS_2-17
        Constellation/GPS
        Satellite/GPS_2-17
    END Instance
    Instance Satellite/GPS_2-18
        Constellation/GPS
        Satellite/GPS_2-18
    END Instance
    Instance Satellite/GPS_2-19
        Constellation/GPS
        Satellite/GPS_2-19
    END Instance
    Instance Satellite/GPS_2-20
        Constellation/GPS
        Satellite/GPS_2-20
    END Instance
    Instance Satellite/GPS_2-21
        Constellation/GPS
        Satellite/GPS_2-21
    END Instance
    Instance Satellite/GPS_2-22
        Constellation/GPS
        Satellite/GPS_2-22
    END Instance
    Instance Satellite/GPS_2-23
        Constellation/GPS
        Satellite/GPS_2-23
    END Instance
    Instance Satellite/GPS_2-24
        Constellation/GPS
        Satellite/GPS_2-24
    END Instance
    Instance Satellite/GPS_2-25
        Constellation/GPS
        Satellite/GPS_2-25
    END Instance
    Instance Satellite/GPS_2-26
        Constellation/GPS
        Satellite/GPS_2-26
    END Instance
    Instance Satellite/GPS_2-27
        Constellation/GPS
        Satellite/GPS_2-27
    END Instance
    Instance Satellite/GPS_2-28
        Constellation/GPS
        Satellite/GPS_2-28
    END Instance
    Instance Satellite/IKONOS
        *
        Satellite/IKONOS
        Satellite/IKONOS/Sensor/Comm_Beam
        Satellite/IKONOS/Sensor/Ikonos_Camera
    END Instance
    Instance Satellite/IKONOS/Sensor/Comm_Beam
        Satellite/IKONOS/Sensor/Comm_Beam
    END Instance
    Instance Satellite/IKONOS/Sensor/Ikonos_Camera
        Satellite/IKONOS/Sensor/Ikonos_Camera
    END Instance
    Instance Satellite/INMARSAT_2-F1
        Satellite/INMARSAT_2-F1
    END Instance
    Instance Satellite/INMARSAT_2-F2
        Satellite/INMARSAT_2-F2
    END Instance
    Instance Satellite/INMARSAT_2-F4
        Aircraft/Globalhawk/Sensor/to_INMARSAT_2F4
        Satellite/INMARSAT_2-F4
        Satellite/INMARSAT_2-F4/Sensor/to_Osan
    END Instance
    Instance Satellite/INMARSAT_2-F4/Sensor/to_Osan
        Satellite/INMARSAT_2-F4/Sensor/to_Osan
    END Instance
END References

END Scenario

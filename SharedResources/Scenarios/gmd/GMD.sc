stk.v.10.0
WrittenBy    STK_v10.0.0
BEGIN Scenario
    Name            GMD

BEGIN Epoch

    Epoch        1 Jun 2001 18:00:00.000000000
    SmartEpoch
	BEGIN	EVENT
			Epoch	1 Jun 2001 18:00:00.000000000
			EventEpoch
				BEGIN	EVENT
					Type	EVENT_LINKTO
					Name	AnalysisStartTime
				END	EVENT
			EpochState	Implicit
	END	EVENT


END Epoch

BEGIN Interval

Start                   1 Jun 2001 18:00:00.000000000
Stop                    2 Jun 2001 18:00:00.000000000
    SmartInterval
	BEGIN	EVENTINTERVAL
			BEGIN Interval
				Start	1 Jun 2001 18:00:00.000000000
				Stop	2 Jun 2001 18:00:00.000000000
			END Interval
			IntervalState	Explicit
	END	EVENTINTERVAL

EpochUsesAnalStart      No
AnimStartUsesAnalStart  Yes
AnimStopUsesAnalStop    Yes

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
    Module    USGOVv10.0
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

    StartTime          1 Jun 2001 18:00:00.000000000
    EndTime            2 Jun 2001 18:00:00.000000000
    CurrentTime        1 Jun 2001 18:00:00.000000000
    Mode               Stop
    Direction          Forward
    UpdateDelta        60.000000
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
            ShowDtedRegions      Off
            ShowAreaTgtCentroids On
            ShowToolBar          On
            ShowStatusBar        On
            ShowScrollBars       On
            AllowAnimUpdate      Off
            AccShowLine          On
            AccAnimHigh          On
            AccStatHigh          On
            ShowPrintButton      On
            ShowAnimButtons      On
            ShowAnimModeButtons  On
            ShowZoomMsrButtons   On
            ShowMapCbButton      Off
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
            CenterLatitude       27.692308
            CenterLongitude      -50.644068
            ProjectionAltitude   63621860.000000
            FieldOfView          35.000000
            OrthoDisplayDistance 20000000.000000
            TransformTrajectory  On
            EquatorialRadius     6378137.000000
            BackgroundColor      #f0ffff
            LatLonLines          On
            LatSpacing           30.000000
            LonSpacing           30.000000
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
            Resolution           VeryLow
            CoordinateSys        ECF
            UseBackgroundImage   On
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
                    CenterLat    27.692308
                    CenterLon    -50.644067
                    ZoomWidth    157.423727
                    ZoomHeight   78.711864
                End ZoomLocation
            END ZoomLocations
            UseVarAspectRatio    No
            SwapMapResolution    Yes
            NoneToVLowSwapDist   2000000.000000
            VLowToLowSwapDist    5.011872
            LowToMediumSwapDist  5.011872
            MediumToHighSwapDist 5.011872
            HighToVHighSwapDist  5.011872
            VHighToSHighSwapDist 5.011872
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
            ShowDtedRegions      Off
            ShowAreaTgtCentroids On
            ShowToolBar          On
            ShowStatusBar        On
            ShowScrollBars       On
            AllowAnimUpdate      Off
            AccShowLine          On
            AccAnimHigh          On
            AccStatHigh          On
            ShowPrintButton      On
            ShowAnimButtons      On
            ShowAnimModeButtons  On
            ShowZoomMsrButtons   On
            ShowMapCbButton      Off
        END DisplayFlags

        BEGIN SoftVTR
            OutputFormat     BMP
            Directory        C:\Temp
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
            UmbraLineColor               #d2691e
            UmbraLineStyle               0
            UmbraLineWidth               2
            FillUmbra                    Off
            UmbraFillColor               #0000ff
            ShowSunlightLine             Off
            SunlightLineColor            #ffff00
            SunlightLineStyle            0
            SunlightLineWidth            2
            FillSunlight                 Off
            SunlightFillColor            #ffff00
            SunlightMinOpacity           0.000000
            SunlightMaxOpacity           0.200000
            UmbraMaxOpacity              0.700000
            UmbraMinOpacity              0.400000
        END LightingData
    END Map

    BEGIN MapStyles

        UseStyleTime        No

        BEGIN Style
        Name                No_Map_Bckgrnd
        Time                0.000000
        UpdateDelta         60.000000

        BEGIN MapAttributes
            PrimaryBody          Earth
            SecondaryBody        Sun
            CenterLatitude       0.000000
            CenterLongitude      0.000000
            ProjectionAltitude   63621860.000000
            FieldOfView          35.000000
            OrthoDisplayDistance 20000000.000000
            TransformTrajectory  On
            EquatorialRadius     6378137.000000
            BackgroundColor      #000000
            LatLonLines          On
            LatSpacing           30.000000
            LonSpacing           30.000000
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
            Resolution           VeryLow
            CoordinateSys        ECF
            UseBackgroundImage   Off
            UseBingForBackground Off
            BingType             Aerial
            BingLogoHorizAlign   Right
            BingLogoVertAlign    Bottom
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
            END ZoomLocations
            UseVarAspectRatio    No
            SwapMapResolution    Yes
            NoneToVLowSwapDist   2000000.000000
            VLowToLowSwapDist    20000.000000
            LowToMediumSwapDist  10000.000000
            MediumToHighSwapDist 5000.000000
            HighToVHighSwapDist  1000.000000
            VHighToSHighSwapDist 100.000000
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
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_International_Borders
                Show Yes
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_Islands
                Show Yes
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_Lakes
                Show Yes
                Color #87cefa
            END Detail
            BEGIN Detail
                Alias RWDB2_Provincial_Borders
                Show Yes
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

        BEGIN SoftVTR
            OutputFormat     BMP
            Directory        C:\Temp
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
            BackTranslucency 0.000000
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
            SunlightMinOpacity           0.100000
            SunlightMaxOpacity           0.500000
            UmbraMaxOpacity              0.500000
            UmbraMinOpacity              0.200000
        END LightingData

        ShowDtedRegions     Off

        End Style

        BEGIN Style
        Name                AGI1024_Map_Bckgrnd
        Time                0.000000
        UpdateDelta         60.000000

        BEGIN MapAttributes
            PrimaryBody          Earth
            SecondaryBody        Sun
            CenterLatitude       0.000000
            CenterLongitude      0.000000
            ProjectionAltitude   63621860.000000
            FieldOfView          35.000000
            OrthoDisplayDistance 20000000.000000
            TransformTrajectory  On
            EquatorialRadius     6378137.000000
            BackgroundColor      #000000
            LatLonLines          On
            LatSpacing           30.000000
            LonSpacing           30.000000
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
            Resolution           VeryLow
            CoordinateSys        ECF
            UseBackgroundImage   On
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
            END ZoomLocations
            UseVarAspectRatio    No
            SwapMapResolution    Yes
            NoneToVLowSwapDist   2000000.000000
            VLowToLowSwapDist    20000.000000
            LowToMediumSwapDist  10000.000000
            MediumToHighSwapDist 5000.000000
            HighToVHighSwapDist  1000.000000
            VHighToSHighSwapDist 100.000000
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
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_International_Borders
                Show Yes
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_Islands
                Show Yes
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_Lakes
                Show Yes
                Color #87cefa
            END Detail
            BEGIN Detail
                Alias RWDB2_Provincial_Borders
                Show Yes
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

        BEGIN SoftVTR
            OutputFormat     BMP
            Directory        C:\Temp
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
            BackTranslucency 0.000000
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
            SunlightMinOpacity           0.100000
            SunlightMaxOpacity           0.500000
            UmbraMaxOpacity              0.500000
            UmbraMinOpacity              0.200000
        END LightingData

        ShowDtedRegions     Off

        End Style

        BEGIN Style
        Name                Orthographic_Projection
        Time                0.000000
        UpdateDelta         60.000000

        BEGIN MapAttributes
            PrimaryBody          Earth
            SecondaryBody        Sun
            CenterLatitude       0.000000
            CenterLongitude      0.000000
            ProjectionAltitude   63621860.000000
            FieldOfView          35.000000
            OrthoDisplayDistance 20000000.000000
            TransformTrajectory  On
            EquatorialRadius     6378137.000000
            BackgroundColor      #000000
            LatLonLines          On
            LatSpacing           30.000000
            LonSpacing           30.000000
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
            Projection           Orthographic
            Resolution           VeryLow
            CoordinateSys        ECF
            UseBackgroundImage   Off
            UseBingForBackground Off
            BingType             Aerial
            BingLogoHorizAlign   Right
            BingLogoVertAlign    Bottom
            UseNightLights       Off
            NightLightsFactor    3.500000
            UseCloudsFile        Off
            BEGIN ZoomLocations
                BEGIN ZoomLocation
                    CenterLat    0.000000
                    CenterLon    0.000000
                    ZoomWidth    359.999990
                    ZoomHeight   180.000000
                End ZoomLocation
            END ZoomLocations
            UseVarAspectRatio    No
            SwapMapResolution    Yes
            NoneToVLowSwapDist   2000000.000000
            VLowToLowSwapDist    20000.000000
            LowToMediumSwapDist  10000.000000
            MediumToHighSwapDist 5000.000000
            HighToVHighSwapDist  1000.000000
            VHighToSHighSwapDist 100.000000
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
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_International_Borders
                Show Yes
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_Islands
                Show Yes
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_Lakes
                Show Yes
                Color #87cefa
            END Detail
            BEGIN Detail
                Alias RWDB2_Provincial_Borders
                Show Yes
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

        BEGIN SoftVTR
            OutputFormat     BMP
            Directory        C:\Temp
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
            BackTranslucency 0.000000
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
            SunlightMinOpacity           0.100000
            SunlightMaxOpacity           0.500000
            UmbraMaxOpacity              0.500000
            UmbraMinOpacity              0.200000
        END LightingData

        ShowDtedRegions     Off

        End Style

        BEGIN Style
        Name                Zoom_North_America
        Time                0.000000
        UpdateDelta         60.000000

        BEGIN MapAttributes
            PrimaryBody          Earth
            SecondaryBody        Sun
            CenterLatitude       0.000000
            CenterLongitude      0.000000
            ProjectionAltitude   63621860.000000
            FieldOfView          35.000000
            OrthoDisplayDistance 20000000.000000
            TransformTrajectory  On
            EquatorialRadius     6378137.000000
            BackgroundColor      #000000
            LatLonLines          On
            LatSpacing           30.000000
            LonSpacing           30.000000
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
            Resolution           Low
            CoordinateSys        ECF
            UseBackgroundImage   Off
            UseBingForBackground Off
            BingType             Aerial
            BingLogoHorizAlign   Right
            BingLogoVertAlign    Bottom
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
                    CenterLat    48.235294
                    CenterLon    -106.229508
                    ZoomWidth    129.836065
                    ZoomHeight   64.918032
                End ZoomLocation
            END ZoomLocations
            UseVarAspectRatio    No
            SwapMapResolution    Yes
            NoneToVLowSwapDist   2000000.000000
            VLowToLowSwapDist    20000.000000
            LowToMediumSwapDist  10000.000000
            MediumToHighSwapDist 5000.000000
            HighToVHighSwapDist  1000.000000
            VHighToSHighSwapDist 100.000000
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
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_International_Borders
                Show Yes
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_Islands
                Show Yes
                Color #00ff00
            END Detail
            BEGIN Detail
                Alias RWDB2_Lakes
                Show Yes
                Color #87cefa
            END Detail
            BEGIN Detail
                Alias RWDB2_Provincial_Borders
                Show Yes
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

        BEGIN SoftVTR
            OutputFormat     BMP
            Directory        C:\Temp
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
            BackTranslucency 0.000000
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
            SunlightMinOpacity           0.100000
            SunlightMaxOpacity           0.500000
            UmbraMaxOpacity              0.500000
            UmbraMinOpacity              0.200000
        END LightingData

        ShowDtedRegions     Off

        End Style

    END MapStyles

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
				Start	1 Jun 2001 18:00:00.000000000
				Stop	2 Jun 2001 18:00:00.000000000
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
		    DefDb        stkCityDb.cd
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
    Directory        C:\Source\STKXExamples\Public\Samples\Scenario\gmd\
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
			ParmData  MAXDRELSETS          1000

		End Parameters


		Begin Network

			NetIF                      Default
			Mode                       Broadcast
			McastIP                    224.0.0.1
			Port                       3000
			rChannelBufferSize         65000
			ReadBufferSize             500
			QueuePollPeriod            20
			MaxRcvQueueEntries         500
			MaxRcvIOThreads            4
			sChannelBufferSize         65000

		End Network


		Begin EntityTypeDef


#			order: kind:domain:country:catagory:subCatagory:specific:xtra ( -1 = * )


		End EntityTypeDef


		Begin EntityFilter
			Include                    *:*:*
		End EntityFilter

    END DIS
    
    BEGIN VO
    END VO

END Extensions

BEGIN SubObjects

Class Chain

	FylingdalesToRV1
	SensorsToMissile

END Class

Class Constellation

	dsp_and_Fylingdales
	Fylingdales
	Missile

END Class

Class Facility

	Beale
	CapeCod
	Clear
	CobraDane
	FortGreely
	Fylingdales
	KMR
	Thule

END Class

Class Missile

	TH1_l2i
	TH1_MSL1
	TH1_MSL2
	TH1_MSL3
	TH1_PBV3
	TH1_RV1
	TH1_RV2
	TH1_RV3
	TH1_SRD1
	TH1_STG1
	TH1_STG2
	TH1_STG3

END Class

Class Satellite

	dsp

END Class

END SubObjects

BEGIN References
    Instance *
        *
        Chain/FylingdalesToRV1
        Chain/SensorsToMissile
        Constellation/Fylingdales
        Constellation/Missile
        Constellation/dsp_and_Fylingdales
    END Instance
    Instance Chain/FylingdalesToRV1
        Chain/FylingdalesToRV1
    END Instance
    Instance Chain/SensorsToMissile
        Chain/SensorsToMissile
    END Instance
    Instance Constellation/Fylingdales
        Chain/FylingdalesToRV1
    END Instance
    Instance Constellation/Missile
        Chain/SensorsToMissile
    END Instance
    Instance Constellation/dsp_and_Fylingdales
        Chain/SensorsToMissile
    END Instance
    Instance Facility/Beale
        Facility/Beale
        Facility/Beale/Sensor/b_pavepaws_face1
        Facility/Beale/Sensor/b_pavepaws_face2
        Facility/Beale/Sensor/b_pavepaws_fence1
        Facility/Beale/Sensor/b_pavepaws_fence2
    END Instance
    Instance Facility/Beale/Sensor/b_pavepaws_face1
        Facility/Beale/Sensor/b_pavepaws_face1
    END Instance
    Instance Facility/Beale/Sensor/b_pavepaws_face2
        Facility/Beale/Sensor/b_pavepaws_face2
    END Instance
    Instance Facility/Beale/Sensor/b_pavepaws_fence1
        Facility/Beale/Sensor/b_pavepaws_fence1
    END Instance
    Instance Facility/Beale/Sensor/b_pavepaws_fence2
        Facility/Beale/Sensor/b_pavepaws_fence2
    END Instance
    Instance Facility/CapeCod
        Facility/CapeCod
        Facility/CapeCod/Sensor/cc_pavepaws_face1
        Facility/CapeCod/Sensor/cc_pavepaws_face2
        Facility/CapeCod/Sensor/cc_pavepaws_fence1
        Facility/CapeCod/Sensor/cc_pavepaws_fence2
    END Instance
    Instance Facility/CapeCod/Sensor/cc_pavepaws_face1
        Facility/CapeCod/Sensor/cc_pavepaws_face1
    END Instance
    Instance Facility/CapeCod/Sensor/cc_pavepaws_face2
        Facility/CapeCod/Sensor/cc_pavepaws_face2
    END Instance
    Instance Facility/CapeCod/Sensor/cc_pavepaws_fence1
        Facility/CapeCod/Sensor/cc_pavepaws_fence1
    END Instance
    Instance Facility/CapeCod/Sensor/cc_pavepaws_fence2
        Facility/CapeCod/Sensor/cc_pavepaws_fence2
    END Instance
    Instance Facility/Clear
        Facility/Clear
        Facility/Clear/Sensor/c_bmews_face1
        Facility/Clear/Sensor/c_bmews_face2
        Facility/Clear/Sensor/c_bmews_fence1
        Facility/Clear/Sensor/c_bmews_fence2
    END Instance
    Instance Facility/Clear/Sensor/c_bmews_face1
        Facility/Clear/Sensor/c_bmews_face1
    END Instance
    Instance Facility/Clear/Sensor/c_bmews_face2
        Facility/Clear/Sensor/c_bmews_face2
    END Instance
    Instance Facility/Clear/Sensor/c_bmews_fence1
        Facility/Clear/Sensor/c_bmews_fence1
    END Instance
    Instance Facility/Clear/Sensor/c_bmews_fence2
        Facility/Clear/Sensor/c_bmews_fence2
    END Instance
    Instance Facility/CobraDane
        Facility/CobraDane
        Facility/CobraDane/Sensor/cd_bmews_face1
    END Instance
    Instance Facility/CobraDane/Sensor/cd_bmews_face1
        Facility/CobraDane/Sensor/cd_bmews_face1
    END Instance
    Instance Facility/FortGreely
        Facility/FortGreely
    END Instance
    Instance Facility/Fylingdales
        Constellation/Fylingdales
        Constellation/dsp_and_Fylingdales
        Facility/Fylingdales
        Facility/Fylingdales/Sensor/box
        Facility/Fylingdales/Sensor/boxscan
        Facility/Fylingdales/Sensor/f_bmews_fence1
        Facility/Fylingdales/Sensor/f_bmews_fence2
        Facility/Fylingdales/Sensor/f_bmews_fence3
        Facility/Fylingdales/Sensor/f_tracking2_rv1
        Facility/Fylingdales/Sensor/f_tracking_rv1
    END Instance
    Instance Facility/Fylingdales/Sensor/box
        Facility/Fylingdales/Sensor/box
    END Instance
    Instance Facility/Fylingdales/Sensor/boxscan
        Facility/Fylingdales/Sensor/boxscan
    END Instance
    Instance Facility/Fylingdales/Sensor/f_bmews_fence1
        Facility/Fylingdales/Sensor/f_bmews_fence1
    END Instance
    Instance Facility/Fylingdales/Sensor/f_bmews_fence2
        Facility/Fylingdales/Sensor/f_bmews_fence2
    END Instance
    Instance Facility/Fylingdales/Sensor/f_bmews_fence3
        Facility/Fylingdales/Sensor/f_bmews_fence3
    END Instance
    Instance Facility/Fylingdales/Sensor/f_tracking2_rv1
        Constellation/Fylingdales
        Constellation/dsp_and_Fylingdales
        Facility/Fylingdales/Sensor/f_tracking2_rv1
    END Instance
    Instance Facility/Fylingdales/Sensor/f_tracking_rv1
        Constellation/Fylingdales
        Constellation/dsp_and_Fylingdales
        Facility/Fylingdales/Sensor/f_tracking_rv1
    END Instance
    Instance Facility/KMR
        Facility/KMR
    END Instance
    Instance Facility/Thule
        Facility/Thule
        Facility/Thule/Sensor/t_bmews_face1
        Facility/Thule/Sensor/t_bmews_face2
    END Instance
    Instance Facility/Thule/Sensor/t_bmews_face1
        Facility/Thule/Sensor/t_bmews_face1
    END Instance
    Instance Facility/Thule/Sensor/t_bmews_face2
        Facility/Thule/Sensor/t_bmews_face2
    END Instance
    Instance Missile/TH1_MSL1
        Constellation/Missile
        Missile/TH1_MSL1
    END Instance
    Instance Missile/TH1_MSL2
        Constellation/Missile
        Missile/TH1_MSL2
    END Instance
    Instance Missile/TH1_MSL3
        Constellation/Missile
        Missile/TH1_MSL3
    END Instance
    Instance Missile/TH1_PBV3
        Constellation/Missile
        Missile/TH1_PBV3
    END Instance
    Instance Missile/TH1_RV1
        Chain/FylingdalesToRV1
        Constellation/Missile
        Facility/Fylingdales/Sensor/f_tracking2_rv1
        Facility/Fylingdales/Sensor/f_tracking_rv1
        Missile/TH1_RV1
    END Instance
    Instance Missile/TH1_RV2
        Constellation/Missile
        Missile/TH1_RV2
    END Instance
    Instance Missile/TH1_RV3
        Constellation/Missile
        Missile/TH1_RV3
    END Instance
    Instance Missile/TH1_SRD1
        Constellation/Missile
        Missile/TH1_SRD1
    END Instance
    Instance Missile/TH1_STG1
        Constellation/Missile
        Missile/TH1_STG1
    END Instance
    Instance Missile/TH1_STG2
        Constellation/Missile
        Missile/TH1_STG2
    END Instance
    Instance Missile/TH1_STG3
        Constellation/Missile
        Missile/TH1_STG3
    END Instance
    Instance Missile/TH1_l2i
        *
        Constellation/Missile
        Missile/TH1_l2i
    END Instance
    Instance Satellite/dsp
        *
        Constellation/dsp_and_Fylingdales
        Satellite/dsp
        Satellite/dsp/Sensor/dsp_fov
        Satellite/dsp/Sensor/dsp_sweep
    END Instance
    Instance Satellite/dsp/Sensor/dsp_fov
        Satellite/dsp/Sensor/dsp_fov
    END Instance
    Instance Satellite/dsp/Sensor/dsp_sweep
        Constellation/dsp_and_Fylingdales
        Satellite/dsp/Sensor/dsp_sweep
    END Instance
END References

END Scenario
stk.v.7.0
BEGIN Scenario
    Name            RangeExample

BEGIN Epoch
    Epoch        1 Jul 2005 12:00:00.00

END Epoch

BEGIN Interval

Start               1 Jul 2005 12:00:00.000000000
Stop                2 Jul 2005 12:00:00.000000000

END Interval

BEGIN EOPFile

    EOPFilename     EOP.dat

END EOPFile

BEGIN GlobalPrefs

    SatelliteNoOrbWarning    No
    MissileNoOrbWarning      No
END GlobalPrefs

BEGIN CentralBody

    PrimaryBody     Earth

END CentralBody

BEGIN Extensions
    
    BEGIN Graphics

BEGIN Animation

    StartTime          1 Jul 2005 12:00:00.000000000
    EndTime            2 Jul 2005 12:00:00.000000000
    Direction          Forward
    UpdateDelta        60.0000
    RefreshDelta       HighSpeed
    XRealTimeMult      1.0000
    RealTimeOffset     0.0000

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
           CreateChunkTrn  No
           OutputFormat    PDTTX
    End TerrainConverterData

    BEGIN Map

        MapNum         1
        TrackingMode   LatLon

        BEGIN MapAttributes
            CenterLatitude       0.000000
            CenterLongitude      0.000000
            ProjectionAltitude   63621860.000000
            FieldOfView          35.000000
            OrthoDisplayDistance 20000000.000000
            TransformTrajectory  On
            EquatorialRadius     6378137.000000
            PrimaryBody          Earth
            SecondaryBody        Sun
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
            BackgroundImageFile  Basic
            UseCloudsFile        Off
            BEGIN ZoomBounds
                -90.000000 -179.999999 90.000000 179.999999
            END ZoomBounds
            Zoomed               No
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

        BEGIN Maps
            RWDB2 Coastlines    No #00ff00
            Coastlines    No #00ff00
            Major_Ice_Shelves    No #00ff00
            Minor_Ice_Shelves    No #00ff00
            RWDB2 International Borders    No #00ff00
                Rank 1: Demarcated or Delimited    No #00ff00
                Rank 2: Indefinite or Disputed    No #00ff00
                Rank 3: Lines of separation or sovereignty on land    No #00ff00
                Rank 4: Lines of separation or sovereignty at sea    No #00ff00
                Rank 5: Other lines of separation or sovereignty at sea    No #00ff00
                Rank 6: Continental shelf boundary in Persian Gulf    No #00ff00
                Rank 7: Demilitarized zone lines in Israel    No #00ff00
                Rank 8: No defined line    No #00ff00
                Rank 9: Selected claimed lines    No #00ff00
                Rank 50: Old Panama Canal Zone lines    No #00ff00
                Rank 51: Old N. Yemen-S.Yemen lines    No #00ff00
                Rank 52: Old Jordan-Iraq lines    No #00ff00
                Rank 53: Old Iraq-Saudi Arabia Neutral Zone lines    No #00ff00
                Rank 54: Old East Germany-West Germany and Berlin lines    No #00ff00
                Rank 55: Old N. Vietnam-S. Vietnam boundary    No #00ff00
                Rank 56: Old Vietnam DMZ lines    No #00ff00
                Rank 57: Old Kuwait-Saudi Arabia Neutral Zone lines    No #00ff00
                Rank 58: Old Oman-Yemen line of separation    No #00ff00
            RWDB2 Islands    No #00ff00
                Rank 1: Major islands    No #00ff00
                Rank 2: Additional major islands    No #00ff00
                Rank 3: Moderately important islands    No #00ff00
                Rank 4: Additional islands    No #00ff00
                Rank 5: Minor islands    No #00ff00
                Rank 6: Very small minor islands    No #00ff00
                Rank 8: Reefs    No #00ff00
                Rank 9: Shoals    No #00ff00
            RWDB2 Lakes    No #00ff00
                Rank 1: Lakes that should appear on all maps    No #00ff00
                Rank 2: Major lakes    No #00ff00
                Rank 3: Additional major lakes    No #00ff00
                Rank 4: Intermediate lakes    No #00ff00
                Rank 5: Minor lakes    No #00ff00
                Rank 6: Additional minor lakes    No #00ff00
                Rank 7: Swamps    No #00ff00
                Rank 11: Intermittent major lakes    No #00ff00
                Rank 12: Intermittent minor lakes    No #00ff00
                Rank 14: Major salt pans    No #00ff00
                Rank 15: Minor salt pans    No #00ff00
                Rank 23: Glaciers    No #00ff00
            RWDB2 Provincial Borders    No #00ff00
                Rank 1: First order    No #00ff00
                Rank 2: Second order    No #00ff00
                Rank 3: Third order    No #00ff00
                Rank 4: Special boundaries    No #00ff00
                Rank 54: Pre-unification German administration lines    No #00ff00
                Rank 61: First order boundaries in the water    No #00ff00
                Rank 62: Second order boundaries in the water    No #00ff00
                Rank 99: Disputed lines    No #00ff00
            RWDB2 Rivers    No #00ff00
                Rank 1: Major rivers    No #00ff00
                Rank 2: Additional major rivers    No #00ff00
                Rank 3: Intermediate rivers    No #00ff00
                Rank 4: Additional intermediate rivers    No #00ff00
                Rank 5: Minor rivers    No #00ff00
                Rank 6: Additional minor rivers    No #00ff00
                Rank 10: Major intermittent rivers    No #00ff00
                Rank 11: Intermediate intermittent rivers    No #00ff00
                Rank 12: Minor intermittent rivers    No #00ff00
                Rank 21: Major canals    No #00ff00
                Rank 22: Minor canals    No #00ff00
                Rank 23: Irrigation canals    No #00ff00
            RWDB2_Coastlines    Yes #8fbc8f
            Coastlines    No #00ff00
            Major_Ice_Shelves    No #00ff00
            Minor_Ice_Shelves    No #00ff00
            RWDB2_International_Borders    Yes #8fbc8f
            Demarcated_or_Delimited    Yes #8fbc8f
            Indefinite_or_Disputed    No #8fbc8f
            Lines_of_separation_on_land    No #8fbc8f
            Lines_of_separation_at_sea    No #8fbc8f
            Other_lines_of_separation_at_sea    No #8fbc8f
            Continental_shelf_boundary_in_Persian_Gulf    No #8fbc8f
            Demilitarized_zone_lines_in_Israel    No #8fbc8f
            No_defined_line    No #8fbc8f
            Selected_claimed_lines    No #8fbc8f
            Old_Panama_Canal_Zone_lines    No #8fbc8f
            Old_North-South_Yemen_lines    No #8fbc8f
            Old_Jordan-Iraq_lines    No #8fbc8f
            Old_Iraq-Saudi_Arabia_Neutral_Zone_lines    No #8fbc8f
            Old_East-West_Germany_and_Berlin_lines    No #8fbc8f
            Old_North-South_Vietnam_boundary    No #8fbc8f
            Old_Vietnam_DMZ_lines    No #8fbc8f
            Old_Kuwait-Saudi_Arabia_Neutral_Zone_lines    No #8fbc8f
            Old_Oman-Yemen_line_of_separation    No #8fbc8f
            RWDB2_Islands    Yes #8fbc8f
            Major_islands    Yes #8fbc8f
            Additional_major_islands    No #8fbc8f
            Moderately_important_islands    No #8fbc8f
            Additional_islands    No #8fbc8f
            Minor_islands    No #8fbc8f
            Very_small_minor_islands    No #8fbc8f
            Reefs    No #8fbc8f
            Shoals    No #8fbc8f
            RWDB2_Lakes    No #87cefa
            Lakes_that_should_appear_on_all_maps    No #87cefa
            Major_lakes    No #87cefa
            Additional_major_lakes    No #87cefa
            Intermediate_lakes    No #87cefa
            Minor_lakes    No #87cefa
            Additional_minor_lakes    No #87cefa
            Swamps    No #87cefa
            Intermittent_major_lakes    No #87cefa
            Intermittent_minor_lakes    No #87cefa
            Major_salt_pans    No #87cefa
            Minor_salt_pans    No #87cefa
            Glaciers    No #87cefa
            RWDB2_Provincial_Borders    No #8fbc8f
            First_order    No #8fbc8f
            Second_order    No #8fbc8f
            Third_order    No #8fbc8f
            Special_boundaries    No #8fbc8f
            Pre-unification_German_administration_lines    No #8fbc8f
            First_order_boundaries_on_water    No #8fbc8f
            Second_order_boundaries_on_water    No #8fbc8f
            Disputed_lines    No #8fbc8f
            RWDB2_Rivers    No #87cefa
            Major_rivers    No #87cefa
            Additional_major_rivers    No #87cefa
            Intermediate_rivers    No #87cefa
            Additional_intermediate_rivers    No #87cefa
            Minor_rivers    No #87cefa
            Additional_minor_rivers    No #87cefa
            Major_intermittent_rivers    No #87cefa
            Intermediate_intermittent_rivers    No #87cefa
            Minor_intermittent_rivers    No #87cefa
            Major_canals    No #87cefa
            Minor_canals    No #87cefa
            Irrigation_canals    No #87cefa
        END Maps


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
            Directory        C:\DOCUME~1\mward\LOCALS~1\Temp
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
        END SoftVTR


        BEGIN TimeDisplay
            Show             0
            TextColor        #ffffff
            Transparent      0
            BackColor        #4d4d4d
            XPosition        20
            YPosition        -20
        END TimeDisplay

        BEGIN WindowLayout
            VariableAspectRatio  Yes
            MapCenterByMouse     Click
        END WindowLayout

        BEGIN LightingData
            DisplayAltitude              0.000000
            SubsolarPoint                Off
            SubsolarPointColor           #ffff00
            SubsolarPointMarkerStyle     2

            ShowUmbraLine                Off
            UmbraLineColor               #0000ff
            UmbraLineStyle               0
            UmbraLineWidth               2
            FillUmbra                    Off
            UmbraFillColor               #0000ff
            UmbraFillStyle               7

            ShowPenumbraLines            Off
            PenumbraLineColor            #87cefa
            PenumbraLineStyle            0
            PenumbraLineWidth            1
            FillPenumbra                 Off
            PenumbraFillColor            #87cefa
            PenumbraFillStyle            7

            ShowSunlightLine             Off
            SunlightLineColor            #ffff00
            SunlightLineStyle            0
            SunlightLineWidth            2
            FillSunlight                 Off
            SunlightFillColor            #ffff00
            SunlightFillStyle            7

        END LightingData

    END Map

    BEGIN MapStyles

        UseStyleTime        No

        BEGIN Style
        Name                No_Map_Bckgrnd
        Time                0.000000
        UpdateDelta         60.000000

        BEGIN MapAttributes
            CenterLatitude       0.000000
            CenterLongitude      0.000000
            ProjectionAltitude   63621860.000000
            FieldOfView          35.000000
            OrthoDisplayDistance 20000000.000000
            TransformTrajectory  On
            EquatorialRadius     6378137.000000
            PrimaryBody          Earth
            SecondaryBody        Sun
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
            UseCloudsFile        Off
            BEGIN ZoomBounds
                -90.000000 -179.999999 90.000000 179.999999
            END ZoomBounds
            Zoomed               No
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

        BEGIN Maps
            RWDB2 Coastlines    No #00ff00
            Coastlines    No #00ff00
            Major_Ice_Shelves    No #00ff00
            Minor_Ice_Shelves    No #00ff00
            RWDB2 International Borders    No #00ff00
                Rank 1: Demarcated or Delimited    No #00ff00
                Rank 2: Indefinite or Disputed    No #00ff00
                Rank 3: Lines of separation or sovereignty on land    No #00ff00
                Rank 4: Lines of separation or sovereignty at sea    No #00ff00
                Rank 5: Other lines of separation or sovereignty at sea    No #00ff00
                Rank 6: Continental shelf boundary in Persian Gulf    No #00ff00
                Rank 7: Demilitarized zone lines in Israel    No #00ff00
                Rank 8: No defined line    No #00ff00
                Rank 9: Selected claimed lines    No #00ff00
                Rank 50: Old Panama Canal Zone lines    No #00ff00
                Rank 51: Old N. Yemen-S.Yemen lines    No #00ff00
                Rank 52: Old Jordan-Iraq lines    No #00ff00
                Rank 53: Old Iraq-Saudi Arabia Neutral Zone lines    No #00ff00
                Rank 54: Old East Germany-West Germany and Berlin lines    No #00ff00
                Rank 55: Old N. Vietnam-S. Vietnam boundary    No #00ff00
                Rank 56: Old Vietnam DMZ lines    No #00ff00
                Rank 57: Old Kuwait-Saudi Arabia Neutral Zone lines    No #00ff00
                Rank 58: Old Oman-Yemen line of separation    No #00ff00
            RWDB2 Islands    No #00ff00
                Rank 1: Major islands    No #00ff00
                Rank 2: Additional major islands    No #00ff00
                Rank 3: Moderately important islands    No #00ff00
                Rank 4: Additional islands    No #00ff00
                Rank 5: Minor islands    No #00ff00
                Rank 6: Very small minor islands    No #00ff00
                Rank 8: Reefs    No #00ff00
                Rank 9: Shoals    No #00ff00
            RWDB2 Lakes    No #00ff00
                Rank 1: Lakes that should appear on all maps    No #00ff00
                Rank 2: Major lakes    No #00ff00
                Rank 3: Additional major lakes    No #00ff00
                Rank 4: Intermediate lakes    No #00ff00
                Rank 5: Minor lakes    No #00ff00
                Rank 6: Additional minor lakes    No #00ff00
                Rank 7: Swamps    No #00ff00
                Rank 11: Intermittent major lakes    No #00ff00
                Rank 12: Intermittent minor lakes    No #00ff00
                Rank 14: Major salt pans    No #00ff00
                Rank 15: Minor salt pans    No #00ff00
                Rank 23: Glaciers    No #00ff00
            RWDB2 Provincial Borders    No #00ff00
                Rank 1: First order    No #00ff00
                Rank 2: Second order    No #00ff00
                Rank 3: Third order    No #00ff00
                Rank 4: Special boundaries    No #00ff00
                Rank 54: Pre-unification German administration lines    No #00ff00
                Rank 61: First order boundaries in the water    No #00ff00
                Rank 62: Second order boundaries in the water    No #00ff00
                Rank 99: Disputed lines    No #00ff00
            RWDB2 Rivers    No #00ff00
                Rank 1: Major rivers    No #00ff00
                Rank 2: Additional major rivers    No #00ff00
                Rank 3: Intermediate rivers    No #00ff00
                Rank 4: Additional intermediate rivers    No #00ff00
                Rank 5: Minor rivers    No #00ff00
                Rank 6: Additional minor rivers    No #00ff00
                Rank 10: Major intermittent rivers    No #00ff00
                Rank 11: Intermediate intermittent rivers    No #00ff00
                Rank 12: Minor intermittent rivers    No #00ff00
                Rank 21: Major canals    No #00ff00
                Rank 22: Minor canals    No #00ff00
                Rank 23: Irrigation canals    No #00ff00
            RWDB2_Coastlines    Yes #00ff00
            Coastlines    No #00ff00
            Major_Ice_Shelves    No #00ff00
            Minor_Ice_Shelves    No #00ff00
            RWDB2_International_Borders    Yes #00ff00
            Demarcated_or_Delimited    Yes #00ff00
            Indefinite_or_Disputed    No #00ff00
            Lines_of_separation_on_land    No #00ff00
            Lines_of_separation_at_sea    No #00ff00
            Other_lines_of_separation_at_sea    No #00ff00
            Continental_shelf_boundary_in_Persian_Gulf    No #00ff00
            Demilitarized_zone_lines_in_Israel    No #00ff00
            No_defined_line    No #00ff00
            Selected_claimed_lines    No #00ff00
            Old_Panama_Canal_Zone_lines    No #00ff00
            Old_North-South_Yemen_lines    No #00ff00
            Old_Jordan-Iraq_lines    No #00ff00
            Old_Iraq-Saudi_Arabia_Neutral_Zone_lines    No #00ff00
            Old_East-West_Germany_and_Berlin_lines    No #00ff00
            Old_North-South_Vietnam_boundary    No #00ff00
            Old_Vietnam_DMZ_lines    No #00ff00
            Old_Kuwait-Saudi_Arabia_Neutral_Zone_lines    No #00ff00
            Old_Oman-Yemen_line_of_separation    No #00ff00
            RWDB2_Islands    Yes #00ff00
            Major_islands    Yes #00ff00
            Additional_major_islands    No #00ff00
            Moderately_important_islands    No #00ff00
            Additional_islands    No #00ff00
            Minor_islands    No #00ff00
            Very_small_minor_islands    No #00ff00
            Reefs    No #00ff00
            Shoals    No #00ff00
            RWDB2_Lakes    Yes #87cefa
            Lakes_that_should_appear_on_all_maps    Yes #87cefa
            Major_lakes    No #00ff00
            Additional_major_lakes    No #00ff00
            Intermediate_lakes    No #00ff00
            Minor_lakes    No #00ff00
            Additional_minor_lakes    No #00ff00
            Swamps    No #00ff00
            Intermittent_major_lakes    No #00ff00
            Intermittent_minor_lakes    No #00ff00
            Major_salt_pans    No #00ff00
            Minor_salt_pans    No #00ff00
            Glaciers    No #00ff00
            RWDB2_Provincial_Borders    Yes #00ff00
            First_order    Yes #00ff00
            Second_order    No #00ff00
            Third_order    No #00ff00
            Special_boundaries    No #00ff00
            Pre-unification_German_administration_lines    No #00ff00
            First_order_boundaries_on_water    No #00ff00
            Second_order_boundaries_on_water    No #00ff00
            Disputed_lines    No #00ff00
            RWDB2_Rivers    No #00ff00
            Major_rivers    No #00ff00
            Additional_major_rivers    No #00ff00
            Intermediate_rivers    No #00ff00
            Additional_intermediate_rivers    No #00ff00
            Minor_rivers    No #00ff00
            Additional_minor_rivers    No #00ff00
            Major_intermittent_rivers    No #00ff00
            Intermediate_intermittent_rivers    No #00ff00
            Minor_intermittent_rivers    No #00ff00
            Major_canals    No #00ff00
            Minor_canals    No #00ff00
            Irrigation_canals    No #00ff00
        END Maps


        BEGIN MapAnnotations
        END MapAnnotations

        BEGIN SoftVTR
            OutputFormat     BMP
            Directory        C:\DOCUME~1\mward\LOCALS~1\Temp
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
        END SoftVTR


        BEGIN TimeDisplay
            Show             0
            TextColor        #00ffff
            Transparent      0
            BackColor        #000000
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
            UmbraFillStyle               7

            ShowPenumbraLines            Off
            PenumbraLineColor            #ffff00
            PenumbraLineStyle            0
            PenumbraLineWidth            1
            FillPenumbra                 Off
            PenumbraFillColor            #000000
            PenumbraFillStyle            7

            ShowSunlightLine             Off
            SunlightLineColor            #ffff00
            SunlightLineStyle            0
            SunlightLineWidth            1
            FillSunlight                 Off
            SunlightFillColor            #ffff00
            SunlightFillStyle            7

        END LightingData

        ShowDtedRegions     Off

        End Style

        BEGIN Style
        Name                Basic_Map_Bckgrnd
        Time                0.000000
        UpdateDelta         60.000000

        BEGIN MapAttributes
            CenterLatitude       0.000000
            CenterLongitude      0.000000
            ProjectionAltitude   63621860.000000
            FieldOfView          35.000000
            OrthoDisplayDistance 20000000.000000
            TransformTrajectory  On
            EquatorialRadius     6378137.000000
            PrimaryBody          Earth
            SecondaryBody        Sun
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
            BackgroundImageFile  Basic
            UseCloudsFile        Off
            BEGIN ZoomBounds
                -90.000000 -179.999999 90.000000 179.999999
            END ZoomBounds
            Zoomed               No
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

        BEGIN Maps
            RWDB2 Coastlines    No #00ff00
            Coastlines    No #00ff00
            Major_Ice_Shelves    No #00ff00
            Minor_Ice_Shelves    No #00ff00
            RWDB2 International Borders    No #00ff00
                Rank 1: Demarcated or Delimited    No #00ff00
                Rank 2: Indefinite or Disputed    No #00ff00
                Rank 3: Lines of separation or sovereignty on land    No #00ff00
                Rank 4: Lines of separation or sovereignty at sea    No #00ff00
                Rank 5: Other lines of separation or sovereignty at sea    No #00ff00
                Rank 6: Continental shelf boundary in Persian Gulf    No #00ff00
                Rank 7: Demilitarized zone lines in Israel    No #00ff00
                Rank 8: No defined line    No #00ff00
                Rank 9: Selected claimed lines    No #00ff00
                Rank 50: Old Panama Canal Zone lines    No #00ff00
                Rank 51: Old N. Yemen-S.Yemen lines    No #00ff00
                Rank 52: Old Jordan-Iraq lines    No #00ff00
                Rank 53: Old Iraq-Saudi Arabia Neutral Zone lines    No #00ff00
                Rank 54: Old East Germany-West Germany and Berlin lines    No #00ff00
                Rank 55: Old N. Vietnam-S. Vietnam boundary    No #00ff00
                Rank 56: Old Vietnam DMZ lines    No #00ff00
                Rank 57: Old Kuwait-Saudi Arabia Neutral Zone lines    No #00ff00
                Rank 58: Old Oman-Yemen line of separation    No #00ff00
            RWDB2 Islands    No #00ff00
                Rank 1: Major islands    No #00ff00
                Rank 2: Additional major islands    No #00ff00
                Rank 3: Moderately important islands    No #00ff00
                Rank 4: Additional islands    No #00ff00
                Rank 5: Minor islands    No #00ff00
                Rank 6: Very small minor islands    No #00ff00
                Rank 8: Reefs    No #00ff00
                Rank 9: Shoals    No #00ff00
            RWDB2 Lakes    No #00ff00
                Rank 1: Lakes that should appear on all maps    No #00ff00
                Rank 2: Major lakes    No #00ff00
                Rank 3: Additional major lakes    No #00ff00
                Rank 4: Intermediate lakes    No #00ff00
                Rank 5: Minor lakes    No #00ff00
                Rank 6: Additional minor lakes    No #00ff00
                Rank 7: Swamps    No #00ff00
                Rank 11: Intermittent major lakes    No #00ff00
                Rank 12: Intermittent minor lakes    No #00ff00
                Rank 14: Major salt pans    No #00ff00
                Rank 15: Minor salt pans    No #00ff00
                Rank 23: Glaciers    No #00ff00
            RWDB2 Provincial Borders    No #00ff00
                Rank 1: First order    No #00ff00
                Rank 2: Second order    No #00ff00
                Rank 3: Third order    No #00ff00
                Rank 4: Special boundaries    No #00ff00
                Rank 54: Pre-unification German administration lines    No #00ff00
                Rank 61: First order boundaries in the water    No #00ff00
                Rank 62: Second order boundaries in the water    No #00ff00
                Rank 99: Disputed lines    No #00ff00
            RWDB2 Rivers    No #00ff00
                Rank 1: Major rivers    No #00ff00
                Rank 2: Additional major rivers    No #00ff00
                Rank 3: Intermediate rivers    No #00ff00
                Rank 4: Additional intermediate rivers    No #00ff00
                Rank 5: Minor rivers    No #00ff00
                Rank 6: Additional minor rivers    No #00ff00
                Rank 10: Major intermittent rivers    No #00ff00
                Rank 11: Intermediate intermittent rivers    No #00ff00
                Rank 12: Minor intermittent rivers    No #00ff00
                Rank 21: Major canals    No #00ff00
                Rank 22: Minor canals    No #00ff00
                Rank 23: Irrigation canals    No #00ff00
            RWDB2_Coastlines    Yes #8fbc8f
            Coastlines    No #00ff00
            Major_Ice_Shelves    No #00ff00
            Minor_Ice_Shelves    No #00ff00
            RWDB2_International_Borders    Yes #8fbc8f
            Demarcated_or_Delimited    Yes #8fbc8f
            Indefinite_or_Disputed    No #00ff00
            Lines_of_separation_on_land    No #00ff00
            Lines_of_separation_at_sea    No #00ff00
            Other_lines_of_separation_at_sea    No #00ff00
            Continental_shelf_boundary_in_Persian_Gulf    No #00ff00
            Demilitarized_zone_lines_in_Israel    No #00ff00
            No_defined_line    No #00ff00
            Selected_claimed_lines    No #00ff00
            Old_Panama_Canal_Zone_lines    No #00ff00
            Old_North-South_Yemen_lines    No #00ff00
            Old_Jordan-Iraq_lines    No #00ff00
            Old_Iraq-Saudi_Arabia_Neutral_Zone_lines    No #00ff00
            Old_East-West_Germany_and_Berlin_lines    No #00ff00
            Old_North-South_Vietnam_boundary    No #00ff00
            Old_Vietnam_DMZ_lines    No #00ff00
            Old_Kuwait-Saudi_Arabia_Neutral_Zone_lines    No #00ff00
            Old_Oman-Yemen_line_of_separation    No #00ff00
            RWDB2_Islands    Yes #8fbc8f
            Major_islands    Yes #8fbc8f
            Additional_major_islands    No #00ff00
            Moderately_important_islands    No #00ff00
            Additional_islands    No #00ff00
            Minor_islands    No #00ff00
            Very_small_minor_islands    No #00ff00
            Reefs    No #00ff00
            Shoals    No #00ff00
            RWDB2_Lakes    No #00ff00
            Lakes_that_should_appear_on_all_maps    No #00ff00
            Major_lakes    No #00ff00
            Additional_major_lakes    No #00ff00
            Intermediate_lakes    No #00ff00
            Minor_lakes    No #00ff00
            Additional_minor_lakes    No #00ff00
            Swamps    No #00ff00
            Intermittent_major_lakes    No #00ff00
            Intermittent_minor_lakes    No #00ff00
            Major_salt_pans    No #00ff00
            Minor_salt_pans    No #00ff00
            Glaciers    No #00ff00
            RWDB2_Provincial_Borders    No #00ff00
            First_order    No #00ff00
            Second_order    No #00ff00
            Third_order    No #00ff00
            Special_boundaries    No #00ff00
            Pre-unification_German_administration_lines    No #00ff00
            First_order_boundaries_on_water    No #00ff00
            Second_order_boundaries_on_water    No #00ff00
            Disputed_lines    No #00ff00
            RWDB2_Rivers    No #00ff00
            Major_rivers    No #00ff00
            Additional_major_rivers    No #00ff00
            Intermediate_rivers    No #00ff00
            Additional_intermediate_rivers    No #00ff00
            Minor_rivers    No #00ff00
            Additional_minor_rivers    No #00ff00
            Major_intermittent_rivers    No #00ff00
            Intermediate_intermittent_rivers    No #00ff00
            Minor_intermittent_rivers    No #00ff00
            Major_canals    No #00ff00
            Minor_canals    No #00ff00
            Irrigation_canals    No #00ff00
        END Maps


        BEGIN MapAnnotations
        END MapAnnotations

        BEGIN SoftVTR
            OutputFormat     BMP
            Directory        C:\DOCUME~1\mward\LOCALS~1\Temp
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
        END SoftVTR


        BEGIN TimeDisplay
            Show             0
            TextColor        #00ffff
            Transparent      0
            BackColor        #000000
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
            UmbraFillStyle               7

            ShowPenumbraLines            Off
            PenumbraLineColor            #ffff00
            PenumbraLineStyle            0
            PenumbraLineWidth            1
            FillPenumbra                 Off
            PenumbraFillColor            #000000
            PenumbraFillStyle            7

            ShowSunlightLine             Off
            SunlightLineColor            #ffff00
            SunlightLineStyle            0
            SunlightLineWidth            1
            FillSunlight                 Off
            SunlightFillColor            #ffff00
            SunlightFillStyle            7

        END LightingData

        ShowDtedRegions     Off

        End Style

        BEGIN Style
        Name                Orthographic_Projection
        Time                0.000000
        UpdateDelta         60.000000

        BEGIN MapAttributes
            CenterLatitude       0.000000
            CenterLongitude      0.000000
            ProjectionAltitude   63621860.000000
            FieldOfView          35.000000
            OrthoDisplayDistance 20000000.000000
            TransformTrajectory  On
            EquatorialRadius     6378137.000000
            PrimaryBody          Earth
            SecondaryBody        Sun
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
            UseCloudsFile        Off
            BEGIN ZoomBounds
                -90.000000 -179.999999 90.000000 179.999999
            END ZoomBounds
            Zoomed               No
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

        BEGIN Maps
            RWDB2 Coastlines    No #00ff00
            Coastlines    No #00ff00
            Major_Ice_Shelves    No #00ff00
            Minor_Ice_Shelves    No #00ff00
            RWDB2 International Borders    No #00ff00
                Rank 1: Demarcated or Delimited    No #00ff00
                Rank 2: Indefinite or Disputed    No #00ff00
                Rank 3: Lines of separation or sovereignty on land    No #00ff00
                Rank 4: Lines of separation or sovereignty at sea    No #00ff00
                Rank 5: Other lines of separation or sovereignty at sea    No #00ff00
                Rank 6: Continental shelf boundary in Persian Gulf    No #00ff00
                Rank 7: Demilitarized zone lines in Israel    No #00ff00
                Rank 8: No defined line    No #00ff00
                Rank 9: Selected claimed lines    No #00ff00
                Rank 50: Old Panama Canal Zone lines    No #00ff00
                Rank 51: Old N. Yemen-S.Yemen lines    No #00ff00
                Rank 52: Old Jordan-Iraq lines    No #00ff00
                Rank 53: Old Iraq-Saudi Arabia Neutral Zone lines    No #00ff00
                Rank 54: Old East Germany-West Germany and Berlin lines    No #00ff00
                Rank 55: Old N. Vietnam-S. Vietnam boundary    No #00ff00
                Rank 56: Old Vietnam DMZ lines    No #00ff00
                Rank 57: Old Kuwait-Saudi Arabia Neutral Zone lines    No #00ff00
                Rank 58: Old Oman-Yemen line of separation    No #00ff00
            RWDB2 Islands    No #00ff00
                Rank 1: Major islands    No #00ff00
                Rank 2: Additional major islands    No #00ff00
                Rank 3: Moderately important islands    No #00ff00
                Rank 4: Additional islands    No #00ff00
                Rank 5: Minor islands    No #00ff00
                Rank 6: Very small minor islands    No #00ff00
                Rank 8: Reefs    No #00ff00
                Rank 9: Shoals    No #00ff00
            RWDB2 Lakes    No #00ff00
                Rank 1: Lakes that should appear on all maps    No #00ff00
                Rank 2: Major lakes    No #00ff00
                Rank 3: Additional major lakes    No #00ff00
                Rank 4: Intermediate lakes    No #00ff00
                Rank 5: Minor lakes    No #00ff00
                Rank 6: Additional minor lakes    No #00ff00
                Rank 7: Swamps    No #00ff00
                Rank 11: Intermittent major lakes    No #00ff00
                Rank 12: Intermittent minor lakes    No #00ff00
                Rank 14: Major salt pans    No #00ff00
                Rank 15: Minor salt pans    No #00ff00
                Rank 23: Glaciers    No #00ff00
            RWDB2 Provincial Borders    No #00ff00
                Rank 1: First order    No #00ff00
                Rank 2: Second order    No #00ff00
                Rank 3: Third order    No #00ff00
                Rank 4: Special boundaries    No #00ff00
                Rank 54: Pre-unification German administration lines    No #00ff00
                Rank 61: First order boundaries in the water    No #00ff00
                Rank 62: Second order boundaries in the water    No #00ff00
                Rank 99: Disputed lines    No #00ff00
            RWDB2 Rivers    No #00ff00
                Rank 1: Major rivers    No #00ff00
                Rank 2: Additional major rivers    No #00ff00
                Rank 3: Intermediate rivers    No #00ff00
                Rank 4: Additional intermediate rivers    No #00ff00
                Rank 5: Minor rivers    No #00ff00
                Rank 6: Additional minor rivers    No #00ff00
                Rank 10: Major intermittent rivers    No #00ff00
                Rank 11: Intermediate intermittent rivers    No #00ff00
                Rank 12: Minor intermittent rivers    No #00ff00
                Rank 21: Major canals    No #00ff00
                Rank 22: Minor canals    No #00ff00
                Rank 23: Irrigation canals    No #00ff00
            RWDB2_Coastlines    Yes #00ff00
            Coastlines    No #00ff00
            Major_Ice_Shelves    No #00ff00
            Minor_Ice_Shelves    No #00ff00
            RWDB2_International_Borders    Yes #00ff00
            Demarcated_or_Delimited    Yes #00ff00
            Indefinite_or_Disputed    No #00ff00
            Lines_of_separation_on_land    No #00ff00
            Lines_of_separation_at_sea    No #00ff00
            Other_lines_of_separation_at_sea    No #00ff00
            Continental_shelf_boundary_in_Persian_Gulf    No #00ff00
            Demilitarized_zone_lines_in_Israel    No #00ff00
            No_defined_line    No #00ff00
            Selected_claimed_lines    No #00ff00
            Old_Panama_Canal_Zone_lines    No #00ff00
            Old_North-South_Yemen_lines    No #00ff00
            Old_Jordan-Iraq_lines    No #00ff00
            Old_Iraq-Saudi_Arabia_Neutral_Zone_lines    No #00ff00
            Old_East-West_Germany_and_Berlin_lines    No #00ff00
            Old_North-South_Vietnam_boundary    No #00ff00
            Old_Vietnam_DMZ_lines    No #00ff00
            Old_Kuwait-Saudi_Arabia_Neutral_Zone_lines    No #00ff00
            Old_Oman-Yemen_line_of_separation    No #00ff00
            RWDB2_Islands    Yes #00ff00
            Major_islands    Yes #00ff00
            Additional_major_islands    No #00ff00
            Moderately_important_islands    No #00ff00
            Additional_islands    No #00ff00
            Minor_islands    No #00ff00
            Very_small_minor_islands    No #00ff00
            Reefs    No #00ff00
            Shoals    No #00ff00
            RWDB2_Lakes    Yes #87cefa
            Lakes_that_should_appear_on_all_maps    Yes #87cefa
            Major_lakes    No #00ff00
            Additional_major_lakes    No #00ff00
            Intermediate_lakes    No #00ff00
            Minor_lakes    No #00ff00
            Additional_minor_lakes    No #00ff00
            Swamps    No #00ff00
            Intermittent_major_lakes    No #00ff00
            Intermittent_minor_lakes    No #00ff00
            Major_salt_pans    No #00ff00
            Minor_salt_pans    No #00ff00
            Glaciers    No #00ff00
            RWDB2_Provincial_Borders    Yes #00ff00
            First_order    Yes #00ff00
            Second_order    No #00ff00
            Third_order    No #00ff00
            Special_boundaries    No #00ff00
            Pre-unification_German_administration_lines    No #00ff00
            First_order_boundaries_on_water    No #00ff00
            Second_order_boundaries_on_water    No #00ff00
            Disputed_lines    No #00ff00
            RWDB2_Rivers    No #00ff00
            Major_rivers    No #00ff00
            Additional_major_rivers    No #00ff00
            Intermediate_rivers    No #00ff00
            Additional_intermediate_rivers    No #00ff00
            Minor_rivers    No #00ff00
            Additional_minor_rivers    No #00ff00
            Major_intermittent_rivers    No #00ff00
            Intermediate_intermittent_rivers    No #00ff00
            Minor_intermittent_rivers    No #00ff00
            Major_canals    No #00ff00
            Minor_canals    No #00ff00
            Irrigation_canals    No #00ff00
        END Maps


        BEGIN MapAnnotations
        END MapAnnotations

        BEGIN SoftVTR
            OutputFormat     BMP
            Directory        C:\DOCUME~1\mward\LOCALS~1\Temp
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
        END SoftVTR


        BEGIN TimeDisplay
            Show             0
            TextColor        #00ffff
            Transparent      0
            BackColor        #000000
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
            UmbraFillStyle               7

            ShowPenumbraLines            Off
            PenumbraLineColor            #ffff00
            PenumbraLineStyle            0
            PenumbraLineWidth            1
            FillPenumbra                 Off
            PenumbraFillColor            #000000
            PenumbraFillStyle            7

            ShowSunlightLine             Off
            SunlightLineColor            #ffff00
            SunlightLineStyle            0
            SunlightLineWidth            1
            FillSunlight                 Off
            SunlightFillColor            #ffff00
            SunlightFillStyle            7

        END LightingData

        ShowDtedRegions     Off

        End Style

        BEGIN Style
        Name                Zoom_North_America
        Time                0.000000
        UpdateDelta         60.000000

        BEGIN MapAttributes
            CenterLatitude       0.000000
            CenterLongitude      0.000000
            ProjectionAltitude   63621860.000000
            FieldOfView          35.000000
            OrthoDisplayDistance 20000000.000000
            TransformTrajectory  On
            EquatorialRadius     6378137.000000
            PrimaryBody          Earth
            SecondaryBody        Sun
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
            UseBackgroundImage   On
            BackgroundImageFile  Earth_STK42
            UseCloudsFile        Off
            BEGIN ZoomBounds
                15.776278 -171.147540 80.694310 -41.311475
                -90.000000 -179.999999 90.000000 179.999999
            END ZoomBounds
            Zoomed               Yes
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

        BEGIN Maps
            RWDB2 Coastlines    No #00ff00
            Coastlines    No #00ff00
            Major_Ice_Shelves    No #00ff00
            Minor_Ice_Shelves    No #00ff00
            RWDB2 International Borders    No #00ff00
                Rank 1: Demarcated or Delimited    No #00ff00
                Rank 2: Indefinite or Disputed    No #00ff00
                Rank 3: Lines of separation or sovereignty on land    No #00ff00
                Rank 4: Lines of separation or sovereignty at sea    No #00ff00
                Rank 5: Other lines of separation or sovereignty at sea    No #00ff00
                Rank 6: Continental shelf boundary in Persian Gulf    No #00ff00
                Rank 7: Demilitarized zone lines in Israel    No #00ff00
                Rank 8: No defined line    No #00ff00
                Rank 9: Selected claimed lines    No #00ff00
                Rank 50: Old Panama Canal Zone lines    No #00ff00
                Rank 51: Old N. Yemen-S.Yemen lines    No #00ff00
                Rank 52: Old Jordan-Iraq lines    No #00ff00
                Rank 53: Old Iraq-Saudi Arabia Neutral Zone lines    No #00ff00
                Rank 54: Old East Germany-West Germany and Berlin lines    No #00ff00
                Rank 55: Old N. Vietnam-S. Vietnam boundary    No #00ff00
                Rank 56: Old Vietnam DMZ lines    No #00ff00
                Rank 57: Old Kuwait-Saudi Arabia Neutral Zone lines    No #00ff00
                Rank 58: Old Oman-Yemen line of separation    No #00ff00
            RWDB2 Islands    No #00ff00
                Rank 1: Major islands    No #00ff00
                Rank 2: Additional major islands    No #00ff00
                Rank 3: Moderately important islands    No #00ff00
                Rank 4: Additional islands    No #00ff00
                Rank 5: Minor islands    No #00ff00
                Rank 6: Very small minor islands    No #00ff00
                Rank 8: Reefs    No #00ff00
                Rank 9: Shoals    No #00ff00
            RWDB2 Lakes    No #00ff00
                Rank 1: Lakes that should appear on all maps    No #00ff00
                Rank 2: Major lakes    No #00ff00
                Rank 3: Additional major lakes    No #00ff00
                Rank 4: Intermediate lakes    No #00ff00
                Rank 5: Minor lakes    No #00ff00
                Rank 6: Additional minor lakes    No #00ff00
                Rank 7: Swamps    No #00ff00
                Rank 11: Intermittent major lakes    No #00ff00
                Rank 12: Intermittent minor lakes    No #00ff00
                Rank 14: Major salt pans    No #00ff00
                Rank 15: Minor salt pans    No #00ff00
                Rank 23: Glaciers    No #00ff00
            RWDB2 Provincial Borders    No #00ff00
                Rank 1: First order    No #00ff00
                Rank 2: Second order    No #00ff00
                Rank 3: Third order    No #00ff00
                Rank 4: Special boundaries    No #00ff00
                Rank 54: Pre-unification German administration lines    No #00ff00
                Rank 61: First order boundaries in the water    No #00ff00
                Rank 62: Second order boundaries in the water    No #00ff00
                Rank 99: Disputed lines    No #00ff00
            RWDB2 Rivers    No #00ff00
                Rank 1: Major rivers    No #00ff00
                Rank 2: Additional major rivers    No #00ff00
                Rank 3: Intermediate rivers    No #00ff00
                Rank 4: Additional intermediate rivers    No #00ff00
                Rank 5: Minor rivers    No #00ff00
                Rank 6: Additional minor rivers    No #00ff00
                Rank 10: Major intermittent rivers    No #00ff00
                Rank 11: Intermediate intermittent rivers    No #00ff00
                Rank 12: Minor intermittent rivers    No #00ff00
                Rank 21: Major canals    No #00ff00
                Rank 22: Minor canals    No #00ff00
                Rank 23: Irrigation canals    No #00ff00
            RWDB2_Coastlines    Yes #00ff00
            Coastlines    No #00ff00
            Major_Ice_Shelves    No #00ff00
            Minor_Ice_Shelves    No #00ff00
            RWDB2_International_Borders    Yes #00ff00
            Demarcated_or_Delimited    Yes #00ff00
            Indefinite_or_Disputed    No #00ff00
            Lines_of_separation_on_land    No #00ff00
            Lines_of_separation_at_sea    No #00ff00
            Other_lines_of_separation_at_sea    No #00ff00
            Continental_shelf_boundary_in_Persian_Gulf    No #00ff00
            Demilitarized_zone_lines_in_Israel    No #00ff00
            No_defined_line    No #00ff00
            Selected_claimed_lines    No #00ff00
            Old_Panama_Canal_Zone_lines    No #00ff00
            Old_North-South_Yemen_lines    No #00ff00
            Old_Jordan-Iraq_lines    No #00ff00
            Old_Iraq-Saudi_Arabia_Neutral_Zone_lines    No #00ff00
            Old_East-West_Germany_and_Berlin_lines    No #00ff00
            Old_North-South_Vietnam_boundary    No #00ff00
            Old_Vietnam_DMZ_lines    No #00ff00
            Old_Kuwait-Saudi_Arabia_Neutral_Zone_lines    No #00ff00
            Old_Oman-Yemen_line_of_separation    No #00ff00
            RWDB2_Islands    Yes #00ff00
            Major_islands    Yes #00ff00
            Additional_major_islands    No #00ff00
            Moderately_important_islands    No #00ff00
            Additional_islands    No #00ff00
            Minor_islands    No #00ff00
            Very_small_minor_islands    No #00ff00
            Reefs    No #00ff00
            Shoals    No #00ff00
            RWDB2_Lakes    Yes #87cefa
            Lakes_that_should_appear_on_all_maps    Yes #87cefa
            Major_lakes    No #00ff00
            Additional_major_lakes    No #00ff00
            Intermediate_lakes    No #00ff00
            Minor_lakes    No #00ff00
            Additional_minor_lakes    No #00ff00
            Swamps    No #00ff00
            Intermittent_major_lakes    No #00ff00
            Intermittent_minor_lakes    No #00ff00
            Major_salt_pans    No #00ff00
            Minor_salt_pans    No #00ff00
            Glaciers    No #00ff00
            RWDB2_Provincial_Borders    Yes #00ff00
            First_order    Yes #00ff00
            Second_order    No #00ff00
            Third_order    No #00ff00
            Special_boundaries    No #00ff00
            Pre-unification_German_administration_lines    No #00ff00
            First_order_boundaries_on_water    No #00ff00
            Second_order_boundaries_on_water    No #00ff00
            Disputed_lines    No #00ff00
            RWDB2_Rivers    No #00ff00
            Major_rivers    No #00ff00
            Additional_major_rivers    No #00ff00
            Intermediate_rivers    No #00ff00
            Additional_intermediate_rivers    No #00ff00
            Minor_rivers    No #00ff00
            Additional_minor_rivers    No #00ff00
            Major_intermittent_rivers    No #00ff00
            Intermediate_intermittent_rivers    No #00ff00
            Minor_intermittent_rivers    No #00ff00
            Major_canals    No #00ff00
            Minor_canals    No #00ff00
            Irrigation_canals    No #00ff00
        END Maps


        BEGIN MapAnnotations
        END MapAnnotations

        BEGIN SoftVTR
            OutputFormat     BMP
            Directory        C:\DOCUME~1\mward\LOCALS~1\Temp
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
        END SoftVTR


        BEGIN TimeDisplay
            Show             0
            TextColor        #00ffff
            Transparent      0
            BackColor        #000000
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
            UmbraFillStyle               7

            ShowPenumbraLines            Off
            PenumbraLineColor            #ffff00
            PenumbraLineStyle            0
            PenumbraLineWidth            1
            FillPenumbra                 Off
            PenumbraFillColor            #000000
            PenumbraFillStyle            7

            ShowSunlightLine             Off
            SunlightLineColor            #ffff00
            SunlightLineStyle            0
            SunlightLineWidth            1
            FillSunlight                 Off
            SunlightFillColor            #ffff00
            SunlightFillStyle            7

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
		DataRateUnit		MegaBitsPerSecond
		Percent		Percentage
		UnitTemperature		UnitKelvin
		RadiationDoseUnit		RadsSilicon
		RadiationShieldThicknessUnit		GramsperSquareCm
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
		DataRateUnit		MegaBitsPerSecond
		Percent		Percentage
		UnitTemperature		UnitKelvin
		RadiationDoseUnit		RadsSilicon
		RadiationShieldThicknessUnit		GramsperSquareCm
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
		DataRateUnit		MegaBitsPerSecond
		Percent		Percentage
		UnitTemperature		UnitKelvin
		RadiationDoseUnit		RadsSilicon
		RadiationShieldThicknessUnit		GramsperSquareCm
		MagneticFieldUnit		nanoTesla
    END ConnectReportUnits
    
    BEGIN GenDb

		BEGIN Database
		    DbType       Satellite
		    DefDb        stkSatDb.sd
		    UseMyDb      Off
		    MaxMatches   2000

		END Database

		BEGIN Database
		    DbType       City
		    DefDb        stkCityDb.cd
		    UseMyDb      Off
		    MaxMatches   2000

		END Database

		BEGIN Database
		    DbType       Facility
		    DefDb        stkFacility.fd
		    UseMyDb      Off
		    MaxMatches   2000

		END Database

		BEGIN Database
		    DbType       Star
		    DefDb        stkStarDb.bd
		    UseMyDb      Off
		    MaxMatches   2000

		END Database
    END GenDb
    
    BEGIN Msgp4Ext
    END Msgp4Ext
    
    BEGIN VectorTool
    ShowAxes      On
    ShowVector    On
    ShowPoint     On
    ShowSystem    On
    ShowAngle     On
    ShowPlane     On
    ShowAdvanced  Off
    ShowAllCB     Off
    END VectorTool
    
    BEGIN Author
	Optimize	Yes
	UseBasicGlobe	Yes
	PasswordProtect	No
    END Author
    
    BEGIN Desc
    END Desc
    
    BEGIN RfEnv
	UseGasAbsorbModel    No

	BEGIN Absorption

		AbsorptionModel	Simple Satcom

		BEGIN ModelData
			SWVC		    7.500000
			Temperature		293.150000

		END ModelData

	END Absorption

	UseRainModel         No
	EarthTemperature    290.000000
	UseCustomModelA    No
	UseCustomModelB    No
	UseCustomModelC    No

	BEGIN RainModel

		RainModelName	Crane1982

	END RainModel


	BEGIN CloudAndFog

		UseCloudFog           Off
		CloudCeiling          3.000000
		CloudThickness        0.500000
		CloudTemperature      273.150000
		CloudLiquidDensity    7.500000
		CloudWaterContent     3750.000000
	END CloudAndFog


	BEGIN TropoScintillation

		ComputeTropoScin          Off
		ComputeDeepFade           Off
		DeepFadePercent           0.100000
		RefractivityGradient      10.000000
	END TropoScintillation

    END RfEnv
    
    BEGIN RCS
	Inherited          False
	ClutterCoef        0.000000e+000
	BEGIN RCSBAND
		ConstantValue      0.000000e+000
		Swerling      0
		BandData      3.000000e+006 3.000000e+011
	END RCSBAND
    END RCS
    
    BEGIN Gator
    END Gator
    
    BEGIN Crdn
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Callisto_Angular_Velocity
			Description	Callisto Angular Velocity
			AbsolutePath	CentralBody/Callisto
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Callisto
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Callisto
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Callisto Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Callisto
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Callisto
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Callisto
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Callisto Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Callisto
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Callisto
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Callisto
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Callisto Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Callisto
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Callisto
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Callisto
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Callisto Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Callisto
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Callisto
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Callisto
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Ceres_Angular_Velocity
			Description	Ceres Angular Velocity
			AbsolutePath	CentralBody/Ceres
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Ceres
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Ceres
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Ceres Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Ceres
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Ceres
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Ceres
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Ceres Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Ceres
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Ceres
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Ceres
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Ceres Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Ceres
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Ceres
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Ceres
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Ceres Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Ceres
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Ceres
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Ceres
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Charon_Angular_Velocity
			Description	Charon Angular Velocity
			AbsolutePath	CentralBody/Charon
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Charon
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Charon
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Charon Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Charon
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Charon
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Charon
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Charon Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Charon
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Charon
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Charon
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Charon Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Charon
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Charon
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Charon
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Charon Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Charon
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Charon
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Charon
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Deimos_Angular_Velocity
			Description	Deimos Angular Velocity
			AbsolutePath	CentralBody/Deimos
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Deimos
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Deimos
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Deimos Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Deimos
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Deimos
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Deimos
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Deimos Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Deimos
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Deimos
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Deimos
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Deimos Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Deimos
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Deimos
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Deimos
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Deimos Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Deimos
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Deimos
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Deimos
					END	AXES
		END	SYSTEM
		BEGIN	AXES
			Type	AXES_ATEPOCH
			Name	Earth_Aligned_at_Epoch
			Description	Aligned with Earth Fixed at Epoch
			AbsolutePath	CentralBody/Earth
				Epoch	1 Jan 2000 11:58:55.816000015
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Earth
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
				LabelX	
				LabelY	
				LabelZ	
		END	AXES
		BEGIN	AXES
			Type	AXES_ATEPOCH
			Name	Mean_Ecliptic_of_Epoch
			Description	Mean Ecliptic of Epoch Axes
			AbsolutePath	CentralBody/Earth
				Epoch	1 Jan 2000 11:58:55.816000015
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MeanEclpDate
						AbsolutePath	CentralBody/Earth
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
				LabelX	
				LabelY	
				LabelZ	
		END	AXES
		BEGIN	AXES
			Type	AXES_ATEPOCH
			Name	Mean_Ecliptic_of_J2000
			Description	Mean Ecliptic of J2000 Axes
			AbsolutePath	CentralBody/Earth
				Epoch	1 Jan 2000 11:58:55.816000015
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MeanEclpDate
						AbsolutePath	CentralBody/Earth
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
				LabelX	
				LabelY	
				LabelZ	
		END	AXES
		BEGIN	AXES
			Type	AXES_ATEPOCH
			Name	Mean_Equinox_True_Equator_of_Epoch
			Description	Mean Equinox True Equator of Epoch Axes
			AbsolutePath	CentralBody/Earth
				Epoch	1 Jan 2000 11:58:55.816000015
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TEMED
						AbsolutePath	CentralBody/Earth
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
				LabelX	
				LabelY	
				LabelZ	
		END	AXES
		BEGIN	AXES
			Type	AXES_ATEPOCH
			Name	Mean_of_Epoch
			Description	Mean of Epoch Axes
			AbsolutePath	CentralBody/Earth
				Epoch	1 Jan 2000 11:58:55.816000015
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Earth
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
				LabelX	
				LabelY	
				LabelZ	
		END	AXES
		BEGIN	AXES
			Type	AXES_ATEPOCH
			Name	True_Ecliptic_of_Epoch
			Description	True Ecliptic of Epoch Axes
			AbsolutePath	CentralBody/Earth
				Epoch	1 Jan 2000 11:58:55.816000015
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TrueEclpDate
						AbsolutePath	CentralBody/Earth
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
				LabelX	
				LabelY	
				LabelZ	
		END	AXES
		BEGIN	AXES
			Type	AXES_ATEPOCH
			Name	True_of_Epoch
			Description	True of Epoch Axes
			AbsolutePath	CentralBody/Earth
				Epoch	1 Jan 2000 11:58:55.816000015
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Earth
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
				LabelX	
				LabelY	
				LabelZ	
		END	AXES
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Earth_Angular_Velocity
			Description	Earth Angular Velocity
			AbsolutePath	CentralBody/Earth
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Earth
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	VECTOR
		BEGIN	VECTOR
			Type	VECTOR_FIXED
			Name	User_Defined
			Description	User Defined Vector Fixed in Coordinate System
			AbsolutePath	CentralBody/Earth
				Dimension	6
				FixedVector
					0.00000000000000e+000
					0.00000000000000e+000
					1.00000000000000e+000
					UiSequence      321
					UiCoordType      4
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	VECTOR
		BEGIN	VECTOR
			Type	VECTOR_FIXED
			Name	X_Vector
			Description	X Axis in Default Coordinate System
			AbsolutePath	CentralBody/Earth
				Dimension	6
				FixedVector
					1.00000000000000e+000
					0.00000000000000e+000
					0.00000000000000e+000
					UiSequence      321
					UiCoordType      4
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Aligned_with_Fixed_at_Epoch
			Description	Earth Centered Aligned with Fixed at Epoch Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Earth_Aligned_at_Epoch
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Earth Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Earth Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Earth Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Earth Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Mean_B1950
			Description	Earth Centered Mean B1950 Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	B1950
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Mean_Ecliptic_of_Date
			Description	Earth Centered Mean Ecliptic of Date Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MeanEclpDate
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Mean_Ecliptic_of_Epoch
			Description	Earth Centered Mean Ecliptic of Epoch Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Mean_Ecliptic_of_Epoch
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Mean_Ecliptic_of_J2000
			Description	Earth Centered Mean Ecliptic of J2000 Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Mean_Ecliptic_of_J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Mean_Equinox_True_Equator_of_Date
			Description	Earth Centered Mean Equinox True Equator of Date Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TEMED
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Mean_Equinox_True_Equator_of_Epoch
			Description	Earth Centered Mean Equinox True Equator of Epoch Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Mean_Equinox_True_Equator_of_Epoch
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Mean_J2000
			Description	Earth Centered Mean J2000 (Default Inertial) Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Mean_of_Date
			Description	Earth Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Mean_of_Epoch
			Description	Earth Centered Mean of Epoch Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Mean_of_Epoch
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	True_Ecliptic_of_Date
			Description	Earth Centered True Ecliptic of Date Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TrueEclpDate
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	True_Ecliptic_of_Epoch
			Description	Earth Centered True Ecliptic of Epoch Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	True_Ecliptic_of_Epoch
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	True_of_Date
			Description	Earth Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	True_of_Epoch
			Description	Earth Centered True of Epoch Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	True_of_Epoch
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	User_Defined
			Description	User Assembled Coordinate System
			AbsolutePath	CentralBody/Earth
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Earth
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Europa_Angular_Velocity
			Description	Europa Angular Velocity
			AbsolutePath	CentralBody/Europa
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Europa
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Europa
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Europa Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Europa
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Europa
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Europa
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Europa Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Europa
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Europa
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Europa
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Europa Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Europa
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Europa
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Europa
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Europa Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Europa
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Europa
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Europa
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Ganymede_Angular_Velocity
			Description	Ganymede Angular Velocity
			AbsolutePath	CentralBody/Ganymede
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Ganymede
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Ganymede
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Ganymede Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Ganymede
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Ganymede
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Ganymede
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Ganymede Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Ganymede
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Ganymede
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Ganymede
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Ganymede Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Ganymede
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Ganymede
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Ganymede
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Ganymede Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Ganymede
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Ganymede
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Ganymede
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Io_Angular_Velocity
			Description	Io Angular Velocity
			AbsolutePath	CentralBody/Io
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Io
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Io
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Io Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Io
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Io
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Io
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Io Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Io
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Io
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Io
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Io Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Io
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Io
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Io
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Io Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Io
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Io
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Io
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Jupiter_Angular_Velocity
			Description	Jupiter Angular Velocity
			AbsolutePath	CentralBody/Jupiter
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Jupiter
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Jupiter
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Jupiter Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Jupiter
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Jupiter
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Jupiter
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Jupiter Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Jupiter
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Jupiter
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Jupiter
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Jupiter Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Jupiter
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Jupiter
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Jupiter
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Jupiter Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Jupiter
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Jupiter
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Jupiter
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Mars_Angular_Velocity
			Description	Mars Angular Velocity
			AbsolutePath	CentralBody/Mars
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Mars
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Mars
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Mars Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Mars
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Mars
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Mars
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Mars Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Mars
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Mars
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Mars
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Mars Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Mars
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Mars
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Mars
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Mars Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Mars
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Mars
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Mars
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Mercury_Angular_Velocity
			Description	Mercury Angular Velocity
			AbsolutePath	CentralBody/Mercury
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Mercury
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Mercury
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Mercury Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Mercury
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Mercury
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Mercury
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Mercury Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Mercury
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Mercury
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Mercury
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Mercury Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Mercury
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Mercury
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Mercury
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Mercury Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Mercury
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Mercury
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Mercury
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Moon_Angular_Velocity
			Description	Moon Angular Velocity
			AbsolutePath	CentralBody/Moon
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Moon
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Moon
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Moon Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Moon
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Moon
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Moon
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Moon Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Moon
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Moon
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Moon
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_J2000
			Description	Moon Centered Mean Earth Equator of J2000 Coordinate System
			AbsolutePath	CentralBody/Moon
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Moon
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Moon Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Moon
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Moon
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Moon
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Moon Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Moon
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Moon
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Moon
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Neptune_Angular_Velocity
			Description	Neptune Angular Velocity
			AbsolutePath	CentralBody/Neptune
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Neptune
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Neptune
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Neptune Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Neptune
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Neptune
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Neptune
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Neptune Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Neptune
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Neptune
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Neptune
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Neptune Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Neptune
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Neptune
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Neptune
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Neptune Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Neptune
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Neptune
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Neptune
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Phobos_Angular_Velocity
			Description	Phobos Angular Velocity
			AbsolutePath	CentralBody/Phobos
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Phobos
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Phobos
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Phobos Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Phobos
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Phobos
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Phobos
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Phobos Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Phobos
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Phobos
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Phobos
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Phobos Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Phobos
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Phobos
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Phobos
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Phobos Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Phobos
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Phobos
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Phobos
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Pluto_Angular_Velocity
			Description	Pluto Angular Velocity
			AbsolutePath	CentralBody/Pluto
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Pluto
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Pluto
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Pluto Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Pluto
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Pluto
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Pluto
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Pluto Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Pluto
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Pluto
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Pluto
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Pluto Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Pluto
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Pluto
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Pluto
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Pluto Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Pluto
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Pluto
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Pluto
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Saturn_Angular_Velocity
			Description	Saturn Angular Velocity
			AbsolutePath	CentralBody/Saturn
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Saturn
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Saturn
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Saturn Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Saturn
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Saturn
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Saturn
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Saturn Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Saturn
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Saturn
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Saturn
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Saturn Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Saturn
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Saturn
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Saturn
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Saturn Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Saturn
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Saturn
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Saturn
					END	AXES
		END	SYSTEM
		BEGIN	AXES
			Type	AXES_LIBRATION
			Name	SEM_L1
			Description	RLP Axes Definition
			AbsolutePath	CentralBody/Sun
				Primary	CentralBody/Sun
				Secondary	CentralBody/Earth
				Secondary	CentralBody/Moon
				PtType	0
				LabelX	
				LabelY	
				LabelZ	
		END	AXES
		BEGIN	AXES
			Type	AXES_LIBRATION
			Name	SEM_L2
			Description	RLP Axes Definition
			AbsolutePath	CentralBody/Sun
				Primary	CentralBody/Sun
				Secondary	CentralBody/Earth
				Secondary	CentralBody/Moon
				PtType	1
				LabelX	
				LabelY	
				LabelZ	
		END	AXES
		BEGIN	POINT
			Type	POINT_LIBRATION
			Name	SEM_L1
			Description	RLP Point Definition
			AbsolutePath	CentralBody/Sun
				Primary	CentralBody/Sun
				Secondary	CentralBody/Earth
				Secondary	CentralBody/Moon
				PtType	0
		END	POINT
		BEGIN	POINT
			Type	POINT_LIBRATION
			Name	SEM_L2
			Description	RLP Point Definition
			AbsolutePath	CentralBody/Sun
				Primary	CentralBody/Sun
				Secondary	CentralBody/Earth
				Secondary	CentralBody/Moon
				PtType	1
		END	POINT
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Sun_Angular_Velocity
			Description	Sun Angular Velocity
			AbsolutePath	CentralBody/Sun
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Sun
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Sun
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Sun Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Sun
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Sun
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Sun
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Sun Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Sun
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Sun
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Sun
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_Ecliptic_of_J2000
			Description	Sun Centered Mean Ecliptic of J2000 Coordinate System
			AbsolutePath	CentralBody/Sun
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Sun
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Mean_Ecliptic_of_J2000
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_J2000
			Description	Sun Centered Mean Earth Equator of J2000 Coordinate System
			AbsolutePath	CentralBody/Sun
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Sun
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Earth
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Sun Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Sun
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Sun
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Sun
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Sun Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Sun
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Sun
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Sun
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Titan_Angular_Velocity
			Description	Titan Angular Velocity
			AbsolutePath	CentralBody/Titan
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Titan
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Titan
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Titan Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Titan
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Titan
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Titan
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Titan Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Titan
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Titan
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Titan
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Titan Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Titan
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Titan
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Titan
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Titan Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Titan
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Titan
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Titan
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Triton_Angular_Velocity
			Description	Triton Angular Velocity
			AbsolutePath	CentralBody/Triton
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Triton
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Triton
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Triton Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Triton
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Triton
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Triton
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Triton Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Triton
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Triton
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Triton
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Triton Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Triton
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Triton
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Triton
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Triton Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Triton
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Triton
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Triton
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Uranus_Angular_Velocity
			Description	Uranus Angular Velocity
			AbsolutePath	CentralBody/Uranus
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Uranus
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Uranus
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Uranus Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Uranus
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Uranus
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Uranus
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Uranus Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Uranus
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Uranus
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Uranus
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Uranus Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Uranus
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Uranus
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Uranus
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Uranus Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Uranus
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Uranus
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Uranus
					END	AXES
		END	SYSTEM
		BEGIN	VECTOR
			Type	VECTOR_AXESDERIVATIVE
			Name	Venus_Angular_Velocity
			Description	Venus Angular Velocity
			AbsolutePath	CentralBody/Venus
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Venus
					END	AXES
				ReferenceAxes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Venus
					END	AXES
		END	VECTOR
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Fixed
			Description	Venus Centered Fixed Coordinate System
			AbsolutePath	CentralBody/Venus
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Venus
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Fixed
						AbsolutePath	CentralBody/Venus
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Inertial
			Description	Venus Centered Inertial Coordinate System
			AbsolutePath	CentralBody/Venus
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Venus
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	Inertial
						AbsolutePath	CentralBody/Venus
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_Mean_of_Date
			Description	Venus Centered Mean of Date Coordinate System
			AbsolutePath	CentralBody/Venus
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Venus
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	MOD
						AbsolutePath	CentralBody/Venus
					END	AXES
		END	SYSTEM
		BEGIN	SYSTEM
			Type	SYSTEM_ASSEMBLED
			Name	Centered_True_of_Date
			Description	Venus Centered True of Date Coordinate System
			AbsolutePath	CentralBody/Venus
				Origin
					BEGIN	POINT
						Type	POINT_LINKTO
						Name	Center
						AbsolutePath	CentralBody/Venus
					END	POINT
				Axes
					BEGIN	AXES
						Type	AXES_LINKTO
						Name	TOD
						AbsolutePath	CentralBody/Venus
					END	AXES
		END	SYSTEM
    END Crdn
    
    BEGIN SpiceExt
    END SpiceExt
    
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
			ReadBufferSize             1500
			QueuePollPeriod            20
			MaxRcvQueueEntries         1000
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
    
    BEGIN PODS
        MintimebtPasses       1800.000000
        MinObsThinTime        0.000000
        SummaryByFile         False
        GlobalConvCrit        2
        GlobalMinIter         1
        GlobalMaxIter         9
        ArcEdSigMult          3.500000
        ArcIRMS               200.000000
        ArcConvCrit           2
        ArcMinIter            4
        ArcMaxIter            9
        ArcMax1globalIter     19
        IterationCntl         Automatic
        IntegrationStepSize   60.000000
        ReferenceDate         1 Jul 2005 12:00:00.000000000
        RefDateControl        Fixed
        ODTimesControl        Fixed
        UseGM                 False
        EstimateGM            False
        GMValue               398600.441800000
        GMSigma               0.000000000
        SemiMajorAxis         6378137.000000
        InvFlatCoef           298.257223491
        SunPert               True
        MoonPert              True
        MarsPert              False
        VenusPert             False
        MercuryPert           False
        PlutoPert             False
        UranusPert            False
        SaturnPert            False
        JupiterPert           False
        NeptunePert           False
        MaxDegree             36
        MaxOrder              36
        FluxFile              I:\STK6.1\PODS\tables\tables.dat
        JPLFile               I:\STK6.1\PODS\ephem\jplde405.dat
        GeoFile               I:\STK6.1\PODS\gco_files\wgs84.gco
        PODSDirectory         I:\STK6.1\PODS
        TrkDataDirectory      I:\STK6.1\PODS\trk_data
        ScratchDirectory      C:\DOCUME~1\mward\LOCALS~1\Temp
        CleanupFlag           True
        PODSMessages          True
        TDMaxObs              1000
        TDMaxPB               168
        TDMaxMB               1000
        TDMaxDR               300
        VOMaxObs              1000
        VOIntSteps            31
        BDMaxSD               20
        BDMaxDR               2
        BEGIN SimData
          TruthTrop           On
          DeviatedTrop        On
          TruthDelay          Off
          DeviatedDelay       Off
          AddBias             Off
          AddNoise            Off
          AddTime             Off
          TimeBias            0.000000
          TimeStep            60.000000
          SimMeasData		17  12300  0  0.000174532925  0.000174532925  0
          SimMeasData		18  86400  0  0.00034906585  0.000174532925  0
          SimMeasData		51  102  0  23  12  0
          SimMeasData		52  630708  0  0.044  0.066  0
        END SimData
    END PODS

END Extensions

BEGIN SubObjects

Class Facility

	Fac_NotUsingRangeExamplePlugin
	Fac_UsingRangeExamplePlugin

END Class

Class Satellite

	Sat_SunSyncCriticalInclined

END Class

END SubObjects

END Scenario

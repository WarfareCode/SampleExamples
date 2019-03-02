stk.v.10.0
WrittenBy    STK_v10.0.0

BEGIN Radar

Name        thaad_radar

	BEGIN System


BEGIN RadarAntenna

	Type                    SquareHorn

	BEGIN Boresight

		RotationType		0
		qx		0.000000e+000
		qy		0.000000e+000
		qz		0.000000e+000
		qs		1.000000e+000
		RelOffsetX		0.000000e+000
		RelOffsetY		0.000000e+000
		RelOffsetZ		0.000000e+000

	END Boresight

	BEGIN SquareHorn

		Efficiency              7.500000e-001
		Dimension               1.000000e+000
		LinearBacklobeGain             1.000000e-003
	END SquareHorn

END RadarAntenna


		Monostatic		Yes
		LinearPeakPower		3.1622776601684e+005
		Wavelength		2.9979249999999999e-002
		FreqMode		0
		ComputeJamming		No

	BEGIN SystemTemperatureConstraintData

		UseComponents					0
		UseEarth						0
		UseSun						0
		UseAtmosphere					0
		UseRain						0
		UseCosmic						0
		UseExternal					0
		UseCloudFog					0
		UseTropoScint					0

	END SystemTemperatureConstraintData
		EnableSignalPSD		No
		RadarXmtrPowerAmpBw		3.000000e+007
		RadarRcvrLNABw		3.000000e+007
		NumberOfSignalPulses		20
		ApplyTransmitSideSpectrumFilter		No
		ApplyReceiveSideSpectrumFilter		No
		EnableTransmitOrthoPol		No


	BEGIN TransmitterPol


BEGIN Polarization

	Type                    0

	VertRefAxis             0

	LinearPolLeak                    0.000001

END Polarization


	END   TransmitterPol
		EnableReceiveOrthoPol		No


	BEGIN ReceiverPol


BEGIN Polarization

	Type                    0

	VertRefAxis             0

	LinearPolLeak                    0.000001

END Polarization


	END   ReceiverPol

	END System

	BEGIN SearchTrack

		Enabled		Yes
		Mode		0
		SubMode		0
		PRF		1.9000000000000e+004
		UnambigRng		7.8892752105263e+003
		UnambigVel		1.4240143750000e+002
		PulseWidthMode		1
		PulseWidth		2.6315789473684e-006
		DutyFactor		5.0000000000000e-002
		MLCFilter		No
		MLCFilterBW		0.0000000000000e+000
		SLCFilter		No
		SLCFilterBW		0.0000000000000e+000
		IntGainMode		0
		Linear_IntGainGoalVal		3.9810717055350e+001 1 1.0000000000000e+000 512
		IntGainFlag		0
		IntGainVal		1.0000000000000e+000
		IntGainExp		1.0000000000000e+000
		Pfa		1.0000000000000e-004
		IsCFAR		No
		RefCells		6

	END SearchTrack

	BEGIN SAR

		Enabled		No
		SARMode		0
		IntegrationTime		1.0000000000000e+000
		AzimuthResolution		1.0000000000000e+001
		PRFMode		0
		PRF		1.0000000000000e+003
		UnambigRng		1.4989622900000e+005
		AzBroadFactor		1.2000000000000e+000
		RngBroadFactor		1.2000000000000e+000
		IFBandwidth		1.0000000000000e+008
		RangeResMode		0
		RangeResolution		1.0000000000000e+001
		Bandwidth		1.7987547480000e+007
		PCRMode		0
		PulseCompRatio		1.0000000000000e+003
		Pulsewidth		5.5594015866359e-005
		SceneDepth		4.6328346555299e+004
		RangeChirp		3.2355186434525e+011
		LinearMultNoiseRatio		1.0000000000000e-002

	END SAR

	BEGIN SystemTemperature

		ConstantTemp      269.619026
		LinearNoiseFigure       1.258925
		WaveguideTemp     290.000000
		LinearWaveguideLoss     0.794328
		ConstantAntTemp     100.000000

	END SystemTemperature

	BEGIN RfRain

		RainModel               Off

	END RfRain

BEGIN Extensions
    
    BEGIN Graphics

	BEGIN Graphics

		ShowRdr		Yes
		ShowXmtTgt		No
		ShowXmtRdr		No
		ShowContour		Yes
		UseSinglePulse		No
		LinearSNR		3.981072e+001
		LineWidth		1.000000e+000
		LineColor		#9b30ff
		LineStyle		1
		XmtTgtWidth		1.000000e+000
		XmtTgtColor		#9b30ff
		XmtTgtStyle		2
		XmtRdrWidth		1.000000e+000
		XmtRdrColor		#9b30ff
		XmtRdrStyle		2
		ShowRdrTgtGrp		No
		RdrTgtGrpMarker		0
		RdrTgtGrpColor		#000000
		ShowXmtTgtGrp		No
		XmtTgtGrpMarker		0
		XmtTgtGrpColor		#000000
		ShowXmtRdrGrp		No
		XmtRdrGrpMarker		0
		XmtRdrGrpColor		#000000

	BEGIN Antenna


BEGIN Graphics

	ShowGfx           On
	Relative          On
	ShowBoresight     On
	BoresightMarker   4
	BoresightColor    #ffffff

END Graphics

	END Antenna


	END Graphics
    END Graphics
    
    BEGIN ContourGfx
	ShowContours      On
    END ContourGfx
    
    BEGIN ExternData
    END ExternData
    
    BEGIN ADFFileData
    END ADFFileData
    
    BEGIN AccessConstraints
		ElevationAngle		Min  5.0000000000e+000   IncludeIntervals 
		LineOfSight   IncludeIntervals 
		RdrXmtTgtAccess   IncludeIntervals 
    END AccessConstraints
    
    BEGIN ObjectCoverage
    END ObjectCoverage
    
    BEGIN Desc
    END Desc
    
    BEGIN Refraction
		RefractionModel	Effective Radius Method

		UseRefractionInAccess		No

		BEGIN ModelData
			RefractionCeiling	5.00000000000000e+003
			MaxTargetAltitude	1.00000000000000e+004
			EffectiveRadius		1.33333333333333e+000

			UseExtrapolation	 Yes


		END ModelData
    END Refraction
    
    BEGIN Contours
	ActiveContourType Antenna Gain

	BEGIN ContourSet Antenna Gain
		Altitude          0.000000e+000
		ShowAtAltitude    Off
		Projected         On
		Relative          On
		ShowLabels        On
		LineWidth         1.000000
		DecimalDigits     1
		ColorRamp         Off
		ColorRampStartColor   #87cefa
		ColorRampEndColor     #00ced1

		BEGIN ContourLevel
			Value            -60.000000
			Color            #8fbc8f
			LineStyle        0
		END ContourLevel

		BEGIN ContourLevel
			Value            -50.000000
			Color            #6b8e23
			LineStyle        0
		END ContourLevel

		BEGIN ContourLevel
			Value            -40.000000
			Color            #00ced1
			LineStyle        0
		END ContourLevel

		BEGIN ContourLevel
			Value            -30.000000
			Color            #87cefa
			LineStyle        0
		END ContourLevel

		BEGIN ContourLevel
			Value            -20.000000
			Color            #4169e1
			LineStyle        0
		END ContourLevel

		BEGIN ContourLevel
			Value            -10.000000
			Color            #ffffff
			LineStyle        0
		END ContourLevel
		BEGIN ContourDefinition
		BEGIN CntrAntAzEl
			BEGIN AzElPattern
				BEGIN AzElPatternDef
					SetResolutionTogether 0
					CoordinateSystem 0
					NumAzPoints      50
					AzimuthRes       7.346939
					MinAzimuth       -180.000000
					MaxAzimuth       180.000000
					NumElPoints      30
					ElevationRes     0.344828
					MinElevation     0.000000
					MaxElevation     10.000000
				END AzElPatternDef
			END AzElPattern
		END CntrAntAzEl
		END ContourDefinition
	END ContourSet

	BEGIN ContourSet EIRP
		Altitude          0.000000e+000
		ShowAtAltitude    Off
		Projected         On
		Relative          Off
		ShowLabels        On
		LineWidth         1.000000
		DecimalDigits     1
		ColorRamp         Off
		ColorRampStartColor   #6b8e23
		ColorRampEndColor     #8fbc8f
		BEGIN ContourDefinition
		BEGIN CntrAntAzEl
			BEGIN AzElPattern
				BEGIN AzElPatternDef
					SetResolutionTogether 0
					CoordinateSystem 0
					NumAzPoints      50
					AzimuthRes       0.000000
					MinAzimuth       -180.000000
					MaxAzimuth       180.000000
					NumElPoints      30
					ElevationRes     0.000000
					MinElevation     0.000000
					MaxElevation     10.000000
				END AzElPatternDef
			END AzElPattern
		END CntrAntAzEl
		END ContourDefinition
	END ContourSet

	BEGIN ContourSet Flux Density
		Altitude          0.000000e+000
		ShowAtAltitude    Off
		Projected         On
		Relative          Off
		ShowLabels        On
		LineWidth         1.000000
		DecimalDigits     1
		ColorRamp         Off
		ColorRampStartColor   #ffd700
		ColorRampEndColor     #ba55d3
		BEGIN ContourDefinition
		BEGIN CntrAntLatLon
			SetResolutionTogether   true
			Resolution	9.000000  9.000000
			ElevationAngleConstraint	90.000000
			BEGIN LatLonSphGrid
				Centroid	0.000000	0.000000
				ConeAngle	0.000000
				NumPts		50	20
				Altitude	0.000000
			END LatLonSphGrid
		END CntrAntLatLon
		END ContourDefinition
	END ContourSet

	BEGIN ContourSet RIP
		Altitude          0.000000e+000
		ShowAtAltitude    Off
		Projected         On
		Relative          Off
		ShowLabels        On
		LineWidth         1.000000
		DecimalDigits     1
		ColorRamp         Off
		ColorRampStartColor   #ff69b4
		ColorRampEndColor     #d2691e
		BEGIN ContourDefinition
		BEGIN CntrAntLatLon
			SetResolutionTogether   true
			Resolution	9.000000  9.000000
			ElevationAngleConstraint	90.000000
			BEGIN LatLonSphGrid
				Centroid	0.000000	0.000000
				ConeAngle	0.000000
				NumPts		50	20
				Altitude	0.000000
			END LatLonSphGrid
		END CntrAntLatLon
		END ContourDefinition
	END ContourSet
    END Contours
    
    BEGIN Crdn
    END Crdn
    
    BEGIN VO
    END VO
    
    BEGIN 3dVolume
	ActiveVolumeType  Antenna Beam

	BEGIN VolumeSet Antenna Beam
	Scale 200000.000000
	NumericGainOffset 1.000000
	Frequency 2997924580.000000
	ShowAsWireframe 0
				BEGIN AzElPatternDef
					SetResolutionTogether 0
					CoordinateSystem 0
					NumAzPoints      50
					AzimuthRes       7.346939
					MinAzimuth       -180.000000
					MaxAzimuth       180.000000
					NumElPoints      50
					ElevationRes     1.836735
					MinElevation     0.000000
					MaxElevation     90.000000
				END AzElPatternDef
	END VolumeSet
Begin VolumeGraphics
	ShowContours    No
	ShowVolume No
End VolumeGraphics
    END 3dVolume

END Extensions

END Radar


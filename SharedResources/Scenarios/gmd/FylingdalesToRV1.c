stk.v.10.0
WrittenBy    STK_v10.0.0

BEGIN Chain

Name  FylingdalesToRV1
BEGIN Definition

   Type        Chain
   Operator    Or
   Order       1
   Recompute   Yes
   IntervalType    0
   ComputeIntervalStart    0.000000
   ComputeIntervalStop     86400.000000
    ComputeIntervalPtr
	BEGIN	EVENTINTERVAL
			BEGIN Interval
				Start	1 Jun 2001 18:00:00.000000000
				Stop	2 Jun 2001 18:00:00.000000000
			END Interval
			IntervalState	Explicit
	END	EVENTINTERVAL

   UseSaveIntervalFile    No
   UseMinAngle     No
   UseMaxAngle     No
   UseMinLinkTime     No
   LTDelayCriterion    2.000000
   TimeConvergence     0.005000
   AbsValueConvergence 1.000000e-014
   RelValueConvergence 1.000000e-008
   MaxTimeStep         360.000000
   MinTimeStep         1.000000e-002
   UseLightTimeDelay   Yes
    DetectEventsUsingSamplesOnly No
    Object  Constellation/Fylingdales
    Object  Missile/TH1_RV1
   SaveMode    1
BEGIN StrandAccesses

  Strand    Facility/Fylingdales/Sensor/f_tracking_rv1 To Missile/TH1_RV1
    Start    375.37305847356265
    Stop     447.45776006007054
    Start    1695.3002713543697
    Stop     1789.2815115738651
  Strand    Facility/Fylingdales/Sensor/f_tracking2_rv1 To Missile/TH1_RV1
    Start    447.45949846780206
    Stop     1695.2995143154344
END StrandAccesses

   UseLoadIntervalFile    No

END Definition

BEGIN Extensions
    
    BEGIN Graphics

BEGIN Attributes

StaticColor					#0000ff
AnimationColor					#ffff00
AnimationLineWidth					1.000000
StaticLineWidth					3.000000

END Attributes

BEGIN Graphics
    ShowGfx                On
    ShowStatic             Off
    ShowAnimationHighlight Off
    ShowAnimationLine      On
    ShowLinkDirection      Off
END Graphics
    END Graphics
    
    BEGIN ADFFileData
    END ADFFileData
    
    BEGIN Desc
    END Desc
    
    BEGIN Crdn
    END Crdn
    
    BEGIN VO
    END VO

END Extensions

END Chain


stk.v.10.0
WrittenBy    STK_v10.0.0

BEGIN Chain

Name  display_time
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
				Start	1 Jul 1998 00:00:00.000000000
				Stop	2 Jul 1998 00:00:00.000000000
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
    Object  Facility/ar_riyad/Sensor/tracking
    Object  Facility/ar_riyad/Sensor/tracking/Radar/thaad_radar
    Object  Missile/scud
   SaveMode    2
   UseLoadIntervalFile    No

END Definition

BEGIN Extensions
    
    BEGIN Graphics

BEGIN Attributes

StaticColor					#ffff00
AnimationColor					#ffff00
AnimationLineWidth					1.000000
StaticLineWidth					3.000000

END Attributes

BEGIN Graphics
    ShowGfx                On
    ShowStatic             Off
    ShowAnimationHighlight Off
    ShowAnimationLine      Off
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


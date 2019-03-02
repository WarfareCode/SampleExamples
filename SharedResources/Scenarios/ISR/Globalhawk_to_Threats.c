stk.v.10.0
WrittenBy    STK_v10.0.0

BEGIN Chain

Name  Globalhawk_to_Threats
BEGIN Definition

   Type        Chain
   Operator    Or
   Order       1
   Recompute   No
   IntervalType    0
   ComputeIntervalStart    0.000000
   ComputeIntervalStop     172801.000000
    ComputeIntervalPtr
	BEGIN	EVENTINTERVAL
			BEGIN Interval
				Start	4 Jul 2000 00:00:00.000000000
				Stop	6 Jul 2000 00:00:01.000000000
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
    Object  Constellation/Globalhawk
    Object  Constellation/Threats
   SaveMode    1
BEGIN StrandAccesses

  Strand    Aircraft/Globalhawk/Sensor/GHawk_Main to Facility/DMZ
    Start    79522.455169808411
    Stop     79564.44029429114
  Strand    Aircraft/Globalhawk/Sensor/GHawk_Main to Facility/Munson_Corridor
    Start    79698.239655737125
    Stop     79712.808585188148
  Strand    Aircraft/Globalhawk/Sensor/GHawk_Main to Facility/Wonsan
    Start    80407.625819428271
    Stop     80449.511452488528
  Strand    Aircraft/Globalhawk/Sensor/GHawk_Main to Facility/threat_3
    Start    80378.029676216087
    Stop     80410.711680801716
END StrandAccesses

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


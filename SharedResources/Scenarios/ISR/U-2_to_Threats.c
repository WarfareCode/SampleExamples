stk.v.10.0
WrittenBy    STK_v10.0.0

BEGIN Chain

Name  U-2_to_Threats
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
    Object  Aircraft/U-2/Sensor/Camera_1
    Object  Constellation/Threats
   SaveMode    1
BEGIN StrandAccesses

  Strand    Aircraft/U-2/Sensor/Camera_1 To Facility/DMZ
  Strand    Aircraft/U-2/Sensor/Camera_1 To Facility/Munson_Corridor
    Start    79332.181505633838
    Stop     79619.45195022026
  Strand    Aircraft/U-2/Sensor/Camera_1 To Facility/Wonsan
    Start    79674.729476500259
    Stop     80220.887999205574
  Strand    Aircraft/U-2/Sensor/Camera_1 To Facility/threat_1
    Start    79641.009555014738
    Stop     80383.134763149399
  Strand    Aircraft/U-2/Sensor/Camera_1 To Facility/threat_2
  Strand    Aircraft/U-2/Sensor/Camera_1 To Facility/threat_3
    Start    79696.09334528097
    Stop     80216.724220471908
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


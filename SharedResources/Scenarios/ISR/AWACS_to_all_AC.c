stk.v.10.0
WrittenBy    STK_v10.0.0

BEGIN Chain

Name  AWACS_to_all_AC
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
    Object  Aircraft/AWACS
    Object  Aircraft/Guardrail
    Object  Aircraft/AWACS
    Object  Aircraft/JSTARS
    Object  Aircraft/AWACS
    Object  Aircraft/Guardrail
    Object  Aircraft/AWACS
    Object  Aircraft/RC-135
   SaveMode    1
BEGIN StrandAccesses

  Strand    Aircraft/AWACS To Aircraft/Guardrail To Aircraft/AWACS To Aircraft/JSTARS To Aircraft/AWACS To Aircraft/Guardrail To Aircraft/AWACS To Aircraft/RC-135
    Start    79200
    Stop     81105.4085612739
END StrandAccesses

   UseLoadIntervalFile    No

END Definition

BEGIN Extensions
    
    BEGIN Graphics

BEGIN Attributes

StaticColor					#ffff00
AnimationColor					#00ffff
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


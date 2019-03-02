stk.v.10.0
WrittenBy    STK_v10.0.0

BEGIN Chain

Name  Guardrail_to_Threats
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
    Object  Constellation/Guardrail
    Object  Constellation/Threats
   SaveMode    1
BEGIN StrandAccesses

  Strand    Aircraft/Guardrail/Sensor/Left_Side_Guardrail_Sensor To Facility/DMZ
    Start    79200
    Stop     80121.329650953921
    Start    81170.299016331075
    Stop     81198.661273941005
  Strand    Aircraft/Guardrail/Sensor/Left_Side_Guardrail_Sensor To Facility/Munson_Corridor
    Start    79200
    Stop     80130.429161713226
    Start    81162.947894271201
    Stop     81198.661273941005
  Strand    Aircraft/Guardrail/Sensor/Left_Side_Guardrail_Sensor To Facility/Wonsan
    Start    79200
    Stop     80163.492893354749
  Strand    Aircraft/Guardrail/Sensor/Left_Side_Guardrail_Sensor To Facility/threat_1
    Start    79331.691699999996
    Stop     80172.17757862166
  Strand    Aircraft/Guardrail/Sensor/Left_Side_Guardrail_Sensor To Facility/threat_2
    Start    80110.084625363539
    Stop     80158.529194730276
    Start    81171.636188669261
    Stop     81198.661273941005
  Strand    Aircraft/Guardrail/Sensor/Left_Side_Guardrail_Sensor To Facility/threat_3
    Start    79200
    Stop     80164.728613451414
  Strand    Aircraft/Guardrail/Sensor/Right_Side_Guardrail_Sensor To Facility/DMZ
    Start    80126.293354083929
    Stop     80131.256180508542
    Start    80210.870435064149
    Stop     81115.596724036586
    Start    81117.861540712824
    Stop     81147.505350537904
  Strand    Aircraft/Guardrail/Sensor/Right_Side_Guardrail_Sensor To Facility/Munson_Corridor
    Start    80135.392836184037
    Stop     80138.703641309214
    Start    80213.568564340007
    Stop     81108.535343264695
  Strand    Aircraft/Guardrail/Sensor/Right_Side_Guardrail_Sensor To Facility/Wonsan
    Start    80170.940809693886
    Stop     80181.655707894228
    Start    80214.620017477719
    Stop     81106.999975670813
    Start    81172.382518906656
    Stop     81198.661273941005
  Strand    Aircraft/Guardrail/Sensor/Right_Side_Guardrail_Sensor To Facility/threat_1
    Start    80181.217094275416
    Stop     80183.14645747935
    Start    80214.942995129153
    Stop     81106.370034551568
    Start    81172.405106364749
    Stop     81198.661273941005
  Strand    Aircraft/Guardrail/Sensor/Right_Side_Guardrail_Sensor To Facility/threat_2
    Start    80163.491979337356
    Stop     80172.178359139813
  Strand    Aircraft/Guardrail/Sensor/Right_Side_Guardrail_Sensor To Facility/threat_3
    Start    80170.938650904543
    Stop     80182.181382622162
    Start    80214.565194422074
    Stop     81107.12339954215
    Start    81172.51132848594
    Stop     81198.661273941005
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


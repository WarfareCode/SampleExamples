stk.v.10.0
WrittenBy    STK_v10.0.0

BEGIN Chain

Name  JSTARS_to_Threats
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
    Object  Constellation/JSTARS
    Object  Constellation/Threats
   SaveMode    1
BEGIN StrandAccesses

  Strand    Aircraft/JSTARS/Sensor/JStars_Left_Side To Facility/DMZ
    Start    80796.103005709272
    Stop     81151.771847352225
  Strand    Aircraft/JSTARS/Sensor/JStars_Left_Side To Facility/Munson_Corridor
    Start    80688.858773885164
    Stop     81232.878295566115
  Strand    Aircraft/JSTARS/Sensor/JStars_Left_Side To Facility/Wonsan
    Start    81031.429083015915
    Stop     81605.905501996516
  Strand    Aircraft/JSTARS/Sensor/JStars_Left_Side To Facility/threat_1
    Start    81053.167559407637
    Stop     81630.081498614323
  Strand    Aircraft/JSTARS/Sensor/JStars_Left_Side To Facility/threat_2
    Start    80592.643739534949
    Stop     81611.949799140086
  Strand    Aircraft/JSTARS/Sensor/JStars_Left_Side To Facility/threat_3
    Start    81046.593424618681
    Stop     81605.904168853318
  Strand    Aircraft/JSTARS/Sensor/JStars_Right_Side To Facility/DMZ
    Start    79535.546428355563
    Stop     80167.871091252673
    Start    81821.125264882226
    Stop     81862.529810477805
  Strand    Aircraft/JSTARS/Sensor/JStars_Right_Side To Facility/Munson_Corridor
    Start    79452.893290418288
    Stop     80246.553048191287
    Start    81821.295152551844
    Stop     81862.529810477805
  Strand    Aircraft/JSTARS/Sensor/JStars_Right_Side To Facility/Wonsan
    Start    79200
    Stop     79933.378886005055
  Strand    Aircraft/JSTARS/Sensor/JStars_Right_Side To Facility/threat_1
    Start    79331.691699999996
    Stop     79912.609207053465
  Strand    Aircraft/JSTARS/Sensor/JStars_Right_Side To Facility/threat_2
    Start    79352.833700000003
    Stop     80084.20890324912
  Strand    Aircraft/JSTARS/Sensor/JStars_Right_Side To Facility/threat_3
    Start    79200
    Stop     79918.117038052646
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


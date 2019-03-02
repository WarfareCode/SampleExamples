stk.v.10.0
WrittenBy    STK_v10.0.0

BEGIN Chain

Name  RC-135_to_Threats
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
    Object  Constellation/RC-135
    Object  Constellation/Threats
   SaveMode    1
BEGIN StrandAccesses

  Strand    Aircraft/RC-135/Sensor/RC-135_Left_Side To Facility/DMZ
    Start    79419.852285030953
    Stop     79780.91776863343
  Strand    Aircraft/RC-135/Sensor/RC-135_Left_Side To Facility/Munson_Corridor
    Start    79590.611004526683
    Stop     79788.549896708995
  Strand    Aircraft/RC-135/Sensor/RC-135_Left_Side To Facility/Wonsan
    Start    79753.433938034286
    Stop     79952.205588038836
  Strand    Aircraft/RC-135/Sensor/RC-135_Left_Side To Facility/threat_1
    Start    79776.335967807856
    Stop     80126.594530013215
  Strand    Aircraft/RC-135/Sensor/RC-135_Left_Side To Facility/threat_2
    Start    79771.755989540427
    Stop     80127.815641418871
  Strand    Aircraft/RC-135/Sensor/RC-135_Left_Side To Facility/threat_3
    Start    79753.43319976276
    Stop     79927.513248598028
  Strand    Aircraft/RC-135/Sensor/RC-135_Right_Side To Facility/DMZ
    Start    80524.065010232516
    Stop     80792.500588064344
  Strand    Aircraft/RC-135/Sensor/RC-135_Right_Side To Facility/Munson_Corridor
    Start    80517.958330996495
    Stop     80618.053921144412
  Strand    Aircraft/RC-135/Sensor/RC-135_Right_Side To Facility/Wonsan
    Start    80306.711654136481
    Stop     80554.600887141962
    Start    81059.425974165046
    Stop     81105.408559945005
  Strand    Aircraft/RC-135/Sensor/RC-135_Right_Side To Facility/threat_1
    Start    80229.87034281464
    Stop     80533.22993588465
    Start    81059.652597332097
    Stop     81105.408559945005
  Strand    Aircraft/RC-135/Sensor/RC-135_Right_Side To Facility/threat_2
    Start    80230.246377854273
    Stop     80537.80634259645
  Strand    Aircraft/RC-135/Sensor/RC-135_Right_Side To Facility/threat_3
    Start    80331.873065437103
    Stop     80554.603656312131
    Start    81059.40037868057
    Stop     81105.408559945005
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


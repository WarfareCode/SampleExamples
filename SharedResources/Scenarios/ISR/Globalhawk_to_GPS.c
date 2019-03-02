stk.v.10.0
WrittenBy    STK_v10.0.0

BEGIN Chain

Name  Globalhawk_to_GPS
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
    Object  Aircraft/Globalhawk/Sensor/GPS_FOV
    Object  Constellation/GPS
   SaveMode    1
BEGIN StrandAccesses

  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-02
    Start    79200
    Stop     80181.434363538356
    Start    80184.013201397567
    Stop     81124.971570417547
    Start    81124.975436529523
    Stop     81260.828810612176
    Start    81271.981424467056
    Stop     81283.454054009911
    Start    81305.432654948032
    Stop     81309.312148711004
    Start    81338.997575440459
    Stop     81348.451188868989
    Start    81350.736808394169
    Stop     81355.22852558989
    Start    81357.220587327727
    Stop     81832.909596527243
    Start    81832.940609713216
    Stop     81840.758069869989
    Start    81869.003800218605
    Stop     81880.638943368715
    Start    81927.914065783974
    Stop     83070.91844795506
    Start    83070.999660477348
    Stop     83111.031401270826
    Start    83112.903745837742
    Stop     84000.264219366494
  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-03
    Start    79720.972310898098
    Stop     79755.563816519571
    Start    80242.181778288417
    Stop     80244.796537479648
    Start    80268.684861423608
    Stop     80296.019138873904
    Start    80417.266684821399
    Stop     81124.971570417547
    Start    81124.975436529523
    Stop     81260.828810612176
    Start    81271.981424467056
    Stop     81282.601534639529
    Start    81350.85178568204
    Stop     81355.22852558989
    Start    81392.857094227511
    Stop     81832.909596527243
    Start    81927.914065783974
    Stop     83070.91844795506
    Start    83070.999660477348
    Stop     83111.031401270826
    Start    83112.903745837742
    Stop     84000.264219366494
  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-04
    Start    79200
    Stop     79682.861745477916
    Start    79722.523897182458
    Stop     80132.409219720328
    Start    80153.320103459424
    Stop     80181.231456815498
    Start    80184.013201397567
    Stop     80524.442950631928
    Start    80930.060228019836
    Stop     80963.629808574653
    Start    80988.818063129365
    Stop     81097.427271554188
    Start    81235.796191066751
    Stop     81247.646857373446
    Start    81459.308760593151
    Stop     81508.661671865848
    Start    81517.074068629794
    Stop     81559.839209462501
    Start    82102.592525530155
    Stop     82181.184114535223
  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-07
    Start    81805.849916366671
    Stop     81812.409785099444
    Start    81832.940609713216
    Stop     81840.758069869989
    Start    81869.003800218605
    Stop     81880.638943368715
    Start    82544.558795661578
    Stop     82580.965143977039
    Start    83074.764082071386
    Stop     83111.031401270826
    Start    83883.313404499277
    Stop     83943.175211400972
    Start    83972.553693505106
    Stop     84000.264219366494
  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-08
    Start    79683.519890278374
    Stop     79719.421153870368
  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-09
    Start    79200
    Stop     79385.051406182902
    Start    79680.649444414259
    Stop     79744.239504870435
    Start    81124.975436529523
    Stop     81166.394196396926
    Start    81198.641290705622
    Stop     81222.358002481036
    Start    81338.997575440459
    Stop     81348.451188868989
    Start    81357.220587327727
    Stop     81416.078253347383
    Start    81720.616023726878
    Stop     81740.279110794043
  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-15
    Start    79200
    Stop     79686.169876562795
    Start    79702.11646656935
    Stop     80181.434363538356
    Start    80184.013201397567
    Stop     81124.971570417547
    Start    81166.849590076032
    Stop     81197.741010300306
    Start    81223.416735731982
    Stop     81260.828810612176
    Start    81271.981424467056
    Stop     81283.454054009911
    Start    81305.432654948032
    Stop     81309.312148711004
    Start    81350.736808394169
    Stop     81355.205216567178
    Start    81418.275243837707
    Stop     81637.817809702334
    Start    81669.902073267323
    Stop     81717.04933643018
    Start    81743.333862706611
    Stop     81832.909596527243
    Start    81832.940609713216
    Stop     81840.758069869989
    Start    81976.446431283432
    Stop     82013.284310080489
    Start    82019.246551141434
    Stop     82221.300523584534
    Start    82231.316043959087
    Stop     82244.910430278687
    Start    82268.524855775468
    Stop     82300.952674877015
    Start    82330.102819784923
    Stop     82835.412917181515
    Start    83412.973947762119
    Stop     83526.124006815662
    Start    83975.006660266125
    Stop     84000.264219366494
  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-19
    Start    79200
    Stop     80181.434363538356
    Start    80244.155015231445
    Stop     80271.994133099768
    Start    80290.055121675075
    Stop     80933.148995138283
    Start    80959.696880864518
    Stop     80992.214016709797
    Start    80998.782589276787
    Stop     81124.971570417547
    Start    81124.975436529523
    Stop     81236.01090347147
    Start    81272.244972430315
    Stop     81283.454054009911
    Start    81305.432654948032
    Stop     81309.312148711004
    Start    81338.997575440459
    Stop     81348.451188868989
    Start    81350.736808394169
    Stop     81355.22852558989
    Start    81357.220587327727
    Stop     81462.097337526662
    Start    81469.524682732328
    Stop     81777.095114423137
    Start    81815.594275007301
    Stop     81830.692820478071
    Start    81869.003800218605
    Stop     81880.638943368715
    Start    81927.914065783974
    Stop     82101.727652185829
    Start    82121.913289837306
    Stop     82451.320285140799
    Start    82461.137906344884
    Stop     82540.573229746675
    Start    82584.5185230801
    Stop     82889.50414489313
    Start    83070.999660477348
    Stop     83078.511681507429
  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-20
    Start    79200
    Stop     79682.740751360703
    Start    79754.171687927723
    Stop     80181.434363538356
    Start    80184.013201397567
    Stop     81050.921053400831
    Start    81165.892556139879
    Stop     81260.828810612176
    Start    81271.981424467056
    Stop     81283.454054009911
    Start    81305.432654948032
    Stop     81309.312148711004
    Start    81338.997575440459
    Stop     81348.451188868989
    Start    81350.736808394169
    Stop     81355.22852558989
    Start    81357.220587327727
    Stop     81832.909596527243
    Start    81832.940609713216
    Stop     81840.758069869989
    Start    81869.003800218605
    Stop     81880.638943368715
    Start    81927.914065783974
    Stop     83070.91844795506
    Start    83070.999660477348
    Stop     83111.031401270826
    Start    83112.903745837742
    Stop     84000.264219366494
  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-21
    Start    83079.595467623847
    Stop     83109.039931766019
    Start    83887.034428822997
    Stop     83939.695903919797
    Start    83976.684968924237
    Stop     83977.914928123661
  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-23
    Start    80932.99851233435
    Stop     80960.0760688054
    Start    80991.134312545983
    Stop     81035.403806500763
    Start    81237.014036924258
    Stop     81253.464745616002
    Start    81460.652276074048
    Stop     81505.803609173556
    Start    81778.418732243066
    Stop     81813.023036970306
    Start    82100.905075101342
    Stop     82159.911626882837
    Start    82451.397909489533
    Stop     82461.09408404553
    Start    82540.606759273403
    Stop     82584.418898091244
    Start    82791.551200226371
    Stop     83070.91844795506
    Start    83112.903745837742
    Stop     83887.563364141039
    Start    83895.332206191102
    Stop     84000.264219366494
  Strand    Aircraft/Globalhawk/Sensor/GPS_FOV to Satellite/gps_2-25
    Start    79685.248703275953
    Stop     79750.463008347506
END StrandAccesses

   UseLoadIntervalFile    No

END Definition

BEGIN Extensions
    
    BEGIN Graphics

BEGIN Attributes

StaticColor					#ffff00
AnimationColor					#ffa500
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


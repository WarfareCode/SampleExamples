stk.v.5.0

BEGIN MissileSystem

Name		MissileSystem1

BEGIN MFTSpec

TRAJECTORY_OPTION		trajspec_reentry_angle
MISSILENAME			LRM_2-12000
LAUNCH				33.334038  44.397835  0.000000
DELTA_STARTTIME			0.000000
EPOCH				1 Jun 2001 18:00:00.000000000
TRAJECTORY_VALUE		30.000000

BEGIN ImpactSpec

IMPACTTYPE			lat_long
RVTYPE				all_rvs
NUMRV				3
		40.669800   -73.943848   0.000000
		40.615520   -73.724390   0.000000
		40.826780   -73.793150   0.000000

END ImpactSpec

END MFTSpec

BEGIN Extensions
    
    BEGIN Graphics

BEGIN Attributes

MarkerColor					-9419919
LineStyle					0
LineWidth					1.000000
MarkerStyle					4
FontStyle					0

END Attributes
    END Graphics
    
    BEGIN AccessConstraints
    END AccessConstraints
    
    BEGIN Desc
    END Desc

END Extensions

BEGIN SubObjects

Class Missile

	TH1_MSL1
	TH1_MSL2
	TH1_MSL3
	TH1_PBV3
	TH1_RV1
	TH1_RV2
	TH1_RV3
	TH1_SRD1
	TH1_STG1
	TH1_STG2
	TH1_STG3
	TH1_l2i

END Class

END SubObjects

END MissileSystem


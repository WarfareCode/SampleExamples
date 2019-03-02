#include "StdAfx.h"
#include ".\objectmodelhelper.h"

CObjectModelHelper::CObjectModelHelper()
{
}

CObjectModelHelper::~CObjectModelHelper(void)
{
}

void CObjectModelHelper::CreateSatellite(STKObjects::IAgStkObjectRootPtr pRoot)
{
	static int                iNext = 0;
	char                      buf[255];
	double                    aporad, 
		                      perrad;
	_bstr_t                   bstrName;
	IAgOrbitStateClassicalPtr pClassical;
	IAgStkObjectPtr           pNewObj;
	IAgSatellitePtr           pSat;
	IAgVePropagatorTwoBodyPtr pTwoBodyProp;
	IAgClassicalSizeShapeRadiusPtr pRadius;

	// Build the satellite name
	itoa(iNext++, buf, 10);
	bstrName = "Satellite";
	bstrName += buf;

	ASSERT(pRoot != NULL);

	// Add a new instance of the Satellite to the scenario
	pNewObj = pRoot->GetCurrentScenario()->GetChildren()->New(eSatellite, bstrName);

	pSat = pNewObj;

	// Set the propagator type
	pSat->SetPropagatorType( ePropagatorTwoBody );

	pTwoBodyProp = pSat->Propagator;

	// Get the initial state's classical representation
	pClassical = pTwoBodyProp->InitialState->Representation->ConvertTo(eOrbitStateClassical);

	pClassical->SizeShapeType = eSizeShapeRadius;

	pRadius = pClassical->SizeShape ;

	// Set up the orbit's initial state
	aporad = rand() % 12000;
	perrad = rand() % 12000;
	pRadius->ApogeeRadius = 6356.75231424 + aporad;
	pRadius->PerigeeRadius = 6356.75231424 + perrad;
	pClassical->Orientation->Inclination = rand() % 180;

	// Assign the orbit state
	pTwoBodyProp->InitialState->Representation->Assign( pClassical );

	// Build the satellite's orbit
	pTwoBodyProp->Propagate();
}
void CObjectModelHelper::CreateFacility(STKObjects::IAgStkObjectRootPtr pRoot)
{
	static int           iNext = 0;
	char                 buf[255];
	_bstr_t              bstrName; 
	IAgStkObjectPtr      pNewObj;
	IAgFacilityPtr       pFacility;
	IAgGeodeticPtr       pPos;

	ASSERT(pRoot != NULL);

	itoa(iNext++, buf, 10);
	bstrName = "Facility";
	bstrName += buf;
	pNewObj = pRoot->GetCurrentScenario()->GetChildren()->New(eFacility, bstrName);

	pFacility =  pNewObj;

	// Get facility position using geodetic representation
	pPos = pFacility->Position->ConvertTo( eGeodetic );

	// Configure the facility position
	pPos->Lat = rand() % 180 - 90;
	pPos->Lon = rand() % 360;

	// Assign the position to the facility
	pFacility->Position->Assign( pPos );

}
void CObjectModelHelper::NewScenario(STKObjects::IAgStkObjectRootPtr pRoot)
{
	ASSERT(pRoot != NULL);
	pRoot->NewScenario("Demo");
}
void CObjectModelHelper::CloseScenario(STKObjects::IAgStkObjectRootPtr pRoot)
{
	ASSERT(pRoot != NULL);
	pRoot->CloseScenario();
}
void CObjectModelHelper::PlayForward(STKObjects::IAgStkObjectRootPtr pRoot)
{
	ASSERT(pRoot != NULL);
	STKObjects::IAgAnimationPtr pAnimation( pRoot );
	pAnimation->PlayForward();
}
void CObjectModelHelper::PlayBackward(STKObjects::IAgStkObjectRootPtr pRoot)
{
	ASSERT(pRoot != NULL);
	STKObjects::IAgAnimationPtr pAnimation( pRoot );
	pAnimation->PlayBackward();
}
void CObjectModelHelper::Pause(STKObjects::IAgStkObjectRootPtr pRoot)
{
	ASSERT(pRoot != NULL);
	STKObjects::IAgAnimationPtr pAnimation( pRoot );
	pAnimation->Pause();
}
void CObjectModelHelper::Rewind(STKObjects::IAgStkObjectRootPtr pRoot)
{
	ASSERT(pRoot != NULL);
	STKObjects::IAgAnimationPtr pAnimation( pRoot );
	pAnimation->Rewind();
}

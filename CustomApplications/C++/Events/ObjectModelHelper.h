#pragma once

class CObjectModelHelper
{
public:
	CObjectModelHelper();
	~CObjectModelHelper(void);

	void CreateSatellite(STKObjects::IAgStkObjectRootPtr pRoot);
	void CreateFacility(STKObjects::IAgStkObjectRootPtr pRoot);
	void NewScenario(STKObjects::IAgStkObjectRootPtr pRoot);
	void CloseScenario(STKObjects::IAgStkObjectRootPtr pRoot);
	void PlayForward(STKObjects::IAgStkObjectRootPtr pRoot);
	void PlayBackward(STKObjects::IAgStkObjectRootPtr pRoot);
	void Pause(STKObjects::IAgStkObjectRootPtr pRoot);
	void Rewind(STKObjects::IAgStkObjectRootPtr pRoot);

};

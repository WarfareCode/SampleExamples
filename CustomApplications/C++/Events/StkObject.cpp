#include "StdAfx.h"
#include ".\stkobject.h"

CStkObject::CStkObject(IAgStkObjectPtr object)
{
	m_object = object;
	IAgDataProviderGroupPtr group;
	IAgDataProviderInfoPtr data,
		                   fixedInfo;

	IAgDataProviderCollectionPtr dpcollection = m_object->DataProviders;
			
	/********************************************************* 
	* get position info
	*********************************************************/
	data = dpcollection->GetItem("LLA State");
	data.QueryInterface(group.GetIID(), &group);
	fixedInfo = group->GetGroup()->GetItem("Fixed");
	fixedInfo.QueryInterface(m_position.GetIID(), &m_position);

	/********************************************************* 
	* get acceleration info
	*********************************************************/
	data = dpcollection->GetItem("Cartesian Acceleration");
	data.QueryInterface(group.GetIID(), &group);
	fixedInfo = group->GetGroup()->GetItem("Fixed");
	fixedInfo.QueryInterface(m_acceleration.GetIID(), &m_acceleration);


	/*********************************************************
	* get velocity info
	*********************************************************/
	data = dpcollection->GetItem("Cartesian Velocity");
	data.QueryInterface(group.GetIID(), &group);
	fixedInfo = group->GetGroup()->GetItem("Fixed");
	fixedInfo.QueryInterface(m_velocity.GetIID(), &m_velocity);

	/*********************************************************
	* get get attitude (PitchRollHeading(yaw)) info
	*********************************************************/
	data = dpcollection->GetItem("Body Axes Orientation");
	data.QueryInterface(group.GetIID(), &group);
	fixedInfo = group->GetGroup()->GetItem("VVLH");
	fixedInfo.QueryInterface(m_attitude.GetIID(), &m_attitude);

	//take note the below code only works if the parent object of this object
	//is the scenario
	/*********************************************************
	* Use the CONNECT command - DISQuery to get DIS data
	* not available through ObjectModel - ref REQ31603
	*********************************************************/
	IAgStkObjectPtr oObj = object->Parent->Parent;
	IAgStkObjectRootPtr root;
	oObj.QueryInterface(root.GetIID(), &root);

	IAgExecCmdResultPtr pResult = root->ExecuteCommand("DISQuery " + object->GetPath() + " IsDIS");
	m_isDIS = pResult->GetItem(0);

	/*********************************************************
	* if isDIS is NO, these commands can be skipped
	*********************************************************/
	pResult = root->ExecuteCommand("DISQuery " + object->GetPath() + " EntityID");
	m_entityID = pResult->GetItem(0);

	pResult = root->ExecuteCommand("DISQuery " + object->GetPath() + " EntityType");
	m_entityType = pResult->GetItem(0);
	
}

CStkObject::~CStkObject(void)
{
}

IAgDrResultPtr CStkObject::Position(double animTime, SAFEARRAY *llaNames)
{
	return m_position->ExecSingleElements(animTime, &llaNames);
}

IAgDrResultPtr CStkObject::Acceleration(double animTime, SAFEARRAY *xyzNames)
{
	return m_acceleration->ExecSingleElements(animTime, &xyzNames);
}

IAgDrResultPtr CStkObject::Velocity(double animTime, SAFEARRAY *xyzNames)
{
	return m_velocity->ExecSingleElements(animTime, &xyzNames);
}

IAgDrResultPtr CStkObject::Attitude(double animTime, SAFEARRAY *yprNames)
{
	return m_attitude->ExecSingleElements(animTime, &yprNames);
}

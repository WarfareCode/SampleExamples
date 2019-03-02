/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"
#include "RangeExample.h"
#include "AGI.Access.Constraint.Plugin.Examples.CPP_i.c"

using namespace std;
using namespace AccessConstraint;

CRangeExample::CRangeExample()
{
}

CRangeExample::~CRangeExample()
{
}

HRESULT CRangeExample::FinalConstruct()
{
	m_pStkPluginSite = NULL;
	m_pStkRootObject = NULL;
	m_DisplayName	 = std::string("CPP.RangeExample");

	return S_OK;
}

void CRangeExample::FinalRelease()
{
}


void CRangeExample::Message(AgEUtLogMsgType severity, std::string& msg)
{
	if(m_pStkPluginSite)
	{
		USES_CONVERSION;

		CComBSTR bstrMsg = A2W(msg.c_str());

		m_pStkPluginSite->Message( severity, bstrMsg );
	}
}

STDMETHODIMP CRangeExample::get_DisplayName(BSTR *pDisplayName)
{
	USES_CONVERSION;

	CComBSTR bstrName = A2W(m_DisplayName.c_str());

	return bstrName.CopyTo(pDisplayName);
}

STDMETHODIMP CRangeExample::Register(IAgAccessConstraintPluginResultRegister* Result )
{
	USES_CONVERSION;

	CComBSTR msg(A2W("Distance"));

	Result->put_BaseObjectType(eAircraft);
	Result->put_BaseDependency(eDependencyRelativePosVel);
	Result->put_Dimension(msg);	
	Result->put_MinValue(0.0);

	Result->put_TargetDependency (eDependencyRelativePosVel);
	Result->AddTarget(eFacility);
	Result->AddTarget(eGroundVehicle);
	Result->AddTarget(eSatellite);		
	Result->Register();

	std::string msgStr = m_DisplayName + std::string(": Register(Aircraft to Facility/GroundVehicle/Satellite)");
	msg = A2W(msgStr.c_str());

	Result->Message(eUtLogMsgInfo, msg);

	Result->put_BaseObjectType(eFacility);
	Result->ClearTargets();
	Result->AddTarget(eAircraft);
	Result->AddTarget(eSatellite);	
	Result->Register();

	msgStr = m_DisplayName + std::string(": Register(Facility to Aircraft/Satellite)");
	msg = A2W(msgStr.c_str());

	Result->Message(eUtLogMsgInfo, msg);

	Result->put_BaseObjectType(eGroundVehicle);
	Result->Register();

	msgStr = m_DisplayName + std::string(": Register(GroundVehicle to Aircraft/Satellite)");
	msg = A2W(msgStr.c_str());

	Result->Message(eUtLogMsgInfo, msg);

	Result->put_BaseObjectType(eSatellite);
	Result->ClearTargets();
	Result->AddTarget(eAircraft);
	Result->AddTarget(eFacility);
	Result->AddTarget(eGroundVehicle);	
	Result->Register();

	msgStr = m_DisplayName + std::string(": Register(Satellite to Aircraft/Facility/GroundVehicle)");
	msg = A2W(msgStr.c_str());

	Result->Message(eUtLogMsgInfo, msg);

	return S_OK;
}

STDMETHODIMP CRangeExample::Init(IAgUtPluginSite* pSite, VARIANT_BOOL* pRet)
{
	pSite->QueryInterface(&m_pStkPluginSite);
	
	Message( eUtLogMsgInfo, m_DisplayName+": Init()" );

	// Demonstrate getting ObjectModel handle

	if(m_pStkPluginSite)
	{
		//----------------------------------------------------
		// Get a pointer to the STK Object Model root object
		//----------------------------------------------------
		CComPtr<IDispatch> pDisp;
		if(SUCCEEDED(m_pStkPluginSite->get_StkRootObject(&pDisp)))
		{
			pDisp->QueryInterface(&m_pStkRootObject);
		}
	}

	*pRet = VARIANT_TRUE;

	return S_OK;
}

STDMETHODIMP CRangeExample::PreCompute(IAgAccessConstraintPluginResultPreCompute* Result,
									   VARIANT_BOOL* pRet)
{
	Message( eUtLogMsgInfo, m_DisplayName+": PreCompute()" );

	// Demonstrate using ObjectModel handle

	if(m_pStkRootObject)
	{
		CComPtr<STKObjects::IAgStkObject> pScen;

		if(SUCCEEDED(m_pStkRootObject->get_CurrentScenario(&pScen)))
		{
			CComBSTR scenName;
	
			if(SUCCEEDED(pScen->get_InstanceName(&scenName)))
			{
				USES_CONVERSION;

				std::string currentScenario = W2A(scenName);

				std::string msg = std::string("Current Scenario is ");
				msg += currentScenario;

				Message(eUtLogMsgInfo, msg);
			}
		}
	}

	*pRet = VARIANT_TRUE;

	return S_OK;
}

STDMETHODIMP CRangeExample::Evaluate( 
	IAgAccessConstraintPluginResultEval* Result, 
	IAgAccessConstraintPluginObjectData* fromObject, 
	IAgAccessConstraintPluginObjectData* toObject,
	VARIANT_BOOL* pRet)
{
	if(Result)
	{
		double range;
		if(SUCCEEDED(Result->get_LightPathRange(&range)))
		{
			if(SUCCEEDED(Result->put_Value(range)))
			{
				*pRet = VARIANT_TRUE;

				return S_OK;
			}
		}
	}

	*pRet = VARIANT_FALSE;

	return E_FAIL;
}

STDMETHODIMP CRangeExample::PostCompute(IAgAccessConstraintPluginResultPostCompute* Result,
									   VARIANT_BOOL* pRet)
{
	Message( eUtLogMsgInfo, m_DisplayName+": PostCompute()" );

	*pRet = VARIANT_TRUE;

	return S_OK;
}

STDMETHODIMP CRangeExample::Free()
{
	Message( eUtLogMsgInfo, m_DisplayName+": Free()" );

	m_pStkPluginSite = NULL;
	m_pStkRootObject = NULL;

	return S_OK;
}

/**********************************************************************/
/*           Copyright 2007, Analytical Graphics, Inc.                */
/**********************************************************************/
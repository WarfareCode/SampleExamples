/**********************************************************************/
/*           Copyright 2009, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"

#include "Example1.h"

#include "Agi.As.EOMFunc.Plugin.CPP.Examples_i.c"

#include <iostream>
using namespace std;

#include <cmath>

CExample1::CExample1()
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.Constructor(): --> Entered\n");
	#endif

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.Constructor(): <-- Exited\n");
	#endif
}

CExample1::~CExample1()
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.Destructor(): --> Entered\n");
	#endif

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EndMdl.CPP.Example1.Destructor(): <-- Exited\n");
	#endif
}

HRESULT CExample1::FinalConstruct()
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.FinalConstruct(): --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	this->m_Name	= CComBSTR(L"EOMFunc.Plugin.CPP.Example1");
	this->m_deltaVAxes = CComBSTR(L"VNC(Earth)");

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.FinalConstruct(): <-- Exited\n");
	#endif

	return hr;
}

void CExample1::FinalRelease() 
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.FinalRelease(): --> Entered\n");
	#endif

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.FinalRelease(): <-- Exited\n");
	#endif
}

//=========================
// IAgUtPluginConfig Methods
//=========================
STDMETHODIMP CExample1::GetPluginConfig(IAgAttrBuilder * pAttrBuilder, IDispatch** ppDispScope)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pAttrBuilder )
		EX_OUT_RETVAL_INTERFACE_PARAM( ppDispScope )
	EX_END_PARAMS()

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.GetPluginConfig(): --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	try
	{
		if( !m_pDispScope )
		{
			EXCEPTION_HR( pAttrBuilder->NewScope( &m_pDispScope ) );

			//====================
			// General Attributes
			//====================
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("DeltaVAxes"), CComBSTR("Axes in which to integrate the delta-V"), CComBSTR("DeltaVAxes"), eAddFlagReadOnly ) );
		}

		EXCEPTION_HR( m_pDispScope.CopyTo( ppDispScope ) );
	
	}
	catch( HRESULT r )
	{
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("EOMFunc.CPP.Example1.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
	}
	catch(...)
	{
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("EOMFunc.CPP.Example1.Init() <-> Exception = \n");
		#endif

		hr = E_FAIL;
	}

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.GetPluginConfig(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::VerifyPluginConfig(IAgUtPluginConfigVerifyResult * pPluginCfgResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pPluginCfgResult )
	EX_END_PARAMS()

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.VerifyPluginConfig(): --> Entered\n");
	#endif

	EX_HR( pPluginCfgResult->put_Result( VARIANT_TRUE ) );
	EX_HR( pPluginCfgResult->put_Message( BSTR( "Ok" ) ) ); 

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.VerifyPluginConfig(): <-- Exited\n");
	#endif

	return S_OK;
}

//=========================
// IAgGatorPluginEngineModel Methods
//=========================
STDMETHODIMP CExample1::Init(IAgUtPluginSite* pUtPluginSite, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pUtPluginSite )
		EX_OUT_RETVAL_PARAM( pResult )
	EX_END_PARAMS()

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.Init(): --> Entered\n");
	#endif

	HRESULT hr		= S_OK;
	bool	br		= false;

	try
	{
		this->m_pUtPluginSite = pUtPluginSite;
		
		if( this->m_pUtPluginSite )
		{
			EXCEPTION_HR( m_pUtPluginSite->QueryInterface( &m_pStkPluginSite ) );

			EXCEPTION_HR( m_pStkPluginSite->get_VectorToolProvider( &m_pCrdnPluginProvider ) );

			if (m_pCrdnPluginProvider)
			{
				EXCEPTION_HR( m_pCrdnPluginProvider->ConfigureAxes( CComBSTR("ICRF"), CComBSTR("CentralBody/Earth"), m_deltaVAxes, CComBSTR(""), &m_pCrdnConfiguredAxes ) );
			}
		
			br = true;
		}
	}
	catch( HRESULT r )
	{
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("EOMFunc.CPP.Example1.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
		br = false;
	}
	catch(...)
	{
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("EOMFunc.CPP.Example1.Init() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pResult = br ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.Init(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::Register(IAgAsEOMFuncPluginRegisterHandler* pRegisterHandler, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pRegisterHandler )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.Register() --> Entered\n");
	#endif

	HRESULT hr  = S_OK;
	bool	br	= true;

	try
	{
        // plugin needs the thrust vector and the mass
        pRegisterHandler->RegisterInput(AgEAsEOMFuncPluginInputStateValuesThrustX);
        pRegisterHandler->RegisterInput(AgEAsEOMFuncPluginInputStateValuesThrustY);
        pRegisterHandler->RegisterInput(AgEAsEOMFuncPluginInputStateValuesThrustZ);

        pRegisterHandler->RegisterInput(AgEAsEOMFuncPluginInputStateValuesTotalMass);

        // plugin gives the derivative of effective impulse and integrated delta-V
        pRegisterHandler->RegisterUserDerivativeOutput(CComBSTR("EffectiveImpulse"));
        pRegisterHandler->RegisterUserDerivativeOutput(CComBSTR("IntegratedDeltaVx"));
        pRegisterHandler->RegisterUserDerivativeOutput(CComBSTR("IntegratedDeltaVy"));
        pRegisterHandler->RegisterUserDerivativeOutput(CComBSTR("IntegratedDeltaVz"));

        // plugin only needs to be called on evaluate
        pRegisterHandler->ExcludeEvent(AgEAsEOMFuncPluginEventTypesPrePropagate);
        pRegisterHandler->ExcludeEvent(AgEAsEOMFuncPluginEventTypesPreNextStep);
        pRegisterHandler->ExcludeEvent(AgEAsEOMFuncPluginEventTypesPostPropagate);


		hr = S_OK;
		br = true;
	}
	catch( HRESULT r )
	{
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("EOMFunc.CPP.Example1.Register() <-> Exception\n");
		#endif

		hr = r;
		br = false;
	}
	catch(...)
	{
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("EOMFunc.CPP.Example1.Register() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pResult = br ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.Register() <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::SetIndices(IAgAsEOMFuncPluginSetIndicesHandler* pSetIndicesHandler, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pSetIndicesHandler )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.SetIndices() --> Entered\n");
	#endif

	HRESULT hr  = S_OK;
	bool	br	= true;

	try
	{
        // get the indices for the input variables
        pSetIndicesHandler->GetInputIndex(AgEAsEOMFuncPluginInputStateValuesThrustX, &m_thrustXIndex);
        pSetIndicesHandler->GetInputIndex(AgEAsEOMFuncPluginInputStateValuesThrustY, &m_thrustYIndex);
        pSetIndicesHandler->GetInputIndex(AgEAsEOMFuncPluginInputStateValuesThrustZ, &m_thrustZIndex);
        pSetIndicesHandler->GetInputIndex(AgEAsEOMFuncPluginInputStateValuesTotalMass, &m_massIndex);

        // get the indices for the derivatives we will output
        pSetIndicesHandler->GetUserDerivativeOutputIndex(CComBSTR("EffectiveImpulse"), &m_effectiveImpulseIndex);
        pSetIndicesHandler->GetUserDerivativeOutputIndex(CComBSTR("IntegratedDeltaVx"), &m_integratedDeltaVxIndex);
        pSetIndicesHandler->GetUserDerivativeOutputIndex(CComBSTR("IntegratedDeltaVy"), &m_integratedDeltaVyIndex);
        pSetIndicesHandler->GetUserDerivativeOutputIndex(CComBSTR("IntegratedDeltaVz"), &m_integratedDeltaVzIndex);

		hr = S_OK;
		br = true;
	}
	catch( HRESULT r )
	{
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("EOMFunc.CPP.Example1.SetIndices() <-> Exception\n");
		#endif

		hr = r;
		br = false;
	}
	catch(...)
	{
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("EOMFunc.CPP.Example1.SetIndices() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pResult = br ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.SetIndices() <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::Calc(AgEAsEOMFuncPluginEventTypes eventType, 
							 IAgAsEOMFuncPluginStateVector* pStateVector, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( eventType )
		EX_IN_INTERFACE_PARAM( pStateVector )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	HRESULT	hr = S_OK;
	bool	br = true;

	try
	{
        // get the current thrust values, and give back the derivatives of
        // effective impulse and the integrated delta V components

        // get thrust
		double thrustX, thrustY, thrustZ;
        pStateVector->GetInputValue(m_thrustXIndex, &thrustX);
        pStateVector->GetInputValue(m_thrustYIndex, &thrustY);
        pStateVector->GetInputValue(m_thrustZIndex, &thrustZ);

        // get mass
        double mass;
		pStateVector->GetInputValue(m_massIndex, &mass);


        // derivative of effective impulse is the total thrust magnitude
        double thrustMag = sqrt(thrustX * thrustX + thrustY * thrustY + thrustZ * thrustZ);
        pStateVector->AddDerivativeOutputValue(m_effectiveImpulseIndex, thrustMag);

        // rotate thrust vector to desired integration frame for integrated delta-V
		VARIANT_BOOL res;
        m_pCrdnConfiguredAxes->TransformComponents(pStateVector, &thrustX, &thrustY, &thrustZ, &res);

        // the derivative of each integrated delta-V component is that component of thrust acceleration 
        pStateVector->AddDerivativeOutputValue(m_integratedDeltaVxIndex, thrustX / mass);
        pStateVector->AddDerivativeOutputValue(m_integratedDeltaVyIndex, thrustY / mass);
        pStateVector->AddDerivativeOutputValue(m_integratedDeltaVzIndex, thrustZ / mass);

	
		hr = S_OK;
		br = true;
	}
	catch( HRESULT r )
	{
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.EndMdl.CPP.Example1.Evaluate() <-> Exception\n");
		#endif

		hr = r;
		br = false;
	}
	catch( ... )
	{
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("EOMFunc.CPP.Example1.Evaluate() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pResult = br ? VARIANT_TRUE : VARIANT_FALSE;

	return hr;
}

STDMETHODIMP CExample1::Free()
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.Free() --> Entered\n");
	#endif

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.Free() <-- Exited\n");
	#endif
	
	return S_OK;
}

STDMETHODIMP CExample1::get_Name(BSTR* pVal)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.get_Name() --> Entered\n");
	#endif
	
	HRESULT hr = S_OK;

	hr = m_Name.CopyTo(pVal);

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "EOMFunc.CPP.Example1.get_Name(): ";
	msg += W2A(m_Name);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("EOMFunc.CPP.Example1.get_Name() <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::get_DeltaVAxes( BSTR* pVal )
{
	m_deltaVAxes.CopyTo( pVal );

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "CPP.Example1.get_DeltaVAxes(): ";
	msg += W2A(m_deltaVAxes.m_str);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}
STDMETHODIMP CExample1::put_DeltaVAxes( BSTR Val )
{
	m_deltaVAxes = Val;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "CPP.Example1.put_DeltaVAxes(): ";
	msg += W2A(m_deltaVAxes.m_str);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

/**********************************************************************/
/*           Copyright 2009, Analytical Graphics, Inc.                */
/**********************************************************************/
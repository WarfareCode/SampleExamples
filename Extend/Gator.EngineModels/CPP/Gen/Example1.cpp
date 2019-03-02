/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"

#include "Example1.h"

#include "Agi.As.Gator.EngMdl.Plugin.CPP.Examples_i.c"

#include <iostream>
using namespace std;

#include <cmath>

CExample1::CExample1()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.Constructor(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.Constructor(): <-- Exited\n");
	#endif
}

CExample1::~CExample1()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.Destructor(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EndMdl.CPP.Example1.Destructor(): <-- Exited\n");
	#endif
}

HRESULT CExample1::FinalConstruct()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.FinalConstruct(): --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	this->m_Name	= CComBSTR(L"Gator.EngMdl.Plugin.CPP.Example1");
	this->m_T0		= 0.0;
	this->m_T1		= 0.0001;
	this->m_T2		= 0.0000001;
	this->m_Ts		= 0;
	this->m_Tc		= 0;
	this->m_Isp		= 3000;

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.FinalConstruct(): <-- Exited\n");
	#endif

	return hr;
}

void CExample1::FinalRelease() 
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.FinalRelease(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.FinalRelease(): <-- Exited\n");
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

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.GetPluginConfig(): --> Entered\n");
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
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("PluginName"), CComBSTR("Human readable plugin name or alias"), CComBSTR("Name"), eAddFlagReadOnly ) );

			//================
			// Thrust Attributes
			//================
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("T0"), CComBSTR("Initial Thrust"), CComBSTR("T0"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("T1"), CComBSTR("Linear Thrust Coefficient"), CComBSTR("T1"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("T2"), CComBSTR("Quadratic Thrust Coefficient"), CComBSTR("T2"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("Ts"), CComBSTR("Sine Thrust Coefficient"), CComBSTR("Ts"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("Tc"), CComBSTR("Cosine Thrust Coefficient"), CComBSTR("Tc"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("Isp"), CComBSTR("Specific Impulse"), CComBSTR("Isp"), eAddFlagNone ) );
		}

		EXCEPTION_HR( m_pDispScope.CopyTo( ppDispScope ) );
	
	}
	catch( HRESULT r )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.EngMdl.CPP.Example1.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
	}
	catch(...)
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.EngMdl.CPP.Example1.Init() <-> Exception = \n");
		#endif

		hr = E_FAIL;
	}

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.GetPluginConfig(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::VerifyPluginConfig(IAgUtPluginConfigVerifyResult * pPluginCfgResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pPluginCfgResult )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.VerifyPluginConfig(): --> Entered\n");
	#endif

	EX_HR( pPluginCfgResult->put_Result( VARIANT_TRUE ) );
	EX_HR( pPluginCfgResult->put_Message( BSTR( "Ok" ) ) ); 

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.VerifyPluginConfig(): <-- Exited\n");
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

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.Init(): --> Entered\n");
	#endif

	HRESULT hr		= S_OK;
	bool	br		= false;

	try
	{
		this->m_pUtPluginSite = pUtPluginSite;
		
		if( this->m_pUtPluginSite )
		{
			EXCEPTION_HR( m_pUtPluginSite->QueryInterface( &m_pGatorPluginSite ) );

			EXCEPTION_HR( m_pGatorPluginSite->get_GatorProvider( &m_pGatorPluginProvider ) );

			EXCEPTION_HR( m_pGatorPluginProvider->ConfigureCalcObject( CComBSTR("Argument_of_Latitude"), &m_pArgOfLat ) );
		
			br = true;
		}
	}
	catch( HRESULT r )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.EngMdl.CPP.Example1.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
		br = false;
	}
	catch(...)
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.EngMdl.CPP.Example1.Init() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pResult = br ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.Init(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::PrePropagate(IAgGatorPluginResultState* Result, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.PrePropagate() --> Entered\n");
	#endif

	HRESULT hr  = S_OK;
	bool	br	= true;

	try
	{
		long WholeDays = 0;
		double SecIntoDay = 0.0;
		EXCEPTION_HR( Result->DayCount( eUtTimeScaleSTKEpochSec, &WholeDays, &SecIntoDay) );
		this->m_InitTime = WholeDays * 86400.0 + SecIntoDay;

		hr = S_OK;
		br = true;
	}
	catch( HRESULT r )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.EngMdl.CPP.Example1.PrePropagate() <-> Exception\n");
		#endif

		hr = r;
		br = false;
	}
	catch(...)
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.EngMdl.CPP.Example1.PrePropagate() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pResult = br ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.PrePropagate() <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::PreNextStep(IAgGatorPluginResultState* Result, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	*pResult = VARIANT_TRUE;

	return S_OK;
}

STDMETHODIMP CExample1::Evaluate(IAgGatorPluginResultEvalEngineModel* Result, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	HRESULT	hr = S_OK;
	bool	br = true;

	try
	{
		double time;
		double deltaT;
		double thrust;
		double argOfLat;

		long WholeDays = 0;
		double SecIntoDay = 0.0;
		EXCEPTION_HR( Result->DayCount( eUtTimeScaleSTKEpochSec, &WholeDays, &SecIntoDay) );
		time = WholeDays * 86400.0 + SecIntoDay;
			
		deltaT = time - this->m_InitTime;

		EXCEPTION_HR( this->m_pArgOfLat->Evaluate( Result, &argOfLat ) );
			
		thrust = this->m_T0 + ( this->m_T1 * deltaT ) + ( this->m_T2 * deltaT * deltaT ) + ( this->m_Ts * sin(argOfLat) ) + ( this->m_Tc * cos(argOfLat) );
			
		EXCEPTION_HR( Result->SetThrustAndIsp( thrust, this->m_Isp, pResult ) );
	
		hr = S_OK;
		br = true;
	}
	catch( HRESULT r )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.EndMdl.CPP.Example1.Evaluate() <-> Exception\n");
		#endif

		hr = r;
		br = false;
	}
	catch( ... )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.EngMdl.CPP.Example1.Evaluate() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pResult = br ? VARIANT_TRUE : VARIANT_FALSE;

	return hr;
}

STDMETHODIMP CExample1::Free()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.Free() --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.Free() <-- Exited\n");
	#endif
	
	return S_OK;
}

STDMETHODIMP CExample1::get_Name(BSTR* pVal)
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.get_Name() --> Entered\n");
	#endif
	
	HRESULT hr = S_OK;

	hr = m_Name.CopyTo(pVal);

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "Gator.EngMdl.CPP.Example1.get_Name(): ";
	msg += W2A(m_Name);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EngMdl.CPP.Example1.get_Name() <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::get_Isp(DOUBLE* pVal)
{
	*pVal	= this->m_Isp;
	return S_OK;
}

STDMETHODIMP CExample1::put_Isp(DOUBLE newVal)
{
	this->m_Isp	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_T0(DOUBLE* pVal)
{
	*pVal	= this->m_T0;
	return S_OK;
}

STDMETHODIMP CExample1::put_T0(DOUBLE newVal)
{
	this->m_T0	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_T1(DOUBLE* pVal)
{
	*pVal = this->m_T1;
	return S_OK;
}

STDMETHODIMP CExample1::put_T1(DOUBLE newVal)
{
	this->m_T1	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_T2(DOUBLE* pVal)
{
	*pVal	= this->m_T2;
	return S_OK;
}

STDMETHODIMP CExample1::put_T2(DOUBLE newVal)
{
	this->m_T2	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_Tc(DOUBLE* pVal)
{
	*pVal = this->m_Tc;
	return S_OK;
}

STDMETHODIMP CExample1::put_Tc(DOUBLE newVal)
{
	this->m_Tc	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_Ts(DOUBLE* pVal)
{
	*pVal = this->m_Ts;
	return S_OK;
}

STDMETHODIMP CExample1::put_Ts(DOUBLE newVal)
{
	this->m_Ts	= newVal;
	return S_OK;
}
/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
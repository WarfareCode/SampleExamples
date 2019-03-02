/**********************************************************************/
/*           Copyright 2005-2011, Analytical Graphics, Inc.           */
/**********************************************************************/
#include "stdafx.h"
#include "Example1.h"
#include "Agi.As.Gator.AttCtrl.Plugin.CPP.Examples_i.c"

#include <iostream>
using namespace std;

#include <cmath>

CExample1::CExample1()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.Constructor(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.Constructor(): <-- Exited\n");
	#endif
}

CExample1::~CExample1()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.Destructor(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.EndMdl.CPP.Example1.Destructor(): <-- Exited\n");
	#endif
}

HRESULT CExample1::FinalConstruct()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.FinalConstruct(): --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	this->m_Name	= CComBSTR(L"Gator.AttCtrl.Plugin.CPP.Example1");

	this->m_Y0		= 0.0;
	this->m_Y1		= 0.0001;
	this->m_Y2		= 0.0000001;
	this->m_Ys		= 0;
	this->m_Yc		= 0;

	this->m_P0		= 0.0;
	this->m_P1		= 0.0002;
	this->m_P2		= 0.00000001;
	this->m_Ps		= 0;
	this->m_Pc		= 0;

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.FinalConstruct(): <-- Exited\n");
	#endif

	return hr;
}

void CExample1::FinalRelease() 
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.FinalRelease(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.FinalRelease(): <-- Exited\n");
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
	OutputDebugString("Gator.AttCtrl.CPP.Example1.GetPluginConfig(): --> Entered\n");
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
			// Yaw Attributes
			//================
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("Y0"), CComBSTR("Initial Yaw"), CComBSTR("Y0"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("Y1"), CComBSTR("Linear Yaw Coefficient"), CComBSTR("Y1"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("Y2"), CComBSTR("Quadratic Yaw Coefficient"), CComBSTR("Y2"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("Ys"), CComBSTR("Sine Yaw Coefficient"), CComBSTR("Ys"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("Yc"), CComBSTR("Cosine Yaw Coefficient"), CComBSTR("Yc"), eAddFlagNone ) );

			//================
			// Pitch Attributes
			//================
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("P0"), CComBSTR("Initial Pitch"), CComBSTR("P0"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("P1"), CComBSTR("Linear Pitch Coefficient"), CComBSTR("P1"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("P2"), CComBSTR("Quadratic Pitch Coefficient"), CComBSTR("P2"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("Ps"), CComBSTR("Sine Pitch Coefficient"), CComBSTR("Ps"), eAddFlagNone ) );
			EXCEPTION_HR( pAttrBuilder->AddStringDispatchProperty ( m_pDispScope, CComBSTR("Pc"), CComBSTR("Cosine Pitch Coefficient"), CComBSTR("Pc"), eAddFlagNone ) );
		}

		EXCEPTION_HR( m_pDispScope.CopyTo( ppDispScope ) );
	
	}
	catch( HRESULT r )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.AttCtrl.CPP.Example1.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
	}
	catch(...)
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.AttCtrl.CPP.Example1.Init() <-> Exception = \n");
		#endif

		hr = E_FAIL;
	}

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.GetPluginConfig(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::VerifyPluginConfig(IAgUtPluginConfigVerifyResult * pPluginCfgResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pPluginCfgResult )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.VerifyPluginConfig(): --> Entered\n");
	#endif

	EX_HR( pPluginCfgResult->put_Result( VARIANT_TRUE ) );
	EX_HR( pPluginCfgResult->put_Message( BSTR( "Ok" ) ) ); 

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.VerifyPluginConfig(): <-- Exited\n");
	#endif

	return S_OK;
}

//=========================
// IAgAsHpopPlugin Methods
//=========================
STDMETHODIMP CExample1::Init(IAgUtPluginSite* pUtPluginSite, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pUtPluginSite )
		EX_OUT_RETVAL_PARAM( pResult )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.Init(): --> Entered\n");
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
		OutputDebugString("Gator.AttCtrl.CPP.Example1.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
		br = false;
	}
	catch(...)
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.AttCtrl.CPP.Example1.Init() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pResult = br ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.Init(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::PrePropagate(IAgGatorPluginResultAttCtrl* Result, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.PrePropagate() --> Entered\n");
	#endif

	HRESULT hr  = S_OK;
	bool	br	= true;
	VARIANT_BOOL vtbResult;

	try
	{
		EXCEPTION_HR( Result->SetRefAxes( CComBSTR("Satellite VNC(Earth)"), &vtbResult ) );

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
		OutputDebugString("Gator.AttCtrl.CPP.Example1.PrePropagate() <-> Exception\n");
		#endif

		hr = r;
		br = false;
	}
	catch(...)
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.AttCtrl.CPP.Example1.PrePropagate() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pResult = br ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.PrePropagate() <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::PreNextStep(IAgGatorPluginResultAttCtrl* Result, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	*pResult = VARIANT_TRUE;

	return S_OK;
}

STDMETHODIMP CExample1::Evaluate(IAgGatorPluginResultAttCtrl* Result, VARIANT_BOOL* pResult)
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
		double yawAngle;
		double pitchAngle;
		double argOfLat;

		long WholeDays = 0;
		double SecIntoDay = 0.0;
		EXCEPTION_HR( Result->DayCount( eUtTimeScaleSTKEpochSec, &WholeDays, &SecIntoDay) );
		time = WholeDays * 86400.0 + SecIntoDay;
			
		deltaT = time - this->m_InitTime;

		EXCEPTION_HR( this->m_pArgOfLat->Evaluate( Result, &argOfLat ) );
			
		yawAngle = this->m_Y0 + ( this->m_Y1 * deltaT ) + ( this->m_Y2 * deltaT * deltaT ) + ( this->m_Ys * sin(argOfLat) ) + ( this->m_Yc * cos(argOfLat) );

		pitchAngle = this->m_P0 + ( this->m_P1 * deltaT ) + ( this->m_P2 * deltaT * deltaT ) + ( this->m_Ps * sin(argOfLat) ) + ( this->m_Pc * cos(argOfLat) );
			
		EXCEPTION_HR( Result->EulerRotate( e321, yawAngle, pitchAngle, 0 ) );
	
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
		OutputDebugString("Gator.AttCtrl.CPP.Example1.Evaluate() <-> Exception\n");
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
	OutputDebugString("Gator.AttCtrl.CPP.Example1.Free() --> Entered\n");
	#endif

	// set the member variables back to NULL
	m_pGatorPluginSite = NULL;
	m_pGatorPluginProvider = NULL;
	m_pArgOfLat = NULL;

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.Free() <-- Exited\n");
	#endif
	
	return S_OK;
}

STDMETHODIMP CExample1::get_Name(BSTR* pVal)
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.get_Name() --> Entered\n");
	#endif
	
	HRESULT hr = S_OK;

	hr = m_Name.CopyTo(pVal);

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "Gator.AttCtrl.CPP.Example1.get_Name(): ";
	msg += W2A(m_Name);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.get_Name() <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::get_Y0(DOUBLE* pVal)
{
	*pVal	= this->m_Y0;
	return S_OK;
}

STDMETHODIMP CExample1::put_Y0(DOUBLE newVal)
{
	this->m_Y0	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_Y1(DOUBLE* pVal)
{
	*pVal = this->m_Y1;
	return S_OK;
}

STDMETHODIMP CExample1::put_Y1(DOUBLE newVal)
{
	this->m_Y1	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_Y2(DOUBLE* pVal)
{
	*pVal	= this->m_Y2;
	return S_OK;
}

STDMETHODIMP CExample1::put_Y2(DOUBLE newVal)
{
	this->m_Y2	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_Yc(DOUBLE* pVal)
{
	*pVal = this->m_Yc;
	return S_OK;
}

STDMETHODIMP CExample1::put_Yc(DOUBLE newVal)
{
	this->m_Yc	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_Ys(DOUBLE* pVal)
{
	*pVal = this->m_Ys;
	return S_OK;
}

STDMETHODIMP CExample1::put_Ys(DOUBLE newVal)
{
	this->m_Ys	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_P0(DOUBLE* pVal)
{
	*pVal	= this->m_P0;
	return S_OK;
}

STDMETHODIMP CExample1::put_P0(DOUBLE newVal)
{
	this->m_P0	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_P1(DOUBLE* pVal)
{
	*pVal = this->m_P1;
	return S_OK;
}

STDMETHODIMP CExample1::put_P1(DOUBLE newVal)
{
	this->m_P1	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_P2(DOUBLE* pVal)
{
	*pVal	= this->m_P2;
	return S_OK;
}

STDMETHODIMP CExample1::put_P2(DOUBLE newVal)
{
	this->m_P2	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_Pc(DOUBLE* pVal)
{
	*pVal = this->m_Pc;
	return S_OK;
}

STDMETHODIMP CExample1::put_Pc(DOUBLE newVal)
{
	this->m_Pc	= newVal;
	return S_OK;
}

STDMETHODIMP CExample1::get_Ps(DOUBLE* pVal)
{
	*pVal = this->m_Ps;
	return S_OK;
}

STDMETHODIMP CExample1::put_Ps(DOUBLE newVal)
{
	this->m_Ps	= newVal;
	return S_OK;
}
/**********************************************************************/
/*           Copyright 2005-2011, Analytical Graphics, Inc.           */
/**********************************************************************/
/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"

#include "Example1.h"

#include "Agi.VGT.Axes.Plugin.Examples.CPP.Example_i.c"

#include <iostream>
using namespace std;

#include <cmath>

CExample1::CExample1()
{
	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.Constructor(): --> Entered\n");
	#endif

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.Constructor(): <-- Exited\n");
	#endif
}

CExample1::~CExample1()
{
	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.Destructor(): --> Entered\n");
	#endif

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.Destructor(): <-- Exited\n");
	#endif
}

HRESULT CExample1::FinalConstruct()
{
	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.FinalConstruct(): --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	this->m_Name	= CComBSTR(L"AxesExample.CPP.Example1.Example1");

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.FinalConstruct(): <-- Exited\n");
	#endif

	return hr;
}

void CExample1::FinalRelease() 
{
	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.FinalRelease(): --> Entered\n");
	#endif

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.FinalRelease(): <-- Exited\n");
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

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.GetPluginConfig(): --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	try
	{
		if( !m_pDispScope )
		{
			EXCEPTION_HR( pAttrBuilder->NewScope( &m_pDispScope ) );
		}

		EXCEPTION_HR( m_pDispScope.CopyTo( ppDispScope ) );
	
	}
	catch( HRESULT r )
	{
		#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("AxesExample.CPP.Example1.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
	}
	catch(...)
	{
		#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("AxesExample.CPP.Example1.Init() <-> Exception = \n");
		#endif

		hr = E_FAIL;
	}

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.GetPluginConfig(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::VerifyPluginConfig(IAgUtPluginConfigVerifyResult * pPluginCfgResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pPluginCfgResult )
	EX_END_PARAMS()

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.VerifyPluginConfig(): --> Entered\n");
	#endif

	EX_HR( pPluginCfgResult->put_Result( VARIANT_TRUE ) );
	EX_HR( pPluginCfgResult->put_Message( BSTR( "Ok" ) ) ); 

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.VerifyPluginConfig(): <-- Exited\n");
	#endif

	return S_OK;
}

//=========================
// IAgCrdnAxesPlugin Methods
//=========================
STDMETHODIMP CExample1::Init(IAgUtPluginSite * Site, VARIANT_BOOL * pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Site )
		EX_OUT_RETVAL_PARAM( pResult )
	EX_END_PARAMS()

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.Init(): --> Entered\n");
	#endif

	HRESULT hr		= S_OK;
	bool	br		= false;

	try
	{
		this->m_pUtPluginSite = Site;
		
		if( this->m_pUtPluginSite )
		{
			EXCEPTION_HR( m_pUtPluginSite->QueryInterface( &m_pStkPluginSite ) );
		
			br = true;
		}
	}
	catch( HRESULT r )
	{
		#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("AxesExample.CPP.Example1.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
		br = false;
	}
	catch(...)
	{
		#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("AxesExample.CPP.Example1.Init() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pResult = br ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.Init(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::Register(IAgCrdnAxesPluginResultReg* Result)
{
	EX_BEGIN_PARAMS()
		EX_OUT_RETVAL_PARAM( Result )
	EX_END_PARAMS()

	HRESULT hr  = S_OK;

	try
	{
		Result->put_ShortDescription(L"Test short Desc");
		Result->put_LongDescription(L"Test long Desc");
	}
	catch( HRESULT r )
	{
		#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("AxesExample.CPP.Example1.Register() <-> Bad HRESULT = \n");
		#endif

		hr = r;
	}
	catch(...)
	{
		#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("AxesExample.CPP.Example1.Register() <-> Exception\n");
		#endif

		hr = E_FAIL;
	}

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("AxesExample.CPP.Example1.Register()\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::Reset(IAgCrdnAxesPluginResultReset* Result, VARIANT_BOOL * pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_PARAM( pResult )
	EX_END_PARAMS()

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.Reset(): --> Entered\n");
	#endif

	HRESULT hr  = S_OK;

	Result->get_CalcToolProvider(&m_CalcToolProvider);
	Result->get_VectorToolProvider(&m_VectorToolProvider);

	m_VectorToolProvider->ConfigureAxes(L"ICRF", L"<MyObject>", L"ICRF", L"<MyObject>", &m_ICRFAxes);

	*pResult = VARIANT_TRUE;

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.Reset(): --> Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::Evaluate(IAgCrdnAxesPluginResultEval* Result, VARIANT_BOOL * pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_PARAM( pResult )
	EX_END_PARAMS()

	HRESULT	hr = S_OK;

	if (m_ICRFAxes != NULL)
	{
		double q1 = 0;
		double q2 = 0;
		double q3 = 0;
		double q4 = 0;

		VARIANT_BOOL currentValueResult;
		m_ICRFAxes->CurrentValue(Result, &q1, &q2, &q3, &q4, &currentValueResult);
		Result->SetQuaternion(q1, q2, q3, q4);

		// Rotate by 30 degrees. The parameters are expected to be in radians.
		Result->EulerRotate(eCrdnEulerSequence121, 0.5236, 0.5236, 0.5236);
		*pResult = VARIANT_TRUE;
	}

	return hr;
}

STDMETHODIMP CExample1::Free()
{
	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.Free()\n");
	#endif

	m_CalcToolProvider = NULL;
	m_VectorToolProvider = NULL;
	m_ICRFAxes = NULL;

	return S_OK;
}

STDMETHODIMP CExample1::get_Name(BSTR* pName)
{
	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.get_Name() --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	hr = m_Name.CopyTo(pName);

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "AxesExample.CPP.Example1.get_Name(): ";
	msg += W2A(m_Name);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	#ifdef _VGT_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("AxesExample.CPP.Example1.get_Name() <-- Exited\n");
	#endif

	return hr;
}

/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/
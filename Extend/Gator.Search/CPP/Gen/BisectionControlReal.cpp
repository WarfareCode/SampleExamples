/**********************************************************************/
/*           Copyright 2006-2008, Analytical Graphics, Inc.           */
/**********************************************************************/
#include "stdafx.h"
#include "BisectionControlReal.h"

#include <iostream>
using namespace std;

#include <cmath>

CBisectionControlReal::CBisectionControlReal()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionControlReal.Constructor(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionControlReal.Constructor(): <-- Exited\n");
	#endif
}

CBisectionControlReal::~CBisectionControlReal()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionControlReal.Destructor(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.EndMdl.CPP.BisectionControlReal.Destructor(): <-- Exited\n");
	#endif
}

HRESULT CBisectionControlReal::FinalConstruct()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionControlReal.FinalConstruct(): --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	this->m_objectName = CComBSTR(L"");
	this->m_controlName = CComBSTR(L"");

	this->m_currentValue = 0.0;
	this->m_initialValue = 0.0;
	this->m_step = 100.0;
	this->m_active = false;

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionControlReal.FinalConstruct(): <-- Exited\n");
	#endif

	return hr;
}

void CBisectionControlReal::FinalRelease() 
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionControlReal.FinalRelease(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionControlReal.FinalRelease(): <-- Exited\n");
	#endif
}

//=========================
// IAgUtPluginConfig Methods
//=========================
STDMETHODIMP CBisectionControlReal::GetPluginConfig(IAgAttrBuilder * pAttrBuilder, IDispatch** ppDispScope)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pAttrBuilder )
		EX_OUT_RETVAL_INTERFACE_PARAM( ppDispScope )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionControlReal.GetPluginConfig(): --> Entered\n");
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
			EXCEPTION_HR( pAttrBuilder->AddBoolDispatchProperty( m_pDispScope, CComBSTR("Active"), CComBSTR("True if used by algorithm"), CComBSTR("IsActive"), eAddFlagNone ) );

			// note: m_dimension and m_internalUnits are set by the plugin point before
			// this method is called
			if (m_dimension == "")
			{
				// dimensionless
				EXCEPTION_HR( pAttrBuilder->AddDoubleDispatchProperty ( m_pDispScope, CComBSTR("Step"), CComBSTR("Step when searching for bounds"), CComBSTR("Step"), eAddFlagNone ) );
			}
			else if (m_dimension == "DateFormat")
			{
				// the step is in timeUnits (seconds) if the control is a date
				EXCEPTION_HR( pAttrBuilder->AddQuantityDispatchProperty ( m_pDispScope, CComBSTR("Step"), CComBSTR("Step when searching for bounds"), CComBSTR("Step"), CComBSTR("Seconds"), CComBSTR("Seconds"), eAddFlagNone ) );
			}
			else
			{
				// attr quantity, use units that were given
				EXCEPTION_HR( pAttrBuilder->AddQuantityDispatchProperty ( m_pDispScope, CComBSTR("Step"), CComBSTR("Step when searching for bounds"), CComBSTR("Step"), m_internalUnit, m_internalUnit, eAddFlagNone ) );
			}
		}

		EXCEPTION_HR( m_pDispScope.CopyTo( ppDispScope ) );
	
	}
	catch( HRESULT r )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Search.CPP.BisectionControlReal.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
	}
	catch(...)
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Search.CPP.BisectionControlReal.Init() <-> Exception = \n");
		#endif

		hr = E_FAIL;
	}

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionControlReal.GetPluginConfig(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CBisectionControlReal::VerifyPluginConfig(IAgUtPluginConfigVerifyResult * pPluginCfgResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pPluginCfgResult )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionControlReal.VerifyPluginConfig(): --> Entered\n");
	#endif

	EX_HR( pPluginCfgResult->put_Result( VARIANT_TRUE ) );
	EX_HR( pPluginCfgResult->put_Message( BSTR( "Ok" ) ) ); 

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionControlReal.VerifyPluginConfig(): <-- Exited\n");
	#endif

	return S_OK;
}

//=========================
// IAgSearchControlReal Methods
//=========================
STDMETHODIMP CBisectionControlReal::get_ObjectName(BSTR* pVal)
{
	HRESULT hr = S_OK;

	m_objectName.CopyTo(pVal);

	return hr;
}

STDMETHODIMP CBisectionControlReal::put_ObjectName(BSTR name)
{
	HRESULT hr = S_OK;

	m_objectName.AssignBSTR(name);

	return hr;
}

STDMETHODIMP CBisectionControlReal::get_ControlName(BSTR* pVal)
{
	HRESULT hr = S_OK;

	m_controlName.CopyTo(pVal);

	return hr;
}

STDMETHODIMP CBisectionControlReal::put_ControlName(BSTR name)
{
	HRESULT hr = S_OK;

	m_controlName.AssignBSTR(name);

	return hr;
}

STDMETHODIMP CBisectionControlReal::get_ControlType(AgESearchControlTypes* pVal)
{
	HRESULT hr = S_OK;

	*pVal = m_type;

	return hr;
}

STDMETHODIMP CBisectionControlReal::put_ControlType(AgESearchControlTypes val)
{
	HRESULT hr = S_OK;

	m_type = val;

	return hr;
}

STDMETHODIMP CBisectionControlReal::get_InitialValue(DOUBLE* pVal)
{
	*pVal = m_initialValue;
	return S_OK;
}

STDMETHODIMP CBisectionControlReal::put_InitialValue(DOUBLE newVal)
{
	m_initialValue = newVal;
	return S_OK;
}

STDMETHODIMP CBisectionControlReal::get_CurrentValue(DOUBLE* pVal)
{
	*pVal = m_currentValue;
	return S_OK;
}

STDMETHODIMP CBisectionControlReal::put_CurrentValue(DOUBLE newVal)
{
	m_currentValue = newVal;
	return S_OK;
}

STDMETHODIMP CBisectionControlReal::get_Dimension(BSTR* pVal)
{
	HRESULT hr = S_OK;

	m_dimension.CopyTo(pVal);

	return hr;
}

STDMETHODIMP CBisectionControlReal::put_Dimension(BSTR newVal)
{
	HRESULT hr = S_OK;

	if (m_dimension != newVal)
	{
		// null out the attr scope, so a new one will be created with the new
		// dimension the next time the scope is needed
		m_pDispScope = NULL;
	}

	m_dimension.AssignBSTR(newVal);

	return hr;
}

STDMETHODIMP CBisectionControlReal::get_InternalUnit(BSTR* pVal)
{
	HRESULT hr = S_OK;

	m_internalUnit.CopyTo(pVal);

	return hr;
}

STDMETHODIMP CBisectionControlReal::put_InternalUnit(BSTR newVal)
{
	HRESULT hr = S_OK;

	m_internalUnit.AssignBSTR(newVal);

	return hr;
}

STDMETHODIMP CBisectionControlReal::get_Step(DOUBLE* pVal)
{
	*pVal = this->m_step;
	return S_OK;
}

STDMETHODIMP CBisectionControlReal::put_Step(DOUBLE newVal)
{
	this->m_step = newVal;
	return S_OK;
}

STDMETHODIMP CBisectionControlReal::get_IsActive(VARIANT_BOOL* pVal)
{
	*pVal = this->m_active ? VARIANT_TRUE : VARIANT_FALSE;
	return S_OK;
}

STDMETHODIMP CBisectionControlReal::put_IsActive(VARIANT_BOOL newVal)
{
	this->m_active = newVal == VARIANT_TRUE;
	return S_OK;
}

/**********************************************************************/
/*           Copyright 2006-2008, Analytical Graphics, Inc.           */
/**********************************************************************/
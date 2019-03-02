/**********************************************************************/
/*           Copyright 2006-2008, Analytical Graphics, Inc.           */
/**********************************************************************/
#include "stdafx.h"
#include "BisectionResult.h"

#include <iostream>
using namespace std;

#include <cmath>

CBisectionResult::CBisectionResult()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionResult.Constructor(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionResult.Constructor(): <-- Exited\n");
	#endif
}

CBisectionResult::~CBisectionResult()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionResult.Destructor(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.EndMdl.CPP.BisectionResult.Destructor(): <-- Exited\n");
	#endif
}

HRESULT CBisectionResult::FinalConstruct()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionResult.FinalConstruct(): --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	this->m_objectName = CComBSTR(L"");
	this->m_resultName = CComBSTR(L"");

	this->m_currentValue = 0.0;
	this->m_desiredValue = 0.0;
	this->m_tolerance = 0.01;
	this->m_active = false;

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionResult.FinalConstruct(): <-- Exited\n");
	#endif

	return hr;
}

void CBisectionResult::FinalRelease() 
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionResult.FinalRelease(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionResult.FinalRelease(): <-- Exited\n");
	#endif
}

//=========================
// IAgUtPluginConfig Methods
//=========================
STDMETHODIMP CBisectionResult::GetPluginConfig(IAgAttrBuilder * pAttrBuilder, IDispatch** ppDispScope)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pAttrBuilder )
		EX_OUT_RETVAL_INTERFACE_PARAM( ppDispScope )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionResult.GetPluginConfig(): --> Entered\n");
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
				// dimensionless - desired value and tolerance are just attr doubles
				EXCEPTION_HR( pAttrBuilder->AddDoubleDispatchProperty ( m_pDispScope, CComBSTR("DesiredValue"), CComBSTR("Desired Value"), CComBSTR("DesiredValue"), eAddFlagNone ) );
				EXCEPTION_HR( pAttrBuilder->AddDoubleDispatchProperty ( m_pDispScope, CComBSTR("Tolerance"), CComBSTR("Tolerance"), CComBSTR("Tolerance"), eAddFlagNone ) );
			}
			else if (m_dimension == "DateFormat")
			{
				// date, set desired value as an attrDate and tolerance as an attrQuantity in seconds
				EXCEPTION_HR( pAttrBuilder->AddDateDispatchProperty ( m_pDispScope, CComBSTR("DesiredValue"), CComBSTR("Desired Value"), CComBSTR("DesiredValue"), eAddFlagNone ) );
				EXCEPTION_HR( pAttrBuilder->AddQuantityDispatchProperty ( m_pDispScope, CComBSTR("Tolerance"), CComBSTR("Tolerance"), CComBSTR("Tolerance"), CComBSTR("Seconds"), CComBSTR("Seconds"), eAddFlagNone ) );
			}
			else
			{
				// attr quantity, use internal units that were given
				EXCEPTION_HR( pAttrBuilder->AddQuantityDispatchProperty ( m_pDispScope, CComBSTR("DesiredValue"), CComBSTR("Desired Value"), CComBSTR("DesiredValue"), m_internalUnit, m_internalUnit, eAddFlagNone ) );
				EXCEPTION_HR( pAttrBuilder->AddQuantityDispatchProperty ( m_pDispScope, CComBSTR("Tolerance"), CComBSTR("Tolerance"), CComBSTR("Tolerance"), m_internalUnit, m_internalUnit, eAddFlagNone ) );
			}
		}

		EXCEPTION_HR( m_pDispScope.CopyTo( ppDispScope ) );
	
	}
	catch( HRESULT r )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Search.CPP.BisectionResult.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
	}
	catch(...)
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Search.CPP.BisectionResult.Init() <-> Exception = \n");
		#endif

		hr = E_FAIL;
	}

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionResult.GetPluginConfig(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CBisectionResult::VerifyPluginConfig(IAgUtPluginConfigVerifyResult * pPluginCfgResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pPluginCfgResult )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionResult.VerifyPluginConfig(): --> Entered\n");
	#endif

	EX_HR( pPluginCfgResult->put_Result( VARIANT_TRUE ) );
	EX_HR( pPluginCfgResult->put_Message( BSTR( "Ok" ) ) ); 

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionResult.VerifyPluginConfig(): <-- Exited\n");
	#endif

	return S_OK;
}

//=========================
// IAgSearchResult Methods
//=========================
STDMETHODIMP CBisectionResult::get_ObjectName(BSTR* pVal)
{
	HRESULT hr = S_OK;

	m_objectName.CopyTo(pVal);

	return hr;
}

STDMETHODIMP CBisectionResult::put_ObjectName(BSTR name)
{
	HRESULT hr = S_OK;

	m_objectName.AssignBSTR(name);

	return hr;
}

STDMETHODIMP CBisectionResult::get_ResultName(BSTR* pVal)
{
	HRESULT hr = S_OK;

	m_resultName.CopyTo(pVal);

	return hr;
}

STDMETHODIMP CBisectionResult::put_ResultName(BSTR name)
{
	HRESULT hr = S_OK;

	m_resultName.AssignBSTR(name);

	return hr;
}

STDMETHODIMP CBisectionResult::get_CurrentValue(DOUBLE* pVal)
{
	*pVal = m_currentValue;
	return S_OK;
}

STDMETHODIMP CBisectionResult::put_CurrentValue(DOUBLE newVal)
{
	m_currentValue = newVal;
	return S_OK;
}

STDMETHODIMP CBisectionResult::get_IsValid(VARIANT_BOOL* pVal)
{
	*pVal = m_valid ? VARIANT_TRUE : VARIANT_FALSE;
	return S_OK;
}

STDMETHODIMP CBisectionResult::put_IsValid(VARIANT_BOOL newVal)
{
	m_valid = newVal == VARIANT_TRUE;
	return S_OK;
}

STDMETHODIMP CBisectionResult::get_Dimension(BSTR* pVal)
{
	HRESULT hr = S_OK;

	m_dimension.CopyTo(pVal);

	return hr;
}

STDMETHODIMP CBisectionResult::put_Dimension(BSTR newVal)
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

STDMETHODIMP CBisectionResult::get_InternalUnit(BSTR* pVal)
{
	HRESULT hr = S_OK;

	m_internalUnit.CopyTo(pVal);

	return hr;
}

STDMETHODIMP CBisectionResult::put_InternalUnit(BSTR newVal)
{
	HRESULT hr = S_OK;

	m_internalUnit.AssignBSTR(newVal);

	return hr;
}

STDMETHODIMP CBisectionResult::get_DesiredValue(DOUBLE* pVal)
{
	*pVal = this->m_desiredValue;
	return S_OK;
}

STDMETHODIMP CBisectionResult::put_DesiredValue(DOUBLE newVal)
{
	this->m_desiredValue = newVal;
	return S_OK;
}

STDMETHODIMP CBisectionResult::get_Tolerance(DOUBLE* pVal)
{
	*pVal = this->m_tolerance;
	return S_OK;
}

STDMETHODIMP CBisectionResult::put_Tolerance(DOUBLE newVal)
{
	this->m_tolerance = newVal;
	return S_OK;
}

STDMETHODIMP CBisectionResult::get_IsActive(VARIANT_BOOL* pVal)
{
	*pVal = this->m_active ? VARIANT_TRUE : VARIANT_FALSE;
	return S_OK;
}

STDMETHODIMP CBisectionResult::put_IsActive(VARIANT_BOOL newVal)
{
	this->m_active = newVal == VARIANT_TRUE;
	return S_OK;
}

/**********************************************************************/
/*           Copyright 2006-2008, Analytical Graphics, Inc.           */
/**********************************************************************/
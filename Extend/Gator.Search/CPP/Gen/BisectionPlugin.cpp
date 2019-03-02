/**********************************************************************/
/*           Copyright 2006-2010, Analytical Graphics, Inc.           */
/**********************************************************************/
#include "stdafx.h"
#include "BisectionPlugin.h"
#include "Agi.Search.Plugin.CPP.Examples_i.c"

#include "BisectionControlReal.h"
#include "BisectionResult.h"

#include <iostream>
using namespace std;

#include <cmath>

CBisectionPlugin::CBisectionPlugin()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.Constructor(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.Constructor(): <-- Exited\n");
	#endif
}

CBisectionPlugin::~CBisectionPlugin()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.Destructor(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.EndMdl.CPP.BisectionPlugin.Destructor(): <-- Exited\n");
	#endif
}

HRESULT CBisectionPlugin::FinalConstruct()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.FinalConstruct(): --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	this->m_Name	= CComBSTR(L"Search.Plugin.CPP.BisectionPlugin");

	this->m_maxIters = 100;

	this->m_controlsRealProgID = CComBSTR(L"AGI.SearchControlReal.Plugin.Examples.CPP.BisectionControlReal.1");
	this->m_resultsProgID = CComBSTR(L"AGI.SearchResult.Plugin.Examples.CPP.BisectionResult.1");

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.FinalConstruct(): <-- Exited\n");
	#endif

	return hr;
}

void CBisectionPlugin::FinalRelease() 
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.FinalRelease(): --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.FinalRelease(): <-- Exited\n");
	#endif
}

//=========================
// IAgUtPluginConfig Methods
//=========================
STDMETHODIMP CBisectionPlugin::GetPluginConfig(IAgAttrBuilder * pAttrBuilder, IDispatch** ppDispScope)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pAttrBuilder )
		EX_OUT_RETVAL_INTERFACE_PARAM( ppDispScope )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.GetPluginConfig(): --> Entered\n");
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
			EXCEPTION_HR( pAttrBuilder->AddIntDispatchProperty ( m_pDispScope, CComBSTR("MaxIterations"), CComBSTR("Maximum Iterations"), CComBSTR("MaxIterations"), eAddFlagNone ) );
		}

		EXCEPTION_HR( m_pDispScope.CopyTo( ppDispScope ) );
	
	}
	catch( HRESULT r )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Search.CPP.BisectionPlugin.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
	}
	catch(...)
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Search.CPP.BisectionPlugin.Init() <-> Exception = \n");
		#endif

		hr = E_FAIL;
	}

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.GetPluginConfig(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CBisectionPlugin::VerifyPluginConfig(IAgUtPluginConfigVerifyResult * pPluginCfgResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pPluginCfgResult )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.VerifyPluginConfig(): --> Entered\n");
	#endif

	EX_HR( pPluginCfgResult->put_Result( VARIANT_TRUE ) );
	EX_HR( pPluginCfgResult->put_Message( BSTR( "Ok" ) ) ); 

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.VerifyPluginConfig(): <-- Exited\n");
	#endif

	return S_OK;
}

//=========================
// IAgPluginSearch methods
//=========================
STDMETHODIMP CBisectionPlugin::Init(IAgUtPluginSite* pUtPluginSite, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pUtPluginSite )
		EX_OUT_RETVAL_PARAM( pResult )
	EX_END_PARAMS()

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.Init(): --> Entered\n");
	#endif

	HRESULT hr		= S_OK;
	bool	br		= false;

	try
	{
		this->m_pUtPluginSite = pUtPluginSite;

		if( this->m_pUtPluginSite )
		{
			br = true;
		}
	}
	catch( HRESULT r )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Search.CPP.BisectionPlugin.Init() <-> Bad HRESULT = \n");
		#endif

		hr = r;
		br = false;
	}
	catch(...)
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Search.CPP.BisectionPlugin.Init() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pResult = br ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.Init(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CBisectionPlugin::Run(IAgSearchPluginOperand* pOperand, VARIANT_BOOL* pTesting, VARIANT_BOOL* pRet)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pOperand )
		EX_OUT_RETVAL_INTERFACE_PARAM( pRet )
	EX_END_PARAMS()

	HRESULT	hr = S_OK;
	bool	br = true;
	*pRet = VARIANT_TRUE;

	try
	{
		// get the control and result with the bisection interface
		CComPtr<IAgSearchControlCollection> pControls = NULL;
		pOperand->get_Controls(&pControls);
		long numControls;
		long controlIndex;
		pControls->get_Count(&numControls);

		CComPtr<IAgSearchControlReal> pAgControl = NULL;
		CComPtr<IBisectionControlReal> pControl = NULL;

		for (long i = 0; i < numControls; ++i)
		{
			CComPtr<IAgSearchControl> pTempControl = NULL;
            pControls->get_Item(i, &pTempControl);
			pTempControl->QueryInterface(&pAgControl);
			pTempControl->QueryInterface(&pControl);
			if (pControl != 0)
			{
				VARIANT_BOOL active;
				pControl->get_IsActive(&active);
				if (active == VARIANT_TRUE)
				{
					controlIndex = i;
					break;
				}
				pControl.Release();
				pAgControl.Release();
			}
		}

		CComPtr<IAgSearchResultCollection> pResults = NULL;
		pOperand->get_Results(&pResults);
		long numResults;
		long resultIndex;
		pResults->get_Count(&numResults);

        CComPtr<IAgSearchResult> pAgResult = NULL;
		CComPtr<IBisectionResult> pResult = NULL;

		for  (long i = 0; i < numResults; ++i)
		{
            pResults->get_Item(i, &pAgResult);
            pAgResult->QueryInterface(&pResult);
			if (pResult != 0)
			{
				VARIANT_BOOL active;
				pResult->get_IsActive(&active);
				if (active == VARIANT_TRUE)
				{
					resultIndex = i;
					break;
				}
				pResult.Release();
				pAgResult.Release();
			}
		}

		if ( (pControl == 0) || (pAgControl == 0) || (pResult == 0))
		{
			// controls and results are not of the right type
			m_pUtPluginSite->Message(eUtLogMsgAlarm, CComBSTR("There must be one active control and one active result."));
			*pRet = VARIANT_TRUE;
			return E_FAIL;
		}

		int count = 0;

		pOperand->Evaluate2(VARIANT_TRUE, pRet);   // the true flag lets the run appear on graphs
		
		double a;
		pAgControl->get_CurrentValue(&a);
		double b = a;
		double fa;
		pAgResult->get_CurrentValue(&fa);
		double fb = fa;
		
		double step;
		pControl->get_Step(&step);
		
		double desired;
		pResult->get_DesiredValue(&desired);
		double tolerance;
		pResult->get_Tolerance(&tolerance);
		
		// are we already within tolerance?
		if (fabs(fa - desired) < tolerance)
		{
			return S_OK;
		}
		
		if (*pTesting)
		{
			// if we're testing we won't do any searching - ok, but return false
			*pRet = VARIANT_FALSE;
			return S_OK;
		}

		// make the status grid
		CComPtr<IAgPluginSearchStatusGrid> pGrid = NULL;
		pOperand->get_StatusGrid(&pGrid);
		
		pGrid->CreateGrid(2, 6);
		pGrid->SetColumnToTruncateLeft(0);
		pGrid->SetColumnToTruncateLeft(2);
		pGrid->SetHeaderCellString(0, 0, CComBSTR("Control Name"));
		pGrid->SetHeaderCellString(0, 1, CComBSTR("Control Value"));
		pGrid->SetHeaderCellString(0, 2, CComBSTR("Result Name"));
		pGrid->SetHeaderCellString(0, 3, CComBSTR("Result Value"));
		pGrid->SetHeaderCellString(0, 4, CComBSTR("Desired Value"));
		pGrid->SetHeaderCellString(0, 5, CComBSTR("Tolerance"));

		USES_CONVERSION;

		BSTR BcontrolObjectName;
		BSTR BcontrolName;
		pAgControl->get_ObjectName(&BcontrolObjectName);
		pAgControl->get_ControlName(&BcontrolName);

		std::string controlGridCell = W2A(BcontrolObjectName);
		controlGridCell += " : ";
		controlGridCell += W2A(BcontrolName);

		pGrid->SetCellString(1, 0, A2W(controlGridCell.c_str()));

		BSTR BresultObjectName;
		BSTR BresultName;
		pAgResult->get_ObjectName(&BresultObjectName);
		pAgResult->get_ResultName(&BresultName);

		std::string resultGridCell = W2A(BresultObjectName);
		resultGridCell += " : ";
		resultGridCell += W2A(BresultName);

		pGrid->SetCellString(1, 2, A2W(resultGridCell.c_str()));

		pGrid->SetCellResultValue(1, 4, resultIndex, desired, 8);
		// tolerance is in delta units
		pGrid->SetCellResultDeltaValue(1, 5, resultIndex, tolerance, 8);

		pGrid->SetStatus(CComBSTR("Initial run"));

		
		bool changedSign = false;
		
		bool bounded = false;
		
		// find the initial set that bounds zero
		while(!bounded && count < m_maxIters)
		{
			++count;

			a = b;
			fa = fb;
			b = a + step;
			pAgControl->put_CurrentValue(b);
		
			pOperand->Evaluate2(VARIANT_TRUE, pRet);
		
			pAgResult->get_CurrentValue(&fb);

			pGrid->SetCellControlValue(1, 1, controlIndex, b, 8);
			pGrid->SetCellResultValue(1, 3, resultIndex, fb, 8);

			char iterStr[38];
			sprintf_s(iterStr, "Iteration %d: Searching for bounds", count);

			pGrid->SetStatus(A2W(iterStr));
			pGrid->Refresh();

			// see if b hit the desired value
			if (fabs(fb - desired) < tolerance)
			{
				pGrid->SetStatus(CComBSTR("Desired value reached while searching for bounds."));
				pGrid->Refresh();
				return S_OK;
			}
			
			bounded = (fa > desired && fb < desired) || (fa < desired && fb > desired);
			
			// make sure we are getting closer to the desired value
			if ( !bounded && fabs(fb - desired) >= fabs(fa - desired) )
			{
				// search in the other direction, unless we've already changed the step once
				if (!changedSign)
				{
					changedSign = true;
					step = -step;
					b = a;
					fb = fa;
				}
				else
				{
					// error out
					pGrid->SetStatus(CComBSTR("Unable to bound desired value"));
					pGrid->Refresh();
					OutputDebugString("Unable to bound desired value with given initial guess and step");
					*pRet = VARIANT_FALSE;                    
					return S_OK;
				}
			}
		}
		
		double c = b;
		double fc = fb;
		while (fabs(fc - desired) > tolerance && count < m_maxIters) 
		{
			++count;

			c = (a + b) / 2.0;
			
			pAgControl->put_CurrentValue(c);
			
			pOperand->Evaluate2(VARIANT_TRUE, pRet);
			
			pAgResult->get_CurrentValue(&fc);
			
			if ( (fc > desired && fa > desired) || (fc < desired && fa < desired))
			{
				a = c;
				fa = fc;
			}
			else
			{
				b = c;
				fb = fc;
			}

			char iterStr[36];
			sprintf_s(iterStr, "Iteration %d: Searching for root", count);

			pGrid->SetStatus(A2W(iterStr));
			pGrid->SetCellControlValue(1, 1, controlIndex, c, 8);
			pGrid->SetCellResultValue(1, 3, resultIndex, fc, 8);
			pGrid->Refresh();
		}
		
		if (fabs(fc - desired) > tolerance)
		{
			char statusStr[50];
			sprintf_s(statusStr, "Unable to converge within %d iterations.", m_maxIters);
			pGrid->SetStatus(A2W(statusStr));
			pGrid->Refresh();
			br = false;
		}
		else
		{
			char statusStr[50];
			sprintf_s(statusStr, "Converged after %d iterations.", count);
			pGrid->SetStatus(A2W(statusStr));
			pGrid->Refresh();
		}
	}
	catch( HRESULT r )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Gator.EndMdl.CPP.BisectionPlugin.Run() <-> Exception\n");
		#endif

		hr = r;
		br = false;
	}
	catch( ... )
	{
		#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("Search.CPP.BisectionPlugin.Run() <-> Exception\n");
		#endif

		hr = E_FAIL;
		br = false;
	}

	*pRet = br ? VARIANT_TRUE : VARIANT_FALSE;

	return hr;
}

STDMETHODIMP CBisectionPlugin::Free()
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.Free() --> Entered\n");
	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.Free() <-- Exited\n");
	#endif
	
	return S_OK;
}

STDMETHODIMP CBisectionPlugin::GetControlsProgID(AgESearchControlTypes controlType, BSTR* pVal)
{
	HRESULT hr = S_OK;

	if (controlType == eSearchControlTypesReal)
	{
        m_controlsRealProgID.CopyTo(pVal);
	}
	else
	{
		CComBSTR temp("");
		temp.CopyTo(pVal);
	}

	return hr;
}

STDMETHODIMP CBisectionPlugin::GetResultsProgID(BSTR* pVal)
{
	HRESULT hr = S_OK;

	m_resultsProgID.CopyTo(pVal);

	return hr;
}

STDMETHODIMP CBisectionPlugin::get_Name(BSTR* pVal)
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.get_Name() --> Entered\n");
	#endif
	
	HRESULT hr = S_OK;

	hr = m_Name.CopyTo(pVal);

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "Search.CPP.BisectionPlugin.get_Name(): ";
	msg += W2A(m_Name);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Search.CPP.BisectionPlugin.get_Name() <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CBisectionPlugin::get_MaxIterations(INT* pVal)
{
	*pVal = this->m_maxIters;
	return S_OK;
}

STDMETHODIMP CBisectionPlugin::put_MaxIterations(INT newVal)
{
	this->m_maxIters = newVal;
	return S_OK;
}
/**********************************************************************/
/*           Copyright 2006-2010, Analytical Graphics, Inc.           */
/**********************************************************************/
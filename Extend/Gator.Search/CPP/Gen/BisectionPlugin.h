/**********************************************************************/
/*           Copyright 2006, Analytical Graphics, Inc.                */
/**********************************************************************/
#ifndef BisectionPlugin_h
#define BisectionPlugin_h

#pragma once
#include "resource.h"       // main symbols
#include "Agi.Search.Plugin.Cpp.Examples.h"

class ATL_NO_VTABLE CBisectionPlugin : 
public CComObjectRootEx<CComSingleThreadModel>,
public CComCoClass<CBisectionPlugin, &CLSID_BisectionPlugin>,
public IDispatchImpl<IBisectionPlugin, &IID_IBisectionPlugin, &LIBID_AgiSearchPluginCppExamplesLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
public IAgUtPluginConfig,
public IAgPluginSearch
{
	public:

		DECLARE_REGISTRY_RESOURCEID(IDR_BISECTIONPLUGIN)
		DECLARE_PROTECT_FINAL_CONSTRUCT()

		BEGIN_COM_MAP(CBisectionPlugin)
			COM_INTERFACE_ENTRY(IBisectionPlugin)
			COM_INTERFACE_ENTRY(IAgUtPluginConfig)
			COM_INTERFACE_ENTRY(IAgPluginSearch)
			COM_INTERFACE_ENTRY(IDispatch)
		END_COM_MAP()

		//====================
		// C++ and ATL method
		//====================
		CBisectionPlugin();
		~CBisectionPlugin();
		HRESULT FinalConstruct();
		void FinalRelease();

		//===========================
		// IAgUtPluginConfig Methods
		//===========================
		STDMETHOD(GetPluginConfig)(IAgAttrBuilder* pAttrBuilder, IDispatch** ppDispScope);
		STDMETHOD(VerifyPluginConfig)(IAgUtPluginConfigVerifyResult* pPluginCfgResult);

		//===================================
		// IAgPluginSearch Methods
		//===================================
		STDMETHOD(Init)	(IAgUtPluginSite * Site, VARIANT_BOOL * pResult);
		STDMETHOD(Run)	(IAgSearchPluginOperand* pSearchOperand, VARIANT_BOOL* pTesting, VARIANT_BOOL * pResult);
		STDMETHOD(Free)	();
		STDMETHOD(GetControlsProgID) (AgESearchControlTypes controlType, BSTR* pProgID);
		STDMETHOD(GetResultsProgID) (BSTR* pProgID);

		//==================================
		// Defined in both 
		// IAgPluginSearch and
		// IBisectionPlugin
		//==================================
		STDMETHOD(get_Name)(BSTR * pName);

		//=======================
		// IBisectionPlugin Methods
		//=======================

		STDMETHOD(get_MaxIterations) (INT* pVal);
		STDMETHOD(put_MaxIterations) (INT pVal);

	private:
	
		CComPtr<IAgUtPluginSite>				m_pUtPluginSite;
		CComPtr<IAgGatorPluginSite>				m_pGatorPluginSite;
		CComPtr<IDispatch>						m_pDispScope;

		CComBSTR m_Name;
		CComBSTR m_controlsRealProgID;
		CComBSTR m_resultsProgID;

		int m_maxIters;

};

#define EX_HR(exp)                                                       \
{                                                                        \
    HRESULT _hresult = exp;                                              \
    if( FAILED(_hresult) )                                               \
	{																	 \
        return _hresult;                                                 \
    }                                                                    \
}                                                                        \

#define EXCEPTION_HR(exp)                                                \
{                                                                        \
    HRESULT _hresult = exp;                                              \
    if( FAILED(_hresult) )                                               \
	{																	 \
        throw _hresult;                                                  \
    }                                                                    \
}                                                                        \

#define EX_BEGIN_PARAMS()                                                \
{                                                                        \
    HRESULT hr = S_OK;                                                   \

#define EX_OUT_RETVAL_PARAM(p)                                           \
    if (p == 0)                                                          \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_OUT_RETVAL_INTERFACE_PARAM(p)                                 \
    if (p)                                                               \
    {                                                                    \
        *p = 0;                                                          \
    }                                                                    \
    else                                                                 \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_OUT_PARAM(p)                                                  \
    if (p == 0)                                                          \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_OUT_INTERFACE_PARAM(p)                                        \
    if (p)                                                               \
    {                                                                    \
        *p = 0;                                                          \
    }                                                                    \
    else                                                                 \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_IN_INTERFACE_PARAM(p)                                         \
    if (p == 0)                                                          \
    {                                                                    \
        hr = E_INVALIDARG;                                               \
    }                                                                    \

#define EX_INOUT_INTERFACE_PARAM(p)                                      \
    if (p == 0)                                                          \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_IN_ARRAY_PARAM(p)                                             \
    if (p.vt != (VT_ARRAY | VT_VARIANT) &&                               \
        p.vt != (VT_ARRAY | VT_VARIANT | VT_BYREF))                      \
    {                                                                    \
        hr = E_INVALIDARG;                                               \
    }                                                                    \

#define EX_IN_BSTR_PARAM(p)                                              \
    if (p == 0)                                                          \
    {                                                                    \
        hr = E_POINTER;                                                  \
    }                                                                    \

#define EX_END_PARAMS()                                                  \
    if (FAILED(hr))                                                      \
    {                                                                    \
        return hr;                                                       \
    }                                                                    \
}                                                                        \

OBJECT_ENTRY_AUTO(__uuidof(BisectionPlugin), CBisectionPlugin)

#endif
/**********************************************************************/
/*           Copyright 2006, Analytical Graphics, Inc.                */
/**********************************************************************/
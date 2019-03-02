/**********************************************************************/
/*           Copyright 2009, Analytical Graphics, Inc.                */
/**********************************************************************/
#pragma once
#include "resource.h"       // main symbols

#include "Agi.As.EOMFunc.Plugin.CPP.Examples.h"

class ATL_NO_VTABLE CExample1 : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CExample1, &CLSID_Example1>,
	public IDispatchImpl<IExample1, &IID_IExample1, &LIBID_AgiAsEOMFuncPluginCPPExamplesLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IAgUtPluginConfig,
	public IAgAsEOMFuncPlugin
{
	public:

		DECLARE_REGISTRY_RESOURCEID(IDR_EXAMPLE1)
		DECLARE_PROTECT_FINAL_CONSTRUCT()

		BEGIN_COM_MAP(CExample1)
			COM_INTERFACE_ENTRY(IExample1)
			COM_INTERFACE_ENTRY(IAgUtPluginConfig)
			COM_INTERFACE_ENTRY(IAgAsEOMFuncPlugin)
			COM_INTERFACE_ENTRY(IDispatch)
		END_COM_MAP()

		//====================
		// C++ and ATL method
		//====================
		CExample1();
		~CExample1();
		HRESULT FinalConstruct();
		void FinalRelease();
		
		//===========================
		// IAgUtPluginConfig Methods
		//===========================
		STDMETHOD(GetPluginConfig)(IAgAttrBuilder* pAttrBuilder, IDispatch** ppDispScope);
		STDMETHOD(VerifyPluginConfig)(IAgUtPluginConfigVerifyResult* pPluginCfgResult);

		//===================================
		// IAgAsEOMFuncPlugin Methods
		//===================================
		STDMETHOD(Init)			(IAgUtPluginSite * Site, VARIANT_BOOL * pResult);
		STDMETHOD(Register)		(IAgAsEOMFuncPluginRegisterHandler* pRegisterHandler, VARIANT_BOOL * pResult);
		STDMETHOD(SetIndices)	(IAgAsEOMFuncPluginSetIndicesHandler* pSetIndicesHandler, VARIANT_BOOL * pResult);
		STDMETHOD(Calc)			(AgEAsEOMFuncPluginEventTypes EventType, IAgAsEOMFuncPluginStateVector* pStateVector, VARIANT_BOOL * pResult);
		STDMETHOD(Free)			();

		//==================================
		// Defined in both 
		// IAgAsEOMFuncPlugin and
		// IExample1
		//==================================
		STDMETHOD(get_Name)(BSTR * pName);

		//=======================
		// IExample1 Methods
		//=======================
		STDMETHOD(get_DeltaVAxes)	(BSTR* pVal);
		STDMETHOD(put_DeltaVAxes)	(BSTR newVal);
	
	private:

		CComPtr<IAgUtPluginSite>				m_pUtPluginSite;
		CComPtr<IAgStkPluginSite>				m_pStkPluginSite;
		CComPtr<IAgCrdnPluginProvider>			m_pCrdnPluginProvider;
		CComPtr<IAgCrdnConfiguredAxes>			m_pCrdnConfiguredAxes;
		CComPtr<IDispatch>						m_pDispScope;
		
		CComBSTR								m_Name;
		CComBSTR								m_deltaVAxes;

		int m_thrustXIndex;
		int m_thrustYIndex;
		int m_thrustZIndex;
		int m_massIndex;

		int m_effectiveImpulseIndex;
		int m_integratedDeltaVxIndex;
		int m_integratedDeltaVyIndex;
		int m_integratedDeltaVzIndex;
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

OBJECT_ENTRY_AUTO(__uuidof(Example1), CExample1)

/**********************************************************************/
/*           Copyright 2009, Analytical Graphics, Inc.                */
/**********************************************************************/

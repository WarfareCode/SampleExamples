/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
#pragma once
#include "resource.h"       // main symbols

#include "Agi.As.Gator.EngMdl.Plugin.CPP.Examples.h"

class ATL_NO_VTABLE CExample1 : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CExample1, &CLSID_Example1>,
	public IDispatchImpl<IExample1, &IID_IExample1, &LIBID_AgiAsGatorEngMdlPluginCPPExamplesLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IAgUtPluginConfig,
	public IAgGatorPluginEngineModel
{
	public:

		DECLARE_REGISTRY_RESOURCEID(IDR_EXAMPLE1)
		DECLARE_PROTECT_FINAL_CONSTRUCT()

		BEGIN_COM_MAP(CExample1)
			COM_INTERFACE_ENTRY(IExample1)
			COM_INTERFACE_ENTRY(IAgUtPluginConfig)
			COM_INTERFACE_ENTRY(IAgGatorPluginEngineModel)
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
		// IAgGatorPluginEngineModel Methods
		//===================================
		STDMETHOD(Init)			(IAgUtPluginSite * Site, VARIANT_BOOL * pResult);
		STDMETHOD(PrePropagate)	(IAgGatorPluginResultState * ResultState, VARIANT_BOOL * pResult);
		STDMETHOD(PreNextStep)	(IAgGatorPluginResultState * ResultState, VARIANT_BOOL * pResult);
		STDMETHOD(Evaluate)		(IAgGatorPluginResultEvalEngineModel * ResultEvalEngineModel, VARIANT_BOOL * pResult);
		STDMETHOD(Free)			();

		//==================================
		// Defined in both 
		// IAgGatorPluginEngineModel and
		// IExample1
		//==================================
		STDMETHOD(get_Name)(BSTR * pName);

		//=======================
		// IExample1 Methods
		//=======================
		STDMETHOD(get_Isp)	(DOUBLE* pVal);
		STDMETHOD(put_Isp)	(DOUBLE newVal);
		STDMETHOD(get_T0)	(DOUBLE* pVal);
		STDMETHOD(put_T0)	(DOUBLE newVal);
		STDMETHOD(get_T1)	(DOUBLE* pVal);
		STDMETHOD(put_T1)	(DOUBLE newVal);
		STDMETHOD(get_T2)	(DOUBLE* pVal);
		STDMETHOD(put_T2)	(DOUBLE newVal);
		STDMETHOD(get_Tc)	(DOUBLE* pVal);
		STDMETHOD(put_Tc)	(DOUBLE newVal);
		STDMETHOD(get_Ts)	(DOUBLE* pVal);
		STDMETHOD(put_Ts)	(DOUBLE newVal);
	
	private:

		CComPtr<IAgUtPluginSite>				m_pUtPluginSite;
		CComPtr<IAgGatorPluginSite>				m_pGatorPluginSite;
		CComPtr<IAgGatorPluginProvider>			m_pGatorPluginProvider;
		CComPtr<IAgGatorConfiguredCalcObject>	m_pArgOfLat;
		CComPtr<IDispatch>						m_pDispScope;
		
		double									m_InitTime;

		CComBSTR								m_Name;
		double									m_Isp;
		double									m_T0;
		double									m_T1;
		double									m_T2;
		double									m_Tc;
		double									m_Ts;
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

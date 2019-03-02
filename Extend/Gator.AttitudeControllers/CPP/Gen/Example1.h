/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
#ifndef Example1_h
#define Example1_h

#pragma once
#include "resource.h"       // main symbols
#include "Agi.As.Gator.AttCtrl.Plugin.Cpp.Examples.h"

class ATL_NO_VTABLE CExample1 : 
public CComObjectRootEx<CComSingleThreadModel>,
public CComCoClass<CExample1, &CLSID_Example1>,
public IDispatchImpl<IExample1, &IID_IExample1, &LIBID_AgiAsGatorAttCtrlPluginCppExamplesLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
public IAgUtPluginConfig,
public IAgGatorPluginAttCtrl
{
	public:

		DECLARE_REGISTRY_RESOURCEID(IDR_EXAMPLE1)
		DECLARE_PROTECT_FINAL_CONSTRUCT()

		BEGIN_COM_MAP(CExample1)
			COM_INTERFACE_ENTRY(IExample1)
			COM_INTERFACE_ENTRY(IAgUtPluginConfig)
			COM_INTERFACE_ENTRY(IAgGatorPluginAttCtrl)
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
		// IAgGatorPluginAttCtrl Methods
		//===================================
		STDMETHOD(Init)			(IAgUtPluginSite * Site, VARIANT_BOOL * pResult);
		STDMETHOD(PrePropagate)	(IAgGatorPluginResultAttCtrl * Result, VARIANT_BOOL * pResult);
		STDMETHOD(PreNextStep)	(IAgGatorPluginResultAttCtrl * Result, VARIANT_BOOL * pResult);
		STDMETHOD(Evaluate)		(IAgGatorPluginResultAttCtrl * Result, VARIANT_BOOL * pResult);
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

		// Yaw
		STDMETHOD(get_Y0)	(DOUBLE* pVal);
		STDMETHOD(put_Y0)	(DOUBLE newVal);
		STDMETHOD(get_Y1)	(DOUBLE* pVal);
		STDMETHOD(put_Y1)	(DOUBLE newVal);
		STDMETHOD(get_Y2)	(DOUBLE* pVal);
		STDMETHOD(put_Y2)	(DOUBLE newVal);
		STDMETHOD(get_Ys)	(DOUBLE* pVal);
		STDMETHOD(put_Ys)	(DOUBLE newVal);
		STDMETHOD(get_Yc)	(DOUBLE* pVal);
		STDMETHOD(put_Yc)	(DOUBLE newVal);

		// Pitch
		STDMETHOD(get_P0)	(DOUBLE* pVal);
		STDMETHOD(put_P0)	(DOUBLE newVal);
		STDMETHOD(get_P1)	(DOUBLE* pVal);
		STDMETHOD(put_P1)	(DOUBLE newVal);
		STDMETHOD(get_P2)	(DOUBLE* pVal);
		STDMETHOD(put_P2)	(DOUBLE newVal);
		STDMETHOD(get_Ps)	(DOUBLE* pVal);
		STDMETHOD(put_Ps)	(DOUBLE newVal);
		STDMETHOD(get_Pc)	(DOUBLE* pVal);
		STDMETHOD(put_Pc)	(DOUBLE newVal);

	private:
	
		CComPtr<IAgUtPluginSite>				m_pUtPluginSite;
		CComPtr<IAgGatorPluginSite>				m_pGatorPluginSite;
		CComPtr<IAgGatorPluginProvider>			m_pGatorPluginProvider;
		CComPtr<IAgGatorConfiguredCalcObject>	m_pArgOfLat;
		CComPtr<IDispatch>						m_pDispScope;

		CComBSTR			m_Name;

		double				m_InitTime;
		// Yaw
		double				m_Y0;
		double				m_Y1;
		double				m_Y2;
		double				m_Ys;
		double				m_Yc;

		// Pitch
		double				m_P0;
		double				m_P1;
		double				m_P2;
		double				m_Ps;
		double				m_Pc;

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

#endif
/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
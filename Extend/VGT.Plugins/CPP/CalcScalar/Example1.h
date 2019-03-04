/**********************************************************************/
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/
#pragma once
#include "resource.h"       // main symbols

#include "Agi.VGT.CalcScalar.Plugin.Examples.CPP.Example.h"

class ATL_NO_VTABLE CExample1 : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CExample1, &CLSID_Example1>,
	public IDispatchImpl<IExample1, &IID_IExample1, &LIBID_AgiVGTCalcScalarPluginCPPExampleLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
	public IAgUtPluginConfig,
	public IAgCrdnCalcScalarPlugin
{
	public:

		DECLARE_REGISTRY_RESOURCEID(IDR_EXAMPLE1)
		DECLARE_PROTECT_FINAL_CONSTRUCT()

		BEGIN_COM_MAP(CExample1)
			COM_INTERFACE_ENTRY(IExample1)
			COM_INTERFACE_ENTRY(IAgUtPluginConfig)
			COM_INTERFACE_ENTRY(IAgCrdnCalcScalarPlugin)
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
		// IAgCrdnCalcScalarPlugin Methods
		//===================================
		STDMETHOD(Init)			(IAgUtPluginSite * Site, VARIANT_BOOL * pResult);
		STDMETHOD(Register)		(IAgCrdnCalcScalarPluginResultReg* Result);
		STDMETHOD(Reset)		(IAgCrdnCalcScalarPluginResultReset* Result, VARIANT_BOOL * pResult);
		STDMETHOD(Evaluate)		(IAgCrdnCalcScalarPluginResultEval* Result, VARIANT_BOOL * pResult);
		STDMETHOD(Free)			();

		//==================================
		// Defined in both 
		// IAgCrdnCalcScalarPlugin and
		// IExample1
		//==================================
		STDMETHOD(get_Name)(BSTR * pName);
	
	private:

		CComPtr<IAgUtPluginSite>				m_pUtPluginSite;
		CComPtr<IAgStkPluginSite>				m_pStkPluginSite;
		CComPtr<IAgCrdnPluginProvider>			m_pCrdnPluginProvider;
		CComPtr<IAgCrdnConfiguredAxes>			m_pCrdnConfiguredAxes;
		CComPtr<IDispatch>						m_pDispScope;

		CComPtr<IAgCrdnPluginCalcProvider>		m_CalcToolProvider;
		CComPtr<IAgCrdnPluginProvider>			m_VectorToolProvider;

		CComPtr<IAgCrdnConfiguredCalcScalar>	m_ObjectTrajectoryCatesianZ;
		CComPtr<IAgCrdnConfiguredCalcScalar>	m_ObjectTrajectoryCatesianX;

		CComBSTR								m_Name;
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
/*           Copyright 2012, Analytical Graphics, Inc.                */
/**********************************************************************/
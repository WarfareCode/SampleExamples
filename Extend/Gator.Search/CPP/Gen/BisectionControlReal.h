/**********************************************************************/
/*           Copyright 2006-2008, Analytical Graphics, Inc.           */
/**********************************************************************/
#ifndef BisectionControlReal_h
#define BisectionControlReal_h

#pragma once
#include "resource.h"       // main symbols
#include "Agi.Search.Plugin.Cpp.Examples.h"

class ATL_NO_VTABLE CBisectionControlReal : 
public CComObjectRootEx<CComSingleThreadModel>,
public CComCoClass<CBisectionControlReal, &CLSID_BisectionControlReal>,
public IDispatchImpl<IBisectionControlReal, &IID_IBisectionControlReal, &LIBID_AgiSearchPluginCppExamplesLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
public IAgUtPluginConfig,
public IAgSearchControlReal
{
	public:

		DECLARE_REGISTRY_RESOURCEID(IDR_BISECTIONCONTROLREAL)
		DECLARE_PROTECT_FINAL_CONSTRUCT()

		BEGIN_COM_MAP(CBisectionControlReal)
			COM_INTERFACE_ENTRY(IBisectionControlReal)
			COM_INTERFACE_ENTRY(IAgUtPluginConfig)
			COM_INTERFACE_ENTRY(IAgSearchControl)
			COM_INTERFACE_ENTRY(IAgSearchControlReal)
			COM_INTERFACE_ENTRY(IDispatch)
		END_COM_MAP()

		//====================
		// C++ and ATL method
		//====================
		CBisectionControlReal();
		~CBisectionControlReal();
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


		//==================================
		// Defined in both 
		// IAgPluginSearch and
		// IBisectionControlReal
		//==================================
		STDMETHOD(get_ObjectName)(/*out, retval]*/ BSTR *pVal);
		STDMETHOD(put_ObjectName)(/*[in]*/ BSTR Name);
		STDMETHOD(get_ControlName)(/*[out, retval]*/ BSTR* pVal);
		STDMETHOD(put_ControlName)(/*[in]*/ BSTR Name);
		STDMETHOD(get_ControlType)(/*[out, retval]*/ AgESearchControlTypes* pVal);
		STDMETHOD(put_ControlType)(/*[in]*/ AgESearchControlTypes type);	
		STDMETHOD(get_CurrentValue)(/*[out, retval]*/ double* pValue);
		STDMETHOD(put_CurrentValue)(/*[in]*/ double value);
		STDMETHOD(get_InitialValue)(/*[out, retval]*/ double* pValue);
		STDMETHOD(put_InitialValue)(/*[in]*/ double value);
		STDMETHOD(get_Dimension)(/*out, retval]*/ BSTR *pVal);
		STDMETHOD(put_Dimension)(/*[in]*/ BSTR Name);
		STDMETHOD(get_InternalUnit)(/*out, retval]*/ BSTR *pVal);
		STDMETHOD(put_InternalUnit)(/*[in]*/ BSTR Name);

		//=======================
		// IBisectionControlReal Methods
		//=======================
		STDMETHOD(get_Step) (DOUBLE* pVal);
		STDMETHOD(put_Step) (DOUBLE val);
		STDMETHOD(get_IsActive) (VARIANT_BOOL* pVal);
		STDMETHOD(put_IsActive) (VARIANT_BOOL val);

	private:
	
		CComPtr<IAgUtPluginSite>				m_pUtPluginSite;
		CComPtr<IAgGatorPluginSite>				m_pGatorPluginSite;
		CComPtr<IDispatch>						m_pDispScope;

		CComBSTR m_objectName;
		CComBSTR m_controlName;
		AgESearchControlTypes m_type;

		double m_currentValue;
		double m_initialValue;
		CComBSTR m_dimension;
		CComBSTR m_internalUnit;

		double m_step;
		bool m_active;
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

OBJECT_ENTRY_AUTO(__uuidof(BisectionControlReal), CBisectionControlReal)

#endif
/**********************************************************************/
/*           Copyright 2006-2008, Analytical Graphics, Inc.           */
/**********************************************************************/
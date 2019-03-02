/**********************************************************************/
/*           Copyright 2006-2008, Analytical Graphics, Inc.           */
/**********************************************************************/
#ifndef BisectionResult_h
#define BisectionResult_h

#pragma once
#include "resource.h"       // main symbols
#include "Agi.Search.Plugin.Cpp.Examples.h"

class ATL_NO_VTABLE CBisectionResult : 
public CComObjectRootEx<CComSingleThreadModel>,
public CComCoClass<CBisectionResult, &CLSID_BisectionResult>,
public IDispatchImpl<IBisectionResult, &IID_IBisectionResult, &LIBID_AgiSearchPluginCppExamplesLib, /*wMajor =*/ 1, /*wMinor =*/ 0>,
public IAgUtPluginConfig,
public IAgSearchResult
{
	public:

		DECLARE_REGISTRY_RESOURCEID(IDR_BISECTIONRESULT)
		DECLARE_PROTECT_FINAL_CONSTRUCT()

		BEGIN_COM_MAP(CBisectionResult)
			COM_INTERFACE_ENTRY(IBisectionResult)
			COM_INTERFACE_ENTRY(IAgUtPluginConfig)
			COM_INTERFACE_ENTRY(IAgSearchResult)
			COM_INTERFACE_ENTRY(IDispatch)
		END_COM_MAP()

		//====================
		// C++ and ATL method
		//====================
		CBisectionResult();
		~CBisectionResult();
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
		// IAgSearchResult and
		// IBisectionResult
		//==================================
		STDMETHOD(get_ObjectName)(/*out, retval]*/ BSTR *pVal);
		STDMETHOD(put_ObjectName)(/*[in]*/ BSTR Name);
		STDMETHOD(get_ResultName)(/*[out, retval]*/ BSTR* pVal);
		STDMETHOD(put_ResultName)(/*[in]*/ BSTR Name);
		STDMETHOD(get_CurrentValue)(/*[out, retval]*/ double* pValue);
		STDMETHOD(put_CurrentValue)(/*[in]*/ double value);
		STDMETHOD(get_IsValid)(/*[out, retval]*/ VARIANT_BOOL* pValue);
		STDMETHOD(put_IsValid)(/*[in]*/ VARIANT_BOOL Value);
		STDMETHOD(get_Dimension)(/*out, retval]*/ BSTR *pVal);
		STDMETHOD(put_Dimension)(/*[in]*/ BSTR Name);
		STDMETHOD(get_InternalUnit)(/*out, retval]*/ BSTR *pVal);
		STDMETHOD(put_InternalUnit)(/*[in]*/ BSTR Name);

		//=======================
		// IBisectionResult Methods
		//=======================
		STDMETHOD(get_DesiredValue) (DOUBLE* pVal);
		STDMETHOD(put_DesiredValue) (DOUBLE val);
		STDMETHOD(get_Tolerance) (DOUBLE* pVal);
		STDMETHOD(put_Tolerance) (DOUBLE val);
		STDMETHOD(get_IsActive) (VARIANT_BOOL* pVal);
		STDMETHOD(put_IsActive) (VARIANT_BOOL val);

	private:
	
		CComPtr<IAgUtPluginSite>				m_pUtPluginSite;
		CComPtr<IAgGatorPluginSite>				m_pGatorPluginSite;
		CComPtr<IDispatch>						m_pDispScope;

		CComBSTR m_objectName;
		CComBSTR m_resultName;
		double m_currentValue;
		bool m_valid;
		CComBSTR m_dimension;
		CComBSTR m_internalUnit;

		double m_desiredValue;
		double m_tolerance;
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

OBJECT_ENTRY_AUTO(__uuidof(BisectionResult), CBisectionResult)

#endif
/**********************************************************************/
/*           Copyright 2006-2008, Analytical Graphics, Inc.           */
/**********************************************************************/
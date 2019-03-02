/**********************************************************************/
/*           Copyright 2007, Analytical Graphics, Inc.                */
/**********************************************************************/
#pragma once
#include "Resource.h"       // main symbols

#include "AGI.Access.Constraint.Plugin.Examples.CPP.h"
#include <string>

class ATL_NO_VTABLE CRangeExample : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CRangeExample, &CLSID_RangeExample>,
	public AccessConstraint::IAgAccessConstraintPlugin
{
	public:

		DECLARE_REGISTRY_RESOURCEID(IDR_RANGEEXAMPLE)
		DECLARE_PROTECT_FINAL_CONSTRUCT()

		BEGIN_COM_MAP(CRangeExample)
			COM_INTERFACE_ENTRY(AccessConstraint::IAgAccessConstraintPlugin)
		END_COM_MAP()
	
		//====================
		// C++ and ATL method
		//====================
		CRangeExample();
		~CRangeExample();
		HRESULT FinalConstruct();
		void FinalRelease();

		//===========================
		// IAgAccessConstraintPlugin
		//===========================
		STDMETHOD(get_DisplayName)(/*[out, retval]*/ BSTR* pDisplayName );
		STDMETHOD(Register)( /*[in]*/ AccessConstraint::IAgAccessConstraintPluginResultRegister *Result);
		STDMETHOD(Init)(/*[in]*/ IAgUtPluginSite* Site, /*[out, retval]*/ VARIANT_BOOL* pResult );
		STDMETHOD(PreCompute)(/*[in]*/ AccessConstraint::IAgAccessConstraintPluginResultPreCompute *Result, 
								/*[out, retval]*/ VARIANT_BOOL* pResult );
		STDMETHOD(Evaluate)( 
			/*[in]*/ AccessConstraint::IAgAccessConstraintPluginResultEval *Result, 
			 /* [in] */ AccessConstraint::IAgAccessConstraintPluginObjectData *BaseData,
			/* [in] */ AccessConstraint::IAgAccessConstraintPluginObjectData *TargetData,
			/*[out, retval]*/ VARIANT_BOOL* pResult );
			STDMETHOD(PostCompute)( /*[in]*/ AccessConstraint::IAgAccessConstraintPluginResultPostCompute *Result,
									/*[out, retval]*/ VARIANT_BOOL* pResult );
		STDMETHOD(Free)();
	private:

		void Message(AgEUtLogMsgType severity, std::string& msg);

		CComPtr<IAgStkPluginSite>				m_pStkPluginSite;
		CComPtr<STKObjects::IAgStkObjectRoot>	m_pStkRootObject;
		std::string								m_DisplayName;
};

#define EX_HR(exp)                                                       \
{                                                                        \
    HRESULT _hresult = exp;                                              \
    if (FAILED(_hresult))                                                \
	{																	 \
        return _hresult;                                                 \
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

#define RAD2DEG 57.2957795130823208767

OBJECT_ENTRY_AUTO(__uuidof(RangeExample), CRangeExample)

/**********************************************************************/
/*           Copyright 2007, Analytical Graphics, Inc.                */
/**********************************************************************/
#pragma once
#include "Resource.h"       // main symbols
#include <string>
#include "MYPLUGINDll.h"

class ATL_NO_VTABLE CMYPLUGIN : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CMYPLUGIN, &CLSID_MYPLUGIN>,
	public IAgUtPluginConfig,
	public IAgAsHpopPlugin,
	public IDispatchImpl<IMYPLUGIN, &IID_IMYPLUGIN, &LIBID_MYPLUGINLib>
{
	public:

		DECLARE_REGISTRY_RESOURCEID(IDR_MYPLUGIN)
		DECLARE_PROTECT_FINAL_CONSTRUCT()

		BEGIN_COM_MAP(CMYPLUGIN)
			COM_INTERFACE_ENTRY(IMYPLUGIN)
			COM_INTERFACE_ENTRY(IAgUtPluginConfig)
			COM_INTERFACE_ENTRY(IAgAsHpopPlugin)
			COM_INTERFACE_ENTRY(IDispatch)
		END_COM_MAP()
	
		//====================
		// C++ and ATL method
		//====================
		CMYPLUGIN();
		~CMYPLUGIN();
		HRESULT FinalConstruct();
		void FinalRelease();

		//===========================
		// IAgUtPluginConfig Methods
		//===========================
		STDMETHOD(GetPluginConfig)(IAgAttrBuilder* pAttrBuilder, IDispatch** ppDispScope);
		STDMETHOD(VerifyPluginConfig)(IAgUtPluginConfigVerifyResult* pPluginCfgResult);

		//=========================
		// IAgAsHpopPlugin Methods
		//=========================
		STDMETHOD(Init)(IAgUtPluginSite * Site, VARIANT_BOOL * pResult);
		STDMETHOD(Free)();
		STDMETHOD(PrePropagate)(IAgAsHpopPluginResult * Result, VARIANT_BOOL * pResult);
		STDMETHOD(PostPropagate)(IAgAsHpopPluginResult * Result, VARIANT_BOOL * pResult);
		STDMETHOD(PreNextStep)(IAgAsHpopPluginResult * Result, VARIANT_BOOL * pResult);
		STDMETHOD(Evaluate)(IAgAsHpopPluginResultEval * ResultEval, VARIANT_BOOL * pResult);
		STDMETHOD(PostEvaluate)(IAgAsHpopPluginResultPostEval * ResultEval, VARIANT_BOOL * pResult);

		//======================
		// Defined in both 
		// IAgAsHpopPlugin and
		// IMYPLUGIN
		//======================
		STDMETHOD(get_Name)(BSTR * pName);

		//=======================
		// IMYPLUGIN Methods
		//=======================
		STDMETHOD(get_Enabled)				(VARIANT_BOOL *);
		STDMETHOD(put_Enabled)				(VARIANT_BOOL);

		STDMETHOD(get_DebugMode)			(VARIANT_BOOL *);
		STDMETHOD(put_DebugMode)			(VARIANT_BOOL);

		STDMETHOD(get_EvalMsgInterval)		(long *);
		STDMETHOD(put_EvalMsgInterval)		(long);

		STDMETHOD(get_PostEvalMsgInterval)		(long *);
		STDMETHOD(put_PostEvalMsgInterval)		(long);

		STDMETHOD(get_PreNextMsgInterval)	(long *);
		STDMETHOD(put_PreNextMsgInterval)	(long);

	private:

		void Message(AgEUtLogMsgType severity, std::string& msgStr);

		CComPtr<IAgUtPluginSite>			m_pUPS;

		CComPtr<IDispatch>					m_pDispScope;

		std::string		m_Name;					
		bool			m_Enabled;				

		// messaging parameter data
		bool			m_DebugMode;	
		int				m_PreNextCntr;
		int				m_EvalCntr;
		int				m_PostEvalCntr;
		int				m_EvalMsgInterval;		
		int				m_PostEvalMsgInterval;	
		int				m_PreNextMsgInterval;	

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

OBJECT_ENTRY_AUTO(__uuidof(MYPLUGIN), CMYPLUGIN)

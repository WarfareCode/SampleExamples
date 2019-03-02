/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
#pragma once
#include "resource.h"       // main symbols

#include "Agi.As.Hpop.Plugins.CPP.Examples.h"

using namespace STKObjects;

class ATL_NO_VTABLE CExample1 : 
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CExample1, &CLSID_Example1>,
	public IAgUtPluginConfig,
	public IAgAsHpopPlugin,
	public IDispatchImpl<IExample1, &IID_IExample1, &LIBID_AgiAsHpopPluginsCPPExamplesLib>
{
	public:

		DECLARE_REGISTRY_RESOURCEID(IDR_EXAMPLE1)
		DECLARE_PROTECT_FINAL_CONSTRUCT()

		BEGIN_COM_MAP(CExample1)
			COM_INTERFACE_ENTRY(IExample1)
			COM_INTERFACE_ENTRY(IAgUtPluginConfig)
			COM_INTERFACE_ENTRY(IAgAsHpopPlugin)
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
		// IExample1
		//======================
		STDMETHOD(get_Name)(BSTR * pName);

		//=======================
		// IExample1 Methods
		//=======================
		STDMETHOD(get_Enabled)				(VARIANT_BOOL *);
		STDMETHOD(put_Enabled)				(VARIANT_BOOL);
		STDMETHOD(get_VectorName)			(BSTR *);
		STDMETHOD(put_VectorName)			(BSTR);
		STDMETHOD(get_AccelX)				(double *);
		STDMETHOD(put_AccelX)				(double);
		STDMETHOD(get_AccelY)				(double *);
		STDMETHOD(put_AccelY)				(double);
		STDMETHOD(get_AccelZ)				(double *);
		STDMETHOD(put_AccelZ)				(double);

		STDMETHOD(get_AccelRefFrame)		(BSTR *);
		STDMETHOD(put_AccelRefFrame)		(BSTR);

		STDMETHOD(get_AccelRefFrameChoices)	(SAFEARRAY **);

		STDMETHOD(get_MsgStatus)			(VARIANT_BOOL *);
		STDMETHOD(put_MsgStatus)			(VARIANT_BOOL);

		STDMETHOD(get_EvalMsgInterval)		(long *);
		STDMETHOD(put_EvalMsgInterval)		(long);

		STDMETHOD(get_PostEvalMsgInterval)		(long *);
		STDMETHOD(put_PostEvalMsgInterval)		(long);

		STDMETHOD(get_PreNextMsgInterval)	(long *);
		STDMETHOD(put_PreNextMsgInterval)	(long);

	private:

		CComPtr<IAgUtPluginSite>			m_pUtPluginSite;
		CComPtr<IAgCrdnPluginProvider>		m_pCrdnPluginProvider;
		CComPtr<IAgCrdnConfiguredVector>	m_pCrdnConfiguredVector;
		CComPtr<IDispatch>					m_pDispScope;
		CComPtr<IAgStkObjectRoot>		    m_pStkRootObject;
		int									m_PreNextCntr;
		int									m_EvalCntr;
		int									m_PostEvalCntr;
		double								m_SRPArea;
		bool								m_SrpIsOn;

		//==================================
		// Attribute Displayed data members
		//==================================
		CComBSTR		m_Name;					// Plugin Significant
		bool			m_Enabled;				// Plugin Significant
		CComBSTR		m_VectorName;			// Init/Evaluate significant

		double			m_AccelX;				// Propagation Significant
		double			m_AccelY;				// Propagation Significant
		double			m_AccelZ;				// Propagation Significant
		int				m_AccelRefFrame;		// Propagation Significant
		SAFEARRAY*		m_pAccelRefFrameChoices;	// Propagation Significant

		bool			m_MsgStatus;			// Uncessesary Added Feature
		int				m_EvalMsgInterval;		// Uncessesary Added Feature
		int				m_PostEvalMsgInterval;	// Uncessesary Added Feature
		int				m_PreNextMsgInterval;	// Uncessesary Added Feature

		HRESULT InitAccelRefFrameChoices();
		HRESULT	EvaluateSRPArea( IAgAsHpopPluginResultEval* ResultEval );
		void PreConfigureObjectModel();
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

OBJECT_ENTRY_AUTO(__uuidof(Example1), CExample1)
/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
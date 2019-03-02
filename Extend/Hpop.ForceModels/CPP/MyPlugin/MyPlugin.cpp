Z/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"

#include "MYPLUGIN.h"

#include "MYPLUGIN_i.c"

#include <iostream>
using namespace std;

#include <cmath>

CMYPLUGIN::CMYPLUGIN():
m_Name(""),
m_Enabled(true),
m_DebugMode(false),
m_EvalMsgInterval(5000),
m_PostEvalMsgInterval(5000),
m_PreNextMsgInterval(1000),
m_PreNextCntr(0),
m_EvalCntr(0),
m_PostEvalCntr(0)
{
}

CMYPLUGIN::~CMYPLUGIN()
{
}

HRESULT CMYPLUGIN::FinalConstruct()
{
	HRESULT hr = S_OK;

	m_pUPS = NULL;

	m_Name	= std::string("MYPLUGIN");

	return hr;
}

void CMYPLUGIN::FinalRelease()
{
}

//=========================
// Messaging code
//=========================

void CMYPLUGIN::Message (AgEUtLogMsgType severity, std::string& msgStr)
{
	if(  m_pUPS != NULL && !msgStr.empty())
	{
		m_pUPS->Message( severity, CComBSTR(msgStr.c_str()));
	}
}

//=========================
// IAgUtPluginConfig Methods
//=========================
STDMETHODIMP CMYPLUGIN::GetPluginConfig(IAgAttrBuilder * pAttrBuilder, IDispatch** ppDispScope)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pAttrBuilder )
		EX_OUT_RETVAL_INTERFACE_PARAM( ppDispScope )
	EX_END_PARAMS()

	if( !m_pDispScope )
	{
		EX_HR( pAttrBuilder->NewScope( &m_pDispScope ) );

		EX_HR( pAttrBuilder->AddStringDispatchProperty( m_pDispScope, 
			CComBSTR("PluginName"), 
			CComBSTR("Human readable plugin name or alias"), 
			CComBSTR("Name"), 
			eAddFlagNone ) );

		EX_HR( pAttrBuilder->AddBoolDispatchProperty  ( m_pDispScope, 
			CComBSTR("PluginEnabled"), 
			CComBSTR("If the plugin is enabled or has experience an error"),	
			CComBSTR("Enabled"), 
			eAddFlagNone ) );

		//=============================
		// Messaging related attributes
		//=============================
		EX_HR( pAttrBuilder->AddBoolDispatchProperty( m_pDispScope, 
			CComBSTR("UsePropagationMessages"), 
			CComBSTR("Send messages to the message window during propagation"), 
			CComBSTR("DebugMode"), 
			eAddFlagNone ) );

		EX_HR( pAttrBuilder->AddIntDispatchProperty ( m_pDispScope, 
			CComBSTR("EvaluateMessageInterval"), 
			CComBSTR("The interval at which to send messages from the Evaluate method during propagation"), 
			CComBSTR("EvalMsgInterval"), 
			eAddFlagNone ) );

		EX_HR( pAttrBuilder->AddIntDispatchProperty ( m_pDispScope, 
			CComBSTR("PostEvaluateMessageInterval"), 
			CComBSTR("The interval at which to send messages from the PostEvaluate method during propagation"), 
			CComBSTR("PostEvalMsgInterval"), 
			eAddFlagNone ) );

		EX_HR( pAttrBuilder->AddIntDispatchProperty ( m_pDispScope, 
			CComBSTR("PreNextStepMessageInterval"), 
			CComBSTR("The interval at which to send messages from the PreNextStep method during propagation"), 
			CComBSTR("PreNextMsgInterval"), 
			eAddFlagNone ) );
	}

	m_pDispScope.CopyTo(ppDispScope);

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::VerifyPluginConfig(IAgUtPluginConfigVerifyResult * pPluginCfgResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pPluginCfgResult )
	EX_END_PARAMS()

	EX_HR( pPluginCfgResult->put_Result( VARIANT_TRUE ) );
	EX_HR( pPluginCfgResult->put_Message( BSTR( "Ok" ) ) );

	return S_OK;
}

//=========================
// IAgAsHpopPlugin Methods
//=========================
STDMETHODIMP CMYPLUGIN::Init(IAgUtPluginSite* pUtPluginSite, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pUtPluginSite )
		EX_OUT_RETVAL_PARAM( pResult )
	EX_END_PARAMS()

	HRESULT hr		= S_OK;

	try
	{
		m_pUPS = pUtPluginSite;
	}
	catch(...)
	{
		m_Enabled = false;

		std::string msg = m_Name;
		msg += "->Init(): Exception";

		Message( eUtLogMsgAlarm, msg );
	}

	*pResult = m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	return hr;
}

STDMETHODIMP CMYPLUGIN::PrePropagate(IAgAsHpopPluginResult* Result, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	try
	{
		if( m_Enabled )
		{
			std::string msg = m_Name;
			msg += "->PrePropagate() called";
			Message(eUtLogMsgDebug, msg);
		}
	}
	catch(...)
	{
		m_Enabled = false;

		std::string msg = m_Name;
		msg += "->PrePropagate(): Exception";

		Message( eUtLogMsgAlarm, msg );
	}

	*pResult = m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::PreNextStep(IAgAsHpopPluginResult* Result, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	try
	{
		if( m_Enabled )
		{
			m_PreNextCntr++;

			if(m_PreNextCntr % m_PreNextMsgInterval == 0)
			{
				char buffer[40];
				std::string msg = m_Name;
				msg += "->PreNextStep() called the ";
				itoa(m_PreNextCntr, buffer, 10);
				msg += std::string(buffer);
				msg += "th time";
				Message(eUtLogMsgDebug, msg);
			}
		}
	}
	catch(...)
	{
		m_Enabled = false;

		std::string msg = m_Name;
		msg += "->PreNextStep(): Exception";

		Message( eUtLogMsgAlarm, msg );
	}

	*pResult = m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::Evaluate(IAgAsHpopPluginResultEval* ResultEval, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( ResultEval )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	try
	{
		if( m_Enabled )
		{
			m_EvalCntr++;

			if(m_EvalCntr % m_EvalMsgInterval == 0)
			{
				char buffer[40];
				std::string msg = m_Name;
				msg += "->Evaluate() called the ";
				itoa(m_EvalCntr, buffer, 10);
				msg += std::string(buffer);
				msg += "th time";
				Message(eUtLogMsgDebug, msg);
			}
		}
	}
	catch(...)
	{
		m_Enabled = false;

		std::string msg = m_Name;
		msg += "->Evaluate(): Exception";

		Message( eUtLogMsgAlarm, msg );
	}

	*pResult = m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::PostEvaluate(IAgAsHpopPluginResultPostEval* ResultEval, VARIANT_BOOL* pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( ResultEval )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	try
	{
		if( m_Enabled )
		{
			m_PostEvalCntr++;

			if(m_PostEvalCntr % m_PostEvalMsgInterval == 0)
			{
				char buffer[40];
				std::string msg = m_Name;
				msg += "->PostEvaluate() called the ";
				itoa(m_PostEvalCntr, buffer, 10);
				msg += std::string(buffer);
				msg += "th time";
				Message(eUtLogMsgDebug, msg);
			}
		}
	}
	catch(...)
	{
		m_Enabled = false;

		std::string msg = m_Name;
		msg += "->PostEvaluate(): Exception";

		Message( eUtLogMsgAlarm, msg );
	}

	*pResult = m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::PostPropagate(IAgAsHpopPluginResult * Result, VARIANT_BOOL * pResult)
{
	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	try
	{
		if( m_Enabled )
		{
			std::string msg = m_Name;
			msg += "->PostPropagate() called";
			Message(eUtLogMsgDebug, msg);
		}
	}
	catch(...)
	{
		m_Enabled = false;

		std::string msg = m_Name;
		msg += "->PostPropagate(): Exception";

		Message( eUtLogMsgAlarm, msg );
	}

	*pResult = m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::Free()
{
	try
	{
		std::string msg = m_Name;
		msg += "->Free() called";
		Message(eUtLogMsgDebug, msg);
	}
	catch(...)
	{
		std::string msg = m_Name;
		msg += "->Free(): Exception";

		Message( eUtLogMsgAlarm, msg );
	}

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::get_Name(BSTR* pVal)
{
	HRESULT hr = S_OK;

	CComBSTR nameBstr = "unknown";
	
	if(!m_Name.empty())
	{	
		USES_CONVERSION;

		nameBstr = A2W(m_Name.c_str());
	}

	hr = nameBstr.CopyTo(pVal);

	return hr;
}

//=====================
// IMYPLUGIN Methods
//=====================
STDMETHODIMP CMYPLUGIN::get_Enabled( VARIANT_BOOL* pVal )
{
	*pVal = m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	return S_OK;
}
STDMETHODIMP CMYPLUGIN::put_Enabled( VARIANT_BOOL Val )
{
	m_Enabled = ( Val == VARIANT_TRUE );

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::get_DebugMode( VARIANT_BOOL* pVal )
{
	*pVal = m_DebugMode ? VARIANT_TRUE : VARIANT_FALSE;

	return S_OK;
}
STDMETHODIMP CMYPLUGIN::put_DebugMode( VARIANT_BOOL Val )
{
	m_DebugMode = ( Val == VARIANT_TRUE );

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::get_EvalMsgInterval( long* pVal )
{
	*pVal = m_EvalMsgInterval;

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::put_EvalMsgInterval( long Val )
{
	m_EvalMsgInterval = Val;

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::get_PostEvalMsgInterval( long* pVal )
{
	*pVal = m_PostEvalMsgInterval;

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::put_PostEvalMsgInterval( long Val )
{
	m_PostEvalMsgInterval = Val;

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::get_PreNextMsgInterval( long* pVal )
{
	*pVal = m_PreNextMsgInterval;

	return S_OK;
}

STDMETHODIMP CMYPLUGIN::put_PreNextMsgInterval( long Val )
{
	m_PreNextMsgInterval = Val;

	return S_OK;
}

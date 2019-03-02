/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"

#include "Example1.h"

#include "Agi.As.Hpop.Plugins.CPP.Examples_i.c"

#include <iostream>
using namespace std;

#include <cmath>

CExample1::CExample1():
m_Name(""),
m_Enabled(true),
m_VectorName(""),
m_AccelX(0.0),
m_AccelY(0.07),
m_AccelZ(0.0),
m_AccelRefFrame(3),
m_pAccelRefFrameChoices(NULL),
m_MsgStatus(false),
m_EvalMsgInterval(5000),
m_PostEvalMsgInterval(5000),
m_PreNextMsgInterval(1000),
m_PreNextCntr(0),
m_EvalCntr(0),
m_PostEvalCntr(0),
m_SRPArea(0.0),
m_SrpIsOn(false)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.Constructor(): --> Entered\n");
	#endif

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.Constructor(): <-- Exited\n");
	#endif
}

CExample1::~CExample1()
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.Destructor(): --> Entered\n");
	#endif

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.Destructor(): <-- Exited\n");
	#endif
}

HRESULT CExample1::FinalConstruct()
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.FinalConstruct(): --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	m_pUtPluginSite = NULL;
	m_pCrdnPluginProvider = NULL;
	m_pCrdnConfiguredVector = NULL;

	m_Name			= CComBSTR(L"CPP.Example1");
	m_VectorName	= CComBSTR(L"Periapsis");

	hr = InitAccelRefFrameChoices();

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.FinalConstruct(): <-- Exited\n");
	#endif

	return hr;
}

void CExample1::FinalRelease()
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.FinalRelease(): --> Entered\n");
	#endif

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.FinalRelease(): <-- Exited\n");
	#endif
}

HRESULT CExample1::InitAccelRefFrameChoices()
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.InitAccelRefFrameChoices(): --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	VARIANT HUGEP	*psaData;

	if( m_pAccelRefFrameChoices )
	{
		::SafeArrayDestroy( m_pAccelRefFrameChoices);
		m_pAccelRefFrameChoices = NULL;
	}

	m_pAccelRefFrameChoices = ::SafeArrayCreateVector(VT_VARIANT, 0, 4);

	EX_HR( ::SafeArrayAccessData( m_pAccelRefFrameChoices, (void HUGEP**)&psaData ) );

	VariantInit( psaData );

	CComVariant vInertial(L"eUtFrameInertial");
	vInertial.Detach( psaData );
	++psaData;

	CComVariant vFixed(L"eUtFrameFixed");
	vFixed.Detach( psaData );
	++psaData;

	CComVariant vLVLH(L"eUtFrameLVLH");
	vLVLH.Detach( psaData );
	++psaData;

	CComVariant vNTC(L"eUtFrameNTC");
	vNTC.Detach( psaData );

	EX_HR( ::SafeArrayUnaccessData( m_pAccelRefFrameChoices) );

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.InitAccelRefFrameChoices(): <-- Exited\n");
	#endif

	return hr;
}

//=========================
// IAgUtPluginConfig Methods
//=========================
STDMETHODIMP CExample1::GetPluginConfig(IAgAttrBuilder * pAttrBuilder, IDispatch** ppDispScope)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.GetPluginConfig(): --> Entered\n");
	#endif

	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pAttrBuilder )
		EX_OUT_RETVAL_INTERFACE_PARAM( ppDispScope )
	EX_END_PARAMS()

	if( !m_pDispScope )
	{
		EX_HR( pAttrBuilder->NewScope( &m_pDispScope ) );

		EX_HR( pAttrBuilder->AddStringDispatchProperty( m_pDispScope, CComBSTR("PluginName"), CComBSTR("Human readable plugin name or alias"), CComBSTR("Name"), eAddFlagNone ) );
		EX_HR( pAttrBuilder->AddBoolDispatchProperty  ( m_pDispScope, CComBSTR("PluginEnabled"), CComBSTR("If the plugin is enabled or has experience an error"),	CComBSTR("Enabled"), eAddFlagNone ) );
		EX_HR( pAttrBuilder->AddStringDispatchProperty( m_pDispScope, CComBSTR("VectorName"), CComBSTR("Vector Name that affects the srp area"), CComBSTR("VectorName"), eAddFlagNone ) );

		//===========================
		// Propagation related
		//===========================
		EX_HR( pAttrBuilder->AddChoicesDispatchProperty( m_pDispScope, CComBSTR("AccelRefFrame"), CComBSTR("Acceleration Reference Frame"), CComBSTR("AccelRefFrame"), m_pAccelRefFrameChoices ) );
		EX_HR( pAttrBuilder->AddDoubleDispatchProperty ( m_pDispScope, CComBSTR("AccelX"), CComBSTR("Acceleration in the X direction"), CComBSTR("AccelX"), eAddFlagNone ) );
		EX_HR( pAttrBuilder->AddDoubleDispatchProperty ( m_pDispScope, CComBSTR("AccelY"), CComBSTR("Acceleration in the Y direction"), CComBSTR("AccelY"), eAddFlagNone ) );
		EX_HR( pAttrBuilder->AddDoubleDispatchProperty ( m_pDispScope, CComBSTR("AccelZ"), CComBSTR("Acceleration in the Z direction"), CComBSTR("AccelZ"), eAddFlagNone ) );

		//=============================
		// Messaging related attributes
		//=============================
		EX_HR( pAttrBuilder->AddBoolDispatchProperty( m_pDispScope, CComBSTR("UsePropagationMessages"), CComBSTR("Send messages to the message window during propagation"), CComBSTR("MsgStatus"), eAddFlagNone ) );
		EX_HR( pAttrBuilder->AddIntDispatchProperty ( m_pDispScope, CComBSTR("EvaluateMessageInterval"), CComBSTR("The interval at which to send messages from the Evaluate method during propagation"), CComBSTR("EvalMsgInterval"), eAddFlagNone ) );
		EX_HR( pAttrBuilder->AddIntDispatchProperty ( m_pDispScope, CComBSTR("PostEvaluateMessageInterval"), CComBSTR("The interval at which to send messages from the PostEvaluate method during propagation"), CComBSTR("PostEvalMsgInterval"), eAddFlagNone ) );
		EX_HR( pAttrBuilder->AddIntDispatchProperty ( m_pDispScope, CComBSTR("PreNextStepMessageInterval"), CComBSTR("The interval at which to send messages from the PreNextStep method during propagation"), CComBSTR("PreNextMsgInterval"), eAddFlagNone ) );
	}

	m_pDispScope.CopyTo(ppDispScope);

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.GetPluginConfig(): <-- Exited\n");
	#endif

	return S_OK;
}

STDMETHODIMP CExample1::VerifyPluginConfig(IAgUtPluginConfigVerifyResult * pPluginCfgResult)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.VerifyPluginConfig(): --> Entered\n");
	#endif

	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pPluginCfgResult )
	EX_END_PARAMS()

	EX_HR( pPluginCfgResult->put_Result( VARIANT_TRUE ) );
	EX_HR( pPluginCfgResult->put_Message( BSTR( "Ok" ) ) );

	double X = NULL;
	double Y = NULL;
	double Z = NULL;

	EX_HR( get_AccelX( &X ) );
	EX_HR( get_AccelY( &Y ) );
	EX_HR( get_AccelZ( &Z ) );

	if( !( X <= 10 && X >= -10 ) )
	{
		EX_HR( pPluginCfgResult->put_Result( VARIANT_FALSE ) );
		EX_HR( pPluginCfgResult->put_Message( CComBSTR("AccelX was not within the range of -10 to +10 meters per second squared") ) );
	}
	else if( !( Y <= 10 && Y >= -10 ) )
	{
		EX_HR( pPluginCfgResult->put_Result( VARIANT_FALSE ) );
		EX_HR( pPluginCfgResult->put_Message( CComBSTR("AccelY was not within the range of -10 to +10 meters per second squared") ) );
	}
	else if( !( Z <= 10 && Z >= -10 ) )
	{
		EX_HR( pPluginCfgResult->put_Result( VARIANT_FALSE ) );
		EX_HR( pPluginCfgResult->put_Message( CComBSTR("AccelZ was not within the range of -10 to +10 meters per second squared") ) );
	}

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.VerifyPluginConfig(): <-- Exited\n");
	#endif

	return S_OK;
}

//=========================
// IAgAsHpopPlugin Methods
//=========================
STDMETHODIMP CExample1::Init(IAgUtPluginSite* pUtPluginSite, VARIANT_BOOL* pResult)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.Init(): --> Entered\n");
	#endif

	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( pUtPluginSite )
		EX_OUT_RETVAL_PARAM( pResult )
	EX_END_PARAMS()

	HRESULT hr		= S_OK;

	try
	{
		this->m_pUtPluginSite = pUtPluginSite;

		if( this->m_Enabled )
		{
			m_pCrdnPluginProvider = NULL;

			CComPtr<IAgStkPluginSite> pPluginSite;

			m_pUtPluginSite->QueryInterface(&pPluginSite);

			if(pPluginSite)
			{
				pPluginSite->get_VectorToolProvider( &m_pCrdnPluginProvider );

				if(m_pCrdnPluginProvider)
				{
					m_pCrdnPluginProvider->ConfigureVector( this->m_VectorName, L"",	L"J2000", L"CentralBody/Earth", &m_pCrdnConfiguredVector );
				}

				m_pStkRootObject = NULL;
				//----------------------------------------------------
				// Get a pointer to the STK Object Model root object
				//----------------------------------------------------
				CComPtr<IDispatch> pRootObjectDisp;
				if(SUCCEEDED(pPluginSite->get_StkRootObject( &pRootObjectDisp )))
				{
					hr = pRootObjectDisp->QueryInterface(&m_pStkRootObject);
				}
			}
			else
			{
				USES_CONVERSION;

				char buffer[256];

				CComBSTR bstrName;
				this->m_pUtPluginSite->get_SiteName(&bstrName);

				sprintf(buffer, "Init(): Could not obtain IAgStkPluginSite from %s", W2A(bstrName) );
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "Init(): Turning off the computation of SRP Area" );
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
			}

			if( this->m_MsgStatus )
			{
				USES_CONVERSION;

				char buffer[256];

				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR("Init():"));

				CComBSTR bstrRefFrame;
				this->get_AccelRefFrame( &bstrRefFrame );
				sprintf(buffer, "Init(): AccelRefFrame( %s )", W2A( bstrRefFrame ) );
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "Init(): AccelX( %6.5e )", this->m_AccelX );
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "Init(): AccelY( %6.5e )", this->m_AccelY );
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "Init(): AccelZ( %6.5e )", this->m_AccelZ );
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
			}

			if( !this->m_pCrdnConfiguredVector)
			{
				USES_CONVERSION;

				char buffer[256];

				sprintf(buffer, "Init(): Could not obtain %s", W2A(this->m_VectorName) );
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "Init(): Turning off the computation of SRP Area" );
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
			}

			if( !m_pStkRootObject )
			{
				USES_CONVERSION;

				char buffer[256];

				sprintf(buffer, "Init(): Could not obtain the STK Object Model" );
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
			}
		}
		else
		{
			if( this->m_MsgStatus )
			{
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR("Init(): Disabled") );
			}
		}
	}
	catch(...)
	{
		this->m_Enabled = false;

		if( this->m_pUtPluginSite )
		{
			this->m_pUtPluginSite->Message( eUtLogMsgAlarm, CComBSTR("Init(): Exception") );
		}
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("CPP.Example1.Init() <-> Exception\n");
		#endif
	}

	*pResult = this->m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.Init(): <-- Exited\n");
	#endif

	return hr;
}

STDMETHODIMP CExample1::PrePropagate(IAgAsHpopPluginResult* Result, VARIANT_BOOL* pResult)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.PrePropagate() --> Entered\n");
	#endif

	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	try
	{
		if( m_Enabled )
		{
			long	WholeDays	=	0;
			double	SecsIntoDay =	0.0;
			long	Year		=	0;
			long	DayOfYear	=	0;
			long	Month		=	0;
			long	Hours		=	0;
			long	Minutes		=	0;
			double	Seconds		=	0.0;

			Result->RefEpoch( eUtTimeScaleUTC, &WholeDays, &SecsIntoDay, &Year, &DayOfYear, &Month, &Hours, &Minutes, &Seconds );

			VARIANT_BOOL	vb;
			Result->IsForceModelOn( eSRPModel, &vb);
			m_SrpIsOn =  (vb == VARIANT_TRUE) ? true : false;
			if(m_SrpIsOn)
			{
				Result->get_SRPArea( &m_SRPArea );
			}

			if( m_MsgStatus )
			{
				USES_CONVERSION;

				char buffer[256];

				sprintf(buffer, "PrePropagate():");
				m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "PrePropagate(): Epoch WholeDays( %d )", WholeDays );
				m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "PrePropagate(): Epoch SecsIntoDay( %d )", (int)SecsIntoDay );
				m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "PrePropagate(): Epoch Year( %d )", Year );
				m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "PrePropagate(): Epoch DayOfYear( %d )", DayOfYear );
				m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "PrePropagate(): Epoch Month( %d )", Month );
				m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "PrePropagate(): Epoch Hours( %d )", Hours );
				m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "PrePropagate(): Epoch Minutes( %d )", Minutes );
				m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				sprintf(buffer, "PrePropagate(): Epoch Seconds( %d )", (int)Seconds );
				m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
			}

			if( m_pStkRootObject )
			{
				PreConfigureObjectModel();
			}
		}
		else
		{
			if( m_MsgStatus )
			{
				m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR("Prepropagate(): Disabled" ) );
			}
		}
	}
	catch(...)
	{
		this->m_Enabled = false;

		if( this->m_pUtPluginSite )
		{
			this->m_pUtPluginSite->Message( eUtLogMsgAlarm, CComBSTR("PrePropagate(): Exception") );
		}
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("CPP.Example1.PrePropagate() <-> Exception\n");
		#endif
	}

	*pResult = this->m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.PrePropagate() <-- Exited\n");
	#endif

	return S_OK;
}

STDMETHODIMP CExample1::PreNextStep(IAgAsHpopPluginResult* Result, VARIANT_BOOL* pResult)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	if( this->m_PreNextCntr % this->m_PreNextMsgInterval == 0 )
	{
		OutputDebugString("CPP.Example1.PreNextStep() --> Entered\n");
	}
	#endif

	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	try
	{
		m_PreNextCntr++;

		if( this->m_Enabled )
		{
			if( this->m_MsgStatus )
			{
				if( this->m_PreNextCntr % this->m_PreNextMsgInterval == 0 )
				{
					char buffer[256];
					sprintf(buffer, "PreNextStep( %d ):", this->m_PreNextCntr );
					this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
				}
			}
		}
		else
		{
			if( this->m_MsgStatus )
			{
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( "PreNextStep(): Disabled" ) );
			}
		}
	}
	catch( ... )
	{
		this->m_Enabled = false;

		if( this->m_pUtPluginSite )
		{
			this->m_pUtPluginSite->Message( eUtLogMsgAlarm, CComBSTR( "PreNextStep(): Exception" ) );
		}
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("CPP.Example1.PreNextStep(): Exception\n");
		#endif
	}

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	if( this->m_PreNextCntr % this->m_PreNextMsgInterval == 0 )
	{
		OutputDebugString("CPP.Example1.PreNextStep() <-- Exited\n");
	}
	#endif

	*pResult = this->m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	return S_OK;
}

STDMETHODIMP CExample1::Evaluate(IAgAsHpopPluginResultEval* ResultEval, VARIANT_BOOL* pResult)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	if( this->m_EvalCntr % this->m_EvalMsgInterval == 0 )
	{
		//string msg;
		//msg += "CPP.Example1.Evaluate( ";
		//msg += this->m_EvalCntr;
		//msg += " ) --> Entered\n";
		//OutputDebugString(msg.c_str());
	}
	#endif

	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( ResultEval )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	try
	{
		this->m_EvalCntr++;

		if( this->m_Enabled )
		{
			this->EvaluateSRPArea( ResultEval );

			ResultEval->AddAcceleration( (AgEUtFrame)this->m_AccelRefFrame, this->m_AccelX, this->m_AccelY, this->m_AccelZ );

			if( this->m_MsgStatus )
			{
				if( this->m_EvalCntr % this->m_EvalMsgInterval == 0 )
				{
					char buffer[256];
					sprintf(buffer, "Evaluate( %d ):", this->m_EvalCntr );
					this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
				}
			}
		}
		else
		{
			if( this->m_MsgStatus )
			{
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR("Evaluate(): Disabled") );
			}
		}
	}
	catch( ... )
	{
		this->m_Enabled = false;

		if( this->m_pUtPluginSite )
		{
			this->m_pUtPluginSite->Message( eUtLogMsgAlarm, CComBSTR("Evaluate(): Exception") );
		}
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("CPP.Example1.Evaluate() <-> Exception Message\n");
		#endif
	}

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	if( this->m_EvalCntr % this->m_EvalMsgInterval == 0 )
	{
		//string msg;
		//msg += "CPP.Example1.Evaluate( ";
		//msg += this->m_EvalCntr;
		//msg += " ) --> Entered\n";
		//OutputDebugString(msg.c_str());
	}
	#endif

	*pResult = this->m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	return S_OK;
}

STDMETHODIMP CExample1::PostEvaluate(IAgAsHpopPluginResultPostEval* ResultEval, VARIANT_BOOL* pResult)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	if( this->m_PostEvalCntr % this->m_PostEvalMsgInterval == 0 )
	{
		//string msg;
		//msg += "CPP.Example1.PostEvaluate( ";
		//msg += this->m_PostEvalCntr;
		//msg += " ) --> Entered\n";
		//OutputDebugString(msg.c_str());
	}
	#endif

	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( ResultEval )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	try
	{
		this->m_PostEvalCntr++;

		if( this->m_Enabled )
		{
			if( this->m_MsgStatus )
			{
				if( this->m_PostEvalCntr % this->m_PostEvalMsgInterval == 0 )
				{
					AgEUtFrame		reportFrame = eUtFrameNTC;
					char			frameName[10];
					strcpy(frameName, "NTC");

					AgEAccelType	accelType = eSRPAccel;
					double			srpX, srpY, srpZ, SRPArea, Alt;

					ResultEval->get_SRPArea( &SRPArea );
					ResultEval->get_Altitude( &Alt );

					ResultEval->GetAcceleration( accelType, reportFrame, &srpX, &srpY, &srpZ );

					char buffer[256];
					sprintf(buffer, "PostEvaluate( %d ): SRPArea (%.3f m^2), Altitude (%.6f km)",
							this->m_PostEvalCntr, SRPArea, Alt*0.001 );
					this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
					sprintf(buffer, "PostEvaluate( %d ): SRPAccel (%s) is (%15.6e, %15.6e, %15.6e) meters/secs^2",
							this->m_PostEvalCntr, frameName, srpX, srpY, srpZ );
					this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

					// report out the added acceleration in NTC components
					double	thrustX, thrustY, thrustZ;
					accelType = eAddedAccel;

					ResultEval->GetAcceleration( accelType, reportFrame, &thrustX, &thrustY, &thrustZ );
					sprintf(buffer, "PostEvaluate( %d ): ThrustAccel (%s) is (%15.6e, %15.6e, %15.6e) meters/secs^2",
							this->m_PostEvalCntr, frameName, thrustX, thrustY, thrustZ );
					this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

				}
			}
		}
		else
		{
			if( this->m_MsgStatus )
			{
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR("PostEvaluate(): Disabled") );
			}
		}
	}
	catch( ... )
	{
		this->m_Enabled = false;

		if( this->m_pUtPluginSite )
		{
			this->m_pUtPluginSite->Message( eUtLogMsgAlarm, CComBSTR("PostEvaluate(): Exception") );
		}
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("CPP.Example1.Evaluate() <-> Exception Message\n");
		#endif
	}

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	if( this->m_PostEvalCntr % this->m_PostEvalMsgInterval == 0 )
	{
		//string msg;
		//msg += "CPP.Example1.PostEvaluate( ";
		//msg += this->m_PostEvalCntr;
		//msg += " ) --> Entered\n";
		//OutputDebugString(msg.c_str());
	}
	#endif

	*pResult = this->m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	return S_OK;
}

HRESULT CExample1::EvaluateSRPArea( IAgAsHpopPluginResultEval* ResultEval )
{
	if(!m_SrpIsOn)
	{
		return S_OK;
	}

	// vector may not have been able to be obtained
	if(m_pCrdnConfiguredVector == NULL)
	{
		return S_OK;
	}

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	if( this->m_EvalCntr % this->m_EvalMsgInterval == 0 )
	{
		//string msg;
		//msg += "CPP.Example1.EvaluateSRPArea( ";
		//msg += this->m_EvalCntr;
		//msg += " ) --> Entered\n";
		//OutputDebugString(msg.c_str());
	}
	#endif

	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( ResultEval )
	EX_END_PARAMS()

	VARIANT_BOOL Result		= VARIANT_FALSE;

	double	VecX			= 0.0;
	double	VecY			= 0.0;
	double	VecZ			= 0.0;

	double	PosX			= 0.0;
	double	PosY			= 0.0;
	double	PosZ			= 0.0;

	double	VelX			= 0.0;
	double	VelY			= 0.0;
	double	VelZ			= 0.0;

	double	VecPosDotProd	= 0.0;
	double	VecMag			= 0.0;
	double	PosMag			= 0.0;
	double	Theta			= 0.0;

	double NewSRPArea		= 0.0;

	//========================================
	// 1. Get the "User Choosen" Vector XYZ
	// 2. Get the Position Vector XYZ in
	//    Central Body Inertial Frame ( CBI )
	//========================================
	try
	{
		ResultEval->PosVel( eUtFrameInertial, &PosX, &PosY, &PosZ, &VelX, &VelY, &VelZ );
		this->m_pCrdnConfiguredVector->CurrentValue( ResultEval, &VecX, &VecY, &VecZ, &Result );

		if( Result )
		{
			//===============================================================
			// Calculate the angle (Theta in radians) between the two vectors
			// 1. Calculate the Dot Product
			// 2. Calculate the angle
			//================================================================
			VecPosDotProd	= (( VecX * PosX ) + ( VecY * PosY ) + ( VecZ * PosZ ));
			VecMag			= sqrt( VecX*VecX + VecY*VecY + VecZ*VecZ );
			PosMag			= sqrt( PosX*PosX + PosY*PosY + PosZ*PosZ );
			Theta			= acos( VecPosDotProd / ( VecMag * PosMag ) );

			//===============================================
			// Calculate the new srp area based on the Theta
			//===============================================
			NewSRPArea = ( this->m_SRPArea / 4.0 ) * ( 3.0 - sin( Theta ) );

			// SRP must be on eslse this will throw an exception
			ResultEval->put_SRPArea( NewSRPArea );

			if( this->m_pUtPluginSite && this->m_MsgStatus )
			{
				if( this->m_EvalCntr % this->m_EvalMsgInterval == 0 )
				{
					char buffer[256];

					sprintf(buffer, "EvaluateSRPArea( %d ): VecX( %15.6e ), VecY( %15.6e ), VecZ( %15.6e ) meters/sec", this->m_EvalCntr, VecX, VecY, VecZ );
					this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

					sprintf(buffer, "EvaluateSRPArea( %d ): PosX( %15.6e ), PosY( %15.6e ), PosZ( %15.6e ) meters", this->m_EvalCntr, PosX, PosY, PosZ );
					this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

					sprintf(buffer, "EvaluateSRPArea( %d ): VelX( %15.6e ), VelY( %15.6e ), VelZ( %15.6e ) meters/sec", this->m_EvalCntr, VelX, VelY, VelZ );
					this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

					double tempSRPArea;
					ResultEval->get_SRPArea( &tempSRPArea );
					sprintf(buffer, "EvaluateSRPArea( %d ): Theta( %.6f  deg), SRPArea( %.3f m^2) ", this->m_EvalCntr, Theta*RAD2DEG, tempSRPArea );
					this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
				}
			}
		}
		else
		{
			if( this->m_pUtPluginSite && this->m_MsgStatus )
			{
				if( this->m_EvalCntr % this->m_EvalMsgInterval == 0 )
				{
					char buffer[256];

					sprintf(buffer, "EvaluateSRPArea( %d ): Result( %d )", this->m_EvalCntr, Result );
					this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
				}
			}
		}
	}
	catch( ... )
	{
		this->m_Enabled = false;

		if( this->m_pUtPluginSite )
		{
			this->m_pUtPluginSite->Message( eUtLogMsgWarning, CComBSTR("EvaluateSRPArea(): Exception") );
		}

		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("CPP.Example1.EvaluateSRPArea() <-> Exception Message\n");
		#endif
	}

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	if( this->m_EvalCntr % this->m_EvalMsgInterval == 0 )
	{
		//string msg;
		//msg += "CPP.Example1.EvaluateSRPArea( ";
		//msg += this->m_EvalCntr;
		//msg += " ) <-- Exited\n";
		//OutputDebugString(msg.c_str());
	}
	#endif

	return S_OK;
}

STDMETHODIMP CExample1::PostPropagate(IAgAsHpopPluginResult * Result, VARIANT_BOOL * pResult)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.PostPropagate() --> Entered\n");
	#endif

	EX_BEGIN_PARAMS()
		EX_IN_INTERFACE_PARAM( Result )
		EX_OUT_RETVAL_INTERFACE_PARAM( pResult )
	EX_END_PARAMS()

	try
	{
		if( this->m_Enabled )
		{
			if( this->m_MsgStatus )
			{
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR("PostPropagate():") );
			}
		}
		else
		{
			if( this->m_MsgStatus )
			{
				this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR("PostPropagate(): Disabled") );
			}
		}
	}
	catch( ... )
	{
		this->m_Enabled = false;

		if( this->m_pUtPluginSite )
		{
			this->m_pUtPluginSite->Message( eUtLogMsgAlarm, CComBSTR("PostPropagate(): Exception" ) );
		}
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("CPP.Example1.PostPropagate() <-> Exception\n");
		#endif
	}

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.PostPropagate() <-- Exited\n");
	#endif

	*pResult = this->m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	return S_OK;
}

STDMETHODIMP CExample1::Free()
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.Free() --> Entered\n");
	#endif

	try
	{
		if ( this->m_MsgStatus )
		{
			char buffer[256];

			this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR("Free():") );

			sprintf(buffer, "Free(): PreNextCntr( %d )", this->m_PreNextCntr );
			this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

			sprintf(buffer, "Free(): EvalCntr( %d )", this->m_EvalCntr );
			this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );

			sprintf(buffer, "Free(): PostEvalCntr( %d )", this->m_PostEvalCntr );
			this->m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
		}
	}
	catch( ... )
	{
		if( this->m_pUtPluginSite )
		{
			this->m_pUtPluginSite->Message( eUtLogMsgAlarm, CComBSTR("Free(): Exception Message)") );
		}
		#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
		OutputDebugString("CPP.Example1.Free() <-> Exception\n");
		#endif
	}

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.Free() <-- Exited\n");
	#endif

	return S_OK;
}

STDMETHODIMP CExample1::get_Name(BSTR* pVal)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.get_Name() --> Entered\n");
	#endif

	HRESULT hr = S_OK;

	hr = m_Name.CopyTo(pVal);

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "CPP.Example1.get_Name(): ";
	msg += W2A(m_Name);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Example1.get_Name() <-- Exited\n");
	#endif

	return hr;
}

//=====================
// IExample1 Methods
//=====================
STDMETHODIMP CExample1::get_Enabled( VARIANT_BOOL* pVal )
{
	*pVal = m_Enabled ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.get_Enabled(): ";
	//msg += m_Enabled;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}
STDMETHODIMP CExample1::put_Enabled( VARIANT_BOOL Val )
{
	m_Enabled = ( Val == VARIANT_TRUE );

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.put_Enabled(): ";
	//msg += m_Enabled;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::get_VectorName( BSTR* pVal )
{
	m_VectorName.CopyTo( pVal );

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "CPP.Example1.get_VectorName(): ";
	msg += W2A(m_VectorName.m_str);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}
STDMETHODIMP CExample1::put_VectorName( BSTR Val )
{
	m_VectorName = Val;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "CPP.Example1.put_VectorName(): ";
	msg += W2A(m_VectorName.m_str);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::get_AccelX( double* pVal )
{
	*pVal = m_AccelX;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.get_AccelX(): ";
	//msg += m_AccelX;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::put_AccelX( double Val )
{
	m_AccelX = Val;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.put_AccelX(): ";
	//msg += m_AccelX;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}
STDMETHODIMP CExample1::get_AccelY( double* pVal )
{
	*pVal = m_AccelY;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.get_AccelY(): ";
	//msg += m_AccelY;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::put_AccelY( double Val )
{
	m_AccelY = Val;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.put_AccelY(): ";
	//msg += m_AccelY;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::get_AccelZ( double* pVal )
{
	*pVal = m_AccelZ;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.get_AccelZ(): ";
	//msg += m_AccelZ;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::put_AccelZ( double Val )
{
	m_AccelZ = Val;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.put_AccelZ(): ";
	//msg += m_AccelZ;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::get_AccelRefFrame( BSTR* pVal )
{
	HRESULT		hr = S_OK;
	long		Index[1];
	CComVariant	vFrame;

	VariantInit(&vFrame);

	if( m_AccelRefFrame == 0 )
	{
		Index[0] = 0;
		EX_HR( ::SafeArrayGetElement( m_pAccelRefFrameChoices, &Index[0], &vFrame ) );
	}
	else if( m_AccelRefFrame == 1 )
	{
		Index[0] = 1;
		EX_HR( ::SafeArrayGetElement( m_pAccelRefFrameChoices, &Index[0], &vFrame ) );
	}
	else if( m_AccelRefFrame == 2 )
	{
		Index[0] = 2;
		EX_HR( ::SafeArrayGetElement( m_pAccelRefFrameChoices, &Index[0], &vFrame ) );
	}
	else if( m_AccelRefFrame == 3 )
	{
		Index[0] = 3;
		EX_HR( ::SafeArrayGetElement( m_pAccelRefFrameChoices, &Index[0], &vFrame ) );
	}
	else
	{
		hr = E_FAIL;
	}

	if( vFrame.vt == VT_BSTR )
	{
		*pVal = vFrame.bstrVal;
	}

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "CPP.Example1.get_AccelRefFrame(): ";
	msg += W2A(vFrame.bstrVal);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	return hr;
}

STDMETHODIMP CExample1::put_AccelRefFrame( BSTR Val )
{
	HRESULT hr = S_OK;
	CComBSTR bstrFrame( Val );

	if( bstrFrame == CComBSTR( L"eUtFrameInertial" ) )
	{
		m_AccelRefFrame = 0;
	}
	else if( bstrFrame == CComBSTR( L"eUtFrameFixed" ) )
	{
		m_AccelRefFrame = 1;
	}
	else if( bstrFrame == CComBSTR( L"eUtFrameLVLH" ) )
	{
		m_AccelRefFrame = 2;
	}
	else if( bstrFrame == CComBSTR( L"eUtFrameNTC" ) )
	{
		m_AccelRefFrame = 3;
	}
	else
	{
		hr = E_FAIL;
	}

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	USES_CONVERSION;

	string msg;
	msg += "CPP.Example1.put_AccelRefFrame(): ";
	msg += W2A(Val);
	msg += "\n";
	OutputDebugString(msg.c_str());

	#endif

	return hr;
}

STDMETHODIMP CExample1::get_AccelRefFrameChoices( SAFEARRAY** ppArray )
{
	ppArray = &m_pAccelRefFrameChoices;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	string msg = "CPP.Example1.get_AccelRefFrameChoices(): ";
	OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::get_MsgStatus( VARIANT_BOOL* pVal )
{
	*pVal = m_MsgStatus ? VARIANT_TRUE : VARIANT_FALSE;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.get_MsgStatus(): ";
	//msg += m_MsgStatus;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}
STDMETHODIMP CExample1::put_MsgStatus( VARIANT_BOOL Val )
{
	m_MsgStatus = ( Val == VARIANT_TRUE );

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.put_MsgStatus(): ";
	//msg += m_MsgStatus;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::get_EvalMsgInterval( long* pVal )
{
	*pVal = m_EvalMsgInterval;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.get_EvalMsgInterval(): ";
	//msg += m_EvalMsgInterval;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::put_EvalMsgInterval( long Val )
{
	m_EvalMsgInterval = Val;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.put_EvalMsgInterval(): ";
	//msg += m_EvalMsgInterval;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::get_PostEvalMsgInterval( long* pVal )
{
	*pVal = m_PostEvalMsgInterval;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.get_PostEvalMsgInterval(): ";
	//msg += m_PostEvalMsgInterval;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::put_PostEvalMsgInterval( long Val )
{
	m_PostEvalMsgInterval = Val;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.put_PostEvalMsgInterval(): ";
	//msg += m_PostEvalMsgInterval;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::get_PreNextMsgInterval( long* pVal )
{
	*pVal = m_PreNextMsgInterval;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.get_PreNextMsgInterval(): ";
	//msg += m_PreNextMsgInterval;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

STDMETHODIMP CExample1::put_PreNextMsgInterval( long Val )
{
	m_PreNextMsgInterval = Val;

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING

	//string msg;
	//msg += "CPP.Example1.put_PreNextMsgInterval(): ";
	//msg += m_PreNextMsgInterval;
	//msg += "\n";
	//OutputDebugString(msg.c_str());

	#endif

	return S_OK;
}

void CExample1::PreConfigureObjectModel()
{
	HRESULT                           hr = S_OK;
	CComPtr<IAgStkObject>             pSc;
	char                              buffer[1024];
	CComBSTR                          bstr;
	CComPtr<IAgStkObjectCollection>   pChildren;

	USES_CONVERSION;
	//-------------------------------------------------------------
	// Through the STK Object Model users gain a limited
	// access to STK. Users are given a read-only access.
	//-------------------------------------------------------------
	if(SUCCEEDED(m_pStkRootObject->get_CurrentScenario(&pSc)))
	{
		// Get the scenario name
		if(SUCCEEDED(pSc->get_InstanceName(&bstr)))
		{
			const char *pInstName = OLE2A((BSTR)bstr);
			sprintf(buffer, "Current Scenario Name is %s",  pInstName);
		}
		else
		{
			sprintf(buffer, __FUNCTION__ ": Failed to get the Scenario Name");
		}
		// Print the scenario name
		m_pUtPluginSite->Message( eUtLogMsgInfo, CComBSTR( buffer ) );

		strcpy(buffer, "");
		// Get the children of the scenario
		if(SUCCEEDED(pSc->get_Children(&pChildren)))
		{
			long lCount;
			if(SUCCEEDED(pChildren->get_Count(&lCount)))
			{
				for(int i = 0; i < lCount; i++)
				{
					CComPtr<IAgStkObject> pChild;
					CComVariant var(i);
					if(SUCCEEDED(pChildren->get_Item(var, &pChild)))
					{
						if(FAILED(pChild->get_InstanceName(&bstr)))
						{
							sprintf(buffer, __FUNCTION__ ": Failed to get the object's instance name at the position \"%d\"", i);
							break;
						}
					}
					else
					{
						sprintf(buffer, __FUNCTION__ ": Failed to get the element at the position \"%d\"", i);
						break;
					}
				}
				if(strlen(buffer))
				{
					// Print the scenario name
					m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
				}
			}
			else
			{
				sprintf(buffer, __FUNCTION__ ": Failed to get the children's count");
			}
		}
		else
		{
			sprintf(buffer, __FUNCTION__ ": Failed to get the Scenario' children");
		}
		if(strlen(buffer))
		{
			m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
		}
	}

	//-------------------------------------------------------------
	// Now we are going to try to execute an action
	// that should be denied by the Object Model.
	//-------------------------------------------------------------
	if(FAILED(hr = m_pStkRootObject->CloseScenario()))
	{
		CComPtr<IErrorInfo> pperrinfo;
		if(SUCCEEDED(GetErrorInfo(0, &pperrinfo)) && SUCCEEDED(pperrinfo->GetDescription(&bstr)))
		{
			CComBSTR bstrTemp;
			bstrTemp.Append(__FUNCTION__ ": CloseScenario()->");
			bstrTemp.AppendBSTR(bstr);
			//m_pUtPluginSite->Message( eUtLogMsgDebug, bstrTemp );
		}
		else
		{
			sprintf(buffer, __FUNCTION__ ": Failed to close the scenario, reason: %x", hr );
			//m_pUtPluginSite->Message( eUtLogMsgDebug, CComBSTR( buffer ) );
		}
	}
	else
	{
			sprintf(buffer, __FUNCTION__ ": Plugin must not be allowed to close a scenario, this will lead to unpredictable results!");
			m_pUtPluginSite->Message( eUtLogMsgAlarm, CComBSTR( buffer ) );
	}
}

/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
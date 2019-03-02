//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
// ObjectModelEventSink.cpp : implementation file
//

#include "AgUiPluginCppAddInStd.h"
#include "AgUiPluginTutorial.h"
#include "ObjectModelEventSink.h"
#include <time.h>

// CObjectModelEventSink

IMPLEMENT_DYNAMIC(CObjectModelEventSink, CCmdTarget)

CObjectModelEventSink::CObjectModelEventSink(IEventLogger* pEventLogger) : m_pEventLogger(pEventLogger)
{
	EnableAutomation();
}

CObjectModelEventSink::~CObjectModelEventSink()
{
}


void CObjectModelEventSink::OnFinalRelease()
{
	CCmdTarget::OnFinalRelease();
}


BEGIN_MESSAGE_MAP(CObjectModelEventSink, CCmdTarget)
END_MESSAGE_MAP()

BEGIN_DISPATCH_MAP(CObjectModelEventSink, CCmdTarget)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnScenarioNew", 1, OnScenarioNew, VT_EMPTY, VTS_VARIANT)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnScenarioLoad", 2, OnScenarioLoad, VT_EMPTY, VTS_VARIANT)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnScenarioClose", 3, OnScenarioClose, VT_EMPTY, VTS_NONE)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnScenarioSave", 4, OnScenarioSave, VT_EMPTY, VTS_VARIANT)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnLogMessage", 5, OnLogMessage, VT_EMPTY, VTS_VARIANT VTS_I2 VTS_I4 VTS_VARIANT VTS_I4 VTS_I2)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnAnimUpdate", 6, OnAnimUpdate, VT_EMPTY, VTS_R8)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnStkObjectAdded", 20, OnStkObjectAdded, VT_EMPTY, VTS_VARIANT)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnStkObjectDeleted", 21, OnStkObjectDeleted, VT_EMPTY, VTS_VARIANT)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnStkObjectRenamed", 22, OnStkObjectRenamed, VT_EMPTY, VTS_VARIANT VTS_VARIANT VTS_VARIANT)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnAnimationPlayback", 23, OnAnimationPlayback, VT_EMPTY, VTS_R8 VTS_I2 VTS_I2)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnAnimationRewind", 24, OnAnimationRewind, VT_EMPTY, VTS_NONE)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnAnimationPause", 25, OnAnimationPause, VT_EMPTY, VTS_R8)
	DISP_FUNCTION_ID(CObjectModelEventSink, "OnObjectChanged", 34, OnObjectChanged, VT_EMPTY, VTS_UNKNOWN)
END_DISPATCH_MAP()

BEGIN_INTERFACE_MAP(CObjectModelEventSink, CCmdTarget)
	INTERFACE_PART(CObjectModelEventSink, __uuidof(IAgStkObjectRootEvents), Dispatch)
END_INTERFACE_MAP()


// CObjectModelEventSink message handlers

void CObjectModelEventSink::OnScenarioNew(VARIANT* bstrPath)
{
	m_pEventLogger->LogEvent(_T("SCENARIONEW"), CString(bstrPath->bstrVal));
}
void CObjectModelEventSink::OnScenarioLoad(VARIANT* bstrPath)
{
	m_pEventLogger->LogEvent(_T("SCENARIOLOAD"), CString(bstrPath->bstrVal));
}
void CObjectModelEventSink::OnScenarioClose()
{
	m_pEventLogger->LogEvent(_T("SCENARIOCLOSED"), _T(""));
}
void CObjectModelEventSink::OnScenarioSave(VARIANT* bstrPath)
{
	m_pEventLogger->LogEvent(_T("SCENARIOSAVE"), CString(bstrPath->bstrVal));
}
void CObjectModelEventSink::OnLogMessage(VARIANT* bstrMessage, int p1, long p2, VARIANT* p3, long p4, int p5)
{
	m_pEventLogger->LogEvent(_T("LOGMESSAGE"), CString(bstrMessage->bstrVal));
}
void CObjectModelEventSink::OnAnimUpdate(double time)
{
	char buf[100];
	sprintf(buf, "%6.2f", time);
	m_pEventLogger->LogEvent(_T("ANIUPDATE"), CString(buf));
}
void CObjectModelEventSink::OnStkObjectAdded(VARIANT* sender)
{
	m_pEventLogger->LogEvent(_T("OBJECTADDED"), CString(sender->bstrVal));
}
void CObjectModelEventSink::OnStkObjectDeleted(VARIANT* sender)
{
	m_pEventLogger->LogEvent(_T("OBJECTDELETED"), CString(sender->bstrVal));
}
void CObjectModelEventSink::OnStkObjectRenamed(VARIANT* sender, VARIANT* oldpath, VARIANT* newpath)
{
	m_pEventLogger->LogEvent(_T("OBJECTRENAMED"), CString(sender->bstrVal));
}
void CObjectModelEventSink::OnAnimationPlayback(double time, int p1, int p2)
{
	TCHAR buf[100];
	TCHAR message[255];
	TCHAR* action;
	TCHAR* direction;

	_stprintf_s(buf, _T("%6.2f"), time);
	switch(p1)
	{
	case eAniActionStart:
		action = _T("Start");
		break;
	case eAniActionPlay:
		action = _T("Play");
		break;
	default:
		action = _T("Unknown");
	}
	switch(p2)
	{
	default:
	case eAniNonAvail:
		direction = _T("Not available");
		break;
	case eAniForward:
		direction = _T("Forward");
		break;
	case eAniBackward:
		direction = _T("Backward");
		break;
		
	}
	_stprintf_s(message, _T("%s, action: %s, direction: %s"), _T("PLAYBACK"), action, direction);
	m_pEventLogger->LogEvent(message, CString(buf));
}
void CObjectModelEventSink::OnAnimationRewind()
{
	m_pEventLogger->LogEvent(_T("REWIND"), _T(""));
}
void CObjectModelEventSink::OnAnimationPause(double time)
{
	char buf[100];
	sprintf(buf, "%6.2f", time);
	m_pEventLogger->LogEvent(_T("PAUSE"), CString(buf));
}
void CObjectModelEventSink::OnObjectChanged(IUnknown* pSender)
{
	CComQIPtr<IAgStkObjectChangedEventArgs> pArgs (pSender);
	if (pArgs)
	{
		CComBSTR bstr;
		pArgs->get_Path(&bstr);

		m_pEventLogger->LogEvent(_T("OBJECTCHANGED"), CString(bstr));
	}
}

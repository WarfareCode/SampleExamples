// ObjectModelEventSink.cpp : implementation file
//

#include "stdafx.h"
#include "Events.h"
#include "ObjectModelEventSink.h"
#include "EventsDlg.h"

// CObjectModelEventSink

IMPLEMENT_DYNAMIC(CObjectModelEventSink, CCmdTarget)
CObjectModelEventSink::CObjectModelEventSink(CEventsDlg*  pOwner) : m_pOwner(pOwner)
{
	EnableAutomation();
}

CObjectModelEventSink::~CObjectModelEventSink()
{
}


void CObjectModelEventSink::OnFinalRelease()
{
	// When the last reference for an automation object is released
	// OnFinalRelease is called.  The base class will automatically
	// deletes the object.  Add additional cleanup required for your
	// object before calling the base class.

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
END_DISPATCH_MAP()

BEGIN_INTERFACE_MAP(CObjectModelEventSink, CCmdTarget)
	INTERFACE_PART(CObjectModelEventSink, __uuidof(STKObjects::IAgStkObjectRootEvents), Dispatch)
END_INTERFACE_MAP()


// CObjectModelEventSink message handlers

void CObjectModelEventSink::OnScenarioNew(VARIANT* bstrPath)
{
	m_pOwner->LogEvent(_T("SCENARIONEW:  ") + CString(bstrPath->bstrVal));
}
void CObjectModelEventSink::OnScenarioLoad(VARIANT* bstrPath)
{
	m_pOwner->LogEvent(_T("SCENARIOLOAD: ") + CString(bstrPath->bstrVal));
}
void CObjectModelEventSink::OnScenarioClose()
{
	m_pOwner->LogEvent(_T("SCENARIOCLOSED"));
}
void CObjectModelEventSink::OnScenarioSave(VARIANT* bstrPath)
{
	m_pOwner->LogEvent(_T("SCENARIOSAVE: ") + CString(bstrPath->bstrVal));
}
void CObjectModelEventSink::OnLogMessage(VARIANT* bstrMessage, int p1, long p2, VARIANT* p3, long p4, int p5)
{
	m_pOwner->LogEvent(_T("LOGMESSAGE:   ") + CString(bstrMessage->bstrVal));
}
void CObjectModelEventSink::OnAnimUpdate(double time)
{
	char buf[100];
	gcvt(time, 2, buf);
	m_pOwner->LogEvent(_T("ANIUPDATE:    ") + CString(buf));
}
void CObjectModelEventSink::OnStkObjectAdded(VARIANT* sender)
{
	m_pOwner->LogEvent(_T("OBJECTADDED:  ") + CString(sender->bstrVal));
}
void CObjectModelEventSink::OnStkObjectDeleted(VARIANT* sender)
{
	m_pOwner->LogEvent(_T("OBJECTDELETED:") + CString(sender->bstrVal));
}
void CObjectModelEventSink::OnStkObjectRenamed(VARIANT* sender, VARIANT* oldpath, VARIANT* newpath)
{
	m_pOwner->LogEvent(_T("OBJECTRENAMED:") + CString(sender->bstrVal));
}
void CObjectModelEventSink::OnAnimationPlayback(double time, int p1, int p2)
{
	char buf[100];
	gcvt(time, 2, buf);
	m_pOwner->LogEvent(_T("PLAYBACK:     ") + CString(buf));
}
void CObjectModelEventSink::OnAnimationRewind()
{
	m_pOwner->LogEvent(_T("REWIND:       "));
}
void CObjectModelEventSink::OnAnimationPause(double time)
{
	char buf[100];
	gcvt(time, 2, buf);
	m_pOwner->LogEvent(_T("PAUSE:        ") + CString(buf));
}

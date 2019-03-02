//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
#pragma once

// CObjectModelEventSink command target


class DECLSPEC_UUID("45DDE9A1-2FC6-45f1-955C-3E1602B58CB2")
IEventLogger : public IUnknown
{
public:
	STDMETHOD(LogEvent)(const CString& rEvent, const CString& rString) = 0;
};


class CObjectModelEventSink : public CCmdTarget
{
	DECLARE_DYNAMIC(CObjectModelEventSink)

public:
	CObjectModelEventSink(IEventLogger* pLogger);
	virtual ~CObjectModelEventSink();

	virtual void OnFinalRelease();
	
protected:
	DECLARE_MESSAGE_MAP()
	DECLARE_DISPATCH_MAP()
	DECLARE_INTERFACE_MAP()

private:
	void OnScenarioNew(VARIANT* bstrPath);
	void OnScenarioLoad(VARIANT* bstrPath);
	void OnScenarioClose();
	void OnScenarioSave(VARIANT* bstrPath);
	void OnLogMessage(VARIANT* bstrMessage, int p1, long p2, VARIANT* p3, long p4, int p5);
	void OnAnimUpdate(double time);
	void OnStkObjectAdded(VARIANT* sender);
	void OnStkObjectDeleted(VARIANT* sender);
	void OnStkObjectRenamed(VARIANT* sender, VARIANT* oldpath, VARIANT* newpath);
	void OnAnimationPlayback(double time, int p1, int p2);
	void OnAnimationRewind();
	void OnAnimationPause(double time);
	void OnObjectChanged(IUnknown* pSender);

	// holds a weak reference to the parent object
	IEventLogger* m_pEventLogger;
};

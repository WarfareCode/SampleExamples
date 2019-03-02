
#pragma once

class CEventsDlg;
// CObjectModelEventSink command target

class CObjectModelEventSink : public CCmdTarget
{
	DECLARE_DYNAMIC(CObjectModelEventSink)

public:
	CObjectModelEventSink(CEventsDlg* pOwner);
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

	// holds a reference to the parent object
	CEventsDlg* m_pOwner;
};



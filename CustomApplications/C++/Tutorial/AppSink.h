#if !defined(AFX_APPSINK_H__279525B4_2EE6_4349_9F17_DD61452DA192__INCLUDED_)
#define AFX_APPSINK_H__279525B4_2EE6_4349_9F17_DD61452DA192__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// AppSink.h : header file
//

const IID DIID_IAgSTKXApplicationEvents = {0x86A7DD52,0x9116,0x41F4,{0x8A,0x74,0x30,0xC7,0x38,0x70,0x3E,0x12}};

/////////////////////////////////////////////////////////////////////////////
// AppSink command target

class AppSink : public CCmdTarget
{
	DECLARE_DYNCREATE(AppSink)

	AppSink();           // protected constructor used by dynamic creation

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(AppSink)
	public:
	virtual void OnFinalRelease();
	//}}AFX_VIRTUAL

// Implementationpublic:
	virtual ~AppSink();
protected:
	void OnScenarioNew(BSTR path);


	// Generated message map functions
	//{{AFX_MSG(AppSink)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
	// Generated OLE dispatch map functions
	//{{AFX_DISPATCH(AppSink)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_DISPATCH
	DECLARE_DISPATCH_MAP()
	DECLARE_INTERFACE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_APPSINK_H__279525B4_2EE6_4349_9F17_DD61452DA192__INCLUDED_)

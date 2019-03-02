// Events.cpp : Defines the class behaviors for the application.
//

#include "stdafx.h"
#include "Events.h"
#include "EventsDlg.h"
#include "AgStkUtil.tli"
#include "AgStkObjects.tli"
#include "STKX.tli"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CEventsApp

BEGIN_MESSAGE_MAP(CEventsApp, CWinApp)
	ON_COMMAND(ID_HELP, CWinApp::OnHelp)
END_MESSAGE_MAP()


// CEventsApp construction

CEventsApp::CEventsApp()
{
	// TODO: add construction code here,
	// Place all significant initialization in InitInstance
}


// The one and only CEventsApp object

CEventsApp theApp;

// CEventsApp initialization

BOOL CEventsApp::InitInstance()
{
	// InitCommonControls() is required on Windows XP if an application
	// manifest specifies use of ComCtl32.dll version 6 or later to enable
	// visual styles.  Otherwise, any window creation will fail.
	InitCommonControls();

	CWinApp::InitInstance();

	// Initialize OLE libraries
	if (!AfxOleInit())
	{
		AfxMessageBox(IDP_OLE_INIT_FAILED);
		return FALSE;
	}

	// Check for Valid STK License
	IAgSTKXApplicationPtr   m_pApp;
	m_pApp.CreateInstance(__uuidof(AgSTKXApplication));
	if (!m_pApp->IsFeatureAvailable(eFeatureCodeGlobeControl)) {
		AfxMessageBox(_T("You do not have the required STK Globe license."));
		return false;
	}

	AfxEnableControlContainer();

	CEventsDlg dlg;
	m_pMainWnd = &dlg;
	INT_PTR nResponse = dlg.DoModal();
	if (nResponse == IDOK)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with OK
	}
	else if (nResponse == IDCANCEL)
	{
		// TODO: Place code here to handle when the dialog is
		//  dismissed with Cancel
	}

	// Since the dialog has been closed, return FALSE so that we exit the
	//  application, rather than start the application's message pump.
	return FALSE;
}

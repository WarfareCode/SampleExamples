//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
// CAgUiPluginCppAddInEmbeddedWindow.cpp : implementation file
//

#include "AgUiPluginCppAddInStd.h"
#include "AgUiPluginTutorial.h"
#include "AgUiPluginCppAddInEmbeddedWindow.h"
#include "AgUiPluginCppAddInCmds.h"
#include "AgUiPluginUtils.h"

extern BOOL LoadIconFromResource(LPCTSTR lpIconName, IPictureDisp** ppRetVal);

// CAgUiPluginCppAddInEmbeddedWindow dialog

IMPLEMENT_DYNAMIC(CAgUiPluginCppAddInEmbeddedWindow, CDialog)

BEGIN_MESSAGE_MAP(CAgUiPluginCppAddInEmbeddedWindow, CDialog)
	ON_WM_SIZE()
	ON_COMMAND(ID_POPUPMENU_CLEAREVENTLOG, OnPopupmenuClearEventLog)
	ON_NOTIFY(NM_RCLICK, IDC_OBJECTMODEL_EVENT_VIEWER, OnNMRclickObjectmodelEventViewer)
END_MESSAGE_MAP()


BEGIN_INTERFACE_MAP(CAgUiPluginCppAddInEmbeddedWindow, CCmdTarget)
	INTERFACE_PART(CAgUiPluginCppAddInEmbeddedWindow, __uuidof(IAgUiPluginEmbeddedWindowHandle), EmbeddedPlugin)
	INTERFACE_PART(CAgUiPluginCppAddInEmbeddedWindow, __uuidof(IEventLogger), EventLogger)
	INTERFACE_PART(CAgUiPluginCppAddInEmbeddedWindow, __uuidof(IAgUiPluginEmbeddedControl), ExtUIObject)
END_INTERFACE_MAP()

CAgUiPluginCppAddInEmbeddedWindow::CAgUiPluginCppAddInEmbeddedWindow()
: CDialog(CAgUiPluginCppAddInEmbeddedWindow::IDD), m_dwCookie(0), m_pSink(0)
{
}

void CAgUiPluginCppAddInEmbeddedWindow::OnPopupmenuClearEventLog()
{
	m_eventLV.DeleteAllItems();
}

void CAgUiPluginCppAddInEmbeddedWindow::OnNMRclickObjectmodelEventViewer(NMHDR *pNMHDR, LRESULT *pResult)
{
	NMITEMACTIVATE *pHdr = (NMITEMACTIVATE*)pNMHDR;
	CMenu popupMenu;
	popupMenu.LoadMenu(IDR_TREEVIEW_POPUPMENU);
	CPoint pt;
	pt.x = pHdr->ptAction.x;
	pt.y = pHdr->ptAction.y;
	ClientToScreen(&pt);
	popupMenu.GetSubMenu(0)->TrackPopupMenu(TPM_LEFTALIGN|TPM_LEFTBUTTON,
		pt.x, pt.y, this, 0);
	*pResult = 1;
}

CAgUiPluginCppAddInEmbeddedWindow::~CAgUiPluginCppAddInEmbeddedWindow()
{
	//-------------------------------------------
	// Disconnect from the STK Object Model events
	//-------------------------------------------
	UnadviseAutomationEvents();
	delete m_pSink;
}

void CAgUiPluginCppAddInEmbeddedWindow::PostNcDestroy()
{
	// Destroy the instance when the reference count has reached zero.
	if (m_dwRef <= 0)
		delete this;
}

HRESULT CAgUiPluginCppAddInEmbeddedWindow::GetSTKObjectRoot(IDispatch** ppRetVal)
{
	USES_CONVERSION;

	CComPtr<IAgUiApplication> pDisp;
	//-------------------------------------------
	// get_Application returns a pointer to the 
	// AGI Application Object Model. 
	//-------------------------------------------
	HRESULT hr = E_FAIL;
	if (m_pSite && SUCCEEDED(hr = m_pSite->get_Application(&pDisp)))
	{
		if (FAILED(hr = AgGetStkObjectRoot(CComQIPtr<IDispatch>(pDisp), ppRetVal)))
		{
			CString strMsg;
			strMsg = "Cannot retrieve a root object of the STK Object Model.";
			m_pSite->LogMessage(eUiPluginLogMsgDebug, CComBSTR((LPCTSTR)strMsg));
		}
	}
	return hr;
}

BOOL CAgUiPluginCppAddInEmbeddedWindow::OnInitDialog()
{
	if (!__super::OnInitDialog())
		return FALSE;

	m_eventLV.SetExtendedStyle(m_eventLV.GetExtendedStyle()|LVS_EX_FULLROWSELECT);
	m_eventLV.InsertColumn(0, _T("Time"), LVCFMT_LEFT, 120);
	m_eventLV.InsertColumn(1, _T("Event Name"), LVCFMT_LEFT, 200, 1);
	m_eventLV.InsertColumn(2, _T("Description"), LVCFMT_LEFT, 300, 2);

	//-------------------------------------------
	// Connect to the STK Object Model events
	//-------------------------------------------
	CComQIPtr<IEventLogger> pEventLogger (GetInterface(&__uuidof(IEventLogger)));
	m_pSink = new CObjectModelEventSink(pEventLogger);
	AdviseAutomationEvents();

	//-------------------------------------------
	// Didnt find the object model so lets 
	// disable the tree control.
	//-------------------------------------------
	if (!m_pRootDisp)
	{
		m_eventLV.EnableWindow(FALSE);
	}

	return TRUE;
}

void CAgUiPluginCppAddInEmbeddedWindow::UnadviseAutomationEvents()
{
	LPUNKNOWN pUnkSink = m_pSink->GetIDispatch(FALSE);
	if (m_pRootDisp)
	{
		// Terminate a connection between source and sink.
		// m_dwCookied is a value obtained through AfxConnectionAdvise.
		AfxConnectionUnadvise(m_pRootDisp, __uuidof(IAgStkObjectRootEvents), 
			pUnkSink, FALSE, m_dwCookie);
		// Release the object model's root object.
		m_pRootDisp.Release();
	}
}

void CAgUiPluginCppAddInEmbeddedWindow::AdviseAutomationEvents()
{
	if (SUCCEEDED(GetSTKObjectRoot(&m_pRootDisp)))
	{
		CComQIPtr<IAgStkObjectRoot> pRoot (m_pRootDisp);
		if (pRoot)
		{
			// Get a pointer to the sink's IUnknown without AddRef'ing it. CObjectModelEventSink
			// implements only dispinterface, so the IUnknown and IDispatch pointers will be the same.
			LPUNKNOWN pUnkSink = m_pSink->GetIDispatch(FALSE);
			// Establish a connection between source and sink.
			// m_dwCookie is a cookie identifying the connection, and is needed
			// to terminate the connection.
			AfxConnectionAdvise(m_pRootDisp, __uuidof(IAgStkObjectRootEvents), 
				pUnkSink, FALSE, &m_dwCookie);
		}
		else
		{
			CString strMsg;
			strMsg = "Cannot get the STK Object Model root object.";
			m_pSite->LogMessage(eUiPluginLogMsgDebug, CComBSTR((LPCTSTR)strMsg));
		}
	}

}

BOOL CAgUiPluginCppAddInEmbeddedWindow::Create(IAgUiPluginSite* pSite)
{
	m_pSite.Release();
	m_pSite = pSite;
	// Create a modeless dialog using a given resource ID
	return CDialog::Create(CAgUiPluginCppAddInEmbeddedWindow::IDD);
}

void CAgUiPluginCppAddInEmbeddedWindow::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_OBJECTMODEL_EVENT_VIEWER, m_eventLV);
}


void CAgUiPluginCppAddInEmbeddedWindow::OnSize(UINT nType, int cx, int cy)
{
	__super::OnSize(nType, cx, cy);

	CWnd* pWnd = GetDlgItem(IDC_OBJECTMODEL_EVENT_VIEWER);
	if (pWnd != NULL)
	{
		pWnd->MoveWindow(0, 0, cx, cy);
	}
}
// CAgUiPluginCppAddInEmbeddedWindow message handlers
STDMETHODIMP CAgUiPluginCppAddInEmbeddedWindow::XEmbeddedPlugin::get_HWND(OLE_HANDLE* pRetVal)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, EmbeddedPlugin)

	if (pRetVal == NULL)
		return E_POINTER;

	// Unsafe type cast! 
	*pRetVal = (OLE_HANDLE)(INT_PTR)pThis->GetSafeHwnd();

	return S_OK;
}
STDMETHODIMP CAgUiPluginCppAddInEmbeddedWindow::XEmbeddedPlugin::Apply(void)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, EmbeddedPlugin)
	
	return S_OK;
}

STDMETHODIMP_(ULONG) CAgUiPluginCppAddInEmbeddedWindow::XEmbeddedPlugin::AddRef()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, EmbeddedPlugin)
	return pThis->ExternalAddRef();
}

STDMETHODIMP_(ULONG) CAgUiPluginCppAddInEmbeddedWindow::XEmbeddedPlugin::Release()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, EmbeddedPlugin)
	return pThis->ExternalRelease();
}

STDMETHODIMP CAgUiPluginCppAddInEmbeddedWindow::XEmbeddedPlugin::QueryInterface(
          REFIID iid, LPVOID far* ppvObj)     
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, EmbeddedPlugin)
	return pThis->ExternalQueryInterface(&iid, ppvObj);
}
////////////////////////////////////////////////////////////////////////////////
// IEventLogger
STDMETHODIMP CAgUiPluginCppAddInEmbeddedWindow::XEventLogger::LogEvent(const CString& rEvent, const CString& rString)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, EventLogger)
	TCHAR buf[100];
	time_t t = time(0);
	struct tm* time = localtime(&t);
	_tcsftime(buf, sizeof(buf), _T("%#c"), time);
	// Check if the window handle has been destroyed but the object still is lingering around
	if (::IsWindow(pThis->GetSafeHwnd()))
	{
		int iPos = pThis->m_eventLV.InsertItem(pThis->m_eventLV.GetItemCount(), buf);
		pThis->m_eventLV.SetItem(iPos, 1, LVIF_TEXT, rEvent, 0, 0, 0, 0);
		pThis->m_eventLV.SetItem(iPos, 2, LVIF_TEXT, rString, 0, 0, 0, 0);
		pThis->m_eventLV.EnsureVisible(iPos, FALSE);
	}
	return S_OK;
}
STDMETHODIMP_(ULONG) CAgUiPluginCppAddInEmbeddedWindow::XEventLogger::AddRef()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, EventLogger)
	return pThis->ExternalAddRef();
}

STDMETHODIMP_(ULONG) CAgUiPluginCppAddInEmbeddedWindow::XEventLogger::Release()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, EventLogger)
	return pThis->ExternalRelease();
}

STDMETHODIMP CAgUiPluginCppAddInEmbeddedWindow::XEventLogger::QueryInterface(
          REFIID iid, LPVOID far* ppvObj)     
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, EventLogger)
	return pThis->ExternalQueryInterface(&iid, ppvObj);
}
STDMETHODIMP CAgUiPluginCppAddInEmbeddedWindow::XExtUIObject::SetSite(IAgUiPluginEmbeddedControlSite* pSite)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, ExtUIObject)
	pThis->m_pContainerSite.Release();
	pThis->m_pContainerSite = pSite;

	if (pThis->m_pContainerSite)
	{
		pThis->m_pContainerSite->SetModifiedFlag(VARIANT_TRUE);
	}

	return S_OK;
}

STDMETHODIMP CAgUiPluginCppAddInEmbeddedWindow::XExtUIObject::OnClosing()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());

	return S_OK;
}

STDMETHODIMP CAgUiPluginCppAddInEmbeddedWindow::XExtUIObject::OnSaveModified()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, ExtUIObject)

	return S_OK;
}
STDMETHODIMP CAgUiPluginCppAddInEmbeddedWindow::XExtUIObject::GetIcon(/*[out,retval]*/IPictureDisp** ppRetVal)
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	
	if(ppRetVal)
	{
		// Uncomment to associate a custom icon with the window
		//return LoadIconFromResource(MAKEINTRESOURCE(IDI_HOURGLASS_ICON), ppRetVal);
		*ppRetVal = NULL;
		return S_OK;
	}
	return S_OK;
}

STDMETHODIMP_(ULONG) CAgUiPluginCppAddInEmbeddedWindow::XExtUIObject::AddRef()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, ExtUIObject)
	return pThis->ExternalAddRef();
}

STDMETHODIMP_(ULONG) CAgUiPluginCppAddInEmbeddedWindow::XExtUIObject::Release()
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, ExtUIObject)
	return pThis->ExternalRelease();
}

STDMETHODIMP CAgUiPluginCppAddInEmbeddedWindow::XExtUIObject::QueryInterface(
          REFIID iid, LPVOID far* ppvObj)     
{
	AFX_MANAGE_STATE(AfxGetStaticModuleState());
	METHOD_PROLOGUE_EX_(CAgUiPluginCppAddInEmbeddedWindow, ExtUIObject)
	return pThis->ExternalQueryInterface(&iid, ppvObj);
}

//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
// AgUiPluginCppAddIn.cpp : Implementation of CAgUiPluginCppAddIn

#include "AgUiPluginCppAddInStd.h"
#include "AgUiPluginTutorial.h"
#include "AgUiPluginCppAddIn.h"
#include "AgUiPluginCppAddInEmbeddedWindow.h"
#include "AgUiPluginCppAddInCmds.h"
#include "AgUiPluginCppAddInConfigPage.h"
#include "AgUiPluginUtils.h"


//////////////////////////////////////////////////////////////////////////////
// CAgUiPluginCppAddIn

CAgUiPluginCppAddIn::CAgUiPluginCppAddIn()
{
}

CAgUiPluginCppAddIn::~CAgUiPluginCppAddIn()
{
}

STDMETHODIMP CAgUiPluginCppAddIn::InterfaceSupportsErrorInfo(REFIID riid)
{
	static const IID* arr[] = 
	{
		&IID_IDispatch,
		&IID_IAgUiPluginCppAddIn
	};

	for (size_t i=0; i < sizeof(arr) / sizeof(arr[0]); i++)
	{
		if (InlineIsEqualGUID(*arr[i],riid))
			return S_OK;
	}
	return S_FALSE;
}

//--------------------------------
// IAgUiPlugin
//--------------------------------
STDMETHODIMP CAgUiPluginCppAddIn::OnStartup(IAgUiPluginSite* pSite)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

	//////////////////////////////////////////////////////
	// OnStart method can be called more than once during
	// the session. The method is invoked by the 
	// hosting environment each time the Ui plugin is
	// enabled by the user. 
	//////////////////////////////////////////////////////

	m_pSite.Release();
	m_pSite = pSite;

	return S_OK;
}
STDMETHODIMP CAgUiPluginCppAddIn::OnShutdown()
{
	// Intentionally left empty
	return S_OK;
}
//--------------------------------
// IAgUiPluginLicense
//--------------------------------
STDMETHODIMP CAgUiPluginCppAddIn::IsLicensed(VARIANT_BOOL* pRetVal)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

	if (!pRetVal)
		return E_POINTER;

	*pRetVal = VARIANT_FALSE;
	return S_OK;
}

//--------------------------------
// IAgUiPluginCommandTarget
//--------------------------------
STDMETHODIMP CAgUiPluginCppAddIn::QueryState(BSTR commandName, AgEUiPluginCommandState *pState)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

	USES_CONVERSION;

	if (!commandName)
		return E_POINTER;
	if (!pState)
		return E_POINTER;

	CComBSTR bstrName(commandName);
	if (bstrName == AgCExtCommand_Info ||
		bstrName == AgCExtCommand_ToolWindow)
	{
		*pState = (AgEUiPluginCommandState)(eUiPluginCommandStateEnabled|eUiPluginCommandStateSupported);
	}
	else
	{
		*pState = eUiPluginCommandStateNone;
	}
	return S_OK;
}
STDMETHODIMP CAgUiPluginCppAddIn::Exec(BSTR commandName, IAgProgressTrackCancel* pRawTrack, IAgUiPluginCommandParameters *pParams)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

	if (!commandName)
		return E_POINTER;
	if (!pRawTrack)
		return E_POINTER;

	USES_CONVERSION;

	HRESULT hr = S_OK;
	CComBSTR bstrName(commandName);

	if (bstrName == AgCExtCommand_Info)
	{
		OLE_HANDLE hWnd = NULL;
		m_pSite->get_MainWindow(&hWnd);
		::MessageBox((HWND)(INT_PTR)hWnd, _T("AGI Ui Plugin Example, v1.0"), _T("Info"),MB_OK|MB_ICONEXCLAMATION);
	}
	else if (bstrName == AgCExtCommand_ToolWindow)
	{
		//-----------------------------------------------------------
		// Check if the site supports creating custom tool windows.
		//-----------------------------------------------------------
		CComQIPtr<IAgUiPluginWindowSite> pWs(m_pSite);
		if (pWs != 0)
		{
			//-----------------------------------------------------------
			// Create a IAgUiEmbeddedWindowHandle-derived class
			//-----------------------------------------------------------
			CAgUiPluginCppAddInEmbeddedWindow* pWnd = new CAgUiPluginCppAddInEmbeddedWindow();
			//-----------------------------------------------------------
			// Create a window handle 
			//-----------------------------------------------------------
			if (!pWnd->Create(m_pSite))
			{
				m_pSite->LogMessage(eUiPluginLogMsgAlarm, CComBSTR("Cannot create an embedded window."));
			}
			ASSERT(::IsWindow(pWnd->GetSafeHwnd()));

			if (::IsWindow(pWnd->GetSafeHwnd()))
			{
				CComPtr<IUnknown> pUnk;
				//-----------------------------------------------------------
				// This is a tricky part: here we are making sure that the instance
				// of CAgUiPluginCppAddInEmbeddedWindow is freed from memory as soon
				// as the reference count reaches zero. 
				//-----------------------------------------------------------
				pUnk.Attach (((CCmdTarget*)pWnd)->GetInterface(&IID_IUnknown));
				//-----------------------------------------------------------
				// use the static_cast to disambiquate the conversion from 
				// CAgUiPluginCppAddIn to IUnknown. CreateToolWindow 
				// creates a tool window that hosts the given custom control.
				//-----------------------------------------------------------
				CComPtr<IAgUiPluginWindowCreateParameters> pParams;
				CComPtr<IAgUiWindow> pWindow;
				pWs->CreateParameters(&pParams);
				pParams->put_Caption(CComBSTR("STK Object Model Automation Event Log"));
				pParams->put_AllowMultiple(VARIANT_FALSE);
				pParams->put_DockStyle(eDockStyleDockedBottom);
				hr = pWs->CreateToolWindowParam(static_cast<IAgUiPlugin*>(this), 
					pUnk, pParams, &pWindow);
			}
			else
			{
				//-----------------------------------------------------------
				// Failed to create a custom control
				//-----------------------------------------------------------
				m_pSite->LogMessage(eUiPluginLogMsgInfo, CComBSTR("Failed to create a custom control."));
				hr = E_FAIL;
			}
		}
		else
		{
			//-----------------------------------------------------------
			// The site does not support custom tool windows
			//-----------------------------------------------------------
			m_pSite->LogMessage(eUiPluginLogMsgInfo, CComBSTR("The site does not support custom tool windows."));
		}
	}
	return hr;
}

BOOL LoadIconFromResource(LPCTSTR lpIconName, IPictureDisp** ppRetVal)
{
	HICON hIcon = ::LoadIcon(_AtlBaseModule.GetResourceInstance(), lpIconName);
	ATLASSERT(hIcon != NULL);

	PICTDESC pictdesc;
	pictdesc.cbSizeofstruct = sizeof(PICTDESC);
	pictdesc.picType = PICTYPE_ICON;
	pictdesc.icon.hicon = hIcon;

	// Note that the instance of IPictureDisp is responsible
	// for cleaning up the associated GDI resource.
	return SUCCEEDED(OleCreatePictureIndirect(&pictdesc, 
		IID_IPictureDisp, TRUE, (LPVOID*)ppRetVal));
}

STDMETHODIMP CAgUiPluginCppAddIn::OnInitializeToolbar(/*[in]*/ IAgUiPluginToolbarBuilder* pToolBarBuilder)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

	CComPtr<IPictureDisp> pPicture;
	LoadIconFromResource(MAKEINTRESOURCE(IDI_HOURGLASS_ICON), &pPicture);
	pToolBarBuilder->AddButton(
		CComBSTR(AgCExtCommand_Info), 
		L"Demonstrates an icon button", 
		L"Click to display a dialog box",
		eToolBarButtonOptionNone, pPicture);

	pPicture.Release();
	LoadIconFromResource(MAKEINTRESOURCE(IDI_CUSTOM_TOOL_WINDOW), &pPicture);
	pToolBarBuilder->AddButton( 
		CComBSTR(AgCExtCommand_ToolWindow), 
		L"Displays a tool window hosting a user-defined control", 
		L"Click to display a tool window hosting a user-defined control provided by the Ui plugin",
		eToolBarButtonOptionNone, pPicture);

	return S_OK;
}
STDMETHODIMP CAgUiPluginCppAddIn::OnDisplayContextMenu(/*[in]*/ IAgUiPluginMenuBuilder* pMenuBuilder)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

	CComPtr<IPictureDisp> pPicture;
	LoadIconFromResource(MAKEINTRESOURCE(IDI_HOURGLASS_ICON), &pPicture);
	pMenuBuilder->AddMenuItem(
		CComBSTR(AgCExtCommand_Info), 
		L"Demonstrates an icon button", 
		L"Click to display a dialog box",
		pPicture);

	pPicture.Release();
	LoadIconFromResource(MAKEINTRESOURCE(IDI_CUSTOM_TOOL_WINDOW), &pPicture);
	pMenuBuilder->AddMenuItem(
		CComBSTR(AgCExtCommand_ToolWindow), 
		L"Displays a tool window hosting a user-defined control", 
		L"Click to display a tool window hosting a user-defined control provided by the Ui plugin",
		pPicture);

	return S_OK;
}
//--------------------------------
// IAgUiPluginConfigPageBuilderClient
//--------------------------------
STDMETHODIMP CAgUiPluginCppAddIn::OnDisplayConfigurationPage(/*[in]*/ IAgUiPluginConfigurationPageBuilder* pToolsOptions)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());

	if (!pToolsOptions)
		return E_POINTER;

	CAgUiPluginCppAddInConfigPage* pWnd = new CAgUiPluginCppAddInConfigPage();
	//-----------------------------------------------------------
	// Create a window handle 
	//-----------------------------------------------------------
	if (!pWnd->Create(m_pSite))
	{
		m_pSite->LogMessage(eUiPluginLogMsgAlarm, CComBSTR("Cannot create an embedded window."));
	}
	ASSERT(::IsWindow(pWnd->GetSafeHwnd()));

	if (::IsWindow(pWnd->GetSafeHwnd()))
	{
		return pToolsOptions->AddCustomEmbeddedControlPage(
			static_cast<IAgUiPlugin*>(this),
			((CCmdTarget*)pWnd)->GetInterface(&IID_IUnknown),
			CComBSTR("Example SCC Settings"));
	}
	return E_FAIL;
}
//--------------------------------
// IAgUiPlugin2
//--------------------------------
STDMETHODIMP CAgUiPluginCppAddIn::OnDisplayMenu(
	BSTR MenuTitle, 
	AgEUiPluginMenuBarKind eMenuKind, 
	IAgUiPluginMenuBuilder2* pBuilder)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());
	USES_CONVERSION;

	CString strMenuTitle = OLE2T(MenuTitle);
	if (strMenuTitle == "Window")
	{
		CComPtr<IPictureDisp> pPicture;
		LoadIconFromResource(MAKEINTRESOURCE(IDI_HOURGLASS_ICON), &pPicture);
		pBuilder->InsertMenuItem1(2, 
			CComBSTR(AgCExtCommand_Info), 
			L"Demonstrates an icon button", 
			L"Click to display a dialog box",
			pPicture);
	}
	return S_OK;
}

//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
// AgUiPluginTutorial.cpp : Implementation of DLL Exports.

#include "AgUiPluginCppAddInStd.h"
#include "resource.h"
#include "AgUiPluginTutorial.h"
#include "AgUiPluginTutorial_i.c"

class CAgUiPluginTutorialModule : public CAtlDllModuleT< CAgUiPluginTutorialModule >
{
public :
	DECLARE_LIBID(LIBID_AgUiPluginTutorialLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_AGUIPLUGINTUTORIAL, "{3EDDBAFC-01AB-40DF-A5AB-0881BB1F02A0}")
};

CAgUiPluginTutorialModule _AtlModule;

class CAgUiPluginTutorialApp : public CWinApp
{
public:

// Overrides
    virtual BOOL InitInstance();
    virtual int ExitInstance();

    DECLARE_MESSAGE_MAP()
};

BEGIN_MESSAGE_MAP(CAgUiPluginTutorialApp, CWinApp)
END_MESSAGE_MAP()

CAgUiPluginTutorialApp theApp;

BOOL CAgUiPluginTutorialApp::InitInstance()
{
	AfxEnableControlContainer();
    return CWinApp::InitInstance();
}

int CAgUiPluginTutorialApp::ExitInstance()
{
    return CWinApp::ExitInstance();
}


// Used to determine whether the DLL can be unloaded by OLE
STDAPI DllCanUnloadNow(void)
{
    AFX_MANAGE_STATE(AfxGetStaticModuleState());
    return (AfxDllCanUnloadNow()==S_OK && _AtlModule.GetLockCount()==0) ? S_OK : S_FALSE;
}


// Returns a class factory to create an object of the requested type
STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
    return _AtlModule.DllGetClassObject(rclsid, riid, ppv);
}


// DllRegisterServer - Adds entries to the system registry
STDAPI DllRegisterServer(void)
{
    // registers object, typelib and all interfaces in typelib
    HRESULT hr = _AtlModule.DllRegisterServer();
	return hr;
}


// DllUnregisterServer - Removes entries from the system registry
STDAPI DllUnregisterServer(void)
{
	HRESULT hr = _AtlModule.DllUnregisterServer();
	return hr;
}

#include "stdafx.h"
#include "Resource.h"
#include "MYPLUGIN.h"

class CMYPLUGINModule : 
public CAtlDllModuleT< CMYPLUGINModule >
{
	public :
		DECLARE_LIBID(LIBID_MYPLUGINLib)
		DECLARE_REGISTRY_APPID_RESOURCEID(IDR_MYPLUGINDLL, "{BEF232A0-0514-41C7-BC6F-422E2D525B25}")
};

CMYPLUGINModule _AtlModule;

extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
	BOOL br = FALSE;

	br = _AtlModule.DllMain(dwReason, lpReserved); 
    
	return br;
}

STDAPI DllCanUnloadNow(void)
{
	HRESULT hr = E_FAIL;

	hr = _AtlModule.DllCanUnloadNow();

	return hr;
}

STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
	HRESULT hr = E_FAIL;

	hr = _AtlModule.DllGetClassObject(rclsid, riid, ppv);

	return hr;
}

STDAPI DllRegisterServer(void)
{
	HRESULT hr = E_FAIL;
	
	hr = _AtlModule.DllRegisterServer();

	return hr;
}

STDAPI DllUnregisterServer(void)
{
	HRESULT hr = E_FAIL;
	
	hr = _AtlModule.DllUnregisterServer();

	return hr;
}


/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"
#include "resource.h"
#include "Agi.As.Hpop.Plugins.CPP.Examples.h"
#include "AgStkObjects.tli"
#include "AgStkUtil.tli"

class CAgiAsHpopPluginsCPPExamplesModule : 
public CAtlDllModuleT< CAgiAsHpopPluginsCPPExamplesModule >
{
	public :
		DECLARE_LIBID(LIBID_AgiAsHpopPluginsCPPExamplesLib)
		DECLARE_REGISTRY_APPID_RESOURCEID(IDR_AGIASHPOPPLUGINSCPPEXAMPLES, "{FC4A5C80-B3DC-4850-90CA-67B319BC6D99}")
};

CAgiAsHpopPluginsCPPExamplesModule _AtlModule;

extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
	//hInstance;
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Examples.DllMain(): --> Entered\n");
	#endif

	BOOL br = FALSE;

	br = _AtlModule.DllMain(dwReason, lpReserved); 

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Examples.DllMain(): <-- Exited\n");
	#endif
    
	return br;
}

STDAPI DllCanUnloadNow(void)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Examples.DllCanUnloadNow(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;

	hr = _AtlModule.DllCanUnloadNow();

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Examples.DllCanUnloadNow(): <-- Exited\n");
	#endif

	return hr;
}

STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Examples.DllGetClassObject(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;

	hr = _AtlModule.DllGetClassObject(rclsid, riid, ppv);

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Examples.DllGetClassObject(): <-- Exited\n");
	#endif

	return hr;
}

STDAPI DllRegisterServer(void)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Examples.DllRegisterServer(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;
	
	hr = _AtlModule.DllRegisterServer();

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Examples.DllRegisterServer(): <-- Exited\n");
	#endif

	return hr;
}

STDAPI DllUnregisterServer(void)
{
	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Examples.DllUnregisterServer(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;
	
	hr = _AtlModule.DllUnregisterServer();

	#ifdef _HPOP_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("CPP.Examples.DllUnregisterServer(): <-- Exited\n");
	#endif

	return hr;
}

/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
#include "stdafx.h"
#include "resource.h"
#include "Agi.As.Gator.AttCtrl.Plugin.Cpp.Examples.h"

class CAgiAsGatorAttCtrlPluginCppExamplesModule : public CAtlDllModuleT< CAgiAsGatorAttCtrlPluginCppExamplesModule >
{
	public :
		DECLARE_LIBID(LIBID_AgiAsGatorAttCtrlPluginCppExamplesLib)
		DECLARE_REGISTRY_APPID_RESOURCEID(IDR_AGIASGATORATTCTRLPLUGINCPPEXAMPLES, "{1229FE59-92C3-420A-B9D0-7D7137686664}")
};

CAgiAsGatorAttCtrlPluginCppExamplesModule _AtlModule;

extern "C" BOOL WINAPI DllMain(HINSTANCE hInstance, DWORD dwReason, LPVOID lpReserved)
{
	//hInstance;
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.DllMain(): --> Entered\n");
	#endif

	BOOL br = FALSE;

	br = _AtlModule.DllMain(dwReason, lpReserved); 

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.DllMain(): <-- Exited\n");
	#endif
    
	return br;
}

STDAPI DllCanUnloadNow(void)
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.DllCanUnloadNow(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;

	hr = _AtlModule.DllCanUnloadNow();

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.DllCanUnloadNow(): <-- Exited\n");
	#endif

	return hr;
}

STDAPI DllGetClassObject(REFCLSID rclsid, REFIID riid, LPVOID* ppv)
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.DllGetClassObject(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;

	hr = _AtlModule.DllGetClassObject(rclsid, riid, ppv);

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.DllGetClassObject(): <-- Exited\n");
	#endif

	return hr;
}

STDAPI DllRegisterServer(void)
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.DllRegisterServer(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;
	
	hr = _AtlModule.DllRegisterServer();

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.DllRegisterServer(): <-- Exited\n");
	#endif

	return hr;
}

STDAPI DllUnregisterServer(void)
{
	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.DllUnregisterServer(): --> Entered\n");
	#endif

	HRESULT hr = E_FAIL;
	
	hr = _AtlModule.DllUnregisterServer();

	#ifdef _GATOR_PLUGIN_CONSOLE_LOGGING
	OutputDebugString("Gator.AttCtrl.CPP.Example1.DllUnregisterServer(): <-- Exited\n");
	#endif

	return hr;
}
/**********************************************************************/
/*           Copyright 2005, Analytical Graphics, Inc.                */
/**********************************************************************/
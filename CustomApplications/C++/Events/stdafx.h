// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently,
// but are changed infrequently

#pragma once

#ifndef VC_EXTRALEAN
#define VC_EXTRALEAN		// Exclude rarely-used stuff from Windows headers
#endif

// Modify the following defines if you have to target a platform prior to the ones specified below.
// Refer to MSDN for the latest info on corresponding values for different platforms.
#ifndef WINVER				// Allow use of features specific to Windows 7 or later.
#define WINVER 0x0601		// Change this to the appropriate value to target other Windows versions.
#endif

#ifndef _WIN32_WINNT		// Allow use of features specific to Windows 7 or later.
#define _WIN32_WINNT 0x0601		// Change this to the appropriate value to target other Windows versions.
#endif						

#ifndef _WIN32_WINDOWS		// Allow use of features specific to Windows 7 or later.
#define _WIN32_WINDOWS 0x0601 // Change this to the appropriate value to target other Windows versions.
#endif

#ifndef _WIN32_IE			// Allow use of features specific to IE 8.0 or later.
#define _WIN32_IE 0x0800	// Change this to the appropriate value to target other IE versions.
#endif

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS	// some CString constructors will be explicit

// turns off MFC's hiding of some common and often safely ignored warning messages
#define _AFX_ALL_WARNINGS

#include <afxwin.h>         // MFC core and standard components
#include <afxext.h>         // MFC extensions
#include <afxdisp.h>        // MFC Automation classes
#include <Afxctl.h>

#include <afxdtctl.h>		// MFC support for Internet Explorer 4 Common Controls
#ifndef _AFX_NO_AFXCMN_SUPPORT
#include <afxcmn.h>			// MFC support for Windows Common Controls
#endif // _AFX_NO_AFXCMN_SUPPORT

#include "AgStkUtil.tlh"
using namespace STKUtil;
#include "AgVGT.tlh"
#include "AgStkGraphics.tlh"
#include "AgStkObjects.tlh"
using namespace STKObjects;
#include "STKX.tlh"
using namespace STKXLib;
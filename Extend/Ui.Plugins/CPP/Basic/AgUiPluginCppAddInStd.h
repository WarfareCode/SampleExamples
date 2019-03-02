//
// This is a part of the STK Developer Kit.
// Copyright (c) Analytical Graphics, Inc.  All rights reserved.
//
// AgUiPluginCppAddInStd.h : include file for standard system include files,
// or project specific include files that are used frequently,
// but are changed infrequently

#pragma once

#ifndef STRICT
#define STRICT
#endif

#ifndef _WIN32_IE			// Allow use of features specific to IE 4.0 or later.
#define _WIN32_IE 0x0600	// Change this to the appropriate value to target IE 6.0 or later.
#endif

#define _ATL_APARTMENT_THREADED
#define _ATL_NO_AUTOMATIC_NAMESPACE

#define _ATL_CSTRING_EXPLICIT_CONSTRUCTORS	// some CString constructors will be explicit

// turns off ATL's hiding of some common and often safely ignored warning messages
#define _ATL_ALL_WARNINGS

#include <afxwin.h>
#include <afxdisp.h>
#include <afxdlgs.h>
#include <afxext.h>
#include <Afxctl.h>
#include <afxcmn.h>

#include "resource.h"
#include <atlbase.h>
#include <atlcom.h>
#include <atlsafe.h>

using namespace ATL;

#include <map>


#include "AgUiPlugins.tlh"
#include "AgUiCore.tlh"
#include "AgUiApplication.tlh"
#include "AgUtPlugin.tlh"
#include "AgAttrAutomation.tlh"
#include "AgStkUtil.tlh"
#include "AgVGT.tlh"
#include "AgStkGraphics.tlh"
#include "AgStkObjects.tlh"
using namespace STKObjects;


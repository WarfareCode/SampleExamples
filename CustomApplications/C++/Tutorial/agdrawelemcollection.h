#if !defined(AFX_AGDRAWELEMCOLLECTION_H__6D13C1BE_F8F6_4FFD_8ACE_09EA90619C77__INCLUDED_)
#define AFX_AGDRAWELEMCOLLECTION_H__6D13C1BE_F8F6_4FFD_8ACE_09EA90619C77__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// Machine generated IDispatch wrapper class(es) created by Microsoft Visual C++

// NOTE: Do not modify the contents of this file.  If this class is regenerated by
//  Microsoft Visual C++, your modifications will be overwritten.


// Dispatch interfaces referenced by this interface
class CAgDrawElem;

/////////////////////////////////////////////////////////////////////////////
// CAgDrawElemCollection wrapper class

class CAgDrawElemCollection : public COleDispatchDriver
{
public:
	CAgDrawElemCollection() {}		// Calls COleDispatchDriver default constructor
	CAgDrawElemCollection(LPDISPATCH pDispatch) : COleDispatchDriver(pDispatch) {}
	CAgDrawElemCollection(const CAgDrawElemCollection& dispatchSrc) : COleDispatchDriver(dispatchSrc) {}

// Attributes
public:

// Operations
public:
	long GetCount();
	CAgDrawElem GetItem(long Index);
	LPUNKNOWN Get_NewEnum();
	void Clear();
	CAgDrawElem Add(LPCTSTR ElemType);
	void Remove(LPDISPATCH DrawElem);
	BOOL GetVisible();
	void SetVisible(BOOL bNewValue);
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_AGDRAWELEMCOLLECTION_H__6D13C1BE_F8F6_4FFD_8ACE_09EA90619C77__INCLUDED_)

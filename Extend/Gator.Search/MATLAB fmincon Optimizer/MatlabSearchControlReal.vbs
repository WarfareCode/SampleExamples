'======================================================
'  Copyright 2007, Analytical Graphics, Inc. 
'
'  Author: Patrick Williams and Jonathan Lowe     
' =====================================================

'===========================================
' AgEAttrAddFlags Enumeration
'===========================================
Dim eFlagNone, eFlagTransparent, eFlagHidden, eFlagTransient, eFlagReadOnly, eFlagFixed

eFlagNone			= 0
eFlagTransparent	= 2
eFlagHidden			= 4
eFlagTransient		= 8  
eFlagReadOnly		= 16
eFlagFixed			= 32

'==================================
' Log Msg Type Enumeration
'==================================
Dim eLogMsgDebug, eLogMsgInfo, eLogMsgForceInfo, eLogMsgWarning, eLogMsgAlarm

eLogMsgDebug	 	= 0
eLogMsgInfo 		= 1
eLogMsgForceInfo 	= 2
eLogMsgWarning 		= 3
eLogMsgAlarm 		= 4

'================================
' Declare Global Variables
'================================
Dim m_AgAttrScope

Set m_AgAttrScope	 = Nothing

'======================================
' Declare Global 'Attribute' Variables
'======================================
Dim m_objectName
Dim m_controlName
Dim m_currentValue
Dim m_initialValue
Dim m_dimension
Dim m_internalUnit
Dim m_scalingMultiplier
Dim m_type
Dim m_hasUpperBound
Dim m_hasLowerBound
Dim m_upperBound
Dim m_lowerBound
Dim m_active
'dim m_hasEqualityConstraint
'dim m_equalityConstraint

m_objectName = ""
m_controlName = ""
m_currentValue = 0.0
m_initialValue = 0.0
m_scalingMultiplier = 1.0
m_hasUpperBound = false
m_hasLowerBound = false
m_upperBound = 0.0
m_lowerBound = 0.0
m_active = false
'm_hasEqualityConstraint = false
'm_equalityConstraint = 0.0

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( AgAttrBuilder )

	If( m_AgAttrScope is Nothing ) Then
		Set m_AgAttrScope = AgAttrBuilder.NewScope()
		
		'===========================
		' General Plugin attributes
		'===========================
		Call AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "Active", "True if control should be used in optimization", "Active", eFlagNone )
		Call AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "HasLowerBound", "True if control has an lower bound inequality constraint", "HasLowerBound", eFlagNone )
		Call AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "HasUpperBound", "True if control has an upper bound inequality constraint", "HasUpperBound", eFlagNone )
		Call AgAttrBuilder.AddDoubleDispatchProperty( m_AgAttrScope, "ScalingMultiplier", "Multiplicative scale factor", "ScalingMultiplier", eFlagNone )
		'Call AgAttrBuilder.AddBoolDispatchProperty( m_AgAttrScope, "HasEqualityConstraint", "True if control has an equality constraint", "HasEqualityConstraint", eFlagNone )
		
		' note: m_dimension and m_internalUnits are set by the plugin point before
		' this method is called
		if (m_dimension = "") Then
			' dimensionless
			Call AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "LowerBound", "Value for lower bound", "LowerBound", eFlagNone )
			Call AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "UpperBound", "Value for upper bound", "UpperBound", eFlagNone )
			'Call AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "EqualityConstraint", "Value for equality constraint", "EqualityConstraint", eFlagNone )
		elseif (m_dimension = "DateFormat") then
			' the step is in timeUnits (seconds) if the control is a date
			Call AgAttrBuilder.AddDateDispatchProperty ( m_AgAttrScope, "LowerBound", "Value for lower bound", "LowerBound", eFlagNone )
			Call AgAttrBuilder.AddDateDispatchProperty ( m_AgAttrScope, "UpperBound", "Value for upper bound", "UpperBound", eFlagNone )
			'Call AgAttrBuilder.AddQuantityDispatchProperty ( m_AgAttrScope, "EqualityConstraint", "Value for equality constraint", "EqualityConstraint", "Seconds", "Seconds", eFlagNone )
		else
			' attr quantity, use internal units that were given
			Call AgAttrBuilder.AddQuantityDispatchProperty ( m_AgAttrScope, "LowerBound", "Value for lower bound", "LowerBound", m_internalUnit, m_internalUnit, eFlagNone )
			Call AgAttrBuilder.AddQuantityDispatchProperty ( m_AgAttrScope, "UpperBound", "Value for upper bound", "UpperBound", m_internalUnit, m_internalUnit, eFlagNone )
			'Call AgAttrBuilder.AddQuantityDispatchProperty ( m_AgAttrScope, "EqualityConstraint", "Value for equality constraint", "EqualityConstraint", m_internalUnit, m_internalUnit, eFlagNone )
		End If
		
		Call AgAttrBuilder.AddDependencyDispatchProperty ( m_AgAttrScope, "HasLowerBound", "LowerBound")
		Call AgAttrBuilder.AddDependencyDispatchProperty ( m_AgAttrScope, "HasUpperBound", "UpperBound")
		'Call AgAttrBuilder.AddDependencyDispatchProperty ( m_AgAttrScope, "HasEqualityConstraint", "EqualityConstraint")
		Call AgAttrBuilder.AddFlagsDispatchProperty( m_AgAttrScope, "LowerBound", "LowerBoundFlags")
		Call AgAttrBuilder.AddFlagsDispatchProperty( m_AgAttrScope, "UpperBound", "UpperBoundFlags")
		'Call AgAttrBuilder.AddFlagsDispatchProperty( m_AgAttrScope, "EqualityConstraint", "EqualityConstraintFlags")
		
		
	End If

	Set GetPluginConfig = m_AgAttrScope

End Function  

'===========================
' VerifyPluginConfig method
'===========================
Function VerifyPluginConfig(AgUtPluginConfigVerifyResult)
   
    Dim Result
    Dim Message

	Result = true
	Message = "Ok"

	AgUtPluginConfigVerifyResult.Result  = Result
	AgUtPluginConfigVerifyResult.Message = Message

End Function  

'===========================
' Message method
'===========================
Sub Message( msgType, msg )
   
	If( Not m_AgUtPluginSite is Nothing) then
	   	
		Call m_AgUtPluginSite.Message( msgType, msg )

	End If
   	
End Sub


'=======================
' ObjectName property
'=======================
Function GetObjectName()

       GetObjectName = m_objectName

End Function

Function SetObjectName( val )

       m_objectName = val

End Function

'=======================
' ControlName property
'=======================
Function GetControlName()

       GetControlName = m_controlName

End Function

Function SetControlName( val )

       m_controlName = val

End Function

'=======================
' ControlType property
'=======================
Function GetControlType()

       GetControlType = m_type

End Function

Function SetControlType( val )

       m_type = val

End Function

'======================
' Active Property
'======================
Function GetActive()
    
   GetActive = m_active
    
End Function

Function SetActive( val )

   m_active = val
    
End Function 


'=======================
' CurrentValue property
'=======================
Function GetCurrentValue()

       GetCurrentValue = m_currentValue

End Function

Function SetCurrentValue( val )

       m_currentValue = val

End Function

'=======================
' InitialValue property
'=======================
Function GetInitialValue()

       GetInitialValue = m_initialValue

End Function

Function SetInitialValue( val )

       m_initialValue = val

End Function

'=======================
' Dimension property
'=======================
Function GetDimension()

       GetDimension = m_dimension

End Function

Function SetDimension( val )

	   if (m_dimension <> val) then
			' reset the attr scope so a new one will be made with the new dimension the next time
			' it is needed
			set m_AgAttrScope = Nothing
	   end if

       m_dimension = val

End Function

'=======================
' InternalUnit property
'=======================
Function GetInternalUnit()

       GetInternalUnit = m_internalUnit

End Function

Function SetInternalUnit( val )

       m_internalUnit = val

End Function

'=======================
' ScalingMultiplier property
'=======================
Function GetScalingMultiplier()

       GetScalingMultiplier = m_scalingMultiplier

End Function

Function SetScalingMultiplier( val )

       m_scalingMultiplier = val

End Function

'======================
' HasUpperBound Property
'======================
Function GetHasUpperBound()
    
   GetHasUpperBound = m_hasUpperBound
    
End Function

Function SetHasUpperBound( val )

   m_hasUpperBound = val
    
End Function 

'======================
' HasLowerBound Property
'======================
Function GetHasLowerBound()
    
   GetHasLowerBound = m_hasLowerBound
    
End Function

Function SetHasLowerBound( val )

   m_hasLowerBound = val
    
End Function 

'=======================
' UpperBound property
'=======================
Function GetUpperBound()

   GetUpperBound = m_upperBound

End Function

Function SetUpperBound( val )

   m_upperBound = val

End Function

'=======================
' LowerBound property
'=======================
Function GetLowerBound()

   GetLowerBound = m_lowerBound

End Function

Function SetLowerBound( val )

   m_lowerBound = val

End Function

'=======================
' UpperBoundFlags
'=======================
Function UpperBoundFlags()

   if (m_hasUpperBound) Then
            
        UpperBoundFlags = 0
            
   else
        
        UpperBoundFlags = 4
            
   end if
       
End Function

'=======================
' LowerBoundFlags
'=======================
Function LowerBoundFlags()

    if (m_hasLowerBound) Then
            
        LowerBoundFlags = 0
            
    else
        
        LowerBoundFlags = 4
           
    End If
        
End Function


'======================
' HasEqualityConstraint property
'======================
'Function HasEqualityConstraint()
    
    'HasEqualityConstraint = m_hasEqualityConstraint

'end function

'Function SethasEqualityConstraint( val )

    'm_hasEqualityConstraint = val
    
'End Function

'======================
' EqualityConstraint Property
'======================
'Function GetEqualityConstraint()

    'GetEqualityConstraint = m_equalityConstraint
    
'End Function

'Function SetEqualityConstraint( val )

    'm_equalityConstraint = val
    
'End Function

'======================
' EqualityConstraintFlags
'======================
'Function EqualityConstraintFlags()

    'if (m_hasEqualityConstraint) Then
        
        'EqualityConstraintFlags = 0
        
    'else
    
        'EqualityConstraintFlags = 4
        
    'End If
    
'End Function

    
    



'======================================================
'  Copyright 2006, Analytical Graphics, Inc.          
' =====================================================
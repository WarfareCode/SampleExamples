'======================================================
'  Copyright 2007, Analytical Graphics, Inc.      
'
'  Author: Patrick Williams and Jonathan Lowe    
' =====================================================


Dim m_mFilename
Dim m_MatlabApp
Dim registry


Set m_MatlabApp = nothing
m_mFilename = "Optimize"

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

'==================================
' Control Type Enumeration
'==================================
Dim eSearchControlTypesReal

eSearchControlTypesReal = 0

'================================
' Declare Global Variables
'================================
Dim m_AgUtPluginSite
Dim m_AgAttrScope

Set m_AgUtPluginSite = Nothing
Set m_AgAttrScope	 = Nothing

'======================================
' Declare Global 'Attribute' Variables
'======================================
Dim m_Name
Dim m_maxIters
dim m_costTolerance
dim m_constraintTolerance
dim m_inputTolerance
dim m_maxFunEvals
dim m_diffMaxChange
dim m_diffMinChange

m_Name	= "MatlabSearch.wsc"   
m_maxIters   = 100
m_costTolerance = 0.001
m_constraintTolerance = 0.001
m_inputTolerance = 0.001
m_maxFunEvals = 1000
m_diffMaxChange = 0.1
m_diffMinChange = 0.00000001

'=======================
' GetPluginConfig method
'=======================
Function GetPluginConfig( AgAttrBuilder )

	If( m_AgAttrScope is Nothing ) Then
		Set m_AgAttrScope = AgAttrBuilder.NewScope()
		
		'===========================
		' General Plugin attributes
		'===========================
		Call AgAttrBuilder.AddStringDispatchProperty( m_AgAttrScope, "PluginName", "Human readable plugin name or alias", "Name", eFlagReadOnly )
		Call AgAttrBuilder.AddIntDispatchProperty  ( m_AgAttrScope, "MaxIterations", "Maximum Iterations (MaxIter)", "MaxIterations", 0 )
		Call AgAttrBuilder.AddIntDispatchProperty  ( m_AgAttrScope, "MaxFunctionEvals", "Maximum Function Evaluations (MaxFunEvals)", "MaxFunctionEvals", 0 )
		Call AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "CostTolerance", "Termination Tolerance for the cost function (TolFun)", "CostTolerance", eFlagNone )
		Call AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "ConstraintTolerance", "Termination Tolerance for the constraint (TolCon)", "ConstraintTolerance", eFlagNone )
		Call AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "InputTolerance", "Termination Tolerance for the independant variables (TolX)", "InputTolerance", eFlagNone )
		Call AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "DiffMaxChange", "Maximum change in variables for finite differencing (DiffMaxChange)", "DiffMaxChange", eFlagNone )
		Call AgAttrBuilder.AddDoubleDispatchProperty ( m_AgAttrScope, "DiffMinChange", "Minimum change in variables for finite differencing (DiffMinChange)", "DiffMinChange", eFlagNone )


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

'======================
' Init Method
'======================
Function Init( AgUtPluginSite )

	Set m_AgUtPluginSite = AgUtPluginSite

	dim filepath
	filepath = ""
	
	set m_MatlabApp = GetObject(filepath,"Matlab.Application")
	
	if (m_MatlabApp is Nothing) Then
	    MsgBox "Cannot get handle to Matlab"
	    Init = 0
	else
	    Init = 1
        on error resume next ' in case the next line fails - if Matlab is already open
		m_MatlabApp.Visible = 0
	End If

End Function

'======================
' Run Method
'======================
Function Run( AgSearchOperand, testing )

    Dim Controls, Results
    Set Controls = AgSearchOperand.Controls
    Set Results = AgSearchOperand.Results
    
    Dim resultIndex
    Dim resultActiveCount
    Dim resultMinimizedCount
    Dim controlIndex
    Dim controlActiveCount
	
	resultActiveCount = 0
	resultMinimizedCount = 0
	controlActiveCount = 0

    for resultIndex = 0 to (Results.Count - 1)
        if (Results.Item(resultIndex).IsMinimized) Then
            resultMinimizedCount = resultMinimizedCount + 1
        end If

        if (Results.Item(resultIndex).Active) Then
            resultActiveCount = resultActiveCount + 1
        end If
    next
     
    for controlIndex = 0 to (Controls.Count - 1)
        if (Controls.Item(controlIndex).Active) Then
            controlActiveCount = controlActiveCount + 1
        end If
    next

	if (controlActiveCount = 0 Or resultActiveCount = 0) then
		Call Message(eLogMsgAlarm, "There must be at least one active control and one active result.")
		Run = 0
		exit function
	end If
	
	if (resultMinimizedCount = 0) then
		Call Message(eLogMsgAlarm, "At least one result must be minimized")
		Run = 0
		exit Function
	end If



'	Initialize Status Grid
    Dim numRows
    Dim i_rows
     if (controlActiveCount > resultActiveCount) then
    	numRows = controlActiveCount+3
    else
    	numRows = resultActiveCount+3
    end If

    Call AgSearchOperand.StatusGrid.CreateGrid(numRows+1,7)
    Call AgSearchOperand.StatusGrid.SetHeaderCellString(3,0,"Control")
    Call AgSearchOperand.StatusGrid.SetHeaderCellString(3,1,"Current Value")
    Call AgSearchOperand.StatusGrid.SetHeaderCellString(3,2,"Last Update")
    Call AgSearchOperand.StatusGrid.SetHeaderCellString(3,3,"Result")
    Call AgSearchOperand.StatusGrid.SetHeaderCellString(3,4,"Desired")
    Call AgSearchOperand.StatusGrid.SetHeaderCellString(3,5,"Achieved")    
    Call AgSearchOperand.StatusGrid.SetHeaderCellString(3,6,"Difference")    
    Call AgSearchOperand.StatusGrid.SetHeaderCellString(3,7,"Best")

	Call AgSearchOperand.StatusGrid.SetCellString(0,0,"Iter")
	Call AgSearchOperand.StatusGrid.SetCellString(0,1,"F-count")
	Call AgSearchOperand.StatusGrid.SetCellString(0,2,"f(x)")
	Call AgSearchOperand.StatusGrid.SetCellString(0,3,"Max Const.")
	Call AgSearchOperand.StatusGrid.SetCellString(0,4,"Step Size")
	Call AgSearchOperand.StatusGrid.SetCellString(0,5,"Dir'l Deriv.")
	Call AgSearchOperand.StatusGrid.SetCellString(0,6,"1st Order Opt")

	Call AgSearchOperand.StatusGrid.SetCellString(1,0,"-------")
	Call AgSearchOperand.StatusGrid.SetCellString(1,1,"-------")
	Call AgSearchOperand.StatusGrid.SetCellString(1,2,"-------")
	Call AgSearchOperand.StatusGrid.SetCellString(1,3,"-------")
	Call AgSearchOperand.StatusGrid.SetCellString(1,4,"-------")
	Call AgSearchOperand.StatusGrid.SetCellString(1,5,"-------")
	Call AgSearchOperand.StatusGrid.SetCellString(1,6,"-------")
    
    Call AgSearchOperand.StatusGrid.SetColumnToTruncateLeft(0)
    Call AgSearchOperand.StatusGrid.SetColumnToTruncateLeft(3)
    Call AgSearchOperand.StatusGrid.Refresh()

'	Set initial values within grid before the first run. This logic is replicated within the *.m files where grid updates will happen during the run
    i_rows = 1+3
    
    
    for i = 0 to (Controls.Count -1)  
       	if ( i_rows <= numRows And Controls.Item(i).Active) Then

		'Control Name column
		Call AgSearchOperand.StatusGrid.SetCellString(i_rows,0,Controls(i).ObjectName + " : " + Controls(i).ControlName)
		
		'Current Value column
		Call AgSearchOperand.StatusGrid.SetCellControlValue(i_rows, 1, i, Controls(i).CurrentValue, 6)
		
		'Last Update column
	    Call AgSearchOperand.StatusGrid.SetCellControlDeltaValue(i_rows, 2, i, Controls(i).CurrentValue - Controls(i).InitialValue, 6)
         		
   		i_rows = i_rows + 1	
	end If 
    next
    Call AgSearchOperand.StatusGrid.Refresh()
    i_rows = 1+3
    for i = 0 to (Results.Count-1)  
    	if ( i_rows <= numRows And Results.Item(i).Active) Then
	    	
		    'Result column	    	
        	Call AgSearchOperand.StatusGrid.SetCellString(i_rows,3,results(i).ObjectName + " : " + results(i).ResultName)
            		
        	'Desired column
        	if (Results.Item(i).IsMinimized) Then
           		Call AgSearchOperand.StatusGrid.SetCellString(i_rows,4,"Min")
        			
        	elseIf (Results.Item(i).HasEqualityConstraint) Then
        		Call AgSearchOperand.StatusGrid.SetCellResultValue(i_rows, 4, i, results(i).EqualityConstraint, 6)
        		
        	elseIf (Results.Item(i).HasLowerBound Or Results.Item(i).HasUpperBound) Then
        		Call AgSearchOperand.StatusGrid.SetCellString(i_rows,4,"-")
        	
        	else
        	
        	end If
            		
        	'Achieved column -- don't set until after running sequence once
        	Call AgSearchOperand.StatusGrid.SetCellString(i_rows,5,"-Not Set-")
            		
        	'Difference column -- don't set until after running sequence once
        	if (Results.Item(i).IsMinimized) Then
        		Call AgSearchOperand.StatusGrid.SetCellString(i_rows,6,"-")            			
        	           		
        	else           		            		           		
       			Call AgSearchOperand.StatusGrid.SetCellString(i_rows,6,"-Not Set-")           		
        						
		    end If
						
            i_rows = i_rows + 1 
        
        end If
         
    next
    
    Call AgSearchOperand.StatusGrid.SetStatus("Initial Run of MCS...")
    Call AgSearchOperand.StatusGrid.Refresh()
    
    if testing then
    
        ' using run once setting, just do 1 run of the target sequence, update the grid and exit
        Call AgSearchOperand.Evaluate()

        i_rows = 1+3
        for i = 0 to (Results.Count-1)  
    	    if ( i_rows <= numRows And Results.Item(i).Active) Then
    	    	              		
        	    'Achieved column
        	    Call AgSearchOperand.StatusGrid.SetCellResultValue(i_rows, 5, i, results(i).CurrentValue, 6)
            		
        	    'Difference column
        	    if (Results.Item(i).IsMinimized) Then
        		    Call AgSearchOperand.StatusGrid.SetCellString(i_rows,6,"-")
            			                	           		
        	    elseIf (Results.Item(i).HasLowerBound And Results.Item(i).HasUpperBound) Then
            		            		           		
        		    if ((Results.Item(i).CurrentValue >= Results.Item(i).LowerBound) And (Results.Item(i).CurrentValue <= Results.Item(i).UpperBound)) Then
        			    Call AgSearchOperand.StatusGrid.SetCellString(i_rows,6,"OK")
            		
        		    elseIf (Results.Item(i).CurrentValue >= Results.Item(i).LowerBound) Then
        			    Call AgSearchOperand.StatusGrid.SetCellResultDeltaValue(i_rows, 6, i, results(i).LowerBound - results(i).CurrentValue, 6)
            		
		            elseIf (Results.Item(i).CurrentValue <= Results.Item(i).UpperBound) Then
        			    Call AgSearchOperand.StatusGrid.SetCellResultDeltaValue(i_rows, 6, i, results(i).CurrentValue - results(i).UpperBound, 6)
            		
        		    else
            		
        		    end If
            			
        	    elseIf ( Results.Item(i).HasUpperBound And (Not Results.Item(i).HasLowerBound) ) Then

        		    if (Results.Item(i).CurrentValue <= Results.Item(i).UpperBound) Then
        			    Call AgSearchOperand.StatusGrid.SetCellString(i_rows,6,"OK")
        		    else
        			    Call AgSearchOperand.StatusGrid.SetCellResultDeltaValue(i_rows, 6, i, results(i).CurrentValue - results(i).UpperBound, 6)
		            end If
			
        	    elseIf ( Results.Item(i).HasLowerBound And (Not Results.Item(i).HasUpperBound) ) Then

        		    if (Results.Item(i).CurrentValue >= Results.Item(i).LowerBound) Then
        			    Call AgSearchOperand.StatusGrid.SetCellString(i_rows,6,"OK")
        		    else
        			    Call AgSearchOperand.StatusGrid.SetCellResultDeltaValue(i_rows, 6, i, results(i).LowerBound - results(i).CurrentValue, 6)
			    end If
    		
		    elseIf (Results.Item(i).HasEqualityConstraint) Then
		       	    Call AgSearchOperand.StatusGrid.SetCellResultDeltaValue(i_rows, 6, i, results(i).CurrentValue - results(i).EqualityConstraint, 6)
    		
		    else
    		
       	    end If
                	
            i_rows = i_rows + 1 
            
            end If
             
        next
        
        ' update header  info
        
        
        Call AgSearchOperand.StatusGrid.SetStatus("Finished: *DID NOT CONVERGE*")  ' optimizers can't converge in run once mode
        Call AgSearchOperand.StatusGrid.Refresh()
    
        Run = 0
        exit function
    
    end if


    Call m_MatlabApp.PutWorkspaceData("operand", "global", AgSearchOperand)
    Call m_MatlabApp.PutWorkspaceData("pluginsite", "global", m_AgUtPluginSite)
    Call m_MatlabApp.PutWorkspaceData("MaxIterations", "global", m_maxIters)
    Call m_MatlabApp.PutWorkspaceData("MaxFunctionEvals", "global", m_maxFunEvals)
    Call m_MatlabApp.PutWorkspaceData("CostTolerance", "global", m_costTolerance)
    Call m_MatlabApp.PutWorkspaceData("ConstraintTolerance", "global", m_constraintTolerance)
    Call m_MatlabApp.PutWorkspaceData("InputTolerance", "global", m_inputTolerance)
    Call m_MatlabApp.PutWorkspaceData("DiffMaxChange", "global", m_diffMaxChange)
    Call m_MatlabApp.PutWorkspaceData("DiffMinChange", "global", m_diffMinChange)
    
    Dim MatlabResults
    
    Call m_MatlabApp.Feval(m_mFilename, 1, MatlabResults)

	Run = 1
	
End Function

'=============================
' Get Control's Prog Id Method
'=============================
Function GetControlsProgId( controlType )

	if (controlType = eSearchControlTypesReal) then
		GetControlsProgId = "MatlabSearchControlReal.WSC"
	else
		GetControlsProgId = ""
	end if

End Function

'=============================
' Get Results's Prog Id Method
'=============================
Function GetResultsProgId()
      
	GetResultsProgId = "MatlabSearchResult.WSC"
       	
End Function

'=================
' Free Method
'=================
Sub Free()

	' do nothing
    Set uiApp = Nothing
End Sub
    
'==================
' Name property
'==================
Function GetName()

	GetName = m_Name

End function

'=======================
' MaxIterations property
'=======================
Function GetMaxIterations()

       GetMaxIterations = m_maxIters

End Function

Function SetMaxIterations( val )

       m_maxIters = val

End Function

'=======================
' MaxFunctionEvals property
'=======================
Function GetMaxFunctionEvals()

       GetMaxFunctionEvals = m_maxFunEvals

End Function

Function SetMaxFunctionEvals( val )

       m_maxFunEvals = val

End Function

'======================
' CostTolerance Property
'======================
Function GetCostTolerance()
    
    GetCostTolerance = m_costTolerance
    
End Function

Function SetCostTolerance( val )

    m_costTolerance = val
    
End Function

'======================
' ConstraintTolerance Property
'======================
Function GetConstraintTolerance()

    GetConstraintTolerance = m_constraintTolerance
    
End Function

Function SetConstraintTolerance( val )

    m_constraintTolerance = val
    
End Function


'======================
' InputTolerance Property
'======================
Function GetInputTolerance()

    GetInputTolerance = m_inputTolerance
    
End Function

Function SetInputTolerance( val )

    m_inputTolerance = val
    
End Function

'======================
' DiffMaxChange Property
'======================
Function GetDiffMaxChange()

    GetDiffMaxChange = m_diffMaxChange
    
End Function

Function SetDiffMaxChange( val )

    m_diffMaxChange = val
    
End Function

'======================
' DiffMinChange Property
'======================
Function GetDiffMinChange()

    GetDiffMinChange = m_diffMinChange
    
End Function

Function SetDiffMinChange( val )

    m_diffMinChange = val
    
End Function

'======================================================
'  Copyright 2006, Analytical Graphics, Inc.          
' =====================================================

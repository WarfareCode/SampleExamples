/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/


INSTALLATION PROCEDURE

The following is a step by step procedure for registering the MATLAB fmincon optimizer search profile plugin. This plugin 
requires STK 9.0 or later.

1. Place the 4 *.m files into a MATLAB path directory. You may
   (a) copy these files into an existing MATLAB path directory, such as <MATLAB install>\toolbox\local, or
   (b) copy these files into any other directory and add it to your MATLAB path, using the 'setpath' command in MATLAB
2. Close STK if it is open.
3. Register each of the *.wsc files by right-clicking on them in Windows Explorer and selecting the "Register" option.
4. Place the *.xml file into your <STK Install>\Plugins directory.
5. Restart STK.

To verify this process was successful, create a Target Sequence in Astrogator and add a New profile. There should now be an 
option for "MATLAB fmincon Optimizer".



PLUGIN OVERVIEW

Any AgSearch plugin actually consists of three separate plugins: one for the controls, one for the results, and one for the 
search algorithm itself. Any plugin built using a scripting language like VBScript or JavaScript (this one uses VBScript)
actually requires two files: one Windows Scripting Component file to register the plugin with Windows and one scripting
language file to actually implement the interface. Additionally, since this scripting language plugin utilizes Matlab, there
are Matlab *.m files that are required as well. The number and format of those files is determined by the requirements of the
fmincon function.

For more details on the AgSearch plugin interface or script-based plugins, consult the STK Integration help.

File listing and description:

 - MatlabSearchControlReal.vbs: conforms to IAgSearchControlReal interface and declares custom controls properties
 - MatlabSearchControlReal.wsc: registers this plugin and its properties and methods with Windows
 
 - MatlabSearchResult.vbs: conforms to IAgSearchResult interface and declares custom results properties
 - MatlabSearchResult.wsc: registers this plugin and its properties and methods with Windows
 
 - MatlabSearch.vbs: conforms to the IAgSearch interface and declares custom search profile properties. Also evaluates
 	controls and results and implements the search algorithm by putting variables into the Matlab workspace and calling
 	Optimize.m.
 - MatlabSearch.wsc: registers this plugin and its properties and methods with Windows
 
 - Optimize.m: Collects inputs and passes them to Matlab's fmincon
 - MatlabSearch.m: The function fmincon evaluates which sends new values to STK, evaluates the Astrogator MCS, and gets 
 	new results from STK
 - MatlabSearchNONLCON.m: If the optimization has non-linear (e.g. inequality) constraints, fmincon evaluates this function
 	to check against them.
 - UpdateIter.m:  The function evaluated at the end of each iteration of fmincon, updates the plugin StatusGrid with 
        iteration data
 	


PLUGIN SETTINGS

Controls plugin custom properties (defined in MatlabSearchControlReal.vbs)
 - Name: Human readable plugin name or alias
 - Active: True if control should be used in optimization
 - HasUpperBound: True if control has an upper bound inequality constraint
 - HasLowerBound: True if control has an lower bound inequality constraint
 - UpperBound: Value for upper bound, only available if HasUpperBound is True
 - LowerBound: Value for lower bound, only available if HasLowerBound is True
 - ScalingMultiplier: Multiplicative scale factor applied to inputs before being sent to fmincon
 
Results plugin custom properties (defined in MatlabSearchResult.vbs)
 - Name: Human readable plugin name or alias
 - Active: True if result should be used in optimization
 - HasUpperBound: True if result has an upper bound inequality constraint
 - HasLowerBound: True if result has an lower bound inequality constraint
 - HasEqualityConstraint: True if result has an equality constraint
 - UpperBound: Value for upper bound, only available if HasUpperBound is True
 - LowerBound: Value for lower bound, only available if HasLowerBound is True
 - EqualityConstraint: Value for equality constraint, only available if HasEqualityConstraint is True
 - IsMinimized: True if result is to be minimized
 - Weight: Weight of result. Used as a multiplicative scale factor to indicate relative importance of multiple goals.
 - ScalingMultiplier: Multiplicative scale factor applied to inputs before being sent to fmincon

Search plugin custom properties (define in MatlabSearch.vbs)
 - Name: Human readable plugin name or alias
 ** Note that the following properties are settings used by fmincon. Consult the Matlab documentation for further help. ** 
 - MaxIterations: Maximum Iterations (MaxIter)
 - MaxFunctionEvals: Maximum Function Evaluations (MaxFunEvals)
 - CostTolerance: Termination Tolerance for the cost function (TolFun)
 - Constraint Tolerance: Termination Tolerance for the constraint (TolCon)
 - InputTolerance: Termination Tolerance for the independant variables (TolX)
 - DiffMaxChange: Maximum change in variables for finite differencing (DiffMaxChange)
 - DiffMinChange: Minimum change in variables for finite differencing (DiffMinChange)
 - Diagnostics: True to display diagnostic information about the function to be minimized



/**********************************************************************/
/*           Copyright 2018, Analytical Graphics, Inc.                */
/**********************************************************************/
 
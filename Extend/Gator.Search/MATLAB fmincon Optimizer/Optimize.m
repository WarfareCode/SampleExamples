function OptimizedControls = Optimize()
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Copyright 2007-2011, Analytical Graphics, Inc
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

global operand;
global MaxIterations;
global MaxFunctionEvals;
global CostTolerance;
global ConstraintTolerance;
global InputTolerance;
global DiffMaxChange;
global DiffMinChange;
global Iteration;
global FMINCONiter;

FMINCONiter = 0;

ABRowCount = 0;
ABeqRowCount = 0;
A = [];
B = [];
Aeq = [];
Beq = [];
LB = [];
UB = [];

options = optimset('DiffMaxChange', DiffMaxChange, 'DiffMinChange', DiffMinChange, 'Display', 'iter', 'MaxFunEvals', MaxFunctionEvals, 'MaxIter', MaxIterations, 'TolCon', ConstraintTolerance, 'TolFun', CostTolerance, 'TolX', InputTolerance,'OutputFcn',@UpdateIter);

Constrained = false;
Iteration = 0;
i_controls = 1;

%Evaluate and apply constraints on Controls. Constraints on Results are
%handled in MatlabSearchNONLCON.m.
for i = 1:operand.Controls.Count

    %Create an subset of scaled controls that only contains those
    %that are Active which will be passed to fmincon.
    if (operand.Controls.Item(i-1).Active == true)

        InitialControls(i_controls) = operand.Controls.Item(i-1).CurrentValue * operand.Controls.Item(i-1).ScalingMultiplier;

        %Create scaled Upper Bound inputs for this control. If there are any
        %bounds, consider this a Constrained problem.
        if (operand.Controls.Item(i-1).HasUpperBound == true)

            UB(i_controls) = operand.Controls.Item(i-1).UpperBound * operand.Controls.Item(i-1).ScalingMultiplier;
            Constrained = true;

        else

            UB(i_controls) = Inf;

        end

        %Create scaled Lower Bound inputs for this control. If there are any
        %bounds, consider this a Constrained problem.
        if (operand.Controls.Item(i-1).HasLowerBound == true)

            LB(i_controls) = operand.Controls.Item(i-1).LowerBound * operand.Controls.Item(i-1).ScalingMultiplier;
            Constrained = true;

        else

            LB(i_controls) = -Inf;

        end
        
        %increment subset counter
        i_controls = i_controls + 1;
        
    else        
              
    end    
        
end

%If there are any bounds on any results, consider this a Constrained
%problem. Constraints on Results are handleded in MatlabSearchNONLCON.m.

for j = 1:operand.Results.Count

    if (operand.Results.Item(j-1).Active == true)
        
        if (operand.Results.Item(j-1).HasUpperBound == true || operand.Results.Item(j-1).HasLowerBound == true || operand.Results.Item(j-1).HasEqualityConstraint == true)

            Constrained = true;
        end
    else
        
    end
end

%Call the unconstrained version of fmincon
if (Constrained ~= true)
	
	[OptimizedControls, OptimizedResults, exitflag, output] = fminunc(@MatlabSearch, InitialControls, options);

    

%Call the constrained version of fmincon
else

	[OptimizedControls, OptimizedResults, exitflag, output]  = fmincon(@MatlabSearch,InitialControls, A, B, Aeq, Beq, LB, UB, @MatlabSearchNONLCON, options);
    
    
end

j_controls = 1;
%Apply the unscaled control values returned from fmincon to the actual controls
for j = 1:operand.Controls.Count
	
   if (operand.Controls.Item(j-1).Active == true)
    	
        operand.Controls.Item(j-1).CurrentValue = OptimizedControls(j_controls) / operand.Controls.Item(j-1).ScalingMultiplier;
        temp = [operand.Controls.Item(j-1).ObjectName '.' operand.Controls.Item(j-1).ControlName ' ' num2str(operand.Controls.Item(j-1).CurrentValue)];
        j_controls = j_controls + 1;

    
   end
end

OptimizedControls
OptimizedResults
%pluginsite.Message(1, 'Here are the optimized controls...');
exitflag
flagout = [''];
switch exitflag
    case -1
        flagout=['Stopped by Output fun. '];
    case -2
        flagout=['No feasible point.  '];
    case 0
        flagout=['Max iters or fun evals.  '];
    case 1
        flagout=['Optimization Successful.  '];
    case 2
        flagout=['Input tolerance met.  '];
    case 3
        flagout=['Objective tolerance met.  '];
    case 4
        flagout=['Mag of seach direction & const tol''s met.  '];
    case 5
        flagout=['Mag of directional derivative & const tol''s met.  '];
end

%pluginsite.Message(1, exitflag);
output
output.message
%pause

%Run the Astrogator MCS
operand.Evaluate();
operand.StatusGrid.Refresh();
temp = ['' flagout 'Iterations: ' num2str(FMINCONiter-1), ', Function evals: ' num2str(Iteration+1) ];
operand.StatusGrid.SetStatus(temp);
operand.StatusGrid.Refresh();




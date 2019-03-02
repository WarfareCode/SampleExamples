function [C,Ceq] = MatlabSearchNONLCON(Controls)
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Copyright 2007-2011, Analytical Graphics, Inc
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

global operand;
global Iteration;

C = [];
Ceq = [];
 
i_controls = 1;
i_rows = 1+3;

canSkip = 1;

%Look at every control, not just the ones passed to this function
for i = 1:operand.Controls.Count
    
    %If a given control is Active, then replace its value with the next one
    %(unscaled) in the Controls variable that was passed to this function
    if (operand.Controls.Item(i-1).Active == true)
        
        if (operand.Controls.Item(i-1).CurrentValue ~= Controls(i_controls) / operand.Controls.Item(i-1).ScalingMultiplier)
        
            canSkip = 0;
        
             operand.Controls.Item(i-1).CurrentValue = Controls(i_controls) / operand.Controls.Item(i-1).ScalingMultiplier;
        end

        %Control Name column
        operand.StatusGrid.SetCellString(i_rows,0,[operand.controls.Item(i-1).ObjectName ' : ' operand.controls.Item(i-1).ControlName]);

        %Current Value column
        operand.StatusGrid.SetCellControlValue(i_rows, 1, i-1, operand.controls.Item(i-1).CurrentValue, 6);

        %Last Update column
        operand.StatusGrid.SetCellControlDeltaValue(i_rows, 2, i-1, operand.controls.Item(i-1).CurrentValue - operand.controls.Item(i-1).InitialValue, 6);
                
        i_controls = i_controls + 1;
        i_rows = i_rows + 1;
    end
end

%Run the Astrogator MCS once to get new results
if (~canSkip)
    operand.Evaluate();
    Iteration = Iteration+1;
end
k = 1;
keq = 1;
i_rows = 1+3;
%Evaluate and apply constraints on the Results
for j = 1:operand.Results.Count
    
    if (operand.Results.Item(j-1).Active == true)
        %Result Name column	    	
        operand.StatusGrid.SetCellString(i_rows,3,[operand.results.Item(j-1).ObjectName ' : ' operand.results.Item(j-1).ResultName]);

        %Desired column
        if (operand.Results.Item(j-1).IsMinimized)
            operand.StatusGrid.SetCellString(i_rows,4,'Min');

        elseif (operand.Results.Item(j-1).HasEqualityConstraint)
            operand.StatusGrid.SetCellResultValue(i_rows, 4, j-1, operand.results.Item(j-1).EqualityConstraint, 6);

        elseif (operand.Results.Item(j-1).HasLowerBound == true || operand.Results.Item(j-1).HasUpperBound == true) 
            operand.StatusGrid.SetCellString(i_rows,4,'-');

        end


        %Achieved column
        operand.StatusGrid.SetCellResultValue(i_rows, 5, j-1, operand.results.Item(j-1).CurrentValue, 6)


        %Difference column
        if (operand.Results.Item(j-1).HasUpperBound == true && operand.Results.Item(j-1).HasLowerBound == true)

            C(k) = (operand.Results.Item(j-1).CurrentValue * operand.Results.Item(j-1).ScalingMultiplier) - (operand.Results.Item(j-1).UpperBound * operand.Results.Item(j-1).ScalingMultiplier);
            k = k + 1;

            C(k) = -(operand.Results.Item(j-1).CurrentValue * operand.Results.Item(j-1).ScalingMultiplier) + (operand.Results.Item(j-1).LowerBound * operand.Results.Item(j-1).ScalingMultiplier);
            k = k + 1;

            %Difference column
            if ((operand.Results.Item(j-1).CurrentValue <= operand.Results.Item(j-1).UpperBound) && (operand.Results.Item(j-1).CurrentValue >= operand.Results.Item(j-1).LowerBound))
                operand.StatusGrid.SetCellString(i_rows,6,'OK');

            elseif (operand.Results.Item(j-1).CurrentValue >= operand.Results.Item(j-1).UpperBound)
                operand.StatusGrid.SetCellResultDeltaValue(i_rows, 6, j-1, operand.results.Item(j-1).CurrentValue - operand.results.Item(j-1).UpperBound, 6);

            elseif (operand.Results.Item(j-1).CurrentValue <= operand.Results.Item(j-1).LowerBound)
                operand.StatusGrid.SetCellResultDeltaValue(i_rows, 6, j-1, operand.results.Item(j-1).LowerBound - operand.results.Item(j-1).CurrentValue, 6);
            end 
        
        %Add scaled Upper Bound constraints, if this result is Active
        elseif (operand.Results.Item(j-1).HasUpperBound == true  && operand.Results.Item(j-1).HasLowerBound == false)

            C(k) = (operand.Results.Item(j-1).CurrentValue * operand.Results.Item(j-1).ScalingMultiplier) - (operand.Results.Item(j-1).UpperBound * operand.Results.Item(j-1).ScalingMultiplier);
            k = k + 1;

            %Difference column
            if (operand.Results.Item(j-1).CurrentValue <= operand.Results.Item(j-1).UpperBound)
                operand.StatusGrid.SetCellString(i_rows,6,'OK');
            else
                operand.StatusGrid.SetCellResultDeltaValue(i_rows, 6, j-1, operand.results.Item(j-1).CurrentValue - operand.results.Item(j-1).UpperBound, 6);
            end

        %Add scaled Lower Bound constraints, if this result is Active
        elseif (operand.Results.Item(j-1).HasUpperBound == false && operand.Results.Item(j-1).HasLowerBound == true)

            C(k) = -(operand.Results.Item(j-1).CurrentValue * operand.Results.Item(j-1).ScalingMultiplier) + (operand.Results.Item(j-1).LowerBound * operand.Results.Item(j-1).ScalingMultiplier);
            k = k + 1;

            %Difference column
            if (operand.Results.Item(j-1).CurrentValue >= operand.Results.Item(j-1).LowerBound)
                    operand.StatusGrid.SetCellString(i_rows,6,'OK');
            else
                    operand.StatusGrid.SetCellResultDeltaValue(i_rows, 6, j-1, operand.results.Item(j-1).CurrentValue - operand.results.Item(j-1).LowerBound, 6);
            end

        end
            
        %Add scaled Equality constraints, if this result is Active
        if (operand.Results.Item(j-1).HasEqualityConstraint == true)

            Ceq(keq) = (operand.Results.Item(j-1).CurrentValue * operand.Results.Item(j-1).ScalingMultiplier) - (operand.Results.Item(j-1).EqualityConstraint * operand.Results.Item(j-1).ScalingMultiplier);
            keq = keq + 1;
            
            %Difference column
            operand.StatusGrid.SetCellResultDeltaValue(i_rows, 6, j-1, operand.results.Item(j-1).CurrentValue - operand.results.Item(j-1).EqualityConstraint, 6);

        end    

    i_rows = i_rows + 1;

    end
end       
operand.StatusGrid.Refresh();    

function Cost = MatlabSearch(Controls)
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%&&&&&&&
% Copyright 2007-2011, Analytical Graphics, Inc
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%&&&&&&&

global operand;
global Iteration;
global FMINCONiter;

i_controls = 1;

%Look at every control, not just the ones passed to this function
for i = 1:operand.Controls.Count
    
   %If a given control is Active, then replace its value with the next one
   %(unscaled) in the Controls variable that was passed to this function
    if (operand.Controls.Item(i-1).Active == true)
    
        operand.Controls.Item(i-1).CurrentValue = Controls(i_controls) / operand.Controls.Item(i-1).ScalingMultiplier;
        
        %Controls(i_controls);
        operand.StatusGrid.SetCellString(i_controls+3,0,[operand.controls.Item(i-1).ObjectName ' : ' operand.controls.Item(i-1).ControlName]);

        %Current Value column
        operand.StatusGrid.SetCellControlValue(i_controls+3, 1, i-1, operand.controls.Item(i-1).CurrentValue, 6);
        
        %Last Update column
        operand.StatusGrid.SetCellControlDeltaValue(i_controls+3, 2, i-1, operand.controls.Item(i-1).CurrentValue - operand.controls.Item(i-1).InitialValue, 6);

        i_controls = i_controls + 1;
    end
end

%Run the Astrogator MCS once to get new results
operand.Evaluate();
Iteration = Iteration + 1;
Cost = 0;
i_rows = 1+3;
%Look at every result
for j = 1:operand.Results.Count

    if (operand.Results.Item(j-1).Active == true)

        %Result Name column
        operand.StatusGrid.SetCellString(i_rows,3,[operand.Results.Item(j-1).ObjectName ' : ' operand.Results.Item(j-1).ResultName]);

        %Desired column
        if (operand.Results.Item(j-1).IsMinimized == true)
            operand.StatusGrid.SetCellString(i_rows,4,'Min');
        elseif (operand.Results.Item(j-1).HasEqualityConstraint == true)
            operand.StatusGrid.SetCellResultValue(i_rows, 4, j-1, operand.results.Item(j-1).EqualityConstraint, 6);
        elseif (operand.Results.Item(j-1).HasLowerBound == true || operand.Results.Item(j-1).HasUpperBound == true) 
            operand.StatusGrid.SetCellString(i_rows,4,'-');
        end

        %Achieved column
        operand.StatusGrid.SetCellResultValue(i_rows, 5, j-1, operand.results.Item(j-1).CurrentValue, 6);

        %Difference column
        if (operand.Results.Item(j-1).IsMinimized == true)

            %If a given result is minimized and active, add its scaled
            %value to the total Cost
            Cost = Cost + (operand.Results.Item(j-1).CurrentValue * operand.Results.Item(j-1).Weight * operand.Results.Item(j-1).ScalingMultiplier );

            if (operand.Results.Item(j-1).HasUpperBound == false)
                operand.StatusGrid.SetCellString(i_rows,6,'-');
            end
        end
        i_rows = i_rows + 1;
    end
    
end
temp = ['Iteration ' num2str(FMINCONiter), ', Function evals ' num2str(Iteration) ', Cost ' num2str(Cost)];
operand.StatusGrid.SetStatus(temp);
operand.StatusGrid.Refresh();
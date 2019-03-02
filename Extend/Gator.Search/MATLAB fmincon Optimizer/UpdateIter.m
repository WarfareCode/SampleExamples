function stop = UpdateIter(x, optimValues, state)
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Copyright 2008-2011, Analytical Graphics, Inc
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

global FMINCONiter
global Iteration
global operand

if (FMINCONiter ~= optimValues.iteration+1)
	operand.Evaluate2(true);
	FMINCONiter = optimValues.iteration+1;
end

operand.StatusGrid.SetCellValue(1,0,optimValues.iteration,'', 6);
operand.StatusGrid.SetCellValue(1,1,optimValues.funccount,'', 6);
operand.StatusGrid.SetCellValue(1,2,optimValues.fval,'', 6);

 if isfield(optimValues,'constrviolation')
 	operand.StatusGrid.SetCellValue(1,3,optimValues.constrviolation,'', 6);
 else
    operand.StatusGrid.SetCellString(1,3,'N/A');
 end

if ~isempty(optimValues.stepsize)
    operand.StatusGrid.SetCellValue(1,4,optimValues.stepsize,'', 6);
end
if ~isempty(optimValues.directionalderivative)
    operand.StatusGrid.SetCellValue(1,5,optimValues.directionalderivative,'', 6);
end
if ~isempty(optimValues.firstorderopt)
    operand.StatusGrid.SetCellValue(1,6,optimValues.firstorderopt,'', 6);
end


stop = false;

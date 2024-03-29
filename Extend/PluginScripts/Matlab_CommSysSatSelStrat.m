function [output] = Matlab_CommSysSatSelStrat(input)
	% NOTE: the outputs that are returned
	%       MUST be in the same order as registered

	% NOTE: Please DO NOT change anything above or below the USER MODEL AREA.
	% 
	% This example shows static output parameters, but these parameters
      % may change value at each time step.

switch input.method
    
    case 'register'
 	% register output variables

        SatSelMeritValue = {'ArgumentName','SatSelMeritValue',...
                     'Name','SatSelMeritValue',...
                     'ArgumentType','Output'};

 	% register input variables       
   
        DateUTC= {'ArgumentName','DateUTC',...
                'Name','DateUTC',...
                'ArgumentType','Input'};
        
        EpochSec = {'ArgumentName','EpochSec',...
                  'Name','EpochSec',...
                  'ArgumentType','Input'};
 
        CbName = {'ArgumentName','CbName',...
                  'Name','CbName',...
                  'ArgumentType','Input'};

        CommSysPath = {'ArgumentName','CommSysPath',...
               'Name','CommSysPath',...
               'ArgumentType','Input'};

        FromIndex = {'ArgumentName','FromIndex',...
               'Name','FromIndex',...
               'ArgumentType','Input'};

        NumberOfFromObjects = {'ArgumentName','NumberOfFromObjects',...
               'Name','NumberOfFromObjects',...
               'ArgumentType','Input'};

        FromObjectsIDArray = {'ArgumentName','FromObjectsIDArray',...
               'Name','FromObjectsIDArray',...
               'ArgumentType','Input'};
 
        FromObjectIsStatic = {'ArgumentName','FromObjectIsStatic',...
               'Name','FromObjectIsStatic',...
               'ArgumentType','Input'};
       
        FromObjectPosCBFArray = {'ArgumentName','FromObjectPosCBFArray',...
               'Name','FromObjectPosCBFArray',...
               'ArgumentType','Input'};

        FromObjectPosLLAArray = {'ArgumentName','FromObjectPosLLAArray',...
               'Name','FromObjectPosLLAArray',...
               'ArgumentType','Input'};

        FromToRelPosArray = {'ArgumentName','FromToRelPosArray',...
               'Name','FromToRelPosArray',...
               'ArgumentType','Input'};
 
        FromObjectAttitudeArray = {'ArgumentName','FromObjectAttitudeArray',...
               'Name','FromObjectAttitudeArray',...
               'ArgumentType','Input'};
       
        ToIndex = {'ArgumentName','ToIndex',...
               'Name','ToIndex',...
               'ArgumentType','Input'};

        NumberOfToObjects = {'ArgumentName','NumberOfToObjects',...
               'Name','NumberOfToObjects',...
               'ArgumentType','Input'};

        ToObjectsIDArray = {'ArgumentName','ToObjectsIDArray',...
               'Name','ToObjectsIDArray',...
               'ArgumentType','Input'};
 
        ToObjectIsStatic = {'ArgumentName','ToObjectIsStatic',...
               'Name','ToObjectIsStatic',...
               'ArgumentType','Input'};
       
       ToObjectPosCBFArray = {'ArgumentName','ToObjectPosCBFArray',...
               'Name','ToObjectPosCBFArray',...
               'ArgumentType','Input'};

        ToObjectPosLLAArray = {'ArgumentName','ToObjectPosLLAArray',...
               'Name','ToObjectPosLLAArray',...
               'ArgumentType','Input'};

        ToFromRelPosArray = {'ArgumentName','ToFromRelPosArray',...
               'Name','ToFromRelPosArray',...
               'ArgumentType','Input'};
 
        ToObjectAttitudeArray = {'ArgumentName','ToObjectAttitudeArray',...
               'Name','ToObjectAttitudeArray',...
               'ArgumentType','Input'};
   
        output = {SatSelMeritValue,...
                  DateUTC, EpochSec, CbName, CommSysPath,...
                  FromIndex, NumberOfFromObjects, FromObjectsIDArray, FromObjectIsStatic,...
                  FromObjectPosCBFArray, FromObjectPosLLAArray, FromToRelPosArray, FromObjectAttitudeArray,...
                  ToIndex, NumberOfToObjects, ToObjectsIDArray, ToObjectIsStatic,...
                  ToObjectPosCBFArray, ToObjectPosLLAArray, ToFromRelPosArray, ToObjectAttitudeArray};
    
    case 'compute'
    
        computeData = input.methodData;

	% compute the Test Model : 
	% Example Model for testing only
	% CommSysSatSelStrat input & output parameter usage is shown
	% 

	% USER CommSysSatSelStrat MODEL AREA.
        
      time = computeData.EpochSec;
      fromNum = computeData.FromIndex;
      toNum = computeData.ToIndex;
      
    % NumberOfFromObjects is total number of from objects and also the length of the From value arrays
	% NumberOfToObjects is total number of to objects and also the length of the To value arrays
      
      fromObj = computeData.FromObjectPosCBFArray; 
      toObj   = computeData.ToObjectPosCBFArray;
      
      rX = fromObj(3*fromNum+1) - toObj(3*toNum+1);
      rY = fromObj(3*fromNum+2) - toObj(3*toNum+2);
      rZ = fromObj(3*fromNum+3) - toObj(3*toNum+3);
      
      range = sqrt(rX*rX + rY*rY + rZ*rZ);
      
        output.SatSelMeritValue = range;

	% END OF USER MODEL AREA

    otherwise
        output = [];
end


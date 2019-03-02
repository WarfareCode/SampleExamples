function [output] = Matlab_CustomVector(input)

switch input.method
    
    case 'register'
        
        vec = {     'ArgumentType','Output',...
                    'ArgumentName','vec',...
                    'Name','Vector'};

	  time = { 	  'ArgumentType','Input',...
			  'ArgumentName','time',...
			  'Name','Epoch'};
    
        apoVec = {  'ArgumentType','Input',...
                    'Name','Apoapsis',...
                    'ArgumentName','apoVec',...
                    'Type','Vector',...
			  'RefName','Body'};

	  bAxes = {    'ArgumentType','Input',...
			   'ArgumentName','bodyAxes',...
			   'Type','Axes',...
			   'Name','Body',...
			   'RefName','TopoCentric'}; 
            
        smAngle = {'ArgumentType','Input',...
                    'Name','SunMoon',...
                    'ArgumentName','sunMoonAngle',...
                    'Type','Angle'};
    
        mnPnt =   { 'ArgumentType','Input',...
                    'Name','Center',...
                    'ArgumentName','moonPnt',...
                    'Type','Point',...
                    'Source','CentralBody/Moon',...
                    'RefName','Inertial',...
                    'RefSource','CentralBody/Sun'};
            
        bSys = { 'ArgumentType','Input',...
                    'Name','Body',...
                    'ArgumentName','bodySys',...
                    'Type','CrdnSystem',...
			  'RefName','Fixed',...
                    'RefSource','CentralBody/Earth'};
            
        output = {vec, time, apoVec, bAxes, smAngle, mnPnt, bSys};
     
    case 'compute'

	  computeData = input.methodData;
		
	  % below shows how to extract inputs

	  % computeData.time
	  % computeData.apoVec
	  % computeData.bodyAxes
	  % computeData.sunMoonAngle
	  % computeData.moonPnt
	  % computeData.bodySys

	  apoVector = computeData.apoVec;

        output.vec(1) = apoVector(1);
        output.vec(2) = apoVector(2);
        output.vec(3) = apoVector(3);
                
    otherwise
        output = [];
end


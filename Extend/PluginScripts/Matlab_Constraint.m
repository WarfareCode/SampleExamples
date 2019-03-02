function [output] = Matlab_Constraint(input)

% Matlab_Constraint
% AUTHOR: Guy Smith
%
% USAGE: 
%
%  This function is called by the AGI Constraint Script Plugin software and is not intended 
%   for general use. This function takes a structure as input that contains
%   a 'method' string parameter that is utilized to determine what the purpose
%   of the call was. Four methods are supported. ( 'compute', 'register', 'GetAccessList',
%   'GetConstraintDisplayName' ) Refer to the Constraint Script Plugin documentation 
%   for an in-depth explaination of the functionality.
%
%  This file provides an example of the capabilities of the Constraint Script Plugin
%   functionality. The simple Constraint that is implemented by this script associates itself
%   with the Facility class and configures itself to return the the Constraint iteration 
%   step Epoch time (in STK Epoch seconds) passed in on every iteration.

% Copyright 2002, Analytical Graphics Incorporated

switch input.method
    
    case 'compute'
    
        computeData = input.methodData;

        % output.status = 'MESSAGE: [Info] Matlab Matlab_Constraint- Everything is fine;  CONTROL: OK';
	  output.status = 'Okay';
        output.Result = computeData.epoch;	% Result is just the input Epoch
        
    case 'register'
        
        status = {  'ArgumentName','status',...
                    'Name','status',...
                    'ArgumentType','Output'};
        
        result = {  'ArgumentName','Result',...
                    'Name','result',...
                    'ArgumentType','Output'};
    
        epoch = {  'ArgumentName','epoch',...
                    'Name','epoch',...
                    'ArgumentType','Input'};
    
        fromPosition = { 'ArgumentName','fromPos',...
                    'Name','fromPosition',...
                    'ArgumentType','Input',...
                    'RefName','Fixed'};

        fromVelocity = { 'ArgumentName','fromVel',...
                    'Name','fromVelocity',...
                    'ArgumentType','Input',...
                    'RefName','Inertial'};
   
        fromQuaternion = { 'ArgumentName','fromQuat',...
                    'Name','fromQuaternion',...
                    'ArgumentType','Input',...
                    'RefName','Fixed'};
    
        toPosition = { 'ArgumentName','toPos',...
                    'Name','toPosition',...
                    'ArgumentType','Input',...
                    'RefName','Inertial'};

        toVelocity = { 'ArgumentName','toVel',...
                    'Name','toVelocity',...
                    'ArgumentType','Input',...
                    'RefName','Fixed'};
   
        toQuaternion = { 'ArgumentName','toQuat',...
                    'Name','toQuaternion',...
                    'ArgumentType','Input',...
                    'RefName','Inertial'};
    
        fromObjectPath = {  'ArgumentName','fromObj',...
                    'Name','fromObjectPath',...
                    'ArgumentType','Input'};
    
        toObjectPath = {  'ArgumentName','toObj',...
                    'Name','toObjectPath',...
                    'ArgumentType','Input'};
    
        vector = { 'ArgumentType','Input',...
                    'Type','Vector',...
                    'Name','Earth',...
                    'Source','CentralBody/Moon',...
                    'RefSource','CentralBody/Sun',...
                    'RefName','Fixed',...
			  'ArgumentName','toEarthFromMoonInSunFixed',...
			  'Derivative','Yes'};
    
        output = {status, result, epoch, fromPosition,...
		           fromVelocity, fromQuaternion, toPosition, toVelocity,...
				    toQuaternion, fromObjectPath, toObjectPath, vector};
    
    case 'GetAccessList'

        computeData = input.methodData;

		if strcmp( computeData, 'Facility' )
			output = 'Aircraft,AreaTarget,Facility,GroundVehicle,LaunchVehicle,Missile,Planet,Radar,Satellite,Ship,Star,Target';
		else
			output = [];
		end

    case 'GetConstraintDisplayName'

		output = 'MatlabPluginConstraint';

    otherwise
        output = [];
end


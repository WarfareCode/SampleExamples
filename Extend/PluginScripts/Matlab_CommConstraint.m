function [output] = Matlab_CommConstraint(input)
	% NOTE: the outputs that are returned
	%       MUST be in the same order as registered

	% NOTE: Please DO NOT change anything above or below the USER MODEL AREA.
	% 
	% This example shows static output parameters, but these parameters
      % may change value at each time step.

switch input.method
    
    case 'register'
 	% register output variables       
        PluginConstraintValue = {'ArgumentName','PluginConstraintValue',...
                     'Name','PluginConstraintValue',...
                     'ArgumentType','Output'};
             
    % register input variables  
    
        DateUTC = {'ArgumentName','DateUTC',...
                'Name','DateUTC',...
                'ArgumentType','Input'};
        
        EpochSec = {'ArgumentName','EpochSec',...
                'Name','EpochSec',...
                'ArgumentType','Input'};
 
        CbName = {'ArgumentName','CbName',...
                  'Name','CbName',...
                  'ArgumentType','Input'};

        ReceiverPath = {'ArgumentName','ReceiverPath',...
               'Name','ReceiverPath',...
               'ArgumentType','Input'};

        RcvrPosCBF = {'ArgumentName','RcvrPosCBF',...
               'Name','RcvrPosCBF',...
               'ArgumentType','Input'};

        RcvrAttitude = {'ArgumentName','RcvrAttitude',...
               'Name','RcvrAttitude',...
               'ArgumentType','Input'};
 
        TransmitterPath = {'ArgumentName','TransmitterPath',...
               'Name','ReceiverPath',...
               'ArgumentType','Input'};
               
        XmtrPosCBF= {'ArgumentName','XmtrPosCBF',...
               'Name','XmtrPosCBF',...
               'ArgumentType','Input'};
       
        XmtrAttitude = {'ArgumentName','XmtrAttitude',...
               'Name','XmtrAttitude',...
               'ArgumentType','Input'};
    
        ReceivedFrequency = {'ArgumentName','ReceivedFrequency',...
                    'Name','ReceivedFrequency',...
                    'ArgumentType','Input'};
            
        DataRate = {'ArgumentName','DataRate',...
                    'Name','DataRate',...
                    'ArgumentType','Input'};
            
        Bandwidth = {'ArgumentName','Bandwidth',...
                    'Name','Bandwidth',...
                    'ArgumentType','Input'};

        CDMAGainValue = {'ArgumentName','CDMAGainValue',...
                     'Name','CDMAGainValue',...
                     'ArgumentType','Input'};
             
        ReceiverGain = {'ArgumentName','ReceiverGain',...
                     'Name','ReceiverGain',...
                     'ArgumentType','Input'};
    
        PolEfficiency = {'ArgumentName','PolEfficiency',...
                    'Name','PolEfficiency',...
                    'ArgumentType','Input'};

        PolRelativeAngle = {'ArgumentName','PolRelativeAngle',...
                     'Name','PolRelativeAngle',...
                     'ArgumentType','Input'};
    
        RIP = {'ArgumentName','RIP',...
                    'Name','RIP',...
                    'ArgumentType','Input'};

        FluxDensity = {'ArgumentName','FluxDensity',...
                     'Name','FluxDensity',...
                     'ArgumentType','Input'};
    
        GOverT = {'ArgumentName','GOverT',...
                    'Name','GOverT',...
                    'ArgumentType','Input'};

        CarrierPower = {'ArgumentName','CarrierPower',...
                     'Name','CarrierPower',...
                     'ArgumentType','Input'};
    
        BandwidthOverlap = {'ArgumentName','BandwidthOverlap',...
                    'Name','BandwidthOverlap',...
                    'ArgumentType','Input'};

        CNo = {'ArgumentName','CNo',...
                     'Name','CNo',...
                     'ArgumentType','Input'};
    
        CNR = {'ArgumentName','CNR',...
                    'Name','CNR',...
                    'ArgumentType','Input'};

        EbNo = {'ArgumentName','EbNo',...
                     'Name','EbNo',...
                     'ArgumentType','Input'};
    
        BER = {'ArgumentName','BER',...
                    'Name','BER',...
                    'ArgumentType','Input'};
  
        output = {PluginConstraintValue,...
                  DateUTC, EpochSec, CbName, ReceiverPath, TransmitterPath,...
                  RcvrPosCBF, RcvrAttitude, XmtrPosCBF, XmtrAttitude,...
                  ReceivedFrequency, DataRate, Bandwidth, CDMAGainValue, ReceiverGain,...
                  PolEfficiency, PolRelativeAngle, RIP, FluxDensity, GOverT, CarrierPower,...
                  BandwidthOverlap, CNo, CNR, EbNo, BER};
    
    case 'compute'
    
        computeData = input.methodData;

	% compute the Test Model : 
	% Example Model for testing only
	% CommConstraint input & output parameter usage is shown
	% 

	% USER CommConstraint MODEL AREA.
        
      time = computeData.EpochSec;
      
	  xmtrPos = computeData.XmtrPosCBF;
	  rcvrPos = computeData.RcvrPosCBF;
	  CNo = computeData.CNo;
       
	  range = norm(xmtrPos  - rcvrPos)/1000;
	  
	  % Example Transmission of 625/50 television by INTELSAT
	  		
	  	% ftpp = peak-to-peak frequency deviation (Hz)
	  		ftpp = 15.0e6;
	  	% Bn = Noise Bandwidth at Receiver (Hz)
	  		Bn = 5.0e6;
	  	% PW = improvement factor due to pre-emphasis and de-emphasis and weighting factor (dB)
	  		PW = 13.2;
	  		
	  	% SNR = 3/2 * (fttp/Bn)^2 * (1/Bn) * pw * C/No
	  		
	SNR = 10.0*log(1.5*ftpp*ftpp/(Bn*Bn*Bn))/log(10.0) + PW + CNo;

      output.PluginConstraintValue = SNR;
        
	% END OF USER MODEL AREA

    otherwise
        output = [];
end


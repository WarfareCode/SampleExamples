#-----------------------------------------------------------------------------------
#
# The Density Model being modeled is that of an exponential atmosphere, 
# exactly like the Exponential Atmosphere model
#
#-----------------------------------------------------------------------------------

# This script requires Perl 5.8.0 or higher

require 5.8.0;

use strict;
use Win32;
use Win32::OLE::Variant;

# ==========================================
#  Log Msg Type Enumeration
# ==========================================
use constant eLogMsgDebug	 	=> 0;
use constant eLogMsgInfo 		=> 1;
use constant eLogMsgForceInfo 	=> 2;
use constant eLogMsgWarning 	=> 3;
use constant eLogMsgAlarm 		=> 4;

# ==========================================
#  AgEAttrAddFlags Enumeration
# ==========================================
use constant eFlagNone			=> 0;
use constant eFlagTransparent	=> 2;
use constant eFlagHidden		=> 4;
use constant eFlagTransient		=> 8;  
use constant eFlagReadOnly		=> 16;
use constant eFlagFixed			=> 32;

use constant false	=> 0;
use constant true	=> 1;

# ==========================================
#  Declare Global Variables
# ==========================================
my $m_AgUtPluginSite	= undef;
my $m_AgAttrScope		= undef;

my $m_MsgCntr			= -1;
my $m_Enabled			= true;
my $m_DebugMode			= false;
my $m_MsgInterval		= 500;
my $m_refDen			= 1.217;
my $m_refAlt 			= 0.0;
my $m_scaleAlt 			= 8.5 * 1000.0;
my $m_Density 			= -1;
my $m_computesTemp		= false;
my $m_computesPressure  = false;
my $m_UserIndex			= 0;

my $dirpath = 'C:\Temp';
my $m_DebugFile = $dirpath.'\Exponential_Debug.txt';

sub Message
{
	my $severity = $_[0];
	my $msg = $_[1];
	
	if( defined($m_AgUtPluginSite) )
	{
		$m_AgUtPluginSite->Message( $severity, "$msg" );
	}
}

sub DebugMsg
{
	my $msg = $_[0];
	
	if($m_DebugMode)
	{
		if($m_MsgCntr % $m_MsgInterval == 0)
		{
			Message(eLogMsgDebug, "$msg");
		}
	}
}

# ========================
#  GetPluginConfig method
# ========================
sub GetPluginConfig
{
	my $AgAttrBuilder = $_[0];
	
	if( !defined($m_AgAttrScope) )
	{
		$m_AgAttrScope = $AgAttrBuilder->NewScope();
		$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, 
			"RefDensity", 
			"Reference Density", 
			"RefDensity", 0 );
		$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, 
			"RefAltitude", 
			"Reference Altitude", 
			"RefAltitude", 0 );
		$AgAttrBuilder->AddDoubleDispatchProperty ( $m_AgAttrScope, 
			"ScaleAltitude", 
			"Scale Altitude", 
			"ScaleAltitude", 0 );
		# ===========================
		#  General Plugin attributes
		# ===========================
		$AgAttrBuilder->AddBoolDispatchProperty  ( $m_AgAttrScope, 
			"PluginEnabled",
			"If the plugin is enabled or has experienced an error", 
			"Enabled",    
			eFlagNone );

		$AgAttrBuilder->AddBoolDispatchProperty  ( $m_AgAttrScope, 
			"DebugMode",
			"Turn debug messages on or off", 
			"DebugMode",    
			eFlagNone );

		$AgAttrBuilder->AddFileDispatchProperty  ( $m_AgAttrScope, 
			"DebugFile",
			"Debug output file", 
			"DebugFile", "", "*.txt",   
			eFlagNone );
		
		# ==============================
		#  Messaging related attributes
		# ==============================
		$AgAttrBuilder->AddIntDispatchProperty ( $m_AgAttrScope, 
			"MessageInterval", 
			"The interval at which to send messages during propagation in Debug mode", 
			"MsgInterval",     
			eFlagNone );
 	}

	return $m_AgAttrScope;
}  

# ===========================
#  VerifyPluginConfig method
# ===========================
sub VerifyPluginConfig
{
	my $AgUtPluginConfigVerifyResult = $_[0];
	
    my $Result = true;
    my $Message = "Ok";
    
	$AgUtPluginConfigVerifyResult->{Result}  = $Result;
	$AgUtPluginConfigVerifyResult->{Message} = $Message;
} 

# ======================
#  Register Method
# ======================
sub Register
{
	my $Result = $_[0];
	if( defined($Result) )
	{		 
		if( $m_DebugMode == true )
		{	
			$Result->Message( eLogMsgInfo, "Register() called" );
		}
	}
}

# ======================
#  Init Method
# ======================
sub Init
{
	my $AgUtPluginSite = $_[0];
	
	$m_AgUtPluginSite = $AgUtPluginSite;
	
	if( defined($m_AgUtPluginSite) )
	{		 
		if( $m_DebugMode == true )
		{
			if( $m_Enabled == true )
			{
				Message( eLogMsgInfo, "Init(): Enabled" );
			}
			else
			{
				Message( eLogMsgInfo, "Init(): Disabled because Enabled flag is False" );
			}
		}
		elsif($m_Enabled == false)
		{
			Message( eLogMsgAlarm, "Init(): Disabled because Enabled flag is False" );
		}
	}
	
    return $m_Enabled;
} 

# =================
#  Evaluate Method
# =================
sub Evaluate
{
	my $Result = $_[0];	# This is an IAgAsDensityModelResultEval interface
	
	$m_MsgCntr++;

	if($m_Enabled == true && defined($Result) )
	{
		$m_Enabled = setDensity($Result);
	}
	return $m_Enabled;
}


sub setDensity
{
	my $Result = $_[0];
	my $enabled = false;
	my $altitude = $Result->{Altitude};
	if(defined($altitude)){
		$m_Density = $m_refDen * exp(($m_refAlt - $altitude) / $m_scaleAlt);
		if(defined($m_Density))
		{
			$Result->SetDensity($m_Density);
			$enabled = true;
		}
	}
	
	return $enabled;	
}

# ===========================================================
#  Free Method
# ===========================================================
sub Free
{
	if( $m_DebugMode == true )
	{
		Message( eLogMsgDebug, "Free(): MsgCntr( $m_MsgCntr )" );
	}

	$m_AgUtPluginSite 		= undef;
}

# ============================================================
#  Computes Temperature property
# ============================================================
sub ComputesTemperature
{
	return $m_computesTemp;
}

# ============================================================
#  Computes Pressure property
# ============================================================
sub ComputesPressure
{
	return $m_computesPressure;
}


# ============================================================
#  ...
# ============================================================
sub CentralBody
{
	return "Earth";
}
sub UsesAugmentedSpaceWeather
{
	return false;
}
sub AtmFluxLags
{
	return false;
}
sub AugmentedAtmFluxLags
{
	return false;
}


# ============================================================
#  Enabled property
# ============================================================
sub GetEnabled
{
	return $m_Enabled;
}

sub SetEnabled
{
	$m_Enabled = $_[0];
}

# ======================================================
#  MsgStatus property
# ======================================================
sub GetDebugMode
{
	return $m_DebugMode;
}

sub SetDebugMode
{
    $m_DebugMode = $_[0];
}


# ======================================================
#  DebugFile property
# ======================================================
sub GetDebugFile
{
	return $m_DebugFile;
}

sub SetDebugFile
{
    $m_DebugFile = $_[0];
}

# =======================================================
#  EvalMsgInterval property
# =======================================================
sub GetMsgInterval
{
	return $m_MsgInterval;
}

sub SetMsgInterval
{
	$m_MsgInterval = $_[0];
}

# =======================================================
#  RefDen property
# =======================================================
sub GetRefDensity
{
	return $m_refDen;
}

sub SetRefDensity
{
	$m_refDen = $_[0];
}

# =======================================================
#  RefAlt property
# =======================================================
sub GetRefAltitude
{
	return $m_refAlt;
}

sub SetRefAltitude
{
	$m_refAlt = $_[0];
}

# =======================================================
#  ScaleAlt property
# =======================================================
sub GetScaleAltitude
{
	return $m_scaleAlt;
}

sub SetScaleAltitude
{
	$m_scaleAlt = $_[0];
}
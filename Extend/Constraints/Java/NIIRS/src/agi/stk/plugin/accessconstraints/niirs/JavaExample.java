package agi.stk.plugin.accessconstraints.niirs;

import java.util.logging.*;
import java.util.*;

import agi.core.*;
import agi.stk.attr.*;
import agi.stk.plugin.util.*;
import agi.stk.plugin.accessconstraints.*;
import agi.stk.plugin.crdn.*;

/**>
* Constraint Plugin component NIIRS_Constraint
* Original Author:     	Tom Johnson
* Converted to C#  by:	Vince Coppola
* Converted to Java by:	Matt Ward
* Company:    Analytical Graphics, Inc.
* Copyright:  None.  Modify and distribute at will
*
* Description:
* 
* This constraint is registered only for Facilities/Targets when doing
* Access to a Sensor.
*
* This constraint calculates a modified form of the NIIRS image quality
* metric.  It's modified in that it doesn't calculate all the terms since
* I didn't have enough information to do so.  The first step is to calculate
* the Ground Sample Distance (GSD) for a sensor
* viewing another object.  This version of the GSD equation is parameterized
* in terms of Q, the optical ratio. Reference document is 
* Image Quality and lamdaFN/p For Remote Sensing Systems, Robert D. Fiete, 
* Optical Engineering, Vol. 38 No 7, July 1999
*
*                  SR * lamda
*       GSD =  --------------------------
*              Q * D * Math.Sqrt( sin(elev)  )
*
* where
*
*       SR = slant range (meters) from STK
*
*       lamda = wavelenth of the sensor you are observing with
*
*       Q     = optical ratio (unitless)
*
*       D     = optical diameter (meters)
*
*       elev  = elevation angle between the ground object and the sensor (from STK)
*
* The assumption is that the user provides lamda, Q, and D within the script and
* STK does the rest.  GSD is returned in meters.  Then we pass it into the NIIRS
* equation and return the NIIRS value.
*/
public class JavaExample 
implements IJavaExample, 
		   IAgAccessConstraintPlugin,
		   IAgUtPluginConfig
{
	/**
	* The logger name for all log calls in this class.
	*/
	private static final String s_LoggerName = JavaExample.class.getName();

	/**
	* The Logger for this class type.
	*/
	private static final Logger s_Logger = Logger.getLogger( s_LoggerName );

	private String											m_DisplayName;
	private IAgUtPluginSite 								m_IAgUtPluginSite;
	private IAgDispatch										m_ConfigScope;
	private Hashtable<String, IAgCrdnConfiguredAxes>	    m_AxesHash;

	// sensor properties
	private double m_lamda;		// Sensor wavelenth (microns)
	private double m_diameter;	// Optical diameter (meters)
	private double m_Q;			// Optical ratio (unitless). 

	///  Optical ratio (unitless). 
	/// 
	///                 f
	///    Q = lamda * ---
	///                 D
	///        ------------
	///              pp
	/// 
	///    where
	/// 
	///    f = focal length (meters)
	/// 
	///    pp = pixel pitch (meters)
	/// 

	// NIIRS coefficients
	private double m_a;
	private double m_b;
	private double m_logRER;

	// debug
	private boolean	m_DebugMode;
	private int	m_MsgInterval;

	// conversions
	private static double s_micron2meter = 1.0e-6;
	private static double s_meters2inches = 39.37007874015876;

	// ellipsoid shape
	private static double s_invASqr = 1.0 /(6378137.0 * 6378137.0);
	private static double s_invCSqr = 1.0 /(6356752.31424 * 6356752.31424);

	public JavaExample()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "<init>", "ENTER" );

		this.m_DisplayName = "Java.NIIRS";

		this.m_AxesHash = new Hashtable<String, IAgCrdnConfiguredAxes>();
		
		this.m_lamda = 0.65;
		this.m_diameter = 10.0;
		this.m_Q = 2.0;
		this.m_a = 3.32;
		this.m_b = 1.559;

		//http://mindprod.com/jgloss/logarithms.html
		this.m_logRER = Math.log(0.9)/Math.log(10);

		// NOTE: if true, will output a msg when entering events other than Evaluate().
		//
		// DON'T set to true when using constraint as a
		// Figure of Merit,because PreCompute() and PostCompute()
		// are called once per animation step, which will cause
		// lots of messages to be written to the Message Viewer.
		this.m_DebugMode = false;	

		this.m_MsgInterval = 100;

		s_Logger.logp(Level.FINEST, s_LoggerName, "<init>", "EXIT" );
	}

	//===================================
	//  IJavaExample
	//===================================
	public Object getDiameter()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDiameter", "m_diameter={0}", new Double(this.m_diameter) );
		return new Double(this.m_diameter);
	}
	public void setDiameter(Object value)
	{
		this.m_diameter = ((Double)value).doubleValue();
		s_Logger.logp(Level.FINEST, s_LoggerName, "setDiameter", "m_diameter={0}", new Double(this.m_diameter) );
	}
	
	public Object getWavelength()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getWavelength", "m_lamda={0}", new Double(this.m_lamda) );
		return new Double(this.m_lamda);
	}
	public void setWavelength(Object value)
	{
		this.m_lamda = ((Double)value).doubleValue();
		s_Logger.logp(Level.FINEST, s_LoggerName, "setWavelength", "m_lamda={0}", new Double(this.m_lamda) );
	}
	
	public Object getOpticalRatio()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getOpticalRatio", "m_Q={0}", new Double(this.m_Q) );
		return new Double(this.m_Q);
	}
	public void setOpticalRatio(Object value)
	{
		this.m_Q = ((Double)value).doubleValue();
		s_Logger.logp(Level.FINEST, s_LoggerName, "setOpticalRatio", "m_Q={0}", new Double(this.m_Q) );
	}
	
	public Object getNIIRS_a()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getNIIRS_a", "m_a={0}", new Double(this.m_a) );
		return new Double(this.m_a);
	}
	public void setNIIRS_a(Object value)
	{
		this.m_a = ((Double)value).doubleValue();
		s_Logger.logp(Level.FINEST, s_LoggerName, "setNIIRS_a", "m_a={0}", new Double(this.m_a) );
	}
	
	public Object getNIIRS_b()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getNIIRS_b", "m_b={0}", new Double(this.m_b) );
		return new Double(this.m_b);
	}
	public void setNIIRS_b(Object value)
	{
		this.m_b = ((Double)value).doubleValue();
		s_Logger.logp(Level.FINEST, s_LoggerName, "setNIIRS_b", "m_b={0}", new Double(this.m_b) );
	}
	
	public Object getNIIRS_RER()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getNIIRS_RER", "m_logRER={0}", new Double(this.m_logRER) );
		return new Double(Math.pow(10, this.m_logRER));
	}
	public void setNIIRS_RER(Object value)
	{
		if(((Double)value).doubleValue() > 0.0){ this.m_logRER = Math.log(((Double)value).doubleValue())/Math.log(10);}
		s_Logger.logp(Level.FINEST, s_LoggerName, "setNIIRS_RER", "m_logRER={0}", new Double(this.m_logRER) );
	}
	
	public Object getDebugMode()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDebugMode", "m_DebugMode={0}", new Boolean(this.m_DebugMode) );
		return new Boolean(this.m_DebugMode);
	}
	public void setDebugMode(Object value)
	{
		this.m_DebugMode = ((Boolean)value).booleanValue();
		s_Logger.logp(Level.FINEST, s_LoggerName, "setDebugMode", "m_DebugMode={0}", new Boolean(this.m_DebugMode) );
	}
	
	public Object getMsgInterval()
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getMsgInterval", "m_MsgInterval={0}", new Integer(this.m_MsgInterval) );
		return new Integer(this.m_MsgInterval);
	}
	public void setMsgInterval(Object value)
	{
		this.m_MsgInterval = ((Integer)value).intValue();
		s_Logger.logp(Level.FINEST, s_LoggerName, "setMsgInterval", "m_MsgInterval={0}", new Integer(this.m_MsgInterval) );
	}

	//===================================
	//  IAgUtPluginConfig
	//===================================
	public IAgDispatch getPluginConfig(IAgAttrBuilder builder)
	throws AgCoreException 
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getPluginConfig", "ENTER");
		if(this.m_ConfigScope == null)
		{
			this.m_ConfigScope = builder.newScope();
			getPluginProperties(builder, this.m_ConfigScope);
			getDebugProperties(builder, this.m_ConfigScope);
		}
		s_Logger.logp(Level.FINEST, s_LoggerName, "getPluginConfig", "EXIT");

		return this.m_ConfigScope;
	}

	private void getPluginProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getPluginProperties", "ENTER");

		IAgDispatch pluginPropScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"Plugin Properties", 
		"The properties of the plugin", 
		pluginPropScope);

		builder.addQuantityDispatchProperty2
		(pluginPropScope, 	
		"Diameter", 
		"Optical diameter", 
		"Diameter", // get/set will be prefixed to this name
		"DistanceUnit",
		"Meters",
		"Meters",
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue() );

		builder.addQuantityDispatchProperty2
		(pluginPropScope, 
		"Wavelength", 
		"Wavelength of the sensor you are observing with", 
		"Wavelength", // get/set will be prefixed to this name
		"DistanceUnit",
		"Meters",
		"Meters",
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue() );

		builder.addDoubleDispatchProperty
		(pluginPropScope, 
		"OpticalRatio", 
		"Optical ratio (Q)", 
		"OpticalRatio", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue() );

		builder.addDoubleDispatchProperty
		(pluginPropScope, 
		"NIIRS_a", 
		"The a coefficient for the NIIRS equation", 
		"NIIRS_a", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue() );

		builder.addDoubleDispatchProperty
		(pluginPropScope, 
		"NIIRS_b", 
		"The b coefficient for the NIIRS equation", 
		"NIIRS_b", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue() );

		builder.addDoubleDispatchProperty
		(pluginPropScope, 
		"NIIRS_RER", 
		"The RER coefficient for the NIIRS equation", 
		"NIIRS_RER", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue() );

		s_Logger.logp(Level.FINEST, s_LoggerName, "getPluginProperties", "EXIT");
	}
	
	private void getDebugProperties(IAgAttrBuilder builder, IAgDispatch parentScope)
	throws AgCoreException
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDebugProperties", "ENTER");

		IAgDispatch debugPropScope = builder.newScope();
		
		builder.addScopeDispatchProperty
		(parentScope, 
		"Debug Properties", 
		"The debug properties of the plugin", 
		debugPropScope);

		builder.addBoolDispatchProperty
		(debugPropScope,
		"DebugMode", 
		"Turn debug messages on or off", 
		"DebugMode",	// get/set will be prefixed to this name 
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue() );

		builder.addIntDispatchProperty
		(debugPropScope, 
		"MessageInterval", 
		"The interval at which to send messages during propagation in Debug mode", 
		"MsgInterval", // get/set will be prefixed to this name
		AgEAttrAddFlags.E_ADD_FLAG_NONE.getValue() );

		s_Logger.logp(Level.FINEST, s_LoggerName, "getDebugProperties", "EXIT");
	}
	
	public void verifyPluginConfig(IAgUtPluginConfigVerifyResult apcvr)
	throws AgCoreException 
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "verifyPluginConfig", "ENTER");

		boolean	result	= true;
		String	message = "Ok";

		if(m_diameter < 0.000001)
		{
			result = false;
			message = "Diameter is too small.";
		}
		
		apcvr.setResult(result);
		apcvr.setMessage(message);

		s_Logger.logp(Level.FINEST, s_LoggerName, "verifyPluginConfig", "EXIT");
	}

	//===================================
	//  IAgAccessConstraintPlugin
	//===================================
	public String getDisplayName() 
	throws AgCoreException 
	{ 
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDisplayName", "ENTER");
		s_Logger.logp(Level.FINEST, s_LoggerName, "getDisplayName", "EXIT");
		return this.m_DisplayName;
	}

	public void register(IAgAccessConstraintPluginResultRegister result)
	throws AgCoreException 
	{
		try
		{
			s_Logger.logp(Level.FINEST, s_LoggerName, "register", "ENTER");

			result.setBaseObjectType(AgEAccessConstraintObjectType.E_FACILITY);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_RELATIVE_POS_VEL.getValue());
			result.setDimension("Unitless");
			result.setMinValue(0.0);

			result.setTargetDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_NONE.getValue());
			result.addTarget(AgEAccessConstraintObjectType.E_SENSOR);
			result.register();

			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(Facility to Sensor)");
			
			result.setBaseObjectType(AgEAccessConstraintObjectType.E_TARGET);
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Register(Target to Sensor)");
		}
		catch(AgCoreException t)
		{
			t.printStackTrace();
			s_Logger.logp(Level.FINEST, s_LoggerName, "register", "Exception", t);
			throw t;
		}
		finally
		{
			s_Logger.logp(Level.FINEST, s_LoggerName, "register", "EXIT");
		}
	}

	public boolean init(IAgUtPluginSite pluginSite) 
	throws AgCoreException 
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "init", "ENTER");

		boolean succeeded = false;
		try
		{
			this.m_IAgUtPluginSite = pluginSite;

			if(this.m_IAgUtPluginSite != null && this.m_DebugMode)
			{
				this.Message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Init()");
			}
			//m_AxesHash.Clear();
			succeeded = true;
		}
		catch(Throwable t)
		{
			t.printStackTrace();
			succeeded = false;
		}
		finally
		{
			s_Logger.logp(Level.FINEST, s_LoggerName, "init", "EXIT");
		}

		return succeeded;
	}

	public boolean preCompute(IAgAccessConstraintPluginResultPreCompute result)
	throws AgCoreException 
	{
		s_Logger.logp(Level.FINEST, s_LoggerName, "preCompute", "ENTER");

		boolean succeeded = false;

		// Get the topocentric Axes from the Vector Tool,
		// for the Facility/Target, to be used in the computation later

		IAgAccessConstraintPluginObjectDescriptor baseDesc = null;
		baseDesc = result.getBase();
		String basePath = baseDesc.getObjectPath();

		if(basePath != "")
		{
			if(!m_AxesHash.containsKey(basePath))
			{
				IAgCrdnPluginProvider vgtProvider = null;
				vgtProvider = baseDesc.getVectorToolProvider();

				if(vgtProvider != null)
				{
					String cbName = "Earth";
					cbName = baseDesc.getCentralBodyName();
					cbName = "CentralBody/"+cbName; 

					IAgCrdnConfiguredAxes topoAxes = null;
					topoAxes = vgtProvider.configureAxes("Fixed", cbName, "TopoCentric", "");

					if(topoAxes != null && topoAxes.getIsConfigured())
					{
						m_AxesHash.put(basePath, topoAxes);
					}
				}
			}
		}

		if(this.m_IAgUtPluginSite != null && this.m_DebugMode)
		{
			this.Message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": PreCompute()");
		}
	    succeeded = true;

	    s_Logger.logp(Level.FINEST, s_LoggerName, "preCompute", "EXIT");

		return succeeded;
	}

	public boolean evaluate(IAgAccessConstraintPluginResultEval result,
			IAgAccessConstraintPluginObjectData baseObj,
			IAgAccessConstraintPluginObjectData targetObj) 
	throws AgCoreException 
	{
	    s_Logger.logp(Level.FINEST, s_LoggerName, "evaluate", "ENTER");

	    if(result != null)
		{
			result.setValue(0.0);
		
			if(baseObj != null)
			{
				double range = baseObj.range(AgEAccessApparentPositionType.E_PROPER_APPARENT_POSITION);
				
				double niirs = -1.0;
				double relX = 0.0;
				double relY = 0.0;
				double relZ = 0.0;

				Object[] sa = (Object[])baseObj.relativePosition_Array_AsObject(
					AgEAccessApparentPositionType.E_PROPER_APPARENT_POSITION,
					AgEUtFrame.E_UT_FRAME_FIXED);

				relX = ((Double)sa[0]).doubleValue();
				relY = ((Double)sa[1]).doubleValue();
				relZ = ((Double)sa[2]).doubleValue();
				           
				boolean usePosToComputeNIIRS = true;

				IAgAccessConstraintPluginObjectDescriptor baseDesc = null;
				baseDesc = baseObj.getDescriptor();
				String basePath = baseDesc.getObjectPath();

				if(basePath != "" && m_AxesHash.containsKey(basePath))
				{
					IAgCrdnConfiguredAxes topoAxes = null;
					topoAxes = (IAgCrdnConfiguredAxes) m_AxesHash.get(basePath);

					if(topoAxes != null)
					{
						Object[] sa2 = null;
						sa2 = (Object[])topoAxes.transformComponents_Array_AsObject(new AgDispatch(baseObj), relX, relY, relZ);

						relX = ((Double)sa2[0]).doubleValue();
						relY = ((Double)sa2[1]).doubleValue();
						relZ = ((Double)sa2[2]).doubleValue();
						
						double sinElev = relZ / range;

						niirs = computeNIIRS(range, sinElev);

						usePosToComputeNIIRS = false;
					}
				}
					
				if(usePosToComputeNIIRS)
				{
					// will only work with Facility/Targets on Earth
					double x = 0.0;
					double y = 0.0;
					double z = 0.0;
					
					Object[] sa3 = null;
					sa3 = (Object[])baseObj.position_Array_AsObject(AgEUtFrame.E_UT_FRAME_FIXED);
					
					x = ((Double)sa3[0]).doubleValue();
					x = ((Double)sa3[1]).doubleValue();
					x = ((Double)sa3[2]).doubleValue();
					
					niirs = computeNIIRSFromPos(range, x, y, z, relX, relY, relZ);
				}
				Message(AgEUtLogMsgType.E_UT_LOG_MSG_DEBUG, this.m_DisplayName+": Evaluate() : niirs="+niirs);
				result.setValue(niirs);
			}
		}
	    s_Logger.logp(Level.FINEST, s_LoggerName, "evaluate", "EXIT");

		return true;
	}

	public boolean postCompute(IAgAccessConstraintPluginResultPostCompute result)
	throws AgCoreException 
	{
	    s_Logger.logp(Level.FINEST, s_LoggerName, "postCompute", "ENTER");
		if(this.m_IAgUtPluginSite != null && this.m_DebugMode)
		{
			Message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": PostCompute()");
		}
	    s_Logger.logp(Level.FINEST, s_LoggerName, "postCompute", "EXIT");
		return true;
	}

	public void free() 
	throws AgCoreException 
	{
	    s_Logger.logp(Level.FINEST, s_LoggerName, "free", "ENTER");
		this.m_AxesHash.clear();
		this.m_AxesHash = null;

		if(this.m_IAgUtPluginSite != null && this.m_DebugMode)
		{
			this.Message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": Free()");
		}
	    s_Logger.logp(Level.FINEST, s_LoggerName, "free", "EXIT");
		this.m_IAgUtPluginSite = null;
	}
	
	/**
	 * Logs a message to the STK Message Viewer
	 * @param severity One of the final static members of AgEUtLogMsgType.
	 * @param msg The message to display in the message viewer.
	 * @throws AgCoreException If an error occurred while logging the message. 
	 */
	private void Message(AgEUtLogMsgType severity, String msg)
	throws AgCoreException
	{
		if(this.m_IAgUtPluginSite != null)
		{
			this.m_IAgUtPluginSite.message(severity, msg);
		}
	}
	
	//======================
	// NIIRS computations
	//======================
	public double computeNIIRS(double range, double sinElev)
	{
		double NIIRS = -1.0;

		// Check for negative elevation angle (e.g. z component of relative
		// position vector is negative.  If so, bail out gracefully.

		if (sinElev > 0.0 )
		{
			// Now calculate the GSD. 

			double GSD = range * m_lamda * s_micron2meter / (m_Q * m_diameter * Math.sqrt(sinElev));
    
			// Now calculate the NIIRS value.  
			// Terms after the RER are truncated since I don't have any values
			// to use for them.
    
			// NIIRS value
           
			//http://mindprod.com/jgloss/logarithms.html
			NIIRS = 10.251 - m_a * (Math.log(GSD * s_meters2inches)/Math.log(10)) + m_b * m_logRER;
		}

		return NIIRS;
	}

	public double computeNIIRSFromPos
	(double range, double x, double y, double z,
	 double relX, double relY, double relZ)
	{
		double sinElev = computeSinElev(x, y, z, relX, relY, relZ);

		double NIIRS = computeNIIRS(range, sinElev);

		return NIIRS;
	}

	public double computeSinElev
	(double x, double y, double z,
	 double relX, double relY, double relZ)
	{
		// this rotuine computes the detic elevation angle (ie the angle
		// from the tangent plane at the Facility/Target to the Sensor,
		// where the tangent plane is tangent to the Earth as represented by the
		// WGS84 oblate spheroid)

		double sinElev = 0.0;

		double[] normals = {0.0, 0.0, 0.0};

		normals = computeSurfNormalVec(x, y, z);

		double relPosNormal = normals[0] * relX + normals[2] * relY + normals[3] * relZ;

		double magRelPos = Math.sqrt( relX * relX + relY * relY + relZ * relZ );

		sinElev = relPosNormal / magRelPos;

		return sinElev;
	}

	public double[] computeSurfNormalVec(double x, double y, double z)
	{
		double[] normals = {0.0, 0.0, 1.0};

		double tempXY = ( x*x + y*y ) * s_invASqr;
		double tempZ  = z*z*s_invCSqr;

		double alpha = Math.sqrt(tempXY + tempZ);

		if(alpha > 0.1)
		{
			double radx = x / alpha;
			double rady = y / alpha;
			double radz = z / alpha;

			double normx = radx * s_invASqr;
			double normy = rady * s_invASqr;
			double normz = radz * s_invCSqr;

			double normMag = Math.sqrt( normx*normx + normy*normy + normz*normz);

			double radMag = Math.sqrt( x*x + y*y + z*z);

			double lam = (1.0 - 1.0 / alpha) * radMag / normMag;

			// Newton iteration

			double xyDenom  = 1.0 / (1.0 + lam*s_invASqr);
			double zDenom   = 1.0 / (1.0 + lam*s_invCSqr);

			double xyDenom2, zDenom2;

			double F = 1.0; double dF;

			while (Math.abs(F) > 1.0e-12)
			{
				xyDenom2 = xyDenom*xyDenom;
				zDenom2  = zDenom*zDenom;

				/*
				 * The function F is the surface constraint, dF is the
				 * partial of F with respect to alpha
				 */
				F  = tempXY*xyDenom2 + tempZ*zDenom2 - 1.0;
				dF = -2.0 * (tempXY*xyDenom2*xyDenom*s_invASqr + 
					tempZ*zDenom2*zDenom*s_invCSqr);

				/* Newton - Raphson update */
				alpha = alpha - F/dF;

				xyDenom  = 1.0 / (1.0 + alpha*s_invASqr);
				zDenom   = 1.0 / (1.0 + alpha*s_invCSqr);
			}

			/*
			 * The detic vector is computed via equations of the form
			 * Sx = Px / (1.0 + alpha/a^2)
			 * which come from the relation Sx + alpha (Sx/a^2) = Px
			 */
			double deticx = x * xyDenom;
			double deticy = y * xyDenom;
			double deticz = z * zDenom;

			// surface normal

			normals[0] = deticx * s_invASqr;
			normals[1] = deticy * s_invASqr;
			normals[2] = deticz * s_invCSqr;

			double nMag = Math.sqrt(normals[0] * normals[0] + 
									normals[1] * normals[1] + 
									normals[2] * normals[2]);

			normals[0] /= nMag;
			normals[1] /= nMag;
			normals[2] /= nMag;
		}
		
		return normals;
	}
}

package agi.stk.plugin.accessconstraints.range;

import agi.core.*;
import agi.stk.attr.*;
import agi.stk.plugin.util.*;
import agi.stk.plugin.accessconstraints.*;
import agi.stk.plugin.stk.*;
import agi.stkobjects.*;

public class JavaExample 
implements IJavaExample, 
		   IAgAccessConstraintPlugin,
		   IAgUtPluginConfig
{
	private IAgStkPluginSite m_IAgStkPluginSite;
	private IAgStkObjectRoot m_IAgStkObjectRoot;
	private String			 m_DisplayName;
	
	public JavaExample()
	{
		this.m_DisplayName = "Java.Range";
	}

	//===================================
	//  IAgAccessConstraintPlugin
	//===================================
	public String getDisplayName() 
	throws AgCoreException 
	{ 
		return this.m_DisplayName;
	}

	public void register(IAgAccessConstraintPluginResultRegister result)
	throws AgCoreException 
	{
		try
		{
			//================
			// Aircraft
			//================
			result.setBaseObjectType(AgEAccessConstraintObjectType.E_AIRCRAFT);
			result.setBaseDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_POS_VEL.getValue());
			result.setDimension("Distance");
			result.setMinValue(0.0);
			result.setTargetDependency(AgEAccessConstraintDependencyFlags.E_DEPENDENCY_POS_VEL.getValue());
			result.addTarget(AgEAccessConstraintObjectType.E_FACILITY);
			result.addTarget(AgEAccessConstraintObjectType.E_GROUND_VEHICLE);
			result.addTarget(AgEAccessConstraintObjectType.E_SATELLITE);
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": register(Aircraft to Facility/GroundVehicle/Satellite)");
			
			//================
			// Facility
			//================
			result.setBaseObjectType(AgEAccessConstraintObjectType.E_FACILITY);
			result.clearTargets();
			result.addTarget(AgEAccessConstraintObjectType.E_AIRCRAFT);
			result.addTarget(AgEAccessConstraintObjectType.E_SATELLITE);
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": register(Facility to Aircraft/Satellite)");
			
			//================
			// GroundVehicle
			//================
			result.setBaseObjectType(AgEAccessConstraintObjectType.E_GROUND_VEHICLE);
			result.clearTargets();
			result.addTarget(AgEAccessConstraintObjectType.E_AIRCRAFT);
			result.addTarget(AgEAccessConstraintObjectType.E_SATELLITE);
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": register(GroundVehicle to Aircraft/Satellite)");
	
			//================
			// Satellite
			//================
			result.setBaseObjectType(AgEAccessConstraintObjectType.E_SATELLITE);
			result.clearTargets();
			result.addTarget(AgEAccessConstraintObjectType.E_AIRCRAFT);
			result.addTarget(AgEAccessConstraintObjectType.E_FACILITY);
			result.addTarget(AgEAccessConstraintObjectType.E_GROUND_VEHICLE);
			result.register();
			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": register(Satellite to Aircraft/Facility/GroundVehicle)");
		}
		catch(Throwable t)
		{
			String msg = t.getMessage();
			StackTraceElement[] elem = t.getStackTrace();

			result.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "Exception Occurred! Message = "+msg);
			
			for(int i = 0; i < elem.length; i++)
			{
				int lineNo = elem[i].getLineNumber();
				String clsName = elem[i].getClassName();
				result.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "StackTrace["+i+"] (lineNo="+lineNo+", clsName="+clsName+")");
			}
			
			throw new AgCoreException(t);
		}
	}

	public boolean init(IAgUtPluginSite pluginSite) 
	throws AgCoreException 
	{
		boolean succeeded = false;
		try
		{
			this.m_IAgStkPluginSite = (IAgStkPluginSite)pluginSite;

			this.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": init()");

			if(this.m_IAgStkPluginSite != null)
			{
				this.m_IAgStkObjectRoot = new AgStkObjectRootClass(this.m_IAgStkPluginSite.getStkRootObject());
			}
			
			succeeded = true;
		}
		catch(Throwable t)
		{
			String msg = t.getMessage();
			StackTraceElement[] elem = t.getStackTrace();

			this.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "Exception Occurred! Message = "+msg);
			
			for(int i = 0; i < elem.length; i++)
			{
				int lineNo = elem[i].getLineNumber();
				String clsName = elem[i].getClassName();
				this.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "StackTrace["+i+"] (lineNo="+lineNo+", clsName="+clsName+")");
			}
			
			succeeded = false;

			throw new AgCoreException(t);
		}

		return succeeded;
	}

	public boolean preCompute(IAgAccessConstraintPluginResultPreCompute result)
	throws AgCoreException 
	{
		boolean succeeded = false;

		try
		{
			this.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": preCompute()");
			
			// Demonstrate using ObjectModel handle
			if(this.m_IAgStkObjectRoot != null)
			{
				AgScenarioClass scenObj = null;
				scenObj = (AgScenarioClass)this.m_IAgStkObjectRoot.getCurrentScenario();
				String scenName = scenObj.getInstanceName();
				this.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, "Current Scenario is "+scenName);
			    succeeded = true;
			}
		}
		catch(Throwable t)
		{
			String msg = t.getMessage();
			StackTraceElement[] elem = t.getStackTrace();

			this.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "Exception Occurred! Message = "+msg);
			
			for(int i = 0; i < elem.length; i++)
			{
				int lineNo = elem[i].getLineNumber();
				String clsName = elem[i].getClassName();
				this.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "StackTrace["+i+"] (lineNo="+lineNo+", clsName="+clsName+")");
			}
			
			succeeded = false;

			throw new AgCoreException(t);
		}

		return succeeded;
	}

	public boolean evaluate(IAgAccessConstraintPluginResultEval result,
			IAgAccessConstraintPluginObjectData fromObject,
			IAgAccessConstraintPluginObjectData toObject) 
	throws AgCoreException 
	{
		boolean succeeded = false;

		try
		{
			if(result != null)
			{
				double range = result.getLightPathRange();
				result.setValue(range);
				succeeded = true;
			}
		}	
		catch(Throwable t)
		{
			String msg = t.getMessage();
			StackTraceElement[] elem = t.getStackTrace();

			this.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "Exception Occurred! Message = "+msg);
			
			for(int i = 0; i < elem.length; i++)
			{
				int lineNo = elem[i].getLineNumber();
				String clsName = elem[i].getClassName();
				this.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "StackTrace["+i+"] (lineNo="+lineNo+", clsName="+clsName+")");
			}
			
			succeeded = false;

			throw new AgCoreException(t);
		}

		return succeeded;
	}

	public boolean postCompute(IAgAccessConstraintPluginResultPostCompute arg0)
	throws AgCoreException 
	{
		boolean succeeded = false;

		try
		{
			this.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": postCompute()");
			succeeded = true;
		}
		catch(Throwable t)
		{
			String msg = t.getMessage();
			StackTraceElement[] elem = t.getStackTrace();

			this.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "Exception Occurred! Message = "+msg);
			
			for(int i = 0; i < elem.length; i++)
			{
				int lineNo = elem[i].getLineNumber();
				String clsName = elem[i].getClassName();
				this.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "StackTrace["+i+"] (lineNo="+lineNo+", clsName="+clsName+")");
			}
			
			succeeded = false;

			throw new AgCoreException(t);
		}

		return succeeded;
	}

	public void free() 
	throws AgCoreException 
	{
		try
		{
			this.message(AgEUtLogMsgType.E_UT_LOG_MSG_INFO, this.m_DisplayName+": free()");
			this.m_IAgStkPluginSite.release();
			this.m_IAgStkPluginSite = null;
			this.m_IAgStkObjectRoot.release();
			this.m_IAgStkObjectRoot = null;
		}
		catch(Throwable t)
		{
			String msg = t.getMessage();
			StackTraceElement[] elem = t.getStackTrace();

			this.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "Exception Occurred! Message = "+msg);
			
			for(int i = 0; i < elem.length; i++)
			{
				int lineNo = elem[i].getLineNumber();
				String clsName = elem[i].getClassName();
				this.message(AgEUtLogMsgType.E_UT_LOG_MSG_ALARM, "StackTrace["+i+"] (lineNo="+lineNo+", clsName="+clsName+")");
			}

			throw new AgCoreException(t);
		}
	}
	
	/**
	 * Logs a message to the STK Message Viewer
	 * @param severity One of the final static members of AgEUtLogMsgType.
	 * @param msg The message to display in the message viewer.
	 * @throws AgCoreException If an error occurred while logging the message. 
	 */
	private void message(AgEUtLogMsgType severity, String msg)
	throws AgCoreException
	{
		if(this.m_IAgStkPluginSite != null)
		{
			((IAgUtPluginSite)this.m_IAgStkPluginSite).message(severity, msg);
		}
	}

	public IAgDispatch getPluginConfig(IAgAttrBuilder builder)
	throws AgCoreException 
	{
		try
		{
			// Dont' wish to have any configuration
			return builder.newScope();
		}
		catch(Throwable t)
		{
			throw new AgCoreException(t);
		}
	}

	public void verifyPluginConfig(IAgUtPluginConfigVerifyResult result)
	throws AgCoreException 
	{
		try
		{
			result.setResult(true);
			result.setMessage("Ok");
		}
		catch(Throwable t)
		{
			String msg = t.getMessage();
			result.setMessage("Exception Occurred! Message = "+msg);
			throw new AgCoreException(t);
		}
	}
}

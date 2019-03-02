package main;

import java.io.*;
import org.eclipse.core.commands.*;
import org.eclipse.swt.*;
import org.eclipse.swt.widgets.*;
import agi.core.*;
import agi.stkutil.*;
import agi.stkobjects.*;
import agi.stkx.*;

public class ScenarioHandler 
extends AbstractHandler 
implements IHandler 
{
	public Object execute(ExecutionEvent event) 
	throws ExecutionException 
	{
		try
		{
			if(event.getCommand().getId().equalsIgnoreCase("newScenario.command"))
			{
				String name = "CustomApp_SWT_STK_X_Eclipse_RCP";
	
				try
				{
	    			MainActivator.getRoot().closeScenario();
	    			MainActivator.getRoot().newScenario( name );
					IAgStkObject scenObject = MainActivator.getRoot().getCurrentScenario();
					IAgScenario m_IAgScenario = new AgScenario(scenObject);
					
	            	String startTime = "1 Jul 2009 15:00:00.000";
	            	String stopTime = "2 Jul 2009 15:00:00.000";
	        		m_IAgScenario.setTimePeriod( startTime, stopTime );
					
					IAgStkObjectCollection scenChildren = scenObject.getChildren();
					scenChildren._new( AgESTKObjectType.eFacility, "fac1" );
						
					IAgStkObject aircraftObject = scenChildren._new( AgESTKObjectType.E_AIRCRAFT, "aircraft1" );
					IAgAircraft ac = new AgAircraft(aircraftObject);
					
					//==================
		    		// Set 2D Graphics
					//==================
					IAgAcGraphics 			acgfx 			= ac.getGraphics();
		    		IAgVeGfxAttributes  	gfxAttr 		= acgfx.getAttributes();
		    		IAgVeGfxAttributesRoute gfxAttrRoute 	= new AgVeGfxAttributesRoute( gfxAttr );
		    		gfxAttrRoute.setColor(0xFFFF00);
		    		gfxAttrRoute.setIsVisible(true);		// default already set
		    		
					//=======================================
		    		// Propagate the Aircraft with Waypoints
					//=======================================
		    		ac.setRouteType(AgEVePropagatorType.E_PROPAGATOR_GREAT_ARC);
		    		IAgVePropagator			prop = ac.getRoute();
		    		IAgVePropagatorGreatArc gaProp = new AgVePropagatorGreatArc( prop );
		    		gaProp.setMethod(AgEVeWayPtCompMethod.eDetermineVelFromTime);
	
		    		IAgVeWaypointsElement waypoint = gaProp.getWaypoints().add();
	
		    		waypoint.setTime("1 Jul 2009 15:00:00.000");
		    		waypoint.setLatitude(new Double(36.406));
		    		waypoint.setLongitude(new Double(-100.212));
	
		    		waypoint = gaProp.getWaypoints().add();
		    		waypoint.setTime("1 Jul 2009 17:00:00.000");
		    		waypoint.setLatitude(new Double(36.406));
		    		waypoint.setLongitude(new Double(-87.212));
	
		    		waypoint = gaProp.getWaypoints().add();
		    		waypoint.setTime("1 Jul 2009 18:00:00.000");
		    		waypoint.setLatitude(new Double(38.852));
		    		waypoint.setLongitude(new Double(-80.941));
	
		    		waypoint = gaProp.getWaypoints().add();
		    		waypoint.setTime("1 Jul 2009 18:30:00.000");
		    		waypoint.setLatitude(new Double(38.835));
		    		waypoint.setLongitude(new Double(-80.066));
	
		    		waypoint = gaProp.getWaypoints().add();
		    		waypoint.setTime("1 Jul 2009 19:00:00.000");
		    		waypoint.setLatitude(new Double(38.368));
		    		waypoint.setLongitude(new Double(-79.401));
	
		    		waypoint = gaProp.getWaypoints().add();
		    		waypoint.setTime("1 Jul 2009 19:30:00.000");
		    		waypoint.setLatitude(new Double(37.902));
		    		waypoint.setLongitude(new Double(-79.233));
	
		    		waypoint = gaProp.getWaypoints().add();
		    		waypoint.setTime("1 Jul 2009 20:00:00.000");
		    		waypoint.setLatitude(new Double(36.678));
		    		waypoint.setLongitude(new Double(-79.491));
	
		    		waypoint = gaProp.getWaypoints().add();
		    		waypoint.setTime("1 Jul 2009 21:00:00.000");
		    		waypoint.setLatitude(new Double(35.949));
		    		waypoint.setLongitude(new Double(-81.079));
	
		    		waypoint = gaProp.getWaypoints().add();
		    		waypoint.setTime("1 Jul 2009 22:00:00.000");
		    		waypoint.setLatitude(new Double(35.696));
		    		waypoint.setLongitude(new Double(-87.128));
	
		    		waypoint = gaProp.getWaypoints().add();
		    		waypoint.setTime("2 Jul 2009 15:00:00.000");
		    		waypoint.setLatitude(new Double(36.406));
		    		waypoint.setLongitude(new Double(-100.212));
	
		    		gaProp.propagate();
	
		    		MainActivator.getRoot().rewind();
				}
				catch( Exception e )
				{
		        	StringBuffer msg = new StringBuffer();
		        	msg.append("Failed to create new scenario!");
		        	msg.append("\n\n");
		        	msg.append("Scenario Name: "+name);
		        	msg.append("\n\n");
		        	msg.append("Exception Msg:      "+e.getMessage());
		        	msg.append("\n");
		        	msg.append("Exception Filename: "+e.getStackTrace()[1].getFileName());
		        	msg.append("\n");
		        	msg.append("Exception Line No:  "+e.getStackTrace()[1].getLineNumber());
		        	if( e instanceof AgCoreException )
		        	{
		            	msg.append("\n");
		    			msg.append( "Exception HRESULT = " + ((AgCoreException)e).getHResultAsHexString() );
		        	}
		
		        	Shell s = Display.getCurrent().getActiveShell();
		        	MessageBox mb = new MessageBox( s, SWT.ICON_ERROR | SWT.OK );
		        	mb.setText("New Scenario Error");
		        	mb.setMessage( msg.toString() );
		        	mb.open();
				}
			}
			else if(event.getCommand().getId().equalsIgnoreCase("openScenario.command"))
			{
				String filePath = null;
				
				try
				{
					String path = getStkHomeDirPath();
					Shell s = Display.getCurrent().getActiveShell();
					FileDialog dialog = new FileDialog( s, SWT.OPEN );
					dialog.setFilterNames(new String [] {"Scenario Files"});
					dialog.setFilterExtensions(new String [] {"*.sc"});
				    String filesep = System.getProperty("file.separator");
				    String newpath = path+filesep+"CodeSamples"+filesep+"SharedResources"+filesep+"Scenarios"+filesep;
					dialog.setFilterPath(newpath);
					dialog.setText("Open scenario");
					
					// blocking call till user completes one of the following ...
					// 1. selects file and clicks ok, to dismiss the dialog.
					// 2. selects cancel, to dismiss the dialog.
					filePath = dialog.open();
					if( filePath != null )
					{
						File f = new File(filePath);
						if( f.exists() && f.isFile() )
						{
							MainActivator.getRoot().closeScenario();
							MainActivator.getRoot().loadScenario( filePath );
						}
					}
				}
				catch( Exception e )
				{
		        	StringBuffer msg = new StringBuffer();
		        	msg.append("Failed to open scenario!");
		        	msg.append("\n\n");
		        	msg.append("Scenario File: "+filePath);
		        	msg.append("\n\n");
		        	msg.append("Exception Msg:      "+e.getMessage());
		        	msg.append("\n");
		        	msg.append("Exception Filename: "+e.getStackTrace()[1].getFileName());
		        	msg.append("\n");
		        	msg.append("Exception Line No:  "+e.getStackTrace()[1].getLineNumber());
		        	if( e instanceof AgCoreException )
		        	{
		            	msg.append("\n");
		    			msg.append( "Exception HRESULT = " + ((AgCoreException)e).getHResultAsHexString() );
		        	}
	
		        	Shell s = Display.getCurrent().getActiveShell();
		        	MessageBox mb = new MessageBox( s, SWT.ICON_ERROR | SWT.OK );
		        	mb.setText("Open Scenario Error");
		        	mb.setMessage( msg.toString() );
		        	mb.open();
				}
			}
			else if(event.getCommand().getId().equalsIgnoreCase("closeScenario.command"))
			{
				try
				{
					MainActivator.getRoot().closeScenario();
				}
				catch( Exception e )
				{
		        	StringBuffer msg = new StringBuffer();
		        	msg.append("Failed to close scenario!");
		        	msg.append("\n\n");
		        	msg.append("Exception Msg:      "+e.getMessage());
		        	msg.append("\n");
		        	msg.append("Exception Filename: "+e.getStackTrace()[1].getFileName());
		        	msg.append("\n");
		        	msg.append("Exception Line No:  "+e.getStackTrace()[1].getLineNumber());
		        	if( e instanceof AgCoreException )
		        	{
		            	msg.append("\n");
		    			msg.append( "Exception HRESULT = " + ((AgCoreException)e).getHResultAsHexString() );
		        	}
	
		        	Shell s = Display.getCurrent().getActiveShell();
		        	MessageBox mb = new MessageBox( s, SWT.ICON_ERROR | SWT.OK );
		        	mb.setText("Close Scenario Error");
		        	mb.setMessage( msg.toString() );
		        	mb.open();
				}
			}
			else if(event.getCommand().getId().equalsIgnoreCase("playScenario.command"))
			{
				MainActivator.getRoot().playForward();
			}
			else if(event.getCommand().getId().equalsIgnoreCase("resetScenario.commmand"))
			{
				MainActivator.getRoot().rewind();
			}
			else if(event.getCommand().getId().equalsIgnoreCase("pauseScenario.command"))
			{
				MainActivator.getRoot().pause();
			}
			else if(event.getCommand().getId().equalsIgnoreCase("speedupScenario.command"))
			{
				MainActivator.getRoot().faster();
			}
			else if(event.getCommand().getId().equalsIgnoreCase("speeddownScenario.command"))
			{
				MainActivator.getRoot().slower();
			}
			else if(event.getCommand().getId().equalsIgnoreCase("rewindScenario.command"))
			{
				MainActivator.getRoot().playBackward();
			}
			else if(event.getCommand().getId().equalsIgnoreCase("stepforwardScenario.command"))
			{
				MainActivator.getRoot().stepForward();
			}
			else if(event.getCommand().getId().equalsIgnoreCase("stepbackwardsScenario.command"))
			{
				MainActivator.getRoot().stepBackward();
			}
			else if(event.getCommand().getId().equalsIgnoreCase("about.command"))
			{
				String javaVer = System.getProperty("java.version");
				String javaClsVer = System.getProperty("java.class.version");
	
				String stkJavaApiVersion = null;
				try
				{
					stkJavaApiVersion = MainActivator.getSTKXApp().getVersion();
				}
				catch( Exception ex )
				{
					ex.printStackTrace();
				}
	
	        	StringBuffer msg = new StringBuffer();
	        	msg.append("Name:\t\t\tCustomApp_SWT_STK_X_Eclipse_RCP\t");
	        	msg.append("\n");
	
	        	if( stkJavaApiVersion != null )
	        	{
	        		msg.append("Version:\t\t\t"+stkJavaApiVersion+"\t");
	            	msg.append("\n");
	        	}
				
	        	if( javaVer != null )
				{
					msg.append("Java Version:\t\t"+javaVer+"\t");
		        	msg.append("\n");
				}
	
	        	if( javaClsVer != null )
				{
					msg.append("Java Cls Version:\t\t"+javaClsVer+"\t");
		        	msg.append("\n");
				}
	
	        	Shell s = Display.getCurrent().getActiveShell();
				MessageBox mb = new MessageBox( s, SWT.ICON_INFORMATION | SWT.OK );
	        	mb.setText("Sample About");
	        	mb.setMessage( msg.toString() );
	        	mb.open();
			}
			else if(event.getCommand().getId().equalsIgnoreCase("directions.command"))
			{
				StringBuffer msg = new StringBuffer();
	        	msg.append("1. In the menubar, click the \"Scenario->New...\" or \"Scenario->Open...\" Menu Item");
	        	msg.append("\n");
	        	msg.append("2. In the menubar, click the \"Scenario->Close\" Menu Item");
	        	msg.append("\n");
	        	
	        	Shell s = Display.getCurrent().getActiveShell();
	        	MessageBox mb = new MessageBox( s, SWT.ICON_INFORMATION | SWT.OK );
	        	mb.setText("Sample Directions");
	        	mb.setMessage( msg.toString() );
	        	mb.open();
			}
			else if(event.getCommand().getId().equalsIgnoreCase("installInfo.command"))
			{
				String msg = null;
				int iconStyle = SWT.ICON_INFORMATION;
				try
				{
		    		IAgStkObject scenObj = MainActivator.getRoot().getCurrentScenario();
		    		if( scenObj != null )
					{
						agi.stkutil.IAgExecCmdResult result = MainActivator.getRoot().executeCommand( "GetReport * \"InstallInfoCon\"" );
				    	int cnt = result.getCount();
		
				    	StringBuffer sb = new StringBuffer();
			            for(int i = 0; i < cnt; i++)
			            {
			                sb.append( result.getItem(i) );
			                sb.append( "\r\n" );
			            }
						result = null;
						msg = sb.toString();
					}
					else
					{
						msg = "No scenario is loaded. Please load a scenario before retrieving installation info";
						iconStyle = SWT.ICON_WARNING;
					}
				}
				catch( Exception ex )
				{
					msg = ex.getMessage();
					iconStyle = SWT.ICON_ERROR;
				}
				
	    		if( msg == null )
	    		{
	    			msg = "Installation information is not available.";
					iconStyle = SWT.ICON_ERROR;
	    		}
	    		
	    		Shell s = Display.getCurrent().getActiveShell();
	    		MessageBox mb = new MessageBox( s, iconStyle | SWT.OK );
	        	mb.setText("Install Information");
	        	mb.setMessage( msg );
	        	mb.open();
			}
			else if(event.getCommand().getId().equalsIgnoreCase("startObjectEdit.command"))
			{
				String objectPath = "/Application/STK/Scenario/CustomApp_SWT_STK_X_Eclipse_RCP/Aircraft/aircraft1";
				GlobeView.getGlobeComposite().getGlobe().startObjectEditing(objectPath);
			}
			else if(event.getCommand().getId().equalsIgnoreCase("cancelObjectEdit.command"))
			{
				GlobeView.getGlobeComposite().getGlobe().stopObjectEditing(true);
			}
			else if(event.getCommand().getId().equalsIgnoreCase("acceptObjectEdit.command"))
			{
				GlobeView.getGlobeComposite().getGlobe().stopObjectEditing(false);
			}
		}
		catch (AgCoreException e) 
		{
			e.printStackTrace();
		}

		return null;
	}

	private String getStkHomeDirPath()
	throws AgCoreException
	{
		agi.stkutil.IAgExecCmdResult res = null;
		res = MainActivator.getRoot().executeCommand("GetDirectory / STKHome");
		String homeDir = (String)res.getItem(0);
		return homeDir;
	}
}

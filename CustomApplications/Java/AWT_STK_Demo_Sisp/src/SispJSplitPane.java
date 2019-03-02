// Java API
import java.lang.reflect.*;
import java.util.*;
import java.awt.Component;
import java.awt.Cursor;
import java.awt.event.*;
import javax.swing.*;

// AGI Java API
import agi.core.AgCoreException;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stk.core.swing.toolbars.animation.*;
import agi.stk.core.swing.toolbars.globe.view.*;
import agi.stk.core.swing.toolbars.map.view.*;
import agi.stkobjects.*;

public class SispJSplitPane
extends JSplitPane
implements IAgAnimationJToolBarEventsListener, IAgGlobeViewJToolBarEventsListener, IAgMapViewJToolBarEventsListener
{
	private static final long		serialVersionUID	= 1L;

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;

	private ContentsJPanel			m_ContentsJPanel;
	private SelectionJTabbedPane	m_SelectionJTabbedPane;

	private boolean					m_AllowPan;

	public SispJSplitPane()
	throws Throwable
	{
		this.m_AgSTKXApplicationClass = new AgSTKXApplicationClass();

		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_ENGINE_RUNTIME))
		{
			String msg = "STK Engine Runtime license is required to run this sample.  Exiting!";
			JOptionPane.showMessageDialog(this, msg, "License Error", JOptionPane.ERROR_MESSAGE);
			System.exit(0);
		}

		if(!this.m_AgSTKXApplicationClass.isFeatureAvailable(AgEFeatureCodes.E_FEATURE_CODE_GLOBE_CONTROL))
		{
			String msg = "You do not have the required STK Globe license.  The sample's globe will not display properly.";
			JOptionPane.showMessageDialog(this, msg, "License Error", JOptionPane.ERROR_MESSAGE);
		}

		this.m_AgStkObjectRootClass = new AgStkObjectRootClass();

		this.m_ContentsJPanel = new ContentsJPanel();
		this.m_SelectionJTabbedPane = new SelectionJTabbedPane();

		this.setLeftComponent(this.m_ContentsJPanel);
		this.setRightComponent(this.m_SelectionJTabbedPane);

		this.setOrientation(JSplitPane.VERTICAL_SPLIT);
		this.setResizeWeight(.5);
		this.resetToPreferredSizes();

		this.getContent().getFeeds().getVehicleFeeds().getForceCode().addForceCodeListener(new VehicleForceCodeListener());
		this.getContent().getFeeds().getVehicleFeeds().getTheater().addTheaterListener(new VehicleTheaterListener());
		this.getContent().getFeeds().getVehicleFeeds().getPlatform().addPlatformListener(new VehiclePlatformListener());

		this.getContent().getFeeds().getSatelliteFeeds().getForceCode().addForceCodeListener(new SatelliteForceCodeListener());
		this.getContent().getFeeds().getSatelliteFeeds().getMission().addMissionListener(new SatelliteMissionListener());
		this.getContent().getFeeds().getSatelliteFeeds().getFuelMass().getMinJCheckBox().addItemListener(new FuelMassMinListener());
		this.getContent().getFeeds().getSatelliteFeeds().getFuelMass().getMaxJCheckBox().addItemListener(new FuelMassMaxListener());
		this.getContent().getFeeds().getSatelliteFeeds().getTotalMass().getMinJCheckBox().addItemListener(new TotalMassMinListener());
		this.getContent().getFeeds().getSatelliteFeeds().getTotalMass().getMaxJCheckBox().addItemListener(new TotalMassMaxListener());

		this.getSelections().getVehs().getShowButton().addActionListener(new VehicleShowListener());
		this.getSelections().getVehs().getNoShowButton().addActionListener(new VehicleNoShowListener());

		this.getSelections().getSats().getShowButton().addActionListener(new SatelliteShowListener());
		this.getSelections().getSats().getNoShowButton().addActionListener(new SatelliteNoShowListener());

		this.getContent().getInfo().getSurveillanceInfo().getAccess().getFindAccessesJCheckBox().addItemListener(new AccessItemListener());

		this.getContent().getAnimationView().getGlobe().addIAgGlobeCntrlEvents(new GlobeListener());

		this.getContent().getAnimationView().getAnimationBar().addAnimationJToolBarListener(this);
		this.getContent().getAnimationView().getGlobeViewBar().addGlobeViewJToolBarListener(this);
		this.getContent().getAnimationView().getMapViewBar().addMapViewJToolBarListener(this);

		this.loadScenario();
	}

	public AgSTKXApplicationClass getSTKXApplication()
	{
		return this.m_AgSTKXApplicationClass;
	}
	
	public AgStkObjectRootClass getStkObjectRootClass()
	{
		return this.m_AgStkObjectRootClass;
	}
	
	public String getVersion()
	throws Throwable
	{
		return this.m_AgSTKXApplicationClass.getVersion();
	}

	/*package*/ ContentsJPanel getContent()
	{
		return this.m_ContentsJPanel;
	}

	private SelectionJTabbedPane getSelections()
	{
		return this.m_SelectionJTabbedPane;
	}

	private class VehicleForceCodeListener
	implements ActionListener
	{
		public void actionPerformed(ActionEvent ae)
		{
			try
			{
				String actionCommand = ae.getActionCommand();

				if(actionCommand.equalsIgnoreCase(ForceCodeJPanel.s_ALL))
				{
					getSelections().getVehs().getAvailable().getVehModel().setForceCodes(true, false, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(ForceCodeJPanel.s_BLUE))
				{
					getSelections().getVehs().getAvailable().getVehModel().setForceCodes(false, true, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(ForceCodeJPanel.s_RED))
				{
					getSelections().getVehs().getAvailable().getVehModel().setForceCodes(false, false, true, false);
				}
				else if(actionCommand.equalsIgnoreCase(ForceCodeJPanel.s_WHITE))
				{
					getSelections().getVehs().getAvailable().getVehModel().setForceCodes(false, false, false, true);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class VehicleTheaterListener
	implements ActionListener
	{
		public void actionPerformed(ActionEvent ae)
		{
			try
			{
				String actionCommand = ae.getActionCommand();

				if(actionCommand.equalsIgnoreCase(TheaterJPanel.s_ALL))
				{
					getSelections().getVehs().getAvailable().getVehModel().setTheaters(true, false, false, false, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(TheaterJPanel.s_USCENTCOM))
				{
					getSelections().getVehs().getAvailable().getVehModel().setTheaters(false, true, false, false, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(TheaterJPanel.s_USEUCOM))
				{
					getSelections().getVehs().getAvailable().getVehModel().setTheaters(false, false, true, false, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(TheaterJPanel.s_USNORTHCOM))
				{
					getSelections().getVehs().getAvailable().getVehModel().setTheaters(false, false, false, true, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(TheaterJPanel.s_USPACOM))
				{
					getSelections().getVehs().getAvailable().getVehModel().setTheaters(false, false, false, false, true, false);
				}
				else if(actionCommand.equalsIgnoreCase(TheaterJPanel.s_USSOUTHCOM))
				{
					getSelections().getVehs().getAvailable().getVehModel().setTheaters(false, false, false, false, false, true);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class VehiclePlatformListener
	implements ActionListener
	{
		public void actionPerformed(ActionEvent ae)
		{
			try
			{
				String actionCommand = ae.getActionCommand();

				if(actionCommand.equalsIgnoreCase(PlatformJPanel.s_ALL))
				{
					getSelections().getVehs().getAvailable().getVehModel().setPlatforms(true, false, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(PlatformJPanel.s_GROUND))
				{
					getSelections().getVehs().getAvailable().getVehModel().setPlatforms(false, true, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(PlatformJPanel.s_AIRCRAFT))
				{
					getSelections().getVehs().getAvailable().getVehModel().setPlatforms(false, false, true, false);
				}
				else if(actionCommand.equalsIgnoreCase(PlatformJPanel.s_SHIP))
				{
					getSelections().getVehs().getAvailable().getVehModel().setPlatforms(false, false, false, true);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class SatelliteForceCodeListener
	implements ActionListener
	{
		public void actionPerformed(ActionEvent ae)
		{
			try
			{
				String actionCommand = ae.getActionCommand();

				if(actionCommand.equalsIgnoreCase(ForceCodeJPanel.s_ALL))
				{
					getSelections().getSats().getAvailable().getSatModel().setForceCodes(true, false, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(ForceCodeJPanel.s_BLUE))
				{
					getSelections().getSats().getAvailable().getSatModel().setForceCodes(false, true, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(ForceCodeJPanel.s_RED))
				{
					getSelections().getSats().getAvailable().getSatModel().setForceCodes(false, false, true, false);
				}
				else if(actionCommand.equalsIgnoreCase(ForceCodeJPanel.s_WHITE))
				{
					getSelections().getSats().getAvailable().getSatModel().setForceCodes(false, false, false, true);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class SatelliteMissionListener
	implements ActionListener
	{
		public void actionPerformed(ActionEvent ae)
		{
			try
			{
				String actionCommand = ae.getActionCommand();

				if(actionCommand.equalsIgnoreCase(MissionJPanel.s_ALL))
				{
					getSelections().getSats().getAvailable().getSatModel().setMission(true, false, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(MissionJPanel.s_EO))
				{
					getSelections().getSats().getAvailable().getSatModel().setMission(false, true, false, false);
				}
				else if(actionCommand.equalsIgnoreCase(MissionJPanel.s_IR))
				{
					getSelections().getSats().getAvailable().getSatModel().setMission(false, false, true, false);
				}
				else if(actionCommand.equalsIgnoreCase(MissionJPanel.s_ELINT))
				{
					getSelections().getSats().getAvailable().getSatModel().setMission(false, false, false, true);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class FuelMassMinListener
	implements ItemListener
	{
		public void itemStateChanged(ItemEvent ie)
		{
			try
			{
				int state = ie.getStateChange();

				if(state == ItemEvent.SELECTED)
				{
					String min = getContent().getFeeds().getSatelliteFeeds().getFuelMass().getMin();

					double dmin = Double.parseDouble(min);

					getSelections().getSats().getAvailable().getSatModel().setMassFuelMin(true, dmin);
				}
				else if(state == ItemEvent.DESELECTED)
				{
					getSelections().getSats().getAvailable().getSatModel().setMassFuelMin(false, 0.0);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class FuelMassMaxListener
	implements ItemListener
	{
		public void itemStateChanged(ItemEvent ie)
		{
			try
			{
				int state = ie.getStateChange();

				if(state == ItemEvent.SELECTED)
				{
					String max = getContent().getFeeds().getSatelliteFeeds().getFuelMass().getMax();

					double dmax = Double.parseDouble(max);

					getSelections().getSats().getAvailable().getSatModel().setMassFuelMax(true, dmax);
				}
				else if(state == ItemEvent.DESELECTED)
				{
					getSelections().getSats().getAvailable().getSatModel().setMassFuelMax(false, 1000.0);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class TotalMassMinListener
	implements ItemListener
	{
		public void itemStateChanged(ItemEvent ie)
		{
			try
			{
				int state = ie.getStateChange();

				if(state == ItemEvent.SELECTED)
				{
					String min = getContent().getFeeds().getSatelliteFeeds().getTotalMass().getMin();

					double dmin = Double.parseDouble(min);

					getSelections().getSats().getAvailable().getSatModel().setMassTotalMin(true, dmin);
				}
				else if(state == ItemEvent.DESELECTED)
				{
					getSelections().getSats().getAvailable().getSatModel().setMassTotalMin(false, 0.0);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class TotalMassMaxListener
	implements ItemListener
	{
		public void itemStateChanged(ItemEvent ie)
		{
			try
			{
				int state = ie.getStateChange();

				if(state == ItemEvent.SELECTED)
				{
					String max = getContent().getFeeds().getSatelliteFeeds().getTotalMass().getMax();

					double dmax = Double.parseDouble(max);

					getSelections().getSats().getAvailable().getSatModel().setMassTotalMax(true, dmax);
				}
				else if(state == ItemEvent.DESELECTED)
				{
					getSelections().getSats().getAvailable().getSatModel().setMassTotalMax(false, 1000.0);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class VehicleShowListener
	implements ActionListener
	{
		public void actionPerformed(ActionEvent ae)
		{
			try
			{
				int count = getSelections().getVehs().getAvailable().getSelectedRowCount();

				for(int num = 0; num < count; num++)
				{
					int index = getSelections().getVehs().getAvailable().getSelectedRow();

					String[] data = getSelections().getVehs().getAvailable().getVehModel().getTempData(index);

					int shownindex = getSelections().getVehs().getShown().getVehModel().addTempData(data);

					loadVehicle(shownindex);

					getSelections().getVehs().getAvailable().getVehModel().removeTempData(data);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class VehicleNoShowListener
	implements ActionListener
	{
		public void actionPerformed(ActionEvent ae)
		{
			try
			{
				int count = getSelections().getVehs().getShown().getSelectedRowCount();

				for(int num = 0; num < count; num++)
				{
					int index = getSelections().getVehs().getShown().getSelectedRow();

					unloadVehicle(index);

					String[] data = getSelections().getVehs().getShown().getVehModel().getTempData(index);

					getSelections().getVehs().getAvailable().getVehModel().addTempData(data);

					getSelections().getVehs().getShown().getVehModel().removeTempData(data);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class SatelliteShowListener
	implements ActionListener
	{
		public void actionPerformed(ActionEvent ae)
		{
			try
			{
				int count = getSelections().getSats().getAvailable().getSelectedRowCount();

				for(int num = 0; num < count; num++)
				{
					int index = getSelections().getSats().getAvailable().getSelectedRow();

					String[] data = getSelections().getSats().getAvailable().getSatModel().getTempData(index);

					int shownindex = getSelections().getSats().getShown().getSatModel().addTempData(data);

					loadSatellite(shownindex);

					getSelections().getSats().getAvailable().getSatModel().removeTempData(data);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class SatelliteNoShowListener
	implements ActionListener
	{
		public void actionPerformed(ActionEvent ae)
		{
			try
			{
				int count = getSelections().getSats().getShown().getSelectedRowCount();

				for(int num = 0; num < count; num++)
				{
					int index = getSelections().getSats().getShown().getSelectedRow();

					unloadSatellite(index);

					String[] data = getSelections().getSats().getShown().getSatModel().getTempData(index);

					getSelections().getSats().getAvailable().getSatModel().addTempData(data);

					getSelections().getSats().getShown().getSatModel().removeTempData(data);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private class GlobeListener
	implements agi.stkx.IAgGlobeCntrlEvents
	{
		private int	m_X;
		private int	m_Y;

		public void onAgGlobeCntrlEvent(AgGlobeCntrlEvent e) 
		{
			try
			{
				int type = e.getType();
				if(type == AgGlobeCntrlEvent.TYPE_DBL_CLICK)
				{
					dblClick();					
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_UP)
				{
					Object[] params = e.getParams();
					short button = ((Short)params[0]).shortValue();
					short shift = ((Short)params[1]).shortValue();
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();
					mouseUp(button, shift, x, y);
				}
				else if(type == AgGlobeCntrlEvent.TYPE_MOUSE_MOVE)
				{
					Object[] params = e.getParams();
					short button = ((Short)params[0]).shortValue();
					short shift = ((Short)params[1]).shortValue();
					int x = ((Integer)params[2]).intValue();
					int y = ((Integer)params[3]).intValue();
					mouseMove(button, shift, x, y);
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}

		public void dblClick()
		throws AgCoreException
		{
			AgGlobeCntrlClass globe = getContent().getAnimationView().getGlobe();

			// System.out.println( "x = "+m_X );
			// System.out.println( "y = "+m_Y );

			IAgPickInfoData pdata = globe.pickInfo(this.m_X, this.m_Y);

			if(pdata != null)
			{
				if(pdata.getIsObjPathValid())
				{
					String path = pdata.getObjPath();

					String[] nodes = path.split("/");

					String type = nodes[nodes.length - 2];
					String name = nodes[nodes.length - 1];

					if(type.equalsIgnoreCase("Satellite"))
					{
						SatelliteData satdata = getSelections().getSats().getAvailable().getSatModel().getAllData(name);

						if(satdata != null)
						{
							getContent().getInfo().getSatelliteInfo().setSatData(satdata);
							// System.out.println( "*******Data shown for "+ satdata.Name );
						}
					}
					else
					{
						VehicleData vehdata = getSelections().getVehs().getAvailable().getVehModel().getAllData(name);

						if(vehdata != null)
						{
							getContent().getInfo().getVehicleInfo().setVehData(vehdata);
							// System.out.println( "*******Data shown for "+ vehdata.Name );
						}
					}
				}
				else
				{
					// System.out.println( "*******Changing location of facility" );

					double lat = pdata.getLat();
					double lon = pdata.getLon();

					String slat = Double.toString(lat);
					int indexlat = slat.indexOf(".");

					if(indexlat != -1)
					{
						if(slat.length() > (indexlat + 2))
						{
							slat = slat.substring(0, indexlat + 2);
						}
					}

					String slon = Double.toString(lon);
					int indexlon = slon.indexOf(".");

					if(indexlon != -1)
					{
						if(slon.length() > (indexlon + 2))
						{
							slon = slon.substring(0, indexlon + 2);
						}
					}

					getContent().getInfo().getSurveillanceInfo().getLLA().setLat(slat);
					getContent().getInfo().getSurveillanceInfo().getLLA().setLon(slon);

					setFacilityPosition(Double.parseDouble(slat), Double.parseDouble(slon));

					if(getContent().getInfo().getSurveillanceInfo().getAccess().getFindAccessesJCheckBox().isSelected())
					{
						removeAccesses();
						showAccesses();
					}
				}
			}
		}

		public void mouseUp(short button, short shift, int x, int y)
		{
			// used by the double click
			this.m_X = x;
			this.m_Y = y;

			// System.out.println( "x = "+m_X );
			// System.out.println( "y = "+m_Y );
		}

		public void mouseMove(short button, short shift, int x, int y)
		{
			getContent().getInfo().getSurveillanceInfo().getXY().setMouseX(Integer.toString(x));
			getContent().getInfo().getSurveillanceInfo().getXY().setMouseY(Integer.toString(y));
		}
	}

	private class AccessItemListener
	implements ItemListener
	{
		public void itemStateChanged(ItemEvent ie)
		{
			try
			{
				int state = ie.getStateChange();

				if(state == ItemEvent.SELECTED)
				{
					showAccesses();
				}
				else if(state == ItemEvent.DESELECTED)
				{
					removeAccesses();
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private void setFacilityPosition(double lat, double lon)
	{
		executeCommand("BatchGraphics * On");
		executeCommand("SetPosition */Facility/accessFac Geodetic " + lat + " " + lon + " 0.0");
		executeCommand("BatchGraphics * Off");
	}

	private void showAccesses()
	{
		executeCommand("BatchGraphics * On");

		int cnt = getSelections().getSats().getShown().getSatModel().getRowCount();

		for(int i = 0; i < cnt; i++)
		{
			String color1 = null;
			String color2 = null;

			String[] tempdata = getSelections().getSats().getShown().getSatModel().getTempData(i);

			String id = tempdata[SatelliteData.s_ID];

			int idindex = getSelections().getSats().getShown().getSatModel().getAllDataIndexOfId(id);

			SatelliteData satdata = getSelections().getSats().getShown().getSatModel().getAllData(idindex);

			if(satdata.ForceCode.equalsIgnoreCase("Blue"))
			{
				color1 = "Blue";
				color2 = "Green";
			}
			else
			{
				color1 = "Red";
				color2 = "Yellow";
			}

			// System.out.println( satdata.Type + "/" + satdata.Name );

			executeCommand("Graphics */" + satdata.Type + "/" + satdata.Name + " SetAttrType AccessIntervals");
			executeCommand("Graphics */" + satdata.Type + "/" + satdata.Name + " AccessIntervals Edit DuringNoAccess show On Color " + color1);
			executeCommand("Graphics */" + satdata.Type + "/" + satdata.Name + " AccessIntervals Edit DuringAccess show on LineWidth 1 Color " + color2);
			executeCommand("Graphics */" + satdata.Type + "/" + satdata.Name + " AccessIntervals ObjectPath Facility/accessFac");
			executeCommand("Access */" + satdata.Type + "/" + satdata.Name + " */Facility/accessFac");
		}

		executeCommand("BatchGraphics * Off");
	}

	private void removeAccesses()
	{
		executeCommand("BatchGraphics * On");

		int cnt = getSelections().getSats().getShown().getSatModel().getRowCount();

		for(int i = 0; i < cnt; i++)
		{
			String color1 = null;
			String color2 = null;

			String[] tempdata = getSelections().getSats().getShown().getSatModel().getTempData(i);

			String id = tempdata[SatelliteData.s_ID];

			int idindex = getSelections().getSats().getShown().getSatModel().getAllDataIndexOfId(id);

			SatelliteData satdata = getSelections().getSats().getShown().getSatModel().getAllData(idindex);

			if(satdata.ForceCode.equalsIgnoreCase("Blue"))
			{
				color1 = "Blue";
				color2 = "Blue";
			}
			else
			{
				color1 = "Red";
				color2 = "Red";
			}

			// System.out.println( satdata.Type + "/" + satdata.Name );

			executeCommand("RemoveAccess */" + satdata.Type + "/" + satdata.Name + " */Facility/accessFac");
			executeCommand("Graphics */" + satdata.Type + "/" + satdata.Name + " AccessIntervals Edit DuringNoAccess show On Color " + color1);
			executeCommand("Graphics */" + satdata.Type + "/" + satdata.Name + " AccessIntervals Edit DuringAccess show on LineWidth 1 Color " + color2);
		}

		executeCommand("BatchGraphics * Off");
	}

	private void executeCommand(String cmd)
	{
		try
		{
			// System.out.println( "I STK Engine/CON: ExecuteCommand: Sent     '" + cmd + "'" );

			this.m_AgStkObjectRootClass.executeCommand(cmd);
		}
		catch(Throwable t)
		{
			// ======================================================================================
			// Execute commands may return exception's if particular modules of STK Engine are not
			// granted activation within your license. For instance, if you tried to execute
			// and "Attitude" related connect command, but did not have an "Attitude" module license
			// then an exception would be thrown. For that reason we comment out the printing of
			// the stack trace in this samples execute command, due to the various permutations
			// of license configurations that AGI customers own, that will run this sample. Uncomment
			// these lines to help you debug a failing connect command ( possibly due to syntax ). First
			// check that you have the particular module license for the particular connect command.
			// =====================================================================================
			// System.out.println( "I STK Engine/CON: ExecuteCommand: Sent     '" + cmd + "'" );
			// t.printStackTrace();
		}
	}

	private void loadScenario()
	throws InterruptedException, InvocationTargetException
	{
		executeCommand("New / Scenario SISP");
		executeCommand("SetUnits * Epochsec");
		executeCommand("New / */Facility accessFac");
		executeCommand("Graphics */Facility/accessFac Label off");
		executeCommand("VO */Facility/accessFac Model off");

		this.setFacilityPosition(0.0, 0.0);
	}

	private void loadVehicle(int index)
	{
		String[] tempdata = getSelections().getVehs().getShown().getVehModel().getTempData(index);

		String id = tempdata[VehicleData.s_ID];

		int idindex = getSelections().getVehs().getShown().getVehModel().getAllDataIndexOfId(id);

		VehicleData vehdata = getSelections().getVehs().getShown().getVehModel().getAllData(idindex);

		this.executeCommand("BatchGraphics * On");

		this.createVeh(vehdata);
		this.addVehWayPoints(vehdata);
		this.setAttitudeView(vehdata);

		this.executeCommand("BatchGraphics * Off");

		//this.m_ContentsJPanel.getAnimationView().getGlobeViewBar().getStoredViewJComboBox().addStoredView(getVehType( vehdata ) + "/" + vehdata.Name);

		this.m_ContentsJPanel.getInfo().getVehicleInfo().setVehData(vehdata);
		int tabindex = this.m_ContentsJPanel.getInfo().indexOfTab(InfoJTabbedPane.s_VEHICLE);
		this.m_ContentsJPanel.getInfo().setSelectedIndex(tabindex);
	}

	private String getVehType(VehicleData vehdata)
	{
		String type = vehdata.Type;

		if(type.equalsIgnoreCase("Ground"))
		{
			type = type + "Vehicle";
		}

		return type;
	}

	private void createVeh(VehicleData vehdata)
	{
		this.executeCommand("New / */" + getVehType(vehdata) + " " + vehdata.Name);
		this.executeCommand("Graphics */" + getVehType(vehdata) + "/" + vehdata.Name + " Basic Show On Label On Color " + vehdata.ForceCode);
	}

	private void addVehWayPoints(VehicleData vehdata)
	{
		ArrayList<VehicleStates> array = this.getVehStates(vehdata.State);

		for(int i = 0; i < array.size(); i++)
		{
			VehicleStates vs = (VehicleStates)array.get(i);

			String s1 = vs.State1;
			String s2 = vs.State2;
			String s3 = vs.State3;

			this.executeCommand("AddWaypoint */" + getVehType(vehdata) + "/" + vehdata.Name + " DetTimeAccFromVel " + s1 + " " + s2 + " " + s3 + " 1");
		}
	}

	private ArrayList<VehicleStates> getVehStates(String states)
	{
		ArrayList<VehicleStates> array = new ArrayList<VehicleStates>();

		String[] temp = states.split(" ");

		for(int i = 0; i < temp.length;)
		{
			VehicleStates vs = new VehicleStates();

			vs.State1 = temp[i];
			vs.State2 = temp[++i];
			vs.State3 = temp[++i];

			++i;

			array.add(vs);
		}

		return array;
	}

	private void setAttitudeView(VehicleData vehdata)
	{
		int opc = Integer.decode(vehdata.OpCapacity).intValue();
		String color = null;

		if(opc <= 30)
		{
			color = "Red";
		}
		else if(opc >= 70)
		{
			color = "Green";
		}
		else
		{
			color = "Yellow";
		}

		this.executeCommand("VO */" + getVehType(vehdata) + "/" + vehdata.Name + " Attitudeview Sphere Show on SphereColor " + color + " LabelType None");
		this.executeCommand("VO */" + getVehType(vehdata) + "/" + vehdata.Name + " Attitudeview Projection Name Earth Show Off");
		this.executeCommand("VO */" + getVehType(vehdata) + "/" + vehdata.Name + " Attitudeview Projection Name Moon Show Off");
		this.executeCommand("VO */" + getVehType(vehdata) + "/" + vehdata.Name + " Attitudeview Projection Name Sun Show Off");
	}

	private void unloadVehicle(int index)
	{
		String[] tempdata = getSelections().getVehs().getShown().getVehModel().getTempData(index);

		String id = tempdata[VehicleData.s_ID];

		int idindex = getSelections().getVehs().getShown().getVehModel().getAllDataIndexOfId(id);

		VehicleData vehdata = getSelections().getVehs().getShown().getVehModel().getAllData(idindex);

		this.executeCommand("BatchGraphics * On");
		executeCommand("Unload / */" + getVehType(vehdata) + "/" + vehdata.Name);
		executeCommand("BatchGraphics * Off");

		this.m_ContentsJPanel.getAnimationView().getGlobeViewBar().getStoredViewJComboBox().removeStoredView(getVehType( vehdata ) + "/" + vehdata.Name);
	}

	private void loadSatellite(int index)
	{
		String[] tempdata = getSelections().getSats().getShown().getSatModel().getTempData(index);

		String id = tempdata[SatelliteData.s_ID];

		int idindex = getSelections().getSats().getShown().getSatModel().getAllDataIndexOfId(id);

		SatelliteData satdata = getSelections().getSats().getShown().getSatModel().getAllData(idindex);

		executeCommand("BatchGraphics * On");
		executeCommand("SetUnits / METER SEC EPOCHSEC");
		executeCommand("New / */" + satdata.Type + " " + satdata.Name);
		executeCommand("SetState */" + satdata.Type + "/" + satdata.Name + " Classical J2Perturbation \"0\" \"86400\" 60.0 J2000 \"0\" " + satdata.State);
		executeCommand("Graphics */" + satdata.Type + "/" + satdata.Name + " Basic Show On Label On Color " + satdata.ForceCode);
		executeCommand("VO */" + satdata.Type + "/" + satdata.Name + " Attitudeview Sphere Show On SphereColor Yellow LabelType None");
		executeCommand("VO */" + satdata.Type + "/" + satdata.Name + " Attitudeview Projection Name Earth Show Off");
		executeCommand("VO */" + satdata.Type + "/" + satdata.Name + " Attitudeview Projection Name Moon Show Off");
		executeCommand("VO */" + satdata.Type + "/" + satdata.Name + " Attitudeview Projection Name Sun Show Off");
		executeCommand("BatchGraphics * Off");

		//this.m_ContentsJPanel.getAnimationView().getGlobeViewBar().getStoredViewJComboBox().addStoredView( satdata.Type+"/" + satdata.Name );

		this.m_ContentsJPanel.getInfo().getSatelliteInfo().setSatData(satdata);
		int tabindex = this.m_ContentsJPanel.getInfo().indexOfTab(InfoJTabbedPane.s_SATELLITE);
		this.m_ContentsJPanel.getInfo().setSelectedIndex(tabindex);
	}

	private void unloadSatellite(int index)
	{
		String[] tempdata = getSelections().getSats().getShown().getSatModel().getTempData(index);

		String id = tempdata[SatelliteData.s_ID];

		int idindex = getSelections().getSats().getShown().getSatModel().getAllDataIndexOfId(id);

		SatelliteData satdata = getSelections().getSats().getShown().getSatModel().getAllData(idindex);

		executeCommand("BatchGraphics * On");
		executeCommand("Unload / */" + satdata.Type + "/" + satdata.Name);
		executeCommand("BatchGraphics * Off");

		this.m_ContentsJPanel.getAnimationView().getGlobeViewBar().getStoredViewJComboBox().removeStoredView( satdata.Type + "/" + satdata.Name );
	}

	public void onMapViewJToolBarAction(AgMapViewJToolBarEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int action = e.getMapViewJToolBarAction();

			if(action == AgMapViewJToolBarEvent.ACTION_VIEW_ZOOM_IN)
			{
				this.getContent().getAnimationView().getMap().zoomIn();
			}
			else if(action == AgMapViewJToolBarEvent.ACTION_VIEW_ZOOM_OUT)
			{
				this.getContent().getAnimationView().getMap().zoomOut();
			}
			else if(action == AgMapViewJToolBarEvent.ACTION_VIEW_ALLOW_PAN)
			{
				this.m_AllowPan = !this.m_AllowPan;
				if(this.m_AllowPan)
				{
					this.m_AgSTKXApplicationClass.executeCommand("Window2d * InpDevMode EnablePickMode Off");
					this.m_AgSTKXApplicationClass.executeCommand("Window2d * InpDevMode EnablePanMode On");
				}
				else
				{
					this.m_AgSTKXApplicationClass.executeCommand("Window2d * InpDevMode EnablePanMode Off");
					this.m_AgSTKXApplicationClass.executeCommand("Window2d * InpDevMode EnablePickMode On");
				}
			}
		}
		catch(AgCoreException ce)
		{
			ce.printHexHresult();
			ce.printStackTrace();
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
		finally
		{
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	public void onGlobeViewJToolBarAction(AgGlobeViewJToolBarEvent e)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			int winid = this.getContent().getAnimationView().getGlobe().getWinID();

			int action = e.getGlobeViewJToolBarAction();
			
			if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_ZOOM_IN)
			{
				this.m_AgStkObjectRootClass.executeCommand("Window3D * InpDevMode Mode RubberBandViewLLA WindowID " + winid);
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_HOME)
			{
				this.m_AgStkObjectRootClass.executeCommand("VO * View Home WindowID " + winid);
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_ORIENT_NORTH)
			{
				this.m_AgStkObjectRootClass.executeCommand("VO * View North WindowID " + winid);
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_ORIENT_FROM_TOP)
			{
				this.m_AgStkObjectRootClass.executeCommand("VO * View Top WindowID " + winid);
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_STOREDVIEW_PREVIOUS)
			{
				this.getContent().getAnimationView().getGlobeViewBar().getStoredViewJComboBox().previousStoredView();
				String currentSelectedView = this.getContent().getAnimationView().getGlobeViewBar().getStoredViewJComboBox().getCurrentSelectedStoredView();
				if(currentSelectedView != null)
				{
					String connectCommand = "VO * UseStoredView " + currentSelectedView + " " + winid;
					this.m_AgStkObjectRootClass.executeCommand(connectCommand);
				}
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_STOREDVIEW_NEXT)
			{
				this.getContent().getAnimationView().getGlobeViewBar().getStoredViewJComboBox().nextStoredView();
				String currentSelectedView = this.getContent().getAnimationView().getGlobeViewBar().getStoredViewJComboBox().getCurrentSelectedStoredView();
				if(currentSelectedView != null)
				{
					String connectCommand = "VO * UseStoredView \"" + currentSelectedView + "\" " + winid;
					this.m_AgStkObjectRootClass.executeCommand(connectCommand);
				}
			}
			else if(action == AgGlobeViewJToolBarEvent.ACTION_VIEW_STOREDVIEW_VIEW)
			{
				String currentSelectedView = this.getContent().getAnimationView().getGlobeViewBar().getStoredViewJComboBox().getCurrentSelectedStoredView();
				if(currentSelectedView != null)
				{
					String connectCommand = "VO * UseStoredView \"" + currentSelectedView + "\" " + winid;
					this.m_AgStkObjectRootClass.executeCommand(connectCommand);
				}
			}
		}
		catch(AgCoreException ce)
		{
			ce.printHexHresult();
			ce.printStackTrace();
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
		finally
		{
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	public void onAnimationJToolBarAction(AgAnimationJToolBarEvent e)
	{
		try
		{
			int action = e.getAnimationJToolBarAction();
			if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_REWIND)
			{
				this.m_AgSTKXApplicationClass.executeCommand("Animate * Reset");
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_PLAYFORWARD)
			{
				this.m_AgSTKXApplicationClass.executeCommand("Animate * Start");
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_PLAYBACKWARD)
			{
				this.m_AgSTKXApplicationClass.executeCommand("Animate * Start Reverse");
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_PAUSE)
			{
				this.m_AgSTKXApplicationClass.executeCommand("Animate * Pause");
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_STEPFORWARD)
			{
				this.m_AgSTKXApplicationClass.executeCommand("Animate * Step Forward");
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_STEPBACKWARD)
			{
				this.m_AgSTKXApplicationClass.executeCommand("Animate * Step Reverse");
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_FASTER)
			{
				this.m_AgSTKXApplicationClass.executeCommand("Animate * Faster");
			}
			else if(action == AgAnimationJToolBarEvent.ACTION_ANIMATION_SLOWER)
			{
				this.m_AgSTKXApplicationClass.executeCommand("Animate * Slower");
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
}
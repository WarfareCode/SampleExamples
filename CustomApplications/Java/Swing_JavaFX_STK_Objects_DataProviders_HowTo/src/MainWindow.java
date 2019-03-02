// Java API
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
import javax.swing.border.*;
import javax.swing.event.*;
import javax.swing.plaf.metal.*;
import javax.swing.tree.*;

// AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.ntvapp.*;
import agi.stkx.awt.*;
import agi.stkobjects.*;
import agi.stkengine.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;
//samples API
import codesnippets.*;
import agi.customapplications.swing.*;
import agi.samples.sharedresources.swing.codesnippets.*;
import charts.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKEngineSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKEngineSampleBaseJFrame
implements IChartDisplayHelper
{
	private final static long			serialVersionUID	= 1L;

	private final static String			s_TITLE				= "CustomApp_Swing_JavaFX_STK_Objects_DataProviders_HowTo";
	private final static String			s_DESCFILENAME		= "AppDescription.html";

	private IAgNtvAppEventsListener		m_IAgNtvAppEventsListener2;

	private JSplitPane					m_JSplitPane1;
	private JTabbedPane					m_JTabbedPane;
	private AgGlobeCntrlClass			m_AgGlobeCntrlClass;
	private AgMapCntrlClass				m_AgMapCntrlClass;
	private RootEventsAdapter			m_RootEventsAdapter;

	// Members need for this specifc sample
	private CodeSnippetSelectionJPanel	m_CodeSnippetSelectionJPanel;
	private CodeSnippetViewJPanel		m_CodeSnippetViewJPanel;
	private DataProviderCodeSnippet	m_CurrentCodeSnippet;

	protected MainWindow()
	throws Throwable
	{
		super(MainWindow.class.getResource(s_DESCFILENAME));

		this.initialize();
	}

	private void initialize()
	throws AgException
	{
		try
		{
			this.setTitle(s_TITLE);
			this.setIconImage(new AgAGIImageIcon().getImage());
			this.setDefaultCloseOperation(EXIT_ON_CLOSE);
			this.addWindowListener(new MainWindowAdapter());
			this.setSize(new Dimension(1000, 618));

			this.m_IAgNtvAppEventsListener2 = new IAgNtvAppEventsListener()
			{
				public void onAgNtvAppEvent(AgNtvAppEvent e)
				{
					try
					{
						final int type = e.getType();
						
						SwingUtilities.invokeAndWait(new Runnable() 
						{
							public void run() 
							{
								if (type == AgNtvAppEvent.TYPE_ON_THREAD_START_END)
								{
									initApp();
								}
								else if (type == AgNtvAppEvent.TYPE_ON_THREAD_STOP_BEGIN)
								{
									uninitApp();
								}
							}
						});
					}
					catch(Throwable t)
					{
						t.printStackTrace();
					}
				}
			};
			this.addNtvAppEventsListener(this.m_IAgNtvAppEventsListener2);
		}
		catch(Throwable t)
		{
			throw new AgException(t);
		}
	}

	protected void finalize()
	{
		try
		{
			this.removeNtvAppEventsListener(this.m_IAgNtvAppEventsListener2);
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	private void initApp()
	throws AgCoreException
	{
		MainWindow.this.m_JTabbedPane = new JTabbedPane();

		MainWindow.this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass();
		this.m_JTabbedPane.addTab("Globe", MainWindow.this.m_AgGlobeCntrlClass);

		MainWindow.this.m_AgMapCntrlClass = new AgMapCntrlClass();
		this.m_JTabbedPane.addTab("Map", MainWindow.this.m_AgMapCntrlClass);

		if(AgMetalThemeFactory.getEnabled())
		{
			MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
			Color awtColor = mt.getPrimaryControl();
			AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);
			this.m_AgGlobeCntrlClass.setBackColor(stkxColor);
			this.m_AgGlobeCntrlClass.setBackground(awtColor);
			this.m_AgMapCntrlClass.setBackColor(stkxColor);
			this.m_AgMapCntrlClass.setBackground(awtColor);
		}

		this.m_CodeSnippetSelectionJPanel = new CodeSnippetSelectionJPanel();
		this.m_CodeSnippetSelectionJPanel.load(new DataProviderCodeSnippetLoader());
		this.m_CodeSnippetSelectionJPanel.getCodeSnippetSelectionJTree().addTreeSelectionListener(new TreeSelectionListener()
		{
			public void valueChanged(TreeSelectionEvent e)
			{
				tvCode_Select(e);
			}
		});

		this.m_CodeSnippetViewJPanel = new CodeSnippetViewJPanel();
		this.m_CodeSnippetViewJPanel.getShowImportsJCheckBox().addItemListener(new ItemListener()
		{
			public void itemStateChanged(ItemEvent e)
			{
				try
				{
					Object src = e.getSource();
					JCheckBox cb = m_CodeSnippetViewJPanel.getShowImportsJCheckBox();
					if(src.equals(cb))
					{
						displayCodeSnippet(m_CurrentCodeSnippet, cb.isSelected());
					}
				}
				catch(Throwable t)
				{
					t.printStackTrace();
				}
			}
		});
		this.m_CodeSnippetViewJPanel.getSourceLinkTextJLabel().addMouseListener(new MouseAdapter()
		{
			public void mouseClicked(MouseEvent e)
			{
				Runtime rt = Runtime.getRuntime();
				try
				{
					rt.exec("rundll32 SHELL32.DLL,ShellExec_RunDLL " + m_CurrentCodeSnippet.getFilePath()).waitFor();
				}
				catch(Exception ex)
				{
				}
			}
		});

		IAgStkEngine stkengine = this.getStkEngine();
		this.m_RootEventsAdapter = new RootEventsAdapter();
		AgStkObjectRootClass root = stkengine.getStkObjectRoot();
		root.addIAgStkObjectRootEvents2(this.m_RootEventsAdapter);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		JMenu scenarioJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getScenarioJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(scenarioJMenu);
		JMenu vdfJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getVDFJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(vdfJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.m_JSplitPane1 = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT);
		this.m_JSplitPane1.setBorder(new BevelBorder(BevelBorder.LOWERED));
		this.m_JSplitPane1.setResizeWeight(0.45);
		this.m_JSplitPane1.setRightComponent(this.m_JTabbedPane);
		this.m_JSplitPane1.setLeftComponent(this.m_CodeSnippetSelectionJPanel);

		CompoundBorder b2 = null;
		b2 = new CompoundBorder(new BevelBorder(BevelBorder.RAISED), new EmptyBorder(5, 5, 5, 5));
		JSplitPane sp2 = new JSplitPane(JSplitPane.VERTICAL_SPLIT);
		sp2.setBorder(b2);
		sp2.setTopComponent(this.m_JSplitPane1);
		sp2.setBottomComponent(this.m_CodeSnippetViewJPanel);
		sp2.setResizeWeight(0.50);

		MainWindow.this.getStkEngineJPanel().add(sp2, BorderLayout.CENTER);
		
		Thread t = new Thread(new Runnable()
		{
			public void run()
			{
				createScenario();
			}
		});
		t.start();
	}

	private void createScenario()
	{
		try
		{
			IAgStkEngine stkengine = this.getStkEngine();
			AgStkObjectRootClass root = stkengine.getStkObjectRoot();
			root.newScenario("dataproviders");
			
			IAgScenario sc = (IAgScenario)root.getCurrentScenario();
			sc.setTimePeriod("1 May 2015 12:00:00.00", "1 May 2015 14:00:00.00");
			
			IAgStkObjectCollection objects = root.getCurrentScenario().getChildren();
			AgSatelliteClass sat = (AgSatelliteClass)objects._new(AgESTKObjectType.E_SATELLITE, "Satellite1");
			sat.setPropagatorType(AgEVePropagatorType.E_PROPAGATOR_TWO_BODY);
			IAgVePropagatorTwoBody prop = (IAgVePropagatorTwoBody)sat.getPropagator();
			prop.propagate();
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
	
	private void uninitApp()
	throws AgCoreException
	{
		IAgStkEngine stkengine = this.getStkEngine();
		AgStkObjectRootClass root = stkengine.getStkObjectRoot();
		root.addIAgStkObjectRootEvents2(this.m_RootEventsAdapter);

		MainWindow.this.m_AgGlobeCntrlClass.dispose();
		MainWindow.this.m_AgMapCntrlClass.dispose();

		MainWindow.this.m_JTabbedPane.removeAll();
		MainWindow.this.getStkEngineJPanel().remove(MainWindow.this.m_JTabbedPane);
	}

	class RootEventsAdapter
	implements IAgStkObjectRootEvents2
	{
		public void onAgStkObjectRootEvent(AgStkObjectRootEvent e)
		{
			try
			{
				int type = e.getType();

				if(type == AgStkObjectRootEvent.TYPE_ON_SCENARIO_NEW)
				{
				}
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}

	private final void tvCode_Select(TreeSelectionEvent evt)
	{
		try
		{
			((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

			TreePath selectedPath = evt.getPath();

			if(selectedPath != null)
			{
				DefaultMutableTreeNode selectedNode = null;
				selectedNode = (DefaultMutableTreeNode)selectedPath.getLastPathComponent();

				if(selectedNode != null)
				{
					Object uo = selectedNode.getUserObject();
					if(uo instanceof CodeSnippetSelectionJTreeNodeData)
					{
						CodeSnippetSelectionJTreeNodeData nodeData = null;
						nodeData = (CodeSnippetSelectionJTreeNodeData)uo;

						DataProviderCodeSnippet codeSnippet = null;
						codeSnippet = (DataProviderCodeSnippet)nodeData.getCodeSnippet();

						if(codeSnippet != null)
						{
							if(this.m_CurrentCodeSnippet != null)
							{
								this.undisplayCodeSnippet(this.m_CurrentCodeSnippet);
							}

							this.m_CurrentCodeSnippet = codeSnippet;
							boolean showImports = this.m_CodeSnippetViewJPanel.getShowImportsJCheckBox().isSelected();
							this.displayCodeSnippet(this.m_CurrentCodeSnippet, showImports);
						}
					}
				}
			}
		}
		catch(AgCoreException ex)
		{
			ex.printHexHresult();
			ex.printStackTrace();
			JOptionPane.showMessageDialog(this, ex.toString(), "Exception", JOptionPane.ERROR_MESSAGE);
		}
		catch(Throwable t)
		{
			t.printStackTrace();
			JOptionPane.showMessageDialog(this, t.toString(), "Exception", JOptionPane.ERROR_MESSAGE);
		}
		finally
		{
			((Component)this).setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
		}
	}

	// Execute the example code and zoom to that part of the globe
	private final void displayCodeSnippet(DataProviderCodeSnippet codeSnippet, boolean showImports)
	throws Throwable
	{
		if(codeSnippet != null)
		{
			this.m_CodeSnippetViewJPanel.displayCodeSnippet(codeSnippet, showImports);

			IAgStkEngine stkengine = this.getStkEngine();
			AgStkObjectRootClass root = stkengine.getStkObjectRoot();

			codeSnippet.execute(this, root);
		}
	}

	// Remove this codesnippet from the 3D window
	private final void undisplayCodeSnippet(DataProviderCodeSnippet codeSnippet)
	throws Throwable
	{
		this.m_CodeSnippetViewJPanel.displayCodeSnippet(null, false);
	}
	
	public void addTab(String tabName, JComponent panel)
	{
		this.m_JTabbedPane.addTab(tabName, panel);
		this.m_JTabbedPane.setSelectedComponent(panel);
	}
	
	class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgGlobeCntrlClass.dispose();
				MainWindow.this.m_AgMapCntrlClass.dispose();

				// Reverse of the initialization order
				AgAwt_JNI.uninitialize_AwtComponents();
				AgStkCustomApplication_JNI.uninitialize();
				AgAwt_JNI.uninitialize_AwtDelegate();
			}
			catch(Throwable t)
			{
				t.printStackTrace();
			}
		}
	}
}
// Java API
import java.awt.*;
import java.awt.event.*;
import java.util.*;
import java.util.logging.*;

import javax.swing.*;
import javax.swing.border.*;
import javax.swing.event.*;
import javax.swing.tree.*;

// AGI Java API
import agi.core.*;
import agi.swing.*;
import agi.core.logging.*;
import agi.stkobjects.*;
import agi.stkx.swing.*;
import agi.stk.plugin.AgStkExtension_JNI;
import agi.stkgraphics.*;
//Sample API
import codesnippets.*;
import agi.customapplications.swing.*;
import agi.samples.sharedresources.swing.codesnippets.*;

public class MainJFrame
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
{
	private final static long			serialVersionUID	= 1L;

	private final static String			s_TITLE				= "CustomApp_AWT_STK_Graphics_Globe_HowTo";
	private final static String			s_DESCFILENAME		= "AppDescription.html";

	private MainSTKEngineHelper			m_MainSTKEngineHelper;
	private JSplitPane					m_JSplitPane1;
	private AgInitializationJPanel		m_AgInitializationJPanel;
	private CodeSnippetSelectionJPanel	m_CodeSnippetSelectionJPanel;
	private CodeSnippetViewJPanel		m_CodeSnippetViewJPanel;

	private STKGraphicsCodeSnippet		m_CurrentSTKGraphicsCodeSnippet;

	public MainJFrame()
	throws Throwable
	{
		super(Main.class.getResource(s_DESCFILENAME));

		// ================================================
		// Set the logging level to Level.FINEST to get
		// all AGI java console logging
		// ================================================
		ConsoleHandler ch = new ConsoleHandler();
		ch.setLevel(Level.OFF);
		ch.setFormatter(new AgFormatter());
		Logger.getLogger("agi").setLevel(Level.OFF);
		Logger.getLogger("agi").addHandler(ch);

		this.getContentPane().setLayout(new BorderLayout());
		this.setTitle(s_TITLE);
		this.setIconImage(new AgAGIImageIcon().getImage());

		Properties p = System.getProperties();
		this.m_AgInitializationJPanel = new AgInitializationJPanel(p);
		AgProgressInfoEventsManager.getManager().setInfoMessage("Initializing, please wait!!");
		AgProgressInfoEventsManager.getManager().workStarted();
		this.m_AgInitializationJPanel.addShimBorder(20, 20, 20, 20);

		this.m_CodeSnippetSelectionJPanel = new CodeSnippetSelectionJPanel();

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
						displayCodeSnippet(m_CurrentSTKGraphicsCodeSnippet, cb.isSelected());
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
					rt.exec("rundll32 SHELL32.DLL,ShellExec_RunDLL " + m_CurrentSTKGraphicsCodeSnippet.getFilePath()).waitFor();
				}
				catch(Exception ex)
				{
				}
			}
		});

		this.m_JSplitPane1 = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT);
		this.m_JSplitPane1.setBorder(new BevelBorder(BevelBorder.LOWERED));
		this.m_JSplitPane1.setResizeWeight(0.45);
		this.m_JSplitPane1.setRightComponent(this.m_AgInitializationJPanel);
		this.m_JSplitPane1.setLeftComponent(this.m_CodeSnippetSelectionJPanel);

		CompoundBorder b2 = null;
		b2 = new CompoundBorder(new BevelBorder(BevelBorder.RAISED), new EmptyBorder(5, 5, 5, 5));
		JSplitPane sp2 = new JSplitPane(JSplitPane.VERTICAL_SPLIT);
		sp2.setBorder(b2);
		sp2.setTopComponent(this.m_JSplitPane1);
		sp2.setBottomComponent(this.m_CodeSnippetViewJPanel);
		sp2.setResizeWeight(0.50);

		this.getContentPane().add(sp2, BorderLayout.CENTER);

		// remove unwanted sample menu bar
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		JMenu scenarioJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getScenarioJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(scenarioJMenu);
		JMenu vdfJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getVDFJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(vdfJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter(this));

		this.setSize(1000, 618);

		this.addComponentListener(new MainJFrameComponentAdapter());
	}

	public AgGlobeJPanel getGlobeJPanel()
	{
		return this.m_MainSTKEngineHelper.getGlobe();
	}

	class MainJFrameComponentAdapter
	extends ComponentAdapter
	{
		public void componentShown(final ComponentEvent e)
		{
			try
			{
				m_MainSTKEngineHelper = new MainSTKEngineHelper();
				
				Thread t = new Thread(new Runnable()
				{
					public void run()
					{
						try
						{
							SwingUtilities.invokeAndWait(new Runnable()
							{
								public void run()
								{
									try
									{
										AgProgressInfoEventsManager manager = AgProgressInfoEventsManager.getManager();
										m_MainSTKEngineHelper.initializeJavaAPI(manager);
										
										AgStkExtension_JNI.initialize(true); // true parameter allows for smart auto class cast

										m_AgInitializationJPanel.invalidate();
										m_AgInitializationJPanel.repaint();
									}
									catch(Throwable t)
									{
										t.printStackTrace();
									}
								}
							});
			
							SwingUtilities.invokeAndWait(new Runnable()
							{
								public void run()
								{
									try
									{
										AgProgressInfoEventsManager manager = AgProgressInfoEventsManager.getManager();
										m_MainSTKEngineHelper.initializeSTKEngine(manager);
										setApp(m_MainSTKEngineHelper.getApp());
										m_AgInitializationJPanel.invalidate();
										m_AgInitializationJPanel.repaint();
									}
									catch(Throwable t)
									{
										t.printStackTrace();
									}
								}
							});
			
							SwingUtilities.invokeAndWait(new Runnable()
							{
								public void run()
								{
									try
									{
										AgProgressInfoEventsManager manager = AgProgressInfoEventsManager.getManager();
										m_MainSTKEngineHelper.initializeLicensing(e.getComponent(), manager);
										m_AgInitializationJPanel.invalidate();
										m_AgInitializationJPanel.repaint();
									}
									catch(Throwable t)
									{
										t.printStackTrace();
									}
								}
							});
			
							SwingUtilities.invokeAndWait(new Runnable()
							{
								public void run()
								{
									try
									{
										AgProgressInfoEventsManager manager = AgProgressInfoEventsManager.getManager();
										m_MainSTKEngineHelper.initializeRoot(manager);
										setRoot(m_MainSTKEngineHelper.getRoot());
										m_AgInitializationJPanel.invalidate();
										m_AgInitializationJPanel.repaint();
									}
									catch(Throwable t)
									{
										t.printStackTrace();
									}
								}
							});
			
							SwingUtilities.invokeAndWait(new Runnable()
							{
								public void run()
								{
									try
									{
										AgProgressInfoEventsManager manager = AgProgressInfoEventsManager.getManager();
										m_MainSTKEngineHelper.initializeScenario(manager);
										m_AgInitializationJPanel.invalidate();
										m_AgInitializationJPanel.repaint();
									}
									catch(Throwable t)
									{
										t.printStackTrace();
									}
								}
							});
			
							SwingUtilities.invokeAndWait(new Runnable()
							{
								public void run()
								{
									try
									{
										AgProgressInfoEventsManager manager = AgProgressInfoEventsManager.getManager();
										m_MainSTKEngineHelper.initializeUnitPreferences(manager);
										m_AgInitializationJPanel.invalidate();
										m_AgInitializationJPanel.repaint();
									}
									catch(Throwable t)
									{
										t.printStackTrace();
									}
								}
							});
			
							SwingUtilities.invokeAndWait(new Runnable()
							{
								public void run()
								{
									try
									{
										AgProgressInfoEventsManager manager = AgProgressInfoEventsManager.getManager();
										m_MainSTKEngineHelper.initializeAnimationDefaults(manager);
										m_AgInitializationJPanel.invalidate();
										m_AgInitializationJPanel.repaint();
									}
									catch(Throwable t)
									{
										t.printStackTrace();
									}
								}
							});

							SwingUtilities.invokeAndWait(new Runnable()
							{
								public void run()
								{
									try
									{
										AgProgressInfoEventsManager manager = AgProgressInfoEventsManager.getManager();
										m_MainSTKEngineHelper.initializeGlobe(manager);
										AgGlobeJPanel globe = m_MainSTKEngineHelper.getGlobe();
										m_JSplitPane1.setRightComponent(globe);
										
										m_CodeSnippetSelectionJPanel.load(new STKGraphicsCodeSnippetLoader(globe.getControl()));
										m_CodeSnippetSelectionJPanel.getCodeSnippetSelectionJTree().addTreeSelectionListener(new TreeSelectionListener()
										{
											public void valueChanged(TreeSelectionEvent e)
											{
												tvCode_Select(e);
											}
										});

										m_AgInitializationJPanel.invalidate();
										m_AgInitializationJPanel.repaint();
									}
									catch(Throwable t)
									{
										t.printStackTrace();
									}
								}
							});

							SwingUtilities.invokeAndWait(new Runnable()
							{
								public void run()
								{
									try
									{
										AgProgressInfoEventsManager manager = AgProgressInfoEventsManager.getManager();
										m_MainSTKEngineHelper.initializeAnnotationDefaults(manager);
										m_AgInitializationJPanel.invalidate();
										m_AgInitializationJPanel.repaint();
									}
									catch(Throwable t)
									{
										t.printStackTrace();
									}
								}
							});
							SwingUtilities.invokeAndWait(new Runnable()
							{
								public void run()
								{
									try
									{
										AgProgressInfoEventsManager manager = AgProgressInfoEventsManager.getManager();
										m_MainSTKEngineHelper.initializeOverlayToobar(manager);
										m_AgInitializationJPanel.invalidate();
										m_AgInitializationJPanel.repaint();
									}
									catch(Throwable t)
									{
										t.printStackTrace();
									}
								}
							});
						}
						catch(Throwable t)
						{
							t.printStackTrace();
						}
					}
				});
				t.start();
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

						STKGraphicsCodeSnippet codeSnippet = null;
						codeSnippet = (STKGraphicsCodeSnippet)nodeData.getCodeSnippet();

						if(codeSnippet != null)
						{
							if(this.m_CurrentSTKGraphicsCodeSnippet != null)
							{
								this.undisplayCodeSnippet(this.m_CurrentSTKGraphicsCodeSnippet);
							}

							this.m_CurrentSTKGraphicsCodeSnippet = codeSnippet;
							boolean showImports = this.m_CodeSnippetViewJPanel.getShowImportsJCheckBox().isSelected();
							this.displayCodeSnippet(this.m_CurrentSTKGraphicsCodeSnippet, showImports);
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
	private final void displayCodeSnippet(STKGraphicsCodeSnippet codeSnippet, boolean showImports)
	throws Throwable
	{
		if(codeSnippet != null)
		{
			this.m_CodeSnippetViewJPanel.displayCodeSnippet(codeSnippet, showImports);

			IAgScenario scenario = (IAgScenario)this.m_MainSTKEngineHelper.getRoot().getCurrentScenario();
			AgStkGraphicsSceneClass scene = (AgStkGraphicsSceneClass)scenario.getSceneManager().getScenes().getItem(0);

			codeSnippet.execute(this.m_MainSTKEngineHelper.getRoot(), scene);
			codeSnippet.view(this.m_MainSTKEngineHelper.getRoot(), scene);
		}
	}

	// Remove this codesnippet from the 3D window
	private final void undisplayCodeSnippet(STKGraphicsCodeSnippet codeSnippet)
	throws Throwable
	{
		this.m_CodeSnippetViewJPanel.displayCodeSnippet(null, false);

		if(codeSnippet != null)
		{
			IAgScenario scenario = (IAgScenario)this.m_MainSTKEngineHelper.getRoot().getCurrentScenario();
			AgStkGraphicsSceneClass scene = (AgStkGraphicsSceneClass)scenario.getSceneManager().getScenes().getItem(0);
			codeSnippet.remove(this.m_MainSTKEngineHelper.getRoot(), scene);
		}
	}
}
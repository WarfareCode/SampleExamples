// Java API
import java.util.logging.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import javax.swing.plaf.metal.*;

// AGI Java API
import agi.core.logging.*;
import agi.swing.*;
import agi.swing.plaf.metal.*;
import agi.core.*;
import agi.core.awt.*;
import agi.stkx.*;
import agi.stkx.awt.*;
import agi.stkobjects.*;
import agi.stkengine.*;

//CodeSample helper code
import agi.customapplications.swing.*;

public class MainWindow
//NOTE:  This sample derives/extends from CustomApplicationSTKSampleBaseJFrame in order to provide
//common sample help regarding Java properties, connect command toolbar, common STK Engine functionality.
//You application is not required to derive from this class or have the same features it provides, but rather
//from the standard JFrame, Frame, or other preference.
extends CustomApplicationSTKSampleBaseJFrame
implements ActionListener
{
	private static final long		serialVersionUID	= 1L;

	private final static String		s_TITLE				= "CustomApp_AWT_STK_X_Globe_DisplayOptions";
	private final static String		s_DESCFILENAME		= "AppDescription.html";

	private AgSTKXApplicationClass	m_AgSTKXApplicationClass;
	private AgStkObjectRootClass	m_AgStkObjectRootClass;
	private AgGlobeCntrlClass		m_AgGlobeCntrlClass;

	private SampleJPanel			m_SampleJPanel;

	public MainWindow()
	throws Throwable
	{
		super(MainWindow.class.getResource(s_DESCFILENAME));

		// ================================================
		// Set the logging level to Level.FINEST to get
		// all AGI java console logging
		// ================================================
		ConsoleHandler ch = new ConsoleHandler();
		ch.setLevel(Level.OFF);
		ch.setFormatter(new AgFormatter());
		Logger.getLogger("agi").setLevel(Level.OFF);
		Logger.getLogger("agi").addHandler(ch);

		// =========================================
		// This must be called before all
		// AWT/Swing/StkUtil/Stkx/StkObjects calls
		// =========================================
		AgAwt_JNI.initialize_AwtDelegate();
		AgStkCustomApplication_JNI.initialize(true); // true parameter allows for smart auto class cast
		AgAwt_JNI.initialize_AwtComponents();

		this.getContentPane().setLayout(new BorderLayout());
		this.setTitle(s_TITLE);
		this.setIconImage(new AgAGIImageIcon().getImage());

		this.m_AgSTKXApplicationClass = new AgSTKXApplicationClass();
		super.setApp(this.m_AgSTKXApplicationClass);

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
		super.setRoot(this.m_AgStkObjectRootClass);

		MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
		Color awtColor = mt.getPrimaryControl();
		AgCoreColor stkxColor = AgAwtColorTranslator.fromAWTtoCoreColor(awtColor);

		this.m_AgGlobeCntrlClass = new AgGlobeCntrlClass();
		this.m_AgGlobeCntrlClass.setBackColor(stkxColor);
		this.m_AgGlobeCntrlClass.setBackground(awtColor);
		this.getContentPane().add(this.m_AgGlobeCntrlClass, BorderLayout.CENTER);

		this.m_SampleJPanel = new SampleJPanel();
		this.m_SampleJPanel.getBackColorJPanel().getBackcolorJButton().addActionListener(this);
		this.m_SampleJPanel.getSplashJPanel().addActionListener(this);
		this.m_SampleJPanel.getProgressImageJPanel().addActionListener(this);
		this.getContentPane().add(this.m_SampleJPanel, BorderLayout.EAST);

		long coreColor = this.m_AgGlobeCntrlClass.getBackColor();
		Color tempColor = AgAwtColorTranslator.fromLongtoAWT(coreColor);
		this.m_SampleJPanel.getBackColorJPanel().getBackcolorJButton().setBackground(tempColor);

		// Remove unwanted menu bars for this sample
		JMenu sampleJMenu = this.getCustomAppSTKSampleBaseJMenuBar().getSampleJMenu();
		this.getCustomAppSTKSampleBaseJMenuBar().remove(sampleJMenu);
		this.getCustomAppSTKSampleBaseJMenuBar().invalidate();
		this.getCustomAppSTKSampleBaseJMenuBar().repaint();

		this.setDefaultCloseOperation(EXIT_ON_CLOSE);
		this.addWindowListener(new MainWindowAdapter());

		this.setSize(1000, 618);
	}

	public void actionPerformed(ActionEvent e)
	{
		((Component)this).setCursor(new Cursor(Cursor.WAIT_CURSOR));

		try
		{
			Object src = e.getSource();

			if(src.equals(this.m_SampleJPanel.getBackColorJPanel().getBackcolorJButton()))
			{
				long oldCoreColor = this.m_AgGlobeCntrlClass.getBackColor();
				Color oldAwtColor = AgAwtColorTranslator.fromLongtoAWT(oldCoreColor);
				Color newAwtColor = JColorChooser.showDialog(this, "Background Color", oldAwtColor);
				AgCoreColor newCoreColor = AgAwtColorTranslator.fromAWTtoCoreColor(newAwtColor);

				this.m_AgGlobeCntrlClass.setBackColor(newCoreColor);
				this.m_SampleJPanel.getBackColorJPanel().getBackcolorJButton().setBackground(newAwtColor);
			}
			else if(this.m_SampleJPanel.getSplashJPanel().isUpdateJButton(src))
			{
				boolean showSplash = this.m_SampleJPanel.getSplashJPanel().getShowSplashScreen();
				if(showSplash)
				{
					if(this.m_SampleJPanel.getSplashJPanel().getUseUserDefinedSplashScreen())
					{
						String splashFilePath = this.m_SampleJPanel.getSplashJPanel().getUserDefinedSplashScreenPath();
						this.m_AgGlobeCntrlClass.setPictureFromFile(splashFilePath);
					}
					else
					{
						this.m_AgGlobeCntrlClass.setPictureFromFile("");
					}
				}
				this.m_AgGlobeCntrlClass.setNoLogo(!showSplash);
			}
			else if(this.m_SampleJPanel.getProgressImageJPanel().isUpdateJButton(src))
			{
				if(this.m_SampleJPanel.getProgressImageJPanel().getUseNoProgressImageFile())
				{
					this.m_AgGlobeCntrlClass.setShowProgressImage(AgEShowProgressImage.E_SHOW_PROGRESS_IMAGE_NONE);
				}
				else if(this.m_SampleJPanel.getProgressImageJPanel().getUseDefaultProgressImageFile())
				{
					this.m_AgGlobeCntrlClass.setShowProgressImage(AgEShowProgressImage.E_SHOW_PROGRESS_IMAGE_DEFAULT);
				}
				else if(this.m_SampleJPanel.getProgressImageJPanel().getUseUserDefinedProgressImageFile())
				{
					this.m_AgGlobeCntrlClass.setShowProgressImage(AgEShowProgressImage.E_SHOW_PROGRESS_IMAGE_USER);

					int xOffset = this.m_SampleJPanel.getProgressImageJPanel().getXOffset();
					this.m_AgGlobeCntrlClass.setProgressImageXOffset(xOffset);
					
					int yOffset = this.m_SampleJPanel.getProgressImageJPanel().getYOffset();
					this.m_AgGlobeCntrlClass.setProgressImageYOffset(yOffset);
	
					int xOrigin = this.m_SampleJPanel.getProgressImageJPanel().getXOrigin();
					this.m_AgGlobeCntrlClass.setProgressImageXOrigin(xOrigin);
					
					int yOrigin = this.m_SampleJPanel.getProgressImageJPanel().getYOrigin();
					this.m_AgGlobeCntrlClass.setProgressImageYOrigin(yOrigin);
	
					String progressImageFilePath = null;
					progressImageFilePath = this.m_SampleJPanel.getProgressImageJPanel().getUserDefinedProgressImageFilePath();
					this.m_AgGlobeCntrlClass.setProgressImageFile(progressImageFilePath);
				}
			}
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

	class MainWindowAdapter
	extends WindowAdapter
	{
		public void windowClosing(WindowEvent evt)
		{
			try
			{
				// Must dispose your control before uninitializing the API
				MainWindow.this.m_AgGlobeCntrlClass.dispose();

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
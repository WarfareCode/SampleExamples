//Java API
import java.io.*;
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;
import javax.swing.border.*;

//AGI Java API
import agi.core.*;

public class SampleJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	private BackColorJPanel		m_BackColorJPanel;
	private ProgressImageJPanel	m_ProgressImageJPanel;
	private SplashJPanel		m_SplashJPanel;

	/* package */SampleJPanel()
	throws AgCoreException
	{
		this.setLayout(new BorderLayout());

		this.m_BackColorJPanel = new BackColorJPanel();
		this.m_ProgressImageJPanel = new ProgressImageJPanel();
		this.m_SplashJPanel = new SplashJPanel();

		this.add(this.m_BackColorJPanel, BorderLayout.NORTH);
		this.add(this.m_ProgressImageJPanel, BorderLayout.CENTER);
		this.add(this.m_SplashJPanel, BorderLayout.SOUTH);
	}

	/* package */BackColorJPanel getBackColorJPanel()
	{
		return this.m_BackColorJPanel;
	}

	/* package */SplashJPanel getSplashJPanel()
	{
		return this.m_SplashJPanel;
	}

	/* package */ProgressImageJPanel getProgressImageJPanel()
	{
		return this.m_ProgressImageJPanel;
	}
}

class BackColorJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	private JLabel				m_BackColorJLabel;
	private JButton				m_BackColorJButton;

	/* package */BackColorJPanel()
	{
		this.setLayout(new BorderLayout());

		Border b1 = BorderFactory.createEtchedBorder();
		Border b2 = BorderFactory.createTitledBorder(b1, "Background", TitledBorder.LEFT, TitledBorder.ABOVE_TOP);
		this.setBorder(b2);

		this.m_BackColorJLabel = new JLabel();
		this.m_BackColorJLabel.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_BackColorJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
		this.m_BackColorJLabel.setText("Color:");

		this.m_BackColorJButton = new JButton();
		this.m_BackColorJButton.setText(" ");
		this.m_BackColorJButton.setForeground(Color.BLACK);

		this.add(new JPanel(), BorderLayout.NORTH); // phantom
		this.add(this.m_BackColorJLabel, BorderLayout.WEST);
		this.add(this.m_BackColorJButton, BorderLayout.CENTER);
		this.add(new JPanel(), BorderLayout.SOUTH); // phantom
	}

	/* package */JButton getBackcolorJButton()
	{
		return this.m_BackColorJButton;
	}
}

class SplashJPanel
extends JPanel
implements ActionListener
{
	private static final long		serialVersionUID	= 1L;

	private JLabel					m_ShowSplashJLabel;
	private JCheckBox				m_ShowSplashJCheckBox;

	private JLabel					m_UserDefinedSplashJLabel;
	private JCheckBox				m_UserDefinedSplashJCheckBox;

	private JPanel					m_FileJPanel;
	private JLabel					m_UserDefinedSplashImageFileJLabel;
	private JTextField				m_UserDefinedSplashImageFileJTextField;
	private JButton					m_UserDefinedSplashImageFileBrowseJButton;

	private JPanel					m_UpdateJPanel;
	private JButton					m_SplashImageFileUpdateJButton;

	private SplashImageJFileChooser	m_SplashImageJFileChooser;

	/* package */SplashJPanel()
	{
		this.setLayout(new GridLayout(4, 1));

		Border b1 = BorderFactory.createEtchedBorder();
		Border b2 = BorderFactory.createTitledBorder(b1, "Splash", TitledBorder.LEFT, TitledBorder.ABOVE_TOP);
		this.setBorder(b2);

		JPanel showSplashJPanel = new JPanel();
		showSplashJPanel.setLayout(new BorderLayout());

		this.m_ShowSplashJLabel = new JLabel();
		this.m_ShowSplashJLabel.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_ShowSplashJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
		this.m_ShowSplashJLabel.setText("Show:");

		this.m_ShowSplashJCheckBox = new JCheckBox();
		this.m_ShowSplashJCheckBox.setHorizontalAlignment(SwingConstants.RIGHT);
		this.m_ShowSplashJCheckBox.addActionListener(this);
		this.m_ShowSplashJCheckBox.setSelected(true);

		showSplashJPanel.add(this.m_ShowSplashJLabel, BorderLayout.WEST);
		showSplashJPanel.add(this.m_ShowSplashJCheckBox, BorderLayout.CENTER);

		this.add(showSplashJPanel);

		JPanel UserDefinedSplashJPanel = new JPanel();
		UserDefinedSplashJPanel.setLayout(new BorderLayout());

		this.m_UserDefinedSplashJLabel = new JLabel();
		this.m_UserDefinedSplashJLabel.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_UserDefinedSplashJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
		this.m_UserDefinedSplashJLabel.setText("UserDefined:");

		this.m_UserDefinedSplashJCheckBox = new JCheckBox();
		this.m_UserDefinedSplashJCheckBox.setHorizontalAlignment(SwingConstants.RIGHT);
		this.m_UserDefinedSplashJCheckBox.addActionListener(this);

		UserDefinedSplashJPanel.add(this.m_UserDefinedSplashJLabel, BorderLayout.WEST);
		UserDefinedSplashJPanel.add(this.m_UserDefinedSplashJCheckBox, BorderLayout.CENTER);

		this.add(UserDefinedSplashJPanel);

		this.m_FileJPanel = new JPanel();
		this.m_FileJPanel.setLayout(new BorderLayout());

		this.m_UserDefinedSplashImageFileJLabel = new JLabel();
		this.m_UserDefinedSplashImageFileJLabel.setText("File:");
		this.m_UserDefinedSplashImageFileJLabel.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_UserDefinedSplashImageFileJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
		this.m_UserDefinedSplashImageFileJLabel.setEnabled(false);
		this.m_FileJPanel.add(this.m_UserDefinedSplashImageFileJLabel, BorderLayout.WEST);

		this.m_UserDefinedSplashImageFileJTextField = new JTextField();
		this.m_UserDefinedSplashImageFileJTextField.setAlignmentX(LEFT_ALIGNMENT);
		this.m_UserDefinedSplashImageFileJTextField.setBorder(new CompoundBorder(new BevelBorder(BevelBorder.LOWERED), new EmptyBorder(0, 5, 0, 5)));
		this.m_UserDefinedSplashImageFileJTextField.setEnabled(false);
		this.m_UserDefinedSplashImageFileJTextField.setText(SampleCode.getUserDefinedDefaultSplashFilePath());
		this.m_UserDefinedSplashImageFileJTextField.setPreferredSize(new Dimension(100,28));
		this.m_FileJPanel.add(this.m_UserDefinedSplashImageFileJTextField, BorderLayout.CENTER);

		this.m_UserDefinedSplashImageFileBrowseJButton = new JButton();
		this.m_UserDefinedSplashImageFileBrowseJButton.setText("Browse");
		this.m_UserDefinedSplashImageFileBrowseJButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_UserDefinedSplashImageFileBrowseJButton.setEnabled(false);
		this.m_UserDefinedSplashImageFileBrowseJButton.addActionListener(this);
		this.m_FileJPanel.add(this.m_UserDefinedSplashImageFileBrowseJButton, BorderLayout.EAST);

		this.add(this.m_FileJPanel);

		String userDir = AgSystemPropertiesHelper.getUserDir();
		String fileSep = AgSystemPropertiesHelper.getFileSeparator();
		String path = userDir + fileSep + "src";
		this.m_SplashImageJFileChooser = new SplashImageJFileChooser("Splash File Image", path);

		this.m_UpdateJPanel = new JPanel();
		this.m_UpdateJPanel.setLayout(new BorderLayout());

		this.m_SplashImageFileUpdateJButton = new JButton();
		this.m_SplashImageFileUpdateJButton.setText("Update");
		this.m_SplashImageFileUpdateJButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_SplashImageFileUpdateJButton.setEnabled(true);
		this.m_UpdateJPanel.add(this.m_SplashImageFileUpdateJButton, BorderLayout.CENTER);

		this.add(this.m_UpdateJPanel);
	}

	/* package */void addActionListener(ActionListener l)
	{
		this.m_SplashImageFileUpdateJButton.addActionListener(l);
	}

	/* package */void removeActionListener(ActionListener l)
	{
		this.m_SplashImageFileUpdateJButton.removeActionListener(l);
	}

	/* package */boolean isUpdateJButton(Object src)
	{
		return src.equals(this.m_SplashImageFileUpdateJButton);
	}

	/* package */boolean getShowSplashScreen()
	{
		return this.m_ShowSplashJCheckBox.isSelected();
	}

	/* package */boolean getUseUserDefinedSplashScreen()
	{
		return this.m_UserDefinedSplashJCheckBox.isSelected();
	}

	/* package */String getUserDefinedSplashScreenPath()
	{
		return this.m_UserDefinedSplashImageFileJTextField.getText();
	}

	//	public void setUseUserDefinedSplashScreenEnabled(boolean showSplash)
//	{
//		this.m_UserDefinedSplashJCheckBox.setEnabled(showSplash);
//	}
//
	public void actionPerformed(ActionEvent ae)
	{
		try
		{
			Object src = ae.getSource();
			if(src.equals(this.m_ShowSplashJCheckBox))
			{
				if(this.m_ShowSplashJCheckBox.isSelected())
				{
					this.m_UserDefinedSplashJCheckBox.setEnabled(true);
				}
				else
				{
					this.m_UserDefinedSplashJCheckBox.setEnabled(false);
				}
			}
			else if(src.equals(this.m_UserDefinedSplashJCheckBox))
			{
				if(this.m_UserDefinedSplashJCheckBox.isSelected())
				{
					this.m_UserDefinedSplashImageFileJLabel.setEnabled(true);
					this.m_UserDefinedSplashImageFileJTextField.setEnabled(true);
					this.m_UserDefinedSplashImageFileBrowseJButton.setEnabled(true);
				}
				else
				{
					this.m_UserDefinedSplashImageFileJLabel.setEnabled(false);
					this.m_UserDefinedSplashImageFileJTextField.setEnabled(false);
					this.m_UserDefinedSplashImageFileBrowseJButton.setEnabled(false);
				}
			}
			else if(src.equals(this.m_UserDefinedSplashImageFileBrowseJButton))
			{
				if(this.m_SplashImageJFileChooser != null)
				{
					int returnVal = this.m_SplashImageJFileChooser.showOpenDialog(this);
					if(returnVal == JFileChooser.APPROVE_OPTION)
					{
						File file = this.m_SplashImageJFileChooser.getSelectedFile();
						this.m_UserDefinedSplashImageFileJTextField.setText(file.getAbsolutePath());
					}
				}
				else
				{
					throw new AgException("SplashImageJFileChooser was null");
				}
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}
}

class SplashImageJFileChooser
extends JFileChooser
{
	private static final long	serialVersionUID	= 1L;

	public SplashImageJFileChooser(String title, String initialDirectory)
	{
		this.setDialogTitle(title);
		this.setFileFilter(new SplashImageFileFilter());
		this.setAcceptAllFileFilterUsed(false);
		this.setMultiSelectionEnabled(false);
		this.setFileHidingEnabled(true);

		if(initialDirectory != null)
		{
			File f = new File(initialDirectory);

			if(f.exists())
			{
				this.setCurrentDirectory(f);
			}
		}
	}
}

class SplashImageFileFilter
extends javax.swing.filechooser.FileFilter
{
	public boolean accept(File f)
	{
		if(f.isDirectory())
		{
			return true;
		}

		String fileName = f.getName();

		int pos = fileName.lastIndexOf('.');

		if(pos > 0 && pos < fileName.length() - 1)
		{
			String extension = fileName.substring(pos + 1).toLowerCase();

			if(extension.equalsIgnoreCase("bmp") || 
				extension.equalsIgnoreCase("jpg") ||
				extension.equalsIgnoreCase("gif")) 
			{
				return true;
			}
		}

		return false;
	}

	public String getDescription()
	{
		return "Bmp Files";
	}
}

class ProgressImageJPanel
extends JPanel
implements ActionListener
{
	private static final long			serialVersionUID	= 1L;

	private JPanel						m_UseJPanel;
	private JRadioButton				m_UseNoProgressImageFileJRadioButton;
	private JRadioButton				m_UseDefaultProgressImageFileJRadioButton;
	private JRadioButton				m_UseUserDefinedProgressImageFileJRadioButton;

	private JPanel						m_FileJPanel;
	private JLabel						m_UserDefinedProgressImageFileJLabel;
	private JTextField					m_UserDefinedProgressImageFileJTextField;
	private JButton						m_UserDefinedProgressImageFileBrowseJButton;

	private JPanel						m_XOffsetJPanel;
	private JLabel						m_XOffsetJLabel;
	private JTextField					m_XOffsetJTextField;

	private JPanel						m_YOffsetJPanel;
	private JLabel						m_YOffsetJLabel;
	private JTextField					m_YOffsetJTextField;

	private JPanel						m_XOriginJPanel;
	private JLabel						m_XOriginJLabel;
	private JTextField					m_XOriginJTextField;

	private JPanel						m_YOriginJPanel;
	private JLabel						m_YOriginJLabel;
	private JTextField					m_YOriginJTextField;

	private JPanel						m_UpdateJPanel;
	private JButton						m_ProgressImageFileUpdateJButton;

	private ProgressImageJFileChooser	m_ProgressImageJFileChooser;

	/* package */ProgressImageJPanel()
	{
		this.setLayout(new GridLayout(12, 1));

		Border b1 = BorderFactory.createEtchedBorder();
		Border b2 = BorderFactory.createTitledBorder(b1, "Progress Image", TitledBorder.LEFT, TitledBorder.ABOVE_TOP);
		this.setBorder(b2);

		this.m_UseJPanel = new JPanel();
		this.m_UseJPanel.setLayout(new GridLayout(1, 2));

		this.m_UseNoProgressImageFileJRadioButton = new JRadioButton();
		this.m_UseNoProgressImageFileJRadioButton.setText("Use None");
		this.m_UseNoProgressImageFileJRadioButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_UseNoProgressImageFileJRadioButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_UseNoProgressImageFileJRadioButton.addActionListener(this);
		this.m_UseJPanel.add(this.m_UseNoProgressImageFileJRadioButton);

		this.m_UseDefaultProgressImageFileJRadioButton = new JRadioButton();
		this.m_UseDefaultProgressImageFileJRadioButton.setText("Use default");
		this.m_UseDefaultProgressImageFileJRadioButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_UseDefaultProgressImageFileJRadioButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_UseDefaultProgressImageFileJRadioButton.addActionListener(this);
		this.m_UseJPanel.add(this.m_UseDefaultProgressImageFileJRadioButton);

		this.m_UseUserDefinedProgressImageFileJRadioButton = new JRadioButton();
		this.m_UseUserDefinedProgressImageFileJRadioButton.setText("Use User Defined");
		this.m_UseUserDefinedProgressImageFileJRadioButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_UseUserDefinedProgressImageFileJRadioButton.setHorizontalAlignment(SwingConstants.LEADING);
		this.m_UseUserDefinedProgressImageFileJRadioButton.addActionListener(this);
		this.m_UseJPanel.add(this.m_UseUserDefinedProgressImageFileJRadioButton);

		ButtonGroup bg = new ButtonGroup();
		bg.add(this.m_UseNoProgressImageFileJRadioButton);
		bg.add(this.m_UseDefaultProgressImageFileJRadioButton);
		bg.add(this.m_UseUserDefinedProgressImageFileJRadioButton);

		this.m_UseDefaultProgressImageFileJRadioButton.setSelected(true);

		this.add(this.m_UseJPanel);

		this.m_FileJPanel = new JPanel();
		this.m_FileJPanel.setLayout(new BorderLayout());

		this.m_UserDefinedProgressImageFileJLabel = new JLabel();
		this.m_UserDefinedProgressImageFileJLabel.setText("File:");
		this.m_UserDefinedProgressImageFileJLabel.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_UserDefinedProgressImageFileJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
		this.m_UserDefinedProgressImageFileJLabel.setEnabled(false);
		this.m_FileJPanel.add(this.m_UserDefinedProgressImageFileJLabel, BorderLayout.WEST);

		this.m_UserDefinedProgressImageFileJTextField = new JTextField();
		this.m_UserDefinedProgressImageFileJTextField.setAlignmentX(LEFT_ALIGNMENT);
		this.m_UserDefinedProgressImageFileJTextField.setBorder(new CompoundBorder(new BevelBorder(BevelBorder.LOWERED), new EmptyBorder(0, 5, 0, 5)));
		this.m_UserDefinedProgressImageFileJTextField.setEnabled(false);
		this.m_UserDefinedProgressImageFileJTextField.setText(SampleCode.getUserDefinedDefaultProgressImageFilePath());
		this.m_UserDefinedProgressImageFileJTextField.setPreferredSize(new Dimension(100,28));
		this.m_FileJPanel.add(this.m_UserDefinedProgressImageFileJTextField, BorderLayout.CENTER);

		this.m_UserDefinedProgressImageFileBrowseJButton = new JButton();
		this.m_UserDefinedProgressImageFileBrowseJButton.setText("Browse");
		this.m_UserDefinedProgressImageFileBrowseJButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_UserDefinedProgressImageFileBrowseJButton.setEnabled(false);
		this.m_UserDefinedProgressImageFileBrowseJButton.addActionListener(this);
		this.m_FileJPanel.add(this.m_UserDefinedProgressImageFileBrowseJButton, BorderLayout.EAST);

		this.add(this.m_FileJPanel);

		this.m_XOffsetJPanel = new JPanel();
		this.m_XOffsetJPanel.setLayout(new BorderLayout());

		this.m_XOffsetJLabel = new JLabel();
		this.m_XOffsetJLabel.setText("X Offset:");
		this.m_XOffsetJLabel.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_XOffsetJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
		this.m_XOffsetJLabel.setEnabled(false);
		this.m_XOffsetJPanel.add(this.m_XOffsetJLabel, BorderLayout.WEST);

		this.m_XOffsetJTextField = new JTextField();
		this.m_XOffsetJTextField.setText("-75");
		this.m_XOffsetJTextField.setHorizontalAlignment(SwingConstants.RIGHT);
		this.m_XOffsetJTextField.setBorder(new CompoundBorder(new BevelBorder(BevelBorder.LOWERED), new EmptyBorder(0, 5, 0, 5)));
		this.m_XOffsetJTextField.setEnabled(false);
		this.m_XOffsetJPanel.add(this.m_XOffsetJTextField, BorderLayout.CENTER);

		this.add(this.m_XOffsetJPanel);

		this.m_YOffsetJPanel = new JPanel();
		this.m_YOffsetJPanel.setLayout(new BorderLayout());

		this.m_YOffsetJLabel = new JLabel();
		this.m_YOffsetJLabel.setText("Y Offset:");
		this.m_YOffsetJLabel.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_YOffsetJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
		this.m_YOffsetJLabel.setEnabled(false);
		this.m_YOffsetJPanel.add(this.m_YOffsetJLabel, BorderLayout.WEST);

		this.m_YOffsetJTextField = new JTextField();
		this.m_YOffsetJTextField.setText("150");
		this.m_YOffsetJTextField.setHorizontalAlignment(SwingConstants.RIGHT);
		this.m_YOffsetJTextField.setBorder(new CompoundBorder(new BevelBorder(BevelBorder.LOWERED), new EmptyBorder(0, 5, 0, 5)));
		this.m_YOffsetJTextField.setEnabled(false);
		this.m_YOffsetJPanel.add(this.m_YOffsetJTextField, BorderLayout.CENTER);

		this.add(this.m_YOffsetJPanel);

		this.m_XOriginJPanel = new JPanel();
		this.m_XOriginJPanel.setLayout(new BorderLayout());

		this.m_XOriginJLabel = new JLabel();
		this.m_XOriginJLabel.setText("X Origin:");
		this.m_XOriginJLabel.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_XOriginJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
		this.m_XOriginJLabel.setEnabled(false);
		this.m_XOriginJPanel.add(this.m_XOriginJLabel, BorderLayout.WEST);

		this.m_XOriginJTextField = new JTextField();
		this.m_XOriginJTextField.setText("0");
		this.m_XOriginJTextField.setHorizontalAlignment(SwingConstants.RIGHT);
		this.m_XOriginJTextField.setBorder(new CompoundBorder(new BevelBorder(BevelBorder.LOWERED), new EmptyBorder(0, 5, 0, 5)));
		this.m_XOriginJTextField.setEnabled(false);
		this.m_XOriginJPanel.add(this.m_XOriginJTextField, BorderLayout.CENTER);

		this.add(this.m_XOriginJPanel);

		this.m_YOriginJPanel = new JPanel();
		this.m_YOriginJPanel.setLayout(new BorderLayout());

		this.m_YOriginJLabel = new JLabel();
		this.m_YOriginJLabel.setText("Y Origin:");
		this.m_YOriginJLabel.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_YOriginJLabel.setBorder(new EmptyBorder(0, 5, 0, 5));
		this.m_YOriginJLabel.setEnabled(false);
		this.m_YOriginJPanel.add(this.m_YOriginJLabel, BorderLayout.WEST);

		this.m_YOriginJTextField = new JTextField();
		this.m_YOriginJTextField.setText("0");
		this.m_YOriginJTextField.setHorizontalAlignment(SwingConstants.RIGHT);
		this.m_YOriginJTextField.setBorder(new CompoundBorder(new BevelBorder(BevelBorder.LOWERED), new EmptyBorder(0, 5, 0, 5)));
		this.m_YOriginJTextField.setEnabled(false);
		this.m_YOriginJPanel.add(this.m_YOriginJTextField, BorderLayout.CENTER);

		this.add(this.m_YOriginJPanel);

		this.m_UpdateJPanel = new JPanel();
		this.m_UpdateJPanel.setLayout(new BorderLayout());

		this.m_ProgressImageFileUpdateJButton = new JButton();
		this.m_ProgressImageFileUpdateJButton.setText("Update");
		this.m_ProgressImageFileUpdateJButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_UpdateJPanel.add(this.m_ProgressImageFileUpdateJButton, BorderLayout.CENTER);

		this.add(this.m_UpdateJPanel);

		String userDir = AgSystemPropertiesHelper.getUserDir();
		String fileSep = AgSystemPropertiesHelper.getFileSeparator();
		String path = userDir + fileSep + "src";
		this.m_ProgressImageJFileChooser = new ProgressImageJFileChooser("Progress File Image", path);
	}

	/* package */void addActionListener(ActionListener l)
	{
		this.m_ProgressImageFileUpdateJButton.addActionListener(l);
	}

	/* package */void removeActionListener(ActionListener l)
	{
		this.m_ProgressImageFileUpdateJButton.removeActionListener(l);
	}

	/* package */boolean isUpdateJButton(Object src)
	{
		return src.equals(this.m_ProgressImageFileUpdateJButton);
	}

	/* package */boolean getUseNoProgressImageFile()
	{
		return this.m_UseNoProgressImageFileJRadioButton.isSelected();
	}

	/* package */boolean getUseDefaultProgressImageFile()
	{
		return this.m_UseDefaultProgressImageFileJRadioButton.isSelected();
	}

	/* package */boolean getUseUserDefinedProgressImageFile()
	{
		return this.m_UseUserDefinedProgressImageFileJRadioButton.isSelected();
	}

	/* package */String getUserDefinedProgressImageFilePath()
	{
		return this.m_UserDefinedProgressImageFileJTextField.getText();
	}

	public void actionPerformed(ActionEvent ae)
	{
		try
		{
			Object src = ae.getSource();
			if(src.equals(this.m_UseNoProgressImageFileJRadioButton))
			{
				this.m_UserDefinedProgressImageFileJLabel.setEnabled(false);
				this.m_UserDefinedProgressImageFileJTextField.setEnabled(false);
				this.m_UserDefinedProgressImageFileBrowseJButton.setEnabled(false);

				this.m_XOffsetJLabel.setEnabled(false);
				this.m_XOffsetJTextField.setEnabled(false);

				this.m_YOffsetJLabel.setEnabled(false);
				this.m_YOffsetJTextField.setEnabled(false);

				this.m_XOriginJLabel.setEnabled(false);
				this.m_XOriginJTextField.setEnabled(false);

				this.m_YOriginJLabel.setEnabled(false);
				this.m_YOriginJTextField.setEnabled(false);
			}
			else if(src.equals(this.m_UseDefaultProgressImageFileJRadioButton))
			{
				this.m_UserDefinedProgressImageFileJLabel.setEnabled(false);
				this.m_UserDefinedProgressImageFileJTextField.setEnabled(false);
				this.m_UserDefinedProgressImageFileBrowseJButton.setEnabled(false);

				this.m_XOffsetJLabel.setEnabled(false);
				this.m_XOffsetJTextField.setEnabled(false);

				this.m_YOffsetJLabel.setEnabled(false);
				this.m_YOffsetJTextField.setEnabled(false);

				this.m_XOriginJLabel.setEnabled(false);
				this.m_XOriginJTextField.setEnabled(false);

				this.m_YOriginJLabel.setEnabled(false);
				this.m_YOriginJTextField.setEnabled(false);
			}
			else if(src.equals(this.m_UseUserDefinedProgressImageFileJRadioButton))
			{
				this.m_UserDefinedProgressImageFileJLabel.setEnabled(true);
				this.m_UserDefinedProgressImageFileJTextField.setEnabled(true);
				this.m_UserDefinedProgressImageFileBrowseJButton.setEnabled(true);

				this.m_XOffsetJLabel.setEnabled(true);
				this.m_XOffsetJTextField.setEnabled(true);

				this.m_YOffsetJLabel.setEnabled(true);
				this.m_YOffsetJTextField.setEnabled(true);

				this.m_XOriginJLabel.setEnabled(true);
				this.m_XOriginJTextField.setEnabled(true);

				this.m_YOriginJLabel.setEnabled(true);
				this.m_YOriginJTextField.setEnabled(true);
			}
			else if(src.equals(this.m_UserDefinedProgressImageFileBrowseJButton))
			{
				if(this.m_ProgressImageJFileChooser != null)
				{
					int returnVal = this.m_ProgressImageJFileChooser.showOpenDialog(this);
					if(returnVal == JFileChooser.APPROVE_OPTION)
					{
						File file = this.m_ProgressImageJFileChooser.getSelectedFile();
						this.m_UserDefinedProgressImageFileJTextField.setText(file.getAbsolutePath());
					}
				}
				else
				{
					throw new AgException("ProgressImageJFileChooser was null");
				}
			}
		}
		catch(Throwable t)
		{
			t.printStackTrace();
		}
	}

	/* package */int getXOrigin()
	{
		return Integer.parseInt(this.m_XOriginJTextField.getText());
	}

	/* package */int getYOrigin()
	{
		return Integer.parseInt(this.m_YOriginJTextField.getText());
	}

	/* package */int getXOffset()
	{
		return Integer.parseInt(this.m_XOffsetJTextField.getText());
	}

	/* package */int getYOffset()
	{
		return Integer.parseInt(this.m_YOffsetJTextField.getText());
	}
}

class ProgressImageJFileChooser
extends JFileChooser
{
	private static final long	serialVersionUID	= 1L;

	public ProgressImageJFileChooser(String title, String initialDirectory)
	{
		this.setDialogTitle(title);
		this.setFileFilter(new ProgressImageFileFilter());
		this.setAcceptAllFileFilterUsed(false);
		this.setMultiSelectionEnabled(false);
		this.setFileHidingEnabled(true);

		if(initialDirectory != null)
		{
			File f = new File(initialDirectory);

			if(f.exists())
			{
				this.setCurrentDirectory(f);
			}
		}
	}
}

class ProgressImageFileFilter
extends javax.swing.filechooser.FileFilter
{
	public boolean accept(File f)
	{
		if(f.isDirectory())
		{
			return true;
		}

		String fileName = f.getName();

		int pos = fileName.lastIndexOf('.');

		if(pos > 0 && pos < fileName.length() - 1)
		{
			String extension = fileName.substring(pos + 1).toLowerCase();

			if(extension.equals("gif"))
			{
				return true;
			}
		}

		return false;
	}

	public String getDescription()
	{
		return "Animated Gif Files";
	}
}
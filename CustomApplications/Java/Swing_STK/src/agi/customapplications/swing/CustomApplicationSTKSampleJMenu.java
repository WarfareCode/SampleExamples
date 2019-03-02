package agi.customapplications.swing;

//Java
import java.awt.event.*;
import javax.swing.*;

public class CustomApplicationSTKSampleJMenu
extends JMenu
{
	private static final long	serialVersionUID	= 1L;

	private final static String	s_TITLE	= "Sample";

	public CustomApplicationSTKSampleJMenu()
	{
		JPopupMenu.setDefaultLightWeightPopupEnabled(false);

		this.setText(s_TITLE);
		this.setMnemonic(KeyEvent.VK_P);
	}
}
//Java
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

//AGI Java API
/* package */class SampleJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	private final static String	s_STEP1_TEXT	= "Create Scenario";
	private final static String	s_STEP2_TEXT	= "Create planets";
	private final static String	s_STEP3_TEXT	= "Spacecraft Graphics";
	private final static String	s_STEP4_TEXT	= "2D Graphics Window";
	private final static String	s_STEP5_TEXT	= "3D Graphics Window: Earth Centered";
	private final static String	s_STEP6_TEXT	= "3D Graphics Window: Moon-Centered";
	private final static String	s_STEP7_TEXT	= "MCS Setup";
	private final static String	s_STEP8_TEXT	= "Trans-Lunar Injection: First Guess";
	private final static String	s_STEP9_TEXT	= "Set up the targeter to calculate launch and coast times";
	private final static String	s_STEP10_TEXT	= "Run the targeter";
	private final static String	s_STEP11_TEXT	= "Setting up the Targeter to Target on the B_Plane";
	private final static String	s_STEP12_TEXT	= "Drawing the B-Plane";
	private final static String	s_STEP13_TEXT	= "Running the Targeter to Achieve B_Plane Params";
	private final static String	s_STEP14_TEXT	= "Targeting Altitude and Inclination";
	private final static String	s_STEP15_TEXT	= "Approaching the moon";
	private final static String	s_STEP16_TEXT	= "Lunar Orbit Insertion";
	private final static String	s_STEP17_TEXT	= "Close Scenario";

	/* package */JButton		m_Step1JButton;
	/* package */JButton		m_Step2JButton;
	/* package */JButton		m_Step3JButton;
	/* package */JButton		m_Step4JButton;
	/* package */JButton		m_Step5JButton;
	/* package */JButton		m_Step6JButton;
	/* package */JButton		m_Step7JButton;
	/* package */JButton		m_Step8JButton;
	/* package */JButton		m_Step9JButton;
	/* package */JButton		m_Step10JButton;
	/* package */JButton		m_Step11JButton;
	/* package */JButton		m_Step12JButton;
	/* package */JButton		m_Step13JButton;
	/* package */JButton		m_Step14JButton;
	/* package */JButton		m_Step15JButton;
	/* package */JButton		m_Step16JButton;
	/* package */JButton		m_Step17JButton;

	public SampleJPanel()
	{
		this.setLayout(new GridLayout(17, 1));

		this.m_Step1JButton = new JButton(s_STEP1_TEXT);
		this.m_Step1JButton.setEnabled(true);
		this.add(this.m_Step1JButton);

		this.m_Step2JButton = new JButton(s_STEP2_TEXT);
		this.m_Step2JButton.setEnabled(false);
		this.add(this.m_Step2JButton);

		this.m_Step3JButton = new JButton(s_STEP3_TEXT);
		this.m_Step3JButton.setEnabled(false);
		this.add(this.m_Step3JButton);

		this.m_Step4JButton = new JButton(s_STEP4_TEXT);
		this.m_Step4JButton.setEnabled(false);
		this.add(this.m_Step4JButton);

		this.m_Step5JButton = new JButton(s_STEP5_TEXT);
		this.m_Step5JButton.setEnabled(false);
		this.add(this.m_Step5JButton);

		this.m_Step6JButton = new JButton(s_STEP6_TEXT);
		this.m_Step6JButton.setEnabled(false);
		this.add(this.m_Step6JButton);

		this.m_Step7JButton = new JButton(s_STEP7_TEXT);
		this.m_Step7JButton.setEnabled(false);
		this.add(this.m_Step7JButton);

		this.m_Step8JButton = new JButton(s_STEP8_TEXT);
		this.m_Step8JButton.setEnabled(false);
		this.add(this.m_Step8JButton);

		this.m_Step9JButton = new JButton(s_STEP9_TEXT);
		this.m_Step9JButton.setEnabled(false);
		this.add(this.m_Step9JButton);

		this.m_Step10JButton = new JButton(s_STEP10_TEXT);
		this.m_Step10JButton.setEnabled(false);
		this.add(this.m_Step10JButton);

		this.m_Step11JButton = new JButton(s_STEP11_TEXT);
		this.m_Step11JButton.setEnabled(false);
		this.add(this.m_Step11JButton);

		this.m_Step12JButton = new JButton(s_STEP12_TEXT);
		this.m_Step12JButton.setEnabled(false);
		this.add(this.m_Step12JButton);

		this.m_Step13JButton = new JButton(s_STEP13_TEXT);
		this.m_Step13JButton.setEnabled(false);
		this.add(this.m_Step13JButton);

		this.m_Step14JButton = new JButton(s_STEP14_TEXT);
		this.m_Step14JButton.setEnabled(false);
		this.add(this.m_Step14JButton);

		this.m_Step15JButton = new JButton(s_STEP15_TEXT);
		this.m_Step15JButton.setEnabled(false);
		this.add(this.m_Step15JButton);

		this.m_Step16JButton = new JButton(s_STEP16_TEXT);
		this.m_Step16JButton.setEnabled(false);
		this.add(this.m_Step16JButton);

		this.m_Step17JButton = new JButton(s_STEP17_TEXT);
		this.m_Step17JButton.setEnabled(false);
		this.add(this.m_Step17JButton);
	}

	public void addActionListener(ActionListener al)
	{
		this.m_Step1JButton.addActionListener(al);
		this.m_Step2JButton.addActionListener(al);
		this.m_Step3JButton.addActionListener(al);
		this.m_Step4JButton.addActionListener(al);
		this.m_Step5JButton.addActionListener(al);
		this.m_Step6JButton.addActionListener(al);
		this.m_Step7JButton.addActionListener(al);
		this.m_Step8JButton.addActionListener(al);
		this.m_Step9JButton.addActionListener(al);
		this.m_Step10JButton.addActionListener(al);
		this.m_Step11JButton.addActionListener(al);
		this.m_Step12JButton.addActionListener(al);
		this.m_Step13JButton.addActionListener(al);
		this.m_Step14JButton.addActionListener(al);
		this.m_Step15JButton.addActionListener(al);
		this.m_Step16JButton.addActionListener(al);
		this.m_Step17JButton.addActionListener(al);
	}
}
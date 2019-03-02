//Java API
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class SampleJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	private final static String	s_STEP1_TEXT	= "New Scenario";
	private final static String	s_STEP2_TEXT	= "Define planets";
	private final static String	s_STEP3_TEXT	= "Set up Graphics";
	private final static String	s_STEP4_TEXT	= "Define the Initial State";
	private final static String	s_STEP5_TEXT	= "Propagating the Interplanetary Trajectory";
	private final static String	s_STEP6_TEXT	= "Stop Near Mars";
	private final static String	s_STEP7_TEXT	= "Create a Mars Point Mass Propagator";
	private final static String	s_STEP8_TEXT	= "Target a Circular Orbit";
	private final static String	s_STEP9_TEXT	= "Propagate the Orbit";
	private final static String	s_STEP10_TEXT	= "Close Scenario";

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

	public SampleJPanel()
	{
		this.setLayout(new GridLayout(10, 1));

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
	}
}
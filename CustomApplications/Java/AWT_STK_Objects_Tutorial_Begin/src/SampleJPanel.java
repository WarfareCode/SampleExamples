import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class SampleJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	public final static String	s_STEP_1			= "Scenario";
	public final static String	s_STEP_2			= "Facility";
	public final static String	s_STEP_3			= "Satellite";
	public final static String	s_STEP_4			= "Access";
	public final static String	s_STEP_5			= "Vectors";
	public final static String	s_STEP_6			= "Data Display";
	public final static String	s_STEP_7			= "Data Providers";
	public final static String	s_STEP_8			= "Save/Unload Scenario";

	private JButton				m_stepOneButton;
	private JButton				m_stepTwoButton;
	private JButton				m_stepThreeButton;
	private JButton				m_stepFourButton;
	private JButton				m_stepFiveButton;
	private JButton				m_stepSixButton;
	private JButton				m_stepSevenButton;
	private JButton				m_stepEightButton;

	public SampleJPanel()
	{
		super();
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout(new BoxLayout(this, BoxLayout.PAGE_AXIS));

		int w = 225;
		int h = 30;

		this.m_stepOneButton = new JButton(s_STEP_1);
		this.m_stepOneButton.setEnabled(true);
		this.m_stepOneButton.setSize(new Dimension(w, h));
		this.m_stepOneButton.setMinimumSize(new Dimension(w, h));
		this.m_stepOneButton.setMaximumSize(new Dimension(w, h));
		this.m_stepOneButton.setPreferredSize(new Dimension(w, h));
		this.m_stepOneButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepOneButton.setHorizontalAlignment(SwingConstants.LEADING);
		this.add(this.m_stepOneButton);

		this.m_stepTwoButton = new JButton(s_STEP_2);
		this.m_stepTwoButton.setEnabled(false);
		this.m_stepTwoButton.setSize(new Dimension(w, h));
		this.m_stepTwoButton.setMinimumSize(new Dimension(w, h));
		this.m_stepTwoButton.setMaximumSize(new Dimension(w, h));
		this.m_stepTwoButton.setPreferredSize(new Dimension(w, h));
		this.m_stepTwoButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepTwoButton.setHorizontalAlignment(SwingConstants.LEADING);
		this.add(this.m_stepTwoButton);

		this.m_stepThreeButton = new JButton(s_STEP_3);
		this.m_stepThreeButton.setEnabled(false);
		this.m_stepThreeButton.setSize(new Dimension(w, h));
		this.m_stepThreeButton.setMinimumSize(new Dimension(w, h));
		this.m_stepThreeButton.setMaximumSize(new Dimension(w, h));
		this.m_stepThreeButton.setPreferredSize(new Dimension(w, h));
		this.m_stepThreeButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepThreeButton.setHorizontalAlignment(SwingConstants.LEADING);
		this.add(this.m_stepThreeButton);

		this.m_stepFourButton = new JButton(s_STEP_4);
		this.m_stepFourButton.setEnabled(false);
		this.m_stepFourButton.setSize(new Dimension(w, h));
		this.m_stepFourButton.setMinimumSize(new Dimension(w, h));
		this.m_stepFourButton.setMaximumSize(new Dimension(w, h));
		this.m_stepFourButton.setPreferredSize(new Dimension(w, h));
		this.m_stepFourButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepFourButton.setHorizontalAlignment(SwingConstants.LEADING);
		this.add(this.m_stepFourButton);

		this.m_stepFiveButton = new JButton(s_STEP_5);
		this.m_stepFiveButton.setEnabled(false);
		this.m_stepFiveButton.setSize(new Dimension(w, h));
		this.m_stepFiveButton.setMinimumSize(new Dimension(w, h));
		this.m_stepFiveButton.setMaximumSize(new Dimension(w, h));
		this.m_stepFiveButton.setPreferredSize(new Dimension(w, h));
		this.m_stepFiveButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepFiveButton.setHorizontalAlignment(SwingConstants.LEADING);
		this.add(this.m_stepFiveButton);

		this.m_stepSixButton = new JButton(s_STEP_6);
		this.m_stepSixButton.setEnabled(false);
		this.m_stepSixButton.setSize(new Dimension(w, h));
		this.m_stepSixButton.setMinimumSize(new Dimension(w, h));
		this.m_stepSixButton.setMaximumSize(new Dimension(w, h));
		this.m_stepSixButton.setPreferredSize(new Dimension(w, h));
		this.m_stepSixButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepSixButton.setHorizontalAlignment(SwingConstants.LEADING);
		this.add(this.m_stepSixButton);

		this.m_stepSevenButton = new JButton(s_STEP_7);
		this.m_stepSevenButton.setEnabled(false);
		this.m_stepSevenButton.setSize(new Dimension(w, h));
		this.m_stepSevenButton.setMinimumSize(new Dimension(w, h));
		this.m_stepSevenButton.setMaximumSize(new Dimension(w, h));
		this.m_stepSevenButton.setPreferredSize(new Dimension(w, h));
		this.m_stepSevenButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepSevenButton.setHorizontalAlignment(SwingConstants.LEADING);
		this.add(this.m_stepSevenButton);

		this.m_stepEightButton = new JButton(s_STEP_8);
		this.m_stepEightButton.setEnabled(false);
		this.m_stepEightButton.setSize(new Dimension(w, h));
		this.m_stepEightButton.setMinimumSize(new Dimension(w, h));
		this.m_stepEightButton.setMaximumSize(new Dimension(w, h));
		this.m_stepEightButton.setPreferredSize(new Dimension(w, h));
		this.m_stepEightButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepEightButton.setHorizontalAlignment(SwingConstants.LEADING);
		this.add(this.m_stepEightButton);
	}

	public void addActionListener(ActionListener al)
	{
		this.m_stepOneButton.addActionListener(al);
		this.m_stepTwoButton.addActionListener(al);
		this.m_stepThreeButton.addActionListener(al);
		this.m_stepFourButton.addActionListener(al);
		this.m_stepFiveButton.addActionListener(al);
		this.m_stepSixButton.addActionListener(al);
		this.m_stepSevenButton.addActionListener(al);
		this.m_stepEightButton.addActionListener(al);
	}

	public JButton getStepOne()
	{
		return this.m_stepOneButton;
	}

	public JButton getStepTwo()
	{
		return this.m_stepTwoButton;
	}

	public JButton getStepThree()
	{
		return this.m_stepThreeButton;
	}

	public JButton getStepFour()
	{
		return this.m_stepFourButton;
	}

	public JButton getStepFive()
	{
		return this.m_stepFiveButton;
	}

	public JButton getStepSix()
	{
		return this.m_stepSixButton;
	}

	public JButton getStepSeven()
	{
		return this.m_stepSevenButton;
	}

	public JButton getStepEight()
	{
		return this.m_stepEightButton;
	}
}
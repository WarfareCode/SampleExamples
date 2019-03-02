//Java
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;

public class SampleJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	public final static String	s_STEP_1			= "Scenario";
	public final static String	s_STEP_2			= "Home Facility";
	public final static String	s_STEP_3			= "Home Facility Vectors";
	public final static String	s_STEP_4			= "Sam Site";
	public final static String	s_STEP_5			= "Sam Site Rings";
	public final static String	s_STEP_6			= "Sam Site Spinning Sensor";
	public final static String	s_STEP_7			= "Sam Site Threat Dome";
	public final static String	s_STEP_8			= "Safe Air Corridor";
	public final static String	s_STEP_9			= "F16 Flight Path";
	public final static String	s_STEP_10			= "F16 Range Rings";
	public final static String	s_STEP_11			= "F16 Vectors";
	public final static String	s_STEP_12			= "F16 DropDown Lines";
	public final static String	s_STEP_13			= "F16 Data Display";
	public final static String	s_STEP_14			= "Sam F16 Targeted Sensor";
	public final static String	s_STEP_15			= "Ground Vehicle";
	public final static String	s_STEP_16			= "Satellite";
	public final static String	s_STEP_17			= "Ship";
	public final static String	s_STEP_18			= "MTO";
	public final static String	s_STEP_19			= "Access";
	public final static String	s_STEP_20			= "Data Providers";
	public final static String	s_STEP_21			= "Reset";

	private JButton				m_stepOneButton;
	private JButton				m_stepTwoButton;
	private JButton				m_stepThreeButton;
	private JButton				m_stepFourButton;
	private JButton				m_stepFiveButton;
	private JButton				m_stepSixButton;
	private JButton				m_stepSevenButton;
	private JButton				m_stepEightButton;
	private JButton				m_stepNineButton;
	private JButton				m_stepTenButton;
	private JButton				m_stepElevenButton;
	private JButton				m_stepTwelveButton;
	private JButton				m_stepThirteenButton;
	private JButton				m_stepFourteenButton;
	private JButton				m_stepFifteenButton;
	private JButton				m_stepSixteenButton;
	private JButton				m_stepSeventeenButton;
	private JButton				m_stepEighteenButton;
	private JButton				m_stepNineteenButton;
	private JButton				m_stepTwentyButton;
	private JButton				m_stepTwentyOneButton;

	public SampleJPanel()
	{
		super();
		this.initialize();
	}

	private void initialize()
	{
		this.setLayout(new GridLayout(22, 1));

		this.m_stepOneButton = new JButton(s_STEP_1);
		this.m_stepOneButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepOneButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepOneButton);

		this.m_stepTwoButton = new JButton(s_STEP_2);
		this.m_stepTwoButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepTwoButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepTwoButton);

		this.m_stepThreeButton = new JButton(s_STEP_3);
		this.m_stepThreeButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepThreeButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepThreeButton);

		this.m_stepFourButton = new JButton(s_STEP_4);
		this.m_stepFourButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepFourButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepFourButton);

		this.m_stepFiveButton = new JButton(s_STEP_5);
		this.m_stepFiveButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepFiveButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepFiveButton);

		this.m_stepSixButton = new JButton(s_STEP_6);
		this.m_stepSixButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepSixButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepSixButton);

		this.m_stepSevenButton = new JButton(s_STEP_7);
		this.m_stepSevenButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepSevenButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepSevenButton);

		this.m_stepEightButton = new JButton(s_STEP_8);
		this.m_stepEightButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepEightButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepEightButton);

		this.m_stepNineButton = new JButton(s_STEP_9);
		this.m_stepNineButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepNineButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepNineButton);

		this.m_stepTenButton = new JButton(s_STEP_10);
		this.m_stepTenButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepTenButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepTenButton);

		this.m_stepElevenButton = new JButton(s_STEP_11);
		this.m_stepElevenButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepElevenButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepElevenButton);

		this.m_stepTwelveButton = new JButton(s_STEP_12);
		this.m_stepTwelveButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepTwelveButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepTwelveButton);

		this.m_stepThirteenButton = new JButton(s_STEP_13);
		this.m_stepThirteenButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepThirteenButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepThirteenButton);

		this.m_stepFourteenButton = new JButton(s_STEP_14);
		this.m_stepFourteenButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepFourteenButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepFourteenButton);

		this.m_stepFifteenButton = new JButton(s_STEP_15);
		this.m_stepFifteenButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepFifteenButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepFifteenButton);

		this.m_stepSixteenButton = new JButton(s_STEP_16);
		this.m_stepSixteenButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepSixteenButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepSixteenButton);

		this.m_stepSeventeenButton = new JButton(s_STEP_17);
		this.m_stepSeventeenButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepSeventeenButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepSeventeenButton);

		this.m_stepEighteenButton = new JButton(s_STEP_18);
		this.m_stepEighteenButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepEighteenButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepEighteenButton);

		this.m_stepNineteenButton = new JButton(s_STEP_19);
		this.m_stepNineteenButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepNineteenButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepNineteenButton);

		this.m_stepTwentyButton = new JButton(s_STEP_20);
		this.m_stepTwentyButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepTwentyButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepTwentyButton);

		this.m_stepTwentyOneButton = new JButton(s_STEP_21);
		this.m_stepTwentyOneButton.setAlignmentX(LEFT_ALIGNMENT);
		this.m_stepTwentyOneButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.add(this.m_stepTwentyOneButton);
	}

	public void reset()
	{
		this.getStepOne().setEnabled(true);
		this.getStepTwo().setEnabled(false);
		this.getStepThree().setEnabled(false);
		this.getStepFour().setEnabled(false);
		this.getStepFive().setEnabled(false);
		this.getStepSix().setEnabled(false);
		this.getStepSeven().setEnabled(false);
		this.getStepEight().setEnabled(false);
		this.getStepNine().setEnabled(false);
		this.getStepTen().setEnabled(false);
		this.getStepEleven().setEnabled(false);
		this.getStepTwelve().setEnabled(false);
		this.getStepThirteen().setEnabled(false);
		this.getStepFourteen().setEnabled(false);
		this.getStepFifteen().setEnabled(false);
		this.getStepSixteen().setEnabled(false);
		this.getStepSeventeen().setEnabled(false);
		this.getStepEighteen().setEnabled(false);
		this.getStepNineteen().setEnabled(false);
		this.getStepTwenty().setEnabled(false);
		this.getStepTwentyOne().setEnabled(true);
	}

	public void addActionListener(ActionListener al)
	{
		this.getStepOne().addActionListener(al);
		this.getStepTwo().addActionListener(al);
		this.getStepThree().addActionListener(al);
		this.getStepFour().addActionListener(al);
		this.getStepFive().addActionListener(al);
		this.getStepSix().addActionListener(al);
		this.getStepSeven().addActionListener(al);
		this.getStepEight().addActionListener(al);
		this.getStepNine().addActionListener(al);
		this.getStepTen().addActionListener(al);
		this.getStepEleven().addActionListener(al);
		this.getStepTwelve().addActionListener(al);
		this.getStepThirteen().addActionListener(al);
		this.getStepFourteen().addActionListener(al);
		this.getStepFifteen().addActionListener(al);
		this.getStepSixteen().addActionListener(al);
		this.getStepSeventeen().addActionListener(al);
		this.getStepEighteen().addActionListener(al);
		this.getStepNineteen().addActionListener(al);
		this.getStepTwenty().addActionListener(al);
		this.getStepTwentyOne().addActionListener(al);
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

	public JButton getStepNine()
	{
		return this.m_stepNineButton;
	}

	public JButton getStepTen()
	{
		return this.m_stepTenButton;
	}

	public JButton getStepEleven()
	{
		return this.m_stepElevenButton;
	}

	public JButton getStepTwelve()
	{
		return this.m_stepTwelveButton;
	}

	public JButton getStepThirteen()
	{
		return this.m_stepThirteenButton;
	}

	public JButton getStepFourteen()
	{
		return this.m_stepFourteenButton;
	}

	public JButton getStepFifteen()
	{
		return this.m_stepFifteenButton;
	}

	public JButton getStepSixteen()
	{
		return this.m_stepSixteenButton;
	}

	public JButton getStepSeventeen()
	{
		return this.m_stepSeventeenButton;
	}

	public JButton getStepEighteen()
	{
		return this.m_stepEighteenButton;
	}

	public JButton getStepNineteen()
	{
		return this.m_stepNineteenButton;
	}

	public JButton getStepTwenty()
	{
		return this.m_stepTwentyButton;
	}

	public JButton getStepTwentyOne()
	{
		return this.m_stepTwentyOneButton;
	}
}
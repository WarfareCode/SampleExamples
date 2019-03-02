import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class SampleJPanel
extends JPanel
{
	private static final long	serialVersionUID				= 1L;

	public final static String	s_TEXT_STEP1_SCENARIO			= "Scenario";
	public final static String	s_TEXT_STEP2_SATELLITE			= "Satellite";
	public final static String	s_TEXT_STEP3_FACILITY			= "Facility";
	public final static String	s_TEXT_STEP4_VIEWPOINT			= "View Point";
	public final static String	s_TEXT_STEP5_DISPLACEMENTVECTOR	= "Displacement Vector";
	public final static String	s_TEXT_STEP6_VELOCITYVECTOR		= "Velocity Vector";
	public final static String	s_TEXT_STEP7_AXES				= "Axes";
	public final static String	s_TEXT_STEP8_PLANE				= "Plane";
	public final static String	s_TEXT_STEP9_ANGLES				= "Angles";
	public final static String	s_TEXT_STEP10_SAVE				= "Save";
	public final static String	s_TEXT_STEP11_RESET				= "Reset";

	/*package*/ JButton				m_1JButton;
	/*package*/ JButton				m_2JButton;
	/*package*/ JButton				m_3JButton;
	/*package*/ JButton				m_4JButton;
	/*package*/ JButton				m_5JButton;
	/*package*/ JButton				m_6JButton;
	/*package*/ JButton				m_7JButton;
	/*package*/ JButton				m_8JButton;
	/*package*/ JButton				m_9JButton;
	/*package*/ JButton				m_10JButton;
	/*package*/ JButton				m_11JButton;

	/* package */SampleJPanel()
	{
		this.setLayout(new GridLayout(11, 1));

		this.m_1JButton = new JButton();
		this.m_1JButton.setText(s_TEXT_STEP1_SCENARIO);
		this.m_1JButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_1JButton.setEnabled(true);
		this.add(this.m_1JButton);

		this.m_2JButton = new JButton();
		this.m_2JButton.setText(s_TEXT_STEP2_SATELLITE);
		this.m_2JButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_2JButton.setEnabled(false);
		this.add(this.m_2JButton);

		this.m_3JButton = new JButton();
		this.m_3JButton.setText(s_TEXT_STEP3_FACILITY);
		this.m_3JButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_3JButton.setEnabled(false);
		this.add(this.m_3JButton);

		this.m_4JButton = new JButton();
		this.m_4JButton.setText(s_TEXT_STEP4_VIEWPOINT);
		this.m_4JButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_4JButton.setEnabled(false);
		this.add(this.m_4JButton);

		this.m_5JButton = new JButton();
		this.m_5JButton.setText(s_TEXT_STEP5_DISPLACEMENTVECTOR);
		this.m_5JButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_5JButton.setEnabled(false);
		this.add(this.m_5JButton);

		this.m_6JButton = new JButton();
		this.m_6JButton.setText(s_TEXT_STEP6_VELOCITYVECTOR);
		this.m_6JButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_6JButton.setEnabled(false);
		this.add(this.m_6JButton);

		this.m_7JButton = new JButton();
		this.m_7JButton.setText(s_TEXT_STEP7_AXES);
		this.m_7JButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_7JButton.setEnabled(false);
		this.add(this.m_7JButton);

		this.m_8JButton = new JButton();
		this.m_8JButton.setText(s_TEXT_STEP8_PLANE);
		this.m_8JButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_8JButton.setEnabled(false);
		this.add(this.m_8JButton);

		this.m_9JButton = new JButton();
		this.m_9JButton.setText(s_TEXT_STEP9_ANGLES);
		this.m_9JButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_9JButton.setEnabled(false);
		this.add(this.m_9JButton);

		this.m_10JButton = new JButton();
		this.m_10JButton.setText(s_TEXT_STEP10_SAVE);
		this.m_10JButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_10JButton.setEnabled(false);
		this.add(this.m_10JButton);

		this.m_11JButton = new JButton();
		this.m_11JButton.setText(s_TEXT_STEP11_RESET);
		this.m_11JButton.setHorizontalAlignment(SwingConstants.LEFT);
		this.m_11JButton.setEnabled(false);
		this.add(this.m_11JButton);
	}

	public void addActionListener(ActionListener l)
	{
		this.m_1JButton.addActionListener(l);
		this.m_2JButton.addActionListener(l);
		this.m_3JButton.addActionListener(l);
		this.m_4JButton.addActionListener(l);
		this.m_5JButton.addActionListener(l);
		this.m_6JButton.addActionListener(l);
		this.m_7JButton.addActionListener(l);
		this.m_8JButton.addActionListener(l);
		this.m_9JButton.addActionListener(l);
		this.m_10JButton.addActionListener(l);
		this.m_11JButton.addActionListener(l);
	}
}
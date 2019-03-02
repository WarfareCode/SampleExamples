import java.awt.*;
import javax.swing.*;
import javax.swing.border.*;

import java.text.*;

public class SampleJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	public final static String	s_TEXT_AZIMUTH		= "Azimuth (deg):";
	public final static String	s_TEXT_ELEVATION	= "Elevation (deg):";
	public final static String	s_TEXT_RANGE		= "Range (m):";

	private JLabel				m_AzimuthJLabel;
	private JLabel				m_ElevationJLabel;
	private JLabel				m_RangeJLabel;

	private JTextField			m_AzimuthJTextField;
	private JTextField			m_ElevationJTextField;
	private JTextField			m_RangeJTextField;

	private NumberFormat		m_DegreesFormatter;
	private NumberFormat		m_MetersFormatter;

	/* package */SampleJPanel()
	{
		this.setLayout(new GridLayout(1, 6));

		this.m_AzimuthJLabel = new JLabel();
		this.m_AzimuthJLabel.setText(s_TEXT_AZIMUTH);
		this.m_AzimuthJLabel.setBorder(new EmptyBorder(0, 10, 0, 10));
		this.m_AzimuthJLabel.setHorizontalAlignment(SwingConstants.RIGHT);
		this.add(this.m_AzimuthJLabel);

		this.m_AzimuthJTextField = new JTextField();
		this.m_AzimuthJTextField.setBorder(new BevelBorder(BevelBorder.LOWERED));
		this.m_AzimuthJTextField.setHorizontalAlignment(SwingConstants.RIGHT);
		this.m_AzimuthJTextField.setEnabled(false);
		this.add(this.m_AzimuthJTextField);

		this.m_ElevationJLabel = new JLabel();
		this.m_ElevationJLabel.setText(s_TEXT_ELEVATION);
		this.m_ElevationJLabel.setBorder(new EmptyBorder(0, 10, 0, 10));
		this.m_ElevationJLabel.setHorizontalAlignment(SwingConstants.RIGHT);
		this.add(this.m_ElevationJLabel);

		this.m_ElevationJTextField = new JTextField();
		this.m_ElevationJTextField.setBorder(new BevelBorder(BevelBorder.LOWERED));
		this.m_ElevationJTextField.setHorizontalAlignment(SwingConstants.RIGHT);
		this.m_ElevationJTextField.setEnabled(false);
		this.add(this.m_ElevationJTextField);

		this.m_RangeJLabel = new JLabel();
		this.m_RangeJLabel.setText(s_TEXT_RANGE);
		this.m_RangeJLabel.setBorder(new EmptyBorder(0, 10, 0, 10));
		this.m_RangeJLabel.setHorizontalAlignment(SwingConstants.RIGHT);
		this.add(this.m_RangeJLabel);

		this.m_RangeJTextField = new JTextField();
		this.m_RangeJTextField.setBorder(new BevelBorder(BevelBorder.LOWERED));
		this.m_RangeJTextField.setHorizontalAlignment(SwingConstants.RIGHT);
		this.m_RangeJTextField.setEnabled(false);
		this.add(this.m_RangeJTextField);
		
		this.m_DegreesFormatter = new DecimalFormat("0.000");
		this.m_MetersFormatter = new DecimalFormat("0.000");
	}

	public void setAER(double azimuth, double elevation, double range)
	{
		this.m_AzimuthJTextField.setText(this.m_DegreesFormatter.format(azimuth));
		this.m_ElevationJTextField.setText(this.m_DegreesFormatter.format(elevation));
		this.m_RangeJTextField.setText(this.m_MetersFormatter.format(range));
	}
}
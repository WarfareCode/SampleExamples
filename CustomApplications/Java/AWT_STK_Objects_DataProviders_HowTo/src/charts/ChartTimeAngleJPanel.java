package charts;

// Java API
import java.awt.*;
import java.util.*;
import javax.swing.*;

// JFreeChart API
import org.jfree.data.time.*;
import org.jfree.ui.HorizontalAlignment;
import org.jfree.chart.*;
import org.jfree.chart.axis.*;
import org.jfree.chart.plot.*;
import org.jfree.chart.renderer.xy.*;

public class ChartTimeAngleJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	private XYPlot		m_XYPlot;
	private JFreeChart 	m_JFreeChart;
	private ChartPanel 	m_ChartPanel;
	
	public ChartTimeAngleJPanel()
	{
		this.setLayout(new BorderLayout());
	}
	
	public void setData(String title, String xAxisTitle, Object[] times, String yAxisTitle, Object[] angles)
	{
        // X Axis
        DateAxis domain = new DateAxis(xAxisTitle);
        domain.setAutoRange(true);
        domain.setTickLabelsVisible(true);
        domain.setLabelPaint(Color.WHITE);
        domain.setAxisLinePaint(Color.WHITE);
        domain.setTickLabelPaint(Color.WHITE);
        domain.setTickMarkPaint(Color.WHITE);

        // Y Axis
        double rangeMin = ChartDataHelper.getMinDoubleValue(angles);
        double rangeMax = ChartDataHelper.getMaxDoubleValue(angles);
        NumberAxis range = new NumberAxis(yAxisTitle);
        range.setAutoRange(true);
        range.setTickLabelsVisible(true);
        range.setStandardTickUnits(NumberAxis.createStandardTickUnits());
        range.setRange(rangeMin, rangeMax);
        range.setLabelPaint(Color.WHITE);
        range.setAxisLinePaint(Color.WHITE);
        range.setTickLabelPaint(Color.WHITE);
        range.setTickMarkPaint(Color.WHITE);

        // Create the data set to contain our time/error values
        TimeSeriesCollection dataset = new TimeSeriesCollection();
        TimeSeries dataSeries = new TimeSeries(yAxisTitle);
        dataset.addSeries( dataSeries );
        
        for( int i=0; i < times.length; i++ )
        {
        	String dateTime = (String)times[i];
        	Date d = ChartDataHelper.convertStkDateStringToDate(dateTime);
            double v = ((Double)angles[i]).doubleValue();
            dataSeries.add( new Second(d), v);
        }

        // Create the graph renderer
        XYItemRenderer renderer = new DefaultXYItemRenderer();
        renderer.setSeriesPaint(0, Color.BLUE);
        //renderer.setBaseStroke(new BasicStroke(0.5f, BasicStroke.CAP_BUTT, BasicStroke.JOIN_BEVEL));
        
        // Create the plot object
        this.m_XYPlot = new XYPlot(dataset, domain, range, renderer);
        this.m_XYPlot.setBackgroundPaint(Color.WHITE);
        
		this.m_JFreeChart = new JFreeChart(title, this.m_XYPlot);
		this.m_JFreeChart.getLegend().setHorizontalAlignment(HorizontalAlignment.LEFT);
		this.m_JFreeChart.getTitle().setPaint(Color.WHITE);
		
		this.m_ChartPanel = new ChartPanel(this.m_JFreeChart);
        this.add(this.m_ChartPanel, BorderLayout.CENTER);
        this.m_ChartPanel.setVisible(false);
        this.m_ChartPanel.setVisible(true);
	}

}
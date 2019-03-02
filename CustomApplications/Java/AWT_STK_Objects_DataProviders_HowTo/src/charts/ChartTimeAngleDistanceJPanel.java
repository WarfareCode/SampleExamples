package charts;

// Java API
import java.awt.*;
import java.util.*;
import javax.swing.*;
import javax.swing.plaf.metal.MetalTheme;

// JFreeChart API
import org.jfree.data.time.*;
import org.jfree.ui.*;
import org.jfree.chart.*;
import org.jfree.chart.axis.*;
import org.jfree.chart.plot.*;
import org.jfree.chart.renderer.xy.*;

// AGI Java API
import agi.core.*;
import agi.core.awt.*;
import agi.swing.plaf.metal.*;

public class ChartTimeAngleDistanceJPanel
extends JPanel
{
	private static final long	serialVersionUID	= 1L;

	private XYPlot		m_XYPlot;
	private JFreeChart 	m_JFreeChart;
	private ChartPanel 	m_ChartPanel;
	
	public ChartTimeAngleDistanceJPanel()
	{
		this.setLayout(new BorderLayout());
	}
	
	public void setData(String title, Object[] timeSeriesData, Object[] angleSeriesTitles, Object[] angleSeriesData, Object[] distanceSeriesTitles, Object[] distanceSeriesData)
	{
		String xAxisTitle = "Time (UTCG)";
		String y1AxisTitle = "Angle (deg)";
		String y2AxisTitle = "Distance (km)";
		
        // X Axis
        DateAxis domain = new DateAxis(xAxisTitle);
        domain.setAutoRange(true);
        domain.setTickLabelsVisible(true);
        domain.setLabelPaint(Color.WHITE);
        domain.setAxisLinePaint(Color.WHITE);
        domain.setTickLabelPaint(Color.WHITE);
        domain.setTickMarkPaint(Color.WHITE);

        // Y1 Axis
        //double range1Min = ChartDataHelper.getMinDoubleValue(angles);
        //double range1Max = ChartDataHelper.getMaxDoubleValue(angles);
        NumberAxis range1 = new NumberAxis(y1AxisTitle);
        range1.setAutoRange(true);
        range1.setTickLabelsVisible(true);
        range1.setStandardTickUnits(NumberAxis.createStandardTickUnits());
        //range1.setRange(range1Min, range1Max);
        range1.setLabelPaint(Color.WHITE);
        range1.setAxisLinePaint(Color.WHITE);
        range1.setTickLabelPaint(Color.WHITE);
        range1.setTickMarkPaint(Color.WHITE);

        // Y2 Axis
        //double range2Min = ChartDataHelper.getMinDoubleValue(angles);
        //double range2Max = ChartDataHelper.getMaxDoubleValue(angles);
        NumberAxis range2 = new NumberAxis(y2AxisTitle);
        range2.setAutoRange(true);
        range2.setTickLabelsVisible(true);
        range2.setStandardTickUnits(NumberAxis.createStandardTickUnits());
        //range2.setRange(range2Min, range2Max);
        range2.setLabelPaint(Color.WHITE);
        range2.setAxisLinePaint(Color.WHITE);
        range2.setTickLabelPaint(Color.WHITE);
        range2.setTickMarkPaint(Color.WHITE);

        // Create the data set to contain our time/error values
        TimeSeriesCollection anglesDataset = new TimeSeriesCollection();
        for(int i=0; i<angleSeriesData.length; i++)
        {
        	String seriesTitle = (String)angleSeriesTitles[i];
        	Object[] seriesDatum = (Object[])angleSeriesData[i];
        	TimeSeries dataSeries = new TimeSeries(seriesTitle);
            for( int j=0; j < seriesDatum.length; j++ )
            {
            	String dateTime = (String)timeSeriesData[j];
            	Date d = ChartDataHelper.convertStkDateStringToDate(dateTime);
                double v = ((Double)seriesDatum[j]).doubleValue();
                dataSeries.add(new Second(d), v);
            }
            anglesDataset.addSeries(dataSeries);
        }

//        // Create the data set to contain our time/error values
//        TimeSeriesCollection distanceDataset = new TimeSeriesCollection();
//        for(int i=0; i<distanceSeriesData.length; i++)
//        {
//        	String seriesTitle = (String)distanceSeriesTitles[i];
//        	Object[] seriesDatum = (Object[])distanceSeriesData[i];
//        	TimeSeries dataSeries = new TimeSeries(seriesTitle, Second.class);
//            for( int j=0; j < seriesDatum.length; j++ )
//            {
//            	String dateTime = (String)timeSeriesData[j];
//            	Date d = ChartDataHelper.convertStkDateStringToDate(dateTime);
//                double v = ((Double)seriesDatum[i]).doubleValue();
//                dataSeries.add(new Second(d), v);
//            }
//            distanceDataset.addSeries(dataSeries);
//        }

        // Create the graph renderer
        XYItemRenderer renderer = new DefaultXYItemRenderer();
        renderer.setSeriesPaint(0, AgAwtColorTranslator.fromLongtoAWT(AgCoreColor.LIGHTBLUE));
        renderer.setSeriesPaint(1, AgAwtColorTranslator.fromLongtoAWT(AgCoreColor.MEDIUMSPRINGGREEN));
        renderer.setSeriesPaint(2, AgAwtColorTranslator.fromLongtoAWT(AgCoreColor.VIOLET));
        renderer.setSeriesPaint(3, Color.ORANGE);
        //renderer.setBaseStroke(new BasicStroke(0.5f, BasicStroke.CAP_BUTT, BasicStroke.JOIN_BEVEL));
        
        // Create the plot object
        this.m_XYPlot = new XYPlot();
        this.m_XYPlot.setDataset(0, anglesDataset);
        //this.m_XYPlot.setDataset(1, distanceDataset);
        this.m_XYPlot.setDomainAxis(domain);
        this.m_XYPlot.setRangeAxes(new ValueAxis[]{range1, range2});
        this.m_XYPlot.setRenderer(renderer);
		if(AgMetalThemeFactory.getEnabled())
		{
			MetalTheme mt = AgMetalThemeFactory.getDefaultMetalTheme();
			this.m_XYPlot.setBackgroundPaint(mt.getInactiveControlTextColor());
		}
		else
		{
			this.m_XYPlot.setBackgroundPaint(Color.WHITE);
		}
        
		this.m_JFreeChart = new JFreeChart(title, this.m_XYPlot);
		this.m_JFreeChart.getLegend().setHorizontalAlignment(HorizontalAlignment.LEFT);
		this.m_JFreeChart.getTitle().setPaint(Color.WHITE);
		
		this.m_ChartPanel = new ChartPanel(this.m_JFreeChart);
        this.add(this.m_ChartPanel, BorderLayout.CENTER);
        this.m_ChartPanel.setVisible(false);
        this.m_ChartPanel.setVisible(true);
	}

}
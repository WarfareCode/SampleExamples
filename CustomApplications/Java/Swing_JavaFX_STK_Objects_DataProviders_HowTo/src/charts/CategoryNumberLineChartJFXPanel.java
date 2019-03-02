package charts;

// Java API

// JavaFX 2.0 API
import javafx.application.*;
import javafx.embed.swing.*;
import javafx.scene.*;
import javafx.scene.chart.*;
import javafx.scene.layout.*;

// AGI Java API

public class CategoryNumberLineChartJFXPanel
extends JFXPanel
{
	private static final long			serialVersionUID	= 1L;

	private BorderPane					m_BorderPane;
	private Scene						m_Scene;
	private LineChart<String, Number>	m_LineChart;

	public CategoryNumberLineChartJFXPanel()
	{
		
	}

	public void initScene()
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				CategoryNumberLineChartJFXPanel.this.m_BorderPane = new BorderPane();

				CategoryAxis xAxis = new CategoryAxis();
				NumberAxis yAxis = new NumberAxis();
				CategoryNumberLineChartJFXPanel.this.m_LineChart = new LineChart<String, Number>(xAxis, yAxis);
				CategoryNumberLineChartJFXPanel.this.m_LineChart.setPrefSize(CategoryNumberLineChartJFXPanel.this.getWidth() - 10, CategoryNumberLineChartJFXPanel.this.getHeight() - 10);
				CategoryNumberLineChartJFXPanel.this.m_BorderPane.setCenter(CategoryNumberLineChartJFXPanel.this.m_LineChart);

				CategoryNumberLineChartJFXPanel.this.m_Scene = new Scene(CategoryNumberLineChartJFXPanel.this.m_BorderPane);
				CategoryNumberLineChartJFXPanel.this.setScene(CategoryNumberLineChartJFXPanel.this.m_Scene);

				CategoryNumberLineChartJFXPanel.this.invalidate();
				CategoryNumberLineChartJFXPanel.this.repaint();
			}
		});
	}
	
	public void initScene(double initLower, double initUpper, double tickAmount)
	{
		final double lower = initLower;
		final double upper = initUpper;
		final double tickLength = tickAmount;
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{			
			@Override
			public void run()
			{
				CategoryNumberLineChartJFXPanel.this.m_BorderPane = new BorderPane();

				CategoryAxis xAxis = new CategoryAxis();
				NumberAxis yAxis = new NumberAxis(lower, upper, tickLength);
				CategoryNumberLineChartJFXPanel.this.m_LineChart = new LineChart<String, Number>(xAxis, yAxis);
				CategoryNumberLineChartJFXPanel.this.m_LineChart.setPrefSize(CategoryNumberLineChartJFXPanel.this.getWidth() - 10, CategoryNumberLineChartJFXPanel.this.getHeight() - 10);
				CategoryNumberLineChartJFXPanel.this.m_BorderPane.setCenter(CategoryNumberLineChartJFXPanel.this.m_LineChart);

				CategoryNumberLineChartJFXPanel.this.m_Scene = new Scene(CategoryNumberLineChartJFXPanel.this.m_BorderPane);
				CategoryNumberLineChartJFXPanel.this.setScene(CategoryNumberLineChartJFXPanel.this.m_Scene);

				CategoryNumberLineChartJFXPanel.this.invalidate();
				CategoryNumberLineChartJFXPanel.this.repaint();
			}
		});
	}

	public void setData(final String title, final String xTitle, final String yTitle, final Object[] xSeriesData, final Object[] ySeriesTitles, final Object[] ySeriesData)
	{
		// This method is invoked on JavaFX thread
		Platform.runLater(new Runnable()
		{
			@Override
			public void run()
			{
				CategoryNumberLineChartJFXPanel.this.m_LineChart.setTitle(title);

				CategoryNumberLineChartJFXPanel.this.m_LineChart.getXAxis().setLabel(xTitle);
				CategoryNumberLineChartJFXPanel.this.m_LineChart.getYAxis().setLabel(yTitle);

				for(int i = 0; i < ySeriesTitles.length; i++)
				{
					XYChart.Series<String, Number> series = new XYChart.Series<String, Number>();
					series.setName((String)ySeriesTitles[i]);
					Object[] seriesData = (Object[])ySeriesData[i];
					for(int j = 0; j < seriesData.length; j++)
					{
						series.getData().add(new XYChart.Data<String, Number>((String)xSeriesData[j], (Double)seriesData[j]));
					}
					CategoryNumberLineChartJFXPanel.this.m_LineChart.getData().add(series);
				}
				
				CategoryNumberLineChartJFXPanel.this.invalidate();
				CategoryNumberLineChartJFXPanel.this.repaint();
			}
		});
	}
}
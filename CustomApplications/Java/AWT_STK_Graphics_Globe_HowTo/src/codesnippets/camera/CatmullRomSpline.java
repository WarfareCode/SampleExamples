package codesnippets.camera;

//#region Imports

//Java API
import java.util.*;

//AGI Java API
import agi.core.*;
import agi.stkobjects.*;
import agi.stkutil.*;

//#endregion

public class CatmullRomSpline
{
	private ArrayList<Object[]>	m_ControlPoints;
	private ArrayList<Object[]>	m_InterpolatorPoints;
	private int			m_NumberOfInterpolatorPoints	= 10000;

	// Creates a spline given a shape and a list of cartographics
	public CatmullRomSpline(String centralBody, ArrayList<Object[]> positions, AgStkObjectRootClass root)
	throws AgCoreException
	{
		calculateControlPoints(root, centralBody, positions);
		calculateInterpolationPoints(root);
	}

	// Creates a spline given a central body, two start points and an altitude
	public CatmullRomSpline(AgStkObjectRootClass root, String centralBody, Object[] start, Object[] end, double altitude)
	throws AgCoreException
	{
		altitude = Math.abs(altitude);
		double height = 0.8;

		Object[] aboveStart = new Object[] {(Double)start[0], (Double)start[1], new Double(altitude * height)};
		Object[] middle = new Object[] 
		{
			new Double((((Double)start[0]).doubleValue() + ((Double)end[0]).doubleValue()) / 2), 
			new Double((((Double)start[1]).doubleValue() + ((Double)end[1]).doubleValue()) / 2), 
			new Double(altitude)
		};
		Object[] aboveEnd = new Object[] {(Double)end[0], (Double)end[1], new Double(altitude * height)};

		ArrayList<Object[]> cartographics = new ArrayList<Object[]>();
		cartographics.add(start);
		cartographics.add(aboveStart);
		cartographics.add(middle);
		cartographics.add(aboveEnd);
		cartographics.add(end);

		calculateControlPoints(root, centralBody, cartographics);
		calculateInterpolationPoints(root);
	}

	// Returns a list of interpolator points
	public final ArrayList<Object[]> getInterpolatorPoints()
	{
		return m_InterpolatorPoints;
	}

	// Sets the number of interpolation points that should be part of the spline
	public final void setNumberOfInterpolationPoints(int numberOfPoints, AgStkObjectRootClass root)
	{
		m_NumberOfInterpolatorPoints = numberOfPoints;
		m_NumberOfInterpolatorPoints = Math.max(m_NumberOfInterpolatorPoints, (m_ControlPoints.size() - 3) * 2);
		m_NumberOfInterpolatorPoints = Math.min(m_NumberOfInterpolatorPoints, 1000000);
		calculateInterpolationPoints(root);
	}

	// Calculates the control points for the spline
	final private void calculateControlPoints(AgStkObjectRootClass root, String centralBody, ArrayList<Object[]> positions) 
	throws AgCoreException
	{
		m_ControlPoints = new ArrayList<Object[]>();
		int numPoints = positions.size();
		if(numPoints >= 2)
		{
			Object[] virtualStart = new Object[] 
			{
				new Double(((Double)((Object[])positions.get(0))[0]).doubleValue() + (((Double)((Object[])positions.get(0))[0]).doubleValue() - ((Double)((Object[])positions.get(1))[0]).doubleValue())),
				new Double(((Double)((Object[])positions.get(0))[1]).doubleValue() + (((Double)((Object[])positions.get(0))[1]).doubleValue() - ((Double)((Object[])positions.get(1))[1]).doubleValue())),
				new Double(((Double)((Object[])positions.get(0))[2]).doubleValue() + (((Double)((Object[])positions.get(0))[2]).doubleValue() - ((Double)((Object[])positions.get(1))[2]).doubleValue()))
			};

			Object[] virtualEnd = new Object[] 
			{
				new Double(((Double)((Object[])positions.get(numPoints - 1))[0]).doubleValue() + ((Double)((Object[])positions.get(numPoints - 1))[0]).doubleValue() - ((Double)((Object[])positions.get(numPoints - 2))[0]).doubleValue()),
				new Double(((Double)((Object[])positions.get(numPoints - 1))[1]).doubleValue() + ((Double)((Object[])positions.get(numPoints - 1))[1]).doubleValue() - ((Double)((Object[])positions.get(numPoints - 2))[1]).doubleValue()),
				new Double(((Double)((Object[])positions.get(numPoints - 1))[2]).doubleValue() + ((Double)((Object[])positions.get(numPoints - 1))[2]).doubleValue() - ((Double)((Object[])positions.get(numPoints - 2))[2]).doubleValue())
			};

			m_ControlPoints.add(cartographicToCartesian(virtualStart, centralBody, root));
			for(int i = 0; i < numPoints; i++)
			{
				m_ControlPoints.add(cartographicToCartesian((Object[])positions.get(i), centralBody, root));
			}
			m_ControlPoints.add(cartographicToCartesian(virtualEnd, centralBody, root));
		}
	}

	// Calculates the interpolator points for the spline
	final private void calculateInterpolationPoints(AgStkObjectRootClass root)
	{
		m_InterpolatorPoints = new ArrayList<Object[]>();
		if(m_ControlPoints.size() >= 4)
		{
			for(int i = 1; i <= m_ControlPoints.size() - 3; i++)
			{
				Object[] points = new Object[4];
				points[0] = m_ControlPoints.get(i - 1);
				points[1] = m_ControlPoints.get(i);
				points[2] = m_ControlPoints.get(i + 1);
				points[3] = m_ControlPoints.get(i + 2);
				int end = m_NumberOfInterpolatorPoints / (m_ControlPoints.size() - 3);
				for(int t = 0; t < end; t++)
				{
					double time = (double)t / (double)(end - 1);
					double t1 = time;
					double t2 = time * time;
					double t3 = time * time * time;

					Object[] points0 = (Object[])points[0];
					Object[] points1 = (Object[])points[1];
					Object[] points2 = (Object[])points[2];
					Object[] points3 = (Object[])points[3];
					
					double t1factor0 = (2 * ((Double)points1[0]).doubleValue()) + (-((Double)points0[0]).doubleValue() + ((Double)points2[0]).doubleValue()) * t1;
					double t2factor0 = (2 * ((Double)points0[0]).doubleValue() - 5 * ((Double)points1[0]).doubleValue() + 4 * ((Double)points2[0]).doubleValue() - ((Double)points3[0]).doubleValue()) * t2;
					double t3factor0 = (-((Double)points0[0]).doubleValue() + 3 * ((Double)points1[0]).doubleValue() - 3 * ((Double)points2[0]).doubleValue() + ((Double)points3[0]).doubleValue()) * t3;
					
					double t1factor1 = (2 * ((Double)points1[1]).doubleValue()) + (-((Double)points0[1]).doubleValue() + ((Double)points2[1]).doubleValue()) * t1;
					double t2factor1 = (2 * ((Double)points0[1]).doubleValue() - 5 * ((Double)points1[1]).doubleValue() + 4 * ((Double)points2[1]).doubleValue() - ((Double)points3[1]).doubleValue()) * t2;
					double t3factor1 = (-((Double)points0[1]).doubleValue() + 3 * ((Double)points1[1]).doubleValue() - 3 * ((Double)points2[1]).doubleValue() + ((Double)points3[1]).doubleValue()) * t3;
					
					double t1factor2 = (2 * ((Double)points1[2]).doubleValue()) + (-((Double)points0[2]).doubleValue() + ((Double)points2[2]).doubleValue()) * t1;
					double t2factor2 = (2 * ((Double)points0[2]).doubleValue() - 5 * ((Double)points1[2]).doubleValue() + 4 * ((Double)points2[2]).doubleValue() - ((Double)points3[2]).doubleValue()) * t2;
					double t3factor2 = (-((Double)points0[2]).doubleValue() + 3 * ((Double)points1[2]).doubleValue() - 3 * ((Double)points2[2]).doubleValue() + ((Double)points3[2]).doubleValue()) * t3;

					double pos1 = 0.5 * (t1factor0 + t2factor0 + t3factor0);
					double pos2 = 0.5 * (t1factor1 + t2factor1 + t3factor1);
					double pos3 = 0.5 * (t1factor2 + t2factor2 + t3factor2);
	
					//System.out.println(pos1 + " " + pos2 + " " + pos3 );
					
					Object[] position = new Object[] {new Double(pos1), new Double(pos2), new Double(pos3)};

					m_InterpolatorPoints.add(position);
				}
			}
		}
	}
	
    public static Object[] cartographicToCartesian(Object[] cartographic, String centralBody, AgStkObjectRootClass root)
    throws AgCoreException
    {
        IAgPosition position = root.getConversionUtility().newPositionOnCB(centralBody);
        position.assignPlanetodetic((Double)cartographic[0], (Double)cartographic[1], Math.max(((Double)cartographic[2]).doubleValue(), 0));

        Object[] cart = (Object[])position.queryCartesianArray_AsObject();

        return cart;
    }

    public static Object[] cartesianToCartographic(Object[] cartesian, String centralBody, AgStkObjectRootClass root)
    throws AgCoreException
    {
        IAgPosition position = root.getConversionUtility().newPositionOnCB(centralBody);
        position.assignCartesian(((Double)cartesian[0]).doubleValue(), ((Double)cartesian[1]).doubleValue(), ((Double)cartesian[2]).doubleValue());

        Object[] lla = (Object[])position.queryPlanetodeticArray_AsObject();

        return lla;
    }
}
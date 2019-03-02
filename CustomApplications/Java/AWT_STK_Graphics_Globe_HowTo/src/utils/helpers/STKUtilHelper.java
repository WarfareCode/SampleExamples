package utils.helpers;

// Java API
import java.io.*;
import java.util.*;

// AGI Java API
import agi.core.*;
import agi.stkutil.*;
import agi.stkobjects.*;

// Sample API
import agi.samples.sharedresources.*;

public final class STKUtilHelper
{
	private STKUtilHelper()
	{
	}

	public static IAgPosition createPosition(AgStkObjectRootClass root, double lat, double lon, double alt)
	throws Throwable
	{
		IAgPosition pos = root.getConversionUtility().newPositionOnEarth();
		pos.assignPlanetodetic(new Double(lat), new Double(lon), new Double(alt).doubleValue());
		return pos;
	}

	/**
	 * Reads an STK area target file (*.at) and returns the points defining the area target's boundary as a list of Cartographic points.
	 * @throws IOException
	 */
	public static Object[] readAreaTargetCartographic(String fileName)
	throws Throwable
	{
		//
		// Open the file and read everything between "BEGIN PolygonPoints"
		// and "END PolygonPoints"
		//
		String areaTarget = FileUtilities.readAllText(fileName);
		String startToken = "BEGIN PolygonPoints";
		String points = areaTarget.substring(areaTarget.indexOf(startToken) + startToken.length());
		points = points.substring(0, points.indexOf("END PolygonPoints"));

		String[] splitPointsWithEmpties = points.split("[\t\n\r]");

		ArrayList<String> splitPoints = new ArrayList<String>();
		for(int i = 0; i < splitPointsWithEmpties.length; ++i)
		{
			if(!splitPointsWithEmpties[i].equalsIgnoreCase(""))
			{
				splitPoints.add(splitPointsWithEmpties[i]);
			}
		}

		Object[] targetPoints = new Object[splitPoints.size()];
		for(int i = 0; i < splitPoints.size(); i += 3)
		{
			// Each line is [Latitude][Longitude][Altitude]. In the file,
			// latitude and longitude are in degrees and altitude is in
			// meters.
			double latDeg = Double.parseDouble((String)splitPoints.get(i));
			targetPoints[i] = new Double(latDeg);

			double lonDeg = Double.parseDouble((String)splitPoints.get(i + 1));
			targetPoints[i + 1] = new Double(lonDeg);

			double alt = Double.parseDouble((String)splitPoints.get(i + 2));
			targetPoints[i + 2] = new Double(alt);
		}

		return targetPoints;
	}

	/**
	 * Reads an STK area target file (*.at) and returns the points defining the area target's boundary as a list Cartesian points in the earth's fixed frame. This method assumes the file exists, that
	 * it is a valid area target file, and the area target is on earth.
	 * @throws AgCoreException
	 * @throws IOException
	 */
	public static Object[] readAreaTargetPoints(IAgStkObjectRoot root, String fileName)
	throws Throwable
	{
		//
		// Open the file and read everything between "BEGIN PolygonPoints"
		// and "END PolygonPoints"
		//
		String areaTarget = FileUtilities.readAllText(fileName);
		String startToken = "BEGIN PolygonPoints";
		String points = areaTarget.substring(areaTarget.indexOf(startToken) + startToken.length());
		points = points.substring(0, points.indexOf("END PolygonPoints"));

		String[] splitPointsWithEmpties = points.split("[\t\n\r]");
		ArrayList<String> splitPoints = new ArrayList<String>();

		for(int i = 0; i < splitPointsWithEmpties.length; ++i)
		{
			if(!splitPointsWithEmpties[i].equalsIgnoreCase(""))
			{
				splitPoints.add(splitPointsWithEmpties[i]);
			}
		}

		Object[] targetPoints = new Object[splitPoints.size()];
		for(int i = 0; i < splitPoints.size(); i += 3)
		{
			// Each line is [Latitude][Longitude][Altitude]. In the file,
			// latitude and longitude are in degrees and altitude is in
			// meters.
			double latDeg = Double.parseDouble((String)splitPoints.get(i));

			double lonDeg = Double.parseDouble((String)splitPoints.get(i + 1));

			double alt = Double.parseDouble((String)splitPoints.get(i + 2));

			IAgPosition pos = root.getConversionUtility().newPositionOnEarth();
			pos.assignPlanetodetic(new Double(latDeg), new Double(lonDeg), alt);

			Object[] cart = (Object[])pos.queryCartesianArray_AsObject();
			targetPoints[i] = cart[0];
			targetPoints[i + 1] = cart[1];
			targetPoints[i + 2] = cart[2];
		}

		return targetPoints;
	}

	public static Object[] readLineTargetPoints(IAgStkObjectRoot root, String fileName)
	throws Throwable
	{
		String areaTarget = FileUtilities.readAllText(fileName);
		String startToken = "BEGIN PolylinePoints";
		String points = areaTarget.substring(areaTarget.indexOf(startToken) + startToken.length());
		points = points.substring(0, points.indexOf("END PolylinePoints"));

		String[] splitPointsWithEmpties = points.split("[\t\n\r ]");
		ArrayList<String> splitPoints = new ArrayList<String>();

		for(int i = 0; i < splitPointsWithEmpties.length; ++i)
		{
			if(!splitPointsWithEmpties[i].equalsIgnoreCase(""))
			{
				splitPoints.add(splitPointsWithEmpties[i]);
			}
		}

		Object[] targetPoints = new Object[splitPoints.size()];

		for(int i = 0; i < splitPoints.size(); i += 3)
		{
			double latDeg = Double.parseDouble((String)splitPoints.get(i));

			double lonDeg = Double.parseDouble((String)splitPoints.get(i + 1));

			double alt = Double.parseDouble((String)splitPoints.get(i + 2));

			IAgPosition pos = root.getConversionUtility().newPositionOnEarth();
			pos.assignPlanetodetic(new Double(latDeg), new Double(lonDeg), alt);

			Object[] cart = (Object[])pos.queryCartesianArray_AsObject();
			targetPoints[i] = cart[0];
			targetPoints[i + 1] = cart[1];
			targetPoints[i + 2] = cart[2];
		}

		return targetPoints;
	}
}
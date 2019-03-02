package utils.helpers;

// Java API
import java.util.*;
import java.io.*;

// AGI Java API
import agi.stkobjects.*;

public class PositionOrientationHelper
{
    private static final String Separator = "    ";

    private IAgStkObjectRoot m_root;

    private ArrayList<Double> m_Dates;
    private ArrayList<Double[]> m_Positions;
    private ArrayList<Double[]> m_Orientations;

    public PositionOrientationHelper(IAgStkObjectRoot root, String filename) 
    throws Throwable
    {
        this.m_root = root;

        this.m_Dates = new ArrayList<Double>();
        this.m_Positions = new ArrayList<Double[]>();
        this.m_Orientations = new ArrayList<Double[]>();

        FileReader sr = new FileReader(filename);
        BufferedReader bir = new BufferedReader(sr);

        String line = null;
        while ((line = bir.readLine()) != null)
        {
            String[] sEntries = line.replaceAll(Separator, ",").split(",");
            m_Dates.add(new Double(Double.parseDouble(m_root.getConversionUtility().newDate("UTCG", sEntries[0]).format("epSec"))));

            double x = Double.parseDouble(sEntries[1]);
            double y = Double.parseDouble(sEntries[2]);
            double z = Double.parseDouble(sEntries[3]);
            Double[] pos = new Double[] { new Double(x), new Double(y), new Double(z) };
            m_Positions.add(pos);

            x = Double.parseDouble(sEntries[4]);
            y = Double.parseDouble(sEntries[5]);
            z = Double.parseDouble(sEntries[6]);
            double w = Double.parseDouble(sEntries[7]);
            Double[] orientation = new Double[] { new Double(x), new Double(y), new Double(z), new Double(w) };
            m_Orientations.add(orientation);
        }
        
        bir.close();
        bir = null;
    }

    public ArrayList<Double> getDatesList()
    {
    	return m_Dates;
    }
    
    public Double[] getDatesArray()
    {
    	Double[] a = new Double[m_Dates.size()];
    	a = m_Dates.toArray(a);
    	return a;
    }

    public ArrayList<Double[]> getPositionsList()
    {
        return m_Positions;
    }

    public Double[][] getPositionsArray()
    {
    	Double[][] a = new Double[m_Positions.size()][3];
    	for(int i=0; i<m_Positions.size(); i++)
    	{
    		Double[] pos = m_Positions.get(i);
    		a[i][0] = pos[0];
    		a[i][1] = pos[1];
    		a[i][2] = pos[2];
    	}    	
        return a;
    }

    public ArrayList<Double[]> getOrientationsList()
    {
        return m_Orientations;
    }

    public Double[][] getOrientationsArray()
    {
    	Double[][] a = new Double[m_Orientations.size()][4];
    	for(int i=0; i<m_Orientations.size(); i++)
    	{
    		Double[] orient = m_Orientations.get(i);
    		a[i][0] = orient[0];
    		a[i][1] = orient[1];
    		a[i][2] = orient[2];
    		a[i][3] = orient[3];
    	}    	
        return a;
    }

    public int findIndexOfClosestTime(double searchTime, int startIndex, int searchLength)
    {
        // Find the midpoint of the length
        int midpoint = startIndex + (searchLength / 2);

        // Base cases
        if (((Double)m_Dates.get(startIndex)).doubleValue() == searchTime || searchLength == 1)
        {
            return startIndex;
        }
        if (searchLength == 2)
        {
            double diff1 = ((Double)m_Dates.get(startIndex)).doubleValue() - searchTime;
            double diff2 = ((Double)m_Dates.get(startIndex + 1)).doubleValue() - searchTime;

            if (Math.abs(diff1) < Math.abs(diff2))
            {
                return startIndex;
            }
            else // Note: error on the larger time if equal
            {
                return startIndex + 1;
            }
        }
        if (((Double)m_Dates.get(midpoint)).doubleValue() == searchTime)
        {
            return midpoint;
        }

        // Normal case: binary search
        if (searchTime < ((Double)m_Dates.get(midpoint)).doubleValue())
        {
            return findIndexOfClosestTime(searchTime, startIndex, midpoint - startIndex);
        }
        else
        {
            return findIndexOfClosestTime(searchTime, midpoint + 1, startIndex + searchLength - (midpoint + 1));
        }
    }
}
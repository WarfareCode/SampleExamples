import java.net.*;
import java.io.*;
import java.util.*;

import javax.swing.table.*;

public class SatelliteTableModel
extends AbstractTableModel
{
	private static final long serialVersionUID = 1L;

	private String[] m_ColumnNames = { "ID", "Name", "Force Code", "Mission", "Total Mass", "Fuel Mass" };
	private ArrayList<String[]> m_AllData;
	private ArrayList<String[]> m_TempData;

	private boolean m_fALL;
	private boolean m_fBLUE;
	private boolean m_fRED;
	private boolean m_fWHITE;

	private boolean m_mALL;
	private boolean m_mEO;
	private boolean m_mIR;
	private boolean m_mELINT;

	private boolean m_bMassFuelMin;
	private double m_dMassFuelMin;

	private boolean m_bMassFuelMax;
	private double m_dMassFuelMax;

	private boolean m_bMassTotalMin;
	private double m_dMassTotalMin;

	private boolean m_bMassTotalMax;
	private double m_dMassTotalMax;

	public SatelliteTableModel( boolean initializeWithData )
	{
		this.initialize( initializeWithData );
	}

	private void initialize( boolean initializeWithData )
	{
		try
		{
			this.m_AllData = new ArrayList<String[]>();
			this.m_TempData = new ArrayList<String[]>();

			this.m_fALL = true;
			this.m_mALL = true;

			URL vehdburl = SatelliteTableModel.class.getResource( "SatelliteDatabase.csv" );
			InputStream is = vehdburl.openStream();
			InputStreamReader isr = new InputStreamReader( is );
			BufferedReader br = new BufferedReader( isr );

			String dataLine = null;

			while( ( dataLine = br.readLine() ) != null )
			{
				this.addDataLineToAllData( dataLine );
			}

			if( initializeWithData )
			{
				this.fillTempData();
			}
		}
		catch( Exception e )
		{
			e.printStackTrace();
		}
	}

	private void addDataLineToAllData( String dataLine )
	{
		this.m_AllData.add( parseDataLine( dataLine ) );
	}

	private String[] parseDataLine( String data )
	{
		String[] array = data.split(",");
		return array;
	}

	private void fillTempData()
	{
		this.m_TempData.clear();

		// Don't start at the first line, because its just a header
		// and we don't want to show it in the tempData that is
		// displayed in the JTable.
		for( int index = 1; index < this.m_AllData.size(); index++ )
		{
			SatelliteData satdata = this.getAllData( index );

			if( this.passForceCodeCheck( satdata ) )
			{
				if( this.passMissionCheck( satdata ) )
				{
					if( this.passMassFuelMinCheck( satdata ))
					{
						if( this.passMassFuelMaxCheck( satdata ))
						{
							if( this.passMassTotalMinCheck( satdata ))
							{
								if( this.passMassTotalMaxCheck( satdata ))
								{
									this.addTempData( this.allDataToTempArray( satdata ) );
								}
							}
						}
					}
				}
			}
		}

		this.fireTableDataChanged();
	}

	private String[] allDataToTempArray( SatelliteData satdata )
	{
		String[] newData = new String[6];

		newData[ 0 ] = satdata.ID; 			// ID
		newData[ 1 ] = satdata.Name; 		// Name
		newData[ 2 ] = satdata.ForceCode; 	// Force Code
		newData[ 3 ] = satdata.Mission; 	// Mission
		newData[ 4 ] = satdata.TotalMass; 	// Total Mass
		newData[ 5 ] = satdata.FuelMass; 	// Fuel Mass

		return newData;
	}

	public int addTempData( String[] data )
	{
		int index = this.m_TempData.size();

		this.m_TempData.add( index, data );

		this.fireTableRowsInserted( index, index );

		return index;
	}

	public void removeTempData( String[] data )
	{
		int index = findRowIndexInTempData( data );

		if( index != -1 )
		{
			this.m_TempData.remove( index );

			this.fireTableRowsDeleted( index, index );
		}
	}

	public String[] getTempData( int index )
	{
		return this.m_TempData.get( index );
	}

	public SatelliteData getAllData( int index )
	{
		SatelliteData satdata = null;

		String[] alldata = ( String[] )this.m_AllData.get( index );

		satdata 					= new SatelliteData();
		satdata.ID 					= alldata[ SatelliteData.s_ID ];
		satdata.Name 				= alldata[ SatelliteData.s_NAME ];
		satdata.Type 				= alldata[ SatelliteData.s_TYPE ];
		satdata.ForceCode 			= alldata[ SatelliteData.s_FORCECODE ];
		satdata.Mission 			= alldata[ SatelliteData.s_MISSION ];
		satdata.State 				= alldata[ SatelliteData.s_STATE ];
		satdata.TotalMass 			= alldata[ SatelliteData.s_TOTALMASS ];
		satdata.FuelMass 			= alldata[ SatelliteData.s_FUELMASS ];
		satdata.CountryOfOrigin 	= alldata[ SatelliteData.s_COUNTRYOFORIGIN ];
		satdata.Size 				= alldata[ SatelliteData.s_SIZE ];
		satdata.RCS 				= alldata[ SatelliteData.s_RCS ];
		satdata.DV 					= alldata[ SatelliteData.s_DV ];
		satdata.Attitude 			= alldata[ SatelliteData.s_ATTITUDE ];
		satdata.MinFrequency 		= alldata[ SatelliteData.s_MINFREQ ];
		satdata.MaxFrequency 		= alldata[ SatelliteData.s_MAXFREQ ];
		satdata.OpCapacity 			= alldata[ SatelliteData.s_OPCAP ];
		satdata.Notes 				= alldata[ SatelliteData.s_NOTES ];
		satdata.Loaded 				= alldata[ SatelliteData.s_LOADED ];

		return satdata;
	}

	public SatelliteData getAllData( String name )
	{
		SatelliteData satdata = null;

		for( int index = 0; index < this.m_AllData.size(); index++ )
		{
			String[] alldata = ( String[] )this.m_AllData.get( index );

			if( name.equalsIgnoreCase( alldata[ SatelliteData.s_NAME ] ) )
			{
				satdata 					= new SatelliteData();
				satdata.ID 					= alldata[ SatelliteData.s_ID ];
				satdata.Name 				= alldata[ SatelliteData.s_NAME ];
				satdata.Type 				= alldata[ SatelliteData.s_TYPE ];
				satdata.ForceCode 			= alldata[ SatelliteData.s_FORCECODE ];
				satdata.Mission 			= alldata[ SatelliteData.s_MISSION ];
				satdata.State 				= alldata[ SatelliteData.s_STATE ];
				satdata.TotalMass 			= alldata[ SatelliteData.s_TOTALMASS ];
				satdata.FuelMass 			= alldata[ SatelliteData.s_FUELMASS ];
				satdata.CountryOfOrigin 	= alldata[ SatelliteData.s_COUNTRYOFORIGIN ];
				satdata.Size 				= alldata[ SatelliteData.s_SIZE ];
				satdata.RCS 				= alldata[ SatelliteData.s_RCS ];
				satdata.DV 					= alldata[ SatelliteData.s_DV ];
				satdata.Attitude 			= alldata[ SatelliteData.s_ATTITUDE ];
				satdata.MinFrequency 		= alldata[ SatelliteData.s_MINFREQ ];
				satdata.MaxFrequency 		= alldata[ SatelliteData.s_MAXFREQ ];
				satdata.OpCapacity 			= alldata[ SatelliteData.s_OPCAP ];
				satdata.Notes 				= alldata[ SatelliteData.s_NOTES ];
				satdata.Loaded 				= alldata[ SatelliteData.s_LOADED ];
			}
		}

		return satdata;
	}

	private int findRowIndexInTempData( String[] data )
	{
		boolean found = false;
		int index = -1;

		for( int j = 0; j < this.m_TempData.size() && !found; j++ )
		{
			String[] tempData = this.m_TempData.get( j );

			for( int i = 0; i < data.length; i++ )
			{
				if( !(data[ i ].equals( tempData[ i ] ) ) )
				{
					break;
				}

				if( i == data.length - 1 )
				{
					found = true;
					index = j;
				}
			}
		}

		return index;
	}

	public int getAllDataIndexOfId( String ID )
	{
		boolean found = false;
		int index = -1;

		for( int i = 0; i < this.m_AllData.size() && !found; i++ )
		{
			String[] allData = ( String[] )this.m_AllData.get( i );

			if( allData[ SatelliteData.s_ID ].equalsIgnoreCase( ID ) )
			{
				found = true;
				index = i;
			}
		}

		return index;
	}

	public void setForceCodes( boolean ALL, boolean BLUE, boolean RED, boolean WHITE )
	{
		this.m_fALL 	= ALL;
		this.m_fBLUE 	= BLUE;
		this.m_fRED 	= RED;
		this.m_fWHITE 	= WHITE;

		this.fillTempData();
	}

	private boolean passForceCodeCheck( SatelliteData satdata )
	{
		boolean pass = false;

		if( this.m_fALL )
		{
			pass = true;
		}
		else if( this.m_fBLUE )
		{
			if( satdata.ForceCode.equalsIgnoreCase( "BLUE" ) )
			{
				pass = true;
			}
		}
		else if( this.m_fRED )
		{
			if( satdata.ForceCode.equalsIgnoreCase( "RED" ) )
			{
				pass = true;
			}
		}
		else if( this.m_fWHITE )
		{
			if( satdata.ForceCode.equalsIgnoreCase( "WHITE" ) )
			{
				pass = true;
			}
		}

		return pass;
	}

	public void setMission( boolean ALL, boolean EO, boolean IR, boolean ELINT )
	{
		this.m_mALL 	= ALL;
		this.m_mEO 		= EO;
		this.m_mIR 		= IR;
		this.m_mELINT 	= ELINT;

		this.fillTempData();
	}

	private boolean passMissionCheck( SatelliteData satdata )
	{
		boolean pass = false;

		if( this.m_mALL )
		{
			pass = true;
		}
		else if( this.m_mEO )
		{
			if( satdata.Mission.equalsIgnoreCase( "EO" ) )
			{
				pass = true;
			}
		}
		else if( this.m_mIR )
		{
			if( satdata.Mission.equalsIgnoreCase( "IR" ) )
			{
				pass = true;
			}
		}
		else if( this.m_mELINT )
		{
			if( satdata.Mission.equalsIgnoreCase( "ELINT" ) )
			{
				pass = true;
			}
		}

		return pass;
	}

	public void setMassFuelMin( boolean use, double min )
	{
		this.m_bMassFuelMin = use;
		this.m_dMassFuelMin = min;

		this.fillTempData();
	}

	private boolean passMassFuelMinCheck( SatelliteData satdata )
	{
		boolean pass = false;

		try
		{
			if( !this.m_bMassFuelMin )
			{
				pass = true;
			}
			else
			{
				String sfuelmass = satdata.FuelMass;

				double dfuelmass = Double.parseDouble( sfuelmass );

				if( dfuelmass > this.m_dMassFuelMin )
				{
					pass = true;
				}
			}
		}
		catch( Exception e )
		{
			e.printStackTrace();
		}

		return pass;
	}

	public void setMassFuelMax( boolean use, double max )
	{
		this.m_bMassFuelMax = use;
		this.m_dMassFuelMax = max;

		this.fillTempData();
	}

	private boolean passMassFuelMaxCheck( SatelliteData satdata )
	{
		boolean pass = false;

		try
		{
			if( !this.m_bMassFuelMax )
			{
				pass = true;
			}
			else
			{
				String sfuelmass = satdata.FuelMass;

				double dfuelmass = Double.parseDouble( sfuelmass );

				if( dfuelmass < this.m_dMassFuelMax )
				{
					pass = true;
				}
			}
		}
		catch( Exception e )
		{
			e.printStackTrace();
		}

		return pass;
	}

	public void setMassTotalMin( boolean use, double min )
	{
		this.m_bMassTotalMin = use;
		this.m_dMassTotalMin = min;

		this.fillTempData();
	}

	private boolean passMassTotalMinCheck( SatelliteData satdata )
	{
		boolean pass = false;

		try
		{
			if( !this.m_bMassTotalMin )
			{
				pass = true;
			}
			else
			{
				String stotalmass = satdata.TotalMass;

				double dtotalmass = Double.parseDouble( stotalmass );

				if( dtotalmass > this.m_dMassTotalMin )
				{
					pass = true;
				}
			}
		}
		catch( Exception e )
		{
			e.printStackTrace();
		}

		return pass;
	}

	public void setMassTotalMax( boolean use, double max )
	{
		this.m_bMassTotalMin = use;
		this.m_dMassTotalMax = max;

		this.fillTempData();
	}

	private boolean passMassTotalMaxCheck( SatelliteData satdata )
	{
		boolean pass = false;

		try
		{
			if( !this.m_bMassTotalMax )
			{
				pass = true;
			}
			else
			{
				String stotalmass = satdata.TotalMass;

				double dtotalmass = Double.parseDouble( stotalmass );

				if( dtotalmass < this.m_dMassTotalMax )
				{
					pass = true;
				}
			}
		}
		catch( Exception e )
		{
			e.printStackTrace();
		}

		return pass;
	}

	public int getColumnCount()
	{
		return m_ColumnNames.length;
	}

	public int getRowCount()
	{
		int cnt = 0;

		if( this.m_TempData != null )
		{
			cnt = this.m_TempData.size();
		}
		else
		{
			cnt = 1;
		}

		return cnt;
	}

	public String getColumnName( int col )
	{
		return this.m_ColumnNames[ col ];
	}

	public Object getValueAt( int row, int col )
	{
		Object o = null;

		if( this.m_TempData != null )
		{
			String[] a = this.m_TempData.get( row );
			o = a[ col ];
		}
		else
		{
			o = new String( "Vehicle" );
		}

		return o;
	}

	public boolean isCellEditable( int row, int col )
	{
		return false;
	}
}

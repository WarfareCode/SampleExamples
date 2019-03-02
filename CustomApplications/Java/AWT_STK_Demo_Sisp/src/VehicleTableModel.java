import java.net.URL;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.BufferedReader;
import java.util.ArrayList;

import javax.swing.table.AbstractTableModel;

public class VehicleTableModel
extends AbstractTableModel
{
	private static final long serialVersionUID = 1L;

	private String[] m_ColumnNames = { "ID", "Name", "Force Code", "Theater", "Platform" };
	private ArrayList<String[]> m_AllData;
	private ArrayList<String[]> m_TempData;

	private boolean m_fALL;
	private boolean m_fBLUE;
	private boolean m_fRED;
	private boolean m_fWHITE;

	private boolean m_tALL;
	private boolean m_tUSPACOM;
	private boolean m_tUSNORTHCOM;
	private boolean m_tUSEUCOM;
	private boolean m_tUSSOUTHCOM;
	private boolean m_tUSCENTCOM;

	private boolean m_pALL;
	private boolean m_pGROUND;
	private boolean m_pAIRCRAFT;
	private boolean m_pSHIP;

	public VehicleTableModel( boolean initializeWithData )
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
			this.m_tALL = true;
			this.m_pALL = true;

			URL vehdburl = VehicleTableModel.class.getResource( "VehicleDatabase.csv" );
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
		//System.out.println( data );
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
			VehicleData vehdata = this.getAllData( index );

			if( this.passForceCodeCheck( vehdata ) )
			{
				if( this.passTheaterCheck( vehdata ) )
				{
					if( this.passPlatformCheck( vehdata ))
					{
						this.addTempData( this.allDataToTempArray( vehdata ) );
					}
				}
			}
		}

		this.fireTableDataChanged();
	}

	private String[] allDataToTempArray( VehicleData vehdata )
	{
		String[] newData = new String[5];

		newData[ 0 ] = vehdata.ID; 			// ID
		newData[ 1 ] = vehdata.Name; 		// Name
		newData[ 2 ] = vehdata.ForceCode; 	// Force Code
		newData[ 3 ] = vehdata.Theater; 	// Theater
		newData[ 4 ] = vehdata.Type; 		// Platform

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
		return ( String[] )this.m_TempData.get( index );
	}

	public VehicleData getAllData( int index )
	{
		VehicleData vehdata = null;

		String[] alldata = ( String[] )this.m_AllData.get( index );

		vehdata 					= new VehicleData();
		vehdata.ID 					= alldata[ VehicleData.s_ID ];
		vehdata.Name 				= alldata[ VehicleData.s_NAME ];
		vehdata.Type 				= alldata[ VehicleData.s_TYPE ];
		vehdata.ForceCode 			= alldata[ VehicleData.s_FORCECODE ];
		vehdata.Mission 			= alldata[ VehicleData.s_MISSION ];
		vehdata.State 				= alldata[ VehicleData.s_STATE ];
		vehdata.CountryOfOrigin 	= alldata[ VehicleData.s_COUNTRYOFORIGIN ];
		vehdata.Weapons 			= alldata[ VehicleData.s_WEAPONS ];
		vehdata.Theater 			= alldata[ VehicleData.s_THEATER ];
		vehdata.OpCapacity 			= alldata[ VehicleData.s_OPCAPACITY ];
		vehdata.Notes 				= alldata[ VehicleData.s_NOTES ];
		vehdata.Loaded 				= alldata[ VehicleData.s_LOADED ];

		return vehdata;
	}

	public VehicleData getAllData( String name )
	{
		VehicleData vehdata = null;

		for( int index = 0; index < this.m_AllData.size(); index++ )
		{
			String[] alldata = ( String[] )this.m_AllData.get( index );

			if( name.equalsIgnoreCase( alldata[ VehicleData.s_NAME ] ) )
			{
				vehdata 					= new VehicleData();
				vehdata.ID 					= alldata[ VehicleData.s_ID ];
				vehdata.Name 				= alldata[ VehicleData.s_NAME ];
				vehdata.Type 				= alldata[ VehicleData.s_TYPE ];
				vehdata.ForceCode 			= alldata[ VehicleData.s_FORCECODE ];
				vehdata.Mission 			= alldata[ VehicleData.s_MISSION ];
				vehdata.State 				= alldata[ VehicleData.s_STATE ];
				vehdata.CountryOfOrigin 	= alldata[ VehicleData.s_COUNTRYOFORIGIN ];
				vehdata.Weapons 			= alldata[ VehicleData.s_WEAPONS ];
				vehdata.Theater 			= alldata[ VehicleData.s_THEATER ];
				vehdata.OpCapacity 			= alldata[ VehicleData.s_OPCAPACITY ];
				vehdata.Notes 				= alldata[ VehicleData.s_NOTES ];
				vehdata.Loaded 				= alldata[ VehicleData.s_LOADED ];
			}
		}

		return vehdata;
	}

	private int findRowIndexInTempData( String[] data )
	{
		boolean found = false;
		int index = -1;

		for( int j = 0; j < this.m_TempData.size() && !found; j++ )
		{
			String[] tempData = ( String[] )this.m_TempData.get( j );

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

			if( allData[ VehicleData.s_ID ].equalsIgnoreCase( ID ) )
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

	private boolean passForceCodeCheck( VehicleData vehdata )
	{
		boolean pass = false;

		if( this.m_fALL )
		{
			pass = true;
		}
		else if( this.m_fBLUE )
		{
			if( vehdata.ForceCode.equalsIgnoreCase( "BLUE" ) )
			{
				pass = true;
			}
		}
		else if( this.m_fRED )
		{
			if( vehdata.ForceCode.equalsIgnoreCase( "RED" ) )
			{
				pass = true;
			}
		}
		else if( this.m_fWHITE )
		{
			if( vehdata.ForceCode.equalsIgnoreCase( "WHITE" ) )
			{
				pass = true;
			}
		}

		return pass;
	}

	public void setTheaters( boolean All, boolean USCENTCOM, boolean USEUCOM, boolean USNORTHCOM, boolean USPACOM, boolean USSOUTHCOM )
	{
		this.m_tALL 		= All;
		this.m_tUSCENTCOM 	= USCENTCOM;
		this.m_tUSEUCOM 	= USEUCOM;
		this.m_tUSNORTHCOM 	= USNORTHCOM;
		this.m_tUSPACOM 	= USPACOM;
		this.m_tUSSOUTHCOM 	= USSOUTHCOM;

		this.fillTempData();
	}

	private boolean passTheaterCheck( VehicleData vehdata )
	{
		boolean pass = false;

		if( this.m_tALL )
		{
			pass = true;
		}
		else if( this.m_tUSCENTCOM )
		{
			if( vehdata.Theater.equalsIgnoreCase( "USCENTCOM" ) )
			{
				pass = true;
			}
		}
		else if( this.m_tUSEUCOM )
		{
			if( vehdata.Theater.equalsIgnoreCase( "USEUCOM" ) )
			{
				pass = true;
			}
		}
		else if( this.m_tUSNORTHCOM )
		{
			if( vehdata.Theater.equalsIgnoreCase( "USNORTHCOM" ) )
			{
				pass = true;
			}
		}
		else if( this.m_tUSPACOM )
		{
			if( vehdata.Theater.equalsIgnoreCase( "USPACOM" ) )
			{
				pass = true;
			}
		}
		else if( this.m_tUSSOUTHCOM )
		{
			if( vehdata.Theater.equalsIgnoreCase( "USSOUTHCOM" ) )
			{
				pass = true;
			}
		}

		return pass;
	}

	public void setPlatforms( boolean ALL, boolean GROUND, boolean AIRCRAFT, boolean SHIP )
	{
		this.m_pALL			= ALL;
		this.m_pGROUND		= GROUND;
		this.m_pAIRCRAFT	= AIRCRAFT;
		this.m_pSHIP		= SHIP;

		this.fillTempData();
	}

	private boolean passPlatformCheck( VehicleData vehdata )
	{
		boolean pass = false;

		if( this.m_pALL )
		{
			pass = true;
		}
		else if( this.m_pGROUND )
		{
			if( vehdata.Type.equalsIgnoreCase( "GROUND" ) )
			{
				pass = true;
			}
		}
		else if( this.m_pAIRCRAFT )
		{
			if( vehdata.Type.equalsIgnoreCase( "AIRCRAFT" ) )
			{
				pass = true;
			}
		}
		else if( this.m_pSHIP )
		{
			if( vehdata.Type.equalsIgnoreCase( "SHIP" ) )
			{
				pass = true;
			}
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
			String[] a = (String[])this.m_TempData.get( row );
			o = a[ col ];
		}
		else
		{
			o = new String( "Vehicle" );
		}

		return o;
	}
}

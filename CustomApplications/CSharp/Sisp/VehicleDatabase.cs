using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace Sisp
{
    internal class VehicleDatabase
    {
        #region class members
        static VehicleDatabase instance = null;
        private static int instances;

        private const string DATASET_VEH = "Vehicle";
        private const string DATASET_SAT = "Satellite";

        private DbConnection dbConn;
        private DataTable[] vehicleTable = new DataTable[2];
        private DataTable[] satelliteTable = new DataTable[2];
        private DataSet dbDataset = new DataSet();
        private SQLiteDataAdapter vehicleDataAdapter;
        private SQLiteDataAdapter satelliteDataAdapter;
        #endregion

        #region constructor/destructor
        /// <summary>
        /// Make datasource Singleton
        /// </summary>
        public static VehicleDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VehicleDatabase();
                }
                return instance;
            }
        }

        private VehicleDatabase()
        {
            Initialize();
        }

        ~VehicleDatabase()
        {
            instances--;
            //dbConn.Close();
        }
        #endregion

        #region public functions
        /// <summary>
        /// Makes an sql command that is sent to the database
        /// Interal DataTable structures are updated accordingly
        /// </summary>
        public void FilterSatelliteData(int forceCode, int mission, bool minMassChecked, int minMassValue, bool maxMassChecked, int maxMassValue, bool minFuelChecked, int minFuelValue, bool maxFuelChecked, int maxFuelValue)
        {
            string sql = GetSatelliteSql(
                forceCode,
                mission,
                minMassChecked,
                minMassValue,
                maxMassChecked,
                maxMassValue,
                minFuelChecked,
                minFuelValue,
                maxFuelChecked,
                maxFuelValue);

            satelliteDataAdapter.SelectCommand.CommandText = sql;

            if (dbDataset.Tables[DATASET_SAT] != null)
                dbDataset.Tables[DATASET_SAT].Clear();
            satelliteDataAdapter.Fill(dbDataset, DATASET_SAT);

            satelliteTable[0].Clear();
            satelliteTable[1].Clear();

            foreach (DataRow r in dbDataset.Tables[DATASET_SAT].Rows)
            {
                if ((long)r[SatelliteInfo.Loaded] == 0L)
                {
                    DataRow newRow = satelliteTable[0].NewRow();
                    newRow["ID"] = r[SatelliteInfo.ID];
                    newRow["Vehicle Name"] = r[SatelliteInfo.Name];
                    newRow["Force Code"] = r[SatelliteInfo.ForceCode];
                    newRow["Mission"] = r[SatelliteInfo.Mission];
                    newRow["Mass"] = r[SatelliteInfo.TotalMass];
                    newRow["Fuel Mass"] = r[SatelliteInfo.FuelMass];

                    satelliteTable[0].Rows.Add(newRow);
                }
                else
                {
                    DataRow newRow = satelliteTable[1].NewRow();
                    newRow["ID"] = r[SatelliteInfo.ID];
                    newRow["Vehicle Name"] = r[SatelliteInfo.Name];
                    newRow["Force Code"] = r[SatelliteInfo.ForceCode];
                    newRow["Mission"] = r[SatelliteInfo.Mission];
                    newRow["Mass"] = r[SatelliteInfo.TotalMass];
                    newRow["Fuel Mass"] = r[SatelliteInfo.FuelMass];

                    satelliteTable[1].Rows.Add(newRow);
                }
            }
        }

        /// <summary>
        /// Makes an sql command that is sent to the database
        /// Interal DataTable structures are updated accordingly
        /// </summary>
        public void FilterVehicleData(int forceCode, int theater, int platform)
        {
            string sql = GetVehicleSql(
                forceCode,
                theater,
                platform);

            vehicleDataAdapter.SelectCommand.CommandText = sql;

            if (dbDataset.Tables[DATASET_VEH] != null)
                dbDataset.Tables[DATASET_VEH].Clear();
            vehicleDataAdapter.Fill(dbDataset, DATASET_VEH);

            vehicleTable[0].Clear();
            vehicleTable[1].Clear();

            foreach (DataRow r in dbDataset.Tables[DATASET_VEH].Rows)
            {
                if ((long)r["Loaded"] == 0L)
                {
                    DataRow newRow = vehicleTable[0].NewRow();

                    newRow["ID"] = r[VehicleInfo.ID];
                    newRow["Name"] = r[VehicleInfo.Name];
                    newRow["Force Code"] = r[VehicleInfo.ForceCode];
                    newRow["Theater"] = r[VehicleInfo.Theater];
                    newRow["Platform"] = r[VehicleInfo.Type];
                    vehicleTable[0].Rows.Add(newRow);
                }
                else
                {
                    DataRow newRow = vehicleTable[1].NewRow();

                    newRow["ID"] = r[VehicleInfo.ID];
                    newRow["Name"] = r[VehicleInfo.Name];
                    newRow["Force Code"] = r[VehicleInfo.ForceCode];
                    newRow["Theater"] = r[VehicleInfo.Theater];
                    newRow["Platform"] = r[VehicleInfo.Type];
                    vehicleTable[1].Rows.Add(newRow);
                }
            }
        }

        /// <summary>
        /// Loads selected satellite rows
        /// </summary>
        public void LoadSatellite(System.Windows.Forms.DataGridViewSelectedRowCollection selectedRows, out ArrayList ret)
        {
            // sending it our view (selected rows
            // updates the database "loaded" column and returns database strings for stk to process
            ret = new ArrayList();

            for (int i = 0; i < selectedRows.Count; i++)
            {
                string[] rowVal = new string[4];

                string satid = (string)selectedRows[i].Cells["ID"].Value;
                DataRow[] rows = dbDataset.Tables[DATASET_SAT].Select(
                    string.Format("ID = {0}", satid));
                rows[0]["Loaded"] = 1;

                rowVal[0] = (string)rows[0][SatelliteInfo.Name];
                rowVal[1] = (string)rows[0][SatelliteInfo.State];
                rowVal[2] = (string)rows[0][SatelliteInfo.ForceCode];
                rowVal[3] = Convert.ToString(rows[0][SatelliteInfo.OpCapacity]);

                ret.Add(rowVal);

                // Update Database
                satelliteDataAdapter.Update(rows);
            }
        }

        /// <summary>
        /// Loads selected vehicle rows
        /// </summary>
        public void LoadVehicle(System.Windows.Forms.DataGridViewSelectedRowCollection selectedRows, out ArrayList ret)
        {
            ret = new ArrayList();

            for (int i = 0; i < selectedRows.Count; i++)
            {
                string[] rowVal = new string[5];

                string vehid = (string)selectedRows[i].Cells["ID"].Value;
                DataRow[] rows = dbDataset.Tables[DATASET_VEH].Select(
                    string.Format("ID = {0}", vehid));
                rows[0]["Loaded"] = 1;

                rowVal[0] = (string)rows[0][VehicleInfo.Name];
                rowVal[1] = (string)rows[0][VehicleInfo.Type];
                rowVal[2] = (string)rows[0][VehicleInfo.State];
                rowVal[3] = (string)rows[0][VehicleInfo.ForceCode];
                rowVal[4] = (string)rows[0][VehicleInfo.OpCapacity];

                ret.Add(rowVal);

                // Update Database
                vehicleDataAdapter.Update(rows);
            }
        }

        /// <summary>
        /// Unloads selected satellite rows
        /// </summary>
        public void UnloadSatellie(System.Windows.Forms.DataGridViewSelectedRowCollection selectedRows, out ArrayList ret)
        {
            ret = new ArrayList();

            for (int i = 0; i < selectedRows.Count; i++)
            {
                string[] rowVals = new string[1];
                string satid = (string)selectedRows[i].Cells["ID"].Value;
                DataRow[] rows = dbDataset.Tables[DATASET_SAT].Select(
                    string.Format("ID = {0}", satid));
                rows[0]["Loaded"] = 0;

                rowVals[0] = (string)rows[0][SatelliteInfo.Name];

                ret.Add(rowVals);

                // Update Database
                satelliteDataAdapter.Update(rows);
            }
        }

        /// <summary>
        /// Unloads selected vehicle rows
        /// </summary>
        public void UnloadVehicle(System.Windows.Forms.DataGridViewSelectedRowCollection selectedRows, out ArrayList ret)
        {
            ret = new ArrayList();

            for (int i = 0; i < selectedRows.Count; i++)
            {
                string[] rowVals = new string[2];
                string vehid = (string)selectedRows[i].Cells["ID"].Value;
                DataRow[] rows = dbDataset.Tables[DATASET_VEH].Select(
                    string.Format("ID = {0}", vehid));
                rows[0]["Loaded"] = 0;

                rowVals[0] = (string)rows[0][VehicleInfo.Name];
                rowVals[1] = (string)rows[0][VehicleInfo.Type];

                ret.Add(rowVals);

                // Update Database
                vehicleDataAdapter.Update(rows);
            }
        }

        /// <summary>
        /// Returns complete vehicle information from Datasource into vehicleInfoArrayList
        /// </summary>
        public void GetVehicleInfo(string name, out ArrayList vehicleInfoArrayList)
        {
            vehicleInfoArrayList = new ArrayList();

            DataRow[] table;
            table = dbDataset.Tables[DATASET_VEH].Select(VehicleInfo.Name + " = '" + name + "'");

            if (table.Length > 0)
            {
                vehicleInfoArrayList.Add(Convert.ToString(table[0][VehicleInfo.Name]));
                vehicleInfoArrayList.Add(Convert.ToString(table[0][VehicleInfo.Type]));
                vehicleInfoArrayList.Add(Convert.ToString(table[0][VehicleInfo.ForceCode]));
                vehicleInfoArrayList.Add(Convert.ToString(table[0][VehicleInfo.Mission]));
                vehicleInfoArrayList.Add(Convert.ToString(table[0][VehicleInfo.State]));
                vehicleInfoArrayList.Add(Convert.ToString(table[0][VehicleInfo.CountryOfOrigin]));
                vehicleInfoArrayList.Add(Convert.ToString(table[0][VehicleInfo.Weapons]));
                vehicleInfoArrayList.Add(Convert.ToString(table[0][VehicleInfo.Theater]));
                vehicleInfoArrayList.Add(Convert.ToString(table[0][VehicleInfo.OpCapacity]));
                vehicleInfoArrayList.Add(Convert.ToString(table[0][VehicleInfo.Loaded]));
                vehicleInfoArrayList.Add(Convert.ToString(table[0][VehicleInfo.Notes]));
            }
        }

        /// <summary>
        /// Returns complete satellite information from Datasource into satelliteInfoArrayList
        /// </summary>
        public void GetSatelliteInfo(string name, out ArrayList satelliteInfoArrayList)
        {
            satelliteInfoArrayList = new ArrayList();

            DataRow[] table;
            table = dbDataset.Tables[DATASET_SAT].Select(SatelliteInfo.Name + " = '" + name + "'");

            if (table.Length > 0)
            {
                satelliteInfoArrayList.Add(Convert.ToString(table[0][SatelliteInfo.Name]));
                satelliteInfoArrayList.Add(Convert.ToString(table[0][SatelliteInfo.ForceCode]));
                satelliteInfoArrayList.Add(Convert.ToString(table[0][SatelliteInfo.CountryOfOrigin]));
                satelliteInfoArrayList.Add(Convert.ToString(table[0][SatelliteInfo.Size]));
                satelliteInfoArrayList.Add(Convert.ToString(table[0][SatelliteInfo.RCS]));
                satelliteInfoArrayList.Add(Convert.ToString(table[0][SatelliteInfo.TotalMass]));
                satelliteInfoArrayList.Add(Convert.ToString(table[0][SatelliteInfo.FuelMass]));
                satelliteInfoArrayList.Add(Convert.ToString(table[0][SatelliteInfo.DV]));
                satelliteInfoArrayList.Add(Convert.ToString(table[0][SatelliteInfo.Attitude]));
                satelliteInfoArrayList.Add(Convert.ToString(table[0][SatelliteInfo.OpCapacity]));
                satelliteInfoArrayList.Add(Convert.ToString(table[0][SatelliteInfo.Notes]));
            }
        }

        /// <summary>
        /// Returns the value for datatable at index
        /// </summary>
        public object GetValue(LoadedType type, int index, string key)
        {
            switch (type)
            {
                case LoadedType.LoadedSatellite:
                    return ((DataRow)SatelliteLoaded.GetValue(index))[key];
                case LoadedType.LoadedVehicle:
                    return ((DataRow)VehicleLoaded.GetValue(index))[key];
                case LoadedType.UnloadedSatellite:
                    return ((DataRow)SatelliteUnloaded.GetValue(index))[key];
                case LoadedType.UnloadedVehicle:
                    return ((DataRow)VehicleUnloaded.GetValue(index))[key];
            }

            return "";
        }

        /// <summary>
        /// Returns number Length of type datasource
        /// Necessarily for iteration
        /// </summary>
        public int GetCount(LoadedType type)
        {
            switch (type)
            {
                case LoadedType.LoadedSatellite:
                    return SatelliteLoaded.Length;
                case LoadedType.LoadedVehicle:
                    return VehicleLoaded.Length;
                case LoadedType.UnloadedSatellite:
                    return SatelliteUnloaded.Length;
                case LoadedType.UnloadedVehicle:
                    return VehicleUnloaded.Length;
            }

            return 0;
        }

        #endregion

        /// <summary>
        /// Unviels internal datasource in order for Form code to bind
        /// </summary>
        #region public properties
        public DataTable UnshownVehicle
        {
            get { return vehicleTable[0]; }
        }
        public DataTable ShownVehicle
        {
            get { return vehicleTable[1]; }
        }
        public DataTable UnshownSatellite
        {
            get { return satelliteTable[0]; }
        }
        public DataTable ShownSatellite
        {
            get { return satelliteTable[1]; }
        }

        #endregion

        #region public enums
        public enum LoadedType
        {
            LoadedSatellite,
            UnloadedSatellite,
            AllSatellite,  // not used yet, can be deleted if not needed
            LoadedVehicle,
            UnloadedVehicle,
            AllVehicle  // not used yet, can be deleted if not needed
        }
        #endregion

        #region private properties
        private DataRow[] SatelliteLoaded
        {
            get { return dbDataset.Tables[DATASET_SAT].Select("Loaded = 1"); }
        }
        private DataRow[] VehicleLoaded
        {
            get { return dbDataset.Tables[DATASET_VEH].Select("Loaded = 1"); }
        }
        private DataRow[] SatelliteUnloaded
        {
            get { return dbDataset.Tables[DATASET_SAT].Select("Loaded = 0"); }
        }
        private DataRow[] VehicleUnloaded
        {
            get { return dbDataset.Tables[DATASET_VEH].Select("Loaded = 0"); }
        }
        #endregion

        #region private functions
        internal void Initialize()
        {
            dbConn = new SQLiteConnection("Data Source=SispDB.db3;New=False;FailIfMissing=true");
            dbConn.Open();

            vehicleDataAdapter = new SQLiteDataAdapter();
            vehicleDataAdapter.SelectCommand = new SQLiteCommand();
            vehicleDataAdapter.SelectCommand.Connection = new SQLiteConnection(dbConn.ConnectionString);

            satelliteDataAdapter = new SQLiteDataAdapter();
            satelliteDataAdapter.SelectCommand = new SQLiteCommand();
            satelliteDataAdapter.SelectCommand.Connection = new SQLiteConnection(dbConn.ConnectionString);

            // used for the dataset update command
            System.Data.SQLite.SQLiteCommandBuilder odcb1 = new System.Data.SQLite.SQLiteCommandBuilder(vehicleDataAdapter);
            System.Data.SQLite.SQLiteCommandBuilder odcb2 = new System.Data.SQLite.SQLiteCommandBuilder(satelliteDataAdapter);

            MakeTableColumns();
        }
        internal void MakeTableColumns()
        {
            vehicleTable[0] = new DataTable();
            vehicleTable[1] = new DataTable();
            satelliteTable[0] = new DataTable();
            satelliteTable[1] = new DataTable();

            // note, the only reason we use ID is b/c it is the primary column
            // if we really wanted to we could get ride of ID and use Name or something
            // but that would be just a coincidence that it is unique
            vehicleTable[0].Columns.AddRange(new DataColumn[] {
                new DataColumn("ID", typeof(string)),
                new DataColumn("Name", typeof(string)),
                new DataColumn("Force Code", typeof(string)),
                new DataColumn("Theater", typeof(string)),
                new DataColumn("Platform", typeof(string))});
            vehicleTable[1].Columns.AddRange(new DataColumn[] {
                new DataColumn("ID", typeof(string)),
                new DataColumn("Name", typeof(string)),
                new DataColumn("Force Code", typeof(string)),
                new DataColumn("Theater", typeof(string)),
                new DataColumn("Platform", typeof(string))});

            satelliteTable[0].Columns.AddRange(new DataColumn[] {
                new DataColumn("ID", typeof(string)),
                new DataColumn("Vehicle Name", typeof(string)),
                new DataColumn("Force Code", typeof(string)),
                new DataColumn("Mission", typeof(string)),
                new DataColumn("Mass", typeof(string)),
                new DataColumn("Fuel Mass", typeof(string))});
            satelliteTable[1].Columns.AddRange(new DataColumn[] {
                new DataColumn("ID", typeof(string)),
                new DataColumn("Vehicle Name", typeof(string)),
                new DataColumn("Force Code", typeof(string)),
                new DataColumn("Mission", typeof(string)),
                new DataColumn("Mass", typeof(string)),
                new DataColumn("Fuel Mass", typeof(string))});
        }
        internal string GetSatelliteSql(int forceCode, int mission, bool minMassChecked, int minMassValue, bool maxMassChecked, int maxMassValue, bool minFuelChecked, int minFuelValue, bool maxFuelChecked, int maxFuelValue)
        {
            // Build the SQL search string using the force code, mission, mass, and fuel search options.
            string sql;
            string massSql;
            string fuelSql;

            sql = "SELECT * FROM SatelliteDatabase Where ";

            // force code
            switch (forceCode)
            {
                case 0:
                    sql = sql + " ( ForceCode = 'Blue' or ForceCode = 'Red' or ForceCode = 'Grey')";
                    break;
                case 1:
                    sql = sql + " ( ForceCode = 'Blue' ) ";
                    break;
                case 2:
                    sql = sql + " ( ForceCode = 'Red' ) ";
                    break;
                case 3:
                    sql = sql + " ( ForceCode = 'Grey' ) ";
                    break;
            }

            sql = sql + " and ";

            // mission
            switch (mission)
            {
                case 0:
                    sql = sql + " ( Mission = 'EO' or Mission= 'IR' or Mission= 'ELINT')";
                    break;
                case 1:
                    sql = sql + " ( Mission = 'EO' ) ";
                    break;
                case 2:
                    sql = sql + " ( Mission = 'IR' ) ";
                    break;
                case 3:
                    sql = sql + " ( Mission = 'ELINT' ) ";
                    break;
            }

            // mass
            massSql = "";
            if (minMassChecked)
            {
                massSql = "Mass >= " + minMassValue;
            }

            if (maxMassChecked)
            {
                if (massSql.Length > 0)
                    massSql = massSql + " and ";
                massSql = massSql + "Mass <= " + maxMassValue;
            }

            if (massSql.Length > 0)
                sql = sql + " and (" + massSql + ")";

            // fuel
            fuelSql = "";
            if (minFuelChecked)
            {
                fuelSql = "Fuel >= " + minFuelValue;
            }

            if (maxFuelChecked)
            {
                if (fuelSql.Length > 0)
                    fuelSql = fuelSql + " and ";
                fuelSql = fuelSql + "Fuel <= " + maxFuelValue;
            }

            if (fuelSql.Length > 0)
                sql = sql + " and (" + fuelSql + ")";

            return sql;
        }
        internal string GetVehicleSql(int forceCode, int theater, int platform)
        {
            // Build the SQL search string using the force code, theater, and vehicle type search options.

            String sql;

            sql = "SELECT * FROM VehicleDatabase Where ";

            switch (forceCode)
            {
                case 0:
                    sql += " ( ForceCode = 'Blue' or ForceCode = 'Red' or ForceCode = 'Grey')";
                    break;
                case 1:
                    sql += " ( ForceCode = 'Blue' ) ";
                    break;
                case 2:
                    sql += " ( ForceCode = 'Red' ) ";
                    break;
                case 3:
                    sql += " ( ForceCode = 'Grey' ) ";
                    break;
            }

            sql += " and ";

            switch (theater)
            {
                case 0:
                    sql += " ( Theater = 'USNORTHCOM' or Theater = 'USSOUTHCOM' or Theater = 'USPACOM' or Theater = 'USEUCOM' or Theater = 'USCENTCOM')";
                    break;
                case 1:
                    sql += " ( Theater = 'USNORTHCOM' ) ";
                    break;
                case 2:
                    sql += " ( Theater = 'USSOUTHCOM' ) ";
                    break;
                case 3:
                    sql += " ( Theater = 'USPACOM' ) ";
                    break;
                case 4:
                    sql += " ( Theater = 'USEUCOM' ) ";
                    break;
                case 5:
                    sql += " ( Theater = 'USCENTCOM' ) ";
                    break;
            }

            sql += " and ";

            switch (platform)
            {
                case 0:
                    sql += " ( VehicleType = 'Aircraft' or VehicleType = 'Ground' or VehicleType = 'Ship' )";
                    break;
                case 1:
                    sql += " ( VehicleType = 'Ground' ) ";
                    break;
                case 2:
                    sql += " ( VehicleType = 'Aircraft' ) ";
                    break;
                case 3:
                    sql += " ( VehicleType = 'Ship' ) ";
                    break;
                default:
                    sql += " ( VehicleType = 'Aircraft' or VehicleType = 'Ground' or VehicleType = 'Ship' )";
                    break;
            }
            return sql;
        }

        #endregion

    }
}
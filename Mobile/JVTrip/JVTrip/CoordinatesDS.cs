using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using JVSQL;
using JVUtils;

namespace JVTrip
{
    class CoordinatesDS: JVSQL.SQLServerCEDataSet
    {
        double _distance = 0;
        double lastLatitude = 0;
        double lastLongitude = 0;

        public double Distance
        {
            get { return _distance; }
        }

        public void RefreshDistance()
        {
            _distance = 0;
            lastLatitude = 0;
            lastLongitude = 0;

            if (DataTable != null && DataTable.Rows != null && DataTable.Rows.Count > 0)
            {
                foreach (DataRow row in DataTable.Rows)
                {
                    if (lastLatitude != 0 && lastLongitude != 0)
                    {
                        _distance = _distance +
                            Utils.DistanceTo(lastLatitude, lastLongitude,
                                             (double)row["latitude"], (double)row["longitude"]);
                    }

                    lastLatitude = (double)row["latitude"];
                    lastLongitude = (double)row["longitude"];
                }
            }
        }

        public override void SelectAll(string filters, string orderby)
        {
            base.SelectAll(filters, orderby);

            RefreshDistance();
        }

        public void Add(Int64 trip, double latitude, double longitude, double altitude, double speed)
        {
            if (lastLatitude != 0 && lastLongitude != 0) 
                _distance = _distance +
                    Utils.DistanceTo(lastLatitude, lastLongitude, latitude, longitude);

            SqlCeCommand command = DB.SQLDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "insert into coordinates (id, latitude, longitude, altitude, speed) " +
                "                 values (@id, @latitude, @longitude, @altitude, @speed)";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@id", SqlDbType.BigInt, 10);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@latitude", SqlDbType.Float, 30);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@longitude", SqlDbType.Float, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@altitude", SqlDbType.Float, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@speed", SqlDbType.Float, 5);
            command.Parameters.Add(param);

            command.Parameters[0].Value = trip;
            command.Parameters[1].Value = latitude;
            command.Parameters[2].Value = longitude;
            command.Parameters[3].Value = altitude;
            command.Parameters[4].Value = speed;

            DB.SQLDataBase.ExecSQL(command);

            lastLatitude = latitude;
            lastLongitude = longitude;
        }

        public void DelAll(Int64 trip)
        {
            DeleteAll("id = " + System.Convert.ToString(trip));
            _distance = 0;
            lastLatitude = 0;
            lastLongitude = 0;
        }
    }
}

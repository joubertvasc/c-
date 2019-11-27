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
    public class CostsDS : JVSQL.SQLServerCEDataSet
    {
        public double TotalCost
        {
            get 
            {
                double totalCost = 0;

                if (DataTable != null && DataTable.Rows != null && DataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in DataTable.Rows)
                    {
                        totalCost = totalCost + (double)row["vlcost"];
                    }
                }

                return totalCost; 
            }
        }

        public void DelAll(Int64 trip)
        {
            DeleteAll("id = " + System.Convert.ToString(trip));
        }

        public void Add(Int64 trip, string cost, double value, double latitude, double longitude)
        {
            cost = Utils.RemoveChar(cost, (char)39);

            Debug.AddLog("Add Cost: cost=" + cost +
                         ", value=" + System.Convert.ToString(value) +
                         ", lat=" + System.Convert.ToString(latitude) +
                         ", lon=" + System.Convert.ToString(longitude));

            DataRow myRow;
            myRow = DataTable.NewRow();
            myRow["decost"] = cost.Trim();
            myRow["vlcost"] = value;
            myRow["latitude"] = latitude;
            myRow["longitude"] = longitude;
            myRow["dtcreated"] = DateTime.Today;

            SqlCeCommand command = DB.SQLDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "insert into costs (id, decost, vlcost, latitude, longitude) " +
                "           values (@id, '" + cost.Trim() + "', @vlcost, @latitude, @longitude)";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@id", SqlDbType.BigInt, 10);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@vlcost", SqlDbType.Float, 30);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@latitude", SqlDbType.Float, 30);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@longitude", SqlDbType.Float, 5);
            command.Parameters.Add(param);

            command.Parameters[0].Value = trip;
            command.Parameters[1].Value = value;
            command.Parameters[2].Value = latitude;
            command.Parameters[3].Value = longitude;

            DB.SQLDataBase.ExecSQL(command);

            SqlCeDataReader myReader = null;
            DB.SQLDataBase.OpenSQL(
                "select max(cost) as last from costs where id = " + System.Convert.ToString(trip),
                out myReader);

            if (myReader != null)
            {
                if (myReader.Read())
                {
                    try
                    {
                        myRow["cost"] = myReader.GetInt64(0);
                    }
                    catch
                    {
                        myRow["cost"] = 0;
                    }
                }
            }

            DataSet.Tables[0].Rows.Add(myRow);
            DataTable.AcceptChanges();
        }

        public void Update(Int64 trip, int rowid, string cost, double value, double latitude, double longitude)
        {
            DataRow row = DataTable.Rows[rowid];
            if (row != null)
            {
                cost = Utils.RemoveChar(cost, (char)39);

                Debug.AddLog("Update cost: id= " + System.Convert.ToString(trip) +
                             " cost= " + System.Convert.ToString(row["cost"]) +
                             ", decost=" + cost +
                             ", value=" + System.Convert.ToString(value) +
                             ", lat=" + System.Convert.ToString(latitude) +
                             ", lon=" + System.Convert.ToString(longitude));

                row["decost"] = cost.Trim();
                row["vlcost"] = value;
                row["latitude"] = latitude;
                row["longitude"] = longitude;
                DataTable.AcceptChanges();

                SqlCeCommand command = DB.SQLDataBase.SqlCeConnection.CreateCommand();

                command.CommandText =
                    "update costs " +
                    "   set decost = '" + cost.Trim() + "'," +
                    "       vlcost = @vlcost, " +
                    "       latitude = @latitude, " +
                    "       longitude = @longitude " +
                    " where id = " + System.Convert.ToString(trip) +
                    "   and cost = @cost";

                SqlCeParameter param = null;

                param = new SqlCeParameter("@vlcost", SqlDbType.Float, 30);
                command.Parameters.Add(param);

                param = new SqlCeParameter("@latitude", SqlDbType.Float, 30);
                command.Parameters.Add(param);

                param = new SqlCeParameter("@longitude", SqlDbType.Float, 5);
                command.Parameters.Add(param);

                param = new SqlCeParameter("@cost", SqlDbType.Int, 10);
                command.Parameters.Add(param);

                command.Parameters[0].Value = value;
                command.Parameters[1].Value = latitude;
                command.Parameters[2].Value = longitude;
                command.Parameters[3].Value = row["cost"];

                DB.SQLDataBase.ExecSQL(command);
            }
            else
            {
                Debug.AddLog("Update cost: rowid not found");
            }
        }

        public void Del(Int64 trip, int rowid)
        {
            DataRow row = DataTable.Rows[rowid];

            if (row != null)
            {
                Debug.AddLog("Delete cost: id= " + System.Convert.ToString(trip) + " cost=" +
                    System.Convert.ToString(row["cost"]));

                // Erase cost
                string sql = "delete from costs where id = " + System.Convert.ToString(trip) +
                             " and cost = " + System.Convert.ToString(row["cost"]);
                DB.SQLDataBase.ExecSQL(sql);

                // Remove from datagrid
                row.Delete();
                DataTable.AcceptChanges();
            }
            else
            {
                Debug.AddLog("Delete cost: rowid not found");
            }
        }
    }
}

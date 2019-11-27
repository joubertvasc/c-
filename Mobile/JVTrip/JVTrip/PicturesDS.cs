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
    public class PicturesDS : JVSQL.SQLServerCEDataSet
    {
        public void DelAll(Int64 trip)
        {
            DeleteAll("id = " + System.Convert.ToString(trip));
        }

        public void Add(Int64 trip, string picture, string path, double latitude, double longitude)
        {
            picture = Utils.RemoveChar(picture, (char)39);

            Debug.AddLog("Add picture: picture=" + picture +
                         ", path=" + path +
                         ", lat=" + System.Convert.ToString(latitude) +
                         ", lon=" + System.Convert.ToString(longitude));

            DataRow myRow;
            myRow = DataTable.NewRow();
            myRow["depicture"] = picture.Trim();
            myRow["depathpicture"] = path.Trim();
            myRow["latitude"] = latitude;
            myRow["longitude"] = longitude;
            myRow["dtcreated"] = DateTime.Today;

            SqlCeCommand command = DB.SQLDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "insert into pictures (id, depicture, depathpicture, latitude, longitude) " +
                "           values (@id, '" + picture.Trim() + "', '" + path.Trim() + 
                "', @latitude, @longitude)";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@id", SqlDbType.BigInt, 10);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@latitude", SqlDbType.Float, 30);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@longitude", SqlDbType.Float, 5);
            command.Parameters.Add(param);

            command.Parameters[0].Value = trip;
            command.Parameters[1].Value = latitude;
            command.Parameters[2].Value = longitude;

            DB.SQLDataBase.ExecSQL(command);

            SqlCeDataReader myReader = null;
            DB.SQLDataBase.OpenSQL(
                "select max(picture) as last from pictures where id = " + System.Convert.ToString(trip),
                out myReader);

            if (myReader != null)
            {
                if (myReader.Read())
                {
                    try
                    {
                        myRow["picture"] = myReader.GetInt64(0);
                    }
                    catch
                    {
                        myRow["picture"] = 0;
                    }
                }
            }

            DataSet.Tables[0].Rows.Add(myRow);
            DataTable.AcceptChanges();
        }

        public void Update(Int64 trip, int rowid, string picture, string path, double latitude, double longitude)
        {
            DataRow row = DataTable.Rows[rowid];
            if (row != null)
            {
                picture = Utils.RemoveChar(picture, (char)39);

                Debug.AddLog("Update picture: id= " + System.Convert.ToString(trip) +
                             " picture= " + System.Convert.ToString(row["picture"]) +
                             ", depicture=" + picture +
                             ", path=" + path +
                             ", lat=" + System.Convert.ToString(latitude) +
                             ", lon=" + System.Convert.ToString(longitude));

                row["depicture"] = picture.Trim();
                row["depathpicture"] = path.Trim();
                row["latitude"] = latitude;
                row["longitude"] = longitude;
                DataTable.AcceptChanges();

                SqlCeCommand command = DB.SQLDataBase.SqlCeConnection.CreateCommand();

                command.CommandText =
                    "update pictures " +
                    "   set depicture = '" + picture.Trim() + "'," +
                    "       depathpicture = '" + path.Trim() + "', " +
                    "       latitude = @latitude, " +
                    "       longitude = @longitude " +
                    " where id = " + System.Convert.ToString(trip) +
                    "   and picture = @picture";

                SqlCeParameter param = null;

                param = new SqlCeParameter("@latitude", SqlDbType.Float, 30);
                command.Parameters.Add(param);

                param = new SqlCeParameter("@longitude", SqlDbType.Float, 5);
                command.Parameters.Add(param);

                param = new SqlCeParameter("@picture", SqlDbType.Int, 10);
                command.Parameters.Add(param);

                command.Parameters[0].Value = latitude;
                command.Parameters[1].Value = longitude;
                command.Parameters[2].Value = row["picture"];

                DB.SQLDataBase.ExecSQL(command);
            }
            else
            {
                Debug.AddLog("Update picture: rowid not found");
            }
        }

        public void Del(Int64 trip, int rowid)
        {
            DataRow row = DataTable.Rows[rowid];

            if (row != null)
            {
                Debug.AddLog("Delete picture: id= " + System.Convert.ToString(trip) + " pic=" +
                    System.Convert.ToString(row["picture"]));

                // Erase picture
                string sql = "delete from pictures where id = " + System.Convert.ToString(trip) +
                             " and picture = " + System.Convert.ToString(row["picture"]);
                DB.SQLDataBase.ExecSQL(sql);

                // Remove from datagrid
                row.Delete();
                DataTable.AcceptChanges();
            }
            else
            {
                Debug.AddLog("Delete picture: rowid not found");
            }
        }
    }
}

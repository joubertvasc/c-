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
    public class NotesDS : JVSQL.SQLServerCEDataSet
    {
        public void DelAll(Int64 trip)
        {
            DeleteAll("id = " + System.Convert.ToString(trip));
        }

        public void Add(Int64 trip, string notes, double latitude, double longitude)
        {
            notes = Utils.RemoveChar(notes, (char)39);

            Debug.AddLog("Add Note: note=" + notes + ", lat=" + System.Convert.ToString(latitude) +
                         ", lon=" + System.Convert.ToString(longitude));
            
            DataRow myRow;
            myRow = DataTable.NewRow();
            myRow["denote"] = notes.Trim();
            myRow["latitude"] = latitude;
            myRow["longitude"] = longitude;
            myRow["dtcreated"] = DateTime.Today;

            SqlCeCommand command = DB.SQLDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "insert into notes (id, denote, latitude, longitude) " +
                "           values (@id, '" + notes.Trim() + "', @latitude, @longitude)";

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
                "select max(note) as last from notes where id = " + System.Convert.ToString(trip), 
                out myReader);

            if (myReader != null)
            {
                if (myReader.Read())
                {
                    try
                    {
                        myRow["note"] = myReader.GetInt64(0);
                    }
                    catch
                    {
                        myRow["note"] = 0;
                    }
                }
            }

            DataSet.Tables[0].Rows.Add(myRow);
            DataTable.AcceptChanges();
        }

        public void Update(Int64 trip, int rowid, string notes, double latitude, double longitude)
        {
            DataRow row = DataTable.Rows[rowid];
            if (row != null)
            {
                notes = Utils.RemoveChar(notes, (char)39);

                Debug.AddLog("Update note: id= " + System.Convert.ToString(trip) + 
                             " note= " + System.Convert.ToString(row["note"]) +
                             ", denote=" + notes +
                             ", lat=" + System.Convert.ToString(latitude) +
                             ", lon=" + System.Convert.ToString(longitude));

                row["denote"] = notes.Trim();
                row["latitude"] = latitude;
                row["longitude"] = longitude;
                DataTable.AcceptChanges();

                SqlCeCommand command = DB.SQLDataBase.SqlCeConnection.CreateCommand();

                command.CommandText =
                    "update notes " +
                    "   set denote = '" + notes.Trim() + "'," +
                    "       latitude = @latitude, " +
                    "       longitude = @longitude " +
                    " where id = " + System.Convert.ToString(trip) +
                    "   and note = @note";

                SqlCeParameter param = null;

                param = new SqlCeParameter("@latitude", SqlDbType.Float, 30);
                command.Parameters.Add(param);

                param = new SqlCeParameter("@longitude", SqlDbType.Float, 5);
                command.Parameters.Add(param);

                param = new SqlCeParameter("@note", SqlDbType.Int, 10);
                command.Parameters.Add(param);

                command.Parameters[0].Value = latitude;
                command.Parameters[1].Value = longitude;
                command.Parameters[2].Value = row["note"];

                DB.SQLDataBase.ExecSQL(command);
            }
            else
            {
                Debug.AddLog("Update note: rowid not found");
            }
        }

        public void Del(Int64 trip, int rowid)
        {
            DataRow row = DataTable.Rows[rowid];

            if (row != null)
            {
                Debug.AddLog("Delete note: id= " + System.Convert.ToString(trip) + " note=" +
                    System.Convert.ToString(row["note"]));

                // Erase note
                string sql = "delete from notes where id = " + System.Convert.ToString(trip) +
                             " and note = " + System.Convert.ToString(row["note"]);
                DB.SQLDataBase.ExecSQL(sql);

                // Remove from datagrid
                row.Delete();
                DataTable.AcceptChanges();
            }
            else
            {
                Debug.AddLog("Delete note: rowid not found");
            }
        }
    }
}

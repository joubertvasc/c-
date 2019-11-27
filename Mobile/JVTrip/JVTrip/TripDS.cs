using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Text;
using System.Data;
using JVSQL;
using JVUtils;

namespace JVTrip
{
    public class TripDS: JVSQL.SQLServerCEDataSet
    {
        public void Add(string name, string from, string to, string notes)
        {
            name = Utils.RemoveChar(name, (char)39);
            from = Utils.RemoveChar(from, (char)39);
            to = Utils.RemoveChar(to, (char)39);
            notes = Utils.RemoveChar(notes, (char)39);

            Debug.AddLog("Add Trip: name=" + name + ", from=" + from + ", to=" + to + ", notes=" + notes);

            DataRow myRow;
            myRow = DataTable.NewRow();
            myRow["nmtrip"] = name.Trim();
            myRow["nmstart"] = from.Trim();
            myRow["nmdestiny"] = to.Trim();
            myRow["blnotes"] = notes.Trim();

            String sql =
                "insert into trip (nmtrip, nmstart, nmdestiny, blnotes) " +
                " values ('" + name.Trim() + "', '" +
                              from.Trim() + "', '" +
                              to.Trim() + "', '" +
                              notes.Trim() + "')";

            DB.SQLDataBase.ExecSQL(sql);

            SqlCeDataReader myReader = null;
            DB.SQLDataBase.OpenSQL("select max(id) as last from trip", out myReader);

            if (myReader != null)
            {
                if (myReader.Read())
                {
                    try
                    {
                        myRow["id"] = myReader.GetInt64(0);
                    }
                    catch
                    {
                        myRow["id"] = 0;
                    }
                }
            }

            DataSet.Tables[0].Rows.Add(myRow);
            DataTable.AcceptChanges();
        }

        public void Update(int rowid, string name, string from, string to, string notes)
        {
            DataRow row = DataTable.Rows[rowid];
            if (row != null)
            {
                name = Utils.RemoveChar(name, (char)39);
                from = Utils.RemoveChar(from, (char)39);
                to = Utils.RemoveChar(to, (char)39);
                notes = Utils.RemoveChar(notes, (char)39);

                Debug.AddLog("Update Trip: id= " + System.Convert.ToString(row["id"]) +
                    ", name=" + name + ", from=" + from + ", to=" + to + ", notes=" + notes);

                row["nmtrip"] = name.Trim();
                row["nmstart"] = from.Trim();
                row["nmdestiny"] = to.Trim();
                row["blnotes"] = notes.Trim();
                DataTable.AcceptChanges();

                SqlCeCommand command = DB.SQLDataBase.SqlCeConnection.CreateCommand();

                command.CommandText =
                    "update trip " +
                    "   set nmtrip = '" + (string)row["nmtrip"] + "', " +
                    "       nmstart= '" + (string)row["nmstart"] + "', " +
                    "       nmdestiny = '" + (string)row["nmdestiny"] + "', " +
                    "       blnotes= '" + (string)row["blnotes"] + "' " +
                    " where id = " + System.Convert.ToString(row["id"]);

                DB.SQLDataBase.ExecSQL(command);
            }
            else
            {
                Debug.AddLog("Update trip: rowid not found");
            }
        }

        public void Del(int rowid)
        {
            DataRow row = DataTable.Rows[rowid];

            if (row != null)
            {
                Debug.AddLog("Delete Trip: id= " + System.Convert.ToString(row["id"]));

                // Erase coordinates
                CoordinatesDS coord = new CoordinatesDS();
                coord.DB = DB;
                coord.TableName = "coordinates";
                coord.DelAll((Int64)row["id"]);

                // Erase costs
                CostsDS cost = new CostsDS();
                cost.DB = DB;
                cost.TableName = "costs";
                cost.DelAll((Int64)row["id"]);

                // Erase notes
                NotesDS note = new NotesDS();
                note.DB = DB;
                note.TableName = "notes";
                note.DelAll((Int64)row["id"]);

                // Erase pictures
                PicturesDS pic = new PicturesDS();
                pic.DB = DB;
                pic.TableName = "pictures";
                pic.DelAll((Int64)row["id"]);

                // Erase trip
                string sql = "delete from trip where id = " + System.Convert.ToString(row["id"]);
                DB.SQLDataBase.ExecSQL(sql);

                // Remove from datagrid
                row.Delete();
                DataTable.AcceptChanges();
            }
            else
            {
                Debug.AddLog("Delete trip: rowid not found");
            }
        }

        public DataRow FindTripByID(Int64 id)
        {
            foreach (DataRow row in DataTable.Rows)
            {
                if (System.Convert.ToDouble(row["id"]) == id)
                    return row;
            }

            return null;
        }
    }
}

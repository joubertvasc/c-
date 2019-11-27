using System;
using System.Data;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Data.SqlServerCe;
using System.Windows.Forms;
using JVUtils;
using JVSQL;

namespace OpenCellClient
{
	/// <summary>
	/// Summary description for CentroCustoDataSet.
	/// </summary>
	public class CellIdDS: JVSQL.SQLServerCEDataSet
	{
        private int total;

        public int Total
        {
            get { return total; }
        }

        public bool ExistsCellID(string cellID, string LAC, string MNC, string MCC, string signal)
        {
            int result = 0;
            Debug.AddLog("ExistsCellID: cellid=" + cellID + ", LAC=" + LAC + ", MNC=" + MNC + ", MCC=" + MCC + ", Signal=" + signal);

            DataRow myRow;
            myRow = FindRow(cellID, LAC, MNC, MCC, signal); // dataTable.Rows[linha];

            if (myRow == null)
            {
                string sql = "select count(*) as result" +
                             "  from cellid " +
                             " where cellid = '" + cellID + "'" +
                             "   and lac = '" + LAC + "'" +
                             "   and mnc = '" + MNC + "'" +
                             "   and mcc = '" + MCC + "'" + 
                             (signal.Equals("") ? "" : "   and signal = '" + signal + "'");

                SqlCeDataReader myReader = null; 
                DB.SQLDataBase.OpenSQL(sql, out myReader);

                if (myReader != null)
                {
                    if (myReader.Read()) 
                    {
                        result = myReader.GetInt32(0);
                        myReader.Close();
                    }
                }

                return (result > 0);
            }
            else
            {
                return true;
            }
        }

        public DataRow AddCellID(string cellID, string LAC, string MNC, string MCC, string lat, string lon, 
            string alias, string highSignal, string lowSignal, string maxSignal, string minSignal, 
            string signal) 
		{
            DataRow myRow = null;
            try
            {
                Debug.AddLog("AddCellID: cellid=" + cellID + ", LAC=" + LAC + ", MNC=" + MNC + ", MCC=" + MCC +
                             ", LAT=" + lat + ", LON=" + lon);

                myRow = DataTable.NewRow();
                myRow["new"] = "Y";
                myRow["newcelldb"] = "Y";
                myRow["cellid"] = cellID;
                myRow["lac"] = LAC;
                myRow["mnc"] = MNC;
                myRow["mcc"] = MCC;
                myRow["lat"] = "";
                myRow["slat"] = "";
                myRow["lon"] = "";
                myRow["slon"] = "";
                myRow["highSignal"] = highSignal;
                myRow["lowSignal"] = lowSignal;
                myRow["maxSignal"] = maxSignal;
                myRow["minSignal"] = minSignal;
                myRow["signal"] = signal;

                if (!lat.Equals(""))
                {
                    myRow["lat"] = lat;
                    myRow["slat"] =
                        (((string)myRow["lat"]).Length > 9 ? ((string)myRow["lat"]).Substring(0, 9) : (string)myRow["lat"]);
                }

                if (!lon.Equals(""))
                {
                    myRow["lon"] = lon;
                    myRow["slon"] = (((string)myRow["lon"]).Length > 9 ? ((string)myRow["lon"]).Substring(0, 9) : (string)myRow["lon"]);
                }

                DataSet.Tables[0].Rows.Add(myRow);
                DataTable.AcceptChanges();

                String sql =
                    "insert into cellid (cellid, lac, mnc, mcc, lat, lon, slat, slon, new, alias, highsignal, lowsignal, maxsignal, minsignal, signal, newcelldb) " +
                    " values ('" + cellID + "', '" +
                                  LAC + "', '" +
                                  MNC + "', '" +
                                  MCC + "', '" +
                                  (lat) + "', '" +
                                  (lon) + "', '" +
                                  (lat.Equals("") ? "" : (string)myRow["slat"]) + "', '" +
                                  (lon.Equals("") ? "" : (string)myRow["slon"]) + "', 'Y', '" +
                                  alias + "', '" +
                                  highSignal + "', '" +
                                  lowSignal + "', '" +
                                  maxSignal + "', '" +
                                  minSignal + "', '" +
                                  signal + "', 'Y')";

                DB.SQLDataBase.ExecSQL(sql);

                total++;
            }
            catch (Exception e)
            {
                Debug.AddLog("AddCellID: Error while adding a new cell: " + e.Message);
                MessageBox.Show ("Error while adding a new cell: " + e.Message);
                return null;
            }

            return myRow;
		}

        public DataRow FindRow(string cellID, string LAC, string MNC, string MCC, string signal)
        {
            Debug.AddLog("FindRow: cellid=" + cellID + ", LAC=" + LAC + ", MNC=" + MNC + 
                         ", MCC=" + MCC + ", Signal=" + signal);

            DataRow dr;
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                dr = DataTable.Rows[i];

                if (((string)dr["cellid"]).Equals(cellID) &&
                    ((string)dr["lac"]).Equals(LAC) &&
                    ((string)dr["mnc"]).Equals(MNC) &&
                    ((string)dr["mcc"]).Equals(MCC) &&
                    (signal.Equals("") || ((string)dr["signal"]).Equals(signal)))
                {
                    Debug.AddLog("FindRow: row exists");
                    return dr;
                }
            }

            Debug.AddLog("FindRow: row does not exists");
            return null;
        }

        public DataRow UpdateCellID(string cellID, string LAC, string MNC, string MCC, string lat,
                                    string lon, string newCellID, string alias, string highSignal, 
                                    string lowSignal, string maxSignal, string minSignal, string signal,
                                    string newCellDB) 
		{
            DataRow myRow = null;

            try
            {
                Debug.AddLog("UpdateCellID: cellid=" + cellID + ", LAC=" + LAC + ", MNC=" + MNC + ", MCC=" + MCC +
                             ", LAT=" + lat + ", LON=" + lon);

                myRow = FindRow(cellID, LAC, MNC, MCC, signal); // dataTable.Rows[linha];

                if (myRow == null)
                {
                    AddCellID(cellID, LAC, MNC, MCC, lat, lon, alias,
                              highSignal, lowSignal, maxSignal, minSignal, signal);
                }
                else
                {
                    if (!lat.Equals("") && !lon.Equals(""))
                    {
                        myRow["new"] = newCellID;
                        myRow["newcelldb"] = newCellDB;
                        myRow["cellid"] = cellID;
                        myRow["lac"] = LAC;
                        myRow["mnc"] = MNC;
                        myRow["mcc"] = MCC;
                        myRow["lat"] = lat;
                        myRow["slat"] =
                          (((string)myRow["lat"]).Length > 9 ? ((string)myRow["lat"]).Substring(0, 9) : (string)myRow["lat"]);
                        myRow["lon"] = lon;
                        myRow["slon"] =
                          (((string)myRow["lon"]).Length > 9 ? ((string)myRow["lon"]).Substring(0, 9) : (string)myRow["lon"]);
                        myRow["highSignal"] = highSignal;
                        myRow["lowSignal"] = lowSignal;
                        myRow["maxSignal"] = maxSignal;
                        myRow["minSignal"] = minSignal;
                        myRow["signal"] = signal;
                        DataTable.AcceptChanges();

                        SqlCeCommand command = DB.SQLDataBase.SqlCeConnection.CreateCommand();

                        command.CommandText =
                            "update cellid " +
                            "   set new = '" + newCellID + "', " +
                            "       newcelldb = '" + newCellDB + "', " +
                            "       lat = '" + (string)myRow["lat"] + "', " +
                            "       slat= '" + (string)myRow["slat"] + "', " +
                            "       lon = '" + (string)myRow["lon"] + "', " +
                            "       slon= '" + (string)myRow["slon"] + "', " +
                            "       alias = '" + alias + "' " +
                            (newCellID.Equals("N") ? ", dtsent = getdate()" : "") +
                            " where cellid = '" + (string)myRow["cellid"] + "'" +
                            "   and lac = '" + (string)myRow["lac"] + "'" +
                            "   and mnc = '" + (string)myRow["mnc"] + "'" +
                            "   and mcc = '" + (string)myRow["mcc"] + "'";

                        DB.SQLDataBase.ExecSQL(command);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.AddLog("UpdateCellID: Error while updating a new cell: " + e.Message);
                MessageBox.Show("Error while updating a cell: " + e.Message);
                return null;
            }

            return myRow;
		}

        public void DelCellID(string cellID, string LAC, string MNC, string MCC, string signal) 
		{
            Debug.AddLog("DelCellID: cellid=" + cellID + ", LAC=" + LAC + ", MNC=" + MNC + ", MCC=" + MCC + ", Signal=" + signal);
            
            DataRow myRow;
            myRow = FindRow(cellID, LAC, MNC, MCC, signal); // dataTable.Rows[linha];

            if (myRow != null)
            {   
                string sql = "delete from cellid " +
                             " where cellid = '" + (string)myRow["cellid"] + "'" +
                             "   and lac = '" + (string)myRow["lac"] + "'" +
                             "   and mnc = '" + (string)myRow["mnc"] + "'" +
                             "   and mcc = '" + (string)myRow["mcc"] + "'" +
                             "   and signal = '" + (string)myRow["signal"] + "'";

                DB.SQLDataBase.ExecSQL(sql);

                myRow.Delete();
                DataTable.AcceptChanges();
                total--;
            }
		}

        public override void DeleteAll()
        {
            base.DeleteAll();

            total = 0;
        }

        public void SelectAll(bool onlyNew) 
		{
            if (onlyNew)
            {
                base.SelectAll("new = 'Y' or (newcelldb = 'Y' and signal <> 0)");
            }
            else
            {
                base.SelectAll();
            }

            if (!onlyNew)
            {
                total = DataTable.Rows.Count;
            }
            else
            {
                string sql = "select count(*) as result from cellid";

                SqlCeDataReader myReader = null;
                try
                {
                    DB.SQLDataBase.OpenSQL(sql, out myReader);
                }
                catch (Exception e)
                {
                    Debug.AddLog("SelectALL: Error while opening cells from database: " + e.Message);
                    MessageBox.Show("Error while opening cells from database: " + e.Message);
                    myReader = null;
                }

                if (myReader != null)
                {
                    if (myReader.Read())
                    {
                        total = myReader.GetInt32(0);
                        myReader.Close();
                    }
                }
            }
        }
	}
}
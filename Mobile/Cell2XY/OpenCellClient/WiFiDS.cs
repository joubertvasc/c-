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
    public class WiFiDS: SQLServerCEDataSet
    {
        private int total;

        public int Total
        {
            get { return total; }
        }

        public bool ExistsWiFi(string macAddress, int signal)
        {
            int result = 0;
            Debug.AddLog("ExistsEifi: mac=" + macAddress);

            DataRow myRow;
            myRow = FindRow(macAddress, signal); 

            if (myRow == null)
            {
                string sql = "select count(*) as result" +
                             "  from wifi " +
                             " where mac = '" + macAddress + "'";

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

        public DataRow AddWiFi(Radio.AccessPointAttributes apAttr, string lat, string lon, string alias) 
		{
            Debug.AddLog("AddWiFi: mac=" + apAttr.macAddress + ", LAT=" + lat + ", LON=" + lon);
            
            DataRow myRow;
			myRow = DataTable.NewRow();
            myRow["new"] = "Y";
            myRow["mac"] = apAttr.macAddress;
            myRow["name"] = apAttr.name;
            myRow["signal"] = Math.Abs(apAttr.signal);
            myRow["lat"] = "";
            myRow["slat"] = "";
            myRow["lon"] = "";
            myRow["slon"] = "";

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

			DataSet.Tables [0].Rows.Add (myRow);
			DataTable.AcceptChanges();

            String sql =
                "insert into wifi (name, mac, strength, inframode, networktype, privacystr, privacy, signal, " +
                "                  lat, lon, slat, slon, new, alias) " +
                " values ('" + apAttr.name + "', '" +
                          apAttr.macAddress + "', '" +
                          apAttr.strength + "', '" +
                          apAttr.infrastructureMode + "', '" +
                          apAttr.networkTypeInUse + "', '" +
                          Radio.getPrivacy (apAttr.privacy) + "', " +
                          System.Convert.ToString (apAttr.privacy) + ", " +
                          System.Convert.ToString (Math.Abs(apAttr.signal)) + ", '" +
                          lat + "', '" +
                          lon + "', '" +
                          (lat.Equals("") ? "" : (string)myRow["slat"]) + "', '" +
                          (lon.Equals("") ? "" : (string)myRow["slon"]) + "', 'Y', '" +
                          alias + "')";

            DB.SQLDataBase.ExecSQL(sql);

            total++;

            return myRow;
		}

        public DataRow FindRow(string MAC, int signal)
        {
            Debug.AddLog("FindRow: mac=" + MAC);

            DataRow dr;
            for (int i = 0; i < DataTable.Rows.Count; i++)
            {
                dr = DataTable.Rows[i];

                if (((string)dr["mac"]).Equals(MAC))
                {
                    Debug.AddLog("FindRow: row exists");
                    return dr;
                }
            }

            Debug.AddLog("FindRow: row does not exists");
            return null;
        }

        public DataRow UpdateWiFi(Radio.AccessPointAttributes apAttr, string lat, string lon, string newWiFi, string alias) 
		{
            Debug.AddLog("UpdateMAC: mac=" + apAttr.macAddress + 
                         ", signal=" + System.Convert.ToString(apAttr.signal) +
                         ", LAT=" + lat + ", LON=" + lon);
            
            DataRow myRow;
            myRow = FindRow(apAttr.macAddress, apAttr.signal); // dataTable.Rows[linha];

            if (myRow == null)
            {
                AddWiFi(apAttr, lat, lon, alias);
            }
            else
            {
                if (!lat.Equals("") && !lon.Equals(""))
                {
                    string oldValue = (string)myRow["new"];

                    myRow["new"] = newWiFi;
                    myRow["mac"] = apAttr.macAddress;
                    myRow["signal"] = Math.Abs(apAttr.signal);
                    myRow["lat"] = lat;
                    myRow["slat"] =
                      (((string)myRow["lat"]).Length > 9 ? ((string)myRow["lat"]).Substring(0, 9) : (string)myRow["lat"]);
                    myRow["lon"] = lon;
                    myRow["slon"] = 
                      (((string)myRow["lon"]).Length > 9 ? ((string)myRow["lon"]).Substring(0, 9) : (string)myRow["lon"]);
                    DataTable.AcceptChanges();

                    SqlCeCommand command = DB.SQLDataBase.SqlCeConnection.CreateCommand();

                    command.CommandText =
                        "update wifi " +
                        "   set new = '" + newWiFi + "', " +
                        "       lat = '" + (string)myRow["lat"] + "', " +
                        "       slat= '" + (string)myRow["slat"] + "', " +
                        "       lon = '" + (string)myRow["lon"] + "', " +
                        "       slon= '" + (string)myRow["slon"] + "', " +

                        "       name= '" + apAttr.name + "', " +
                        "       strength= '" + apAttr.strength + "', " +
                        "       inframode= '" + apAttr.infrastructureMode + "', " +
                        "       networktype= '" + apAttr.networkTypeInUse + "', " +
                        "       privacystr= '" + Radio.getPrivacy(apAttr.privacy) + "', " +
                        "       privacy= " + System.Convert.ToString(apAttr.privacy) + ", " +
                        "       signal= " + System.Convert.ToString(Math.Abs(apAttr.signal)) + ", " +
                        "       alias = '" + alias + "' " +
                        (newWiFi.Equals("N") ? ", dtsent = getdate()" : "") +
                        " where mac = '" + apAttr.macAddress + "'";

                    DB.SQLDataBase.ExecSQL(command);
                }
            }

            return myRow;
		}

        public void DelWiFi(string MAC, int signal) 
		{
            Debug.AddLog("DelWiFi: mac=" + MAC);
            
            DataRow myRow;
            myRow = FindRow(MAC, signal);

            if (myRow != null)
            {   
                string sql = "delete from wifi " +
                             " where mac = '" + (string)myRow["mac"] + "'";

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
                base.SelectAll(" new = 'Y' ");
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
                string sql = "select count(*) as result from wifi";

                SqlCeDataReader myReader = null;
                DB.SQLDataBase.OpenSQL(sql, out myReader);

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

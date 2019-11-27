using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JVUtils;

// DEPRECATED!

namespace OpenCellClient
{
    public class CellIdDataSet
    {
        private static DataTable dataTable;
        private static DataSet dataSet;
        private static int total;

        public static DataSet DataSet
        {
            get
            {
                return dataSet;
            }
        }
        public static DataTable DataTable
        {
            get
            {
                return dataTable;
            }
        }
        public static int Total
        {
            get { return total; }
        }

        public static void CreateDataSet()
        {
            if (dataSet == null)
            {
                dataSet = new DataSet();
            }

            if (dataTable == null)
            {
                dataTable = dataSet.Tables.Add("CellID");

                dataTable.Columns.Add("cellid", System.Type.GetType("System.String"));
                dataTable.Columns.Add("lac", System.Type.GetType("System.String"));
                dataTable.Columns.Add("mnc", System.Type.GetType("System.String"));
                dataTable.Columns.Add("mcc", System.Type.GetType("System.String"));
                dataTable.Columns.Add("lat", System.Type.GetType("System.String"));
                dataTable.Columns.Add("lon", System.Type.GetType("System.String"));
                dataTable.Columns.Add("slat", System.Type.GetType("System.String"));
                dataTable.Columns.Add("slon", System.Type.GetType("System.String"));
                dataTable.Columns.Add("new", System.Type.GetType("System.String"));
            }

        }

        public static void LoadDataSet(string xmlFileName)
        {
            if (dataSet != null && File.Exists(xmlFileName))
            {
                dataSet.ReadXml(xmlFileName);
                SetShortCoordinates();
                total = CountNew();
            }
        }

        public static void SaveDataSet(string xmlFileName)
        {
            if (dataSet != null && !xmlFileName.Equals(""))
            {
                dataSet.WriteXml(xmlFileName);
            }
        }

        public static void UpdateDataGrid(DataGrid dg, string xmlFileName)
        {
            CreateDataSet();

            DataGridTableStyle DGStyle = new DataGridTableStyle();
            DataGridTextBoxColumn textColumn;

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "new";
            textColumn.HeaderText = "New";
            textColumn.Width = 60;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "cellid";
            textColumn.HeaderText = "CellID";
            textColumn.Width = 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "lac";
            textColumn.HeaderText = "LAC";
            textColumn.Width = 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "mnc";
            textColumn.HeaderText = "MNC";
            textColumn.Width = 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "mcc";
            textColumn.HeaderText = "MCC";
            textColumn.Width = 100;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "slat";
            textColumn.HeaderText = "Latitude";
            textColumn.Width = 150;
            DGStyle.GridColumnStyles.Add(textColumn);

            textColumn = new DataGridTextBoxColumn();
            textColumn.MappingName = "slon";
            textColumn.HeaderText = "Longitude";
            textColumn.Width = 150;
            DGStyle.GridColumnStyles.Add(textColumn);

            DGStyle.MappingName = "CellID";

            // The Clear() method is called to ensure that
            // the previous style is removed.
            dg.TableStyles.Clear();

            // Add the new DataGridTableStyle collection to
            // the TableStyles collection
            dg.TableStyles.Add(DGStyle);

            dg.DataSource = dataTable;

            if (!xmlFileName.Equals(""))
            {
                LoadDataSet(xmlFileName);
            }
        }

        public static DataRow FindByMacCellID(string cellID) 
        {
            DataRow result = null;
            DataRow row;
            
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = dataTable.Rows [i];
                if (row["cellid"].ToString().Equals(cellID))
                {
                    result = row;
                    break;
                }
            }

            return result;
        }

        public static int CountNew()
        {
            DataRow row;
            int totNew = 0;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = dataTable.Rows[i];
                if (row["new"].ToString().Equals("Y"))
                {
                    totNew++;
                }
            }

            return totNew;
        }

        public static void UpdateRow(DataRow rw)
        {
            DataRow row = FindByMacCellID((string)rw["cellid"]);
            if (row != null)
            {
                row["cellid"] = rw["cellid"];
                row["lac"] = rw["lac"];
                row["mnc"] = rw["mnc"];
                row["mcc"] = rw["mcc"];
                row["lat"] = rw["lat"];
                row["lon"] = rw["lon"];
                row["slat"] = rw["slat"];
                row["slon"] = rw["slon"];
                row["new"] = rw["new"];

                dataTable.AcceptChanges();
            }
        }

        public static DataRow UpdateCoordinates(string cellID, string longLatitude, string longLongitude)
        {
            DataRow row = FindByMacCellID(cellID);

            if (row != null)
            {
                if (((string)row["new"]).Equals("N"))
                {
                    total++;
                }

                row["new"] = "Y";
                row["lat"] = longLatitude;
                row["lon"] = longLongitude;
                row["slat"] = (longLatitude.Length > 9 ? longLatitude.Substring(0, 9) : longLatitude);
                row["slon"] = (longLongitude.Length > 9 ? longLongitude.Substring(0, 9) : longLongitude);
                dataTable.AcceptChanges();
            }

            return row;
        }

        public static void SetAllToOld()
        {
            DataRow dr;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dr = dataTable.Rows[i];
                dr["new"] = "N";
                dataTable.AcceptChanges();
            }

            total = 0;
        }

        public static DataRow AddCellID(CellIDInformations cid, string longLatitude, string longLongitude)
        {
            DataRow row;
            row = dataTable.NewRow();

            row["new"] = "Y";
            row["cellid"] = cid.cellID;
            row["lac"] = cid.localAreaCode;
            row["mnc"] = cid.mobileNetworkCode;
            row["mcc"] = cid.mobileCountryCode;
            row["lat"] = longLatitude;
            row["lon"] = longLongitude;
            row["slat"] = (longLatitude.Length > 9 ? longLatitude.Substring(0, 9) : longLatitude);
            row["slon"] = (longLongitude.Length > 9 ? longLongitude.Substring(0, 9) : longLongitude);

            dataTable.Rows.Add(row);

            total++;

            return row;
        }

        private static void SetShortCoordinates()
        {
            DataRow dr;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dr = dataTable.Rows[i];
                dr["slat"] = "";
                dr["slon"] = "";

                if (!((string)dr["lat"]).Equals(""))
                {
                    dr["slat"] = (((string)dr["lat"]).Length > 9 ? ((string)dr["lat"]).Substring(0, 9) : dr["lat"]);
                    dr["slon"] = (((string)dr["lon"]).Length > 9 ? ((string)dr["lon"]).Substring(0, 9) : dr["lon"]);
                    dataTable.AcceptChanges();
                }
            }
        }
    }
}
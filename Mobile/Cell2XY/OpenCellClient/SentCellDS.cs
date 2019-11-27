using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace OpenCellClient
{
    public class SentCellDS
    {
        private static DataTable dataTable;
        private static DataSet dataSet;

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

        public static void CreateDataSet()
        {
            if (dataSet == null)
            {
                dataSet = new DataSet();
            }

            if (dataTable == null)
            {
                dataTable = dataSet.Tables.Add("Sent");

                dataTable.Columns.Add("id", System.Type.GetType("System.String"));
                dataTable.Columns.Add("lat", System.Type.GetType("System.String"));
                dataTable.Columns.Add("lon", System.Type.GetType("System.String"));
            }
        }

        public static void XMLParser(string fileName)
        {
            string id;
            string lat;
            string lon;
            DataRow row;

            if (dataSet == null)
                CreateDataSet();

            System.IO.FileStream fsReadXml = new System.IO.FileStream (fileName, System.IO.FileMode.Open);

            // Create an XmlTextReader to read the file.            
            System.Xml.XmlTextReader xmlReader = new System.Xml.XmlTextReader(fsReadXml);

            //Read the attributes on the root element.
            xmlReader.MoveToContent();
            // Parse the file and display each of the nodes.
            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xmlReader.HasAttributes)
                        {
                            id = "";
                            lat = "";
                            lon = "";

                            while (xmlReader.MoveToNextAttribute())
                            {
                                if (xmlReader.Name.Equals("id"))
                                    id = xmlReader.Value;

                                if (xmlReader.Name.Equals("lat"))
                                    lat = xmlReader.Value;
                                
                                if (xmlReader.Name.Equals("lon"))
                                    lon = xmlReader.Value;
                            }

                            if (!id.Equals("") && !lat.Equals("") && !lon.Equals(""))
                            {
                                row = dataTable.NewRow();
                                row["id"] = id;
                                row["lat"] = lat;
                                row["lon"] = lon;
                                dataTable.Rows.Add(row);
                            }
                        }
                        break;
                }
            }

            fsReadXml.Close();
            xmlReader.Close();
        }
    }
}

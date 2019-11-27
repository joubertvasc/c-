using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Data;
using System.Windows;
using System.Windows.Forms;

namespace RTRegCreator
{

    public class Languages
    {
        DataSet ds;

        public Languages()
        {
            ds = new DataSet();
        }

        public DataSet dataSet
        {
            get { return ds; }
        }

        public int count
        {
            get { return ds.Tables[0].Rows.Count; }
        }
        public DataRow row(int index)
        {
            return ds.Tables[0].Rows[index];
        }
        public int code(int index)
        {
            if (index <= count) {
              DataRow dataRow = ds.Tables[0].Rows[index];
              return System.Convert.ToInt32(dataRow["code"]);
            } else { 
              return -1; 
            }
        }
        public string name(int index)
        {
            if (index <= count)
            {
                DataRow dataRow = ds.Tables[0].Rows[index];
                return dataRow["name"].ToString();
            }
            else
            {
                return "";
            }
        }
        public string fileName(int index)
        {
            if (index <= count)
            {
                DataRow dataRow = ds.Tables[0].Rows[index];
                return dataRow["file"].ToString();
            }
            else
            {
                return "";
            }
        }
        public string helpFileName(int index)
        {
            if (index <= count)
            {
                DataRow dataRow = ds.Tables[0].Rows[index];
                return dataRow["help"].ToString();
            }
            else
            {
                return "";
            }
        }

        public bool LoadLanguages(string appPath)
        {
            ds = new DataSet();
            try
            {
                ds.ReadXml(appPath + "languages.xml");
            } catch
            {
//                MessageBox.Show("Invalid or missing language package. Using English.");
                return false;
            }

            return true;
        }
    }
}

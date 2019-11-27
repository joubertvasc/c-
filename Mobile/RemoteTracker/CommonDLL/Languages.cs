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
using JVUtils;

namespace CommonDLL
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
            get { return (ds.Tables.Count == 0 ? 0 : ds.Tables[0].Rows.Count); }
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
            JVUtils.Debug.AddLog("Languages.language: Index = " + System.Convert.ToString(index), true);

            if (index >= count)
                index = 0;

            if (index < count && index >= 0 && ds.Tables.Count > 0)
            {
                DataRow dataRow = ds.Tables[0].Rows[index];
                JVUtils.Debug.AddLog("Languages.language: " + dataRow["file"].ToString(), true);
                return dataRow["file"].ToString();
            }
            else
            {
                JVUtils.Debug.AddLog("Languages.language: The index does not exists.", true);
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
            JVUtils.Debug.AddLog("LoadLanguage: before load language. Path=" + appPath + "languages.xml", true);
            ds = new DataSet();

            if (!File.Exists(appPath + "languages.xml"))
                return false;

            try
            {
                ds.ReadXml(appPath + "languages.xml");
                JVUtils.Debug.AddLog("LoadLanguage: after load language.", true);
            }
            catch (Exception e)
            {
                JVUtils.Debug.AddLog("LoadLanguage: error: " + Utils.GetOnlyErrorMessage(e.Message), true);
                return false;
            }

            return true;
        }
    }
}

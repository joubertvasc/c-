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
    public class LanguageXML
    {
        DataSet ds;

        public LanguageXML()
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
        
        public bool LoadLanguageXML(string XMLFile)
        {
            ds = new DataSet();
            try
            {
                JVUtils.Debug.AddLog("LoadLanguageXML: File: " + XMLFile, true);
                ds.ReadXml(XMLFile);
            }
            catch (Exception ex)
            {
                JVUtils.Debug.AddLog("LoadLanguageXML: Error: " + Utils.GetOnlyErrorMessage(ex.Message), true);
                MessageBox.Show("Could not read the file '" + XMLFile + "'. Error: " + ex.ToString(), 
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }

            return true;
        }

        public string getColumn(string column, string defaultColumn)
        {
            string result;
            try
            {
                DataRow row = this.row(0);
                result = row[column].ToString();
            }
            catch
            {
                result = defaultColumn;
            }

            return result;
        }

    }
}

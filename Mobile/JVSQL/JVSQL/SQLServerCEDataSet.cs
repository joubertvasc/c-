using System;
using System.Data;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Data.SqlServerCe;
using System.Windows.Forms;
using JVUtils;

namespace JVSQL
{
    public class SQLServerCEDataSet
    {
        #region Internal variables
        private DataTable dataTable;
        private DataSet dataSet;
        private DataBase db;
        private string tableName;
        #endregion

        #region Public properties
        public DataSet DataSet
        {
            get
            {
                return dataSet;
            }
        }
        public DataTable DataTable
        {
            get
            {
                return dataTable;
            }
        }
        public DataBase DB
        {
            get { return db; }
            set { db = value; }
        }
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }
        #endregion

        #region Public declarations
        public virtual void DeleteAll()
        {
            DeleteAll("");
        }

        public virtual void DeleteAll(string filter)
        {
            Debug.AddLog("DeleteAll: filter = " + filter);

            string sql = "delete from " + tableName + (filter.Trim().Equals("") ? "" : " where " + filter);

            db.SQLDataBase.ExecSQL(sql);

            if (dataTable != null)
            {
                dataTable.Rows.Clear();
                dataTable.AcceptChanges();
            }
        }

        public virtual void SelectAll()
        {
            SelectAll("", "");
        }

        public virtual void SelectAll(string filters)
        {
            SelectAll(filters, "");
        }

        public virtual void SelectAll(string filters, string orderBy)
        {
            String sql = "select * from " + tableName +
                (filters.Trim().Equals("") ? "" : " where " + filters) +
                (orderBy.Trim().Equals("") ? "" : " order by " + orderBy);

            db.SQLDataBase.OpenSQL(sql, out dataSet);

            if (dataSet != null)
            {
                dataTable = dataSet.Tables[0];
            }
        }
        #endregion
    }
}

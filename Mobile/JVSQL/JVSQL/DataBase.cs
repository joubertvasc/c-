using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using JVUtils;

namespace JVSQL
{
    public class DataBase
    {
        #region Internal Variables
        private SQLServerCEDataBase database = null;
        private string db = "";
        private string password = "";
        private int appVersion = 0;
        private int versionInDatabase;
        #endregion

        #region Public Properties
        public SQLServerCEDataBase SQLDataBase
        {
            get
            {
                return database;
            }
        }
        public int VersionInDataBase
        {
            get { return versionInDatabase; }
            set { versionInDatabase = value; }
        }
        public string DataBaseFileName
        {
            get { return db; }
        }
        #endregion

        #region Public declarations
        public DataBase()
        {
            Debug.AddLog("DataBase, creating object", true);
            database = new SQLServerCEDataBase();
        }

        ~DataBase()
        {
            if (database != null)
                database.CloseDatabase();
        }

        public bool OpenDataBase(string dbFileName, string dbPassword, string appVersionNumber)
        {
            db = dbFileName;
            password = dbPassword;
            appVersion = System.Convert.ToInt32(Utils.RemoveChar(Utils.RemoveChar(appVersionNumber, '-'), '.'));
            bool result = false;

            Debug.AddLog("OpenDataBase: object is valid? " + (database != null ? "Yes" : "No"), true);
            if (database != null)
            {
                if (database.OpenDatabase(db, password))
                {
                    if (!VerifyDataBase())
                    {
                        MessageBox.Show("Could not open/create database file " + db, "Error");
                        CloseDataBase();
                    }
                    else
                    {
                        result = true;
                    }
                }
            }

            Debug.AddLog("OpenDataBase: opened? " + (result ? "Yes" : "No"), true);
            return result;
        }

        public void CloseDataBase()
        {
            if (database != null)
            {
                database.CloseDatabase();
                database = null;
            }
        }

        public bool TableExists(string tableName)
        {
            return database.TableExists(tableName);
        }

        public virtual bool VerifyTables()
        {
            // VIRTUAL!
            return true;
        }

        public bool UpdateVersion(int newVersion)
        {
            SqlCeDataReader myReader = null;
            database.OpenSQL("select * from config", out myReader);

            if (myReader != null)
            {
                if (myReader.Read())
                {
                    myReader.Close();

                    if (database.ExecSQL(
                        "update config set nuversion ='" + System.Convert.ToString(newVersion) + "'") == -1)
                    {
                        return false;
                    }
                }
                else
                {
                    if (database.ExecSQL(
                        "insert into config (nuversion) values ('" + System.Convert.ToString(newVersion) + "')") == -1)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            versionInDatabase = newVersion;
            return true;
        }
        #endregion

        #region Private declarations
        private bool VerifyDataBase()
        {
            if (database != null)
            {
                if (!VerifyInternalTables())
                {
                    return false;
                }
                else
                {
                    return true;
                };
            }
            else
            {
                return false;
            }
        }

        private bool VerifyInternalTables()
        {
            // Verify if there is a config table. If don't, we need to create database.
            if (!database.TableExists("config"))
            {
                // Create config table
                if (database.ExecSQL("create table config (nuversion integer not null)") == -1)
                {
                    return false;
                }

                // Insert an empty version.
                if (!UpdateVersion(0))
                {
                    return false;
                }
            }
            else
            {   // If the table exists, get the stored version
                SqlCeDataReader myReader = null;
                database.OpenSQL("select * from config", out myReader);

                if (myReader != null)
                {
                    if (myReader.Read())
                    {
                        try
                        {
                            versionInDatabase = myReader.GetInt32(0);
                        }
                        catch
                        {
                            versionInDatabase = 0;
                        }

                        myReader.Close();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            ////////////////////////////////////////////
            // Verify if the database must be updated //
            ////////////////////////////////////////////
            return VerifyTables();
        }
        #endregion
    }
}

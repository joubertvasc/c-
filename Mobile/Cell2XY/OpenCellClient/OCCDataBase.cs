using System;
using System.Data.SqlServerCe;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using JVUtils;
using JVSQL;

namespace OpenCellClient
{
    public class OCCDataBase: JVSQL.DataBase
    {
        private string db = "\\Application Data\\Cell2XY\\cell2xy.sdf";
        private string password = "cell2xy";

        public bool OpenDataBase(string version)
        {
            return base.OpenDataBase(db, password, version);
        }

        public override bool VerifyTables()
        {
            ////////////////////////////////////////////
            // Verify if the database must be updated //
            ////////////////////////////////////////////

            if (base.VerifyTables())
            {
                // Verify if version is before 0030
                if (VersionInDataBase < 30)
                {
                    if (!VerifyVersion0001())
                    {
                        return false;
                    }
                    else
                    {
                        if (!UpdateVersion(30))
                        {
                            return false;
                        }
                    }
                }

                // Verify if version is before 0110
                if (VersionInDataBase < 110)
                {
                    if (!VerifyVersion0110())
                    {
                        return false;
                    }
                    else
                    {
                        if (!UpdateVersion(110))
                        {
                            return false;
                        }
                    }
                }

                // Verify if version is before 0200
                if (VersionInDataBase < 200)
                {
                    if (!VerifyVersion0200())
                    {
                        return false;
                    }
                    else
                    {
                        if (!UpdateVersion(200))
                        {
                            return false;
                        }
                    }
                }

                // Verify if version is before 0300
                if (VersionInDataBase < 300)
                {
                    if (!VerifyVersion0300())
                    {
                        return false;
                    }
                    else
                    {
                        if (!UpdateVersion(300))
                        {
                            return false;
                        }
                    }
                }
                // Put new codes here for new versions
                return true;
            }
            else
            {
                return false;
            }
        }

        // Create tables, if they don't exists.
        private Boolean VerifyVersion0001()
        {
            if (!SQLDataBase.CreateTableIfNotExists("cellid",
                "create table cellid (cellid nvarchar(10) not null, " +
                "                     lac nvarchar(10) not null, " +
                "                     mnc nchar(2) not null, " +
                "                     mcc nchar(3) not null, " +
                "                     new nchar (1) not null, " +
                "                     lat nvarchar(20), " +
                "                     lon nvarchar(20), " +
                "                     slat nvarchar(10), " +
                "                     slon nvarchar(10), " +
                "        primary key (cellid, lac, mnc, mcc))"))
            {
                return false;
            }

            return true;
        }

        private Boolean VerifyVersion0110()
        {
            // Drop the old primary key
            string sql = "select constraint_name FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS" +
                         " where constraint_name like '%cellid%'";

            SqlCeDataReader myReader = null;
            SQLDataBase.OpenSQL(sql, out myReader);

            if (myReader != null)
            {
                if (myReader.Read())
                {
                    string result = myReader.GetString(0);
                    myReader.Close();

                    if (!result.Equals(""))
                    {
                        sql = "alter table cellid drop constraint " + result;
                        SQLDataBase.ExecSQL(sql);
                    }
                }
            }

            // Create new columns
            sql = "alter table cellid " +
                  "  add id bigint identity(1,1) not null, " +
                  "      dtsaved datetime not null default getdate(), " +
                  "      dtsent datetime, " +
                  "      alias nvarchar (30)";
            SQLDataBase.ExecSQL(sql);

            // Set today as a date for sent cells
            sql = "update cellid set dtsent = getdate() where new = 'N'";
            SQLDataBase.ExecSQL(sql);

            // Create indexes
            sql = "alter table cellid add constraint PK_CELLID primary key (id)";
            SQLDataBase.ExecSQL(sql);

            sql = "create index IDX_CELLID on cellid (cellid, lac, mnc, mcc)";
            SQLDataBase.ExecSQL(sql);

            return true;
        }

        private Boolean VerifyVersion0200()
        {
            string sql = 
                "create table wifi (id bigint identity(1,1) not null, " +
                "                   new nchar (1) not null, " +
                "                   name nvarchar (250) not null, " +
                "                   mac nchar(17) not null, " +
                "                   strength nvarchar (20) not null, " +
                "                   inframode nvarchar (20) not null, " +
                "                   networktype nvarchar (20) not null, " +
                "                   privacystr nvarchar (20) not null, " +
                "                   privacy int not null, " +
                "                   signal int not null, " +
                "                   dtsaved datetime not null default getdate(), " +
                "                   dtsent datetime, " +
                "                   lat nvarchar(20), " +
                "                   lon nvarchar(20), " +
                "                   slat nvarchar(10), " +
                "                   slon nvarchar(10), " +
                "                   alias nvarchar (30))";
            SQLDataBase.ExecSQL(sql);

            // Create indexes
            sql = "alter table wifi add constraint PK_WIFI primary key (id)";
            SQLDataBase.ExecSQL(sql);

            sql = "create index IDX_WIFI on wifi (mac)";
            SQLDataBase.ExecSQL(sql);

            // Change MNC to three digits. Because it's part of an index, first drop the index...
            sql = "drop index cellid.IDX_CELLID";
            SQLDataBase.ExecSQL(sql);

            // ...change the size...
            sql = "alter table cellid alter column mnc nvarchar(3) not null";
            SQLDataBase.ExecSQL(sql);

            // ...and create the index again.
            sql = "create index IDX_CELLID on cellid (cellid, lac, mnc, mcc)";
            SQLDataBase.ExecSQL(sql);

            return true;
        }

        private Boolean VerifyVersion0300()
        {
            // Create new columns
            string sql = "alter table cellid " +
                         "  add newcelldb nchar(1) default 'Y', " +
                         "      highsignal nvarchar (10) default 0, " +
                         "      lowsignal nvarchar (10) default 0, " +
                         "      maxsignal nvarchar (10) default 0, " +
                         "      minsignal nvarchar (10) default 0, " +
                         "      signal nvarchar (10) default 0";
            SQLDataBase.ExecSQL(sql);

            sql = "create index IDX_CELLID2 on cellid (cellid, lac, mnc, mcc, signal)";
            SQLDataBase.ExecSQL(sql);

            sql = "update wifi set signal = -signal where signal < 0";
            SQLDataBase.ExecSQL(sql);

            return true;
        }

    }
}

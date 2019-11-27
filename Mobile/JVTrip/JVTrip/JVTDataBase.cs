using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JVSQL;
using JVUtils;

namespace JVTrip
{
    public class JVTDataBase: JVSQL.DataBase
    {
        private string db = "\\Application Data\\JVTrip\\jvtrip.sdf";
        private string password = "jvtrip";

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
                // Verify if version is before 0100
                if (VersionInDataBase < 100)
                {
                    if (!VerifyVersion0100())
                    {
                        return false;
                    }
                    else
                    {
                        if (!UpdateVersion(100))
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
        private Boolean VerifyVersion0100()
        {
            // Creating main tables

            try
            {
                // Table Travel
                string sql =
                    "create table trip (id bigint identity(1,1) not null, " +
                    "                   nmtrip nvarchar (20) not null, " +
                    "                   nmstart nvarchar (250) not null, " +
                    "                   nmdestiny nvarchar(250) not null, " +
                    "                   dtcreated datetime not null default getdate(), " +
                    "                   dtstart datetime, " +
                    "                   dtend datetime, " +
                    "                   blnotes nvarchar (2000))";
                SQLDataBase.ExecSQL(sql);

                // Table Coordinates
                sql =
                    "create table coordinates (id bigint not null, " +
                    "                          coordinate bigint identity(1,1) not null, " +
                    "                          latitude float not null, " +
                    "                          longitude float not null, " +
                    "                          altitude float not null, " +
                    "                          speed float not null)";
                SQLDataBase.ExecSQL(sql);

                // Table Notes
                sql =
                    "create table notes (id bigint not null, " +
                    "                    note int identity(1,1) not null, " +
                    "                    denote nvarchar (250) not null, " +
                    "                    dtcreated datetime not null default getdate(), " +
                    "                    latitude float, " +
                    "                    longitude float)";
                SQLDataBase.ExecSQL(sql);

                // Table Costs
                sql =
                    "create table costs (id bigint not null, " +
                    "                    cost int identity(1,1) not null, " +
                    "                    decost nvarchar (250) not null, " +
                    "                    vlcost float not null, " +
                    "                    dtcreated datetime not null default getdate(), " +
                    "                    latitude float, " +
                    "                    longitude float)";
                SQLDataBase.ExecSQL(sql);

                // Table Pictures
                sql =
                    "create table pictures (id bigint not null, " +
                    "                       picture int identity(1,1) not null, " +
                    "                       depicture nvarchar (250) not null, " +
                    "                       depathpicture nvarchar (250) not null, " +
                    "                       dtcreated datetime not null default getdate(), " +
                    "                       latitude float, " +
                    "                       longitude float)";
                SQLDataBase.ExecSQL(sql);

                // Create indexes
                sql = "alter table trip add constraint PK_TRIP primary key (id)";
                SQLDataBase.ExecSQL(sql);

                sql = "alter table coordinates add constraint PK_COORDS primary key (id, coordinate)";
                SQLDataBase.ExecSQL(sql);

                sql = "alter table notes add constraint PK_NOTES primary key (id, note)";
                SQLDataBase.ExecSQL(sql);

                sql = "alter table costs add constraint PK_COSTS primary key (id, cost)";
                SQLDataBase.ExecSQL(sql);

                sql = "alter table pictures add constraint PK_PICS primary key (id, picture)";
                SQLDataBase.ExecSQL(sql);

                return true;
            }
            catch (Exception e)
            {
                Debug.AddLog("VerifyVersion0100: " + e.Message);
                return false;
            }
        }
    }
}

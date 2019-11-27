﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.3705.6018
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace MyDUMeter {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class Log : DataSet {
        
        private RateLogDataTable tableRateLog;
        
        public Log() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected Log(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["RateLog"] != null)) {
                    this.Tables.Add(new RateLogDataTable(ds.Tables["RateLog"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.InitClass();
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public RateLogDataTable RateLog {
            get {
                return this.tableRateLog;
            }
        }
        
        public override DataSet Clone() {
            Log cln = ((Log)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        protected override void ReadXmlSerializable(XmlReader reader) {
            this.Reset();
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            if ((ds.Tables["RateLog"] != null)) {
                this.Tables.Add(new RateLogDataTable(ds.Tables["RateLog"]));
            }
            this.DataSetName = ds.DataSetName;
            this.Prefix = ds.Prefix;
            this.Namespace = ds.Namespace;
            this.Locale = ds.Locale;
            this.CaseSensitive = ds.CaseSensitive;
            this.EnforceConstraints = ds.EnforceConstraints;
            this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
        
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new XmlTextReader(stream), null);
        }
        
        internal void InitVars() {
            this.tableRateLog = ((RateLogDataTable)(this.Tables["RateLog"]));
            if ((this.tableRateLog != null)) {
                this.tableRateLog.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "Log";
            this.Prefix = "";
            this.Namespace = "http://www.tempuri.org/Log.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-ZA");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableRateLog = new RateLogDataTable();
            this.Tables.Add(this.tableRateLog);
        }
        
        private bool ShouldSerializeRateLog() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void RateLogRowChangeEventHandler(object sender, RateLogRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class RateLogDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnID;
            
            private DataColumn columnRecv;
            
            private DataColumn columnSend;
            
            private DataColumn columnDateDay;
            
            private DataColumn columnDateMonth;
            
            private DataColumn columnDateYear;
            
            private DataColumn columnTimeHour;
            
            private DataColumn columnTimeMinute;
            
            internal RateLogDataTable() : 
                    base("RateLog") {
                this.InitClass();
            }
            
            internal RateLogDataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn IDColumn {
                get {
                    return this.columnID;
                }
            }
            
            internal DataColumn RecvColumn {
                get {
                    return this.columnRecv;
                }
            }
            
            internal DataColumn SendColumn {
                get {
                    return this.columnSend;
                }
            }
            
            internal DataColumn DateDayColumn {
                get {
                    return this.columnDateDay;
                }
            }
            
            internal DataColumn DateMonthColumn {
                get {
                    return this.columnDateMonth;
                }
            }
            
            internal DataColumn DateYearColumn {
                get {
                    return this.columnDateYear;
                }
            }
            
            internal DataColumn TimeHourColumn {
                get {
                    return this.columnTimeHour;
                }
            }
            
            internal DataColumn TimeMinuteColumn {
                get {
                    return this.columnTimeMinute;
                }
            }
            
            public RateLogRow this[int index] {
                get {
                    return ((RateLogRow)(this.Rows[index]));
                }
            }
            
            public event RateLogRowChangeEventHandler RateLogRowChanged;
            
            public event RateLogRowChangeEventHandler RateLogRowChanging;
            
            public event RateLogRowChangeEventHandler RateLogRowDeleted;
            
            public event RateLogRowChangeEventHandler RateLogRowDeleting;
            
            public void AddRateLogRow(RateLogRow row) {
                this.Rows.Add(row);
            }
            
            public RateLogRow AddRateLogRow(System.DateTime ID, System.Single Recv, System.Single Send, System.Single DateDay, System.Single DateMonth, System.Single DateYear, System.Single TimeHour, System.Single TimeMinute) {
                RateLogRow rowRateLogRow = ((RateLogRow)(this.NewRow()));
                rowRateLogRow.ItemArray = new object[] {
                        ID,
                        Recv,
                        Send,
                        DateDay,
                        DateMonth,
                        DateYear,
                        TimeHour,
                        TimeMinute};
                this.Rows.Add(rowRateLogRow);
                return rowRateLogRow;
            }
            
            public RateLogRow FindByID(System.DateTime ID) {
                return ((RateLogRow)(this.Rows.Find(new object[] {
                            ID})));
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                RateLogDataTable cln = ((RateLogDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new RateLogDataTable();
            }
            
            internal void InitVars() {
                this.columnID = this.Columns["ID"];
                this.columnRecv = this.Columns["Recv"];
                this.columnSend = this.Columns["Send"];
                this.columnDateDay = this.Columns["DateDay"];
                this.columnDateMonth = this.Columns["DateMonth"];
                this.columnDateYear = this.Columns["DateYear"];
                this.columnTimeHour = this.Columns["TimeHour"];
                this.columnTimeMinute = this.Columns["TimeMinute"];
            }
            
            private void InitClass() {
                this.columnID = new DataColumn("ID", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnID);
                this.columnRecv = new DataColumn("Recv", typeof(System.Single), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnRecv);
                this.columnSend = new DataColumn("Send", typeof(System.Single), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnSend);
                this.columnDateDay = new DataColumn("DateDay", typeof(System.Single), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnDateDay);
                this.columnDateMonth = new DataColumn("DateMonth", typeof(System.Single), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnDateMonth);
                this.columnDateYear = new DataColumn("DateYear", typeof(System.Single), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnDateYear);
                this.columnTimeHour = new DataColumn("TimeHour", typeof(System.Single), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnTimeHour);
                this.columnTimeMinute = new DataColumn("TimeMinute", typeof(System.Single), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnTimeMinute);
                this.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[] {
                                this.columnID}, true));
                this.columnID.AllowDBNull = false;
                this.columnID.Unique = true;
            }
            
            public RateLogRow NewRateLogRow() {
                return ((RateLogRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new RateLogRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(RateLogRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.RateLogRowChanged != null)) {
                    this.RateLogRowChanged(this, new RateLogRowChangeEvent(((RateLogRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.RateLogRowChanging != null)) {
                    this.RateLogRowChanging(this, new RateLogRowChangeEvent(((RateLogRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.RateLogRowDeleted != null)) {
                    this.RateLogRowDeleted(this, new RateLogRowChangeEvent(((RateLogRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.RateLogRowDeleting != null)) {
                    this.RateLogRowDeleting(this, new RateLogRowChangeEvent(((RateLogRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveRateLogRow(RateLogRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class RateLogRow : DataRow {
            
            private RateLogDataTable tableRateLog;
            
            internal RateLogRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableRateLog = ((RateLogDataTable)(this.Table));
            }
            
            public System.DateTime ID {
                get {
                    return ((System.DateTime)(this[this.tableRateLog.IDColumn]));
                }
                set {
                    this[this.tableRateLog.IDColumn] = value;
                }
            }
            
            public System.Single Recv {
                get {
                    try {
                        return ((System.Single)(this[this.tableRateLog.RecvColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableRateLog.RecvColumn] = value;
                }
            }
            
            public System.Single Send {
                get {
                    try {
                        return ((System.Single)(this[this.tableRateLog.SendColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableRateLog.SendColumn] = value;
                }
            }
            
            public System.Single DateDay {
                get {
                    try {
                        return ((System.Single)(this[this.tableRateLog.DateDayColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableRateLog.DateDayColumn] = value;
                }
            }
            
            public System.Single DateMonth {
                get {
                    try {
                        return ((System.Single)(this[this.tableRateLog.DateMonthColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableRateLog.DateMonthColumn] = value;
                }
            }
            
            public System.Single DateYear {
                get {
                    try {
                        return ((System.Single)(this[this.tableRateLog.DateYearColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableRateLog.DateYearColumn] = value;
                }
            }
            
            public System.Single TimeHour {
                get {
                    try {
                        return ((System.Single)(this[this.tableRateLog.TimeHourColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableRateLog.TimeHourColumn] = value;
                }
            }
            
            public System.Single TimeMinute {
                get {
                    try {
                        return ((System.Single)(this[this.tableRateLog.TimeMinuteColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableRateLog.TimeMinuteColumn] = value;
                }
            }
            
            public bool IsRecvNull() {
                return this.IsNull(this.tableRateLog.RecvColumn);
            }
            
            public void SetRecvNull() {
                this[this.tableRateLog.RecvColumn] = System.Convert.DBNull;
            }
            
            public bool IsSendNull() {
                return this.IsNull(this.tableRateLog.SendColumn);
            }
            
            public void SetSendNull() {
                this[this.tableRateLog.SendColumn] = System.Convert.DBNull;
            }
            
            public bool IsDateDayNull() {
                return this.IsNull(this.tableRateLog.DateDayColumn);
            }
            
            public void SetDateDayNull() {
                this[this.tableRateLog.DateDayColumn] = System.Convert.DBNull;
            }
            
            public bool IsDateMonthNull() {
                return this.IsNull(this.tableRateLog.DateMonthColumn);
            }
            
            public void SetDateMonthNull() {
                this[this.tableRateLog.DateMonthColumn] = System.Convert.DBNull;
            }
            
            public bool IsDateYearNull() {
                return this.IsNull(this.tableRateLog.DateYearColumn);
            }
            
            public void SetDateYearNull() {
                this[this.tableRateLog.DateYearColumn] = System.Convert.DBNull;
            }
            
            public bool IsTimeHourNull() {
                return this.IsNull(this.tableRateLog.TimeHourColumn);
            }
            
            public void SetTimeHourNull() {
                this[this.tableRateLog.TimeHourColumn] = System.Convert.DBNull;
            }
            
            public bool IsTimeMinuteNull() {
                return this.IsNull(this.tableRateLog.TimeMinuteColumn);
            }
            
            public void SetTimeMinuteNull() {
                this[this.tableRateLog.TimeMinuteColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class RateLogRowChangeEvent : EventArgs {
            
            private RateLogRow eventRow;
            
            private DataRowAction eventAction;
            
            public RateLogRowChangeEvent(RateLogRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public RateLogRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}
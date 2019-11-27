/*
 * NETMeter - by Alessandro Fragnani
 * StatisticsReports.cs
 * Created: 27 march 2005
 * 
 * Displays Statistics Reports for bandwidth use.
 * Originaly has:
 *  - Total reports : which displays "Today" and "Since installation" values for Download, Upload
 *    and Both directions
 *  - Daily reports : which displays "Daily" values for Download, Upload and Both Directions
 *  - Monthy reports: which displays "Monthly" values for Download, Upload and Both Directions
 * 
 * 
 * Thanks to:
 *  - Alexander Youmashev for his article "Sorting DataGrid programmatically"
 *    (http://www.codeproject.com/csharp/datagridsort.asp)
 *  - Pete2004 for his article series "A Practical Guide to .NET DataTables, DataSets and DataGrids"
 *    (http://www.codeproject.com/csharp/PracticalGuideDataGrids1.asp) (2, 3 and 4)
 * 
 */ 

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using MyDUMeter;

namespace NETMeter
{
	/// <summary>
	/// Summary description for StatisticsReports.
	/// </summary>
	public class StatisticsReports : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControlReports;
		private System.Windows.Forms.TabPage tabPageDailyReport;
		private System.Windows.Forms.TabPage tabPageMonthly;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		private System.Data.OleDb.OleDbConnection connection;
		private System.Windows.Forms.DataGrid dataGridDaily;
		private System.Windows.Forms.DataGrid dataGridMonthly;
		private System.Windows.Forms.Button buttonOk;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label labelBothDirectionsAll;
		private System.Windows.Forms.Label labelDownloadedAll;
		private System.Windows.Forms.Label labelUploadedAll;
		private System.Windows.Forms.Label labelBothDirectionsToday;
		private System.Windows.Forms.Label labelUploadedToday;
		private System.Windows.Forms.Label labelDownloadedToday;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelBothDirectionsTodayValue;
		private System.Windows.Forms.Label labelUploadedTodayValue;
		private System.Windows.Forms.Label labelDownloadedTodayValue;
		private System.Windows.Forms.Label labelBothDirectionsAllValue;
		private System.Windows.Forms.Label labelDownloadedAllValue;
		private System.Windows.Forms.Label labelUploadedAllValue;
		private System.Data.OleDb.OleDbCommand selectDaily = null;


    	/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatisticsReports));
			this.tabControlReports = new System.Windows.Forms.TabControl();
			this.tabPageDailyReport = new System.Windows.Forms.TabPage();
			this.dataGridDaily = new System.Windows.Forms.DataGrid();
			this.tabPageMonthly = new System.Windows.Forms.TabPage();
			this.dataGridMonthly = new System.Windows.Forms.DataGrid();
			this.buttonOk = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.labelBothDirectionsAllValue = new System.Windows.Forms.Label();
			this.labelUploadedAllValue = new System.Windows.Forms.Label();
			this.labelDownloadedAllValue = new System.Windows.Forms.Label();
			this.labelBothDirectionsTodayValue = new System.Windows.Forms.Label();
			this.labelUploadedTodayValue = new System.Windows.Forms.Label();
			this.labelDownloadedTodayValue = new System.Windows.Forms.Label();
			this.labelBothDirectionsAll = new System.Windows.Forms.Label();
			this.labelDownloadedAll = new System.Windows.Forms.Label();
			this.labelUploadedAll = new System.Windows.Forms.Label();
			this.labelBothDirectionsToday = new System.Windows.Forms.Label();
			this.labelUploadedToday = new System.Windows.Forms.Label();
			this.labelDownloadedToday = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControlReports.SuspendLayout();
			this.tabPageDailyReport.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridDaily)).BeginInit();
			this.tabPageMonthly.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridMonthly)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControlReports
			// 
			this.tabControlReports.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.tabControlReports.Controls.AddRange(new System.Windows.Forms.Control[] {
																							this.tabPageDailyReport,
																							this.tabPageMonthly});
			this.tabControlReports.Location = new System.Drawing.Point(4, 104);
			this.tabControlReports.Name = "tabControlReports";
			this.tabControlReports.SelectedIndex = 0;
			this.tabControlReports.Size = new System.Drawing.Size(424, 288);
			this.tabControlReports.TabIndex = 0;
			this.tabControlReports.TabIndexChanged += new System.EventHandler(this.tabControlReports_TabIndexChanged);
			// 
			// tabPageDailyReport
			// 
			this.tabPageDailyReport.Controls.AddRange(new System.Windows.Forms.Control[] {
																							 this.dataGridDaily});
			this.tabPageDailyReport.Location = new System.Drawing.Point(4, 22);
			this.tabPageDailyReport.Name = "tabPageDailyReport";
			this.tabPageDailyReport.Size = new System.Drawing.Size(416, 262);
			this.tabPageDailyReport.TabIndex = 1;
			this.tabPageDailyReport.Text = "Daily Report";
			// 
			// dataGridDaily
			// 
			this.dataGridDaily.AlternatingBackColor = System.Drawing.Color.Lavender;
			this.dataGridDaily.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.dataGridDaily.BackColor = System.Drawing.Color.WhiteSmoke;
			this.dataGridDaily.BackgroundColor = System.Drawing.Color.LightGray;
			this.dataGridDaily.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridDaily.CaptionBackColor = System.Drawing.Color.LightSteelBlue;
			this.dataGridDaily.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.dataGridDaily.CaptionForeColor = System.Drawing.Color.MidnightBlue;
			this.dataGridDaily.CaptionVisible = false;
			this.dataGridDaily.DataMember = "";
			this.dataGridDaily.FlatMode = true;
			this.dataGridDaily.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.dataGridDaily.ForeColor = System.Drawing.Color.MidnightBlue;
			this.dataGridDaily.GridLineColor = System.Drawing.Color.Gainsboro;
			this.dataGridDaily.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
			this.dataGridDaily.HeaderBackColor = System.Drawing.Color.MidnightBlue;
			this.dataGridDaily.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.dataGridDaily.HeaderForeColor = System.Drawing.Color.WhiteSmoke;
			this.dataGridDaily.LinkColor = System.Drawing.Color.Teal;
			this.dataGridDaily.Location = new System.Drawing.Point(8, 8);
			this.dataGridDaily.Name = "dataGridDaily";
			this.dataGridDaily.ParentRowsBackColor = System.Drawing.Color.Gainsboro;
			this.dataGridDaily.ParentRowsForeColor = System.Drawing.Color.MidnightBlue;
			this.dataGridDaily.ParentRowsVisible = false;
			this.dataGridDaily.ReadOnly = true;
			this.dataGridDaily.SelectionBackColor = System.Drawing.Color.CadetBlue;
			this.dataGridDaily.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
			this.dataGridDaily.Size = new System.Drawing.Size(400, 248);
			this.dataGridDaily.TabIndex = 1;
			this.dataGridDaily.Resize += new System.EventHandler(this.dataGridDaily_Resize);
			// 
			// tabPageMonthly
			// 
			this.tabPageMonthly.Controls.AddRange(new System.Windows.Forms.Control[] {
																						 this.dataGridMonthly});
			this.tabPageMonthly.Location = new System.Drawing.Point(4, 22);
			this.tabPageMonthly.Name = "tabPageMonthly";
			this.tabPageMonthly.Size = new System.Drawing.Size(416, 262);
			this.tabPageMonthly.TabIndex = 2;
			this.tabPageMonthly.Text = "Monthly Report";
			// 
			// dataGridMonthly
			// 
			this.dataGridMonthly.AlternatingBackColor = System.Drawing.Color.Lavender;
			this.dataGridMonthly.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.dataGridMonthly.BackColor = System.Drawing.Color.WhiteSmoke;
			this.dataGridMonthly.BackgroundColor = System.Drawing.Color.LightGray;
			this.dataGridMonthly.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridMonthly.CaptionBackColor = System.Drawing.Color.LightSteelBlue;
			this.dataGridMonthly.CaptionFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.dataGridMonthly.CaptionForeColor = System.Drawing.Color.MidnightBlue;
			this.dataGridMonthly.CaptionVisible = false;
			this.dataGridMonthly.CausesValidation = false;
			this.dataGridMonthly.DataMember = "";
			this.dataGridMonthly.FlatMode = true;
			this.dataGridMonthly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.dataGridMonthly.ForeColor = System.Drawing.Color.MidnightBlue;
			this.dataGridMonthly.GridLineColor = System.Drawing.Color.Gainsboro;
			this.dataGridMonthly.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
			this.dataGridMonthly.HeaderBackColor = System.Drawing.Color.MidnightBlue;
			this.dataGridMonthly.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.dataGridMonthly.HeaderForeColor = System.Drawing.Color.WhiteSmoke;
			this.dataGridMonthly.LinkColor = System.Drawing.Color.Teal;
			this.dataGridMonthly.Location = new System.Drawing.Point(8, 8);
			this.dataGridMonthly.Name = "dataGridMonthly";
			this.dataGridMonthly.ParentRowsBackColor = System.Drawing.Color.Gainsboro;
			this.dataGridMonthly.ParentRowsForeColor = System.Drawing.Color.MidnightBlue;
			this.dataGridMonthly.ParentRowsVisible = false;
			this.dataGridMonthly.ReadOnly = true;
			this.dataGridMonthly.SelectionBackColor = System.Drawing.Color.CadetBlue;
			this.dataGridMonthly.SelectionForeColor = System.Drawing.Color.WhiteSmoke;
			this.dataGridMonthly.Size = new System.Drawing.Size(400, 248);
			this.dataGridMonthly.TabIndex = 2;
			// 
			// buttonOk
			// 
			this.buttonOk.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonOk.Location = new System.Drawing.Point(352, 400);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 1;
			this.buttonOk.Text = "Ok";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.panel1.BackColor = System.Drawing.Color.Lavender;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.labelBothDirectionsAllValue,
																				 this.labelUploadedAllValue,
																				 this.labelDownloadedAllValue,
																				 this.labelBothDirectionsTodayValue,
																				 this.labelUploadedTodayValue,
																				 this.labelDownloadedTodayValue,
																				 this.labelBothDirectionsAll,
																				 this.labelDownloadedAll,
																				 this.labelUploadedAll,
																				 this.labelBothDirectionsToday,
																				 this.labelUploadedToday,
																				 this.labelDownloadedToday,
																				 this.panel2});
			this.panel1.Location = new System.Drawing.Point(8, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(416, 88);
			this.panel1.TabIndex = 18;
			// 
			// labelBothDirectionsAllValue
			// 
			this.labelBothDirectionsAllValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelBothDirectionsAllValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelBothDirectionsAllValue.Location = new System.Drawing.Point(300, 63);
			this.labelBothDirectionsAllValue.Name = "labelBothDirectionsAllValue";
			this.labelBothDirectionsAllValue.Size = new System.Drawing.Size(82, 23);
			this.labelBothDirectionsAllValue.TabIndex = 24;
			this.labelBothDirectionsAllValue.Text = "Both ";
			this.labelBothDirectionsAllValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelUploadedAllValue
			// 
			this.labelUploadedAllValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelUploadedAllValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelUploadedAllValue.Location = new System.Drawing.Point(300, 42);
			this.labelUploadedAllValue.Name = "labelUploadedAllValue";
			this.labelUploadedAllValue.Size = new System.Drawing.Size(82, 23);
			this.labelUploadedAllValue.TabIndex = 23;
			this.labelUploadedAllValue.Text = "Uploaded";
			this.labelUploadedAllValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDownloadedAllValue
			// 
			this.labelDownloadedAllValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelDownloadedAllValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelDownloadedAllValue.Location = new System.Drawing.Point(300, 23);
			this.labelDownloadedAllValue.Name = "labelDownloadedAllValue";
			this.labelDownloadedAllValue.Size = new System.Drawing.Size(82, 23);
			this.labelDownloadedAllValue.TabIndex = 22;
			this.labelDownloadedAllValue.Text = "Downloaded";
			this.labelDownloadedAllValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelBothDirectionsTodayValue
			// 
			this.labelBothDirectionsTodayValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelBothDirectionsTodayValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelBothDirectionsTodayValue.Location = new System.Drawing.Point(100, 62);
			this.labelBothDirectionsTodayValue.Name = "labelBothDirectionsTodayValue";
			this.labelBothDirectionsTodayValue.Size = new System.Drawing.Size(82, 23);
			this.labelBothDirectionsTodayValue.TabIndex = 21;
			this.labelBothDirectionsTodayValue.Text = "Both";
			this.labelBothDirectionsTodayValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelUploadedTodayValue
			// 
			this.labelUploadedTodayValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelUploadedTodayValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelUploadedTodayValue.Location = new System.Drawing.Point(100, 42);
			this.labelUploadedTodayValue.Name = "labelUploadedTodayValue";
			this.labelUploadedTodayValue.Size = new System.Drawing.Size(82, 23);
			this.labelUploadedTodayValue.TabIndex = 20;
			this.labelUploadedTodayValue.Text = "Uploaded";
			this.labelUploadedTodayValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelDownloadedTodayValue
			// 
			this.labelDownloadedTodayValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelDownloadedTodayValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.labelDownloadedTodayValue.Location = new System.Drawing.Point(100, 22);
			this.labelDownloadedTodayValue.Name = "labelDownloadedTodayValue";
			this.labelDownloadedTodayValue.Size = new System.Drawing.Size(82, 23);
			this.labelDownloadedTodayValue.TabIndex = 19;
			this.labelDownloadedTodayValue.Text = "Downloaded";
			this.labelDownloadedTodayValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelBothDirectionsAll
			// 
			this.labelBothDirectionsAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelBothDirectionsAll.Location = new System.Drawing.Point(216, 64);
			this.labelBothDirectionsAll.Name = "labelBothDirectionsAll";
			this.labelBothDirectionsAll.Size = new System.Drawing.Size(82, 23);
			this.labelBothDirectionsAll.TabIndex = 18;
			this.labelBothDirectionsAll.Text = "Both directions:";
			// 
			// labelDownloadedAll
			// 
			this.labelDownloadedAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelDownloadedAll.Location = new System.Drawing.Point(216, 23);
			this.labelDownloadedAll.Name = "labelDownloadedAll";
			this.labelDownloadedAll.Size = new System.Drawing.Size(82, 23);
			this.labelDownloadedAll.TabIndex = 16;
			this.labelDownloadedAll.Text = "Downloaded:";
			// 
			// labelUploadedAll
			// 
			this.labelUploadedAll.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelUploadedAll.Location = new System.Drawing.Point(216, 42);
			this.labelUploadedAll.Name = "labelUploadedAll";
			this.labelUploadedAll.Size = new System.Drawing.Size(82, 23);
			this.labelUploadedAll.TabIndex = 17;
			this.labelUploadedAll.Text = "Uploaded:";
			// 
			// labelBothDirectionsToday
			// 
			this.labelBothDirectionsToday.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelBothDirectionsToday.Location = new System.Drawing.Point(16, 62);
			this.labelBothDirectionsToday.Name = "labelBothDirectionsToday";
			this.labelBothDirectionsToday.Size = new System.Drawing.Size(82, 23);
			this.labelBothDirectionsToday.TabIndex = 15;
			this.labelBothDirectionsToday.Text = "Both directions:";
			// 
			// labelUploadedToday
			// 
			this.labelUploadedToday.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelUploadedToday.Location = new System.Drawing.Point(16, 42);
			this.labelUploadedToday.Name = "labelUploadedToday";
			this.labelUploadedToday.Size = new System.Drawing.Size(82, 23);
			this.labelUploadedToday.TabIndex = 14;
			this.labelUploadedToday.Text = "Uploaded:";
			// 
			// labelDownloadedToday
			// 
			this.labelDownloadedToday.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelDownloadedToday.Location = new System.Drawing.Point(16, 22);
			this.labelDownloadedToday.Name = "labelDownloadedToday";
			this.labelDownloadedToday.Size = new System.Drawing.Size(82, 23);
			this.labelDownloadedToday.TabIndex = 13;
			this.labelDownloadedToday.Text = "Downloaded:";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.SlateBlue;
			this.panel2.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.label2,
																				 this.label1});
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(414, 16);
			this.panel2.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.SystemColors.Window;
			this.label2.Location = new System.Drawing.Point(206, 1);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 23);
			this.label2.TabIndex = 17;
			this.label2.Text = "Since installation";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.Window;
			this.label1.Location = new System.Drawing.Point(6, 1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 23);
			this.label1.TabIndex = 10;
			this.label1.Text = "Today";
			// 
			// StatisticsReports
			// 
			this.AcceptButton = this.buttonOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonOk;
			this.ClientSize = new System.Drawing.Size(434, 432);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1,
																		  this.buttonOk,
																		  this.tabControlReports});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(440, 464);
			this.Name = "StatisticsReports";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Statistics";
			this.tabControlReports.ResumeLayout(false);
			this.tabPageDailyReport.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridDaily)).EndInit();
			this.tabPageMonthly.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridMonthly)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void tabPageTotals_Click(object sender, System.EventArgs e)
		{
		
		}

		private void tabControlReports_TabIndexChanged(object sender, System.EventArgs e)
		{
			
		}

		public StatisticsReports(System.Data.OleDb.OleDbConnection connection, MyDUMeter.ControlOptions options)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.connection = connection;

			// fire reports
			FireTotalReports();

			FireDailyReports();

			FireMonthlyReports();
		}


		/// <summary>
		/// Fire TOTAL REPORTS
		/// </summary>
		private void FireTotalReports()
		{
			if (this.connection != null) 
			{
				DateTime oNow = DateTime.Now;

				/// today
				/// 
				selectDaily = new System.Data.OleDb.OleDbCommand();
				selectDaily.CommandText = "SELECT " + 					
					" sum(recv) as Download, " +
					" sum(send) as Upload, " +
					" Download + Upload as BothSides " +
					"from " +
					" RateLog " +
					"where " +
					" dateday = " + oNow.Day + " and datemonth = " + oNow.Month + 
					" and dateyear = " + oNow.Year;
				selectDaily.Connection = this.connection;
				selectDaily.ExecuteNonQuery();

				OleDbDataAdapter selectDA = new OleDbDataAdapter();
				selectDA.SelectCommand = selectDaily;

				DataSet selectDS = new DataSet();
				selectDA.Fill(selectDS);
			
				if ((selectDS.Tables[0].Rows.Count == 1) && (selectDS.Tables[0].Rows[0]["Download"].ToString() == ""))
				{
					labelDownloadedTodayValue.Text = TranslateBytes("0.0");
					labelUploadedTodayValue.Text = TranslateBytes("0.0");
					labelBothDirectionsTodayValue.Text = TranslateBytes("0.0");					
					
					labelDownloadedAllValue.Text = TranslateBytes("0.0");
					labelUploadedAllValue.Text = TranslateBytes("0.0");
					labelBothDirectionsAllValue.Text = TranslateBytes("0.0");
				} 
				else 
				{
					// For each row, print the values of each column.
					foreach(DataRow myRow in selectDS.Tables[0].Rows)
					{
						labelDownloadedTodayValue.Text = TranslateBytes(myRow["Download"].ToString());
						labelUploadedTodayValue.Text = TranslateBytes(myRow["Upload"].ToString());
						labelBothDirectionsTodayValue.Text = TranslateBytes(myRow["BothSides"].ToString());
					}


					/// all
					/// 
					selectDaily = new System.Data.OleDb.OleDbCommand();
					selectDaily.CommandText = "SELECT " + 					
						" sum(recv) as Download, " +
						" sum(send) as Upload, " +
						" Download + Upload as BothSides " +
						"from " +
						" RateLog ";
					selectDaily.Connection = this.connection;
					selectDaily.ExecuteNonQuery();

					selectDA = new OleDbDataAdapter();
					selectDA.SelectCommand = selectDaily;

					selectDS = new DataSet();
					selectDA.Fill(selectDS);

					// For each row, print the values of each column.
					foreach(DataRow myRow in selectDS.Tables[0].Rows)
					{
						labelDownloadedAllValue.Text = TranslateBytes(myRow["Download"].ToString());
						labelUploadedAllValue.Text = TranslateBytes(myRow["Upload"].ToString());
						labelBothDirectionsAllValue.Text = TranslateBytes(myRow["BothSides"].ToString());
					}
				}
			}
		}



		/// <summary>
		/// Filre DAILY REPORTS
		/// </summary>
		private void FireDailyReports()
		{
			if (this.connection != null) 
			{
				selectDaily = new System.Data.OleDb.OleDbCommand();
				selectDaily.CommandText = "SELECT " + 
					" dateday, datemonth, dateyear, " +
					" sum(recv) as Download, " +
					" sum(send) as Upload, " +
					" Download + Upload as BothSides " +
					"from " +
					" RateLog " +
					"group by " +
					" dateday, datemonth, dateyear ";
				selectDaily.Connection = this.connection;
				selectDaily.ExecuteNonQuery();

				OleDbDataAdapter selectDA = new OleDbDataAdapter();
				selectDA.SelectCommand = selectDaily;

				DataSet selectDS = new DataSet();
				selectDA.Fill(selectDS);

				DataTable dt = new DataTable();
				dt.TableName = "Table";
				DataColumn dc;

				dc = new DataColumn("Day", System.Type.GetType("System.DateTime"));
				dt.Columns.Add(dc);
				dc = new DataColumn("Download", System.Type.GetType("System.String"));
				dt.Columns.Add(dc);
				dc = new DataColumn("Upload", System.Type.GetType("System.String"));
				dt.Columns.Add(dc);
				dc = new DataColumn("Both Directions", System.Type.GetType("System.String"));
				dt.Columns.Add(dc);

				
				// For each row, print the values of each column.
				foreach(DataRow myRow in selectDS.Tables[0].Rows)
				{
					DataRow anewRow = dt.NewRow();
					anewRow["Day"] = TwoDigits(myRow["dateday"].ToString()) + "/" + 
						TwoDigits(myRow["datemonth"].ToString()) + "/" + myRow["dateyear"];
					anewRow["Download"] = TranslateBytes(myRow["download"].ToString());
					anewRow["Upload"] = TranslateBytes(myRow["Upload"].ToString());
					anewRow["Both Directions"] = TranslateBytes(myRow["BothSides"].ToString());
					dt.Rows.Add(anewRow);
				}
 
				// na mão
				AutoResizeDataGridTableStyle DGStyle = new AutoResizeDataGridTableStyle();

				// In this example the .NET DataGridTextBoxColumn class is used.
				DataGridTextBoxColumn textColumn;
				// Loop through each Column in table dt to get a DataColumn
				// object that will be used
				// to define properties for its TextBoxColumn style.
				foreach (DataColumn dataColumn in dt.Columns)
				{
					textColumn = new DataGridTextBoxColumn();
					// the MappingName must correspond to the Table Column Name
					// in order to establish the relationship between them
					textColumn.MappingName = dataColumn.ColumnName;
					// the HeaderText value is displayed in Header for the column, here
					// the Caption value is used.
					textColumn.HeaderText = dataColumn.Caption;
					// specify some other property values
					textColumn.Width = (dataGridDaily.Width - dataGridDaily.RowHeaderWidth - 16) / 4;

					//textColumn.Alignment = System.Windows.Forms.HorizontalAlignment.Left ;
					if (dataColumn.ColumnName.ToLower().Equals("day") || dataColumn.ColumnName.ToLower().Equals("month / year"))
					{
						textColumn.Alignment = HorizontalAlignment.Left;
						textColumn.Format = "d";
					} 
					else 
					{
						textColumn.Alignment = HorizontalAlignment.Right;
					}

					textColumn.ReadOnly = true;
					// Add this column object with its specifications to the
					// GridColumnStyles collection
					DGStyle.GridColumnStyles.Add(textColumn);
				}
				DGStyle.MappingName = selectDS.Tables[0].TableName;

				// The Clear() method is called to ensure that
				// the previous style is removed.
				dataGridDaily.TableStyles.Clear();

				// Add the new DataGridTableStyle collection to
				// the TableStyles collection
				dataGridDaily.TableStyles.Add(DGStyle);

				dataGridDaily.DataSource = dt;


				SortColumn(dataGridDaily, 0);
			}
		}

		

		/// <summary>
		/// Fire MONTHLY REPORTS
		/// </summary>
		private void FireMonthlyReports()
		{
			if (this.connection != null) 
			{
				selectDaily = new System.Data.OleDb.OleDbCommand();
								selectDaily.CommandText = "SELECT  " +
									"datemonth, dateyear, " +
									"sum(recv) as Download, " +
									"sum(send) as Upload, " +
										"Download + Upload as BothSides " +
										"from " +
										" RateLog " +
										"group by " +
					" datemonth, dateyear ";
				selectDaily.Connection = this.connection;
				selectDaily.ExecuteNonQuery();

				//dataGridDailyReport.DataSource = selectDaily;
				
				OleDbDataAdapter selectDA = new OleDbDataAdapter();
				selectDA.SelectCommand = selectDaily;

				DataSet selectDS = new DataSet();
				selectDA.Fill(selectDS);

				DataTable dt = new DataTable();
				dt.TableName = "Table";
				DataColumn dc;
				dc = new DataColumn("Month / Year", System.Type.GetType("System.String"));
				dt.Columns.Add(dc);
				dc = new DataColumn("Download", System.Type.GetType("System.String"));
				dt.Columns.Add(dc);
				dc = new DataColumn("Upload", System.Type.GetType("System.String"));
				dt.Columns.Add(dc);
				dc = new DataColumn("Both Directions", System.Type.GetType("System.String"));
				dt.Columns.Add(dc);

				
				// For each row, print the values of each column.
				foreach(DataRow myRow in selectDS.Tables[0].Rows)
				{
					DataRow anewRow = dt.NewRow();
					anewRow["Month / Year"] = TranslateMonth(myRow["datemonth"].ToString()) + " / " + myRow["dateyear"];
					anewRow["Download"] = TranslateBytes(myRow["download"].ToString());
					anewRow["Upload"] = TranslateBytes(myRow["Upload"].ToString());
					anewRow["Both Directions"] = TranslateBytes(myRow["BothSides"].ToString());
					dt.Rows.Add(anewRow);
				}

				//
				AutoResizeDataGridTableStyle DGStyle = new AutoResizeDataGridTableStyle();

				// In this example the .NET DataGridTextBoxColumn class is used.
				DataGridTextBoxColumn textColumn;
				// Loop through each Column in table dt to get a DataColumn
				// object that will be used
				// to define properties for its TextBoxColumn style.
				foreach (DataColumn dataColumn in dt.Columns)
				{
					textColumn = new DataGridTextBoxColumn();
					// the MappingName must correspond to the Table Column Name
					// in order to establish the relationship between them
					textColumn.MappingName = dataColumn.ColumnName;
					// the HeaderText value is displayed in Header for the column, here
					// the Caption value is used.
					textColumn.HeaderText = dataColumn.Caption;
					// specify some other property values
					textColumn.Width = (dataGridMonthly.Width - dataGridMonthly.RowHeaderWidth - 16) / 4;

					//textColumn.Alignment = System.Windows.Forms.HorizontalAlignment.Left ;
					if (dataColumn.ColumnName.ToLower().Equals("day") || dataColumn.ColumnName.ToLower().Equals("month / year"))
					{
						textColumn.Alignment = HorizontalAlignment.Left;
						textColumn.Format = "d";
					} 
					else 
					{
						textColumn.Alignment = HorizontalAlignment.Right;
					}

					textColumn.ReadOnly = true;
					// Add this column object with its specifications to the
					// GridColumnStyles collection
					DGStyle.GridColumnStyles.Add(textColumn);
				}
				DGStyle.MappingName = selectDS.Tables[0].TableName;

 				// The Clear() method is called to ensure that
				// the previous style is removed.
				dataGridMonthly.TableStyles.Clear();

				// Add the new DataGridTableStyle collection to
				// the TableStyles collection
				dataGridMonthly.TableStyles.Add(DGStyle);

				dataGridMonthly.DataSource = dt;

				SortColumn(dataGridMonthly, 0);
			}
		}

		

		#region Extra Functions

		/// <summary>
		/// Translate a MONTH CODE to its DESCRIPTION
		/// </summary>
		/// <param name="month">The Month Code (1..12)</param>
		/// <returns>The Month description (January..December)</returns>
		private string TranslateMonth(String month) 
		{
			switch(month)       
			{         
				case "1": return "January";        
				case "2": return "February";        
				case "3": return "March";        
				case "4": return "April";        
				case "5": return "May";        
				case "6": return "June";        
				case "7": return "July";        
				case "8": return "August";        
				case "9": return "September";        
				case "10": return "October";        
				case "11": return "November";        
				default: return "December";        
			}
		}


		/// <summary>
		/// Returns a "Two digits" representation of a value, with "0" in front (if necessary)
		/// </summary>
		/// <param name="number">The original number</param>
		/// <returns>The Two digit representation</returns>
		private string TwoDigits(String number) 
		{
			if (number.Length == 1) 
			{
				return "0" + number;
			}
			else 
			{
				return number;
			}
		}


		/// <summary>
		/// Convert a value into Kb, Mb or Gb value (/1024)
		/// </summary>
		/// <param name="amount">The original value</param>
		/// <returns>Bytes representation</returns>
		private string TranslateBytes(String amount) 
		{
			// bytes
			double valor = Convert.ToDouble(amount);

			// kb
			valor = valor / 1024;
			if (valor < 1024) 
			{
				return valor.ToString("f2") + " Kb";
			} 
			else 
			{
				valor = valor / 1024;
				if (valor < 1024) 
				{
					return valor.ToString("f2") + " Mb";
					} 
				else 
				{
					valor = valor / 1024;
					return valor.ToString("f2") + " Gb";
				}
			}
		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void dataGridDaily_Resize(object sender, System.EventArgs e)
		{
			//
		}

		/// <summary>
		/// Sort a column by its index
		/// </summary>
		/// <param name="datagrid"></param>
		/// <param name="columnIndex"></param>
		public void SortColumn(DataGrid datagrid, int columnIndex)
		{
			if(datagrid.DataSource!=null)
			{
				//discover the TYPE of underlying objects
				Type sourceType = datagrid.DataSource.GetType();

				//get the PropertyDescriptor for a sorted column
				//assume TableStyles[0] is used for our datagrid... (change it if necessary)
				System.ComponentModel.PropertyDescriptor pd =
					datagrid.TableStyles[0].GridColumnStyles[columnIndex].PropertyDescriptor;

				//if the above line of code didn't work try to get a propertydescriptor
				// via MappingName
				if(pd == null)
				{
					System.ComponentModel.PropertyDescriptorCollection pdc =
						System.ComponentModel.TypeDescriptor.GetProperties(sourceType);
					pd =
						pdc.Find( datagrid.TableStyles[0].GridColumnStyles[columnIndex].MappingName, 
						false);
				}

				//now invoke ColumnHeaderClicked method using system.reflection tools
				System.Reflection.MethodInfo mi =
					typeof(System.Windows.Forms.DataGrid).GetMethod("ColumnHeaderClicked",
					System.Reflection.BindingFlags.Instance |
					System.Reflection.BindingFlags.NonPublic);
				mi.Invoke(datagrid, new object[] { pd });
			}
		}
		
	}
}

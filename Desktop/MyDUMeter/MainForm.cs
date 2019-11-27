/*
 * MyDUMeter - by leppie
 * Original Project.
 * 
 * NETMeter - by Alessandro Fragnani
 * MainForm.cs
 * 
 * Changes:  
 *  - Changed: Load and Save settings, to use the new method where is passed only the Options
 *             object as parameter (and so, it uses the static properties values)
 *  - Changed: Since I added the customization of the "Display Kind", I had to add some panels
 *             to the form, so I could select Graphic or Text view.
 *  - Changed: DrawGraph() method to add the new columns to the database, to be able to create
 *             the statistics reports
 *  - Removed: The text on the Graphic Panel from DrawGraph() method. Since I added a specific
 *             panel for the "Human readable value" below the graphic
 *  - Changed: PaintBoxResize() method to respect the new measures for the Text panel
 *  - Added: ReApplyBandwidthDisplay() method to change the view of the Meter
 *  - Changed: pictureBox1_Paint() method to use the "Options.TimeIntervalLine" property and to 
 *             not draw the Text on the Graphic
 *  - Added: Menu Items to select the "Display Kind" and the new "Statistic Report"
 *  - Added: MSN Like Notifier events
 * 
 * Thanks to:
 *  - John O'Byrne for his article "TaskbarNotifier, a skinnable MSN Messenger-like popup in 
 *    C# and now in VB.NET too"
 *    (http://www.codeproject.com/cs/miscctrl/taskbarnotifier.asp)
 * 
 */ 


//#define WIP

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Threading;
using NETMeter;

using CustomUIControls;

namespace MyDUMeter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;
		private System.Diagnostics.PerformanceCounter performanceCounter1;
		private System.Diagnostics.PerformanceCounter performanceCounter2;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private MyDUMeter.Log log1;
		private System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1;
		private System.Data.OleDb.OleDbConnection oleDbConnection1;
		private System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
		private System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
		private System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
		private System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
 
		Bitmap bit;
		int linespeed = 16 * 1024;
		private System.Windows.Forms.ImageList imageList1;
		//Bitmap icon;

		// Amount Volume Notifier
		private TaskbarNotifier taskbarNotifier;
		private double volumeReceived = 0;
		private System.Windows.Forms.MenuItem menuItemOptions;
		private System.Windows.Forms.MenuItem menuItemHide;
		private System.Windows.Forms.MenuItem menuItemReports;
		private System.Windows.Forms.MenuItem menuItemReportLastHour;
		private System.Windows.Forms.MenuItem menuItemReportLast3Hours;
		private System.Windows.Forms.MenuItem menuItemReportLast6Hours;
		private System.Windows.Forms.MenuItem menuItemReportLast12Hours;
		private System.Windows.Forms.MenuItem menuItemReportLast24Hours;
		private System.Windows.Forms.MenuItem menuItemStartPing;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItemStatistics;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panelBandwidthLabels;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItemBandwidthDisplay;
		private System.Windows.Forms.MenuItem menuItemBDGraphics;
		private System.Windows.Forms.MenuItem menuItemBDText;
		private System.Windows.Forms.Panel panelBandwidthGraphic;
		private System.Windows.Forms.MenuItem menuItemBDBoth;


		// Options
		ControlOptions options;

		/*
		 * Inicializar o Taskbar Notifier 
		 */

		/// <summary>
		/// The Database
		/// </summary>
		public static string SkinBMP
		{
			get 
			{
#if (DEBUG)
				return Application.StartupPath + "\\skin.bmp";
#else
				string sDir = Application.LocalUserAppDataPath;
				DirectoryInfo di = Directory.GetParent(sDir);
				return di.FullName + "\\skin.bmp";
#endif
			}
		}

		public static string CloseBMP
		{
			get 
			{
#if (DEBUG)
				return Application.StartupPath + "\\close.bmp";
#else
				string sDir = Application.LocalUserAppDataPath;
				DirectoryInfo di = Directory.GetParent(sDir);
				return di.FullName + "\\close.bmp";
#endif
			}
		}


		private void InitializeTaskbarNotifier() 
		{
			taskbarNotifier = new TaskbarNotifier();

			if (!(File.Exists(MainForm.SkinBMP)))
			{
				Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("MyDUMeter.skin.bmp");
				Stream r = File.Create(MainForm.SkinBMP);
				int len = 8192;
				byte[] buffer = new byte[len];
				while (len > 0)
				{
					len = s.Read(buffer, 0, len);
					r.Write(buffer, 0, len);
				}
				r.Close();
				s.Close();
			}        
  
			if (!(File.Exists(MainForm.CloseBMP)))
			{
				Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("MyDUMeter.close.bmp");
				Stream r = File.Create(MainForm.CloseBMP);
				int len = 8192;
				byte[] buffer = new byte[len];
				while (len > 0)
				{
					len = s.Read(buffer, 0, len);
					r.Write(buffer, 0, len);
				}
				r.Close();
				s.Close();
			}          

			taskbarNotifier.SetBackgroundBitmap(MainForm.SkinBMP,
				Color.FromArgb(255,0,255));
			taskbarNotifier.SetCloseBitmap(MainForm.CloseBMP,
				Color.FromArgb(255,0,255),new Point(127,8));
			taskbarNotifier.TitleRectangle = new Rectangle(40,9,70,25);
			taskbarNotifier.ContentRectangle = new Rectangle(8,41,133,68);
		}


      
		public MainForm()
		{
			InitializeComponent();

			InitializeTaskbarNotifier();

			// form styles
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			options = new ControlOptions();

			// creates the database
			if (!(File.Exists(Options.LOGDatabase/*"log.mdb"*/)))
			{
				Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("MyDUMeter.log.mdb");
				Stream r = File.Create(Options.LOGDatabase/*"log.mdb"*/);
				int len = 8192;
				byte[] buffer = new byte[len];
				while (len > 0)
				{
					len = s.Read(buffer, 0, len);
					r.Write(buffer, 0, len);
				}
				r.Close();
				s.Close();
			}          
			oleDbConnection1.ConnectionString = 
				"Provider=Microsoft.Jet.OLEDB.4.0;Password=\"\";User ID=Admin;Data Source=" + 
				Options.LOGDatabase + ";Mode=Share Deny None;Extended Properties=\"\";Jet OLEDB:System database=\"\";Jet OLEDB:Registry Path=\"\";Jet OLEDB:Database Password=\"\";Jet OLEDB:Engine Type=5;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:New Database Password=\"\";Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;Jet OLEDB:SFP=False";			

			// starting
			Application.ApplicationExit += new EventHandler(this.AppExit);
			MouseWheel += new MouseEventHandler(MyMouseWheel);
			bit = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			notifyIcon1.Icon = Icon;
			//icon = Icon.ToBitmap();
			//any more and it sucks too much CPU (was 500)
			timer1.Interval = 1000;
			recvallq = new FixedSizeQueue(Screen.PrimaryScreen.Bounds.Width);
			sendallq = new FixedSizeQueue(Screen.PrimaryScreen.Bounds.Width);
			oleDbConnection1.Open();
		}


		private void MyMouseWheel(object sender, MouseEventArgs e)
		{
			if (briteness)
			{
				options.Transparency += (e.Delta/2000F);
				if (options.Transparency < 0.1)
				{
					options.Transparency = 0.1;
				}
				if (options.Transparency > 1)
				{
					options.Transparency = 1;
				}
				Opacity = options.Transparency;
				hidecounter = 0;
				Invalidate();
			}
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				StopPing();
				up.Dispose();
				down.Dispose();
				both.Dispose();
				h.Dispose();
				//icon.Dispose();

				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
			this.performanceCounter2 = new System.Diagnostics.PerformanceCounter();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItemOptions = new System.Windows.Forms.MenuItem();
			this.menuItemHide = new System.Windows.Forms.MenuItem();
			this.menuItemReports = new System.Windows.Forms.MenuItem();
			this.menuItemReportLastHour = new System.Windows.Forms.MenuItem();
			this.menuItemReportLast3Hours = new System.Windows.Forms.MenuItem();
			this.menuItemReportLast6Hours = new System.Windows.Forms.MenuItem();
			this.menuItemReportLast12Hours = new System.Windows.Forms.MenuItem();
			this.menuItemReportLast24Hours = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemStatistics = new System.Windows.Forms.MenuItem();
			this.menuItemBandwidthDisplay = new System.Windows.Forms.MenuItem();
			this.menuItemBDGraphics = new System.Windows.Forms.MenuItem();
			this.menuItemBDText = new System.Windows.Forms.MenuItem();
			this.menuItemBDBoth = new System.Windows.Forms.MenuItem();
			this.menuItemStartPing = new System.Windows.Forms.MenuItem();
			this.menuItemAbout = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.log1 = new MyDUMeter.Log();
			this.oleDbDataAdapter1 = new System.Data.OleDb.OleDbDataAdapter();
			this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbConnection1 = new System.Data.OleDb.OleDbConnection();
			this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
			this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelBandwidthGraphic = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panelBandwidthLabels = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.log1)).BeginInit();
			this.panel1.SuspendLayout();
			this.panelBandwidthGraphic.SuspendLayout();
			this.panelBandwidthLabels.SuspendLayout();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// performanceCounter1
			// 
			this.performanceCounter1.CategoryName = "Network Interface";
			this.performanceCounter1.CounterName = "Bytes Received/sec";
			// 
			// performanceCounter2
			// 
			this.performanceCounter2.CategoryName = "Network Interface";
			this.performanceCounter2.CounterName = "Bytes Sent/sec";
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItemOptions,
																						 this.menuItemHide,
																						 this.menuItemReports,
																						 this.menuItemBandwidthDisplay,
																						 this.menuItemStartPing,
																						 this.menuItemAbout,
																						 this.menuItemExit});
			// 
			// menuItemOptions
			// 
			this.menuItemOptions.Index = 0;
			this.menuItemOptions.Text = "Options...";
			this.menuItemOptions.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItemHide
			// 
			this.menuItemHide.Index = 1;
			this.menuItemHide.Text = "Hide";
			this.menuItemHide.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItemReports
			// 
			this.menuItemReports.Index = 2;
			this.menuItemReports.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.menuItemReportLastHour,
																							this.menuItemReportLast3Hours,
																							this.menuItemReportLast6Hours,
																							this.menuItemReportLast12Hours,
																							this.menuItemReportLast24Hours,
																							this.menuItem1,
																							this.menuItemStatistics});
			this.menuItemReports.Text = "Reports";
			// 
			// menuItemReportLastHour
			// 
			this.menuItemReportLastHour.Index = 0;
			this.menuItemReportLastHour.Text = "Last Hour";
			this.menuItemReportLastHour.Click += new System.EventHandler(this.menuItem3_Click_1);
			// 
			// menuItemReportLast3Hours
			// 
			this.menuItemReportLast3Hours.Index = 1;
			this.menuItemReportLast3Hours.Text = "Last 3 hours";
			this.menuItemReportLast3Hours.Click += new System.EventHandler(this.menuItem10_Click_1);
			// 
			// menuItemReportLast6Hours
			// 
			this.menuItemReportLast6Hours.Index = 2;
			this.menuItemReportLast6Hours.Text = "Last 6 hours";
			this.menuItemReportLast6Hours.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// menuItemReportLast12Hours
			// 
			this.menuItemReportLast12Hours.Index = 3;
			this.menuItemReportLast12Hours.Text = "Last 12 hours";
			this.menuItemReportLast12Hours.Click += new System.EventHandler(this.menuItem4_Click_1);
			// 
			// menuItemReportLast24Hours
			// 
			this.menuItemReportLast24Hours.Index = 4;
			this.menuItemReportLast24Hours.Text = "Last 24 hours";
			this.menuItemReportLast24Hours.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 5;
			this.menuItem1.Text = "-";
			// 
			// menuItemStatistics
			// 
			this.menuItemStatistics.Index = 6;
			this.menuItemStatistics.Text = "Statistics...";
			this.menuItemStatistics.Click += new System.EventHandler(this.menuItemStatistics_Click);
			// 
			// menuItemBandwidthDisplay
			// 
			this.menuItemBandwidthDisplay.Index = 3;
			this.menuItemBandwidthDisplay.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																									 this.menuItemBDGraphics,
																									 this.menuItemBDText,
																									 this.menuItemBDBoth});
			this.menuItemBandwidthDisplay.Text = "Bandwidth Display";
			// 
			// menuItemBDGraphics
			// 
			this.menuItemBDGraphics.Index = 0;
			this.menuItemBDGraphics.RadioCheck = true;
			this.menuItemBDGraphics.Text = "Graphics only";
			this.menuItemBDGraphics.Click += new System.EventHandler(this.menuItemBDBoth_Click);
			// 
			// menuItemBDText
			// 
			this.menuItemBDText.Index = 1;
			this.menuItemBDText.RadioCheck = true;
			this.menuItemBDText.Text = "Text Only";
			this.menuItemBDText.Click += new System.EventHandler(this.menuItemBDBoth_Click);
			// 
			// menuItemBDBoth
			// 
			this.menuItemBDBoth.Checked = true;
			this.menuItemBDBoth.Index = 2;
			this.menuItemBDBoth.RadioCheck = true;
			this.menuItemBDBoth.Text = "Both Graphics and Text";
			this.menuItemBDBoth.Click += new System.EventHandler(this.menuItemBDBoth_Click);
			// 
			// menuItemStartPing
			// 
			this.menuItemStartPing.Index = 4;
			this.menuItemStartPing.RadioCheck = true;
			this.menuItemStartPing.Text = "Start Ping";
			this.menuItemStartPing.Visible = false;
			this.menuItemStartPing.Click += new System.EventHandler(this.menuItem12_Click);
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Index = 5;
			this.menuItemAbout.Text = "About...";
			this.menuItemAbout.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 6;
			this.menuItemExit.Text = "Exit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// notifyIcon1
			// 
			this.notifyIcon1.Text = "";
			this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
			// 
			// log1
			// 
			this.log1.DataSetName = "Log";
			this.log1.Locale = new System.Globalization.CultureInfo("en-ZA");
			this.log1.Namespace = "http://www.tempuri.org/Log.xsd";
			// 
			// oleDbDataAdapter1
			// 
			this.oleDbDataAdapter1.DeleteCommand = this.oleDbDeleteCommand1;
			this.oleDbDataAdapter1.InsertCommand = this.oleDbInsertCommand1;
			this.oleDbDataAdapter1.SelectCommand = this.oleDbSelectCommand1;
			this.oleDbDataAdapter1.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																										new System.Data.Common.DataTableMapping("Table", "RateLog", new System.Data.Common.DataColumnMapping[] {
																																																				   new System.Data.Common.DataColumnMapping("ID", "ID"),
																																																				   new System.Data.Common.DataColumnMapping("Recv", "Recv"),
																																																				   new System.Data.Common.DataColumnMapping("Send", "Send")})});
			this.oleDbDataAdapter1.UpdateCommand = this.oleDbUpdateCommand1;
			// 
			// oleDbDeleteCommand1
			// 
			this.oleDbDeleteCommand1.CommandText = "DELETE FROM RateLog WHERE (ID = ?)";
			this.oleDbDeleteCommand1.Connection = this.oleDbConnection1;
			this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ID", System.Data.DataRowVersion.Original, null));
			// 
			// oleDbConnection1
			// 
			this.oleDbConnection1.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Password="""";User ID=Admin;Data Source=D:\dev\MyDUMeter\MyDUMeter\bin\Release\log.mdb;Mode=Share Deny None;Extended Properties="""";Jet OLEDB:System database="""";Jet OLEDB:Registry Path="""";Jet OLEDB:Database Password="""";Jet OLEDB:Engine Type=5;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:New Database Password="""";Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;Jet OLEDB:SFP=False";
			// 
			// oleDbInsertCommand1
			// 
			this.oleDbInsertCommand1.CommandText = "INSERT INTO RateLog (Recv, Send, DateDay, DateMonth, DateYear, TimeHour, TimeMinu" +
				"te) VALUES (?, ?, ?, ?, ?, ?, ?)";
			this.oleDbInsertCommand1.Connection = this.oleDbConnection1;
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Recv", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(7)), ((System.Byte)(0)), "Recv", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Send", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(7)), ((System.Byte)(0)), "Send", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DateDay", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DateDay", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DateMonth", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DateMonth", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DateYear", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "DateYear", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TimeHour", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "TimeHour", System.Data.DataRowVersion.Current, null));
			this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TimeMinute", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(10)), ((System.Byte)(0)), "TimeMinute", System.Data.DataRowVersion.Current, null));
			// 
			// oleDbSelectCommand1
			// 
			this.oleDbSelectCommand1.CommandText = "SELECT ID, Recv, Send FROM RateLog WHERE (ID > ?)";
			this.oleDbSelectCommand1.Connection = this.oleDbConnection1;
			this.oleDbSelectCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.DBDate, 0, "ID"));
			// 
			// oleDbUpdateCommand1
			// 
			this.oleDbUpdateCommand1.CommandText = "UPDATE RateLog SET ID = ?, Recv = ?, Send = ? WHERE (ID = ?)";
			this.oleDbUpdateCommand1.Connection = this.oleDbConnection1;
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("ID", System.Data.OleDb.OleDbType.Integer, 0, "ID"));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Recv", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(7)), ((System.Byte)(0)), "Recv", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Send", System.Data.OleDb.OleDbType.Single, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(7)), ((System.Byte)(0)), "Send", System.Data.DataRowVersion.Current, null));
			this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ID", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ID", System.Data.DataRowVersion.Original, null));
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.AddRange(new System.Windows.Forms.Control[] {
																				 this.panelBandwidthGraphic,
																				 this.panelBandwidthLabels});
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(166, 70);
			this.panel1.TabIndex = 3;
			// 
			// panelBandwidthGraphic
			// 
			this.panelBandwidthGraphic.Controls.AddRange(new System.Windows.Forms.Control[] {
																								this.pictureBox1});
			this.panelBandwidthGraphic.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelBandwidthGraphic.DockPadding.All = 2;
			this.panelBandwidthGraphic.Name = "panelBandwidthGraphic";
			this.panelBandwidthGraphic.Size = new System.Drawing.Size(164, 48);
			this.panelBandwidthGraphic.TabIndex = 3;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.ContextMenu = this.contextMenu1;
			this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBox1.Location = new System.Drawing.Point(2, 2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(160, 44);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Resize += new System.EventHandler(this.pictureBox1_Resize);
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.pictureBox1.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
			this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			// 
			// panelBandwidthLabels
			// 
			this.panelBandwidthLabels.Controls.AddRange(new System.Windows.Forms.Control[] {
																							   this.label1});
			this.panelBandwidthLabels.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBandwidthLabels.DockPadding.All = 2;
			this.panelBandwidthLabels.Location = new System.Drawing.Point(0, 48);
			this.panelBandwidthLabels.Name = "panelBandwidthLabels";
			this.panelBandwidthLabels.Size = new System.Drawing.Size(164, 20);
			this.panelBandwidthLabels.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.Window;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.ContextMenu = this.contextMenu1;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(2, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Down: ??  Up: ??";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
			this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			this.label1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
			this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
			this.label1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
			this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(18, 39);
			this.ClientSize = new System.Drawing.Size(166, 70);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.panel1});
			this.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(80, 30);
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "NETMeter";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Move += new System.EventHandler(this.Form1_Move);
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.performanceCounter2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.log1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panelBandwidthGraphic.ResumeLayout(false);
			this.panelBandwidthLabels.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}


		private void timer1_Tick(object sender, System.EventArgs e)
		{
			LogData();

			showNotification();
		}


		static ArrayList rl = new ArrayList();
		static ArrayList sl = new ArrayList();

		/*
		 * Returns the average measure of the log
		 */
		private float GetAvg(ArrayList log)
		{
			float tot = 0;
			foreach (float n in log)
				tot += n;
			float avg = tot/log.Count;
			log.Clear();
			return avg;
		}

		
		static int logcounter = 0;

		Queue recvallq;
		Queue sendallq;

		/*
		 * Logs the bytes received/sent
		 */
		private void LogData()
		{
         
			if (logcounter < 1000/timer1.Interval)
			{
				try 
				{
					rl.Add( performanceCounter1.NextValue());
					sl.Add( performanceCounter2.NextValue());
					logcounter++;
				} 
				catch (System.InvalidOperationException){}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
				}
			}
			else 
			{
				DrawGraph();
				logcounter = 0;
				LogData();
			}
		}


		Pen up;
		Pen down;
		Pen both;
		Pen h;
		Brush textc;

		int minutec = 0;

		/* 
		 * Return the COUNT last values from the queue
		 */
		private float GetLastValues(Queue queue, int count)
		{
			object[] x = queue.ToArray();
			float total = 0;
			for (int i = (x.Length > count) ? x.Length - count : 0 ; i < x.Length; i++)
			{
				total += (float)x[i];
			}
			return total;
		}

		
		private class FixedSizeQueue : Queue 
		{
			int maxcap;
			public FixedSizeQueue(int maxcap):base(maxcap)
			{
				this.maxcap = maxcap;
			}
			public override void Enqueue(object obj)
			{
				if (Count == maxcap)
					this.Dequeue();
				base.Enqueue(obj);
			}
		}

	
		int hidecounter = 0;

		/*
		 * Draws the graph
		 */
		private void DrawGraph()
		{
			Graphics g = Graphics.FromImage(bit);
			g.Clear(options.Background);
         
			float recv = GetAvg(rl)/linespeed;
			float send = GetAvg(sl)/linespeed;

			recvallq.Enqueue(recv);
			sendallq.Enqueue(send);
         
			if (options.HideWhenIdle)
			{
				if (GetLastValues(sendallq, 10) < 0.5 & 
					GetLastValues(recvallq, 10) < 0.5 & 
					hidecounter++ >= 600/options.LogInterval)
				{
					HideForm(false);
				}
				else ShowForm();
			}

			if (minutec == options.LogInterval - 1)
			{
				oleDbInsertCommand1.Parameters["Recv"].Value = 
					GetLastValues(recvallq, options.LogInterval) * linespeed;
				oleDbInsertCommand1.Parameters["Send"].Value = 
					GetLastValues(sendallq, options.LogInterval) * linespeed;

				// AFM - Added the new columns to be able to create the statistics reports
				DateTime oNow = DateTime.Now;
				oleDbInsertCommand1.Parameters["DateDay"].Value = oNow.Day;
				oleDbInsertCommand1.Parameters["DateMonth"].Value = oNow.Month;
				oleDbInsertCommand1.Parameters["DateYear"].Value = oNow.Year;
				oleDbInsertCommand1.Parameters["TimeHour"].Value = oNow.Hour;
				oleDbInsertCommand1.Parameters["TimeMinute"].Value = oNow.Minute;

				//we keep it open, this and the performance counter SUCKs CPU.
				oleDbInsertCommand1.ExecuteNonQuery();

				minutec = 0;
			}
			else 
			{
				minutec++;
			}

			// AFM - removed, since now the "Text" has its own panel
			//Text = "Down: " + (recv*linespeed/1024).ToString("f1") + " Up: " + 
			//   (send*linespeed/1024).ToString("f1");
			Text = "";
			label1.Text = "Down: " + (recv*linespeed/1024).ToString("f1") + " K  Up: " + 
				(send*linespeed/1024).ToString("f1") + " K";

			// AFM - notifier is enabled?
			if (options.EnableNotifier) 
			{
				volumeReceived += (recv * linespeed / 1024);
			}

			notifyIcon1.Text = Text;

			object[] rf = recvallq.ToArray();
			object[] sf = sendallq.ToArray();

			for (int i = 0; i < bit.Width & i < rf.Length; i++)
			{ 
				float recv2 = (float)rf[rf.Length - (1 + i)];
				float send2 = (float)sf[sf.Length - (1 + i)];
				float maxr = recv2;
				float minr = send2;
				Pen maxp = down;
				if (recv2 < send2)
				{
					maxp = up;
					maxr = send2;
					minr = recv2;
				}
         
				//Pen s = both;

				int max = Convert.ToInt32(bit.Height - (maxr * bit.Height / (1 + options.Overflow)));
				int min = Convert.ToInt32(bit.Height - (minr * bit.Height  / (1 + options.Overflow)));

				if (max < 0)
					max = 0;
				if (min < 0)
					min = 0;

				g.DrawLine(maxp,
					bit.Width - i - 1, bit.Height, 
					bit.Width - i - 1, max);
         
				g.DrawLine(both,
					bit.Width - i - 1, bit.Height, 
					bit.Width - i - 1, min);
			}

			pictureBox1.Image = bit; 
#if(WIP)
         //notify icon stuff
         icon = notifyIcon1.Icon.ToBitmap();
         Graphics g2 = Graphics.FromImage(icon);
         g2.Clear(Color.DimGray);
         //g2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
         g2.FillRectangle(up.Brush, 0, icon.Height, icon.Width/2F, icon.Height * (1 - recv));
         g2.FillRectangle(down.Brush, icon.Width/2F, icon.Height, icon.Width, icon.Height * (1 - send));
         Font f = new Font(Font.FontFamily, 8, GraphicsUnit.Pixel);
         g2.DrawString((recv*linespeed/1024).ToString("f1"), 
            f, 
            Brushes.White, 0, 0);
         notifyIcon1.Icon = Icon.FromHandle(icon.GetHicon());

         
         g2.Dispose();
         f.Dispose();
#endif
			g.Dispose();
		}

		private void pictureBox1_Resize(object sender, System.EventArgs e)
		{
			// AFM - changed, since now I have the "Text" panel
			if (pictureBox1.Height > 10) 
			{
				bit = new Bitmap(bit, pictureBox1.Width - 2,pictureBox1.Height - 2);
				options.Size = Size;
			}
//			bit = new Bitmap(bit, pictureBox1.Width - 2,pictureBox1.Height - 2);
//			options.Size = Size;
		}


		bool mousedown = false;
		Point mousept;
		bool briteness = false;

		private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			switch (e.Button)
			{
				case MouseButtons.Left:
					briteness = false;
					Cursor = Cursors.SizeAll;
					mousept = new Point(e.X, e.Y);
					mousedown = true;
					break;
				case MouseButtons.Middle:
					if (briteness)
					{
						Cursor = Cursors.Default;
						briteness = false;
					}
					else 
					{
						Cursor = Cursors.NoMoveVert;
						briteness = true;
					}
					break;
				default:
					break;
			}
			//clear the counter so it wont hide so quick
			hidecounter = 0;
		}

		private void pictureBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (mousedown)
			{
				Opacity = options.Transparency - 0.3F;
				Location = new Point(Location.X - (mousept.X - e.X), Location.Y - (mousept.Y - e.Y)); 
			}
		}

		private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{      
			if (e.Button == MouseButtons.Left)
			{
				Cursor = Cursors.Default;
				mousedown = false;
				Opacity = options.Transparency;
			}
			//clear the counter so it wont hide so quick, incase we were holding the mouse button in too long
			hidecounter = 0;
		}


		#region Options and other

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			ShowOptions();
		}

		private void ShowOptions()
		{
			OptionForm opt = new OptionForm(options);
			opt.ShowDialog(this);
			opt.Dispose();
			if (options.Interface == "")
			{
				MessageBox.Show(this, "Please select a valid interface",
					"User Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				ShowOptions();
			}
			else 
			{
				//Options.Save("MyDUMeter.config", options);
				Options.Save(options);
				ReApplyOptions();
			}
		}

		private void ReApplyOptions()
		{
			performanceCounter1.InstanceName = options.Interface;
			performanceCounter2.InstanceName = options.Interface;
			performanceCounter1.MachineName = options.MachineName;
			performanceCounter2.MachineName = options.MachineName;
			linespeed = (int)options.LineSpeed/8;
			Opacity = options.Transparency;
			Location = options.Location;
			
			menuItemBDGraphics.Checked = options.DisplayKind == BandwidthDisplay.Graphic;
			menuItemBDText.Checked = options.DisplayKind == BandwidthDisplay.Text;
			menuItemBDBoth.Checked = options.DisplayKind == BandwidthDisplay.Both;
			ReApplyBandwidthDisplay();
			
			Size = options.Size;
			if (up != null) up.Dispose();
			if (down != null) down.Dispose();
			if (both != null) both.Dispose();
			if (textc != null) textc.Dispose();
			if (h != null) h.Dispose();
			up = new Pen(options.Up);
			down = new Pen(options.Down);
			both = new Pen(options.Both);
			textc = new SolidBrush(options.Text);
			h = new Pen(options.Lines);
		}

		/// <summary>
		/// Updates the visual bandwidth display kind
		/// </summary>
		private void ReApplyBandwidthDisplay() 
		{
			panelBandwidthGraphic.Visible = menuItemBDGraphics.Checked || menuItemBDBoth.Checked;
			panelBandwidthLabels.Visible = menuItemBDText.Checked || menuItemBDBoth.Checked;
			if (!panelBandwidthGraphic.Visible) 
			{
				panelBandwidthLabels.Dock = DockStyle.Fill;
			} 
			else 
			{
				panelBandwidthLabels.Dock = DockStyle.Bottom;
			}
		}


		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void AppExit(object sender, EventArgs e)
		{
			oleDbConnection1.Close();
			//Options.Save("MyDUMeter.config", options);
			Options.Save(options);
		}

		private void Form1_Move(object sender, System.EventArgs e)
		{
			options.Location = Location;
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			try 
			{
				//Options.Load("MyDUMeter.config", options);
				Options.Load(options);
				ReApplyOptions();
			}
			catch (System.IO.FileNotFoundException)
			{
				ShowOptions();
			}
			finally
			{
				timer1.Start();
				ICollection thds = Process.GetCurrentProcess().Threads;
				foreach (ProcessThread pt in thds)
					pt.PriorityLevel = ThreadPriorityLevel.BelowNormal;
			}       
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			NETMeter.About about = new NETMeter.About();
			about.ShowDialog();
			about.Dispose();
		}

      #endregion

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			HideForm(true);
		}

		private void HideForm(bool force)
		{
			if (Visible)
			{
				if (force) hidecounter = 600/options.LogInterval;
				//else forewin = GetForeGroundWindow();
				notifyIcon1.Visible = true;
				this.Hide();
			}
		}

		private void ShowForm()
		{
			if (!Visible)
			{
				hidecounter = 0;
				notifyIcon1.Visible = false;

				IntPtr fw = GetForegroundWindow(); 
#if DEBUG
				if (fw == IntPtr.Zero)
					Trace.WriteLine("Window not get", "GUI");

				if (fw == Handle)
					Trace.WriteLine("I should not have focus here!", "GUI");
#endif   
				this.Show();

				if (fw != IntPtr.Zero)
					if (!SetForegroundWindow(fw))
						Trace.WriteLine("Window not set", "GUI");
			}
		}

		[System.Runtime.InteropServices.DllImport("User32")]
		private static extern IntPtr GetForegroundWindow();

		[System.Runtime.InteropServices.DllImport("User32")]
		private static extern bool SetForegroundWindow(
			IntPtr hWnd   // handle to window
			);

		private void notifyIcon1_Click(object sender, System.EventArgs e)
		{
			ShowForm();
		}

		private void pictureBox1_DoubleClick(object sender, System.EventArgs e)
		{
			if (FormBorderStyle != FormBorderStyle.SizableToolWindow)
				FormBorderStyle = FormBorderStyle.SizableToolWindow;
			else 
				FormBorderStyle = FormBorderStyle.None;
		}

		
		static bool hover = false;

		private void pictureBox1_MouseHover(object sender, System.EventArgs e)
		{
			hover = true;
		}

		private void pictureBox1_MouseLeave(object sender, System.EventArgs e)
		{
			hover = false;
		}

		
		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			// draws a "imaginary bottom line" that delimites the bandwitdh
			h.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			e.Graphics.DrawLine(h,
				0, Convert.ToInt32(bit.Height * (options.Overflow)),
				bit.Width, Convert.ToInt32(bit.Height * (options.Overflow))
				);

			// draws a "timeline" of 60 seconds, from right to left (always finish with 60 seconds
			if (options.TimeIntervalLine != TimeLine.Disabled) 
			{
				h.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
				// AFM - now use the property
				for (int i = 0; i < bit.Width; i+= (int)options.TimeIntervalLine /*60*/) // 60 seconds
				{
					e.Graphics.DrawLine(h,
						bit.Width - i, 0 , bit.Width - i, bit.Height);
				}
			}

			// AFM - not necessary
			// draws the Text
//			if (hover)/// | options.AlwaysShowText)
//			{
				if (Text != "") 
				{							  
					StringFormat sf = new StringFormat();
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;
            
					SizeF s = e.Graphics.MeasureString(Text, Font);

					e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

					Font font = new Font(Font.FontFamily, Font.Size*(e.ClipRectangle.Width/s.Width));

					e.Graphics.DrawString(Text, 
						font,
						textc, e.ClipRectangle, sf);
					if (pinging)
					{
						sf.LineAlignment = StringAlignment.Far;

						e.Graphics.DrawString(lastping + "ms", 
							font,
							textc, e.ClipRectangle, sf);
					}
					sf.Dispose();
					font.Dispose();
//				}
			}
			if (briteness)
			{
				Brush selb = new SolidBrush(options.Selection);
				e.Graphics.FillRectangle(selb, 4 ,(float)(4 + (bit.Height)*(1 - Opacity)),
					bit.Width/20F, (bit.Height - 10) - (float)((bit.Height)*(1 - Opacity)));
				selb.Dispose();
			}
		}


      #region ReportInit

		private void menuItem3_Click_1(object sender, System.EventArgs e)
		{
			MakeReport(1);
		}

		private void MakeReport(int from)
		{
			oleDbSelectCommand1.Parameters["ID"].Value = DateTime.Now.AddHours(-from);
			int res = oleDbDataAdapter1.Fill(log1.RateLog);
			ReportForm report = new ReportForm(from, log1.RateLog, options);
			report.Show();
		}

		private void menuItem4_Click_1(object sender, System.EventArgs e)
		{
			MakeReport(12);
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			MakeReport(24);
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			MakeReport(72);
		}

		private void menuItem10_Click_1(object sender, System.EventArgs e)
		{
			MakeReport(3);
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			MakeReport(6);
		}
      #endregion

      #region Pinger

		Thread thread;
		bool pinging = false;

		private void StopPing()
		{
			if (pinging)
			{
				SaurabhPing.Ping.PingReply -= new SaurabhPing.Ping.PingEventHandler(OnPing);
				thread.Abort();
				thread = null;
				menuItemStartPing.Text = "Start ping";
				pinging = false;
			}
		}

		private void StartPing()
		{
			if (thread == null)
				InitThread();

			SaurabhPing.Ping.PingReply += new SaurabhPing.Ping.PingEventHandler(OnPing);
			menuItemStartPing.Text = "Stop ping";
			pinging = true;
			thread.Start();
		}

		int lastping = 0;

		private void StartPingThread()
		{
			while(true)
			{
				Thread.Sleep(options.PingInterval);
				ThreadPool.QueueUserWorkItem(  
					new WaitCallback(SaurabhPing.Ping.PingHost), options.PingEndPoint);
			}
		}

		private void OnPing(System.Net.EndPoint ep, int ms, int bytes)
		{
			lastping = ms;
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			if (pinging)
				StopPing();
			else
				StartPing();
		}

		private void InitThread()
		{
			thread = new Thread(new ThreadStart(StartPingThread));
			thread.Name = "Pinger";
			thread.ApartmentState = ApartmentState.STA;
		}
      #endregion



		/// <summary>
		/// Shows the Notification Tray
		/// </summary>
		private void showNotification() 
		{
			if ((options.EnableNotifier) & (volumeReceived > options.AmountVolume)) 		  
			{
				taskbarNotifier.Show("Amount Notification", "You reached your amount volume.", 500, 3000, 500);
				volumeReceived = 0;
			}
		}


		/// <summary>
		/// Shows the Statistics Reports
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemStatistics_Click(object sender, System.EventArgs e)
		{
			NETMeter.StatisticsReports report = new NETMeter.StatisticsReports(oleDbConnection1, options);			
			report.ShowDialog();
			report.Dispose();
			this.TopMost = false;
			this.TopMost = true;
		}


		/// <summary>
		/// When selecting a menu item
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItemBDBoth_Click(object sender, System.EventArgs e)
		{
			menuItemBDGraphics.Checked = sender == menuItemBDGraphics;
			menuItemBDText.Checked = sender == menuItemBDText;
			menuItemBDBoth.Checked = sender == menuItemBDBoth;

			ReApplyBandwidthDisplay();
		}

	}
}

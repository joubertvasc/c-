using System;
using System.Threading;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenNETCF.Tapi;
using System.IO;

namespace TapiApp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView lvDevices;
		private System.Windows.Forms.ColumnHeader hdrName;
		private System.Windows.Forms.ColumnHeader hdrVer;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox cbNoCallerID;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabProps;
		private System.Windows.Forms.TabPage tabLines;
		private System.Windows.Forms.TabPage tabCalls;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtManufacturer;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.TextBox txtRevision;
		private System.Windows.Forms.TextBox txtSerNo;
		private System.Windows.Forms.TextBox txtSubNo;
		private System.Windows.Forms.Label lblLineName;
		private System.Windows.Forms.Label lblProvider;
		private System.Windows.Forms.Button btnMakeAsstCall;
		private System.Windows.Forms.TextBox txtPhoneNum;
		private System.Windows.Forms.TextBox txtSysType;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.MainMenu mainMenu1;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.lvDevices = new System.Windows.Forms.ListView();
            this.hdrName = new System.Windows.Forms.ColumnHeader();
            this.hdrVer = new System.Windows.Forms.ColumnHeader();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cbNoCallerID = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLines = new System.Windows.Forms.TabPage();
            this.tabCalls = new System.Windows.Forms.TabPage();
            this.txtPhoneNum = new System.Windows.Forms.TextBox();
            this.btnMakeAsstCall = new System.Windows.Forms.Button();
            this.tabProps = new System.Windows.Forms.TabPage();
            this.lblLineName = new System.Windows.Forms.Label();
            this.txtManufacturer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.txtRevision = new System.Windows.Forms.TextBox();
            this.txtSerNo = new System.Windows.Forms.TextBox();
            this.txtSubNo = new System.Windows.Forms.TextBox();
            this.lblProvider = new System.Windows.Forms.Label();
            this.txtSysType = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabLines.SuspendLayout();
            this.tabCalls.SuspendLayout();
            this.tabProps.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvDevices
            // 
            this.lvDevices.Columns.Add(this.hdrName);
            this.lvDevices.Columns.Add(this.hdrVer);
            this.lvDevices.Location = new System.Drawing.Point(0, 0);
            this.lvDevices.Name = "lvDevices";
            this.lvDevices.Size = new System.Drawing.Size(236, 112);
            this.lvDevices.TabIndex = 0;
            this.lvDevices.View = System.Windows.Forms.View.Details;
            this.lvDevices.SelectedIndexChanged += new System.EventHandler(this.lvDevices_SelectedIndexChanged);
            // 
            // hdrName
            // 
            this.hdrName.Text = "Name";
            this.hdrName.Width = 100;
            // 
            // hdrVer
            // 
            this.hdrVer.Text = "Ver";
            this.hdrVer.Width = 60;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.listBox1.Location = new System.Drawing.Point(0, 8);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(240, 106);
            this.listBox1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.button1.Location = new System.Drawing.Point(0, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 20);
            this.button1.TabIndex = 2;
            this.button1.Text = "Make Call";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbNoCallerID
            // 
            this.cbNoCallerID.Checked = true;
            this.cbNoCallerID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNoCallerID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.cbNoCallerID.Location = new System.Drawing.Point(0, 120);
            this.cbNoCallerID.Name = "cbNoCallerID";
            this.cbNoCallerID.Size = new System.Drawing.Size(112, 20);
            this.cbNoCallerID.TabIndex = 3;
            this.cbNoCallerID.Text = "Supress Caller ID";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabLines);
            this.tabControl1.Controls.Add(this.tabCalls);
            this.tabControl1.Controls.Add(this.tabProps);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 248);
            this.tabControl1.TabIndex = 0;
            // 
            // tabLines
            // 
            this.tabLines.Controls.Add(this.lvDevices);
            this.tabLines.Location = new System.Drawing.Point(0, 0);
            this.tabLines.Name = "tabLines";
            this.tabLines.Size = new System.Drawing.Size(240, 225);
            this.tabLines.Text = "Lines";
            // 
            // tabCalls
            // 
            this.tabCalls.Controls.Add(this.txtPhoneNum);
            this.tabCalls.Controls.Add(this.btnMakeAsstCall);
            this.tabCalls.Controls.Add(this.button1);
            this.tabCalls.Controls.Add(this.cbNoCallerID);
            this.tabCalls.Controls.Add(this.listBox1);
            this.tabCalls.Location = new System.Drawing.Point(0, 0);
            this.tabCalls.Name = "tabCalls";
            this.tabCalls.Size = new System.Drawing.Size(240, 225);
            this.tabCalls.Text = "Calls";
            // 
            // txtPhoneNum
            // 
            this.txtPhoneNum.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPhoneNum.Location = new System.Drawing.Point(0, 168);
            this.txtPhoneNum.Name = "txtPhoneNum";
            this.txtPhoneNum.Size = new System.Drawing.Size(100, 19);
            this.txtPhoneNum.TabIndex = 0;
            this.txtPhoneNum.Text = "6503061417";
            // 
            // btnMakeAsstCall
            // 
            this.btnMakeAsstCall.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnMakeAsstCall.Location = new System.Drawing.Point(0, 192);
            this.btnMakeAsstCall.Name = "btnMakeAsstCall";
            this.btnMakeAsstCall.Size = new System.Drawing.Size(88, 20);
            this.btnMakeAsstCall.TabIndex = 1;
            this.btnMakeAsstCall.Text = "Make asst call";
            this.btnMakeAsstCall.Click += new System.EventHandler(this.btnMakeAsstCall_Click);
            // 
            // tabProps
            // 
            this.tabProps.Controls.Add(this.lblLineName);
            this.tabProps.Controls.Add(this.txtManufacturer);
            this.tabProps.Controls.Add(this.label1);
            this.tabProps.Controls.Add(this.label2);
            this.tabProps.Controls.Add(this.label3);
            this.tabProps.Controls.Add(this.label4);
            this.tabProps.Controls.Add(this.label5);
            this.tabProps.Controls.Add(this.txtModel);
            this.tabProps.Controls.Add(this.txtRevision);
            this.tabProps.Controls.Add(this.txtSerNo);
            this.tabProps.Controls.Add(this.txtSubNo);
            this.tabProps.Controls.Add(this.lblProvider);
            this.tabProps.Controls.Add(this.txtSysType);
            this.tabProps.Controls.Add(this.label6);
            this.tabProps.Location = new System.Drawing.Point(0, 0);
            this.tabProps.Name = "tabProps";
            this.tabProps.Size = new System.Drawing.Size(240, 225);
            this.tabProps.Text = "Properties";
            // 
            // lblLineName
            // 
            this.lblLineName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblLineName.Location = new System.Drawing.Point(16, 8);
            this.lblLineName.Name = "lblLineName";
            this.lblLineName.Size = new System.Drawing.Size(208, 16);
            // 
            // txtManufacturer
            // 
            this.txtManufacturer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.txtManufacturer.Location = new System.Drawing.Point(112, 64);
            this.txtManufacturer.Name = "txtManufacturer";
            this.txtManufacturer.ReadOnly = true;
            this.txtManufacturer.Size = new System.Drawing.Size(100, 20);
            this.txtManufacturer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(0, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.Text = "Manufacturer";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(0, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Model";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(0, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Revision";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(0, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.Text = "Serial number";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(0, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 20);
            this.label5.Text = "Subscriber number";
            // 
            // txtModel
            // 
            this.txtModel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.txtModel.Location = new System.Drawing.Point(112, 88);
            this.txtModel.Name = "txtModel";
            this.txtModel.ReadOnly = true;
            this.txtModel.Size = new System.Drawing.Size(100, 20);
            this.txtModel.TabIndex = 7;
            // 
            // txtRevision
            // 
            this.txtRevision.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.txtRevision.Location = new System.Drawing.Point(112, 112);
            this.txtRevision.Name = "txtRevision";
            this.txtRevision.ReadOnly = true;
            this.txtRevision.Size = new System.Drawing.Size(100, 20);
            this.txtRevision.TabIndex = 8;
            // 
            // txtSerNo
            // 
            this.txtSerNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.txtSerNo.Location = new System.Drawing.Point(112, 128);
            this.txtSerNo.Name = "txtSerNo";
            this.txtSerNo.ReadOnly = true;
            this.txtSerNo.Size = new System.Drawing.Size(100, 20);
            this.txtSerNo.TabIndex = 9;
            // 
            // txtSubNo
            // 
            this.txtSubNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular);
            this.txtSubNo.Location = new System.Drawing.Point(112, 152);
            this.txtSubNo.Name = "txtSubNo";
            this.txtSubNo.ReadOnly = true;
            this.txtSubNo.Size = new System.Drawing.Size(100, 20);
            this.txtSubNo.TabIndex = 10;
            // 
            // lblProvider
            // 
            this.lblProvider.Location = new System.Drawing.Point(16, 24);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(208, 16);
            // 
            // txtSysType
            // 
            this.txtSysType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSysType.Location = new System.Drawing.Point(112, 176);
            this.txtSysType.Name = "txtSysType";
            this.txtSysType.ReadOnly = true;
            this.txtSysType.Size = new System.Drawing.Size(100, 19);
            this.txtSysType.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(0, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 20);
            this.label6.Text = "System type";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "TapiApp";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
            this.tabControl1.ResumeLayout(false);
            this.tabLines.ResumeLayout(false);
            this.tabCalls.ResumeLayout(false);
            this.tabProps.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new Form1());
		}

		#region Tapi-related variables

		Tapi tapi;
		Line line;
		Call call;
		Hashtable DeviceCaps;

		#endregion

		private void Form1_Load(object sender, System.EventArgs e)
		{
			int ret = 0;
			tapi = new Tapi();
			tapi.Initialize();
			tapi.LineMessage += new Tapi.MessageHandler(tapi_LineMessage);
			DeviceCaps = new Hashtable();
			for( int i = 0; i < tapi.NumDevices; i++ )
			{
				LINEDEVCAPS dc = new LINEDEVCAPS(1024);
				dc.Store();
				int dwVersion = tapi.NegotiateVersion(i);
				ret = NativeTapi.lineGetDevCaps(tapi.hLineApp, i, dwVersion, 0, dc.Data);
				if ( ret < 0 )
					MessageBox.Show(((LINEERR)ret).ToString());
				if ( (LINEERR)ret == LINEERR.STRUCTURETOOSMALL )
				{
					dc.Data = new byte[dc.dwNeededSize];
					ret = NativeTapi.lineGetDevCaps(tapi.hLineApp, i, dwVersion, 0, dc.Data);
				}
				dc.Load();
				lvDevices.Items.Add(new ListViewItemExt(dc.LineName, dc));
				DeviceCaps.Add(i, dc);

				LINEADDRESSCAPS ac = new LINEADDRESSCAPS(1024);
				ac.Store();
				ret = NativeTapi.lineGetAddressCaps(tapi.hLineApp, i, 0, dwVersion, 0, ac.Data);
				ac.Load();
				ac = null;


			}

			// Try opening the cellular line

			int deviceIDCell = -1;

			foreach (int i in DeviceCaps.Keys)
			{
				LINEDEVCAPS dc = DeviceCaps[i] as LINEDEVCAPS;

				if (dc != null && dc.ProviderName == CellTSP.CELLTSP_PROVIDERINFO_STRING)
				{
					deviceIDCell = i;
					break;
				}
			}

			if (deviceIDCell == -1) // Not found
			{
				MessageBox.Show("Could not find cellular line");
				return;
			}

			lvDevices.Items[deviceIDCell].Selected = true;
			lvDevices_SelectedIndexChanged(null, new EventArgs());

		}

		// Cleanup
		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			tapi.Shutdown();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if ( line == null )
			{
				MessageBox.Show("No open line");
				return;
			}

			try
			{
				LINECALLPARAMS lp = new LINECALLPARAMS(Marshal.SizeOf(typeof(LINECALLPARAMS)) + Marshal.SizeOf(typeof(LINECALLPARAMSDEVSPECIFIC)));
				lp.dwDevSpecificOffset = Marshal.SizeOf(typeof(LINECALLPARAMS));
				lp.dwDevSpecificSize = Marshal.SizeOf(typeof(LINECALLPARAMSDEVSPECIFIC));
				lp.Store();
				LINECALLPARAMSDEVSPECIFIC lcpds = new LINECALLPARAMSDEVSPECIFIC();
				lcpds.cidoOptions = cbNoCallerID.Checked? CALLER_ID_OPTIONS.BLOCK: CALLER_ID_OPTIONS.DEFAULT;
				int index = lp.dwDevSpecificOffset;
				ByteCopy.StructToByteArray(lcpds, ref index, lp.Data);
				//line.BeginMakeCall(txtPhoneNum.Text, 1, lp.Data, new AsyncCallback(OnMakeCall), this);
				line.MakeCall(txtPhoneNum.Text, 1, lp.Data);
			}
			catch(TapiException ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void OnMakeCall(IAsyncResult ar)
		{
			call = line.EndMakeCall(ar);
			MessageBox.Show("Call made. Call state is " + call.State.ToString());
			call.Hangup();
			call.Dispose();
			call = null;
		}

		LINEMESSAGE message;
		private void tapi_LineMessage(LINEMESSAGE msg)
		{
			message = msg;
			listBox1.Invoke(new EventHandler(OnTapiMessage));
		}

		private void OnTapiMessage(object sender, EventArgs e)
		{
			listBox1.Items.Add(message.ToString());
		}
		private void line_NewCall(Call call)
		{
			//MessageBox.Show("New call has arrived");
			this.call = call;
			this.call.CallState += new OpenNETCF.Tapi.Call.CallStateHandler(call_CallState);
		}

		private void lvDevices_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( lvDevices.SelectedIndices.Count != 1 || lvDevices.SelectedIndices[0] < 0 )
				return;
			if ( line != null )
				line.Dispose();
			line = null;
			LINEDEVCAPS dc = (lvDevices.Items[lvDevices.SelectedIndices[0]] as ListViewItemExt).Tag as LINEDEVCAPS;
			LINEMEDIAMODE mode = (dc.dwMediaModes & LINEMEDIAMODE.INTERACTIVEVOICE) != 0? LINEMEDIAMODE.INTERACTIVEVOICE: LINEMEDIAMODE.DATAMODEM;
			try
			{
				line = tapi.CreateLine(lvDevices.SelectedIndices[0], mode, LINECALLPRIVILEGE.OWNER|LINECALLPRIVILEGE.MONITOR);
				line.NewCall += new Line.NewCallHandler(line_NewCall);
				int ret = NativeTapi.lineSetStatusMessages(line.hLine, (LINEDEVSTATE)(-1), (LINEADDRESSSTATE)(-1));

				lblLineName.Text = dc.LineName;
				lblProvider.Text = dc.ProviderName;

				if ( dc.ProviderName == CellTSP.CELLTSP_PROVIDERINFO_STRING )
				{
					LINEOPERATOR lop = new LINEOPERATOR();
					byte[] data = new byte[lop.SizeOf];
					ret = CellTSP.lineGetCurrentOperator(line.hLine, data);
					ByteCopy.ByteArrayToStruct(data, lop);
					LINEGENERALINFO lgi = new LINEGENERALINFO(1024);
					lgi.Store();
					ret = CellTSP.lineGetGeneralInfo(line.hLine, lgi.Data);
					lgi.Load();
					txtManufacturer.Text = lgi.Manufacturer;
					txtModel.Text = lgi.Model;
					txtRevision.Text = lgi.Revision;
					txtSerNo.Text = lgi.SerialNumber;
					txtSubNo.Text = lgi.SubscriberNumber;
					LINESYSTEMTYPE sysType;
					CellTSP.lineGetCurrentSystemType(line.hLine, out sysType);
					txtSysType.Text = sysType.ToString();
					lgi = null;

//					VARSTRING DeviceID = new VARSTRING(100);
//					DeviceID.Store();
//					NativeTapi.lineGetID(line.hLine, 0, IntPtr.Zero, LINECALLSELECT.LINE, DeviceID.Data, "ndis");
//					DeviceID.Load();
//					int id = BitConverter.ToInt32(DeviceID.Data, DeviceID.dwStringOffset);
//					MessageBox.Show(id.ToString());
//
//					CELLDEVCONFIG cfg = new CELLDEVCONFIG(1024);
//					cfg.Store();
//					ret = NativeTapi.lineGetDevConfig(id, cfg.Data, "ndis");
//					cfg.Load();
//					MessageBox.Show("BearerInfo flag is: " + cfg.bBearerInfoValid.ToString());
				}
				else
				{
					txtManufacturer.Text = "N/A";
					txtModel.Text = "N/A";
					txtRevision.Text = "N/A";
					txtSerNo.Text = "N/A";
					txtSubNo.Text = "N/A";
					txtSysType.Text = "N/A";
				}
			}
			catch(TapiException ex)
			{
				MessageBox.Show("Failed to open line: " + ex.ToString());
			}
		}

		private void call_CallState(Call call, LINECALLSTATE state)
		{
			listBox1.Items.Add(string.Format("New call state:" + state.ToString()));
			if ( state == LINECALLSTATE.IDLE )
			{
				call.Dispose();
				call = null;
			}
		}

		// This is how we use assisted Tapi
		// Note that the string "Bob" will appear in the call log.
		private void btnMakeAsstCall_Click(object sender, System.EventArgs e)
		{
			tapiRequestMakeCall(txtPhoneNum.Text, null, "Bob", null);
		}

		[DllImport("cellcore.dll")]
		extern static int tapiRequestMakeCall(string sAddr, string lpszAppName,
			string lpszCalledParty,
			string lpszComment );
	}


	// Helper class to allow associating some data with ListView items
	class ListViewItemExt: ListViewItem
	{
		public ListViewItemExt(string[] items): base(items)
		{
		}
		public ListViewItemExt(string[] items, object tag): base(items)
		{
			this.tag = tag;
		}
		public ListViewItemExt(string text, object tag): base(text)
		{
			this.tag = tag;
		}
		private object tag;
		public object Tag { get { return tag; } set { tag = value; } }
	}



}

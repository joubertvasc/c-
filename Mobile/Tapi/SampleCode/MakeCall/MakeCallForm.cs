using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using OpenNETCF.Tapi;

namespace MakeCall
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMakeCall : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox txtPhoneNumber;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnMakeCallSync;
		private System.Windows.Forms.ListBox lbLines;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lbMessages;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox cbSync;
		private System.Windows.Forms.CheckBox cbNoCallerID;
		private System.Windows.Forms.CheckBox cbPhoneOn;
		private System.Windows.Forms.MainMenu mainMenu1;

		public frmMakeCall()
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
			this.txtPhoneNumber = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnMakeCallSync = new System.Windows.Forms.Button();
			this.lbLines = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lbMessages = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbSync = new System.Windows.Forms.CheckBox();
			this.cbNoCallerID = new System.Windows.Forms.CheckBox();
			this.cbPhoneOn = new System.Windows.Forms.CheckBox();
			// 
			// txtPhoneNumber
			// 
			this.txtPhoneNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.txtPhoneNumber.Location = new System.Drawing.Point(80, 0);
			this.txtPhoneNumber.Size = new System.Drawing.Size(160, 20);
			this.txtPhoneNumber.Text = "6503061417";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.label1.Size = new System.Drawing.Size(80, 20);
			this.label1.Text = "Phone Number:";
			// 
			// btnMakeCallSync
			// 
			this.btnMakeCallSync.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.btnMakeCallSync.Location = new System.Drawing.Point(0, 24);
			this.btnMakeCallSync.Size = new System.Drawing.Size(80, 20);
			this.btnMakeCallSync.Text = "Make call";
			this.btnMakeCallSync.Click += new System.EventHandler(this.btnMakeCallSync_Click);
			// 
			// lbLines
			// 
			this.lbLines.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.lbLines.Location = new System.Drawing.Point(0, 88);
			this.lbLines.Size = new System.Drawing.Size(240, 67);
			this.lbLines.SelectedIndexChanged += new System.EventHandler(this.lbLines_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.label2.Location = new System.Drawing.Point(0, 64);
			this.label2.Size = new System.Drawing.Size(80, 20);
			this.label2.Text = "Lines:";
			// 
			// lbMessages
			// 
			this.lbMessages.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.lbMessages.Location = new System.Drawing.Point(0, 176);
			this.lbMessages.Size = new System.Drawing.Size(240, 93);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.label3.Location = new System.Drawing.Point(0, 160);
			this.label3.Size = new System.Drawing.Size(80, 20);
			this.label3.Text = "Messages:";
			// 
			// cbSync
			// 
			this.cbSync.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.cbSync.Location = new System.Drawing.Point(88, 24);
			this.cbSync.Text = "Synchronous";
			// 
			// cbNoCallerID
			// 
			this.cbNoCallerID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.cbNoCallerID.Location = new System.Drawing.Point(88, 40);
			this.cbNoCallerID.Size = new System.Drawing.Size(104, 20);
			this.cbNoCallerID.Text = "Supress caller ID";
			// 
			// cbPhoneOn
			// 
			this.cbPhoneOn.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
			this.cbPhoneOn.Location = new System.Drawing.Point(88, 56);
			this.cbPhoneOn.Size = new System.Drawing.Size(120, 20);
			this.cbPhoneOn.Text = "Phone on";
			this.cbPhoneOn.CheckStateChanged += new System.EventHandler(this.cbPhoneOn_CheckStateChanged);
			// 
			// frmMakeCall
			// 
			this.Controls.Add(this.cbPhoneOn);
			this.Controls.Add(this.cbSync);
			this.Controls.Add(this.lbLines);
			this.Controls.Add(this.btnMakeCallSync);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtPhoneNumber);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lbMessages);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cbNoCallerID);
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Text = "MakeCall";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMakeCall_Closing);
			this.Load += new System.EventHandler(this.frmMakeCall_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new frmMakeCall());
		}

		#region Private Data
		Tapi tapi;
		Line line;
		Call call;
		#endregion

		private void frmMakeCall_Load(object sender, System.EventArgs e)
		{
			// Open Tapi
			tapi = new Tapi();
			tapi.Initialize();
			tapi.LineMessage += new OpenNETCF.Tapi.Tapi.MessageHandler(tapi_LineMessage);
			PopulateLines();
		}

		private void frmMakeCall_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Stop tapi
			tapi.Shutdown();
		}

		private void PopulateLines()
		{
			for ( int i = 0; i < tapi.NumDevices; i++ )
			{
				LINEDEVCAPS dc;
				LINEERR ret = tapi.GetDevCaps(i, out dc);
				lbLines.Items.Add(new LineDescriptor(i, dc));
			}
		}

		private void lbLines_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Select a new line
			if ( line != null )
			{
				line.Dispose();
				line = null;
			}

			// Sanity check
			if ( lbLines.SelectedIndex < 0 || lbLines.SelectedIndex >= tapi.NumDevices )
				return;

			int id = (lbLines.SelectedItem as LineDescriptor).ID;
			LINEDEVCAPS dc = (lbLines.SelectedItem as LineDescriptor).Caps;
			// media mode to use is what we want intersected with what line supports
			LINEMEDIAMODE mode = (LINEMEDIAMODE.INTERACTIVEVOICE|LINEMEDIAMODE.DATAMODEM) & dc.dwMediaModes;
			try
			{
				line = tapi.CreateLine(id, mode, LINECALLPRIVILEGE.MONITOR|LINECALLPRIVILEGE.OWNER);
			}
			catch(TapiException ex)
			{
				MessageBox.Show(ex.ToString());
				lbLines.SelectedIndex = -1;
			}

			LINEEQUIPSTATE es;
			LINERADIOSUPPORT rs;
			NativeTapi.lineGetEquipmentState(line.hLine, out es, out rs);
			MessageBox.Show(es.ToString(), rs.ToString());
			cbPhoneOn.Checked = ( es == LINEEQUIPSTATE.FULL );
		}

		private void tapi_LineMessage(LINEMESSAGE msg)
		{
			// Add new line and scroll it into view
//			lbMessages.Items.Add(msg.ToString());
//			lbMessages.SelectedIndex = lbMessages.Items.Count - 1;
		}

		private void btnMakeCallSync_Click(object sender, System.EventArgs e)
		{
			if ( line == null )
			{
				MessageBox.Show("No line selected");
				return;
			}

			if ( cbSync.Checked )
			{
				//make synchronous call
				call = line.MakeCall(txtPhoneNumber.Text, 1, cbNoCallerID.Checked);
				MessageBox.Show("Call made. Call state is " + call.State.ToString());
			}
			else
			{
				//make asynchronous call
				IAsyncResult ar = line.BeginMakeCall(txtPhoneNumber.Text, 1, null, new AsyncCallback(OnCallMade), this);
			}
		}

		private void OnCallMade(IAsyncResult ar)
		{
			call = line.EndMakeCall(ar);
			MessageBox.Show("Async call made. Call state is " + call.State.ToString());
			call.Hangup();
			call.Dispose();
			call = null;
		}

		private void cbPhoneOn_CheckStateChanged(object sender, System.EventArgs e)
		{
			if ( line == null )
			{
				MessageBox.Show("No line is selected");
				return;
			}
			if ( cbPhoneOn.Checked )
				NativeTapi.lineSetEquipmentState(line.hLine, LINEEQUIPSTATE.FULL);
			else
				NativeTapi.lineSetEquipmentState(line.hLine, LINEEQUIPSTATE.NOTXRX);
		}
	}

	public class LineDescriptor
	{
		private LINEDEVCAPS m_caps;
		private int m_id;
		public LineDescriptor( int id, LINEDEVCAPS caps)
		{
			m_id = id;
			m_caps = caps;
		}

		public override string ToString()
		{
			return m_caps == null? "Unknown": m_caps.LineName;
		}

		public LINEDEVCAPS Caps { get { return m_caps; } }
		public int ID { get { return m_id; } }

	}
}

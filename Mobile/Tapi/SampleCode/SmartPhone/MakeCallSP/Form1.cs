using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using OpenNETCF.Tapi;

namespace MakeCallSP
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;

		private Tapi tapi;
		private Line line;
		private System.Windows.Forms.TextBox txtPhoneNum;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MenuItem mnuDial;
		private System.Windows.Forms.MenuItem mnuHangup;
		private System.Windows.Forms.ComboBox cbLines;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtMessages;
		private System.Windows.Forms.CheckBox cbPhoneOn;
		private Call call;

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
			this.mnuDial = new System.Windows.Forms.MenuItem();
			this.mnuHangup = new System.Windows.Forms.MenuItem();
			this.txtPhoneNum = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cbLines = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMessages = new System.Windows.Forms.TextBox();
			this.cbPhoneOn = new System.Windows.Forms.CheckBox();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.mnuDial);
			this.mainMenu1.MenuItems.Add(this.mnuHangup);
			// 
			// mnuDial
			// 
			this.mnuDial.Text = "Dial";
			this.mnuDial.Click += new System.EventHandler(this.mnuDial_Click);
			// 
			// mnuHangup
			// 
			this.mnuHangup.Text = "Hangup";
			this.mnuHangup.Click += new System.EventHandler(this.mnuHangup_Click);
			// 
			// txtPhoneNum
			// 
			this.txtPhoneNum.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Regular);
			this.txtPhoneNum.Location = new System.Drawing.Point(0, 72);
			this.txtPhoneNum.Size = new System.Drawing.Size(176, 22);
			this.txtPhoneNum.Text = "6503061417";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(0, 48);
			this.label1.Size = new System.Drawing.Size(176, 22);
			this.label1.Text = "Phone:";
			// 
			// cbLines
			// 
			this.cbLines.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Regular);
			this.cbLines.Location = new System.Drawing.Point(0, 24);
			this.cbLines.Size = new System.Drawing.Size(176, 22);
			this.cbLines.SelectedIndexChanged += new System.EventHandler(this.cbLines_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Bold);
			this.label2.Size = new System.Drawing.Size(176, 22);
			this.label2.Text = "Lines:";
			// 
			// txtMessages
			// 
			this.txtMessages.Font = new System.Drawing.Font("Nina", 8F, System.Drawing.FontStyle.Regular);
			this.txtMessages.Location = new System.Drawing.Point(0, 120);
			this.txtMessages.Multiline = true;
			this.txtMessages.Size = new System.Drawing.Size(176, 56);
			this.txtMessages.Text = "";
			// 
			// cbPhoneOn
			// 
			this.cbPhoneOn.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Regular);
			this.cbPhoneOn.Location = new System.Drawing.Point(0, 96);
			this.cbPhoneOn.Size = new System.Drawing.Size(152, 16);
			this.cbPhoneOn.Text = "Phone on";
			this.cbPhoneOn.CheckStateChanged += new System.EventHandler(this.cbPhoneOn_CheckStateChanged);
			// 
			// Form1
			// 
			this.Controls.Add(this.cbPhoneOn);
			this.Controls.Add(this.txtMessages);
			this.Controls.Add(this.cbLines);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtPhoneNum);
			this.Controls.Add(this.label2);
			this.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Bold);
			this.Menu = this.mainMenu1;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			tapi = new Tapi();
			tapi.Initialize();
			tapi.LineMessage += new OpenNETCF.Tapi.Tapi.MessageHandler(tapi_LineMessage);
			int ret;
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
				cbLines.Items.Add(new LineDescriptor(i, dc));

				LINEADDRESSCAPS ac = new LINEADDRESSCAPS(1024);
				ac.Store();
				ret = NativeTapi.lineGetAddressCaps(tapi.hLineApp, i, 0, dwVersion, 0, ac.Data);
				ac.Load();
				ac = null;


			}
		}

		private void tapi_LineMessage(LINEMESSAGE msg)
		{
			txtMessages.Text += msg.ToString() + "\r\n";
		}

		private void mnuDial_Click(object sender, System.EventArgs e)
		{
			if ( line != null )
				line.Dispose();
			if ( cbLines.SelectedIndex < 0 )
				return;
			line = tapi.CreateLine((cbLines.SelectedItem as LineDescriptor).ID, LINEMEDIAMODE.INTERACTIVEVOICE, LINECALLPRIVILEGE.OWNER);
			call = line.MakeCall(txtPhoneNum.Text, 1, null );
		}

		private void mnuHangup_Click(object sender, System.EventArgs e)
		{
			if ( call != null )
			{
				call.Hangup();
				call = null;
			}
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

		private void cbLines_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( line != null )
				line.Dispose();
			line = tapi.CreateLine((cbLines.SelectedItem as LineDescriptor).ID, LINEMEDIAMODE.INTERACTIVEVOICE, LINECALLPRIVILEGE.OWNER);
			LINEEQUIPSTATE es;
			LINERADIOSUPPORT rs;
			int ret = NativeTapi.lineGetEquipmentState(line.hLine, out es, out rs);
			if ( ret == 0 )
			{
				MessageBox.Show(es.ToString(), rs.ToString());
				cbPhoneOn.Checked = ( es == LINEEQUIPSTATE.FULL );
			}
		
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

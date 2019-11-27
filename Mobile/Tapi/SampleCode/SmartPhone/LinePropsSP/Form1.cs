using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using OpenNETCF.Tapi;

namespace LinePropsSP
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;

		Tapi tapi;
		Line line;
		private System.Windows.Forms.TextBox txtManufacturer;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.TextBox txtRevision;
		private System.Windows.Forms.TextBox txtSerNo;
		private System.Windows.Forms.TextBox txtSubNo;
		private System.Windows.Forms.TextBox txtSysType;
		private System.Windows.Forms.ComboBox cbLines;
		private System.Windows.Forms.MenuItem menuItem1;
		Call call;

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
			this.cbLines = new System.Windows.Forms.ComboBox();
			this.txtManufacturer = new System.Windows.Forms.TextBox();
			this.txtModel = new System.Windows.Forms.TextBox();
			this.txtRevision = new System.Windows.Forms.TextBox();
			this.txtSerNo = new System.Windows.Forms.TextBox();
			this.txtSysType = new System.Windows.Forms.TextBox();
			this.txtSubNo = new System.Windows.Forms.TextBox();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.Add(this.menuItem1);
			// 
			// cbLines
			// 
			this.cbLines.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Regular);
			this.cbLines.Size = new System.Drawing.Size(176, 22);
			this.cbLines.SelectedIndexChanged += new System.EventHandler(this.cbLines_SelectedIndexChanged);
			// 
			// txtManufacturer
			// 
			this.txtManufacturer.Location = new System.Drawing.Point(0, 32);
			this.txtManufacturer.Size = new System.Drawing.Size(176, 25);
			this.txtManufacturer.Text = "";
			// 
			// txtModel
			// 
			this.txtModel.Location = new System.Drawing.Point(0, 56);
			this.txtModel.Size = new System.Drawing.Size(176, 25);
			this.txtModel.Text = "";
			// 
			// txtRevision
			// 
			this.txtRevision.Location = new System.Drawing.Point(0, 80);
			this.txtRevision.Size = new System.Drawing.Size(176, 25);
			this.txtRevision.Text = "";
			this.txtRevision.TextChanged += new System.EventHandler(this.txtRevision_TextChanged);
			// 
			// txtSerNo
			// 
			this.txtSerNo.Location = new System.Drawing.Point(0, 104);
			this.txtSerNo.Size = new System.Drawing.Size(176, 25);
			this.txtSerNo.Text = "";
			// 
			// txtSysType
			// 
			this.txtSysType.Location = new System.Drawing.Point(0, 128);
			this.txtSysType.Size = new System.Drawing.Size(176, 25);
			this.txtSysType.Text = "";
			// 
			// txtSubNo
			// 
			this.txtSubNo.Location = new System.Drawing.Point(0, 152);
			this.txtSubNo.Size = new System.Drawing.Size(176, 25);
			this.txtSubNo.Text = "";
			// 
			// menuItem1
			// 
			this.menuItem1.Text = "Quit";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// Form1
			// 
			this.Controls.Add(this.txtManufacturer);
			this.Controls.Add(this.cbLines);
			this.Controls.Add(this.txtModel);
			this.Controls.Add(this.txtRevision);
			this.Controls.Add(this.txtSerNo);
			this.Controls.Add(this.txtSysType);
			this.Controls.Add(this.txtSubNo);
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

		private void cbLines_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( line != null )
				line.Dispose();
			line = null;
			LINEDEVCAPS dc = (cbLines.SelectedItem as LineDescriptor).Caps;
			LINEMEDIAMODE mode = (dc.dwMediaModes & (LINEMEDIAMODE.INTERACTIVEVOICE|LINEMEDIAMODE.DATAMODEM)) != 0? LINEMEDIAMODE.INTERACTIVEVOICE: LINEMEDIAMODE.DATAMODEM;
			try
			{
				line = tapi.CreateLine((cbLines.SelectedItem as LineDescriptor).ID, mode, LINECALLPRIVILEGE.OWNER|LINECALLPRIVILEGE.MONITOR);
				int ret = NativeTapi.lineSetStatusMessages(line.hLine, (LINEDEVSTATE)(-1), (LINEADDRESSSTATE)(-1));

				//lblLineName.Text = dc.LineName;
				//lblProvider.Text = dc.ProviderName;

				if ( dc.ProviderName.ToLower().Contains(CellTSP.CELLTSP_PROVIDERINFO_STRING.ToLower()))
				{
					LINEOPERATOR lop = new LINEOPERATOR();
					byte[] data = new byte[lop.SizeOf];
					ret = CellTSP.lineGetCurrentOperator(line.hLine, data);
					ByteCopy.ByteArrayToStruct(data, lop);
					LINEGENERALINFO lgi = new LINEGENERALINFO(System.Runtime.InteropServices.Marshal.SizeOf(typeof(LINEGENERALINFO)));
					lgi.Store();
					ret = CellTSP.lineGetGeneralInfo(line.hLine, lgi.Data);
					lgi.Load();
					lgi = new LINEGENERALINFO(lgi.dwNeededSize);
					lgi.Store();
					ret = CellTSP.lineGetGeneralInfo(line.hLine, lgi.Data);
					lgi.Load();
					txtManufacturer.Text = lgi.Manufacturer;
					txtModel.Text = lgi.Model;
					txtRevision.Text = lgi.Revision;
					txtSerNo.Text = lgi.SerialNumber;
					txtSubNo.Text = lgi.SubscriberNumber;
					LINESYSTEMTYPE sysType;
					ret = CellTSP.lineGetCurrentSystemType(line.hLine, out sysType);
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

		private void tapi_LineMessage(LINEMESSAGE msg)
		{

		}

		private void txtRevision_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			Close();
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

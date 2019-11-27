/*
 * MyDUMeter - by leppie
 * Original Project.
 * 
 * NETMeter - by Alessandro Fragnani
 * ControlOptions.cs
 * 
 * Changes: 
 *  - Changed: LoadWithWindows property to save on "NETMeter" key, not "MyDUMeter" key on 
 *             registry
 *  - Added: Notifier Category, to show a "MSN Like Notifier" when somethings happens. Originally
 *           only has a "Volume amount reach", but could be much more fun to add other features
 *  - Added: "Time Interval Line" property to "Visual Options". It uses "Timeline" enum type to 
 *           let the user decides the distance between the "vertical lines" on the graph window.
 *  - Added: "Display Kind" property to "Visual Options". It uses "Bandwidth Display" enum type
 *           to let the user decides how is the appearance of the meter. Graphic, Text or Both.
 * 
 * Thanks to:
 *  - Javier Campos for his article "Description Enum TypeConverter"
 *    (http://www.codeproject.com/csharp/EnumDescConverter.asp)
 * 
 */ 

using System;
using System.Globalization;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Jcl.Util;

namespace MyDUMeter
{
	public enum LineSpeed 
	{
		Modem28800 = 28800,
		Modem33600 = 33600,
		Modem56k = 56000,
		ISDN64k = 64000,
		ISDN128k = 128000,
		DSL512k = 512000,
		DSL600k = 600000,
		DSL800k = 800000,
		DSL1_5m = 1024000 + DSL512k,
		LAN10m = 10 * 1024000,
		LAN100m = 100 * 1024000,
	}


	/// <summary>
	/// Enum with Description is AWESOME :)
	/// </summary>
	public enum TimeLine
	{
		[Description("Disabled")]			Disabled = 0,
		[Description("Every 10 seconds")]	Every10Seconds = 10,
		[Description("Every 20 seconds")]	Every20Seconds = 20,
		[Description("Every 30 seconds")]	Every30Seconds = 30,
		[Description("Every minute")]		Every60Seconds = 60
	}

	/// <summary>
	/// Kind of display
	/// </summary>
	public enum BandwidthDisplay
	{
		[Description("Graphics")]				Graphic = 0,
		[Description("Text")]					Text = 1,
		[Description("Both Graphic and Text")]	Both = 2
	}


	[DefaultProperty("Interface")]
	public class ControlOptions
	{
		public ControlOptions()
		{
		}

	
		private LineSpeed speed = LineSpeed.ISDN128k; 

		[Category("Interface Options")]
		public LineSpeed LineSpeed 
		{
			get {return speed;}
			set {speed = value;}
		}

		private Color downcolor = Color.Red;
		private Color upcolor = Color.Green;
		private Color bothcolor = Color.Yellow;
		private Color textcolor = Color.Black;
		private Color bc = SystemColors.Control;
		private Color lines = Color.DimGray;
		private Color selcolor = Color.Blue;

		[Category("Color Options")]
		public Color Lines
		{
			get {return lines;}
			set {lines = value;}
		}

		[Category("Color Options")]
		public Color Selection
		{
			get {return selcolor;}
			set {selcolor = value;}
		}

		[Category("Color Options")]
		public Color Background
		{
			get {return bc;}
			set {bc = value;}
		}

		[Category("Color Options")]
		public Color Text 
		{
			get {return textcolor;}
			set {textcolor = value;}
		}

		[Category("Color Options")]
		public Color Down 
		{
			get {return downcolor;}
			set {downcolor = value;}
		}

		[Category("Color Options")]
		public Color Up 
		{
			get {return upcolor;}
			set {upcolor = value;}
		}

		[Category("Color Options")]
		public Color Both 
		{
			get {return bothcolor;}
			set {bothcolor = value;}
		}

		private string instancename = "";

		[Description("Select an interface to be monitored")]
		[Category("Interface Options")]
		[TypeConverter(typeof(ControlOptions.InstanceConverter))]
		public string Interface 
		{
			get {return instancename;}
			set {instancename = value;}
		}
      
		class InstanceConverter : TypeConverter
		{
			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				ArrayList vals = new ArrayList();
				string[] instnames = new PerformanceCounterCategory("Network Interface", sname).GetInstanceNames();
				foreach (string i in instnames)
					vals.Add(i);
				return new StandardValuesCollection(vals);
			}

			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}

		internal static string sname = ".";

		private string pcname = ".";

		[Category("Interface Options")]
		[Description("Select the PC you would like to monitor")]
		[TypeConverter(typeof(ControlOptions.MachineConvertor))]
		public string MachineName 
		{
			get {return pcname;}
			set 
			{
				pcname = value;
				sname = value;
			}
		}

		[DllImport("Netapi32")]
		private static extern int NetServerEnum(
			string servername, //must be null
			int level, //101
			out IntPtr buffer,
			int maxlen, //out
			out int entriesread, //out
			out int totalentries, //out
			int servertype, //in 3 = all
			string domain, //null primary
			int resumehandle); //must be 0

		[DllImport("Netapi32")]
		private static extern int NetApiBufferFree(IntPtr ptr);

		[StructLayout(LayoutKind.Sequential)]
			internal class ServerInfo 
		{
			public int platformid;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string name;
			public int majorver;
			public int minorver;
			public int type;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string comment;
		}

		class MachineConvertor : TypeConverter 
		{
			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				int eread, etot;
				IntPtr buffer;
				int l = 20;     
				int size = Marshal.SizeOf(typeof(ServerInfo));
				int res = NetServerEnum(
					null, 
					101, 
					out buffer, 
					size * l, 
					out eread, 
					out etot, 
					3, 
					null, 
					0);
            
				IntPtr p = buffer;
				string[] arr = new string[etot];
				for (int i = 0; i < eread; i++)
				{
					ServerInfo si = Marshal.PtrToStructure(p, typeof(ServerInfo)) as ServerInfo;   
					if (si.majorver > 4)
						arr[i] = si.name;
					p = (IntPtr)((int)p + size);
				}
      
				res = NetApiBufferFree(buffer);
				return new StandardValuesCollection(arr);
			}

			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}

		private double opac = 0.7F;

		[Category("Visual Options"),
		Description("Select the transparence of the monitor window")]
		[TypeConverter(typeof(System.Windows.Forms.OpacityConverter))]
		public double Transparency 
		{
			get {return opac;}
			set {opac = value;}
		}

		private double overflow = 0.2F;

		[Category("Visual Options"),
		Description("Select the overflow amount to draw the horizontal line")]
		[TypeConverter(typeof(System.Windows.Forms.OpacityConverter))]
		public double Overflow 
		{
			get {return overflow;}
			set {overflow = value;}
		}

		private Point loc = new Point(0,0);

		[Browsable(false)]
		public Point Location 
		{
			get {return loc;}
			set {loc = value;}
		}

		private Size size = new Size(180,50);

		[Browsable(false)]
		public Size Size 
		{
			get {return size;}
			set {size = value;}
		}

		private bool loadwin = false;
		private bool checkreg = false;

		[Category("Windows Options"),
		Description("Select if you would like to have NETMeter starting with MSWindows")]
		public bool LoadWithWindows 
		{
			get 
			{
				if (!checkreg)
				{
					if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
						@"Software\Microsoft\Windows\CurrentVersion\Run").GetValue(
						"NETMeter") != null)
						loadwin = true;
					checkreg = true;
				}

				return loadwin;
			}
			set 
			{
				if (value)
					Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
						@"Software\Microsoft\Windows\CurrentVersion\Run", true).SetValue(
						"NETMeter", Application.ExecutablePath);
				else 
					Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
						@"Software\Microsoft\Windows\CurrentVersion\Run", true).DeleteValue(
						"NETMeter", false);
				loadwin = value;
			}
		}

		private string ping = "www.bbnplanet.net";

		[Category("Ping Options")]
		public string PingEndPoint 
		{
			get {return ping;}
			set {ping = value;}
		}

		private int pinginterval = 500;

		[Description("Period in millisenconds")]
		[Category("Ping Options")]
		public int PingInterval 
		{
			get {return pinginterval;}
			set {pinginterval = value;}
		}

		private bool hidewhenidle = false;

		[Category("Visual Options"),
		Description("Hides the monitor window if the active connection is idle")]
		public bool HideWhenIdle 
		{
			get {return hidewhenidle;}
			set {hidewhenidle = value;}
		}

		private int loginterval = 60;

		[
		ReadOnly(true),
		Description("Log interval in seconds"),
		Category("Log Options")]
		public int LogInterval 
		{
			get {return loginterval;}
			set {loginterval = value;}
		}



		// NETMeter customization --

		// Volume Notifier
		[Category("Notifier"),
		Description("Indicates if will appear a notifier when some amount of download is reached")]
		public bool EnableNotifier
		{
			get {return enableNotifier;}	    
			set {enableNotifier = value;}
		}

		private bool enableNotifier = true;


		[Category("Notifier"),
		Description("Select the amount volume in Kb/s to be notified")]
		public int AmountVolume 
		{
			get {return amountVolume;}
			set {amountVolume = value;}
		}

		private int amountVolume = 1024;


		// Timeline
		[Category("Visual Options"),		
		Description("Select the time interval for drawing the vertical lines on the graph")]
		[TypeConverter(typeof(Jcl.Util.EnumDescConverter))]
		public TimeLine TimeIntervalLine
		{
			get {return timeIntervalLine;}
			set {timeIntervalLine = value;}
		}

		private TimeLine timeIntervalLine = TimeLine.Every30Seconds;


		// bandwidth display
		private BandwidthDisplay displaykind = BandwidthDisplay.Both;

		[Category("Visual Options"),
		Description("Display kind for bandwidth values")]
		[TypeConverter(typeof(Jcl.Util.EnumDescConverter))]
		public BandwidthDisplay DisplayKind 
		{
			get {return displaykind;}
			set {displaykind = value;}
		}

	}
}

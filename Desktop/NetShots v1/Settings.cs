/*
 * NETShots - by Alessandro Fragnani
 * Settings.cs
 * Created: 28 march 2005
 * 
 * Settings Class that will be serialized
 * 
 * Originaly has:
 *  - The Basic Settings 
 * 
 * Thanks to:
 *  - Zenicom for his article "Load and Save objects to XML using serialization"
 *    (http://www.codeproject.com/csharp/xml_serializationasp.asp)
 * 
 */ 

using System;
using System.Xml.Serialization;  
using System.IO;			     
using System.Windows.Forms;

namespace NetShots
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	[XmlRootAttribute("Settings", Namespace="", IsNullable=false)]
	public class Settings
	{
		public static string XMLFileName
		{
			get 
			{
#if (DEBUG)
				return Application.StartupPath + "\\NETShots.xml";
#else
				string sDir = Application.LocalUserAppDataPath;
				DirectoryInfo di = Directory.GetParent(sDir);
				return di.FullName + "\\NETShots.xml";
#endif
			}
		}

		public Settings()
		{
			this.PlaceOnTray = true;
			this.UseAsWallpaper = true;
			this.WallpaperCycleEach = 15;
			this.UseAsScreenshot = true;
			this.ScreenshotStartsIn = 5;
			this.ScreenshotCycleEach = 10;
			this.LastWallpaperCycle = DateTime.Now;
		}

		[XmlArray ("Paths"), XmlArrayItem("Path", typeof(string))]
		public System.Collections.ArrayList Paths = new System.Collections.ArrayList();


		// tray
		public bool PlaceOnTray;



		// wallpaper
		public bool UseAsWallpaper;

		public int WallpaperCycleEach;



		// screenshot
		public bool UseAsScreenshot;

		public int ScreenshotStartsIn;

		public int ScreenshotCycleEach;



		// shortcuts
		public string ShortcutWallpaper;

		public string ShortcutScreenshot;


		[XmlAttributeAttribute(DataType="date")]
		public System.DateTime LastWallpaperCycle;

	}
}

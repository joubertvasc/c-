/*
 * NETShots - by Alessandro Fragnani
 * SettingsManager.cs
 * Created: 28 march 2005
 * 
 * A Manager class for the Settings class. Ok, I should think in implementing something like
 * a "Singleton" pattern. Maybe for version 2 :)
 * 
 * Originaly has:
 *  - The Basic Settings 
 * 
 * Thanks to:
 *  - Zenicom for his article "Load and Save objects to XML using serialization"
 *    (http://www.codeproject.com/csharp/xml_serializationasp.asp)
 *  - Rakesh Rajan for his article "How to develop a screen saver in C#"
 *    (http://www.codeproject.com/csharp/scrframework.asp)
 * 
 */ 

using System;
using Zenicom.XML;
using System.IO;
using Microsoft.Win32;

namespace NetShots
{
	/// <summary>
	/// A Global Object that holds the "settings"
	/// </summary>
	public class SettingsManager
	{

		public enum ExecutionKind {
			ekSetup,
			ekScreensaver,
			ekApplication
		}

		public static ExecutionKind executionKind = ExecutionKind.ekSetup;

		
		// the "settings" object
		public Settings settings = null;

		/// <summary>
		/// While creating the manager, creates the settings object, loading from the
		/// settings XML File if exists
		/// </summary>
		public SettingsManager()
		{
			if (File.Exists(Settings.XMLFileName)) 
			{
				Zenicom.XML.ObjectXMLSerializer objectXMLSerializer = new Zenicom.XML.ObjectXMLSerializer();			
		
				//Load the customer object from the XML file using our custom class...
				settings = new Settings();
				settings = (Settings)objectXMLSerializer.Load(settings, Settings.XMLFileName);
			}
			else // default
			{	
				settings = new Settings();
			}

			// good time to check if NETShots is the real screensaver
			CheckIfAmITheScreensaver();
		}

		/// <summary>
		/// Save the settings XML File
		/// </summary>
		public void SaveSettings() 
		{
			if (executionKind != ExecutionKind.ekScreensaver) 
			{
				//Save the customer object to the XML file using our custom class...
				Zenicom.XML.ObjectXMLSerializer objectXMLSerializer = new Zenicom.XML.ObjectXMLSerializer();
				objectXMLSerializer.Save(settings, Settings.XMLFileName);
			}
		}

		/// <summary>
		/// Checks if NETShots is the real screensaver, to correctly appear on GUI
		/// </summary>
		private void CheckIfAmITheScreensaver() 
		{
			// check the registry
			// there is screensaver?
			RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
				@"Control Panel\Desktop", false);

			string val = rk.GetValue("SCRNSAVE.EXE").ToString();

			// Am I?
			if (val != null)
			{
				int pos = val.ToLower().IndexOf("NETShots.scr".ToLower());
				if (pos > 0)
				{
					settings.UseAsScreenshot = true;
					int time = Convert.ToInt32(rk.GetValue("ScreenSaveTimeOut").ToString());
					settings.ScreenshotStartsIn = time / 60;
				} 
				else
				{
					settings.UseAsScreenshot = false;
				}
			}


		}
	}
}

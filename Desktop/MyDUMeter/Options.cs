/*
 * MyDUMeter - by leppie
 * Original Project.
 * 
 * NETMeter - by Alessandro Fragnani
 * Options.cs
 * 
 * Changes: 
 *  - Added: XMLFileName static property that indicates the XML File where the settings are 
 *           saved. Depening on DEBUG or RELEASE, it uses Application.StartupPath or 
 *           "C:\Documents and Settings\xxx" folder to save the file
 *  - Added: LOGDatabase static property that indicates the MDB Database File. Depending on
 *           DEBUG or RELEASE, it uses Application.StartupPath or "C:\Documents and Settings\xxx"
 *           folder to save the file
 *  - Added: Save and Load methods that only has "Options" parameter, and interally uses the
 *           static properties
 *   
 */ 

using System;
using System.Reflection;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyDUMeter
{
	public class Options
	{
		private Options(){}

		/// <summary>
		/// The XML Config file
		/// </summary>
		public static string XMLFileName
		{
			get 
			{
#if (DEBUG)
				return Application.StartupPath + "\\NETMeter.xml";
#else
				string sDir = Application.LocalUserAppDataPath;
				DirectoryInfo di = Directory.GetParent(sDir);
				return di.FullName + "\\NETMeter.xml";
#endif
			}
		}


		/// <summary>
		/// The Database
		/// </summary>
		public static string LOGDatabase
		{
			get 
			{
#if (DEBUG)
				return Application.StartupPath + "\\log.mdb";
#else
				string sDir = Application.LocalUserAppDataPath;
				DirectoryInfo di = Directory.GetParent(sDir);
				return di.FullName + "\\log.mdb";
#endif
			}
		}


		/// <summary>
		/// Save with the XML File
		/// </summary>
		/// <param name="options"></param>
		public static void Save(object options) 
		{
			Save(XMLFileName, options);
		}


		/// <summary>
		/// Load with the XML File
		/// </summary>
		/// <param name="options"></param>
		public static void Load(object options)
		{
			Load(XMLFileName, options);
		}




		public static void Save(string filename, object options)
		{

			Byte[] buffer = new Byte[80];
			MemoryStream ms;
			BinaryFormatter bf = new BinaryFormatter();

			System.Xml.XmlTextWriter xmlwriter = 
				new XmlTextWriter(filename, System.Text.Encoding.Default);

			xmlwriter.Formatting = Formatting.Indented;
			xmlwriter.WriteStartDocument();

			xmlwriter.WriteComment("Option File. Do not edit! (c)llewellyn@pritchard.org");
			xmlwriter.WriteStartElement(options.ToString());
		
			PropertyInfo[] props = options.GetType().GetProperties(
				BindingFlags.Public | 
				BindingFlags.Instance | 
				BindingFlags.SetField);

			foreach (PropertyInfo prop in props)
			{
				xmlwriter.WriteStartElement(prop.Name);

				object da = prop.GetValue(options, null);

				if (da != null) 
				{
					xmlwriter.WriteAttributeString("Value", da.ToString());

					ms = new MemoryStream();
					try 
					{
						bf.Serialize(ms, da);
						ms.Position = 0;
						int count = 0;
						do 
						{
							count = ms.Read(buffer, 0, buffer.Length);
							xmlwriter.WriteBase64(buffer, 0, count);
						}
						while ( count == buffer.Length);
					} 
					catch (System.Runtime.Serialization.SerializationException)
					{
						Console.WriteLine("SERIALIZATION FAILED: {0}", prop.Name);
					}

				}
				else xmlwriter.WriteAttributeString("Value", "null");

				xmlwriter.WriteEndElement();
			}
			xmlwriter.WriteEndElement();
			xmlwriter.WriteEndDocument();
			xmlwriter.Flush();
			xmlwriter.Close();
		}

		public static void Load(string filename, object options)
		{
			Byte[] buffer = new Byte[80];
			MemoryStream ms;
			BinaryFormatter bf = new BinaryFormatter();

			System.Xml.XmlTextReader reader = new XmlTextReader(filename);

			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:

						if (reader.HasAttributes)
						{
							string name = reader.Name;
							string val = reader.GetAttribute("Value");

							ms = new MemoryStream();
		
							int count = 0;
							do 
							{
								count = reader.ReadBase64(buffer, 0 , buffer.Length);
								ms.Write(buffer, 0,count);
							}
							while (count == buffer.Length);

							ms.Position = 0;

							if (val != "null") 
							{
								try 
								{
									object da = bf.Deserialize(ms);

									Console.Write("Applying {0} : ", name);
									options.GetType().GetProperty(name).SetValue(options, da, null);
									Console.WriteLine("OK");
								}
								catch (System.Runtime.Serialization.SerializationException e)
								{
									Console.WriteLine("FAIL: {0}",e.Message);
								}
							}
						}
						break;
				}
			}
			reader.Close();
		}
	}
}

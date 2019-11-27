/*
 * NETShots - by Alessandro Fragnani
 * Wallpaper.cs
 * Created: 28 march 2005
 * 
 * Responsible to shoot the Wallpaper to Desktop
 * 
 * Thanks to:
 *  - Steve Dunn for his article "Desktop Decorator : Changing your wallpaper the easy way in 
 *    .NET"
 *    (http://www.codeproject.com/csharp/xml_serializationasp.asp)
 *  - Joel Neubeck for his article "Resizing a Photographic image with GDI+ for .NET"
 *    (http://www.codeproject.com/csharp/imageresize.asp)
 */ 

using System;
using System.Net;
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace NetShots
{
	/// <summary>
	/// Summary description for Wallpaper.
	/// 
	/// Thanks to:
	///  - "Steve Dunn" (http://www.codeproject.com/dotnet/SettingWallpaperDotNet.asp)
	///  - "Joel Neubeck" (http://www.codeproject.com/csharp/imageresize.asp)
	/// 
	/// </summary>
	public sealed class Wallpaper
	{
		Wallpaper( ) { }

		enum Dimensions 
		{
			Width,
			Height
		}

		const int SPI_SETDESKWALLPAPER = 20  ;
		const int SPIF_UPDATEINIFILE = 0x01;
		const int SPIF_SENDWININICHANGE = 0x02;

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static  extern int SystemParametersInfo (int uAction , int uParam , string lpvParam , int fuWinIni) ;

		public enum Style : int
		{
			Tiled,
			Centered,
			Stretched
		}

		/// <summary>
		/// "Steve Dunn" (http://www.codeproject.com/dotnet/SettingWallpaperDotNet.asp)
		///  - with some modifications to resize the image to fit the desktop, with aspect ratio
		/// </summary>
		/// <param name="uri"></param>
		/// <param name="style"></param>
		public static void Set ( Uri uri, Style style )
		{
			System.IO.Stream s = new WebClient( ).OpenRead( uri.ToString( ) );
			System.Drawing.Image img = System.Drawing.Image.FromStream( s );
			Image imgPhoto = null;

			string tempPath = Path.Combine( Path.GetTempPath( ), "wallpaper.bmp"  ) ;


			// resize the image
			if (img.Width > img.Height) 
			{
				imgPhoto = ConstrainProportions(img, Screen.GetWorkingArea(Point.Empty).Width/*1280*/, Dimensions.Width);
			} 
			else
			{
				imgPhoto = ConstrainProportions(img, Screen.GetWorkingArea(Point.Empty).Height/*1024*/, Dimensions.Height);
			}
			imgPhoto.Save(tempPath, ImageFormat.Bmp );
			imgPhoto.Dispose();


			// save the registry
			RegistryKey key = Registry.CurrentUser.OpenSubKey( @"Control Panel\Desktop", true ) ;
			if ( style == Style.Stretched )
			{
				key.SetValue(@"WallpaperStyle", 2.ToString( ) ) ;
				key.SetValue(@"TileWallpaper", 0.ToString( ) ) ;
			}

			if ( style == Style.Centered )
			{
				key.SetValue(@"WallpaperStyle", 1.ToString( ) ) ;
				key.SetValue(@"TileWallpaper", 0.ToString( ) ) ;
			}

			if ( style == Style.Tiled )
			{
				key.SetValue(@"WallpaperStyle", 1.ToString( ) ) ;
				key.SetValue(@"TileWallpaper", 1.ToString( ) ) ;
			}

			// message to all
			SystemParametersInfo( SPI_SETDESKWALLPAPER, 
				0, 
				tempPath,  
				SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE );
		}



		/// <summary>
		/// "Joel Neubeck" (http://www.codeproject.com/csharp/imageresize.asp)
		/// </summary>
		/// <param name="imgPhoto"></param>
		/// <param name="Size"></param>
		/// <param name="Dimension"></param>
		/// <returns></returns>
		static Image ConstrainProportions(Image imgPhoto, int Size, Dimensions Dimension)
		{
			int sourceWidth = imgPhoto.Width;
			int sourceHeight = imgPhoto.Height;
			int sourceX = 0;
			int sourceY = 0;
			int destX = 0;
			int destY = 0; 
			float nPercent = 0;

			switch(Dimension)
			{
				case Dimensions.Width:
					nPercent = ((float)Size/(float)sourceWidth);
					break;
				default:
					nPercent = ((float)Size/(float)sourceHeight);
					break;
			}
				
			int destWidth  = (int)(sourceWidth * nPercent);
			int destHeight = (int)(sourceHeight * nPercent);

			Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
			bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

			Graphics grPhoto = Graphics.FromImage(bmPhoto);
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

			grPhoto.DrawImage(imgPhoto, 
				new Rectangle(destX,destY,destWidth,destHeight),
				new Rectangle(sourceX,sourceY,sourceWidth,sourceHeight),
				GraphicsUnit.Pixel);

			grPhoto.Dispose();
			return bmPhoto;
		}
	}
}

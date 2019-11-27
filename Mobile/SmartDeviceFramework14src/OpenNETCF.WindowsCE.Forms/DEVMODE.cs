//==========================================================================================
//
//		OpenNETCF.WindowsCE.Forms.DEVMODE
//		Copyright (c) 2004, OpenNETCF.org
//
//		This library is free software; you can redistribute it and/or modify it under 
//		the terms of the OpenNETCF.org Shared Source License.
//
//		This library is distributed in the hope that it will be useful, but 
//		WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
//		FITNESS FOR A PARTICULAR PURPOSE. See the OpenNETCF.org Shared Source License 
//		for more details.
//
//		You should have received a copy of the OpenNETCF.org Shared Source License 
//		along with this library; if not, email licensing@opennetcf.org to request a copy.
//
//		If you wish to contact the OpenNETCF Advisory Board to discuss licensing, please 
//		email licensing@opennetcf.org.
//
//		For general enquiries, email enquiries@opennetcf.org or visit our website at:
//		http://www.opennetcf.org
//
//==========================================================================================

using System;

namespace OpenNETCF.WindowsCE.Forms
{
	/// <summary>
	/// DEVMODE struct used with ChangeDisplaySettingsEx
	/// </summary>
	internal class DEVMODE
	{
		public const short Size = 192;
		/*WCHAR dmDeviceName[CCHDEVICENAME]; 
		short dmSpecVersion; 
		short dmDriverVersion; 
		short dmSize; 
		short dmDriverExtra; 
		int dmFields; 
		short dmOrientation; 
		short dmPaperSize; 
		short dmPaperLength; 
		short dmPaperWidth; 
		short dmScale; 
		short dmCopies; 
		short dmDefaultSource; 
		short dmPrintQuality; 
		short dmColor; 
		short dmDuplex; 
		short dmYResolution; 
		short dmTTOption; 
		short dmCollate; 
		int dmFormName; 
		short dmLogPixels; 
		int dmBitsPerPel; 
		int dmPelsWidth; 
		int dmPelsHeight; 
		int dmDisplayFlags; 
		int dmDisplayFrequency;*/
		private byte[] mData;

		public DEVMODE()
		{
			mData = new byte[Size];
			BitConverter.GetBytes((short)Size).CopyTo(mData, 68);
		}

		public byte[] ToByteArray()
		{
			return mData;
		}

		public DM Fields
		{
			get
			{
				return (DM)BitConverter.ToInt32(mData, 72);
			}
			set
			{
				BitConverter.GetBytes((int)value).CopyTo(mData, 72);
			}
		}
		public int DisplayOrientation
		{
			get
			{
				return BitConverter.ToInt32(mData, 188);
			}
			set
			{
				BitConverter.GetBytes(value).CopyTo(mData, 188);
			}
		}
	}

	[Flags()]
	internal enum DM
	{
		ORIENTATION      = 0x00000001,
		PAPERSIZE        = 0x00000002,
		//PAPERLENGTH      = 0x00000004,
		//PAPERWIDTH       = 0x00000008,
		//SCALE            = 0x00000010,
		//COPIES           = 0x00000100,
		//DEFAULTSOURCE    = 0x00000200,
		PRINTQUALITY     = 0x00000400,
		COLOR            = 0x00000800,
		//DUPLEX           = 0x00001000,
		//YRESOLUTION      = 0x00002000,
		//TTOPTION         = 0x00004000,
		//COLLATE          = 0x00008000,
		//FORMNAME         = 0x00010000,
		//LOGPIXELS        = 0x00020000,
		BITSPERPEL       = 0x00040000,
		PELSWIDTH        = 0x00080000,
		PELSHEIGHT       = 0x00100000,
		//DISPLAYFLAGS     = 0x00200000,
		//DISPLAYFREQUENCY = 0x00400000,
		DISPLAYORIENTATION = 0x00800000,
		DISPLAYQUERYORIENTATION = 0x01000000,
	}
}

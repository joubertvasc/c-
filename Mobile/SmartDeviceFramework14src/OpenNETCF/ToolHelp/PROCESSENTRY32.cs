using System;
using System.Runtime.InteropServices;

namespace OpenNETCF.ToolHelp
{
	#region PROCESSENTRY32 implementation

	//		typedef struct tagPROCESSENTRY32 
	//		{ 
	//			DWORD dwSize; 
	//			DWORD cntUsage; 
	//			DWORD th32ProcessID; 
	//			DWORD th32DefaultHeapID; 
	//			DWORD th32ModuleID; 
	//			DWORD cntThreads; 
	//			DWORD th32ParentProcessID; 
	//			LONG pcPriClassBase; 
	//			DWORD dwFlags; 
	//			TCHAR szExeFile[MAX_PATH]; 
	//			DWORD th32MemoryBase;
	//			DWORD th32AccessKey;
	//		} PROCESSENTRY32;

	internal class PROCESSENTRY32
	{
		// constants for structure definition
		private const int SizeOffset = 0;
		private const int UsageOffset = 4;
		private const int ProcessIDOffset=8;
		private const int DefaultHeapIDOffset = 12;
		private const int ModuleIDOffset = 16;
		private const int ThreadsOffset = 20;
		private const int ParentProcessIDOffset = 24;
		private const int PriClassBaseOffset = 28;
		private const int dwFlagsOffset = 32;
		private const int ExeFileOffset = 36;
		private const int MemoryBaseOffset = 556;
		private const int AccessKeyOffset = 560;
		private const int Size = 564;
		private const int MAX_PATH = 260;

		// data members
		public uint dwSize; 
		public uint cntUsage; 
		public uint th32ProcessID; 
		public uint th32DefaultHeapID; 
		public uint th32ModuleID; 
		public uint cntThreads; 
		public uint th32ParentProcessID; 
		public long pcPriClassBase; 
		public uint dwFlags; 
		public string szExeFile;
		public uint th32MemoryBase;
		public uint th32AccessKey;

		//Default constructor
		public PROCESSENTRY32()
		{
		}

		// create a PROCESSENTRY instance based on a byte array		
		public PROCESSENTRY32(byte[] aData)
		{
			dwSize = Util.GetUInt(aData, SizeOffset);
			cntUsage = Util.GetUInt(aData, UsageOffset);
			th32ProcessID = Util.GetUInt(aData, ProcessIDOffset);
			th32DefaultHeapID = Util.GetUInt(aData, DefaultHeapIDOffset);
			th32ModuleID = Util.GetUInt(aData, ModuleIDOffset);
			cntThreads = Util.GetUInt(aData, ThreadsOffset);
			th32ParentProcessID = Util.GetUInt(aData, ParentProcessIDOffset);
			pcPriClassBase = (long) Util.GetUInt(aData, PriClassBaseOffset);
			dwFlags = Util.GetUInt(aData, dwFlagsOffset);
			szExeFile = Util.GetString(aData, ExeFileOffset, MAX_PATH).TrimEnd('\0');
			th32MemoryBase = Util.GetUInt(aData, MemoryBaseOffset);
			th32AccessKey = Util.GetUInt(aData, AccessKeyOffset);
		}

		// create an initialized data array
		public byte[] ToByteArray()
		{
			byte[] aData;
			aData = new byte[Size];
			//set the Size member
			Util.SetUInt(aData, SizeOffset, Size);
			return aData;
		}

		public string ExeFile
		{
			get { return szExeFile; }
		}

		/// <summary>
		/// Identifier of the process. The contents of this member can be used by Win32 API elements.
		/// </summary>
		public ulong ProcessID
		{
			get
			{
				return th32ProcessID;
			}
		}

		public uint BaseAddress
		{
			get
			{
				return th32MemoryBase;
			}
		}

		public ulong ThreadCount
		{
			get
			{
				return cntThreads;
			}
		}
	}
	#endregion
}

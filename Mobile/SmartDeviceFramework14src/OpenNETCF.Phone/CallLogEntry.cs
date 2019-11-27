using System;
using System.Runtime.InteropServices;
using OpenNETCF.Runtime.InteropServices;

namespace OpenNETCF.Phone
{
	/// <summary>
	/// Contains details of a specific call log entry.
	/// <para><b>New in v1.1</b></para>
	/// </summary>
	public class CallLogEntry : IDisposable
	{
		private byte[] m_data;
		private bool disposed = false;
		
		internal CallLogEntry(byte[] data)
		{
			m_data = data;
		}

		#region Start Time Property
		/// <summary>
		/// The start time of the logged call.
		/// </summary>
		public DateTime StartTime
		{
			get
			{
				return DateTime.FromFileTime(BitConverter.ToInt64(m_data, 4));
			}
		}
		#endregion

		#region End Time Property
		/// <summary>
		/// The end time of the logged call.
		/// </summary>
		public DateTime EndTime
		{
			get
			{
				return DateTime.FromFileTime(BitConverter.ToInt64(m_data, 12));

			}
		}
		#endregion


		#region Call Type Property
		/// <summary>
		/// Indicates whether the call was missed (incoming), answered (incoming), or outgoing.
		/// </summary>
		public CallType CallType
		{
			get
			{
				return (CallType)BitConverter.ToInt32(m_data, 20);
			}
		}
		#endregion


		#region Outgoing Property
		/// <summary>
		/// Indicates the direction of the call (missed calls are incoming).
		/// </summary>
		public bool Outgoing
		{
			get
			{
				return BitConverter.ToBoolean(m_data, 24);
			}
		}
		#endregion

		#region Connected Property
		/// <summary>
		/// Indicates whether the call connected (as opposed to a busy signal or no answer).
		/// </summary>
		public bool Connected
		{
			get
			{
				return BitConverter.ToBoolean(m_data, 25);
			}
		}
		#endregion

		#region Ended Property
		/// <summary>
		/// Indicates whether the call ended (as opposed to being dropped).
		/// </summary>
		public bool Ended
		{
			get
			{
				return BitConverter.ToBoolean(m_data, 26);
			}
		}
		#endregion

		#region Roaming Property
		/// <summary>
		/// Indicates whether the call was made while roaming (as opposed to a call made within the home service area).
		/// </summary>
		public bool Roaming
		{
			get
			{
				return BitConverter.ToBoolean(m_data, 27);
			}
		}
		#endregion


		#region CallerID Type Property
		/// <summary>
		/// The caller ID type.
		/// </summary>
		public CallerIDType CallerIDType
		{
			get
			{
				return (CallerIDType)BitConverter.ToInt32(m_data, 28);
			}
		}
		#endregion


		#region Number Property
		/// <summary>
		/// The telephone number of the call.
		/// </summary>
		public string Number
		{
			get
			{
				return Marshal.PtrToStringUni((IntPtr)BitConverter.ToInt32(m_data, 32));
			}
		}
		#endregion

		#region Name
		/// <summary>
		/// The name associated with the call.
		/// </summary>
		public string Name
		{
			get
			{
				return Marshal.PtrToStringUni((IntPtr)BitConverter.ToInt32(m_data, 36));
			}
		}
		#endregion

		#region Name Type
		/// <summary>
		/// The name type associated with the call. For example, "w" would correspond with the work telephone number, "h" would correspond with the home telephone number, and so on.
		/// </summary>
		public string NameType
		{
			get
			{
				return Marshal.PtrToStringUni((IntPtr)BitConverter.ToInt32(m_data, 40));
			}
		}
		#endregion

		#region Note
		/// <summary>
		/// File name of the Notes file associated with the call (if a Notes file exists).
		/// </summary>
		public string Note
		{
			get
			{
				return Marshal.PtrToStringUni((IntPtr)BitConverter.ToInt32(m_data, 44));
			}
		}
		#endregion

		#region IDisposable Members

		protected void Dispose(bool disposing)
		{
			if(!disposed)
			{
				disposed = true;
			}

		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~CallLogEntry()
		{
			Dispose(false);
		}

		#endregion
	}
}

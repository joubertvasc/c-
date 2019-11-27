//==========================================================================================
//
//		OpenNETCF.Phone.Sms.Sms
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
using System.Runtime.InteropServices;

namespace OpenNETCF.Phone.Sms
{
	/// <summary>
	/// Provides access to the SMS functionality on Pocket PC Phone Edition and Smartphone devices.
	/// <para><b>New in v1.1</b></para>
	/// </summary>
	public class Sms : IDisposable
	{
		public const string TextProvider = "Microsoft Text SMS Protocol";
		private const string NotificationProvider = "Microsoft Notification SMS Protocol (Receive Only)";
		private const string WdpProvider = "Microsoft WDP SMS Protocol";

		/// <summary>
		/// SMS Handle
		/// </summary>
		private IntPtr m_handle = IntPtr.Zero;
		/// <summary>
		/// Event Handle
		/// </summary>
		private IntPtr m_eventhandle = IntPtr.Zero;
		private bool m_keepwaiting  = true;

		/// <summary>
		/// Creates a new instance of SmsSender for sending.
		/// </summary>
		public Sms() : this(TextProvider, SmsMode.Send){}
		
		/// <summary>
		/// Creates a new instance of Sms with specified mode.
		/// </summary>
		/// <param name="mode">SmsMode (Send  and/or Receive)</param>
		public Sms(SmsMode mode) : this(TextProvider, mode){}
		
		/// <summary>
		/// Creates a new instance of Sms with specified mode and provider.
		/// </summary>
		/// <param name="provider">Message provider to use.</param>
		/// <param name="mode">SmsMode (Send  and/or Receive)</param>
		public Sms(string provider, SmsMode mode)
		{
			//open SMS
			int hresult = SmsOpen(provider, mode, ref m_handle, ref m_eventhandle);

			if(hresult!=0)
			{
				throw new ExternalException("Error opening SMS");
			}

			//System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(DoWait));
			//t.Start();
		}

		/*private void DoWait()
		{
			while(m_keepwaiting)
			{
				//wait on 
				OpenNETCF.Win32.Core.WaitForSingleObject(m_eventhandle, 0xffffffff);

				if(MessageReceived!=null)
				{
					MessageReceived(this, new EventArgs());
				}
			}

		}
		public event EventHandler MessageReceived;*/

		/// <summary>
		/// Sends a binary SMS message to the specified address.
		/// </summary>
		/// <param name="destination">Address of recipient.</param>
		/// <param name="message">Binary representation of message contents.</param>
		/// <returns>An identifier for the message which can be used to check the status.</returns>
		public int SendMessage(SmsAddress destination, byte[] message)
		{
			
			TextProviderSpecificData tpsd = new TextProviderSpecificData();

			tpsd.MessageClass = MessageClass.Class1;
			
			return SendMessage(destination, message, tpsd);
		}

		/// <summary>
		/// Sends a binary SMS message to the specified address with the specified provider specific options.
		/// </summary>
		/// <param name="destination">Address of recipient.</param>
		/// <param name="message">Binary representation of message contents.</param>
		/// <param name="providerData"></param>
		/// <returns>An identifier for the message which can be used to check the status.</returns>
		public int SendMessage(SmsAddress destination, byte[] message, ProviderSpecificData providerData)
		{
			int messageid = 0;

			int result = SmsSendMessage(m_handle, null, destination.ToByteArray(), null, message, message.Length, providerData, TextProviderSpecificData.Length, DataEncoding.Optimal, 0, ref messageid);


			if(result !=0)
			{
				throw new ExternalException("Error sending message");
			}

			/*string headerbytes = "";
			int headerlen = ((TextProviderSpecificData)providerData).HeaderDataSize;

			byte[] header = ((TextProviderSpecificData)providerData).HeaderData;
			for(int ibyte = 0; ibyte < headerlen; ibyte++)
			{
				headerbytes += header[ibyte].ToString("X") + "|";
			}
			System.Windows.Forms.MessageBox.Show(headerbytes);*/

			return messageid;
		}

		/// <summary>
		/// Sends a text SMS message to the specified address with the specified provider specific options.
		/// <para><b>New in v1.3</b></para>
		/// </summary>
		/// <param name="destination">Address of recipient.</param>
		/// <param name="message">Message body as a string.</param>
		/// <param name="providerData">Provider specific options.</param>
		/// <returns>An identifier for the message which can be used to check the status.</returns>
		public int SendMessage(SmsAddress destination, string message, ProviderSpecificData providerData)
		{
			return SendMessage(destination, System.Text.Encoding.Unicode.GetBytes(message), providerData);
		}

		/// <summary>
		/// Sends a text SMS message to the specified address.
		/// </summary>
		/// <param name="destination">Address of recipient.</param>
		/// <param name="message">Message body as a string.</param>
		/// <returns>An identifier for the message which can be used to check the status.</returns>
		public int SendMessage(SmsAddress destination, string message)
		{
			return SendMessage(destination, System.Text.Encoding.Unicode.GetBytes(message));
		}

		/// <summary>
		/// Retrieves the status of a given SMS message.
		/// </summary>
		/// <param name="messageid">The message id, retrieved from the SendMessage method.</param>
		/// <returns>An SmsMessageStatus structure containing status information, or null (Nothing in VB) if unavailable.</returns>
		public SmsMessageStatus GetStatus(int messageid)
		{
			return GetStatus(messageid, 0);
		}
		
		/// <summary>
		/// Retrieves the status of a given SMS message, waiting within a specified timeout.
		/// </summary>
		/// <param name="messageid">The message id, retrieved from the SendMessage method.</param>
		/// <param name="timeout">Timeout in milliseconds to wait for a response.</param>
		/// <returns>An SmsMessageStatus structure containing status information, or null (Nothing in VB) if unavailable.</returns>
		public SmsMessageStatus GetStatus(int messageid, int timeout)
		{
			SmsMessageStatus status = new SmsMessageStatus();

			int result = SmsGetMessageStatus(this.m_handle, messageid, status, timeout);

			if(result!=0)
			{
				return null;
			}

			return status;
		}

		/// <summary>
		/// Returns an estimate of the current time from the SMSC clock.
		/// </summary>
		/// <returns>Estimated current time</returns>
		public DateTime Time
		{
			get
			{
				OpenNETCF.Win32.SystemTime st = new OpenNETCF.Win32.SystemTime();
				int error = 0;

				int result = SmsGetTime(st, ref error);
				if(result==0x82000104)
				{
					throw new ExternalException("TimeUnavailable");
				}

				return st.ToDateTime();
			}
		}

		/// <summary>
		/// Closes the Sms system releasing system resources.
		/// </summary>
		public void Close()
		{
			m_keepwaiting = false;

			//check handle
			if(m_handle!=IntPtr.Zero)
			{
				//close handle
				int result = SmsClose(m_handle);
				if(result != 0)
				{
					throw new ExternalException("Error closing SMS");
				}

				m_handle = IntPtr.Zero;
			}
		}

		~Sms()
		{
			//close sms if handle is still open
			this.Close();
		}

		/// <summary>
		/// Retrieves the devices phone number.
		/// </summary>
		public SmsAddress PhoneNumber
		{
			get
			{
				SmsAddress buffer = new SmsAddress();

				int hresult = SmsGetPhoneNumber(buffer.ToByteArray());

				if(hresult!=0)
				{
					throw new System.ComponentModel.Win32Exception(hresult, "Error retrieving phone number");
				}
				return buffer;
			}
		}

		/// <summary>
		/// Retrieves the default SMSC
		/// </summary>
		public SmsAddress ServiceCentre
		{
			get
			{
				SmsAddress buffer = new SmsAddress();

				int result = SmsGetSMSC(buffer.ToByteArray());

				if(result!=0)
				{
					throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "Error retrieving SMSC address");
				}

				return buffer;
			}
		}

		[DllImport("sms.dll")]
		private static extern int SmsGetPhoneNumber(byte[] psmsaAddress);


		[DllImport("sms.dll")]
		private static extern int SmsOpen (
			string ptsMessageProtocol,
			SmsMode dwMessageModes,
			ref IntPtr psmshHandle,
			ref IntPtr phMessageAvailableEvent);

		[DllImport("sms.dll")]
		private static extern int SmsClose (IntPtr smshHandle);

		[DllImport("sms.dll")]
		private static extern int SmsSendMessage (
			IntPtr smshHandle,
			byte[] psmsaSMSCAddress,
			byte[] psmsaDestinationAddress,
			byte[] pstValidityPeriod,
			byte[] pbData,
			int dwDataSize,
			byte[] pbProviderSpecificData,
			int dwProviderSpecificDataSize,
			DataEncoding smsdeDataEncoding,
			int dwOptions,
			ref int psmsmidMessageID);

		[DllImport("sms.dll")]
		private static extern int SmsGetMessageStatus(
			IntPtr smshHandle,
			int smsmidMessageID,
			byte[] psmssiStatusInformation,
			int dwTimeout);

		[DllImport("sms.dll")]
		private static extern int SmsGetTime (
			byte[] ptsCurrentTime,
			ref int pdwErrorMargin);

		[DllImport("sms.dll")]
		private static extern int SmsGetSMSC (
			byte[] psmsaSMSCAddress);

		#region IDisposable Members

		public void Dispose()
		{
			//close if handle still open
			if(m_handle!=IntPtr.Zero)
			{
				Close();
			}
		}

		#endregion
	}
}

/*=======================================================================================

    OpenNETCF.Net.FTP
    Copyright 2003, OpenNETCF.org

    This library is free software; you can redistribute it and/or modify it under 
    the terms of the OpenNETCF.org Shared Source License.

    This library is distributed in the hope that it will be useful, but 
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or 
    FITNESS FOR A PARTICULAR PURPOSE. See the OpenNETCF.org Shared Source License 
    for more details.

    You should have received a copy of the OpenNETCF.org Shared Source License 
    along with this library; if not, email licensing@opennetcf.org to request a copy.

    If you wish to contact the OpenNETCF Advisory Board to discuss licensing, please 
    email licensing@opennetcf.org.

    For general enquiries, email enquiries@opennetcf.org or visit our website at:
    http://www.opennetcf.org

=======================================================================================*/
using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace OpenNETCF.Net.Ftp
{
	/*	
	FTP Command set:
 
	Note that commands marked with a * are not implemented in a number of FTP servers. 


	Common commands
	ABOR - abort a file transfer 
	CWD - change working directory 
	DELE - delete a remote file 
	LIST - list remote files 
	MDTM - return the modification time of a file 
	MKD - make a remote directory 
	NLST - name list of remote directory 
	PASS - send password 
	PASV - enter passive mode 
	PORT - open a data port 
	PWD - print working directory 
	QUIT - terminate the connection 
	RETR - retrieve a remote file 
	RMD - remove a remote directory 
	RNFR - rename from 
	RNTO - rename to 
	SITE - site-specific commands 
	SIZE - return the size of a file 
	STOR - store a file on the remote host 
	TYPE - set transfer type 
	USER - send username 
	Less common commandsACCT* - send account information 
	APPE - append to a remote file 
	CDUP - CWD to the parent of the current directory 
	HELP - return help on using the server 
	MODE - set transfer mode 
	NOOP - do nothing 
	REIN* - reinitialize the connection 
	STAT - return server status 
	STOU - store a file uniquely 
	STRU - set file transfer structure 
	SYST - return system type 
	*/

	/// <summary>
	/// Summary description for FtpWebRequest.
	/// </summary>
	public sealed class FtpWebRequest : WebRequest
	{
		private FtpControlStream controlStream;
		private FtpDataStream dataStream;
		private FtpCommandType commandType;
		private ICredentials credentials;
		private static ICredentials defaultCredentials = new NetworkCredential( "anonymous", "anonymous@yohoo.com" );
		private X509CertificateCollection clientCertificates;
		private bool binary;
		private IWebProxy proxy;
		private Uri uri;
		private bool passive;
		private bool useDefaultCredentials;
		private FtpWebResponse ftpWebResponse;

		public FtpWebRequest( Uri inUri )
		{
			if( inUri.Scheme != "ftp" )  // This class is only for ftp urls
				throw new NotSupportedException( "This protocol is not supported" );
			uri = inUri;
			commandType = FtpCommandType.FtpDataReceiveCommand; // Defaults to retrieval of a file;
			passive = true;
			useDefaultCredentials = true;
		}

		public override Uri RequestUri
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override void Abort()
		{
			throw new NotSupportedException();
		}

		// Was: [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)]
		public override IAsyncResult BeginGetRequestStream( AsyncCallback callback, object state )
		{
			throw new NotSupportedException();
		}

		// Was: [HostProtection(SecurityAction.LinkDemand, ExternalThreading=true)]
		public override IAsyncResult BeginGetResponse( AsyncCallback callback, object state )
		{
			throw new NotSupportedException();
		}

		public override Stream EndGetRequestStream( IAsyncResult asyncResult )
		{
			throw new NotSupportedException();
		}

		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			throw new NotSupportedException();
		}

		private void OpenControlConnection( ServicePoint svcPoint )
		{
			Socket controlSocket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			EndPoint clientIPEndPoint = new IPEndPoint( IPAddress.Any, 0 );
			try
			{
				controlSocket.Bind( clientIPEndPoint );
				IPHostEntry serverHostEntry = Dns.GetHostByName( uri.Host );
				IPEndPoint serverEndPoint = new IPEndPoint( serverHostEntry.AddressList[0], uri.Port ); // TODO: Rollback to try other addresses if first one fails...
				controlSocket.Connect( serverEndPoint );
				controlStream = new FtpControlStream( controlSocket );
			}
			catch( Exception )
			{
				controlSocket.Close();
				throw;
			}
			catch
			{
				controlSocket.Close();
				throw;
			}
		}

		private string FormatAddress( UInt32 address, int port )
		{
			StringBuilder sb = new StringBuilder(32);
			UInt32 quotient = address;
			UInt32 remainder;	
			while( quotient!=0 ) 
			{
				remainder = quotient % 256;
				quotient = quotient / 256;
				sb.Append( remainder );
				sb.Append(',');
			}
			sb.Append( port / 256 );
			sb.Append( ',' );
			sb.Append( port % 256 );
			return sb.ToString();
		}

		private string GetIP( CommandResponse response )
		{
			StringBuilder ip = new StringBuilder();
			string tmp = null;
			int pos1 = response.Message.IndexOf("(")+1;
			int pos2 = response.Message.IndexOf(",");
			for( int i =0; i < 3; i++ )
			{
				tmp = response.Message.Substring( pos1, pos2 - pos1 ) + ".";
				ip.Append( tmp );
				pos1 = pos2 + 1;
				pos2 = response.Message.IndexOf( ",", pos1 );
			}
			tmp = response.Message.Substring(pos1,pos2-pos1);
			ip.Append( tmp );
			return ip.ToString();
		}

		private int GetPort( CommandResponse response )
		{
			int port = 0;
			int pos1 = response.Message.IndexOf("(");
			int pos2 = response.Message.IndexOf(",");
			for( int i =0; i<3; i++ )
			{
				pos1 = pos2 + 1;
				pos2 = response.Message.IndexOf( ",",pos1 );
			}
			pos1 = pos2 + 1;
			pos2 = response.Message.IndexOf( ",",pos1 );
			string PortSubstr1 = response.Message.Substring( pos1, pos2 - pos1 );

			pos1 = pos2 + 1;
			pos2 = response.Message.IndexOf( ")",pos1 );
			string PortSubstr2 = response.Message.Substring( pos1, pos2 - pos1 );

			//evaluate port number
			port = Convert.ToInt32( PortSubstr1 ) * 256;
			port = port + Convert.ToInt32( PortSubstr2 );
			return port;
		}

		private void OpenPassiveDataConnection()
		{
			CommandResponse response = SendCommand( "PASV", null );
			if( response.Status != 227 )
				throw new ApplicationException( "Couldn't open passive data connection, no DataConnection IP was given" );

			// TODO Use this IP instead of the same as the communication ip
			string ip = GetIP( response );
			int port = GetPort( response );

			Socket dataSocket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			IPHostEntry serverHostEntry = Dns.GetHostByName( uri.Host );
			IPEndPoint	serverEndPoint = new IPEndPoint( serverHostEntry.AddressList[0], port );
			dataSocket.Connect( serverEndPoint );
			dataStream = new FtpDataStream( dataSocket );
		}

		private void OpenActiveDataConnection()
		{
			Socket dataSocket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			IPHostEntry localhostEntry = Dns.GetHostByName( Dns.GetHostName() );
			IPEndPoint listener = new IPEndPoint( localhostEntry.AddressList[0], 0 );
			dataSocket.Bind( listener );
			dataSocket.Listen( 5 );
			IPEndPoint localEP = (IPEndPoint)dataSocket.LocalEndPoint;
			UInt32 localEPAddress = (UInt32) localEP.Address.Address;
			string local = FormatAddress( localEPAddress, localEP.Port );
			CommandResponse response = SendCommand( "PORT", local );
			dataStream = new FtpDataStream( dataSocket );
		}

		private CommandResponse SendCommand( string method, string parameter )
		{
			// Building the request and writing it to the controolStream
			string request = method;
			if( parameter != null && parameter.Length > 0 )
			{
				request += " "+parameter;
			}
			request += "\r\n";
			byte[] rawBuffer = Encoding.ASCII.GetBytes( request );
			controlStream.Write( rawBuffer, 0, rawBuffer.Length );

			// Reading in our CommandResponse from our controlStream
			CommandResponse retVal = new CommandResponse();
			int bufferSize = 256;
			byte[] buffer = new byte[ bufferSize+1 ];
			while( true )
			{
				// We need to "keep" the buffer and not flush it, therefor using the "secret" read function which preserves the stream and doesn't
				// increase the "stream pointer"
				int bytesRead = controlStream.ReadButDontClear( buffer, 0, bufferSize );
				retVal.message += Encoding.ASCII.GetString( buffer, 0, bytesRead );
				if( bytesRead == 0 )
					break;
			}
			retVal.status = controlStream.StatusCode;
			return retVal;
		}

		private void OpenDataConnection()
		{
			if( Passive )
				OpenPassiveDataConnection();
			else
				OpenActiveDataConnection();
		}

		private void Login( ServicePoint svcPoint, string username, string password )
		{
			OpenControlConnection( svcPoint );
			CommandResponse response = SendCommand( "USER", username );
			if( response.Status == 331 )
			{
				response = SendCommand( "PASS", password );
			}
			if( !(response.Status == 220 || response.Status == 230) )
				throw new ArgumentException( "Couldn't log in to Ftp server"+response.Message );
		}

		/// <summary>
		/// Gets a stream instance which youu can use to WRITE request data to.
		/// Once the stream has been returned you can use the Write method to send data
		/// </summary>
		/// <returns>FtpDataStream</returns>
		public override Stream GetRequestStream()
		{
			// Retrieving credentials and loging in
			NetworkCredential creds;
			if( useDefaultCredentials )
			{
				creds = defaultCredentials.GetCredential( uri, null );
			}
			else
			{
				creds = credentials.GetCredential( uri, null );
			}
			ServicePoint svcPoint = ServicePointManager.FindServicePoint( uri, proxy );
			Login( svcPoint, creds.UserName, creds.Password );
			controlStream.Position = 0; // Wee need to be able to get the response from the server and since we have written to the buffered memory stream this is possible

			return controlStream;
		}

		/// <summary>
		/// Sends the request to the server and returns the response
		/// </summary>
		/// <returns>Returns an FtpWebResponse which can be used to access the response from the request</returns>
		public override WebResponse GetResponse()
		{
			OpenDataConnection();
			ftpWebResponse = new FtpWebResponse( dataStream );
			return ftpWebResponse;
		}

		public bool Binary
		{
			get
			{
				return binary;
			}
			set
			{
				binary = value;
			}
		}

		public X509CertificateCollection ClientCertificates
		{
			get
			{
				return clientCertificates;
			}
		}

		public override string ConnectionGroupName
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public override long ContentLength
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public long ContentOffset
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public override string ContentType
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public override ICredentials Credentials
		{
			get
			{
				return credentials;
			}
			set
			{
				useDefaultCredentials = false;
				credentials = value;
			}
		}

		public bool EnableSsl
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public override WebHeaderCollection Headers
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public bool KeepAlive
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public override String Method
		{
			get
			{
				return commandType.ToString();
			}
			set
			{
				commandType = (FtpCommandType)System.Enum.ToObject( typeof(FtpCommandType), value );
			}
		}

		public bool Passive
		{
			get
			{
				return passive;
			}
			set
			{
				passive = value;
			}
		}

		public override bool PreAuthenticate
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public override IWebProxy Proxy
		{
			get
			{
				return proxy;
			}
			set
			{
				proxy = value;
			}
		}

		public int ReadWriteTimeout
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public ServicePoint ServicePoint
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public override int Timeout
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public bool UseDefaultCredentials
		{
			get
			{
				return useDefaultCredentials;
			}
			set
			{
				useDefaultCredentials = value;
			}
		}
	}
}





















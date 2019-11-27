using System;
using System.Collections;
using OpenNETCF.Win32;
using OpenNETCF.IO;
using OpenNETCF.Runtime.InteropServices;

namespace OpenNETCF.Net
{
	#region -------------- Internal-only Classes --------------

	/// <summary>
	/// This class represents the data returned by the 
	/// WZCQueryInterface() call to WZC.  There are several
	/// values in the INTF_ENTRY struct returned by that
	/// query, including a couple of SSID lists (those SSID
	/// values currently 'audible' to the adapter and the
	/// preferred list).  This type is how those lists are
	/// returned.
	/// </summary>
	internal class WZC_WLAN_CONFIG_LIST
	{
		RAW_DATA	data;

		// The memory layout of this structure is:
		//	ULONG           NumberOfItems;  // number of elements in the array below
		//	ULONG           Index;          // [start] index in the array below
		//	WZC_WLAN_CONFIG Config[1];      // array of WZC_WLAN_CONFIGs

		static	int NumberOfItemsOffset = 0;
		static	int BaseIndexOffset = 4;
		static	int	ConfigOffset = 8;

		public WZC_WLAN_CONFIG_LIST( RAW_DATA rd )
		{
			data = rd;
		}

		public uint NumberOfItems
		{
			get { return BitConverter.ToUInt32( data.lpData, NumberOfItemsOffset ); }
		}

		public uint BaseIndex
		{
			get { return BitConverter.ToUInt32( data.lpData, BaseIndexOffset ); }
		}

		public WZC_WLAN_CONFIG Item( int index )
		{
			// ???? check range and throw exception

			// Figure out where in the array the indicated
			// index is located.
//			int		actualInd = index + (int)BaseIndex;

			// Figure out how big each element in the array
			// is.
			int	elemSize = BitConverter.ToInt32( data.lpData, ConfigOffset );

			// Use the actual index to get the indicated element
			// in the Config list.
			WZC_WLAN_CONFIG	wlc = new WZC_WLAN_CONFIG();
			Buffer.BlockCopy(data.lpData, ConfigOffset + index * (int)elemSize, wlc.Data, 0, elemSize);

			return wlc;
		}
	}

	#endregion

	/// <summary>
	/// Class that represents a collection of the SSID values
	/// that a given network adapter can hear over the 
	/// airwaves.  For each SSID, you can get the signal
	/// strength and random other information.
	/// </summary>
	public class AccessPointCollection : CollectionBase
	{
		internal Adapter	adapter = null;

		/// <summary>
		/// The Adapter instance with which the SSID instance
		/// is associated.
		/// </summary>
		public Adapter AssociatedAdapter
		{
			get { return adapter; }
		}

		internal AccessPointCollection( Adapter a )
		{
			adapter = a;

			this.RefreshList( true );
		}

		internal AccessPointCollection( Adapter a, bool nearbyOnly )
		{
			adapter = a;

			this.RefreshListPreferred( nearbyOnly );
		}

		internal unsafe void ClearCache()
		{
			// Tell the driver to search for any new SSIDs
			// and return them on the next OID_802_11_BSSID_LIST
			// message.
			uint		dwBytesReturned = 0;
			NDISUIO_QUERY_OID	queryOID = new NDISUIO_QUERY_OID( 0 );
			IntPtr		ndisAccess;
			bool		retval;

			// Attach to NDISUIO.
			ndisAccess = FileEx.CreateFile( 
				NDISUIOPInvokes.NDISUIO_DEVICE_NAME, 
				FileAccess.All, 
				FileShare.None,
				FileCreateDisposition.OpenExisting,
				NDISUIOPInvokes.FILE_ATTRIBUTE_NORMAL | NDISUIOPInvokes.FILE_FLAG_OVERLAPPED );
			if ( (int)ndisAccess == FileEx.InvalidHandle )
			{
				// The operation failed.  Leave us empty.
				return;
			}

			// Send message to driver.
			byte[]	namestr = System.Text.Encoding.Unicode.GetBytes(adapter.Name+'\0');
			fixed (byte *name = &namestr[ 0 ])
			{
				// Get Signal strength
				queryOID.ptcDeviceName = name;
				queryOID.Oid = NDISUIOPInvokes.OID_802_11_BSSID_LIST_SCAN; // 0x0D01011A

				retval = NDISUIOPInvokes.DeviceIoControl( ndisAccess,
					NDISUIOPInvokes.IOCTL_NDISUIO_SET_OID_VALUE,	// 0x120814
					queryOID, 
					queryOID.Size,
					queryOID, 
					queryOID.Size,
					ref dwBytesReturned,
					IntPtr.Zero);
			}

			if( retval )
			{
				// The call went fine.  There is no return
				// data.
			}
			else
			{
				// There was an error.
				int	err = MarshalEx.GetLastWin32Error();

				// ToDo: Additional error processing.
			}

			queryOID = null;

			FileEx.CloseHandle( ndisAccess );
		}

		internal unsafe void RefreshList( Boolean clearCache )
		{
			// If we are to clear the driver's cache of SSID
			// values, call the appropriate method.
			//Console.WriteLine("Entering RefreshList");
			if ( clearCache )
			{
				this.ClearCache();

				// This seems to be needed to avoid having
				// a list of zero elements returned.
				System.Threading.Thread.Sleep( 1000 );
			}

			this.List.Clear();

			// Retrieve a list of NDIS_802_11_BSSID_LIST 
			// structures from the driver.  We'll parse that
			// list and populate ourselves based on the data
			// that we find there.
			uint		dwBytesReturned = 0;
			NDISUIO_QUERY_OID	queryOID = new NDISUIO_QUERY_OID( 6000 /* TESTING JFK was 2000 */ );
			IntPtr		ndisAccess;
			bool		retval;

			// Attach to NDISUIO.
			ndisAccess = FileEx.CreateFile( 
				NDISUIOPInvokes.NDISUIO_DEVICE_NAME, 
				FileAccess.All, 
				FileShare.None,
				FileCreateDisposition.OpenExisting,
				NDISUIOPInvokes.FILE_ATTRIBUTE_NORMAL | NDISUIOPInvokes.FILE_FLAG_OVERLAPPED );
			if ( (int)ndisAccess == FileEx.InvalidHandle )
			{
				Console.WriteLine("Attach to NDISUIO Failed");
				// The operation failed.  Leave us empty.
				return;
			}

			// Get Signal strength
			byte[]	namestr = System.Text.Encoding.Unicode.GetBytes(adapter.Name+'\0');
			fixed (byte *name = &namestr[ 0 ])
			{
				// Get Signal strength
				queryOID.ptcDeviceName = name;
				queryOID.Oid = NDISUIOPInvokes.OID_802_11_BSSID_LIST; // 0x0D010217

				retval = NDISUIOPInvokes.DeviceIoControl( ndisAccess,
					NDISUIOPInvokes.IOCTL_NDISUIO_QUERY_OID_VALUE,	// 0x00120804
					queryOID, 
					queryOID.Size,
					queryOID, 
					queryOID.Size,
					ref dwBytesReturned,
					IntPtr.Zero);
			}

			if( retval )
			{
				// Now we need to parse the incoming data into
				// suitable representations of the SSIDs.

				// Figure out how many SSIDs there are.
				NDIS_802_11_BSSID_LIST	rawlist = new NDIS_802_11_BSSID_LIST( queryOID.Data );

				for ( int i = 0; i < rawlist.NumberOfItems; i++ )
				{
					// Get the next raw item from the list.
					NDIS_WLAN_BSSID	bssid = rawlist.Item( i );

					// Using the raw item, create a cooked 
					// SSID item.
					AccessPoint	ssid = new AccessPoint( bssid );

					// Add the new item to this.
					this.List.Add( ssid );
				}
			}
			else
			{
				// We might just need more room.
				// For now, we just leave the list empty.
				// ToDo: Additional error processing.
				Console.WriteLine("ERROR Buffer Too Small");//You'll notice here there should be some sort of error contol but there isn't.
				//We simply don't have enough room.
			}
		}

		internal unsafe void RefreshListPreferred( bool nearbyOnly )
		{
			// If the caller wants only the local preferred APs,
			// we check nearby list and, if the AP is not there,
			// we don't add it to our own preferred list.
			AccessPointCollection	apc = null;
			if ( nearbyOnly )
			{
				apc = adapter.NearbyAccessPoints;
			}

			// First step is to get the INTF_ENTRY for the adapter.
			// This includes the list of preferred SSID values.
			INTF_ENTRY	ie = INTF_ENTRY.GetEntry( this.adapter.Name );

			// The field rdStSSIDList is the preferred list.  It comes
			// in the form of a WZC_802_11_CONFIG_LIST.
			RAW_DATA	rd = ie.rdStSSIDList;
			WZC_WLAN_CONFIG_LIST	cl = new WZC_WLAN_CONFIG_LIST( rd );

			// Step through the list and add a new AP to the
			// collection for each entry.
			for ( int i = 0; i < cl.NumberOfItems; i++ )
			{
				WZC_WLAN_CONFIG		c = cl.Item( 0 );

				// Get a NDIS_WLAN_BSSID corresponding to the
				// part of the WZC_WLAN_CONFIG entry and use that
				// to build an AccessPoint instance for this
				// entry.
				NDIS_WLAN_BSSID		bssid = c.ToBssid();

				// If we're only showing those which we can hear,
				// see if the current SSID is in the nearby list.
				if ( nearbyOnly )
				{
					// Find the currently active AP with the SSID
					// to match the one we're working on.
					AccessPoint	activeAP = apc.FindBySSID( bssid.SSID );
					int			ss;

					// If the given SSID is not in range, don't add
					// an entry to the list.
					if ( activeAP != null )
					{
						// Update signal strength.
						ss = activeAP.SignalStrengthInDecibels;

						// Copy the signal strength value to the 
						// NDIS_WLAN_BSSID structure for the 
						// preferred list entry.
						bssid.Rssi = ss;
					
						// Create the AP instance and add it to the
						// preferred list.
						AccessPoint			ap = new AccessPoint( bssid );
						this.List.Add( ap );
					}
				}
				else
				{
					// Create the AP instance and add it to the
					// preferred list.  The signal strength will 
					// not necessarily be valid.
					AccessPoint			ap = new AccessPoint( bssid );
					this.List.Add( ap );
				}
			}
		}

		/// <summary>
		/// Indexer for contained Adapters
		/// </summary>
		public Adapter this[int index]
		{
			get
			{
				return (Adapter)List[ index ];;
			}
		}

		/// <summary>
		/// Refresh the list of SSID values, asking the 
		/// adapter to scan for new ones, also.
		/// </summary>
		public void Refresh()
		{
			this.RefreshList( true );
		}

		/// <summary>
		/// Find a given access point in the collection by
		/// looking for a matching SSID value.
		/// </summary>
		/// <param name="ssid">
		/// String SSID to search for.
		/// </param>
		/// <returns>
		/// First AccessPoint in the collection with the 
		/// indicated SSID, or null, if none was found.
		/// </returns>
		public AccessPoint FindBySSID( String ssid )
		{
			for ( int i = 0; i < this.Count; i++ )
			{
				AccessPoint	ap = ((AccessPoint)this.List[ i ]);
				if ( ap.Name == ssid )
				{
					return ap;
				}
			}

			return null;
		}
	}

}

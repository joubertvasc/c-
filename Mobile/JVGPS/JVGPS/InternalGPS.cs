using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Text;
using JVUtils;

namespace JVGPS
{
    public delegate void LocationChangedEventHandler(object sender, InternapGPS.LocationChangedEventArgs args);
    public delegate void DeviceStateChangedEventHandler(object sender, InternapGPS.DeviceStateChangedEventArgs args);
    public delegate void DeviceNotFoundEventHandler(object sender, EventArgs args);

    /// <summary>
    /// Summary description for GPS.
    /// </summary>
    public class InternapGPS
    {
        bool listening;

        // handle to the gps device
        IntPtr gpsHandle = IntPtr.Zero;

        // handle to the native event that is signalled when the GPS
        // devices gets a new location
        IntPtr newLocationHandle = IntPtr.Zero;

        // handle to the native event that is signalled when the GPS
        // device state changes
        IntPtr deviceStateChangedHandle = IntPtr.Zero;

        // handle to the native event that is signalled when the GPS 
        // device was not found
        IntPtr deviceNotFoundHandle = IntPtr.Zero;

        // handle to the native event that we use to stop our event
        // thread
        IntPtr stopHandle = IntPtr.Zero;

        // holds our event thread instance
        System.Threading.Thread gpsEventThread = null;

        event LocationChangedEventHandler locationChanged;

        /// <summary>
        /// Event that is raised when the GPS locaction data changes
        /// </summary>
        public event LocationChangedEventHandler LocationChanged
        {
            add
            {
                locationChanged += value;

                // create our event thread only if the user decides to listen
                CreateGpsEventThread();
            }
            remove
            {
                locationChanged -= value;
            }
        }

        public bool GPSPresent()
        {
            return !GetCurrentDriver().Equals("");
        }

        public string GetCurrentDriver()
        {
            string result = "";

            RegistryKey r =
                Registry.LocalMachine.OpenSubKey(
                "\\System\\CurrentControlSet\\GPS Intermediate Driver\\Drivers");

            if (r != null)
            {
                try
                {
                    result = (string)r.GetValue("CurrentDriver", "");
                }
                finally
                {
                    r.Close();
                }
            }

            return result;
        }

        event DeviceStateChangedEventHandler deviceStateChanged;
        event DeviceNotFoundEventHandler deviceNotFound;

        /// <summary>
        /// Event that is raised when the GPS device state changes
        /// </summary>
        public event DeviceStateChangedEventHandler DeviceStateChanged
        {
            add
            {
                deviceStateChanged += value;

                // create our event thread only if the user decides to listen
                CreateGpsEventThread();
            }
            remove
            {
                deviceStateChanged -= value;
            }
        }

        public event DeviceNotFoundEventHandler DeviceNotFound
        {
            add { deviceNotFound += value; }
            remove { deviceNotFound -= value; }
        }

        /// <summary>
        /// True: The GPS device has been opened. False: It has not been opened
        /// </summary>
        public bool Opened
        {
            get { return gpsHandle != IntPtr.Zero; }
        }

        public InternapGPS()
        {
        }

        ~InternapGPS()
        {
            // make sure that the GPS was closed.
            Close();
        }

        /// <summary>
        /// Opens the GPS device and prepares to receive data from it.
        /// </summary>
        public void Open()
        {
            if (!Opened)
            {
                // create handles for GPS events
                newLocationHandle = CreateEvent(IntPtr.Zero, 0, 0, null);
                deviceStateChangedHandle = CreateEvent(IntPtr.Zero, 0, 0, null);
                deviceNotFoundHandle = CreateEvent(IntPtr.Zero, 0, 0, null);
                stopHandle = CreateEvent(IntPtr.Zero, 0, 0, null);

                gpsHandle = GPSOpenDevice(newLocationHandle, deviceStateChangedHandle, null, 0);

                // if events were hooked up before the device was opened, we'll need
                // to create the gps event thread.
                if (locationChanged != null || deviceStateChanged != null)
                {
                    CreateGpsEventThread();
                }
            }
        }

        /// <summary>
        /// Closes the gps device.
        /// </summary>
        public void Close()
        {
            if (gpsHandle != IntPtr.Zero)
            {
                GPSCloseDevice(gpsHandle);
                gpsHandle = IntPtr.Zero;
            }

            // Set our native stop event so we can exit our event thread.
            if (stopHandle != IntPtr.Zero)
            {
                EventModify(stopHandle, eventSet);
            }

            // block until our event thread is finished before
            // we close our native event handles
            listening = false;
//            lock (this)
            {
                if (newLocationHandle != IntPtr.Zero)
                {
                    CloseHandle(newLocationHandle);
                    newLocationHandle = IntPtr.Zero;
                }

                if (deviceStateChangedHandle != IntPtr.Zero)
                {
                    CloseHandle(deviceStateChangedHandle);
                    deviceStateChangedHandle = IntPtr.Zero;
                }

                if (deviceNotFoundHandle != IntPtr.Zero)
                {
                    CloseHandle(deviceNotFoundHandle);
                    deviceNotFoundHandle = IntPtr.Zero;
                }                             

                if (stopHandle != IntPtr.Zero)
                {
                    CloseHandle(stopHandle);
                    stopHandle = IntPtr.Zero;
                }
            }
        }

        /// <summary>
        /// Get the position reported by the GPS receiver
        /// </summary>
        /// <returns>GpsPosition class with all the position details</returns>
        public InternalGpsPosition GetPosition()
        {
            return GetPosition(TimeSpan.Zero);
        }


        /// <summary>
        /// Get the position reported by the GPS receiver that is no older than
        /// the maxAge passed in
        /// </summary>
        /// <param name="maxAge">Max age of the gps position data that you want back. 
        /// If there is no data within the required age, null is returned.  
        /// if maxAge == TimeSpan.Zero, then the age of the data is ignored</param>
        /// <returns>GpsPosition class with all the position details</returns>
        public InternalGpsPosition GetPosition(TimeSpan maxAge)
        {
            InternalGpsPosition gpsPosition = null;
            if (Opened)
            {
                // allocate the necessary memory on the native side.  We have a class (GpsPosition) that 
                // has the same memory layout as its native counterpart
                IntPtr ptr = Utils.LocalAlloc(Marshal.SizeOf(typeof(InternalGpsPosition)));

                // fill in the required fields 
                gpsPosition = new InternalGpsPosition();
                gpsPosition.dwVersion = 1;
                gpsPosition.dwSize = Marshal.SizeOf(typeof(InternalGpsPosition));

                // Marshal our data to the native pointer we allocated.
                Marshal.StructureToPtr(gpsPosition, ptr, false);

                // call native method passing in our native buffer
                int result = GPSGetPosition(gpsHandle, ptr, 500000, 0);
                if (result == 0)
                {
                    // native call succeeded, marshal native data to our managed data
                    gpsPosition = (InternalGpsPosition)Marshal.PtrToStructure(ptr, typeof(InternalGpsPosition));

                    if (maxAge != TimeSpan.Zero)
                    {
                        // check to see if the data is recent enough.
                        if (!gpsPosition.TimeValid || DateTime.Now - maxAge > gpsPosition.Time)
                        {
                            gpsPosition = null;
                        }
                    }
                }

                // free our native memory
                Utils.LocalFree(ptr);
            }

            return gpsPosition;            
        }

        /// <summary>
        /// Queries the device state.
        /// </summary>
        /// <returns>Device state information</returns>
        public InternalGpsDeviceState GetDeviceState()
        {
            InternalGpsDeviceState device = null;

            // allocate a buffer on the native side.  Since the
            IntPtr pGpsDevice = Utils.LocalAlloc(InternalGpsDeviceState.GpsDeviceStructureSize);
            
            // GPS_DEVICE structure has arrays of characters, it's easier to just
            // write directly into memory rather than create a managed structure with
            // the same layout.
            Marshal.WriteInt32(pGpsDevice, 1);	// write out GPS version of 1
            Marshal.WriteInt32(pGpsDevice, 4, InternalGpsDeviceState.GpsDeviceStructureSize);	// write out dwSize of structure

            int result = GPSGetDeviceState(pGpsDevice);

            if (result == 0)
            {
                // instantiate the GpsDeviceState class passing in the native pointer
                device = new InternalGpsDeviceState(pGpsDevice);
            }

            // free our native memory
            Utils.LocalFree(pGpsDevice);

            return device;
        }

        /// <summary>
        /// Creates our event thread that will receive native events
        /// </summary>
        private void CreateGpsEventThread()
        {
            // we only want to create the thread if we don't have one created already 
            // and we have opened the gps device
            if (gpsEventThread == null && gpsHandle != IntPtr.Zero)
            {
                // Create and start thread to listen for GPS events
                listening = true;
                gpsEventThread = new System.Threading.Thread(new System.Threading.ThreadStart(WaitForGpsEvents));
                gpsEventThread.Start();
            }
        }

        /// <summary>
        /// Method used to listen for native events from the GPS. 
        /// </summary>
        private void WaitForGpsEvents()
        {
            listening = true;
            // allocate 3 handles worth of memory to pass to WaitForMultipleObjects
            IntPtr handles = Utils.LocalAlloc(12);

            // write the three handles we are listening for.
            Marshal.WriteInt32(handles, 0, stopHandle.ToInt32());
            Marshal.WriteInt32(handles, 4, deviceStateChangedHandle.ToInt32());
            Marshal.WriteInt32(handles, 8, newLocationHandle.ToInt32());

            while (listening)
            {
//                Debug.AddLog("WaitForGpsEvents: before WaitForMultipleObjects", true);
                int obj = WaitForMultipleObjects(3, handles, 0, 5000);
//                Debug.AddLog("WaitForGpsEvents: after WaitForMultipleObjects. OBJ=" + System.Convert.ToString(obj), true);
                if (obj != waitFailed)
                {
                    switch (obj)
                    {
                        case 0:
                            // we've been signalled to stop
                            listening = false;
                            break;
                        case 1:
                            // device state has changed
                            if (deviceStateChanged != null)
                            {
                                deviceStateChanged(this, new DeviceStateChangedEventArgs(GetDeviceState()));
                            }
                            break;
                        case 2:
                            // location has changed
                            if (locationChanged != null)
                            {
                                locationChanged(this, new LocationChangedEventArgs(GetPosition()));
                            }
                            break;
                        default:
                            {
                                if (deviceNotFound != null)
                                    deviceNotFound(this, new EventArgs());

                                listening = false;
                                break;
                            }
                    }
                }
            }

            // free the memory we allocated for the native handles
            Utils.LocalFree(handles);

            // clear our gpsEventThread so that we can recreate this thread again
            // if the events are hooked up again.
            gpsEventThread = null;
        }

        public class LocationChangedEventArgs : EventArgs
        {
            public LocationChangedEventArgs(InternalGpsPosition position)
            {
                this.position = position;
            }

            /// <summary>
            /// Gets the new position when the GPS reports a new position.
            /// </summary>
            public InternalGpsPosition Position
            {
                get
                {
                    return position;
                }
            }

            private InternalGpsPosition position;
        }

        public class DeviceStateChangedEventArgs : EventArgs
        {
            public DeviceStateChangedEventArgs(InternalGpsDeviceState deviceState)
            {
                this.deviceState = deviceState;
            }

            /// <summary>
            /// Gets the new device state when the GPS reports a new device state.
            /// </summary>
            public InternalGpsDeviceState DeviceState
            {
                get
                {
                    return deviceState;
                }
            }

            private InternalGpsDeviceState deviceState;
        }

        #region PInvokes to gpsapi.dll
        [DllImport("gpsapi.dll")]
        static extern IntPtr GPSOpenDevice(IntPtr hNewLocationData, IntPtr hDeviceStateChange, string szDeviceName, int dwFlags);

        [DllImport("gpsapi.dll")]
        static extern int  GPSCloseDevice(IntPtr hGPSDevice);

        [DllImport("gpsapi.dll")]
        static extern int  GPSGetPosition(IntPtr hGPSDevice, IntPtr pGPSPosition, int dwMaximumAge, int dwFlags);

        [DllImport("gpsapi.dll")]
        static extern int  GPSGetDeviceState(IntPtr pGPSDevice);
        #endregion

        #region PInvokes to coredll.dll
        [DllImport("coredll.dll")]
        static extern IntPtr CreateEvent(IntPtr lpEventAttributes, int bManualReset, int bInitialState, StringBuilder lpName);

        [DllImport("coredll.dll")]
        static extern int CloseHandle(IntPtr hObject);

        const int waitFailed = -1;
        [DllImport("coredll.dll")]
        static extern int WaitForMultipleObjects(int nCount, IntPtr lpHandles, int fWaitAll, int dwMilliseconds);

        const int eventSet = 3;
        [DllImport("coredll.dll")]
        static extern int EventModify(IntPtr hHandle, int dwFunc);
        
#endregion

    }
}
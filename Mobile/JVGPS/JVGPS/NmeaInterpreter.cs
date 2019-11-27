//*********************************************************************
//**  A high-precision NMEA interpreter
//**  Written by Jon Person, author of "GPS.NET" (www.gpsdotnet.com)
//*********************************************************************

using System;
using System.Globalization;
using JVUtils;

namespace JVGPS
{
    public class NmeaInterpreter
    {
        int GPGSVsentences = 0;
        Satellite[] satellites = new Satellite[0];

        // Represents the EN-US culture, used for numers in NMEA sentences
        public static CultureInfo NmeaCultureInfo = new CultureInfo("en-US");

        // Used to convert knots into miles per hour
        public static double MPHPerKnot = double.Parse("1.150779", NmeaCultureInfo);

        #region Delegates
        public delegate void PositionReceivedEventHandler(string latitude, string longitude);
        public delegate void DateTimeChangedEventHandler(System.DateTime dateTime);
        public delegate void AltitudeChangedEventHandler(double altitude);
        public delegate void BearingReceivedEventHandler(double bearing);
        public delegate void SpeedReceivedEventHandler(double speed);
        public delegate void SpeedLimitReachedEventHandler();
        public delegate void FixObtainedEventHandler(FixType fixType);
        public delegate void FixLostEventHandler();
        public delegate void SatelliteReceivedEventHandler(Satellite[] Satellites);
        public delegate void HDOPReceivedEventHandler(double value);
        public delegate void VDOPReceivedEventHandler(double value);
        public delegate void PDOPReceivedEventHandler(double value);
        #endregion

        #region Events
        public event PositionReceivedEventHandler PositionReceived;
        public event DateTimeChangedEventHandler DateTimeChanged;
        public event AltitudeChangedEventHandler AltitudeChanged;
        public event BearingReceivedEventHandler BearingReceived;
        public event SpeedReceivedEventHandler SpeedReceived;
        public event SpeedLimitReachedEventHandler SpeedLimitReached;
        public event FixObtainedEventHandler FixObtained;
        public event FixLostEventHandler FixLost;
        public event SatelliteReceivedEventHandler SatelliteReceived;
        public event HDOPReceivedEventHandler HDOPReceived;
        public event VDOPReceivedEventHandler VDOPReceived;
        public event PDOPReceivedEventHandler PDOPReceived;
        #endregion
        
        // Processes information from the GPS receiver
        public bool Parse(string sentence)
        {
            Debug.AddLog("Parse: " + sentence);
            // Discard the sentence if its checksum does not match our calculated checksum
            if (!IsValid(sentence)) return false;
            // Look at the first word to decide where to go next
            switch (GetWords(sentence)[0])
            {
                case "$GPRMC":
                    // A "Recommended Minimum" sentence was found!
                    return ParseGPRMC(sentence);
                case "$GPGSV":
                    // A "Satellites in View" sentence was recieved
                    return ParseGPGSV(sentence);
                case "$GPGGA":
                    // 
                    ParseGPGGA(sentence);
                    return true;
                case "$GPGSA":
                    return ParseGPGSA(sentence);
                default:
                    // Indicate that the sentence was not recognized
                    return false;
            }
        }

        // Divides a sentence into individual words
        public string[] GetWords(string sentence)
        {
            return sentence.Split(',');
        }

        public void ParseGPGGA(string sentence)
        {
            // $GPGGA,123519,4807.038,N,01131.000,E,1,08,0.9,545.4,M,46.9,M,,*47
            //
            //Where:
            //     GGA          Global Positioning System Fix Data
            //     123519       Fix taken at 12:35:19 UTC
            //     4807.038,N   Latitude 48 deg 07.038' N
            //     01131.000,E  Longitude 11 deg 31.000' E
            //     1            Fix quality: 0 = invalid
            //                               1 = GPS fix (SPS)
            //                               2 = DGPS fix
            //                               3 = PPS fix
            //			                     4 = Real Time Kinematic
            //			                     5 = Float RTK
            //                               6 = estimated (dead reckoning) (2.3 feature)
            //			                     7 = Manual input mode
            //			                     8 = Simulation mode
            //     08           Number of satellites being tracked
            //     0.9          Horizontal dilution of position
            //     545.4,M      Altitude, Meters, above mean sea level
            //     46.9,M       Height of geoid (mean sea level) above WGS84
            //                      ellipsoid
            //     (empty field) time in seconds since last DGPS update
            //     (empty field) DGPS station ID number
            //     *47          the checksum data, always begins with *

            // Divide the sentence into words
            string[] Words = GetWords(sentence);

            if (!Words[9].Equals(""))
            {
                if (AltitudeChanged != null)
                    AltitudeChanged(float.Parse(Words[9], NmeaCultureInfo));
            }
        }

        // Interprets a $GPRMC message
        public bool ParseGPRMC(string sentence)
        {
            //$GPRMC,123519,A,4807.038,N,01131.000,E,022.4,084.4,230394,003.1,W*6A
            //
            //Where:
            //     RMC          Recommended Minimum sentence C
            //     123519       Fix taken at 12:35:19 UTC
            //     A            Status A=active or V=Void.
            //     4807.038,N   Latitude 48 deg 07.038' N
            //     01131.000,E  Longitude 11 deg 31.000' E
            //     022.4        Speed over the ground in knots
            //     084.4        Track angle in degrees True
            //     230394       Date - 23rd of March 1994
            //     003.1,W      Magnetic Variation
            //     *6A          The checksum data, always begins with *

            // Divide the sentence into words
            string[] Words = GetWords(sentence);
            // Do we have enough values to describe our location?
            if (Words[3] != "" & Words[4] != "" & Words[5] != "" & Words[6] != "")
            {
                // Notify the calling application of the change
                if (PositionReceived != null)
                {
                    double lat = double.Parse(Words[3], NmeaCultureInfo);
                    int integerPart = Utils.GetIntegerPart(lat / 100.0, NmeaCultureInfo);
                    lat = lat - (100 * integerPart);
                    lat = integerPart + (lat / 60.0);

                    if (Words[4].Equals("S"))
                    {
                        lat = -lat;
                    }

                    double lon = double.Parse(Words[5], NmeaCultureInfo);
                    integerPart = Utils.GetIntegerPart(lon / 100.0, NmeaCultureInfo);
                    lon = lon - (100 * integerPart);
                    lon = integerPart + (lon / 60.0);

                    if (Words[6].Equals("W"))
                    {
                        lon = -lon;
                    }
                    
                    PositionReceived(System.Convert.ToString(lat), System.Convert.ToString(lon));
                }
            }
            
            // Do we have enough values to parse satellite-derived time?
            if (Words[1] != "")
            {
                // Yes. Extract hours, minutes, seconds and milliseconds
                int UtcHours = Convert.ToInt32(Words[1].Substring(0, 2));
                int UtcMinutes = Convert.ToInt32(Words[1].Substring(2, 2));
                int UtcSeconds = Convert.ToInt32(Words[1].Substring(4, 2));
                int UtcMilliseconds = 0;
                // Extract milliseconds if it is available
                if (Words[1].Length > 7)
                {
                    UtcMilliseconds = Convert.ToInt32(Words[1].Substring(7));
                }
                // Now build a DateTime object with all values
                System.DateTime Today = System.DateTime.Now.ToUniversalTime();
                System.DateTime SatelliteTime = new System.DateTime(Today.Year, Today.Month, Today.Day, UtcHours, UtcMinutes, UtcSeconds, UtcMilliseconds);
                // Notify of the new time, adjusted to the local time zone
                if (DateTimeChanged != null)
                    DateTimeChanged(SatelliteTime.ToLocalTime());
            }
            
            // Do we have enough information to extract the current speed?
            if (Words[7] != "")
            {
                // Yes.  Parse the speed and convert it to MPH
                double Speed = double.Parse(Words[7], NmeaCultureInfo) * MPHPerKnot;
                // Notify of the new speed
                if (SpeedReceived != null)
                    SpeedReceived(Speed);
                // Are we over the highway speed limit?
                if (Speed > 55)
                    if (SpeedLimitReached != null)
                        SpeedLimitReached();
            }
            
            // Do we have enough information to extract bearing?
            if (Words[8] != "")
            {
                // Indicate that the sentence was recognized
                double Bearing = double.Parse(Words[8], NmeaCultureInfo);
                if (BearingReceived != null)
                    BearingReceived(Bearing);
            }

            // Indicate that the sentence was recognized
            return true;
        }

        // Interprets a "Satellites in View" NMEA sentence
        public bool ParseGPGSV(string sentence)
        {
            // $GPGSV,2,1,08,01,40,083,46,02,17,308,41,12,07,344,39,14,22,228,45*75
            //
            //Where:
            //      GSV          Satellites in view
            //      2            Number of sentences for full data
            //      1            sentence 1 of 2
            //      08           Number of satellites in view
            //
            //      01           Satellite PRN number
            //      40           Elevation, degrees
            //      083          Azimuth, degrees
            //      46           SNR - higher is better
            //      for up to 4 satellites per sentence
            //      *75          the checksum data, always begins with *
            
            int PseudoRandomCode = 0;
            int Azimuth = 0;
            int Elevation = 0;
            int SignalToNoiseRatio = 0;

            // Divide the sentence into words
            sentence = sentence.Substring(0, sentence.IndexOf('*'));
            if (sentence.EndsWith(","))
                sentence = sentence.Substring(0, sentence.Length - 1);
            string[] Words = GetWords(sentence);

            // If we are receiving the first GPGSV, we mark how much sentences will be received
            // before call the event
            if (!Words[2].Equals(""))
            {
                if (System.Convert.ToInt16(Words[2]) == 1)
                {
                    GPGSVsentences = System.Convert.ToInt16(Words[1]);
                    Array.Clear(satellites, 0, satellites.Length);
                    Array.Resize(ref satellites, 0);
                }

                // Each sentence contains four blocks of satellite information.  Read each block
                // and report each satellite's information
                int Count = 0;
                for (Count = 1; Count <= 4; Count++)
                {
                    // Does the sentence have enough words to analyze?
                    if ((Words.Length - 1) >= (Count * 4 + 3))
                    {
                        // Yes.  Proceed with analyzing the block.  Does it contain any information?
                        if (Words[Count * 4] != "" & Words[Count * 4 + 1] != "" & Words[Count * 4 + 2] != "" & Words[Count * 4 + 3] != "")
                        {
                            // Yes. Extract satellite information and report it
                            PseudoRandomCode = System.Convert.ToInt32(Words[Count * 4]);
                            Elevation = Convert.ToInt32(Words[Count * 4 + 1]);
                            Azimuth = Convert.ToInt32(Words[Count * 4 + 2]);
                            SignalToNoiseRatio = Convert.ToInt32(Words[Count * 4 + 3]);

                            Array.Resize(ref satellites, satellites.Length + 1);
                            satellites[satellites.Length - 1] = 
                                new Satellite(PseudoRandomCode, Elevation, Azimuth, SignalToNoiseRatio);
                        }
                    }
                }

                if (GPGSVsentences == System.Convert.ToInt16(Words[2]))
                {
                    // Notify with satellite records
                    if (SatelliteReceived != null)
                        SatelliteReceived(satellites);
                }
            }

            // Indicate that the sentence was recognized
            return true;
        }

        // Interprets a "Fixed Satellites and DOP" NMEA sentence
        public bool ParseGPGSA(string sentence)
        {
            //  $GPGSA,A,3,04,05,,09,12,,,24,,,,,2.5,1.3,2.1*39
            //
            //Where:
            //     GSA      Satellite status
            //     A        Auto selection of 2D or 3D fix (M = manual) 
            //     3        3D fix - values include: 1 = no fix
            //                                       2 = 2D fix
            //                                       3 = 3D fix
            //     04,05... PRNs of satellites used for fix (space for 12) 
            //     2.5      PDOP (dilution of precision) 
            //     1.3      Horizontal dilution of precision (HDOP) 
            //     2.1      Vertical dilution of precision (VDOP)
            //     *39      the checksum data, always begins with *
            
            // Divide the sentence into words
            string[] Words = GetWords(sentence);

            FixType ft;
            ft = FixType.Unknown;

            if (Words[1].Equals("A"))
            {
                if (Words[2].Equals("2"))
                    ft = FixType.XyD;
                if (Words[2].Equals("3"))
                    ft = FixType.XyzD;

                if (FixObtained != null)
                   FixObtained(ft);
            }
            else
            {
                if (FixLost != null)
                   FixLost();
            }

            // Get the PRN of satellites
            for (int i = 3; i < 15; i++)
            {
                if (!Words[i].Equals(""))
                {
                }
            }

            // Update the DOP values
            if (Words[15] != "")
            {
                if (PDOPReceived != null)
                    PDOPReceived(double.Parse(Words[15], NmeaCultureInfo));
            }
            if (Words[16] != "")
            {
                if (HDOPReceived != null)
                    HDOPReceived(double.Parse(Words[16], NmeaCultureInfo));
            }
            if (Words[17] != "")
            {
                if (VDOPReceived != null)
                    VDOPReceived(double.Parse(Words[17], NmeaCultureInfo));
            }
            return true;
        }

        // Returns True if a sentence's checksum matches the calculated checksum
        public bool IsValid(string sentence)
        {
            // Compare the characters after the asterisk to the calculation
            return sentence.Substring(sentence.IndexOf("*") + 1) == GetChecksum(sentence);
        }

        // Calculates the checksum for a sentence
        public string GetChecksum(string sentence)
        {
            // Loop through all chars to get a checksum
            //INSTANT C# NOTE: Commented this declaration since looping variables in 'foreach' loops are declared in the 'foreach' header in C#
            //        char Character = '\0';
            int Checksum = 0;
            foreach (char Character in sentence)
            {
                if (Character == '$')
                {
                    // Ignore the dollar sign
                }
                else if (Character == '*')
                {
                    // Stop processing before the asterisk
                    break;
                }
                else
                {
                    // Is this the first value for the checksum?
                    if (Checksum == 0)
                    {
                        // Yes. Set the checksum to the value
                        Checksum = Convert.ToByte(Character);
                    }
                    else
                    {
                        // No. XOR the checksum with this character's value
                        Checksum = Checksum ^ Convert.ToByte(Character);
                    }
                }
            }
            // Return the checksum formatted as a two-character hexadecimal
            return Checksum.ToString("X2");
        }
    }
}

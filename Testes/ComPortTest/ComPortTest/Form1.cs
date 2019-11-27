using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Diagnostics;
using OpenNETCF;

namespace ComPortTest
{
    public partial class Form1 : Form
    {
        OpenNETCF.IO.Serial.Port m_Port;
        string m_cGPSBufferRemaining;
        stGPSPosition m_stLastPositionRead;
        stGPSPosition m_stLastPositionUploaded;
        bool isStarted;
        CultureInfo m_ci;
        CultureInfoCollection cic;
        string[] logs = new string[0];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 15; i++)
            {
                comboBoxPort.Items.Add("COM" + i);
            }
            comboBoxPort.SelectedIndex = 3;

            comboBoxBaudRate.Items.Clear();
            comboBoxBaudRate.Items.Add("2400");
            comboBoxBaudRate.Items.Add("4800");
            comboBoxBaudRate.Items.Add("9600");
            comboBoxBaudRate.Items.Add("14400");
            comboBoxBaudRate.Items.Add("19200");
            comboBoxBaudRate.Items.Add("38400");
            comboBoxBaudRate.Items.Add("56000");
            comboBoxBaudRate.Items.Add("57600");
            comboBoxBaudRate.SelectedIndex = 7;

            m_Port = new OpenNETCF.IO.Serial.Port("COM1:");
            m_Port.Settings.Parity = 0;
            m_Port.Settings.StopBits = 0;
            m_Port.DataReceived += port_DataReceived;

            miStart.Text = "Start";
            log.Items.Clear();
            isStarted = false;
            m_ci = null; // new CultureInfo("en-US");

            cic = new CultureInfoCollection();

            for (int i = 0; i < cic.Count(); i++)
            {
                cbCulture.Items.Add(cic[i].Culture);
            }

            cbCulture.SelectedIndex = 65;
        }

        private bool OpenCOMPort()
        {
            try
            {
                m_Port.PortName = "COM" + System.Convert.ToString(comboBoxPort.SelectedIndex + 1) + ":";
                switch (comboBoxBaudRate.SelectedIndex)
                {
                    case 0:
                        m_Port.Settings.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_2400;
                        break;

                    case 1:
                        m_Port.Settings.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_4800;
                        break;

                    case 2:
                        m_Port.Settings.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_9600;
                        break;

                    case 3:
                        m_Port.Settings.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_14400;
                        break;

                    case 4:
                        m_Port.Settings.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_19200;
                        break;

                    case 5:
                        m_Port.Settings.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_38400;
                        break;

                    case 6:
                        m_Port.Settings.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_56000;
                        break;

                    default:
                        m_Port.Settings.BaudRate = OpenNETCF.IO.Serial.BaudRates.CBR_57600;
                        break;
                }
                AddLog("Port: " + m_Port.PortName + " BR: " + m_Port.Settings.BaudRate.ToString());
                AddLog("Before open.");
                m_Port.Open();
                AddLog("After open.");
            }
            catch (Exception exception)
            {
                if (exception.Message.StartsWith("CreateFile Failed"))
                {
                    AddLog("OpenCOMPort: Unable to open port " + m_Port.PortName);
                }
                else
                {
                    AddLog ("OpenCOMPort: " + exception.Message);
                }

                Stop();
                return false;
            }

            return true;
        }

        private void port_DataReceived()
        {
            if (m_Port.InBufferCount > 0)
            {
                try
                {
                    byte[] bytes = new byte[0x800];
                    bytes = m_Port.Input;
                    string str = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                    m_cGPSBufferRemaining = m_cGPSBufferRemaining + str;
                    for (int i = m_cGPSBufferRemaining.IndexOf("\r\n"); i != -1; i = m_cGPSBufferRemaining.IndexOf("\r\n"))
                    {
                        string str2 = m_cGPSBufferRemaining.Substring(0, i);
                        m_cGPSBufferRemaining = m_cGPSBufferRemaining.Substring(i + 2, (m_cGPSBufferRemaining.Length - i) - 2);

                        Array.Resize(ref logs, logs.Length + 1);
                        logs[logs.Length-1] = str2;

                        if (str2.StartsWith("$GPGGA"))
                        {
                            m_stLastPositionRead.cGPSMessage = str2;
                            m_stLastPositionRead.dtOccurred = DateTime.Now;
                            m_stLastPositionRead.nLatitude = 0.0;
                            m_stLastPositionRead.nLongitude = 0.0;
                            m_stLastPositionRead.nAltitude = 0.0;
                            m_stLastPositionRead.nSpeed = 0.0;
                            m_stLastPositionRead.nAngle = -1.0;
                        }
                    }
                }
                catch (Exception exception)
                {
                    DoUpdate(exception.Message);
                }
            }
        }

        private void DoUpdate(string message)
        {
            AddLog("DoUpdate: " + message);
            if (this.InvokeRequired)
            {
                base.Invoke(new UpdateStatusDelegate(this.DoUpdate), new object[] { message });
            }
            else
            {
                AddLog ("DoUpdate: " + message);
            }
        }

        private void Start()
        {
            Array.Clear(logs, 0, logs.Length);

            if (cbCulture.SelectedIndex == -1)
            {
                MessageBox.Show("Please select your culture.", "Error");
            }
            else
            {
                m_ci = new CultureInfo(cic[cbCulture.SelectedIndex].Name);

                AddLog("Starting...");
                Application.DoEvents();
                m_Port.Close();

                if (!OpenCOMPort())
                {
                    AddLog("Not found.");
                }
                else
                {
                    isStarted = true;
                    AddLog("The port is open.");
                    miStart.Text = "Stop";

                    AddLog("Waiting for GPS response...");
                    tmCheckConnection.Interval = 0x1388;
                    tmCheckConnection.Enabled = true;

                    tmGetData.Interval = 0x1388;
                    tmGetData.Enabled = true;
                }
            }
        }

        private void Stop()
        {
            tmCheckConnection.Enabled = false;
            tmGetData.Enabled = false;

            if (m_Port.IsOpen)
            {
                AddLog("Closing port...");
                m_Port.Close();
                AddLog("Stopped.");
            }

            isStarted = false;
            miStart.Text = "Start";
            Application.DoEvents();

            StreamWriter sw = File.CreateText("\\temp\\gpslog.txt");

            foreach (string l in logs)
                sw.WriteLine(l);

            sw.Flush();
            sw.Close();
        }

        void AddLog(string message)
        {
            if (message != null && !message.Equals (""))
                log.Items.Add(message);
        }

        private delegate void UpdateStatusDelegate(string message);

        private void miStart_Click(object sender, EventArgs e)
        {
            if (!isStarted)
            {
                Start();
            }
            else
            {
                Stop();
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            DoClose();
            base.Close();
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            DoClose();
        }

        void DoClose()
        {
            Stop();
        }

        private void miSaveLog_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = File.CreateText(saveFileDialog1.FileName);

                for (int i = 0; i < log.Items.Count; i++)
                {
                    sw.WriteLine(log.Items[i].ToString());
                }

                sw.Flush();
                sw.Close();
            }
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            log.Items.Clear();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (m_Port.IsOpen)
            {
                string cGPSMessage = m_stLastPositionRead.cGPSMessage;

                switch (cGPSMessage)
                {
                    case "":
                    case null:
                        return;
                }

                string[] strArray = cGPSMessage.Split(new char[] { ',' });
                
                if (strArray.Length >= 8)
                {
                    if (strArray[6] == "0")
                    {
                        AddLog("Searching for satellites.");
                    }
                    else
                    {
                        AddLog (strArray[7] + " Connected.");
                    }
                }
            }
        }

        private void tmGetData_Tick(object sender, EventArgs e)
        {
            if (m_Port.IsOpen && m_stLastPositionRead.cGPSMessage != null)
            {
                string[] strArray = m_stLastPositionRead.cGPSMessage.Split(new char[] { ',' });

                if ((strArray.Length < 10) || !(strArray[6] != "0"))
                {
                    return;
                }
                else
                {
                    // Getting latitude
                    double num = double.Parse(strArray[2], m_ci);
                    int integerPart = GetIntegerPart(num / 100.0, m_ci);
                    double num3 = num - (100 * integerPart);
                    num = integerPart + (num3 / 60.0);
                    if (strArray[3] == "S")
                    {
                        num = -num;
                    }
                    m_stLastPositionRead.nLatitude = num;

                    // Getting longitude
                    double num4 = double.Parse(strArray[4], m_ci);
                    integerPart = GetIntegerPart(num4 / 100.0, m_ci);
                    num3 = num4 - (100 * integerPart);
                    num4 = integerPart + (num3 / 60.0);
                    if (strArray[5] == "W")
                    {
                        num4 = -num4;
                    }
                    m_stLastPositionRead.nLongitude = num4;

                    // Getting altitude
                    m_stLastPositionRead.nAltitude = float.Parse(strArray[9], m_ci);

                    // Getting speed
                    double num5 = DistanceTo(m_stLastPositionUploaded.nLatitude, m_stLastPositionUploaded.nLongitude, m_stLastPositionRead.nLatitude, m_stLastPositionRead.nLongitude);
                    TimeSpan span = m_stLastPositionRead.dtOccurred.Subtract(m_stLastPositionUploaded.dtOccurred);
                    m_stLastPositionRead.nSpeed = num5 / span.TotalSeconds;

                    // Getting angle
                    m_stLastPositionRead.nAngle = AngleTo(m_stLastPositionUploaded.nLatitude, m_stLastPositionUploaded.nLongitude, m_stLastPositionRead.nLatitude, m_stLastPositionRead.nLongitude);

                    m_stLastPositionUploaded.cGPSMessage = m_stLastPositionRead.cGPSMessage;
                    m_stLastPositionUploaded.dtOccurred = m_stLastPositionRead.dtOccurred;
                    m_stLastPositionUploaded.nLatitude = m_stLastPositionRead.nLatitude;
                    m_stLastPositionUploaded.nLongitude = m_stLastPositionRead.nLongitude;
                    m_stLastPositionUploaded.nAltitude = m_stLastPositionRead.nAltitude;
                    m_stLastPositionUploaded.nSpeed = m_stLastPositionRead.nSpeed;
                    m_stLastPositionUploaded.nAngle = m_stLastPositionRead.nAngle;

                    AddLog ("lat: " + System.Convert.ToString(m_stLastPositionUploaded.nLatitude) + 
                            "lon: " + System.Convert.ToString(m_stLastPositionUploaded.nLongitude) + 
                            "alt: " + System.Convert.ToString(m_stLastPositionUploaded.nAltitude));
                }
            }
//            else if (OpenCOMPort())
//            {
//                tmGetData.Interval = 0x1388;
//                AddLog ("Waiting for GPS response...");
//            }
//
        }

        public static double AngleTo(double lat1, double lon1, double lat2, double lon2)
        {
            lat1 = DegreesToRadians(lat1);
            lon1 = DegreesToRadians(lon1);
            lat2 = DegreesToRadians(lat2);
            lon2 = DegreesToRadians(lon2);
            double radians = -Math.Atan2(Math.Sin(lon1 - lon2) * Math.Cos(lat2), (Math.Cos(lat1) * Math.Sin(lat2)) - ((Math.Sin(lat1) * Math.Cos(lat2)) * Math.Cos(lon1 - lon2)));
            if (radians < 0.0)
            {
                radians += 6.2831853071795862;
            }
            return RadiansToDegrees(radians);
        }

        private static double DegreesToRadians(double degrees)
        {
            return (degrees / 57.29578);
        }

        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2)
        {
            double num = 6371.0;
            double num2 = DegreesToRadians(lat2 - lat1);
            double num3 = DegreesToRadians(lon2 - lon1);
            lat1 = DegreesToRadians(lat1);
            lat2 = DegreesToRadians(lat2);
            double a = (Math.Sin(num2 / 2.0) * Math.Sin(num2 / 2.0)) + (((Math.Cos(lat1) * Math.Cos(lat2)) * Math.Sin(num3 / 2.0)) * Math.Sin(num3 / 2.0));
            double num5 = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            double num6 = num * num5;
            return (num6 * 1000.0);
        }

        public static string GetDecimalPart(double nValue, CultureInfo ci)
        {
            return nValue.ToString(ci).Split(new char[] { '.' })[1];
        }

        public static int GetIntegerPart(double nValue, CultureInfo ci)
        {
            return int.Parse(nValue.ToString(ci).Split(new char[] { '.' })[0]);
        }

        private static double RadiansToDegrees(double radians)
        {
            return (radians * 57.29578);
        }
    }

   [StructLayout(LayoutKind.Sequential)]
    public struct stGPSPosition
    {
        public string cGPSMessage;
        public DateTime dtOccurred;
        public double nLatitude;
        public double nLongitude;
        public double nAltitude;
        public double nSpeed;
        public double nAngle;
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using Microsoft.WindowsMobile;
using Microsoft.WindowsMobile.Status;
//using OpenNETCF.Net;

namespace ConnectionTest
{
    public partial class Form1 : Form
    {
        string iif(bool cond, string rsOk, string resFalse)
        {
            if (cond) { return rsOk; } else { return resFalse; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        SystemState wlan = null;
        SystemState gprs = null;
        SystemState activesync = null;

        public void Subscribe()
        {
            gprs = new SystemState(SystemProperty.ConnectionsCellularCount);
            gprs.Changed += new ChangeEventHandler(GPRS_Changed);

            activesync = new
            SystemState(SystemProperty.ConnectionsDesktopCount);
            activesync.Changed += new
            ChangeEventHandler(DesktopConnection_Changed);

            wlan = new SystemState(SystemProperty.ConnectionsNetworkCount);
            wlan.Changed += new ChangeEventHandler(WLAN_Changed);
        }

        void DesktopConnection_Changed(object sender, ChangeEventArgs args)
        {
            if (SystemState.ConnectionsDesktopCount > 0)
            {
                listBox1.Items.Add("DesktopConnection connected");
            }
            else
            {
                listBox1.Items.Add("DesktopConnection disconnected");
            }
        }

        void GPRS_Changed(object sender, ChangeEventArgs args)
        {
            if (SystemState.ConnectionsCellularCount > 0)
            {
                listBox1.Items.Add("CellularConnection connected");
            }
            else
            {
                listBox1.Items.Add("CellularConnection disconnected");
            }
        }

        void WLAN_Changed(object sender, ChangeEventArgs args)
        {
            if (SystemState.ConnectionsNetworkCount > 0)
            {
                listBox1.Items.Add("WLanConnection connected");
            }
            else
            {
                listBox1.Items.Add("WLanConnection disconnected");
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < SystemState.ConnectionsCellularCount; i++)
            {
                listBox1.Items.Add(SystemState.ConnectionsCellularDescriptions[i]);
            }
            listBox1.Items.Add("AS status" + SystemState.ActiveSyncStatus.ToString());
            listBox1.Items.Add("1xrtt avail: " + iif (SystemState.CellularSystemAvailable1xrtt, "y", "n"));
            listBox1.Items.Add("edge avail: " + iif (SystemState.CellularSystemAvailableEdge, "y", "n"));
            listBox1.Items.Add("evdo avail: " + iif (SystemState.CellularSystemAvailableEvdo, "y", "n"));
            listBox1.Items.Add("evdv avail: " + iif (SystemState.CellularSystemAvailableEvdv, "y", "n"));
            listBox1.Items.Add("grps avail: " + iif (SystemState.CellularSystemAvailableGprs, "y", "n"));
            listBox1.Items.Add("hsdpa avail: " + iif (SystemState.CellularSystemAvailableHsdpa, "y", "n"));
            listBox1.Items.Add("umts avail: " + iif (SystemState.CellularSystemAvailableUmts, "y", "n"));
            listBox1.Items.Add("1xrtt conn: " + iif (SystemState.CellularSystemConnected1xrtt, "y", "n"));
            listBox1.Items.Add("csd conn: " + iif (SystemState.CellularSystemConnectedCsd, "y", "n"));
            listBox1.Items.Add("edge conn: " + iif (SystemState.CellularSystemConnectedEdge, "y", "n"));
            listBox1.Items.Add("evdo conn: " + iif (SystemState.CellularSystemConnectedEvdo, "y", "n"));
            listBox1.Items.Add("evdv conn: " + iif (SystemState.CellularSystemConnectedEvdv, "y", "n"));
            listBox1.Items.Add("grps conn: " + iif (SystemState.CellularSystemConnectedGprs, "y", "n"));
            listBox1.Items.Add("hsdpa conn: " + iif (SystemState.CellularSystemConnectedHsdpa, "y", "n"));
            listBox1.Items.Add("umts conn: " + iif (SystemState.CellularSystemConnectedUmts, "y", "n"));
            listBox1.Items.Add("gprs cover: " + iif (SystemState.PhoneGprsCoverage, "y", "n"));
            listBox1.Items.Add("wifi connected: " + iif (SystemState.WiFiStateConnected, "y", "n"));
            listBox1.Items.Add("wifi connecting: " + iif (SystemState.WiFiStateConnecting, "y", "n"));
            listBox1.Items.Add("wifi hardware present: " + iif (SystemState.WiFiStateHardwarePresent, "y", "n"));
            listBox1.Items.Add("wifi net avail: " + iif (SystemState.WiFiStateNetworksAvailable, "y", "n"));
            listBox1.Items.Add("wifi power on: " + iif (SystemState.WiFiStatePowerOn, "y", "n"));

            listBox1.Items.Add("");
            listBox1.Items.Add("");
            listBox1.Items.Add("");
            listBox1.Items.Add("");

            if (SystemState.WiFiStateHardwarePresent)
            {
                
            }

            string url = "www.msn.com";
            bool res = GPRSConnection.Setup("http://" + url + "/");
            if (res)
            {
                TcpClient tc = new TcpClient(url, 80);
                NetworkStream ns = tc.GetStream();
                byte[] buf = new byte[100];
                ns.Write(buf, 0, 100);
                tc.Client.Shutdown(SocketShutdown.Both);
                ns.Close();
                tc.Close();
                MessageBox.Show("Wrote 100 bytes");
            }
            else
            {
                MessageBox.Show("Connection establishment failed");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Subscribe();
        }

/*        void UpdateAdapters()
        {
            // Get the available adapters
            m_adapters = Networking.GetAdapters();

            // Clear the combo
            cboAdapters.Items.Clear();

            // Add the adapters
            foreach (Adapter adapter in m_adapters)
            {
                cboAdapters.Items.Add(adapter);
            }
        }
        void UpdateConfig()
        {
            tabConfiguration.SuspendLayout();

            // Update the adapter's configuration information
            lblMACAddress.Text =
                BitConverter.ToString(m_currentAdapter.MacAddress);

            lblIPAddress.Text = m_currentAdapter.CurrentIpAddress;
            lblSubnet.Text = m_currentAdapter.CurrentSubnetMask;
            lblGateway.Text = m_currentAdapter.Gateway;

            // Wireless information
            bool wireless = m_currentAdapter.IsWireless;
            if (wireless)
            {
                lblIsWireless.Text = "True";
                lblWZCCompat.Text =
                    m_currentAdapter.IsWirelessZeroConfigCompatible.
                                                            ToString();
            }
            else
            {
                lblIsWireless.Text = "False";
                lblWZCCompat.Text = "N/A";
            }

            // Dynamic Host Configuration Protocol (DHCP) information
            bool dhcpEnabled = m_currentAdapter.DhcpEnabled;
            if (dhcpEnabled)
            {
                lblDHCPEnabled.Text = "True";
                lblLeaseObtained.Text =
                    m_currentAdapter.LeaseObtained.ToShortDateString();
                lblLeaseExpires.Text =
                    m_currentAdapter.LeaseExpires.ToShortDateString(); ;
                mnuRenew.Enabled = true;
            }
            else
            {
                lblDHCPEnabled.Text = "False";
                lblLeaseObtained.Text = "N/A";
                lblLeaseExpires.Text = "N/A";
                mnuRenew.Enabled = false;
            }

            tabConfiguration.ResumeLayout();
        }
        /**/

    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IO.Serial;
using JVUtils;

namespace JVGPS.Forms
{
    public partial class GPSSettings : Form
    {
        #region Internal Variables
        private string _comPortNotSelected = "The serial port was not selected.";
        private string _baudRateNotSelected = "The baud rate was not selected.";
        bool _windowsGPS;
        bool _backupGPS;
        #endregion

        #region Public Properties
        public bool UsingWindowsGPS
        {
            get { return _windowsGPS; }
            set 
            { 
                _windowsGPS = value;
                _backupGPS = value;

                tmConfig.Enabled = true;

                rbUseInternal.Checked = _windowsGPS;
                rbUseManualGPS.Checked = !_windowsGPS;
                ConfigureCombos();
            }
        }
        public string SerialPortText
        {
            get { return lblSerialPort.Text; }
            set { lblSerialPort.Text = value; }
        }
        public string BaudRateText
        {
            get { return lblBaudRate.Text; }
            set { lblBaudRate.Text = value; }
        }
        public string UseWindowsGPSText
        {
            get { return rbUseInternal.Text; }
            set { rbUseInternal.Text = value; }
        }
        public string UseManualGPSText
        {
            get { return rbUseManualGPS.Text; }
            set { rbUseManualGPS.Text = value; }
        }
        public string Caption
        {
            get { return lblCaption.Text; }
            set { lblCaption.Text = value; }
        }
        public string MenuOkText
        {
            get { return miOk.Text; }
            set { miOk.Text = value; }
        }
        public string MenuCancelText
        {
            get { return miCancel.Text; }
            set { miCancel.Text = value; }
        }
        public string MessageSerialPortNotSelected
        {
            get { return _comPortNotSelected; }
            set { _comPortNotSelected = value; }
        }
        public string MessageBaudRateNotSelected
        {
            get { return _baudRateNotSelected; }
            set { _baudRateNotSelected = value; }
        }
        public string SelectedSerialPort
        {
            get 
            { 
                if (comboBoxPort.SelectedIndex == -1) 
                {
                    return "";
                }
                else 
                {
                    return (string)comboBoxPort.Items[comboBoxPort.SelectedIndex]; 
                }
            }
            set 
            {
                if (value == null || value.Equals(""))
                {
                    comboBoxPort.SelectedIndex = -1;
                }
                else
                {
                    for (int i = 0; i < comboBoxPort.Items.Count; i++)
                    {
                        if (((string)comboBoxPort.Items[i]).ToLower().Equals(value.ToLower()))
                        {
                            comboBoxPort.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
        }
        public BaudRates SelectedBaudRate
        {
            get
            {
                if (comboBoxBaudRate.SelectedIndex == -1)
                {
                    return BaudRates.CBR_110;
                }
                else
                {
                    return Utils.ConvertStringToBaudRate((string)comboBoxBaudRate.Items[comboBoxBaudRate.SelectedIndex]);
                }
            }
            set
            {
                for (int i = 0; i < comboBoxBaudRate.Items.Count; i++)
                {
                    if (((string)comboBoxBaudRate.Items[i]).ToLower().Equals(Utils.ConvertBaudeRateToString(value)))
                    {
                        comboBoxBaudRate.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        #endregion

        public GPSSettings()
        {
            InitializeComponent();

            comboBoxPort.Items.Clear();
            for (int i = 1; i < 15; i++)
            {
                comboBoxPort.Items.Add("COM" + i);
            }

            comboBoxBaudRate.Items.Clear();
            comboBoxBaudRate.Items.Add("2400");
            comboBoxBaudRate.Items.Add("4800");
            comboBoxBaudRate.Items.Add("9600");
            comboBoxBaudRate.Items.Add("14400");
            comboBoxBaudRate.Items.Add("19200");
            comboBoxBaudRate.Items.Add("38400");
            comboBoxBaudRate.Items.Add("56000");
            comboBoxBaudRate.Items.Add("57600");
        }

        private void rbUseInternal_Click_1(object sender, EventArgs e)
        {
            rbUseInternal.Checked = rbUseInternal == (CheckBox)sender;
            rbUseManualGPS.Checked = !rbUseInternal.Checked;
            _windowsGPS = rbUseInternal.Checked;
            ConfigureCombos();
        }

        private void ConfigureCombos()
        {
            comboBoxPort.Enabled = !_windowsGPS;
            comboBoxBaudRate.Enabled = comboBoxPort.Enabled;

            if (_windowsGPS)
            {
                rbUseInternal.Focus();
            } else 
            {
                comboBoxPort.Focus();
            } 
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void miOk_Click(object sender, EventArgs e)
        {
            bool ok = true;
            if (rbUseManualGPS.Checked)
            {
                if (comboBoxPort.SelectedIndex == -1)
                {
                    MessageBox.Show(_comPortNotSelected, "!");
                    comboBoxPort.Focus();
                    ok = false;
                }
                else if (comboBoxBaudRate.SelectedIndex == -1)
                {
                    MessageBox.Show(_baudRateNotSelected, "!");
                    comboBoxBaudRate.Focus();
                    ok = false;
                }
            }

            if (ok)
                DialogResult = DialogResult.OK;
        }

        private void tmConfig_Tick(object sender, EventArgs e)
        {
            tmConfig.Enabled = false;
            _windowsGPS = _backupGPS;
            rbUseInternal.Checked = _windowsGPS;
            rbUseManualGPS.Checked = !_windowsGPS;

            ConfigureCombos();
        }
    }
}
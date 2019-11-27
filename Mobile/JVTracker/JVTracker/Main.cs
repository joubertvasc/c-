using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace JVTracker
{
    public partial class Main : Form
    {
        #region Internal Variables
        bool bExiting = false;
        int heightOffSet = 21;
        string rtPath = "";
        DateTime dtLastExec = DateTime.MinValue;
        int executedTimes = 0;
        #if trial
        bool bExpired  = false;
        #endif
        #endregion

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            #if trial
            lblProjectName.Text += " Trial Version";
            lblTrial.Text = "RemoteTracker will be asked only 5 times.";
            #endif
            ipTracker.Enabled = false;

            // Realign components using screen metrics
            ResizeScreen();

            // Verify if RemoteTracker is installed and configured
            rtPath = GetRemoteTrackerPath();

            if (rtPath == "")
            {
                lblStatus.Text = "RemoteTracker was not found.";
                lblStatus.Font = new Font("Tahoma", 9F, FontStyle.Bold);
                lblStatus.ForeColor = Color.Red;
                pbStart.Image = ilStartStop.Images[4];
                pbStop.Image = ilStartStop.Images[5];
                pbStart.Tag = -1;
                pbStop.Tag = -1;
            }
            else
            {
                // Load configuration from registry
                LoadConfiguration();

                // Verify messages
                VerifyMessages();
            }
        }

        private void Main_Closing(object sender, CancelEventArgs e)
        {
            CloseApp();
        }

        private void miApply_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            CloseApp();
            Application.Exit();
        }

        private void pbStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (rtPath != "" && (int)pbStart.Tag == 0)
                pbStart.Image = ilStartStop.Images[1];
        }

        private void pbStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (rtPath != "" && (int)pbStart.Tag == 0)
            {
                pbStart.Image = ilStartStop.Images[0];
                StartLog();
            }
        }

        private void pbStop_MouseDown(object sender, MouseEventArgs e)
        {
            if (rtPath != "" && (int)pbStop.Tag == 0)
                pbStop.Image = ilStartStop.Images[3];
        }

        private void pbStop_MouseUp(object sender, MouseEventArgs e)
        {
            if (rtPath != "" && (int)pbStop.Tag == 0)
            {
                pbStop.Image = ilStartStop.Images[2];
                StopLog();
            }
        }

        private void cbSMS_CheckStateChanged(object sender, EventArgs e)
        {
            cbGPRS.Checked = !cbSMS.Checked;
            SetConfigTab();
        }

        private void cbGPRS_CheckStateChanged(object sender, EventArgs e)
        {
            cbSMS.Checked = !cbGPRS.Checked;
            SetConfigTab();
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            miApply.Enabled = tcMain.SelectedIndex == 1;
        }

        private void nuInterval_ValueChanged(object sender, EventArgs e)
        {
            // Set the interval for timer
            tmMain.Interval = ((int)nuInterval.Value * 60000);
        }

        private void tbPhoneNumber_GotFocus(object sender, EventArgs e)
        {
            ipTracker.Enabled = true;
        }

        private void tbPhoneNumber_LostFocus(object sender, EventArgs e)
        {
            ipTracker.Enabled = false;
        }

        private void ipTracker_EnabledChanged(object sender, EventArgs e)
        {
            if (!bExiting)
            {
                if (ipTracker.Enabled)
                {
                    pnlConfig.Height -= (ipTracker.Bounds.Height - heightOffSet);
                    pnlMain.Height -= (ipTracker.Bounds.Height - heightOffSet);
                }
                else
                {
                    pnlConfig.Height = tpConfig.Height;
                    pnlMain.Height = tpMain.Height;
                }
            }

        }

        private void lblURL_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(lblURL.Text, "");
        }

        private void lblContact_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:joubertvasc.gmail.com", "");
        }

        private void tmMain_Tick(object sender, EventArgs e)
        {
            // Set timer off
            tmMain.Enabled = false;

            // Call Remotetracker to send coordinates
            CallRemoteTracker();

            // Set timer on again to wait another turn
            dtLastExec = DateTime.Now;
            executedTimes++;

            StartLog();
        }

        private void tmReg_Tick(object sender, EventArgs e)
        {
            // Load configuration from Registry
            tmReg.Enabled = false;
            try
            {
                VerifyMessages();
            }
            finally
            {
                tmReg.Enabled = true;
            }
        }

        void CloseApp()
        {
            tmMain.Enabled = false;
            bExiting = true;
            ipTracker.Enabled = false;
            SaveConfiguration();
        }

        void ResizeScreen()
        {
            tcMain.Width = Screen.PrimaryScreen.WorkingArea.Width;
            tcMain.Height = Screen.PrimaryScreen.WorkingArea.Height;

            int x = 0;

            if (tcMain.Width > 240)
            {
                x = (tcMain.Width - 6 - (pbStart.Size.Width / 2) - (pbStop.Size.Width / 2)) / 2;
            }
            else
            {
                x = (tcMain.Width - 6 - pbStart.Size.Width - pbStop.Size.Width) / 2;
            }

            pbStart.Location = new Point(x, 3);
            if (tpMain.Width > 240)
            {
                pbStop.Location = new Point(pbStart.Location.X + 6 + (pbStart.Size.Width / 2), 3);
            }
            else
            {
                pbStop.Location = new Point(pbStart.Location.X + 6 + pbStart.Size.Width, 3);
            }
        }

        string GetRemoteTrackerPath()
        {
            string result = "";
            try
            {
                RegistryKey r = Registry.LocalMachine.OpenSubKey("\\Software\\Microsoft\\Inbox\\Rules\\RemoteTracker");
                result = (string)r.GetValue("Command", "");
                r.Close();

                if (result != "")
                {
                    result = result.Substring(0, result.LastIndexOf(@"\") + 1);
                }
            }
            catch
            { }

            return result;
        }

        void LoadConfiguration()
        {
            Configuration.LoadConfiguration();

            // Copy configuration to Config components
            cbSMS.Checked = Configuration.UseSMS;
            cbGPRS.Checked = !cbSMS.Checked;
            tbPhoneNumber.Text = Configuration.PhoneNumber;
            tbWebServer.Text = Configuration.WebServer;
            nuInterval.Value = Configuration.Interval;
            cbAutoStart.Checked = Configuration.AutoStart;

            miApply.Enabled = false;

            SetConfigTab();

            // Set the interval for timer
            tmMain.Interval = Configuration.Interval * 60000;

            // Autostart?
            if (Configuration.AutoStart) {
                StartLog();
            } else {
                StopLog();
            }
        }

        void SaveConfiguration()
        {
            Configuration.AutoStart = cbAutoStart.Checked;
            Configuration.Interval = (int)nuInterval.Value;
            Configuration.PhoneNumber = tbPhoneNumber.Text;
            Configuration.UseSMS = cbSMS.Checked;
            Configuration.WebServer = tbWebServer.Text;
            Configuration.SaveConfiguration();
        }

        void SetConfigTab()
        {
            if (cbSMS.Checked)
            {
                cbGPRS.Checked = false;
                tbWebServer.Enabled = false;
                tbPhoneNumber.Enabled = true;

                if (tcMain.SelectedIndex == 1)
                {
                    tbPhoneNumber.Focus();
                }
            }

            if (cbGPRS.Checked)
            {
                cbSMS.Checked = false;
                tbPhoneNumber.Enabled = false;
                tbWebServer.Enabled = true;

                if (tcMain.SelectedIndex == 1)
                {
                    tbWebServer.Focus();
                }
            }
        }

        void StartLog()
        {
            bool bOk = true;
            #if trial
            if (executedTimes == 6)
            {
                lblStatus.Text = "*** EXPIRED ***";
                lblStatus.Font = new Font("Tahoma", 9F, FontStyle.Bold);
                lblStatus.ForeColor = Color.Red;
                pbStart.Image = ilStartStop.Images[4];
                pbStop.Image = ilStartStop.Images[5];
                pbStart.Tag = -1;
                pbStop.Tag = -1;
                bOk = false;
                tmReg.Enabled = false;
                bExpired = true;
            }
            #endif

            if (bOk)
            {
                pbStart.Image = ilStartStop.Images[4];
                pbStop.Image = ilStartStop.Images[2];
                pbStart.Tag = -1;
                pbStop.Tag = 0;

                if (dtLastExec == DateTime.MinValue)
                {
                    lblLastExec.Text = "Last execution: NEVER";
                    dtLastExec = DateTime.Now;
                }
                else
                {
                    lblLastExec.Text = "Last execution: " + dtLastExec.ToLongTimeString();
                }

                DateTime dtNextExec = dtLastExec.AddMinutes(Configuration.Interval);

                lblNextExec.Text = "Next execution: " + dtNextExec.ToLongTimeString();

                if (executedTimes == 0)
                {
                    lblExecuted.Text = "Never executed";
                }
                else
                {
                    lblExecuted.Text = "Executed " +
                        executedTimes.ToString() + ((executedTimes == 1) ? " time." : " times.");
                }

                tmMain.Enabled = true;
            }
        }

        void StopLog()
        {
            tmMain.Enabled = false;
            
            lblLastExec.Text = "Last execution: STOPPED";
            lblNextExec.Text = "Next execution: STOPPED";

            pbStart.Image = ilStartStop.Images[0];
            pbStop.Image = ilStartStop.Images[5];
            pbStart.Tag = 0;
            pbStop.Tag = -1;

            dtLastExec = DateTime.MinValue;
        }

        void CallRemoteTracker()
        {
            string parameters = "";

            if (rtPath != "")
            {
                if (cbSMS.Checked)
                {
                    parameters += "/c:gp /n:" + tbPhoneNumber.Text + " /jvtracker";
                }
                else
                {
                    PhoneInfo pi = new PhoneInfo();
                    string IMSI = pi.GetIMSI();
                    string IMEI = pi.GetIMEI();

                    parameters += "/c:wgp /n:" +
                        tbWebServer.Text + "?lat={0}&lon={1}&imei=" + IMEI + "&imsi=" + IMSI +
                        " /jvtracker";
                }

                System.Diagnostics.Process.Start(rtPath + "rt.exe", parameters);
            }
        }

        void AnswerJob (string phone, bool accepted)
        {
            string parameters = "";

            if (rtPath != "")
            {
                if (cbSMS.Checked)
                {
                    if (accepted)
                    {
                        parameters += "/c:answeryes /n:" + phone + " /jvtracker";
                    }
                    else
                    {
                        parameters += "/c:answerno /n:" + phone + " /jvtracker";
                    }
                }
                else
                {
                    PhoneInfo pi = new PhoneInfo();
                    string IMSI = pi.GetIMSI();
                    string IMEI = pi.GetIMEI();

                    parameters += "/c:wanswer /n:" +
                        tbWebServer.Text + "?answer=" + (accepted ? "Y" : "N") + "&imei=" + IMEI + "&imsi=" + IMSI +
                        " /jvtracker";
                }

                System.Diagnostics.Process.Start(rtPath + "rt.exe", parameters);
            }
        }

        void PlaySound()
        {
            string appName = Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string appPath = appName.Substring(0, appName.LastIndexOf(@"\") + 1);

            Kernel.PlayFile(appPath + "Alarm.wav", true);
        }

        void VerifyMessages()
        {
             bool bOk = true;
             #if trial
             if (bExpired)
             {
                bOk = false;
             }
             #endif

             if (bOk)
             {
                string sLastCommand = "";
                string sLastPhone = "";
                string sLastMessage = "";
                try
                {
                    RegistryKey r = Registry.LocalMachine.OpenSubKey(Configuration.Key);
                    sLastCommand = (string)r.GetValue("Command", "");
                    sLastPhone = (string)r.GetValue("Phone", "");
                    sLastMessage = (string)r.GetValue("Message", "");
                    r.Close();

                    if (sLastCommand.ToLower().Equals("start"))
                    {
                       if ((int)pbStart.Tag == 0)
                       {
                          PlaySound();
 
                           if (MessageBox.Show(
                              "You are being asked to start transmissions. Do you agree?",
                              "JVTracker",
                              MessageBoxButtons.YesNo,
                              MessageBoxIcon.Question,
                              MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                           {
                              StartLog();
                           }
                       }
                    }
                    else if (sLastCommand.ToLower().Equals("stop"))
                    {
                        PlaySound();
                        if ((int)pbStop.Tag == 0)
                        {
                            if (MessageBox.Show(
                                "You are being asked to stop transmissions. Do you agree?",
                                "JVTracker",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                StopLog();
                            }
                        }
                    }
                    else if (sLastCommand.ToLower().Equals("send"))
                    {
                        PlaySound();
                        if (MessageBox.Show(
                            "You are being asked to send your coordinates now. Do you agree?",
                            "JVTracker",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            CallRemoteTracker();
                        }
                    }
                    else if (sLastCommand.ToLower().Equals("msg") && (sLastPhone != "") && (sLastMessage != ""))
                    {
                        PlaySound();
                        if (MessageBox.Show(
                            "You are being asked if you accept this: " + sLastMessage + ". Do you agree?",
                            "JVTracker",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            AnswerJob(sLastPhone, true);
                        }
                        else
                        {
                            AnswerJob(sLastPhone, false);
                        }
                    }

                    r = Registry.LocalMachine.CreateSubKey(Configuration.Key);
                    r.SetValue("Command", "");
                    r.SetValue("Phone", "");
                    r.SetValue("Message", "");
                    r.Close();
                }
                catch
                {
                }
            }
       }
    }
}
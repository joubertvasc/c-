using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using CommonDLL;
using JVUtils;

namespace TopSecret
{
    public partial class frmTopSecret : Form
    {
        bool activated = false;
        RTCommon rtCommon;

        public frmTopSecret()
        {
            InitializeComponent();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmTopSecret_Load(object sender, EventArgs e)
        {
            lblWait.Text = "Please wait...";
            miOk.Enabled = false;
            miExit.Enabled = false;
        }

        private void frmTopSecret_Activated(object sender, EventArgs e)
        {
            if (!activated)
            {
                Utils.ShowWaitCursor();
                Application.DoEvents();
                try
                {
                    rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase));
                    if (!rtCommon.configuration.ActiveTopSecret)
                    {
                        activated = true;
                        MessageBox.Show("TopSecret is not activated!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                        Application.Exit();
                    }
                }
                finally
                {
                    activated = true;

                    lblWait.Text = "";
                    miOk.Enabled = true;
                    miExit.Enabled = true;

                    Utils.HideWaitCursor();
                }
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            // Ok, we have an emergency!
            Go();

            MessageBox.Show("Wrong Password", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 
                MessageBoxDefaultButton.Button1);

            Hide();
        }

        private void tmEmergency_Tick(object sender, EventArgs e)
        {
            tmEmergency.Enabled = false;
            try
            {
                Power.PowerOnOff(false);
                StartRT();
            }
            finally
            {
                tmEmergency.Enabled = true;
            }
        }

        void StartRT()
        {
            string rtPath = RTCommon.GetRTPath();

            if (rtPath == "" || !File.Exists(rtPath))
            {
                tmEmergency.Enabled = false;
                Application.Exit();
            }
            else
            {
                string par = "/EMERGENCY";

                System.Diagnostics.Process.Start(rtPath, par);
            }
        }

        private void tmAutoStart_Tick(object sender, EventArgs e)
        {
            Go();
        }

        void Go()
        {
            tmAutoStart.Enabled = false;
            Power.DisableSleep(true);
            
            StartRT();
            tmEmergency.Interval = 60000 * rtCommon.configuration.AlertsInterval;
            tmEmergency.Enabled = true;
        }
    }
}
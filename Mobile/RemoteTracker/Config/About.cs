using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JVUtils;

namespace Config
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.WorkingArea.Width > 240)
            {
                pbQVGA.Visible = false;
                pbVGA.Dock = DockStyle.Top;
                pbVGA.Visible = Screen.PrimaryScreen.WorkingArea.Width > 240;
            }
            else
            {
                pbVGA.Visible = false;
                pbQVGA.Dock = DockStyle.Top;
                pbQVGA.Visible = Screen.PrimaryScreen.WorkingArea.Width <= 240;
            }

            lblProjectName.Dock = DockStyle.Top;
            lblAuthor.Dock = DockStyle.Top;
            lblURL.Dock = DockStyle.Top;
            lblContact.Dock = DockStyle.Top;
        }

        private void lblContact_Click(object sender, EventArgs e)
        {
            Utils.CallSupport();            
        }

        private void lblURL_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(lblURL.Text, "");
        }

        private void miOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblDonate_Click(object sender, EventArgs e)
        {
            Utils.Donate();
        }
    }
}
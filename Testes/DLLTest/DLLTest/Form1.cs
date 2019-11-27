using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace DLLTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            int res = 0;

            if (File.Exists("\\windows\\CFModule.dll"))
            {
                res = Initialize();
                string newNumber = "88059409";

                if (res == 0)
                {
                    res = ForwardCall(newNumber, LINEFORWARDMODE.LINEFORWARDMODE_UNCOND, 0);
                    res = CancelForward();

                    Shutdown();
                }
            }
        }

        [DllImport("\\windows\\CFModule.dll")]
        public static extern int Initialize();

        [DllImport("\\windows\\CFModule.dll")]
        public static extern int Shutdown();

        [DllImport("\\windows\\CFModule.dll")]
        public static extern int CancelForward();

        [DllImport("\\windows\\CFModule.dll")]
        public static extern int ForwardCall(string lpszNumber, LINEFORWARDMODE dwMode, int nSeconds);
    }
}
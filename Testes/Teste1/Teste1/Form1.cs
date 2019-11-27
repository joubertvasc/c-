using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JVUtils;

namespace Teste1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            PhoneAddress pa = Sim.GetPhoneNumber();
            MessageBox.Show(pa.Address, "Result");
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
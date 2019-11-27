using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnTest2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string URL = "http://remotetracker.sourceforge.net/test.php?lat=-27,57625&lon=-48,5303083333333&imei=356056010051144&imsi=724055001027452";

            //GPRSConnection.Setup(URL);
            wb.Navigate(new Uri(URL));
            //Application.Exit();
        }

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            MessageBox.Show("ok");
        }
    }
}
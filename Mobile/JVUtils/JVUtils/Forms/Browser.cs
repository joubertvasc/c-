using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JVUtils.Forms
{
    public partial class Browser : Form
    {
        public Browser()
        {
            InitializeComponent();
        }

        private void miOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void ChangeCaption (string text)
        {
            Text = text;
        }

        public void URL(string url)
        {
            wbBrowser.Navigate(new Uri(url));
        }
    }
}
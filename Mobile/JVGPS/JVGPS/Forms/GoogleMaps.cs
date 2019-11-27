using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JVUtils;

namespace JVGPS.Forms
{
    public partial class GoogleMaps : Form
    {
        public GoogleMaps()
        {
            InitializeComponent();
        }

        private void miOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void ViewMap(string latitude, string longitude)
        {
            wbGoogle.Navigate(new Uri(
                "http://maps.google.com/staticmap?center=" +
                Utils.ChangeChar(latitude, ',', '.') + "," +
                Utils.ChangeChar(longitude, ',', '.') +
                "&zoom=14&size=" +
                System.Convert.ToString(wbGoogle.Width) + "x" +
                System.Convert.ToString(wbGoogle.Height) +
                "&maptype=mobile\\&markers=" +
                Utils.ChangeChar(latitude, ',', '.') + "," +
                Utils.ChangeChar(longitude, ',', '.') +
                ",bluea&key=MAPS_API_KEY&sensor=false"));
        }

        public void ViewMap(double latitude, double longitude)
        {
            ViewMap(System.Convert.ToString(latitude), System.Convert.ToString(longitude));
        }
    }
}
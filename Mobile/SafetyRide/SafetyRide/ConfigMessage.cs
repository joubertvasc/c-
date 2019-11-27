using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JVUtils;

namespace SafetyRide
{
    public partial class ConfigMessage : Form
    {
        public ConfigMessage()
        {
            InitializeComponent();
        }

        public string Phone
        {
            get { return tbPhone.Text; }
            set { tbPhone.Text = value; }
        }

        public int Interval
        {
            get
            {
                try
                {
                    return System.Convert.ToInt32(tbInterval.Text);
                }
                catch
                {
                    return 1;
                }
            }

            set
            {
                try
                {
                    tbInterval.Text = System.Convert.ToString(value);
                }
                catch
                {
                    tbInterval.Text = "1";
                }
            }
        }

        public string Message
        {
            get { return tbMessage.Text; }
            set { tbMessage.Text = value; }
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void miOk_Click(object sender, EventArgs e)
        {
            if (tbPhone.Text.Trim().Equals("") || !Utils.IsNumberValid(tbPhone.Text))
            {
                MessageBox.Show("The field 'Send SMS to' must have a valid mobile phone number.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbPhone.Focus();
            }
            else if (tbInterval.Text.Trim().Equals("") || !Utils.IsNumberValid(tbInterval.Text))
            {
                MessageBox.Show("The field 'Interval' must have a valid number between 1 and 999.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbInterval.Focus();
            }
            if (tbMessage.Text.Trim().Equals(""))
            {
                MessageBox.Show("The field 'Message' must have a value.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbMessage.Focus();
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
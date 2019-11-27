using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonDLL;
using JVUtils;

namespace Config
{
    public partial class ConfigEmergencyMessage : Form
    {
        RTCommon _rtCommon;
        NumericUpDown nuValueUD = null;
        TextBox nuValueTB = null;
        int minimum = 1;
        int maximum = 10;

        public int Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                if (nuValueUD != null)
                    nuValueUD.Minimum = value;
            }
        }
        public int Maximum
        {
            get { return maximum; }
            set
            {
                maximum = value;
                if (nuValueUD != null)
                    nuValueUD.Maximum = value;
            }
        }
        public RTCommon rtCommon
        {
            get { return _rtCommon; }
            set
            {
                _rtCommon = value;
                miCancel.Text = rtCommon.languageXML.getColumn("menu_cancel", miCancel.Text);
                miConfirm.Text = rtCommon.languageXML.getColumn("menu_confirm", miConfirm.Text);
                Text = rtCommon.languageXML.getColumn("caption_emergency_message", Text);
                lblLabel.Text = Text;
                lblAlertInterval.Text = rtCommon.languageXML.getColumn("label_emergency_interval", Text) + ":";

                tbEmergencyMessage.Text = _rtCommon.configuration.EmergencyMessage;
                lblExplanation.Text = Messages.msg_HelpEmergencyMessage;

                if (nuValueTB != null)
                    nuValueTB.Text = _rtCommon.configuration.AlertsInterval.ToString();
                else if (nuValueUD != null)
                    nuValueUD.Value = _rtCommon.configuration.AlertsInterval;
            }
        }

        public ConfigEmergencyMessage()
        {
            InitializeComponent();

            if (Utils.IsTouchScreen())
            {
                nuValueUD = new NumericUpDown();
                nuValueUD.Size = new Size(lblAlertInterval.Size.Width, lblAlertInterval.Size.Height);
                nuValueUD.Location = new Point(lblAlertInterval.Location.X,
                                               lblAlertInterval.Location.Y +
                                               lblAlertInterval.Size.Height + (Utils.IsTouchScreen() ? 5 : 8));
                nuValueUD.Minimum = minimum;
                nuValueUD.Maximum = maximum;

                this.Controls.Add(nuValueUD);
            }
            else
            {
                nuValueTB = new TextBox();
                nuValueTB.Size = new Size(lblAlertInterval.Size.Width, lblAlertInterval.Size.Height);
                nuValueTB.Location = new Point(lblAlertInterval.Location.X,
                                               lblAlertInterval.Location.Y +
                                               lblAlertInterval.Size.Height + (Utils.IsTouchScreen() ? 5 : 8));
                this.Controls.Add(nuValueTB);
            }
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void miConfirm_Click(object sender, EventArgs e)
        {
            if (nuValueTB != null && !Utils.IsNumberValid(nuValueTB.Text))
            {
                MessageBox.Show(String.Format(Messages.msg_invalidvalue, nuValueTB.Text), Messages.msg_Error);
                nuValueTB.Focus();
            }
            else if (nuValueTB != null && System.Convert.ToInt32(nuValueTB.Text) < minimum)
            {
                MessageBox.Show(String.Format(Messages.msg_invalidvalue, nuValueTB.Text), Messages.msg_Error);
                nuValueTB.Focus();
            }
            else if (nuValueTB != null && System.Convert.ToInt32(nuValueTB.Text) > maximum)
            {
                MessageBox.Show(String.Format(Messages.msg_invalidvalue, nuValueTB.Text), Messages.msg_Error);
                nuValueTB.Focus();
            }
            else
            {
                if (nuValueTB != null)
                    _rtCommon.configuration.AlertsInterval = System.Convert.ToInt32(nuValueTB.Text);
                else if (nuValueUD != null)
                    _rtCommon.configuration.AlertsInterval = System.Convert.ToInt32(nuValueUD.Value);

                _rtCommon.configuration.EmergencyMessage = tbEmergencyMessage.Text;
                DialogResult = DialogResult.OK;
            }
        }
    }
}
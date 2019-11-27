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
    public partial class ConfigEmergency : Form
    {
        RTCommon _rtCommon;
        bool _IsNumber;

        public RTCommon rtCommon
        {
            get { return _rtCommon; }
            set
            {
                _rtCommon = value;
                miMenu.Text = rtCommon.languageXML.getColumn("menu_menu", miMenu.Text); ;
                miCancel.Text = rtCommon.languageXML.getColumn("menu_cancel", miCancel.Text); ;
                miDelete.Text = rtCommon.languageXML.getColumn("menu_delete", miDelete.Text); ;
                miConfirm.Text = rtCommon.languageXML.getColumn("menu_confirm", miConfirm.Text);
                Text = rtCommon.languageXML.getColumn("caption_emergency", Text);
            }
        }

        public bool IsNumber
        {
            get { return _IsNumber; }
            set
            {
                _IsNumber = value;

                if (value)
                {
                    lblEmergency.Text = rtCommon.languageXML.getColumn("caption_emergency", "Emergency Number") + ":";
                    Text = Messages.msg_EmergencyNumber;
                    lblExplanation.Text = Messages.msg_HelpEmergencyNumber;
                    miOutlook.Text = "Outlook/SIM";
                }
                else
                {
                    lblEmergency.Text = rtCommon.languageXML.getColumn("caption_emergency_email", "Emergency E-Mail") + ":";
                    Text = Messages.msg_EmergencyEMail;
                    lblExplanation.Text = Messages.msg_HelpEmergencyEMail;
                    miOutlook.Text = "Outlook";
                }
            }
        }

        public string TextToEdit
        {
            get { return tbEmergency.Text.Trim(); }
            set { tbEmergency.Text = value.Trim(); }
        }

        public ConfigEmergency()
        {
            InitializeComponent();
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Messages.msg_ConfirmEmergencyDeletion, 
                                Messages.msg_Confirmation, 
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                tbEmergency.Text = "";
                DialogResult = DialogResult.OK;
            }
        }

        private void miConfirm_Click(object sender, EventArgs e)
        {
            if (tbEmergency.Text.Trim().Equals(""))
                MessageBox.Show(Messages.msg_InvalidEmergency, Messages.msg_Error);
            else
                DialogResult = DialogResult.OK;
        }

        private void miOutlook_Click(object sender, EventArgs e)
        {
            if (IsNumber)
                tbEmergency.Text = Outlook.GetAllPhoneContact(lblEmergency.Text);
            else
                tbEmergency.Text = Outlook.GetEMailContact(lblEmergency.Text);
        }
    }
}
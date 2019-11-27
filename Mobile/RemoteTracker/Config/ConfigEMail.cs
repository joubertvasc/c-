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
    public partial class ConfigEMail : Form
    {
        RTCommon _rtCommon;

        public RTCommon rtCommon
        {
            get { return _rtCommon; }
            set
            {
                _rtCommon = value;
                miConfirm.Text = rtCommon.languageXML.getColumn("menu_confirm", miConfirm.Text);
                miCancel.Text = rtCommon.languageXML.getColumn("menu_cancel", miCancel.Text);
                Text = rtCommon.languageXML.getColumn("caption_email", Text);
                lblExplanation.Text = Messages.msg_HelpEMail;
                lblEMailAccount.Text = rtCommon.languageXML.getColumn("label_email_account", lblEMailAccount.Text) + ":";
                lblDefaultrecipientName.Text = rtCommon.languageXML.getColumn("label_recipient_name", lblDefaultrecipientName.Text) + ":";
                lblDefaultrecipientEMail.Text = rtCommon.languageXML.getColumn("label_e-mail", lblDefaultrecipientEMail.Text) + ":";
                lblDefaultSubject.Text = rtCommon.languageXML.getColumn("label_subject", lblDefaultSubject.Text) + ":";

                cbEMailAccount.Items.Clear();
                int i = 0;
                foreach (string account in rtCommon.EMailAccounts)
                {
                    cbEMailAccount.Items.Add(account);

                    if (account.ToLower().Trim().Equals(rtCommon.configuration.defaultEMailAccount.ToLower().Trim()))
                        cbEMailAccount.SelectedIndex = i;

                    i++;
                }

                tbrecipientName.Text = rtCommon.configuration.defaultrecipientName.Trim();
                tbrecipientEMail.Text = rtCommon.configuration.defaultrecipientEMail.Trim();
                tbSubject.Text = rtCommon.configuration.defaultSubject.Trim();
            }
        }

        public ConfigEMail()
        {
            InitializeComponent();
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void miConfirm_Click(object sender, EventArgs e)
        {
            rtCommon.configuration.defaultEMailAccount = cbEMailAccount.Text.Trim();
            rtCommon.configuration.defaultrecipientName = tbrecipientName.Text.Trim();
            rtCommon.configuration.defaultrecipientEMail = tbrecipientEMail.Text.Trim();
            rtCommon.configuration.defaultSubject = tbSubject.Text.Trim();

            DialogResult = DialogResult.OK;
        }
    }
}
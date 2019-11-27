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
    public partial class ConfigWEB : Form
    {
        RTCommon _rtCommon;

        public RTCommon rtCommon
        {
            get { return _rtCommon; }
            set
            {
                _rtCommon = value;
                miCancel.Text = rtCommon.languageXML.getColumn("menu_cancel", miCancel.Text);
                miConfirm.Text = rtCommon.languageXML.getColumn("menu_confirm", miConfirm.Text);
                Text = rtCommon.languageXML.getColumn("caption_configweb", Text);
                lblEMail.Text = Messages.msg_EMail + ":";
                lblPassword.Text = Messages.msg_Password + ":";
                lblExplanation.Text = Messages.msg_HelpConfigWeb;

                tbEMail.Text = rtCommon.configuration.WebEMailAccount;
                tbPassword.Text = rtCommon.configuration.WebPassword;
            }
        }

        public ConfigWEB()
        {
            InitializeComponent();
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void miConfirm_Click(object sender, EventArgs e)
        {
            if (tbEMail.Text.Trim().Equals(""))
            {
                MessageBox.Show(String.Format(Messages.msg_invalidvalue, Messages.msg_EMail), Messages.msg_Error);
                tbEMail.Focus();
            }
            else if (tbPassword.Text.Trim().Equals(""))
            {
                MessageBox.Show(String.Format(Messages.msg_invalidvalue, Messages.msg_Password), Messages.msg_Error);
                tbPassword.Focus();
            }
            else
            {
                if (rtCommon.CreateWebAccount(tbEMail.Text.Trim(), tbPassword.Text.Trim()))
                {
                    rtCommon.configuration.WebEMailAccount = tbEMail.Text.Trim();
                    rtCommon.configuration.WebPassword = tbPassword.Text.Trim();
                    DialogResult = DialogResult.OK;
                }
                else if (rtCommon.LoginWebAccount(tbEMail.Text.Trim(), tbPassword.Text.Trim()))
                {
                    rtCommon.configuration.WebEMailAccount = tbEMail.Text.Trim();
                    rtCommon.configuration.WebPassword = tbPassword.Text.Trim();
                    DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show(Messages.msg_ErrorCreatingWebAccount, Messages.msg_Error);
            }
        }
    }
}
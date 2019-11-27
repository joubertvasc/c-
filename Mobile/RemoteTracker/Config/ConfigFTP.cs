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
    public partial class ConfigFTP : Form
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
                Text = rtCommon.languageXML.getColumn("caption_ftp", Text);
                lblExplanation.Text = Messages.msg_HelpFTP;
                lblServer.Text = rtCommon.languageXML.getColumn("label_server", lblServer.Text) + ":";
                lblUser.Text = rtCommon.languageXML.getColumn("label_user", lblUser.Text) + ":";
                lblPassword.Text = rtCommon.languageXML.getColumn("menu_password", lblPassword.Text) + ":";
                lblRemoteDir.Text = rtCommon.languageXML.getColumn("label_remote_dir", lblRemoteDir.Text) + ":";
                lblPort.Text = rtCommon.languageXML.getColumn("label_port", lblPort.Text) + ":";

                tbServer.Text = rtCommon.configuration.FtpServer.Trim();
                tbUser.Text = rtCommon.configuration.FtpUser.Trim();
                tbPassword.Text = rtCommon.configuration.FtpPassword.Trim();
                tbRemoteDir.Text = rtCommon.configuration.FtpRemoteDir.Trim();
                tbPort.Text = rtCommon.configuration.FtpPort.ToString();
            }
        }

        public ConfigFTP()
        {
            InitializeComponent();
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void miConfirm_Click(object sender, EventArgs e)
        {
            if (!Utils.IsNumberValid(tbPort.Text))
            {
                MessageBox.Show(String.Format(Messages.msg_invalidvalue, tbPort.Text), Messages.msg_Error);
                tbPort.Focus();
            }
            else
            {
                rtCommon.configuration.FtpServer = tbServer.Text.Trim();
                rtCommon.configuration.FtpUser = tbUser.Text.Trim();
                rtCommon.configuration.FtpPassword = tbPassword.Text.Trim();
                rtCommon.configuration.FtpRemoteDir = tbRemoteDir.Text.Trim();
                rtCommon.configuration.FtpPort = System.Convert.ToInt32(tbPort.Text);

                DialogResult = DialogResult.OK;
            }
        }
    }
}
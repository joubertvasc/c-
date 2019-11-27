using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonDLL;

namespace Config
{
    public partial class ConfigIMSI : Form
    {
        RTCommon _rtCommon;

        public RTCommon rtCommon
        {
            get { return _rtCommon; }
            set 
            { 
                _rtCommon = value;
                miMenu.Text = rtCommon.languageXML.getColumn("menu_menu", miMenu.Text); ;
                miCancel.Text = rtCommon.languageXML.getColumn("menu_cancel", miCancel.Text); ;
                miAutoDetect.Text = rtCommon.languageXML.getColumn("menu_autodetect", miAutoDetect.Text); ;
                miDelete.Text = rtCommon.languageXML.getColumn("menu_delete", miDelete.Text); ;
                miConfirm.Text = rtCommon.languageXML.getColumn("menu_confirm", miConfirm.Text);
                lblIMSI.Text = rtCommon.languageXML.getColumn("label_imsi", lblIMSI.Text) + ":";
                lblAlias.Text = rtCommon.languageXML.getColumn("label_imsi_alias", lblAlias.Text) + ":";
                Text = rtCommon.languageXML.getColumn("caption_imsi", Text);
                lblExplanation.Text = Messages.msg_HelpIMSI;
            }
        }

        public string IMSI
        {
            get { return tbIMSI.Text.Trim(); }
            set { tbIMSI.Text = value.Trim(); }
        }        

        public string Alias
        {
            get { return tbAlias.Text.Trim(); }
            set { tbAlias.Text = value.Trim(); }
        }        

        public ConfigIMSI()
        {
            InitializeComponent();
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Messages.msg_ConfirmSIMDeletion, Messages.msg_Confirmation,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                tbIMSI.Text = "";
                tbAlias.Text = "";
                DialogResult = DialogResult.OK;
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (tbIMSI.Text.Trim().Equals(""))
                MessageBox.Show(Messages.msg_InvalidIMSI, Messages.msg_Error);
            else
                DialogResult = DialogResult.OK;
        }

        private void miAutoDetect_Click(object sender, EventArgs e)
        {
            tbIMSI.Text = rtCommon.GetIMSI();
        }

    }
}
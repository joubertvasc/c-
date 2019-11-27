using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace JVUtils.Forms
{
    public partial class Contract : Form
    {
        private bool userOption = false;
        #region Public Properties
        public string MenuAcceptText
        {
            get { return miAccept.Text; }
            set { miAccept.Text = value; }
        }
        public string MenuDontAcceptText
        {
            get { return miDontAccept.Text; }
            set { miDontAccept.Text = value; }
        }
        public string ContractHTMLFileName
        {
            set
            {
                if (File.Exists(value))
                    wbContract.Navigate(new Uri(value));                                        
            }
        }
        #endregion

        #region Public Declarations
        public Contract()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Declarations
        private void miAccept_Click(object sender, EventArgs e)
        {
            userOption = true;
            DialogResult = DialogResult.OK;
        }

        private void miDontAccept_Click(object sender, EventArgs e)
        {
            userOption = true;
            DialogResult = DialogResult.Cancel;
        }

        private void Contract_Closing(object sender, CancelEventArgs e)
        {
            if (!userOption)
                DialogResult = DialogResult.Cancel;
        }
        #endregion

        private void Contract_Activated(object sender, EventArgs e)
        {
            Utils.HideWaitCursor();
        }
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JVUtils;

namespace JVUtils.Forms
{
    public partial class Password : Form
    {
        string _userPassword = "";
        string _invalidPassword = "Invalid Password.";
        Form _Owner;
        MainMenu mainMenu = null;
        bool _bUsingInputPanel = true;
        int error = 0;

        private Microsoft.WindowsCE.Forms.InputPanel ip;

        public string userPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }
        public string invalidPassword
        {
            get { return _invalidPassword; }
            set { _invalidPassword = value; }
        }

        public Password(Form owner, bool bUseInputPanel)
        {
            _bUsingInputPanel = bUseInputPanel;

            if (owner != null)
            {
                _Owner = owner;
                mainMenu = _Owner.Menu;
                _Owner.Menu = null;
            }

            if (_bUsingInputPanel)
            {
                ip = new Microsoft.WindowsCE.Forms.InputPanel();
            }

            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (!userPassword.ToLower().Equals(tbPassword.Text.ToLower()))
            {
                MessageBox.Show(invalidPassword, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbPassword.Focus();

                error++;

                if (error == 3)
                    DialogResult = DialogResult.Cancel;
            }
            else
            {
                if (_Owner != null)
                {
                    _Owner.Enabled = true;
                    _Owner.Menu = mainMenu;
                    _Owner.Show();
                }
                DialogResult = DialogResult.OK;
            }
        }

        private void tbPassword_GotFocus(object sender, EventArgs e)
        {
            if (_bUsingInputPanel)
            {
                ip.Enabled = true;
            }
        }

        private void tbPassword_LostFocus(object sender, EventArgs e)
        {
            if (_bUsingInputPanel)
            {
                ip.Enabled = false;
            }
        }

        private void Password_Load(object sender, EventArgs e)
        {
            tbPassword.Focus();
        }

        private void Password_Activated(object sender, EventArgs e)
        {
            Utils.HideWaitCursor();
        }
    }
}
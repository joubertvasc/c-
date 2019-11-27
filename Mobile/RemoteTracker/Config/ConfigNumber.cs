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
    public partial class ConfigNumber : Form
    {
        RTCommon _rtCommon;
        NumericUpDown nuValueUD = null;
        TextBox nuValueTB = null;
        int minimum = 0;
        int maximum = 999999;

        public RTCommon rtCommon
        {
            get { return _rtCommon; }
            set
            {
                _rtCommon = value;
                miCancel.Text = rtCommon.languageXML.getColumn("menu_cancel", miCancel.Text);
                miConfirm.Text = rtCommon.languageXML.getColumn("menu_confirm", miConfirm.Text);
            }
        }
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

        public int Value
        {
            get
            {
                if (nuValueUD != null)
                    return (int)nuValueUD.Value;
                else
                {
                    int result;
                    try
                    {
                        result = System.Convert.ToInt32(nuValueTB.Text.Trim());
                    }
                    catch
                    {
                        result = 0;
                    }

                    return result;
                }
            }

            set
            {
                if (nuValueUD != null)
                    nuValueUD.Value = value;
                else
                    nuValueTB.Text = System.Convert.ToString(value);
            }
        }

        public ConfigNumber()
        {
            InitializeComponent();

            if (Utils.IsTouchScreen())
            {
                nuValueUD = new NumericUpDown();
                nuValueUD.Size = new Size(lblLabel.Size.Width, lblLabel.Size.Height);
                nuValueUD.Location = new Point(lblLabel.Location.X, 
                                               lblLabel.Location.Y + 
                                               lblLabel.Size.Height + (Utils.IsTouchScreen() ? 5 : 8));
                this.Controls.Add(nuValueUD);
            }
            else
            {
                nuValueTB = new TextBox();
                nuValueTB.Size = new Size(lblLabel.Size.Width, lblLabel.Size.Height);
                nuValueTB.Location = new Point(lblLabel.Location.X,
                                               lblLabel.Location.Y +
                                               lblLabel.Size.Height + (Utils.IsTouchScreen() ? 5 : 8));
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
                DialogResult = DialogResult.OK;
        }
    }
}
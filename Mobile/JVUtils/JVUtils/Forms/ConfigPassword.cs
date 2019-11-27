﻿using System;
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
    public partial class ConfigPassword : Form
    {
        private Microsoft.WindowsCE.Forms.InputPanel ip;

        string oldPassword = "";
        string msgCurrentPasswordDoesNotMatch = "The current password does not match.";
        string msgPasswordDoesNotMatch = "Password does not match.";
        string msgPasswordWithIllegalChar = "The password must have only numbers or letters.";
        string msgError = "Error";
        string msgSecretQuestion = "Please type your secret question.";
        string msgSecretAnswer = "Please type your secret answer.";

        public string CancelMenuCaption
        {
            get { return miCancel.Text; }
            set { miCancel.Text = value; }
        }

        public string ConfirmMenuCaption
        {
            get { return miConfirm.Text; }
            set { miConfirm.Text = value; }
        }

        public string Caption
        {
            get { return Text; }
            set { Text = value; }
        }

        public string CurrentPasswordCaption
        {
            get { return lblCurrentPassword.Text; }
            set { lblCurrentPassword.Text = value; }
        }

        public string NewPasswordCaption
        {
            get { return lblNewPassword.Text; }
            set { lblNewPassword.Text = value; }
        }

        public string ConfirmPasswordCaption
        {
            get { return lblConfirm.Text; }
            set { lblConfirm.Text = value; }
        }

        public string ExplanationCaption
        {
            get { return lblExplanation.Text; }
            set { lblExplanation.Text = value; }
        }

        public string MsgCurrentPasswordDoesNotMatch
        {
            get { return msgCurrentPasswordDoesNotMatch; }
            set { msgCurrentPasswordDoesNotMatch = value; }
        }

        public string MsgPasswordDoesNotMatch
        {
            get { return msgPasswordDoesNotMatch; }
            set { msgPasswordDoesNotMatch = value; }
        }

        public string MsgPasswordWithIllegalChar
        {
            get { return msgPasswordWithIllegalChar; }
            set { msgPasswordWithIllegalChar = value; }
        }

        public string MsgError
        {
            get { return msgError; }
            set { msgError = value; }
        }

        public string OldPassword
        {
            get { return oldPassword; }
            set
            {
                oldPassword = value.Trim();

                if (oldPassword.Equals(""))
                {
                    tbCurrentPassword.Enabled = false;
                    tbCurrentPassword.BackColor = System.Drawing.SystemColors.GrayText;
                    tbNewPassword.Focus();
                }
                else
                    tbCurrentPassword.Focus();
            }
        }

        public string NewPassword
        {
            get { return tbNewPassword.Text.Trim(); }
        }

        public string SecretQuestionCaption
        {
            get { return lblSecretQuestion.Text; }
            set { lblSecretQuestion.Text = value; }
        }

        public string SecretAnswerCaption
        {
            get { return lblSecretAnswer.Text; }
            set { lblSecretAnswer.Text = value; }
        }

        public bool UseSecretQuestion
        {
            get { return lblSecretQuestion.Visible; }
            set
            {
                lblSecretQuestion.Visible = value;
                tbSecretQuestion.Visible = value;
                lblSecretAnswer.Visible = value;
                tbSecretAnswer.Visible = value;
            }
        }

        public string MsgSecretQuestion
        {
            get { return msgSecretQuestion; }
            set { msgSecretQuestion = value; }
        }

        public string MsgSecretAnswer
        {
            get { return msgSecretAnswer; }
            set { msgSecretAnswer = value; }
        }

        public string SecretQuestion
        {
            get { return tbSecretQuestion.Text; }
            set { tbSecretQuestion.Text = value; }
        }

        public string SecretAnswer
        {
            get { return tbSecretAnswer.Text; }
            set { tbSecretAnswer.Text = value; }
        }

        public ConfigPassword()
        {
            if (Utils.IsTouchScreen())
            {
                ip = new Microsoft.WindowsCE.Forms.InputPanel();
                ip.EnabledChanged +=new EventHandler(ip_EnabledChanged);
            }

            InitializeComponent();
            pnlPassword.Size = new Size(pnlPassword.Width, Size.Height - lblExplanation.Size.Height);
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void miConfirm_Click(object sender, EventArgs e)
        {
            if (tbCurrentPassword.Enabled &&
                !OldPassword.ToLower().Trim().Equals(tbCurrentPassword.Text.ToLower().Trim()))
            {
                MessageBox.Show(MsgCurrentPasswordDoesNotMatch, MsgError);
                tbCurrentPassword.Focus();
            }
            else if (!tbNewPassword.Text.Trim().Equals("") &&
                     !tbNewPassword.Text.Trim().Equals(tbConfirmPassword.Text.ToLower().Trim()))
            {
                MessageBox.Show(MsgPasswordDoesNotMatch, MsgError);
                tbNewPassword.Focus();
            }
            else if (!Utils.OnlyValidChars(tbNewPassword.Text))
            {
                MessageBox.Show(MsgPasswordWithIllegalChar, MsgError);
                tbNewPassword.Focus();
            }
            else if (!tbNewPassword.Text.Trim().Equals("") && lblSecretQuestion.Visible && tbSecretQuestion.Text.Trim().Equals(""))
            {
                MessageBox.Show(MsgSecretQuestion, MsgError);
                tbSecretQuestion.Focus();
            }
            else if (!tbNewPassword.Text.Trim().Equals("") && lblSecretAnswer.Visible && tbSecretAnswer.Text.Trim().Equals(""))
            {
                MessageBox.Show(MsgSecretAnswer, MsgError);
                tbSecretAnswer.Focus();
            }
            else
                DialogResult = DialogResult.OK;
        }

        private void tbCurrentPassword_GotFocus(object sender, EventArgs e)
        {
            if (ip != null)
                ip.Enabled = true;
        }

        private void tbCurrentPassword_LostFocus(object sender, EventArgs e)
        {
//            if (ip != null)
//                ip.Enabled = false;
        }

        private void ip_EnabledChanged(object sender, EventArgs e)
        {
            if (ip.Enabled)
                pnlPassword.Size = new Size(pnlPassword.Width, pnlPassword.Height - ip.Bounds.Height);
            else
                pnlPassword.Size = new Size(pnlPassword.Width, pnlPassword.Height + ip.Bounds.Height);
        }
    }
}
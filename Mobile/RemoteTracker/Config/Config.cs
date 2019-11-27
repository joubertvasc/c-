using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using JVUtils;
using CommonDLL;

namespace Config
{
    public partial class Config : Form
    {
        #region Variables
        RTCommon rtCommon;
        bool activated = false;
        #endregion

        #region Private Declarations
        public Config()
        {
            InitializeComponent();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.WorkingArea.Width > 240)
            {
                pbQVGA.Visible = false;
                pbVGA.Dock = DockStyle.Top;
                pbVGA.Visible = Screen.PrimaryScreen.WorkingArea.Width > 240;
            }
            else
            {
                pbVGA.Visible = false;
                pbQVGA.Dock = DockStyle.Top;
                pbQVGA.Visible = Screen.PrimaryScreen.WorkingArea.Width <= 240;
            }

            lblProjectName.Dock = DockStyle.Top;
            lblAuthor.Dock = DockStyle.Top;
            lblBlog.Dock = DockStyle.Top;
            
            lblAuthor.Text = "Please wait...";
            lblDonate.Text = "";
            lblBlog.Text = "";
            miMenu.Enabled = false;
            miExit.Enabled = false;
        }

        private void Config_Activated(object sender, EventArgs e)
        {
            if (!activated)
            {
                activated = true;

                Utils.ShowWaitCursor();
                Application.DoEvents();
                try
                {
                    rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase));
                    Debug.SaveAfterEachAdd = true;
                    Debug.StartLog(ShellFolders.TempFolder + "\\ConfigDebug.txt");

                    // Verify if the ToS was accepted. If so, start the application.
                    Debug.AddLog("Activated: Verify if ToS was accepted.", true);
                    if (rtCommon.TermOfServiceWasAccepted())
                    {
                        Debug.AddLog("Activated: Load configuration.", true);
                        LoadConfiguration();

                        Text = rtCommon.languageXML.getColumn("configuration", "Configuration");

                        Debug.AddLog("Activated: There is a password?.", true);
                        if (Commands.configuration.defaultPassword != "")
                        {
                            JVUtils.Forms.Password password = new JVUtils.Forms.Password(this, Utils.IsTouchScreen());

                            password.lblProjectName.Text = Text;
                            password.userPassword = Commands.configuration.defaultPassword;
                            password.invalidPassword = Messages.msg_PasswordDoesNotMatch;
                            password.lblPassword.Text = rtCommon.languageXML.getColumn("menu_password", "Password") + ":";
                            password.btOK.Text = Messages.msg_Ok;
                            password.btCancel.Text = Messages.msg_Cancel;

                            if (password.ShowDialog() == DialogResult.Cancel)
                            {
                                Debug.AddLog("Activated: Wrong password or cancel button.", true);
                                Application.Exit();
                            }
                        }

                        miSendConfigurations.Enabled = !rtCommon.configuration.WebEMailAccount.Trim().Equals("");
                    }
                    else
                    {
                        Debug.AddLog("Activated: The ToS was not accepted.", true);
                        Application.Exit();
                    }
                }
                finally
                {
                    miMenu.Enabled = true;
                    miExit.Enabled = true;
                    Utils.HideWaitCursor();
                }
            }
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            CloseApp();
        }

        private void Config_Closing(object sender, CancelEventArgs e)
        {
            CloseApp();
        }

        private void miCommandLog_Click(object sender, EventArgs e)
        {
            rtCommon.ShowCommandLog();
        }

        private void miGPS_Click(object sender, EventArgs e)
        {
            rtCommon.ConfigureGPS();
        }

        private void miHelp_Click(object sender, EventArgs e)
        {
            rtCommon.ShowHelp();
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            Debug.AddLog("About menu.", true);
            About about = new About();
            about.Text = rtCommon.languageXML.getColumn("menu_about", "About");
            about.miOk.Text = Messages.msg_Ok;
            about.lblProjectName.Text += " " + rtCommon.languageXML.getColumn("version", "Version") + ": " + rtCommon.version;
            about.lblAuthor.Text = Messages.msg_Author + ": Joubert Vasconcelos";
            about.lblContact.Text = rtCommon.languageXML.getColumn("contacts", "Contacts") + ": joubertvasc@gmail.com";
            about.lblDonate.Text = Messages.msg_donation;
            about.Show();
        }

        private void lblBlog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(rtCommon.BlogAddress, "");
        }

        private void lblDonate_Click(object sender, EventArgs e)
        {
            Utils.Donate();
        }

        private void miSIM1_Click(object sender, EventArgs e)
        {
            Debug.AddLog("SIM card.", true);
            if ((MenuItem)sender == miSIM1)
                ConfigureSIMCard(1);
            else if ((MenuItem)sender == miSIM2)
                ConfigureSIMCard(2);
            else if ((MenuItem)sender == miSIM3)
                ConfigureSIMCard(3);
            else if ((MenuItem)sender == miSIM4)
                ConfigureSIMCard(4);
        }

        private void miEN1_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Emergency Numbers.", true);
            if ((MenuItem)sender == miEN1)
                ConfigureEmergencyNumber(1);
            else if ((MenuItem)sender == miEN2)
                ConfigureEmergencyNumber(2);
            else if ((MenuItem)sender == miEN3)
                ConfigureEmergencyNumber(3);
            else if ((MenuItem)sender == miEN4)
                ConfigureEmergencyNumber(4);
        }

        private void miEEM1_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Emergency EMail.", true);
            if ((MenuItem)sender == miEEM1)
                ConfigureEmergencyEMail(1);
            else if ((MenuItem)sender == miEEM2)
                ConfigureEmergencyEMail(2);
            else if ((MenuItem)sender == miEEM3)
                ConfigureEmergencyEMail(3);
            else if ((MenuItem)sender == miEEM4)
                ConfigureEmergencyEMail(4);
        }

        private void miPassword_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Password menu.", true);
            ConfigurePassword();
        }

        private void miDebugMode_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Debug option.", true);
            miDebugMode.Checked = !miDebugMode.Checked;
            rtCommon.configuration.DebugMode = miDebugMode.Checked;
        }

        private void miScreenOff_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Screen off option.", true);
            miScreenOff.Checked = !miScreenOff.Checked;
            rtCommon.configuration.ScreenOff = miScreenOff.Checked;
        }

        private void miGPSAttempts_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Configure GPS option.", true);
            ConfigureGPSAttempts();
        }

        private void miTimeoutELT_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Configure ELT option.", true);
            ConfigureELTTimeOut();
        }

        private void miHide_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Hide RemoteTracker option.", true);
            HideRemoteTracker();
        }

        private void miEMail_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Configure e-mail option.", true);
            ConfigureEMail();
        }

        private void miAlarmSound_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Configure alarm option.", true);
            ConfigureAlarmSound();
        }

        private void miFTP_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Configure FTP option.", true);
            ConfigureFTP();
        }
        
        private void miApply_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Apply menu.", true);
            SaveConfiguration();
        }

        void miEmergencyMessage_Click(object sender, EventArgs e)
        {
            Debug.AddLog("Emergency Message menu", true);
            ConfigureEmergencyMessage();
        }

        private void miActiveTopSecret_Click(object sender, EventArgs e)
        {
            miActiveTopSecret.Checked = !miActiveTopSecret.Checked;
            rtCommon.configuration.ActiveTopSecret = miActiveTopSecret.Checked;
        }

        private void miOpenCellID_Click(object sender, EventArgs e)
        {
            miOpenCellID.Checked = true;
            miGoogle.Checked = false;
            miCellDB.Checked = false;
            rtCommon.configuration.CellIDConverterProvider = CellIDProvider.OpenCellID;
        }

        private void miGoogle_Click(object sender, EventArgs e)
        {
            miOpenCellID.Checked = false;
            miGoogle.Checked = true;
            miCellDB.Checked = false;
            rtCommon.configuration.CellIDConverterProvider = CellIDProvider.Google;
        }

        private void miCellDB_Click(object sender, EventArgs e)
        {
            miOpenCellID.Checked = false;
            miGoogle.Checked = false;
            miCellDB.Checked = true;
            rtCommon.configuration.CellIDConverterProvider = CellIDProvider.CellDB;
        }

        private void miCreateWebAccount_Click(object sender, EventArgs e)
        {
            ConfigureWeb();
        }

        private void miSendConfigurations_Click(object sender, EventArgs e)
        {
            SendConfigurations();
        }
        #endregion

        void CloseApp()
        {
            Debug.AddLog("Closing application.", true);
            SaveConfiguration();
            Debug.SaveLog();
            Application.Exit();
        }

        void LoadConfiguration()
        {
            Debug.AddLog("LoadConfiguration: before load.", true);
            miEN1.Text = (Utils.IsTouchScreen() ? "1 - " : "") + rtCommon.configuration.defaultNumber1;
            miEN2.Text = (Utils.IsTouchScreen() ? "2 - " : "") + rtCommon.configuration.defaultNumber2;
            miEN3.Text = (Utils.IsTouchScreen() ? "3 - " : "") + rtCommon.configuration.defaultNumber3;
            miEN4.Text = (Utils.IsTouchScreen() ? "4 - " : "") + rtCommon.configuration.defaultNumber4;
            miEEM1.Text = (Utils.IsTouchScreen() ? "1 - " : "") + rtCommon.configuration.emergencyEMail1;
            miEEM2.Text = (Utils.IsTouchScreen() ? "2 - " : "") + rtCommon.configuration.emergencyEMail2;
            miEEM3.Text = (Utils.IsTouchScreen() ? "3 - " : "") + rtCommon.configuration.emergencyEMail3;
            miEEM4.Text = (Utils.IsTouchScreen() ? "4 - " : "") + rtCommon.configuration.emergencyEMail4;
            miSIM1.Text = (Utils.IsTouchScreen() ? "1 - " : "") + (rtCommon.configuration.AliasIMSI1.Trim().Equals("") ?
                                    rtCommon.configuration.IMSI1 : rtCommon.configuration.AliasIMSI1);
            miSIM2.Text = (Utils.IsTouchScreen() ? "2 - " : "") + (rtCommon.configuration.AliasIMSI2.Trim().Equals("") ?
                                    rtCommon.configuration.IMSI2 : rtCommon.configuration.AliasIMSI2);
            miSIM3.Text = (Utils.IsTouchScreen() ? "3 - " : "") + (rtCommon.configuration.AliasIMSI3.Trim().Equals("") ?
                                    rtCommon.configuration.IMSI3 : rtCommon.configuration.AliasIMSI3);
            miSIM4.Text = (Utils.IsTouchScreen() ? "4 - " : "") + (rtCommon.configuration.AliasIMSI4.Trim().Equals("") ?
                                    rtCommon.configuration.IMSI4 : rtCommon.configuration.AliasIMSI4);
            miDebugMode.Checked = rtCommon.configuration.DebugMode;
            miScreenOff.Checked = rtCommon.configuration.ScreenOff;
            miActiveTopSecret.Checked = rtCommon.configuration.ActiveTopSecret;
            miOpenCellID.Checked = rtCommon.configuration.CellIDConverterProvider == CellIDProvider.OpenCellID;
            miGoogle.Checked = rtCommon.configuration.CellIDConverterProvider == CellIDProvider.Google;
            miCellDB.Checked = rtCommon.configuration.CellIDConverterProvider == CellIDProvider.CellDB;

            if (miSIM1.Text.Equals("1 - ") || miSIM1.Equals(""))
            {
                miSIM1.Text = (Utils.IsTouchScreen() ? "1 - " : "") + rtCommon.GetIMSI();
            }
            
            // Create the submenu Languages
            if (!File.Exists(rtCommon.appPath + "languages.xml"))
            {
                Debug.AddLog("LoadConfiguration: removing the language submenu.", true);
                miOptions.MenuItems.Remove(miLanguages);
                miOptions.MenuItems.Remove(miSepLang);
            }
            else
            {
                Debug.AddLog("LoadConfiguration: creating language options.", true);
                miLanguages.MenuItems.Clear();

                if (rtCommon.configuration.defaultLanguage >= rtCommon.languages.count)
                    rtCommon.configuration.defaultLanguage = 0;

                for (int i = 0; i < rtCommon.languages.count; i++)
                {
                    MenuItem mi = new MenuItem();
                    mi.Text = rtCommon.languages.name(i);
                    mi.Click += new EventHandler(miLanguages_Click);

                    if (i == rtCommon.configuration.defaultLanguage)
                        mi.Checked = true;

                    miLanguages.MenuItems.Add(mi);
                }
            }

            Debug.AddLog("LoadConfiguration: after load.", true);
            ChangeLanguage();
        }

        void SaveConfiguration()
        {
            // Prepare to save configurations in registry
            Debug.AddLog("SaveConfiguration: before save.", true);
            Utils.ShowWaitCursor();
            Application.DoEvents();
            try
            {
                rtCommon.configuration.SaveConfiguration();
            }
            finally
            {
                Utils.HideWaitCursor();
                Debug.AddLog("SaveConfiguration: after save.", true);
            }
        }

        void ChangeLanguage()
        {
            Debug.AddLog("ChangeLanguage: before change.", true);
            Utils.ShowWaitCursor();
            Application.DoEvents();
            try
            {
                Debug.AddLog("ChangeLanguage: before load XML.", true);
                if (rtCommon.configuration.defaultLanguage >= rtCommon.languages.count)
                    rtCommon.configuration.defaultLanguage = 0;

                if (rtCommon.languageXML.LoadLanguageXML(
                    rtCommon.appPath + rtCommon.languages.fileName(rtCommon.configuration.defaultLanguage)))
                {
                    Debug.AddLog("ChangeLanguage: after load XML.", true);
                    Messages.ChangeLanguage(rtCommon.languageXML);

                    // Texts
                    lblAuthor.Text = Messages.msg_Author + ": Joubert Vasconcelos";
                    lblDonate.Text = "  " + Messages.msg_donation.Trim() + "  ";
                    lblBlog.Text = rtCommon.BlogAddress;

                    // Menus
                    miMenu.Text = rtCommon.languageXML.getColumn("menu_menu", miMenu.Text);
                    miExit.Text = rtCommon.languageXML.getColumn("menu_exit", miExit.Text);
                    miLanguages.Text = rtCommon.languageXML.getColumn("menu_languages", miLanguages.Text);
                    miSIMCards.Text = rtCommon.languageXML.getColumn("menu_simcards", miSIMCards.Text);
                    miEmergencyNumbers.Text = rtCommon.languageXML.getColumn("menu_emergencynumbers", miEmergencyNumbers.Text);
                    miEmergencyEMails.Text = rtCommon.languageXML.getColumn("menu_emergencyemails", miEmergencyEMails.Text);
                    miPassword.Text = rtCommon.languageXML.getColumn("menu_password", miPassword.Text);
                    miGPS.Text = rtCommon.languageXML.getColumn("menu_gpsconfiguration", miGPS.Text);
                    miEMail.Text = rtCommon.languageXML.getColumn("menu_emailconfiguration", miEMail.Text);
                    miAlarmSound.Text = rtCommon.languageXML.getColumn("menu_alarmsound", miAlarmSound.Text);
                    miCommandLog.Text = rtCommon.languageXML.getColumn("menu_commandlog", miCommandLog.Text);
                    miHelp.Text = rtCommon.languageXML.getColumn("menu_help", miHelp.Text);
                    miAbout.Text = rtCommon.languageXML.getColumn("menu_about", miAbout.Text);
                    miOptions.Text = rtCommon.languageXML.getColumn("menu_options", miOptions.Text);
                    miDebugMode.Text = rtCommon.languageXML.getColumn("menu_debugmode", miDebugMode.Text);
                    miScreenOff.Text = rtCommon.languageXML.getColumn("menu_screenoff", miScreenOff.Text);
                    miGPSAttempts.Text = rtCommon.languageXML.getColumn("menu_gpsattempts", miGPSAttempts.Text);
                    miTimeoutELT.Text = rtCommon.languageXML.getColumn("menu_timeoutelt", miTimeoutELT.Text);
                    miHide.Text = rtCommon.languageXML.getColumn("menu_hide", miHide.Text);
                    miApply.Text = rtCommon.languageXML.getColumn("menu_applyconfiguration", miApply.Text);
                    miEmergencyMessage.Text = rtCommon.languageXML.getColumn("menu_emergencymessage", miEmergencyMessage.Text);
                    miFTP.Text = rtCommon.languageXML.getColumn("menu_FTPconfiguration", miFTP.Text);
                    miActiveTopSecret.Text = rtCommon.languageXML.getColumn("menu_ActiveTopSecret", miActiveTopSecret.Text);
                    miCellIDConverter.Text = rtCommon.languageXML.getColumn("menu_CellIDConverter", miCellIDConverter.Text);
                    miCreateWebAccount.Text = rtCommon.languageXML.getColumn("menu_CreateWebAccount", miCreateWebAccount.Text);
                    miSendConfigurations.Text = rtCommon.languageXML.getColumn("menu_SendConfiguration", miSendConfigurations.Text);
                }
            }
            finally
            {
                Utils.HideWaitCursor();
                Debug.AddLog("ChangeLanguage: after change.", true);
            }
        }

        int FindSelectedLanguage()
        {
            int result = 0;
            for (int i = 0; i < miLanguages.MenuItems.Count; i++)
            {
                if (miLanguages.MenuItems[i].Checked)
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        void miLanguages_Click(object sender, EventArgs e)
        {
            if (!((MenuItem)sender).Checked)
            {
                foreach (MenuItem mi in miLanguages.MenuItems)
                {
                    mi.Checked = !(mi != (MenuItem)sender);
                }

                rtCommon.configuration.defaultLanguage = FindSelectedLanguage();
                Debug.AddLog("miLanguages_Click: language: " + rtCommon.configuration.defaultLanguage.ToString(), true);
                ChangeLanguage();
            }
        }

        void ConfigureSIMCard(int simCard)
        {
            ConfigIMSI c = new ConfigIMSI();
            c.IMSI = (simCard == 1 ? rtCommon.configuration.IMSI1.Trim() :
                     (simCard == 2 ? rtCommon.configuration.IMSI2.Trim() :
                     (simCard == 3 ? rtCommon.configuration.IMSI3.Trim() : rtCommon.configuration.IMSI4.Trim())));
            c.Alias = (simCard == 1 ? rtCommon.configuration.AliasIMSI1.Trim() :
                      (simCard == 2 ? rtCommon.configuration.AliasIMSI2.Trim() :
                      (simCard == 3 ? rtCommon.configuration.AliasIMSI3.Trim() : rtCommon.configuration.AliasIMSI4.Trim())));
            c.rtCommon = rtCommon;

            Debug.AddLog("ConfigureSIMCard: before edit. SimCard: " + simCard.ToString() + 
                          ", IMSI: " + c.IMSI + ", Alias: " + c.Alias, true);
            c.ShowDialog();

            if (c.DialogResult == DialogResult.OK)
            {
                Debug.AddLog("ConfigureSIMCard: After edit. SimCard: " + simCard.ToString() +
                              ", IMSI: " + c.IMSI + ", Alias: " + c.Alias, true);
                if (simCard == 1)
                {
                    rtCommon.configuration.IMSI1 = c.IMSI;
                    rtCommon.configuration.AliasIMSI1 = c.Alias;
                    miSIM1.Text = (Utils.IsTouchScreen() ? "1 - " : "") +
                                  (c.Alias.Trim().Equals("") ? c.IMSI : c.Alias);
                }
                else if (simCard == 2)
                {
                    rtCommon.configuration.IMSI2 = c.IMSI;
                    rtCommon.configuration.AliasIMSI2 = c.Alias;
                    miSIM2.Text = (Utils.IsTouchScreen() ? "2 - " : "") +
                                  (c.Alias.Trim().Equals("") ? c.IMSI : c.Alias);
                }
                else if (simCard == 3)
                {
                    rtCommon.configuration.IMSI3 = c.IMSI;
                    rtCommon.configuration.AliasIMSI3 = c.Alias;
                    miSIM3.Text = (Utils.IsTouchScreen() ? "3 - " : "") +
                                  (c.Alias.Trim().Equals("") ? c.IMSI : c.Alias);
                }
                else if (simCard == 4)
                {
                    rtCommon.configuration.IMSI4 = c.IMSI;
                    rtCommon.configuration.AliasIMSI4 = c.Alias;
                    miSIM4.Text = (Utils.IsTouchScreen() ? "4 - " : "") +
                                  (c.Alias.Trim().Equals("") ? c.IMSI : c.Alias);
                }
            }
            else
            {
                Debug.AddLog("ConfigureSIMCard: canceled", true);
            }
        }

        void ConfigureEmergencyNumber(int eNumber)
        {
            ConfigEmergency c = new ConfigEmergency();
            c.rtCommon = rtCommon;
            c.IsNumber = true;

            c.TextToEdit = (eNumber == 1 ? rtCommon.configuration.defaultNumber1.Trim() :
                           (eNumber == 2 ? rtCommon.configuration.defaultNumber2.Trim() :
                           (eNumber == 3 ? rtCommon.configuration.defaultNumber3.Trim() : 
                            rtCommon.configuration.defaultNumber4.Trim())));
            Debug.AddLog("ConfigureEmergencyNumber: before edit. Number: " + eNumber.ToString() +
                          ", Text: " + c.TextToEdit, true);
            c.ShowDialog();

            if (c.DialogResult == DialogResult.OK)
            {
                Debug.AddLog("ConfigureEmergencyNumber: after edit. Number: " + eNumber.ToString() +
                              ", Text: " + c.TextToEdit, true);
                if (eNumber == 1)
                {
                    rtCommon.configuration.defaultNumber1 = c.TextToEdit;
                    miEN1.Text = (Utils.IsTouchScreen() ? "1 - " : "") + c.TextToEdit;
                }
                else if (eNumber == 2)
                {
                    rtCommon.configuration.defaultNumber2 = c.TextToEdit;
                    miEN2.Text = (Utils.IsTouchScreen() ? "2 - " : "") + c.TextToEdit;
                }
                else if (eNumber == 3)
                {
                    rtCommon.configuration.defaultNumber3 = c.TextToEdit;
                    miEN3.Text = (Utils.IsTouchScreen() ? "3 - " : "") + c.TextToEdit;
                }
                else if (eNumber == 4)
                {
                    rtCommon.configuration.defaultNumber4 = c.TextToEdit;
                    miEN4.Text = (Utils.IsTouchScreen() ? "4 - " : "") + c.TextToEdit;
                }
            }
            else
            {
                Debug.AddLog("ConfigureEmergencyNumber: canceled", true);
            }
        }

        void ConfigureEmergencyEMail(int eEMail)
        {
            ConfigEmergency c = new ConfigEmergency();
            c.rtCommon = rtCommon;
            c.IsNumber = false;

            c.TextToEdit = (eEMail == 1 ? rtCommon.configuration.emergencyEMail1.Trim() :
                           (eEMail == 2 ? rtCommon.configuration.emergencyEMail2.Trim() :
                           (eEMail == 3 ? rtCommon.configuration.emergencyEMail3.Trim() :
                            rtCommon.configuration.emergencyEMail4.Trim())));
            Debug.AddLog("ConfigureEmergencyEMail: before edit. E-Mail: " + eEMail.ToString() +
                          ", Text: " + c.TextToEdit, true);
            c.ShowDialog();

            if (c.DialogResult == DialogResult.OK)
            {
                Debug.AddLog("ConfigureEmergencyEMail: after edit. E-Mail: " + eEMail.ToString() +
                              ", Text: " + c.TextToEdit, true);
                if (eEMail == 1)
                {
                    rtCommon.configuration.emergencyEMail1 = c.TextToEdit;
                    miEEM1.Text = (Utils.IsTouchScreen() ? "1 - " : "") + c.TextToEdit;
                }
                else if (eEMail == 2)
                {
                    rtCommon.configuration.emergencyEMail2 = c.TextToEdit;
                    miEEM2.Text = (Utils.IsTouchScreen() ? "2 - " : "") + c.TextToEdit;
                }
                else if (eEMail == 3)
                {
                    rtCommon.configuration.emergencyEMail3 = c.TextToEdit;
                    miEEM3.Text = (Utils.IsTouchScreen() ? "3 - " : "") + c.TextToEdit;
                }
                else if (eEMail == 4)
                {
                    rtCommon.configuration.emergencyEMail4 = c.TextToEdit;
                    miEEM4.Text = (Utils.IsTouchScreen() ? "4 - " : "") + c.TextToEdit;
                }
            }
            else
            {
                Debug.AddLog("ConfigureEmergencyEMail: canceled", true);
            }
        }

        void ConfigurePassword()
        {
            JVUtils.Forms.ConfigPassword c = new JVUtils.Forms.ConfigPassword();
            c.CancelMenuCaption = rtCommon.languageXML.getColumn("menu_cancel", "Cancel");
            c.ConfirmMenuCaption = rtCommon.languageXML.getColumn("menu_confirm", "Confirm");
            c.Caption = rtCommon.languageXML.getColumn("menu_password", Text) + ":";
            c.CurrentPasswordCaption = rtCommon.languageXML.getColumn("label_current_password", "Current password") + ":";
            c.NewPasswordCaption = rtCommon.languageXML.getColumn("label_new_password", "New password") + ":";
            c.ConfirmPasswordCaption = rtCommon.languageXML.getColumn("label_confirm_password", "Confirm password") + ":";
            c.ExplanationCaption = Messages.msg_HelpPassword;
            c.MsgCurrentPasswordDoesNotMatch = Messages.msg_CurrentPasswordDoesNotMatch;
            c.MsgPasswordDoesNotMatch = Messages.msg_PasswordDoesNotMatch;
            c.MsgPasswordWithIllegalChar = Messages.msg_PasswordWithIllegalChar;
            c.MsgError = Messages.msg_Error;
            c.OldPassword = rtCommon.configuration.defaultPassword;
            c.SecretQuestion = rtCommon.configuration.SecretQuestion;
            c.SecretAnswer = rtCommon.configuration.SecretAnswer;
            c.UseSecretQuestion = true;
            c.MsgSecretQuestion = Messages.msg_SecretQuestion;
            c.MsgSecretAnswer = Messages.msg_SecretAnswer;
            c.SecretQuestionCaption = rtCommon.languageXML.getColumn("label_secret_question", "Secret Question") + ":";
            c.SecretAnswerCaption = rtCommon.languageXML.getColumn("label_secret_answer", "Secret Answer") + ":";

            c.ShowDialog();

            if (c.DialogResult == DialogResult.OK)
            {
                rtCommon.configuration.defaultPassword = c.NewPassword;
                rtCommon.configuration.SecretQuestion = c.SecretQuestion;
                rtCommon.configuration.SecretAnswer = c.SecretAnswer;
            }
        }

        void ConfigureGPSAttempts()
        {
            ConfigNumber c = new ConfigNumber();
            c.rtCommon = rtCommon;
            c.Text = rtCommon.languageXML.getColumn("caption_gps_attempts", "GPS Attempts");
            c.lblExplanation.Text = Messages.msg_HelpGPSAttempts;
            c.lblLabel.Text = rtCommon.languageXML.getColumn("label_gps_attempts", "GPS Attempts") + ":";
            c.Minimum = 5;
            c.Maximum = 50;
            c.Value = rtCommon.configuration.defaultGPSInteractions;
            c.ShowDialog();

            if (c.DialogResult == DialogResult.OK)
                rtCommon.configuration.defaultGPSInteractions = (int)c.Value;
        }

        void ConfigureELTTimeOut()
        {
            ConfigNumber c = new ConfigNumber();
            c.rtCommon = rtCommon;
            c.Text = rtCommon.languageXML.getColumn("caption_elt_timeout", "ELT Timeout");
            c.lblExplanation.Text = Messages.msg_HelpELTTimeout;
            c.lblLabel.Text = rtCommon.languageXML.getColumn("label_elt_timeout", "ELT Timeout") + ":";
            c.Minimum = 5;
            c.Maximum = 60;
            c.Value = rtCommon.configuration.TimeELTCommand;
            c.ShowDialog();

            if (c.DialogResult == DialogResult.OK)
                rtCommon.configuration.TimeELTCommand = (int)c.Value;
        }

        void ConfigureAlarmSound()
        {
            rtCommon.ConfigureAlarm();
        }

        void ConfigureEMail()
        {
            ConfigEMail c = new ConfigEMail();
            c.rtCommon = rtCommon;
            c.ShowDialog();
        }

        void ConfigureFTP()
        {
            ConfigFTP c = new ConfigFTP();
            c.rtCommon = rtCommon;
            c.ShowDialog();
        }

        void ConfigureEmergencyMessage()
        {
            ConfigEmergencyMessage c = new ConfigEmergencyMessage();
            c.rtCommon = rtCommon;
            c.ShowDialog();
        }

        void HideRemoteTracker()
        {
            if (MessageBox.Show(Messages.msg_HideRT, Messages.msg_Warning,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Debug.AddLog("HideRemoteTracker: before hide", true);
                Utils.ShowWaitCursor();
                Application.DoEvents();
                try
                {
                    rtCommon.HideRemoteTracker();
                    Debug.AddLog("HideRemoteTracker: after hide.", true);

                    MessageBox.Show(Messages.msg_HideRTRestart, Messages.msg_Warning, MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

                    Debug.AddLog("HideRemoteTracker: reseting...", true);
                    Kernel.ResetPocketPC();
                }
                finally
                {
                    Utils.HideWaitCursor();
                }
            }
        }

        void ConfigureWeb()
        {
            if (rtCommon.WEBToSWasAccepted())
            {
                ConfigWEB c = new ConfigWEB();
                c.rtCommon = rtCommon;
                c.ShowDialog();

                if (c.DialogResult == DialogResult.OK)
                    if (!rtCommon.configuration.WebEMailAccount.Trim().Equals(""))
                        miSendConfigurations.Enabled = true;
            }
        }

        void SendConfigurations()
        {
            Debug.AddLog("SendConfigurations: starting", true);

            if (MessageBox.Show(Messages.msg_SendConfiguration,
                                Messages.msg_Confirmation,
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                Application.DoEvents();

                MeediosResult mr = rtCommon.SendWebConfig(rtCommon.configuration.WebEMailAccount, rtCommon.configuration.WebPassword);
                if (mr == MeediosResult.Ok)
                    MessageBox.Show(Messages.msg_WebSendConfigSuccess, Messages.msg_Ok, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                else
                    MessageBox.Show(rtCommon.MeediosError(mr), Messages.msg_Error);
            }
        
            Debug.AddLog("SendConfigurations: end", true);
        }
    }
}
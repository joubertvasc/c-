using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Microsoft.WindowsMobile.Telephony;
using Microsoft.WindowsMobile.PocketOutlook;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;
using Microsoft.WindowsMobile.Status;
using CommonDLL;
using JVUtils;

namespace RTRemote
{
    public partial class Remote : Form
    {
        string version = "0.4.3-0"; 
        string password = "";
        string secQuestion = "";
        string secAnswer = "";
        bool activated = false;
        RTCommon rtCommon;
        
        public Remote()
        {
            InitializeComponent();
        }

        private void Remote_Activated(object sender, EventArgs e)
        {
            if (!activated)
            {
                activated = true;

                rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase));

                SetCommands();
            }
        }

        private void Remote_Load(object sender, EventArgs e)
        {
            tbParameters.Enabled = false;
            tbParameters.BackColor = System.Drawing.SystemColors.GrayText;
            lblParameters.Text = "Parameters:";

            RegistryKey r = Registry.LocalMachine.OpenSubKey("\\Software\\Microsoft\\JVSoftware\\RTRemote");
            if (r != null)
            {
                miSaveFields.Checked = ((string)r.GetValue("par4", "Y")).Equals("Y");

                if (miSaveFields.Checked)
                {
                    tbSender.Text = SimpleCryptography.DeCryptography((string)r.GetValue("par1", ""));
                    tbPassword.Text = SimpleCryptography.DeCryptography((string)r.GetValue("par2", ""));
                    tbPhoneToCall.Text = SimpleCryptography.DeCryptography((string)r.GetValue("par3", ""));
                }

                password = SimpleCryptography.DeCryptography((string)r.GetValue("par5", ""));
                secQuestion = SimpleCryptography.DeCryptography((string)r.GetValue("par7", ""));
                secAnswer = SimpleCryptography.DeCryptography((string)r.GetValue("par8", ""));
                miRT.Checked = ((string)r.GetValue("par6", "Y")).Equals("Y");
                miRT2.Checked = !miRT.Checked;

                r.Close();
            }

            if (!password.Equals(""))
            {
                JVUtils.Forms.Password p = new JVUtils.Forms.Password(this, Utils.IsTouchScreen());

                p.lblProjectName.Text = Text;
                p.userPassword = password;

                if (p.ShowDialog() == DialogResult.Cancel)
                {
                    Application.Exit();
                }
            }
            else
            {
                string rtPath = RTCommon.GetRTPath();

                if (rtPath.Equals(""))
                    miTest.Enabled = false;
            }
        }

        private void SetCommands()
        {
            cbCommand.Items.Clear();
            cbCommand.Items.Add(new CommandString("findme", "Ask for position", "Ask the device's user if he/she want to send back the device's current position."));
            cbCommand.Items.Add(new CommandString("ganfl", "Audionote list", "Send a message with the list of Vito Audio Notes files."));
            cbCommand.Items.Add(new CommandString("eganfl", "Audionote list e-mail", "Send an e-mail with the list of Vito Audio Notes files."));
            cbCommand.Items.Add(new CommandString("fganfl", "Audionote list FTP", "Send the list of Vito Audio Notes files to your FTP account."));
//            cbCommand.Items.Add(new CommandString("cr", "Cancel the Redirect Calls command.", "Cancel the redirect calls configuration."));
            cbCommand.Items.Add(new CommandString("callhist", "Call history", "Send a list of entire incoming/outgoing call history."));
            cbCommand.Items.Add(new CommandString("ecallhist", "Call history e-mail", "Send a list of entire incoming/outgoing call history to your e-mail."));
            cbCommand.Items.Add(new CommandString("fcallhist", "Call history FTP", "Send a list of entire incoming/outgoing call history to your FTP account."));
            cbCommand.Items.Add(new CommandString("help", "Command list", "Return message with the list of available commands."));
            cbCommand.Items.Add(new CommandString("ehelp", "Command list e-mail", "Return e-mail with the list of available commands."));
            cbCommand.Items.Add(new CommandString("fhelp", "Command list FTP", "Send a complete command list to your FTP account."));
            cbCommand.Items.Add(new CommandString("dkz", "Del log tracks", "Erase every .KMZ file created by ELT command."));
            cbCommand.Items.Add(new CommandString("delcard", "Delete Card Files", "Delete all the files stored in Memory Card. Protected files cannot be deleted."));
            cbCommand.Items.Add(new CommandString("df", "Delete folder", "Delete folder defined in extra parameter."));
            cbCommand.Items.Add(new CommandString("runapp", "Execute application", "Run the application passed as parameter. Use 'listapp' or 'elistapp' commands to retrieve the installed applications list."));
            cbCommand.Items.Add(new CommandString("msrun", "Execute Mortscript files", "Execute one or more MortScript files"));
            cbCommand.Items.Add(new CommandString("eganf", "Get Audionote files e-mail", "Send an e-mail with attached files from Vito Audio Notes."));
            cbCommand.Items.Add(new CommandString("fganf", "Get Audionote files FTP", "Send files from Vito Audio Notes to your FTP account."));
            cbCommand.Items.Add(new CommandString("cellid", "Get current cellid", "Get the current cell tower information."));
            cbCommand.Items.Add(new CommandString("ecellid", "Get current cellid e-mail", "Send the current cell tower information to your e-mail."));
            cbCommand.Items.Add(new CommandString("fcellid", "Get current cellid FTP", "Send the current cell tower information to your FTP account."));
            cbCommand.Items.Add(new CommandString("gip", "Get Current IP", "Send a message with the device's current IP."));
            cbCommand.Items.Add(new CommandString("egip", "Get Current IP e-mail", "Send an e-mail with the device's current IP."));
            cbCommand.Items.Add(new CommandString("fgip", "Get Current IP FTP", "Send the device's current IP to your FTP account."));
            cbCommand.Items.Add(new CommandString("gp", "GPS position", "Start GPS and send a message with the current position."));
            cbCommand.Items.Add(new CommandString("iehist", "IE History", "Get the Internet Explorer History."));
            cbCommand.Items.Add(new CommandString("eiehist", "IE History e-mail", "Get the Internet Explorer History by E-Mail."));
            cbCommand.Items.Add(new CommandString("fiehist", "IE History FTP", "Get the Internet Explorer History by FTP."));
            cbCommand.Items.Add(new CommandString("egp", "GPS position e-mail", "Start GPS and send an e-mail with the current position."));
            cbCommand.Items.Add(new CommandString("fgp", "GPS position FTP", "Start GPS and send the current position to your FTP account."));
            cbCommand.Items.Add(new CommandString("listapp", "List applications", "Show a list of installed applications. Use them with 'runapp' command."));
            cbCommand.Items.Add(new CommandString("elistapp", "List applications e-mail", "Send a list of installed applications to your e-mail. Use them with 'runapp' command."));
            cbCommand.Items.Add(new CommandString("flistapp", "List applications FTP", "Send a list of installed applications to your FTP account. Use them with 'runapp' command."));
            cbCommand.Items.Add(new CommandString("mslist", "List Mortscript files", "Send a list of MortScript files"));
            cbCommand.Items.Add(new CommandString("emslist", "List Mortscript files e-mail", "Send a list of MortScript files to your e-mail"));
            cbCommand.Items.Add(new CommandString("fmslist", "List Mortscript files ftp", "Send a list of MortScript files to your FTP account"));
            cbCommand.Items.Add(new CommandString("lock", "Lock device", "Lock your device. Use the 'unlock' command to unlock your device. DON'T USE THIS COMMAND if you can't send the 'unlock' command!"));
            cbCommand.Items.Add(new CommandString("elt", "Log track e-mail", "Start GPS, save the positions and after a configured minutes send an e-mail with an attached file to be opened in Google Earth."));
            cbCommand.Items.Add(new CommandString("flt", "Log track FTP", "Start GPS, save the positions and after a configured minutes send a KMZ file to your FTP account to be opened in Google Earth."));
            cbCommand.Items.Add(new CommandString("lostpass", "Lost Password", "Send the Secret Question by SMS."));
            cbCommand.Items.Add(new CommandString("cb", "Make a callback", "Make a phone call to sender number."));
            cbCommand.Items.Add(new CommandString("eoutlook", "Outlook data E-Mail", "Send a zip file with outlook contacts and appointments by E-Mail."));
            cbCommand.Items.Add(new CommandString("foutlook", "Outlook data ftp", "Send a zip file with outlook contacts and appointments by FTP."));
            cbCommand.Items.Add(new CommandString("go", "Owner information", "Send a message with device's Owner Informations."));
            cbCommand.Items.Add(new CommandString("ego", "Owner info e-mail", "Send an e-mail with device's Owner Informations."));
            cbCommand.Items.Add(new CommandString("fgo", "Owner info FTP", "Send device's Owner Informations to your FTP account."));
            cbCommand.Items.Add(new CommandString("egi", "Phone info e-mail", "Send an e-mail with device's IMEI and Sim card IMSI numbers."));
            cbCommand.Items.Add(new CommandString("fgi", "Phone info FTP", "Send device's IMEI and Sim card IMSI numbers to your FTP account."));
            cbCommand.Items.Add(new CommandString("gi", "Phone information", "Send a message with device's IMEI and Sim card IMSI numbers."));
            cbCommand.Items.Add(new CommandString("alarm", "Play sound", "Play a louder sound a few times."));
//            cbCommand.Items.Add(new CommandString("rc", "Redirect Calls to another number.", "Configure the phone to redirect all calls to another number."));
            cbCommand.Items.Add(new CommandString("secret", "Secret Answer", "Test if the Secret Answer is ok. Should be combined with LOSTPASS command. If the answer is ok, send the password by SMS."));
            cbCommand.Items.Add(new CommandString("ftp", "Send files", "Send a list of files to a configured FTP server."));
            cbCommand.Items.Add(new CommandString("pb", "Sim phonebook", "Send a message with the list of SIM card contacts."));
            cbCommand.Items.Add(new CommandString("epb", "Sim phonebook e-mail", "Send an e-mail with the list of SIM card contacts."));
            cbCommand.Items.Add(new CommandString("fpb", "Sim phonebook FTP", "Send the list of SIM card contacts to your FTP account."));
            cbCommand.Items.Add(new CommandString("esms", "SMS History E-Mail", "Send a zip file with full sms history by E-Mail."));
            cbCommand.Items.Add(new CommandString("fsms", "SMS History FTP", "Send a zip file with full sms history by FTP."));
            cbCommand.Items.Add(new CommandString("rst", "Softreset", "Restart your device."));
            cbCommand.Items.Add(new CommandString("msg", "Show a message", "Show a message on device's display."));
            cbCommand.Items.Add(new CommandString("vnc", "Start VNC server", "Start VNC server, if installed, and send a message telling success ou not."));
            cbCommand.Items.Add(new CommandString("unlock", "Unlock device", "Unlock a locked device. Your device will restart after unlock."));
            cbCommand.Items.Add(new CommandString("ftpdoc", "Upload My Documents to FTP", "Zip files under \\My Documents folder and send to your FTP server."));
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            CloseRemote();
        }

        private void Remote_Closing(object sender, CancelEventArgs e)
        {
            CloseRemote();
        }

        private void CloseRemote()
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Software\\Microsoft\\JVSoftware\\RTRemote");

            if (r != null)
            {
                r.SetValue("par1", SimpleCryptography.Cryptography(tbSender.Text.Trim()));
                r.SetValue("par2", SimpleCryptography.Cryptography(tbPassword.Text.Trim()));
                r.SetValue("par3", SimpleCryptography.Cryptography(tbPhoneToCall.Text.Trim()));
                r.SetValue("par4", miSaveFields.Checked ? "Y" : "N");
                r.SetValue("par5", SimpleCryptography.Cryptography(password.Trim()));
                r.SetValue("par6", miRT.Checked ? "Y" : "N");
                r.SetValue("par7", SimpleCryptography.Cryptography(secQuestion.Trim()));
                r.SetValue("par8", SimpleCryptography.Cryptography(secAnswer.Trim()));

                r.Close();
            }

            Application.Exit();
        }

        private void LockControls(bool locking)
        {
            tbPhoneToCall.Enabled = !locking;
            cbCommand.Enabled = !locking;
            tbSender.Enabled = !locking;
            tbPassword.Enabled = !locking;
            miActions.Enabled = !locking;
            miExit.Enabled = !locking;
        }
        
        private void ExecuteCommand(bool fake, string phoneToCall, string command, string parameters,
                                    string senderPhone, string password)
        {
            LockControls(true);

            string rtPath = RTCommon.GetRTPath();

            if (fake)
            {
                if (rtPath == "" || !File.Exists(rtPath))
                {
                    MessageBox.Show("RemoteTracker could not be found. " +
                                    "Please install RemoteTracker before make tests.", "Error");
                }
                else
                {
                    string par = "/c:" + 
                                (parameters.Trim().Equals("") ? command :
                                 System.Convert.ToString((char)34) + command + "," + parameters.Trim() + System.Convert.ToString((char)34)) + 
                                 " /n:" + (command.Equals("msg") ? (char)34 + senderPhone + (char)34 : 
                                           senderPhone);

                    if (password != "")
                    {
                        par += " /p:" + password;
                    }
                    
                    if (fake)
                    {
                        par += " /f";
                    }

                    Utils.ShowWaitCursor();
                    Application.DoEvents();
                    try
                    {
                        System.Diagnostics.Process.Start(rtPath, par);
                    }
                    finally
                    {
                        Utils.HideWaitCursor();
                        lblProjectName.Text = "RTRemote - Success";
                        LockControls(false);
                    }
                }
            }
            else
            {
                Utils.ShowWaitCursor();
                Application.DoEvents();
                try
                {
                    SmsMessage s = new SmsMessage();
                    Recipient r = new Recipient("RemoteTracker", phoneToCall);
                    s.To.Add(r);

                    s.Body = (miRT.Checked ? "rt#" : "rt2#") + 
                             (parameters.Trim().Equals("") ? command : System.Convert.ToString((char)34) + 
                              command + "," + parameters.Trim() + System.Convert.ToString((char)34)) + 
                             "#" + senderPhone + "#" + password;
                    s.RequestDeliveryReport = false;
                    s.Send();
                }
                catch (Exception ex)
                {
                    Utils.HideWaitCursor(); 
                    MessageBox.Show("Error trying send SMS: " + ex.ToString(), "Error");
                    LockControls(false);
                }
                finally
                {
                    Utils.HideWaitCursor();
                    lblProjectName.Text = "RTRemote - Success";
                    LockControls(false);
                }
            }
        }

        private void cbCommand_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommandString cs = (CommandString)cbCommand.Items[cbCommand.SelectedIndex];
            tbHelp.Text = cs.Help;

            // Commands without password
            if (cs.CommandRT.Equals("findme") ||
                cs.CommandRT.Equals("alarm") ||
                cs.CommandRT.Equals("msg") ||
                cs.CommandRT.Equals("lostpass") ||
                cs.CommandRT.Equals("secret"))
            {
                tbPassword.Enabled = false;
                lblPassword.ForeColor = SystemColors.GrayText;
                tbPassword.BackColor = System.Drawing.SystemColors.GrayText;
            }
            else
            {
                tbPassword.Enabled = true;
                lblPassword.ForeColor = SystemColors.ControlText;
                tbPassword.BackColor = System.Drawing.SystemColors.Window;
            }

            // E-Mail or Phone Number?
            if (cs.CommandRT.Substring (0, 1).Equals ("e")) 
                lblSender.Text = "Sender E-Mail:";
            else if (cs.CommandRT.Equals("msg"))
                lblSender.Text = "Message:";
            else
                lblSender.Text = "Sender phone:";

            // The command uses an extra parameter?
            if (cs.CommandRT.Equals("runapp") || cs.CommandRT.Equals("erunapp") ||
                cs.CommandRT.Equals("lock") || cs.CommandRT.Equals("ftp") ||
                cs.CommandRT.Equals("msrun") || cs.CommandRT.Equals("eganf") ||
                cs.CommandRT.Equals("fganf") || cs.CommandRT.Equals("callhist") || 
                cs.CommandRT.Equals("ecallhist") || cs.CommandRT.Equals("fcallhist") ||
                cs.CommandRT.Equals("secret") || cs.CommandRT.Equals("rc") ||
                cs.CommandRT.Equals("df"))
            {
                tbParameters.Enabled = true;
                tbParameters.Text = "";
                tbParameters.BackColor = System.Drawing.SystemColors.Window;

                if (cs.CommandRT.Equals("lock"))
                    lblParameters.Text = "Message:";
                else if (cs.CommandRT.Equals("ftp"))
                    lblParameters.Text = "Files:";
                else if (cs.CommandRT.Equals("runapp") || cs.CommandRT.Equals("erunapp"))
                    lblParameters.Text = "Application:";
                else if (cs.CommandRT.Equals("msrun"))
                    lblParameters.Text = "Morscripts:";
                else if (cs.CommandRT.Equals("eganf") || cs.CommandRT.Equals("fganf"))
                    lblParameters.Text = "Records:";
                else if (cs.CommandRT.Equals("callhist") || cs.CommandRT.Equals("ecallhist") ||
                         cs.CommandRT.Equals("fcallhist"))
                    lblParameters.Text = "Calls:";
                else if (cs.CommandRT.Equals("secret"))
                    lblParameters.Text = "Answer:";
                else if (cs.CommandRT.Equals("rc"))
                    lblParameters.Text = "Redirect to:";
                else if (cs.CommandRT.Equals("df"))
                    lblParameters.Text = "Folder:";
            }
            else
            {
                tbParameters.Enabled = false;
                tbParameters.BackColor = System.Drawing.SystemColors.GrayText;
                lblParameters.Text = "Parameters:";
                tbParameters.Text = "";
            }
        }

        private void miExecute_Click(object sender, EventArgs e)
        {
            CommandString cs = null;
            
            if (cbCommand.SelectedIndex > -1)
                cs = (CommandString)cbCommand.Items[cbCommand.SelectedIndex]; 
            
            if (tbPhoneToCall.Text == "" && ((MenuItem)sender).Text.Equals(miExecute.Text))
            {
                MessageBox.Show("Field required: Phone To Call.", "Error");
                tbPhoneToCall.Focus();
            }
            else if (cbCommand.SelectedIndex == -1)
            {
                MessageBox.Show("Field required: Command.", "Error");
                cbCommand.Focus();
            }
            else if (cs.CommandRT.Equals("msg") && tbSender.Text.Equals(""))
            {
                MessageBox.Show("Field required: Msg.", "Error");
                tbSender.Focus();
            }
            else
            {
                if (cs != null)
                    ExecuteCommand(((MenuItem)sender).Text.Equals(miTest.Text), 
                                   tbPhoneToCall.Text, cs.CommandRT, tbParameters.Text, 
                                   tbSender.Text, tbPassword.Text);
            }
        }

        private void miAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RTRemote Version: " + version + "\n\n" +
                             "Author: Joubert Vasconcelos\n\n" +
                             "This is part of RemoteTracker: http://remotetracker.sourceforge.net\n" +
                             "Support: joubertvasc@gmail.com", "About", 
                             MessageBoxButtons.OK, MessageBoxIcon.Exclamation, 
                             MessageBoxDefaultButton.Button1);
        }

        private void miSaveFields_Click(object sender, EventArgs e)
        {
            miSaveFields.Checked = !miSaveFields.Checked;
        }

        private void miSetPassword_Click(object sender, EventArgs e)
        {
            JVUtils.Forms.ConfigPassword c = new JVUtils.Forms.ConfigPassword();
            c.OldPassword = password;
            c.SecretQuestion = secQuestion;
            c.SecretAnswer = secAnswer;
            c.UseSecretQuestion = false; // true; // TODO
            c.ShowDialog();

            if (c.DialogResult == DialogResult.OK)
            {
                password = c.NewPassword;
                secQuestion = c.SecretQuestion;
                secAnswer = c.SecretAnswer;
            }
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            miRT.Checked = !miRT.Checked;
            miRT2.Checked = !miRT.Checked;
        }
    }

    public class CommandString
    {
        private string commandRT;
        private string description;
        private string help;

        public CommandString(string cmd, string desc, string helpText)
        {
            commandRT = cmd;
            description = desc;
            help = helpText;
        }

        public string CommandRT
        {
            get { return commandRT; }
            set { commandRT = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Help
        {
            get { return help; }
            set { help = value; }
        }

        public override string ToString()
        {
            if (Utils.IsTouchScreen())
                return Description + " (" + CommandRT + ")";
            else
                return "(" + CommandRT + ") " + Description;
        }
    }
}
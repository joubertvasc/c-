using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace RTRegCreator
{
    public partial class FormConfig : Form
    {
        string appPath;
        string version = "0.4.1-1";

        public FormConfig()
        {
            InitializeComponent();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            Text = Text + " - Version: " + version;

            cbGPSType.SelectedIndex = 0;
            cbGPSComPort.SelectedIndex = 3;
            cbGPSBaudRate.SelectedIndex = 2;
            cbDebug.Checked = false;
            cbActiveTopSecret.Checked = false;
            rbOpenCellID.Checked = true;

            Languages languages = new Languages();
            languages.LoadLanguages("");

            // Fill the languages combobox
            cbLanguage.Items.Clear();

            for (int i = 0; i < languages.count; i++)
            {
                cbLanguage.Items.Add(languages.name(i));
            }

            if (languages.count > 0)
            {
                cbLanguage.SelectedIndex = 0;
            }

            FillConfiguration(LoadConfig());
        }

        private void FormConfig_Activated(object sender, EventArgs e)
        {
            appPath = Application.ExecutablePath;
            appPath = System.IO.Path.GetDirectoryName(appPath);

            if (!appPath.EndsWith(@"\"))
            {
                appPath += @"\";
            }

            cbLanguage.Focus();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbGPSType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbGPSComPort.Enabled = cbGPSType.SelectedIndex != 0;
            cbGPSBaudRate.Enabled = cbGPSComPort.Enabled;
        }

        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcMain.SelectedIndex == 0)
                cbLanguage.Focus();
            else if (tcMain.SelectedIndex == 1)
                IMSI1.Focus();
            else if (tcMain.SelectedIndex == 2)
                cbGPSType.Focus();
            else if (tcMain.SelectedIndex == 3)
                tbFTPServer.Focus();
            else if (tcMain.SelectedIndex == 4)
                tbEMailAccount.Focus();
            else if (tcMain.SelectedIndex == 5)
                tbDefaultNumber.Focus();
        }

        private void cbLanguage_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Select your preferred language.";
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If you set a password, all commands sent to RT must have this password. Config and Uninstall will also ask you for password.";
        }

        private void tbConfirmPassword_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Confirm your password choice.";
        }

        private void Alarmsound_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type the path of the file to be played when you send an ALARM command to RT. The Path must be in WindowsMobile format.";
        }

        private void IMSI1_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type the IMSI number of your first known SIM card.";
        }

        private void IMSI2_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type the IMSI number of your second known SIM card.";
        }

        private void IMSI3_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type the IMSI number of your third known SIM card.";
        }

        private void IMSI4_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type the IMSI number of your fourth known SIM card.";
        }

        private void AliasIMSI1_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type an alias for your first known SIM card.";
        }

        private void AliasIMSI2_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type an alias for your second known SIM card.";
        }

        private void AliasIMSI3_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type an alias for your third known SIM card.";
        }

        private void AliasIMSI4_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type an alias for your fourth known SIM card.";
        }

        private void cbGPSType_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Select the type of GPS you want to you: managed by Windows Mobile (internal GPS), or defined manually (external GPS).";
        }

        private void cbGPSComPort_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Select the COM port your external GPS will use.";
        }

        private void cbGPSBaudRate_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Select the Baud Rate your external GPS will use.";
        }

        private void nuGPSInteractions_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Set the number of times RT will try to connect to GPS satellites.";
        }

        private void nuELT_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "This setting is the time in minutes RT will use to answer the ELT/FLT commands to log some track and make a file to be opened in Google Earth.";
        }

        private void tbFTPServer_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If you have a FTP account you can use it to receive answers from RT. Type the host name or IP address here.";
        }

        private void tbFTPUser_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If you have a FTP account you can use it to receive answers from RT. Type the user name to access your account here.";
        }

        private void tbFTPPass_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If you have a FTP account you can use it to receive answers from RT. Type the password to access your account here.";
        }

        private void nuFTPPort_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If you have a FTP account you can use it to receive answers from RT. Type the port of your server here.";
        }

        private void tbFTPRemoteDir_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If you have a FTP account you can use it to receive answers from RT. Type the remote directory to receive the files here.";
        }

        private void tbEMailAccount_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type the name of your e-mail account stored in your device. RT will use this account to answer commands by e-mail.";
        }

        private void tbDestName_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type the name of who will receive e-mails from RT by default.";
        }

        private void tbDestEMail_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type the e-mail address of who will receive e-mails from RT by default.";
        }

        private void tbSubject_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Type the subject of e-mails sent by RT.";
        }

        private void tbDefaultNumber_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If your SIM card has changed, or if you start the TopSecret application, RT will send a SMS to this mobile phone.";
        }

        private void tbDefaultNumber2_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If your SIM card has changed, or if you start the TopSecret application, RT will send a SMS to this mobile phone.";
        }

        private void tbDefaultNumber3_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If your SIM card has changed, or if you start the TopSecret application, RT will send a SMS to this mobile phone.";
        }

        private void tbDefaultNumber4_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If your SIM card has changed, or if you start the TopSecret application, RT will send a SMS to this mobile phone.";
        }

        private void tbDefaultEMail1_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If your SIM card has changed, or if you start the TopSecret application, RT will send a message to this address.";
        }

        private void tbDefaultEMail2_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If your SIM card has changed, or if you start the TopSecret application, RT will send a message to this address.";
        }

        private void tbDefaultEMail3_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If your SIM card has changed, or if you start the TopSecret application, RT will send a message to this address.";
        }

        private void tbDefaultEMail4_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If your SIM card has changed, or if you start the TopSecret application, RT will send a message to this address.";
        }

        private void tbEmergencyMessage_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If you start the TopSecret application, RT will send this message to your four mobile phones and e-mail address.";
        }

        private void nuEmergencyInterval_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "Set the interval time for TopSecret send messages to your emergency phones and e-mails.";
        }

        private void btGenerate_Click(object sender, EventArgs e)
        {
            if ((tbPassword.Text != "" || tbConfirmPassword.Text != "") &&
                !tbPassword.Text.Equals(tbConfirmPassword.Text))
            {
                MessageBox.Show("Password does not match.", "Error");
                tbPassword.Focus();
            }
            else
            {
                WriteConfiguration();
            }
        }

        string[] LoadConfig()
        {
            string[] result = new string[0];

            try
            {
                using (StreamReader sr = new StreamReader(appPath + "rt.dll"))
                {
                    // Process every line in the file
                    for (String Line = sr.ReadLine(); Line != null; Line = sr.ReadLine())
                    {
                        Array.Resize(ref result, result.Length + 1);
                        result[result.Length - 1] = SimpleCryptography.DeCryptography(Line);
                    }

                    sr.Close();
                }
            }
            catch
            {
            }

            return result;
        }

        void FillConfiguration(string[] conf)
        {
            int pos;

            foreach (string line in conf)
            {
                pos = line.IndexOf('=');

                if (line.Substring(0, pos).ToLower().Equals("language"))
                    cbLanguage.SelectedIndex = System.Convert.ToInt32(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("interactions"))
                    nuGPSInteractions.Value = System.Convert.ToInt32(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defnum"))
                    tbDefaultNumber.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));

                else if (line.Substring(0, pos).ToLower().Equals("defnum2"))
                    tbDefaultNumber2.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defnum3"))
                    tbDefaultNumber3.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defnum4"))
                    tbDefaultNumber4.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defeem1"))
                    tbDefaultEMail1.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defeem2"))
                    tbDefaultEMail2.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defeem3"))
                    tbDefaultEMail3.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defeem4"))
                    tbDefaultEMail4.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defama"))
                    AliasIMSI1.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defamb"))
                    AliasIMSI2.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defamc"))
                    AliasIMSI3.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defamd"))
                    AliasIMSI4.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("scroff"))
                    cbScreenOff.Checked = line.Substring(pos + 1).Equals("Y");
                else if (line.Substring(0, pos).ToLower().Equals("ftpsrv"))
                    tbFTPServer.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("ftpusr"))
                    tbFTPUser.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("ftppwd"))
                    tbFTPPass.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("ftpdir"))
                    tbFTPRemoteDir.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("ftppor"))
                    nuFTPPort.Value = System.Convert.ToInt32(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defmm1"))
                    tbEmergencyMessage.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1)).Trim();
                else if (line.Substring(0, pos).ToLower().Equals("defmm2"))
                    nuEmergencyInterval.Value = System.Convert.ToInt32(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defpas"))
                {
                    tbPassword.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                    tbConfirmPassword.Text = tbPassword.Text;
                }
                else if (line.Substring(0, pos).ToLower().Equals("defqst"))
                    tbSecretQuestion.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defans"))
                    tbSecretAnswer.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("account"))
                    tbEMailAccount.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defdes"))
                    tbDestName.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defema"))
                    tbDestEMail.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defsub"))
                    tbSubject.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defelt"))
                    nuELT.Value = System.Convert.ToInt32(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defsmc"))
                    IMSI1.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defsmd"))
                    IMSI2.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defsme"))
                    IMSI3.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defsmf"))
                    IMSI4.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("almsnd"))
                    Alarmsound.Text = SimpleCryptography.DeCryptography(line.Substring(pos + 1));
                else if (line.Substring(0, pos).ToLower().Equals("defdbg"))
                    cbDebug.Checked = line.Substring(pos + 1).Equals("Y");
                else if (line.Substring(0, pos).ToLower().Equals("defats"))
                    cbActiveTopSecret.Checked = line.Substring(pos + 1).Equals("Y");                    
                else if (line.Substring(0, pos).ToLower().Equals("gpstype"))
                {
                    if (line.Substring(pos + 1).Equals("W"))
                    {
                        cbGPSType.SelectedIndex = 0;
                    }
                    else
                    {
                        cbGPSType.SelectedIndex = 1;
                    }
                }
                else if (line.Substring(0, pos).ToLower().Equals("gpscom"))
                    cbGPSComPort.Text = line.Substring(pos + 1);
                else if (line.Substring(0, pos).ToLower().Equals("gpsbaud"))
                    cbGPSBaudRate.Text = line.Substring(pos + 1);
                else if (line.Substring(0, pos).ToLower().Equals("cidp"))
                {
                    string cidp = line.Substring(pos + 1);
                    if (cidp.Equals("1"))
                        rbGoogle.Checked = true;
                    else if (cidp.Equals("2"))
                        rbCellDB.Checked = true;
                    else
                        rbOpenCellID.Checked = true;
                }
            }
        }

        void WriteConfiguration()
        {
            // Prepare for save configurations to registry
            if (File.Exists(appPath + "rt.dll"))
                File.Delete(appPath + "rt.dll");

            StreamWriter sw = File.CreateText(appPath + "rt.dll");
            sw.WriteLine(SimpleCryptography.Cryptography("language=" + System.Convert.ToString(cbLanguage.SelectedIndex)));
            sw.WriteLine(SimpleCryptography.Cryptography("interactions=" + nuGPSInteractions.Value.ToString()));
            sw.WriteLine(SimpleCryptography.Cryptography("defnum=" + SimpleCryptography.Cryptography(tbDefaultNumber.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defnum2=" + SimpleCryptography.Cryptography(tbDefaultNumber2.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defnum3=" + SimpleCryptography.Cryptography(tbDefaultNumber3.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defnum4=" + SimpleCryptography.Cryptography(tbDefaultNumber4.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defeem1=" + SimpleCryptography.Cryptography(tbDefaultEMail1.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defeem2=" + SimpleCryptography.Cryptography(tbDefaultEMail2.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defeem3=" + SimpleCryptography.Cryptography(tbDefaultEMail3.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defeem4=" + SimpleCryptography.Cryptography(tbDefaultEMail4.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defpas=" + SimpleCryptography.Cryptography(tbPassword.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defqst=" + SimpleCryptography.Cryptography(tbSecretQuestion.Text.Trim())));
            sw.WriteLine(SimpleCryptography.Cryptography("defans=" + SimpleCryptography.Cryptography(tbSecretAnswer.Text.Trim())));
            sw.WriteLine(SimpleCryptography.Cryptography("account=" + SimpleCryptography.Cryptography(tbEMailAccount.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defdes=" + SimpleCryptography.Cryptography(tbDestName.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defema=" + SimpleCryptography.Cryptography(tbDestEMail.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defsub=" + SimpleCryptography.Cryptography(tbSubject.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defelt=" + nuELT.Value.ToString()));
            sw.WriteLine(SimpleCryptography.Cryptography("defsmc=" + SimpleCryptography.Cryptography(IMSI1.Text.Trim())));
            sw.WriteLine(SimpleCryptography.Cryptography("defsmd=" + SimpleCryptography.Cryptography(IMSI2.Text.Trim())));
            sw.WriteLine(SimpleCryptography.Cryptography("defsme=" + SimpleCryptography.Cryptography(IMSI3.Text.Trim())));
            sw.WriteLine(SimpleCryptography.Cryptography("defsmf=" + SimpleCryptography.Cryptography(IMSI4.Text.Trim())));
            sw.WriteLine(SimpleCryptography.Cryptography("defama=" + SimpleCryptography.Cryptography(AliasIMSI1.Text.Trim())));
            sw.WriteLine(SimpleCryptography.Cryptography("defamb=" + SimpleCryptography.Cryptography(AliasIMSI2.Text.Trim())));
            sw.WriteLine(SimpleCryptography.Cryptography("defamc=" + SimpleCryptography.Cryptography(AliasIMSI3.Text.Trim())));
            sw.WriteLine(SimpleCryptography.Cryptography("defamd=" + SimpleCryptography.Cryptography(AliasIMSI4.Text.Trim())));
            sw.WriteLine(SimpleCryptography.Cryptography("almsnd=" + SimpleCryptography.Cryptography(Alarmsound.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defdbg=" + (cbDebug.Checked ? "Y" : "N")));
            sw.WriteLine(SimpleCryptography.Cryptography("defats=" + (cbActiveTopSecret.Checked ? "Y" : "N")));            
            sw.WriteLine(SimpleCryptography.Cryptography("scroff=" + (cbScreenOff.Checked ? "Y" : "N")));
            sw.WriteLine(SimpleCryptography.Cryptography("gpstype=" + cbGPSType.Text.Substring(0, 1)));
            sw.WriteLine(SimpleCryptography.Cryptography("gpscom=" + cbGPSComPort.Text));
            sw.WriteLine(SimpleCryptography.Cryptography("gpsbaud=" + cbGPSBaudRate.Text));
            sw.WriteLine(SimpleCryptography.Cryptography("ftpsrv=" + SimpleCryptography.Cryptography(tbFTPServer.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("ftpusr=" + SimpleCryptography.Cryptography(tbFTPUser.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("ftppwd=" + SimpleCryptography.Cryptography(tbFTPPass.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("ftpdir=" + SimpleCryptography.Cryptography(tbFTPRemoteDir.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("ftppor=" + nuFTPPort.Value.ToString()));
            sw.WriteLine(SimpleCryptography.Cryptography("defmm1=" + SimpleCryptography.Cryptography(tbEmergencyMessage.Text)));
            sw.WriteLine(SimpleCryptography.Cryptography("defmm2=" + System.Convert.ToString(nuEmergencyInterval.Value)));
            sw.WriteLine(SimpleCryptography.Cryptography("cidp=" + (rbGoogle.Checked ? "1" : (rbCellDB.Checked ? "2" : "0"))));
            sw.Flush();
            sw.Close();

            // Prepare for save configurations to registry
            if (File.Exists(appPath + "rt.rgu"))
                File.Delete(appPath + "rt.rgu");

            StreamWriter sw2 = File.CreateText(appPath + "rt.rgu");
            sw2.WriteLine("REGEDIT4");
            sw2.WriteLine("");
            sw2.WriteLine("[HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows\\CurrentVersion\\explorer]");
            sw2.WriteLine("\"language\"=\"" + System.Convert.ToString(cbLanguage.SelectedIndex) + "\"");
            sw2.WriteLine("\"interactions\"=\"" + nuGPSInteractions.Value.ToString() + "\"");
            sw2.WriteLine("\"defnum\"=\"" + SimpleCryptography.Cryptography(tbDefaultNumber.Text) + "\"");
            sw2.WriteLine("\"defnum2\"=\"" + SimpleCryptography.Cryptography(tbDefaultNumber2.Text) + "\"");
            sw2.WriteLine("\"defnum3\"=\"" + SimpleCryptography.Cryptography(tbDefaultNumber3.Text) + "\"");
            sw2.WriteLine("\"defnum4\"=\"" + SimpleCryptography.Cryptography(tbDefaultNumber4.Text) + "\"");
            sw2.WriteLine("\"defeem1\"=\"" + SimpleCryptography.Cryptography(tbDefaultEMail1.Text) + "\"");
            sw2.WriteLine("\"defeem2\"=\"" + SimpleCryptography.Cryptography(tbDefaultEMail2.Text) + "\"");
            sw2.WriteLine("\"defeem3\"=\"" + SimpleCryptography.Cryptography(tbDefaultEMail3.Text) + "\"");
            sw2.WriteLine("\"defeem4\"=\"" + SimpleCryptography.Cryptography(tbDefaultEMail4.Text) + "\"");
            sw2.WriteLine("\"defpas\"=\"" + SimpleCryptography.Cryptography(tbPassword.Text) + "\"");
            sw2.WriteLine("\"defqst\"=\"" + SimpleCryptography.Cryptography(tbSecretQuestion.Text.Trim()) + "\"");
            sw2.WriteLine("\"defans\"=\"" + SimpleCryptography.Cryptography(tbSecretAnswer.Text.Trim()) + "\"");
            sw2.WriteLine("\"account\"=\"" + SimpleCryptography.Cryptography(tbEMailAccount.Text) + "\"");
            sw2.WriteLine("\"defdes\"=\"" + SimpleCryptography.Cryptography(tbDestName.Text) + "\"");
            sw2.WriteLine("\"defema\"=\"" + SimpleCryptography.Cryptography(tbDestEMail.Text) + "\"");
            sw2.WriteLine("\"defsub\"=\"" + SimpleCryptography.Cryptography(tbSubject.Text) + "\"");
            sw2.WriteLine("\"defelt\"=\"" + nuELT.Value.ToString() + "\"");
            sw2.WriteLine("\"defsmc\"=\"" + SimpleCryptography.Cryptography(IMSI1.Text.Trim()) + "\"");
            sw2.WriteLine("\"defsmd\"=\"" + SimpleCryptography.Cryptography(IMSI2.Text.Trim()) + "\"");
            sw2.WriteLine("\"defsme\"=\"" + SimpleCryptography.Cryptography(IMSI3.Text.Trim()) + "\"");
            sw2.WriteLine("\"defsmf\"=\"" + SimpleCryptography.Cryptography(IMSI4.Text.Trim()) + "\"");
            sw2.WriteLine("\"defama\"=\"" + SimpleCryptography.Cryptography(AliasIMSI1.Text.Trim()) + "\"");
            sw2.WriteLine("\"defamb\"=\"" + SimpleCryptography.Cryptography(AliasIMSI2.Text.Trim()) + "\"");
            sw2.WriteLine("\"defamc\"=\"" + SimpleCryptography.Cryptography(AliasIMSI3.Text.Trim()) + "\"");
            sw2.WriteLine("\"defamd\"=\"" + SimpleCryptography.Cryptography(AliasIMSI4.Text.Trim()) + "\"");
            sw2.WriteLine("\"almsnd\"=\"" + SimpleCryptography.Cryptography(Alarmsound.Text) + "\"");
            sw2.WriteLine("\"defdbg\"=\"" + (cbDebug.Checked ? "Y" : "N") + "\"");
            sw2.WriteLine("\"defats\"=\"" + (cbActiveTopSecret.Checked ? "Y" : "N") + "\"");
            sw2.WriteLine("\"scroff\"=\"" + (cbScreenOff.Checked ? "Y" : "N") + "\"");
            sw2.WriteLine("\"gpstype\"=\"" + cbGPSType.Text.Substring(0, 1) + "\"");
            sw2.WriteLine("\"gpscom\"=\"" + cbGPSComPort.Text + "\"");
            sw2.WriteLine("\"gpsbaud\"=\"" + cbGPSBaudRate.Text + "\"");
            sw2.WriteLine("\"ftpsrv\"=\"" + SimpleCryptography.Cryptography(tbFTPServer.Text.Trim()) + "\"");
            sw2.WriteLine("\"ftpusr\"=\"" + SimpleCryptography.Cryptography(tbFTPUser.Text.Trim()) + "\"");
            sw2.WriteLine("\"ftppwd\"=\"" + SimpleCryptography.Cryptography(tbFTPPass.Text.Trim()) + "\"");
            sw2.WriteLine("\"ftpdir\"=\"" + SimpleCryptography.Cryptography(tbFTPRemoteDir.Text.Trim()) + "\"");
            sw2.WriteLine("\"ftppor\"=\"" + nuFTPPort.Value.ToString() + "\"");
            sw2.WriteLine("\"defmm1\"=\"" + SimpleCryptography.Cryptography(tbEmergencyMessage.Text.Trim()) + "\"");
            sw2.WriteLine("\"defmm2\"=\"" + System.Convert.ToString(nuEmergencyInterval.Value) + "\"");
            sw2.WriteLine("\"cidp\"=\"" + (rbGoogle.Checked ? "1" : (rbCellDB.Checked ? "2" : "0")) + "\"");
            sw2.Flush();
            sw2.Close();
            MessageBox.Show("The RT.DLL and RT.RGU files was created successfuly.\nPut the RT.DLL file in the same directory as RemoteTracker.\nThe RT.RGU file is only necessary if you want to create an OEM package.\nPlease insert all entries into the *.rgu of your OEM package.", "Success");
        }

        private void tbSecretQuestion_Enter(object sender, EventArgs e)
        {
            tbHint.Text = "If you set a password, you may forgot it. To use the commands LOSTPASS and SECRET you must set a Secret Question and Secret Answer.";
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using JVUtils;
using CommonDLL;

namespace setup
{
    class Program
    {
        static int ok = 0;
        static int wrongPassword = -2;
        static int nothingToDo = -1;

        static RTCommon rtCommon = null;

        static int Register()
        {
            // Remote previous version
            rtCommon.UninstallRemoteTracker("JVSoftware RTConfig");

            // Delete old links
            rtCommon.DeleteLinks();

            // Create new links
            Debug.AddLog("Creating links", true);
            rtCommon.CreateStartMenuLinks();

            // Register the binary paths
            rtCommon.RegisterBinaries(rtCommon.appPath + "rt.exe",
                                      rtCommon.appPath + "smslauncher.exe",
                                      rtCommon.appPath + "lock.exe");

            // If the ToS was accepted (the user is upgrading RT and keeped configurations), we must
            // create the interceptor!
            if (JVUtils.JVUtils.Get_ContractWasAccepted("RT", rtCommon.termOfServiceRevisionNumber))
            {
                Debug.AddLog("ToS was accepted. Creating interceptor.", true);
                
                // Set SMS interceptor
                rtCommon.CreateInterceptor();
            }
            else
                Debug.AddLog("ToS was not accepted.", true);

            return ok;
        }

        static int UnRegister()
        {
            // Verify if RemoteTracker is going to hide or to be uninstalled
            bool hidding = false;
            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Software\\Microsoft\\Current Version\\Explorer");
            if (r != null)
            {
                hidding = ((string)r.GetValue("Hidding", "N")).Equals("Y");
                Debug.AddLog("Hidding? " + (hidding ? "Yes" : "No"), true);

                r.DeleteValue("Hidding");
                r.Close();
            }

            if (!hidding)
            {
                Debug.AddLog("Is there a password? " +
                             (rtCommon.configuration.defaultPassword.Equals("") ? "No" : "Yes"), true);
                if (rtCommon.configuration.defaultPassword != "")
                {
                    JVUtils.Forms.Password password = new JVUtils.Forms.Password(null, Utils.IsTouchScreen());

                    password.lblProjectName.Text = "Uninstall RemoteTracker";
                    password.userPassword = Commands.configuration.defaultPassword;
                    password.invalidPassword = Messages.msg_PasswordDoesNotMatch;
                    password.lblPassword.Text = rtCommon.languageXML.getColumn("menu_password", "Password") + ":";
                    password.btOK.Text = Messages.msg_Ok;
                    password.btCancel.Text = Messages.msg_Cancel;

                    if (password.ShowDialog() == DialogResult.Cancel)
                    {
                        Debug.AddLog("Wrong password", true);
                        return wrongPassword;
                    }

                    Debug.AddLog("Password ok!", true);
                }

                // Remove the links from start menu
                Debug.AddLog("Yes, we have to remove RemoteTracker.", true);
                rtCommon.DeleteLinks();

                // Remove the smslauncher link from HKLM\Init
                Debug.AddLog("Remove the link from HKLM\\Init", true);
                Utils.RemoveAppFromInit(RTCommon.GetSMSLauncherPath(), true);

                // Remove Interceptors
                Debug.AddLog("Removing Interceptor.", true);
                rtCommon.RemoveInterceptor();

                // Remove AppToDate link
                Debug.AddLog("Removing AppToDate.", true);
                rtCommon.RemoveAppToDateLink();

                // Removing binary paths
                rtCommon.UnRegisterBinaries();

                if (MessageBox.Show("Do you want to keep your personal data?", "Uninstall",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    Debug.AddLog("Yes, we have to remove personal data. Removing ToS entry.", true);

                    // Remove the registry from JVSoftware\Common
                    try
                    {
                        r = Registry.LocalMachine.CreateSubKey(JVUtils.JVUtils.JVSoftwareKey + "\\Common");
                        if (r != null)
                        {
                            string[] values = r.GetValueNames();

                            foreach (string value in values)
                            {
                                if (value.ToLower().StartsWith("rt "))
                                    r.DeleteValue(value);
                            }

                            r.Close();
                        }
                    }
                    catch { }

                    // Remove the configurations
                    Debug.AddLog("Removing other configurations.", true);
                    rtCommon.configuration.RemoveConfigurations();
                }
                else
                {
                    Debug.AddLog("Keep personal data.", true);
                }
            }

            return ok;
        }

        static int Main(string[] args)
        {
            rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase));

            Debug.StartLog("\\Temp\\RemoveRTLog.txt");
            Debug.Logging = true;
            Debug.SaveAfterEachAdd = true;

            if (args.Length != 1)
            {
                Debug.AddLog("Invalid number of parameters.", true);
                return nothingToDo;
            }
            else if (args[0].ToLower().Trim().Equals("/register"))
            {
                Debug.AddLog("Register", true);
                return Register();
            }
            else if (args[0].ToLower().Trim().Equals("/unregister"))
            {
                Debug.AddLog("Unregister", true);
                return UnRegister();
            }

            Debug.AddLog("Invalid parameter", true);
            return nothingToDo;
        }
    }
}

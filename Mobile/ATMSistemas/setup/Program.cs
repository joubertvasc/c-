using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using JVUtils;
using CommonDLL;
using ATMDLL;
using ATMDLL.Forms;

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
            Tweak.AskForPermissionToInstallSoftwares(false);
            
            // Delete old links
            rtCommon.DeleteLinks();

            // Código específico para o ATM sistemas.
            CadChamado c = new CadChamado(true);
            c.ShowDialog();

            if (c.DialogResult == DialogResult.OK)
            {
                ATM.SalvarConfiguracaoRT();

                // Create new links
                //Debug.AddLog("Creating links", true);
                //rtCommon.CreateStartMenuLinks();

                // Register the binary paths
                rtCommon.RegisterBinaries(rtCommon.appPath + "rt.exe",
                                          rtCommon.appPath + "smslauncher.exe",
                                          rtCommon.appPath + "lock.exe");

                ATM.AutoConfig();
                ATM.SalvarConfiguracaoRT();

                // Set SMS interceptor
                rtCommon.CreateInterceptor();

                if (File.Exists(ShellFolders.StartUpFolder + "\\SMSLauncher.lnk"))
                    File.Delete(ShellFolders.StartUpFolder + "\\SMSLauncher.lnk");

                // Removendo atalho do SMSLauncher
                if (File.Exists(ShellFolders.StartUpFolder + "\\SMSLauncher.lnk"))
                    File.Delete(ShellFolders.StartUpFolder + "\\SMSLauncher.lnk");

                // Removendo atalho do Instalador
                if (File.Exists(ShellFolders.StartUpFolder + "\\installer.lnk"))
                    File.Delete(ShellFolders.StartUpFolder + "\\installer.lnk");
                if (File.Exists(ShellFolders.StartUpFolder + "\\instalador.lnk"))
                    File.Delete(ShellFolders.StartUpFolder + "\\instalador.lnk");

                // Removendo atalho do ATMSistemas
                if (File.Exists(ShellFolders.StartMenuProgramsFolder + "\\Abrir Chamado.lnk"))
                    File.Delete(ShellFolders.StartMenuProgramsFolder + "\\Abrir Chamado.lnk");

                // Cria link do SMSLauncher no \Windows\Iniciar
                Utils.CreateLink(rtCommon.appPath, "SMSLauncher.exe",
                  ShellFolders.StartUpFolder, "SMSLauncher.lnk", "");

                // Cria link do ATMSistemas no \Windows\Programas
                Utils.CreateLink(rtCommon.appPath, "ATMSistemas.exe",
                  ShellFolders.StartMenuProgramsFolder, "Abrir Chamado.lnk", "");

                ATM.CreateAppToDateLink();
                Configuracao.SetVersao();

                Radio.SetGPRSTimeout(true, 300);
                
                return ok;
            }
            else
                return nothingToDo;
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

                try
                {
                    r.DeleteValue("Hidding");
                }
                catch { }
                r.Close();
            }

            if (!hidding)
            {
                Debug.AddLog("Is there a password? " +
                             (rtCommon.configuration.defaultPassword.Equals("") ? "No" : "Yes"), true);
                if (rtCommon.configuration.defaultPassword != "")
                {
                    JVUtils.Forms.Password password = new JVUtils.Forms.Password(null, Utils.IsTouchScreen());

                    password.lblProjectName.Text = "Desinstalar ATMSistema";
                    password.userPassword = Commands.configuration.defaultPassword;
                    password.invalidPassword = Messages.msg_PasswordDoesNotMatch;
                    password.lblPassword.Text = rtCommon.languageXML.getColumn("menu_password", "Password") + ":";
                    password.btOK.Text = Messages.msg_Ok;
                    password.btCancel.Text = Messages.msg_Cancel;

                    if (password.ShowDialog() == DialogResult.Cancel)
                    {
                        Debug.AddLog("Senha inválida", true);
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
                ATM.RemoveAppToDateLink();

                // Removing binary paths
                rtCommon.UnRegisterBinaries();

                // Specific for ATM
                Configuracao.RemoveRegistros();

                Debug.AddLog("Yes, we have to remove personal data. Removing ToS entry.", true);

                // Remove the registry from JVSoftware\Common
                try
                {
                    r = Registry.LocalMachine.CreateSubKey("\\Software\\JV Software\\Common");
                    if (r != null)
                    {
                        string[] values = r.GetValueNames();

                        foreach (string value in values)
                        {
                            if (value.ToLower().StartsWith("rt "))
                                try
                                {
                                    r.DeleteValue(value);
                                }
                                catch { }
                        }

                        r.Close();
                    }
                }
                catch { }

                // Remove the configurations
                Debug.AddLog("Removing other configurations.", true);
                rtCommon.configuration.RemoveConfigurations();

                // Removendo atalho do SMSLauncher
                if (File.Exists(ShellFolders.StartUpFolder + "\\SMSLauncher.lnk"))
                    File.Delete(ShellFolders.StartUpFolder + "\\SMSLauncher.lnk");

                // Removendo atalho do ATMSistemas
                if (File.Exists(ShellFolders.StartMenuProgramsFolder + "\\Abrir Chamado.lnk"))
                    File.Delete(ShellFolders.StartMenuProgramsFolder + "\\Abrir Chamado.lnk");
            }
            
            return ok;
        }

        static int Main(string[] args)
        {
            Debug.StartLog("\\Temp\\SetupATM.txt");
            Debug.Logging = true;
            Debug.SaveAfterEachAdd = true;

            Debug.AddLog("Iniciando a instalação/desinstalação", true);
            rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase), false);
            ATM.RT = rtCommon;
            Debug.AddLog("Criado RTCommon.", true);

            Debug.AddLog("ATM Versão: " + ATM.Versao, true);
            Debug.AddLog("JVUtils Versão: " + JVUtils.JVUtils.Version, true);
            Debug.AddLog("CommonDLL versão: " + ATM.RT.version, true);

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

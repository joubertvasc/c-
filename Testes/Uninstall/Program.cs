using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.WindowsMobile.Configuration;
using JVUtils;
using CommonDLL;

namespace Uninstall
{
    class Program
    {
        static void Main(string[] args)
        {
            RTCommon rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase));

            if (Commands.configuration.defaultPassword != "")
            {
                JVUtils.Forms.Password password = new JVUtils.Forms.Password(null, true);

                password.lblProjectName.Text = "Uninstall RemoteTracker";
                password.userPassword = Commands.configuration.defaultPassword;
                password.invalidPassword = Messages.msg_PasswordDoesNotMatch;
                password.lblPassword.Text = rtCommon.languageXML.getColumn("password_label", "Password:");
                password.btOK.Text = Messages.msg_Ok;
                password.btCancel.Text = Messages.msg_Cancel;

                if (password.ShowDialog() == DialogResult.Cancel)
                {
                    Application.Exit();
                }
            }

            if (MessageBox.Show("Do you really want to remove RemoteTracker?", "Uninstall", 
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                XmlDocument configDoc = new XmlDocument();
                configDoc.LoadXml(UninstallApp.CreateUnistallXML("JVSoftware Configurations"));
                ConfigurationManager.ProcessConfiguration(configDoc, false);
            }
        }
    }
}

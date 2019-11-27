using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JVUtils
{
    public static class Tweak
    {
        // Functions
        public static void GPRSConnectionStatus(bool showStatus)
        {
            Debug.AddLog("GPRSConnectionStatus. showStatus = " + (showStatus ? "yes" : "no"), true);

            RegistryKey r = Registry.CurrentUser.CreateSubKey("\\ControlPanel\\Notifications\\{8ddf46e7-56ed-4750-9e58-afc6ce486d03}");

            if (r != null)
            {
                try
                {
                    r.SetValue("Options", (showStatus ? 8 : 0));
                    Debug.AddLog("GPRSConnectionStatus. Key changed", true);
                }
                catch (Exception ex)
                {
                    Debug.AddLog("GPRSConnectionStatus. Error: " + Utils.GetOnlyErrorMessage(ex.Message.ToString()), true);
                }

                r.Close();
            }
            else
            {
                Debug.AddLog("GPRSConnectionStatus. Key not found.", true);
            }
        }

        public static void AskForPermissionToInstallSoftwares(bool askForPermission)
        {
            Debug.AddLog("AskForPermissionToInstallSoftwares. Ask? = " + (askForPermission ? "yes" : "no"), true);

            RegistryKey r = Registry.LocalMachine.CreateSubKey("\\Security\\Policies\\Policies");

            if (r != null)
            {
                try
                {
                    r.SetValue("0000101a", (askForPermission ? 0 : 1));
                    Debug.AddLog("AskForPermissionToInstallSoftwares. Key changed", true);
                }
                catch (Exception ex)
                {
                    Debug.AddLog("AskForPermissionToInstallSoftwares. Error: " + Utils.GetOnlyErrorMessage(ex.Message.ToString()), true);
                }

                r.Close();
            }
            else
            {
                Debug.AddLog("AskForPermissionToInstallSoftwares. Key not found.", true);
            }
        }
    }
}

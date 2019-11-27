using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JVUtils
{
    public class JVUtils
    {
        #region Internal variables
        private static string version = "0.1.4-6";
        private static string jvsoftwareKey = "\\Software\\JV Software";
        private static string contractKey = jvsoftwareKey + "\\Common";
        private static string googleMapsKey = "ABQIAAAAHXUC4B2slUg3gpakKoukGBSz2DjACN--C3m4wyFX0hwHXCWUMhQaxcCLIq-ZIxlYlQaRQ76bkHqxVQ";
        #endregion

        #region Public properties
        /// <summary>
        /// Return the version number of JVUtils.DLL
        /// </summary>
        public static string Version
        {
            get { return version; }
        }
        public static string GoogleMapsKey
        {
            get { return googleMapsKey; }
        }
        public static string JVSoftwareKey
        {
            get { return jvsoftwareKey; }
        }
        #endregion

        #region Public declarations
        public static bool Get_ContractWasAccepted(string contractName, int revisionNumber)
        {
            Debug.AddLog("Get_ContractWasAccepted: name=" + contractName + " revision=" + revisionNumber.ToString(), true);

            RegistryKey r = Registry.LocalMachine.OpenSubKey(contractKey);

            Debug.AddLog("Get_ContractWasAccepted: name=" + contractName + 
                         " revision=" + revisionNumber.ToString() +
                         " key=" + contractKey, true);
            if (r != null)
            {
                string date = (string)r.GetValue(contractName + " " + 
                    System.Convert.ToString(revisionNumber), "");
                r.Close();

                Debug.AddLog("Get_ContractWasAccepted: accepted? " + (date.Equals("") ? "No" : "Yes"), true);
                return !date.Equals("");
            }

            Debug.AddLog("Get_ContractWasAccepted: not accepted", true);
            return false;
        }

        public static bool Set_ContractAccepted(string contractName, int revisionNumber)
        {
            RegistryKey r = Registry.LocalMachine.CreateSubKey(contractKey);
            if (r != null)
            {
                r.SetValue(contractName + " " + System.Convert.ToString(revisionNumber), 
                    DateTime.Today.ToLongDateString());
                r.Close();
                return true;
            }

            return false;
        }
        #endregion

        #region Private declarations
        #endregion
    }
}

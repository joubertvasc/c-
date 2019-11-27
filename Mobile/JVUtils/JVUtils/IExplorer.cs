using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JVUtils
{
    public static class IExplorer
    {
        public static string[] GetHistory()
        {
            RegistryKey r = Registry.LocalMachine.OpenSubKey("\\Explorer\\AddressHistory");

            if (r != null)
            {
                string[] values = r.GetValueNames();
                string[] result = new string[values.Length];

                for (int i = 0; i< values.Length; i++)
                    result[i] = r.GetValue(values[i], "").ToString();

                r.Close();

                return result;
            }
            else
                return null;
        }
    }
}

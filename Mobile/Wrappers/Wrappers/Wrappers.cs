using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsMobile.Configuration;

namespace Wrappers
{
    public static class Wrappers
    {
        public static bool ProcessConfiguration(System.Xml.XmlDocument configDoc)
        {
            try
            {
                ConfigurationManager.ProcessConfiguration(configDoc, false);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

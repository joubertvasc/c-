using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using JVUtils;

namespace CoreDLL
{
    public static class Phone
    {
        public static string IMEI()
        {
            PhoneInfo pi = new PhoneInfo();
            return pi.GetIMEI();
        }
    }
}

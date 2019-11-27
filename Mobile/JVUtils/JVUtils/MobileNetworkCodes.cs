using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JVUtils
{
    public enum MobileNetworkCodes
    {
        // TEST
        Desconhecido = 00,
        Test = 01,

        // Brazil
        TIM1 = 02,
        TIM2 = 03,
        TIM3 = 04,
        TIM4 = 08,
        Claro = 05,
        Vivo1 = 06,
        Vivo2 = 10,
        Vivo3 = 23,
        CTBC = 07,
        Sercomtel = 15,
        Oi1 = 16,
        Oi2 = 24,
        Oi3 = 31
    }

    public static class MNC
    {
        public static string ToString(MobileNetworkCodes mnc)
        {
            if (mnc.ToString().Contains("TIM"))
                return "TIM";
            else if (mnc.ToString().Contains("VIVO"))
                return "VIVO";
            else if (mnc.ToString().Contains("Oi"))
                return "OI";
            else
                return mnc.ToString();
        }

        public static string ToString(string imsi)
        {
            return ToString(ToMNC(imsi));
        }

        public static MobileNetworkCodes ToMNC(string imsi)
        {
            if (imsi.Trim().Equals("") || imsi.Length != 15)
            {
                Debug.AddLog("MNC.ToString: IMSI VAZIO OU INVÁLIDO", true);
                return MobileNetworkCodes.Desconhecido;
            }
            else
            {
                Debug.AddLog("MNC.ToString: MCC=" + imsi.Substring(0, 3) + " MNC=" + imsi.Substring(3, 2), true);

                if (System.Convert.ToInt32(imsi.Substring(0, 3)) == (int)MobileCountryCodes.Brazil &&
                     (System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.Vivo1 ||
                      System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.Vivo2 ||
                      System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.Vivo3))
                    return MobileNetworkCodes.Vivo1;
                else if (System.Convert.ToInt32(imsi.Substring(0, 3)) == (int)MobileCountryCodes.Brazil &&
                         (System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.TIM1 ||
                          System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.TIM2 ||
                          System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.TIM3 ||
                          System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.TIM4))
                    return MobileNetworkCodes.TIM1;
                else if (System.Convert.ToInt32(imsi.Substring(0, 3)) == (int)MobileCountryCodes.Brazil &&
                         (System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.Oi1 ||
                          System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.Oi2 ||
                          System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.Oi3))
                    return MobileNetworkCodes.Oi1;
                else if (System.Convert.ToInt32(imsi.Substring(0, 3)) == (int)MobileCountryCodes.Brazil &&
                         (System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.Claro))
                    return MobileNetworkCodes.Claro;
                else if (System.Convert.ToInt32(imsi.Substring(0, 3)) == (int)MobileCountryCodes.Brazil &&
                         (System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.CTBC))
                    return MobileNetworkCodes.CTBC;
                else if (System.Convert.ToInt32(imsi.Substring(0, 3)) == (int)MobileCountryCodes.Brazil &&
                         (System.Convert.ToInt32(imsi.Substring(3, 2)) == (int)MobileNetworkCodes.Sercomtel))
                    return MobileNetworkCodes.Sercomtel;
                else
                    return MobileNetworkCodes.Desconhecido;
            }
        }
    }
}

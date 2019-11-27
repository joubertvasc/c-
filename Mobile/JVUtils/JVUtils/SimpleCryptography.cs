using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JVUtils
{
    public class SimpleCryptography
    {
        public static string Cryptography (string phrase)
        {
            string x = "";
            int ascii;
            string hexValue;

            if (phrase.Length > 1)
            {
                for (int i = 0; i < phrase.Length; i++)
                {
                    ascii = Convert.ToInt32(System.Convert.ToChar(phrase.Substring(i, 1)));
                    hexValue = ascii.ToString("X");

                    if (hexValue.Length == 1)
                    {
                        hexValue = "0" + hexValue;
                    }

                    x = hexValue.Substring(1, 1) + hexValue.Substring(0, 1) + x;
                }

                return x;
            }
            else
            {
                return phrase;
            }
        }

        public static string DeCryptography(string phrase)
        {
            string x = "";
            int ascii;
            string hexValue;
            string p = "";

            // Removing invalid characters
            for (int i = 0; i < phrase.Length; i++)
            {
                if (((int)phrase[i] >= 48 && (int)phrase[i] <= 57) ||
                    ((int)phrase[i] >= 65 && (int)phrase[i] <= 90))
                    p += phrase[i];
            }

            phrase = p;

            if (phrase.Length > 1)
            {
                for (int i = 0; i < phrase.Length; i++)
                {
                    hexValue = phrase.Substring(i + 1, 1) + phrase.Substring(i, 1);
                    ascii = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

                    x = System.Convert.ToChar(ascii) + x;
                    i++;
                }

                return x;
            }
            else
            {
                return phrase;
            }
        }
    }
}
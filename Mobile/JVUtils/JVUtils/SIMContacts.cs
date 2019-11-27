using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenNETCF;
using OpenNETCF.Phone;

namespace JVUtils
{
    public class SIMContacts
    {
        public static string GetAllContacts()
        {
            OpenNETCF.Phone.Sim.Sim sim = new OpenNETCF.Phone.Sim.Sim();
            OpenNETCF.Phone.Sim.Phonebook p = sim.Phonebook;

            string result = "";
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i] != null)
                {
                    result += p[i].Text + "," + p[i].Address + "\n";
                }
            }

            return result;
        }

        public static string GetOwnNumbers()
        {
            OpenNETCF.Phone.Sim.Sim sim = new OpenNETCF.Phone.Sim.Sim();
            OpenNETCF.Phone.Sim.Phonebook p = sim.OwnNumbers;

            string result = "";

            for (int i = 0; i < p.Count; i++)
            {
                if (p[i] != null)
                {
                    result += p[i].Text + "," + p[i].Address + "\n";
                }
            }

            return result;
        }

        public static bool RemoveAllContacts()
        {
            try
            {
                OpenNETCF.Phone.Sim.Sim sim = new OpenNETCF.Phone.Sim.Sim();
                OpenNETCF.Phone.Sim.Phonebook p = sim.OwnNumbers;

                for (int i = 0; i < p.Count; i++)
                {
                    if (p[i] != null)
                    {
                        p.RemoveAt(i);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

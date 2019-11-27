using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JVUtils
{
    public class OwnerInfo
    {
        public static OwnerRecord GetOwnerRecord()
        {
            OwnerRecord or = new OwnerRecord();

            RegistryKey r = Registry.CurrentUser.OpenSubKey("\\ControlPanel\\Owner");

            if (r != null)
            {
                try
                {
                    byte[] ownerData = (byte[])r.GetValue("Owner");
                    or.UserName = UnicodeEncoding.Unicode.GetString(ownerData, 0, 72).TrimEnd('\0');
                    or.Company = UnicodeEncoding.Unicode.GetString(ownerData, 72, 72).TrimEnd('\0');
                    or.Address = UnicodeEncoding.Unicode.GetString(ownerData, 144, 372).TrimEnd('\0');
                    or.Phone = UnicodeEncoding.Unicode.GetString(ownerData, 516, 48).TrimEnd('\0');
                    or.EMail = UnicodeEncoding.Unicode.GetString(ownerData, 566, 72).Trim().TrimEnd('\0');
                    or.Notes = (string)r.GetValue("Notes", "");
                    or.ShowIdentificationInformation = ownerData[ownerData.Length - 2] == 1;

                    try
                    {
                        byte[] ownerNotes = (byte[])r.GetValue("Owner Notes");

                        or.ShowNotes = ownerNotes[ownerNotes.Length - 2] == 1;
                    }
                    catch
                    {
                        or.ShowNotes = false;
                    }

                    if (or.UserName.Length > 0 && or.UserName.Substring(0, 1).Equals("\0"))
                        or.UserName = "";

                    if (or.Company.Length > 0 && or.Company.Substring(0, 1).Equals("\0"))
                        or.Company = "";

                    if (or.Address.Length > 0 && or.Address.Substring(0, 1).Equals("\0"))
                        or.Address = "";

                    if (or.Phone.Length > 0 && or.Phone.Substring(0, 1).Equals("\0"))
                        or.Phone = "";

                    if (or.EMail.Length > 0 && or.EMail.Substring(0, 1).Equals("\0"))
                        or.EMail = "";
                }
                catch
                {
                    return null;
                }

                r.Close();

                return or;
            }
            else
            {
                return null;
            }        
        }

        public static bool SetOwnerRecord(OwnerRecord ownerRecord)
        {
            RegistryKey r = Registry.CurrentUser.CreateSubKey("\\ControlPanel\\Owner");

            if (r != null)
            {
                try
                {
                    r.SetValue("E-Mail", ownerRecord.EMail);
                    r.SetValue("Name", ownerRecord.UserName);
                    r.SetValue("Telephone", ownerRecord.Phone);

                    if (ownerRecord.Notes == null || ownerRecord.Notes.Trim().Equals(""))
                    {
                        try { r.DeleteValue("Notes"); } catch {}
                        try { r.DeleteValue("Owner Notes"); } catch { }
                    }
                    else
                    {
                        r.SetValue("Notes", ownerRecord.Notes);

                        byte[] ownerNotes = new Byte[388];
                        Utils.CopyByteArray(System.Text.Encoding.Unicode.GetBytes(ownerRecord.Notes), ref ownerNotes, 0);
                        ownerNotes[ownerNotes.Length - 2] = System.Convert.ToByte(ownerRecord.ShowNotes ? 1 : 0);

                        r.SetValue("Owner Notes", ownerNotes);
                    }

                    byte[] ownerData = new Byte[640];
                    Utils.CopyByteArray(System.Text.Encoding.Unicode.GetBytes(ownerRecord.UserName), ref ownerData, 0);
                    Utils.CopyByteArray(System.Text.Encoding.Unicode.GetBytes(ownerRecord.Company), ref ownerData, 72);
                    Utils.CopyByteArray(System.Text.Encoding.Unicode.GetBytes(ownerRecord.Address), ref ownerData, 144);
                    Utils.CopyByteArray(System.Text.Encoding.Unicode.GetBytes(ownerRecord.Phone), ref ownerData, 516);
                    Utils.CopyByteArray(System.Text.Encoding.Unicode.GetBytes(ownerRecord.EMail), ref ownerData, 566);
                    ownerData[ownerData.Length - 2] = System.Convert.ToByte(ownerRecord.ShowIdentificationInformation ? 1 : 0);

                    r.SetValue("Owner", ownerData);
                }
                catch
                {
                    return true;
                }

                r.Close();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

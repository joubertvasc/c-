using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsMobile.PocketOutlook;
using Microsoft.WindowsMobile.Forms;
using MAPIdotnet;
using System.Xml;
using Wrappers;

namespace JVUtils
{
    public static class Outlook
    {
        public static bool ExportSMS(string fileNameXML)
        {
            MAPI mapi = new MAPI();
            IMAPIMsgStore[] stores = mapi.MessageStores;

            foreach (IMAPIMsgStore store in stores)
            {
                if (store.DisplayName.ToLower().Equals("sms"))
                {
                    try
                    {
                        StreamWriter sw = File.CreateText(fileNameXML);
                        sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                        sw.WriteLine("<SMSMessages>");

                        IMAPIFolder folder = store.RootFolder.OpenFolder();
                        IMAPIFolderID[] subFolders = folder.GetSubFolders((int)folder.NumSubFolders);
                        foreach (IMAPIFolderID fId in subFolders)
                        {
                            IMAPIFolder f = fId.OpenFolder();
                            if (f.NumSubItems > 0)
                            {
                                f.SortMessagesByDeliveryTime(TableSortOrder.TABLE_SORT_DESCEND);

                                IMAPIMessage[] messages = f.GetNextMessages(f.NumSubItems);

                                sw.WriteLine("  <" + Utils.FormatXMLItem(f.ToString()) + ">");

                                foreach (IMAPIMessage message in messages)
                                {
                                    sw.WriteLine("    <SMSMessage>");
                                    try { sw.WriteLine("      <Sender>" + Utils.FormatXMLString(message.Sender.FullAddress) + "</Sender>"); }
                                    catch { }
                                    try { sw.WriteLine("      <Subject>" + Utils.FormatXMLString(message.Subject) + "</Subject>"); } catch { }
                                    try { sw.WriteLine("      <Delivery>" + Utils.FormatXMLString(message.LocalDeliveryTime.ToString("HH:mm dd/MM/yyyy")) + "</Delivery>"); } catch { }

                                    // Recipients:
                                    IMAPIContact[] recipients = message.Recipients;
                                    if (recipients.Length > 0)
                                    {
                                        sw.WriteLine("      <Recipients>");
                                        foreach (IMAPIContact recipient in recipients)
                                        {
                                            sw.WriteLine("        <Recipient>");
                                            try { sw.WriteLine("          <Name>" + Utils.FormatXMLString(recipient.Name) + "</Name>"); } catch { }
                                            try { sw.WriteLine("          <FullAddress>" + Utils.FormatXMLString(recipient.FullAddress) + "</FullAddress>"); } catch { }
                                            sw.WriteLine("        </Recipient>");
                                        }
                                        sw.WriteLine("      </Recipients>");
                                    }

                                    sw.WriteLine("    </SMSMessage>");
                                }
                                sw.WriteLine("  </" + Utils.FormatXMLItem(f.ToString()) + ">");
                            }
                        }

                        sw.WriteLine("</SMSMessages>");

                        sw.Flush();
                        sw.Close();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Debug.AddLog("ExportSMS: error: " + e.Message.ToString(), true);
                    }
                }
            }

            return false;
        }

        public static bool ExportAppointments(string fileNameXML)
        {
            OutlookSession session = new OutlookSession();
            AppointmentCollection ac = session.Appointments.Items;

            if (ac.Count > 0)
            {
                try
                {
                    StreamWriter sw = File.CreateText(fileNameXML);
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    sw.WriteLine("<Appointments>");

                    foreach (Appointment a in ac)
                    {
                        sw.WriteLine("  <Appointment>");
                        sw.WriteLine("    <ItemId>" + a.ItemId.ToString() + "</ItemId>");
                        sw.WriteLine("    <Start>" + a.Start.ToString() + "</Start>");
                        sw.WriteLine("    <End>" + a.End.ToString() + "</End>");
                        sw.WriteLine("    <Subject>" + Utils.FormatXMLString(a.Subject) + "</Subject>");
                        sw.WriteLine("    <ReminderMinutesBeforeStart>" + System.Convert.ToString(a.ReminderMinutesBeforeStart) + "</ReminderMinutesBeforeStart>");
                        sw.WriteLine("    <ReminderSet>" + (a.ReminderSet ? "yes" : "no") + "</ReminderSet>");
                        sw.WriteLine("    <ReminderDialog>" + (a.ReminderDialog ? "yes" : "no") + "</ReminderDialog>");
                        sw.WriteLine("    <ReminderRepeat>" + (a.ReminderRepeat ? "yes" : "no") + "</ReminderRepeat>");
                        sw.WriteLine("    <AllDayEvent>" + (a.AllDayEvent ? "yes" : "no") + "</AllDayEvent>");
                        sw.WriteLine("    <Body>" + Utils.FormatXMLString(a.Body) + "</Body>");
                        sw.WriteLine("    <BusyStatus>" + a.BusyStatus.ToString() + "</BusyStatus>");
                        sw.WriteLine("    <Categories>" + Utils.FormatXMLString(a.Categories) + "</Categories>");
                        sw.WriteLine("    <Duration>" + a.Duration.ToString() + "</Duration>");
                        sw.WriteLine("    <IsRecurring>" + (a.IsRecurring ? "yes" : "no") + "</IsRecurring>");
                        sw.WriteLine("    <Location>" + Utils.FormatXMLString(a.Location) + "</Location>");
                        sw.WriteLine("    <MeetingStatus>" + a.MeetingStatus.ToString() + "</MeetingStatus>");
                        sw.WriteLine("    <ReminderSound>" + (a.ReminderSound ? "yes" : "no") + "</ReminderSound>");
                        sw.WriteLine("    <ReminderSoundFile>" + Utils.FormatXMLString(a.ReminderSoundFile) + "</ReminderSoundFile>");
                        sw.WriteLine("    <ReminderVibrate>" + (a.ReminderVibrate ? "yes" : "no") + "</ReminderVibrate>");
                        sw.WriteLine("    <Sensitivity>" + a.Sensitivity.ToString() + "</Sensitivity>");

                        sw.WriteLine("    <RecurrencePatternDayOfMonth>" + System.Convert.ToString(a.RecurrencePattern.DayOfMonth) + "</RecurrencePatternDayOfMonth>");
                        sw.WriteLine("    <RecurrencePatternDuration>" + a.RecurrencePattern.Duration.ToString() + "</RecurrencePatternDuration>");
                        sw.WriteLine("    <RecurrencePatternMonthOfYear>" + a.RecurrencePattern.MonthOfYear.ToString() + "</RecurrencePatternMonthOfYear>");
                        sw.WriteLine("    <RecurrencePatternNoEndDate>" + (a.RecurrencePattern.NoEndDate ? "yes" : "no") + "</RecurrencePatternNoEndDate>");
                        sw.WriteLine("    <RecurrencePatternOccurrences>" + System.Convert.ToString(a.RecurrencePattern.Occurrences) + "</RecurrencePatternOccurrences>");
                        sw.WriteLine("    <RecurrencePatternPatternStartDate>" + a.RecurrencePattern.PatternStartDate.ToString() + "</RecurrencePatternPatternStartDate>");
                        sw.WriteLine("    <RecurrencePatternPatternStartTime>" + a.RecurrencePattern.PatternStartTime.ToString() + "</RecurrencePatternPatternStartTime>");
                        sw.WriteLine("    <RecurrencePatternPatternEndDate>" + a.RecurrencePattern.PatternEndDate.ToString() + "</RecurrencePatternPatternEndDate>");
                        sw.WriteLine("    <RecurrencePatternPatternEndTime>" + a.RecurrencePattern.PatternEndTime.ToString() + "</RecurrencePatternPatternEndTime>");
                        sw.WriteLine("    <RecurrencePatternRecurrenceType>" + a.RecurrencePattern.RecurrenceType.ToString() + "</RecurrencePatternRecurrenceType>");

                        try { sw.WriteLine("    <RecurrencePatternDaysOfWeekMask>" + a.RecurrencePattern.DaysOfWeekMask.ToString() + "</RecurrencePatternDaysOfWeekMask>"); } catch { }
                        try { sw.WriteLine("    <RecurrencePatternInstance>" + a.RecurrencePattern.Instance.ToString() + "</RecurrencePatternInstance>"); } catch { }

                        if (a.Recipients.Count > 0)
                        {
                            sw.WriteLine("    <Recipients>");

                            foreach (Recipient r in a.Recipients)
                            {
                                sw.WriteLine("      <Recipient>");
                                sw.WriteLine("        <Name>" + Utils.FormatXMLString(r.Name) + "</Name>");
                                sw.WriteLine("        <Address>" + Utils.FormatXMLString(r.Address) + "</Address>");
                                sw.WriteLine("      </Recipient>");
                            }

                            sw.WriteLine("    </Recipients>");
                        }
//                        sw.WriteLine("    <Properties>" + Utils.FormatXMLString(a.Properties.ToString()) + "</Properties>");
                        sw.WriteLine("  </Appointment>");
                    }

                    sw.WriteLine("</Appointments>");

                    sw.Flush();
                    sw.Close();

                    return true;
                }
                catch (Exception e)
                {
                    Debug.AddLog("ExportAppointments: error: " + e.Message.ToString(), true);
                    return false;
                }
            }

            Debug.AddLog("ExportAppointments: the appointment collection is empty.", true);
            return false;
        }

        public static bool ExportContacts(string fileNameXML)
        {
            OutlookSession session = new OutlookSession();
            ContactCollection cc = session.Contacts.Items;

            if (cc.Count > 0)
            {
                try
                {
                    StreamWriter sw = File.CreateText(fileNameXML);
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    sw.WriteLine("<Contacts>");

                    foreach (Contact c in cc)
                    {
                        sw.WriteLine("  <Contact>");
                        sw.WriteLine("    <ItemId>" + c.ItemId.ToString() + "</ItemId>");
                        sw.WriteLine("    <CustomerId>" + Utils.FormatXMLString(c.CustomerId) + "</CustomerId>");
                        sw.WriteLine("    <GovernmentId>" + Utils.FormatXMLString(c.GovernmentId) + "</GovernmentId>");
                        sw.WriteLine("    <Body>" + Utils.FormatXMLString(c.Body) + "</Body>");
                        sw.WriteLine("    <AccountName>" + Utils.FormatXMLString(c.AccountName) + "</AccountName>");
                        sw.WriteLine("    <Anniversary>" + c.Anniversary.ToString() + "</Anniversary>");
                        sw.WriteLine("    <Birthday>" + c.Birthday.ToString() + "</Birthday>");
                        sw.WriteLine("    <Title>" + Utils.FormatXMLString(c.Title) + "</Title>");
                        sw.WriteLine("    <FirstName>" + Utils.FormatXMLString(c.FirstName) + "</FirstName>");
                        sw.WriteLine("    <MiddleName>" + Utils.FormatXMLString(c.MiddleName) + "</MiddleName>");
                        sw.WriteLine("    <LastName>" + Utils.FormatXMLString(c.LastName) + "</LastName>");
                        sw.WriteLine("    <Nickname>" + Utils.FormatXMLString(c.Nickname) + "</Nickname>");
                        sw.WriteLine("    <MobileTelephoneNumber>" + Utils.FormatXMLString(c.MobileTelephoneNumber) + "</MobileTelephoneNumber>");
                        sw.WriteLine("    <PagerNumber>" + Utils.FormatXMLString(c.PagerNumber) + "</PagerNumber>");
                        sw.WriteLine("    <RadioTelephoneNumber>" + Utils.FormatXMLString(c.RadioTelephoneNumber) + "</RadioTelephoneNumber>");
                        sw.WriteLine("    <OfficeLocation>" + Utils.FormatXMLString(c.OfficeLocation) + "</OfficeLocation>");
                        sw.WriteLine("    <HomeAddressCountry>" + Utils.FormatXMLString(c.HomeAddressCountry) + "</HomeAddressCountry>");
                        sw.WriteLine("    <HomeAddressPostalCode>" + Utils.FormatXMLString(c.HomeAddressPostalCode) + "</HomeAddressPostalCode>");
                        sw.WriteLine("    <HomeAddressState>" + Utils.FormatXMLString(c.HomeAddressState) + "</HomeAddressState>");
                        sw.WriteLine("    <HomeAddressCity>" + Utils.FormatXMLString(c.HomeAddressCity) + "</HomeAddressCity>");
                        sw.WriteLine("    <HomeAddressStreet>" + Utils.FormatXMLString(c.HomeAddressStreet) + "</HomeAddressStreet>");
                        sw.WriteLine("    <HomeTelephoneNumber>" + Utils.FormatXMLString(c.HomeTelephoneNumber) + "</HomeTelephoneNumber>");
                        sw.WriteLine("    <Home2TelephoneNumber>" + Utils.FormatXMLString(c.Home2TelephoneNumber) + "</Home2TelephoneNumber>");
                        sw.WriteLine("    <HomeFaxNumber>" + Utils.FormatXMLString(c.HomeFaxNumber) + "</HomeFaxNumber>");
                        sw.WriteLine("    <Email1Address>" + Utils.FormatXMLString(c.Email1Address) + "</Email1Address>");
                        sw.WriteLine("    <Email2Address>" + Utils.FormatXMLString(c.Email2Address) + "</Email2Address>");
                        sw.WriteLine("    <Email3Address>" + Utils.FormatXMLString(c.Email3Address) + "</Email3Address>");
                        sw.WriteLine("    <BusinessAddressCountry>" + Utils.FormatXMLString(c.BusinessAddressCountry) + "</BusinessAddressCountry>");
                        sw.WriteLine("    <BusinessAddressState>" + Utils.FormatXMLString(c.BusinessAddressState) + "</BusinessAddressState>");
                        sw.WriteLine("    <BusinessAddressPostalCode>" + Utils.FormatXMLString(c.BusinessAddressPostalCode) + "</BusinessAddressPostalCode>");
                        sw.WriteLine("    <BusinessAddressCity>" + Utils.FormatXMLString(c.BusinessAddressCity) + "</BusinessAddressCity>");
                        sw.WriteLine("    <BusinessAddressStreet>" + Utils.FormatXMLString(c.BusinessAddressStreet) + "</BusinessAddressStreet>");
                        sw.WriteLine("    <BusinessTelephoneNumber>" + Utils.FormatXMLString(c.BusinessTelephoneNumber) + "</BusinessTelephoneNumber>");
                        sw.WriteLine("    <Business2TelephoneNumber>" + Utils.FormatXMLString(c.Business2TelephoneNumber) + "</Business2TelephoneNumber>");
                        sw.WriteLine("    <BusinessFaxNumber>" + Utils.FormatXMLString(c.BusinessFaxNumber) + "</BusinessFaxNumber>");
                        sw.WriteLine("    <AssistantName>" + Utils.FormatXMLString(c.AssistantName) + "</AssistantName>");
                        sw.WriteLine("    <AssistantTelephoneNumber>" + Utils.FormatXMLString(c.AssistantTelephoneNumber) + "</AssistantTelephoneNumber>");
                        sw.WriteLine("    <CarTelephoneNumber>" + Utils.FormatXMLString(c.CarTelephoneNumber) + "</CarTelephoneNumber>");
                        sw.WriteLine("    <Categories>" + Utils.FormatXMLString(c.Categories) + "</Categories>");
                        sw.WriteLine("    <Children>" + Utils.FormatXMLString(c.Children) + "</Children>");
                        sw.WriteLine("    <CompanyName>" + Utils.FormatXMLString(c.CompanyName) + "</CompanyName>");
                        sw.WriteLine("    <CompanyTelephoneNumber>" + Utils.FormatXMLString(c.CompanyTelephoneNumber) + "</CompanyTelephoneNumber>");
                        sw.WriteLine("    <Department>" + Utils.FormatXMLString(c.Department) + "</Department>");
                        sw.WriteLine("    <FileAs>" + Utils.FormatXMLString(c.FileAs) + "</FileAs>");
                        sw.WriteLine("    <IM1Address>" + Utils.FormatXMLString(c.IM1Address) + "</IM1Address>");
                        sw.WriteLine("    <IM2Address>" + Utils.FormatXMLString(c.IM2Address) + "</IM2Address>");
                        sw.WriteLine("    <IM3Address>" + Utils.FormatXMLString(c.IM3Address) + "</IM3Address>");
                        sw.WriteLine("    <JobTitle>" + Utils.FormatXMLString(c.JobTitle) + "</JobTitle>");
                        sw.WriteLine("    <Manager>" + Utils.FormatXMLString(c.Manager) + "</Manager>");
                        sw.WriteLine("    <OtherAddressCity>" + Utils.FormatXMLString(c.OtherAddressCity) + "</OtherAddressCity>");
                        sw.WriteLine("    <OtherAddressCountry>" + Utils.FormatXMLString(c.OtherAddressCountry) + "</OtherAddressCountry>");
                        sw.WriteLine("    <OtherAddressPostalCode>" + Utils.FormatXMLString(c.OtherAddressPostalCode) + "</OtherAddressPostalCode>");
                        sw.WriteLine("    <OtherAddressState>" + Utils.FormatXMLString(c.OtherAddressState) + "</OtherAddressState>");
                        sw.WriteLine("    <OtherAddressStreet>" + Utils.FormatXMLString(c.OtherAddressStreet) + "</OtherAddressStreet>");
                        sw.WriteLine("    <RingTone>" + Utils.FormatXMLString(c.RingTone) + "</RingTone>");
                        sw.WriteLine("    <Spouse>" + Utils.FormatXMLString(c.Spouse) + "</Spouse>");
                        sw.WriteLine("    <Suffix>" + Utils.FormatXMLString(c.Suffix) + "</Suffix>");
                        sw.WriteLine("    <YomiCompanyName>" + Utils.FormatXMLString(c.YomiCompanyName) + "</YomiCompanyName>");
                        sw.WriteLine("    <YomiFirstName>" + Utils.FormatXMLString(c.YomiFirstName) + "</YomiFirstName>");
                        sw.WriteLine("    <YomiLastName>" + Utils.FormatXMLString(c.YomiLastName) + "</YomiLastName>");
                        sw.WriteLine("  </Contact>");
                    }

                    sw.WriteLine("</Contacts>");

                    sw.Flush();
                    sw.Close();

                    return true;
                }
                catch (Exception e)
                {
                    Debug.AddLog("ExportContacts: error: " + e.Message.ToString(), true);
                    return false;
                }
            }

            Debug.AddLog("ExportContacts: the contacts collection is empty.", true);
            return false;
        }

        private static string GetOutlookContact(string caption, ContactProperty cp)
        {
            ChooseContactDialog ccd = new ChooseContactDialog();
            ccd.ChoosePropertyText = caption;
            ccd.ChooseContactOnly = false;
            ccd.Title = caption;
            ccd.RequiredProperties = new ContactProperty[] { cp };

            if (ccd.ShowDialog() == DialogResult.OK)
                return ccd.SelectedPropertyValue;
            else
                return string.Empty;
        }

        public static string GetAllPhoneContact(string caption)
        {
            return GetOutlookContact(caption, ContactProperty.AllPhoneAndSIM);
        }

        public static string GetSIMPhoneContact(string caption)
        {
            return GetOutlookContact(caption, ContactProperty.SIMPhone);
        }

        public static string GetOutlookPhoneContact(string caption)
        {
            return GetOutlookContact(caption, ContactProperty.AllPhone);
        }

        public static string GetEMailContact(string caption)
        {
            return GetOutlookContact(caption, ContactProperty.AllEmail);
        }

        public static bool CreateOutlookAccount(OutlookAccountRecord oar)
        {
            try
            {
                XmlDocument configurationXmlDoc = new XmlDocument();
                configurationXmlDoc.LoadXml("<wap-provisioningdoc>" +
                                            "  <characteristic type=\"EMAIL2\">" +
                                            "    <characteristic type=\"" + oar.Guid + "\">" +
                                            "      <parm name=\"SERVICENAME\" value=\"" + oar.DisplayName + "\"/>" +
                                            "      <parm name=\"SERVICETYPE\" value=\"" + oar.AccountType.ToUpper() + "\"/>" +
                                            "      <parm name=\"INSERVER\" value=\"" + oar.IncommingServer + "\"/>" +
                                            "      <parm name=\"OUTSERVER\" value=\"" + oar.OutgoingServer + "\"/>" +
                                            "      <parm name=\"NAME\" value=\"" + oar.Name + "\"/>" +
                                            "      <parm name=\"REPLYADDR\" value=\"" + (oar.ReplyToAddress.Trim().Equals("") ? oar.EmailAddress : oar.ReplyToAddress) + "\"/>" +
                                            "      <parm name=\"AUTHNAME\" value=\"" + oar.UserName + "\"/>" +
                                            "      <parm name=\"AUTHSECRET\" value=\"" + oar.Password + "\"/>" +
                                            "      <parm name=\"AUTHREQUIRED\" value=\"" + (oar.AuthRequired ? "1" : "0") + "\"/>" +
                                            "      <parm name=\"DOMAIN\" value=\"" + oar.Domain + "\"/>" +
                                            "      <parm name=\"LINGER\" value=\"" + System.Convert.ToString(oar.Linger) + "\"/>" +
                                            "      <parm name=\"RETRIEVE\" value=\"" + System.Convert.ToString(oar.Retrieve) + "\"/>" +
                                            "      <parm name=\"DWNDAY\" value=\"" + System.Convert.ToString(oar.DwnDay) + "\"/>" +
                                            "    </characteristic>" +
                                            "  </characteristic>" +
                                            "</wap-provisioningdoc>");

                //ConfigurationManager.ProcessConfiguration(configurationXmlDoc, false);
                Wrappers.Wrappers.ProcessConfiguration(configurationXmlDoc);
                /**/
                return true;
            }
            catch
            {
                return false;
            } 
        }
    }

    public class OutlookAccountRecord
    {
        private string guid = "";
        private string name = "";
        private string displayName = "";
        private string accountType = "POP3";
        private string incommingServer = "";
        private string outgoingServer = "";
        private string emailAddress = "";
        private string replyToAddress = "";
        private string userName = "";
        private string password = "";
        private bool authRequired = false;
        private string domain = "";
        private int linger = 5;    // Time between verifications
        private int retrieve = -1; // The size of the message to be downloaded. -1 is Only Headers.
        private int dwnDay = -1;   // retrieve messages only retrieved on inbox within last n days.

        public string Guid
        {
            get { return guid; }
            set { guid = value; }
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        public string AccountType
        {
            get { return accountType; }
            set { accountType = value; }
        }

        public string IncommingServer
        {
            get { return incommingServer; }
            set { incommingServer = value; }
        }

        public string OutgoingServer
        {
            get { return outgoingServer; }
            set { outgoingServer = value; }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public string ReplyToAddress
        {
            get { return replyToAddress; }
            set { replyToAddress = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool AuthRequired
        {
            get { return authRequired; }
            set { authRequired = value; }
        }

        public string Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        public int Linger
        {
            get { return linger; }
            set { linger = value; }
        }

        public int Retrieve
        {
            get { return retrieve; }
            set { retrieve = value; }
        }

        public int DwnDay
        {
            get { return dwnDay; }
            set { dwnDay = value; }
        }
    }
}

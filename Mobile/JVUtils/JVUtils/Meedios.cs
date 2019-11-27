using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace JVUtils
{
    public static class Meedios
    {
        private static string key = "jf93729u40aspa91";
        private static string url = "http://meedios.com/remotetracker/";

        public static bool RegisterNewAccount(string email, string password)
        {
            Debug.AddLog("Meedios.RegisterNewAccount before Post", true);
            try
            {
                string res = Utils.Post(url + "register.php",
                                        "email=" +email.Trim().ToLower() +
                                        "&pass=" + Utils.MD5Hash(password.Trim().ToLower()).ToLower() +
                                        "&action=register");

                Debug.AddLog("Meedios.RegisterNewAccount result: " + res, true);
                return res.Contains("Thanks!");
            }
            catch (Exception e)
            {
                Debug.AddLog("Meedios.RegisterNewAccount error: " + e.Message.ToString(), true);
                return false;
            }
        }

        public static bool Login(string email, string password)
        {
            Debug.AddLog("Meedios.Login before Post", true);
            try
            {
                string res = Utils.Post(url + "login.php",
                                        "email=" + email.Trim().ToLower() +
                                        "&pass=" + Utils.MD5Hash(password.Trim().ToLower()).ToLower() +
                                        "&action=login");

                Debug.AddLog("Meedios.Login result: " + res, true);
                return res.Contains("successful");
            }
            catch (Exception e)
            {
                Debug.AddLog("Meedios.Login error: " + e.Message.ToString(), true);
                return false;
            }
        }

        public static MeediosResult SendConfig(string email, string password, string imei, string values)
        {
            Debug.AddLog("Meedios.SendConfig before Post", true);
            try
            {
                string res = Utils.Post(url + "api.php",
                                        "a=set&u=" + email.Trim().ToLower() +
                                        "&p=" + Utils.MD5Hash(password.Trim().ToLower()).ToLower() +
                                        "&k=" + Utils.MD5Hash(imei).ToLower() +
                                        "&v=" + values.Trim() +
                                        "&z=" + key);

                Debug.AddLog("Meedios.SendConfig result: " + res, true);
                if (res.ToLower().Contains("success"))
                    return MeediosResult.Ok;
                else
                {
                    if (res.IndexOf("#") > 0)
                    {
                        int err = System.Convert.ToInt32(res.Substring(res.IndexOf("#") + 1, 1));
                        if (err == 2)
                            return MeediosResult.WrongUser;
                        else if (err == 3 || err == 5)
                            return MeediosResult.WrongPass;
                        else if (err == 4)
                            return MeediosResult.InvalidUser;
                        else if (err == 6)
                            return MeediosResult.FailedGeneratedAccound;
                        else if (err == 7)
                            return MeediosResult.FailedStoreIMEI;
                        else if (err == 8)
                            return MeediosResult.InvalidAPIKey;
                        else
                            return MeediosResult.UndefinedError;
                    }
                    else
                        return MeediosResult.UndefinedError;
                }
            }
            catch (Exception e)
            {
                Debug.AddLog("Meedios.SendConfig error: " + e.Message.ToString(), true);
                return MeediosResult.OtherException;
            }
        }

        public static string GetConfig(string imei)
        {
            Debug.AddLog("Meedios.GetConfig before Post", true);
            try
            {
                string res = Utils.Post(url + "api.php", "k=" + Utils.MD5Hash(imei).ToLower() + "&z=" + key);

                Debug.AddLog("Meedios.GetConfig result: " + res, true);

                if (res.ToLower().Contains("error"))
                    return string.Empty;
                else
                    return res;
            }
            catch (Exception e)
            {
                Debug.AddLog("Meedios.GetConfig error: " + e.Message.ToString(), true);
                return string.Empty;
            }
        }
    }
}

// URL http://meedios.com/remotetracker

// 5 params (GET or POST -- post is more private and better)

// ['a'] == "action = set"
// ['u'] == "username (email)"
// ['p'] == "password"
// ['k'] == "key (lowercase md5 of 15 digit imei)"
// ['v'] == "value (+44123456;e@ma.il;"
// ['z'] == "api key, always jf93729u40aspa91"

// examples with GET:
// http://meedios.com/remotetracker/api.php?a=set&u=slug@skyforge.net&p=secrethehe&k=1d506d757ddfb7da952d19ff34852610&v=+447810123456;slug@skyforge.com&z=jf93729u40aspa91
// http://meedios.com/remotetracker/api.php?k=3ee58a8f9b255cfd6299b1ed5897cca3&z=jf93729u40aspa91
//                                         "k=3ee58a8f9b255cfd6299b1ed5897cca3&z=jf93729u40aspa91"                                          
// Registering:
// http://meedios.com/remotetracker/register.php?email=joubertvasc@softplan.com.br&pass=45ma12mb&action=register

// Login:
// http://meedios.com/remotetracker/login.php?email=joubertvasc@softplan.com.br&pass=45ma12mb&action=login

// Sending IMEI and Emergency number:
// http://meedios.com/remotetracker/index.php?imei=123456789012345&contact=<phone numbers>&action=addimei

// imsi=12345;phone=+1234566;email=blah@there.com
// email=first@email.org;email=second@email.org

//ERROR: [#123] short name - user interface error

//current messages:
//SUCCESS!
//ERROR: [#2] wrong user - a different user owns your imei, are you the thief?
//ERROR: [#3] wrong pass - enter correct password from signup
//ERROR: [#4] invalid user - you were trying to update someone elses imei without even having an account of which you specified
//ERROR: [#5] wrong pass - enter correct password from signup
//ERROR: [#6] failed to generate new account - a server error has occurred
//ERROR: [#7] failed to store imei - a server error has occurred
//ERROR: [#8] invalid api key - this application is not authorized to use this database
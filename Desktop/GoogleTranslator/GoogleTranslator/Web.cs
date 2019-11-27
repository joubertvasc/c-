using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace GoogleTranslator
{
    public static class Web
    {
        private static string FormatURL(string url)
        {
            if (!url.Trim().ToUpper().StartsWith("HTTP://"))
            {
                return "http://" + url.Trim();
            } else {
                return url.Trim();
            }
        }

        public static string Request(string URL)
        {
            URL = FormatURL(URL);

            string strResult = ""; 

            try
            {
                WebRequest req = WebRequest.Create(URL);
                WebResponse resp = req.GetResponse();
                using (StreamReader reader = new StreamReader
                (resp.GetResponseStream(), System.Text.Encoding.UTF7))
                {
                    strResult = reader.ReadToEnd();
                    reader.Close();
                }
                resp.Close();
            }
            catch (Exception ex)
            {
                strResult = "";
            }

            return strResult;
        }

        public static void Post(string url, string inputData)
        {
            url = FormatURL(url);

            // Set the 'Method' property of the 'Webrequest' to 'POST'.
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            myHttpWebRequest.Method = "POST";
            Console.WriteLine("\nPlease enter the data to be posted to the (http://www.contoso.com/codesnippets/next.asp) Uri :");

            string postData = "firstone=" + inputData;
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(postData);

            // Set the content type of the data being posted.
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";

            // Set the content length of the string being posted.
            myHttpWebRequest.ContentLength = byte1.Length;

            Stream newStream = myHttpWebRequest.GetRequestStream();

            newStream.Write(byte1, 0, byte1.Length);
            Console.WriteLine("The value of 'ContentLength' property after sending the data is {0}", myHttpWebRequest.ContentLength);

            // Close the Stream object.
            newStream.Close();
        }

        public static bool Upload(string url, string file)
        {
            bool result = false;

            if (File.Exists(file))
            {
                FileStream fs = new FileStream(file, FileMode.Open);

                if (fs != null)
                {
                    Uri requestUri = new Uri(FormatURL(url));
                    HttpWebRequest request = null;
                    HttpWebResponse response = null;
                    Stream responseStream = null;
                    StreamReader reader = null;

                    try
                    {
                        request = (HttpWebRequest)WebRequest.Create(requestUri);
                        request.Method = "POST";
                        request.Timeout = 0x4650;
                        request.ContentType = "multipart/form-data";
//                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = fs.Length;
                        StreamWriter writer = new StreamWriter(request.GetRequestStream());
                        writer.Write(fs.ToString());
                        writer.Close();
                        response = (HttpWebResponse)request.GetResponse();
                        responseStream = response.GetResponseStream();
                        reader = new StreamReader(responseStream, Encoding.UTF8);

                        result = true;
                    }
                    catch (Exception e)
                    {
                    }
                    finally
                    {
                        if (response != null)
                        {
                            response.Close();
                        }
                        if (responseStream != null)
                        {
                            responseStream.Close();
                        }
                        if (reader != null)
                        {
                            reader.Close();
                        }
                    }
                }
            }

            return result;
        }

        public static string UploadFile(string localFile, string uploadUrl)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uploadUrl);
            req.Method = "PUT";
            req.AllowWriteStreamBuffering = true;

            // Retrieve request stream and wrap in StreamWriter
            Stream reqStream = req.GetRequestStream();
            StreamWriter wrtr = new StreamWriter(reqStream);

            // Open the local file
            StreamReader rdr = new StreamReader(localFile);

            // loop through the local file reading each line 
            //  and writing to the request stream buffer
            string inLine = rdr.ReadLine();
            while (inLine != null)
            {
                wrtr.WriteLine(inLine);
                inLine = rdr.ReadLine();
            }

            rdr.Close();
            wrtr.Close();

            WebResponse resp = req.GetResponse();

            string strResult = "";
            
            using (StreamReader reader = new StreamReader (resp.GetResponseStream(), System.Text.Encoding.UTF7))
            {
                strResult += reader.ReadToEnd();
                reader.Close();
            }

            resp.Close();

            return strResult;
        }
    }
}

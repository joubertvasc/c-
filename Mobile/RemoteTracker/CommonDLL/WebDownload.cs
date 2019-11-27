using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using JVUtils;

namespace CommonDLL
{
    /// Class used to download files from the web when System.Net.WebClient
    /// is unavailable
    public static class WebDownload
    {
        /// Download a file as a string from the web.
        /// url - URL of the file to download
        /// returns - non empty string if it was successful - empty string otherwise
        public static string RetrieveString(string url)
        {
            System.Net.HttpWebRequest request = null;
            System.Net.HttpWebResponse response = null;

            System.IO.Stream strm = null;
            System.IO.StreamReader sr = null;

            String responseText = "";

            try
            {
                request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "OpenControl";
                request.Proxy = System.Net.GlobalProxySelection.GetEmptyWebProxy();

                request.Timeout = 10000; // 10 seconds
                response = (System.Net.HttpWebResponse)request.GetResponse();

                if (response != null)
                {
                    strm = response.GetResponseStream();
                    sr = new System.IO.StreamReader(strm);

                    responseText = sr.ReadToEnd();

                    strm.Close();
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                // something went terribly wrong.
                JVUtils.Debug.AddLog("error at RetrieveString - " + Utils.GetOnlyErrorMessage(ex.Message), true);
            }
            finally
            {
                // cleanup all potentially open streams.

                if (null != strm)
                    strm.Close();

                if (null != sr)
                    sr.Close();

                if (null != response)
                    response = null;

                if (null != request)
                    request = null;
            }

            return responseText;
        }

        /// Download a file from the web.
        /// url - URL of the file to download
        /// destination - Full path of the destination of the file we are downloading
        /// returns - flag indicating whether the file download was successful
        public static bool DownloadFile(string url, string destination)
        {
            bool success = false;

            System.Net.HttpWebRequest request = null;
            System.Net.WebResponse response = null;
            System.IO.Stream responseStream = null;
            System.IO.FileStream fileStream = null;

            try
            {
                request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 100000; // 100 seconds
                request.UserAgent = "OpenControl";
                request.Proxy = System.Net.GlobalProxySelection.GetEmptyWebProxy();

                response = request.GetResponse();

                responseStream = response.GetResponseStream();

                fileStream = System.IO.File.Open(destination, FileMode.Create, FileAccess.Write, FileShare.None);

                // read up to ten kilobytes at a time
                int maxRead = 10240;
                byte[] buffer = new byte[maxRead];
                int bytesRead = 0;
                int totalBytesRead = 0;

                // loop until no data is returned
                while ((bytesRead = responseStream.Read(buffer, 0, maxRead)) > 0)
                {
                    totalBytesRead += bytesRead;
                    fileStream.Write(buffer, 0, bytesRead);
                }

                // we got to this point with no exception. Ok.
                success = true;
            }
            catch (Exception exp)
            {
                // something went terribly wrong.
                success = false;
                JVUtils.Debug.AddLog(Utils.GetOnlyErrorMessage(exp.Message), true);
            }
            finally
            {
                // cleanup all potentially open streams.

                if (null != responseStream)
                    responseStream.Close();

                if (null != response)
                    response.Close();

                if (null != fileStream)
                    fileStream.Close();
            }

            // if part of the file was written and the transfer failed, delete the partial file
            if (!success && File.Exists(destination))
                File.Delete(destination);

            return success;
        }
    }

}

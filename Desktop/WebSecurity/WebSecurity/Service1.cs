using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using DirectShowLib;

namespace WebSecurity
{
    public partial class WebSecurity : ServiceBase
    {
        // Internal variables
        PictureBox pictureBox2;
        string appPath;
        string ultimaImagem = "";
        int devices;
        System.Timers.Timer tm = null;
        
        // Config variables
        int interval = 10;
        int videoDevice = 0;
        int videoWidth = 640;
        int videoHeight = 480;
        short videoBitsPerPixel = 24;
        string imagePath = "";
        
        public WebSecurity()
        {
            InitializeComponent();
            pictureBox2 = new System.Windows.Forms.PictureBox();

            appPath = Application.ExecutablePath;
            appPath = System.IO.Path.GetDirectoryName(appPath) + "\\";

            Debug.StartLog(appPath + "websecurity.log");
            Debug.SaveAfterEachAdd = true;
            Debug.Logging = true;
            Debug.AddLog("WebSecurity: Program start. Path=" + appPath, true);

            Debug.AddLog("WebSecurity: InitializeComponent. Reading configurations.", true);
            LoadConfig();

            if (imagePath.Equals(""))
                imagePath = appPath;

            if (!imagePath.EndsWith("\\"))
                imagePath += "\\";

            Debug.AddLog("WebSecurity: Interval = " + interval.ToString() +
                         " VideoDevice = " + videoDevice.ToString() +
                         " VideoWidth = " + videoWidth.ToString() +
                         " VideoHeight = " + videoHeight.ToString() +
                         " VideoBitsPerPixel = " + videoBitsPerPixel.ToString() +
                         " ImagePath = " + imagePath, 
                         true);

            DsDevice[] capDevices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
            devices = capDevices.Length;

            if (devices == 0)
            {
                Debug.AddLog("WebSecurity: There are video capture devices.", true);
                Application.Exit();
            }
            else
                Debug.AddLog("WebSecurity: Devices found=" + devices.ToString(), true);
        }

        private void LoadConfig()
        {
            string xml = appPath + "WebSecurity.xml";

            Debug.AddLog("Config: verificando se existe XML de configuração: " + xml, true);
            if (!xml.Trim().Equals("") && File.Exists(xml))
            {
                try
                {
                    Debug.AddLog("Config: lendo arquivo de configuração", true);
                    DataSet ds = new DataSet();
                    ds.ReadXml(xml);

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            //Rows[ind];
                            if (!ds.Tables[0].Rows[0]["Interval"].ToString().Equals(""))
                            {
                                Debug.AddLog("Config: reading interval", true);
                                interval = System.Convert.ToInt32(ds.Tables[0].Rows[0]["Interval"]);
                                Debug.AddLog("Config: interval=" + System.Convert.ToString(interval), true);
                            }
                            if (!ds.Tables[0].Rows[0]["VideoDevice"].ToString().Equals(""))
                            {
                                Debug.AddLog("Config: reading videoDevice", true);
                                videoDevice = System.Convert.ToInt32(ds.Tables[0].Rows[0]["VideoDevice"]);
                                Debug.AddLog("Config: videoDevice=" + System.Convert.ToString(videoDevice), true);
                            }
                            if (!ds.Tables[0].Rows[0]["VideoWidth"].ToString().Equals(""))
                            {
                                Debug.AddLog("Config: reading videoWidth", true);
                                videoWidth = System.Convert.ToInt32(ds.Tables[0].Rows[0]["VideoWidth"]);
                                Debug.AddLog("Config: videoWidth=" + System.Convert.ToString(videoWidth), true);
                            }
                            if (!ds.Tables[0].Rows[0]["VideoHeight"].ToString().Equals(""))
                            {
                                Debug.AddLog("Config: reading videoHeight", true);
                                videoHeight = System.Convert.ToInt32(ds.Tables[0].Rows[0]["VideoHeight"]);
                                Debug.AddLog("Config: videoHeight=" + System.Convert.ToString(videoHeight), true);
                            }
                            if (!ds.Tables[0].Rows[0]["VideoBitsPerPixel"].ToString().Equals(""))
                            {
                                Debug.AddLog("Config: reading videoBitsPerPixel", true);
                                videoBitsPerPixel = ((short)System.Convert.ToInt32(ds.Tables[0].Rows[0]["VideoBitsPerPixel"]));
                                Debug.AddLog("Config: videoBitsPerPixel=" + System.Convert.ToString(videoBitsPerPixel), true);
                            }
                            if (!ds.Tables[0].Rows[0]["ImagePath"].ToString().Equals(""))
                            {
                                Debug.AddLog("Config: reading imagePath", true);
                                imagePath = ds.Tables[0].Rows[0]["ImagePath"].ToString();
                                Debug.AddLog("Config: imagePath=" + System.Convert.ToString(imagePath), true);
                            }

                            Debug.AddLog("Config: arquivo de configuração lido com sucesso", true);
                        }
                        else
                        {
                            Debug.AddLog("Config: arquivo de configuração inválido", true);
                        }
                    }
                    else
                    {
                        Debug.AddLog("Config: arquivo de configuração inválido", true);
                    }
                }
                catch (Exception ex)
                {
                    Debug.AddLog("Config: Error- " + ex.Message.ToString(), true);
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            Debug.AddLog("OnStart", true);

            System.Timers.Timer tm = new System.Timers.Timer(interval * 60000);
            tm.Elapsed +=new System.Timers.ElapsedEventHandler(tm_Elapsed);
            tm.AutoReset = true;
            tm.Interval = interval * 60000;
            tm.Enabled = true;
            Debug.AddLog("OnStart: Interval= " + System.Convert.ToString(tm.Interval) +
                         " Enabled= " + (tm.Enabled ? "yes" : "no"), true);

            DoScreenShot();
//            timer1.Interval = interval * 60000;
//            timer1.Enabled = true;
            Debug.AddLog("OnStart: Started.", true);
        }

        protected override void OnStop()
        {
            Debug.AddLog("OnStop", true);
            tm.Enabled = false;

            Debug.EndLog();
        }

        private void tm_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            Debug.AddLog("TM tick.", true);
            DoScreenShot();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Debug.AddLog("Timer tick.", true);
            DoScreenShot();
        }

        private string ChangeChar(string original, char charToReplace, char replacingChar)
        {
            string result = "";

            for (int i = 0; i < original.Length; i++)
            {
                if (original.Substring(i, 1) == charToReplace.ToString())
                {
                    result += replacingChar;
                }
                else
                {
                    result += original.Substring(i, 1);
                }
            }

            return result;
        }

        private void DoScreenShot()
        {
            Debug.AddLog("DoScreenShot: Start.", true);

            if (tm != null)
            {
                tm.Enabled = false;
                Debug.AddLog("DoScreenShot: Timer=" + (tm.Enabled ? "on" : "off"), true);
            }
            try
            {
                try
                {
                    for (int i = 0; i <= devices - 1; i++)
                    {
                        Debug.AddLog("DoScreenShot: before get image from camera " + i.ToString(), true);

                        IntPtr m_ip = IntPtr.Zero;
                        Capture cam = new Capture(i, videoWidth, videoHeight, videoBitsPerPixel, pictureBox2);

                        // capture image
                        m_ip = cam.Click();
                        Bitmap b = new Bitmap(cam.Width, cam.Height, cam.Stride, PixelFormat.Format24bppRgb, m_ip);
                        Debug.AddLog("DoScreenShot: after get image from camera " + i.ToString(), true);

                        // If the image is upsidedown
                        Debug.AddLog("DoScreenShot: before rotate image", true);
                        b.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        Debug.AddLog("DoScreenShot: after rotate image, before save file", true);

                        if (ImageChanged(b, i))
                        {
                            string data = System.DateTime.Now.ToString();
                            data = ChangeChar(data, '/', '-');
                            data = ChangeChar(data, ':', '-');

                            string fn = imagePath + "Imagem-cam-" + i.ToString() +
                                        "-" + data + ".png";

                            b.Save(fn, ImageFormat.Png);
                            ultimaImagem = fn;
                            Debug.AddLog("DoScreenShot: after save file.", true);
                        }
                        else
                        {
                            Debug.AddLog("DoScreenShot: nothing to save from camera " + i.ToString() + ".", true);
                        }

                        // Release any previous buffer
                        if (m_ip != IntPtr.Zero)
                        {
                            Debug.AddLog("DoScreenShot: releasing memory.", true);
                            Marshal.FreeCoTaskMem(m_ip);
                            m_ip = IntPtr.Zero;
                            Debug.AddLog("DoScreenShot: memory released.", true);
                        }

                        cam.Dispose();
                    }
                }
                catch (Exception e)
                {
                    Debug.AddLog("DoScreenShot: error- " + e.ToString(), true);
                }
            }
            finally
            {
                if (tm != null)
                {
                    tm.Interval = interval * 60000;
                    tm.Enabled = true;
                    Debug.AddLog("DoScreenShot: Timer=" + (tm.Enabled ? "on" : "off"), true);
                }
            }

            Debug.AddLog("DoScreenShot: End.", true);
        }

        private bool ImageChanged(Bitmap b, int device)
        {
            if (ultimaImagem.Equals("") || !File.Exists(ultimaImagem))
            {
                ultimaImagem = "";
                return true;
            }
            else
            {
                Debug.AddLog("ImageChanged: Started.", true);
                string hash1;
                string hash2;

                MemoryStream ms = new MemoryStream();
                b.Save(ms, ImageFormat.Png);

                SHA256Managed sha = new SHA256Managed();
                byte[] checksum = sha.ComputeHash(ms);
                hash1 = BitConverter.ToString(checksum).Replace("-", String.Empty);

                using (FileStream stream = File.OpenRead(ultimaImagem))
                {
                    SHA256Managed sha2 = new SHA256Managed();
                    byte[] checksum2 = sha2.ComputeHash(stream);
                    hash2 = BitConverter.ToString(checksum2).Replace("-", String.Empty);
                }

                Debug.AddLog("ImageChanged: hash1=" + hash1 + " hash2=" + hash2 +
                             " Changed=" + (!hash1.Equals(hash2) ? "yes" : "no"), true);
                return !hash1.Equals(hash2);
            }
        }
    }
}

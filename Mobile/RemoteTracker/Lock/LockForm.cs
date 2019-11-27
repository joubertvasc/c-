using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsCE.Forms;
using System.Runtime.InteropServices;
using JVUtils;
using OpenNETCF.ToolHelp;

namespace Lock
{
    public enum KeysHardware : int
    {
        RedPhoneButton = 0x73,
        GreenPhoneButton = 0x72
    }

    public partial class LockForm : Form
    {
        internalMessageWindow messageWindow;
        string MessageToShow = "This device was locked by its owner.";

        public LockForm()
        {
            string appPath = Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase);
            appPath = appPath.Substring(0, appPath.LastIndexOf(@"\") + 1);

            if (File.Exists(appPath + "Lock.txt"))
            {
                MessageToShow = "";
                using (StreamReader sr = new StreamReader(appPath + "Lock.txt"))
                {
                    // Process every line in the file
                    for (String Line = sr.ReadLine(); Line != null; Line = sr.ReadLine())
                        MessageToShow += Line;
                }
            }

            InitializeComponent();

            this.messageWindow = new internalMessageWindow(this);
            RegisterHKeys.RegisterRecordKey(this.messageWindow.Hwnd);
        }

        ~LockForm()
        {
//            Kernel.ShowWindow(Kernel.FindWindow("HHTaskBar", null), 1); 
        }

        private void LockForm_Load(object sender, EventArgs e)
        {
            lblMessage.Text = MessageToShow;

            Kernel.SignalStarted(uint.Parse("0"));
            IntPtr handle = Handle;
            Kernel.SHFullScreen(handle, Kernel.SHFS_HIDETASKBAR);
            Kernel.SHFullScreen(handle, Kernel.SHFS_HIDESIPBUTTON);
            Kernel.SHFullScreen(handle, Kernel.SHFS_HIDESTARTICON);

            if (!Utils.IsTouchScreen())
            {
                WindowState = FormWindowState.Normal;
                int l = Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;

                Kernel.MoveWindow(handle, 0, -l, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height + l, 1);
                Kernel.MoveWindow(Kernel.FindWindowW("HHTaskBar", null), 0, 
                                  Screen.PrimaryScreen.Bounds.Height, 
                                  Screen.PrimaryScreen.Bounds.Width, l, 1);
            }
            else
            {
                Menu = null;
                ControlBox = false;
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                Text = string.Empty;
                MaximizeBox = false;
                MinimizeBox = false;
            }
        }

        private void LockForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }
        }
    }

    public class internalMessageWindow : MessageWindow
    {
        // Which message type ?
        public const int WM_HOTKEY = 0x0312;

        Form referedForm;

        public internalMessageWindow(Form referedForm)
        {
            this.referedForm = referedForm;
        }

        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
            {
                case WM_HOTKEY:
                    // Do no reply to this key ...
                    return;
            }
            base.WndProc(ref msg);
        }
    }

    public class RegisterHKeys
    {
        public static void RegisterRecordKey(IntPtr hWnd)
        {
            // Disable StartMenu Button. Not working properly...
            //Kernel.ShowWindow(Kernel.FindWindow("HHTaskBar", null), 0); 

            // Disable Phone buttons
            ProcessEntry pe = null;

            try
            {
                pe = Utils.SearchProcess("cprog.exe");
            }
            catch
            {
                pe = null;
            }

            if (pe != null)
                Utils.KillProcess(pe);

            Kernel.UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.GreenPhoneButton);
            Kernel.RegisterHotKey(hWnd, (int)KeysHardware.GreenPhoneButton, KeyModifiers.None, (int)KeysHardware.GreenPhoneButton);

            Kernel.UnregisterFunc1(KeyModifiers.Windows, (int)KeysHardware.RedPhoneButton);
            Kernel.RegisterHotKey(hWnd, (int)KeysHardware.RedPhoneButton, KeyModifiers.None, (int)KeysHardware.RedPhoneButton);

            // Disable other hardware buttons
            RegistryKey r = Registry.LocalMachine.OpenSubKey("\\Software\\Microsoft\\Shell\\Keys");
            if (r != null)
            {
                string[] keys = r.GetSubKeyNames();

                foreach (string k in keys)
                {
                    string x = k.Substring(k.Length - 2, 2);
                    int i = -1;

                    if (x.Equals("C1"))
                        i = 193;
                    else if (x.Equals("C2"))
                        i = 194;
                    else if (x.Equals("C3"))
                        i = 195;
                    else if (x.Equals("C4"))
                        i = 196;
                    else if (x.Equals("C5"))
                        i = 197;
                    else if (x.Equals("C6"))
                        i = 198;
                    else if (x.Equals("C7"))
                        i = 199;
                    else if (x.Equals("C8"))
                        i = 200;
                    else if (x.Equals("C9"))
                        i = 201;

                    if (i > -1)
                    {
                        Kernel.UnregisterFunc1(KeyModifiers.Windows, i);
                        Kernel.RegisterHotKey(hWnd, i, KeyModifiers.Windows, i);
                    }
                }

                r.Close();
            }
        }
    }
}
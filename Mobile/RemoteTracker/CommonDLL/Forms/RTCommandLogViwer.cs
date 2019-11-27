using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using JVUtils;

namespace CommonDLL.Forms
{
    public partial class RTCommandLogViwer : Form
    {
        public string logPath;

        public RTCommandLogViwer()
        {
            InitializeComponent();
        }

        private void miOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RTCommandLogViwer_Load(object sender, EventArgs e)
        {
            tbCommands.Text = "";

            if (File.Exists(logPath))
            {
                using (StreamReader sr = new StreamReader(logPath))
                {
                    // Process every line in the file
                    for (String Line = sr.ReadLine(); Line != null; Line = sr.ReadLine())
                        tbCommands.Text += SimpleCryptography.DeCryptography(Line) + Environment.NewLine;
                }
            }
            else
            {
                tbCommands.Text = Messages.msg_CommandLogEmpty;
            }
        }
    }
}
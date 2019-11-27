using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Globalization;

namespace GoogleTranslator
{
    public partial class GoogleTranslator : Form
    {
        string appPath = "";

        public GoogleTranslator()
        {
            InitializeComponent();
        }

        private void GoogleTranslator_Load(object sender, EventArgs e)
        {
            appPath = Application.ExecutablePath;
            appPath = System.IO.Path.GetDirectoryName(appPath);

            if (!appPath.EndsWith(@"\"))
            {
                appPath += @"\";
            }

            ClearApp();
            LoadConfiguration();
        }

        private void GoogleTranslator_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConfig();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            CloseConfig();
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            ClearApp();
        }

        void ClearApp()
        {
            tcTranslations.TabPages.Clear();
            tbSourceText.Text = "";
            tbSourceText.Focus();

            pbTranslating.Value = 0;
        }

        void LoadConfiguration()
        {
            if (File.Exists(appPath + "GoogleTranslator.cfg"))
            {
                using (StreamReader sr = new StreamReader(appPath + "GoogleTranslator.cfg"))
                {
                    // Process every line in the file
                    for (String line = sr.ReadLine(); line != null; line = sr.ReadLine())
                    {
                        for (int i = 0; i < gbLanguages.Controls.Count; i++)
                        {
                            if (gbLanguages.Controls[i].Tag.ToString().ToLower().Trim().Equals(line.ToLower().Trim()))
                            {
                                ((CheckBox)gbLanguages.Controls[i]).Checked = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        void SaveConfiguration()
        {
            StreamWriter sw = File.CreateText(appPath + "GoogleTranslator.cfg");

            for (int i = 0; i < gbLanguages.Controls.Count; i++)
            {
                if (((CheckBox)gbLanguages.Controls[i]).Checked)
                    sw.WriteLine(gbLanguages.Controls[i].Tag.ToString());
            }

            sw.Flush();
            sw.Close();
        }

        void CloseConfig()
        {
            SaveConfiguration();
            Application.Exit();
        }

        private void btTranslate_Click(object sender, EventArgs e)
        {
            pbTranslating.Value = 0;
            pbTranslating.Maximum = gbLanguages.Controls.Count;
            pbTranslating.Minimum = 0;

            for (int i = 0; i < gbLanguages.Controls.Count; i++)
            {
                if (((CheckBox)gbLanguages.Controls[i]).Checked)
                {
                    string res = 
                        Web.Request("http://translate.google.com.br/translate_t?hl=pt-BR&ie=UTF-8&sl=en&tl=" +
                                    gbLanguages.Controls[i].Tag.ToString() + 
                                    "&text=" + tbSourceText.Text.Trim());

                    int p = res.ToLower().IndexOf("<div id=result_box dir=") + 29;
                    int f = res.ToLower().IndexOf("</div></td></tr><tr>");

                    res = res.Substring(p, (f - p));

                    tcTranslations.TabPages.Add(gbLanguages.Controls[i].Text);

                    RichTextBox tb = new RichTextBox();
                    tb.Parent = tcTranslations.TabPages[tcTranslations.TabCount - 1];
                    tb.Multiline = true;
                    tb.ReadOnly = true;
                    tb.WordWrap = true;
                    tb.ScrollBars = RichTextBoxScrollBars.Vertical;
                    tb.Dock = DockStyle.Fill;
                    tb.Text = res;
                }

                pbTranslating.Value++;
            }

            pbTranslating.Value = 0;
        }
    }
}

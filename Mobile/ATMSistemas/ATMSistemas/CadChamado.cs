using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JVUtils;

namespace ATMSistemas
{
    public partial class CadChamado : Form
    {
        string imei;
        string imsi;

        string notes;
        bool showNotes;
        bool showInfo;

        public CadChamado()
        {
            InitializeComponent();
        }

        private void miCancel_Click(object sender, EventArgs e)
        {
            DoSair();
        }

        private void CadChamado_Closing(object sender, CancelEventArgs e)
        {
            DoSair();
        }

        void DoSair()
        {
            SalveConfiguracoes();
            Application.Exit();
        }

        private void CadChamado_Load(object sender, EventArgs e)
        {
            LeiaConfiguracoes();

            PhoneInfo pi = new PhoneInfo();
            imei = pi.GetIMEI();
            imsi = pi.GetIMSI();

            lblVlIMEI.Text = imei;
            lblVlIMSI.Text = imsi;
            lblVlLinha.Text = "<<não implementado>>";
            cbMotivo.SelectedIndex = 0;
        }

        void LeiaConfiguracoes()
        {
            OwnerRecord or = OwnerInfo.GetOwnerRecord();

            if (or != null)
            {
                tbNome.Text = or.UserName;
                tbEmpresa.Text = or.Company;

                if (or.Phone.Length > 2)
                {
                    tbDDD.Text = or.Phone.Substring(0, 2);
                    tbCelContato.Text = or.Phone.Substring(2);
                }
                else
                {
                    tbDDD.Text = "";
                    tbCelContato.Text = "";
                }

                if (or.Address.Contains("\\"))
                {
                    tbCidade.Text = or.Address.Substring(0, or.Address.IndexOf("\\"));
                    tbBairro.Text = or.Address.Substring(or.Address.IndexOf("\\") + 1);
                }
                else
                {
                    tbCidade.Text = or.Address;
                    tbBairro.Text = "";
                }
                tbEMail.Text = or.EMail;
                notes = or.Notes;
                showNotes = or.ShowNotes;
                showInfo = or.ShowIdentificationInformation;
            }
        }

        void SalveConfiguracoes()
        {
            OwnerRecord or = new OwnerRecord();
            or.UserName = tbNome.Text;
            or.Company = tbEmpresa.Text;
            or.Phone = tbDDD.Text + tbCelContato.Text;
            or.Address = tbCidade.Text + "\\" + tbBairro.Text;
            or.EMail = tbEMail.Text;
            or.Notes = notes;
            or.ShowNotes = showNotes;
            or.ShowIdentificationInformation = showInfo;

            OwnerInfo.SetOwnerRecord(or);
        }

        private void miChamado_Click(object sender, EventArgs e)
        {
            if (tbNome.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'Nome' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbNome.Focus();
            }
            else if (tbDDD.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'DDD' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbDDD.Focus();
            }
            else if (!Utils.IsNumberValid(tbDDD.Text))
            {
                MessageBox.Show("O campo 'DDD' é inválido. Favor digitar apenas números", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbDDD.Focus();
            }
            else if (tbCelContato.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'Cel. Contato' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbCelContato.Focus();
            }
            else if (!Utils.IsNumberValid(tbCelContato.Text))
            {
                MessageBox.Show("O campo 'Cel. Contato' é inválido. Favor digitar apenas números", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbCelContato.Focus();
            }
            else if (tbCidade.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'Cidade' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbCidade.Focus();
            }
            else if (tbBairro.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'Bairro' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbBairro.Focus();
            }
            else if (tbEMail.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'E-Mail' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbEMail.Focus();
            }
            else if (!Utils.IsValidEMail(tbEMail.Text))
            {
                MessageBox.Show("O campo 'E-Mail' não parece ser válido. Por favor revise.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                tbEMail.Focus();
            }
            else if (cbMotivo.SelectedIndex == -1 || cbMotivo.Text.Trim().Equals(""))
            {
                MessageBox.Show("O campo 'Motivo' é obrigatório", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                cbMotivo.Focus();
            }
            else
            {
                // X;IP;EMAIL;IMSI;EMEI;BAT;NOME;DDD+CEL;CIDADE+BAIRRO;MOTIVO
                string currentIP;

                Utils.ShowWaitCursor();
                try
                {
                    string[] IPs = GetInternalIP();
                    currentIP = "";

                    if (IPs.Length == 0)
                        currentIP = "0";
                    else
                        currentIP = IPs[0];
                }
                finally
                {
                    Utils.HideWaitCursor();
                }

                SalveConfiguracoes();
                MessageBox.Show("0;" + currentIP + ";" + tbEMail.Text + ";" + imsi + ";" + imei + ";" + System.Convert.ToString(Utils.BatteryMetter()) +
                                ";" + tbNome.Text + ";" + tbDDD.Text + tbCelContato.Text + ";" + tbCidade.Text + " " + tbBairro.Text + ";" + cbMotivo.Text, 
                                "Exemplo");
            }
        }

        string[] GetInternalIP()
        {
            try
            {
                string[] ip = JVUtils.Utils.GetIPAddress();
                Debug.AddLog("GetInternalIP: number of addresses: " + ip.Length.ToString(), true);

                // No ip address or Only activesync designed IP address force GPRS/3G connection
                if (ip.Length == 0 || (ip.Length == 1 && (ip[0].StartsWith("169.") || ip[0].Equals("127.0.0.1"))))
                {
                    Debug.AddLog("GetInternalIP: trying to access http://www.google.com to force a connection.", true);
                    try
                    {
                        Web.Request("http://www.google.com"); // Try to connect to google
                    }
                    catch (Exception e)
                    {
                        Debug.AddLog("GetInternalIP: error trying to force a connection: " + Utils.GetOnlyErrorMessage(e.Message), true);
                    }

                    ip = Utils.GetIPAddress();
                    Debug.AddLog("GetInternalIP: number of addresses: " + ip.Length.ToString(), true);
                }

                string[] validIP = new string[0];
                for (int i = 0; i < ip.Length; i++)
                {
                    // Ignore loopback and the IP designed by ActiveSync if there are more Address
                    if (!ip[i].Equals("127.0.0.1"))
                        if (!ip[i].StartsWith("169.") || (ip[i].StartsWith("169.") && ip.Length == 1))
                        {
                            Array.Resize(ref validIP, validIP.Length + 1);
                            validIP[validIP.Length - 1] = ip[i];
                        }
                }

                return validIP;
            }
            catch (Exception ex)
            {
                Debug.AddLog("GetInternalIP: Error message: " + ex.ToString(), true);

                return new string[0];
            }
        }

        private void tbNome_LostFocus(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = Utils.Capitalize(((TextBox)sender).Text);
        }
    }
}
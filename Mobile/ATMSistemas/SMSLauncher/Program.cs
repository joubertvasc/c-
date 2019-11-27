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
using System.Threading;
using CommonDLL;
using Microsoft.WindowsMobile.PocketOutlook;
using Microsoft.WindowsMobile.PocketOutlook.MessageInterception;
using Microsoft.WindowsMobile.Status;
using Microsoft.WindowsCE.Forms;
using JVUtils;
using ATMDLL;

namespace SMSLauncher
{
    class Program
    {
        static RTCommon rtCommon;
        static string sInstalledIMSI = "";
        static string sUltimoIMSI = "";
        static string sIMEI = "";

        static void Main(string[] args)
        {
            try
            {
                Debug.StartLog();

                // Hide cursor
                Utils.HideWaitCursor();

                // Execute common taks 
                rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase), false);
                ATM.RT = rtCommon;
                ATM.LoadXML(ShellFolders.TempFolder + "\\atmconfig.xml");

                Debug.AddLog("ATM Versão: " + ATM.Versao, true);
                Debug.AddLog("JVUtils Versão: " + JVUtils.JVUtils.Version, true);
                Debug.AddLog("CommonDLL versão: " + ATM.RT.version, true);

                bool notificacao = ATM.DispositivoEmEstadoDeNotificacao();

                ATM.ReconfigureContaEMail();

                Debug.AddLog("DefaultNumber1 is set? " + (rtCommon.configuration.defaultNumber1.Equals("") ? "No" : "Yes") + "\n" +
                             "Outgoing e-mail account is set? " + (rtCommon.configuration.defaultEMailAccount.Equals("") ? "No" : "Yes") + "\n" +
                             "Default recipient is set? " + (rtCommon.configuration.defaultrecipientEMail.Equals("") ? "No" : "Yes") + "\n" +
                             "EmergencyEMail1 is set? " + (rtCommon.configuration.emergencyEMail1.Equals("") ? "No" : "Yes") + "\n" +
                             "Config IMSI1 is set? " + (rtCommon.configuration.IMSI1.Equals("") ? "No" : "Yes") + "\n" +
                             "Config IMSI2 is set? " + (rtCommon.configuration.IMSI2.Equals("") ? "No" : "Yes"), true);

                // Set SMS interceptor
                rtCommon.CreateInterceptor();

                // Carrega as informações de proprietário
                ATM.LeiaOwnerInfo();

                // Configura os campos IMSI1 e IMSI2 dependendo do SIM card instalado.
                if (ATM.VerificaSIMCard(rtCommon.GetIMSI()))
                    ATM.SalvarConfiguracaoRT();

                // Inicia o teste de troca do SIM card.
                if (rtCommon.configuration.defaultNumber1 != "" &&
                    (rtCommon.configuration.IMSI1 != "" || rtCommon.configuration.IMSI2 != ""))
                {
                    int nCount = 0;
                    bool bCanSendMessage = false;

                    // Verify if the Phone Service is ok. If not, try 30 times each 10 seconds
                    do
                    {
                        // Get the installed IMSI;
                        sInstalledIMSI = rtCommon.GetIMSI();
                        // Pega o último cartão usado, para comparar com o atualmente instalado
                        sUltimoIMSI = Configuracao.GetUltimoCartaoUsado();
                        // Pega o identificador do aparelho
                        sIMEI = rtCommon.GetIMEI();

                        Debug.AddLog("Installed IMSI ok? " + Utils.iif(sInstalledIMSI != "", "Yes", "No"));

                        // Verify if SIM card was changed.
                        if (sInstalledIMSI != "")
                        {
                            Debug.AddLog("IMSI1 is equal than installed? " + (rtCommon.configuration.IMSI1.Equals(sInstalledIMSI) ? "Yes" : "No"));
                            Debug.AddLog("IMSI2 is equal than installed? " + (rtCommon.configuration.IMSI2.Equals(sInstalledIMSI) ? "Yes" : "No"));
                            Debug.AddLog("IMSI instalado é igual ao último usado? " + (sInstalledIMSI.Equals(sUltimoIMSI) ? "Sim" : "Não"));

                            // Verifica se precisa fazer a verificação inicial de conexão com a internet
                            bool ok = false;
                            if (DateTime.Today != Configuracao.PegaUltimaVerificacaoInicial())
                            {
                                string ip = Utils.PegaIP();

                                if (ip.Equals("0"))
                                    ok = ATM.EnviaMensagem(ATMResultado.VerificacaoInicialSemIP,
                                                           ATMMotivosType.Nenhum,
                                                           TipoMensagem.SomenteSMS);
                                else
                                    ok = ATM.EnviaMensagem(ATMResultado.VerificacaoInicialComIP,
                                                           ATMMotivosType.Nenhum,
                                                           (notificacao ? TipoMensagem.SomenteSMS : TipoMensagem.Ambos));

                                if (ok)
                                    Configuracao.GuardaUltimaVerificacaoInicial(DateTime.Today);
                            }

                            // Testa se o cartão foi trocado ou se ele é diferente do principal e contingente
                            if (!ok &&
                                (!sInstalledIMSI.Equals(sUltimoIMSI) ||
                                 (!rtCommon.configuration.IMSI1.Equals(sInstalledIMSI) &&
                                  !rtCommon.configuration.IMSI2.Equals(sInstalledIMSI))))
                            {
                                bCanSendMessage = false;

                                Debug.AddLog("Count = " + System.Convert.ToString(nCount) + "\n" +
                                    "RadioOff? " + (Microsoft.WindowsMobile.Status.SystemState.PhoneRadioOff ? "Yes" : "No") + "\n" +
                                    "NoService? " + (Microsoft.WindowsMobile.Status.SystemState.PhoneNoService ? "Yes" : "No") + "\n" +
                                    "Searching? " + (Microsoft.WindowsMobile.Status.SystemState.PhoneSearchingForService ? "Yes" : "No"));
                                if (!Microsoft.WindowsMobile.Status.SystemState.PhoneRadioOff &&
                                    !Microsoft.WindowsMobile.Status.SystemState.PhoneNoService &&
                                    !Microsoft.WindowsMobile.Status.SystemState.PhoneSearchingForService)
                                {
                                    bCanSendMessage = true;
                                }

                                // If the phone line is ok, send the message.
                                Debug.AddLog("Can send message? " + Utils.iif(bCanSendMessage, "Yes", "No"));
                                if (bCanSendMessage)
                                {
                                    // Se trocou o cartão, verifica se não precisa enviar alguma mensagem de erro pendente
                                    if (!sUltimoIMSI.Equals(sInstalledIMSI))
                                    {
                                        string pendente = Configuracao.PegaErroEnvioSMS(sInstalledIMSI);

                                        if (!pendente.Equals(""))
                                        {
                                            try
                                            {
                                                if (ATM.EnviaMensagemPendente(pendente))
                                                    Configuracao.ApagaErroEnvioSMS();
                                            }
                                            catch (Exception ex)
                                            {
                                            }
                                        }
                                    }

                                    ATMResultado motivo = ATMResultado.NenhumaMensagem;

                                    // Definindo motivo da mensagem
                                    if (sUltimoIMSI.Equals(rtCommon.configuration.IMSI1) &&
                                        sInstalledIMSI.Equals(rtCommon.configuration.IMSI2))
                                    {
                                        Debug.AddLog("Tirou SIM principal e colocou contingente.");
                                        motivo = ATMResultado.UsuarioTirouChipPrincipal;
                                    }
                                    else if (sUltimoIMSI.Equals(rtCommon.configuration.IMSI2) &&
                                             sInstalledIMSI.Equals(rtCommon.configuration.IMSI1))
                                    {
                                        Debug.AddLog("Tirou SIM principal e colocou contingente.");
                                        motivo = ATMResultado.UsuarioTirouChipContingente;
                                    }
                                    else if (!sInstalledIMSI.Equals(sUltimoIMSI) ||
                                             (!sInstalledIMSI.Equals(rtCommon.configuration.IMSI1) &&
                                              !sInstalledIMSI.Equals(rtCommon.configuration.IMSI2)))
                                    {
                                        Debug.AddLog("Tirou inserir SIM card desconhecido.");
                                        motivo = ATMResultado.ChipNaoEhPrincipalNemContingente;
                                    }

                                    // Envia SMS e EMAIL...
                                    if (notificacao)
                                    {
                                        Debug.AddLog("Dispositivo em estado de notificação. Enviando apenas SMS", true);
                                        ATM.EnviaMensagem(motivo, ATMMotivosType.Nenhum, TipoMensagem.SomenteSMS);
                                    }
                                    else
                                    {
                                        Debug.AddLog("Dispositivo fora do estado de notificação. Enviando SMS e EMAIL", true);
                                        ATM.EnviaMensagem(motivo, ATMMotivosType.Nenhum, TipoMensagem.Ambos);
                                    }

                                    break;
                                }
                                else
                                {
                                    Debug.AddLog("Sleeping, phone not ok...");
                                    System.Threading.Thread.Sleep(10000);

                                    nCount++;
                                    Debug.AddLog("Count = " + nCount.ToString(), true);
                                }
                            }
                            else
                            {
                                Debug.AddLog("Abortando. O cartão é conhecido e não foi trocado desde o último reboot, ou já foi enviado uma mensagem hoje.", true);
                                break;
                            }
                        }
                        else
                        {
                            Debug.AddLog("Sleeping, new IMSI not found...");
                            System.Threading.Thread.Sleep(10000);

                            nCount++;
                            Debug.AddLog("Count = " + nCount.ToString(), true);
                        }
                    } while (nCount < 60);
                    Debug.AddLog("After DO looping", true);

                    // Registra o SIM atual como sendo o último usado
                    if (!sInstalledIMSI.Trim().Equals("") && !sInstalledIMSI.Equals(sUltimoIMSI))
                        Configuracao.SetUltimoCartaoUsado();
                }
                else
                {
                    Debug.AddLog("DefaultNumert1 or any IMSI are not defined.", true);
                }
            }
            catch (Exception ex)
            {
                Debug.AddLog("Exceção: " + ex.Message.ToString() + "\n\n" + ex.StackTrace, true);            
            }

            Debug.EndLog();
            Debug.SaveLog(ShellFolders.TempFolder + "\\SMSLauncher.Debug.txt");

            // Signal to kernel the application was loaded
            Kernel.SignalStarted(uint.Parse("0"));
        }
    }
}
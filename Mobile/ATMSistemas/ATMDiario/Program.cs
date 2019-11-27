using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CommonDLL;
using ATMDLL;
using JVUtils;

namespace ATMDiario
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.StartLog(ShellFolders.TempFolder + "\\ATMDiario.Debug.txt");
            Debug.SaveAfterEachAdd = true;
            Debug.Logging = true;
            Debug.AddLog("Iniciando...", true);

            Debug.AddLog("Criando rtCommon.", true);
            RTCommon rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase), false);
            ATM.RT = rtCommon;

            if (DateTime.Today != Configuracao.PegaUltimaVerificacaoInicial())
            {
                Debug.AddLog("ATM Versão: " + ATM.Versao, true);
                Debug.AddLog("JVUtils Versão: " + JVUtils.JVUtils.Version, true);
                Debug.AddLog("CommonDLL versão: " + ATM.RT.version, true);

                ATM.LoadXML(ShellFolders.TempFolder + "\\atmconfig.xml");

                if (File.Exists(Configuracao.PegaCaminhoATM() + "rt.exe"))
                {
                    string ip = Utils.PegaIP();
                    bool notificacao = ATM.DispositivoEmEstadoDeNotificacao();
                    bool ok = false;

                    ATM.LeiaOwnerInfo();

                    if (ip.Equals("0"))
                        ok = ATM.EnviaMensagem(ATMResultado.VerificacaoInicialSemIP, ATMMotivosType.Nenhum, TipoMensagem.SomenteSMS);
                    else
                        ok = ATM.EnviaMensagem(ATMResultado.VerificacaoInicialComIP, ATMMotivosType.Nenhum,
                                                 (notificacao ? TipoMensagem.SomenteSMS : TipoMensagem.SomenteEMail));

                    if (ok)
                        Configuracao.GuardaUltimaVerificacaoInicial(DateTime.Today);
                }
                else
                    Debug.AddLog("RemoteTracker não foi encontrado no caminho: " + Configuracao.PegaCaminhoATM() + "rt.exe", true);
            }
            else
            {
                Debug.AddLog("Nada para fazer. O log de hoje já foi gerado.", true);
            }

            Debug.AddLog("Terminando...", true);

            // Depois de executar o RemoteTracker para executar o comando ALOG, agenda para nova execução no mês subsequente.
            ATM.AgendaProximoEnvioDiario();

            Debug.SaveLog();
            Debug.EndLog();
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using CommonDLL;
using ATMDLL;
using JVUtils;

namespace ATMWakeUp
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.StartLog(ShellFolders.TempFolder + "\\ATMWakeUp.Debug.txt");
            Debug.SaveAfterEachAdd = true;
            Debug.Logging = true;

            Debug.AddLog("Iniciando... Criando rtCommon.", true);
            RTCommon rtCommon = new RTCommon(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase), false);
            ATM.RT = rtCommon;

            Debug.AddLog("ATM Versão: " + ATM.Versao, true);
            Debug.AddLog("JVUtils Versão: " + JVUtils.JVUtils.Version, true);
            Debug.AddLog("CommonDLL versão: " + ATM.RT.version, true);
            
            ATM.LoadXML(ShellFolders.TempFolder + "\\atmconfig.xml");

            if (File.Exists(Configuracao.PegaCaminhoATM() + "rt.exe"))
            {
                Debug.AddLog("Executando o RemoteTracker...", true);

                System.Diagnostics.Process.Start(Configuracao.PegaCaminhoATM() + "rt.exe",
                                                 "/c:alog /n:" + LineNumberStore.GetLineNumberForIMSI(rtCommon.GetIMSI()));
                Debug.AddLog("RemoteTracker executado.", true);
            }
            else
                Debug.AddLog("RemoteTracker não foi encontrado no caminho: " + Configuracao.PegaCaminhoATM() + "rt.exe", true);

            // Depois de executar o RemoteTracker para executar o comando ALOG, agenda para nova execução no mês subsequente.
            ATM.AgendaProximoEnvioLog();

            Debug.SaveLog();
            Debug.EndLog();
        }
    }
}

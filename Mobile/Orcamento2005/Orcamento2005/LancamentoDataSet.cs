using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Globalization;

namespace Orcamento2005
{
    /// <summary>
    /// Summary description for CentroCustoDataSet.
    /// </summary>
    public class LancamentoDataSet
    {
      private SQLCEGeraCodigo geraCodigo;
      private DataTable dataTable;
      private DataSet dataSet;
      private OrcamentoDataBase db;
      private ContaDataSet contaDataSet;
      private TipoMovimDataSet tipoMovimDataSet;
      private DataSet centroCusto;
      private DataSet tipoMovim;
      private DataSet conta;

      public DataSet DataSet {
          get {
              return dataSet;
          }
      }
      public DataTable DataTable {
          get {
              return dataTable;
          }
      }
      public DataSet Conta
      {
        get
        {
          return conta;
        }
      }
      public DataSet CentroCusto
      {
        get
        {
          return centroCusto;
        }
      }
      public DataSet TipoMovim
      {
        get
        {
          return tipoMovim;
        }
      }
      
      public LancamentoDataSet(OrcamentoDataBase orcamentoDataBase)
      {
        db = orcamentoDataBase;
          geraCodigo = new SQLCEGeraCodigo(db.SQLServerCEDataBase);
          contaDataSet = new ContaDataSet(db);
          tipoMovimDataSet = new TipoMovimDataSet(db);

        db.SQLServerCEDataBase.OpenSQL(
          "select cdcentro, decentro from centrocusto " +
          " where flforauso = 'N' and flOperacao <> 'D' order by decentro", out centroCusto);

        db.SQLServerCEDataBase.OpenSQL(
          "select cdtipomovim, detipomovim from tipomovim " +
          " where flnatureza = 'D' and flforauso = 'N' and flOperacao <> 'D' order by detipomovim", out tipoMovim);
          
        db.SQLServerCEDataBase.OpenSQL(
          "select cdconta, deconta, cdtipoconta from conta " +
          " where flforauso = 'N' and flOperacao <> 'D' order by deconta", out conta);
      }

        public void AddLancamento(
            string cdlancamento,
            string delancamento,
            DateTime dtlancamento,
       			string cdtipomovim,
			      string cdcentrocusto,
			      Decimal vllancamento,
            string cdconta,
            DateTime dtquitacao,
            string cdcontafixa,
            string cdcreditofixo,
            string cdprestacao,
            int nuparcela,
            Decimal vljuros,
            Decimal vldesconto,
            string nucheque)
        {
            if (dataTable != null)
            {
                DataRow myRow;
                myRow = dataTable.NewRow();
                myRow["cdlancamento" ] = cdlancamento;
                myRow["delancamento" ] = delancamento;
                myRow["dtlancamento" ] = dtlancamento;
                myRow["cdtipomovim"  ] = cdtipomovim;
                myRow["cdcentrocusto"] = cdcentrocusto;
                myRow["vllancamento" ] = vllancamento;
                myRow["cdconta"      ] = cdconta;
                myRow["dtquitacao"   ] = dtquitacao;
                myRow["cdcontafixa"  ] = cdcontafixa;
                myRow["cdcreditofixo"] = cdcreditofixo;
                myRow["cdprestacao"  ] = cdprestacao;
                myRow["nuparcela"    ] = nuparcela;
                myRow["vljuros"      ] = vljuros;
                myRow["vldesconto"   ] = vldesconto;
                myRow["nucheque"     ] = nucheque;

                dataSet.Tables[0].Rows.Add(myRow);
                dataTable.AcceptChanges();
            }

            SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "insert into lancamento " +
                "  (cdlancamento, delancamento, dtlancamento, cdtipomovim, " +
                "   cdcentrocusto, vllancamento, floperacao, dtoperacao, cdconta, " +
                "   cdcontafixa, cdcreditofixo, cdprestacao, " +
                "   nuparcela, vljuros, vldesconto, nucheque ";

            if (dtquitacao.ToString("dd/MM/yyyy") != "31/12/3000") {
                command.CommandText += ",dtquitacao";
            }

            command.CommandText +=
                ") values " +
                "  (@cdlancamento, @delancamento, @dtlancamento, @cdtipomovim, " +
                "   @cdcentrocusto, @vllancamento, 'I', GetDate(), @cdconta, " +
                "   @cdcontafixa, @cdcreditofixo, @cdprestacao, " +
                "   @nuparcela, @vljuros, @vldesconto, @nucheque";

            if (dtquitacao.ToString("dd/MM/yyyy") != "31/12/3000") {
                command.CommandText += ",@dtquitacao";
            }

            command.CommandText += ")";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@cdlancamento", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@delancamento", SqlDbType.NVarChar, 30);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@dtlancamento", SqlDbType.DateTime);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdtipomovim", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdcentrocusto", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@vllancamento", SqlDbType.Decimal);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdconta", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdcontafixa", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdcreditofixo", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdprestacao", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@nuparcela", SqlDbType.Int);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@vljuros", SqlDbType.Decimal);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@vldesconto", SqlDbType.Decimal);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@nucheque", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);

            if (dtquitacao.ToString("dd/MM/yyyy") != "31/12/3000") {
              param = new SqlCeParameter("@dtquitacao", SqlDbType.DateTime);
              command.Parameters.Add(param);
            }

            command.Parameters["@cdlancamento" ].Size = 10;
            command.Parameters["@delancamento" ].Size = 30;
            command.Parameters["@cdtipomovim"  ].Size = 5;
            command.Parameters["@cdcentrocusto"].Size = 5;
            command.Parameters["@cdconta"      ].Size = 5;
            command.Parameters["@cdcontafixa"  ].Size = 10;
            command.Parameters["@cdcreditofixo"].Size = 10;
            command.Parameters["@cdprestacao"  ].Size = 10;
            command.Parameters["@nucheque"     ].Size = 10;

            command.Parameters[0].Value = cdlancamento;
            command.Parameters[1].Value = delancamento;
            command.Parameters[2].Value = dtlancamento;
            command.Parameters[3].Value = cdtipomovim;
            command.Parameters[4].Value = cdcentrocusto;
            command.Parameters[5].Value = vllancamento;
            command.Parameters[6].Value = cdconta;
            command.Parameters[7].Value = cdcontafixa;
            command.Parameters[8].Value = cdcreditofixo;
            command.Parameters[9].Value = cdprestacao;
            command.Parameters[10].Value = nuparcela;
            command.Parameters[11].Value = vljuros;
            command.Parameters[12].Value = vldesconto;
            command.Parameters[13].Value = nucheque;

            if (dtquitacao.ToString("dd/MM/yyyy") != "31/12/3000") {
                command.Parameters[14].Value = dtquitacao;
            }
            
            db.SQLServerCEDataBase.ExecSQL(command);

            // Se o lançamento estiver quitado, então eu tenho que lançar o valor no saldo da conta
            if (dtquitacao.ToString("dd/MM/yyyy") != "31/12/3000") {
              // Se o lançamento é um débito, então tenho que retirar da conta,
              // senão, tenho que acrescentar na conta
              if (tipoMovimDataSet.Natureza(cdtipomovim) == "D") {
                contaDataSet.SubtraiSaldoConta(cdconta, vllancamento);
              } else {
                contaDataSet.AdicionaSaldoConta(cdconta, vllancamento);
              }
            }        
        }

        public void AltLancamento(int linha,
            string cdlancamento,
            string delancamento,
            DateTime dtlancamento,
            string cdtipomovim,
            string cdcentrocusto,
            Decimal vllancamento,
            string cdconta,
            DateTime dtquitacao,
            string cdcontafixa,
            string cdcreditofixo,
            string cdprestacao,
            int nuparcela,
            Decimal vljuros,
            Decimal vldesconto,
            string nucheque)
        {
            if (dataTable != null)
            {
                DataRow myRow;
                myRow = dataTable.Rows[linha];
                myRow["cdlancamento" ] = cdlancamento;
                myRow["delancamento" ] = delancamento;
                myRow["dtlancamento" ] = dtlancamento;
                myRow["cdtipomovim"  ] = cdtipomovim;
                myRow["cdcentrocusto"] = cdcentrocusto;
                myRow["vllancamento" ] = vllancamento;
                myRow["cdconta"      ] = cdconta;
                myRow["dtquitacao"   ] = dtquitacao;
                myRow["cdcontafixa"  ] = cdcontafixa;
                myRow["cdcreditofixo"] = cdcreditofixo;
                myRow["cdprestacao"  ] = cdprestacao;
                myRow["nuparcela"    ] = nuparcela;
                myRow["vljuros"      ] = vljuros;
                myRow["vldesconto"   ] = vldesconto;
                myRow["nucheque"     ] = nucheque;
                dataTable.AcceptChanges();
            }

            // Se o lançamento estiver quitado, descobre o valor anterior para poder corrigir
            // o saldo da conta
            Decimal vlAnterior = 0;
            String conta = "";
            String sql = "";
            if (dtquitacao.ToString("dd/MM/yyyy") != "31/12/3000") {
                sql = "select l.vlLancamento, l.cdconta, tm.flnatureza from lancamento l, tipomovim tm" +
                      " where l.cdtipomovim = tp.cdtipomovim and l.cdLancamento = '" + cdlancamento + "'";
                SqlCeDataReader myReader = null;

                db.SQLServerCEDataBase.OpenSQL(sql, out myReader);

                if (myReader != null)
                {
                    if (myReader.Read())
                    {
                        // Ok, achei a conta do lançamento e seu valor
                        vlAnterior = System.Convert.ToDecimal(myReader["vlLancamento"]);
                        conta = System.Convert.ToString(myReader["cdConta"]);

                        myReader.Close();

                      // Agora remove esse valor da conta que foi lançada.
                      // Se o lançamento anterior era um crédito, então tenho que retirar da conta,
                      // senão, tenho que acrescentar na conta
                      if (System.Convert.ToString(myReader["flnatureza"]) == "C") {
                        contaDataSet.SubtraiSaldoConta(conta, vlAnterior);
                      } else {
                        contaDataSet.AdicionaSaldoConta(conta, vlAnterior);
                      }
                    }
                }
            }
                
            SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "update lancamento set " +
                "       delancamento=@delancamento, " +
                "       dtlancamento=@dtlancamento, " +
                "       cdtipomovim=@cdtipomovim, " +
                "       cdcentrocusto=@cdcentrocusto, " +
                "       vllancamento=@vllancamento, " +
                "       floperacao='A', " +
                "       dtoperacao=GetDate(), " +
                "       cdconta=@cdconta, ";
            if (dtquitacao.ToString("dd/MM/yyyy") != "31/12/3000") {
                command.CommandText +=
                "       dtquitacao=@dtquitacao, ";
            }

            command.CommandText +=
                "       cdcontafixa=@cdcontafixa, " +
                "       cdcreditofixo=@cdcreditofixo, " +
                "       cdprestacao=@cdprestacao, " +
                "       nuparcela=@nuparcela, " +
                "       vljuros=@vljuros, " +
                "       vldesconto=@vldesconto, " +
                "       nucheque=@nucheque " +
                " where cdlancamento=@cdlancamento";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@cdlancamento", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@delancamento", SqlDbType.NVarChar, 30);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@dtlancamento", SqlDbType.DateTime);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdtipomovim", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdcentrocusto", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@vllancamento", SqlDbType.Decimal);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdconta", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdcontafixa", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdcreditofixo", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@cdprestacao", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@nuparcela", SqlDbType.Int);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@vljuros", SqlDbType.Decimal);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@vldesconto", SqlDbType.Decimal);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@nucheque", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);

            if (dtquitacao.ToString("dd/MM/yyyy") != "31/12/3000")
            {
                param = new SqlCeParameter("@dtquitacao", SqlDbType.DateTime);
                command.Parameters.Add(param);
            }

            command.Parameters["@cdlancamento" ].Size = 10;
            command.Parameters["@delancamento" ].Size = 30;
            command.Parameters["@cdtipomovim"  ].Size = 5;
            command.Parameters["@cdcentrocusto"].Size = 5;
            command.Parameters["@cdconta"      ].Size = 5;
            command.Parameters["@cdcontafixa"  ].Size = 10;
            command.Parameters["@cdcreditofixo"].Size = 10;
            command.Parameters["@cdprestacao"  ].Size = 10;
            command.Parameters["@nucheque"     ].Size = 10;

            command.Parameters[0].Value = cdlancamento;
            command.Parameters[1].Value = delancamento;
            command.Parameters[2].Value = dtlancamento;
            command.Parameters[3].Value = cdtipomovim;
            command.Parameters[4].Value = cdcentrocusto;
            command.Parameters[5].Value = vllancamento;
            command.Parameters[6].Value = cdconta;
            command.Parameters[7].Value = cdcontafixa;
            command.Parameters[8].Value = cdcreditofixo;
            command.Parameters[9].Value = cdprestacao;
            command.Parameters[10].Value = nuparcela;
            command.Parameters[11].Value = vljuros;
            command.Parameters[12].Value = vldesconto;
            command.Parameters[13].Value = nucheque;

            if (dtquitacao.ToString("dd/MM/yyyy") != "31/12/3000") {
                command.Parameters[14].Value = dtquitacao;
            }

            db.SQLServerCEDataBase.ExecSQL(command);

            // Se o lançamento estiver quitado, então eu tenho que lançar o valor no saldo da conta
            if (dtquitacao.ToString("dd/MM/yyyy") != "31/12/3000") {
              // Se o lançamento é um débito, então tenho que retirar da conta,
              // senão, tenho que acrescentar na conta
              if (tipoMovimDataSet.Natureza(cdtipomovim) == "D") {
                contaDataSet.SubtraiSaldoConta(cdconta, vlAnterior);
              } else {
                contaDataSet.AdicionaSaldoConta(cdconta, vlAnterior);
              }
            }
        }

        public void DelLancamento(int linha)
        {
            DataRow myRow;
            myRow = dataTable.Rows[linha];

            String sql = "";
            
            // Se este lançamento estiver quitado, então tenho que remover o valor do saldo
            // da conta onde o lançamento foi realizado
            if (System.Convert.ToString(myRow["dtquitacao"]) != "31/12/3000") {
              sql = "select l.vlLancamento, l.cdconta, tm.flnatureza from lancamento l, tipomovim tm" +
                    " where l.cdtipomovim = tp.cdtipomovim and l.cdLancamento = '" + myRow["cdlancamento"] + "'";
                SqlCeDataReader myReader = null;

                db.SQLServerCEDataBase.OpenSQL(sql, out myReader);

                if (myReader != null) {
                    if (myReader.Read()) {
                        // Ok, achei a conta do lançamento e seu valor
                        Decimal vlAnterior = System.Convert.ToDecimal(myReader["vlLancamento"]);
                        String conta = System.Convert.ToString(myReader["cdConta"]);
                        myReader.Close();

                        // Se o lançamento anterior era um crédito, então tenho que retirar da conta,
                        // senão, tenho que acrescentar na conta
                        if (System.Convert.ToString(myReader["flnatureza"]) == "C") {
                            contaDataSet.SubtraiSaldoConta(conta, vlAnterior);
                        } else {
                            contaDataSet.AdicionaSaldoConta(conta, vlAnterior);
                        }
                    }
                }
            }
            
            sql = "update lancamento set flOperacao = 'D', dtoperacao=GETDATE() " +
                  " where cdlancamento = '" + myRow["cdlancamento"] + "'";

            db.SQLServerCEDataBase.ExecSQL(sql);

            myRow.Delete();
            dataTable.AcceptChanges();
        }

        public String GeraCodigo() {
            return geraCodigo.GeraCodigo("lancamento", "cdlancamento");
        }

        public void SelectPrestacao (String cdPrestacao)
        {
            String sql2 = "select cdlancamento, dtlancamento, vllancamento, " +
                          "       dtquitacao, cdprestacao, nuparcela, nucheque " +
                          "  from lancamento " +
                          " where cdprestacao= '" +cdPrestacao + "' " +
                          "   and flOperacao <> 'D' " +
                          " order by nuparcela";

            db.SQLServerCEDataBase.OpenSQL(sql2, out dataSet);

            if (dataTable != null) {
                dataTable = dataSet.Tables[0];
            }
        }

        public Decimal QuantidadePrestacoesPagas(String cdPrestacao)
        {
            String sql2 = "select count(*) as total from lancamento " +
                          " where cdprestacao= '" + cdPrestacao + "' " +
                          "   and dtquitacao is not null and flOperacao <> 'D'";
            SqlCeDataReader myReader = null;
            Decimal result = -1;

            db.SQLServerCEDataBase.OpenSQL(sql2, out myReader);

            if (myReader != null) {
                if (myReader.Read()) {
                    result = System.Convert.ToDecimal (myReader ["total"]);
                    myReader.Close();
                }
            }

            return result;
        }

        public void ExcluirPrestacoesExcedentes(String cdPrestacao, Decimal prestacoes)
        {
            String sql = "update lancamento " +
                         " set flOperacao = 'D', dtOperacao = getDate() " +
                         " where cdprestacao= '" + cdPrestacao + "' " +
                         "   and nuparcela > " + System.Convert.ToString(prestacoes);
            db.SQLServerCEDataBase.ExecSQL(sql);
        }
 
        public void ExcluirPrestacoesNaoPagas(String cdPrestacao)
        {
            String sql = "update lancamento " +
                         " set flOperacao = 'D', dtOperacao = getDate() " +
                         " where cdprestacao= '" + cdPrestacao + "' " +
                         "   and dtquitacao is null";
            db.SQLServerCEDataBase.ExecSQL(sql);
        }
       
      public string EncontraDeTipoMovim(string codigo)
        {
          for (int i = 0; i < tipoMovim.Tables[0].Rows.Count; i++)
          {
            if ((String)tipoMovim.Tables[0].Rows[i].ItemArray[0] == codigo)
            {
              return (String)tipoMovim.Tables[0].Rows[i].ItemArray[1];
            }
          }

          return "";
        }

      public string EncontraCdTipoMovim(string descricao)
        {
          for (int i = 0; i < tipoMovim.Tables[0].Rows.Count; i++)
          {
            if ((String)tipoMovim.Tables[0].Rows[i].ItemArray[1] == descricao)
            {
              return (String)tipoMovim.Tables[0].Rows[i].ItemArray[0];
            }
          }

          return "";
        }

      public string EncontraDeCentroCusto(string codigo)
        {
          for (int i = 0; i < centroCusto.Tables[0].Rows.Count; i++)
          {
            if ((String)centroCusto.Tables[0].Rows[i].ItemArray[0] == codigo)
            {
              return (String)centroCusto.Tables[0].Rows[i].ItemArray[1];
            }
          }

          return "";
        }

      public string EncontraCdCentroCusto(string descricao)
        {
          for (int i = 0; i < centroCusto.Tables[0].Rows.Count; i++)
          {
            if ((String)centroCusto.Tables[0].Rows[i].ItemArray[1] == descricao)
            {
              return (String)centroCusto.Tables[0].Rows[i].ItemArray[0];
            }
          }

          return "";
        }

      public string EncontraDeConta(string codigo)
        {
          for (int i = 0; i < conta.Tables[0].Rows.Count; i++)
          {
            if ((String)conta.Tables[0].Rows[i].ItemArray[0] == codigo)
            {
              return (String)conta.Tables[0].Rows[i].ItemArray[1];
            }
          }

          return "";
        }

      public string EncontraCdConta(string descricao)
        {
          for (int i = 0; i < conta.Tables[0].Rows.Count; i++) {
            if ((String)conta.Tables[0].Rows[i].ItemArray[1] == descricao) {
              return (String)conta.Tables[0].Rows[i].ItemArray[0];
            }
          }

          return "";
        }

      public DataSet Consulta(
        String cdConta, 
        String cdCentro,
        String cdMovimentacao,
        String deLancamento,
        String nuCheque,
        DateTime dtInicial,
        DateTime dtFinal,
        DateTime dtInicialQuit,
        DateTime dtFinalQuit,
        Decimal vlInicial,
        Decimal vlFinal,
        Boolean flSomenteComJuros,
        Boolean flSomenteComDesconto,
        String flTipoAgrupamento,
        Boolean flOrdenacaoDescendente) 
      {        
        SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

        // TODO - Resolver outerjoin com Centro de Custo

        command.CommandText =
            "select l.*, tm.cdTipoMovim, tm.flNatureza, c.deConta, cc.deCentro " +
            "  from Lancamento l, TipoMovim tm, CentroCusto cc, Conta C " +
            " where l.cdConta = c.cdConta " +
            "   and l.cdCentroCusto = cc.cdCentro " +
            "   and l.cdTipoMovim = tm.cdTipoMovim ";

        if (cdConta != "") {
          command.CommandText += " and l.cdConta='"+cdConta+"' ";
        }

        if (cdCentro != "") {
          command.CommandText += " and l.cdCentroCusto='" + cdCentro + "' ";
        }

        if (cdMovimentacao != "") {
          command.CommandText += " and l.cdTipoMovim='" + cdMovimentacao + "' ";
        }

        if (deLancamento != "") {
          command.CommandText += " and lower(l.deLancamento) like'%" + deLancamento.ToLower() + "%' ";
        }

        if (nuCheque != "") {
          command.CommandText += " and l.nuCheque ='" + nuCheque + "' ";
        }

        if (dtInicial.ToString("dd/MM/yyyy") != "31/12/3000") {
          command.CommandText += " and l.dtLancamento >= @dtInicial ";
        }

        if (dtFinal.ToString("dd/MM/yyyy") != "31/12/3000") {
          command.CommandText += " and l.dtLancamento <= @dtFinal ";
        }

        if (dtInicialQuit.ToString("dd/MM/yyyy") != "31/12/3000") {
          command.CommandText += " and l.dtQuitacao >= @dtInicialQuit ";
        }

        if (dtFinalQuit.ToString("dd/MM/yyyy") != "31/12/3000") {
          command.CommandText += " and l.dtQuitacao <= @dtFinalQuit ";
        }

        if (vlInicial > 0) {
           command.CommandText += " and l.vlLancamento >= @vlInicial ";
        }

        if (vlFinal > 0) {
           command.CommandText += " and l.vlLancamento <= @vlFinal ";
        }

        if (flSomenteComJuros) {
           command.CommandText += " and l.vlJuros > 0 ";
        }

        if (flSomenteComDesconto) {
           command.CommandText += " and l.vlDesconto > 0 ";
        }

        if (flTipoAgrupamento == "N") { // Nenhum agrupamento
          command.CommandText += " order by l.dtLancamento ";
        } else if (flTipoAgrupamento == "C") { // Agrupado por centro de custo
          command.CommandText += " order by cc.deCentro, l.dtLancamento ";
        } else if (flTipoAgrupamento == "M") { // Agrupado por Tipo de Movimentação
          command.CommandText += " order by tm.deTipoMovim, l.dtLancamento ";
        }

        if (flOrdenacaoDescendente) {
           command.CommandText += " desc ";
        }

        SqlCeParameter param = null;

        if (dtInicial.ToString("dd/MM/yyyy") != "31/12/3000") {
          param = new SqlCeParameter("@dtInicial", SqlDbType.DateTime);
          command.Parameters.Add(param);
          command.Parameters[command.Parameters.Count - 1].Value = dtInicial;
        }

        if (dtFinal.ToString("dd/MM/yyyy") != "31/12/3000") {
          param = new SqlCeParameter("@dtFinal", SqlDbType.DateTime);
          command.Parameters.Add(param);
          command.Parameters[command.Parameters.Count - 1].Value = dtFinal;
        }

        if (dtInicialQuit.ToString("dd/MM/yyyy") != "31/12/3000") {
          param = new SqlCeParameter("@dtInicialQuit", SqlDbType.DateTime);
          command.Parameters.Add(param);
          command.Parameters[command.Parameters.Count - 1].Value = dtInicialQuit;
        }

        if (dtFinalQuit.ToString("dd/MM/yyyy") != "31/12/3000") {
          param = new SqlCeParameter("@dtFinalQuit", SqlDbType.DateTime);
          command.Parameters.Add(param);
          command.Parameters[command.Parameters.Count - 1].Value = dtFinalQuit;
        }

        if (vlInicial > 0) {
          param = new SqlCeParameter("@vlInicial", SqlDbType.Decimal);
          command.Parameters.Add(param);
          command.Parameters[command.Parameters.Count - 1].Value = vlInicial;
        }

        if (vlFinal > 0) {
          param = new SqlCeParameter("@vlFinal", SqlDbType.Decimal);
          command.Parameters.Add(param);
          command.Parameters[command.Parameters.Count - 1].Value = vlFinal;
        }

        SqlCeDataAdapter da = new SqlCeDataAdapter(command);
        DataSet ds = new DataSet();
        da.Fill(ds);
        command.Dispose();
        
        return ds;
      }
    }
}
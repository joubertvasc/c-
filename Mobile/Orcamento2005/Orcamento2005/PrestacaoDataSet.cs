using System;
using System.Data;
using System.Globalization;
using System.Data.SqlTypes;
using System.Data.SqlServerCe;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Orcamento2005
{
    /// <summary>
    /// Summary description for ContaDataSet.
    /// </summary>
    public class PrestacaoDataSet
    {
        private SQLCEGeraCodigo geraCodigo;
        private DataTable dataTable;
        private DataSet dataSet;
        private OrcamentoDataBase db;
        private DataSet centroCusto;
        private DataSet tipoMovim;

        public DataSet DataSet
        {
            get
            {
                return dataSet;
            }
        }

        public DataTable DataTable
        {
            get
            {
                return dataTable;
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

        public PrestacaoDataSet(OrcamentoDataBase orcamentoDataBase)
        {
            db = orcamentoDataBase;
            geraCodigo = new SQLCEGeraCodigo(db.SQLServerCEDataBase);

            db.SQLServerCEDataBase.OpenSQL(
              "select cdcentro, decentro from centrocusto " +
              " where flforauso = 'N' order by decentro", out centroCusto);

            db.SQLServerCEDataBase.OpenSQL(
              "select cdtipomovim, detipomovim from tipomovim " +
              " where flnatureza = 'D' and flforauso = 'N' order by detipomovim", out tipoMovim);
        }

        public void AddPrestacao(
            string codigo,
            string descricao,
            string cdTipoMovim,
            string cdCentroCusto,
            decimal nuParcelas,
            decimal vlPrestacao,
            decimal vlParcela,
            decimal nuDiaVenc,
            DateTime dtAquisicao,
            DateTime dtPrimeira)
        {
            DataRow myRow;
            myRow = dataTable.NewRow();
            myRow["cdprestacao"  ] = codigo;
            myRow["deprestacao"  ] = descricao;
            myRow["cdtipomovim"  ] = cdTipoMovim;
            myRow["cdcentrocusto"] = cdCentroCusto;
            myRow["nuparcelas"   ] = nuParcelas;
            myRow["vlprestacao"  ] = vlPrestacao;
            myRow["nudiavenc"    ] = nuDiaVenc;
            myRow["dtaquisicao"  ] = dtAquisicao;
            myRow["dtprimeira"   ] = dtPrimeira;
            myRow["faltantes"    ] = nuParcelas;
            myRow["vlparcela"    ] = vlParcela;

            dataSet.Tables[0].Rows.Add(myRow);
            dataTable.AcceptChanges();

            SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "insert into prestacoes (cdprestacao, deprestacao, cdtipomovim, " +
                "                       cdcentrocusto, nuparcelas, vlprestacao, " +
                "                       dtaquisicao, dtprimeira, nudiavenc, floperacao, " +
                "                       dtoperacao, vlparcela) " +
                " values (@codigo, @descricao, @cdTipoMovim, @cdCentroCusto, " +
                "         @nuParcelas, @vlPrestacao, @dtaquisicao, @dtprimeira, " +
                "         @nudiavenc, 'I', GETDATE(), @vlparcela)";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@codigo", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@descricao", SqlDbType.NVarChar, 30);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@cdTipoMovim", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@cdCentroCusto", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@nuParcelas", SqlDbType.Int);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@vlPrestacao", SqlDbType.Float);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@dtaquisicao", SqlDbType.DateTime);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@dtprimeira", SqlDbType.DateTime);
            command.Parameters.Add(param);
            
            param = new SqlCeParameter("@nuDiaVenc", SqlDbType.Int);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@vlParcela", SqlDbType.Float);
            command.Parameters.Add(param);

            command.Parameters["@codigo"].Size = 5;
            command.Parameters["@descricao"].Size = 20;
            command.Parameters["@cdTipoMovim"].Size = 5;
            command.Parameters["@cdCentroCusto"].Size = 5;

            command.Parameters[0].Value = codigo;
            command.Parameters[1].Value = descricao;
            command.Parameters[2].Value = cdTipoMovim;
            command.Parameters[3].Value = cdCentroCusto;
            command.Parameters[4].Value = nuParcelas;
            command.Parameters[5].Value = vlPrestacao;
            command.Parameters[6].Value = dtAquisicao;
            command.Parameters[7].Value = dtPrimeira;
            command.Parameters[8].Value = nuDiaVenc;
            command.Parameters[9].Value = vlParcela;

            db.SQLServerCEDataBase.ExecSQL(command);
        }

        public void AltPrestacao(int linha,
            string codigo,
            string descricao,
            string cdTipoMovim,
            string cdCentroCusto,
            decimal nuParcelas,
            decimal vlPrestacao,
            decimal vlParcela,
            decimal nuDiaVenc,
            DateTime dtAquisicao,
            DateTime dtPrimeira,
            decimal nuRestantes)
        {
            DataRow myRow;
            myRow = dataTable.Rows[linha];
            myRow["cdprestacao"  ] = codigo;
            myRow["deprestacao"  ] = descricao;
            myRow["cdtipomovim"  ] = cdTipoMovim;
            myRow["cdcentrocusto"] = cdCentroCusto;
            myRow["nuparcelas"   ] = nuParcelas;
            myRow["vlprestacao"  ] = vlPrestacao;
            myRow["nudiavenc"    ] = nuDiaVenc;
            myRow["dtaquisicao"  ] = dtAquisicao;
            myRow["dtprimeira"   ] = dtPrimeira;
            myRow["faltantes"    ] = nuRestantes;
            myRow["vlparcela"    ] = vlParcela;

            dataTable.AcceptChanges();

            SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "update prestacoes set " +
                " deprestacao=@descricao, " +
                " cdtipomovim = @cdTipoMovim, " +
                " cdcentrocusto = @cdCentroCusto, " +
                " nuparcelas = @nuParcelas, " +
                " vlprestacao = @vlPrestacao, " +
                " vlparcela = @vlparcela, " +
                " dtaquisicao = @dtAquisicao, " +
                " dtprimeira = @dtPrimeira, " +
                " nudiavenc = @nuDiaVenc, " +
                " floperacao='A', dtoperacao=getdate()" +
                " where cdprestacao = @codigo";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@codigo", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@descricao", SqlDbType.NVarChar, 30);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@cdTipoMovim", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@cdCentroCusto", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@nuParcelas", SqlDbType.Int);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@vlPrestacao", SqlDbType.Float);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@dtaquisicao", SqlDbType.DateTime);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@dtprimeira", SqlDbType.DateTime);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@nuDiaVenc", SqlDbType.Int);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@vlParcela", SqlDbType.Float);
            command.Parameters.Add(param);

            command.Parameters["@codigo"].Size = 5;
            command.Parameters["@descricao"].Size = 20;
            command.Parameters["@cdTipoMovim"].Size = 5;
            command.Parameters["@cdCentroCusto"].Size = 5;

            command.Parameters[0].Value = codigo;
            command.Parameters[1].Value = descricao;
            command.Parameters[2].Value = cdTipoMovim;
            command.Parameters[3].Value = cdCentroCusto;
            command.Parameters[4].Value = nuParcelas;
            command.Parameters[5].Value = vlPrestacao;
            command.Parameters[6].Value = dtAquisicao;
            command.Parameters[7].Value = dtPrimeira;
            command.Parameters[8].Value = nuDiaVenc;
            command.Parameters[9].Value = vlParcela;

            db.SQLServerCEDataBase.ExecSQL(command);
        }

        public void DelPrestacao(int linha)
        {
            DataRow myRow;
            myRow = dataTable.Rows[linha];

            String sql =
                "update prestacoes set flOperacao = 'D', dtoperacao=GETDATE() " +
                " where cdprestacao = '" + myRow["cdprestacao"] + "'";

            db.SQLServerCEDataBase.ExecSQL(sql);

            myRow.Delete();
            dataTable.AcceptChanges();
        }

        public String GeraCodigo()
        {
            return geraCodigo.GeraCodigo("Prestacao", "cdprestacao");
        }

        public void SelectAll(
            Boolean filtrarForaUso,
            Boolean filtrarDeletados,
            String campoOrdenacao,
            Boolean ordemCrescente,
            Boolean somenteOsAtivos)
        {
            String sql2 = "";

            if (somenteOsAtivos) {
                sql2 = "select p.cdprestacao, p.deprestacao, p.cdtipomovim, " +
                      "       p.cdcentrocusto, p.nuparcelas, p.vlprestacao, " +
                      "       p.dtaquisicao, p.dtprimeira, p.nudiavenc, p.vlparcela, " +
                      "       tm.detipomovim, cc.decentro, count(*) as faltantes " +
                      "  from prestacoes p, tipomovim tm, centrocusto cc, lancamento l " +
                      " where p.cdprestacao = l.cdprestacao and l.dtquitacao is null " +
                      "   and p.cdtipomovim = tm.cdtipomovim " +
                      "   and p.cdcentrocusto = cc.cdcentro ";

                if (filtrarDeletados)
                {
                    sql2 += " and p.floperacao <> 'D'";
                }

                sql2 +=
                      " group by p.cdprestacao, p.deprestacao, p.cdtipomovim, " +
                      "          p.cdcentrocusto, p.nuparcelas, p.vlprestacao, " +
                      "          p.dtaquisicao, p.dtprimeira, p.nudiavenc, p.vlparcela, " +
                      "          tm.detipomovim, cc.decentro";
            } else {
                sql2 = "select p.cdprestacao, p.deprestacao, p.cdtipomovim, " +
                      "       p.cdcentrocusto, p.nuparcelas, p.vlprestacao, " +
                      "       p.dtaquisicao, p.dtprimeira, p.nudiavenc, p.vlparcela, " +
                      "       tm.detipomovim, cc.decentro, 0 as faltantes " +
                      "  from prestacoes p, tipomovim tm, centrocusto cc " +
                      " where p.cdtipomovim = tm.cdtipomovim " +
                      "   and p.cdcentrocusto = cc.cdcentro";

                if (filtrarDeletados)
                {
                    sql2 += " and p.floperacao <> 'D'";
                }
            }

            if (campoOrdenacao != "")
            {
                sql2 += " order by " + campoOrdenacao;

                if (!ordemCrescente)
                {
                    sql2 += " desc";
                }
            }

            db.SQLServerCEDataBase.OpenSQL(sql2, out dataSet);

            dataTable = dataSet.Tables[0];
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
    }
}

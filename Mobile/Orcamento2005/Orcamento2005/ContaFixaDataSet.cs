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
    public class ContaFixaDataSet
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

        public ContaFixaDataSet(OrcamentoDataBase orcamentoDataBase)
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

        public void AddContaFixa(
            string codigo,
            string descricao,
            string cdTipoMovim,
            string cdCentroCusto,
            decimal nuDiaVencimento,
            decimal vlConta,
            string foraUso)
        {
            DataRow myRow;
            myRow = dataTable.NewRow();
            myRow["cdcontafixa"] = codigo;
            myRow["decontafixa"] = descricao;
            myRow["cdtipomovim"] = cdTipoMovim;
            myRow["cdcentrocusto"] = cdCentroCusto;
            myRow["nudiavencimento"] = nuDiaVencimento;
            myRow["vlconta"] = vlConta;
            myRow["flforauso"] = foraUso;

            dataSet.Tables[0].Rows.Add(myRow);
            dataTable.AcceptChanges();

            SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "insert into contafixa (cdcontafixa, decontafixa, cdtipomovim, " +
                "                       cdcentrocusto, nudiavencimento, vlconta, " +
                "                       flforauso, floperacao, dtoperacao) " +
                " values (@codigo, @descricao, @cdTipoMovim, @cdCentroCusto, " + 
                "@nuDiaVencimento, @vlConta, @foraUso, 'I', GETDATE() )";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@codigo", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@descricao", SqlDbType.NVarChar, 30);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@cdTipoMovim", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@cdCentroCusto", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@nuDiaVencimento", SqlDbType.Int);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@vlConta", SqlDbType.Float);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@forauso", SqlDbType.NChar, 1);
            command.Parameters.Add(param);

            command.Parameters["@codigo"].Size = 5;
            command.Parameters["@descricao"].Size = 20;
            command.Parameters["@cdTipoMovim"].Size = 5;
            command.Parameters["@cdCentroCusto"].Size = 5;
            command.Parameters["@forauso"].Size = 1;

            command.Parameters[0].Value = codigo;
            command.Parameters[1].Value = descricao;
            command.Parameters[2].Value = cdTipoMovim;
            command.Parameters[3].Value = cdCentroCusto;
            command.Parameters[4].Value = nuDiaVencimento;
            command.Parameters[5].Value = vlConta;
            command.Parameters[6].Value = foraUso;

            db.SQLServerCEDataBase.ExecSQL(command);
        }

        public void AltContaFixa(int linha,
            string codigo,
            string descricao,
            string cdTipoMovim,
            string cdCentroCusto,
            decimal nuDiaVencimento,
            decimal vlConta,
            string foraUso)
        {
            DataRow myRow;
            myRow = dataTable.Rows[linha];
            myRow["cdcontafixa"] = codigo;
            myRow["decontafixa"] = descricao;
            myRow["cdtipomovim"] = cdTipoMovim;
            myRow["cdcentrocusto"] = cdCentroCusto;
            myRow["nudiavencimento"] = nuDiaVencimento;
            myRow["vlconta"] = vlConta;
            myRow["flforauso"] = foraUso;

            dataTable.AcceptChanges();

            SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "update contafixa set " +
                " decontafixa=@descricao, " +
                " cdtipomovim = @cdTipoMovim, " +
                " cdcentrocusto = @cdCentroCusto, " +
                " nudiavencimento = @nuDiaVencimento, " +
                " vlconta = @vlConta, " +
                " flforauso=@foraUso, " +
                " floperacao='A', dtoperacao=getdate()" +
                " where cdcontafixa = @codigo";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@codigo", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@descricao", SqlDbType.NVarChar, 30);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@cdTipoMovim", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@cdCentroCusto", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@nuDiaVencimento", SqlDbType.Int);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@vlConta", SqlDbType.Float);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@forauso", SqlDbType.NChar, 1);
            command.Parameters.Add(param);

            command.Parameters["@codigo"       ].Size = 5;
            command.Parameters["@descricao"    ].Size = 20;
            command.Parameters["@cdTipoMovim"  ].Size = 5;
            command.Parameters["@cdCentroCusto"].Size = 5;
            command.Parameters["@forauso"      ].Size = 1;

            command.Parameters[0].Value = codigo;
            command.Parameters[1].Value = descricao;
            command.Parameters[2].Value = cdTipoMovim;
            command.Parameters[3].Value = cdCentroCusto;
            command.Parameters[4].Value = nuDiaVencimento;
            command.Parameters[5].Value = vlConta;
            command.Parameters[6].Value = foraUso;

            db.SQLServerCEDataBase.ExecSQL(command);
        }

        public void DelContaFixa(int linha)
        {
            DataRow myRow;
            myRow = dataTable.Rows[linha];

            String sql =
                "update contafixa set flOperacao = 'D', dtoperacao=GETDATE() " +
                " where cdcontafixa = '" + myRow["cdconta"] + "'";

            db.SQLServerCEDataBase.ExecSQL(sql);

            myRow.Delete();
            dataTable.AcceptChanges();
        }

        public String GeraCodigo()
        {
            return geraCodigo.GeraCodigo("ContaFixa", "cdcontafixa");
        }

        public void SelectAll(
            Boolean filtrarForaUso,
            Boolean filtrarDeletados,
            String campoOrdenacao,
            Boolean ordemCrescente)
        {
            String sql = "select cf.cdcontafixa, cf.decontafixa, cf.cdtipomovim, " +
                "                cf.cdcentrocusto, cf.nudiavencimento, cf.vlconta, " +
                "                cf.flforauso, tm.detipomovim, cc.decentro " +
                " from contafixa cf, tipomovim tm, centrocusto cc " +
                " where cf.cdtipomovim = tm.cdtipomovim and cf.cdcentrocusto = cc.cdcentro";

            if (filtrarForaUso)
            {
                sql += " and cf.flforauso = 'N' ";
            }

            if (filtrarDeletados)
            {
                sql += " and cf.floperacao <> 'D'";
            }

            if (campoOrdenacao != "")
            {
                sql += " order by " + campoOrdenacao;

                if (!ordemCrescente)
                {
                    sql += " desc";
                }
            }

            db.SQLServerCEDataBase.OpenSQL(sql, out dataSet);

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

        public Boolean ExisteLancamento()
        {
            String sql = "select count(*) as total from contafixa";
            SqlCeDataReader myReader = null;
            Boolean result = false;

            db.SQLServerCEDataBase.OpenSQL(sql, out myReader);

            if (myReader != null) {
                if (myReader.Read()) {
                    result = System.Convert.ToDecimal(myReader["total"]) > 0;
                    myReader.Close();
                }
            }

            return result;
        }

        public SqlCeDataReader ContasAPagar()
        {
            String sql = "select cdcontafixa, decontafixa, cdtipomovim, cdcentrocusto, " + 
                         "       nudiavencimento, vlconta " +
                         "  from contafixa  where flForaUso = 'N' and flOperacao <> 'D'";
            SqlCeDataReader myReader = null;

            db.SQLServerCEDataBase.OpenSQL(sql, out myReader);

            return myReader;
        }
    }
}

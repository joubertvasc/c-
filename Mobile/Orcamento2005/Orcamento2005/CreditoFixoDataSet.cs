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
    public class CreditoFixoDataSet
    {
        private SQLCEGeraCodigo geraCodigo;
        private DataTable dataTable;
        private DataSet dataSet;
        private OrcamentoDataBase db;
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

        public DataSet TipoMovim
        {
            get
            {
                return tipoMovim;
            }
        }

        public CreditoFixoDataSet(OrcamentoDataBase orcamentoDataBase)
        {
            db = orcamentoDataBase;
            geraCodigo = new SQLCEGeraCodigo(db.SQLServerCEDataBase);

            db.SQLServerCEDataBase.OpenSQL(
              "select cdtipomovim, detipomovim from tipomovim " +
              " where flnatureza = 'C' and flforauso = 'N' order by detipomovim", out tipoMovim);
        }

        public void AddCreditoFixo(
            string codigo,
            string descricao,
            string cdTipoMovim,
            decimal nuDiaRecebimento,
            decimal vlCredito,
            string foraUso)
        {
            DataRow myRow;
            myRow = dataTable.NewRow();
            myRow["cdcreditofixo"] = codigo;
            myRow["decreditofixo"] = descricao;
            myRow["cdtipomovim"] = cdTipoMovim;
            myRow["nudiarecebimento"] = nuDiaRecebimento;
            myRow["vlcredito"] = vlCredito;
            myRow["flforauso"] = foraUso;

            dataSet.Tables[0].Rows.Add(myRow);
            dataTable.AcceptChanges();

            SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "insert into creditofixo (cdcreditofixo, decreditofixo, cdtipomovim, " +
                "                       nudiarecebimento, vlcredito, " +
                "                       flforauso, floperacao, dtoperacao) " +
                " values (@codigo, @descricao, @cdTipoMovim, " + 
                "@nuDiaRecebimento, @vlCredito, @foraUso, 'I', GETDATE() )";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@codigo", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@descricao", SqlDbType.NVarChar, 30);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@cdTipoMovim", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@nuDiaRecebimento", SqlDbType.Int);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@vlCredito", SqlDbType.Float);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@forauso", SqlDbType.NChar, 1);
            command.Parameters.Add(param);

            command.Parameters["@codigo"].Size = 5;
            command.Parameters["@descricao"].Size = 20;
            command.Parameters["@cdTipoMovim"].Size = 5;
            command.Parameters["@forauso"].Size = 1;

            command.Parameters[0].Value = codigo;
            command.Parameters[1].Value = descricao;
            command.Parameters[2].Value = cdTipoMovim;
            command.Parameters[3].Value = nuDiaRecebimento;
            command.Parameters[4].Value = vlCredito;
            command.Parameters[5].Value = foraUso;

            db.SQLServerCEDataBase.ExecSQL(command);
        }

        public void AltCreditoFixo(int linha,
            string codigo,
            string descricao,
            string cdTipoMovim,
            decimal nuDiaRecebimento,
            decimal vlCredito,
            string foraUso)
        {
            DataRow myRow;
            myRow = dataTable.Rows[linha];
            myRow["cdcreditofixo"] = codigo;
            myRow["decreditofixo"] = descricao;
            myRow["cdtipomovim"] = cdTipoMovim;
            myRow["nudiarecebimento"] = nuDiaRecebimento;
            myRow["vlcredito"] = vlCredito;
            myRow["flforauso"] = foraUso;

            dataTable.AcceptChanges();

            SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "update CreditoFixo set " +
                " decreditofixo=@descricao, " +
                " cdtipomovim = @cdTipoMovim, " +
                " nudiarecebimento = @nuDiaRecebimento, " +
                " vlcredito = @vlCredito, " +
                " flforauso=@foraUso, " +
                " floperacao='A', dtoperacao=getdate()" +
                " where cdcreditofixo = @codigo";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@codigo", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@descricao", SqlDbType.NVarChar, 30);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@cdTipoMovim", SqlDbType.NVarChar, 5);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@nuDiaRecebimento", SqlDbType.Int);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@vlCredito", SqlDbType.Float);
            command.Parameters.Add(param);

            param = new SqlCeParameter("@forauso", SqlDbType.NChar, 1);
            command.Parameters.Add(param);

            command.Parameters["@codigo"       ].Size = 5;
            command.Parameters["@descricao"    ].Size = 20;
            command.Parameters["@cdTipoMovim"  ].Size = 5;
            command.Parameters["@forauso"      ].Size = 1;

            command.Parameters[0].Value = codigo;
            command.Parameters[1].Value = descricao;
            command.Parameters[2].Value = cdTipoMovim;
            command.Parameters[3].Value = nuDiaRecebimento;
            command.Parameters[4].Value = vlCredito;
            command.Parameters[5].Value = foraUso;

            db.SQLServerCEDataBase.ExecSQL(command);
        }

        public void DelCreditoFixo(int linha)
        {
            DataRow myRow;
            myRow = dataTable.Rows[linha];

            String sql =
                "update CreditoFixo set flOperacao = 'D', dtoperacao=GETDATE() " +
                " where cdCreditoFixo = '" + myRow["cdcreditofixo"] + "'";

            db.SQLServerCEDataBase.ExecSQL(sql);

            myRow.Delete();
            dataTable.AcceptChanges();
        }

        public String GeraCodigo()
        {
            return geraCodigo.GeraCodigo("CreditoFixo", "cdcreditofixo");
        }

        public void SelectAll(
            Boolean filtrarForaUso,
            Boolean filtrarDeletados,
            String campoOrdenacao,
            Boolean ordemCrescente)
        {
            String sql = "select cf.cdcreditofixo, cf.decreditofixo, cf.cdtipomovim, " +
                "                cf.nudiarecebimento, cf.vlcredito, " +
                "                cf.flforauso, tm.detipomovim " +
                " from creditofixo cf, tipomovim tm" +
                " where cf.cdtipomovim = tm.cdtipomovim";

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

        public Boolean ExisteLancamento() {
            String sql = "select count(*) as total from creditofixo";
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

        public SqlCeDataReader ContasAReceber() {
            String sql = "select cdcreditofixo, decreditofixo, cdtipomovim, nudiarecebimento, " +
                         "       vlcredito, flforauso from creditofixo " +
                         " where flForaUso = 'N' and flOperacao <> 'D'";
            SqlCeDataReader myReader = null;

            db.SQLServerCEDataBase.OpenSQL(sql, out myReader);

            return myReader;
        }
    }
}

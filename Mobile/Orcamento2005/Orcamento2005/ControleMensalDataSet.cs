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
    class ControleMensalDataSet
    {
        private SQLCEGeraCodigo geraCodigo;
        private OrcamentoDataBase db;
        private LancamentoDataSet lancamentoDataSet;
        private CreditoFixoDataSet creditoFixoDataSet;
        private ContaFixaDataSet contaFixaDataSet;

        public ControleMensalDataSet(OrcamentoDataBase orcamentoDataBase) {
            db = orcamentoDataBase;
            geraCodigo = new SQLCEGeraCodigo(db.SQLServerCEDataBase);
            lancamentoDataSet = new LancamentoDataSet(db);
            contaFixaDataSet = new ContaFixaDataSet(db);
            creditoFixoDataSet = new CreditoFixoDataSet(db);
        }

        public Boolean JaFoiRealizadoLancamento(int mes, int ano)
        {
            String sql = "select count(*) as total from controlemensal " +
                         " where numes = " + System.Convert.ToString(mes) +
                         "   and nuano = " + System.Convert.ToString(ano);
            SqlCeDataReader myReader = null;
            Boolean result = false;

            db.SQLServerCEDataBase.OpenSQL(sql, out myReader);

            if (myReader != null) {
                if (myReader.Read()) {
                    result = System.Convert.ToDecimal(myReader["total"]) > 0;
                    myReader.Close();
                }
            }

            // Se não encontrou registros de lançamentos de contas fixas para o mês e ano,
            // então verifica se há ocorrências cadastradas para não questionar o usuário
            // inutilmente
            if (!result) {
                result = !(contaFixaDataSet.ExisteLancamento() || 
                           creditoFixoDataSet.ExisteLancamento());
            }

            return result;
        }

        public void RealizaLancamentos(int mes, int ano)
        {
            // Primeiro lança os créditos
            SqlCeDataReader myReader = creditoFixoDataSet.ContasAReceber();
            if (myReader != null) {
                while (myReader.Read()) {
                    lancamentoDataSet.AddLancamento(lancamentoDataSet.GeraCodigo(),
                        System.Convert.ToString(myReader["decreditofixo"]),
                        System.DateTime.Parse(System.Convert.ToDecimal(myReader["nudiarecebimento"]).ToString("00") + "/" +
                                              System.Convert.ToDecimal(mes).ToString("00") + "/" +
                                              System.Convert.ToDecimal(ano).ToString("00")),
                        System.Convert.ToString(myReader["cdtipomovim"]), "",
                        System.Convert.ToDecimal(myReader["vlcredito"]), "",
                        System.DateTime.Parse("31/12/3000"), "", 
                        System.Convert.ToString(myReader["cdcreditofixo"]), "", 0, 0, 0, "");
                }

                myReader.Close();
            }

            // Depois lanço os débitos
            myReader = contaFixaDataSet.ContasAPagar();
            if (myReader != null)
            {
                while (myReader.Read())
                {
                    lancamentoDataSet.AddLancamento(
                        lancamentoDataSet.GeraCodigo(),
                        System.Convert.ToString(myReader["decontafixa"]),
                        System.DateTime.Parse(System.Convert.ToDecimal(myReader["nudiavencimento"]).ToString("00") + "/" +
                                              System.Convert.ToDecimal(mes).ToString("00") + "/" +
                                              System.Convert.ToDecimal(ano).ToString("00")),
                        System.Convert.ToString(myReader["cdtipomovim"]), 
                        System.Convert.ToString(myReader["cdcentrocusto"]),
                        System.Convert.ToDecimal(myReader["vlconta"]), "",
                        System.DateTime.Parse("31/12/3000"), 
                        System.Convert.ToString(myReader["cdcontafixa"]), 
                        "", "", 0, 0, 0, "");
                }

                myReader.Close();
            }

            // Por último atualizo o controle
            AtualizaControle(mes, ano);
        }

        public void AtualizaControle(int mes, int ano) {
            SqlCeCommand command = db.SQLServerCEDataBase.SqlCeConnection.CreateCommand();

            command.CommandText =
                "insert into controlemensal " +
                "  (cdcontrole, numes, nuano) values " +
                "  (@cdcontrole, @numes, @nuano)";

            SqlCeParameter param = null;

            param = new SqlCeParameter("@cdcontrole", SqlDbType.NVarChar, 10);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@numes", SqlDbType.Int);
            command.Parameters.Add(param);
            param = new SqlCeParameter("@nuano", SqlDbType.Int);
            command.Parameters.Add(param);

            command.Parameters["@cdcontrole" ].Size = 10;

            command.Parameters[0].Value = this.GeraCodigo();
            command.Parameters[1].Value = mes;
            command.Parameters[2].Value = ano;

            db.SQLServerCEDataBase.ExecSQL(command);
        }

        public String GeraCodigo() {
            return geraCodigo.GeraCodigo("controlemensal", "cdcontrole");
        }
    }
}
